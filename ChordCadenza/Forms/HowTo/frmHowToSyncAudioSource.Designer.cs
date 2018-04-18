namespace MPlay.Forms.HowTo {
  partial class frmHowToSyncAudioSource {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHowToSyncAudioSource));
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.textBox3 = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cmdSyncStream = new System.Windows.Forms.Button();
      this.cmdSyncFile = new System.Windows.Forms.Button();
      this.cmdPlayStream = new System.Windows.Forms.Button();
      this.cmdPlayFile = new System.Windows.Forms.Button();
      this.cmdLiveStream = new System.Windows.Forms.Button();
      this.textBox4 = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.cmdSyncComp = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // textBox1
      // 
      this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox1.Location = new System.Drawing.Point(27, 42);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new System.Drawing.Size(469, 104);
      this.textBox1.TabIndex = 61;
      this.textBox1.TabStop = false;
      this.textBox1.Text = resources.GetString("textBox1.Text");
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(12, 20);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(75, 13);
      this.label5.TabIndex = 60;
      this.label5.Text = "Introduction";
      // 
      // textBox2
      // 
      this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox2.Location = new System.Drawing.Point(24, 324);
      this.textBox2.Multiline = true;
      this.textBox2.Name = "textBox2";
      this.textBox2.ReadOnly = true;
      this.textBox2.Size = new System.Drawing.Size(469, 118);
      this.textBox2.TabIndex = 63;
      this.textBox2.TabStop = false;
      this.textBox2.Text = resources.GetString("textBox2.Text");
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(9, 302);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(196, 13);
      this.label1.TabIndex = 62;
      this.label1.Text = "Choosing Stream Synchronisation";
      // 
      // textBox3
      // 
      this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox3.Location = new System.Drawing.Point(27, 201);
      this.textBox3.Multiline = true;
      this.textBox3.Name = "textBox3";
      this.textBox3.ReadOnly = true;
      this.textBox3.Size = new System.Drawing.Size(469, 70);
      this.textBox3.TabIndex = 65;
      this.textBox3.TabStop = false;
      this.textBox3.Text = resources.GetString("textBox3.Text");
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(12, 179);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(177, 13);
      this.label2.TabIndex = 64;
      this.label2.Text = "Choosing File Synchronisation";
      // 
      // cmdSyncStream
      // 
      this.cmdSyncStream.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdSyncStream.Location = new System.Drawing.Point(509, 324);
      this.cmdSyncStream.Name = "cmdSyncStream";
      this.cmdSyncStream.Size = new System.Drawing.Size(208, 30);
      this.cmdSyncStream.TabIndex = 67;
      this.cmdSyncStream.Text = "HowTo Sync to a Stream";
      this.cmdSyncStream.UseVisualStyleBackColor = true;
      this.cmdSyncStream.Click += new System.EventHandler(this.cmdSyncStream_Click);
      // 
      // cmdSyncFile
      // 
      this.cmdSyncFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdSyncFile.Location = new System.Drawing.Point(512, 201);
      this.cmdSyncFile.Name = "cmdSyncFile";
      this.cmdSyncFile.Size = new System.Drawing.Size(208, 30);
      this.cmdSyncFile.TabIndex = 68;
      this.cmdSyncFile.Text = "HowTo Sync to a File";
      this.cmdSyncFile.UseVisualStyleBackColor = true;
      this.cmdSyncFile.Click += new System.EventHandler(this.cmdSyncFile_Click);
      // 
      // cmdPlayStream
      // 
      this.cmdPlayStream.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdPlayStream.Location = new System.Drawing.Point(509, 360);
      this.cmdPlayStream.Name = "cmdPlayStream";
      this.cmdPlayStream.Size = new System.Drawing.Size(208, 48);
      this.cmdPlayStream.TabIndex = 69;
      this.cmdPlayStream.Text = "HowTo Play to a File Stream";
      this.cmdPlayStream.UseVisualStyleBackColor = true;
      this.cmdPlayStream.Click += new System.EventHandler(this.cmdPlayStream_Click);
      // 
      // cmdPlayFile
      // 
      this.cmdPlayFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdPlayFile.Location = new System.Drawing.Point(512, 237);
      this.cmdPlayFile.Name = "cmdPlayFile";
      this.cmdPlayFile.Size = new System.Drawing.Size(208, 48);
      this.cmdPlayFile.TabIndex = 70;
      this.cmdPlayFile.Text = "HowTo Play to a File or Stream";
      this.cmdPlayFile.UseVisualStyleBackColor = true;
      this.cmdPlayFile.Click += new System.EventHandler(this.cmdPlayFile_Click);
      // 
      // cmdLiveStream
      // 
      this.cmdLiveStream.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdLiveStream.Location = new System.Drawing.Point(509, 502);
      this.cmdLiveStream.Name = "cmdLiveStream";
      this.cmdLiveStream.Size = new System.Drawing.Size(208, 48);
      this.cmdLiveStream.TabIndex = 74;
      this.cmdLiveStream.Text = "HowTo Play to a Live \r\nStream";
      this.cmdLiveStream.UseVisualStyleBackColor = true;
      this.cmdLiveStream.Click += new System.EventHandler(this.cmdLiveStream_Click);
      // 
      // textBox4
      // 
      this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox4.Location = new System.Drawing.Point(24, 502);
      this.textBox4.Multiline = true;
      this.textBox4.Name = "textBox4";
      this.textBox4.ReadOnly = true;
      this.textBox4.Size = new System.Drawing.Size(469, 86);
      this.textBox4.TabIndex = 72;
      this.textBox4.TabStop = false;
      this.textBox4.Text = resources.GetString("textBox4.Text");
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(9, 480);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(181, 13);
      this.label3.TabIndex = 71;
      this.label3.Text = "Choosing Live Synchronisation";
      // 
      // cmdSyncComp
      // 
      this.cmdSyncComp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cmdSyncComp.Location = new System.Drawing.Point(512, 42);
      this.cmdSyncComp.Name = "cmdSyncComp";
      this.cmdSyncComp.Size = new System.Drawing.Size(208, 30);
      this.cmdSyncComp.TabIndex = 75;
      this.cmdSyncComp.Text = "Sync Comparison Table";
      this.cmdSyncComp.UseVisualStyleBackColor = true;
      this.cmdSyncComp.Click += new System.EventHandler(this.cmdSyncComp_Click);
      // 
      // frmHowToSyncAudioSource
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(737, 607);
      this.Controls.Add(this.cmdSyncComp);
      this.Controls.Add(this.cmdLiveStream);
      this.Controls.Add(this.textBox4);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.cmdPlayFile);
      this.Controls.Add(this.cmdPlayStream);
      this.Controls.Add(this.cmdSyncFile);
      this.Controls.Add(this.cmdSyncStream);
      this.Controls.Add(this.textBox3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.textBox2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.label5);
      this.Name = "frmHowToSyncAudioSource";
      this.Text = "HowTo Sync to an Audio Source";
      this.Load += new System.EventHandler(this.frmHowToSync_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBox3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button cmdSyncStream;
    private System.Windows.Forms.Button cmdSyncFile;
    private System.Windows.Forms.Button cmdPlayStream;
    private System.Windows.Forms.Button cmdPlayFile;
    private System.Windows.Forms.Button cmdLiveStream;
    private System.Windows.Forms.TextBox textBox4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button cmdSyncComp;
  }
}