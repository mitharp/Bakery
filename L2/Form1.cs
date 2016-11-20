using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace L2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();//Provider=SQLOLEDB;
            SqlConnection sqlCon = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;Database=BakeryDb;Trusted_Connection=yes;");
            sqlCon.Open();
            SqlCommand command = sqlCon.CreateCommand();
            command.CommandText = "SELECT * FROM Recipe";
            SqlDataAdapter sqlda = new SqlDataAdapter(command.CommandText, sqlCon);
            SqlCommandBuilder cb = new SqlCommandBuilder(sqlda);
            DataSet ds = new DataSet();
            sqlda.Fill(ds, "Recipe");
            SqlDataReader dataReader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            //dt.Load(dataReader);

        }
    }
}