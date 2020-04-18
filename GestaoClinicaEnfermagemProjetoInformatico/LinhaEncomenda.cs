﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestaoClinicaEnfermagemProjetoInformatico
{
    public partial class LinhaEncomenda : Form
    {
        RegistarEncomendas registar = new RegistarEncomendas();
        Encomendas encomendas = null;
        SqlConnection conn = new SqlConnection();
        SqlCommand com = new SqlCommand();
        private List<ListarProdutos> listaProdutos = new List<ListarProdutos>();
        private List<ListarProdutos> listaEncomenda = new List<ListarProdutos>();

        public LinhaEncomenda(Encomendas enc, RegistarEncomendas registarEncomendas)
        {
            InitializeComponent();
            encomendas = enc;
            registar = registarEncomendas;
            conn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SiltesSaude;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LinhaEncomenda_Load(object sender, EventArgs e)
        {
            UpdateListBox();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var resposta = MessageBox.Show("Tem a certeza que deseja sair da aplicação?", "Fechar Aplicação!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resposta == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (listaEncomenda.Count > 0)
            {
                try
                {
                    conn.Open();
                    foreach (var item in listaEncomenda)
                    {
                        string queryInsertData = "INSERT INTO LinhaEncomenda(quantidade,idProdutoStock,idEncomenda) VALUES(@Quantidade,@IdProdutoStock,@IdEncomenda);";
                        SqlCommand sqlCommand = new SqlCommand(queryInsertData, conn);
                        sqlCommand.Parameters.AddWithValue("@Quantidade", item.quant);
                        sqlCommand.Parameters.AddWithValue("@IdProdutoStock", item.id);
                        sqlCommand.Parameters.AddWithValue("@IdEncomenda", encomendas.IdEncomenda);
                        sqlCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("Encomenda registada com Sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    this.Close();
                }
                catch (SqlException excep)
                {
                    MessageBox.Show("Por erro interno é impossível registar a encomenda", excep.Message);
                }
            } 
            else
            {
                MessageBox.Show("A lista de ENcomenda não contem items. Para poder registar a encomenda, tem de ter pelo menos um item", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private Boolean VerificarDadosInseridos()
        {
            string quantidade = Convert.ToString(numericUpDownQuant.Value);


            if (quantidade == string.Empty)
            {
                MessageBox.Show("Campo Obrigatório, por favor preencha o campo!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            if (Convert.ToInt32(quantidade) <= 0)
            {
                MessageBox.Show("A quantidade tem de ser superior a 0. Por favor corrija!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ProdutosEmStock produtos = new ProdutosEmStock(this);
            produtos.Show();              
        }

        private void button1_Click(object sender, EventArgs e)
        {
            registar.Delete(encomendas.IdEncomenda);
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
        }

        public void UpdateListBox()
        {
            listaProdutos.Clear();
            conn.Open();
            com.Connection = conn;

            SqlCommand cmd = new SqlCommand("select IdProdutoStock, NomeProduto, precoUnitario, taxaIVA from ProdutoStock ORDER BY NomeProduto", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ListarProdutos produto = new ListarProdutos
                {
                    id = (int)reader["IdProdutoStock"],
                    nome = (string)reader["NomeProduto"],
                    preco = (decimal)reader["precoUnitario"],
                    iva = (int)reader["taxaIVA"]
                };
                produto.quant = 0;
                listaProdutos.Add(produto);
                listBoxProdutos.Items.Add(new ComboBoxItem
                {
                    Value = produto.id,
                    Text = produto.nome
                });
            }
           
            conn.Close();
        }

        private void listBoxProdutos_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (!VerificarDadosInseridos())
            {
                MessageBox.Show("Dados incorretos!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ComboBoxItem idProduto = (listBoxProdutos.SelectedItem as ComboBoxItem);
                listBoxEncomenda.Items.Add(idProduto);
                foreach (var produto in listaProdutos)
                {
                    if (produto.nome.Equals(idProduto.Text))
                    {
                        ListarProdutos listar = produto;
                        listar.quant = Convert.ToInt16(numericUpDownQuant.Value);
                        listaEncomenda.Add(listar);
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            
            ComboBoxItem idProduto = (listBoxEncomenda.SelectedItem as ComboBoxItem);
            listBoxEncomenda.Items.Remove(idProduto);
            foreach (var produto in listaProdutos)
            {
                if (produto.nome.Equals(idProduto.Text))
                {
                    ListarProdutos listar = produto;
                    listaEncomenda.Remove(listar);
                }
            }
        }
    }
}
