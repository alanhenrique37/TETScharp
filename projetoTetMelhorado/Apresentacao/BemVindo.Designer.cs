namespace projetoTetMelhorado.Apresentacao
{
    partial class BemVindo
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BemVindo));
            this.flowLayoutPanelProjetos = new System.Windows.Forms.FlowLayoutPanel();
            this.contextMenuUsuario = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.gerenciarPerfilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxUsuario = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.contextMenuUsuario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUsuario)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanelProjetos
            // 
            this.flowLayoutPanelProjetos.AutoScroll = true;
            this.flowLayoutPanelProjetos.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanelProjetos.Location = new System.Drawing.Point(361, 352);
            this.flowLayoutPanelProjetos.Name = "flowLayoutPanelProjetos";
            this.flowLayoutPanelProjetos.Size = new System.Drawing.Size(695, 375);
            this.flowLayoutPanelProjetos.TabIndex = 6;
            this.flowLayoutPanelProjetos.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanelProjetos_Paint);
            // 
            // contextMenuUsuario
            // 
            this.contextMenuUsuario.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gerenciarPerfilToolStripMenuItem,
            this.sairDToolStripMenuItem});
            this.contextMenuUsuario.Name = "contextMenuUsuario";
            this.contextMenuUsuario.Size = new System.Drawing.Size(155, 48);
            // 
            // gerenciarPerfilToolStripMenuItem
            // 
            this.gerenciarPerfilToolStripMenuItem.Name = "gerenciarPerfilToolStripMenuItem";
            this.gerenciarPerfilToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.gerenciarPerfilToolStripMenuItem.Text = "Gerenciar Perfil";
            this.gerenciarPerfilToolStripMenuItem.Click += new System.EventHandler(this.gerenciarPerfilToolStripMenuItem_Click);
            // 
            // sairDToolStripMenuItem
            // 
            this.sairDToolStripMenuItem.Name = "sairDToolStripMenuItem";
            this.sairDToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.sairDToolStripMenuItem.Text = "Sair da conta";
            this.sairDToolStripMenuItem.Click += new System.EventHandler(this.sairDToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(555, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(338, 99);
            this.label2.TabIndex = 14;
            this.label2.Text = "Olá Administrador! Seja bem vindo!";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(17, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(163, 178);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBoxUsuario
            // 
            this.pictureBoxUsuario.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxUsuario.Location = new System.Drawing.Point(1269, 35);
            this.pictureBoxUsuario.Name = "pictureBoxUsuario";
            this.pictureBoxUsuario.Size = new System.Drawing.Size(60, 60);
            this.pictureBoxUsuario.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxUsuario.TabIndex = 10;
            this.pictureBoxUsuario.TabStop = false;
            this.pictureBoxUsuario.Click += new System.EventHandler(this.pictureBoxUsuario_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(474, 231);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(495, 37);
            this.label3.TabIndex = 15;
            this.label3.Text = "Visualize, edite e controle usuários em segundos.";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(479, 271);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(479, 27);
            this.textBox1.TabIndex = 16;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // BemVindo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1363, 745);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBoxUsuario);
            this.Controls.Add(this.flowLayoutPanelProjetos);
            this.Name = "BemVindo";
            this.Text = "BemVindo";
            this.Load += new System.EventHandler(this.BemVindo_Load_1);
            this.contextMenuUsuario.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUsuario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProjetos;
        private System.Windows.Forms.PictureBox pictureBoxUsuario;
        private System.Windows.Forms.ContextMenuStrip contextMenuUsuario;
        private System.Windows.Forms.ToolStripMenuItem gerenciarPerfilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairDToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
    }
}