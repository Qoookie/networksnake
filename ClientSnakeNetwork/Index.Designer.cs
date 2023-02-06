
namespace ClientSnakeNetwork
{
    partial class Index
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
            this.lblTitre = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.first = new System.Windows.Forms.MaskedTextBox();
            this.pnlAdresseIP = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.fourth = new System.Windows.Forms.MaskedTextBox();
            this.third = new System.Windows.Forms.MaskedTextBox();
            this.second = new System.Windows.Forms.MaskedTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.cbxCouleurSerpent = new System.Windows.Forms.ComboBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.btnRejoindreGame = new System.Windows.Forms.Button();
            this.btnCreerPartie = new System.Windows.Forms.Button();
            this.btnLauchGame = new System.Windows.Forms.Button();
            this.lblFalsePass = new System.Windows.Forms.Label();
            this.pnlAdresseIP.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.Location = new System.Drawing.Point(12, 9);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(299, 25);
            this.lblTitre.TabIndex = 7;
            this.lblTitre.Text = "Entrez l\'adresse IP du serveur";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(196, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Discover";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(14, 96);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(84, 13);
            this.lblError.TabIndex = 3;
            this.lblError.Text = "ServerConnecte";
            this.lblError.Visible = false;
            // 
            // first
            // 
            this.first.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.first.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.first.Location = new System.Drawing.Point(4, 7);
            this.first.Mask = "099";
            this.first.Name = "first";
            this.first.Size = new System.Drawing.Size(23, 15);
            this.first.TabIndex = 0;
            this.first.KeyDown += new System.Windows.Forms.KeyEventHandler(this.first_KeyDown);
            this.first.KeyUp += new System.Windows.Forms.KeyEventHandler(this.first_KeyUp);
            // 
            // pnlAdresseIP
            // 
            this.pnlAdresseIP.BackColor = System.Drawing.Color.White;
            this.pnlAdresseIP.Controls.Add(this.label5);
            this.pnlAdresseIP.Controls.Add(this.label4);
            this.pnlAdresseIP.Controls.Add(this.label3);
            this.pnlAdresseIP.Controls.Add(this.fourth);
            this.pnlAdresseIP.Controls.Add(this.third);
            this.pnlAdresseIP.Controls.Add(this.second);
            this.pnlAdresseIP.Controls.Add(this.first);
            this.pnlAdresseIP.Location = new System.Drawing.Point(17, 53);
            this.pnlAdresseIP.Name = "pnlAdresseIP";
            this.pnlAdresseIP.Size = new System.Drawing.Size(173, 25);
            this.pnlAdresseIP.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(122, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 18);
            this.label5.TabIndex = 5;
            this.label5.Text = ".";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(75, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = ".";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(28, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 18);
            this.label3.TabIndex = 1;
            this.label3.Text = ".";
            // 
            // fourth
            // 
            this.fourth.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fourth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fourth.Location = new System.Drawing.Point(140, 7);
            this.fourth.Mask = "099";
            this.fourth.Name = "fourth";
            this.fourth.Size = new System.Drawing.Size(23, 15);
            this.fourth.TabIndex = 6;
            this.fourth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fourth_KeyDown);
            this.fourth.KeyUp += new System.Windows.Forms.KeyEventHandler(this.fourth_KeyUp);
            // 
            // third
            // 
            this.third.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.third.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.third.Location = new System.Drawing.Point(93, 7);
            this.third.Mask = "099";
            this.third.Name = "third";
            this.third.Size = new System.Drawing.Size(23, 15);
            this.third.TabIndex = 4;
            this.third.KeyDown += new System.Windows.Forms.KeyEventHandler(this.third_KeyDown);
            this.third.KeyUp += new System.Windows.Forms.KeyEventHandler(this.third_KeyUp);
            // 
            // second
            // 
            this.second.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.second.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.second.Location = new System.Drawing.Point(46, 7);
            this.second.Mask = "099";
            this.second.Name = "second";
            this.second.Size = new System.Drawing.Size(23, 15);
            this.second.TabIndex = 2;
            this.second.KeyDown += new System.Windows.Forms.KeyEventHandler(this.second_KeyDown);
            this.second.KeyUp += new System.Windows.Forms.KeyEventHandler(this.second_KeyUp);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(277, 52);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Loopback";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cbxCouleurSerpent
            // 
            this.cbxCouleurSerpent.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbxCouleurSerpent.FormattingEnabled = true;
            this.cbxCouleurSerpent.Location = new System.Drawing.Point(208, 150);
            this.cbxCouleurSerpent.Name = "cbxCouleurSerpent";
            this.cbxCouleurSerpent.Size = new System.Drawing.Size(121, 21);
            this.cbxCouleurSerpent.TabIndex = 6;
            this.cbxCouleurSerpent.Text = "Aleatoire";
            this.cbxCouleurSerpent.Visible = false;
            this.cbxCouleurSerpent.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbxCouleurSerpent_DrawItem);
            this.cbxCouleurSerpent.SelectedIndexChanged += new System.EventHandler(this.cbxCouleurSerpent_SelectedIndexChanged);
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(14, 153);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(188, 13);
            this.lblColor.TabIndex = 5;
            this.lblColor.Text = "Choisissez la couleur de votre serpent:";
            this.lblColor.Visible = false;
            // 
            // btnRejoindreGame
            // 
            this.btnRejoindreGame.Location = new System.Drawing.Point(189, 114);
            this.btnRejoindreGame.Name = "btnRejoindreGame";
            this.btnRejoindreGame.Size = new System.Drawing.Size(163, 23);
            this.btnRejoindreGame.TabIndex = 8;
            this.btnRejoindreGame.Text = "Rejoindre une partie";
            this.btnRejoindreGame.UseVisualStyleBackColor = true;
            this.btnRejoindreGame.Visible = false;
            this.btnRejoindreGame.Click += new System.EventHandler(this.btnRejoindreGame_Click);
            // 
            // btnCreerPartie
            // 
            this.btnCreerPartie.Location = new System.Drawing.Point(12, 114);
            this.btnCreerPartie.Name = "btnCreerPartie";
            this.btnCreerPartie.Size = new System.Drawing.Size(163, 23);
            this.btnCreerPartie.TabIndex = 8;
            this.btnCreerPartie.Text = "Creer une partie";
            this.btnCreerPartie.UseVisualStyleBackColor = true;
            this.btnCreerPartie.Visible = false;
            this.btnCreerPartie.Click += new System.EventHandler(this.btnCreerPartie_Click);
            // 
            // btnLauchGame
            // 
            this.btnLauchGame.Location = new System.Drawing.Point(11, 114);
            this.btnLauchGame.Name = "btnLauchGame";
            this.btnLauchGame.Size = new System.Drawing.Size(341, 23);
            this.btnLauchGame.TabIndex = 9;
            this.btnLauchGame.Text = "Démarrer la partie";
            this.btnLauchGame.UseVisualStyleBackColor = true;
            this.btnLauchGame.Visible = false;
            this.btnLauchGame.Click += new System.EventHandler(this.btnLauchGame_Click);
            // 
            // lblFalsePass
            // 
            this.lblFalsePass.AutoSize = true;
            this.lblFalsePass.ForeColor = System.Drawing.Color.Red;
            this.lblFalsePass.Location = new System.Drawing.Point(14, 153);
            this.lblFalsePass.Name = "lblFalsePass";
            this.lblFalsePass.Size = new System.Drawing.Size(112, 13);
            this.lblFalsePass.TabIndex = 10;
            this.lblFalsePass.Text = "Mot de passe incorect";
            this.lblFalsePass.Visible = false;
            // 
            // Index
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 147);
            this.Controls.Add(this.lblFalsePass);
            this.Controls.Add(this.btnCreerPartie);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.btnRejoindreGame);
            this.Controls.Add(this.cbxCouleurSerpent);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.pnlAdresseIP);
            this.Controls.Add(this.btnLauchGame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Index";
            this.Text = "Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlAdresseIP.ResumeLayout(false);
            this.pnlAdresseIP.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.MaskedTextBox first;
        private System.Windows.Forms.Panel pnlAdresseIP;
        private System.Windows.Forms.MaskedTextBox fourth;
        private System.Windows.Forms.MaskedTextBox third;
        private System.Windows.Forms.MaskedTextBox second;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cbxCouleurSerpent;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Button btnRejoindreGame;
        private System.Windows.Forms.Button btnCreerPartie;
        private System.Windows.Forms.Button btnLauchGame;
        private System.Windows.Forms.Label lblFalsePass;
    }
}

