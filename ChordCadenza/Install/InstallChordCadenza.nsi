;NSIS script to install ChordCadenza on 64 or 32-bit systems

!define STARTERSOUNDFONT
;!define X32
!define LINK_NAME "Chord Cadenza"
!define PRODUCT_NAME "ChordCadenza"
;!define LINK_NAME "Chord Cadenza Test"
;!define PRODUCT_NAME "ChordCadenzaTest"

!define VSNAME "ChordCadenza"  ;same as visual studio
!define PRODUCT_PUBLISHER "ChordCadenza"
!define PRODUCT_WEB_SITE "http://www.chordcadenza.org"
!define  OUTDIR "D:\D0\Dev\ChCa\ChordCadenza.UWP\ChordCadenza\bin\Downloads"

!include LogicLib.nsh
!include "x64.nsh" 
!include DotNetChecker.nsh
;!include "MUI.nsh"
;!include WinMessages.nsh

!define PRODUCT_VERSION "0.0.0.0" 
;!define PRODUCT_VERSION "2.0.4.2" 
!define CFG_BUILD_DIR "D:\D0\Dev\ChCa\ChordCadenza.UWP\ChordCadenza\AppCfg"
!define BIN_BUILD_DIR "D:\D0\Dev\ChCa\ChordCadenza.UWP\ChordCadenza\bin"
!define BUILD_DIR_64  "D:\D0\Dev\ChCa\ChordCadenza.UWP\ChordCadenza\bin\Release X64"
!define BUILD_DIR_32  "D:\D0\Dev\ChCa\ChordCadenza.UWP\ChordCadenza\bin\Release"

!getdllversion "${BUILD_DIR_64}\${VSNAME}.exe" VS64V_
!getdllversion "${BUILD_DIR_32}\${VSNAME}.exe" VS32V_
!echo "VS64 version: ${VS64V_1}.${VS64V_2}.${VS64V_3}.${VS64V_4}"
!echo "VS32 version: ${VS32V_1}.${VS32V_2}.${VS32V_3}.${VS32V_4}"

!define XVS64VERSION "${VS64V_1}.${VS64V_2}.${VS64V_3}.${VS64V_4}"
!echo "XVS64VERSION is ${XVS64VERSION}"
!if ${XVS64VERSION} != ${PRODUCT_VERSION}
  !error "PRODUCT_VERSION: ${PRODUCT_VERSION} does not match 64-bit VS Version: ${XVS64VERSION}"
!endif 

!define XVS32VERSION "${VS32V_1}.${VS32V_2}.${VS32V_3}.${VS32V_4}"
!echo "XVS32VERSION is ${XVS32VERSION}"
!if ${XVS32VERSION} != ${PRODUCT_VERSION}
  !error "PRODUCT_VERSION: ${PRODUCT_VERSION} does not match 32-bit VS Version: ${XVS32VERSION}"
!endif 

LoadLanguageFile "${NSISDIR}\Contrib\Language files\English.nlf"  
VIProductVersion "${PRODUCT_VERSION}"
;VIFileVersion "${PRODUCT_VERSION}"
VIAddVersionKey /LANG=${LANG_ENGLISH} "LegalCopyright" "Copyright Derrick Maule 2018"
VIAddVersionKey /LANG=${LANG_ENGLISH} "FileDescription" "Chord Cadenza"
VIAddVersionKey /LANG=${LANG_ENGLISH} "FileVersion" "${PRODUCT_VERSION}"

!ifdef STARTERSOUNDFONT 
  !ifdef X32
    OutFile "${OUTDIR}\Setup_${PRODUCT_NAME}_${PRODUCT_VERSION}_32.exe"
  !else
    OutFile "${OUTDIR}\Setup_${PRODUCT_NAME}_${PRODUCT_VERSION}.exe"
  !endif
!else
  !ifdef X32
    OutFile "${OUTDIR}\Setup_${PRODUCT_NAME}_${PRODUCT_VERSION}_NoSoundFont_32.exe"
  !else
    OutFile "${OUTDIR}\Setup_${PRODUCT_NAME}_${PRODUCT_VERSION}_NoSoundFont.exe"
  !endif
!endif  

; Following constants you should never change
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

; Replace the constants below to suit your project
Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
LicenseData "License.txt"

;UninstallSubCaption 0 ": DGMConfirmation"
;UninstallSubCaption 1 ": DGMUninstalling Files"
;UninstallSubCaption 2 ": DGMCompleted"
UninstallText "Uninstall Chord Cadenza. Projects, samples, and soundfonts will not be uninstalled by this process."

