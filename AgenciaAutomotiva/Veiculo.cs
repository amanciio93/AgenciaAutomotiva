using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgenciaAutomotiva
{
    public class Veiculo : tb_veiculos
    {
        public Veiculo GetVeiculo(int id)
        {
            using (var dbContext = new AgenciaAutomotivaBDEntities())
            {
                var entidadeVeiculo = dbContext.tb_veiculos.Find(id);

                if (entidadeVeiculo != null)
                {
                    var veiculo = new Veiculo();
                    veiculo.Id = entidadeVeiculo.Id;
                    veiculo.marca = entidadeVeiculo.marca;
                    veiculo.modelo = entidadeVeiculo.modelo;
                    veiculo.ano = entidadeVeiculo.ano;
                    veiculo.fabricacao = entidadeVeiculo.fabricacao;
                    veiculo.cor = entidadeVeiculo.cor;
                    veiculo.valor = entidadeVeiculo.valor;
                    veiculo.combustivel = entidadeVeiculo.combustivel;
                    veiculo.automatico = entidadeVeiculo.automatico;
                    veiculo.situacao = entidadeVeiculo.situacao;
                    return veiculo;
                }
                else
                    return null;
            }
        }

        public static List<Veiculo> GetVeiculos(string marca)
        {
            using (var dbContext = new AgenciaAutomotivaBDEntities())
            {
                var entidadeListaVeiculos = dbContext.tb_veiculos.Where(v => v.marca.Contains(marca));

                if (entidadeListaVeiculos != null)
                {
                    var listaVeiculos = new List<Veiculo>();
                    foreach (var item in entidadeListaVeiculos)
                    {
                        listaVeiculos.Add(new Veiculo
                        {
                            Id = item.Id,
                            marca = item.marca,
                            modelo = item.modelo,
                            ano = item.ano,
                            fabricacao = item.fabricacao,
                            cor = item.cor,
                            combustivel = item.combustivel,
                            automatico = item.automatico,
                            valor = item.valor,
                            situacao = item.situacao
                        });
                    }
                    return listaVeiculos;
                }
                else
                    return null;
            }
        }

        public void Salvar(Veiculo veiculo)
        {
            using (var dbContext = new AgenciaAutomotivaBDEntities())
            {
                var entidadeVeiculo = new tb_veiculos();

                entidadeVeiculo.Id = veiculo.Id;
                entidadeVeiculo.marca = veiculo.marca;
                entidadeVeiculo.modelo = veiculo.modelo;
                entidadeVeiculo.ano = veiculo.ano;
                entidadeVeiculo.cor = veiculo.cor;
                entidadeVeiculo.fabricacao = veiculo.fabricacao;
                entidadeVeiculo.valor = veiculo.valor;
                entidadeVeiculo.combustivel = veiculo.combustivel;
                entidadeVeiculo.automatico = veiculo.automatico;
                entidadeVeiculo.situacao = veiculo.situacao;

                if (entidadeVeiculo.Id == 0)
                    dbContext.tb_veiculos.Add(entidadeVeiculo);
                else
                    dbContext.Entry(entidadeVeiculo).State = System.Data.Entity.EntityState.Modified;

                dbContext.SaveChanges();

            }
        }

        public void Excluir(Veiculo veiculo)
        {
            using (var dbContext = new AgenciaAutomotivaBDEntities())
            {
                var entidadeVeiculo = new tb_veiculos();

                entidadeVeiculo.Id = veiculo.Id;
                var entry = dbContext.Entry(entidadeVeiculo);

                if (entry.State == System.Data.Entity.EntityState.Detached)
                    dbContext.tb_veiculos.Attach(entidadeVeiculo);

                dbContext.tb_veiculos.Remove(entidadeVeiculo);
                dbContext.SaveChanges();
            }
        }
    
    }

    public enum Combustivel : byte
    { 
        Gasolina = 1,
        Álcool = 2,
        Flex = 3,
        Diesel = 4,
        Gás = 5,
        Elétrico = 6,
        Híbrido = 7
    }

}
