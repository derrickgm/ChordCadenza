;NSIS script to install ChordCadenza on 64 or 32-bit systems

!define STARTERSOUNDFONT
!define X32

!define VSNAME "ChordCadenza"  ;same as visual studio
!define PRODUCT_NAME "ChordCadenza"
!define PRODUCT_PUBLISHER "ChordCadenza"
!define PRODUCT_WEB_SITE "http://www.chordcadenza.co.uk"
!define STARTERSOUNDFONT_BUILD_DIR "D:\Software New\SoundFonts"
!define OUTDIR "D:\D2\Dev\CS.Express\ChordCadenza\ChordCadenza\WebSite\Simple\Downloads"

!include LogicLib.nsh
!include "x64.nsh" 

!define PRODUCT_VERSION "dev"  
!define CFG_BUILD_DIR "D:\D2\Dev\CS.Express\${VSNAME}\${VSNAME}\bin\Cfg"
!define BUILD_DIR_64  "D:\D2\Dev\CS.Express\${VSNAME}\${VSNAME}\bin\Release X64"
!define BUILD_DIR_32  "D:\D2\Dev\CS.Express\${VSNAME}\${VSNAME}\bin\Release"

;!define PRODUCT_VERSION "1.11.1"  
;!define CFG_BUILD_DIR "D:\GoogleDrive\Expression BU\ChordCadenza ${PRODUCT_VERSION}\ChordCadenza\bin\Cfg"
;!define BUILD_DIR_64  "D:\GoogleDrive\Expression BU\ChordCadenza ${PRODUCT_VERSION}\ChordCadenza\bin\Release X64"
;!define BUILD_DIR_32  "D:\GoogleDrive\Expression BU\ChordCadenza ${PRODUCT_VERSION}\ChordCadenza\bin\Release"

!ifdef STARTERSOUNDFONT 
  !ifdef X32
    OutFile "${OUTDIR}\Setup ${PRODUCT_NAME}_${PRODUCT_VERSION}_SoundFont_32.exe"
  !else
    OutFile "${OUTDIR}\Setup ${PRODUCT_NAME}_${PRODUCT_VERSION}_SoundFont.exe"
  !endif
!else
  !ifdef X32
    OutFile "${OUTDIR}\Setup ${PRODUCT_NAME}_${PRODUCT_VERSION}_32.exe"
  !else
    OutFile "${OUTDIR}\Setup ${PRODUCT_NAME}_${PRODUCT_VERSION}.exe"
  !endif
!endif  

; Following constants you should never change
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

; Replace the constants below to suit your project
Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
LicenseData "License.txt"

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
Section -Main 
  ;All sections execute after Pages (license etc.) 
  ;Use Function .onInit to execute earlier
  
  SetOverwrite ifnewer
  SetOutPath $INSTDIR
  
  !ifdef X32
      DetailPrint "32-bit system forced"
      File "${BUILD_DIR_32}\${VSNAME}.exe"
      File "${BUILD_DIR_32}\Bass.dll"
      File "${BUILD_DIR_32}\Bass_fx.dll"
      File "${BUILD_DIR_32}\Bassasio.dll"
      File "${BUILD_DIR_32}\Bassmidi.dll"
      File "${BUILD_DIR_32}\Bassmix.dll"
      File "${BUILD_DIR_32}\Bass.Net.dll"
  !else
    ${If} ${RunningX64} 
      DetailPrint "64-bit system detected"
      File "${BUILD_DIR_64}\${VSNAME}.exe"
      File "${BUILD_DIR_64}\Bass.dll"
      File "${BUILD_DIR_64}\Bass_fx.dll"
      File "${BUILD_DIR_64}\Bassasio.dll"
      File "${BUILD_DIR_64}\Bassmidi.dll"
      File "${BUILD_DIR_64}\Bassmix.dll"
      File "${BUILD_DIR_64}\Bass.Net.dll"
    ${Else}
      DetailPrint "32-bit system detected"
      File "${BUILD_DIR_32}\${VSNAME}.exe"
      File "${BUILD_DIR_32}\Bass.dll"
      File "${BUILD_DIR_32}\Bass_fx.dll"
      File "${BUILD_DIR_32}\Bassasio.dll"
      File "${BUILD_DIR_32}\Bassmidi.dll"
      File "${BUILD_DIR_32}\Bassmix.dll"
      File "${BUILD_DIR_32}\Bass.Net.dll"
    ${EndIf}
  !endif

  SetOutPath "$APPDATA\${VSNAME}"
  File "${CFG_BUILD_DIR}\InitialScreen.dat"
  File "${CFG_BUILD_DIR}\Chords.dat"
  File "${CFG_BUILD_DIR}\ToolTips.html"
  File "${CFG_BUILD_DIR}\ChordCadenza.chm"
  
  CreateDirectory "$MUSIC\${VSNAME} Projects"
  !ifdef STARTERSOUNDFONT
    CreateDirectory "$MUSIC\SoundFonts"
    SetOutPath "$MUSIC\SoundFonts"
  !endif  
  
