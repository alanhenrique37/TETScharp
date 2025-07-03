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
using System.Runtime.InteropServices;

namespace projetoTetMelhorado.Apresentacao
{
    public partial class BemVindo : Form
    {
        const int WS_VSCROLL = 0x00200000;
        const int WS_HSCROLL = 0x00100000;
        const int GWL_STYLE = -16;

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private void HideScrollbars(Control ctrl)
        {
            int style = GetWindowLong(ctrl.Handle, GWL_STYLE);
            style &= ~WS_VSCROLL; // remove barra vertical
            style &= ~WS_HSCROLL; // remove barra horizontal
            SetWindowLong(ctrl.Handle, GWL_STYLE, style);
            ctrl.Refresh();
        }




        public BemVindo()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(BemVindo_Paint);
            this.Load += BemVindo_Load;
            textBox1.Resize += textBox1_Resize;
            pictureBoxUsuario.Resize += pictureBoxUsuario_Resize;
        }

        private void BemVindo_Load(object sender, EventArgs e)
        {
            // Seu código atual aqui...
            CarregarImagemUsuario();
            ArredondarBordasForm(this, 20);
            ArredondarTextBox(textBox1, 10);
            ArredondarPictureBox(pictureBoxUsuario);

            pictureBoxUsuario.ContextMenuStrip = contextMenuUsuario;
            pictureBoxUsuario.MouseClick += pictureBoxUsuario_MouseClick;

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    ArredondarBotao(btn);
                }
            }

            // Chama aqui pra esconder as barras de scroll
            HideScrollbars(flowLayoutPanelProjetos);
        }

        private void ArredondarBordasForm(Form form, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(form.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(form.Width - radius, form.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, form.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            form.Region = new Region(path);
        }

        private void textBox1_Resize(object sender, EventArgs e)
        {
            ArredondarTextBox(textBox1, 10);
        }

        private void pictureBoxUsuario_Resize(object sender, EventArgs e)
        {
            ArredondarPictureBox(pictureBoxUsuario);
        }

        private void ArredondarTextBox(TextBox textBox, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(textBox.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(textBox.Width - radius, textBox.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, textBox.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            textBox.Region = new Region(path);
        }

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
            card.Size = new Size(flowLayoutPanelProjetos.Width - 30, 160);
            card.BorderStyle = BorderStyle.None;
            card.Margin = new Padding(10);
            card.BackColor = Color.White;
            ArredondarBordasPanel(card, 15);

            // FOTO
            PictureBox pic = new PictureBox();
            pic.Size = new Size(70, 70);
            pic.Location = new Point(20, (card.Height - 70 - 30 - 5) / 2);
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            pic.Image = foto != null ? Image.FromStream(new MemoryStream(foto)) : Properties.Resources.avatar_padrao;
            ArredondarPictureBox(pic);

            // NOME
            Label lblNome = new Label();
            lblNome.Text = tipo == "admin" ? $"{nome} (ADM)" : nome;
            lblNome.Font = new Font("Poppins", 12, FontStyle.Bold);
            lblNome.AutoSize = false;
            lblNome.TextAlign = ContentAlignment.MiddleCenter;
            lblNome.Size = new Size(pic.Width + 10, 30);
            lblNome.Location = new Point(pic.Left - 5, pic.Bottom + 5);

            // BOTÃO EXCLUIR
            Button btnExcluir = new Button();
            btnExcluir.Text = "Excluir Usuário";
            btnExcluir.Size = new Size(140, 35);
            btnExcluir.BackColor = Color.FromArgb(231, 76, 60);
            btnExcluir.ForeColor = Color.White;
            btnExcluir.FlatStyle = FlatStyle.Flat;
            btnExcluir.FlatAppearance.BorderSize = 0;
            btnExcluir.Font = new Font("Poppins", 9, FontStyle.Bold);
            btnExcluir.Location = new Point(card.Width - btnExcluir.Width - 20, (card.Height - btnExcluir.Height) / 2 + 25);
            btnExcluir.Click += (s, e) =>
            {
                if (MessageBox.Show("Tem certeza que deseja excluir este usuário?", "Confirmação",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    ExcluirUsuario(email);
                    CarregarUsuarios();
                }
            };
            ArredondarBotao(btnExcluir, 5);

            // BOTÃO VER POSTS (acima do botão Excluir)
            Button btnVerPosts = new Button();
            btnVerPosts.Text = "Ver Posts";
            btnVerPosts.Size = new Size(140, 35);
            btnVerPosts.BackColor = Color.FromArgb(32, 53, 98); // #203562
            btnVerPosts.ForeColor = Color.White;
            btnVerPosts.FlatStyle = FlatStyle.Flat;
            btnVerPosts.FlatAppearance.BorderSize = 0;
            btnVerPosts.Font = new Font("Poppins", 9, FontStyle.Bold);
            btnVerPosts.Location = new Point(card.Width - btnVerPosts.Width - 20, (card.Height - btnVerPosts.Height) / 2 - 25);
            btnVerPosts.Click += (s, e) =>
            {
                // Abre a tela PostsDoUsuario passando o email
                PostsDoUsuario postsForm = new PostsDoUsuario(email);
                postsForm.ShowDialog();
            };
            ArredondarBotao(btnVerPosts, 5);

            int spacingY = 5;
            int spacingIconText = 6;
            int iconSizeEmail = 28;
            int iconSizeTel = 22; // Ícone do telefone menor

            // Espaço horizontal entre foto e botão
            int espaçoEsquerda = pic.Right + 10;   // 10 px depois da foto
            int espaçoDireita = btnVerPosts.Left - 10; // 10 px antes do botão Ver Posts
            int larguraDisponivel = espaçoDireita - espaçoEsquerda;

            // PAINEL CONTAINER PARA EMAIL E TELEFONE (vertical)
            Panel painelInfo = new Panel();
            painelInfo.BackColor = Color.Transparent;
            painelInfo.AutoSize = true;

            // EMAIL TÍTULO
            Label lblEmailTitulo = new Label();
            lblEmailTitulo.Text = "Email";
            lblEmailTitulo.Font = new Font("Poppins", 11, FontStyle.Bold);
            lblEmailTitulo.AutoSize = true;
            lblEmailTitulo.TextAlign = ContentAlignment.MiddleCenter;

            // PAINEL EMAIL ÍCONE + TEXTO
            Panel painelEmailInfo = new Panel();
            painelEmailInfo.AutoSize = true;
            painelEmailInfo.Padding = new Padding(0, spacingY, 0, spacingY);

            PictureBox picEmail = new PictureBox();
            picEmail.Size = new Size(iconSizeEmail, iconSizeEmail);
            picEmail.Image = Properties.Resources.email_icone;
            picEmail.SizeMode = PictureBoxSizeMode.Zoom;
            picEmail.Location = new Point(0, 0);

            Label lblEmail = new Label();
            lblEmail.Text = email;
            lblEmail.Font = new Font("Poppins", 11);
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(picEmail.Right + spacingIconText, (iconSizeEmail - lblEmail.Height) / 2);

            painelEmailInfo.Controls.AddRange(new Control[] { picEmail, lblEmail });
            painelEmailInfo.Size = new Size(lblEmail.Right, iconSizeEmail);

            // Definindo a largura do bloco email para centralização do título e do conteúdo
            int larguraBlocoEmail = Math.Max(lblEmailTitulo.Width, painelEmailInfo.Width);
            lblEmailTitulo.Width = larguraBlocoEmail;
            lblEmailTitulo.Location = new Point(0, 0);
            painelEmailInfo.Location = new Point((larguraBlocoEmail - painelEmailInfo.Width) / 2, lblEmailTitulo.Bottom + spacingY);

            // TELEFONE TÍTULO
            Label lblTelTitulo = new Label();
            lblTelTitulo.Text = "Telefone";
            lblTelTitulo.Font = new Font("Poppins", 11, FontStyle.Bold);
            lblTelTitulo.AutoSize = true;
            lblTelTitulo.TextAlign = ContentAlignment.MiddleCenter;
            lblTelTitulo.Location = new Point(0, painelEmailInfo.Bottom + spacingY);

            // PAINEL TELEFONE ÍCONE + TEXTO
            Panel painelTelInfo = new Panel();
            painelTelInfo.AutoSize = true;
            painelTelInfo.Padding = new Padding(0, spacingY, 0, spacingY);

            PictureBox picTel = new PictureBox();
            picTel.Size = new Size(iconSizeTel, iconSizeTel);
            picTel.Image = Properties.Resources.telefone_icone;
            picTel.SizeMode = PictureBoxSizeMode.Zoom;
            picTel.Location = new Point(0, 0);

            Label lblTel = new Label();
            lblTel.Text = telefone;
            lblTel.Font = new Font("Poppins", 11);
            lblTel.AutoSize = true;
            lblTel.Location = new Point(picTel.Right + spacingIconText, (iconSizeTel - lblTel.Height) / 2);

            painelTelInfo.Controls.AddRange(new Control[] { picTel, lblTel });
            painelTelInfo.Size = new Size(lblTel.Right, iconSizeTel);

            // Definindo a largura do bloco telefone para centralização do título e do conteúdo
            int larguraBlocoTel = Math.Max(lblTelTitulo.Width, painelTelInfo.Width);
            lblTelTitulo.Width = larguraBlocoTel;
            lblTelTitulo.Location = new Point(0, lblTelTitulo.Top);
            painelTelInfo.Location = new Point((larguraBlocoTel - painelTelInfo.Width) / 2, lblTelTitulo.Bottom + spacingY);

            // Monta painelInfo
            painelInfo.Controls.AddRange(new Control[] { lblEmailTitulo, painelEmailInfo, lblTelTitulo, painelTelInfo });
            painelInfo.Size = new Size(Math.Max(larguraBlocoEmail, larguraBlocoTel), painelTelInfo.Bottom);

            // Centraliza painelInfo dentro do espaço disponível
            painelInfo.Location = new Point(
                espaçoEsquerda + (larguraDisponivel - painelInfo.Width) / 2,
                (card.Height - painelInfo.Height) / 2
            );

            // ADICIONA CONTROLES
            card.Controls.AddRange(new Control[] {
        pic,
        lblNome,
        painelInfo,
        btnVerPosts,
        btnExcluir
    });

            return card;
        }










        private void ArredondarBordasPanel(Panel panel, int radius)
        {
            panel.Paint += (sender, e) =>
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                Rectangle bounds = panel.ClientRectangle;
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
                    path.AddArc(bounds.Right - radius, bounds.Y, radius, radius, 270, 90);
                    path.AddArc(bounds.Right - radius, bounds.Bottom - radius, radius, radius, 0, 90);
                    path.AddArc(bounds.X, bounds.Bottom - radius, radius, radius, 90, 90);
                    path.CloseAllFigures();

                    panel.Region = new Region(path);
                }
            };

            // Força a repintura na hora
            panel.Invalidate();
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

        private void BemVindo_Paint(object sender, PaintEventArgs e)
        {
            Color corTopo = Color.FromArgb(32, 53, 98);
            Color corBaixo = Color.FromArgb(125, 130, 155);

            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, corTopo, corBaixo, 90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void pictureBoxUsuario_Click(object sender, EventArgs e) { }

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

        private void ArredondarPictureBox(PictureBox pic)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, pic.Width, pic.Height);
            pic.Region = new Region(path);
            pic.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void ArredondarBotao(Button botao, int raio = 20)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, raio, raio, 180, 90);
            path.AddArc(botao.Width - raio, 0, raio, raio, 270, 90);
            path.AddArc(botao.Width - raio, botao.Height - raio, raio, raio, 0, 90);
            path.AddArc(0, botao.Height - raio, raio, raio, 90, 90);
            path.CloseFigure();

            botao.Region = new Region(path);
        }

        private void flowLayoutPanelProjetos_Paint(object sender, PaintEventArgs e) { }

        private void label2_Click(object sender, EventArgs e) { }
    }
}