ShowInstDetails show
ShowUnInstDetails show

Page license
Page components
Page directory
Page instfiles
UninstPage uninstConfirm
UninstPage instfiles

; Request application privileges for Windows Vista
RequestExecutionLevel admin

!macro Deinstall
  Delete "$INSTDIR\*.*"
  RMDir "$INSTDIR"
  
  ; Remove shortcuts, if any
  Delete "$SMPROGRAMS\${LINK_NAME}.lnk"
  Delete "$DESKTOP\${LINK_NAME}.lnk" 

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"

  SetAutoClose false
!macroend

/*
!macro Deinstall
  ExecWait  '"$INSTDIR\uninst.exe" _?=$INSTDIR'
!macroend
*/

!macro RemAppData
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON1 "Remove Configuration (.Ini) Files?$\r$\nClick 'NO' if you want to retain your settings for a new installation of Chord Cadenza." IDYES +1 IDNO RetainIni
  Delete "$APPDATA\${PRODUCT_NAME}\*.*"
  RMDir "$APPDATA\${PRODUCT_NAME}"
  Goto doneit
  
  RetainIni:
  Delete "$APPDATA\${PRODUCT_NAME}\*.dat"
  Delete "$APPDATA\${PRODUCT_NAME}\*.htm"
  Delete "$APPDATA\${PRODUCT_NAME}\*.chm"
  
  doneit:
!macroend

!macro CHECKMUTEX un
  Function ${un}CheckMutex
    System::Call 'kernel32::OpenMutex(i 0x100000, b 0, t "3c5cb8b3-77cb-4eef-8c6d-3fea869afeaa") i .R0'
    IntCmp $R0 0 notRunning
    System::Call 'kernel32::CloseHandle(i $R0)'
    MessageBox MB_OK|MB_ICONEXCLAMATION "${PRODUCT_NAME} is running. Please close it first" /SD IDOK
    Abort
    notRunning:
  FunctionEnd
!macroend

!insertmacro CHECKMUTEX ""
!insertmacro CHECKMUTEX "un."

!include "FileFunc.nsh"

; Following lists the files you want to include, go through this list carefully!
Section "Chord Cadenza" Desc_Chord_Cadenza
  ;All sections execute after Pages (license etc.) 
  ;Use Function .onInit to execute earlier
  
  SectionIn RO
  SetOverwrite ifnewer
  SetOutPath $INSTDIR
  
  !insertmacro CheckNetFramework 45

  !ifdef X32
    DetailPrint "32-bit system forced"
    File "${BUILD_DIR_32}\${VSNAME}.exe"
    File "${BUILD_DIR_32}\*.dll"
  !else
    ${If} ${RunningX64} 
      DetailPrint "64-bit system detected"
      File "${BUILD_DIR_64}\${VSNAME}.exe"
      File "${BUILD_DIR_64}\*.dll"
    ${Else}
      DetailPrint "32-bit system detected"
      File "${BUILD_DIR_32}\${VSNAME}.exe"
      File "${BUILD_DIR_32}\*.dll"
    ${EndIf}
  !endif
  
  ;SetOutPath "$APPDATA\${PRODUCT_NAME}"
  File "${CFG_BUILD_DIR}\*.dat"
  File "${CFG_BUILD_DIR}\License.txt"
  File "${CFG_BUILD_DIR}\ToolTips.html"
  File "${CFG_BUILD_DIR}\ChordCadenza.chm"

SectionEnd

;Section "GetSize"
; ${GetSize} "$INSTDIR" "/S=0K" $0 $1 $2
; IntFmt $0 "0x%08X" $0
; WriteRegDWORD HKLM "${ARP}" "EstimatedSize" "$0"
;SectionEnd

Section "Desktop Shortcuts" Desc_Desktop_Shortcuts"
; It is pretty clear what following line does: just rename the file name to your project startup executable.
  CreateShortCut "$DESKTOP\${LINK_NAME}.lnk" "$INSTDIR\${VSNAME}.exe" ""
SectionEnd

Section "Start Menu Shortcuts" Desc_Start_Menu_Shortcuts
  ;CreateDirectory "$SMPROGRAMS\${PRODUCT_NAME}"
  ;CreateShortCut "$SMPROGRAMS\${PRODUCT_NAME}\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "$INSTDIR\uninstall.exe" 0
  CreateShortCut "$SMPROGRAMS\${LINK_NAME}.lnk" "$INSTDIR\${VSNAME}.exe" 
