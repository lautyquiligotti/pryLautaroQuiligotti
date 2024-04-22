using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Permissions;

namespace pryLautaroQuiligotti
{
    internal class clsNave
    {
        public int vida;
        public string nombre;
        int puntosdaño;
        public PictureBox imgNave;
        public PictureBox imgEnemigo1;
        public PictureBox imgEnemigo2;
        public PictureBox imgEnemigo3;
        public PictureBox imgBala;

        //Nuevo
        public List<PictureBox> enemigos = new List<PictureBox>(); 
        Random random = new Random();
        //Nuevo


        public void CrearJugador()
        {
            vida = 100;
            nombre = "Jugador1";
            puntosdaño = 1;

            imgNave = new PictureBox();
            imgNave.SizeMode = PictureBoxSizeMode.StretchImage;
            imgNave.Image = Image.FromFile(@"C:\Users\lautaroquiligotti\source\repos\pryLautaroQuiligotti\bin\Debug\imgNave.png");
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
                        enemigo.Image = Image.FromFile(@"C:\Users\lautaroquiligotti\source\repos\pryLautaroQuiligotti\bin\Debug\enemigoGalaga1.jpg");
                        break;
                    case 2:
                        enemigo.Image = Image.FromFile(@"C:\Users\lautaroquiligotti\source\repos\pryLautaroQuiligotti\bin\Debug\enemigoGalaga2.jpg");
                        break;
                    case 3:
                        enemigo.Image = Image.FromFile(@"C:\Users\lautaroquiligotti\source\repos\pryLautaroQuiligotti\bin\Debug\enemigoGalaga3.jpg");
                        break;
                    default:
                        break;
                }

                // Generar posiciones aleatorias
                int posX = random.Next(0, 720); // 800 - 80 para evitar que los enemigos se salgan del formulario
                int posY = random.Next(0, 520); // 600 - 80

                enemigo.Location = new Point(posX, posY);
                enemigo.Tag = "Enemigo";

                enemigos.Add(enemigo);
            }
        }

        public void Disparo()
        {
            imgBala = new PictureBox();
            imgBala.SizeMode = PictureBoxSizeMode.StretchImage;
            imgBala.Image = Image.FromFile(@"C:\Users\lautaroquiligotti\source\repos\pryLautaroQuiligotti\bin\Debug\bala.jpg");
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
    }
}
