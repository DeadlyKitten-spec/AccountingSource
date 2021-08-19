using System;
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
    public partial class Dictionaries : Form
    {
        Color colorButton;
        Color FlatColor;
        public Dictionaries()
        {
            InitializeComponent();
            FlatColor = button6.FlatAppearance.BorderColor;

            button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button6.FlatAppearance.BorderColor = FlatColor;

            button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button7.FlatAppearance.BorderColor = FlatColor;

            button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button8.FlatAppearance.BorderColor = FlatColor;

            button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button9.FlatAppearance.BorderColor = FlatColor;

            button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button10.FlatAppearance.BorderColor = FlatColor;

            button15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button15.FlatAppearance.BorderColor = FlatColor;

            button20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button20.FlatAppearance.BorderColor = FlatColor;

            button24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button24.FlatAppearance.BorderColor = FlatColor;

            button26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button26.FlatAppearance.BorderColor = FlatColor;

            button27.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button27.FlatAppearance.BorderColor = FlatColor;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Contractor cont = new Contractor();
            cont.Show();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Drivers drivers = new Drivers();
            drivers.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Cars cars = new Cars();
            cars.Show();
            this.Close();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Trailer trailer = new Trailer();
            trailer.Show();
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            CounterParty party = new CounterParty();
            party.Show();
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ObjectCounterparty objectt = new ObjectCounterparty();
            objectt.Show();
            this.Close();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            NomGroup nomGroup = new NomGroup();
            nomGroup.Show();
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Nomenclature nom = new Nomenclature();
            nom.Show();
            this.Close();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.Show();
            this.Close();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Worker worker = new Worker();
            worker.Show();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //this.Owner.Show();
            this.Close();
        }
    }
}
