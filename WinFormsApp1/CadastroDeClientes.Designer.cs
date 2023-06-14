namespace TreinamentoInvent
{
    partial class CadastroDeClientes
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            txtCpf = new MaskedTextBox();
            txtNome = new TextBox();
            txtEmail = new TextBox();
            txtTelefone = new MaskedTextBox();
            txtData = new DateTimePicker();
            btnSalvar = new Button();
            btnCancelar = new Button();
            label7 = new Label();
            openFileDialog1 = new OpenFileDialog();
            btnAbrirFoto = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(183, 9);
            label1.Name = "label1";
            label1.Size = new Size(143, 41);
            label1.TabIndex = 0;
            label1.Text = "Cadastro";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 63);
            label2.Name = "label2";
            label2.Size = new Size(50, 20);
            label2.TabIndex = 1;
            label2.Text = "Nome";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(24, 132);
            label3.Name = "label3";
            label3.Size = new Size(33, 20);
            label3.TabIndex = 2;
            label3.Text = "CPF";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(24, 199);
            label4.Name = "label4";
            label4.Size = new Size(52, 20);
            label4.TabIndex = 3;
            label4.Text = "E-Mail";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(24, 263);
            label5.Name = "label5";
            label5.Size = new Size(66, 20);
            label5.TabIndex = 4;
            label5.Text = "Telefone";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(24, 328);
            label6.Name = "label6";
            label6.Size = new Size(145, 20);
            label6.TabIndex = 5;
            label6.Text = "Data de Nascimento";
            // 
            // txtCpf
            // 
            txtCpf.Location = new Point(24, 155);
            txtCpf.Mask = "999.999.999-99";
            txtCpf.Name = "txtCpf";
            txtCpf.Size = new Size(456, 27);
            txtCpf.TabIndex = 6;
            // 
            // txtNome
            // 
            txtNome.Location = new Point(24, 86);
            txtNome.Name = "txtNome";
            txtNome.Size = new Size(456, 27);
            txtNome.TabIndex = 7;
            txtNome.KeyPress += EntradaDeLetrasParaNome;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(24, 222);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(456, 27);
            txtEmail.TabIndex = 8;
            // 
            // txtTelefone
            // 
            txtTelefone.Location = new Point(24, 286);
            txtTelefone.Mask = "99 9 9999-9999";
            txtTelefone.Name = "txtTelefone";
            txtTelefone.Size = new Size(456, 27);
            txtTelefone.TabIndex = 9;
            // 
            // txtData
            // 
            txtData.Format = DateTimePickerFormat.Short;
            txtData.Location = new Point(24, 351);
            txtData.Name = "txtData";
            txtData.Size = new Size(145, 27);
            txtData.TabIndex = 10;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(24, 488);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(126, 57);
            btnSalvar.TabIndex = 11;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += AoClicarEmSalvar;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(354, 488);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(126, 57);
            btnCancelar.TabIndex = 12;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += AoClicarEmCancelar;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(24, 394);
            label7.Name = "label7";
            label7.Size = new Size(107, 20);
            label7.TabIndex = 13;
            label7.Text = "Adicionar Foto";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.FileOk += openFileDialog1_FileOk;
            // 
            // btnAbrirFoto
            // 
            btnAbrirFoto.Location = new Point(24, 417);
            btnAbrirFoto.Name = "btnAbrirFoto";
            btnAbrirFoto.Size = new Size(145, 30);
            btnAbrirFoto.TabIndex = 14;
            btnAbrirFoto.Text = "Arquivo";
            btnAbrirFoto.UseVisualStyleBackColor = true;
            btnAbrirFoto.Click += btnAbrirFoto_Click;
            // 
            // CadastroDeClientes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(505, 573);
            Controls.Add(btnAbrirFoto);
            Controls.Add(label7);
            Controls.Add(btnCancelar);
            Controls.Add(btnSalvar);
            Controls.Add(txtData);
            Controls.Add(txtTelefone);
            Controls.Add(txtEmail);
            Controls.Add(txtNome);
            Controls.Add(txtCpf);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "CadastroDeClientes";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private MaskedTextBox txtCpf;
        private TextBox txtNome;
        private TextBox txtEmail;
        private MaskedTextBox txtTelefone;
        private DateTimePicker txtData;
        private Button btnSalvar;
        private Button btnCancelar;
        private Label label7;
        private OpenFileDialog openFileDialog1;
        private Button btnAbrirFoto;
    }
}