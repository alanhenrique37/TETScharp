using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetoTetMelhorado.Apresentacao
{
    public partial class telaInicial : Form
    {
        public telaInicial()
        {
            InitializeComponent();

            // Ativa o evento Paint para desenhar o gradiente
            this.Paint += new PaintEventHandler(Form_Paint);
        }

        private void telaInicial_Load(object sender, EventArgs e)
        {
            // Chama a função para arredondar o botão
      
            ArredondarBotaoEntrar(button1, 20);
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            // Define as cores do gradiente (de cima para baixo)
            Color corTopo = Color.FromArgb(32, 53, 98);       // Azul escuro
            Color corBaixo = Color.FromArgb(125, 130, 155);   // Azul acinzentado claro

            // Cria o gradiente vertical
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, corTopo, corBaixo, 90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

      
        private void ArredondarBotaoEntrar(Button button1, int raio)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, raio, raio, 180, 90);
            path.AddArc(button1.Width - raio, 0, raio, raio, 270, 90);
            path.AddArc(button1.Width - raio, button1.Height - raio, raio, raio, 0, 90);
            path.AddArc(0, button1.Height - raio, raio, raio, 90, 90);
            path.CloseAllFigures();
            button1.Region = new Region(path);

            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 fo3 = new Form1();
            fo3.Show();
            this.Hide();  // Esconde o formulário atual ao invés de fechar
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

     
    }
}
