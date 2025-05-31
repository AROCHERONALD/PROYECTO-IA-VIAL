using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Drawing;

// GMap.NET
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using GMap.NET;

namespace PROYECTOFINAL
{
    public partial class Form1 : Form
    {
        // Variables para el simulador de examen
        private List<PreguntaExamen> preguntas;
        private int preguntaActual = 0;
        private int correctas = 0;
        private string openAIApiKey = "";

        // Control de mapa
        private GMapControl gmap;

        public Form1()
        {
            InitializeComponent();

            // Personalización visual moderna
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10F);

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = Color.SteelBlue;
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                }
                if (ctrl is TextBox txt)
                {
                    txt.Font = new Font("Segoe UI", 10F);
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
                if (ctrl is Label lbl)
                {
                    lbl.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                }
                if (ctrl is ComboBox cb)
                {
                    cb.Font = new Font("Segoe UI", 10F);
                }
                if (ctrl is RadioButton rb)
                {
                    rb.Font = new Font("Segoe UI", 10F);
                }
            }

            // Título destacado para la pregunta de examen
            lblPreguntaExamen.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPreguntaExamen.ForeColor = Color.DarkSlateBlue;

            // Puntaje destacado
            lblPuntaje.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPuntaje.ForeColor = Color.DarkGreen;

            // Opcional: Cambia el color de fondo de los TextBox de zona de riesgo
            txtUbicacionZona.BackColor = Color.White;
            txtDescripcionZona.BackColor = Color.White;

            // Placeholder manual para txtApiKey (API Key)
            txtApiKey.ForeColor = Color.Gray;
            txtApiKey.Text = "Pega aquí tu API Key de OpenAI";
            txtApiKey.GotFocus += txtApiKey_GotFocus;
            txtApiKey.LostFocus += txtApiKey_LostFocus;

