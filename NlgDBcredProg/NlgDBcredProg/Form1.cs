﻿using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;
using System.Diagnostics;

namespace NlgDBcredProg
{
    public partial class Form1 : Form

    {
        DataSet dataSet;
        SqlDataAdapter adName, adKredDog, adZaemwik, adKredDocum, adOsnSdelkVdch, adZalogPoruch, adOsnovnSd, adGrpObject, adDocsZalPor, adDopSogZalPor, adObjData;
        BindingSource bsName, bsKredDog, bsZaemwik, bsKredDocum, bsOsnSdelkVdch, bsZalogPoruch, bsOsnovnSd, bsGrpObject, bsDocsZalPor, bsDopSogZalPor, bsObjData;
        DataGridView gdName, gdKredDog, gdKredDocum, gdOsnSdelkVdch, gdZalogPoruch, gdOsnovnSd;
        SqlConnection connection = new SqlConnection(Program.connection);

        public Form1()
        {
            InitializeComponent();

            //SELECT FROM TABLES AREA 
            adName = new SqlDataAdapter("SELECT * FROM Name", Program.connection);
            adKredDog = new SqlDataAdapter("SELECT * FROM KredDog", Program.connection);
            adZaemwik = new SqlDataAdapter("SELECT * FROM Zaemwik", Program.connection);
            adKredDocum = new SqlDataAdapter("SELECT * FROM KredDocum", Program.connection);
            adOsnSdelkVdch = new SqlDataAdapter("SELECT * FROM OsnSdelkVdch", Program.connection);
            adZalogPoruch = new SqlDataAdapter("SELECT * FROM ZalogPoruch", Program.connection);
            adOsnovnSd = new SqlDataAdapter("SELECT * FROM OsnovnSd", Program.connection);
            adGrpObject = new SqlDataAdapter("SELECT * FROM GrpObject", Program.connection);
            adDocsZalPor = new SqlDataAdapter("SELECT * FROM DocsZalPor", Program.connection);
            adDopSogZalPor = new SqlDataAdapter("SELECT * FROM DopSogZalPor", Program.connection);
            adObjData = new SqlDataAdapter("SELECT * FROM ObjData", Program.connection);


            //CREATE DATASET WITH TABLES AREA 
            dataSet = new DataSet();
            adName.Fill(dataSet, "Name");
            adKredDog.Fill(dataSet, "KredDog");
            adZaemwik.Fill(dataSet, "Zaemwik");
            adKredDocum.Fill(dataSet, "KredDocum");
            adOsnSdelkVdch.Fill(dataSet, "OsnSdelkVdch");
            adZalogPoruch.Fill(dataSet, "ZalogPoruch");
            adOsnovnSd.Fill(dataSet, "OsnovnSd");
            adGrpObject.Fill(dataSet, "GrpObject");
            adDocsZalPor.Fill(dataSet, "DocsZalPor");
            adDopSogZalPor.Fill(dataSet, "DopSogZalPor");
            adObjData.Fill(dataSet, "ObjData");

            //RELATIONS IN DB AREA 
            dataSet.Relations.Add("Name-KredDog", dataSet.Tables["Name"].Columns["idName"], dataSet.Tables["KredDog"].Columns["id"]);
            dataSet.Relations.Add("Name-Zaemwik", dataSet.Tables["Name"].Columns["idName"], dataSet.Tables["Zaemwik"].Columns["id"]);  
            dataSet.Relations.Add("KredDog-KredDocum", dataSet.Tables["KredDog"].Columns["idKredDog"], dataSet.Tables["KredDocum"].Columns["id"]);
            dataSet.Relations.Add("KredDog-OsnSdelkVdch", dataSet.Tables["KredDog"].Columns["idKredDog"], dataSet.Tables["OsnSdelkVdch"].Columns["id"]);
            dataSet.Relations.Add("KredDog-ZalogPoruch", dataSet.Tables["KredDog"].Columns["idKredDog"], dataSet.Tables["ZalogPoruch"].Columns["id"]);
            dataSet.Relations.Add("ZalogPoruch-OsnovnSd", dataSet.Tables["ZalogPoruch"].Columns["idZalPor"], dataSet.Tables["OsnovnSd"].Columns["id"]);
            dataSet.Relations.Add("ZalogPoruch-DopSogZalPor", dataSet.Tables["ZalogPoruch"].Columns["idZalPor"], dataSet.Tables["DopSogZalPor"].Columns["id"]);
            dataSet.Relations.Add("ZalogPoruch-GrpObject", dataSet.Tables["ZalogPoruch"].Columns["idZalPor"], dataSet.Tables["GrpObject"].Columns["id"]);
            dataSet.Relations.Add("ZalogPoruch-DocsZalPor", dataSet.Tables["ZalogPoruch"].Columns["idZalPor"], dataSet.Tables["DocsZalPor"].Columns["id"]);   
            dataSet.Relations.Add("GrpObject-ObjData", dataSet.Tables["GrpObject"].Columns["idGrObj"], dataSet.Tables["ObjData"].Columns["id"]);   

            //BIND.SOURCE AREA 
            bsName = new BindingSource(dataSet, "Name");
            bsKredDog = new BindingSource(dataSet, "KredDog");
            bsZaemwik = new BindingSource(dataSet, "Zaemwik");
            bsKredDocum = new BindingSource(dataSet, "KredDocum");
            bsOsnSdelkVdch = new BindingSource(dataSet, "OsnSdelkVdch");
            bsZalogPoruch = new BindingSource(dataSet, "ZalogPoruch");
            bsOsnovnSd = new BindingSource(dataSet, "OsnovnSd");
            bsGrpObject = new BindingSource(dataSet, "GrpObject");
            bsDocsZalPor = new BindingSource(dataSet, "DocsZalPor");
            bsDopSogZalPor = new BindingSource(dataSet, "DopSogZalPor");
            bsObjData = new BindingSource(dataSet, "ObjData");

            //BIND.SOURCE WITH RELATIONS AREA 
            bsKredDog = new BindingSource(bsName, "Name-KredDog");
            bsZaemwik = new BindingSource(bsName, "Name-Zaemwik");
            bsKredDocum = new BindingSource(bsKredDog, "KredDog-KredDocum");
            bsOsnSdelkVdch = new BindingSource(bsKredDog, "KredDog-OsnSdelkVdch");
            bsZalogPoruch = new BindingSource(bsKredDog, "KredDog-ZalogPoruch");
            bsOsnovnSd = new BindingSource(bsZalogPoruch, "ZalogPoruch-OsnovnSd");
            bsDopSogZalPor = new BindingSource(bsZalogPoruch, "ZalogPoruch-DopSogZalPor");
            bsGrpObject = new BindingSource(bsZalogPoruch, "ZalogPoruch-GrpObject");
            bsDocsZalPor = new BindingSource(bsZalogPoruch, "ZalogPoruch-DocsZalPor");
            bsObjData = new BindingSource(bsGrpObject, "GrpObject-ObjData");

            //DATA GRID LOCATION AND SIZE AREA
            gdName = new DataGridView(); //dg Name
            gdName.Size = new Size(245, 500);
            gdName.Location = new Point(5, 35);
            gdName.DataSource = bsName;
            gdKredDog = new DataGridView(); //dg KredDog
            gdKredDog.Size = new Size(245, 150);
            gdKredDog.Location = new Point(255, 35);
            gdKredDog.DataSource = bsKredDog; 
            gdKredDocum = new DataGridView(); //dg KredDocum
            gdKredDocum.Size = new Size(245, 150);
            gdKredDocum.Location = new Point(255, gdKredDog.Bottom + 30);
            gdKredDocum.DataSource = bsKredDocum;
            gdOsnSdelkVdch = new DataGridView(); //dg OsnSdelkVdch
            gdOsnSdelkVdch.Size = new Size(450, 150);
            gdOsnSdelkVdch.Location = new Point(505, 35);
            gdOsnSdelkVdch.DataSource = bsOsnSdelkVdch;
            gdZalogPoruch = new DataGridView(); //dg ZalogPoruch
            gdZalogPoruch.Size = new Size(345, 150);
            gdZalogPoruch.Location = new Point(505, gdOsnSdelkVdch.Bottom + 30);
            gdZalogPoruch.DataSource = bsZalogPoruch;
            gdOsnovnSd = new DataGridView(); //dg OsnovnSd
            gdOsnovnSd.Size = new Size(595, 140);
            gdOsnovnSd.Location = new Point(255, gdKredDocum.Bottom + 30);
            gdOsnovnSd.DataSource = bsOsnovnSd;

            this.Controls.AddRange(new Control[] { gdName, gdKredDog, gdKredDocum, gdOsnSdelkVdch, gdZalogPoruch, gdOsnovnSd }); //control 

            //CLICKABLE DATA IN DATA GRID'S
            this.gdOsnSdelkVdch.CellContentClick += new DataGridViewCellEventHandler(this.gdOsnSdelkVdch_CellContentClick);
            this.gdKredDocum.CellContentClick += new DataGridViewCellEventHandler(this.gdKredDocum_CellContentClick);
            this.gdOsnovnSd.CellContentClick += new DataGridViewCellEventHandler(this.gdOsnovnSd_CellContentClick);

            //HIDDEN ID'S AREA
            dataSet.Tables["Zaemwik"].Columns["id"].ColumnMapping = MappingType.Hidden;
            dataSet.Tables["KredDocum"].Columns["id"].ColumnMapping = MappingType.Hidden;
            dataSet.Tables["OsnSdelkVdch"].Columns["id"].ColumnMapping = MappingType.Hidden;
            dataSet.Tables["OsnovnSd"].Columns["id"].ColumnMapping = MappingType.Hidden;
            dataSet.Tables["DocsZalPor"].Columns["id"].ColumnMapping = MappingType.Hidden;

            //HIDDEN SCROLLBARS AREA
            gdKredDog.ScrollBars = ScrollBars.Vertical;
            gdZalogPoruch.ScrollBars = ScrollBars.Vertical;
            gdName.ScrollBars = ScrollBars.Vertical;
        }

