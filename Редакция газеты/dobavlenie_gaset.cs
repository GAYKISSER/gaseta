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
    
    public partial class dobavlenie_gaset : Form
    {
        BD bd = new BD();
        public dobavlenie_gaset()
        {
            InitializeComponent();
        }
        // закрытие окна
        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // действия при загрузке окна
        private void dobavlenie_gaset_Load(object sender, EventArgs e)
        {
            grid1();
            comboBox1.SelectedIndex = 0;
        }
        // поясняющая кнопка
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Здесь вы выбираете какую газету одобрите для выпуска, а также устанавливаете её тип", "Cистема");
        }
        // настройка комбобокса
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;           
        }
        // вывод таблицы
        private void grid1()
        {
            SqlConnection con = new SqlConnection(bd.sqlCon);
            con.Open();
            SqlCommand com = new SqlCommand(@"SELECT id_g as 'id газеты',tema_g as 'тема газеты',tip_g as 'тип газеты',
redactor as 'редактор',cena as 'цена' FROM gasetu WHERE tip_g ='на расмотрении'", con);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "gasetu");
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        // изменение газеты
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                string selectstate = comboBox1.SelectedItem.ToString();

                var a = $"update gasetu set tip_g = '{selectstate}' where id_g = {textBox1.Text};";
                bd.queryEx(a);
            }
            else
            {
                MessageBox.Show("Выберите газету для одобрения","Система");
            }
                

        }
        // взаимодействие с таблицей
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int a = e.RowIndex;

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[a];
                //////////////////
                textBox1.Text = row.Cells[0].Value.ToString();
            }
        }
        // обновление таблицы
        private void button3_Click(object sender, EventArgs e)
        {
            grid1();
        }
    }
}
