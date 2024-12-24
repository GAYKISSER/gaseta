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
    public partial class predlozenie_gazet : Form
    {
        BD bd = new BD();
        public predlozenie_gazet()
        {
            InitializeComponent();
        }
        // закрытие окна
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // поясняющия кнопка
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вам нужно заполнить только 2 поля, остальные данные (такие как издатель и редактор газеты) заполнятся автоматически.", "система");
        }


        // добавление газеты
        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                var a = $"insert into gasetu(tema_g,tip_g,redactor,cena) values ('{textBox1.Text}','на расмотрении','{avtorization.id_polzov}','{textBox2.Text}')";
                bd.queryEx(a);
            }
            catch
            {
                MessageBox.Show("Введены неверные данные", "Система");
            }
        }

        private void predlozenie_gazet_Load(object sender, EventArgs e)
        {

        }
    }
}
