﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GestaoClinicaEnfermagemProjetoInformatico
{
    public partial class FormLocalizacaoDorDopplerArterialVenoso : Form
    {
        Paciente paciente = null;



        Point point;
        public FormLocalizacaoDorDopplerArterialVenoso(Paciente ut)
        {
            InitializeComponent();
            paciente = ut;

            label1.Text = "Nome do Utente: " + paciente.Nome;
        }

       
    

        
        

     

       

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            var bmp = new Bitmap(GestaoClinicaEnfermagemProjetoInformatico.Properties.Resources.identificacaoAnatomica1_jpg);
            pictureBoxCorpo.Image = bmp;
            pictureBoxCorpo.Controls.Clear();
            textBox1.Clear();
            pictureBoxCorpo.Refresh();
        }

        private void hora_Tick(object sender, EventArgs e)
        {
            lblHora.Text = "Hora " + DateTime.Now.ToLongTimeString();
            lblDia.Text = DateTime.Now.ToString("dddd, dd " + "'de '" + "MMMM" + "' de '" + "yyyy");
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            VerLocalizacaoDorDopplerArterialVenoso verLocalizacaoDorDopplerArterialVenoso = new VerLocalizacaoDorDopplerArterialVenoso(paciente);
            verLocalizacaoDorDopplerArterialVenoso.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string localizacaoDor = textBox1.Text;


            try
            {
                SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SiltesSaude;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                connection.Open();

                string queryInsertData = "INSERT INTO LocalizacaoDorDopplerArterialVenoso(idPaciente,localizacao) VALUES(@idPaciente,@localizacao);";
                SqlCommand sqlCommand = new SqlCommand(queryInsertData, connection);

                sqlCommand.Parameters.AddWithValue("@idPaciente", paciente.IdPaciente);

                sqlCommand.Parameters.AddWithValue("@localizacao", localizacaoDor);


                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Dados Localizacao dor registados com Sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                connection.Close();
                connection.Open();
            }
            catch (SqlException excep)
            {

                MessageBox.Show("Por erro interno é impossível registar os dados da localizacao da dor", excep.Message);
            }
        }

        private void btnVoltar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBoxCorpo_MouseClick(object sender, MouseEventArgs e)
        {
            TextBox textBox = new TextBox();


            textBox.Location = PointToScreen(e.Location);

            pictureBoxCorpo.Controls.Add(textBox);
            textBox1.Clear();

            // string dor = textBox.Text;
            for (int i = 0; i < this.pictureBoxCorpo.Controls.Count; i++)
            {

                if (this.pictureBoxCorpo.Controls[i] is TextBox)
                {
                    TextBox txtserial = (TextBox)this.pictureBoxCorpo.Controls[i];
                    string value = txtserial.Text;
                    textBox1.Text += value.ToString();
                    textBox1.AppendText("\r\n\t");

                }
            }
        }
    }
}

