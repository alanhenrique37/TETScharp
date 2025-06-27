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
            ArredondarBotao(cadastrar, 20);
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            // Define as cores do gradiente (de cima para baixo)
            Color corTopo = Color.FromArgb(15, 32, 85);       // Azul escuro
            Color corBaixo = Color.FromArgb(125, 130, 155);   // Azul acinzentado claro

            // Cria o gradiente vertical
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, corTopo, corBaixo, 90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void cadastrar_Click(object sender, EventArgs e)
        {
            // Ação do botão cadastrar
            MessageBox.Show("Cadastro clicado!");
        }

        private void ArredondarBotao(Button botao, int raio)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, raio, raio, 180, 90);
            path.AddArc(botao.Width - raio, 0, raio, raio, 270, 90);
            path.AddArc(botao.Width - raio, botao.Height - raio, raio, raio, 0, 90);
            path.AddArc(0, botao.Height - raio, raio, raio, 90, 90);
            path.CloseAllFigures();
            botao.Region = new Region(path);

            botao.FlatStyle = FlatStyle.Flat;
            botao.FlatAppearance.BorderSize = 0;
            botao.BackColor = Color.DarkBlue; // Pode trocar a cor
            botao.ForeColor = Color.White;
            botao.Font = new Font("Segoe UI", 10, FontStyle.Bold); // Visual moderno
        }
    }
}
