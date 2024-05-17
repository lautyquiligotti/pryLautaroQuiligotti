namespace pryLautaroQuiligotti
{
    partial class frmJuego
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
            this.temporizadorEnemigo = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.temporizadorDisparo = new System.Windows.Forms.Timer(this.components);
            this.timerEnemigosRestantes = new System.Windows.Forms.Timer(this.components);
            this.lblScore = new System.Windows.Forms.Label();
            this.timerBalasNave = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // temporizadorEnemigo
            // 
            this.temporizadorEnemigo.Enabled = true;
            this.temporizadorEnemigo.Tick += new System.EventHandler(this.temporizadorEnemigo_Tick);
            // 
            // temporizadorDisparo
            // 
            this.temporizadorDisparo.Enabled = true;
            this.temporizadorDisparo.Tick += new System.EventHandler(this.temporizadorDisparo_Tick);
            // 
            // timerEnemigosRestantes
            // 
            this.timerEnemigosRestantes.Enabled = true;
            this.timerEnemigosRestantes.Tick += new System.EventHandler(this.timerEnemigosRestantes_Tick);
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.BackColor = System.Drawing.Color.Transparent;
            this.lblScore.ForeColor = System.Drawing.Color.Lime;
            this.lblScore.Location = new System.Drawing.Point(754, 19);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(35, 13);
            this.lblScore.TabIndex = 0;
            this.lblScore.Text = "Score";
            // 
            // frmJuego
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(884, 861);
            this.Controls.Add(this.lblScore);
            this.Name = "frmJuego";
            this.Text = "GALAGA";
            this.Load += new System.EventHandler(this.frmJuego_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmJuego_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer temporizadorEnemigo;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Timer temporizadorDisparo;
        private System.Windows.Forms.Timer timerEnemigosRestantes;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Timer timerBalasNave;
    }
}