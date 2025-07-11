using projetoTetMelhorado.Modelo;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace projetoTetMelhorado.Apresentacao
{
    public partial class CadastreSe : Form
    {
        public CadastreSe()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(CadastreSe_Paint);
        }

        private void CadastreSe_Load(object sender, EventArgs e)
        {
            ArredondarTextBox(textBoxNome, 10, 35);
            ArredondarTextBox(textBox1, 10, 35);
            ArredondarTextBox(textBox2, 10, 35);
            ArredondarTextBox(textBox5, 10, 35);
            ArredondarTextBox(txbTelefone, 10, 35);

            ConfigurarPlaceholder(textBoxNome, "Digite seu nome");
            ConfigurarPlaceholder(textBox1, "Digite seu e-mail");
            ConfigurarPlaceholder(textBox2, "Digite sua senha");
            ConfigurarPlaceholder(textBox5, "Confirme sua senha");
            ConfigurarPlaceholder(txbTelefone, "Digite seu telefone");

            EstilizarBotao(btnCadastrar);

            // Configuração visual do button1 (apenas imagem visível)
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button1.BackColor = Color.Transparent;
            button1.TabStop = false;
            button1.Cursor = Cursors.Hand;
            button1.Text = "";


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
        }

        private bool CamposPreenchidos()
        {
            if (string.IsNullOrWhiteSpace(textBoxNome.Text) ||
                string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(txbTelefone.Text))
            {
                return false;
            }

            if (!EmailValido(textBox1.Text))
            {
                MessageBox.Show("E-mail inválido. Por favor, insira um e-mail válido.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool EmailValido(string email)
        {
            string padraoEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, padraoEmail);
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (!CamposPreenchidos())
            {
                MessageBox.Show("Por favor, preencha todos os campos!", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Controle controle = new Controle();
            string mensagem = controle.cadastrar(
                textBoxNome.Text,
                textBox1.Text,
                textBox2.Text,
                textBox5.Text,
                txbTelefone.Text
            );

            if (controle.tem)
            {
                MessageBox.Show(mensagem, "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1 fot = new Form1();
                fot.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show(controle.mensagem, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txbTelefone_TextChanged(object sender, EventArgs e)
        {
            int pos = txbTelefone.SelectionStart;
            string somenteNumeros = new string(txbTelefone.Text.Where(char.IsDigit).ToArray());

            if (somenteNumeros.Length > 11)
                somenteNumeros = somenteNumeros.Substring(0, 11);

            string telefoneFormatado = "";

            if (somenteNumeros.Length <= 2)
                telefoneFormatado = "(" + somenteNumeros;
            else if (somenteNumeros.Length <= 6)
                telefoneFormatado = $"({somenteNumeros.Substring(0, 2)}) {somenteNumeros.Substring(2)}";
            else if (somenteNumeros.Length <= 10)
                telefoneFormatado = $"({somenteNumeros.Substring(0, 2)}) {somenteNumeros.Substring(2, 4)}-{somenteNumeros.Substring(6)}";
            else
                telefoneFormatado = $"({somenteNumeros.Substring(0, 2)}) {somenteNumeros.Substring(2, 5)}-{somenteNumeros.Substring(7)}";

            txbTelefone.TextChanged -= txbTelefone_TextChanged;
            txbTelefone.Text = telefoneFormatado;
            txbTelefone.SelectionStart = telefoneFormatado.Length;
            txbTelefone.TextChanged += txbTelefone_TextChanged;
        }

        private void CadastreSe_Paint(object sender, PaintEventArgs e)
        {
            Color corTopo = Color.FromArgb(32, 53, 98);
            Color corBaixo = Color.FromArgb(125, 130, 155);

            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, corTopo, corBaixo, 90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
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

        private void EstilizarBotao(Button botao)
        {
            botao.FlatStyle = FlatStyle.Flat;
            botao.FlatAppearance.BorderSize = 0;
            botao.BackColor = Color.White;
            botao.ForeColor = Color.Black;

            int raio = 20;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, raio, raio, 180, 90);
            path.AddArc(botao.Width - raio, 0, raio, raio, 270, 90);
            path.AddArc(botao.Width - raio, botao.Height - raio, raio, raio, 0, 90);
            path.AddArc(0, botao.Height - raio, raio, raio, 90, 90);
            path.CloseAllFigures();

            botao.Region = new Region(path);
        }

        // Eventos vazios
        private void textBoxNome_TextChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox5_TextChanged(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 fot = new Form1();
            fot.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 fot = new Form1();
            fot.Show();
            this.Hide();
        }
    }
}
