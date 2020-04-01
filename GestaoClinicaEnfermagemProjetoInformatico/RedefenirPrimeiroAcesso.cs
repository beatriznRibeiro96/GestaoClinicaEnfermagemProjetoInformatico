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
using System.Text.RegularExpressions;

namespace GestaoClinicaEnfermagemProjetoInformatico
{
    public partial class PrimeiroAcesso : Form
    {
        string username = SendCode.to;
        private Enfermeiro enfermeiro = new Enfermeiro();
        public PrimeiroAcesso(Enfermeiro enf)
        {
            InitializeComponent();
            enfermeiro = enf;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNovaPassoword.Text == txtConfirmarNovaPassword.Text)
            {
                try
                {
                    SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SiltesSaude;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                    SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Enfermeiro] SET [password] = '" + CalculaHash(txtConfirmarNovaPassword.Text) + "', [username] = '" + txtUsername.Text + "', [passwordDefault] = 0 WHERE [IdEnfermeiro] = '" + enfermeiro.IdEnfermeiro + "' ", conn);

                    conn.Open();

                    cmd.ExecuteNonQuery();
                  //  cmd1.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Passe mudada com sucesso!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }


            }
            else
            {
                MessageBox.Show("As palavras passes não correspodem, volte a intesir");
            }
        }

        public static string CalculaHash(string pass)
        {
            try
            {
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(pass);
                byte[] hash = md5.ComputeHash(inputBytes);
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString(); // Retorna senha criptografada 
            }
            catch (Exception)
            {
                return null; // Caso encontre erro retorna nulo
            }
        }


        public Boolean VerificarDadosInseridos()
        {
            string username = txtUsername.Text;
            string password = txtNovaPassoword.Text;
            string confirmaPassword = txtConfirmarNovaPassword.Text;
            if (!ValidarForcaSenha())
            {
                MessageBox.Show("A password tem que conter no minimo 6 caracteres, dos quais devem ser numeros, letras maiusculas e minusculas", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtNovaPassoword.Text != txtConfirmarNovaPassword.Text)
            {
                MessageBox.Show("As passwors não coincidem.", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public Boolean ValidarForcaSenha()
        {
            if (string.IsNullOrEmpty(txtNovaPassoword.Text) || txtNovaPassoword.Text.Length < 6)
            {
                return false;
            }
            if (!Regex.IsMatch(txtNovaPassoword.Text, @"\d") || !Regex.IsMatch(txtNovaPassoword.Text, "[a-zA-Z]"))
            {
                return false;
            }
            return true;
        }
    }
}