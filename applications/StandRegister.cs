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
    public partial class StandRegister : Form
    {
        public StandRegister()
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

        private void button3_Click(object sender, EventArgs e)
        {
            dateTimePicker1.ResetText();
            dateTimePicker3.ResetText();
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

            string Line = "";
            Line = "SELECT * FROM `request` WHERE `docDate` BETWEEN '" + answer1 + "' AND '" + answer2 + "' AND `stand` = 'Да' ORDER BY `cars` ASC;";
            DB db = new DB();
            MySqlCommand command = new MySqlCommand(Line, db.getConnection());
            bool g = true;
            db.openConnection();
            object obj = command.ExecuteScalar();
            db.closeConnection();
            List<ForStandRegister> array = new List<ForStandRegister>();
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
                        ForStandRegister temp = new ForStandRegister(myReader.GetString("cars"),  datesplit[0], myReader.GetString("object"), myReader.GetString("id"), myReader.GetString("timeLoading"), myReader.GetString("timeUnloading"));
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
                    new DataGridViewTextBoxColumn() { Name = "car", HeaderText = "№ Автомобиля" },
                    new DataGridViewTextBoxColumn() { Name = "date", HeaderText = "Дата" },
                    new DataGridViewTextBoxColumn() { Name = "object", HeaderText = "Объект поставки" },
                    new DataGridViewTextBoxColumn() { Name = "load", HeaderText = "Погрузка" },
                    new DataGridViewTextBoxColumn() { Name = "unload", HeaderText = "Разгрузка" });
                dgv.Rows.Clear();
                //dgv.Rows.Add();
                string line = array[0].car;
                int count = 0;
                int sumTrip = 0;
                int sumAll = 0;
                for (int i = 0; i < array.Count; i++)
                {
                    if (!line.Equals(array[i].car) || (i == array.Count - 1))
                    {
                        dgv.Rows.Add();
                        dgv[0, count].Value = "Итого время простоя";
                        string timeCar = sumTrip / 60 + ":" + sumTrip % 60;
                        dgv[4, count].Value = timeCar;
                        dgv.Rows[count].DefaultCellStyle.BackColor = Color.LightGray;
                        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        sumAll += sumTrip;
                        sumTrip = 0;
                        count++;
                        line = array[i].car;
                    }
                    dgv.Rows.Add();
                    dgv[0, count].Value = array[i].car;
                    dgv[1, count].Value = array[i].date;
                    dgv[2, count].Value = array[i].objectt;
                    dgv[3, count].Value = array[i].timeLoad;
                    dgv[4, count].Value = array[i].timeUnload;
                    string[] timel = array[i].timeLoad.Split(':');
                    int minutel = int.Parse(timel[0]) * 60 + int.Parse(timel[1]);
                    string[] timeul = array[i].timeUnload.Split(':');
                    int minuteul = int.Parse(timeul[0]) * 60 + int.Parse(timeul[1]);
                    sumTrip += (minuteul - minutel);
                    count++;
                    if ((i == array.Count - 1))
                    {
                        dgv.Rows.Add();
                        dgv[0, count].Value = "Итого время простоя";
                        string timeCar = sumTrip / 60 + ":" + sumTrip % 60;
                        dgv[4, count].Value = timeCar;
                        dgv.Rows[count].DefaultCellStyle.BackColor = Color.LightGray;
                        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        sumAll += sumTrip;
                        sumTrip = 0;
                        count++;
                        line = array[i].car;
                    }
                }
                sumAll += sumTrip;
                dgv.Rows.Add();
                dgv[0, count].Value = "Всего время простоя";
                string timeCarr = sumAll / 60 + ":" + sumAll % 60;
                dgv[4, count].Value = timeCarr;
                dgv.Rows[count].DefaultCellStyle.BackColor = Color.LightGray;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }
    }
}
