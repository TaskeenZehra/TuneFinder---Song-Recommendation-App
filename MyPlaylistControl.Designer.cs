namespace Song_Recommendation_App
{
    partial class MyPlaylistControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            Playlistdata = new DataGridView();
            label1 = new Label();
            backbutton = new Button();
            ((System.ComponentModel.ISupportInitialize)Playlistdata).BeginInit();
            SuspendLayout();
            // 
            // Playlistdata
            // 
            Playlistdata.BackgroundColor = Color.Plum;
            Playlistdata.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            Playlistdata.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            Playlistdata.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            Playlistdata.DefaultCellStyle = dataGridViewCellStyle2;
            Playlistdata.EnableHeadersVisualStyles = false;
            Playlistdata.GridColor = SystemColors.Control;
            Playlistdata.Location = new Point(89, 59);
            Playlistdata.Name = "Playlistdata";
            Playlistdata.ReadOnly = true;
            Playlistdata.RowHeadersVisible = false;
            Playlistdata.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            Playlistdata.Size = new Size(312, 405);
            Playlistdata.TabIndex = 0;
            Playlistdata.CellContentClick += Playlistdata_CellContentClick_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Viner Hand ITC", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(154, 0);
            label1.Name = "label1";
            label1.Size = new Size(187, 44);
            label1.TabIndex = 1;
            label1.Text = "My Playlist";
            // 
            // backbutton
            // 
            backbutton.BackColor = Color.Transparent;
            backbutton.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            backbutton.Location = new Point(12, 11);
            backbutton.Name = "backbutton";
            backbutton.Size = new Size(75, 23);
            backbutton.TabIndex = 2;
            backbutton.Text = "Back";
            backbutton.UseVisualStyleBackColor = false;
            backbutton.Click += backbutton_Click;
            // 
            // MyPlaylistControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.bg;
            ClientSize = new Size(484, 511);
            ControlBox = false;
            Controls.Add(backbutton);
            Controls.Add(label1);
            Controls.Add(Playlistdata);
            FormBorderStyle = FormBorderStyle.None;
            MinimizeBox = false;
            Name = "MyPlaylistControl";
            Load += MyPlaylistControl_Load;
            ((System.ComponentModel.ISupportInitialize)Playlistdata).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView Playlistdata;
        private Label label1;
        private Button backbutton;
    }
}
