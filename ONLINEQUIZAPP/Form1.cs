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


namespace ONLINEQUIZAPP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const string ConnectionString = "Data Source=DESKTOP-IC75OQU;Initial Catalog=quizapplication;Persist Security Info=True;User ID=sa;Password=***********; Trusted_Connection=True";
        private const string CmdText = @"INSERT INTO [dbo].[Compare]
           ([value])
     VALUES
           ('correct')";
        string submittedanswer;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            submittedanswer = "Arithmetic logic unit";
          /*  if (radioButton1.Checked == true)
            {
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
            }*/




        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            submittedanswer = "memory unit";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            submittedanswer = "control unit";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            submittedanswer = "DMA controller";
        }

        public void buttonsubmit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand(@"
INSERT INTO [dbo].[Submittedanswer1]
           ([sa])
     VALUES
           ('" + submittedanswer + "')", con);
            con.Open();
            _ = cmd.ExecuteNonQuery();
            con.Close();
            _ = MessageBox.Show("your ans is sub");
            SqlCommand cmd1 = new SqlCommand("select  sa, Answer.Ans from Answer INNER JOIN(SELECT TOP 1 sa from Submittedanswer1 ORDER BY ID DESC) as Submittedanswer ON  sa = Answer.Ans; ", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            _ = sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                SqlCommand cmd2 = new SqlCommand(CmdText, con);
                con.Open();
                _ = cmd2.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                SqlCommand cmd3 = new SqlCommand(@"INSERT INTO [dbo].[Compare]
           ([value])
     VALUES
           ('incorrect')", con);
                con.Open();
                _ = cmd3.ExecuteNonQuery();
                con.Close();
            }
            buttonsubmit.Enabled = false;

        }

        private void buttoncheck_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd4 = new SqlCommand("select top 1 value from Compare order by id desc", con);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd4);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            MessageBox.Show("your answer is " + dt1.Rows[0][0]);
            buttoncheck.Enabled = false;
        }

        private void buttonnext_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
