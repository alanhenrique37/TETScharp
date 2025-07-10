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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projetoTetMelhorado.Apresentacao
{
    public partial class PostsDoUsuario : Form
    {
        private string emailUsuario;

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
            style &= ~WS_VSCROLL;
            style &= ~WS_HSCROLL;
            SetWindowLong(ctrl.Handle, GWL_STYLE, style);
            ctrl.Refresh();
        }

        public PostsDoUsuario(string email)
        {
            InitializeComponent();
            emailUsuario = email;
            this.Paint += new PaintEventHandler(PostsDoUsuario_Paint);
        }

        public PostsDoUsuario()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(PostsDoUsuario_Paint);
        }

        private void PostsDoUsuario_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = "Posts do " + emailUsuario;
            CarregarPostsDoUsuario();
 

            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button1.BackColor = Color.Transparent;
            button1.TabStop = false;
            button1.Cursor = Cursors.Hand;
            button1.Text = "";
        }

        private void CarregarPostsDoUsuario()
        {
            try
            {
                flowLayoutPanelPosts.Controls.Clear();

                using (MySqlConnection con = new Conexao().conectar())
                {
                    byte[] fotoPerfil = null;

                    string sqlFoto = "SELECT foto_perfil FROM logins WHERE email = @Email";
                    using (MySqlCommand cmdFoto = new MySqlCommand(sqlFoto, con))
                    {
                        cmdFoto.Parameters.AddWithValue("@Email", emailUsuario);
                        var resultado = cmdFoto.ExecuteScalar();
                        if (resultado != null && resultado != DBNull.Value)
                        {
                            fotoPerfil = (byte[])resultado;
                        }
                    }

                    string sql = @"SELECT id, nome_projeto, descricao, valor, data_criacao 
                                   FROM projetos 
                                   WHERE email_autor = @Email";

                    using (MySqlCommand cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", emailUsuario);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["id"]);
                                string nome = reader["nome_projeto"].ToString();
                                string desc = reader["descricao"].ToString();
                                string valor = Convert.ToDecimal(reader["valor"]).ToString("C");
                                string data = Convert.ToDateTime(reader["data_criacao"]).ToString("dd/MM/yyyy");

                                flowLayoutPanelPosts.Controls.Add(CriarCardPost(id, nome, desc, valor, data, fotoPerfil));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar posts: " + ex.Message);
            }
        }

        private Panel CriarCardPost(int id, string nome, string descricao, string valor, string data, byte[] fotoPerfil)
        {
            Panel card = new Panel();
            card.Size = new Size(flowLayoutPanelPosts.Width - 30, 200);
            card.BorderStyle = BorderStyle.None;
            card.Margin = new Padding(10);
            card.BackColor = Color.White;
            ArredondarBordasPanel(card, 15);

            PictureBox pic = new PictureBox();
            pic.Size = new Size(120, 120);
            pic.Location = new Point(20, (card.Height - pic.Height) / 2);
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            pic.Image = fotoPerfil != null ? Image.FromStream(new MemoryStream(fotoPerfil)) : Properties.Resources.avatar_padrao;
            ArredondarPictureBox(pic);

            Label lblTitulo = new Label();
            lblTitulo.Text = "Projeto: " + nome;
            lblTitulo.Font = new Font("Poppins", 12, FontStyle.Bold);
            lblTitulo.AutoSize = true;

            Label lblDescricao = new Label();
            lblDescricao.Text = descricao;
            lblDescricao.Font = new Font("Poppins", 10);
            lblDescricao.AutoSize = true;
            lblDescricao.MaximumSize = new Size(400, 40);
            lblDescricao.AutoEllipsis = true;

            Label lblValor = new Label();
            lblValor.Text = "Valor: " + valor;
            lblValor.Font = new Font("Poppins", 10, FontStyle.Italic);
            lblValor.AutoSize = true;

            Label lblData = new Label();
            lblData.Text = "Data: " + data;
            lblData.Font = new Font("Poppins", 10, FontStyle.Italic);
            lblData.AutoSize = true;

            Panel painelInfo = new Panel();
            painelInfo.BackColor = Color.Transparent;
            painelInfo.AutoSize = true;

            int spacingY = 2;
            lblTitulo.Location = new Point(0, 0);
            lblDescricao.Location = new Point(0, lblTitulo.Bottom + spacingY);
            lblValor.Location = new Point(0, lblDescricao.Bottom + spacingY);
            lblData.Location = new Point(0, lblValor.Bottom + spacingY);

            painelInfo.Controls.AddRange(new Control[] { lblTitulo, lblDescricao, lblValor, lblData });

            Button btnEditar = new Button();
            btnEditar.Text = "Editar";
            btnEditar.Size = new Size(130, 45);
            btnEditar.BackColor = Color.FromArgb(32, 53, 98);
            btnEditar.ForeColor = Color.White;
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.FlatAppearance.BorderSize = 0;
            btnEditar.Font = new Font("Poppins", 9, FontStyle.Bold);
            ArredondarBotao(btnEditar, 5);
            btnEditar.Click += (s, e) =>
            {
                var editar = new EditarPostAdm(id);
                editar.ShowDialog();
                CarregarPostsDoUsuario();
            };

            Button btnExcluir = new Button();
            btnExcluir.Text = "Excluir";
            btnExcluir.Size = new Size(130, 45);
            btnExcluir.BackColor = Color.FromArgb(231, 76, 60);
            btnExcluir.ForeColor = Color.White;
            btnExcluir.FlatStyle = FlatStyle.Flat;
            btnExcluir.FlatAppearance.BorderSize = 0;
            btnExcluir.Font = new Font("Poppins", 9, FontStyle.Bold);
            ArredondarBotao(btnExcluir, 5);
            btnExcluir.Click += (s, e) =>
            {
                var resultado = MessageBox.Show("Tem certeza que deseja excluir este post?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    ExcluirPost(id);
                    CarregarPostsDoUsuario();
                }
            };

            int espaçoEsquerda = pic.Right + 10;
            int espaçoDireita = card.Width - btnExcluir.Width - 30;
            int larguraDisponivel = espaçoDireita - espaçoEsquerda;

            painelInfo.Location = new Point(espaçoEsquerda + (larguraDisponivel - painelInfo.Width) / 2, (card.Height - painelInfo.Height) / 2);

            int botoesX = card.Width - btnEditar.Width - 20;
            btnEditar.Location = new Point(botoesX, (card.Height / 2) - btnEditar.Height - 5);
            btnExcluir.Location = new Point(botoesX, (card.Height / 2) + 5);

            card.Controls.AddRange(new Control[] {
                pic,
                painelInfo,
                btnEditar,
                btnExcluir
            });

            return card;
        }

        private void ExcluirPost(int idPost)
        {
            try
            {
                using (MySqlConnection con = new Conexao().conectar())
                {
                    string sqlExcluir = "DELETE FROM projetos WHERE id = @Id";
                    using (MySqlCommand cmd = new MySqlCommand(sqlExcluir, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", idPost);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Post excluído com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir o post: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void ArredondarPictureBox(PictureBox pic)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(0, 0, pic.Width, pic.Height);
            pic.Region = new Region(path);
            pic.SizeMode = PictureBoxSizeMode.Zoom;
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
            panel.Invalidate();
        }

        private void PostsDoUsuario_Paint(object sender, PaintEventArgs e)
        {
            Color corTopo = Color.FromArgb(32, 53, 98);
            Color corBaixo = Color.FromArgb(125, 130, 155);

            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, corTopo, corBaixo, 90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void lblUsuario_Click(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
    }
}
