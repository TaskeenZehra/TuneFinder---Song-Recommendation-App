namespace Song_Recommendation_App
{
    partial class Search
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Search));
            searchBox = new TextBox();
            searchbtn = new Button();
            Searchdata = new DataGridView();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)Searchdata).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // searchBox
            // 
            searchBox.Location = new Point(54, 41);
            searchBox.Name = "searchBox";
            searchBox.PlaceholderText = "Enter Song or Artist Name...";
            searchBox.Size = new Size(296, 23);
            searchBox.TabIndex = 0;
            searchBox.TextChanged += searchBox_TextChanged;
            // 
            // searchbtn
            // 
            searchbtn.BackColor = Color.SeaGreen;
            searchbtn.FlatStyle = FlatStyle.Popup;
            searchbtn.Font = new Font("Viner Hand ITC", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            searchbtn.ForeColor = SystemColors.ControlLightLight;
            searchbtn.Location = new Point(356, 35);
            searchbtn.Name = "searchbtn";
            searchbtn.Size = new Size(97, 35);
            searchbtn.TabIndex = 1;
            searchbtn.Text = "Search";
            searchbtn.UseVisualStyleBackColor = false;
            searchbtn.Click += searchbtn_Click;
            // 
            // Searchdata
            // 
            Searchdata.BackgroundColor = Color.Plum;
            Searchdata.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Searchdata.Location = new Point(12, 76);
            Searchdata.Name = "Searchdata";
            Searchdata.Size = new Size(459, 448);
            Searchdata.TabIndex = 2;
            Searchdata.CellContentClick += Searchdata_CellContentClick;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.Location = new Point(22, 41);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(26, 26);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Viner Hand ITC", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(159, 4);
            label1.Name = "label1";
            label1.Size = new Size(152, 34);
            label1.TabIndex = 4;
            label1.Text = "Search Songs";
            // 
            // button1
            // 
            button1.Font = new Font("Tahoma", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.Location = new Point(12, 9);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 5;
            button1.Text = "Back";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Search
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            BackgroundImage = Properties.Resources.bg;
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(483, 555);
            ControlBox = false;
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(Searchdata);
            Controls.Add(searchbtn);
            Controls.Add(searchBox);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Search";
            Load += Search_Load;
            ((System.ComponentModel.ISupportInitialize)Searchdata).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox searchBox;
        private Button searchbtn;
        private DataGridView Searchdata;
        private PictureBox pictureBox1;
        private Label label1;
        private Button button1;
    }
}
