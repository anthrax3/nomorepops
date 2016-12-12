namespace NoMorePops
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.panelMain = new System.Windows.Forms.Panel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.labelStatue = new System.Windows.Forms.Label();
            this.linkLabelCopyDownloadLink = new System.Windows.Forms.LinkLabel();
            this.labelArabic = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelAutoComplete = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.linkLabelDebug = new System.Windows.Forms.LinkLabel();
            this.button2 = new System.Windows.Forms.Button();
            this.rtxbx_Result = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.comboBox_tanslation = new System.Windows.Forms.ComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timerPoster = new System.Windows.Forms.Timer(this.components);
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMain.BackColor = System.Drawing.SystemColors.GrayText;
            this.panelMain.BackgroundImage = global::NoMorePops.Properties.Resources.shutterstock_154257524_1280x960;
            this.panelMain.Controls.Add(this.linkLabel2);
            this.panelMain.Controls.Add(this.labelStatue);
            this.panelMain.Controls.Add(this.linkLabelCopyDownloadLink);
            this.panelMain.Controls.Add(this.labelArabic);
            this.panelMain.Controls.Add(this.progressBar1);
            this.panelMain.Controls.Add(this.labelAutoComplete);
            this.panelMain.Controls.Add(this.linkLabel1);
            this.panelMain.Controls.Add(this.pictureBox1);
            this.panelMain.Controls.Add(this.buttonDownload);
            this.panelMain.Controls.Add(this.linkLabelDebug);
            this.panelMain.Controls.Add(this.button2);
            this.panelMain.Controls.Add(this.rtxbx_Result);
            this.panelMain.Controls.Add(this.label2);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.button1);
            this.panelMain.Controls.Add(this.textBoxInput);
            this.panelMain.Controls.Add(this.comboBox_tanslation);
            this.panelMain.Location = new System.Drawing.Point(4, 4);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(873, 482);
            this.panelMain.TabIndex = 0;
            // 
            // linkLabel2
            // 
            this.linkLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel2.LinkColor = System.Drawing.Color.Purple;
            this.linkLabel2.Location = new System.Drawing.Point(793, 461);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(35, 13);
            this.linkLabel2.TabIndex = 18;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "About";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AboutLnkLbl_LinkClicked);
            // 
            // labelStatue
            // 
            this.labelStatue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStatue.AutoSize = true;
            this.labelStatue.BackColor = System.Drawing.Color.Transparent;
            this.labelStatue.ForeColor = System.Drawing.Color.Yellow;
            this.labelStatue.Location = new System.Drawing.Point(9, 461);
            this.labelStatue.Name = "labelStatue";
            this.labelStatue.Size = new System.Drawing.Size(13, 13);
            this.labelStatue.TabIndex = 17;
            this.labelStatue.Text = "..";
            // 
            // linkLabelCopyDownloadLink
            // 
            this.linkLabelCopyDownloadLink.AutoSize = true;
            this.linkLabelCopyDownloadLink.BackColor = System.Drawing.Color.Transparent;
            this.linkLabelCopyDownloadLink.DisabledLinkColor = System.Drawing.Color.Transparent;
            this.linkLabelCopyDownloadLink.ForeColor = System.Drawing.Color.Blue;
            this.linkLabelCopyDownloadLink.Location = new System.Drawing.Point(58, 158);
            this.linkLabelCopyDownloadLink.Name = "linkLabelCopyDownloadLink";
            this.linkLabelCopyDownloadLink.Size = new System.Drawing.Size(105, 13);
            this.linkLabelCopyDownloadLink.TabIndex = 16;
            this.linkLabelCopyDownloadLink.TabStop = true;
            this.linkLabelCopyDownloadLink.Text = "Copy Download Link";
            this.linkLabelCopyDownloadLink.Visible = false;
            this.linkLabelCopyDownloadLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelCopyDownloadLink_LinkClicked);
            // 
            // labelArabic
            // 
            this.labelArabic.AutoSize = true;
            this.labelArabic.BackColor = System.Drawing.Color.Transparent;
            this.labelArabic.ForeColor = System.Drawing.Color.Red;
            this.labelArabic.Location = new System.Drawing.Point(351, 35);
            this.labelArabic.Name = "labelArabic";
            this.labelArabic.Size = new System.Drawing.Size(74, 13);
            this.labelArabic.TabIndex = 15;
            this.labelArabic.Text = "Not supported";
            this.labelArabic.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(211, 175);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(94, 12);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 14;
            // 
            // labelAutoComplete
            // 
            this.labelAutoComplete.AutoSize = true;
            this.labelAutoComplete.BackColor = System.Drawing.Color.Transparent;
            this.labelAutoComplete.ForeColor = System.Drawing.Color.Olive;
            this.labelAutoComplete.Location = new System.Drawing.Point(151, 35);
            this.labelAutoComplete.Name = "labelAutoComplete";
            this.labelAutoComplete.Size = new System.Drawing.Size(35, 13);
            this.labelAutoComplete.TabIndex = 13;
            this.labelAutoComplete.Text = "label3";
            this.labelAutoComplete.Visible = false;
            this.labelAutoComplete.Click += new System.EventHandler(this.labelAutoComplete_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.LinkColor = System.Drawing.Color.Black;
            this.linkLabel1.Location = new System.Drawing.Point(446, 61);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(31, 13);
            this.linkLabel1.TabIndex = 12;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Load";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(530, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(306, 167);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // buttonDownload
            // 
            this.buttonDownload.Location = new System.Drawing.Point(365, 116);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(75, 23);
            this.buttonDownload.TabIndex = 10;
            this.buttonDownload.Text = "Download";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // linkLabelDebug
            // 
            this.linkLabelDebug.AutoSize = true;
            this.linkLabelDebug.BackColor = System.Drawing.Color.Transparent;
            this.linkLabelDebug.LinkColor = System.Drawing.Color.Gainsboro;
            this.linkLabelDebug.Location = new System.Drawing.Point(27, 22);
            this.linkLabelDebug.Name = "linkLabelDebug";
            this.linkLabelDebug.Size = new System.Drawing.Size(15, 13);
            this.linkLabelDebug.TabIndex = 8;
            this.linkLabelDebug.TabStop = true;
            this.linkLabelDebug.Text = "D";
            this.linkLabelDebug.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDebug_LinkClicked);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(151, 116);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(163, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Watch";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // rtxbx_Result
            // 
            this.rtxbx_Result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxbx_Result.Location = new System.Drawing.Point(30, 193);
            this.rtxbx_Result.Name = "rtxbx_Result";
            this.rtxbx_Result.Size = new System.Drawing.Size(808, 247);
            this.rtxbx_Result.TabIndex = 6;
            this.rtxbx_Result.Text = "";
            this.rtxbx_Result.TextChanged += new System.EventHandler(this.rtxbx_Result_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(286, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Translation";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(55, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Type a name";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(55, 116);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Get";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(151, 54);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(289, 20);
            this.textBoxInput.TabIndex = 3;
            this.textBoxInput.Text = "Limitless";
            this.textBoxInput.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // comboBox_tanslation
            // 
            this.comboBox_tanslation.FormattingEnabled = true;
            this.comboBox_tanslation.Items.AddRange(new object[] {
            "None",
            "Arabic",
            "English"});
            this.comboBox_tanslation.Location = new System.Drawing.Point(351, 80);
            this.comboBox_tanslation.Name = "comboBox_tanslation";
            this.comboBox_tanslation.Size = new System.Drawing.Size(89, 21);
            this.comboBox_tanslation.TabIndex = 4;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // timerPoster
            // 
            this.timerPoster.Interval = 1000;
            this.timerPoster.Tick += new System.EventHandler(this.timerPoster_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 490);
            this.Controls.Add(this.panelMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "NoMorePops                         v 0.1-Beta";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.ComboBox comboBox_tanslation;
        private System.Windows.Forms.RichTextBox rtxbx_Result;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.LinkLabel linkLabelDebug;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        public System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label labelAutoComplete;
        private System.Windows.Forms.Timer timerPoster;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelArabic;
        private System.Windows.Forms.LinkLabel linkLabelCopyDownloadLink;
        private System.Windows.Forms.Label labelStatue;
        private System.Windows.Forms.LinkLabel linkLabel2;
    }
}

