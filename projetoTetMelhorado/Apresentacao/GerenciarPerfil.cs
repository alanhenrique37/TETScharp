using projetoTetMelhorado.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace projetoTetMelhorado.Apresentacao
{
    public partial class GerenciarPerfil : Form
    {
        public GerenciarPerfil()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(GerenciarPerfil_Paint); // <- GRADIENTE ADICIONADO
        }

        private void GerenciarPerfil_Load(object sender, EventArgs e)
        {
            lblEmail.Text = SessaoUsuario.EmailLogado;
            CarregarFotoPerfil();
            CarregarDadosDoUsuario();
        }

        private void btnSairDaConta_Click(object sender, EventArgs e)
        {
            SessaoUsuario.EmailLogado = null;
            Form1 login = new Form1();
            login.Show();
            this.Close();
        }

        private void btnVoltaBV_Click(object sender, EventArgs e)
        {
            BemVindo bemVindo = new BemVindo();
            bemVindo.Show();
            this.Close();
        }

        private void btnMIP_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Imagens (*.jpg;*.png)|*.jpg;*.png";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                byte[] novaImagem = File.ReadAllBytes(ofd.FileName);
                LoginDaoComandos dao = new LoginDaoComandos();
                string resultado = dao.AtualizarFotoPerfil(SessaoUsuario.EmailLogado, novaImagem);
                MessageBox.Show(resultado);
                CarregarFotoPerfil();
            }
        }

        private void pictureBoxPerfil_Click(object sender, EventArgs e)
        {
        }

        private void CarregarFotoPerfil()
        {
            using (MySqlConnection con = new Conexao().conectar())
            {
                MySqlCommand cmd = new MySqlCommand("SELECT foto_perfil FROM logins WHERE email = @email", con);
                cmd.Parameters.AddWithValue("@email", SessaoUsuario.EmailLogado);
                var resultado = cmd.ExecuteScalar();

                if (resultado != DBNull.Value && resultado != null)
                {
                    byte[] imagemBytes = (byte[])resultado;
                    using (MemoryStream ms = new MemoryStream(imagemBytes))
                    {
                        pictureBoxPerfil.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    pictureBoxPerfil.Image = Properties.Resources.avatar_padrao;
                }
            }
        }

        private void CarregarDadosDoUsuario()
        {
            using (MySqlConnection con = new Conexao().conectar())
            {
                string query = "SELECT nome, senha FROM logins WHERE email = @email";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@email", SessaoUsuario.EmailLogado);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string nome = reader["nome"].ToString();
                        string senha = reader["senha"].ToString();
                        lblNome.Text = nome;
                        lblSenha.Text = new string('*', senha.Length);
                    }
                }
            }
        }

        private void btnEditarPerfil_Click(object sender, EventArgs e)
        {
            EditarPerfil editarPerfil = new EditarPerfil();
            editarPerfil.Show();
            this.Close();
        }

        // === GRADIENTE ADICIONADO ===
        private void GerenciarPerfil_Paint(object sender, PaintEventArgs e)
        {
            Color corTopo = Color.FromArgb(32, 53, 98);
            Color corBaixo = Color.FromArgb(125, 130, 155);

            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, corTopo, corBaixo, 90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }
    }
}
