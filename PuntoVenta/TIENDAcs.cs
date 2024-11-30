using MySql.Data.MySqlClient;
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
    public partial class TIENDA : Form
    {
        public TIENDA()
        {

            InitializeComponent();
            CargarClientes();
            CargarProductos();
            InicializarColumnasDataGridView();
        }
        

        private void CargarClientes()
        {
            cbxNombre.DataSource = CustomerDAO.GetAllCustomers();
            cbxNombre.DisplayMember = "CompanyName";
            cbxNombre.ValueMember = "CustomerID";
        }
        private void CargarProductos()
        {
            cbxNombreProducto.DataSource = ProductDAO.GetAllProducts();
            cbxNombreProducto.DisplayMember = "ProductName";
            cbxNombreProducto.ValueMember = "ProductID";
        }
        private void InicializarColumnasDataGridView()
        {
            if (dtvTienda.Columns.Count == 0) 
            {
                dtvTienda.Columns.Add("ProductID", "ID Producto");
                dtvTienda.Columns.Add("ProductName", "Nombre del Producto");
                dtvTienda.Columns.Add("Quantity", "Cantidad");
                dtvTienda.Columns.Add("UnitPrice", "Precio Unitario");
                dtvTienda.Columns.Add("Subtotal", "Subtotal");
            }
        }


        private void ActualizarTotales()
        {
            decimal subtotal = 0;

            foreach (DataGridViewRow row in dtvTienda.Rows)
            {
                subtotal += Convert.ToDecimal(row.Cells["Subtotal"].Value);
            }

            decimal iva = subtotal * 0.16m; 
            decimal total = subtotal + iva;

            txtSubtotal.Text = subtotal.ToString("C");
            txtIVA.Text = iva.ToString("C");
            txtTotal.Text = total.ToString("C");
        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbxNombre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxNombre.SelectedValue != null)
            {
                txtIdCliente.Text = cbxNombre.SelectedValue.ToString();
            }
        }

        private void cbxNombreProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxNombreProducto.SelectedValue != null)
            {
                txtIdProducto.Text = cbxNombreProducto.SelectedValue.ToString();
            }
        }

        private void dtvTienda_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdProducto.Text) || string.IsNullOrEmpty(txtCantidad.Text))
            {
                MessageBox.Show("Debe seleccionar un producto y especificar la cantidad.");
                return;
            }

            int productId = Convert.ToInt32(txtIdProducto.Text); // ID del producto seleccionado
            int quantity = Convert.ToInt32(txtCantidad.Text);    // Cantidad ingresada por el usuario

            // Obtener el precio del producto usando ProductDAO
            decimal unitPrice = ProductDAO.GetProductPrice(productId);  // Uso estático
            decimal subtotal = quantity * unitPrice;

            // Agregar los datos al DataGridView
            dtvTienda.Rows.Add(productId, cbxNombreProducto.Text, quantity, unitPrice, subtotal);

            // Actualizar los totales
            ActualizarTotales();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dtvTienda.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dtvTienda.SelectedRows)
                {
                    dtvTienda.Rows.Remove(row);
                }
                ActualizarTotales();
            }
            else
            {
                MessageBox.Show("Seleccione un producto para eliminar.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dtvTienda.Rows.Count == 0)
            {
                MessageBox.Show("No hay productos en el carrito para realizar la compra.");
                return;
            }

            DataTable productos = new DataTable();
            productos.Columns.Add("ProductID", typeof(int));
            productos.Columns.Add("ProductName", typeof(string));
            productos.Columns.Add("Quantity", typeof(int));
            productos.Columns.Add("UnitPrice", typeof(decimal));
            productos.Columns.Add("Subtotal", typeof(decimal));

            foreach (DataGridViewRow row in dtvTienda.Rows)
            {
                productos.Rows.Add(
                    row.Cells["ProductID"].Value,
                    row.Cells["ProductName"].Value,
                    row.Cells["Quantity"].Value,
                    row.Cells["UnitPrice"].Value,
                    row.Cells["Subtotal"].Value
                );
            }

            // Aquí se podría llamar a un método para registrar la venta usando `SalesDAO`.
            MessageBox.Show("Compra realizada con éxito.");
            dtvTienda.Rows.Clear();
            ActualizarTotales();


        }

        private void btnAgregarnuevo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdProductoNuevo.Text) || string.IsNullOrEmpty(txtNombreProductoNuevo.Text) || string.IsNullOrEmpty(txtPrecio.Text))
            {
                MessageBox.Show("Por favor, llena los campos de ID, Nombre y Precio del producto.");
                return;
            }

            int nuevoIdProducto = Convert.ToInt32(txtIdProductoNuevo.Text);
            string nuevoNombreProducto = txtNombreProductoNuevo.Text;
            decimal nuevoPrecioProducto = Convert.ToDecimal(txtPrecio.Text);

            try
            {
                ProductDAO.AddNewProduct(nuevoIdProducto, nuevoNombreProducto, nuevoPrecioProducto);  // Uso estático
                MessageBox.Show("Producto agregado con éxito.");
                CargarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar el producto: {ex.Message}");
            }
        }
    }
}