        private void DocumentsForm_Click(object sender, EventArgs e)
        {
            try // try take 2 arguments
            {
                int trans1 = (int)gdName.CurrentRow.Cells[2].Value;
                int trans2 = (int)gdZalogPoruch.CurrentRow.Cells[3].Value;
                DocumentsForm SDS = new DocumentsForm(trans1, trans2);
                SDS.ShowDialog();
            }
            catch // if don't work
            {

                try // try take 1 arguments
                {
                    if (gdName.CurrentRow.Cells[2].Value != null) // try take 1 arguments
                    {
                        int trans1 = (int)gdName.CurrentRow.Cells[2].Value;
                        DocumentsForm SDS = new DocumentsForm(trans1);
                        SDS.ShowDialog();
                    }
                    else 
                    {
                        MessageBox.Show("CIB 2016-2017", "About", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                catch // if DB clear
                {
                    MessageBox.Show("В базе данных нет информации", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
      
        private void Form1_Load(object sender, EventArgs e) //main form position and size
        {
            this.Location = new Point(0, 0);
            Size = new Size(980, 620);
        }

        private void searchForm_Click(object sender, EventArgs e) //button to open Search form
        {
            Search src = new Search();
            src.ShowDialog();
        }

        private void spis_dop_sog_and__gr_obj_Click(object sender, EventArgs e) //button to open Spisok dop.sogl i grup.obj form
        {
            try // try take 1 argument
            {
                int trans2 = (int)gdZalogPoruch.CurrentRow.Cells[3].Value;
                Spid_ds_and_grob SDSG = new Spid_ds_and_grob(trans2);
                SDSG.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Нет данных по залогодателю/поручителю", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) //search OOO in main form
        {
            bsName.Filter = "Наименование LIKE '%' + '" + textBox1.Text + "%'"; 
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //trouble with int type
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) // close application and connection to SQL
        {
            connection.Close();
            Application.Exit();
        }

    }
}
