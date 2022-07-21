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
    public partial class CheckPrices : Form
    {
        public CheckPrices()
        {
            InitializeComponent();

            button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.WindowState = FormWindowState.Maximized;
            this.ControlBox = false;
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dateTimePicker1.ResetText();
            dateTimePicker3.ResetText();
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

            string Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' ORDER BY `object`, `id` ASC;";
            DB db = new DB();
            MySqlCommand command = new MySqlCommand(Line, db.getConnection());
            bool g = true;
            db.openConnection();
            object obj = command.ExecuteScalar();
            db.closeConnection();
            List<ForCheckPrices> array = new List<ForCheckPrices>();
            if (obj != null)
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlDataReader myReader;
                try
                {
                    db.openConnection();
                    myReader = command.ExecuteReader();
                    while (myReader.Read())
                    {
                        string datefirst = myReader.GetString("docDate");
                        string[] datesplit = datefirst.Split(' ');
                        string pr = myReader.GetString("price");
                        if (pr.Equals("пусто"))
                        {
                            pr = "0";
                        }
                        string prS = myReader.GetString("priceSalary");
                        if (prS.Equals("пусто"))
                        {
                            prS = "0";
                        }
                        ForCheckPrices temp = new ForCheckPrices(myReader.GetString("id"), datesplit[0], myReader.GetString("buyer"), myReader.GetString("object"), pr, prS, "", "");
                        bool f = true;
                        for (int i = 0; i < array.Count; i++)
                        {
                            if (array[i].id.Equals(temp.id))
                            {
                                f = false;
                                break;
                            }
                        }
                        if (f)
                        {
                            array.Add(temp);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                db.closeConnection();
            }
            for(int i = 0; i < array.Count; i++) { 
                Line = "SELECT * FROM `counterparty` WHERE `name` = '" + array[i].cp + "' and `objectName` = '" + array[i].objectt + "';";
                command = new MySqlCommand(Line, db.getConnection());
                db.openConnection();
                object objj = command.ExecuteScalar();
                db.closeConnection();
                if (objj != null)
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlDataReader myReader2;
                    try
                    {
                        db.openConnection();
                        myReader2 = command.ExecuteReader();
                        while (myReader2.Read())
                        {
                            string priceCount = myReader2.GetString("priceCount");
                            string objName = "price" + priceCount;
                            string pricec = myReader2.GetString(objName);
                            array[i].priceSalaryDict = pricec;
                            string priceCountBuy = myReader2.GetString("priceBuyerCount");
                            string objNameBuy = "priceBuyer" + priceCountBuy;
                            string pricecBuy = myReader2.GetString(objNameBuy);
                            array[i].priceDict = pricecBuy;
                        }
                        myReader2.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    db.closeConnection();
                }
            }
            dgv.Columns.Clear();
            dgv.Rows.Clear();
            dgv.Columns.AddRange(
                new DataGridViewTextBoxColumn() { Name = "id", HeaderText = "№ заявки" },
                new DataGridViewTextBoxColumn() { Name = "date", HeaderText = "Дата" },
                new DataGridViewTextBoxColumn() { Name = "cp", HeaderText = "Контрагент" },
                new DataGridViewTextBoxColumn() { Name = "object", HeaderText = "Объект поставки" },
                new DataGridViewTextBoxColumn() { Name = "price", HeaderText = "Расценок покупателю в заявке" },
                new DataGridViewTextBoxColumn() { Name = "priceSalary", HeaderText = "Расценок ЗП в заявке" },
                new DataGridViewTextBoxColumn() { Name = "priceDict", HeaderText = "Расценок покупателю в справочнике" },
                new DataGridViewTextBoxColumn() { Name = "priceSalaryDict", HeaderText = "Расценок ЗП в справочнике" });
            dgv.Rows.Clear();
            int count = 0;
            for (int i = 0; i < array.Count; i++)
            {
                if(array[i].price.Equals(array[i].priceDict) && array[i].priceSalary.Equals(array[i].priceSalaryDict))
                {
                    continue;
                }
                dgv.Rows.Add();
                dgv[0, count].Value = array[i].id;
                dgv[1, count].Value = array[i].date;
                dgv[2, count].Value = array[i].cp;
                dgv[3, count].Value = array[i].objectt;
                dgv[4, count].Value = array[i].price;
                dgv[5, count].Value = array[i].priceSalary;
                dgv[6, count].Value = array[i].priceDict;
                dgv[7, count].Value = array[i].priceSalaryDict;
                count++;
                    
            }
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
    }
}
