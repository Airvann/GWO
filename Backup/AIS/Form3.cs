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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            dataGridView1.RowCount = 2;
            dataGridView1.Rows[0].Cells[0].Value = "x";
            dataGridView1.Rows[1].Cells[0].Value = "y";
            //numericUpDown1.Value = (decimal)Ar[0];
        }

        public float[] A = new float[8];
        public float[] Ar = new float[8];
        public bool[] flines = new bool[8];
        public double[,] obl = new double[2, 2];
        public double[,] oblbase = new double[2, 2];

        private void button1_Click(object sender, EventArgs e)
        {
            Ar[4] = (float)numericUpDown1.Value;
            Ar[3] = (float)numericUpDown2.Value;
            Ar[2] = (float)numericUpDown3.Value;
            Ar[1] = (float)numericUpDown4.Value;
            Ar[0] = (float)numericUpDown5.Value;
            Ar[5] = (float)numericUpDown6.Value;
            Ar[6] = (float)numericUpDown7.Value;
            Ar[7] = (float)numericUpDown8.Value;
            
            flines[0] = checkBox1.Checked;
            flines[1] = checkBox2.Checked;
            flines[2] = checkBox3.Checked;
            flines[3] = checkBox4.Checked;
            flines[4] = checkBox5.Checked;
            flines[5] = checkBox6.Checked;
            flines[6] = checkBox7.Checked;
            flines[7] = checkBox8.Checked;

            int error = 0;
            try
            {
                obl[0, 0] = Convert.ToDouble(dataGridView1.Rows[0].Cells[1].Value);
                obl[0, 1] = Convert.ToDouble(dataGridView1.Rows[0].Cells[2].Value);
                obl[1, 0] = Convert.ToDouble(dataGridView1.Rows[1].Cells[1].Value);
                obl[1, 1] = Convert.ToDouble(dataGridView1.Rows[1].Cells[2].Value);
            }
            catch
            {
                error = 1;
            }

            if ((obl[0, 1] - obl[0, 0] < 0) || (obl[1, 1] - obl[1, 0] < 0)) error = 1;

            if (error == 0)
            {
                this.Close();
            }
            else MessageBox.Show("Неверно введена область определения функции!");
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = (decimal)Ar[4];
            numericUpDown2.Value = (decimal)Ar[3];
            numericUpDown3.Value = (decimal)Ar[2];
            numericUpDown4.Value = (decimal)Ar[1];
            numericUpDown5.Value = (decimal)Ar[0];
            numericUpDown6.Value = (decimal)Ar[5];
            numericUpDown7.Value = (decimal)Ar[6];
            numericUpDown8.Value = (decimal)Ar[7];

            checkBox1.Checked = flines[0];
            checkBox2.Checked = flines[1];
            checkBox3.Checked = flines[2];
            checkBox4.Checked = flines[3];
            checkBox5.Checked = flines[4];
            checkBox6.Checked = flines[5];
            checkBox7.Checked = flines[6];
            checkBox8.Checked = flines[7];


            dataGridView1.Rows[0].Cells[1].Value = obl[0, 0];
            dataGridView1.Rows[0].Cells[2].Value = obl[0, 1];
            dataGridView1.Rows[1].Cells[1].Value = obl[1, 0];
            dataGridView1.Rows[1].Cells[2].Value = obl[1, 1];

        }

        private void button2_Click(object sender, EventArgs e)
        {
            numericUpDown1.Value = (decimal)A[4];
            numericUpDown2.Value = (decimal)A[3];
            numericUpDown3.Value = (decimal)A[2];
            numericUpDown4.Value = (decimal)A[1];
            numericUpDown5.Value = (decimal)A[0];
            numericUpDown6.Value = 0;
            numericUpDown7.Value = 0;
            numericUpDown8.Value = 0;
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;


            dataGridView1.Rows[0].Cells[1].Value = oblbase[0, 0];
            dataGridView1.Rows[0].Cells[2].Value = oblbase[0, 1];
            dataGridView1.Rows[1].Cells[1].Value = oblbase[1, 0];
            dataGridView1.Rows[1].Cells[2].Value = oblbase[1, 1];


        }
    }
}
