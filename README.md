# ChordCadenza
This program can help you to improvise and play along with a midi file or other audio source using a midi keyboard. It does not require midi keyboard skills or the ability to read music. 

## Main Features

* Midi Sequencer with track display, mute, solo, patch, vol, pan, etc.

* Extract keys, modulations, and chords from a midifile

* Display chords on a vertical piano roll to facilitate playing and improvisation

* Display solfa or pitch notation

* Output to ASIO or non-ASIO soundcard (using soundfonts) or to a midi synth (hardware or software)

* Multiple playmodes to allow for different levels of midi keyboard skills

* Synchronise and play along with midi files (.mid) or audio files (.mp3)

* Play along with any audio stream or other musicians.

## Programming

The program was developed in C# using Visual Studio Community Edition 2015.

It requires .NET 4.0 or later, and has been tested on Windows 7 and Windows 10. There are currently no ports to any other OS.

This project uses:
* Un4Seen Audio Library (aka. BASS) http://www.un4seen.com
	copyright © 2003-2018 un4seen developments. all rights reserved.
	BASS is free for non-commercial use.
* BASS Add-Ons
	All trademarks and other registered names contained in the BASS.NET package are the property of their respective owners.
	See www.un4seen.com for details!
* BASS.NET API
	Copyright 2005-2018 by radio42, Author: Bernd Niedergesaess  (bn@radio42.com). All rights reserved. 
	BASS.NET is the property of radio42 and is protected by copyright laws and international copyright treaties. BASS.NET is not sold, it is licensed.

## Installation

The program can be installed using the downloadable executable at [ChordCadenza.org](http://chordcadenza.org/#loc_download). This is a Nullsoft installation package that will install a 32-bit or 64-bit version of the program.

## Source Files

The files contained here should include all of the source code necessary to compile and build the program. It also contains the Nullsoft installation script, and the files used to create the Windows Help file (ChordCadenza.chm)

## Development

I developed the program in order to help me to play along with midi and audio files using a midi keyboard, having struggled for many years to master improvisiation. If you have any comments, feedback, ideas, or questions, please contact me at support@chordcadenza.org.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
