using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Mix;

namespace ChordCadenza {
  internal partial class clsMP3Bass{
    private int PosSyncHandle = 0;
    private int EndSyncHandle = 0;

    private void SetEndSync() {
      //Debug.WriteLine("clsAutoSyncBeat: clsBASSMP3: SetEndSync: ");
      EndSyncHandle = Bass.BASS_ChannelSetSync(StreamHandle, BASSSync.BASS_SYNC_END, 0, delegOnEnd, IntPtr.Zero);
    }

    private void SetPosSync(long sigpos) {
      IntPtr user = new IntPtr(sigpos);
      //Debug.WriteLine("clsAutoSyncBeat: clsBASSMP3: SetPosSync: " + sigpos);
      PosSyncHandle = Bass.BASS_ChannelSetSync(StreamHandle, BASSSync.BASS_SYNC_POS | BASSSync.BASS_SYNC_ONETIME, sigpos, delegOnPos, user);
    }

    //public void RemovePosSync() {
    //  Debug.WriteLine("RemovePosSync: StreamHandle = " + StreamHandle + "; PosSyncHandle = " + PosSyncHandle);
    //  if (PosSyncHandle != 0) {
    //    clsBassOut.CheckOK(Bass.BASS_ChannelRemoveSync(StreamHandle, PosSyncHandle));
    //    PosSyncHandle = 0;
    //  }
    //}

    public void RemoveEndSync() {
      if (EndSyncHandle != 0) {
        Bass.BASS_ChannelRemoveSync(StreamHandle, EndSyncHandle);
        EndSyncHandle = 0;
      }
    }
  }
}