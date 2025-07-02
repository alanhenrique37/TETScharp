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

namespace projetoTetMelhorado.Apresentacao
{
    public partial class BemVindo : Form
    {
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
            card.Size = new Size(flowLayoutPanelProjetos.Width - 30, 170); // Aumentei a altura para 170px
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Margin = new Padding(10);
            card.BackColor = Color.White;
            ArredondarBordasPanel(card, 10);

            // Foto do usuário (80x80) com padding
            PictureBox pic = new PictureBox();
            pic.Size = new Size(80, 80);
            pic.Location = new Point(25, 25);  // Padding left e top de 25px
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            pic.Image = foto != null ? Image.FromStream(new MemoryStream(foto)) : Properties.Resources.avatar_padrao;
            ArredondarPictureBox(pic);

            // Nome abaixo da imagem com padding bottom de 15px
            Label lblNome = new Label();
            lblNome.Text = tipo == "admin" ? $"{nome} (ADM)" : nome;
            lblNome.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblNome.Location = new Point(25, 120);  // 25 (left) + 80 (altura img) + 15 (padding bottom)
            lblNome.AutoSize = true;

            // Container para email e telefone com padding right
            Panel pnlInfo = new Panel();
            pnlInfo.Location = new Point(140, 25);  // 25 (padding) + 80 (largura img) + 35 (padding right)
            pnlInfo.Size = new Size(card.Width - 280, 80);  // Reduzi a largura para dar mais espaço
            pnlInfo.BackColor = Color.Transparent;

            // Email
            Label lblEmail = new Label();
            lblEmail.Text = email;
            lblEmail.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            lblEmail.Location = new Point(0, 10);
            lblEmail.AutoSize = true;

            // Telefone
            Label lblTel = new Label();
            lblTel.Text = telefone;
            lblTel.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            lblTel.Location = new Point(0, 35);
            lblTel.AutoSize = true;

            pnlInfo.Controls.Add(lblEmail);
            pnlInfo.Controls.Add(lblTel);

            // Botão Excluir
            Button btnExcluir = new Button();
            btnExcluir.Text = "Excluir";
            btnExcluir.Size = new Size(120, 35);
            btnExcluir.Location = new Point(card.Width - 155, (card.Height - btnExcluir.Height) / 2);
            btnExcluir.BackColor = Color.Red;
            btnExcluir.ForeColor = Color.White;
            btnExcluir.FlatStyle = FlatStyle.Flat;
            btnExcluir.FlatAppearance.BorderSize = 0;
            btnExcluir.Font = new Font("Segoe UI", 9, FontStyle.Bold);
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

            card.Controls.Add(pic);
            card.Controls.Add(lblNome);
            card.Controls.Add(pnlInfo);
            card.Controls.Add(btnExcluir);

            return card;
        }

        private void ArredondarBordasPanel(Panel panel, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(panel.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(panel.Width - radius, panel.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, panel.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            panel.Region = new Region(path);
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