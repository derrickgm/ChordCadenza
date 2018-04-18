//var elements = document.getElementsByClassName("song");

for (i = 0; i < elements.length; i++) {
  if (elements[i].innerHTML == "Song") {
    elements[i].innerHTML = '<A HREF="JavaScript:HHCTRL.TextPopup(TextSong,MyFont,9,9,-1,-1)" Title="Click for Popup">Song</A> ';
  } else if (elements[i].innerHTML == "CalcKeys") {
    elements[i].innerHTML = '<A HREF="JavaScript:HHCTRL.TextPopup(TextCalcKeys,MyFont,9,9,-1,-1)" Title="Click for Popup">CalcKeys</A> ';    
  }
}
