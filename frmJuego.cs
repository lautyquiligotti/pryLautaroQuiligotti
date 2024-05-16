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

            //temporizadorDisparo.Interval = 50; // Intervalo de tiempo en milisegundos
            //temporizadorDisparo.Tick += temporizadorDisparo_Tick;
            //temporizadorDisparo.Start();
        }

        clsNave objNave;
        List<PictureBox> balasEnemigas = new List<PictureBox>(); // Lista para las balas de los enemigos

        private void frmJuego_Load(object sender, EventArgs e)
        {
            //Tamaño del frm
            this.Width = 900;
            this.Height = 1000;

            objNave = new clsNave();
            objNave.CrearJugador();
            objNave.imgNave.Location = new Point(350, 750);
            Controls.Add(objNave.imgNave);
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
            // Verificar si la nave ya fue destruida para evitar múltiples mensajes
            if (naveDestruida)
                return;

            // Copia de la lista de balas de enemigos
            var balasEnemigasCopia = new List<PictureBox>(balasEnemigas);

            foreach (PictureBox balaEnemiga in balasEnemigasCopia)
            {
                // Mover la bala hacia abajo
                balaEnemiga.Top += 10; //Velocidad de la bala

                // Verificar colisión con la nave
                if (balaEnemiga.Bounds.IntersectsWith(objNave.imgNave.Bounds))
                {
                    naveDestruida = true;

                    // Mostrar el mensaje de que la nave ha sido destruida
                    objNave.imgNave.Dispose();
                    MessageBox.Show("¡La nave ha sido destruida!");

                    // Eliminar todas las balas y detener el temporizador
                    foreach (var bala in balasEnemigas)
                    {
                        Controls.Remove(bala);
                        bala.Dispose();
                    }
                    balasEnemigas.Clear();
                    temporizadorDisparo.Stop();
                    break; 
                }

                // Eliminar la bala si sale de la pantalla
                if (balaEnemiga.Top >= this.ClientSize.Height)
                {
                    balasEnemigas.Remove(balaEnemiga);
                    Controls.Remove(balaEnemiga);
                    balaEnemiga.Dispose();
                }
            }
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
                objNave.Disparo();
            }
        }
    }
}
