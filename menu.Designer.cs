namespace p3
{
    partial class menu
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.registro = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.calificaciones = new System.Windows.Forms.RadioButton();
            this.nomina = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.registro);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.calificaciones);
            this.panel1.Controls.Add(this.nomina);
            this.panel1.Location = new System.Drawing.Point(25, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(227, 229);
            this.panel1.TabIndex = 0;
            // 
            // registro
            // 
            this.registro.AutoSize = true;
            this.registro.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registro.Location = new System.Drawing.Point(15, 174);
            this.registro.Name = "registro";
            this.registro.Size = new System.Drawing.Size(137, 21);
            this.registro.TabIndex = 3;
            this.registro.Text = "Registrar Usuario";
            this.registro.UseVisualStyleBackColor = true;
            this.registro.CheckedChanged += new System.EventHandler(this.registro_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Registros y Mantenimientos";
            // 
            // calificaciones
            // 
            this.calificaciones.AutoSize = true;
            this.calificaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calificaciones.Location = new System.Drawing.Point(15, 116);
            this.calificaciones.Name = "calificaciones";
            this.calificaciones.Size = new System.Drawing.Size(112, 21);
            this.calificaciones.TabIndex = 1;
            this.calificaciones.Text = "Calificaciones";
            this.calificaciones.UseVisualStyleBackColor = true;
            this.calificaciones.CheckedChanged += new System.EventHandler(this.calificaciones_CheckedChanged);
            // 
            // nomina
            // 
            this.nomina.AutoSize = true;
            this.nomina.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nomina.Location = new System.Drawing.Point(15, 58);
            this.nomina.Name = "nomina";
            this.nomina.Size = new System.Drawing.Size(74, 21);
            this.nomina.TabIndex = 0;
            this.nomina.Text = "Nomina";
            this.nomina.UseVisualStyleBackColor = true;
            this.nomina.CheckedChanged += new System.EventHandler(this.nomina_CheckedChanged);
            // 
            // menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 329);
            this.Controls.Add(this.panel1);
            this.Name = "menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "menu";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton calificaciones;
        private System.Windows.Forms.RadioButton nomina;
        private System.Windows.Forms.RadioButton registro;
    }
}