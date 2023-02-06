
namespace ServerSnakeNetwork
{
    partial class serveurGame
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
            this.pConfiguration = new System.Windows.Forms.Panel();
            this.lblConf = new System.Windows.Forms.Label();
            this.txtConfiguration = new System.Windows.Forms.Label();
            this.lbxAdresseIP = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.histTouche = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tPartie = new System.Windows.Forms.Timer(this.components);
            this.tBonus = new System.Windows.Forms.Timer(this.components);
            this.pConfiguration.SuspendLayout();
            this.SuspendLayout();
            // 
            // pConfiguration
            // 
            this.pConfiguration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pConfiguration.Controls.Add(this.lblConf);
            this.pConfiguration.Controls.Add(this.txtConfiguration);
            this.pConfiguration.Location = new System.Drawing.Point(12, 12);
            this.pConfiguration.Name = "pConfiguration";
            this.pConfiguration.Size = new System.Drawing.Size(256, 140);
            this.pConfiguration.TabIndex = 0;
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
            // lbxAdresseIP
            // 
            this.lbxAdresseIP.FormattingEnabled = true;
            this.lbxAdresseIP.Location = new System.Drawing.Point(12, 179);
            this.lbxAdresseIP.Name = "lbxAdresseIP";
            this.lbxAdresseIP.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbxAdresseIP.Size = new System.Drawing.Size(256, 212);
            this.lbxAdresseIP.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Liste des Machines Connectées";
            // 
            // histTouche
            // 
            this.histTouche.FormattingEnabled = true;
            this.histTouche.Location = new System.Drawing.Point(292, 49);
            this.histTouche.Name = "histTouche";
            this.histTouche.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.histTouche.Size = new System.Drawing.Size(242, 342);
            this.histTouche.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(289, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Historique des touches";
            // 
            // tPartie
            // 
            this.tPartie.Interval = 80;
            this.tPartie.Tick += new System.EventHandler(this.tDeplacment_Tick);
            // 
            // tBonus
            // 
            this.tBonus.Interval = 10000;
            this.tBonus.Tick += new System.EventHandler(this.tBonus_Tick);
            // 
            // serveurGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 400);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.histTouche);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbxAdresseIP);
            this.Controls.Add(this.pConfiguration);
            this.Name = "serveurGame";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pConfiguration.ResumeLayout(false);
            this.pConfiguration.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pConfiguration;
        private System.Windows.Forms.Label txtConfiguration;
        private System.Windows.Forms.Label lblConf;
        private System.Windows.Forms.ListBox lbxAdresseIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox histTouche;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer tPartie;
        private System.Windows.Forms.Timer tBonus;
    }
}

