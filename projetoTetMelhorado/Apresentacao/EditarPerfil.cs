using MySql.Data.MySqlClient;
using projetoTetMelhorado.DAL;
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
    public partial class EditarPerfil : Form
    {
        public EditarPerfil()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(EditarPerfil_Paint);
        }

        private void EditarPerfil_Load(object sender, EventArgs e)
        {
            using (MySqlConnection con = new Conexao().conectar())
            {
                string query = "SELECT nome, telefone FROM logins WHERE email = @email";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@email", SessaoUsuario.EmailLogado);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        txtNovoTelefone.Text = reader["telefone"].ToString();
                    }
                }
            }

            // Configuração visual do button1 (imagem)
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button1.BackColor = Color.Transparent;
            button1.TabStop = false;
            button1.Cursor = Cursors.Hand;
            button1.Text = "";

            // Aumentar altura dos TextBoxes
            txtNovoNome.Height = 100;
            txtNovaSenha.Height = 100;
            txtConfirmarSenha.Height = 100;
            txtNovoTelefone.Height = 100;

            // Arredondar TextBoxes
            ArredondarTextBox(txtNovoNome);
            ArredondarTextBox(txtNovaSenha);
            ArredondarTextBox(txtConfirmarSenha);
            ArredondarTextBox(txtNovoTelefone);

            // Arredondar Botões com raios diferentes
            ArredondarBotao(btnSalvarNome, 10);
            ArredondarBotao(btnSalvarSenha, 10);
            ArredondarBotao(btnSalvarTelefone, 10);

            ArredondarBotao(btnSalvarTudo, 20);
            ArredondarBotao(btnCancelar, 20);
        }

        private void ArredondarBotao(Button botao, int radius)
        {
            botao.FlatStyle = FlatStyle.Flat;
            botao.FlatAppearance.BorderSize = 0;

            Rectangle bounds = new Rectangle(0, 0, botao.Width, botao.Height);
            GraphicsPath path = new GraphicsPath();

            path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
            path.AddArc(bounds.Right - radius, bounds.Y, radius, radius, 270, 90);
            path.AddArc(bounds.Right - radius, bounds.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - radius, radius, radius, 90, 90);
            path.CloseAllFigures();

            botao.Region = new Region(path);
        }

        private void ArredondarTextBox(TextBox txt)
        {
            txt.BorderStyle = BorderStyle.None;
            txt.BackColor = Color.White;
            txt.Multiline = false;

            Rectangle bounds = new Rectangle(0, 0, txt.Width, txt.Height);
            GraphicsPath path = new GraphicsPath();
            int radius = 12;

            path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
            path.AddArc(bounds.Right - radius, bounds.Y, radius, radius, 270, 90);
            path.AddArc(bounds.Right - radius, bounds.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - radius, radius, radius, 90, 90);
            path.CloseAllFigures();

            txt.Region = new Region(path);
        }

        private void EditarPerfil_Paint(object sender, PaintEventArgs e)
        {
            Color corTopo = Color.FromArgb(32, 53, 98);
            Color corBaixo = Color.FromArgb(125, 130, 155);

            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, corTopo, corBaixo, 90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void btnSalvarNome_Click(object sender, EventArgs e)
        {
            string novoNome = txtNovoNome.Text.Trim();

            if (string.IsNullOrEmpty(novoNome))
            {
                MessageBox.Show("Informe um novo nome.");
                return;
            }

            using (MySqlConnection con = new Conexao().conectar())
            {
                string query = "UPDATE logins SET nome = @nome WHERE email = @email";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@nome", novoNome);
                cmd.Parameters.AddWithValue("@email", SessaoUsuario.EmailLogado);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Nome atualizado com sucesso!");
                txtNovoNome.Clear();
            }
        }

        private void btnSalvarSenha_Click(object sender, EventArgs e)
        {
            string novaSenha = txtNovaSenha.Text.Trim();
            string confirmarSenha = txtConfirmarSenha.Text.Trim();

            if (string.IsNullOrEmpty(novaSenha))
            {
                MessageBox.Show("Digite a nova senha.");
                return;
            }

            if (novaSenha != confirmarSenha)
            {
                MessageBox.Show("As senhas não coincidem.");
                return;
            }

            using (MySqlConnection con = new Conexao().conectar())
            {
                string query = "UPDATE logins SET senha = @senha WHERE email = @email";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@senha", novaSenha);
                cmd.Parameters.AddWithValue("@email", SessaoUsuario.EmailLogado);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Senha atualizada com sucesso!");
                txtNovaSenha.Clear();
                txtConfirmarSenha.Clear();
            }
        }

        private void btnSalvarTelefone_Click(object sender, EventArgs e)
        {
            string telefoneFormatado = txtNovoTelefone.Text.Trim();
            string telefoneNumerico = new string(telefoneFormatado.Where(char.IsDigit).ToArray());

            if (telefoneNumerico.Length != 11)
            {
                MessageBox.Show("O telefone deve conter exatamente 11 números (incluindo o DDD).");
                return;
            }

            using (MySqlConnection con = new Conexao().conectar())
            {
                string query = "UPDATE logins SET telefone = @telefone WHERE email = @email";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@telefone", telefoneNumerico);
                cmd.Parameters.AddWithValue("@email", SessaoUsuario.EmailLogado);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Telefone atualizado com sucesso!");
            }
        }

        private void btnSalvarTudo_Click(object sender, EventArgs e)
        {
            string novoNome = txtNovoNome.Text.Trim();
            string novaSenha = txtNovaSenha.Text.Trim();
            string confirmarSenha = txtConfirmarSenha.Text.Trim();
            string novoTelefone = txtNovoTelefone.Text.Trim();

            string telefoneNumeros = new string(novoTelefone.Where(char.IsDigit).ToArray());

            if (string.IsNullOrEmpty(novoNome) && string.IsNullOrEmpty(novaSenha) && string.IsNullOrEmpty(telefoneNumeros))
            {
                MessageBox.Show("Nenhuma alteração informada.");
                return;
            }

            if (!string.IsNullOrEmpty(novaSenha) && novaSenha != confirmarSenha)
            {
                MessageBox.Show("As senhas não coincidem.");
                return;
            }

            if (!string.IsNullOrEmpty(novoTelefone) && telefoneNumeros.Length != 11)
            {
                MessageBox.Show("Telefone deve conter 11 dígitos numéricos.");
                return;
            }

            using (MySqlConnection con = new Conexao().conectar())
            {
                List<string> campos = new List<string>();
                if (!string.IsNullOrEmpty(novoNome)) campos.Add("nome = @nome");
                if (!string.IsNullOrEmpty(novaSenha)) campos.Add("senha = @senha");
                if (!string.IsNullOrEmpty(telefoneNumeros)) campos.Add("telefone = @telefone");

                string query = "UPDATE logins SET " + string.Join(", ", campos) + " WHERE email = @email";
                MySqlCommand cmd = new MySqlCommand(query, con);

                if (!string.IsNullOrEmpty(novoNome)) cmd.Parameters.AddWithValue("@nome", novoNome);
                if (!string.IsNullOrEmpty(novaSenha)) cmd.Parameters.AddWithValue("@senha", novaSenha);
                if (!string.IsNullOrEmpty(telefoneNumeros)) cmd.Parameters.AddWithValue("@telefone", telefoneNumeros);
                cmd.Parameters.AddWithValue("@email", SessaoUsuario.EmailLogado);

                int linhas = cmd.ExecuteNonQuery();

                if (linhas > 0)
                {
                    MessageBox.Show("Dados atualizados com sucesso!");

                    if (!string.IsNullOrEmpty(novoNome)) txtNovoNome.Clear();
                    if (!string.IsNullOrEmpty(novaSenha))
                    {
                        txtNovaSenha.Clear();
                        txtConfirmarSenha.Clear();
                    }
                    if (!string.IsNullOrEmpty(novoTelefone)) txtNovoTelefone.Clear();
                }
                else
                {
                    MessageBox.Show("Nenhuma alteração foi feita.");
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            GerenciarPerfil gerenciarPerfil = new GerenciarPerfil();
            gerenciarPerfil.Show();
            this.Close();
        }

        private void txtNovoTelefone_TextChanged(object sender, EventArgs e)
        {
            int pos = txtNovoTelefone.SelectionStart;
            string somenteNumeros = new string(txtNovoTelefone.Text.Where(char.IsDigit).ToArray());

            if (somenteNumeros.Length > 11)
                somenteNumeros = somenteNumeros.Substring(0, 11);

            string telefoneFormatado = "";

            if (somenteNumeros.Length <= 2)
                telefoneFormatado = "(" + somenteNumeros;
            else if (somenteNumeros.Length <= 7)
                telefoneFormatado = "(" + somenteNumeros.Substring(0, 2) + ")" + somenteNumeros.Substring(2);
            else
                telefoneFormatado = "(" + somenteNumeros.Substring(0, 2) + ")" +
                                    somenteNumeros.Substring(2, 5) + "-" +
                                    somenteNumeros.Substring(7);

            if (txtNovoTelefone.Text != telefoneFormatado)
            {
                txtNovoTelefone.Text = telefoneFormatado;
                txtNovoTelefone.SelectionStart = txtNovoTelefone.Text.Length;
            }
        }

        private void txtNovoNome_TextChanged(object sender, EventArgs e) { }
        private void txtNovaSenha_TextChanged(object sender, EventArgs e) { }
        private void txtConfirmarSenha_TextChanged(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e)
        {
            GerenciarPerfil ger = new GerenciarPerfil();
            ger.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
