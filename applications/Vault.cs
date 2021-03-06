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
    public partial class Vault : Form
    {
        Color FlatColor;
        public Vault()
        {
            InitializeComponent();
            LoadDGV();
            this.ControlBox = false;
            //comboBox2.Text = "За изделия";
            comboBox2.SelectedIndex = 1;
            this.WindowState = FormWindowState.Maximized;
            button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button1.FlatAppearance.BorderColor = FlatColor;
            button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button2.FlatAppearance.BorderColor = FlatColor;
            button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button3.FlatAppearance.BorderColor = FlatColor;
            button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button8.FlatAppearance.BorderColor = FlatColor;
            button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button13.FlatAppearance.BorderColor = FlatColor;
            button15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button15.FlatAppearance.BorderColor = FlatColor;

            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox7.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        void LoadDGV()
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            dgv.Columns.AddRange(
            new DataGridViewTextBoxColumn() { Name = "worker", HeaderText = "Рабочий" },
            new DataGridViewTextBoxColumn() { Name = "clock", HeaderText = "Кол-во часов" });

            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            dataGridView2.Columns.AddRange(
            new DataGridViewTextBoxColumn() { Name = "product", HeaderText = "Товар" },
            new DataGridViewTextBoxColumn() { Name = "price", HeaderText = "Расценок" },
            new DataGridViewTextBoxColumn() { Name = "count", HeaderText = "Кол-во" });

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.AddRange(
            new DataGridViewTextBoxColumn() { Name = "product", HeaderText = "Товар" },
            new DataGridViewTextBoxColumn() { Name = "count", HeaderText = "Кол-во" });

            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();
            dataGridView3.Columns.AddRange(
            new DataGridViewTextBoxColumn() { Name = "product", HeaderText = "Товар" },
            new DataGridViewTextBoxColumn() { Name = "price", HeaderText = "Расценок" },
            new DataGridViewTextBoxColumn() { Name = "count", HeaderText = "Кол-во" },
            new DataGridViewTextBoxColumn() { Name = "ope", HeaderText = "Операция" });
        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ForWorkers fw = new ForWorkers();
            if (comboBox1.Text.Equals("Первая"))
            {
                fw.shift = "1";
            }
            if (comboBox1.Text.Equals("Вторая"))
            {
                fw.shift = "2";
            }
            if (comboBox1.Text.Equals("Третья"))
            {
                fw.shift = "3";
            }
            if (comboBox1.Text.Equals("Все"))
            {
                fw.shift = "";
            }
            fw.Owner = this;
            //MessageBox.Show(fw.shift);
            fw.Show();
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

        private void button12_Click(object sender, EventArgs e)
        {
            int year = dateTimePicker4.Value.Date.Year;
            int month = dateTimePicker4.Value.Date.Month;
            int day = dateTimePicker4.Value.Date.Day;
            int[] dateArr = visok(dateTimePicker4.Value.Date.Year);
            if (day == 1)
            {
                month--;
                if (month == 0)
                {
                    month = 12;
                    year--;
                }
                day = dateArr[month - 1];
                dateTimePicker4.Value = new DateTime(year, month, day);
            }
            else
            {
                dateTimePicker4.Value = new DateTime(year, month, day - 1);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int year = dateTimePicker4.Value.Date.Year;
            int month = dateTimePicker4.Value.Date.Month;
            int day = dateTimePicker4.Value.Date.Day;
            int[] dateArr = visok(dateTimePicker4.Value.Date.Year);
            if (day == dateArr[month - 1])
            {
                if (month == 12)
                {
                    month = 0;
                    year++;
                }
                day = 1;
                dateTimePicker4.Value = new DateTime(year, month + 1, day);
            }
            else
            {
                dateTimePicker4.Value = new DateTime(year, month, day + 1);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count > 1)
            {
                DialogResult result = MessageBox.Show(
                        "Удалить данную позицию?",
                        "Сообщение",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1
                        //MessageBoxOptions.DefaultDesktopOnly
                        );

                if (result == DialogResult.Yes)
                {
                    dgv.Rows.RemoveAt(curRow);
                }
            }
        }
        public int curRow;
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            curRow = dgv.CurrentCell.RowIndex;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            ForProduct fp = new ForProduct();
            fp.table = 0;
            fp.comboBox1.Hide();
            fp.label3.Hide();
            //fp.label2.Hide();
            //fp.textBox2.Hide();
            fp.Owner = this;
            fp.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 1)
            {
                DialogResult result = MessageBox.Show(
                        "Удалить данную позицию?",
                        "Сообщение",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1
                        //MessageBoxOptions.DefaultDesktopOnly
                        );

                if (result == DialogResult.Yes)
                {
                    dataGridView2.Rows.RemoveAt(curRow);
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            curRow = dataGridView2.CurrentCell.RowIndex;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                       "Введенные данные верны?",
                       "Сообщение",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Information,
                       MessageBoxDefaultButton.Button1
                       //MessageBoxOptions.DefaultDesktopOnly
                       );

            if (result == DialogResult.Yes)
            {
                if (comboBox2.SelectedIndex == 1)
                {
                    double salary = 0;
                    int clock = 0;
                    for(int i = 0; i < dgv.Rows.Count - 1; i++)
                    {
                        clock += int.Parse(dgv["clock", i].Value.ToString());
                    }
                    for(int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                    {
                        int val = int.Parse(dataGridView2["price", i].Value.ToString()) * int.Parse(dataGridView2["count", i].Value.ToString());
                        salary += val / clock;
                    }
                    for (int i = 0; i < dgv.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dataGridView2.Rows.Count - 1; j++)
                        {
                            DB db = new DB();
                            MySqlCommand command = new MySqlCommand("INSERT INTO `work` (`date`, `shift`, `worker`, `clock`, `product`, `count`, `salary`) VALUES (@date, @shift, @worker, @clock, @product, @count, @salary)", db.getConnection());

                            command.Parameters.Add("@date", MySqlDbType.Date).Value = dateTimePicker1.Value;
                            if (comboBox1.Text.Equals("Первая"))
                            {
                                command.Parameters.Add("@shift", MySqlDbType.Int64).Value = "1";
                            }
                            if (comboBox1.Text.Equals("Вторая"))
                            {
                                command.Parameters.Add("@shift", MySqlDbType.Int64).Value = "2";
                            }
                            command.Parameters.Add("@worker", MySqlDbType.VarChar).Value = dgv[0, i].Value;
                            string time = dgv[1, i].Value.ToString();
                            for (int p = 0; p < time.Length; p++)
                            {
                                if (time[p] == '.')
                                {
                                    string[] ans = time.Split('.');
                                    time = ans[0] + "," + ans[1];
                                    break;
                                }
                            }
                            command.Parameters.Add("@clock", MySqlDbType.VarChar).Value = time;
                            command.Parameters.Add("@product", MySqlDbType.VarChar).Value = dataGridView2[0, j].Value;
                            string num = dataGridView2[1, j].Value.ToString();
                            for (int p = 0; p < num.Length; p++)
                            {
                                if (num[p] == '.')
                                {
                                    string[] ans = num.Split('.');
                                    num = ans[0] + "," + ans[1];
                                    break;
                                }
                            }
                            command.Parameters.Add("@count", MySqlDbType.VarChar).Value = num;
                            command.Parameters.Add("@salary", MySqlDbType.VarChar).Value = salary;
                            db.openConnection();

                            command.ExecuteNonQuery();


                            db.closeConnection();

                        }
                    }
                    for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                    {
                        DB db = new DB();
                        string num = dataGridView2[1, i].Value.ToString();
                        for (int p = 0; p < num.Length; p++)
                        {
                            if (num[p] == '.')
                            {
                                string[] ans = num.Split('.');
                                num = ans[0] + "," + ans[1];
                                break;
                            }
                        }
                        MySqlCommand cmd = new MySqlCommand("SELECT * FROM `product` WHERE `name` = '" + dataGridView2[0, i].Value + "'", db.getConnection());
                        MySqlDataReader myReader;
                        double count = 0;
                        try
                        {
                            db.openConnection();
                            myReader = cmd.ExecuteReader();

                            int j = 0;
                            while (myReader.Read())
                            {
                                count = double.Parse(myReader.GetString("count"));
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        db.closeConnection();
                        count += double.Parse(num);
                        MySqlCommand command = new MySqlCommand("UPDATE `product` SET `count` = '" + count + "' WHERE `name` = '" + dataGridView2[0, i].Value + "'", db.getConnection());
                        db.openConnection();

                        command.ExecuteNonQuery();


                        db.closeConnection();


                        String[] dates = dateTimePicker1.Value.ToString().Split(' ');
                        String[] rightDate = dates[0].Split('.');
                        String right = rightDate[2] + "-" + rightDate[1] + "-" + rightDate[0];
                        MySqlCommand comand = new MySqlCommand("SELECT * FROM `operations` WHERE `product` = '" + dataGridView2[0, i].Value + "' and `date` <= '" + right + "' order by `dateWrite` desc;", db.getConnection());
                        //MessageBox.Show("SELECT * FROM `product` WHERE `name` = '" + dataGridView1[0, p].Value + "';");
                        double change = 0;
                        //MessageBox.Show("SELECT * FROM `operations` WHERE `product` = '" + dataGridView1[0, p].Value + "' and `date` <= '" + right + "' order by `dateWrite` desc;");
                        try
                        {
                            db.openConnection();
                            myReader = comand.ExecuteReader();

                            while (myReader.Read())
                            {
                                //MessageBox.Show(myReader.GetString("countNow"));
                                change = double.Parse(myReader.GetString("countNow"));
                                change += double.Parse(dataGridView2[2, i].Value.ToString());
                                break;
                            }
                            myReader.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        db.closeConnection();

                        //comand = new MySqlCommand("UPDATE `product` SET `count` = '" + change.ToString() + "' WHERE `name` = '" + dataGridView1[0, p].Value + "';", db.getConnection());
                        //MessageBox.Show("UPDATE `product` SET `count` = '" + change.ToString() + "' WHERE `name` = '" + dataGridView1[0, p].Value + "';");
                        //db.openConnection();

                        //.ExecuteNonQuery();

                        //db.closeConnection();

                        comand = new MySqlCommand("INSERT INTO `operations` (`date`, `operation`, `product`, `count`, `countNow`) VALUES (@date, @ope, @product, @count, @countNow)", db.getConnection());
                        //MessageBox.Show(dateTimePicker1.Value.ToString() + " "  + dataGridView2[0, i].Value.ToString() + " " + dataGridView2[1, i].Value.ToString() + " " + change.ToString());
                        //string[] fordate = dateTimePicker1.Value.ToString().Split(' ');
                        comand.Parameters.Add("@date", MySqlDbType.Date).Value = dateTimePicker1.Value;
                        //int idtemp = int.Parse(idRequest) - 1;
                        comand.Parameters.Add("@ope", MySqlDbType.VarChar).Value = "Поступление от производства";
                        comand.Parameters.Add("@product", MySqlDbType.VarChar).Value = dataGridView2[0, i].Value;
                        string numm = dataGridView2[2, i].Value.ToString();
                        for (int c = 0; c < numm.Length; c++)
                        {
                            if (numm[c] == '.')
                            {
                                string[] ans = numm.Split('.');
                                numm = ans[0] + "," + ans[1];
                                break;
                            }
                        }
                        comand.Parameters.Add("@count", MySqlDbType.VarChar).Value = numm;
                        comand.Parameters.Add("@countNow", MySqlDbType.VarChar).Value = change;
                        db.openConnection();

                        comand.ExecuteNonQuery();

                        db.closeConnection();

                    }
                    comboBox1.Text = "";
                    dgv.Rows.Clear();
                    dataGridView2.Rows.Clear();
                }
            }
           
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ForProduct fp = new ForProduct();
            fp.table = 1;
            fp.comboBox1.Hide();
            fp.label3.Hide();
            fp.label2.Hide();
            fp.textBox2.Hide();
            fp.Owner = this;
            fp.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 1)
            {
                DialogResult result = MessageBox.Show(
                        "Удалить данную позицию?",
                        "Сообщение",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1
                        //MessageBoxOptions.DefaultDesktopOnly
                        );

                if (result == DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(curRow);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                       "Введенные данные верны?",
                       "Сообщение",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Information,
                       MessageBoxDefaultButton.Button1
                       //MessageBoxOptions.DefaultDesktopOnly
                       );

            if (result == DialogResult.Yes)
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    DB db = new DB();
                    string num = dataGridView1[1, i].Value.ToString();
                    for (int p = 0; p < num.Length; p++)
                    {
                        if (num[p] == '.')
                        {
                            string[] ans = num.Split('.');
                            num = ans[0] + "," + ans[1];
                            break;
                        }
                    }
                    MySqlCommand command = new MySqlCommand("UPDATE `product` SET `count` = '" + num + "' WHERE `name` = '" + dataGridView1[0, i].Value + "'", db.getConnection());
                    db.openConnection();

                    command.ExecuteNonQuery();


                    db.closeConnection();

                    command = new MySqlCommand("INSERT INTO `operations` (`date`, `operation`, `product`, `count`) VALUES (@date, @ope, @product, @count)", db.getConnection());

                    //string[] fordate = dateTimePicker1.Value.ToString().Split(' ');
                    command.Parameters.Add("@date", MySqlDbType.Date).Value = dateTimePicker4.Value;
                    command.Parameters.Add("@ope", MySqlDbType.VarChar).Value = "Инвентаризация";
                    command.Parameters.Add("@product", MySqlDbType.VarChar).Value = dataGridView1[0, i].Value;
                    string numm = dataGridView1[1, i].Value.ToString();
                    for (int p = 0; p < numm.Length; p++)
                    {
                        if (numm[p] == '.')
                        {
                            string[] ans = numm.Split('.');
                            numm = ans[0] + "," + ans[1];
                            break;
                        }
                    }
                    command.Parameters.Add("@count", MySqlDbType.VarChar).Value = numm;

                    //MessageBox.Show("INSERT INTO `operations` (`date`, `operation`, `product`, `count`) VALUES (" + fordate[0] + ", Поступление от производства, " + dataGridView2[0, i].Value + ", " + numm + ")");
                    db.openConnection();

                    command.ExecuteNonQuery();


                    db.closeConnection();
                }
                dataGridView1.Rows.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VaultStatement vaultStatement = new VaultStatement();
            vaultStatement.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            ForProduct fp = new ForProduct();
            fp.table = 2;
            fp.Owner = this;
            fp.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (dataGridView3.Rows.Count > 1)
            {
                DialogResult result = MessageBox.Show(
                        "Удалить данную позицию?",
                        "Сообщение",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1
                        //MessageBoxOptions.DefaultDesktopOnly
                        );

                if (result == DialogResult.Yes)
                {
                    dataGridView3.Rows.RemoveAt(curRow);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                       "Введенные данные верны?",
                       "Сообщение",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Information,
                       MessageBoxDefaultButton.Button1
                       //MessageBoxOptions.DefaultDesktopOnly
                       );

            if (result == DialogResult.Yes)
            {
                for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
                {
                    DB db = new DB();
                    string num = dataGridView3[1, i].Value.ToString();
                    for (int p = 0; p < num.Length; p++)
                    {
                        if (num[p] == '.')
                        {
                            string[] ans = num.Split('.');
                            num = ans[0] + "," + ans[1];
                            break;
                        }
                    }
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM `product` WHERE `name` = '" + dataGridView3[0, i].Value + "'", db.getConnection());
                    MySqlDataReader myReader;
                    double count = 0;
                    try
                    {
                        db.openConnection();
                        myReader = cmd.ExecuteReader();

                        int j = 0;
                        while (myReader.Read())
                        {
                            count = double.Parse(myReader.GetString("count"));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    db.closeConnection();
                    count -= double.Parse(num);
                    MySqlCommand command = new MySqlCommand("UPDATE `product` SET `count` = '" + count + "' WHERE `name` = '" + dataGridView3[0, i].Value + "'", db.getConnection());
                    db.openConnection();

                    command.ExecuteNonQuery();


                    db.closeConnection();

                    String[] dates = dateTimePicker2.Value.ToString().Split(' ');
                    String[] rightDate = dates[0].Split('.');
                    String right = rightDate[2] + "-" + rightDate[1] + "-" + rightDate[0];
                    MySqlCommand comand = new MySqlCommand("SELECT * FROM `operations` WHERE `product` = '" + dataGridView3[0, i].Value + "' and `date` <= '" + right + "' order by `dateWrite` desc;", db.getConnection());
                    //MessageBox.Show("SELECT * FROM `product` WHERE `name` = '" + dataGridView1[0, p].Value + "';");
                    double change = 0;
                    //MessageBox.Show("SELECT * FROM `operations` WHERE `product` = '" + dataGridView1[0, p].Value + "' and `date` <= '" + right + "' order by `dateWrite` desc;");
                    try
                    {
                        db.openConnection();
                        myReader = comand.ExecuteReader();

                        while (myReader.Read())
                        {
                            //MessageBox.Show(myReader.GetString("countNow"));
                            change = double.Parse(myReader.GetString("countNow"));
                            change -= double.Parse(dataGridView3[2, i].Value.ToString());
                            break;
                        }
                        myReader.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    db.closeConnection();

                    //comand = new MySqlCommand("UPDATE `product` SET `count` = '" + change.ToString() + "' WHERE `name` = '" + dataGridView1[0, p].Value + "';", db.getConnection());
                    //MessageBox.Show("UPDATE `product` SET `count` = '" + change.ToString() + "' WHERE `name` = '" + dataGridView1[0, p].Value + "';");
                    //db.openConnection();

                    //.ExecuteNonQuery();

                    //db.closeConnection();

                    comand = new MySqlCommand("INSERT INTO `operations` (`date`, `operation`, `product`, `count`, `countNow`) VALUES (@date, @ope, @product, @count, @countNow)", db.getConnection());
                    //MessageBox.Show(dateTimePicker1.Value.ToString() + " "  + dataGridView2[0, i].Value.ToString() + " " + dataGridView2[1, i].Value.ToString() + " " + change.ToString());
                    //string[] fordate = dateTimePicker1.Value.ToString().Split(' ');
                    comand.Parameters.Add("@date", MySqlDbType.Date).Value = dateTimePicker2.Value;
                    //int idtemp = int.Parse(idRequest) - 1;
                    comand.Parameters.Add("@ope", MySqlDbType.VarChar).Value = dataGridView3[3, i].Value;
                    comand.Parameters.Add("@product", MySqlDbType.VarChar).Value = dataGridView3[0, i].Value;
                    string numm = dataGridView3[2, i].Value.ToString();
                    for (int c = 0; c < numm.Length; c++)
                    {
                        if (numm[c] == '.')
                        {
                            string[] ans = numm.Split('.');
                            numm = ans[0] + "," + ans[1];
                            break;
                        }
                    }
                    comand.Parameters.Add("@count", MySqlDbType.VarChar).Value = numm;
                    comand.Parameters.Add("@countNow", MySqlDbType.VarChar).Value = change;
                    db.openConnection();

                    comand.ExecuteNonQuery();

                    db.closeConnection();
                }
                dataGridView3.Rows.Clear();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex == 0)
            {
                label3.Hide();
                dataGridView2.Hide();
                pictureBox4.Hide();
                pictureBox6.Hide();
            }
            else
            {
                label3.Show();
                dataGridView2.Show();
                pictureBox4.Show();
                pictureBox6.Show();
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
