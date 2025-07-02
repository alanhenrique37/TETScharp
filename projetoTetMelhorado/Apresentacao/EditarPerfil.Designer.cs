namespace projetoTetMelhorado.Apresentacao
{
    partial class EditarPerfil
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditarPerfil));
            this.txtNovoNome = new System.Windows.Forms.TextBox();
            this.txtNovaSenha = new System.Windows.Forms.TextBox();
            this.txtNovoTelefone = new System.Windows.Forms.TextBox();
            this.btnSalvarNome = new System.Windows.Forms.Button();
            this.btnSalvarSenha = new System.Windows.Forms.Button();
            this.btnSalvarTelefone = new System.Windows.Forms.Button();
            this.btnSalvarTudo = new System.Windows.Forms.Button();
            this.txtConfirmarSenha = new System.Windows.Forms.TextBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNovoNome
            // 
            this.txtNovoNome.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNovoNome.Location = new System.Drawing.Point(489, 245);
            this.txtNovoNome.Name = "txtNovoNome";
            this.txtNovoNome.Size = new System.Drawing.Size(314, 33);
            this.txtNovoNome.TabIndex = 3;
            this.txtNovoNome.TextChanged += new System.EventHandler(this.txtNovoNome_TextChanged);
            // 
            // txtNovaSenha
            // 
            this.txtNovaSenha.Font = new System.Drawing.Font("Calibri", 15.75F);
            this.txtNovaSenha.Location = new System.Drawing.Point(489, 344);
            this.txtNovaSenha.Name = "txtNovaSenha";
            this.txtNovaSenha.Size = new System.Drawing.Size(314, 33);
            this.txtNovaSenha.TabIndex = 4;
            this.txtNovaSenha.Text = " ";
            this.txtNovaSenha.TextChanged += new System.EventHandler(this.txtNovaSenha_TextChanged);
            // 
            // txtNovoTelefone
            // 
            this.txtNovoTelefone.Font = new System.Drawing.Font("Calibri", 15.75F);
            this.txtNovoTelefone.Location = new System.Drawing.Point(489, 531);
            this.txtNovoTelefone.Name = "txtNovoTelefone";
            this.txtNovoTelefone.Size = new System.Drawing.Size(314, 33);
            this.txtNovoTelefone.TabIndex = 5;
            this.txtNovoTelefone.TextChanged += new System.EventHandler(this.txtNovoTelefone_TextChanged);
            // 
            // btnSalvarNome
            // 
            this.btnSalvarNome.BackColor = System.Drawing.Color.White;
            this.btnSalvarNome.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvarNome.Location = new System.Drawing.Point(809, 248);
            this.btnSalvarNome.Name = "btnSalvarNome";
            this.btnSalvarNome.Size = new System.Drawing.Size(98, 23);
            this.btnSalvarNome.TabIndex = 6;
            this.btnSalvarNome.Text = "Salvar Nome";
            this.btnSalvarNome.UseVisualStyleBackColor = false;
            this.btnSalvarNome.Click += new System.EventHandler(this.btnSalvarNome_Click);
            // 
            // btnSalvarSenha
            // 
            this.btnSalvarSenha.BackColor = System.Drawing.Color.White;
            this.btnSalvarSenha.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Bold);
            this.btnSalvarSenha.Location = new System.Drawing.Point(809, 441);
            this.btnSalvarSenha.Name = "btnSalvarSenha";
            this.btnSalvarSenha.Size = new System.Drawing.Size(98, 23);
            this.btnSalvarSenha.TabIndex = 7;
            this.btnSalvarSenha.Text = " Salvar Senha";
            this.btnSalvarSenha.UseVisualStyleBackColor = false;
            this.btnSalvarSenha.Click += new System.EventHandler(this.btnSalvarSenha_Click);
            // 
            // btnSalvarTelefone
            // 
            this.btnSalvarTelefone.BackColor = System.Drawing.Color.White;
            this.btnSalvarTelefone.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Bold);
            this.btnSalvarTelefone.Location = new System.Drawing.Point(809, 534);
            this.btnSalvarTelefone.Name = "btnSalvarTelefone";
            this.btnSalvarTelefone.Size = new System.Drawing.Size(98, 23);
            this.btnSalvarTelefone.TabIndex = 8;
            this.btnSalvarTelefone.Text = " Salvar Telefone";
            this.btnSalvarTelefone.UseVisualStyleBackColor = false;
            this.btnSalvarTelefone.Click += new System.EventHandler(this.btnSalvarTelefone_Click);
            // 
            // btnSalvarTudo
            // 
            this.btnSalvarTudo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(53)))), ((int)(((byte)(98)))));
            this.btnSalvarTudo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvarTudo.ForeColor = System.Drawing.Color.White;
            this.btnSalvarTudo.Location = new System.Drawing.Point(555, 597);
            this.btnSalvarTudo.Name = "btnSalvarTudo";
            this.btnSalvarTudo.Size = new System.Drawing.Size(197, 41);
            this.btnSalvarTudo.TabIndex = 9;
            this.btnSalvarTudo.Text = " Salvar Tudo";
            this.btnSalvarTudo.UseVisualStyleBackColor = false;
            this.btnSalvarTudo.Click += new System.EventHandler(this.btnSalvarTudo_Click);
            // 
            // txtConfirmarSenha
            // 
            this.txtConfirmarSenha.Font = new System.Drawing.Font("Calibri", 15.75F);
            this.txtConfirmarSenha.Location = new System.Drawing.Point(489, 438);
            this.txtConfirmarSenha.Name = "txtConfirmarSenha";
            this.txtConfirmarSenha.Size = new System.Drawing.Size(314, 33);
            this.txtConfirmarSenha.TabIndex = 10;
            this.txtConfirmarSenha.TextChanged += new System.EventHandler(this.txtConfirmarSenha_TextChanged);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.White;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(53)))), ((int)(((byte)(98)))));
            this.btnCancelar.Location = new System.Drawing.Point(555, 648);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(197, 41);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(571, 202);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 30);
            this.label4.TabIndex = 13;
            this.label4.Text = " Novo Nome";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(574, 296);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 30);
            this.label5.TabIndex = 14;
            this.label5.Text = " Nova Senha";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(555, 393);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(173, 30);
            this.label6.TabIndex = 15;
            this.label6.Text = " Confirma senha";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(567, 488);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(156, 30);
            this.label7.TabIndex = 16;
            this.label7.Text = "Novo Telefone";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(23, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 37);
            this.button1.TabIndex = 17;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(1216, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(135, 135);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 32.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(465, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(406, 59);
            this.label1.TabIndex = 19;
            this.label1.Text = "Editar Informações";
            // 
            // EditarPerfil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1363, 745);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.txtConfirmarSenha);
            this.Controls.Add(this.btnSalvarTudo);
            this.Controls.Add(this.btnSalvarTelefone);
            this.Controls.Add(this.btnSalvarSenha);
            this.Controls.Add(this.btnSalvarNome);
            this.Controls.Add(this.txtNovoTelefone);
            this.Controls.Add(this.txtNovaSenha);
            this.Controls.Add(this.txtNovoNome);
            this.Name = "EditarPerfil";
            this.Text = "EditarPerfil";
            this.Load += new System.EventHandler(this.EditarPerfil_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtNovoNome;
        private System.Windows.Forms.TextBox txtNovaSenha;
        private System.Windows.Forms.TextBox txtNovoTelefone;
        private System.Windows.Forms.Button btnSalvarNome;
        private System.Windows.Forms.Button btnSalvarSenha;
        private System.Windows.Forms.Button btnSalvarTelefone;
        private System.Windows.Forms.Button btnSalvarTudo;
        private System.Windows.Forms.TextBox txtConfirmarSenha;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}