namespace projetoTetMelhorado.Apresentacao
{
    partial class PostsDoUsuario
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
            this.flowLayoutPanelPosts = new System.Windows.Forms.FlowLayoutPanel();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // flowLayoutPanelPosts
            // 
            this.flowLayoutPanelPosts.AutoScroll = true;
            this.flowLayoutPanelPosts.Location = new System.Drawing.Point(137, 144);
            this.flowLayoutPanelPosts.Name = "flowLayoutPanelPosts";
            this.flowLayoutPanelPosts.Size = new System.Drawing.Size(1039, 497);
            this.flowLayoutPanelPosts.TabIndex = 0;
            // 
            // btnVoltar
            // 
            this.btnVoltar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar.Location = new System.Drawing.Point(1081, 680);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(95, 34);
            this.btnVoltar.TabIndex = 2;
            this.btnVoltar.Text = "Voltar";
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.Location = new System.Drawing.Point(88, 26);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(70, 25);
            this.lblUsuario.TabIndex = 3;
            this.lblUsuario.Text = "label1";
            // 
            // PostsDoUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1363, 745);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.btnVoltar);
            this.Controls.Add(this.flowLayoutPanelPosts);
            this.Name = "PostsDoUsuario";
            this.Text = " ";
            this.Load += new System.EventHandler(this.PostsDoUsuario_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPosts;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.Label lblUsuario;
    }
}