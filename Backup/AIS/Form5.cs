using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AIS
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            dataGridView2.RowCount = 8;
            dataGridView2.Rows[0].Cells[0].Value = "Размер начальной популяции";
            dataGridView2.Rows[1].Cells[0].Value = "Максимальное количество популяций";
            dataGridView2.Rows[2].Cells[0].Value = "Тип клонирования (1 - простое, 2 - сложное)";
            dataGridView2.Rows[3].Cells[0].Value = "Параметр клонирования";
            dataGridView2.Rows[4].Cells[0].Value = "Параметр мутации";
            dataGridView2.Rows[5].Cells[0].Value = "Порог близости между популяциями";
            dataGridView2.Rows[6].Cells[0].Value = "Порог близости между клетками";
            dataGridView2.Rows[7].Cells[0].Value = "Процент добавляемых клеток";
        }

        public List<double[]> Population;

        public void FillList()
        {
            for (int i = 0; i < Population.Count; i++)
            {
                ListViewItem Item = new ListViewItem();
                Item.SubItems.Add((i + 1).ToString());
                Item.SubItems.Add(Math.Round(Population[i][0], 4).ToString());
                Item.SubItems.Add(Math.Round(Population[i][1], 4).ToString());
                Item.SubItems.Add(Math.Round(Population[i][2], 4).ToString());
                listView1.Items.Add(Item);
            }
        }

        public void FillData(int a1,int a2,int a3,double a4,double a5,double a6,double a7,int a8)
        {
            dataGridView2.Rows[0].Cells[1].Value = a1;
            dataGridView2.Rows[1].Cells[1].Value = a2;
            dataGridView2.Rows[2].Cells[1].Value = a3;
            dataGridView2.Rows[3].Cells[1].Value = a4;
            dataGridView2.Rows[4].Cells[1].Value = a5;
            dataGridView2.Rows[5].Cells[1].Value = a6;
            dataGridView2.Rows[6].Cells[1].Value = a7;
            dataGridView2.Rows[7].Cells[1].Value = a8;
        
        
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
