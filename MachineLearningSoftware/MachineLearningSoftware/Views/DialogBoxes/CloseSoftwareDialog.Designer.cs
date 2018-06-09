namespace MachineLearningSoftware.Views.DialogBoxes
{
    partial class CloseSoftwareDialog
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
            this.ExitLabel1 = new System.Windows.Forms.Label();
            this.YesButton1 = new System.Windows.Forms.Button();
            this.NoButton1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ExitLabel1
            // 
            this.ExitLabel1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitLabel1.Location = new System.Drawing.Point(12, 9);
            this.ExitLabel1.Name = "ExitLabel1";
            this.ExitLabel1.Size = new System.Drawing.Size(230, 23);
            this.ExitLabel1.TabIndex = 0;
            this.ExitLabel1.Text = "Are you sure you want to exit?";
            this.ExitLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // YesButton1
            // 
            this.YesButton1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YesButton1.Location = new System.Drawing.Point(37, 46);
            this.YesButton1.Name = "YesButton1";
            this.YesButton1.Size = new System.Drawing.Size(75, 23);
            this.YesButton1.TabIndex = 1;
            this.YesButton1.Text = "Yes";
            this.YesButton1.UseVisualStyleBackColor = true;
            this.YesButton1.Click += new System.EventHandler(this.YesButton1_Click);
            // 
            // NoButton1
            // 
            this.NoButton1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoButton1.Location = new System.Drawing.Point(145, 46);
            this.NoButton1.Name = "NoButton1";
            this.NoButton1.Size = new System.Drawing.Size(75, 23);
            this.NoButton1.TabIndex = 2;
            this.NoButton1.Text = "No";
            this.NoButton1.UseVisualStyleBackColor = true;
            this.NoButton1.Click += new System.EventHandler(this.NoButton1_Click);
            // 
            // CloseSoftwareDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(254, 81);
            this.Controls.Add(this.NoButton1);
            this.Controls.Add(this.YesButton1);
            this.Controls.Add(this.ExitLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CloseSoftwareDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exit?";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label ExitLabel1;
        private System.Windows.Forms.Button YesButton1;
        private System.Windows.Forms.Button NoButton1;
    }
}