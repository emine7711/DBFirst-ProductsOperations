using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBFirst_CategoryList
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NorthwindEntities db = new NorthwindEntities();
        private void Form1_Load(object sender, EventArgs e)
        {
            //Categorilerin tamamını gösteren kod
            var categoryList = db.Categories.Select(x => x).ToList();
            dataGridView1.DataSource = categoryList;

            //SadeceCategoryId ve Name i anonym types ile gösteren kod
            //var categoryList = db.Categories.Select(x => new { ID =x.CategoryID, Name= x.CategoryName}).ToList();
            //dataGridView1.DataSource = categoryList;

            //foreach kullanarak sadece 2 sütun gösteren kod
            //var categoryList = db.Categories.Select(x => x).ToList();
            //foreach (var item in categoryList)
            //{
            //    dataGridView1.DataSource = categoryList;
            //    dataGridView1.Columns["Description"].Visible = false;
            //    dataGridView1.Columns["Picture"].Visible = false;
            //}

        }
    }
}
