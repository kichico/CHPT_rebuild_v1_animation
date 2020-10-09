namespace CHPT_rebuild_v1_animation
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.CMB_DriverSelect = new System.Windows.Forms.ComboBox();
            this.lbl_DriverSelect = new System.Windows.Forms.Label();
            this.BTN_Reset = new System.Windows.Forms.Button();
            this.CMB_Mode = new System.Windows.Forms.ComboBox();
            this.lbl_Case = new System.Windows.Forms.Label();
            this.TXT_Bai = new System.Windows.Forms.TextBox();
            this.lbl_Bai = new System.Windows.Forms.Label();
            this.PB = new System.Windows.Forms.PictureBox();
            this.TXT_Length = new System.Windows.Forms.TextBox();
            this.lb_Lebgth = new System.Windows.Forms.Label();
            this.BTN_Start = new System.Windows.Forms.Button();
            this.BTN_Cancel = new System.Windows.Forms.Button();
            this.BTN_Set = new System.Windows.Forms.Button();
            this.TXT_time = new System.Windows.Forms.TextBox();
            this.TXT_CarNum = new System.Windows.Forms.TextBox();
            this.lbl_time = new System.Windows.Forms.Label();
            this.lbl_CarNum = new System.Windows.Forms.Label();
            this.CMB_ModelMode = new System.Windows.Forms.ComboBox();
            this.lbl_kind = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PB)).BeginInit();
            this.SuspendLayout();
            // 
            // CMB_DriverSelect
            // 
            this.CMB_DriverSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CMB_DriverSelect.FormattingEnabled = true;
            this.CMB_DriverSelect.Location = new System.Drawing.Point(714, 30);
            this.CMB_DriverSelect.Name = "CMB_DriverSelect";
            this.CMB_DriverSelect.Size = new System.Drawing.Size(153, 20);
            this.CMB_DriverSelect.TabIndex = 5;
            // 
            // lbl_DriverSelect
            // 
            this.lbl_DriverSelect.AutoSize = true;
            this.lbl_DriverSelect.Location = new System.Drawing.Point(712, 15);
            this.lbl_DriverSelect.Name = "lbl_DriverSelect";
            this.lbl_DriverSelect.Size = new System.Drawing.Size(103, 12);
            this.lbl_DriverSelect.TabIndex = 15;
            this.lbl_DriverSelect.Text = "ドライバーモード選択";
            // 
            // BTN_Reset
            // 
            this.BTN_Reset.Enabled = false;
            this.BTN_Reset.Location = new System.Drawing.Point(785, 178);
            this.BTN_Reset.Name = "BTN_Reset";
            this.BTN_Reset.Size = new System.Drawing.Size(116, 45);
            this.BTN_Reset.TabIndex = 9;
            this.BTN_Reset.Text = "Reset";
            this.BTN_Reset.UseVisualStyleBackColor = true;
            this.BTN_Reset.Click += new System.EventHandler(this.BTN_Reset_Click);
            // 
            // CMB_Mode
            // 
            this.CMB_Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CMB_Mode.FormattingEnabled = true;
            this.CMB_Mode.Location = new System.Drawing.Point(544, 30);
            this.CMB_Mode.Name = "CMB_Mode";
            this.CMB_Mode.Size = new System.Drawing.Size(153, 20);
            this.CMB_Mode.TabIndex = 4;
            // 
            // lbl_Case
            // 
            this.lbl_Case.AutoSize = true;
            this.lbl_Case.Location = new System.Drawing.Point(542, 15);
            this.lbl_Case.Name = "lbl_Case";
            this.lbl_Case.Size = new System.Drawing.Size(57, 12);
            this.lbl_Case.TabIndex = 14;
            this.lbl_Case.Text = "モード選択";
            // 
            // TXT_Bai
            // 
            this.TXT_Bai.Location = new System.Drawing.Point(411, 30);
            this.TXT_Bai.Name = "TXT_Bai";
            this.TXT_Bai.Size = new System.Drawing.Size(116, 19);
            this.TXT_Bai.TabIndex = 3;
            this.TXT_Bai.Text = "2";
            // 
            // lbl_Bai
            // 
            this.lbl_Bai.AutoSize = true;
            this.lbl_Bai.Location = new System.Drawing.Point(409, 15);
            this.lbl_Bai.Name = "lbl_Bai";
            this.lbl_Bai.Size = new System.Drawing.Size(29, 12);
            this.lbl_Bai.TabIndex = 13;
            this.lbl_Bai.Text = "倍速";
            // 
            // PB
            // 
            this.PB.Location = new System.Drawing.Point(0, 80);
            this.PB.Margin = new System.Windows.Forms.Padding(0);
            this.PB.Name = "PB";
            this.PB.Size = new System.Drawing.Size(1045, 74);
            this.PB.TabIndex = 31;
            this.PB.TabStop = false;
            // 
            // TXT_Length
            // 
            this.TXT_Length.Location = new System.Drawing.Point(12, 30);
            this.TXT_Length.Name = "TXT_Length";
            this.TXT_Length.Size = new System.Drawing.Size(116, 19);
            this.TXT_Length.TabIndex = 0;
            this.TXT_Length.Text = "1000";
            // 
            // lb_Lebgth
            // 
            this.lb_Lebgth.AutoSize = true;
            this.lb_Lebgth.Location = new System.Drawing.Point(12, 15);
            this.lb_Lebgth.Name = "lb_Lebgth";
            this.lb_Lebgth.Size = new System.Drawing.Size(46, 12);
            this.lb_Lebgth.TabIndex = 10;
            this.lb_Lebgth.Text = "全周[m]";
            // 
            // BTN_Start
            // 
            this.BTN_Start.Enabled = false;
            this.BTN_Start.Location = new System.Drawing.Point(652, 178);
            this.BTN_Start.Name = "BTN_Start";
            this.BTN_Start.Size = new System.Drawing.Size(116, 45);
            this.BTN_Start.TabIndex = 8;
            this.BTN_Start.Text = "Start";
            this.BTN_Start.UseVisualStyleBackColor = true;
            this.BTN_Start.Click += new System.EventHandler(this.BTN_Start_Click);
            // 
            // BTN_Cancel
            // 
            this.BTN_Cancel.Location = new System.Drawing.Point(918, 178);
            this.BTN_Cancel.Name = "BTN_Cancel";
            this.BTN_Cancel.Size = new System.Drawing.Size(116, 45);
            this.BTN_Cancel.TabIndex = 10;
            this.BTN_Cancel.Text = "Cancel";
            this.BTN_Cancel.UseVisualStyleBackColor = true;
            this.BTN_Cancel.Click += new System.EventHandler(this.BTN_Cancel_Click);
            // 
            // BTN_Set
            // 
            this.BTN_Set.Location = new System.Drawing.Point(519, 178);
            this.BTN_Set.Name = "BTN_Set";
            this.BTN_Set.Size = new System.Drawing.Size(116, 45);
            this.BTN_Set.TabIndex = 7;
            this.BTN_Set.Text = "Initial Set";
            this.BTN_Set.UseVisualStyleBackColor = true;
            this.BTN_Set.EnabledChanged += new System.EventHandler(this.BTN_Set_EnabledChanged);
            this.BTN_Set.Click += new System.EventHandler(this.BTN_Set_Click);
            // 
            // TXT_time
            // 
            this.TXT_time.Location = new System.Drawing.Point(278, 30);
            this.TXT_time.Name = "TXT_time";
            this.TXT_time.Size = new System.Drawing.Size(116, 19);
            this.TXT_time.TabIndex = 2;
            this.TXT_time.Text = "0.05";
            // 
            // TXT_CarNum
            // 
            this.TXT_CarNum.Location = new System.Drawing.Point(145, 30);
            this.TXT_CarNum.Name = "TXT_CarNum";
            this.TXT_CarNum.Size = new System.Drawing.Size(116, 19);
            this.TXT_CarNum.TabIndex = 1;
            this.TXT_CarNum.Text = "1";
            // 
            // lbl_time
            // 
            this.lbl_time.AutoSize = true;
            this.lbl_time.Location = new System.Drawing.Point(276, 15);
            this.lbl_time.Name = "lbl_time";
            this.lbl_time.Size = new System.Drawing.Size(79, 12);
            this.lbl_time.TabIndex = 12;
            this.lbl_time.Text = "時間解像度[s]";
            // 
            // lbl_CarNum
            // 
            this.lbl_CarNum.AutoSize = true;
            this.lbl_CarNum.Location = new System.Drawing.Point(143, 15);
            this.lbl_CarNum.Name = "lbl_CarNum";
            this.lbl_CarNum.Size = new System.Drawing.Size(55, 12);
            this.lbl_CarNum.TabIndex = 11;
            this.lbl_CarNum.Text = "車両数[-]";
            // 
            // CMB_ModelMode
            // 
            this.CMB_ModelMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CMB_ModelMode.FormattingEnabled = true;
            this.CMB_ModelMode.Location = new System.Drawing.Point(884, 30);
            this.CMB_ModelMode.Name = "CMB_ModelMode";
            this.CMB_ModelMode.Size = new System.Drawing.Size(153, 20);
            this.CMB_ModelMode.TabIndex = 6;
            // 
            // lbl_kind
            // 
            this.lbl_kind.AutoSize = true;
            this.lbl_kind.Location = new System.Drawing.Point(882, 15);
            this.lbl_kind.Name = "lbl_kind";
            this.lbl_kind.Size = new System.Drawing.Size(62, 12);
            this.lbl_kind.TabIndex = 56;
            this.lbl_kind.Text = "モデルモード";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 241);
            this.Controls.Add(this.CMB_ModelMode);
            this.Controls.Add(this.lbl_kind);
            this.Controls.Add(this.CMB_DriverSelect);
            this.Controls.Add(this.lbl_DriverSelect);
            this.Controls.Add(this.BTN_Reset);
            this.Controls.Add(this.CMB_Mode);
            this.Controls.Add(this.lbl_Case);
            this.Controls.Add(this.TXT_Bai);
            this.Controls.Add(this.lbl_Bai);
            this.Controls.Add(this.PB);
            this.Controls.Add(this.TXT_Length);
            this.Controls.Add(this.lb_Lebgth);
            this.Controls.Add(this.BTN_Start);
            this.Controls.Add(this.BTN_Cancel);
            this.Controls.Add(this.BTN_Set);
            this.Controls.Add(this.TXT_time);
            this.Controls.Add(this.TXT_CarNum);
            this.Controls.Add(this.lbl_time);
            this.Controls.Add(this.lbl_CarNum);
            this.MaximumSize = new System.Drawing.Size(3000, 280);
            this.MinimumSize = new System.Drawing.Size(438, 280);
            this.Name = "Form1";
            this.Text = "CHPT_rebuild_v1_animation";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Drawing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.PB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CMB_DriverSelect;
        private System.Windows.Forms.Label lbl_DriverSelect;
        private System.Windows.Forms.Button BTN_Reset;
        private System.Windows.Forms.ComboBox CMB_Mode;
        private System.Windows.Forms.Label lbl_Case;
        private System.Windows.Forms.TextBox TXT_Bai;
        private System.Windows.Forms.Label lbl_Bai;
        private System.Windows.Forms.PictureBox PB;
        private System.Windows.Forms.TextBox TXT_Length;
        private System.Windows.Forms.Label lb_Lebgth;
        private System.Windows.Forms.Button BTN_Start;
        private System.Windows.Forms.Button BTN_Cancel;
        private System.Windows.Forms.Button BTN_Set;
        private System.Windows.Forms.TextBox TXT_time;
        private System.Windows.Forms.TextBox TXT_CarNum;
        private System.Windows.Forms.Label lbl_time;
        private System.Windows.Forms.Label lbl_CarNum;
        private System.Windows.Forms.ComboBox CMB_ModelMode;
        private System.Windows.Forms.Label lbl_kind;
    }
}

