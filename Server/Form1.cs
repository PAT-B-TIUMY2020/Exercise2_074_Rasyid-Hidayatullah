﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using Service;

namespace Server
{
    public partial class Form1 : Form
    {
        ServiceHost hostObject;
        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hostObject = null;

            try
            {
                Task.Factory.StartNew(() =>
                {
                    hostObject = new ServiceHost(typeof(TI_UMY));
                    hostObject.Open();
                });
                label1.Text = "Sudah Berjalan";
                label2.Text = "Klik OFF untuk menonaktifkan server";
                button1.Enabled = false;
                button2.Enabled = true;
            }
            catch (Exception ex)
            {
                hostObject = null;
                label2.Text = "Server Error";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                hostObject.Abort();
                label1.Text = "Sudah dimatikan";
                label2.Text = "Klik ON untuk menghidupkan server";
                button2.Enabled = false;
                button1.Enabled = true;
            }
            catch (Exception ex)
            {
                button1.Enabled = false;
                button2.Enabled = true;
                label2.Text = "Server Error";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
