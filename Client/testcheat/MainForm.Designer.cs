
namespace testcheat
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.SampleEvent = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // SampleEvent
            // 
            this.SampleEvent.Location = new System.Drawing.Point(103, 120);
            this.SampleEvent.Margin = new System.Windows.Forms.Padding(6);
            this.SampleEvent.Name = "SampleEvent";
            this.SampleEvent.Size = new System.Drawing.Size(353, 122);
            this.SampleEvent.TabIndex = 0;
            this.SampleEvent.Text = "Paid Cheat form example";
            this.SampleEvent.Click += new System.EventHandler(this.SampleEvent_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 384);
            this.Controls.Add(this.SampleEvent);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("MainForm.IconOptions.SvgImage")));
            this.Name = "MainForm";
            this.Text = "Cheat Form Example";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton SampleEvent;
    }
}

