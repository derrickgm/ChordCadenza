using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChordCadenza {
  internal static class MidiCtlrList {
    internal static string[] Desc = new string[102];

    static MidiCtlrList() {
      string lines;
      lines = @"0 Bank Select (MSB)
1 Modulation Wheel (MSB)
2 Breath Controller (MSB)
3
4 Foot Controller (MSB)
5 Portamento Time (MSB)
6 Data Entry (MSB)
7 Channel Volume (MSB)
8 Balance (MSB)
9
10 Pan (MSB)
11 Expression (MSB)
12 Effect Control 1 (MSB)
13 Effect Control 2 (MSB)
14
15
16 General Purpose Controller 1 (MSB)
17 General Purpose Controller 2 (MSB)
18 General Purpose Controller 3 (MSB)
19 General Purpose Controller 4 (MSB)
20
21
22
23
24
25
26
27
28
29
30
31
32 Bank Select (LSB)
33 Modulation Wheel (LSB)
34 Breath Controller (LSB)
35
36 Foot Controller (LSB)
37 Portamento Time (LSB)
38 Data Entry (LSB)
39 Channel Volume (LSB)
40 Balance (LSB)
41
42 Pan (LSB)
43 Expression (LSB)
44 Effect Control 1 (LSB)
45 Effect Control 2 (LSB)
46
47
48 General Purpose Controller 1 (LSB)
49 General Purpose Controller 2 (LSB)
50 General Purpose Controller 3 (LSB)
51 General Purpose Controller 4 (LSB)
52
53
54
55
56
57
58
59
60
61
62
63
64 Sustain Pedal
65 Portamento On/Off
66 Sostenuto
67 Soft Pedal
68 Legato Footswitch
69 Hold 2
70 Sound Controller 1 (default: Sound Variation)
71 Sound Controller 2 (default: Timbre / Harmonic Quality)
72 Sound Controller 3 (default: Release Time)
73 Sound Controller 4 (default: Attack Time)
74 Sound Controller 5 (default: Brightness)
75 Sound Controller 6 (GM2 default: Decay Time)
76 Sound Controller 7 (GM2 default: Vibrato Rate)
77 Sound Controller 8 (GM2 default: Vibrato Depth)
78 Sound Controller 9 (GM2 default: Vibrato Delay)
79 Sound Controller 10 (GM2 default: Undefined)
80 General Purpose Controller 5
81 General Purpose Controller 6
82 General Purpose Controller 7
83 General Purpose Controller 8
84 Portamento Control
85
86
87
88
89
90
91 Effects 1 Depth (default: Reverb Send)
92 Effects 2 Depth (default: Tremolo Depth)
93 Effects 3 Depth (default: Chorus Send)
94 Effects 4 Depth (default: Celeste [Detune] Depth)
95 Effects 5 Depth (default: Phaser Depth)
96 Data Increment
97 Data Decrement
98 Non-Registered Parameter Number (LSB)
99 Non-Registered Parameter Number(MSB)
100 Registered Parameter Number (LSB)
101 Registered Parameter Number(MSB) 
";
      char[] delimspace = new char[] { ' ' };
      string[] delimnewline = new string[] { "\r\n" };
      string[] l = lines.Split(delimnewline, StringSplitOptions.None);
      for (int i = 0; i <= 101; i++) {
        string[] f = l[i].Split(delimspace, 2);
        if (int.Parse(f[0]) != i) LogicError.Throw(eLogicError.X051);
        if (f.Length < 2) Desc[i] = "N/A";
        else Desc[i] = f[1];
      }
    }
  }
}
