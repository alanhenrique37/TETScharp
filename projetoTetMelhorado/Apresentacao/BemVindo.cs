using MySql.Data.MySqlClient;
using projetoTetMelhorado.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using projetoTetMelhorado.Apresentacao;
using Mysqlx.Crud;

namespace projetoTetMelhorado.Apresentacao
{
    public partial class BemVindo : Form
    {
        public BemVindo()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(BemVindo_Paint); // ADICIONADO GRADIENTE
        }

        private void BemVindo_Load(object sender, EventArgs e)
        {
            CarregarImagemUsuario();


            ArredondarTextBox(textBox1, 100);
            pictureBoxUsuario.ContextMenuStrip = contextMenuUsuario;
            pictureBoxUsuario.MouseClick += pictureBoxUsuario_MouseClick;
        }

       

        private void ArredondarTextBox(TextBox textBox1, int raio)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, raio, raio, 180, 90);
            path.AddArc(textBox1.Width - raio, 0, raio, raio, 270, 90);
            path.AddArc(textBox1.Width - raio, textBox1.Height - raio, raio, raio, 0, 90);
            path.AddArc(0, textBox1.Height - raio, raio, raio, 90, 90);
            path.CloseAllFigures();
            textBox1.Region = new Region(path);
        } // Fim

      

        private void CarregarUsuarios()
        {
            flowLayoutPanelProjetos.Controls.Clear();

            try
            {
                using (MySqlConnection con = new Conexao().conectar())
                {
                    string query = "SELECT nome, email, telefone, foto_perfil, tipo FROM logins";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string nome = reader["nome"].ToString();
                        string email = reader["email"].ToString();
                        string telefone = reader["telefone"].ToString();
                        string tipo = reader["tipo"].ToString();
                        byte[] foto = reader["foto_perfil"] != DBNull.Value ? (byte[])reader["foto_perfil"] : null;

                        flowLayoutPanelProjetos.Controls.Add(CriarCardUsuario(nome, email, telefone, foto, tipo));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar usuários: " + ex.Message);
            }
        }

        private Panel CriarCardUsuario(string nome, string email, string telefone, byte[] foto, string tipo)
        {
            Panel card = new Panel();
            card.Size = new Size(flowLayoutPanelProjetos.Width - 30, 100);
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Margin = new Padding(10);

            PictureBox pic = new PictureBox();
            pic.Size = new Size(60, 60);
            pic.Location = new Point(10, 10);
            pic.SizeMode = PictureBoxSizeMode.Zoom;

            pic.Image = foto != null ? Image.FromStream(new MemoryStream(foto)) : Properties.Resources.avatar_padrao;

            Label lblNome = new Label();
            lblNome.Text = tipo == "admin" ? $"{nome} (ADM)" : nome;
            lblNome.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblNome.Location = new Point(80, 10);
            lblNome.AutoSize = true;

            Label lblEmail = new Label();
            lblEmail.Text = "Email: " + email;
            lblEmail.Location = new Point(80, 35);
            lblEmail.AutoSize = true;

            Label lblTel = new Label();
            lblTel.Text = "Tel: " + telefone;
            lblTel.Location = new Point(80, 55);
            lblTel.AutoSize = true;

            Button btnVerPosts = new Button();
            btnVerPosts.Text = "Ver Posts";
            btnVerPosts.Size = new Size(90, 30);
            btnVerPosts.Location = new Point(card.Width - 210, 30);
            btnVerPosts.Click += (s, e) =>
            {
                PostsDoUsuario tela = new PostsDoUsuario(email);
                tela.ShowDialog();
            };

            Button btnExcluir = new Button();
            btnExcluir.Text = "Excluir";
            btnExcluir.Size = new Size(90, 30);
            btnExcluir.Location = new Point(card.Width - 110, 30);
            btnExcluir.BackColor = Color.Red;
            btnExcluir.ForeColor = Color.White;
            btnExcluir.Click += (s, e) =>
            {
                if (MessageBox.Show("Tem certeza que deseja excluir este usuário?", "Confirmação",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    ExcluirUsuario(email);
                    CarregarUsuarios();
                }
            };

            card.Controls.Add(pic);
            card.Controls.Add(lblNome);
            card.Controls.Add(lblEmail);
            card.Controls.Add(lblTel);
            card.Controls.Add(btnVerPosts);
            card.Controls.Add(btnExcluir);

            return card;
        }

        private Panel CriarCardUsuario(string nome, string email, string telefone, byte[] foto)
        {
            return CriarCardUsuario(nome, email, telefone, foto, "usuario");
        }

        private void ExcluirUsuario(string email)
        {
            try
            {
                using (MySqlConnection con = new Conexao().conectar())
                {
                    string query = "DELETE FROM logins WHERE email = @Email";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Email", email);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    MessageBox.Show(rowsAffected > 0 ? "Usuário excluído com sucesso!" : "Usuário não encontrado ou erro ao excluir.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir usuário: " + ex.Message);
            }
        }

        private void AdicionarProjetoAoFeed(string nome, string descricao, decimal valor, DateTime data,
            string nomeAutor, byte[] fotoAutor, string emailAutor, string telefoneAutor, int qtdPessoas)
        {
            Panel projetoPanel = new Panel();
            projetoPanel.Width = flowLayoutPanelProjetos.Width - 30;
            projetoPanel.Height = 180;
            projetoPanel.BorderStyle = BorderStyle.FixedSingle;
            projetoPanel.Margin = new Padding(10);

            PictureBox picAutor = new PictureBox();
            picAutor.Size = new Size(50, 50);
            picAutor.Location = new Point(10, 10);
            picAutor.SizeMode = PictureBoxSizeMode.Zoom;
            picAutor.Image = fotoAutor != null ? Image.FromStream(new MemoryStream(fotoAutor)) : null;

            Label lblAutor = new Label();
            lblAutor.Text = "Por: " + nomeAutor;
            lblAutor.Location = new Point(70, 25);
            lblAutor.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            lblAutor.AutoSize = true;

            Label lblNome = new Label();
            lblNome.Text = nome;
            lblNome.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblNome.Location = new Point(10, 70);
            lblNome.AutoSize = true;

            Label lblDescricao = new Label();
            lblDescricao.Text = descricao;
            lblDescricao.Location = new Point(10, 95);
            lblDescricao.Width = projetoPanel.Width - 20;
            lblDescricao.Height = 40;
            lblDescricao.AutoSize = false;

            Label lblValor = new Label();
            lblValor.Text = "Valor: R$ " + valor.ToString("N2");
            lblValor.Location = new Point(10, 140);
            lblValor.AutoSize = true;

            Label lblQtdPessoas = new Label();
            lblQtdPessoas.Text = "Equipe: " + qtdPessoas + " pessoa(s)";
            lblQtdPessoas.Location = new Point(120, 140);
            lblQtdPessoas.AutoSize = true;

            Label lblData = new Label();
            lblData.Text = "Criado em: " + data.ToString("dd/MM/yyyy HH:mm");
            lblData.Location = new Point(250, 140);
            lblData.AutoSize = true;

            Button btnContato = new Button();
            btnContato.Text = "Contato";
            btnContato.Size = new Size(80, 30);
            btnContato.Location = new Point(projetoPanel.Width - 100, 10);
            btnContato.Click += (s, e) =>
            {
                ContatoPerfil formContato = new ContatoPerfil(nomeAutor, emailAutor, telefoneAutor);
                formContato.ShowDialog();
            };

            projetoPanel.Controls.Add(picAutor);
            projetoPanel.Controls.Add(lblAutor);
            projetoPanel.Controls.Add(lblNome);
            projetoPanel.Controls.Add(lblDescricao);
            projetoPanel.Controls.Add(lblValor);
            projetoPanel.Controls.Add(lblQtdPessoas);
            projetoPanel.Controls.Add(lblData);
            projetoPanel.Controls.Add(btnContato);

            flowLayoutPanelProjetos.Controls.Add(projetoPanel);
        }

        private void btnGerenciarPerfil_Click(object sender, EventArgs e)
        {
            new GerenciarPerfil().Show();
            this.Close();
        }

        private void BemVindo_Load_1(object sender, EventArgs e)
        {
            CarregarUsuarios();
            CarregarImagemUsuario();
            pictureBoxUsuario.ContextMenuStrip = contextMenuUsuario;
            pictureBoxUsuario.MouseClick += pictureBoxUsuario_MouseClick;
        }

        private void CarregarImagemUsuario()
        {
            try
            {
                using (MySqlConnection con = new Conexao().conectar())
                {
                    string query = "SELECT foto_perfil FROM logins WHERE email = @Email";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Email", SessaoUsuario.EmailLogado);

                    var resultado = cmd.ExecuteScalar();
                    if (resultado != null && resultado != DBNull.Value)
                    {
                        byte[] imagemBytes = (byte[])resultado;
                        using (MemoryStream ms = new MemoryStream(imagemBytes))
                        {
                            pictureBoxUsuario.Image = Image.FromStream(ms);
                        }
                    }
                    else
                    {
                        pictureBoxUsuario.Image = Properties.Resources.avatar_padrao;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar imagem do usuário: " + ex.Message);
                pictureBoxUsuario.Image = Properties.Resources.avatar_padrao;
            }
        }

        private void pictureBoxUsuario_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                contextMenuUsuario.Show(pictureBoxUsuario, new Point(e.X, e.Y));
            }
        }

        private void gerenciarPerfilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new GerenciarPerfil().Show();
            this.Close();
        }

        private void sairDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SessaoUsuario.EmailLogado = null;
            new Form1().Show();
            this.Close();
        }

        // === MÉTODO ADICIONADO: Pinta o fundo com gradiente ===
        private void BemVindo_Paint(object sender, PaintEventArgs e)
        {
            Color corTopo = Color.FromArgb(32, 53, 98);
            Color corBaixo = Color.FromArgb(125, 130, 155);

            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, corTopo, corBaixo, 90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void pictureBoxUsuario_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string termo = textBox1.Text.Trim().ToLower();

            foreach (Control ctrl in flowLayoutPanelProjetos.Controls)
            {
                if (ctrl is Panel panel)
                {
                    Label lblNome = panel.Controls
                        .OfType<Label>()
                        .FirstOrDefault(lbl => lbl.Font.Bold);

                    if (lblNome != null)
                    {
                        string nomeUsuario = lblNome.Text.Trim().ToLower();
                        panel.Visible = nomeUsuario.Contains(termo);
                    }
                }
            }
        }

        
    }
}