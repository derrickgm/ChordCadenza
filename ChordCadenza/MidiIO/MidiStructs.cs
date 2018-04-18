using System;
using System.Runtime.InteropServices;

namespace ChordCadenza {
  [StructLayout(LayoutKind.Sequential, Pack=1)]
  internal struct MidiHeader {

    /// Pointer to MIDI data.
    internal IntPtr data;

    /// Size of the buffer.
    internal int bufferLength;

    /// Actual amount of data in the buffer. This value should be less than 
    /// or equal to the value given in the dwBufferLength member.
    internal int bytesRecorded;

    /// Custom user data.
    internal IntPtr user;

    /// Flags giving information about the buffer.
    internal int flags;

    /// Reserved; do not use.
    internal IntPtr next;

    /// Reserved; do not use.
    internal IntPtr reserved;

    /// Offset into the buffer when a callback is performed. (This 
    /// callback is generated because the MEVT_F_CALLBACK flag is 
    /// set in the dwEvent member of the MidiEventArgs structure.) 
    /// This offset enables an application to determine which 
    /// event caused the callback. 
    internal int offset;

    /// Reserved; do not use.
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
    internal IntPtr[] reservedArray;
  }

  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  internal struct MMTime {
    internal uint wType;
    internal int Ticks;
    //* other time formats are available 
  }

  internal abstract class DeviceException : ApplicationException {
    internal const int MMSYSERR_ALLOCATED = 4;
    internal const int MMSYSERR_BADDB = 14;
    internal const int MMSYSERR_BADDEVICEID = 2;
    internal const int MMSYSERR_BADERRNUM = 9;
    internal const int MMSYSERR_DELETEERROR = 18;
    internal const int MMSYSERR_ERROR = 1;
    internal const int MMSYSERR_HANDLEBUSY = 12;
    internal const int MMSYSERR_INVALFLAG = 10;
    internal const int MMSYSERR_INVALHANDLE = 5;
    internal const int MMSYSERR_INVALIDALIAS = 13;
    internal const int MMSYSERR_INVALPARAM = 11;
    internal const int MMSYSERR_KEYNOTFOUND = 15;
    internal const int MMSYSERR_LASTERROR = 20;
    internal const int MMSYSERR_NODRIVER = 6;
    internal const int MMSYSERR_NODRIVERCB = 20;
    internal const int MMSYSERR_NOERROR = 0;
    internal const int MMSYSERR_NOMEM = 7;
    internal const int MMSYSERR_NOTENABLED = 3;
    internal const int MMSYSERR_NOTSUPPORTED = 8;
    internal const int MMSYSERR_READERROR = 16;
    internal const int MMSYSERR_VALNOTFOUND = 19;
    internal const int MMSYSERR_WRITEERROR = 17;
  }

  internal class MidiDeviceException : DeviceException {
    internal const int MIDIERR_UNPREPARED = 64; /* header not prepared */
    internal const int MIDIERR_STILLPLAYING = 65; /* still something playing */
    internal const int MIDIERR_NOMAP = 66; /* no configured instruments */
    internal const int MIDIERR_NOTREADY = 67; /* hardware is still busy */
    internal const int MIDIERR_NODEVICE = 68; /* port no longer connected */
    internal const int MIDIERR_INVALIDSETUP = 69; /* invalid MIF */
    internal const int MIDIERR_BADOPENMODE = 70; /* operation unsupported w/ open mode */
    internal const int MIDIERR_DONT_CONTINUE = 71; /* thru device 'eating' a message */
    internal const int MIDIERR_LASTERROR = 71; /* last error in range */

    /// <summary>
    /// Initializes a new instance of the DeviceException class with the
    /// specified error code.
    /// </summary>
    /// <param name="errCode">
    /// The error code.
    /// </param>
    //internal MidiDeviceException(int errCode)
    //  : base(errCode) {
    //}
  }

  internal static class MMConstants {