; Note: my system has a config template, which should manually be edited. This is a nice trick to save your username/password somewhere,
; but you can entirely skip this by deleting the following line. 
; File /oname=zawscc.exe.config "App.config.template"

SectionEnd

!ifdef STARTERSOUNDFONT
Section "SoundFont"
  ;File "${STARTERSOUNDFONT_BUILD_DIR}\SYNTHGMS.sf2"
  File "${STARTERSOUNDFONT_BUILD_DIR}\TimGM6mb.sf2"
  ;File "${STARTERSOUNDFONT_BUILD_DIR}\FluidR3 GM2-2.SF2"
SectionEnd
!endif  

;Section "GetSize"
; ${GetSize} "$INSTDIR" "/S=0K" $0 $1 $2
; IntFmt $0 "0x%08X" $0
; WriteRegDWORD HKLM "${ARP}" "EstimatedSize" "$0"
;SectionEnd

Section "Desktop Shortcuts"
; It is pretty clear what following line does: just rename the file name to your project startup executable.
  CreateShortCut "$DESKTOP\${PRODUCT_NAME}.lnk" "$INSTDIR\${VSNAME}.exe" ""
SectionEnd

Section "Start Menu Shortcuts"
  ;CreateDirectory "$SMPROGRAMS\${PRODUCT_NAME}"
  ;CreateShortCut "$SMPROGRAMS\${PRODUCT_NAME}\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "$INSTDIR\uninstall.exe" 0
  CreateShortCut "$SMPROGRAMS\${PRODUCT_NAME}.lnk" "$INSTDIR\${VSNAME}.exe" 
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
  
  Push "D:\Temp\NSISLog.txt"
  Call DumpLog
  ;SetAutoClose false
SectionEnd

Function .onInit
  Call CheckMutex

  ReadRegStr $R0 HKLM "${PRODUCT_UNINST_KEY}" "UninstallString"
  StrCmp $R0 "" done
  MessageBox MB_OKCANCEL|MB_ICONEXCLAMATION \
  "${PRODUCT_NAME} is already installed. $\n$\nClick `OK` to remove the \
  previous version, or `CANCEL` to cancel this installation." \
  IDOK uninst
  Abort
    
  uninst:
    ;Exec $INSTDIR\uninst.exe 
    ClearErrors
    Exec $R0
  done:
  
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
  
  ;!include DotNetSearch.nsh
  ;!insertmacro DotNetSearch 4 0 "" "ABORT" ""
  ;;DotNetSearch DOTNETVMAJOR DOTNETVMINOR DOTNETVMINORMINOR DOTNETLASTFUNCTION DOTNETPATH 
  ;;DOTNETPATH = location to install .NET
  ;;DOTNETLASTFUNCTION = INSTALL_ABORT, ABORT etc.
FunctionEnd
 
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
  Delete "$INSTDIR\*.*"
  RMDir "$INSTDIR"
  ;RMDir "$INSTDIR\.."
  
  ; Remove shortcuts, if any
  Delete "$SMPROGRAMS\${PRODUCT_NAME}.lnk"
  Delete "$DESKTOP\${PRODUCT_NAME}.lnk" 

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  ; Change following to be exactly as above
  ;DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "SOFTWARE\Microsoft\.NETFramework\v2.0.50727\AssemblyFoldersEx\ZWare\ZAwsCC" 

  SetAutoClose false
  
SectionEnd

Section un.RemoveAppData
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON1 "Remove Configuration (.Ini) Files? Click 'NO' if you want to retain your settings for a new installation of Chord Cadenza.)" IDYES +1 IDNO RetainIni
  Delete "$APPDATA\${VSNAME}\*.*"
  RMDir "$APPDATA\${VSNAME}"
  Goto done
  
  RetainIni:
  Delete "$APPDATA\${VSNAME}\*.dat"
  Delete "$APPDATA\${VSNAME}\*.htm"
  Delete "$APPDATA\${VSNAME}\*.chm"
  
  done:
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
