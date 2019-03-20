using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBFirst_ProductList
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
            var productList = db.Products.Select(x =>new {ID=x.ProductID, Name=x.ProductName,Category= x.Category.CategoryName } ).ToList();
            dataGridView1.DataSource = productList;
        }
    }
}
