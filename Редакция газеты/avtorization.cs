using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Редакция_ежедневной_газеты
{
    public partial class avtorization : Form
    {
        BD bd = new BD();
        public avtorization()
        {
            InitializeComponent();
        }
        // выход
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Point lastpoint;
        // изменение положения окна
        private void avtorization_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void avtorization_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }
        // взаимодействие с кнопкой выход
        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Blue;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Black;
        }
        // переход на другое окно
        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            registration reg = new registration();
            reg.Show();
        }
         public static int id_polzov;
        public static int id_z = id_polzov;

        // метод для нахождения id
        private void id()
        {
            SqlConnection con = new SqlConnection(bd.sqlCon);
            con.Open();
            SqlCommand com = new SqlCommand($"select id_sp from sotrydniki_and_p where email = '{textBox1.Text}' and pasvord = '{textBox2.Text}'", con);
            id_polzov = ((int)com.ExecuteScalar());
            con.Close();
        }
        // кнопка вход
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var loginUs = textBox1.Text;
                var paswordUs = textBox2.Text;
                string aa = "-";
                string bb = "администратор";
                string cc = "редактор";
                SqlConnection con = new SqlConnection(bd.sqlCon);
                string b = $"select * from sotrydniki_and_p where email = '{loginUs}' and pasvord = '{paswordUs}' and dolgnost = '{aa}'";
                string a = $"select * from sotrydniki_and_p where email = '{loginUs}' and pasvord = '{paswordUs}' and dolgnost = '{bb}'";
                string c = $"select * from sotrydniki_and_p where email = '{loginUs}' and pasvord = '{paswordUs}' and dolgnost = '{cc}'";

                SqlCommand command = new SqlCommand(b, con);
                SqlCommand command1 = new SqlCommand(a, con);
                SqlCommand command2 = new SqlCommand(c, con);
                DataTable table = new DataTable();
                DataTable table1 = new DataTable();
                DataTable table2 = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlDataAdapter adapter1 = new SqlDataAdapter();
                SqlDataAdapter adapter2 = new SqlDataAdapter();

                adapter2.SelectCommand = command2;
                adapter2.Fill(table2);
                adapter1.SelectCommand = command1;
                adapter1.Fill(table1);
                adapter.SelectCommand = command;
                adapter.Fill(table);


                id();

                if (table2.Rows.Count == 1)
                {
                    MessageBox.Show("приветствую работник", "система");
                    this.Hide();
                    sotrudniki rab = new sotrudniki();
                    rab.Show();
                }

                else if (table1.Rows.Count == 1)
                {
                    MessageBox.Show("приветствую администратор", "система");
                    this.Hide();
                    admin ad = new admin();
                    ad.Show();
                }

                else if (table.Rows.Count == 1)
                {
                    MessageBox.Show("вы успешно вошли", "система");
                    this.Hide();
                    polzovatel user = new polzovatel();
                    user.Show();
                }
                else
                {
                    MessageBox.Show("неверные данные", "Ошибка");
                }
                con.Close();
            }
            catch
            {
                MessageBox.Show("Такого пользователя нет", "Система");
            }
        }

        private void avtorization_Load(object sender, EventArgs e)
        {

        }
    }
}
