namespace MachineLearningSoftware.Views.DialogBoxes
{
    partial class WideDeepHelpDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WideDeepHelpDialog));
            this.HelpTitle1 = new System.Windows.Forms.Label();
            this.InformationLabel1 = new System.Windows.Forms.Label();
            this.SentenceLabel1 = new System.Windows.Forms.Label();
            this.SentenceLabel2 = new System.Windows.Forms.Label();
            this.SentenceLabel3 = new System.Windows.Forms.Label();
            this.SentenceLabel4 = new System.Windows.Forms.Label();
            this.CloseButton1 = new System.Windows.Forms.Button();
            this.InstructionsTitle1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // HelpTitle1
            // 
            this.HelpTitle1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpTitle1.Location = new System.Drawing.Point(12, 14);
            this.HelpTitle1.Name = "HelpTitle1";
            this.HelpTitle1.Size = new System.Drawing.Size(359, 23);
            this.HelpTitle1.TabIndex = 0;
            this.HelpTitle1.Text = "Wide Deep Help";
            // 
            // InformationLabel1
            // 
            this.InformationLabel1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InformationLabel1.Location = new System.Drawing.Point(12, 40);
            this.InformationLabel1.Name = "InformationLabel1";
            this.InformationLabel1.Size = new System.Drawing.Size(359, 42);
            this.InformationLabel1.TabIndex = 1;
            this.InformationLabel1.Text = "This page will help you understand how to use the wide deep model to make predict" +
    "ions.";
            // 
            // SentenceLabel1
            // 
            this.SentenceLabel1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SentenceLabel1.Location = new System.Drawing.Point(12, 115);
            this.SentenceLabel1.Name = "SentenceLabel1";
            this.SentenceLabel1.Size = new System.Drawing.Size(359, 44);
            this.SentenceLabel1.TabIndex = 2;
            this.SentenceLabel1.Text = "1. The first step is to make sure that the latest version of python has been inst" +
    "alled on the local computer. The latest version of python is recommended.";
            // 
            // SentenceLabel2
            // 
            this.SentenceLabel2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SentenceLabel2.Location = new System.Drawing.Point(12, 164);
            this.SentenceLabel2.Name = "SentenceLabel2";
            this.SentenceLabel2.Size = new System.Drawing.Size(359, 61);
            this.SentenceLabel2.TabIndex = 3;
            this.SentenceLabel2.Text = resources.GetString("SentenceLabel2.Text");
            // 
            // SentenceLabel3
            // 
            this.SentenceLabel3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SentenceLabel3.Location = new System.Drawing.Point(12, 226);
            this.SentenceLabel3.Name = "SentenceLabel3";
            this.SentenceLabel3.Size = new System.Drawing.Size(359, 36);
            this.SentenceLabel3.TabIndex = 4;
            this.SentenceLabel3.Text = "3. After the installation is done, you can now provide multiple information withi" +
    "n the first table that you would like to predict.";
            // 
            // SentenceLabel4
            // 
            this.SentenceLabel4.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SentenceLabel4.Location = new System.Drawing.Point(12, 260);
            this.SentenceLabel4.Name = "SentenceLabel4";
            this.SentenceLabel4.Size = new System.Drawing.Size(359, 45);
            this.SentenceLabel4.TabIndex = 5;
            this.SentenceLabel4.Text = "4. Lastly, you can click save which will save your predictions to a file. Then th" +
    "e predict button can be pressed which will provide predicitons within the second" +
    " table.";
            // 
            // CloseButton1
            // 
            this.CloseButton1.Location = new System.Drawing.Point(154, 318);
            this.CloseButton1.Name = "CloseButton1";
            this.CloseButton1.Size = new System.Drawing.Size(75, 23);
            this.CloseButton1.TabIndex = 6;
            this.CloseButton1.Text = "Close";
            this.CloseButton1.UseVisualStyleBackColor = true;
            this.CloseButton1.Click += new System.EventHandler(this.CloseButton1_Click);
            // 
            // InstructionsTitle1
            // 
            this.InstructionsTitle1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InstructionsTitle1.Location = new System.Drawing.Point(12, 93);
            this.InstructionsTitle1.Name = "InstructionsTitle1";
            this.InstructionsTitle1.Size = new System.Drawing.Size(359, 15);
            this.InstructionsTitle1.TabIndex = 7;
            this.InstructionsTitle1.Text = "Instructions:";
            // 
            // WideDeepHelpDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(383, 352);
            this.Controls.Add(this.InstructionsTitle1);
            this.Controls.Add(this.CloseButton1);
            this.Controls.Add(this.SentenceLabel4);
            this.Controls.Add(this.SentenceLabel3);
            this.Controls.Add(this.SentenceLabel2);
            this.Controls.Add(this.SentenceLabel1);
            this.Controls.Add(this.InformationLabel1);
            this.Controls.Add(this.HelpTitle1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WideDeepHelpDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tensor Flow Help";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label HelpTitle1;
        private System.Windows.Forms.Label InformationLabel1;
        private System.Windows.Forms.Label SentenceLabel1;
        private System.Windows.Forms.Label SentenceLabel2;
        private System.Windows.Forms.Label SentenceLabel3;
        private System.Windows.Forms.Label SentenceLabel4;
        private System.Windows.Forms.Button CloseButton1;
        private System.Windows.Forms.Label InstructionsTitle1;
    }
}