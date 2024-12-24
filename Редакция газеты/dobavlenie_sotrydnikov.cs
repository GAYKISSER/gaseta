using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Редакция_ежедневной_газеты
{
    public partial class dobavlenie_sotrydnikov : Form
    {
        BD bd = new BD();
        public dobavlenie_sotrydnikov()
        {
            InitializeComponent();
        }
        // закрытие окна
        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // добавление сотрудника
        private void button2_Click(object sender, EventArgs e)
        {
            
            if (textBox6.Text ==""|| listBox1.Text==""|| textBox4.Text == ""|| textBox3.Text == ""|| textBox2.Text == "")
            {
                MessageBox.Show("введите данные");
            }
            else if (listBox1.Text == "" )
            {
                MessageBox.Show("введите должность пользователя");
            }
            else
            {
                var a = $"INSERT INTO sotrydniki_and_p(VIO,dolgnost,email,pasvord,n_pasport,balance) values" +
                    $" ('{textBox6.Text}','{listBox1.Text}','{textBox4.Text}','{textBox3.Text}','{textBox2.Text}','0')";
                bd.queryEx(a);
                
            }
        }
    }
}
