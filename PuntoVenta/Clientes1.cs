using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoVenta
{
    public partial class Clientes1 : Form
    {
        public Clientes1()
        {
            InitializeComponent();
        }
        private CustomerDAO customerDAO = new CustomerDAO();
        private void CargarClientes()
        {
            // Obtenemos todos los clientes desde el DAO y los asignamos al DataGridView
            DataTable customers = CustomerDAO.GetAllCustomers(); // Acceso estático
            dtvClientes.DataSource = customers;

            // Asegúrate de que la columna 'CustomerID' esté oculta si no quieres mostrarla
            dtvClientes.Columns["CustomerID"].Visible = false;
        }
       
        private void Clientes1_Load(object sender, EventArgs e)
        {
            CargarClientes();
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text) || string.IsNullOrEmpty(txtDireccion.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }

            // Llamamos al DAO para agregar el cliente
            CustomerDAO.AddCustomer(txtNombre.Text, txtApellido.Text, txtDireccion.Text);
            MessageBox.Show("Cliente agregado.");

            // Recargar los clientes en el DataGridView
            CargarClientes();
        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            // Verificar si hay una fila seleccionada
            if (dtvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un cliente para eliminar.");
                return;
            }

            // Obtener el ID del cliente seleccionado
            string customerId = dtvClientes.SelectedRows[0].Cells["CustomerID"].Value.ToString();

            // Llamamos al DAO para eliminar el cliente
            CustomerDAO.DeleteCustomer(customerId);
            MessageBox.Show("Cliente eliminado.");

            // Recargar los clientes en el DataGridView
            CargarClientes();
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            // Verificar si hay una fila seleccionada
            if (dtvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un cliente para editar.");
                return;
            }

            // Obtener el ID del cliente seleccionado
            string customerId = dtvClientes.SelectedRows[0].Cells["CustomerID"].Value.ToString();

            // Llamamos al DAO para actualizar el cliente
            CustomerDAO.UpdateCustomer(customerId, txtNombre.Text, txtApellido.Text, txtDireccion.Text);
            MessageBox.Show("Cliente editado.");

            // Recargar los clientes en el DataGridView
            CargarClientes();
        }

        private void dtvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
