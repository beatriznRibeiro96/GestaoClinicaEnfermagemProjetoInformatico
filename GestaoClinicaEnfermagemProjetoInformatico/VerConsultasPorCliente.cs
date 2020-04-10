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
    public partial class VerConsultasPorCliente : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand com = new SqlCommand();
        private Enfermeiro enfermeiro = null;
        private List<AgendamentoConsultaGridView> auxiliar = new List<AgendamentoConsultaGridView>();
        private List<AgendamentoConsultaGridView> agendamentos = new List<AgendamentoConsultaGridView>();
        public VerConsultasPorCliente(Enfermeiro enf)
        {
            InitializeComponent();
            enfermeiro = enf;
            conn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SiltesSaude;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            UpdateGridViewConsultas();
        }

        private void ConsultasPorCliente_Load(object sender, EventArgs e)
        {

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var resposta = MessageBox.Show("Tem a certeza que deseja sair da aplicação?", "Fechar Aplicação!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resposta == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else 
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void hora_Tick(object sender, EventArgs e)
        {
            lblHora.Text = "Hora " + DateTime.Now.ToLongTimeString();
            lblDia.Text = DateTime.Now.ToString("dddd, dd " + "'de '" + "MMMM" + "' de '" + "yyyy");
        }

        private void dataConsulta_ValueChanged(object sender, EventArgs e)
        {
            UpdateGridViewConsultas();
        }

        public void UpdateGridViewConsultas()
        {
            agendamentos.Clear();
            dataGridViewConsultas.DataSource = new List<AgendamentoConsultaGridView>();

            conn.Open();
            com.Connection = conn;

            SqlCommand cmd = new SqlCommand("select agendamento.dataProximaConsulta,  agendamento.horaProximaConsulta, p.Nome, p.Nif from AgendamentoConsulta agendamento INNER JOIN Paciente p ON agendamento.IdPaciente = p.IdPaciente WHERE agendamento.IdEnfermeiro =  " + enfermeiro.IdEnfermeiro + " AND ConsultaRealizada= 0 ORDER BY agendamento.dataProximaConsulta, agendamento.horaProximaConsulta", conn);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string dataConsulta = DateTime.ParseExact(reader["dataProximaConsulta"].ToString(), "dd/MM/yyyy HH:mm:ss", null).ToString("dd/MM/yyyy");

                AgendamentoConsultaGridView agendamento = new AgendamentoConsultaGridView
                {
                    dataProximaConsulta = dataConsulta,
                    horaProximaConsulta = (string)reader["horaProximaConsulta"],
                    NomePaciente = (string)reader["Nome"],
                    NifPaciente = Convert.ToDouble(reader["Nif"]),
                };
                agendamentos.Add(agendamento);
            }
            auxiliar = agendamentos;
            var bindingSource1 = new System.Windows.Forms.BindingSource { DataSource = filtrosDePesquisa() };
            dataGridViewConsultas.DataSource = bindingSource1;
           // dataGridViewConsultas.DataSource = filtrosDePesquisa();
            dataGridViewConsultas.Columns[0].HeaderText = "Hora Consulta";
            dataGridViewConsultas.Columns[1].HeaderText = "Data Consulta";
            dataGridViewConsultas.Columns[2].HeaderText = "Nome Utente";
            dataGridViewConsultas.Columns[3].HeaderText = "Nif Paciente";
            //  auxiliar = agendamentos;
            conn.Close();
            filtrosDePesquisa();
        }

        private List<AgendamentoConsultaGridView> filtrosDePesquisa()
        {
            auxiliar = new List<AgendamentoConsultaGridView>();
            if (txtNIF.Text != "" && txtNome.Text == "")
            {
                foreach (AgendamentoConsultaGridView agendamentoConsulta in agendamentos)
                {
                    if (agendamentoConsulta.NifPaciente == Convert.ToDouble(txtNIF.Text))
                    {
                        auxiliar.Add(agendamentoConsulta);
                    }
                }
                return auxiliar;
            }
            if (txtNIF.Text == "" && txtNome.Text != "")
            {
                foreach (AgendamentoConsultaGridView agendamentoConsulta in agendamentos)
                {
                    if (agendamentoConsulta.NomePaciente.ToLower().Contains(txtNome.Text.ToLower()))
                    {
                        auxiliar.Add(agendamentoConsulta);
                    }
                }
                return auxiliar;
            }
            if (txtNIF.Text != "" && txtNome.Text != "")
            {
                foreach (AgendamentoConsultaGridView agendamentoConsulta in agendamentos)
                {
                    if (agendamentoConsulta.NomePaciente.ToLower().Contains(txtNome.Text.ToLower()) && agendamentoConsulta.NifPaciente == Convert.ToDouble(txtNIF.Text))
                    {
                        auxiliar.Add(agendamentoConsulta);
                    }
                }
                return auxiliar;
            }
            auxiliar = agendamentos;
            return agendamentos;
        }

        private void txtNome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dataGridViewConsultas.DataSource = filtrosDePesquisa();
            }
        }

        private void txtNIF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dataGridViewConsultas.DataSource = filtrosDePesquisa();
            }
        }
    }
}
