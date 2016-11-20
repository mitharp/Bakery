using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static Bakery.Forms.BakeryDataSet;

namespace Bakery.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = comboBox2.SelectedIndex = comboBox3.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'bakeryDataSet.Product' table. You can move, or remove it, as needed.
            productTableAdapter.Fill(bakeryDataSet.Product);
            // TODO: This line of code loads data into the 'bakeryDataSet.Recipe' table. You can move, or remove it, as needed.
            recipeTableAdapter.Fill(bakeryDataSet.Recipe);
        }

        private void prev_Click(object sender, EventArgs e)
        {
            productBindingSource.MovePrevious();
        }

        private void saveall_Click(object sender, EventArgs e)
        {
            productBindingSource.EndEdit();
            tableAdapterManager.UpdateAll(bakeryDataSet);
            productTableAdapter.Fill(bakeryDataSet.Product);
        }

        private void first_Click(object sender, EventArgs e)
        {
            productBindingSource.MoveFirst();
        }

        private void last_Click(object sender, EventArgs e)
        {
            productBindingSource.MoveLast();
        }

        private void next_Click(object sender, EventArgs e)
        {
            productBindingSource.MoveNext();
        }

        private void add_Click(object sender, EventArgs e)
        {
            productBindingSource.AddNew();
            productDataGridView.Update();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            productDataGridView.Rows.Remove(productDataGridView.CurrentRow);
            productDataGridView.Update();
        }

        private void save_Click(object sender, EventArgs e)
        {
            productBindingSource.EndEdit();
            tableAdapterManager.UpdateAll(bakeryDataSet);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int index = 0;
            int amount = productDataGridView.Rows.Count;
            if (textBox2.Text == string.Empty)
                return;
            try
            {
                index = Convert.ToInt32(textBox2.Text) - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid number");
            }
            if (index > amount)
                productBindingSource.MoveLast();
            else if (index <= 0)
                productBindingSource.MoveFirst();
            else
                productBindingSource.Position = index;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                productBindingSource.Filter = null;
            }
            else
                productBindingSource.Filter = $"{comboBox1.Text}='{textBox1.Text}'";
        }

        private void recipeBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            Validate();
            recipeBindingSource.EndEdit();
            tableAdapterManager.UpdateAll(bakeryDataSet);
            productTableAdapter.Fill(bakeryDataSet.Product);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            productBindingSource.Sort = comboBox2.Text;
        }

        private void recipeBindingSource_PositionChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                ProductDataTable pdt = new ProductDataTable();
                (bakeryDataSet.Tables["Recipe"].Rows[recipeBindingSource.Position].GetChildRows("FK_Product_Recipe") as ProductRow[]).CopyToDataTable(pdt, LoadOption.PreserveChanges);
                productBindingSource.DataSource = pdt;
            }
            else
            {
                productBindingSource.DataSource = bakeryDataSet.Tables["Product"];
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != null)
                productBindingSource.Position = productBindingSource.Find(comboBox3.Text, textBox3.Text);
        }
    }
}