SectionEnd

!ifdef STARTERSOUNDFONT 
  ;separate section to show option on install components screen
  Section "SoundFont" Desc_SoundFont
    CreateDirectory "$MUSIC\SoundFonts"
    SetOutPath "$MUSIC\SoundFonts"
    File "D:\D0\Dev\ChCa\ChordCadenza.UWP\ChordCadenza\Soundfonts\TimGM6mb.sf2"
  SectionEnd
!endif  

Section "Sample Projects" Desc_Sample_Projects
  ;separate section to show option on install components screen
  CreateDirectory "$MUSIC\${PRODUCT_NAME} Projects\Samples"
  SetOutPath "$MUSIC\${PRODUCT_NAME} Projects\Samples"
  File /r "D:\D0\Dev\ChCa\ChordCadenza.UWP\ChordCadenza\Samples\*.*"
SectionEnd

Section -Post
  ;Following lines will make uninstaller work - do not change anything, unless you really want to.
  WriteUninstaller "$INSTDIR\uninst.exe"
  
  ;get installed size
  Call GetInstalledSize
  Pop $0
  WriteRegDWORD ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "EstimatedSize" "$0"
  
  ;update registry
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "${PRODUCT_NAME}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "URLInfoAbout" "${PRODUCT_WEB_SITE}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
  
;  !ifdef StarterSoundFont
;    SectionSetSize ${SoundFont_id} 5830
;  !endif
;  SectionSetSize ${Samples_id} 7230
  
  Push "D:\Temp\NSISLog.txt"
  Call DumpLog
  ;SetAutoClose false
SectionEnd

;LangString Desc_Chord_Cadenza ${LANG_ENGLISH} "Description of section 1."
;LangString Desc_Desktop_Shortcuts ${LANG_ENGLISH} "Description of section 2."
;
;!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
;  !insertmacro MUI_DESCRIPTION_TEXT ${Section1} $(Desc_Chord_Cadenza)
;  !insertmacro MUI_DESCRIPTION_TEXT ${Section2} $(Desc_Desktop_Shortcuts)
;!insertmacro MUI_FUNCTION_DESCRIPTION_END

Function .onInit
  Call CheckMutex

  ReadRegStr $R0 HKLM "${PRODUCT_UNINST_KEY}" "UninstallString"
  StrCmp $R0 "" uninstdone
  MessageBox MB_OKCANCEL|MB_ICONEXCLAMATION|MB_TOPMOST|MB_SETFOREGROUND \
  "${PRODUCT_NAME} is already installed. $\n$\nClick `OK` to remove the \
  previous version, or `CANCEL` to cancel this installation." \
  IDOK uninst
  Abort
    
  uninst:
    !insertmacro Deinstall
    !insertmacro RemAppData
    MessageBox MB_ICONINFORMATION|MB_OK "Application was successfully removed from your computer."

;    ClearErrors
;    ExecWait $R0
;    ;ExecWait '$R0 _?=$INSTDIR' ;Do not copy the uninstaller to a temp file
    
  uninstdone:
    SetOverwrite ifnewer

  !ifdef X32
      StrCpy $INSTDIR $PROGRAMFILES\${PRODUCT_NAME}
  !else
    ${If} ${RunningX64}
      StrCpy $INSTDIR $PROGRAMFILES64\${PRODUCT_NAME}
    ${Else}
      StrCpy $INSTDIR $PROGRAMFILES\${PRODUCT_NAME}
    ${EndIf}
  !endif
  
