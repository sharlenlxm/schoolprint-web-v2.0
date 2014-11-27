namespace SchoolPrint
{
    partial class ListboxItem
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblFileName = new System.Windows.Forms.Label();
            this.lblDownSpeed = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblFileId = new System.Windows.Forms.Label();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.timDown = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.AutoPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFileName
            // 
            this.lblFileName.Location = new System.Drawing.Point(88, 8);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(244, 20);
            this.lblFileName.TabIndex = 1;
            this.lblFileName.Text = "大学语文.doc";
            this.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFileName.MouseLeave += new System.EventHandler(this.ListboxItem_MouseLeave);
            this.lblFileName.MouseHover += new System.EventHandler(this.ListboxItem_MouseHover);
            // 
            // lblDownSpeed
            // 
            this.lblDownSpeed.Location = new System.Drawing.Point(86, 51);
            this.lblDownSpeed.Name = "lblDownSpeed";
            this.lblDownSpeed.Size = new System.Drawing.Size(244, 20);
            this.lblDownSpeed.TabIndex = 2;
            this.lblDownSpeed.Text = "512kB/s";
            this.lblDownSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDownSpeed.MouseLeave += new System.EventHandler(this.ListboxItem_MouseLeave);
            this.lblDownSpeed.MouseHover += new System.EventHandler(this.ListboxItem_MouseHover);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(88, 30);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(321, 18);
            this.progressBar.TabIndex = 3;
            this.progressBar.MouseLeave += new System.EventHandler(this.ListboxItem_MouseLeave);
            this.progressBar.MouseHover += new System.EventHandler(this.ListboxItem_MouseHover);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(467, 8);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(44, 22);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "button1";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnDelete.MouseLeave += new System.EventHandler(this.ListboxItem_MouseLeave);
            this.btnDelete.MouseHover += new System.EventHandler(this.ListboxItem_MouseHover);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Location = new System.Drawing.Point(415, 8);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(46, 22);
            this.btnOpen.TabIndex = 5;
            this.btnOpen.Text = "button2";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            this.btnOpen.MouseLeave += new System.EventHandler(this.ListboxItem_MouseLeave);
            this.btnOpen.MouseHover += new System.EventHandler(this.ListboxItem_MouseHover);
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgress.Location = new System.Drawing.Point(237, 51);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(172, 20);
            this.lblProgress.TabIndex = 7;
            this.lblProgress.Text = "label3";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblProgress.Visible = false;
            this.lblProgress.Click += new System.EventHandler(this.lblProgress_Click);
            this.lblProgress.MouseLeave += new System.EventHandler(this.ListboxItem_MouseLeave);
            this.lblProgress.MouseHover += new System.EventHandler(this.ListboxItem_MouseHover);
            // 
            // lblFileId
            // 
            this.lblFileId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFileId.Location = new System.Drawing.Point(259, 7);
            this.lblFileId.Name = "lblFileId";
            this.lblFileId.Size = new System.Drawing.Size(150, 20);
            this.lblFileId.TabIndex = 6;
            this.lblFileId.Text = "yy071141";
            this.lblFileId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblFileId.Visible = false;
            this.lblFileId.MouseLeave += new System.EventHandler(this.ListboxItem_MouseLeave);
            this.lblFileId.MouseHover += new System.EventHandler(this.ListboxItem_MouseHover);
            // 
            // picImage
            // 
            this.picImage.BackgroundImage = global::SchoolPrint.Properties.Resources.lovely;
            this.picImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picImage.Location = new System.Drawing.Point(8, 4);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(72, 72);
            this.picImage.TabIndex = 0;
            this.picImage.TabStop = false;
            this.picImage.MouseLeave += new System.EventHandler(this.ListboxItem_MouseLeave);
            this.picImage.MouseHover += new System.EventHandler(this.ListboxItem_MouseHover);
            // 
            // timDown
            // 
            this.timDown.Enabled = true;
            this.timDown.Interval = 300;
            this.timDown.Tick += new System.EventHandler(this.timDown_Tick);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(424, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "金额";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(459, 48);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(66, 21);
            this.textBox1.TabIndex = 9;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // AutoPrint
            // 
            this.AutoPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AutoPrint.Location = new System.Drawing.Point(517, 8);
            this.AutoPrint.Name = "AutoPrint";
            this.AutoPrint.Size = new System.Drawing.Size(44, 22);
            this.AutoPrint.TabIndex = 10;
            this.AutoPrint.Text = "打印";
            this.AutoPrint.UseVisualStyleBackColor = true;
            this.AutoPrint.Click += new System.EventHandler(this.AutoPrint_Click);
            // 
            // ListboxItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.AutoPrint);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.lblFileId);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblDownSpeed);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.picImage);
            this.MinimumSize = new System.Drawing.Size(450, 80);
            this.Name = "ListboxItem";
            this.Size = new System.Drawing.Size(567, 80);
            this.Load += new System.EventHandler(this.ListboxItem_Load);
            this.MouseLeave += new System.EventHandler(this.ListboxItem_MouseLeave);
            this.MouseHover += new System.EventHandler(this.ListboxItem_MouseHover);
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Label lblDownSpeed;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Label lblFileId;
        private System.Windows.Forms.Timer timDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button AutoPrint;
    }
}
