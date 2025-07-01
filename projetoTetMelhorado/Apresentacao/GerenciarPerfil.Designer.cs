namespace projetoTetMelhorado.Apresentacao
{
    partial class GerenciarPerfil
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label label1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblEmail = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSairDaConta = new System.Windows.Forms.Button();
            this.btnVoltaBV = new System.Windows.Forms.Button();
            this.lblNome = new System.Windows.Forms.Label();
            this.lblSenha = new System.Windows.Forms.Label();
            this.btnMIP = new System.Windows.Forms.Button();
            this.pictureBoxPerfil = new System.Windows.Forms.PictureBox();
            this.btnEditarPerfil = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPerfil)).BeginInit();
            this.SuspendLayout();
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(45, 152);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(0, 13);
            this.lblEmail.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 1;
            // 
            // btnSairDaConta
            // 
            this.btnSairDaConta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSairDaConta.Location = new System.Drawing.Point(259, 115);
            this.btnSairDaConta.Margin = new System.Windows.Forms.Padding(2);
            this.btnSairDaConta.Name = "btnSairDaConta";
            this.btnSairDaConta.Size = new System.Drawing.Size(94, 50);
            this.btnSairDaConta.TabIndex = 2;
            this.btnSairDaConta.Text = "Sair da conta";
            this.btnSairDaConta.UseVisualStyleBackColor = true;
            this.btnSairDaConta.Click += new System.EventHandler(this.btnSairDaConta_Click);
            // 
            // btnVoltaBV
            // 
            this.btnVoltaBV.Location = new System.Drawing.Point(259, 179);
            this.btnVoltaBV.Margin = new System.Windows.Forms.Padding(2);
            this.btnVoltaBV.Name = "btnVoltaBV";
            this.btnVoltaBV.Size = new System.Drawing.Size(94, 32);
            this.btnVoltaBV.TabIndex = 3;
            this.btnVoltaBV.Text = "Voltar";
            this.btnVoltaBV.UseVisualStyleBackColor = true;
            this.btnVoltaBV.Click += new System.EventHandler(this.btnVoltaBV_Click);
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(45, 125);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(87, 13);
            this.lblNome.TabIndex = 4;
            this.lblNome.Text = "Nome de usuário";
            // 
            // lblSenha
            // 
            this.lblSenha.AutoSize = true;
            this.lblSenha.Location = new System.Drawing.Point(45, 179);
            this.lblSenha.Name = "lblSenha";
            this.lblSenha.Size = new System.Drawing.Size(38, 13);
            this.lblSenha.TabIndex = 5;
            this.lblSenha.Text = "Senha";
            // 
            // btnMIP
            // 
            this.btnMIP.Location = new System.Drawing.Point(259, 64);
            this.btnMIP.Name = "btnMIP";
            this.btnMIP.Size = new System.Drawing.Size(90, 23);
            this.btnMIP.TabIndex = 6;
            this.btnMIP.Text = "Mudar imagem";
            this.btnMIP.UseVisualStyleBackColor = true;
            this.btnMIP.Click += new System.EventHandler(this.btnMIP_Click);
            // 
            // pictureBoxPerfil
            // 
            this.pictureBoxPerfil.Location = new System.Drawing.Point(48, 27);
            this.pictureBoxPerfil.Name = "pictureBoxPerfil";
            this.pictureBoxPerfil.Size = new System.Drawing.Size(109, 79);
            this.pictureBoxPerfil.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPerfil.TabIndex = 7;
            this.pictureBoxPerfil.TabStop = false;
            this.pictureBoxPerfil.Click += new System.EventHandler(this.pictureBoxPerfil_Click);
            // 
            // btnEditarPerfil
            // 
            this.btnEditarPerfil.Location = new System.Drawing.Point(259, 35);
            this.btnEditarPerfil.Name = "btnEditarPerfil";
            this.btnEditarPerfil.Size = new System.Drawing.Size(90, 23);
            this.btnEditarPerfil.TabIndex = 8;
            this.btnEditarPerfil.Text = "Editar Perfil";
            this.btnEditarPerfil.UseVisualStyleBackColor = true;
            this.btnEditarPerfil.Click += new System.EventHandler(this.btnEditarPerfil_Click);
            // 
            // GerenciarPerfil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1363, 745);
            this.Controls.Add(this.btnEditarPerfil);
            this.Controls.Add(this.pictureBoxPerfil);
            this.Controls.Add(this.btnMIP);
            this.Controls.Add(this.lblSenha);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.btnVoltaBV);
            this.Controls.Add(this.btnSairDaConta);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblEmail);
            this.Name = "GerenciarPerfil";
            this.Text = "Gerenciar Perfil";
            this.Load += new System.EventHandler(this.GerenciarPerfil_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPerfil)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button btnSairDaConta;
        private System.Windows.Forms.Button btnVoltaBV;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label lblSenha;
        private System.Windows.Forms.Button btnMIP;
        private System.Windows.Forms.PictureBox pictureBoxPerfil;
        private System.Windows.Forms.Button btnEditarPerfil;
    }
}
