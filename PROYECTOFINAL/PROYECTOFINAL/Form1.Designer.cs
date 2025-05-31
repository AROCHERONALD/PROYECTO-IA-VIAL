namespace PROYECTOFINAL
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtPregunta;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.TextBox txtRespuesta;
        private System.Windows.Forms.Button btnHistorial;

        // Controles adicionales
        private System.Windows.Forms.Label lblPreguntaExamen;
        private System.Windows.Forms.RadioButton radioA;
        private System.Windows.Forms.RadioButton radioB;
        private System.Windows.Forms.RadioButton radioC;
        private System.Windows.Forms.Button btnResponder;
        private System.Windows.Forms.Button btnRepetirExamen;
        private System.Windows.Forms.Button btnTrivia;
        private System.Windows.Forms.TextBox txtUbicacionZona;
        private System.Windows.Forms.TextBox txtDescripcionZona;
        private System.Windows.Forms.Button btnReportarZona;
        private System.Windows.Forms.Button btnMostrarZonas;
        private System.Windows.Forms.ComboBox comboBoxPerfil;
        private System.Windows.Forms.Label lblPuntaje;

        // CONTROLES PARA API KEY
        private System.Windows.Forms.TextBox txtApiKey;
        private System.Windows.Forms.Button btnGuardarApiKey;

        // NUEVOS CONTROLES PARA EL MAPA
        private System.Windows.Forms.Button btnCentrarMapa;
        private System.Windows.Forms.TextBox txtBuscarMapa;
        private System.Windows.Forms.Button btnBuscarMapa;

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
            this.txtPregunta = new System.Windows.Forms.TextBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.txtRespuesta = new System.Windows.Forms.TextBox();
            this.btnHistorial = new System.Windows.Forms.Button();
            this.lblPreguntaExamen = new System.Windows.Forms.Label();
            this.radioA = new System.Windows.Forms.RadioButton();
            this.radioB = new System.Windows.Forms.RadioButton();
            this.radioC = new System.Windows.Forms.RadioButton();
            this.btnResponder = new System.Windows.Forms.Button();
            this.btnRepetirExamen = new System.Windows.Forms.Button();
            this.btnTrivia = new System.Windows.Forms.Button();
            this.txtUbicacionZona = new System.Windows.Forms.TextBox();
            this.txtDescripcionZona = new System.Windows.Forms.TextBox();
            this.btnReportarZona = new System.Windows.Forms.Button();
            this.btnMostrarZonas = new System.Windows.Forms.Button();
            this.comboBoxPerfil = new System.Windows.Forms.ComboBox();
            this.lblPuntaje = new System.Windows.Forms.Label();

            // CONTROLES PARA API KEY
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.btnGuardarApiKey = new System.Windows.Forms.Button();

            // NUEVOS CONTROLES PARA EL MAPA
            this.btnCentrarMapa = new System.Windows.Forms.Button();
            this.txtBuscarMapa = new System.Windows.Forms.TextBox();
            this.btnBuscarMapa = new System.Windows.Forms.Button();

            this.SuspendLayout();
            // 
            // txtApiKey
            // 
            this.txtApiKey.Location = new System.Drawing.Point(12, 5);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.Size = new System.Drawing.Size(250, 22);
            this.txtApiKey.TabIndex = 100;
            this.txtApiKey.ForeColor = System.Drawing.Color.Gray;
            this.txtApiKey.Text = "Pega aquí tu API Key de OpenAI";
            this.txtApiKey.GotFocus += new System.EventHandler(this.txtApiKey_GotFocus);
            this.txtApiKey.LostFocus += new System.EventHandler(this.txtApiKey_LostFocus);
            // 
            // btnGuardarApiKey
            // 
            this.btnGuardarApiKey.Location = new System.Drawing.Point(270, 3);
            this.btnGuardarApiKey.Name = "btnGuardarApiKey";
            this.btnGuardarApiKey.Size = new System.Drawing.Size(120, 25);
            this.btnGuardarApiKey.TabIndex = 101;
            this.btnGuardarApiKey.Text = "Guardar API Key";
            this.btnGuardarApiKey.UseVisualStyleBackColor = true;
            this.btnGuardarApiKey.Click += new System.EventHandler(this.btnGuardarApiKey_Click);
            // 
            // txtPregunta
            // 
            this.txtPregunta.Location = new System.Drawing.Point(12, 35);
            this.txtPregunta.Multiline = true;
            this.txtPregunta.Name = "txtPregunta";
            this.txtPregunta.Size = new System.Drawing.Size(360, 60);
            this.txtPregunta.TabIndex = 0;
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(378, 35);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(90, 60);
            this.btnEnviar.TabIndex = 1;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // txtRespuesta
            // 
            this.txtRespuesta.Location = new System.Drawing.Point(12, 113);
            this.txtRespuesta.Multiline = true;
            this.txtRespuesta.Name = "txtRespuesta";
            this.txtRespuesta.ReadOnly = true;
            this.txtRespuesta.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRespuesta.Size = new System.Drawing.Size(456, 60);
            this.txtRespuesta.TabIndex = 2;
            // 
            // btnHistorial
            // 
            this.btnHistorial.Location = new System.Drawing.Point(12, 183);
            this.btnHistorial.Name = "btnHistorial";
            this.btnHistorial.Size = new System.Drawing.Size(456, 30);
            this.btnHistorial.TabIndex = 3;
            this.btnHistorial.Text = "Ver Historial";
            this.btnHistorial.UseVisualStyleBackColor = true;
            this.btnHistorial.Click += new System.EventHandler(this.btnHistorial_Click);
            // 
            // lblPuntaje
            // 
            this.lblPuntaje.Location = new System.Drawing.Point(12, 218);
            this.lblPuntaje.Name = "lblPuntaje";
            this.lblPuntaje.Size = new System.Drawing.Size(456, 20);
            this.lblPuntaje.TabIndex = 16;
            this.lblPuntaje.Text = "Puntaje: 0 de 0";
            // 
            // lblPreguntaExamen
            // 
            this.lblPreguntaExamen.Location = new System.Drawing.Point(12, 243);
            this.lblPreguntaExamen.Name = "lblPreguntaExamen";
            this.lblPreguntaExamen.Size = new System.Drawing.Size(456, 40);
            this.lblPreguntaExamen.TabIndex = 5;
            this.lblPreguntaExamen.Text = "Pregunta del examen";
            // 
            // radioA
            // 
            this.radioA.Location = new System.Drawing.Point(12, 293);
            this.radioA.Name = "radioA";
            this.radioA.Size = new System.Drawing.Size(456, 20);
            this.radioA.TabIndex = 6;
            this.radioA.Text = "Opción A";
            // 
            // radioB
            // 
            this.radioB.Location = new System.Drawing.Point(12, 323);
            this.radioB.Name = "radioB";
            this.radioB.Size = new System.Drawing.Size(456, 20);
            this.radioB.TabIndex = 7;
            this.radioB.Text = "Opción B";
            // 
            // radioC
            // 
            this.radioC.Location = new System.Drawing.Point(12, 353);
            this.radioC.Name = "radioC";
            this.radioC.Size = new System.Drawing.Size(456, 20);
            this.radioC.TabIndex = 8;
            this.radioC.Text = "Opción C";
            // 
            // btnResponder
            // 
            this.btnResponder.Location = new System.Drawing.Point(12, 383);
            this.btnResponder.Name = "btnResponder";
            this.btnResponder.Size = new System.Drawing.Size(456, 30);
            this.btnResponder.TabIndex = 9;
            this.btnResponder.Text = "Responder Examen";
            this.btnResponder.UseVisualStyleBackColor = true;
            this.btnResponder.Click += new System.EventHandler(this.btnResponder_Click);
            // 
            // btnRepetirExamen
            // 
            this.btnRepetirExamen.Location = new System.Drawing.Point(12, 418);
            this.btnRepetirExamen.Name = "btnRepetirExamen";
            this.btnRepetirExamen.Size = new System.Drawing.Size(456, 30);
            this.btnRepetirExamen.TabIndex = 15;
            this.btnRepetirExamen.Text = "Repetir Examen";
            this.btnRepetirExamen.UseVisualStyleBackColor = true;
            this.btnRepetirExamen.Click += new System.EventHandler(this.btnRepetirExamen_Click);
            // 
            // btnTrivia
            // 
            this.btnTrivia.Location = new System.Drawing.Point(12, 453);
            this.btnTrivia.Name = "btnTrivia";
            this.btnTrivia.Size = new System.Drawing.Size(220, 30);
            this.btnTrivia.TabIndex = 10;
            this.btnTrivia.Text = "Trivia Educativa";
            this.btnTrivia.UseVisualStyleBackColor = true;
            this.btnTrivia.Click += new System.EventHandler(this.btnTrivia_Click);
            // 
            // txtUbicacionZona
            // 
            this.txtUbicacionZona.Location = new System.Drawing.Point(12, 493);
            this.txtUbicacionZona.Name = "txtUbicacionZona";
            this.txtUbicacionZona.Size = new System.Drawing.Size(220, 22);
            this.txtUbicacionZona.TabIndex = 11;
            // 
            // txtDescripcionZona
            // 
            this.txtDescripcionZona.Location = new System.Drawing.Point(240, 493);
            this.txtDescripcionZona.Name = "txtDescripcionZona";
            this.txtDescripcionZona.Size = new System.Drawing.Size(228, 22);
            this.txtDescripcionZona.TabIndex = 12;
            // 
            // btnReportarZona
            // 
            this.btnReportarZona.Location = new System.Drawing.Point(12, 523);
            this.btnReportarZona.Name = "btnReportarZona";
            this.btnReportarZona.Size = new System.Drawing.Size(220, 30);
            this.btnReportarZona.TabIndex = 13;
            this.btnReportarZona.Text = "Reportar Zona de Riesgo";
            this.btnReportarZona.UseVisualStyleBackColor = true;
            this.btnReportarZona.Click += new System.EventHandler(this.btnReportarZona_Click);
            // 
            // btnMostrarZonas
            // 
            this.btnMostrarZonas.Location = new System.Drawing.Point(240, 523);
            this.btnMostrarZonas.Name = "btnMostrarZonas";
            this.btnMostrarZonas.Size = new System.Drawing.Size(228, 30);
            this.btnMostrarZonas.TabIndex = 14;
            this.btnMostrarZonas.Text = "Mostrar Zonas de Riesgo";
            this.btnMostrarZonas.UseVisualStyleBackColor = true;
            this.btnMostrarZonas.Click += new System.EventHandler(this.btnMostrarZonas_Click);
            // 
            // btnCentrarMapa
            // 
            this.btnCentrarMapa.Location = new System.Drawing.Point(12, 560);
            this.btnCentrarMapa.Name = "btnCentrarMapa";
            this.btnCentrarMapa.Size = new System.Drawing.Size(140, 30);
            this.btnCentrarMapa.TabIndex = 102;
            this.btnCentrarMapa.Text = "Centrar Mapa";
            this.btnCentrarMapa.UseVisualStyleBackColor = true;
            this.btnCentrarMapa.Click += new System.EventHandler(this.btnCentrarMapa_Click);
            // 
            // txtBuscarMapa
            // 
            this.txtBuscarMapa.Location = new System.Drawing.Point(160, 565);
            this.txtBuscarMapa.Name = "txtBuscarMapa";
            this.txtBuscarMapa.Size = new System.Drawing.Size(180, 22);
            this.txtBuscarMapa.TabIndex = 103;
            this.txtBuscarMapa.ForeColor = System.Drawing.Color.Gray;
            this.txtBuscarMapa.Text = "Buscar dirección";
            this.txtBuscarMapa.GotFocus += new System.EventHandler(this.txtBuscarMapa_GotFocus);
            this.txtBuscarMapa.LostFocus += new System.EventHandler(this.txtBuscarMapa_LostFocus);
            // 
            // btnBuscarMapa
            // 
            this.btnBuscarMapa.Location = new System.Drawing.Point(350, 560);
            this.btnBuscarMapa.Name = "btnBuscarMapa";
            this.btnBuscarMapa.Size = new System.Drawing.Size(118, 30);
            this.btnBuscarMapa.TabIndex = 104;
            this.btnBuscarMapa.Text = "Buscar en el mapa";
            this.btnBuscarMapa.UseVisualStyleBackColor = true;
            this.btnBuscarMapa.Click += new System.EventHandler(this.btnBuscarMapa_Click);
            // 
            // comboBoxPerfil
            // 
            this.comboBoxPerfil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPerfil.Items.AddRange(new object[] {
            "Conductor",
            "Peatón",
            "Ciclista",
            "Motociclista"});
            this.comboBoxPerfil.Location = new System.Drawing.Point(378, 100);
            this.comboBoxPerfil.Name = "comboBoxPerfil";
            this.comboBoxPerfil.Size = new System.Drawing.Size(90, 24);
            this.comboBoxPerfil.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 790);
            this.Controls.Add(this.txtApiKey);
            this.Controls.Add(this.btnGuardarApiKey);
            this.Controls.Add(this.txtPregunta);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.txtRespuesta);
            this.Controls.Add(this.btnHistorial);
            this.Controls.Add(this.lblPuntaje);
            this.Controls.Add(this.comboBoxPerfil);
            this.Controls.Add(this.lblPreguntaExamen);
            this.Controls.Add(this.radioA);
            this.Controls.Add(this.radioB);
            this.Controls.Add(this.radioC);
            this.Controls.Add(this.btnResponder);
            this.Controls.Add(this.btnRepetirExamen);
            this.Controls.Add(this.btnTrivia);
            this.Controls.Add(this.txtUbicacionZona);
            this.Controls.Add(this.txtDescripcionZona);
            this.Controls.Add(this.btnReportarZona);
            this.Controls.Add(this.btnMostrarZonas);
            this.Controls.Add(this.btnCentrarMapa);
            this.Controls.Add(this.txtBuscarMapa);
            this.Controls.Add(this.btnBuscarMapa);
            this.Name = "Form1";
            this.Text = "AI-VIAL - Asistente Vial Inteligente";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
