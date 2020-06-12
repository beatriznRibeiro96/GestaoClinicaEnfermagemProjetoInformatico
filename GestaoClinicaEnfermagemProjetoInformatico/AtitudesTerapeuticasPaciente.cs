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
    public partial class AtitudesTerapeuticasPaciente : Form
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand com = new SqlCommand();
        private Paciente paciente = new Paciente();
       // private List<ComboBoxItem> analises = new List<ComboBoxItem>();
      //  private List<ComboBoxItem> auxiliar = new List<ComboBoxItem>();
       // private List<AnaliseLaboratorialPaciente> analisePaciente = new List<AnaliseLaboratorialPaciente>();
        private ErrorProvider errorProvider = new ErrorProvider();
        public AtitudesTerapeuticasPaciente(Paciente pac)
        {
            InitializeComponent();
            paciente = pac;
            label1.Text = "Nome do Utente: " + paciente.Nome;
            conn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SiltesSaude;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            //dataDiagnostico.MaxDate = DateTime.Today;
        }

        private void AtitudesTerapeuticasPaciente_Load(object sender, EventArgs e)
        {
            errorProvider.ContainerControl = this;
            errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            if(paciente.Sexo == "Feminino")
            {
                btnColpocitologia.Visible = true;
                btnDIU.Visible = true;
                btnImplanteContracetivo.Visible = true;
            }
            if (paciente.Sexo != "Feminino")
            {
                btnColpocitologia.Visible = false;
                btnDIU.Visible = false;
                btnImplanteContracetivo.Visible = false;
            }
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

        private void button12_Click(object sender, EventArgs e)
        {
            AdministrarMedicacaoPaciente administrarMedicacaoPaciente = new AdministrarMedicacaoPaciente(paciente);
            administrarMedicacaoPaciente.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdicionarAlgariacaoPaciente adicionarAlgariacaoPaciente = new AdicionarAlgariacaoPaciente(paciente);
            adicionarAlgariacaoPaciente.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdicionarAspiracaoSecrecaoPaciente adicionarAspiracaoSecrecaoPaciente = new AdicionarAspiracaoSecrecaoPaciente(paciente);
            adicionarAspiracaoSecrecaoPaciente.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdicionarCateterismoPaciente adicionarCateterismoPaciente = new AdicionarCateterismoPaciente(paciente);
            adicionarCateterismoPaciente.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AdicionarColheitaUrinaPaciente adicionarColheitaUrinaPaciente = new AdicionarColheitaUrinaPaciente(paciente);
            adicionarColheitaUrinaPaciente.Show();
        }

        private void btnColpocitologia_Click(object sender, EventArgs e)
        {
            AdicionarColpocitologiaPaciente adicionarColpocitologiaPaciente = new AdicionarColpocitologiaPaciente(paciente);
            adicionarColpocitologiaPaciente.Show();
        }

        private void btnDIU_Click(object sender, EventArgs e)
        {
            AdicionarColocacaoDIUPaciente adicionarColocacaoDIUPaciente = new AdicionarColocacaoDIUPaciente(paciente);
            adicionarColocacaoDIUPaciente.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AdicionarDrenagemLocasPaciente adicionarDrenagemLocasPaciente = new AdicionarDrenagemLocasPaciente(paciente);
            adicionarDrenagemLocasPaciente.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AdicionarDesbridamentoPaciente adicionarDesbridamentoPaciente = new AdicionarDesbridamentoPaciente(paciente);
            adicionarDesbridamentoPaciente.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AdicionarENGPaciente adicionarENGPaciente = new AdicionarENGPaciente(paciente);
            adicionarENGPaciente.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            AdicionarFlebografiaPaciente adicionarFlebografiaPaciente = new AdicionarFlebografiaPaciente(paciente);
            adicionarFlebografiaPaciente.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            AdicionarInalacaoPaciente adicionarInalacaoPaciente = new AdicionarInalacaoPaciente(paciente);
            adicionarInalacaoPaciente.Show();
        }

        private void btnImplanteContracetivo_Click(object sender, EventArgs e)
        {
            AdicionarImplanteContracetivoPaciente adicionarImplanteContracetivoPaciente = new AdicionarImplanteContracetivoPaciente(paciente);
            adicionarImplanteContracetivoPaciente.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            AdicionarLavagemAuricular adicionarLavagemAuricular = new AdicionarLavagemAuricular(paciente);
            adicionarLavagemAuricular.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            AdicionarLavagemOcular adicionarLavagemOcular = new AdicionarLavagemOcular(paciente);
            adicionarLavagemOcular.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Falta Implementar!!!");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Falta Implementar!!!");
        }

        private void button29_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Falta Implementar!!!");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Falta Implementar!!!");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Falta Implementar!!!");
        }

        private void button27_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Falta Implementar!!!");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Falta Implementar!!!");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            AdicionarLavagemVesicalPaciente adicionarLavagemVesicalPaciente = new AdicionarLavagemVesicalPaciente(paciente);
            adicionarLavagemVesicalPaciente.Show();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            AdicionarMonitorizacaoECGPaciente adicionarMonitorizacaoECGPaciente = new AdicionarMonitorizacaoECGPaciente(paciente);
            adicionarMonitorizacaoECGPaciente.Show();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            AdicionarPressoterapiaPaciente adicionarPressoterapiaPaciente = new AdicionarPressoterapiaPaciente(paciente);
            adicionarPressoterapiaPaciente.Show();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            AdicionarSuturasPaciente adicionarSuturasPaciente = new AdicionarSuturasPaciente(paciente);
            adicionarSuturasPaciente.Show();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            AdicionarAvaliacaoAcuidadeVisual adicionarAvaliacaoAcuidadeVisual = new AdicionarAvaliacaoAcuidadeVisual(paciente);
            adicionarAvaliacaoAcuidadeVisual.Show();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            AdicionarTricotomiaPaciente adicionarTricotomiaPaciente = new AdicionarTricotomiaPaciente(paciente);
            adicionarTricotomiaPaciente.Show();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            AdicionarZaragatoaOnofaringePaciente adicionarZaragatoaOnofaringePaciente = new AdicionarZaragatoaOnofaringePaciente(paciente);
            adicionarZaragatoaOnofaringePaciente.Show();
        }
    }
}
