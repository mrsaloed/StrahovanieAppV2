namespace StrahovanieAppV2
{
    partial class AuthorizationForm
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
            this.LoginTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.PasswordTextBox = new MaterialSkin.Controls.MaterialTextBox();
            this.LoginBtn = new MaterialSkin.Controls.MaterialButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.WelcomeLabel = new MaterialSkin.Controls.MaterialLabel();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // LoginTextBox
            // 
            this.LoginTextBox.AnimateReadOnly = false;
            this.LoginTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LoginTextBox.Depth = 0;
            this.LoginTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.LoginTextBox.Hint = "Логин";
            this.LoginTextBox.LeadingIcon = null;
            this.LoginTextBox.Location = new System.Drawing.Point(24, 156);
            this.LoginTextBox.MaxLength = 50;
            this.LoginTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.LoginTextBox.Multiline = false;
            this.LoginTextBox.Name = "LoginTextBox";
            this.LoginTextBox.Size = new System.Drawing.Size(291, 50);
            this.LoginTextBox.TabIndex = 0;
            this.LoginTextBox.Text = "";
            this.LoginTextBox.TrailingIcon = null;
            this.LoginTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.LoginTextBox_Validating);
            this.LoginTextBox.Validated += new System.EventHandler(this.LoginTextBox_Validated);
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.AnimateReadOnly = false;
            this.PasswordTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.PasswordTextBox.Depth = 0;
            this.PasswordTextBox.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.PasswordTextBox.Hint = "Пароль";
            this.PasswordTextBox.LeadingIcon = null;
            this.PasswordTextBox.Location = new System.Drawing.Point(24, 213);
            this.PasswordTextBox.MaxLength = 50;
            this.PasswordTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.PasswordTextBox.Multiline = false;
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Password = true;
            this.PasswordTextBox.Size = new System.Drawing.Size(291, 50);
            this.PasswordTextBox.TabIndex = 1;
            this.PasswordTextBox.Text = "";
            this.PasswordTextBox.TrailingIcon = null;
            this.PasswordTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.PasswordTextBox_Validating);
            this.PasswordTextBox.Validated += new System.EventHandler(this.PasswordTextBox_Validated);
            // 
            // LoginBtn
            // 
            this.LoginBtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.LoginBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.LoginBtn.Depth = 0;
            this.LoginBtn.HighEmphasis = true;
            this.LoginBtn.Icon = null;
            this.LoginBtn.Location = new System.Drawing.Point(126, 272);
            this.LoginBtn.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.LoginBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.LoginBtn.Size = new System.Drawing.Size(71, 36);
            this.LoginBtn.TabIndex = 2;
            this.LoginBtn.Text = "Войти";
            this.LoginBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.LoginBtn.UseAccentColor = false;
            this.LoginBtn.UseVisualStyleBackColor = true;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // WelcomeLabel
            // 
            this.WelcomeLabel.AutoSize = true;
            this.WelcomeLabel.Depth = 0;
            this.WelcomeLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.WelcomeLabel.Location = new System.Drawing.Point(104, 106);
            this.WelcomeLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.WelcomeLabel.Name = "WelcomeLabel";
            this.WelcomeLabel.Size = new System.Drawing.Size(149, 19);
            this.WelcomeLabel.TabIndex = 3;
            this.WelcomeLabel.Text = "Добро пожаловать!";
            this.WelcomeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AuthorizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 450);
            this.Controls.Add(this.WelcomeLabel);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.LoginTextBox);
            this.MaximumSize = new System.Drawing.Size(340, 450);
            this.MinimumSize = new System.Drawing.Size(340, 450);
            this.Name = "AuthorizationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Страхование. Авторизация";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AuthorizationForm_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AuthorizationForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialTextBox LoginTextBox;
        private MaterialSkin.Controls.MaterialTextBox PasswordTextBox;
        private MaterialSkin.Controls.MaterialButton LoginBtn;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private MaterialSkin.Controls.MaterialLabel WelcomeLabel;
    }
}