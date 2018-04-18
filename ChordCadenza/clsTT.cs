using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace ChordCadenza {
  internal class clsTT {
    internal class clsForm {
      internal static readonly string[] StaticForms = new string[] {
        "Generic",
        "frmColoursNM",
        "frmColoursSC",
        "frmColoursTonnetz",
        "frmAutoSync",
        "frmCalcKeys",
        "frmCfgBezier",
        "frmConfigBass",
        "frmFX",
        "frmInitial",
        "frmMidiDevs",
        "frmTrackMap",
        "frmChordMap",
        "frmSC",
        "frmSCAlign",
        "frmSCOctaves",
        "frmStart",
        "frmSummary",
        "frmSwitch",
        "frmNewProject",
        "frmSaveAs",
        "frmCHP",
        "frmSaveMidiFileAs",
        "frmTonnetz",
        "frmManChordSync"
      }; 
      internal string Form;  //without '$$'
      internal List<string> Ctrls;  //without '$'
      internal List<List<string>> HTML;

      internal clsForm(string form) {
        Form = form;
        Ctrls = new List<string>();
        HTML = new List<List<string>>();
      }
    }

    internal struct sTTcmd {
      internal string Name;  //eg "cmdClose"
      internal string Text;  //eg "Click to close window"
      internal List<string> HTML;
      internal sTTcmd(string name, List<string> html) {
        Name = name;
        //Text = text;
        HTML = html;
        Text = HTMLToText(HTML);
      }
    }

    //* key: $$<formname>.$<controlname> or $$Generic.$<controlname>
    private static Dictionary<string, sTTcmd> DictTTcmds = new Dictionary<string, sTTcmd>();
    internal static List<ToolStripMenuItem> frmSC_MenuItems;
    internal static List<ToolStripMenuItem> frmAutoSync_MenuItems;

    static clsTT() {
      string[] ttfrmctl;
      string ttprevfrm = "";
      string ttthisfrm = "";
      char[] delimdot = new char[] { '.' };
      List<clsForm> frms = new List<clsForm>();
      List<string> lines = GetLines(Cfg.ToolTipsFilePath);
      clsForm frm = null;
      List<string> html = null;

      foreach (string l in lines) {
        string ltrim = l.Trim();
        if (ltrim.StartsWith("$$")) {
          ttfrmctl = ltrim.Split(delimdot);
          if (ttfrmctl.Length != 2 || !ttfrmctl[1].StartsWith("$")) {
            LogicError.Throw(eLogicError.X136);
          }
          ttthisfrm = ttfrmctl[0].Substring(2);  //omit '$$'
          if (ttthisfrm != ttprevfrm) {  //check if form already created
            frm = CheckNewFrm(ttthisfrm, frms, frm);
            ttprevfrm = ttthisfrm;
          }
          frm.Ctrls.Add(ttfrmctl[1].Substring(1));  //omit '$'
          html = new List<string>();
          frm.HTML.Add(html);
        } else if (ltrim.StartsWith("$")) {
          LogicError.Throw(eLogicError.X136);
        } else {
          if (l != "") html.Add(l);
        }
      }

      for (int i = 0; i < frms.Count; i++) {
        for (int j = frms[i].HTML.Count - 2; j >= 0; j--) {  //propagate last to first
          if (frms[i].HTML[j].Count == 0) frms[i].HTML[j] = frms[i].HTML[j + 1];
        }
      }

      foreach (clsForm f in frms) {
        LoadStaticToolTips(frms, f);
      }
    }

    private static clsForm CheckNewFrm(string ttthisfrm, List<clsForm> frms, clsForm frm) {
      foreach (clsForm f in frms) {
        if (f.Form == ttthisfrm) {
          LogicError.Throw(eLogicError.X137);
          return frm;
        }
      }
      //if (!clsForm.StaticForms.Contains(ttthisfrm) && ttthisfrm != "frmNewProject") {
      if (!clsForm.StaticForms.Contains(ttthisfrm)) {
        LogicError.Throw(eLogicError.X138);
      }
      frm = new clsForm(ttthisfrm);
      frms.Add(frm);
      return frm;
    }

    internal static void LoadStaticToolTips(List<clsTT.clsForm> frms, clsTT.clsForm frm) {
      for (int i = 0; i < frm.Ctrls.Count; i++) {
        string ctrl = frm.Ctrls[i];
        //string text = clsTT.HTMLToText(frm.HTML[i]);
        sTTcmd ttcmd = new sTTcmd(ctrl, frm.HTML[i]);
        DictTTcmds.Add("$$" + frm.Form + ".$" + ctrl, ttcmd);
      }

      for (int i = 0; i < frms[0].Ctrls.Count; i++) {
        string ctrl = frms[0].Ctrls[i];
        //string text = clsTT.HTMLToText(frms[0].HTML[i]);
        sTTcmd ttcmd = new sTTcmd(ctrl, frms[0].HTML[i]);
        string key = "$$Generic.$" + ctrl;
        if (!DictTTcmds.ContainsKey(key)) DictTTcmds.Add("$$Generic.$" + ctrl, ttcmd);
      }
      //if (form != null) LoadToolTips(form);  //static -> form instance
    }

    internal static void LoadToolTips(Form form) {
      if (!(form is ITT)) LogicError.Throw(eLogicError.X141, form.Name);
      ToolTip tt;
      LoadToolTips(form, out tt);  
    }

    internal static void LoadToolTips(Form form, out ToolTip tt) {
      //return;  //temp
      //* Create the ToolTip and associate with the Form container.
      //ToolTip tt = new ToolTip();
      // Set up the delays for the ToolTip.
      //DictFormTT.Remove(form.Name);
      tt = new ToolTip();
      tt.Active = P.frmStart.chkTTActive.Checked;
      tt.AutoPopDelay = 15000;
      tt.InitialDelay = 1000;
      tt.ReshowDelay = 500;
      // Force the ToolTip text to be displayed whether or not the form is active.
      tt.ShowAlways = true;
      if (form is ITT) ((ITT)form).TT = tt;
      //DictFormTT.Add(form.Name, tt);

      List<Control> ctls = new List<Control>();
      List<ToolStripMenuItem> items = new List<ToolStripMenuItem>();
      GetAllControls(form, ctls, items);

      if (form.Name == "frmSC") frmSC_MenuItems = items;
      else if (form.Name == "frmAutoSync") frmAutoSync_MenuItems = items;

      foreach (Control ctrl in ctls) LoadToolTipCtrl(tt, form, ctrl);
      foreach (ToolStripMenuItem item in items) LoadToolTipMenuItem(form, item);

#if DEBUG
      CheckCtls(form, ctls, items);
#endif
    }

#if DEBUG
    private static void CheckCtls(Form frm, List<Control> ctls, List<ToolStripMenuItem> items) {
      //* check for any controls in dict that are not in ctls 
      //* slow and inefficient - DEBUG only
      Debug.WriteLine("Checking Controls in Form: " + frm.Name); 
      char[] delim = new char[] { '.' };
      foreach (string key in DictTTcmds.Keys) {
        if (!key.StartsWith("$$" + frm.Name)) continue;
        string[] keyarr = key.Split(delim);
        bool hit = false;
        if (keyarr[1].StartsWith("$Trk:")) {
          continue;  //not checked
        } else if (keyarr[1].StartsWith("$mnu")) {
          foreach (ToolStripMenuItem item in items) {
            if (keyarr[1] == "$" + item.Name) {
              hit = true;
              break;
            }
          }
        } else {
          foreach (Control ctl in ctls) {
            if (keyarr[1] == "$" + ctl.Name) {
              hit = true;
              break;
            }
          }
        }
        if (!hit) Debug.WriteLine("Key: " + key + " in Dictionary not present in Form");
      }
    }
#endif

    internal static void LoadToolTipCtrl(ToolTip tt, Form form, Control ctrl) {
      string key = LoadToolTip(form.Name, ctrl.Name);
      //if (key != "") DictFormTT[form.Name].SetToolTip(ctrl, DictTTcmds[key].Text);
      if (key != "") tt.SetToolTip(ctrl, DictTTcmds[key].Text);
    }

    internal static void LoadToolTipCtrlMM(ToolTip tttrks, Form form, Control ctrl, string name) {
      //* called from frmMultiMap
      string key = LoadToolTip(form.Name, name);
      if (key != "") tttrks.SetToolTip(ctrl, DictTTcmds[key].Text);
    }

    internal static void LoadToolTipMenuItem(Form form, ToolStripMenuItem item) {
      //* ShowItemToolTips doesn't seem to work
      //* this method is a getaround - can't switch off or on without restarting app
      if (P.frmStart.chkTTActive.Checked) {
        string key = LoadToolTip(form.Name, item.Name);
        if (key != "") item.ToolTipText = DictTTcmds[key].Text;
      } else {
        item.ToolTipText = "";
      }
    }

    private static string LoadToolTip(string formname, string ctrlname) {
      if (ctrlname == null || ctrlname == "") return "";
      string key = "$$" + formname + ".$" + ctrlname;
      string formkey = key;
      if (!DictTTcmds.ContainsKey(key)) {
        key = "$$Generic.$" + ctrlname;
        if (!DictTTcmds.ContainsKey(key)) {
          Debug.WriteLine("Info: clsTT.LoadToolTips: key not found: " + formkey);
          //Debug.WriteLine(formkey);
          return "";
        }
      } else {  //contains non-generic key
        if (DictTTcmds[key].Text.StartsWith("*")) return "";  //don't load tooltip
      }
      return key;
    }

    internal static void GetAllControls(Control container, List<Control> ctls, List<ToolStripMenuItem> items) {
      foreach (Control c in container.Controls) {
        if (c is MenuStrip) {
          MenuStrip menustrip = (MenuStrip)c;
          if (menustrip.Items.Count > 0) {
            menustrip.ShowItemToolTips = P.frmStart.chkTTActive.Checked;  //doesn't seem to work
            GetAllMenuStripItems(menustrip, items);
          } else {
            return;  //empty menustrip
          }
        } else {
          if (c.Controls.Count > 0) {
            GetAllControls(c, ctls, items);
            if (c is Form || c is Panel || c is SplitContainer || c is SplitterPanel) continue;
          }
          ctls.Add(c);  //no need to add container to list (except nud etc.)
        }
      }
    }

    private static void GetAllMenuStripItems(MenuStrip container, List<ToolStripMenuItem> items) {
      foreach (ToolStripMenuItem item in container.Items) {
        if (item.DropDownItems.Count > 0) GetAllToolStripMenuStripItems(item, items);
        else items.Add(item);  //no need to add container to list 
      }
    }

    private static void GetAllToolStripMenuStripItems(ToolStripMenuItem container, List<ToolStripMenuItem> items) {
      foreach (ToolStripMenuItem item in container.DropDownItems.OfType<ToolStripMenuItem>()) {
        if (item.DropDownItems.Count > 0) GetAllToolStripMenuStripItems(item, items);
        else items.Add(item);  //no need to add container to list 
      }
    }

    internal static string HTMLToText(List<string> html) {
      string txt = "";
      //* pattern for <p> </p> <b> </b> <span> </span> <a href="xxx"> </a> 
      //* remove matched patterns
      for (int i = 0; i < html.Count; i++) {  
        string s = html[i].Trim();
        //if (s.Contains("Close this window")) Debugger.Break();
        string line = Regex.Replace(s, @"<[A-Za-z/_"" =\.]*>", "", RegexOptions.None);
        if (line != "") txt += line + "\r\n";
      }
      return txt;
    }

#if ADVANCED
    internal static void CopyHTMLFiles() {
      //if (!P.Advanced || !P.Debug) {
      //  MessageBox.Show("CopyHTMLFiles failed - invalid context");
      //  return;
      //}
      string path = Cfg.CfgPath + "\\MainHelp";
      //* copy and update html files (after creating backup of Out file)
      //* $<name> -->
      string[] files = Directory.GetFiles(path + "\\In");
      //string backupdir = path + "\\OutBU" + DateTime.Now.Ticks;
      //Directory.CreateDirectory(backupdir);
      foreach (string f in files) {
        string filename = Path.GetFileName(f);
        string filenameXext = Path.GetFileNameWithoutExtension(f);
        string outf = path + "\\Out\\" + filename;
        //string backupf = backupdir + "\\" + filename;
        if (f.EndsWith(".htm")) {
          //if (!File.Exists(outf)) {
          //  if (MessageBox.Show("Output file: " + outf + " not found for backup - continue?",
          //  MessageBoxButtons.YesNo) == DialogResult.No) return;
          //} else {
          //  File.Copy(outf, backupf, false);  //no overwrite (should get IOException)
          //}
          List<string> inlines = GetLines(f);
          List<string> outlines = GetLines(path + @"\Header.html");

          {  //insert title
            int i;
            for (i = 0; i < outlines.Count; i++) {
              if (outlines[i].StartsWith("<title>")) {
                outlines[i] = "<title>" + filenameXext + "</title>";
                break;
              }
            }
            if (i >= outlines.Count) Debugger.Break();
          }

          foreach (string l in inlines) {
            string ltrim = l.Trim();
            if (ltrim.StartsWith("$$")) {  //format: $$form.$ctrl
              if (DictTTcmds.ContainsKey(ltrim)) {
                outlines.AddRange(DictTTcmds[ltrim].HTML);
                continue;
              } else {
                Debug.WriteLine("clsTT.CopyHTMLFiles:key " + ltrim + " not found");
                LogicError.Throw(eLogicError.X133);
              }
            } else if (ltrim.StartsWith("$")) {
              Debug.WriteLine("clsTT.CopyHTMLFiles:key " + ltrim + " invalid '$'");
              LogicError.Throw(eLogicError.X135);
            }
            outlines.Add(l);
          }
          outlines.AddRange(GetLines(path + @"\Tail.html"));
          SaveLines(outf, outlines);  //overwrite if necessary
        } else {  //not .htm
          LogicError.Throw(eLogicError.X134);
          //File.Copy(f, outf, true);  //overwrite if necessary
        }
      }
    }
#endif

    //private static List<string> TempRemoveOldHeadAndTail(List<string> lines) {
    //  List<string> ret = new List<string>();
    //  int i;
    //  for (i = 0; i < lines.Count; i++) {
    //    if (lines[i].TrimStart().StartsWith("<body>")) break;
    //  }
    //  if (!lines[i].TrimStart().StartsWith("<body>")) Debugger.Break();
    //  for (i = i + 1; i < lines.Count; i++) {
    //    if (lines[i].TrimStart().StartsWith("<object")) break;
    //    if (lines[i].TrimStart().StartsWith("</body>")) break;
    //    ret.Add(lines[i]);
    //  }
    //  if (lines[i].TrimStart().StartsWith("</body>")) return ret;
    //  if (lines[i].TrimStart().StartsWith("<object")) return ret;
    //  Debugger.Break();
    //  return null;
    //}

    //private static List<string> GetLines(string path) {
    //  List<string> lines;
    //  Utils.LoadFile(path, LoadFileSub, out lines);
    //  return lines;
    //}

    private static List<string> GetLines(string path) {
      List<string> inlines = Utils.ReadLines(path);
      if (inlines == null) return new List<string>(0);
      List<string> outlines = new List<string>();
      char[] space = new char[] { ' ' };
      foreach (string l in inlines) {
        if (l.TrimStart(space).StartsWith(@"//*")) continue;
        outlines.Add(l);
        //*throw new IOException("Testing");
      }
      return outlines;
    }

#if ADVANCED
    private static void SaveLines(string path, List<string> lines) {
      //* called by CopyHTML (P.Advanced only)
      try {
        FileStream st = new FileStream(path, FileMode.Create, FileAccess.Write);
        using (StreamWriter sw = new StreamWriter(st)) {
          foreach (string l in lines) {
            sw.WriteLine(l);
          }
        }
      }
      catch {
        Debugger.Break();
      }
    }
#endif
  }
}