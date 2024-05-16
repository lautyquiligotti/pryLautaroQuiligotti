using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace pryLautaroQuiligotti
{
    internal class clsNave
    {
        public int vida;
        public string nombre;
        int puntosdaño;
        public PictureBox imgNave;
        public PictureBox imgBala;
        public int score = 0;
        public List<PictureBox> enemigos = new List<PictureBox>();
        Random random = new Random();

        public void CrearJugador()
        {
            vida = 100;
            nombre = "Jugador1";
            puntosdaño = 1;

            imgNave = new PictureBox();
            imgNave.SizeMode = PictureBoxSizeMode.StretchImage;
            imgNave.Image = Imagenes.imgNave;
        }

        public void CrearEnemigos(int cantidad)
        {
            for (int i = 0; i < cantidad; i++)
            {
                PictureBox enemigo = new PictureBox();
                enemigo.SizeMode = PictureBoxSizeMode.StretchImage;
                enemigo.Size = new Size(80, 80);

                // Selección aleatoria de la imagen del enemigo
                int tipoEnemigo = random.Next(1, 4);
                switch (tipoEnemigo)
                {
                    case 1:
                        enemigo.Image = Imagenes.enemigoGalaga1;
                        break;
                    case 2:
                        enemigo.Image = Imagenes.enemigoGalaga2; ;
                        break;
                    case 3:
                        enemigo.Image = Imagenes.enemigoGalaga3;
                        break;
                    default:
                        break;
                }

                // Generar posiciones aleatorias
                int posX = random.Next(0, 720); 
                int posY = random.Next(0, 520); 

                enemigo.Location = new Point(posX, posY);
                enemigo.Tag = "Enemigo";

                enemigos.Add(enemigo);
            }
        }

        public void Disparo()
        {
            imgBala = new PictureBox();
            imgBala.SizeMode = PictureBoxSizeMode.StretchImage;
            imgBala.Image = Imagenes.bala;
            imgBala.Size = new Size(30, 40);
            imgBala.Location = new Point(imgNave.Location.X + (imgNave.Width / 2) - (imgBala.Width / 2), imgNave.Location.Y);

            Form.ActiveForm.Controls.Add(imgBala);

            Timer timerBala = new Timer();
            timerBala.Interval = 20;

            timerBala.Tick += (sender, e) =>
            {
                imgBala.Top -= 10;
                imgBala.BringToFront();

                foreach (Control control in Form.ActiveForm.Controls)
                {
                    if (control is PictureBox && control.Tag != null && control.Tag.ToString() == "Enemigo")
                    {
                        PictureBox enemigo = (PictureBox)control;
                        if (imgBala.Bounds.IntersectsWith(enemigo.Bounds))
                        {
                            timerBala.Stop();
                            Form.ActiveForm.Controls.Remove(imgBala);
                            Form.ActiveForm.Controls.Remove(enemigo);
                            imgBala.Dispose();
                            enemigo.Dispose();
                            score++;
                            return;
                        }
                    }
                };

                if (imgBala.Top + imgBala.Height < 0)
                {
                    timerBala.Stop();
                    Form.ActiveForm.Controls.Remove(imgBala);
                    imgBala.Dispose();
                }
            };
            timerBala.Start();
        }

        public int EnemigosEliminados()
        {
            return score;
        }
    }
}
