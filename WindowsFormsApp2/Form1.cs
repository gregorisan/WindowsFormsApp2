using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : MetroForm

    {
        SqlConnection SqlConnection;
        //Картинка
        public Form1()
        {
            InitializeComponent();




        }
        //Предмет ТРПО
        private async void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Csharp\SQL\WindowsFormsApp2\WindowsFormsApp2\Database1.mdf;Integrated Security=True";
            SqlConnection = new SqlConnection(connectionString);

            await SqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("Select *  FROM [Predmet]", SqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["TRPO"] + " " + Convert.ToString(sqlReader["Name"]) + " " + Convert.ToString(sqlReader["Ocenka"]) + " " + Convert.ToString(sqlReader["1"]) + " " + Convert.ToString(sqlReader["2"]) + " " + Convert.ToString(sqlReader["3"] + " " + Convert.ToString(sqlReader["4"] + " " + Convert.ToString(sqlReader["5"] + " " + Convert.ToString(sqlReader["6"] + " " + Convert.ToString(sqlReader["7"])))))));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }

        }

        //Здесь должен был быть выход, но пока не сделал
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SqlConnection != null && SqlConnection.State != ConnectionState.Closed)
                SqlConnection.Close();
        }
        //Добавление в ТРПО
        private async void button1_Click(object sender, EventArgs e)
        {
            if (label6.Visible)
                label6.Visible = false;
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text))
                  

            {
                SqlCommand command = new SqlCommand("Insert INTO [Predmet]  (Name,Ocenka)VALUES(@Name,@Ocenka)", SqlConnection);

                command.Parameters.AddWithValue("Name", textBox1.Text);
                command.Parameters.AddWithValue("Ocenka", textBox2.Text);

                await command.ExecuteNonQueryAsync();


            }
            else
            {
                label6.Visible = true;
                label6.Text = "Поля должны быть заполнены!";
            }
        }
        //Обноление ТРПО
        private async void обновитьToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {


            listBox1.Items.Clear();

            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("Select *  FROM [Predmet]", SqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["TRPO"] + " " + Convert.ToString(sqlReader["Name"]) + " " + Convert.ToString(sqlReader["Ocenka"]) + " " + Convert.ToString(sqlReader["1"]) + " " + Convert.ToString(sqlReader["2"]) + " " + Convert.ToString(sqlReader["3"])));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }
        //Изменение в ТРПО
        private async void button2_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;
            if
                    (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlCommand command = new SqlCommand("Update [Predmet] SET [Name]=@Name, [Ocenka]=@Ocenka  WHERE [Name]=@Name  ", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox4.Text);
                command.Parameters.AddWithValue("Name", textBox6.Text);
                command.Parameters.AddWithValue("Ocenka", textBox3.Text);

                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }

        }


        //Удаление из ТРПО
        private async void button3_Click(object sender, EventArgs e)

        {
            if (label9.Visible)
                label9.Visible = false;
            if (!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [PREDMET] WHERE [TRPO]=@TRPO", SqlConnection);

                command.Parameters.AddWithValue("TRPO", textBox5.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label9.Visible = true;
                label9.Text = "Поля ID должны быть заполнены!";
            }
            {

                DialogResult result = MessageBox.Show("Удалить?", "ЗаписьA", MessageBoxButtons.YesNo);
                switch (result)
                {
                    case DialogResult.Yes:
                        {

                            Form1 form1 = new Form1();
                            form1.Show();
                            break;
                        }
                    case DialogResult.No:
                        {
                            MessageBox.Show("Досвидание");
                            break;
                        }
                }
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //Предмет СУБД
        private async void button4_Click(object sender, EventArgs e)
        {
            listBox3.Visible = true;
            listBox1.Visible = false;
            listBox2.Visible = false;
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Csharp\SQL\WindowsFormsApp2\WindowsFormsApp2\Database1.mdf;Integrated Security=True";
            SqlConnection = new SqlConnection(connectionString);

            await SqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("Select *  FROM [Predmet2]", SqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox3.Items.Add(Convert.ToString(sqlReader["SYBD"] + " " + Convert.ToString(sqlReader["Name"]) + " " + Convert.ToString(sqlReader["Ocenka"] + " " + Convert.ToString(sqlReader["1"]) + " " + Convert.ToString(sqlReader["2"]) + " " + Convert.ToString(sqlReader["3"] + " " + Convert.ToString(sqlReader["4"] + " " + Convert.ToString(sqlReader["5"] + " " + Convert.ToString(sqlReader["6"] + " " + Convert.ToString(sqlReader["7"]))))))));

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }

        }
        //Добавление в СУБД
        private async void button5_Click(object sender, EventArgs e)
        {
            if (label6.Visible)
                label6.Visible = false;
            if (!string.IsNullOrEmpty(textBox10.Text) && !string.IsNullOrWhiteSpace(textBox10.Text))

            {
                SqlCommand command = new SqlCommand("Insert INTO [Predmet2] (Name)VALUES(@Name)", SqlConnection);
                command.Parameters.AddWithValue("Name", textBox10.Text);
               

                await command.ExecuteNonQueryAsync();


            }
            else
            {
                label6.Visible = true;
                label6.Text = "Поля должны быть заполнены!";
            }
        }


        //Не знаю, как это убрать 
        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        //Изменение в СУБД
        private async void button7_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;
            if (!string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text))
            {
                SqlCommand command = new SqlCommand("Update [Predmet2] SET [Name]=@Name, [Ocenka]=@Ocenka WHERE [Name]=@Name", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox7.Text);
                command.Parameters.AddWithValue("Name", textBox8.Text);
                command.Parameters.AddWithValue("Ocenka", textBox9.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }

        }
        //Обновление
        private async void обновитьКПиЯПToolStripMenuItem_Click(object sender, EventArgs e)
        {

            listBox3.Items.Clear();

            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("Select *  FROM [Predmet2]", SqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["SYBD"] + " " + Convert.ToString(sqlReader["Name"]) + " " + Convert.ToString(sqlReader["Ocenka"])));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }
        //Удаление из СУБД
        private async void button6_Click(object sender, EventArgs e)
        {
            {
                if (label9.Visible)
                    label9.Visible = false;
                if (!string.IsNullOrEmpty(textBox12.Text) && !string.IsNullOrWhiteSpace(textBox12.Text))
                {
                    SqlCommand command = new SqlCommand("DELETE FROM [PREDMET2] WHERE [SYBD]=@SYBD", SqlConnection);

                    command.Parameters.AddWithValue("SYBD", textBox12.Text);
                    await command.ExecuteNonQueryAsync();
                }
                else if (!string.IsNullOrEmpty(textBox12.Text) && !string.IsNullOrWhiteSpace(textBox12.Text))
                {
                    label7.Visible = true;
                    label7.Text = "Поля ID должны быть заполнены!";
                }
                else
                {
                    label9.Visible = true;
                    label9.Text = "Поля ID должны быть заполнены!";
                }


            }
        }
        //Предмет КПиЯП 
        private async void button9_Click(object sender, EventArgs e)
        {
            {
                listBox2.Visible = true;


                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Csharp\SQL\WindowsFormsApp2\WindowsFormsApp2\Database1.mdf;Integrated Security=True";
                SqlConnection = new SqlConnection(connectionString);

                await SqlConnection.OpenAsync();
                SqlDataReader sqlReader = null;
                SqlCommand command = new SqlCommand("Select *  FROM [Predmet3]", SqlConnection);

                try
                {
                    sqlReader = await command.ExecuteReaderAsync();

                    while (await sqlReader.ReadAsync())
                    {
                        listBox3.Items.Add(Convert.ToString(sqlReader["KPuIP"] + " " + Convert.ToString(sqlReader["Name"]) + " " + Convert.ToString(sqlReader["Ocenka"]) + " " + Convert.ToString(sqlReader["1"]) + " " + Convert.ToString(sqlReader["2"]) + " " + Convert.ToString(sqlReader["3"] + " " + Convert.ToString(sqlReader["4"] + " " + Convert.ToString(sqlReader["5"] + " " + Convert.ToString(sqlReader["6"]))))));

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (sqlReader != null)
                        sqlReader.Close();
                }

            }
        }
        //Добавление в КПиЯП
        private async void button8_Click(object sender, EventArgs e)
        {
            if (label6.Visible)
                label6.Visible = false;
            if (!string.IsNullOrEmpty(textBox13.Text) && !string.IsNullOrWhiteSpace(textBox13.Text))

            {
                SqlCommand command = new SqlCommand("Insert INTO [Predmet3] (Name)VALUES(@Name)", SqlConnection);
                command.Parameters.AddWithValue("Name", textBox13.Text);
               

                await command.ExecuteNonQueryAsync();


            }
            else
            {
                label6.Visible = true;
                label6.Text = "Поля должны быть заполнены!";
            }
        }
        //Изменение в КПиЯП
        private async void button10_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;
            if (!string.IsNullOrEmpty(textBox15.Text) && !string.IsNullOrWhiteSpace(textBox15.Text) &&
                    !string.IsNullOrEmpty(textBox16.Text) && !string.IsNullOrWhiteSpace(textBox16.Text))

            {
                SqlCommand command = new SqlCommand("Update [Predmet3] SET [Name]=@Name, [Ocenka]=@Ocenka WHERE [Name]=@Name", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox15.Text);
                command.Parameters.AddWithValue("Name", textBox16.Text);
                command.Parameters.AddWithValue("Ocenka", textBox17.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox15.Text) && !string.IsNullOrWhiteSpace(textBox15.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }

        }
        //Удаление из КПиЯП
        private async void button11_Click(object sender, EventArgs e)
        {
            {
                if (label9.Visible)
                    label9.Visible = false;
                if (!string.IsNullOrEmpty(textBox18.Text) && !string.IsNullOrWhiteSpace(textBox18.Text))
                {
                    SqlCommand command = new SqlCommand("DELETE FROM [PREDMET3] WHERE [KPuIP]=@KPuIP", SqlConnection);

                    command.Parameters.AddWithValue("KPuIP", textBox18.Text);
                    await command.ExecuteNonQueryAsync();
                }
                else if (!string.IsNullOrEmpty(textBox18.Text) && !string.IsNullOrWhiteSpace(textBox18.Text)) ;



            }
        }

        private async void button12_Click_1(object sender, EventArgs e)
        {

            if (label7.Visible)
                label7.Visible = false;
            if
                    (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlCommand command = new SqlCommand("Update [Predmet] SET [1]=@1  WHERE [Name]=@Name  ", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox4.Text);
                command.Parameters.AddWithValue("Name", textBox6.Text);
                command.Parameters.AddWithValue("1", textBox19.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }

        }

        private async void button13_Click(object sender, EventArgs e)
        {

            if (label7.Visible)
                label7.Visible = false;
            if
                    (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlCommand command = new SqlCommand("Update [Predmet] SET [2]=@2  WHERE [Name]=@Name  ", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox4.Text);
                command.Parameters.AddWithValue("Name", textBox6.Text);
                command.Parameters.AddWithValue("2", textBox20.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }

        }

        private async void button14_Click(object sender, EventArgs e)
        {

            if (label7.Visible)
                label7.Visible = false;
            if
                    (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlCommand command = new SqlCommand("Update [Predmet] SET [3]=@3  WHERE [Name]=@Name  ", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox4.Text);
                command.Parameters.AddWithValue("Name", textBox6.Text);
                command.Parameters.AddWithValue("3", textBox21.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }

        }

        private async void button15_Click(object sender, EventArgs e)
        {

            if (label7.Visible)
                label7.Visible = false;
            if
                    (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlCommand command = new SqlCommand("Update [Predmet] SET [4]=@4  WHERE [Name]=@Name  ", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox4.Text);
                command.Parameters.AddWithValue("Name", textBox6.Text);
                command.Parameters.AddWithValue("4", textBox22.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }

        }

        private async void button16_Click(object sender, EventArgs e)
        {

            if (label7.Visible)
                label7.Visible = false;
            if
                    (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                SqlCommand command = new SqlCommand("Update [Predmet] SET [5]=@5  WHERE [Name]=@Name  ", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox4.Text);
                command.Parameters.AddWithValue("Name", textBox6.Text);
                command.Parameters.AddWithValue("5", textBox23.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }

        }

        private async void button17_Click(object sender, EventArgs e)
        {
            {

                if (label7.Visible)
                    label7.Visible = false;
                if
                        (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                    !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
                {
                    SqlCommand command = new SqlCommand("Update [Predmet] SET [6]=@6  WHERE [Name]=@Name  ", SqlConnection);

                    command.Parameters.AddWithValue("Id", textBox4.Text);
                    command.Parameters.AddWithValue("Name", textBox6.Text);
                    command.Parameters.AddWithValue("6", textBox24.Text);
                    await command.ExecuteNonQueryAsync();
                }
                else if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    label7.Visible = true;
                    label7.Text = "Поля ID должны быть заполнены!";
                }
                else
                {
                    label7.Visible = true;
                    label7.Text = "Поля должны быть заполнены!";

                }

            }
        }



        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
        // КНОПКИ СУБД
        private async void button18_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;
            if
                    (!string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text))

            {
                SqlCommand command = new SqlCommand("Update [Predmet2] SET  [1]=@1 WHERE [Name]=@Name", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox7.Text);
                command.Parameters.AddWithValue("Name", textBox8.Text);
                command.Parameters.AddWithValue("1", textBox25.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }

        }

        private async void button19_Click(object sender, EventArgs e)
        {
            {
                if (label7.Visible)
                    label7.Visible = false;
                if
                        (!string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text))

                {
                    SqlCommand command = new SqlCommand("Update [Predmet2] SET  [2]=@2 WHERE [Name]=@Name", SqlConnection);

                    command.Parameters.AddWithValue("Id", textBox7.Text);
                    command.Parameters.AddWithValue("Name", textBox8.Text);
                    command.Parameters.AddWithValue("2", textBox26.Text);
                    await command.ExecuteNonQueryAsync();
                }
                else if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    label7.Visible = true;
                    label7.Text = "Поля ID должны быть заполнены!";
                }
                else
                {
                    label7.Visible = true;
                    label7.Text = "Поля должны быть заполнены!";

                }
            }
        }

        private async void button20_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;
            if
                    (!string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text))

            {
                SqlCommand command = new SqlCommand("Update [Predmet2] SET  [3]=@3 WHERE [Name]=@Name", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox7.Text);
                command.Parameters.AddWithValue("Name", textBox8.Text);
                command.Parameters.AddWithValue("3", textBox27.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }
        }

        private async void button21_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;
            if
                    (!string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text))

            {
                SqlCommand command = new SqlCommand("Update [Predmet2] SET  [4]=@4 WHERE [Name]=@Name", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox7.Text);
                command.Parameters.AddWithValue("Name", textBox8.Text);
                command.Parameters.AddWithValue("4", textBox28.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }

        }

        private async void button22_Click(object sender, EventArgs e)
        {
            {
                if (label7.Visible)
                    label7.Visible = false;
                if
                        (!string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text))

                {
                    SqlCommand command = new SqlCommand("Update [Predmet2] SET  [5]=@5 WHERE [Name]=@Name", SqlConnection);

                    command.Parameters.AddWithValue("Id", textBox7.Text);
                    command.Parameters.AddWithValue("Name", textBox8.Text);
                    command.Parameters.AddWithValue("5", textBox29.Text);
                    await command.ExecuteNonQueryAsync();
                }
                else if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    label7.Visible = true;
                    label7.Text = "Поля ID должны быть заполнены!";
                }
                else
                {
                    label7.Visible = true;
                    label7.Text = "Поля должны быть заполнены!";

                }
            }
        }

        private async void button23_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;
            if
                    (!string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text))

            {
                SqlCommand command = new SqlCommand("Update [Predmet2] SET  [6]=@6 WHERE [Name]=@Name", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox7.Text);
                command.Parameters.AddWithValue("Name", textBox8.Text);
                command.Parameters.AddWithValue("6", textBox30.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }

        }
        //КНОПКИ КПиЯП
        private async void button29_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;
            if (!string.IsNullOrEmpty(textBox15.Text) && !string.IsNullOrWhiteSpace(textBox15.Text) &&
                    !string.IsNullOrEmpty(textBox16.Text) && !string.IsNullOrWhiteSpace(textBox16.Text))

            {
                SqlCommand command = new SqlCommand("Update [Predmet3] SET  [6]=@6 WHERE [Name]=@Name", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox15.Text);
                command.Parameters.AddWithValue("Name", textBox16.Text);
                command.Parameters.AddWithValue("6", textBox36.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox15.Text) && !string.IsNullOrWhiteSpace(textBox15.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }
        }

        private async void button24_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;
            if (!string.IsNullOrEmpty(textBox15.Text) && !string.IsNullOrWhiteSpace(textBox15.Text) &&
                    !string.IsNullOrEmpty(textBox16.Text) && !string.IsNullOrWhiteSpace(textBox16.Text))

            {
                SqlCommand command = new SqlCommand("Update [Predmet3] SET  [1]=@1 WHERE [Name]=@Name", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox15.Text);
                command.Parameters.AddWithValue("Name", textBox16.Text);
                command.Parameters.AddWithValue("1", textBox31.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox15.Text) && !string.IsNullOrWhiteSpace(textBox15.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }
        }

        private async void button25_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;
            if (!string.IsNullOrEmpty(textBox15.Text) && !string.IsNullOrWhiteSpace(textBox15.Text) &&
                    !string.IsNullOrEmpty(textBox16.Text) && !string.IsNullOrWhiteSpace(textBox16.Text))

            {
                SqlCommand command = new SqlCommand("Update [Predmet3] SET  [2]=@2 WHERE [Name]=@Name", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox15.Text);
                command.Parameters.AddWithValue("Name", textBox16.Text);
                command.Parameters.AddWithValue("2", textBox32.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox15.Text) && !string.IsNullOrWhiteSpace(textBox15.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }
        }

        private async void button26_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;
            if (!string.IsNullOrEmpty(textBox15.Text) && !string.IsNullOrWhiteSpace(textBox15.Text) &&
                    !string.IsNullOrEmpty(textBox16.Text) && !string.IsNullOrWhiteSpace(textBox16.Text))

            {
                SqlCommand command = new SqlCommand("Update [Predmet3] SET  [3]=@3 WHERE [Name]=@Name", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox15.Text);
                command.Parameters.AddWithValue("Name", textBox16.Text);
                command.Parameters.AddWithValue("3", textBox33.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox15.Text) && !string.IsNullOrWhiteSpace(textBox15.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }
        }

        private async void button27_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;
            if (!string.IsNullOrEmpty(textBox15.Text) && !string.IsNullOrWhiteSpace(textBox15.Text) &&
                    !string.IsNullOrEmpty(textBox16.Text) && !string.IsNullOrWhiteSpace(textBox16.Text))

            {
                SqlCommand command = new SqlCommand("Update [Predmet3] SET  [4]=@4 WHERE [Name]=@Name", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox15.Text);
                command.Parameters.AddWithValue("Name", textBox16.Text);
                command.Parameters.AddWithValue("4", textBox34.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox15.Text) && !string.IsNullOrWhiteSpace(textBox15.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }
        }

        private async void button28_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;
            if (!string.IsNullOrEmpty(textBox15.Text) && !string.IsNullOrWhiteSpace(textBox15.Text) &&
                    !string.IsNullOrEmpty(textBox16.Text) && !string.IsNullOrWhiteSpace(textBox16.Text))

            {
                SqlCommand command = new SqlCommand("Update [Predmet3] SET  [5]=@5 WHERE [Name]=@Name", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox15.Text);
                command.Parameters.AddWithValue("Name", textBox16.Text);
                command.Parameters.AddWithValue("5", textBox35.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox15.Text) && !string.IsNullOrWhiteSpace(textBox15.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }
        }

        private void button31_Click(object sender, EventArgs e)
        {

        }

        private async void button30_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button32_Click(object sender, EventArgs e)
        {
            listBox2.Visible = false;
            listBox3.Visible = false;
            listBox1.Visible = true;
        }

        private void button33_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
        }

        private async void button34_Click(object sender, EventArgs e)
        {
           
        }

        private async void button35_Click(object sender, EventArgs e)
        {
            {
                listBox2.Visible = true;


                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Csharp\SQL\WindowsFormsApp2\WindowsFormsApp2\Database1.mdf;Integrated Security=True";
                SqlConnection = new SqlConnection(connectionString);

                await SqlConnection.OpenAsync();
                SqlDataReader sqlReader = null;
                SqlCommand command = new SqlCommand("Select *  FROM [Predmet4]", SqlConnection);

                try
                {
                    sqlReader = await command.ExecuteReaderAsync();

                    while (await sqlReader.ReadAsync())
                    {
                        listBox3.Items.Add(Convert.ToString(sqlReader["ZKI"] + " " + Convert.ToString(sqlReader["Name"]) + " " + Convert.ToString(sqlReader["1"]) + " " + Convert.ToString(sqlReader["2"]) + " " + Convert.ToString(sqlReader["3"] + " " + Convert.ToString(sqlReader["4"] + " " + Convert.ToString(sqlReader["5"] + " " + Convert.ToString(sqlReader["6"]))))));

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (sqlReader != null)
                        sqlReader.Close();
                }

            }
        }

        private async void button42_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;
            if (!string.IsNullOrEmpty(textBox47.Text) && !string.IsNullOrWhiteSpace(textBox47.Text)) 

            {
                SqlCommand command = new SqlCommand("Update [Predmet4] SET  [1]=@1 WHERE [Name]=@Name", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox47.Text);
                command.Parameters.AddWithValue("Name", textBox46.Text);
                command.Parameters.AddWithValue("1", textBox45.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox47.Text) && !string.IsNullOrWhiteSpace(textBox47.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }
        }

        private async void button41_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;
            if (!string.IsNullOrEmpty(textBox47.Text) && !string.IsNullOrWhiteSpace(textBox47.Text))

            {
                SqlCommand command = new SqlCommand("Update [Predmet4] SET  [2]=@2 WHERE [Name]=@Name", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox47.Text);
                command.Parameters.AddWithValue("Name", textBox46.Text);
                command.Parameters.AddWithValue("2", textBox44.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox47.Text) && !string.IsNullOrWhiteSpace(textBox47.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }
        }

        private async void button40_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;
            if (!string.IsNullOrEmpty(textBox47.Text) && !string.IsNullOrWhiteSpace(textBox47.Text))

            {
                SqlCommand command = new SqlCommand("Update [Predmet4] SET  [3]=@3 WHERE [Name]=@Name", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox47.Text);
                command.Parameters.AddWithValue("Name", textBox46.Text);
                command.Parameters.AddWithValue("3", textBox43.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox47.Text) && !string.IsNullOrWhiteSpace(textBox47.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }
        }

        private async void button39_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;
            if (!string.IsNullOrEmpty(textBox47.Text) && !string.IsNullOrWhiteSpace(textBox47.Text))

            {
                SqlCommand command = new SqlCommand("Update [Predmet4] SET  [4]=@4 WHERE [Name]=@Name", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox47.Text);
                command.Parameters.AddWithValue("Name", textBox46.Text);
                command.Parameters.AddWithValue("4", textBox42.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox47.Text) && !string.IsNullOrWhiteSpace(textBox47.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }
        }

        private async void button38_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;
            if (!string.IsNullOrEmpty(textBox47.Text) && !string.IsNullOrWhiteSpace(textBox47.Text))

            {
                SqlCommand command = new SqlCommand("Update [Predmet4] SET  [5]=@5 WHERE [Name]=@Name", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox47.Text);
                command.Parameters.AddWithValue("Name", textBox46.Text);
                command.Parameters.AddWithValue("5", textBox41.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox47.Text) && !string.IsNullOrWhiteSpace(textBox47.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }
        }

        private async void button37_Click(object sender, EventArgs e)
        {
            {
                if (label7.Visible)
                    label7.Visible = false;
                if (!string.IsNullOrEmpty(textBox47.Text) && !string.IsNullOrWhiteSpace(textBox47.Text))

                {
                    SqlCommand command = new SqlCommand("Update [Predmet4] SET  [6]=@6 WHERE [Name]=@Name", SqlConnection);

                    command.Parameters.AddWithValue("Id", textBox47.Text);
                    command.Parameters.AddWithValue("Name", textBox46.Text);
                    command.Parameters.AddWithValue("6", textBox40.Text);
                    await command.ExecuteNonQueryAsync();
                }
                else if (!string.IsNullOrEmpty(textBox47.Text) && !string.IsNullOrWhiteSpace(textBox47.Text))
                {
                    label7.Visible = true;
                    label7.Text = "Поля ID должны быть заполнены!";
                }
                else
                {
                    label7.Visible = true;
                    label7.Text = "Поля должны быть заполнены!";

                }
            }
        }

        private async void button36_Click(object sender, EventArgs e)
        {
            if (label7.Visible)
                label7.Visible = false;
            if (!string.IsNullOrEmpty(textBox47.Text) && !string.IsNullOrWhiteSpace(textBox47.Text))

            {
                SqlCommand command = new SqlCommand("Update [Predmet4] SET  [7]=@7 WHERE [Name]=@Name", SqlConnection);

                command.Parameters.AddWithValue("Id", textBox47.Text);
                command.Parameters.AddWithValue("Name", textBox46.Text);
                command.Parameters.AddWithValue("7", textBox39.Text);
                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox47.Text) && !string.IsNullOrWhiteSpace(textBox47.Text))
            {
                label7.Visible = true;
                label7.Text = "Поля ID должны быть заполнены!";
            }
            else
            {
                label7.Visible = true;
                label7.Text = "Поля должны быть заполнены!";

            }
        }

        private async void button34_Click_1(object sender, EventArgs e)
        {
            if (label6.Visible)
                label6.Visible = false;
            if (!string.IsNullOrEmpty(textBox37.Text) && !string.IsNullOrWhiteSpace(textBox37.Text))

            {
                SqlCommand command = new SqlCommand("Insert INTO [Predmet4] (Name)VALUES(@Name)", SqlConnection);
                command.Parameters.AddWithValue("Name", textBox37.Text);
               

                await command.ExecuteNonQueryAsync();


            }
            else
            {
                label6.Visible = true;
                label6.Text = "Поля должны быть заполнены!";
            }
        }

        private async void button43_Click(object sender, EventArgs e)
        {
            {
                if (label9.Visible)
                    label9.Visible = false;
                if (!string.IsNullOrEmpty(textBox18.Text) && !string.IsNullOrWhiteSpace(textBox18.Text))
                {
                    SqlCommand command = new SqlCommand("DELETE FROM [PREDMET4] WHERE [ZKI]=@ZKI", SqlConnection);

                    command.Parameters.AddWithValue("ZKI", textBox2.Text);
                    await command.ExecuteNonQueryAsync();
                }
                else if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text)) ;



            }
        }

        private void button44_Click(object sender, EventArgs e)
        {
            pictureBox3.Visible = true;
        }

        private void button45_Click(object sender, EventArgs e)
        {
            pictureBox4.Visible = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(e.KeyChar)) return;
            {
                e.Handled = true;
            }
        }

        private void textBox24_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void textBox30_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void textBox36_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void textBox39_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private void textBox37_KeyPress(object sender, KeyPressEventArgs e)
        {

            char number = e.KeyChar;
            if (!Char.IsDigit(e.KeyChar)) return;
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number))
            {
                e.Handled = true;
            }
        }

        private async void button30_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Csharp\SQL\WindowsFormsApp2\WindowsFormsApp2\Database1.mdf;Integrated Security=True";
            SqlConnection = new SqlConnection(connectionString);

            await SqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("Select AVG[Ocenka] FROM [Predmet2]", SqlConnection);         
        }
    }
}
    
    
    
    
    
    

  


