using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace ChordCadenza.Forms {
  public partial class frmPCKBCfg : Form {
    private Label[,] LblPCs = new Label[4, 12];
    private Label[,] LblKBs = new Label[4, 12];
    private RadioButton[,] Opts = new RadioButton[4, 12];

    public frmPCKBCfg() {
      InitializeComponent();
    }

    private void frmPCKBCfg_Load(object sender, EventArgs e) {
      Point origin = new Point(lblBottomOctave.Left, lblBottomOctave.Bottom + 10);
      for (int oct = 0; oct < 4; oct++) {
        for (int pc = 0; pc < 12; pc++) {
          Keys key = clsPCKB.GetKB(oct, pc);

          //* set up labels (pitch classes)
          Label lblpc = new Label();
          LblPCs[oct, pc] = lblpc;
          panMain.Controls.Add(lblpc);
          lblpc.AutoSize = true;
          lblpc.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
          lblpc.Left = origin.X + oct * 200;
          lblpc.Top =  origin.Y + 2 + pc * (lblpc.Height + 5);
          lblpc.TabIndex = oct * 12 + pc;
          lblpc.Text = NoteName.MajKeyNames[pc];

          //* set up labels (pitch keyboard chars)
          Label lblkb = new Label();
          LblKBs[oct, pc] = lblkb;
          panMain.Controls.Add(lblkb);
          lblkb.AutoSize = true;
          lblkb.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
          lblkb.Left = origin.X + 25 + oct * 200;
          lblkb.Top =  origin.Y + 2 + pc * (lblkb.Height + 5);
          lblkb.TabIndex = oct * 12 + pc;
          lblkb.Text = clsPCKB.GetChars(key);

          //* set up opts
          RadioButton opt = new RadioButton();
          Opts[oct, pc] = opt;
          panMain.Controls.Add(opt);
          opt.AutoSize = true;
          opt.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
          opt.Left = origin.X + 55 + oct * 200;
          opt.Top = origin.Y + pc * (lblpc.Height + 5);
          opt.TabIndex = oct * 12 + pc;
          opt.TabStop = false;
          opt.UseVisualStyleBackColor = true;
          //opt.KeyPress += opt_KeyPress;
          opt.KeyDown += opt_KeyDown;
          opt.KeyUp += opt_KeyUp;
          opt.PreviewKeyDown += opt_PreviewKeyDown;
          opt.Text = key.ToString();
          opt.Tag = oct * 12 + pc;
        }
      }
      Opts[0, 0].Checked = true;
      ActiveControl = Opts[0, 0];
    }

    private void LoadKB() {
      for (int oct = 0; oct < 4; oct++) {
        for (int pc = 0; pc < 12; pc++) {
          Keys key = clsPCKB.GetKB(oct, pc);
          LblKBs[oct, pc].Text = clsPCKB.GetChars(key);
          Opts[oct, pc].Text = key.ToString();
        }
      }
      P.PCKB?.InitData();

    }

    //private void opt_KeyPress(object sender, KeyPressEventArgs e) {
    //  Debug.WriteLine("KeyPress: <" + e.KeyChar + ">");
    //}

    private void opt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
      //if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape || e.KeyCode == Keys.Tab
      //  || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down 
      //  || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right) {
      //  e.IsInputKey = true;
      //}
      //if (Forms.frmSC.KeyUpDownFilter(e)) return;
      e.IsInputKey = true;
    }

    private void opt_KeyDown(object sender, KeyEventArgs e) {
      RadioButton opt = (RadioButton)sender;
      int oct = (int)opt.Tag / 12;
      int pc = ((int)opt.Tag).Mod12();

      if (e.KeyCode >= Keys.F1 && e.KeyCode <= Keys.F12) {
        return;
      } else if (e.KeyCode == Keys.Space) {
        opt.Text = Keys.None.ToString();
        LblKBs[oct, pc].Text = "";
      } else {
        opt.Text = e.KeyCode.ToString();
        LblKBs[oct, pc].Text = clsPCKB.GetChars(e.KeyCode);
      }
      Debug.WriteLine("KeyDown: Chars: <" + clsPCKB.GetChars(e.KeyCode) + ">");
    }

    private void opt_KeyUp(object sender, KeyEventArgs e) {
      if (e.KeyCode >= Keys.F1 && e.KeyCode <= Keys.F12) return;
      if (e.KeyCode == Keys.PrintScreen) return;  //keydown not recognised
      RadioButton opt = (RadioButton)sender;
      if (opt.TabIndex < 47) {  //not last one
        SelectNextControl(opt, true, false, true, false);
        GetOpt(opt.TabIndex + 1).Checked = true;  //move to next
      }
    }

    private RadioButton GetOpt(int seq) {
      int oct = seq / 12;
      int pc = seq.Mod12();
      return Opts[oct, pc];
    }

    private void frmPCKBCfg_FormClosed(object sender, FormClosedEventArgs e) {
      P.frmPCKB = null;
    }

    private bool Apply() {
      //* return false if not applied
      Keys[] newkb = new Keys[48];
      for (int oct = 0; oct < 4; oct++) {
        for (int pc = 0; pc < 12; pc++) {
          Keys key = (Keys)Enum.Parse(typeof(Keys), Opts[oct, pc].Text);
          if (key != Keys.None && newkb.Contains(key)) {
            string msg = "Duplicate keystroke <" + Opts[oct, pc].Text + "> found";
            msg += "\r\nChanges not applied";
            MessageBox.Show(msg);
            return false;
          }
          newkb[oct * 12 + pc] = key;
        }
      }
      clsPCKB.KB = newkb;
      P.PCKB?.InitData();
      P.frmSC.Refresh();
      return true;
    }

    private void cmdApply_Click(object sender, EventArgs e) {
      Apply();
    }

    private void cmdOK_Click(object sender, EventArgs e) {
      if (Apply()) Close();
    }

    private void cmdCancel_Click(object sender, EventArgs e) {
      Close();
    }

    private void cmdDefault24_Click(object sender, EventArgs e) {
      clsPCKB.KB = clsPCKB.KB24.ToArray();
      LoadKB();
    }

    private void cmdDefault44_Click(object sender, EventArgs e) {
      clsPCKB.KB = clsPCKB.KB44.ToArray();
      LoadKB();
    }

    private void cmdClear_Click(object sender, EventArgs e) {
      clsPCKB.KB = new Keys[48];
      for (int i = 0; i < 48; i++) clsPCKB.KB[i] = Keys.None;
      LoadKB();
    }
  }
}
