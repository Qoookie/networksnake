
namespace ServerSnakeNetwork
{
    partial class IndexServeur
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
            this.lbxConnection = new System.Windows.Forms.ListBox();
            this.lbxNomLobby = new System.Windows.Forms.ListBox();
            this.txtConfiguration = new System.Windows.Forms.Label();
            this.pConfiguration = new System.Windows.Forms.Panel();
            this.lblConf = new System.Windows.Forms.Label();
            this.pConfiguration.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbxConnection
            // 
            this.lbxConnection.FormattingEnabled = true;
            this.lbxConnection.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbxConnection.Location = new System.Drawing.Point(274, 12);
            this.lbxConnection.Name = "lbxConnection";
            this.lbxConnection.Size = new System.Drawing.Size(196, 316);
            this.lbxConnection.TabIndex = 0;
            // 
            // lbxNomLobby
            // 
            this.lbxNomLobby.FormattingEnabled = true;
            this.lbxNomLobby.Location = new System.Drawing.Point(12, 158);
            this.lbxNomLobby.Name = "lbxNomLobby";
            this.lbxNomLobby.Size = new System.Drawing.Size(256, 173);
            this.lbxNomLobby.TabIndex = 2;
            // 
            // txtConfiguration
            // 
            this.txtConfiguration.AutoSize = true;
            this.txtConfiguration.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfiguration.Location = new System.Drawing.Point(3, 9);
            this.txtConfiguration.Name = "txtConfiguration";
            this.txtConfiguration.Size = new System.Drawing.Size(194, 18);
            this.txtConfiguration.TabIndex = 0;
            this.txtConfiguration.Text = "Configuration IP du serveur :";
            // 
            // pConfiguration
            // 
            this.pConfiguration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pConfiguration.Controls.Add(this.lblConf);
            this.pConfiguration.Controls.Add(this.txtConfiguration);
            this.pConfiguration.Location = new System.Drawing.Point(12, 12);
            this.pConfiguration.Name = "pConfiguration";
            this.pConfiguration.Size = new System.Drawing.Size(256, 140);
            this.pConfiguration.TabIndex = 3;
            // 
            // lblConf
            // 
            this.lblConf.AutoSize = true;
            this.lblConf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConf.Location = new System.Drawing.Point(16, 30);
            this.lblConf.Name = "lblConf";
            this.lblConf.Size = new System.Drawing.Size(0, 15);
            this.lblConf.TabIndex = 1;
            // 
            // IndexServeur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 340);
            this.Controls.Add(this.pConfiguration);
            this.Controls.Add(this.lbxNomLobby);
            this.Controls.Add(this.lbxConnection);
            this.Name = "IndexServeur";
            this.Text = "IndexServeur";
            this.Load += new System.EventHandler(this.IndexServeur_Load);
            this.pConfiguration.ResumeLayout(false);
            this.pConfiguration.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbxConnection;
        private System.Windows.Forms.ListBox lbxNomLobby;
        private System.Windows.Forms.Label txtConfiguration;
        private System.Windows.Forms.Panel pConfiguration;
        private System.Windows.Forms.Label lblConf;
    }
}