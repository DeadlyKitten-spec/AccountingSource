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
    public partial class ObjectCounterparty : Form
    {
        public ObjectCounterparty()
        {
            InitializeComponent();

            int ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            int ScreenHeight = Screen.PrimaryScreen.Bounds.Height;

            this.Location = new Point((ScreenWidth / 2) - (this.Width / 2), (ScreenHeight / 2) - (this.Height / 2));

            FillCombo1();

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            label4.Text = "Расценок \nпокупателю";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            this.ControlBox = false;
        }

        public string val = "";

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Dictionaries dictionaries = new Dictionaries();
            dictionaries.Show();
            this.Close();
        }

        void FillCombo1()
        {
            string Query = "SELECT * FROM `counterparty` ORDER BY `name` ASC";
            DB db = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            List<string> names = new List<string>();
            try
            {
                db.openConnection();
                myReader = cmdDataBase.ExecuteReader();

                while (myReader.Read())
                {
                    string objName = myReader.GetString("name");
                    bool f = true;
                    for (int i = 0; i < names.Count; i++)
                    {
                        if (names[i].Equals(objName))
                        {
                            f = false;
                            break;
                        }
                    }
                    if (f)
                    {
                        names.Add(objName);
                        comboBox1.Items.Add(objName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string Query = "SELECT * FROM `counterparty` WHERE `name` = '" + comboBox1.Text.ToString() + "' order by `objectName`;";
            DB db = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            List<ForPrice> frprice = new List<ForPrice>();
            int k = 0;
            try
            {
                db.openConnection();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    string name = myReader.GetString("name");
                    string objName = myReader.GetString("objectName");
                    string priceCount = myReader.GetString("priceCount");
                    string priceCountBuy = myReader.GetString("priceBuyerCount");
                    int itter = 0;
                    if (objName.Equals("пусто"))
                    {
                        continue;
                    }
                    frprice.Add(new ForPrice(name, objName, myReader.GetString("price" + priceCount), priceCount, myReader.GetString("priceBuyer" + priceCountBuy), priceCountBuy, myReader.GetString("ageOb")));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            for (int i = 0; i < frprice.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, k].Value = frprice[i].counterparty;
                dataGridView1[1, k].Value = frprice[i].objectt;
                dataGridView1[2, k].Value = frprice[i].price;
                dataGridView1[3, k].Value = frprice[i].pricebuy;
                if (frprice[i].age.Equals("старый"))
                {
                    dataGridView1[4, k].Value = "Нет";
                }
                else
                {
                    dataGridView1[4, k].Value = "Да";
                }
                k++;
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }


        


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            MySqlCommand command = new MySqlCommand("INSERT INTO `counterparty` (`name`, `objectName`, `status`, `price1`, `priceCount`, `priceBuyer1`, `priceBuyerCount`, `ageOb`) VALUES (@name, @object, @status, @price, @priceCount, @priceBuyer, @priceBuyerCount, @ageOb)", db.getConnection());

            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = comboBox1.Text;
            command.Parameters.Add("@object", MySqlDbType.VarChar).Value = textBox2.Text;
            string num = "0";
            if (!textBox1.Text.Equals(""))
            {
                num = textBox1.Text;
                for (int j = 0; j < num.Length; j++)
                {
                    if (num[j] == '.')
                    {
                        string[] ans = num.Split('.');
                        num = ans[0] + "," + ans[1];
                        break;
                    }
                }
            }
            string numbuy = "0";
            if (!textBox3.Text.Equals(""))
            {
                numbuy = textBox3.Text;
                for (int j = 0; j < numbuy.Length; j++)
                {
                    if (numbuy[j] == '.')
                    {
                        string[] ans = numbuy.Split('.');
                        numbuy = ans[0] + "," + ans[1];
                        break;
                    }
                }
            }
            command.Parameters.Add("@status", MySqlDbType.VarChar).Value = "пусто";
            command.Parameters.Add("@price", MySqlDbType.VarChar).Value = num;
            command.Parameters.Add("@priceCount", MySqlDbType.Int32).Value = 1;
            command.Parameters.Add("@priceBuyer", MySqlDbType.VarChar).Value = numbuy;
            command.Parameters.Add("@priceBuyerCount", MySqlDbType.Int32).Value = 1;
            if (checkBox1.CheckState == CheckState.Checked)
            {
                command.Parameters.Add("@ageOb", MySqlDbType.VarChar).Value = "старый";
            }
            else
            {
                command.Parameters.Add("@ageOb", MySqlDbType.VarChar).Value = "новый";
            }

            db.openConnection();
            command.ExecuteNonQuery();
            db.closeConnection();
            dataGridView1.Rows.Clear();
            string Query = "SELECT * FROM `counterparty` WHERE `name` = '" + comboBox1.Text.ToString() + "' ORDER BY `objectName` ASC;";
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            int k = 0;
            List<ForPrice> frprice = new List<ForPrice>();
            try
            {
                db.openConnection();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    string name = myReader.GetString("name");
                    string objName = myReader.GetString("objectName");
                    string priceCount = myReader.GetString("priceCount");
                    string priceCountBuy = myReader.GetString("priceBuyerCount");
                    int itter = 0;
                    if (objName.Equals("пусто"))
                    {
                        continue;
                    }
                    frprice.Add(new ForPrice(name, objName, myReader.GetString("price" + priceCount), priceCount, myReader.GetString("priceBuyer" + priceCountBuy), priceCountBuy, myReader.GetString("ageOb")));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            for (int i = 0; i < frprice.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, k].Value = frprice[i].counterparty;
                dataGridView1[1, k].Value = frprice[i].objectt;
                dataGridView1[2, k].Value = frprice[i].price;
                dataGridView1[3, k].Value = frprice[i].pricebuy;
                if (frprice[i].age.Equals("старый"))
                {
                    dataGridView1[4, k].Value = "Нет";
                }
                else
                {
                    dataGridView1[4, k].Value = "Да";
                }
                k++;
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            textBox2.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";
            checkBox1.CheckState = CheckState.Unchecked;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            string Query2 = "SELECT * FROM `counterparty` WHERE `name` = '" + comboBox1.Text.ToString() + "' and `objectName` = '" + dataGridView1["objectt", dataGridView1.CurrentRow.Index].Value.ToString() + "' ORDER BY `name` ASC;";
            MySqlCommand cmdDataBase2 = new MySqlCommand(Query2, db.getConnection());
            MySqlDataReader myReader2;
            db.openConnection();
            myReader2 = cmdDataBase2.ExecuteReader();
            myReader2.Read();
            string priceCountt = myReader2.GetString("priceCount");
            string priceBuyerCount = myReader2.GetString("priceBuyerCount");
            string pricec = "price" + priceCountt;
            string pricecbuy = "priceBuyer" + priceBuyerCount;
            bool d = false;
            bool dbuy = false;
            if (!myReader2.GetString(pricec).Equals("0"))
            {
                int a = int.Parse(priceCountt);
                a++;
                priceCountt = a.ToString();
                pricec = "price" + priceCountt;
                d = true;
            }
            if (!myReader2.GetString(pricecbuy).Equals("0"))
            {
                int a = int.Parse(priceBuyerCount);
                a++;
                priceBuyerCount = a.ToString();
                pricecbuy = "priceBuyer" + priceBuyerCount;
                dbuy = true;
            }
            string num = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            if (!textBox1.Text.Equals(""))
            {
                num = textBox1.Text;
                for (int j = 0; j < num.Length; j++)
                {
                    if (num[j] == '.')
                    {
                        string[] ans = num.Split('.');
                        num = ans[0] + "," + ans[1];
                        break;
                    }
                }
            }
            string numbuy = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            if (!textBox3.Text.Equals(""))
            {
                numbuy = textBox3.Text;
                for (int j = 0; j < numbuy.Length; j++)
                {
                    if (numbuy[j] == '.')
                    {
                        string[] ans = numbuy.Split('.');
                        numbuy = ans[0] + "," + ans[1];
                        break;
                    }
                }
            }
            string objectName = "";
            if (textBox2.Text.Equals(""))
            {
                objectName = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            }
            else
            {
                objectName = textBox2.Text;
            }
            db.closeConnection();
            myReader2.Close();
            MySqlCommand command = new MySqlCommand();
            string ageLine = "";
            if (checkBox1.CheckState == CheckState.Checked)
            {
                ageLine = "старый";
            }
            else
            {
                ageLine = "новый";
            }
            command = new MySqlCommand("UPDATE `counterparty` SET `objectName` = '" + objectName + "' , `" + pricec + "` = '" + num + "', `" + pricecbuy + "` = '" + numbuy + "', `priceCount` = '" + priceCountt + "', `priceBuyerCount` = '" + priceBuyerCount + "', `ageOb` = '" + ageLine + "' WHERE `objectName` = '" + val + "' and `name` = '" + dataGridView1["name", dataGridView1.CurrentRow.Index].Value.ToString() + "'", db.getConnection());
            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            MySqlCommand command2 = new MySqlCommand("UPDATE `request` SET `object` = '" + objectName + "' WHERE `object` = '" + val + "' and `buyer` = '" + dataGridView1["name", dataGridView1.CurrentRow.Index].Value.ToString() + "'", db.getConnection());
            db.openConnection();

            command2.ExecuteNonQuery();

            db.closeConnection();

            command2 = new MySqlCommand("UPDATE `request` SET `objectArrive` = '" + objectName + "' WHERE `objectArrive` = '" + val + "' and `recipient` = '" + dataGridView1["name", dataGridView1.CurrentRow.Index].Value.ToString() + "'", db.getConnection());
            db.openConnection();

            command2.ExecuteNonQuery();

            db.closeConnection();

            command2 = new MySqlCommand("UPDATE `request` SET `objectSend` = '" + objectName + "' WHERE `objectSend` = '" + val + "'", db.getConnection());
            db.openConnection();

            command2.ExecuteNonQuery();

            db.closeConnection();

            textBox2.Text = "";
            checkBox1.CheckState = CheckState.Unchecked;
            dataGridView1.Rows.Clear();
            string Query = "SELECT * FROM `counterparty` WHERE `name` = '" + comboBox1.Text.ToString() + "' order by `objectName` ASC;";
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            int k = 0;
            List<ForPrice> frprice = new List<ForPrice>();
            try
            {
                db.openConnection();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    string name = myReader.GetString("name");
                    string objName = myReader.GetString("objectName");
                    string priceCount = myReader.GetString("priceCount");
                    string priceCountBuy = myReader.GetString("priceBuyerCount");
                    int itter = 0;
                    if (objName.Equals("пусто"))
                    {
                        continue;
                    }
                    frprice.Add(new ForPrice(name, objName, myReader.GetString("price" + priceCount), priceCount, myReader.GetString("priceBuyer" + priceCountBuy), priceCountBuy, myReader.GetString("ageOb")));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            for (int i = 0; i < frprice.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, k].Value = frprice[i].counterparty;
                dataGridView1[1, k].Value = frprice[i].objectt;
                dataGridView1[2, k].Value = frprice[i].price;
                dataGridView1[3, k].Value = frprice[i].pricebuy;
                if (frprice[i].age.Equals("старый"))
                {
                    dataGridView1[4, k].Value = "Нет";
                }
                else
                {
                    dataGridView1[4, k].Value = "Да";
                }
                k++;
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


            db.closeConnection();

            textBox2.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = dataGridView1.CurrentRow.Index;
            val = dataGridView1[1, idx].Value.ToString();
            textBox2.Text = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            textBox1.Text = dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString();
            textBox3.Text = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
            if(dataGridView1[4, dataGridView1.CurrentRow.Index].Value.Equals("Нет"))
            {
                checkBox1.CheckState = CheckState.Checked;
            }
            else
            {
                checkBox1.CheckState = CheckState.Unchecked;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("DELETE FROM `counterparty` WHERE `objectName` = '" + val + "' and `name` = '" + dataGridView1["name", dataGridView1.CurrentRow.Index].Value.ToString() + "'", db.getConnection());

            db.openConnection();

            command.ExecuteNonQuery();

            db.closeConnection();

            textBox2.Text = "";
            dataGridView1.Rows.Clear();
            string Query = "SELECT * FROM `counterparty` WHERE `name` = '" + comboBox1.Text.ToString() + "' order by `objectName` ASC;";
            MySqlCommand cmdDataBase = new MySqlCommand(Query, db.getConnection());
            MySqlDataReader myReader;
            List<ForPrice> frprice = new List<ForPrice>();
            int k = 0;
            try
            {
                db.openConnection();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    string name = myReader.GetString("name");
                    string objName = myReader.GetString("objectName");
                    string priceCount = myReader.GetString("priceCount");
                    string priceCountBuy = myReader.GetString("priceBuyerCount");
                    int itter = 0;
                    if (objName.Equals("пусто"))
                    {
                        continue;
                    }
                    frprice.Add(new ForPrice(name, objName, myReader.GetString("price" + priceCount), priceCount, myReader.GetString("priceBuyer" + priceCountBuy), priceCountBuy, myReader.GetString("ageOb")));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            db.closeConnection();
            for (int i = 0; i < frprice.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1[0, k].Value = frprice[i].counterparty;
                dataGridView1[1, k].Value = frprice[i].objectt;
                dataGridView1[2, k].Value = frprice[i].price;
                dataGridView1[3, k].Value = frprice[i].pricebuy;
                if (frprice[i].age.Equals("старый"))
                {
                    dataGridView1[4, k].Value = "Нет";
                }
                else
                {
                    dataGridView1[4, k].Value = "Да";
                }
                k++;
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


            db.closeConnection();
            textBox2.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";
            checkBox1.CheckState = CheckState.Unchecked;
        }
    }
}
