using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryLautaroQuiligotti
{
    public partial class frmFirma : Form
    {
        private Bitmap ArchivoImagen;
        private string carpetaFirmas;

        public frmFirma()
        {
            InitializeComponent();
            ArchivoImagen = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            carpetaFirmas = "firmas";
            if (!Directory.Exists(carpetaFirmas))
            {
                Directory.CreateDirectory(carpetaFirmas);
            }
        }

        private void frmFirma_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                using (Graphics objetoLoco = Graphics.FromImage(ArchivoImagen))
                {
                    objetoLoco.FillEllipse(Brushes.Black, e.X, e.Y, 5, 5);
                }
                pictureBox1.Image = ArchivoImagen;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ArchivoImagen = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = ArchivoImagen;
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            string FechaActual = DateTime.Now.ToString("(yyyy.MM.dd) HH.mm");
            string nombreArchivo = $"{FechaActual}.png"; // Cambiado a PNG
            string Guardar = Path.Combine(carpetaFirmas, nombreArchivo);

            // Guardar como PNG
            ArchivoImagen.Save(Guardar, System.Drawing.Imaging.ImageFormat.Png);
            MessageBox.Show("FIRMA GUARDADA CON EXITO");
        }
    }
}
