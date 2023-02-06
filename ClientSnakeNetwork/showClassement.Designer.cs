
namespace ClientSnakeNetwork
{
    partial class showClassement
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
            this.btnRecommencer = new System.Windows.Forms.Button();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRecommencer
            // 
            this.btnRecommencer.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.btnRecommencer.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRecommencer.Location = new System.Drawing.Point(0, 0);
            this.btnRecommencer.Name = "btnRecommencer";
            this.btnRecommencer.Size = new System.Drawing.Size(142, 40);
            this.btnRecommencer.TabIndex = 0;
            this.btnRecommencer.Text = "Recommencer";
            this.btnRecommencer.UseVisualStyleBackColor = true;
            this.btnRecommencer.Click += new System.EventHandler(this.btnRecommencer_Click);
            // 
            // btnQuitter
            // 
            this.btnQuitter.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btnQuitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnQuitter.Location = new System.Drawing.Point(154, 0);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(142, 40);
            this.btnQuitter.TabIndex = 1;
            this.btnQuitter.Text = "Quitter";
            this.btnQuitter.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRecommencer);
            this.panel1.Controls.Add(this.btnQuitter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 233);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(296, 40);
            this.panel1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::ClientSnakeNetwork.Properties.Resources.info;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(262, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 25);
            this.button1.TabIndex = 5;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Classement General";
            // 
            // showClassement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 273);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "showClassement";
            this.Text = "showClassement";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.showClassement_FormClosing);
            this.Load += new System.EventHandler(this.showClassement_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.showClassement_Paint);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRecommencer;
        private System.Windows.Forms.Button btnQuitter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}