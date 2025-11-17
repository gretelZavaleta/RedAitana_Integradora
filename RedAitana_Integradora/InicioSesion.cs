namespace RedAitana_Integradora
{
    public partial class InicioSesion : Form
    {
        public InicioSesion()
        {
            InitializeComponent();

            // Puedes usar esto si decides controlar el tipo desde el combo
            comboBox1.Items.Add("Administrador");
            comboBox1.Items.Add("Usuario");
            comboBox1.SelectedIndex = -1;
        }

        private void InicioSesion_Load(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string username = comboBox1.SelectedItem?.ToString();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(password)) 
            {
                MessageBox.Show("Complete todos los campos.");
                return;
            }
            if (ValidarCredencial.IniciarSesion(username, password))
            {
                MessageBox.Show($"Bienvenido {ValidarCredencial.TipoUsuario}");
                var ventanaBienvenido = new Bienvenido();
                ventanaBienvenido.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas.");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra la ventana de login
        }

        private void label1_Click(object sender, EventArgs e) { }

        private void txtPassword_TextChanged(object sender, EventArgs e) { }
    }
}

