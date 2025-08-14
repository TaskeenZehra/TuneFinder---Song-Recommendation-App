namespace Song_Recommendation_App
{
    partial class PlaylistTracksForm
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
            Playlistdata = new DataGridView();
            backbutton = new Button();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)Playlistdata).BeginInit();
            SuspendLayout();
            // 
            // Playlistdata
            // 
            Playlistdata.BackgroundColor = Color.Plum;
            Playlistdata.BorderStyle = BorderStyle.None;
            Playlistdata.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Playlistdata.Location = new Point(49, 44);
            Playlistdata.Name = "Playlistdata";
            Playlistdata.Size = new Size(359, 441);
            Playlistdata.TabIndex = 1;
            Playlistdata.CellContentClick += Playlistdata_CellContentClick;
            // 
            // backbutton
            // 
            backbutton.BackColor = Color.Transparent;
            backbutton.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            backbutton.Location = new Point(12, 8);
            backbutton.Name = "backbutton";
            backbutton.Size = new Size(75, 23);
            backbutton.TabIndex = 3;
            backbutton.Text = "Back";
            backbutton.UseVisualStyleBackColor = false;
            backbutton.Click += backbutton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Viner Hand ITC", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(135, -3);
            label1.Name = "label1";
            label1.Size = new Size(187, 44);
            label1.TabIndex = 4;
            label1.Text = "My Playlist";
            // 
            // PlaylistTracksForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.bg;
            ClientSize = new Size(468, 517);
            ControlBox = false;
            Controls.Add(label1);
            Controls.Add(backbutton);
            Controls.Add(Playlistdata);
            FormBorderStyle = FormBorderStyle.None;
            Name = "PlaylistTracksForm";
            Text = "PlaylistTracksForm";
            Load += PlaylistTracksForm_Load_1;
            ((System.ComponentModel.ISupportInitialize)Playlistdata).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView Playlistdata;
        private Button backbutton;
        private Label label1;

    }
}