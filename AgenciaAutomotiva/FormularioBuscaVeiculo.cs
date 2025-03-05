using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgenciaAutomotiva
{
    public partial class FormularioBuscaVeiculo: Form
    {
        Parametro EnviaVeiculoParametro = new Parametro();
        public FormularioBuscaVeiculo(Parametro veiculoParametro)
        {
            InitializeComponent();
            this.EnviaVeiculoParametro = veiculoParametro;
        }

        private void btPesquisar_Click(object sender, EventArgs e)
        {
            BuscarVeiculoPorMarca(txtMarca.Text);
        }

        private void BuscarVeiculoPorMarca(string marca)
        {
            dataGridView1.DataSource = Veiculo.GetVeiculos(marca);
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            EnviaVeiculoParametro.Id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value);
        }

        private void selecionarToolStripButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
