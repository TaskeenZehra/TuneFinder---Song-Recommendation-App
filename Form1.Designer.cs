namespace Song_Recommendation_App
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            login = new Button();
            signup = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // login
            // 
            login.BackColor = Color.SeaGreen;
            login.FlatStyle = FlatStyle.Popup;
            login.Font = new Font("Viner Hand ITC", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            login.ForeColor = SystemColors.ControlLightLight;
            login.Location = new Point(108, 266);
            login.Name = "login";
            login.Size = new Size(272, 48);
            login.TabIndex = 0;
            login.Text = "Login With Spotify";
            login.UseVisualStyleBackColor = false;
            login.Click += login_Click;
            // 
            // signup
            // 
            signup.BackColor = Color.SeaGreen;
            signup.FlatStyle = FlatStyle.Popup;
            signup.Font = new Font("Viner Hand ITC", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            signup.ForeColor = SystemColors.ControlLightLight;
            signup.Location = new Point(108, 320);
            signup.Name = "signup";
            signup.Size = new Size(272, 48);
            signup.TabIndex = 1;
            signup.Text = "Create an Account";
            signup.UseVisualStyleBackColor = false;
            signup.Click += signup_Click;
            // 
            // label1
            // 
            label1.BackColor = SystemColors.InactiveCaptionText;
            label1.Font = new Font("MV Boli", 72F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Image = Properties.Resources.bg;
            label1.ImageAlign = ContentAlignment.TopCenter;
            label1.Location = new Point(82, 75);
            label1.Name = "label1";
            label1.Size = new Size(323, 147);
            label1.TabIndex = 2;
            label1.Text = "Music";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveCaptionText;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(499, 550);
            Controls.Add(label1);
            Controls.Add(signup);
            Controls.Add(login);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "Form1";
            Text = "Login Form";
            TransparencyKey = Color.DarkGreen;
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button login;
        private Button signup;
        private Label label1;
    }
}
