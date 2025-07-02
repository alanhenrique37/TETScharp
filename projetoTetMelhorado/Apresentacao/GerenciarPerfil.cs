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

            // Evento de pintura do PictureBox
            pictureBoxPerfil.Paint += new PaintEventHandler(PictureBoxPerfil_Paint);
        }

        private void GerenciarPerfil_Load(object sender, EventArgs e)
        {
           
            CarregarFotoPerfil();
            CarregarDadosDoUsuario();

            // Botão imagem invisível (sem texto, sem borda)
            ConfigurarBotaoTransparente(button1);

            // Outros botões arredondados
            ArredondarBotao(btnSairDaConta);
         
            ArredondarBotao(btnMIP);
            ArredondarBotao(btnEditarPerfil);

            // Redimensiona imagem para se ajustar corretamente
            pictureBoxPerfil.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void btnSairDaConta_Click(object sender, EventArgs e)
        {
            SessaoUsuario.EmailLogado = null;
            Form1 login = new Form1();
            login.Show();
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

        private void pictureBoxPerfil_Click(object sender, EventArgs e) { }

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

                pictureBoxPerfil.Invalidate(); // Redesenha com borda redonda
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

        private void GerenciarPerfil_Paint(object sender, PaintEventArgs e)
        {
            Color corTopo = Color.FromArgb(32, 53, 98);
            Color corBaixo = Color.FromArgb(125, 130, 155);

            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, corTopo, corBaixo, 90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BemVindo bem = new BemVindo();
            bem.Show();
            this.Hide();
        }

        private void PictureBoxPerfil_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            GraphicsPath path = new GraphicsPath();
            Rectangle bounds = pictureBoxPerfil.ClientRectangle;
            path.AddEllipse(bounds);
            pictureBoxPerfil.Region = new Region(path);
        }

        // === MÉTODOS AUXILIARES ===

        private void ArredondarBotao(Button botao)
        {
            botao.FlatStyle = FlatStyle.Flat;
            botao.FlatAppearance.BorderSize = 0;
           
 

            botao.Region = new Region(new GraphicsPath()
            {
                FillMode = FillMode.Winding
            }.AddRoundedRectangle(botao.ClientRectangle, 10)); // raio 20
        }

        private void ConfigurarBotaoTransparente(Button botao)
        {
            botao.FlatStyle = FlatStyle.Flat;
            botao.FlatAppearance.BorderSize = 0;
            botao.FlatAppearance.MouseOverBackColor = Color.Transparent;
            botao.FlatAppearance.MouseDownBackColor = Color.Transparent;
            botao.BackColor = Color.Transparent;
            botao.TabStop = false;
            botao.Text = "";
            botao.Cursor = Cursors.Hand;
        }
    }

    // Extensão para criar cantos arredondados
    public static class GraphicsExtensions
    {
        public static GraphicsPath AddRoundedRectangle(this GraphicsPath path, Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);

            path.StartFigure();
            // Top left arc
            path.AddArc(arc, 180, 90);
            // Top right arc
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            // Bottom right arc
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            // Bottom left arc
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}
