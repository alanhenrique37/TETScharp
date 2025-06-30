using projetoTetMelhorado.Apresentacao;
using projetoTetMelhorado.DAL;
using projetoTetMelhorado.Modelo;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace projetoTetMelhorado
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Configuração visual do button1 (apenas imagem visível)
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button1.BackColor = Color.Transparent;
            button1.TabStop = false;
            button1.Cursor = Cursors.Hand;
            button1.Text = "";

            // Arredonda botões Entrar (button2)
            ArredondarBotao(button2, 20);

            // Botão cadastrar-se só texto (sem fundo, sem borda, sem efeito hover)
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
            button3.BackColor = Color.Transparent;
            button3.Cursor = Cursors.Hand;
            button3.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button3.FlatAppearance.MouseDownBackColor = Color.Transparent;

            // Eventos para manter fundo transparente sempre
            button3.MouseEnter += (s, ev) => button3.BackColor = Color.Transparent;
            button3.MouseLeave += (s, ev) => button3.BackColor = Color.Transparent;

            // Arredonda TextBoxes com altura maior
            ArredondarTextBox(textBox3, 10, 35);
            ArredondarTextBox(textBox4, 10, 35);

            // Configura placeholders
            ConfigurarPlaceholder(textBox3, "Digite seu e-mail");
            ConfigurarPlaceholder(textBox4, "Digite sua senha");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CadastreSe cad = new CadastreSe();
            cad.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            telaInicial tel = new telaInicial();
            tel.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text) || textBox3.ForeColor == Color.Gray ||
                string.IsNullOrWhiteSpace(textBox4.Text) || textBox4.ForeColor == Color.Gray)
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            button2.Enabled = false;
            try
            {
                Controle controle = new Controle();
                controle.acessar(textBox3.Text, textBox4.Text);

                if (string.IsNullOrEmpty(controle.mensagem))
                {
                    if (controle.tem)
                    {
                        MessageBox.Show("Logado com sucesso", "Entrando", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        SessaoUsuario.EmailLogado = textBox3.Text;

                        BemVindo bemVindo = new BemVindo();
                        bemVindo.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Login não encontrado, verifique login e senha", "ERRO!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox4.Clear();
                        textBox4.Focus();
                    }
                }
                else
                {
                    MessageBox.Show(controle.mensagem, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                button2.Enabled = true;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e) { }

        private void textBox4_TextChanged(object sender, EventArgs e) { }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color corTopo = Color.FromArgb(32, 53, 98);
            Color corBaixo = Color.FromArgb(125, 130, 155);

            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, corTopo, corBaixo, 90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
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
            botao.BackColor = Color.White;
            botao.ForeColor = Color.Black;
        }

        private void ArredondarTextBox(TextBox textbox, int raio, int novaAltura)
        {
            textbox.BorderStyle = BorderStyle.None;
            textbox.Multiline = true;

            Panel painel = new Panel();
            painel.Size = new Size(textbox.Width, novaAltura);
            painel.Location = textbox.Location;
            painel.BackColor = Color.White;
            painel.Padding = new Padding(1);

            this.Controls.Add(painel);
            painel.BringToFront();

            textbox.Parent = painel;
            textbox.Location = new Point(5, 7);
            textbox.Width = painel.Width - 10;
            textbox.Height = novaAltura - 14;

            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, raio, raio, 180, 90);
            path.AddArc(painel.Width - raio, 0, raio, raio, 270, 90);
            path.AddArc(painel.Width - raio, painel.Height - raio, raio, raio, 0, 90);
            path.AddArc(0, painel.Height - raio, raio, raio, 90, 90);
            path.CloseFigure();

            painel.Region = new Region(path);
        }

        private void ConfigurarPlaceholder(TextBox textbox, string placeholder)
        {
            textbox.Text = placeholder;
            textbox.ForeColor = Color.Gray;

            textbox.GotFocus += (s, ev) =>
            {
                if (textbox.Text == placeholder)
                {
                    textbox.Text = "";
                    textbox.ForeColor = Color.Black;
                }
            };

            textbox.LostFocus += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(textbox.Text))
                {
                    textbox.Text = placeholder;
                    textbox.ForeColor = Color.Gray;
                }
            };
        }
    }
}