/*
  ;check .NET
  ReadRegDWORD $0 HKLM "SOFTWARE\Microsoft\NET Framework_Setup\NDP\v4\Full" "Release"
  IntCmp "$0" "${ReleaseDotNet45}" DotNetInstalled DotNetNotInstalled DotNetInstalled  
  
  DotNetNotInstalled:
    NSISDL::download ${DOWNLOAD_URL} "${NETInstaller}"
   
    Pop $0
    ${If} $0 == "cancel"
      goto SetupGoOnWithoutDotNet
    ${ElseIf} $0 != "success"
      goto SetupGoOnWithoutDotNet
    ${EndIf}
     
    ${StdUtils.ExecShellWaitEx} $0 $1 "${NETInstaller}" "open" ""
    StrCmp $0 "error" ExecFailed
    StrCmp $0 "no_wait" ExecFailed
    StrCmp $0 "ok" WaitForProc
    Abort
   
  WaitForProc:
    ${StdUtils.WaitForProcEx} $2 $1
    StrCmp "$2" "" EndInstallDotNet  ;installed OK
    StrCmp "$2" "0" EndInstallDotNet  ;installed OK
    StrCmp "$2" "3010" EndInstallDotNet  ;installed OK - restart required
    StrCmp "$2" "8192" EndInstallDotNet  ;???
    MessageBox MB_ICONQUESTION|MB_YESNO "DotNet install failed - continue anyway?" IDYES +2
    Abort
    
  SetupGoOnWithoutDotNet:
    MessageBox MB_ICONINFORMATION|MB_OK "Continuing Setup - you will need to install .NET 4 before running application!"

  EndInstallDotNet:
  DotNetInstalled:
*/
  
FunctionEnd
 
;Function .onMouseOverSection
;  ;FindWindow $R0 "#32770" "" $HWNDPARENT
;  ;GetDlgItem $R0 $R0 1201 ; description item (must be added to the UI);
;  GetDlgItem $0 $HWNDPARENT 1
;  StrCmp $0 0 "" +2
;    SendMessage $R0 ${WM_SETTEXT} 0 "STR:first section description"
;
;  StrCmp $0 1 "" +2
;    SendMessage $R0 ${WM_SETTEXT} 0 "STR:second section description"
;FunctionEnd 
 
; Replace the following strings to suit your needs
Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "Application was successfully removed from your computer."
FunctionEnd

Function un.onInit
  Call un.CheckMutex
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Are you sure you want to completely remove ${PRODUCT_NAME} and all of its components?" IDYES +2
  Abort
FunctionEnd

; Remove any file that you have added above - removing uninstallation and folders last.
; Note: if there is any file changed or added to these folders, they will not be removed. Also, parent folder (which in my example 
; is company name ZWare) will not be removed if there is any other application installed in it.
Section Uninstall
  !insertmacro Deinstall
SectionEnd

Section un.RemoveAppData
  !insertmacro RemAppData
SectionEnd

;next 2 lines comes with DumpLog macro
!define LVM_GETITEMCOUNT 0x1004
!define LVM_GETITEMTEXTA 0x102D
Function DumpLog
  Exch $5
  Push $0
  Push $1
  Push $2
  Push $3
  Push $4
  Push $6

  FindWindow $0 "#32770" "" $HWNDPARENT
  GetDlgItem $0 $0 1016
  StrCmp $0 0 error
  FileOpen $5 $5 "w"
  StrCmp $5 0 error
    SendMessage $0 ${LVM_GETITEMCOUNT} 0 0 $6
    System::Alloc ${NSIS_MAX_STRLEN}
    Pop $3
    StrCpy $2 0
    System::Call "*(i, i, i, i, i, i, i, i, i) i \
      (0, 0, 0, 0, 0, r3, ${NSIS_MAX_STRLEN}) .r1"
    loop: StrCmp $2 $6 done
      System::Call "User32::SendMessageA(i, i, i, i) i \
        ($0, ${LVM_GETITEMTEXTA}, $2, r1)"
      System::Call "*$3(&t${NSIS_MAX_STRLEN} .r4)"
      FileWrite $5 "$4$\r$\n"
      IntOp $2 $2 + 1
      Goto loop
    done:
      FileClose $5
      System::Free $1
      System::Free $3
      Goto exit
  error:
    MessageBox MB_OK error
  exit:
    Pop $6
    Pop $4
    Pop $3
    Pop $2
    Pop $1
    Pop $0
    Exch $5
FunctionEnd

Var GetInstalledSize.total
Function GetInstalledSize
	Push $0  
	Push $1  
	StrCpy $GetInstalledSize.total 0
	${ForEach} $1 0 256 + 1
		${if} ${SectionIsSelected} $1
			SectionGetSize $1 $0
			IntOp $GetInstalledSize.total $GetInstalledSize.total + $0
		${Endif}
 
		; Error flag is set when an out-of-bound section is referenced
		${if} ${errors}
			${break}
		${Endif}
	${Next}
 
	ClearErrors
	Pop $1
	Pop $0
	IntFmt $GetInstalledSize.total "0x%08X" $GetInstalledSize.total
	Push $GetInstalledSize.total
FunctionEnd