    //* types for wType field in MMTIME struct */
    internal const int TIME_MS        = 0x0001;  /* time in milliseconds */
    internal const int TIME_SAMPLES   = 0x0002;  /* number of wave samples */
    internal const int TIME_BYTES     = 0x0004;  /* current byte offset */
    internal const int TIME_SMPTE     = 0x0008;  /* SMPTE time */
    internal const int TIME_MIDI      = 0x0010;  /* MIDI time */
    internal const int TIME_TICKS     = 0x0020;  /* Ticks within MIDI stream */


    internal const int CALLBACK_TYPEMASK = 0x00070000;   /* callback type mask */
    internal const int CALLBACK_NULL = 0x00000000;    /* no callback */
    internal const int CALLBACK_WINDOW = 0x00010000;    /* dwCallback is a HWND */
    internal const int CALLBACK_TASK = 0x00020000;    /* dwCallback is a HTASK */
    internal const int CALLBACK_FUNCTION = 0x00030000;    /* dwCallback is a FARPROC */
    //private const int  CALLBACK_THREAD     (CALLBACK_TASK)/* thread ID replaces 16 bit task */
    internal const int CALLBACK_EVENT = 0x00050000;    /* dwCallback is an EVENT Handle */

    /* Type codes which go in the high byte of the event DWORD of a stream buffer */
    /* */
    /* Type codes 00-7F contain parameters within the low 24 bits */
    /* Type codes 80-FF contain a length of their parameter in the low 24 */
    /* bits, followed by their parameter data in the buffer. The event */
    /* DWORD contains the exact byte length; the parm data itself must be */
    /* padded to be an even multiple of 4 bytes long. */
    /* */

    internal const uint MEVT_F_SHORT       = 0x00000000;
    internal const uint MEVT_F_LONG        = 0x80000000;
    internal const uint MEVT_F_CALLBACK    = 0x40000000;

    internal const byte MEVT_SHORTMSG     =  0x00;    /* parm = shortmsg for midiOutShortMsg */
    internal const byte MEVT_TEMPO        =  0x01;    /* parm = new tempo in microsec/qn     */
    internal const byte MEVT_NOP          =  0x02;    /* parm = unused; does nothing         */

    /* 0x04-0x7F reserved */

    internal const byte MEVT_LONGMSG      =  0x80;    /* parm = bytes to send verbatim       */
    internal const byte MEVT_COMMENT      =  0x82;    /* parm = comment data                 */
    internal const byte MEVT_VERSION      =  0x84;    /* parm = MIDISTRMBUFFVER struct       */

    /* 0x81-0xFF reserved */

    //internal const int MIDISTRM_ERROR      (-2)

    /* */
    /* Structures and defines for midiStreamProperty */
    /* */
    internal const uint MIDIPROP_SET      = 0x80000000;
    internal const uint MIDIPROP_GET      = 0x40000000;

    /* These are intentionally both non-zero so the app cannot accidentally */
    /* leave the operation off and happen to appear to work due to default */
    /* action. */

    internal const uint MIDIPROP_TIMEDIV  = 0x00000001;
    internal const uint MIDIPROP_TEMPO    = 0x00000002;

    internal const int MM_MOM_OPEN       = 0x3C7;          /* MIDI output */
    internal const int MM_MOM_CLOSE      = 0x3C8;
    internal const int MM_MOM_DONE       = 0x3C9;
    internal const int MM_MOM_POSITIONCB = 0x3CA;           /* Callback for MEVT_POSITIONCB */

    internal const int MIM_OPEN = 0x3C1;           /* MIDI input */
    internal const int MIM_CLOSE = 0x3C2;
    internal const int MIM_DATA = 0x3C3;
    internal const int MIM_LONGDATA = 0x3C4;
    internal const int MIM_ERROR = 0x3C5;
    internal const int MIM_LONGERROR = 0x3C6;
    internal const int MIM_MOREDATA = 0x3CC;

    //typedef struct midiproptimediv_tag
    //{
    //    DWORD       cbStruct;
    //    DWORD       dwTimeDiv;
    //} MIDIPROPTIMEDIV, FAR *LPMIDIPROPTIMEDIV;

    //typedef struct midiproptempo_tag
    //{
    //    DWORD       cbStruct;
    //    DWORD       dwTempo;
    //} MIDIPROPTEMPO, FAR *LPMIDIPROPTEMPO;
  }
}
