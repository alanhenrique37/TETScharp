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
    public partial class EditarPostAdm : Form
    {
        int idPost;

        public EditarPostAdm(int id)
        {
            InitializeComponent();
            idPost = id;
        }

        private void EditarPostAdm_Load(object sender, EventArgs e)
        {
            CarregarDadosDoPost();

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
            txtNomeProjeto.Height = 100;
            txtDescricao.Height = 100;
            txtValor.Height = 100;
        
            // Arredondar TextBoxes
            ArredondarTextBox(txtNomeProjeto);
            ArredondarTextBox(txtDescricao);
            ArredondarTextBox(txtValor);

            // Arredondar Botões com raios diferentes
            ArredondarBotao(btnSalvar, 20);
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

        private void EditarPostAdm_Paint(object sender, PaintEventArgs e)
        {
            Color corTopo = Color.FromArgb(32, 53, 98);
            Color corBaixo = Color.FromArgb(125, 130, 155);

            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, corTopo, corBaixo, 90F))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void CarregarDadosDoPost()
        {
            try
            {
                Conexao conexao = new Conexao();
                string sql = "SELECT nome_projeto, descricao, valor FROM projetos WHERE id = @id";
                MySqlCommand cmd = new MySqlCommand(sql, conexao.conectar());
                cmd.Parameters.AddWithValue("@id", idPost);

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtNomeProjeto.Text = reader["nome_projeto"].ToString();
                    txtDescricao.Text = reader["descricao"].ToString();
                    txtValor.Text = reader["valor"].ToString();
                }

                conexao.desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                Conexao conexao = new Conexao();
                string sql = @"UPDATE projetos 
                               SET nome_projeto = @nome, descricao = @descricao, valor = @valor 
                               WHERE id = @id";

                MySqlCommand cmd = new MySqlCommand(sql, conexao.conectar());
                cmd.Parameters.AddWithValue("@nome", txtNomeProjeto.Text.Trim());
                cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text.Trim());
                cmd.Parameters.AddWithValue("@valor", Convert.ToDecimal(txtValor.Text));
                cmd.Parameters.AddWithValue("@id", idPost);

                cmd.ExecuteNonQuery();
                conexao.desconectar();

                MessageBox.Show("Post atualizado com sucesso!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message);
            }
        }
        //fim do botão salvar
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //fim do botão cancelar
        private void txtValor_TextChanged(object sender, EventArgs e)
        {
            string texto = txtValor.Text;

            // Remove tudo que não for número ou vírgula
            string apenasNumerosEVirgula = new string(texto.Where(c => char.IsDigit(c) || c == ',').ToArray());

            // Garante que só tenha uma vírgula
            int primeiraVirgula = apenasNumerosEVirgula.IndexOf(',');
            if (primeiraVirgula >= 0)
            {
                // Remove vírgulas extras
                apenasNumerosEVirgula = apenasNumerosEVirgula.Substring(0, primeiraVirgula + 1) +
                    apenasNumerosEVirgula.Substring(primeiraVirgula + 1).Replace(",", "");
            }

            // Atualiza o TextBox apenas se foi alterado
            if (txtValor.Text != apenasNumerosEVirgula)
            {
                int pos = txtValor.SelectionStart;
                txtValor.Text = apenasNumerosEVirgula;
                txtValor.SelectionStart = Math.Min(pos, txtValor.Text.Length);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PostsDoUsuario pos = new PostsDoUsuario();
            pos.Show();
            this.Hide();
        }
    }
}
