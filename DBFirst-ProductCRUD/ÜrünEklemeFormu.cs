using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBFirst_ProductCRUD
{
    public partial class ÜrünEklemeFormu : Form
    {
        public ÜrünEklemeFormu()
        {
            InitializeComponent();
        }
        NorthwindEntities db = new NorthwindEntities();
        //Kategori comboboxını dolduran metot
        public void GetCategory()
        {
            var categoryList = db.Categories.Select(x => x).ToList();
            cbCategory.DisplayMember = "CategoryName";
            cbCategory.ValueMember = "CategoryID";
            cbCategory.DataSource = categoryList;
        }
        //Tedarikçi comboboxını dolduran metot
        public void GetSupplier()
        {
            var supplierList = db.Suppliers.Select(x => x).ToList();
            cbSupplier.DisplayMember = "CompanyName";
            cbSupplier.ValueMember = "SupplierID";
            cbSupplier.DataSource = supplierList;
        }
        //DatagridView i Arama textboxındaki içeriğe göre ürün listesiyle dolduran metot
        public void GetAll()
        {
            string searched = txtSearch.Text;
            var list = db.Products.Where(x => x.ProductName.Contains(searched)).Select(x => x).ToList();
            dataGridView1.DataSource = list;
        }
        //Arama texboxına birşey yazıldığında datagridview i güncelleyen event
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetAll();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetAll();
            GetCategory();
            GetSupplier();
        }
        //Alanlarda girilen bilgilerle Products tablosuna kayıt atan buton eventi
        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                Product newProduct = new Product();
                newProduct.ProductName = txtProductName.Text;
                newProduct.UnitsInStock = Convert.ToInt16(txtStock.Text);
                newProduct.UnitPrice = Convert.ToDecimal(txtUnitPrice.Text);
                newProduct.CategoryID = (int)cbCategory.SelectedValue;
                newProduct.SupplierID = (int)cbCategory.SelectedValue;
                db.Products.Add(newProduct);
                db.SaveChanges();
                GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //Herhangi bir hücreye tıklanıldığında o hücrenin bulunduğu satırdaki bilgileri alanlara atan event
        public static int selectedID;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            var p = db.Products.Where(x => x.ProductID == selectedID).FirstOrDefault();
            txtProductName.Text = p.ProductName;
            txtStock.Text = p.UnitsInStock.ToString();
            txtUnitPrice.Text = p.UnitPrice.ToString();
            cbCategory.SelectedValue = p.CategoryID;
            cbSupplier.SelectedValue = p.SupplierID;
        }
        //butona tıklandığında alanlardaki bilgilerle Products tablosunu güncelleyen buton eventi
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                selectedID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                var p = db.Products.Where(x => x.ProductID == selectedID).FirstOrDefault();
                p.ProductName = txtProductName.Text;
                p.UnitsInStock = Convert.ToInt16(txtStock.Text);
                p.UnitPrice = Convert.ToDecimal(txtUnitPrice.Text);
                p.CategoryID = (int)cbCategory.SelectedValue;
                p.SupplierID = (int)cbCategory.SelectedValue;
                db.SaveChanges();
                GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //butona tıklandığında seçili olan satırı Products tablosundan silen buton eventi
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                selectedID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                var p = db.Products.Where(x => x.ProductID == selectedID).FirstOrDefault();
                db.Products.Remove(p);
                db.SaveChanges();
                GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
