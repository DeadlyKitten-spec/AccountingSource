﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace applications
{
    public partial class Analytics : Form
    {
        Color FlatColor;
        public Analytics()
        {
            InitializeComponent();

            FlatColor = button24.FlatAppearance.BorderColor;

            button24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button24.FlatAppearance.BorderColor = FlatColor;

            button15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button15.FlatAppearance.BorderColor = FlatColor;

            button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button2.FlatAppearance.BorderColor = FlatColor;

            button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button3.FlatAppearance.BorderColor = FlatColor;

            button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button4.FlatAppearance.BorderColor = FlatColor;

            button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button5.FlatAppearance.BorderColor = FlatColor;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            PersonalStatBuy psb = new PersonalStatBuy();
            psb.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            StatementCounterparty stc = new StatementCounterparty();
            stc.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StatementClient stcl = new StatementClient();
            stcl.Show();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            CostRegister cr = new CostRegister();
            cr.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            StandRegister sr = new StandRegister();
            sr.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CheckPrices chep = new CheckPrices();
            chep.Show();
        }
    }
}
