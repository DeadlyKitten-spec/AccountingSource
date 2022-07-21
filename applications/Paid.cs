using MySql.Data.MySqlClient;
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
    public partial class Paid : Form
    {
        Color FlatColor;
        public Paid()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Maximized;
            FlatColor = button6.FlatAppearance.BorderColor;

            //FillCombo2();
            button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button6.FlatAppearance.BorderColor = FlatColor;

            button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button3.FlatAppearance.BorderColor = FlatColor;

            button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button4.FlatAppearance.BorderColor = FlatColor;
            this.ControlBox = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int year = dateTimePicker1.Value.Date.Year;
            int month = dateTimePicker1.Value.Date.Month;
            int day = dateTimePicker1.Value.Date.Day;
            int[] dateArr = visok(dateTimePicker1.Value.Date.Year);
            if (day == 1)
            {
                month--;
                if (month == 0)
                {
                    month = 12;
                    year--;
                }
                day = dateArr[month - 1];
                dateTimePicker1.Value = new DateTime(year, month, day);
            }
            else
            {
                dateTimePicker1.Value = new DateTime(year, month, day - 1);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int year = dateTimePicker1.Value.Date.Year;
            int month = dateTimePicker1.Value.Date.Month;
            int day = dateTimePicker1.Value.Date.Day;
            int[] dateArr = visok(dateTimePicker1.Value.Date.Year);
            if (day == dateArr[month - 1])
            {
                if (month == 12)
                {
                    month = 0;
                    year++;
                }
                day = 1;
                dateTimePicker1.Value = new DateTime(year, month + 1, day);
            }
            else
            {
                dateTimePicker1.Value = new DateTime(year, month, day + 1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int year = dateTimePicker3.Value.Date.Year;
            int month = dateTimePicker3.Value.Date.Month;
            int day = dateTimePicker3.Value.Date.Day;
            int[] dateArr = visok(dateTimePicker3.Value.Date.Year);
            if (day == 1)
            {
                month--;
                if (month == 0)
                {
                    month = 12;
                    year--;
                }
                day = dateArr[month - 1];
                dateTimePicker3.Value = new DateTime(year, month, day);
            }
            else
            {
                dateTimePicker3.Value = new DateTime(year, month, day - 1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int year = dateTimePicker3.Value.Date.Year;
            int month = dateTimePicker3.Value.Date.Month;
            int day = dateTimePicker3.Value.Date.Day;
            int[] dateArr = visok(dateTimePicker3.Value.Date.Year);
            if (day == dateArr[month - 1])
            {
                if (month == 12)
                {
                    month = 0;
                    year++;
                }
                day = 1;
                dateTimePicker3.Value = new DateTime(year, month + 1, day);
            }
            else
            {
                dateTimePicker3.Value = new DateTime(year, month, day + 1);
            }
        }
        int[] visok(int year)
        {
            int[] dateArr = new int[12];
            if (((year % 100) % 4) != 0)
            {
                dateArr = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            }
            else
            {
                dateArr = new int[] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            }
            return dateArr;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dateTimePicker1.ResetText();
            dateTimePicker3.ResetText();
            comboBox2.Text = "";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            string time = dateTimePicker1.Value.ToString();
            string[] asd = time.Split(' ');
            string[] zxc = asd[0].Split('.');
            string answer1 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];

            time = dateTimePicker3.Value.ToString();
            asd = null;
            asd = time.Split(' ');
            zxc = null;
            zxc = asd[0].Split('.');
            string answer2 = zxc[2] + '-' + zxc[1] + '-' + zxc[0];

            //string Line = "SELECT * FROM `request` WHERE `docDate` = '" + answer + "';";
            bool fdriver = false;

            string Line = "";// = "SELECT * FROM `request` WHERE `status` = '" + comboBox1.Text + "';";
            if (!comboBox2.Text.Equals(""))
            {
                fdriver = true;
            }

            if (fdriver)
            {
                //(SELECT * FROM main.request order by `id` asc) order by `object` asc;
                string tar = "";
                if (comboBox2.Text.Equals("Наличный"))
                {
                    tar = "AND `cash` = 'Да'";
                }
                else
                {
                    if (comboBox2.Text.Equals("Без наличный"))
                    {
                        tar = "AND `cash` = 'Нет'";
                    }
                }

                Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' " + tar + " AND `status` = 'Исполнена' ORDER BY `buyer`, `id` ASC;";
                DB db = new DB();
                MySqlCommand command = new MySqlCommand(Line, db.getConnection());
                bool g = true;
                db.openConnection();
                object obj = command.ExecuteScalar();
                db.closeConnection();
                dgv.Columns.Clear();
                dgv.Rows.Clear();
                dgv.Columns.AddRange(
                new DataGridViewTextBoxColumn() { Name = "id", HeaderText = "Номер заявки" },
                new DataGridViewTextBoxColumn() { Name = "date", HeaderText = "Дата" },
                new DataGridViewTextBoxColumn() { Name = "buyer", HeaderText = "Заказчик" },
                new DataGridViewTextBoxColumn() { Name = "object", HeaderText = "Название Объекта" },
                new DataGridViewTextBoxColumn() { Name = "paid", HeaderText = "Оплата" },
                new DataGridViewTextBoxColumn() { Name = "cash", HeaderText = "Наличные" },
                new DataGridViewTextBoxColumn() { Name = "card", HeaderText = "На карту" });
                MySqlDataReader myReader;
                try
                {
                    db.openConnection();
                    myReader = command.ExecuteReader();
                    int itter = 0;
                    while (myReader.Read())
                    {
                        if (itter != 0)
                        {
                            //MessageBox.Show("asd");
                            if (myReader.GetString("id").Equals(dgv[0, itter - 1].Value))
                            {
                                continue;
                            }
                        }
                        dgv.Rows.Add();
                        dgv[0, itter].Value = myReader.GetString("id");
                        string[] fs = myReader.GetString("docDate").Split(' ');
                        dgv[1, itter].Value = fs[0];
                        dgv[2, itter].Value = myReader.GetString("buyer");
                        dgv[3, itter].Value = myReader.GetString("object");
                        if (myReader.GetString("paid").Equals("Да"))
                        {
                            dgv[4, itter].Value = "Оплачено";
                        }
                        else
                        {

                            dgv[4, itter].Value = "Не оплачено";
                        }
                        dgv[5, itter].Value = myReader.GetString("cash");
                        dgv[6, itter].Value = myReader.GetString("card");
                        itter++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }
    }
}
