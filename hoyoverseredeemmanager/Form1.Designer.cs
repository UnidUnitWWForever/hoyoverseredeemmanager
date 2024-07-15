namespace hoyoverseredeemmanager
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ListViewItem listViewItem1 = new ListViewItem(new string[] { "원신", "GENSHINGIFT" }, -1);
            ListViewItem listViewItem2 = new ListViewItem(new string[] { "붕괴:스타레일", "STARRAILGIFT" }, -1);
            ListViewItem listViewItem3 = new ListViewItem(new string[] { "젠레스 존 제로", "ZENLESSGIFT" }, -1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            groupBox1 = new GroupBox();
            label3 = new Label();
            btn_add = new Button();
            tb_codes = new TextBox();
            label2 = new Label();
            cbx_games = new ComboBox();
            label1 = new Label();
            groupBox2 = new GroupBox();
            btn_copyurl = new Button();
            btn_del = new Button();
            btn_getredeem = new Button();
            btn_savecsv = new Button();
            lvw_codes = new ListView();
            games = new ColumnHeader();
            codes = new ColumnHeader();
            btn_exit = new Button();
            label4 = new Label();
            pbx_launcher = new PictureBox();
            save_csv = new SaveFileDialog();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbx_launcher).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(btn_add);
            groupBox1.Controls.Add(tb_codes);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(cbx_games);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(593, 106);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "리딤코드 추가";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 64);
            label3.Name = "label3";
            label3.Size = new Size(550, 30);
            label3.TabIndex = 0;
            label3.Text = "주의 : 리딤코드는 수량 및 기간제한이 있을 수 있습니다. \r\n추가한 리딤코드를 사용할 때 이미 사용하였거나 기간 만료 메시지를 받게될 수 있음에 유의하십시오.";
            // 
            // btn_add
            // 
            btn_add.Location = new Point(495, 27);
            btn_add.Name = "btn_add";
            btn_add.Size = new Size(83, 24);
            btn_add.TabIndex = 3;
            btn_add.Text = "추가";
            btn_add.UseVisualStyleBackColor = true;
            btn_add.Click += btn_add_Click;
            // 
            // tb_codes
            // 
            tb_codes.Location = new Point(329, 27);
            tb_codes.Name = "tb_codes";
            tb_codes.Size = new Size(160, 23);
            tb_codes.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(261, 30);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 2;
            label2.Text = "리딤코드: ";
            // 
            // cbx_games
            // 
            cbx_games.DropDownStyle = ComboBoxStyle.DropDownList;
            cbx_games.FormattingEnabled = true;
            cbx_games.Items.AddRange(new object[] { "원신", "붕괴:스타레일", "젠레스 존 제로" });
            cbx_games.Location = new Point(66, 28);
            cbx_games.Name = "cbx_games";
            cbx_games.Size = new Size(171, 23);
            cbx_games.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 31);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 0;
            label1.Text = "게임: ";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btn_copyurl);
            groupBox2.Controls.Add(btn_del);
            groupBox2.Controls.Add(btn_getredeem);
            groupBox2.Controls.Add(btn_savecsv);
            groupBox2.Controls.Add(lvw_codes);
            groupBox2.Location = new Point(12, 124);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(593, 310);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "리딤코드 목록";
            // 
            // btn_copyurl
            // 
            btn_copyurl.Location = new Point(243, 266);
            btn_copyurl.Name = "btn_copyurl";
            btn_copyurl.Size = new Size(132, 32);
            btn_copyurl.TabIndex = 7;
            btn_copyurl.Text = "리딤코드 URL 복사";
            btn_copyurl.UseVisualStyleBackColor = true;
            btn_copyurl.Click += btn_copyurl_Click;
            // 
            // btn_del
            // 
            btn_del.Location = new Point(487, 266);
            btn_del.Name = "btn_del";
            btn_del.Size = new Size(100, 32);
            btn_del.TabIndex = 9;
            btn_del.Text = "삭제";
            btn_del.UseVisualStyleBackColor = true;
            btn_del.Click += btn_del_Click;
            // 
            // btn_getredeem
            // 
            btn_getredeem.Location = new Point(89, 266);
            btn_getredeem.Name = "btn_getredeem";
            btn_getredeem.Size = new Size(148, 32);
            btn_getredeem.TabIndex = 6;
            btn_getredeem.Text = "선택한 리딤코드 사용";
            btn_getredeem.UseVisualStyleBackColor = true;
            btn_getredeem.Click += btn_getredeem_Click;
            // 
            // btn_savecsv
            // 
            btn_savecsv.Location = new Point(381, 266);
            btn_savecsv.Name = "btn_savecsv";
            btn_savecsv.Size = new Size(100, 32);
            btn_savecsv.TabIndex = 8;
            btn_savecsv.Text = "CSV 내보내기";
            btn_savecsv.UseVisualStyleBackColor = true;
            btn_savecsv.Click += btn_savecsv_Click;
            // 
            // lvw_codes
            // 
            lvw_codes.Columns.AddRange(new ColumnHeader[] { games, codes });
            lvw_codes.FullRowSelect = true;
            lvw_codes.GridLines = true;
            lvw_codes.Items.AddRange(new ListViewItem[] { listViewItem1, listViewItem2, listViewItem3 });
            lvw_codes.Location = new Point(6, 22);
            lvw_codes.MultiSelect = false;
            lvw_codes.Name = "lvw_codes";
            lvw_codes.Size = new Size(581, 238);
            lvw_codes.TabIndex = 5;
            lvw_codes.UseCompatibleStateImageBehavior = false;
            lvw_codes.View = View.Details;
            lvw_codes.ColumnClick += lvw_codes_ColumnClick;
            // 
            // games
            // 
            games.Text = "게임";
            games.Width = 400;
            // 
            // codes
            // 
            codes.Text = "리딤코드";
            codes.Width = 170;
            // 
            // btn_exit
            // 
            btn_exit.Location = new Point(507, 444);
            btn_exit.Name = "btn_exit";
            btn_exit.Size = new Size(100, 32);
            btn_exit.TabIndex = 10;
            btn_exit.Text = "종료";
            btn_exit.UseVisualStyleBackColor = true;
            btn_exit.Click += btn_exit_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Enabled = false;
            label4.Font = new Font("맑은 고딕", 6.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label4.Location = new Point(179, 440);
            label4.Name = "label4";
            label4.Size = new Size(208, 36);
            label4.TabIndex = 3;
            label4.Text = "Version 1.000 HSE-704\r\nDeveloped by Unidentified Unit.\r\nThis software is NOT a official HoYoverse product.";
            // 
            // pbx_launcher
            // 
            pbx_launcher.Location = new Point(12, 440);
            pbx_launcher.Name = "pbx_launcher";
            pbx_launcher.Size = new Size(161, 36);
            pbx_launcher.TabIndex = 11;
            pbx_launcher.TabStop = false;
            // 
            // save_csv
            // 
            save_csv.Filter = "CSV파일(쉼표로 분리)|*.csv";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(617, 484);
            Controls.Add(pbx_launcher);
            Controls.Add(label4);
            Controls.Add(btn_exit);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HoYoverse 리딤코드 관리 소프트웨어";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbx_launcher).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private Label label3;
        private Button btn_add;
        private TextBox tb_codes;
        private Label label2;
        private ComboBox cbx_games;
        private Label label1;
        private GroupBox groupBox2;
        private ListView lvw_codes;
        private ColumnHeader games;
        private ColumnHeader codes;
        private Button btn_savecsv;
        private Button btn_exit;
        private Label label4;
        private Button btn_getredeem;
        private Button btn_del;
        private Button btn_copyurl;
        private PictureBox pbx_launcher;
        private SaveFileDialog save_csv;
    }
}
