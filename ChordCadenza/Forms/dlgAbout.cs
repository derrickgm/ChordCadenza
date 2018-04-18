using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using System.Reflection;
using System.Runtime.InteropServices;

namespace ChordCadenza.Forms {
  public partial class dlgAbout : Form {
    public dlgAbout() {
      InitializeComponent();
    }

    private void frmAbout_Load(object sender, EventArgs e) {
      lblVRMF.Text = "Chord Cadenza " + Application.ProductVersion;
      BackColor = Utils.SetBackColor(Forms.frmSC.Mtx, BackColor);
      txtInfo.SelectionStart = 0;
      txtInfo.SelectionLength = 0;
      lblVRMF.Text += (Environment.Is64BitProcess) ? " 64-bit" : " 32-bit";
#if DEBUG
      lblVRMF.Text += " (Debug)";
#endif
      //* license locations
      //* -----------------
      //* here
      //* NSIS install directory (MIT, freeware)
      //* website
      txtInfo.Lines = Utils.ReadLines(Cfg.LicenseFilePath).ToArray();
      txtInfo.Select(0, 0);
    }

    private void cmdOK_Click(object sender, EventArgs e) {
      Close();
    }

    private void lnkHomePage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
      Process.Start("http://" + lnkHomePage.Text);
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
      Process.Start("mailto:" + lnkEmail.Text);
    }
  }
}
