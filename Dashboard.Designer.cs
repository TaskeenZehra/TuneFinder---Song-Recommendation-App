namespace Song_Recommendation_App
{
    partial class Dashboard
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
            SideBar = new Panel();
            logout = new Button();
            playlist = new Button();
            search = new Button();
            recommended = new Button();
            Contentpnl = new Panel();
            dataGridView1 = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            backbutton = new Button();
            SideBar.SuspendLayout();
            Contentpnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // SideBar
            // 
            SideBar.BackColor = SystemColors.ActiveCaptionText;
            SideBar.Controls.Add(logout);
            SideBar.Controls.Add(playlist);
            SideBar.Controls.Add(search);
            SideBar.Controls.Add(recommended);
            SideBar.Location = new Point(1, 133);
            SideBar.Name = "SideBar";
            SideBar.Size = new Size(181, 349);
            SideBar.TabIndex = 0;
            SideBar.Paint += SideBar_Paint;
            // 
            // logout
            // 
            logout.BackColor = Color.SeaGreen;
            logout.FlatStyle = FlatStyle.Popup;
            logout.Font = new Font("Viner Hand ITC", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            logout.ForeColor = SystemColors.ControlLightLight;
            logout.Location = new Point(0, 258);
            logout.Name = "logout";
            logout.Size = new Size(181, 44);
            logout.TabIndex = 3;
            logout.Text = "🚪 Logout ";
            logout.TextAlign = ContentAlignment.MiddleLeft;
            logout.UseVisualStyleBackColor = false;
            logout.Click += logout_Click;
            // 
            // playlist
            // 
            playlist.BackColor = Color.SeaGreen;
            playlist.FlatStyle = FlatStyle.Popup;
            playlist.Font = new Font("Viner Hand ITC", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            playlist.ForeColor = SystemColors.ControlLightLight;
            playlist.Location = new Point(0, 187);
            playlist.Name = "playlist";
            playlist.Size = new Size(181, 44);
            playlist.TabIndex = 2;
            playlist.Text = "🎵 Playlist";
            playlist.TextAlign = ContentAlignment.MiddleLeft;
            playlist.UseVisualStyleBackColor = false;
            playlist.Click += playlist_Click_1;
            // 
            // search
            // 
            search.BackColor = Color.SeaGreen;
            search.FlatStyle = FlatStyle.Popup;
            search.Font = new Font("Viner Hand ITC", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            search.ForeColor = SystemColors.ControlLightLight;
            search.Location = new Point(0, 112);
            search.Name = "search";
            search.Size = new Size(181, 44);
            search.TabIndex = 1;
            search.Text = "🔍 Search Music";
            search.TextAlign = ContentAlignment.MiddleLeft;
            search.UseVisualStyleBackColor = false;
            search.Click += search_Click;
            // 
            // recommended
            // 
            recommended.BackColor = Color.SeaGreen;
            recommended.FlatStyle = FlatStyle.Popup;
            recommended.Font = new Font("Viner Hand ITC", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            recommended.ForeColor = SystemColors.ControlLightLight;
            recommended.Location = new Point(0, 36);
            recommended.Name = "recommended";
            recommended.Size = new Size(181, 44);
            recommended.TabIndex = 0;
            recommended.Text = "🎧Recommend";
            recommended.TextAlign = ContentAlignment.MiddleLeft;
            recommended.UseVisualStyleBackColor = false;
            recommended.Click += recommended_Click;
            // 
            // Contentpnl
            // 
            Contentpnl.BackColor = SystemColors.ControlDarkDark;
            Contentpnl.Controls.Add(dataGridView1);
            Contentpnl.Controls.Add(label1);
            Contentpnl.Location = new Point(202, 118);
            Contentpnl.Name = "Contentpnl";
            Contentpnl.Size = new Size(285, 390);
            Contentpnl.TabIndex = 1;
            Contentpnl.Paint += Contentpnl_Paint;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.Plum;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(16, 51);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(252, 322);
            dataGridView1.TabIndex = 1;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.FlatStyle = FlatStyle.Popup;
            label1.Font = new Font("Viner Hand ITC", 18F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(16, 15);
            label1.Name = "label1";
            label1.Size = new Size(252, 39);
            label1.TabIndex = 0;
            label1.Text = "Recommended Songs";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("MV Boli", 48F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(90, 9);
            label2.Name = "label2";
            label2.Size = new Size(294, 85);
            label2.TabIndex = 2;
            label2.Text = "Welcome";
            // 
            // backbutton
            // 
            backbutton.BackColor = Color.Transparent;
            backbutton.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            backbutton.Location = new Point(12, 9);
            backbutton.Name = "backbutton";
            backbutton.Size = new Size(75, 23);
            backbutton.TabIndex = 3;
            backbutton.Text = "back";
            backbutton.UseVisualStyleBackColor = false;
            backbutton.Click += backbutton_Click;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            BackgroundImage = Properties.Resources.bg;
            ClientSize = new Size(499, 550);
            ControlBox = false;
            Controls.Add(backbutton);
            Controls.Add(label2);
            Controls.Add(Contentpnl);
            Controls.Add(SideBar);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Dashboard";
            Text = "Dashboard";
            Load += Dashboard_Load;
            SideBar.ResumeLayout(false);
            Contentpnl.ResumeLayout(false);
            Contentpnl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel SideBar;
        private Button logout;
        private Button playlist;
        private Button search;
        private Button recommended;
        private Panel Contentpnl;
        private Label label1;
        private Label label2;
        private Button backbutton;
        private DataGridView dataGridView1;
    }
}