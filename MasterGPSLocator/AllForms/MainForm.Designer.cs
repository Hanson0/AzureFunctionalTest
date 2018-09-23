namespace MasterGPSLocator
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblStopWatchLabel = new System.Windows.Forms.Label();
            this.txtUart = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lblStopWatch = new System.Windows.Forms.Label();
            this.txtYeild = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更改计划单号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.信息看板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblYeild = new System.Windows.Forms.Label();
            this.lblClear = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtFail = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.txtLabelSn = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblFail = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.lblLabelSn = new System.Windows.Forms.Label();
            this.frpStopWatch = new System.Windows.Forms.GroupBox();
            this.grpResult = new System.Windows.Forms.GroupBox();
            this.picFormMainHeader = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.frpStopWatch.SuspendLayout();
            this.grpResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFormMainHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStopWatchLabel
            // 
            this.lblStopWatchLabel.AutoSize = true;
            this.lblStopWatchLabel.Font = new System.Drawing.Font("宋体", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStopWatchLabel.Location = new System.Drawing.Point(56, 48);
            this.lblStopWatchLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStopWatchLabel.Name = "lblStopWatchLabel";
            this.lblStopWatchLabel.Size = new System.Drawing.Size(202, 34);
            this.lblStopWatchLabel.TabIndex = 5;
            this.lblStopWatchLabel.Text = "HH:MM:SS:MS";
            // 
            // txtUart
            // 
            this.txtUart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUart.BackColor = System.Drawing.Color.White;
            this.txtUart.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUart.Location = new System.Drawing.Point(368, 140);
            this.txtUart.Margin = new System.Windows.Forms.Padding(4);
            this.txtUart.Multiline = true;
            this.txtUart.Name = "txtUart";
            this.txtUart.ReadOnly = true;
            this.txtUart.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtUart.Size = new System.Drawing.Size(583, 274);
            this.txtUart.TabIndex = 141;
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLog.Location = new System.Drawing.Point(369, 431);
            this.txtLog.Margin = new System.Windows.Forms.Padding(4);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(583, 320);
            this.txtLog.TabIndex = 142;
            // 
            // lblStopWatch
            // 
            this.lblStopWatch.AutoSize = true;
            this.lblStopWatch.Font = new System.Drawing.Font("宋体", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblStopWatch.Location = new System.Drawing.Point(55, 95);
            this.lblStopWatch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStopWatch.Name = "lblStopWatch";
            this.lblStopWatch.Size = new System.Drawing.Size(219, 34);
            this.lblStopWatch.TabIndex = 4;
            this.lblStopWatch.Text = "00:00:00:000";
            // 
            // txtYeild
            // 
            this.txtYeild.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.txtYeild.Location = new System.Drawing.Point(143, 184);
            this.txtYeild.Margin = new System.Windows.Forms.Padding(4);
            this.txtYeild.Name = "txtYeild";
            this.txtYeild.ReadOnly = true;
            this.txtYeild.Size = new System.Drawing.Size(188, 42);
            this.txtYeild.TabIndex = 3;
            this.txtYeild.Text = "0";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem,
            this.信息ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(964, 35);
            this.menuStrip1.TabIndex = 151;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.更改计划单号ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(64, 31);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 更改计划单号ToolStripMenuItem
            // 
            this.更改计划单号ToolStripMenuItem.Name = "更改计划单号ToolStripMenuItem";
            this.更改计划单号ToolStripMenuItem.Size = new System.Drawing.Size(210, 32);
            this.更改计划单号ToolStripMenuItem.Text = "更改计划单号";
            // 
            // 信息ToolStripMenuItem
            // 
            this.信息ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.信息看板ToolStripMenuItem});
            this.信息ToolStripMenuItem.Name = "信息ToolStripMenuItem";
            this.信息ToolStripMenuItem.Size = new System.Drawing.Size(64, 31);
            this.信息ToolStripMenuItem.Text = "信息";
            // 
            // 信息看板ToolStripMenuItem
            // 
            this.信息看板ToolStripMenuItem.Name = "信息看板ToolStripMenuItem";
            this.信息看板ToolStripMenuItem.Size = new System.Drawing.Size(170, 32);
            this.信息看板ToolStripMenuItem.Text = "信息看板";
            // 
            // lblYeild
            // 
            this.lblYeild.AutoSize = true;
            this.lblYeild.Location = new System.Drawing.Point(7, 184);
            this.lblYeild.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblYeild.Name = "lblYeild";
            this.lblYeild.Size = new System.Drawing.Size(103, 30);
            this.lblYeild.TabIndex = 24;
            this.lblYeild.Text = "RATE :";
            // 
            // lblClear
            // 
            this.lblClear.AutoSize = true;
            this.lblClear.Location = new System.Drawing.Point(7, 231);
            this.lblClear.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClear.Name = "lblClear";
            this.lblClear.Size = new System.Drawing.Size(103, 30);
            this.lblClear.TabIndex = 23;
            this.lblClear.Text = "CLEAR:";
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.txtTotal.Location = new System.Drawing.Point(143, 135);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(188, 42);
            this.txtTotal.TabIndex = 2;
            this.txtTotal.Text = "0";
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("宋体", 15F);
            this.btnClear.Location = new System.Drawing.Point(141, 232);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(191, 38);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "清零";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtFail
            // 
            this.txtFail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.txtFail.Location = new System.Drawing.Point(143, 86);
            this.txtFail.Margin = new System.Windows.Forms.Padding(4);
            this.txtFail.Name = "txtFail";
            this.txtFail.ReadOnly = true;
            this.txtFail.Size = new System.Drawing.Size(188, 42);
            this.txtFail.TabIndex = 1;
            this.txtFail.Text = "0";
            // 
            // txtPass
            // 
            this.txtPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.txtPass.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPass.Location = new System.Drawing.Point(141, 38);
            this.txtPass.Margin = new System.Windows.Forms.Padding(4);
            this.txtPass.Name = "txtPass";
            this.txtPass.ReadOnly = true;
            this.txtPass.Size = new System.Drawing.Size(189, 42);
            this.txtPass.TabIndex = 0;
            this.txtPass.Text = "0";
            // 
            // txtLabelSn
            // 
            this.txtLabelSn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLabelSn.BackColor = System.Drawing.Color.White;
            this.txtLabelSn.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLabelSn.Location = new System.Drawing.Point(371, 44);
            this.txtLabelSn.Margin = new System.Windows.Forms.Padding(4);
            this.txtLabelSn.Name = "txtLabelSn";
            this.txtLabelSn.Size = new System.Drawing.Size(583, 57);
            this.txtLabelSn.TabIndex = 136;
            this.txtLabelSn.TextChanged += new System.EventHandler(this.txtLabelSn_TextChanged);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(7, 136);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(103, 30);
            this.lblTotal.TabIndex = 2;
            this.lblTotal.Text = "TOTAL:";
            // 
            // lblFail
            // 
            this.lblFail.AutoSize = true;
            this.lblFail.Location = new System.Drawing.Point(8, 89);
            this.lblFail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFail.Name = "lblFail";
            this.lblFail.Size = new System.Drawing.Size(103, 30);
            this.lblFail.TabIndex = 1;
            this.lblFail.Text = "FAIL :";
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Location = new System.Drawing.Point(8, 41);
            this.lblPass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(103, 30);
            this.lblPass.TabIndex = 0;
            this.lblPass.Text = "PASS :";
            // 
            // lblLabelSn
            // 
            this.lblLabelSn.AutoSize = true;
            this.lblLabelSn.BackColor = System.Drawing.Color.White;
            this.lblLabelSn.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblLabelSn.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblLabelSn.Location = new System.Drawing.Point(409, 53);
            this.lblLabelSn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLabelSn.Name = "lblLabelSn";
            this.lblLabelSn.Size = new System.Drawing.Size(174, 44);
            this.lblLabelSn.TabIndex = 143;
            this.lblLabelSn.Text = "标签MAC";
            this.lblLabelSn.Click += new System.EventHandler(this.lblLabelImei_Click);
            // 
            // frpStopWatch
            // 
            this.frpStopWatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.frpStopWatch.Controls.Add(this.lblStopWatchLabel);
            this.frpStopWatch.Controls.Add(this.lblStopWatch);
            this.frpStopWatch.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.frpStopWatch.ForeColor = System.Drawing.Color.Green;
            this.frpStopWatch.Location = new System.Drawing.Point(9, 586);
            this.frpStopWatch.Margin = new System.Windows.Forms.Padding(4);
            this.frpStopWatch.Name = "frpStopWatch";
            this.frpStopWatch.Padding = new System.Windows.Forms.Padding(4);
            this.frpStopWatch.Size = new System.Drawing.Size(341, 156);
            this.frpStopWatch.TabIndex = 150;
            this.frpStopWatch.TabStop = false;
            this.frpStopWatch.Text = "Elapsed Time";
            // 
            // grpResult
            // 
            this.grpResult.Controls.Add(this.txtYeild);
            this.grpResult.Controls.Add(this.lblYeild);
            this.grpResult.Controls.Add(this.lblClear);
            this.grpResult.Controls.Add(this.txtTotal);
            this.grpResult.Controls.Add(this.btnClear);
            this.grpResult.Controls.Add(this.txtFail);
            this.grpResult.Controls.Add(this.txtPass);
            this.grpResult.Controls.Add(this.lblTotal);
            this.grpResult.Controls.Add(this.lblFail);
            this.grpResult.Controls.Add(this.lblPass);
            this.grpResult.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpResult.ForeColor = System.Drawing.Color.Green;
            this.grpResult.Location = new System.Drawing.Point(9, 295);
            this.grpResult.Margin = new System.Windows.Forms.Padding(4);
            this.grpResult.Name = "grpResult";
            this.grpResult.Padding = new System.Windows.Forms.Padding(4);
            this.grpResult.Size = new System.Drawing.Size(347, 275);
            this.grpResult.TabIndex = 149;
            this.grpResult.TabStop = false;
            this.grpResult.Text = "Result";
            // 
            // picFormMainHeader
            // 
            this.picFormMainHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picFormMainHeader.Location = new System.Drawing.Point(9, 43);
            this.picFormMainHeader.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picFormMainHeader.Name = "picFormMainHeader";
            this.picFormMainHeader.Size = new System.Drawing.Size(347, 244);
            this.picFormMainHeader.TabIndex = 148;
            this.picFormMainHeader.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 754);
            this.Controls.Add(this.lblLabelSn);
            this.Controls.Add(this.txtUart);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.frpStopWatch);
            this.Controls.Add(this.grpResult);
            this.Controls.Add(this.picFormMainHeader);
            this.Controls.Add(this.txtLabelSn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TD1030_CNRtest_20180920_V1.0.0beta";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.frpStopWatch.ResumeLayout(false);
            this.frpStopWatch.PerformLayout();
            this.grpResult.ResumeLayout(false);
            this.grpResult.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFormMainHeader)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStopWatchLabel;
        private System.Windows.Forms.TextBox txtUart;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblStopWatch;
        private System.Windows.Forms.TextBox txtYeild;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 更改计划单号ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 信息看板ToolStripMenuItem;
        private System.Windows.Forms.Label lblYeild;
        private System.Windows.Forms.Label lblClear;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtFail;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.TextBox txtLabelSn;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblFail;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Label lblLabelSn;
        private System.Windows.Forms.GroupBox frpStopWatch;
        private System.Windows.Forms.GroupBox grpResult;
        private System.Windows.Forms.PictureBox picFormMainHeader;
    }
}

