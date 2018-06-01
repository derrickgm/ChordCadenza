MyFont="Helvetica,10,,plain"
TextSong="A folder containing a collection of files associated with a particular piece of music. May include a midi file (*.mid), chord file (*.chp), audio file (*.mp3), audiosync file (*.chtc or *.chtx), lyrics file (*.chl)"
TextAudioSync="A window for managing audio sync tracks, opened from PlayMap Menu > Configure > Show AudioSync Window."
TextCalcKeys="An algorithm for calculating the keys of a piece of music from a midifile."

for (i = 0; i < elements.length; i++) {
  if (elements[i].innerHTML == "Song") {
    elements[i].innerHTML = '<A HREF="JavaScript:HHCTRL.TextPopup(TextSong,MyFont,9,9,-1,-1)" Title="Click for Popup">Song</A> ';
  } else if (elements[i].innerHTML == "CalcKeys") {
    elements[i].innerHTML = '<A HREF="JavaScript:HHCTRL.TextPopup(TextCalcKeys,MyFont,9,9,-1,-1)" Title="Click for Popup">CalcKeys</A> ';    
  } else if (elements[i].innerHTML == "AudioSync") {
    elements[i].innerHTML = '<A HREF="JavaScript:HHCTRL.TextPopup(TextAudioSync,MyFont,9,9,-1,-1)" Title="Click for Popup">AudioSync</A> ';    
  }
}
    