            // Placeholder manual para txtUbicacionZona
            txtUbicacionZona.ForeColor = Color.Gray;
            txtUbicacionZona.Text = "Ubicación de la zona";
            txtUbicacionZona.GotFocus += (s, e) => {
                if (txtUbicacionZona.Text == "Ubicación de la zona")
                {
                    txtUbicacionZona.Text = "";
                    txtUbicacionZona.ForeColor = Color.Black;
                }
            };
            txtUbicacionZona.LostFocus += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtUbicacionZona.Text))
                {
                    txtUbicacionZona.Text = "Ubicación de la zona";
                    txtUbicacionZona.ForeColor = Color.Gray;
                }
            };

            // Placeholder manual para txtDescripcionZona
            txtDescripcionZona.ForeColor = Color.Gray;
            txtDescripcionZona.Text = "Descripción";
            txtDescripcionZona.GotFocus += (s, e) => {
                if (txtDescripcionZona.Text == "Descripción")
                {
                    txtDescripcionZona.Text = "";
                    txtDescripcionZona.ForeColor = Color.Black;
                }
            };
            txtDescripcionZona.LostFocus += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtDescripcionZona.Text))
                {
                    txtDescripcionZona.Text = "Descripción";
                    txtDescripcionZona.ForeColor = Color.Gray;
                }
            };

            // Placeholder manual para txtBuscarMapa
            txtBuscarMapa.ForeColor = Color.Gray;
            txtBuscarMapa.Text = "Buscar dirección";
            txtBuscarMapa.GotFocus += txtBuscarMapa_GotFocus;
            txtBuscarMapa.LostFocus += txtBuscarMapa_LostFocus;

            // Inicializa el mapa
            gmap = new GMapControl();
            gmap.Name = "gmap";
            gmap.Height = 300;
            gmap.Dock = DockStyle.Bottom;
            this.Controls.Add(gmap);

            gmap.MapProvider = GMapProviders.GoogleMap;
            gmap.Position = new PointLatLng(14.2901, -89.8930); // Jutiapa por defecto
            gmap.MinZoom = 2;
            gmap.MaxZoom = 18;
            gmap.Zoom = 12;
            gmap.ShowCenter = false;

            // ¡Activa el simulador de examen!
            CargarPreguntas();

            // Muestra zonas en el mapa al iniciar
            MostrarZonasEnMapa();
        }

        // --- NUEVOS MÉTODOS PARA EL MAPA ---

        // Centrar el mapa en Jutiapa
        private void btnCentrarMapa_Click(object sender, EventArgs e)
        {
            gmap.Position = new PointLatLng(14.2901, -89.8930); // Jutiapa
            gmap.Zoom = 12;
        }

        // Buscar una dirección y centrar el mapa en ella
        private async void btnBuscarMapa_Click(object sender, EventArgs e)
        {
            string direccion = txtBuscarMapa.Text.Trim();
            if (direccion == "Buscar dirección") direccion = "";
            if (string.IsNullOrWhiteSpace(direccion))
            {
                MessageBox.Show("Ingresa una dirección para buscar.");
                return;
            }
            var punto = await GeocodificarDireccionAsync(direccion);
            if (punto != null)
            {
                gmap.Position = punto.Value;
                gmap.Zoom = 16;
            }
            else
            {
                MessageBox.Show("No se encontró la dirección.");
            }
        }

        // Placeholder para txtBuscarMapa
        private void txtBuscarMapa_GotFocus(object sender, EventArgs e)
        {
            if (txtBuscarMapa.Text == "Buscar dirección")
            {
                txtBuscarMapa.Text = "";
                txtBuscarMapa.ForeColor = Color.Black;
            }
        }

        private void txtBuscarMapa_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarMapa.Text))
            {
                txtBuscarMapa.Text = "Buscar dirección";
                txtBuscarMapa.ForeColor = Color.Gray;
            }
        }

        // --- FIN NUEVOS MÉTODOS ---

        // Placeholder para txtApiKey
        private void txtApiKey_GotFocus(object sender, EventArgs e)
        {
            if (txtApiKey.Text == "Pega aquí tu API Key de OpenAI")
            {
                txtApiKey.Text = "";
                txtApiKey.ForeColor = Color.Black;
            }
        }

        private void txtApiKey_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtApiKey.Text))
            {
                txtApiKey.Text = "Pega aquí tu API Key de OpenAI";
                txtApiKey.ForeColor = Color.Gray;
            }
        }

        // Método para guardar la API Key desde el TextBox
        private void btnGuardarApiKey_Click(object sender, EventArgs e)
        {
            openAIApiKey = txtApiKey.Text.Trim();
            if (string.IsNullOrEmpty(openAIApiKey) || openAIApiKey == "Pega aquí tu API Key de OpenAI")
            {
                MessageBox.Show("Por favor, ingresa una API Key válida.");
            }
            else
            {
                MessageBox.Show("API Key guardada en memoria.");
            }
        }

        // Clase para preguntas de examen
        public class PreguntaExamen
        {
            public int Id { get; set; }
            public string Pregunta { get; set; }
            public string OpcionA { get; set; }
            public string OpcionB { get; set; }
            public string OpcionC { get; set; }
            public string RespuestaCorrecta { get; set; }
        }

        // 1. Asistente AI con perfil (OpenAI API)
        public async Task<string> ConsultarAIAsync(string pregunta, string perfil = "")
        {
            if (string.IsNullOrWhiteSpace(openAIApiKey) || openAIApiKey == "Pega aquí tu API Key de OpenAI")
                return "No se ha ingresado la API Key de OpenAI. Por favor, ingrésala y guárdala.";

            string endpoint = "https://api.openai.com/v1/chat/completions";

            // Prompt personalizado según perfil
            string promptSistema = string.IsNullOrWhiteSpace(perfil)
                ? "Eres un asistente vial experto y amable. Responde de forma clara y útil."
                : $"Eres un asistente vial experto. Responde como si fueras un {perfil.ToLower()} y da consejos prácticos.";

            var mensajes = new[]
            {
                new { role = "system", content = promptSistema },
                new { role = "user", content = pregunta }
            };

            var body = new
            {
                model = "gpt-3.5-turbo",
                messages = mensajes,
                max_tokens = 200
            };

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openAIApiKey);
                    var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(endpoint, content);
                    if (!response.IsSuccessStatusCode)
                    {
                        if ((int)response.StatusCode == 401)
                            return "API Key inválida o sin permisos. Verifica tu clave de OpenAI.";
                        if ((int)response.StatusCode == 429)
                            return "Límite de uso de la API alcanzado. Intenta más tarde.";
                        return "Error al consultar la API: " + response.ReasonPhrase;
                    }
                    var json = await response.Content.ReadAsStringAsync();
                    dynamic result = JsonConvert.DeserializeObject(json);
                    if (result.choices == null || result.choices.Count == 0)
                        return "No se recibió respuesta de la IA.";
                    return result.choices[0].message.content.ToString();
                }
            }
            catch (Exception ex)
            {
                return "Error de conexión o API: " + ex.Message;
            }
        }

        // 2. Guardar interacción en la base de datos
        private void GuardarInteraccion(string pregunta, string respuesta)
        {
            using (var conn = new SqlConnection("Data Source=PC-GAMING-RAAS\\SQLEXPRESS;Initial Catalog=AIVialDB;Integrated Security=True"))
            {
                conn.Open();
                var cmd = new SqlCommand("INSERT INTO Interacciones (Pregunta, Respuesta) VALUES (@pregunta, @respuesta)", conn);
                cmd.Parameters.AddWithValue("@pregunta", pregunta);
                cmd.Parameters.AddWithValue("@respuesta", respuesta);
                cmd.ExecuteNonQuery();
            }
        }

        // 3. Evento para el botón Enviar (Asistente AI)
        private async void btnEnviar_Click(object sender, EventArgs e)
        {
            string pregunta = txtPregunta.Text.Trim();
            if (string.IsNullOrEmpty(pregunta))
            {
                MessageBox.Show("Por favor, ingresa una pregunta.");
                return;
            }
            string perfil = comboBoxPerfil.SelectedItem?.ToString() ?? "";

            btnEnviar.Enabled = false;
            btnEnviar.Text = "Consultando...";
            txtRespuesta.Text = "Consultando AI...";

            string respuesta = await ConsultarAIAsync(pregunta, perfil);

            txtRespuesta.Text = respuesta;
            GuardarInteraccion(pregunta, respuesta);

            btnEnviar.Enabled = true;
            btnEnviar.Text = "Enviar";
            txtPregunta.Text = ""; // Limpia el campo de pregunta
        }

        // 4. Historial de consultas
        private void btnHistorial_Click(object sender, EventArgs e)
        {
            using (var conn = new SqlConnection("Data Source=PC-GAMING-RAAS\\SQLEXPRESS;Initial Catalog=AIVialDB;Integrated Security=True"))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT TOP 10 Fecha, Pregunta, Respuesta FROM Interacciones ORDER BY Fecha DESC", conn);
                var reader = cmd.ExecuteReader();
                StringBuilder historial = new StringBuilder();
                while (reader.Read())
                {
                    historial.AppendLine($"[{reader.GetDateTime(0)}]");
                    historial.AppendLine($"Pregunta: {reader.GetString(1)}");
                    historial.AppendLine($"Respuesta: {reader.GetString(2)}");
                    historial.AppendLine(new string('-', 40));
                }
                MessageBox.Show(historial.ToString(), "Historial de Interacciones");
            }
        }

        // 5. Simulador de Examen (aleatorio)
        private void CargarPreguntas()
        {
            preguntas = new List<PreguntaExamen>();
            using (var conn = new SqlConnection("Data Source=PC-GAMING-RAAS\\SQLEXPRESS;Initial Catalog=AIVialDB;Integrated Security=True"))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT Id, Pregunta, OpcionA, OpcionB, OpcionC, RespuestaCorrecta FROM Examenes", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    preguntas.Add(new PreguntaExamen
                    {
                        Id = reader.GetInt32(0),
                        Pregunta = reader.GetString(1),
                        OpcionA = reader.GetString(2),
                        OpcionB = reader.GetString(3),
                        OpcionC = reader.GetString(4),
                        RespuestaCorrecta = reader.GetString(5)
                    });
                }
            }
            // Aleatorizar preguntas
            var rnd = new Random();
            preguntas = preguntas.OrderBy(x => rnd.Next()).ToList();
            preguntaActual = 0;
            correctas = 0;
            MostrarPregunta();
        }

        private void MostrarPregunta()
        {
            lblPuntaje.Text = $"Puntaje: {correctas} de {preguntas.Count}";
            if (preguntaActual < preguntas.Count)
            {
                var p = preguntas[preguntaActual];
                lblPreguntaExamen.Text = p.Pregunta;
                radioA.Text = p.OpcionA;
                radioB.Text = p.OpcionB;
                radioC.Text = p.OpcionC;
                radioA.Checked = false;
                radioB.Checked = false;
                radioC.Checked = false;
                // Restablece color de fondo
                lblPreguntaExamen.BackColor = Color.Transparent;
            }
            else
            {
                MessageBox.Show($"Examen finalizado. Respuestas correctas: {correctas} de {preguntas.Count}");
            }
        }

        private void btnResponder_Click(object sender, EventArgs e)
        {
            string seleccion = radioA.Checked ? "A" : radioB.Checked ? "B" : radioC.Checked ? "C" : "";
            if (string.IsNullOrEmpty(seleccion))
            {
                MessageBox.Show("Selecciona una opción antes de continuar.");
                return;
            }
            if (preguntaActual < preguntas.Count)
            {
                bool esCorrecta = seleccion == preguntas[preguntaActual].RespuestaCorrecta;
                if (esCorrecta)
                {
                    correctas++;
                    lblPreguntaExamen.BackColor = Color.LightGreen;
                }
                else
                {
                    lblPreguntaExamen.BackColor = Color.LightCoral;
                }
                lblPuntaje.Text = $"Puntaje: {correctas} de {preguntas.Count}";
                // Espera breve para mostrar el color
                var t = new Timer();
                t.Interval = 700;
                t.Tick += (s, args) =>
                {
                    t.Stop();
                    preguntaActual++;
                    MostrarPregunta();
                };
                t.Start();
            }
        }

        // Evento para repetir el examen
        private void btnRepetirExamen_Click(object sender, EventArgs e)
        {
            CargarPreguntas();
        }

        // 6. Reportar zona de riesgo (con manejo de errores y geocodificación)
        private async void btnReportarZona_Click(object sender, EventArgs e)
        {
            string ubicacion = txtUbicacionZona.Text.Trim();
            string descripcion = txtDescripcionZona.Text.Trim();
            if (ubicacion == "Ubicación de la zona") ubicacion = "";
            if (descripcion == "Descripción") descripcion = "";
            if (string.IsNullOrEmpty(ubicacion) || string.IsNullOrEmpty(descripcion))
            {
                MessageBox.Show("Por favor, ingresa la ubicación y descripción.");
                return;
            }
            await ReportarZonaRiesgo(ubicacion, descripcion);
        }

        // Geocodificación y guardado de zona de riesgo
        private async Task ReportarZonaRiesgo(string ubicacion, string descripcion)
        {
            try
            {
                PointLatLng? punto = await GeocodificarDireccionAsync(ubicacion);
                double? lat = punto?.Lat;
                double? lng = punto?.Lng;

                using (var conn = new SqlConnection("Data Source=PC-GAMING-RAAS\\SQLEXPRESS;Initial Catalog=AIVialDB;Integrated Security=True"))
                {
                    conn.Open();
                    var cmd = new SqlCommand("INSERT INTO ZonasRiesgo (Ubicacion, Descripcion, Latitud, Longitud) VALUES (@ubicacion, @descripcion, @lat, @lng)", conn);
                    cmd.Parameters.AddWithValue("@ubicacion", ubicacion);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@lat", (object)lat ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@lng", (object)lng ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Zona de riesgo reportada correctamente.");
                MostrarZonasEnMapa(); // Actualiza el mapa
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al reportar zona de riesgo: " + ex.Message);
            }
        }

        // Geocodificación usando Nominatim (OpenStreetMap)
        public async Task<PointLatLng?> GeocodificarDireccionAsync(string direccion)
        {
            string url = $"https://nominatim.openstreetmap.org/search?format=json&q={Uri.EscapeDataString(direccion)}";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("AI-VIAL-App");
                var response = await client.GetStringAsync(url);
                dynamic results = JsonConvert.DeserializeObject(response);
                if (results.Count > 0)
                {
                    double lat = double.Parse((string)results[0].lat, System.Globalization.CultureInfo.InvariantCulture);
                    double lon = double.Parse((string)results[0].lon, System.Globalization.CultureInfo.InvariantCulture);
                    return new PointLatLng(lat, lon);
                }
            }
            return null;
        }

        // 7. Mostrar zonas de riesgo (con manejo de errores)
        private void btnMostrarZonas_Click(object sender, EventArgs e)
        {
            MostrarZonasRiesgo();
            MostrarZonasEnMapa(); // También refresca el mapa
        }

        private void MostrarZonasRiesgo()
        {
            try
            {
                using (var conn = new SqlConnection("Data Source=PC-GAMING-RAAS\\SQLEXPRESS;Initial Catalog=AIVialDB;Integrated Security=True"))
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT Ubicacion, Descripcion, FechaReporte FROM ZonasRiesgo ORDER BY FechaReporte DESC", conn);
                    var reader = cmd.ExecuteReader();
                    StringBuilder zonas = new StringBuilder();
                    while (reader.Read())
                    {
                        zonas.AppendLine($"Ubicación: {reader.GetString(0)}");
                        zonas.AppendLine($"Descripción: {reader.GetString(1)}");
                        zonas.AppendLine($"Fecha: {reader.GetDateTime(2)}");
                        zonas.AppendLine(new string('-', 40));
                    }
                    MessageBox.Show(zonas.Length > 0 ? zonas.ToString() : "No hay zonas reportadas.", "Zonas de Riesgo Reportadas");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar zonas de riesgo: " + ex.Message);
            }
        }

        // Muestra los marcadores de zonas en el mapa
        private void MostrarZonasEnMapa()
        {
            if (gmap == null) return;
            gmap.Overlays.Clear();
            var zonasOverlay = new GMapOverlay("zonas");

            using (var conn = new SqlConnection("Data Source=PC-GAMING-RAAS\\SQLEXPRESS;Initial Catalog=AIVialDB;Integrated Security=True"))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT Ubicacion, Descripcion, Latitud, Longitud FROM ZonasRiesgo WHERE Latitud IS NOT NULL AND Longitud IS NOT NULL", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    double lat = reader.GetDouble(2);
                    double lng = reader.GetDouble(3);
                    string tooltip = $"{reader.GetString(0)}\n{reader.GetString(1)}";
                    var marker = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
                        new PointLatLng(lat, lng),
                        GMap.NET.WindowsForms.Markers.GMarkerGoogleType.red_dot
                    );
                    marker.ToolTipText = tooltip;
                    zonasOverlay.Markers.Add(marker);
                }
            }
            gmap.Overlays.Add(zonasOverlay);
        }

        // 8. Modo educativo (trivia)
        private async void btnTrivia_Click(object sender, EventArgs e)
        {
            string prompt = "Genera una trivia de educación vial para niños de primaria en Jutiapa.";
            string trivia = await ConsultarAIAsync(prompt, "Modo Educativo");
            MessageBox.Show(trivia, "Trivia Educativa");
        }
    }
}
