namespace DesignFormGS
{
    partial class DesignFormGS
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
            GSM01000 = new Button();
            GSM01001GOA = new Button();
            SuspendLayout();
            // 
            // GSM01000
            // 
            GSM01000.Location = new Point(35, 40);
            GSM01000.Name = "GSM01000";
            GSM01000.Size = new Size(112, 34);
            GSM01000.TabIndex = 0;
            GSM01000.Text = "GSM01000";
            GSM01000.UseVisualStyleBackColor = true;
            GSM01000.Click += GSM01000_Click;
            // 
            // GSM01001GOA
            // 
            GSM01001GOA.Location = new Point(35, 97);
            GSM01001GOA.Name = "GSM01001GOA";
            GSM01001GOA.Size = new Size(156, 34);
            GSM01001GOA.TabIndex = 0;
            GSM01001GOA.Text = "GSM01000GOA";
            GSM01001GOA.UseVisualStyleBackColor = true;
            GSM01001GOA.Click += GSM01000GOA_Click;
            // 
            // DesignFormGS
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(GSM01000);
            Controls.Add(GSM01001GOA);
            Name = "DesignFormGS";
            Text = "DesignFormGS";
            Load += DesignFormGS_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button GSM01000;
        private Button GSM01001GOA;
    }
}