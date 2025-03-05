using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgenciaAutomotiva
{
    public partial class FormularioMostraVeiculo : Form
    {
        public FormularioMostraVeiculo()
        {
            InitializeComponent();
            InicializarCombustiveis(cbCombustivel);
        }

        private void InicializarCombustiveis(ComboBox cb)
        {
            cb.DataSource = Enum.GetValues(typeof(Combustivel));
            cb.SelectedIndex = -1;
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            var id = Convert.ToInt32("0" + txtId.Text);
            if(id > 0)
                BuscarVeiculoPorId(id);
            txtId.Enabled = false;
        }

        private void BuscarVeiculoPorId(int id)
        {
            try
            {
                var veiculo = new Veiculo();
                veiculo = veiculo.GetVeiculo(id);

                if (veiculo != null)
                {
                    txtId.Text = veiculo.Id.ToString();
                    txtMarca.Text = veiculo.marca.ToString();
                    txtModelo.Text = veiculo.modelo.ToString();
                    txtAno.Text = veiculo.ano.ToString();
                    txtFabricacao.Text = veiculo.fabricacao.ToString();
                    txtCor.Text = veiculo.cor.ToString();
                    txtValor.Text = veiculo.valor.ToString();

                    if (veiculo.combustivel > 0)
                        cbCombustivel.Text = ((Combustivel)veiculo.combustivel).ToString();

                    if (veiculo.automatico)
                        cbAutomatico.Text = "Sim";
                    else
                        cbAutomatico.Text = "Não";

                    Console.WriteLine(veiculo.situacao);
                    if (Convert.ToBoolean(veiculo.situacao))
                        cbSituacao.Text = "Sim";
                    else if (!Convert.ToBoolean(veiculo.situacao))
                        cbSituacao.Text = "Não";

                    

                }
                else
                    MessageBox.Show("Veículo não encontrado!");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void buscarToolStripButton_Click(object sender, EventArgs e)
        {
            var RecebeVeiculoParametro = new Parametro();
            RecebeVeiculoParametro.Id = 0;

            using (FormularioBuscaVeiculo fbv = new FormularioBuscaVeiculo(RecebeVeiculoParametro))
            {
                fbv.ShowDialog();
                if (RecebeVeiculoParametro.Id > 0)
                {
                    BuscarVeiculoPorId(RecebeVeiculoParametro.Id);
                    txtMarca.Focus();
                    txtId.Enabled = false;
                }

            }

        }

        private void novoToolStripButton_Click(object sender, EventArgs e)
        {
            LimparForm();
            txtId.Enabled = true;
            txtId.Focus();
        }

        private void LimparForm()
        {
            txtId.Clear();
            txtMarca.Clear();
            txtModelo.Clear();
            txtAno.Clear();
            txtFabricacao.Clear();
            txtCor.Clear();
            txtValor.Clear();
            cbCombustivel.SelectedIndex = -1;
            cbSituacao.SelectedIndex = -1;
            cbAutomatico.SelectedIndex = -1;
        }

        private void salvarToolStripButton_Click(object sender, EventArgs e)
        {
            if (ValidarFormulario())
            {
                if (Salvar())
                {
                    LimparForm();
                    txtId.Enabled = true;
                    txtId.Focus();
                }
            }
        }

        private bool Salvar()
        {
            var veiculo = new Veiculo();

            veiculo.Id = Convert.ToInt32("0" + txtId.Text);
            veiculo.marca = txtMarca.Text.ToString();
            veiculo.modelo = txtModelo.Text.ToString();
            veiculo.ano = Convert.ToInt16("0" + txtAno.Text);
            veiculo.fabricacao = Convert.ToInt16("0" + txtFabricacao.Text);
            veiculo.cor = txtCor.Text.ToString();
            veiculo.valor = Convert.ToDecimal("0" + txtValor.Text);

            if (cbCombustivel.SelectedItem != null)
                veiculo.combustivel = (byte)cbCombustivel.SelectedItem;

            if (cbAutomatico.Text == "Sim")
                veiculo.automatico = true;
            else
                veiculo.automatico = false;

            if (cbSituacao.Text == "Sim")
                veiculo.situacao = true;
            else
                veiculo.situacao = false;

            try
            {
                veiculo.Salvar(veiculo);
                MessageBox.Show("Gravado com sucesso!");
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return false;
            }

        }

        private bool ValidarFormulario() 
        {
            if (txtMarca.Text == "")
            {
                MessageBox.Show("Informe a marca do veículo!");
                txtMarca.Focus();
                return false;
            }
            else if (txtModelo.Text == "")
            {
                MessageBox.Show("Informe o modelo do veículo!");
                txtModelo.Focus();
                return false;
            }
            else if (Convert.ToInt32("0" + txtAno.Text) == 0)
            {
                MessageBox.Show("Informe o ano do veículo!");
                txtAno.Focus();
                return false;
            }
            else if (Convert.ToInt32("0" + txtFabricacao.Text) == 0)
            {
                MessageBox.Show("Informe o ano de fabricação do veículo!");
                txtFabricacao.Focus();
                return false;
            }
            else if (txtCor.Text == "")
            {
                MessageBox.Show("Informe a cor do veículo!");
                txtCor.Focus();
                return false;
            }
            else if (Convert.ToDecimal("0" + txtValor.Text) == 0)
            {
                MessageBox.Show("Informe o valor do veículo!");
                txtValor.Focus();
                return false;
            }
            else if (cbCombustivel.Text == "")
            {
                MessageBox.Show("Informe o tipo de combustível do veículo!");
                cbCombustivel.Focus();
                return false;
            }
            else if (cbAutomatico.Text == "")
            {
                MessageBox.Show("Informe o tipo de câmbio do veículo!");
                cbAutomatico.Focus();
                return false;
            }
            else if (cbSituacao.Text == "")
            {
                MessageBox.Show("Informe se o veículo está ativo!");
                cbSituacao.Focus();
                return false;
            }

            return true;

        }

        private void deletarToolStripButton_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32("0" + txtId.Text) > 0)
            {
                DialogResult resposta;
                resposta = MessageBox.Show("Deseja excluir este veículo?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (resposta == DialogResult.Yes)
                {
                    var veiculo = new Veiculo();

                    veiculo.Id = Convert.ToInt32(txtId.Text);

                    veiculo.Excluir(veiculo);

                    LimparForm();
                    txtId.Enabled = true;
                    txtId.Focus();
                }
            }
            else
                MessageBox.Show("Selecione um veículo!", "Excluir", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
            

