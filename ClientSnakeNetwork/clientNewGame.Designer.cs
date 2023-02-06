
namespace ClientSnakeNetwork
{
    partial class clientNewGame
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbxMotDePass = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbVitesse = new System.Windows.Forms.GroupBox();
            this.rbtnVitesse1 = new System.Windows.Forms.RadioButton();
            this.rbtnVitesse3 = new System.Windows.Forms.RadioButton();
            this.rbtnVitesse2 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnPoint50 = new System.Windows.Forms.RadioButton();
            this.rbtnPointMax = new System.Windows.Forms.RadioButton();
            this.nudRound = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblNomPartie = new System.Windows.Forms.Label();
            this.tbxNomPartie = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.gbVitesse.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRound)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mot de passe de la partie : ";
            // 
            // tbxMotDePass
            // 
            this.tbxMotDePass.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.tbxMotDePass.Location = new System.Drawing.Point(144, 31);
            this.tbxMotDePass.Name = "tbxMotDePass";
            this.tbxMotDePass.Size = new System.Drawing.Size(113, 20);
            this.tbxMotDePass.TabIndex = 1;
            this.tbxMotDePass.UseSystemPasswordChar = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.gbVitesse);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.nudRound);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(15, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(242, 176);
            this.panel1.TabIndex = 6;
            // 
            // gbVitesse
            // 
            this.gbVitesse.Controls.Add(this.rbtnVitesse1);
            this.gbVitesse.Controls.Add(this.rbtnVitesse3);
            this.gbVitesse.Controls.Add(this.rbtnVitesse2);
            this.gbVitesse.Location = new System.Drawing.Point(7, 28);
            this.gbVitesse.Name = "gbVitesse";
            this.gbVitesse.Size = new System.Drawing.Size(228, 46);
            this.gbVitesse.TabIndex = 1;
            this.gbVitesse.TabStop = false;
            this.gbVitesse.Text = "Vitesse de déplacment";
            // 
            // rbtnVitesse1
            // 
            this.rbtnVitesse1.AutoSize = true;
            this.rbtnVitesse1.Checked = true;
            this.rbtnVitesse1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnVitesse1.Location = new System.Drawing.Point(7, 19);
            this.rbtnVitesse1.Name = "rbtnVitesse1";
            this.rbtnVitesse1.Size = new System.Drawing.Size(58, 17);
            this.rbtnVitesse1.TabIndex = 0;
            this.rbtnVitesse1.TabStop = true;
            this.rbtnVitesse1.Tag = "80";
            this.rbtnVitesse1.Text = "Normal";
            this.rbtnVitesse1.UseVisualStyleBackColor = true;
            // 
            // rbtnVitesse3
            // 
            this.rbtnVitesse3.AutoSize = true;
            this.rbtnVitesse3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnVitesse3.ForeColor = System.Drawing.Color.Red;
            this.rbtnVitesse3.Location = new System.Drawing.Point(136, 19);
            this.rbtnVitesse3.Name = "rbtnVitesse3";
            this.rbtnVitesse3.Size = new System.Drawing.Size(83, 17);
            this.rbtnVitesse3.TabIndex = 2;
            this.rbtnVitesse3.Tag = "10";
            this.rbtnVitesse3.Text = "Tres Rapide";
            this.rbtnVitesse3.UseVisualStyleBackColor = true;
            // 
            // rbtnVitesse2
            // 
            this.rbtnVitesse2.AutoSize = true;
            this.rbtnVitesse2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnVitesse2.Location = new System.Drawing.Point(71, 19);
            this.rbtnVitesse2.Name = "rbtnVitesse2";
            this.rbtnVitesse2.Size = new System.Drawing.Size(59, 17);
            this.rbtnVitesse2.TabIndex = 1;
            this.rbtnVitesse2.Tag = "40";
            this.rbtnVitesse2.Text = "Rapide";
            this.rbtnVitesse2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtnPoint50);
            this.groupBox1.Controls.Add(this.rbtnPointMax);
            this.groupBox1.Location = new System.Drawing.Point(8, 119);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 45);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nombre max de point";
            // 
            // rbtnPoint50
            // 
            this.rbtnPoint50.AutoSize = true;
            this.rbtnPoint50.Checked = true;
            this.rbtnPoint50.Location = new System.Drawing.Point(110, 19);
            this.rbtnPoint50.Name = "rbtnPoint50";
            this.rbtnPoint50.Size = new System.Drawing.Size(37, 17);
            this.rbtnPoint50.TabIndex = 1;
            this.rbtnPoint50.TabStop = true;
            this.rbtnPoint50.Text = "50";
            this.rbtnPoint50.UseVisualStyleBackColor = true;
            // 
            // rbtnPointMax
            // 
            this.rbtnPointMax.AutoSize = true;
            this.rbtnPointMax.Location = new System.Drawing.Point(6, 19);
            this.rbtnPointMax.Name = "rbtnPointMax";
            this.rbtnPointMax.Size = new System.Drawing.Size(98, 17);
            this.rbtnPointMax.TabIndex = 0;
            this.rbtnPointMax.Text = "remplir le terrain";
            this.rbtnPointMax.UseVisualStyleBackColor = true;
            this.rbtnPointMax.CheckedChanged += new System.EventHandler(this.rbtnPointMax_CheckedChanged);
            // 
            // nudRound
            // 
            this.nudRound.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudRound.Location = new System.Drawing.Point(10, 93);
            this.nudRound.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudRound.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRound.Name = "nudRound";
            this.nudRound.Size = new System.Drawing.Size(127, 20);
            this.nudRound.TabIndex = 3;
            this.nudRound.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Nombre de round";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "Parametre de la partie";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Location = new System.Drawing.Point(15, 240);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(242, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "creer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblNomPartie
            // 
            this.lblNomPartie.AutoSize = true;
            this.lblNomPartie.Location = new System.Drawing.Point(12, 9);
            this.lblNomPartie.Name = "lblNomPartie";
            this.lblNomPartie.Size = new System.Drawing.Size(90, 13);
            this.lblNomPartie.TabIndex = 8;
            this.lblNomPartie.Text = "Nom de la partie :";
            // 
            // tbxNomPartie
            // 
            this.tbxNomPartie.Location = new System.Drawing.Point(111, 6);
            this.tbxNomPartie.Name = "tbxNomPartie";
            this.tbxNomPartie.Size = new System.Drawing.Size(146, 20);
            this.tbxNomPartie.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(5, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "*";
            // 
            // clientNewGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 273);
            this.Controls.Add(this.tbxNomPartie);
            this.Controls.Add(this.lblNomPartie);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tbxMotDePass);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "clientNewGame";
            this.Text = "clientNewGame";
            this.Load += new System.EventHandler(this.clientNewGame_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbVitesse.ResumeLayout(false);
            this.gbVitesse.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRound)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxMotDePass;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbVitesse;
        private System.Windows.Forms.RadioButton rbtnVitesse1;
        private System.Windows.Forms.RadioButton rbtnVitesse3;
        private System.Windows.Forms.RadioButton rbtnVitesse2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnPoint50;
        private System.Windows.Forms.RadioButton rbtnPointMax;
        private System.Windows.Forms.NumericUpDown nudRound;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblNomPartie;
        private System.Windows.Forms.TextBox tbxNomPartie;
        private System.Windows.Forms.Label label2;
    }
}