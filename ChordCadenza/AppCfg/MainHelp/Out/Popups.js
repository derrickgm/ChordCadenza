var MyFont="Helvetica,10,,plain";

var TextProject = "A collection of files associated with a particular piece of music. May include a midi file (*.mid), chord file (*.chp), audio file (*.mp3), audiosync file (*.chtc or *.chtx), lyrics file (*.chl)";
var TextCalcKeys = "An algorithm for calculating the keys of a piece of music from a midifile.";
var TextAudioSyncWindow = "A window for managing audio sync tracks, opened from PlayMap Menu > Configure > Show AudioSync Window.";
var TextChordFile = "A file (*.chp) created by this program containing the chords, keys, time signatures and track parameters for a piece of music.";
var TextASIO = "A soundcard driver providing low latency for realtime applications";
var TextMode = "KeyboardMode or ChordMode";
var TextKeyboardMode = "Keyboard plays normal notes.";
var TextChordMode = "Keyboard plays chord notes on white keys.";
var TextKBChord = "Plays the nearest chordnote to the note played on the keyboard";
var TextSyncBar = "The position to start synchronising from, as displayed in the Bars Pane of the TrackMap or ChordMap";

var elements = document.getElementsByTagName("span");

for (i = 0; i < elements.length; i++) {
  if (elements[i].innerHTML == "Project") {
    elements[i].innerHTML = '<A HREF="JavaScript:HHCTRL.TextPopup(TextProject,MyFont,9,9,-1,-1)" Title="Click for Popup">Project</A>';
  } else if (elements[i].innerHTML == "CalcKeys") {
    elements[i].innerHTML = '<A HREF="JavaScript:HHCTRL.TextPopup(TextCalcKeys,MyFont,9,9,-1,-1)" Title="Click for Popup">CalcKeys</A>';    
  } else if (elements[i].innerHTML == "AudioSync Window") {
    elements[i].innerHTML = '<A HREF="JavaScript:HHCTRL.TextPopup(TextAudioSyncWindow,MyFont,9,9,-1,-1)" Title="Click for Popup">AudioSync Window</A>'; 
  } else if (elements[i].innerHTML == "ChordFile") {
    elements[i].innerHTML = '<A HREF="JavaScript:HHCTRL.TextPopup(TextChordFile,MyFont,9,9,-1,-1)" Title="Click for Popup">ChordFile</A>';    
  } else if (elements[i].innerHTML == "ASIO") {
    elements[i].innerHTML = '<A HREF="JavaScript:HHCTRL.TextPopup(TextASIO,MyFont,9,9,-1,-1)" Title="Click for Popup">ASIO</A>';    
  } else if (elements[i].innerHTML == "Mode") {
    elements[i].innerHTML = '<A HREF="JavaScript:HHCTRL.TextPopup(TextMode,MyFont,9,9,-1,-1)" Title="Click for Popup">Mode</A>';    
  } else if (elements[i].innerHTML == "KeyboardMode") {
    elements[i].innerHTML = '<A HREF="JavaScript:HHCTRL.TextPopup(TextKeyboardMode,MyFont,9,9,-1,-1)" Title="Click for Popup">KeyboardMode</A>';    
  } else if (elements[i].innerHTML == "ChordMode") {
    elements[i].innerHTML = '<A HREF="JavaScript:HHCTRL.TextPopup(TextChordMode,MyFont,9,9,-1,-1)" Title="Click for Popup">ChordMode</A>';    
  } else if (elements[i].innerHTML == "KBChord") {
    elements[i].innerHTML = '<A HREF="JavaScript:HHCTRL.TextPopup(TextKBChord,MyFont,9,9,-1,-1)" Title="Click for Popup">KBChord</A>';    
  } else if (elements[i].innerHTML == "SyncBar") {
    elements[i].innerHTML = '<A HREF="JavaScript:HHCTRL.TextPopup(TextSyncBar,MyFont,9,9,-1,-1)" Title="Click for Popup">SyncBar</A>';    
  }
}
