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

It uses the BASS audio library from [un4seen.com](https://un4seen.com) and Bass.NET at [bass.radio42.com](http://bass.radio42.com).

## Installation

The program can be installed using the downloadable executable at [ChordCadenza.org](http://chordcadenza.org/#loc_download). This is a Nullsoft installation package that will install a 32-bit or 64-bit version of the program.

## Source Files

The files contained here should include all of the source code necessary to compile and build the program. It also contains the Nullsoft installation script, and the files used to create the Windows Help file (ChordCadenza.chm)

## Development

I developed the program in order to help me to play along with midi and audio files using a midi keyboard, having struggled for many years to master improvisiation. If you have any comments, feedback, ideas, or questions, please contact me at support@chordcadenza.org.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
