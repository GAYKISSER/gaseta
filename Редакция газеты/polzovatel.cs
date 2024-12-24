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
    public partial class polzovatel : Form
    {
        BD bd = new BD();
        
        
        public polzovatel()
        {
            InitializeComponent();
        }
        // выход из приложения
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        // вывод таблиц
        private void grid1()
        {
            SqlConnection con = new SqlConnection(bd.sqlCon);
            con.Open();
            SqlCommand com = new SqlCommand(@"SELECT * FROM gasetu WHERE tip_g !='на расмотрении'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "gasetu");
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        public int test;
        private void grid2()
        {
            //avtorization avt = new avtorization();
            //test = Convert.ToString(avt.id_polzov);
            test = avtorization.id_polzov;
            SqlConnection con = new SqlConnection(bd.sqlCon);
            con.Open();
            SqlCommand com = new SqlCommand($"SELECT id_z,tema_c,tip_c,redactor_c,cena_c,kol_vo,adres" +
                $" FROM carzina WHERE id_pc = '{test}'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "carzina");
            dataGridView2.DataSource = ds.Tables[0];
            con.Close();
        }
        // взаимодействие с таблицей
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int a = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[a];

                textBox1.Text = row.Cells[0].Value.ToString();
            }
        }
        // удаление газеты из карзины
        private void button3_Click(object sender, EventArgs e)
        {
            var a = $"delete carzina where id_z = '{textBox8.Text}'";
            bd.queryEx(a);
            clear();
        }
        // действие при загрузке окна
        private void polzovatel_Load(object sender, EventArgs e)
        {
            grid1();
            grid2();           
            balance();
            sum();
        }
        // метод для вычисление суммы из таблицы корзина
        private void sum()
        {
            try
            {
                SqlConnection con = new SqlConnection(bd.sqlCon);
                con.Open();
                SqlCommand com = new SqlCommand($"select sum (cena_c * kol_vo) from carzina where id_pc = {test}", con);
                summ = ((int)com.ExecuteScalar());
                label3.Text = $"Итого: {summ} руб.";
                con.Close();
            }
            catch
            {
                label3.Text = $"Итого: 0 руб.";
            }

;        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int a = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[a];

                textBox8.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                textBox3.Text = row.Cells[2].Value.ToString();
                textBox4.Text = row.Cells[3].Value.ToString();
                textBox6.Text = row.Cells[4].Value.ToString();
                textBox7.Text = row.Cells[5].Value.ToString();
            }
        }
        // добавление газеты в корзину
        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox_adres.Text != "")
            {
                var a = $"insert into carzina (id_pc,tema_c,tip_c,redactor_c,cena_c,kol_vo,adres)\r" +
    $"\nSelect '{test}',tema_g,tip_g,redactor,cena,'1','{textBox_adres.Text}' from gasetu where id_g={textBox1.Text}";
                bd.queryEx(a);
            }
            else
            {
                MessageBox.Show("Введите адрес доставки","Система");
            }
        }
        // обновление данных
        private void button6_Click(object sender, EventArgs e)
        {
            grid1();
            grid2();
            sum();
            balance();
        }
        public int many;
        public int summ;
        // вывод баланса
        public void balance()
        {
            SqlConnection con = new SqlConnection(bd.sqlCon);
            con.Open();
            SqlCommand com = new SqlCommand($"select balance from sotrydniki_and_p where id_sp = {test}", con);
            many = ((int)com.ExecuteScalar());
            label6.Text = $"Ваш баланс: {many} руб.";
            con.Close();
        }
        // изменение газет в корзине
        private void button4_Click(object sender, EventArgs e)
        {
            var a = $"update carzina set kol_vo = '{textBox7.Text}', adres='{textBox_adres.Text}' where id_z='{textBox8.Text}'";
            bd.queryEx(a);

        }

        // удаление газеты из корзины
        private void zakaz_Click(object sender, EventArgs e)
        {
            if (many >= summ)
            {
                var a = $"delete carzina where id_pc='{textBox8.Text}'";
                bd.queryEx(a);
                var b = $"update sotrydniki_and_p set balance = '{many - summ}' where id_sp= {test}";
                bd.queryEx(b);
                clear();
            }
            else
            {
                MessageBox.Show("Недостаточно средств", "Система");
            }
            
        }

        // кнопка для покупки газет
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int x = Convert.ToInt32(textBox6.Text);
                int y = Convert.ToInt32(textBox7.Text);
                int sum = x * y;
                if (many >= sum)
                {
                    var a = $"delete carzina where id_z='{textBox8.Text}'";
                    bd.queryEx(a);
                    var b = $"update sotrydniki_and_p set balance = '{many - sum}' where id_sp= {test}";
                    bd.queryEx(b);
                    clear();
                }
                else
                {
                    MessageBox.Show("Недостаточно средств", "Система");
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Система");
            }
        }
        // переход к другому окну
        private void button7_Click(object sender, EventArgs e)
        {
            balance popol = new balance();
            popol.Show();
        }
        // поясняющая кнопка
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Вы можете добавить в свою корзину несколько газет и они " +
                $"\nостанутся в ней (а ещё только в нашем приложении вы можете " +
                $"\nкупить газету, если она есть в наличии но её нет в списке доступных" +
                $"\nгазет)Также после каждого действия не забудьте обновить таблицу." ,"Система");
        }
        // очистка 
        private void clear()
        {
            textBox8.Text = "";
            textBox7.Text = "";
            textBox6.Text = "";
            textBox4.Text = "";
            textBox3.Text = "";
            textBox1.Text = "";
        }
        // изменение положения окна
        Point lastpoint;
        private void polzovatel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void polzovatel_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }
        // переход в другок окно
        private void button8_Click(object sender, EventArgs e)
        {
            avtorization avt = new avtorization();
            this.Close();
            avt.Show();
        }
    }
}
