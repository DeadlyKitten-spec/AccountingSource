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
    public partial class CostRegister : Form
    {
        public CostRegister()
        {
            InitializeComponent();

            FillCombo2();
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

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox3.SelectedIndex == 0)
            {
                label3.Text = "Покупатель";
                label7.Text = "Поставщик";
            }
            else
            {
                label3.Text = "Заказчик";
                label7.Text = "Исполнитель";
            }
        }

        void FillCombo2()
        {
            string Query = "SELECT * FROM `counterparty` WHERE `status` != 'Грузоотправитель'" + /*WHERE status = 'Грузополучатель/грузоотправитель' OR status = 'Диспетчер'*/  "ORDER BY `name` ASC;";
            DB db = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            string[] names = new string[1000];
            try
            {
                db.openConnection();
                myReader = cmdDataBase.ExecuteReader();

                int j = 0;
                while (myReader.Read())
                {
                    string objName = myReader.GetString("name");
                    bool f = true;
                    for (int i = 0; i < names.Length; i++)
                    {
                        if (names[i] != null)
                        {
                            if (names[i].Equals(objName))
                            {
                                f = false;
                                break;
                            }
                        }
                    }
                    if (f == true)
                    {
                        names[j] = objName;
                        j++;
                        comboBox2.Items.Add(objName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            comboBox2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dateTimePicker1.ResetText();
            dateTimePicker3.ResetText();
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox4.Text.Equals(""))
            {
                MessageBox.Show("Вы не выбрали продавца");
            }
            else
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
                bool fbuyer = false;

                string Line = "";// = "SELECT * FROM `request` WHERE `status` = '" + comboBox1.Text + "';";
                if (!comboBox2.Text.Equals(""))
                {
                    fbuyer = true;
                }

                if (fbuyer)
                {
                    Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `deal` = '" + comboBox3.Text + "' AND `ourFirms` = '" + comboBox4.Text + "' AND `buyer` = '" + comboBox2.Text + "' ORDER BY `object` ASC;";
                    DB db = new DB();
                    MySqlCommand command = new MySqlCommand(Line, db.getConnection());
                    bool g = true;
                    db.openConnection();
                    object obj = command.ExecuteScalar();
                    db.closeConnection();
                    List<ForCostRegister> array = new List<ForCostRegister>();
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
                                ForCostRegister temp = new ForCostRegister(datesplit[0], myReader.GetString("object"), myReader.GetString("id"), pr);
                                bool f = true;
                                for(int i = 0; i < array.Count; i++)
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
                        dgv.Columns.Clear();
                        dgv.Rows.Clear();
                        dgv.Columns.AddRange(
                        new DataGridViewTextBoxColumn() { Name = "date", HeaderText = "Дата" },
                        new DataGridViewTextBoxColumn() { Name = "object", HeaderText = "Объект поставки" },
                        new DataGridViewTextBoxColumn() { Name = "id", HeaderText = "№ заявки" },
                        new DataGridViewTextBoxColumn() { Name = "price", HeaderText = "Стоимость" });
                        dgv.Rows.Clear();
                        string line = array[0].objectt;
                        int count = 0;
                        double sumTrip = 0;
                        double sumAll = 0;
                        for (int i = 0; i < array.Count; i++)
                        {
                            if (!line.Equals(array[i].objectt) || ((i == array.Count - 1) && (i != 0)))
                            {
                                dgv.Rows.Add();
                                dgv[0, count].Value = "Итого";
                                dgv[3, count].Value = sumTrip;
                                dgv.Rows[count].DefaultCellStyle.BackColor = Color.LightGray;
                                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                                sumAll += sumTrip;
                                sumTrip = 0;
                                count++;
                                line = array[i].objectt;
                            }
                            dgv.Rows.Add();
                            dgv[0, count].Value = array[i].date;
                            dgv[1, count].Value = array[i].objectt;
                            dgv[2, count].Value = array[i].id;
                            dgv[3, count].Value = array[i].price;
                            sumTrip += double.Parse(array[i].price);
                            count++;
                            if ((i == array.Count - 1))
                            {
                                dgv.Rows.Add();
                                dgv[0, count].Value = "Итого";
                                dgv[3, count].Value = sumTrip;
                                dgv.Rows[count].DefaultCellStyle.BackColor = Color.LightGray;
                                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                                sumAll += sumTrip;
                                sumTrip = 0;
                                count++;
                                line = array[i].objectt;
                            }
                        }
                        sumAll += sumTrip;
                        dgv.Rows.Add();
                        dgv[0, count].Value = "Всего";
                        dgv[3, count].Value = sumAll;
                        dgv.Rows[count].DefaultCellStyle.BackColor = Color.LightGray;
                        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    }

                }
                else
                {
                    Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `deal` = '" + comboBox3.Text + "' AND `ourFirms` = '" + comboBox4.Text + "' ORDER BY `object` ASC;";
                    DB db = new DB();
                    MySqlCommand command = new MySqlCommand(Line, db.getConnection());
                    bool g = true;
                    db.openConnection();
                    object obj = command.ExecuteScalar();
                    db.closeConnection();
                    List<ForCostRegister> array = new List<ForCostRegister>();
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
                                ForCostRegister temp = new ForCostRegister(datesplit[0], myReader.GetString("object"), myReader.GetString("id"), pr);
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
                        dgv.Columns.Clear();
                        dgv.Rows.Clear();
                        dgv.Columns.AddRange(
                        new DataGridViewTextBoxColumn() { Name = "date", HeaderText = "Дата" },
                        new DataGridViewTextBoxColumn() { Name = "object", HeaderText = "Объект поставки" },
                        new DataGridViewTextBoxColumn() { Name = "id", HeaderText = "№ заявки" },
                        new DataGridViewTextBoxColumn() { Name = "price", HeaderText = "Стоимость" });
                        dgv.Rows.Clear();
                        string line = array[0].objectt;
                        int count = 0;
                        double sumTrip = 0;
                        double sumAll = 0;
                        for (int i = 0; i < array.Count; i++)
                        {
                            if (!line.Equals(array[i].objectt) || ((i == array.Count - 1) && (i != 0)))
                            {
                                dgv.Rows.Add();
                                dgv[0, count].Value = "Итого";
                                dgv[3, count].Value = sumTrip;
                                dgv.Rows[count].DefaultCellStyle.BackColor = Color.LightGray;
                                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                                sumAll += sumTrip;
                                sumTrip = 0;
                                count++;
                                line = array[i].objectt;
                            }
                            dgv.Rows.Add();
                            dgv[0, count].Value = array[i].date;
                            dgv[1, count].Value = array[i].objectt;
                            dgv[2, count].Value = array[i].id;
                            dgv[3, count].Value = array[i].price;
                            sumTrip += double.Parse(array[i].price);
                            count++;
                            if ((i == array.Count - 1))
                            {
                                dgv.Rows.Add();
                                dgv[0, count].Value = "Итого";
                                dgv[3, count].Value = sumTrip;
                                dgv.Rows[count].DefaultCellStyle.BackColor = Color.LightGray;
                                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                                sumAll += sumTrip;
                                sumTrip = 0;
                                count++;
                                line = array[i].objectt;
                            }
                        }
                        sumAll += sumTrip;
                        dgv.Rows.Add();
                        dgv[0, count].Value = "Всего";
                        dgv[3, count].Value = sumAll;
                        dgv.Rows[count].DefaultCellStyle.BackColor = Color.LightGray;
                        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    }
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
