﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestaoClinicaEnfermagemProjetoInformatico
{
    public partial class ForgotPassword : Form
    {
       private Enfermeiro enfermeiro = null;
        SqlConnection conn = new SqlConnection();
        SqlCommand com = new SqlCommand();
        public ForgotPassword(Enfermeiro enf)
        {
            InitializeComponent();
            enfermeiro = enf;
                        conn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SiltesSaude;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        }

        private void btnAlterarPassword_Click(object sender, EventArgs e)
        {
            if (VerificarDadosInseridos())
            {
                try
                {
                    SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SiltesSaude;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                    SqlCommand cmd = new SqlCommand("UPDATE Enfermeiro SET password = @Password, passwordDefault = 0 WHERE IdEnfermeiro = @IdEnfermeiro", conn);
                    cmd.Parameters.AddWithValue("@Password", Validacoes.CalculaHash(txtConfirmarNovaPassword.Text));
                    cmd.Parameters.AddWithValue("@IdEnfermeiro", enfermeiro.IdEnfermeiro);

                    conn.Open();

                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Palavra passe mudada com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception)
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    MessageBox.Show("Por erro interno é impossível alterar a palavra passe", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("As palavras passes não correspodem, volte a insesir", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Boolean VerificarDadosInseridos()
        {

            string password = txtNovaPassword.Text;
            string confirmaPassword = txtConfirmarNovaPassword.Text;
            if (!ValidarForcaSenha() && (password == confirmaPassword))
            {
                MessageBox.Show("A palavra passe tem que conter no minimo 6 caracteres, dos quais devem ser: \nNúmeros, letras maiúsculas e minusculas", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtNovaPassword.Text != txtConfirmarNovaPassword.Text)
            {
                MessageBox.Show("As palavras passes não coincidem.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public Boolean ValidarForcaSenha()
        {
            if (string.IsNullOrEmpty(txtNovaPassword.Text) || txtNovaPassword.Text.Length < 6)
            {
                return false;
            }
            if (!Regex.IsMatch(txtNovaPassword.Text, @"\d") || !Regex.IsMatch(txtNovaPassword.Text, "[a-zA-Z]"))
            {
                return false;
            }
            return true;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
