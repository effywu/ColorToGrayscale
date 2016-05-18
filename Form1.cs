using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace FirstWindowsProject
{
    public partial class Form1 : Form
    {
        int count = 0;
        string textholder;
        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        Bitmap greyimg;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private static Bitmap ToGrayscale(Bitmap original)
        {
            Bitmap greybitmap = new Bitmap(original.Width, original.Height);
            Graphics gr = Graphics.FromImage(greybitmap);
            ColorMatrix colorMatrix = new ColorMatrix(
                new float[][]
                {
                 new float[] {.3f, .3f, .3f, 0, 0},
                 new float[] {.59f, .59f, .59f, 0, 0},
                 new float[] {.11f, .11f, .11f, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {0, 0, 0, 0, 1}
                });
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);
            gr.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            gr.Dispose();
            return greybitmap;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect=false;
            openFileDialog1.Filter="jpg files|*.jpg";
            openFileDialog1.FilterIndex=0;
            openFileDialog1.DefaultExt=".jpg";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap img = new Bitmap(openFileDialog1.FileName);
                greyimg = ToGrayscale(img);
                img.Dispose();
                int ogWidth = greyimg.Width;
                int ogHeight = greyimg.Height;

                float ratioX = (float)475 / (float)ogWidth;
                float ratioY = (float)192 / (float)ogHeight;
                float ratio = Math.Min(ratioX, ratioY);

                int newWidth = (int)(ogWidth * ratio);
                int newHeight = (int)(ogHeight * ratio);

                Bitmap newImage = new Bitmap(greyimg, newWidth, newHeight);
                pb.SizeMode = PictureBoxSizeMode.CenterImage;
                pb.Image = newImage;

            };
            /*
            count = 0;
            timer1.Tick += new EventHandler(printOneByOne);
            timer1.Interval = 200;
            textholder = "...Sure";
            timer1.Start();
            */
            
            
        }
        private void printOneByOne(Object o, EventArgs e)
        {
            if (count < textholder.Length)
            {
                label1.Text += textholder[count];
                count++;
            }
            else
            {
                timer1.Stop();
                label1.Text = "";
                Random rnd = new Random();
                int randomX = rnd.Next(1, this.ClientSize.Width - 183);
                int randomY = rnd.Next(1, this.ClientSize.Height - 32);
                this.button1.Text = randomX.ToString() + " , " + randomY.ToString();
                //this.textBox1.SetBounds(randomX, randomY, 183, 32);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPG Files|*.jpg";
            saveFileDialog1.FilterIndex = 0;
            saveFileDialog1.DefaultExt = ".jpg";
            saveFileDialog1.OverwritePrompt = false;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                greyimg.Save(saveFileDialog1.FileName);
            }
        }

    }
}
