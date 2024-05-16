using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryLautaroQuiligotti
{
    public partial class frmJuego : Form
    {
        public frmJuego()
        {
            InitializeComponent();
        }

        clsNave objNave;
        List<PictureBox> balasEnemigas = new List<PictureBox>(); // Lista para las balas de los enemigos
        List<PictureBox> balasNave = new List<PictureBox>();

        int enemigosRestantes = 5; 

        private void frmJuego_Load(object sender, EventArgs e)
        {
            //Tamaño del frm
            this.Width = 900;
            this.Height = 1000;

            objNave = new clsNave();
            objNave.CrearJugador();
            objNave.imgNave.Location = new Point(350, 750);
            Controls.Add(objNave.imgNave);

            temporizadorEnemigo.Enabled = true; //Nuevo
        }

        private void temporizadorEnemigo_Tick(object sender, EventArgs e)
        {
            objNave.CrearEnemigos(5); // Crear 5 enemigos en cada tick del timer
            foreach (PictureBox enemigo in objNave.enemigos)
            {
                Controls.Add(enemigo);
                // Crear balas para cada enemigo
                PictureBox balaEnemiga = new PictureBox();
                balaEnemiga.BackColor = Color.Red;
                balaEnemiga.Size = new Size(5, 5); 
                balaEnemiga.Location = new Point(enemigo.Location.X + enemigo.Width / 2, enemigo.Location.Y + enemigo.Height);
                Controls.Add(balaEnemiga);
                balasEnemigas.Add(balaEnemiga);
                temporizadorEnemigo.Enabled = false;
            }
        }

        bool naveDestruida = false;
        private void temporizadorDisparo_Tick(object sender, EventArgs e)
        {
            if (naveDestruida) // Verificar si la nave ya fue destruida para evitar múltiples mensajes
                return;

            var balasEnemigasCopia = new List<PictureBox>(balasEnemigas); // Copia de la lista de balas de enemigos

            foreach (PictureBox balaEnemiga in balasEnemigasCopia)
            {
                // Mover la bala hacia abajo
                balaEnemiga.Top += 6; //Velocidad de la bala

                if (balaEnemiga.Bounds.IntersectsWith(objNave.imgNave.Bounds)) // Verificar colisión con la nave
                {
                    naveDestruida = true;
                    objNave.imgNave.Dispose();
                    MessageBox.Show("¡La nave ha sido destruida!");

                    foreach (var bala in balasEnemigas) // Eliminar todas las balas y detener el temporizador
                    {
                        Controls.Remove(bala);
                        bala.Dispose();
                    }
                    balasEnemigas.Clear();
                    temporizadorDisparo.Stop();
                    break; 
                }

                if (balaEnemiga.Top >= this.ClientSize.Height) // Eliminar la bala si sale de la pantalla
                {
                    balasEnemigas.Remove(balaEnemiga);
                    Controls.Remove(balaEnemiga);
                    balaEnemiga.Dispose();
                }
            }
            Score();
        }

        private void frmJuego_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right) 
            {
                objNave.imgNave.Location = new Point(
                    objNave.imgNave.Location.X + 5, objNave.imgNave.Location.Y);
            }
            if (e.KeyCode == Keys.Left) 
            {
                objNave.imgNave.Location = new Point(
                    objNave.imgNave.Location.X - 5, objNave.imgNave.Location.Y);
            }
            if (e.KeyCode == Keys.Space) 
            {
                //objNave.Disparo();
                PictureBox balaNave = new PictureBox();
                balaNave.SizeMode = PictureBoxSizeMode.StretchImage;
                balaNave.Image = Imagenes.bala;
                balaNave.Size = new Size(30, 40);
                balaNave.Location = new Point(objNave.imgNave.Location.X + (objNave.imgNave.Width / 2) - (balaNave.Width / 2), objNave.imgNave.Location.Y);
                Controls.Add(balaNave);
                balasNave.Add(balaNave);
                objNave.Disparo(balaNave);
            }
        }

        private void timerEnemigosRestantes_Tick(object sender, EventArgs e)
        {
            bool quedanEnemigos = objNave.enemigos.Any(enemigo => enemigo.Visible);

            if (!quedanEnemigos)
            {
                objNave.CrearEnemigos(5); // Crear 5 enemigos en cada tick del timer
                foreach (PictureBox enemigo in objNave.enemigos)
                {
                    Controls.Add(enemigo);
                    // Crear balas para cada enemigo
                    PictureBox balaEnemiga = new PictureBox();
                    balaEnemiga.BackColor = Color.Red;
                    balaEnemiga.Size = new Size(5, 5);
                    balaEnemiga.Location = new Point(enemigo.Location.X + enemigo.Width / 2, enemigo.Location.Y + enemigo.Height);
                    Controls.Add(balaEnemiga);
                    balasEnemigas.Add(balaEnemiga);
                    temporizadorEnemigo.Enabled = false;
                }
            }
        }
        private void Score()
        {
            int enemigosEliminados = objNave.EnemigosEliminados();
            lblScore.Text = "SCORE:" + enemigosEliminados.ToString();
        }
    }
}
