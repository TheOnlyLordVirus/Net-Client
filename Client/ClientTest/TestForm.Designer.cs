namespace ClientTest
{
    partial class TestForm
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
            this.testButton1 = new System.Windows.Forms.Button();
            this.testButton2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // testButton1
            // 
            this.testButton1.Location = new System.Drawing.Point(108, 184);
            this.testButton1.Name = "testButton1";
            this.testButton1.Size = new System.Drawing.Size(198, 84);
            this.testButton1.TabIndex = 0;
            this.testButton1.Text = "Test Client Command";
            this.testButton1.UseVisualStyleBackColor = true;
            this.testButton1.Click += new System.EventHandler(this.testButton_Click);
            // 
            // testButton2
            // 
            this.testButton2.Location = new System.Drawing.Point(470, 184);
            this.testButton2.Name = "testButton2";
            this.testButton2.Size = new System.Drawing.Size(198, 84);
            this.testButton2.TabIndex = 1;
            this.testButton2.Text = "Check time";
            this.testButton2.UseVisualStyleBackColor = true;
            this.testButton2.Click += new System.EventHandler(this.testButton2_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 413);
            this.Controls.Add(this.testButton2);
            this.Controls.Add(this.testButton1);
            this.Name = "TestForm";
            this.Text = "Test Client Command";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button testButton1;
        private System.Windows.Forms.Button testButton2;
    }
}

