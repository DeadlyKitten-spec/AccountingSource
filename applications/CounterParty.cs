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
    public partial class CounterParty : Form
    {
        public CounterParty()
        {
            InitializeComponent();

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

            FillDGV();
            int ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            int ScreenHeight = Screen.PrimaryScreen.Bounds.Height;

            this.Location = new Point((ScreenWidth / 2) - (this.Width / 2), (ScreenHeight / 2) - (this.Height / 2));
            this.ControlBox = false;
        }

        public string val = "";
            

        void FillDGV()
        {
            string Query = "SELECT * FROM `counterparty` ORDER BY `name` ASC";
            DB db = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            List<CounterObject> counter = new List<CounterObject>();
            int k = 0;
            try
            {
                db.openConnection();
                myReader = cmdDataBase.ExecuteReader();

                int j = 0;
                while (myReader.Read())
                {
                    string Name = myReader.GetString("name");
                    string objName = myReader.GetString("status");
                    bool f = true;
                    for(int i = 0; i < counter.Count; i++)
                    {
                        if (counter[i].name.Equals(Name))
                        {
                            if (!counter[i].status.Equals(objName))
                            {
                                if (!objName.Equals("пусто"))
                                {
                                    counter[i].status = objName;
                                }
                            }
                            f = false;
                        }
                    }
                    if(f)
                    {
                        counter.Add(new CounterObject(Name, objName, myReader.GetString("ageCP")));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            for (int i = 0; i < counter.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, k].Value = counter[i].name;
                dataGridView1[1, k].Value = counter[i].status;
                if (counter[i].age.Equals("старый"))
                {
                    dataGridView1[2, k].Value = "Нет";
                }
                else
                {
                    dataGridView1[2, k].Value = "Да";
                }
                k++;
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (checkBox3.CheckState == CheckState.Checked && ((checkBox1.CheckState == CheckState.Checked) || (checkBox2.CheckState == CheckState.Checked)))
            {
                MessageBox.Show("Неверный статус");
            }
            else
            {
                DB db = new DB();
                MySqlCommand command = new MySqlCommand("INSERT INTO `counterparty` (`name`, `objectName`, `price1`, `priceCount`, `status`, `priceBuyer1`, `priceBuyerCount`, `ageCP`) VALUES (@party, @object, @price, @count, @status, @priceBuyer, @priceBuyerCount, @ageCP)", db.getConnection());

                command.Parameters.Add("@party", MySqlDbType.VarChar).Value = textBox2.Text;
                command.Parameters.Add("@object", MySqlDbType.VarChar).Value = "пусто";
                command.Parameters.Add("@price", MySqlDbType.VarChar).Value = "0";
                command.Parameters.Add("@count", MySqlDbType.Int32).Value = 1;
                command.Parameters.Add("@priceBuyer", MySqlDbType.VarChar).Value = "0";
                command.Parameters.Add("@priceBuyerCount", MySqlDbType.Int32).Value = 1;
                if (checkBox3.CheckState == CheckState.Checked)
                {
                    command.Parameters.Add("@status", MySqlDbType.VarChar).Value = "Диспетчер";
                }
                else
                {
                    if ((checkBox1.CheckState == CheckState.Checked) && (checkBox2.CheckState == CheckState.Checked))
                    {
                        command.Parameters.Add("@status", MySqlDbType.VarChar).Value = "Грузополучатель/грузоотправитель";
                    }
                    else
                    {
                        if (checkBox1.CheckState == CheckState.Checked)
                        {
                            command.Parameters.Add("@status", MySqlDbType.VarChar).Value = "Грузополучатель";
                        }
                        else
                        {
                            if (checkBox2.CheckState == CheckState.Checked)
                            {
                                command.Parameters.Add("@status", MySqlDbType.VarChar).Value = "Грузоотправитель";
                            }
                            if ((checkBox1.CheckState != CheckState.Checked) && (checkBox2.CheckState != CheckState.Checked))
                            {
                                command.Parameters.Add("@status", MySqlDbType.VarChar).Value = "Б/С";
                            }
                        }
                    }
                }
                if (checkBox4.CheckState == CheckState.Checked)
                {
                    command.Parameters.Add("@ageCP", MySqlDbType.VarChar).Value = "старый";
                }
                else
                {
                    command.Parameters.Add("@ageCP", MySqlDbType.VarChar).Value = "новый";
                }

                db.openConnection();

                command.ExecuteNonQuery();

                db.closeConnection();

                textBox2.Text = "";
                checkBox1.CheckState = CheckState.Unchecked;
                checkBox2.CheckState = CheckState.Unchecked;
                checkBox3.CheckState = CheckState.Unchecked;
                checkBox4.CheckState = CheckState.Unchecked;
                dataGridView1.Rows.Clear();
                command = new MySqlCommand("SELECT * FROM `counterparty` ORDER BY `name` ASC", db.getConnection());
                MySqlDataReader myReader;
                List<CounterObject> counter = new List<CounterObject>();
                int k = 0;
                try
                {
                    db.openConnection();
                    myReader = command.ExecuteReader();

                    int j = 0;
                    while (myReader.Read())
                    {
                        string Name = myReader.GetString("name");
                        string objName = myReader.GetString("status");
                        bool f = true;
                        for (int i = 0; i < counter.Count; i++)
                        {
                            if (counter[i].name.Equals(Name))
                            {
                                if (!counter[i].status.Equals(objName))
                                {
                                    if (!objName.Equals("пусто"))
                                    {
                                        counter[i].status = objName;
                                    }
                                }
                                f = false;
                            }
                        }
                        if (f)
                        {
                            counter.Add(new CounterObject(Name, objName, myReader.GetString("ageCP")));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                db.closeConnection();
                for (int i = 0; i < counter.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, k].Value = counter[i].name;
                    dataGridView1[1, k].Value = counter[i].status;
                    if (counter[i].age.Equals("старый"))
                    {
                        dataGridView1[2, k].Value = "Нет";
                    }
                    else
                    {
                        dataGridView1[2, k].Value = "Да";
                    }
                    k++;
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Dictionaries dictionaries = new Dictionaries();
            dictionaries.Show();
            this.Close();
        }

        Point lastPoint;
        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }


        private void panel2_MouseDown_1(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel2_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = dataGridView1.CurrentRow.Index;
            val = dataGridView1[0, idx].Value.ToString();
            if(dataGridView1[1, idx].Value != null)
            {
                if (dataGridView1[1, idx].Value.Equals("Диспетчер"))
                {
                    checkBox1.CheckState = CheckState.Unchecked;
                    checkBox2.CheckState = CheckState.Unchecked;
                    checkBox3.CheckState = CheckState.Checked;
                }
                if (dataGridView1[1, idx].Value.Equals("Грузополучатель/грузоотправитель"))
                {
                    checkBox1.CheckState = CheckState.Checked;
                    checkBox2.CheckState = CheckState.Checked;
                    checkBox3.CheckState = CheckState.Unchecked;
                }
                if (dataGridView1[1, idx].Value.Equals("Грузополучатель"))
                {
                    checkBox1.CheckState = CheckState.Checked;
                    checkBox2.CheckState = CheckState.Unchecked;
                    checkBox3.CheckState = CheckState.Unchecked;
                }
                if (dataGridView1[1, idx].Value.Equals("Б/С"))
                {
                    checkBox1.CheckState = CheckState.Unchecked;
                    checkBox2.CheckState = CheckState.Unchecked;
                    checkBox3.CheckState = CheckState.Unchecked;
                }
                if (dataGridView1[1, idx].Value.Equals("Грузоотправитель"))
                {
                    checkBox1.CheckState = CheckState.Unchecked;
                    checkBox2.CheckState = CheckState.Checked;
                    checkBox3.CheckState = CheckState.Unchecked;
                }
            }
            textBox2.Text = dataGridView1[0, idx].Value.ToString();
            if (dataGridView1[2, idx].Value.Equals("Нет"))
            {
                checkBox4.CheckState = CheckState.Checked; ;
            }
            else
            {
                checkBox4.CheckState = CheckState.Unchecked;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (checkBox3.CheckState == CheckState.Checked && ((checkBox1.CheckState == CheckState.Checked) || (checkBox2.CheckState == CheckState.Checked)))
            {
                MessageBox.Show("Неверный статус");
            }
            else
            {
                DB db = new DB();
                String Line = "";
                if (!textBox2.Text.Equals(""))
                {
                    if (checkBox3.CheckState == CheckState.Checked)
                    {
                        Line = "UPDATE `counterparty` SET `name` = '" + textBox2.Text + "', `status` = 'Диспетчер'";
                    }
                    else
                    {
                        if ((checkBox1.CheckState == CheckState.Checked) && (checkBox2.CheckState == CheckState.Checked))
                        {
                            Line = "UPDATE `counterparty` SET `name` = '" + textBox2.Text + "', `status` = 'Грузополучатель/грузоотправитель'";
                        }
                        else
                        {
                            if (checkBox1.CheckState == CheckState.Checked)
                            {
                                Line = "UPDATE `counterparty` SET `name` = '" + textBox2.Text + "', `status` = 'Грузополучатель'";
                            }
                            else
                            {
                                if (checkBox2.CheckState == CheckState.Checked)
                                {
                                    Line = "UPDATE `counterparty` SET `name` = '" + textBox2.Text + "', `status` = 'Грузоотправитель'";
                                }
                            }
                        }
                    }
                    if(checkBox1.CheckState == CheckState.Unchecked && checkBox2.CheckState == CheckState.Unchecked && checkBox3.CheckState == CheckState.Unchecked)
                    {
                        Line = "UPDATE `counterparty` SET `name` = '" + textBox2.Text + "', `status` = 'Б/С'";
                    }
                }
                else
                {
                    MessageBox.Show("Вы не ввели название контрагенту");
                }

                if (checkBox4.CheckState == CheckState.Checked)
                {
                    Line += ", `ageCP` = 'старый' WHERE `name` = '" + val + "'";
                }
                else
                {
                    Line += ", `ageCP` = 'новый' WHERE `name` = '" + val + "'";
                }

                MySqlCommand command = new MySqlCommand(Line, db.getConnection());

                db.openConnection();

                command.ExecuteNonQuery();

                db.closeConnection();

                if (!textBox2.Text.Equals(""))
                {
                    MySqlCommand command2 = new MySqlCommand("UPDATE `request` SET `buyer` = '" + textBox2.Text + "' WHERE `buyer` = '" + val + "'", db.getConnection());
                    db.openConnection();

                    command2.ExecuteNonQuery();

                    db.closeConnection();

                    command2 = new MySqlCommand("UPDATE `request` SET `sender` = '" + textBox2.Text + "' WHERE `sender` = '" + val + "'", db.getConnection());
                    db.openConnection();

                    command2.ExecuteNonQuery();

                    db.closeConnection();

                    command2 = new MySqlCommand("UPDATE `request` SET `recipient` = '" + textBox2.Text + "' WHERE `recipient` = '" + val + "'", db.getConnection());
                    db.openConnection();

                    command2.ExecuteNonQuery();

                    db.closeConnection();
                    
                    command2 = new MySqlCommand("UPDATE `request` SET `fromCounterparty` = '" + textBox2.Text + "' WHERE `fromCounterparty` = '" + val + "'", db.getConnection());
                    db.openConnection();

                    command2.ExecuteNonQuery();

                    db.closeConnection();
                }

                textBox2.Text = "";
                checkBox1.CheckState = CheckState.Unchecked;
                checkBox2.CheckState = CheckState.Unchecked;
                checkBox3.CheckState = CheckState.Unchecked;
                checkBox4.CheckState = CheckState.Unchecked;
                dataGridView1.Rows.Clear();
                command = new MySqlCommand("SELECT * FROM `counterparty` ORDER BY `name` ASC", db.getConnection());
                MySqlDataReader myReader;
                List<CounterObject> counter = new List<CounterObject>();
                int k = 0;
                try
                {
                    db.openConnection();
                    myReader = command.ExecuteReader();

                    int j = 0;
                    while (myReader.Read())
                    {
                        string Name = myReader.GetString("name");
                        string objName = myReader.GetString("status");
                        bool f = true;
                        for (int i = 0; i < counter.Count; i++)
                        {
                            if (counter[i].name.Equals(Name))
                            {
                                if (!counter[i].status.Equals(objName))
                                {
                                    if (!objName.Equals("пусто"))
                                    {
                                        counter[i].status = objName;
                                    }
                                }
                                f = false;
                            }
                        }
                        if (f)
                        {
                            counter.Add(new CounterObject(Name, objName, myReader.GetString("ageCP")));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                db.closeConnection();
                for (int i = 0; i < counter.Count; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1[0, k].Value = counter[i].name;
                    dataGridView1[1, k].Value = counter[i].status;
                    if (counter[i].age.Equals("старый"))
                    {
                        dataGridView1[2, k].Value = "Нет";
                    }
                    else
                    {
                        dataGridView1[2, k].Value = "Да";
                    }
                    k++;
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("DELETE FROM `counterparty` WHERE `name` = '" + val + "'", db.getConnection());

            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            textBox2.Text = "";
            checkBox1.CheckState = CheckState.Unchecked;
            checkBox2.CheckState = CheckState.Unchecked;
            checkBox3.CheckState = CheckState.Unchecked;
            checkBox4.CheckState = CheckState.Unchecked;
            dataGridView1.Rows.Clear();
            command = new MySqlCommand("SELECT * FROM `counterparty` ORDER BY `name` ASC", db.getConnection());
            MySqlDataReader myReader;
            List<CounterObject> counter = new List<CounterObject>();
            int k = 0;
            try
            {
                db.openConnection();
                myReader = command.ExecuteReader();

                int j = 0;
                while (myReader.Read())
                {
                    string Name = myReader.GetString("name");
                    string objName = myReader.GetString("status");
                    bool f = true;
                    for (int i = 0; i < counter.Count; i++)
                    {
                        if (counter[i].name.Equals(Name))
                        {
                            if (!counter[i].status.Equals(objName))
                            {
                                if (!objName.Equals("пусто"))
                                {
                                    counter[i].status = objName;
                                }
                            }
                            f = false;
                        }
                    }
                    if (f)
                    {
                        counter.Add(new CounterObject(Name, objName, myReader.GetString("ageCP")));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            for (int i = 0; i < counter.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, k].Value = counter[i].name;
                dataGridView1[1, k].Value = counter[i].status;
                if (counter[i].age.Equals("старый"))
                {
                    dataGridView1[2, k].Value = "Нет";
                }
                else
                {
                    dataGridView1[2, k].Value = "Да";
                }
                k++;
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
    }
}
