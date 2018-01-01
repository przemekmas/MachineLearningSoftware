namespace ObjectRecognitionSoftware.Views.DialogBoxes
{
    partial class HelpDialogBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpDialogBox));
            this.TitleLabel1 = new System.Windows.Forms.Label();
            this.VersionLabel1 = new System.Windows.Forms.Label();
            this.MainInformationLabel1 = new System.Windows.Forms.Label();
            this.CopyrightLabel1 = new System.Windows.Forms.Label();
            this.OkButton1 = new System.Windows.Forms.Button();
            this.LogoLabel1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TitleLabel1
            // 
            this.TitleLabel1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel1.Location = new System.Drawing.Point(12, 108);
            this.TitleLabel1.Name = "TitleLabel1";
            this.TitleLabel1.Size = new System.Drawing.Size(320, 23);
            this.TitleLabel1.TabIndex = 0;
            this.TitleLabel1.Text = "Tensor Flow Software";
            this.TitleLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VersionLabel1
            // 
            this.VersionLabel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.VersionLabel1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VersionLabel1.Location = new System.Drawing.Point(13, 142);
            this.VersionLabel1.Name = "VersionLabel1";
            this.VersionLabel1.Size = new System.Drawing.Size(316, 23);
            this.VersionLabel1.TabIndex = 1;
            this.VersionLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainInformationLabel1
            // 
            this.MainInformationLabel1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainInformationLabel1.Location = new System.Drawing.Point(13, 177);
            this.MainInformationLabel1.Name = "MainInformationLabel1";
            this.MainInformationLabel1.Size = new System.Drawing.Size(316, 114);
            this.MainInformationLabel1.TabIndex = 2;
            this.MainInformationLabel1.Text = resources.GetString("MainInformationLabel1.Text");
            // 
            // CopyrightLabel1
            // 
            this.CopyrightLabel1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CopyrightLabel1.Location = new System.Drawing.Point(19, 291);
            this.CopyrightLabel1.Name = "CopyrightLabel1";
            this.CopyrightLabel1.Size = new System.Drawing.Size(313, 23);
            this.CopyrightLabel1.TabIndex = 3;
            this.CopyrightLabel1.Text = "Copyright © 2017 Przemyslaw Maslowski";
            this.CopyrightLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OkButton1
            // 
            this.OkButton1.Location = new System.Drawing.Point(133, 331);
            this.OkButton1.Name = "OkButton1";
            this.OkButton1.Size = new System.Drawing.Size(75, 23);
            this.OkButton1.TabIndex = 4;
            this.OkButton1.Text = "Ok";
            this.OkButton1.UseVisualStyleBackColor = true;
            this.OkButton1.Click += new System.EventHandler(this.OkButton1_Click);
            // 
            // LogoLabel1
            // 
            this.LogoLabel1.Image = global::ObjectRecognitionSoftware.Properties.Resources.HelpTensorFlowLogo;
            this.LogoLabel1.Location = new System.Drawing.Point(12, -1);
            this.LogoLabel1.Name = "LogoLabel1";
            this.LogoLabel1.Size = new System.Drawing.Size(320, 109);
            this.LogoLabel1.TabIndex = 5;
            // 
            // HelpDialogBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.White;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(344, 361);
            this.Controls.Add(this.LogoLabel1);
            this.Controls.Add(this.OkButton1);
            this.Controls.Add(this.CopyrightLabel1);
            this.Controls.Add(this.MainInformationLabel1);
            this.Controls.Add(this.VersionLabel1);
            this.Controls.Add(this.TitleLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HelpDialogBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About Software";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label TitleLabel1;
        private System.Windows.Forms.Label VersionLabel1;
        private System.Windows.Forms.Label MainInformationLabel1;
        private System.Windows.Forms.Label CopyrightLabel1;
        private System.Windows.Forms.Button OkButton1;
        private System.Windows.Forms.Label LogoLabel1;
    }
}