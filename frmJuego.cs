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

            temporizadorDisparo.Interval = 50; // Intervalo de tiempo en milisegundos
            temporizadorDisparo.Tick += temporizadorDisparo_Tick;
            temporizadorDisparo.Start();
        }

        clsNave objNave;
        Random random = new Random();
        Timer timerEnemigos = new Timer(); // Timer para el movimiento y disparo de los enemigos
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
                balaEnemiga.Size = new Size(5, 5); // Tamaño de la bala
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
                balaEnemiga.Top += 3; //Velocidad de la bala

                // Verificar colisión con la nave
                if (balaEnemiga.Bounds.IntersectsWith(objNave.imgNave.Bounds))
                {
                    // Marcar que la nave ha sido destruida
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
                    break; // Salir del bucle una vez que se detecta la colisión
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
            if (e.KeyCode == Keys.Right) //Mover hacia la derecha
            {
                objNave.imgNave.Location = new Point(
                    objNave.imgNave.Location.X + 5, objNave.imgNave.Location.Y);
            }
            if (e.KeyCode == Keys.Left) //Mover hacia la izquierda
            {
                objNave.imgNave.Location = new Point(
                    objNave.imgNave.Location.X - 5, objNave.imgNave.Location.Y);
            }

            if (e.KeyCode == Keys.Space) //Si presiona el espacio
            {
                //Que dispare bala
                objNave.Disparo();
            }
        }

        //private void CrearBalasEnemigos()
        //{
        //    foreach (PictureBox enemigo in objNave.enemigos)
        //    {
        //        // Crear una nueva bala para cada enemigo y moverla hacia abajo
        //        PictureBox balaEnemiga = new PictureBox();
        //        balaEnemiga.BackColor = Color.Red;
        //        balaEnemiga.Size = new Size(5, 5); // Tamaño de la bala
        //        balaEnemiga.Location = new Point(enemigo.Location.X + enemigo.Width / 2, enemigo.Location.Y + enemigo.Height);
        //        Controls.Add(balaEnemiga);
        //        balasEnemigas.Add(balaEnemiga);
        //    }
        //}

        //private void VerificarColisiones()
        //{
        //    foreach (PictureBox balaEnemiga in balasEnemigas.ToList()) // Usar ToList para evitar excepción al modificar la lista
        //    {
        //        // Mover la bala hacia abajo
        //        balaEnemiga.Top += 1;

        //        // Verificar colisión con la nave
        //        if (balaEnemiga.Bounds.IntersectsWith(objNave.imgNave.Bounds))
        //        {
        //            // Marcar que la nave ha sido destruida
        //            naveDestruida = true;

        //            // Mostrar el mensaje de que la nave ha sido destruida
        //            objNave.imgNave.Dispose();
        //            MessageBox.Show("¡La nave ha sido destruida!");

        //            // Eliminar todas las balas y detener el temporizador
        //            foreach (var bala in balasEnemigas)
        //            {
        //                Controls.Remove(bala);
        //                bala.Dispose();
        //            }
        //            balasEnemigas.Clear();
        //            temporizadorDisparo.Stop();
        //            break; // Salir del bucle una vez que se detecta la colisión
        //        }

        //        // Eliminar la bala si sale de la pantalla
        //        if (balaEnemiga.Top >= this.ClientSize.Height)
        //        {
        //            balasEnemigas.Remove(balaEnemiga);
        //            Controls.Remove(balaEnemiga);
        //            balaEnemiga.Dispose();
        //        }
        //    }

        //}
    }
}
