using ConnectionMCC75.Models;
using System.Data.SqlClient;

namespace ConnectionMCC75;

public class Program
{
    SqlConnection sqlConnection;
    string connectionString = "Data Source=CAMOUFLY;Initial Catalog=db_hr;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    public static void Main(string[] args)
    {
        Program program = new Program();
        Region region = new Region();
        region.Name = "cek 123";

        Console.WriteLine("saya Melakukan perubahan disini");
        //program.GetRegions();

        //program.InsertRegion(region);
        Console.WriteLine("test");
        program.GetByIdRegion(5);
    }

    // Table Regions
    // Create 
    public void InsertRegion(Region entity)
    {
        using (sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.Transaction = sqlTransaction;

            try
            {
                sqlCommand.CommandText = "INSERT INTO tb_m_regions VALUES (@name);";
                // INSERT INTO tb_m_regions VALUES ('entity.Name')

                // Parameter Name
                SqlParameter pName = new SqlParameter();
                pName.ParameterName = "@name";
                pName.SqlDbType = System.Data.SqlDbType.VarChar;
                pName.Value = entity.Name;
                sqlCommand.Parameters.Add(pName);

                // Untuk menjalankan perintah transaksi
                sqlCommand.ExecuteNonQuery();
                sqlTransaction.Commit();

                Console.WriteLine("Data Berhasil Di Masukan");

                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                try
                {
                    sqlTransaction.Rollback();
                }
                catch (Exception exRollback)
                {
                    Console.WriteLine(exRollback.Message);
                }
            }

        }
    }

    // Read / View
    public void GetRegions()
    {
        sqlConnection = new SqlConnection(connectionString);

        // Membuat instance SqlCommand untuk mendifinisikan sebuah query & connection
        SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = sqlConnection;
        sqlCommand.CommandText = "SELECT * FROM tb_m_regions;";

        // membuka koneksi
        sqlConnection.Open();

        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
        {
            // Mengecek apakah ada data atau tidak
            if (sqlDataReader.HasRows)
            {
                //jika ada, maka tampilkan datanya
                while (sqlDataReader.Read())
                {
                    Console.WriteLine("Id : " + sqlDataReader[0]);
                    Console.WriteLine("Name : " + sqlDataReader[1]);
                }
            }
            else
            {
                Console.WriteLine("Data Kosong");
            }
            sqlDataReader.Close();
        }
        sqlConnection.Close();
    }

    // Read by Id
    public void GetByIdRegion(int id)
    {
        sqlConnection = new SqlConnection(connectionString);

        try
        {
            // Membuat instance SqlCommand untuk mendifinisikan sebuah query & connection
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "SELECT * FROM tb_m_regions WHERE id = @id;";

            SqlParameter pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.SqlDbType = System.Data.SqlDbType.Int;
            pId.Value = id;
            sqlCommand.Parameters.Add(pId);

            // membuka koneksi
            sqlConnection.Open();

            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
                // Mengecek apakah ada data atau tidak
                if (sqlDataReader.HasRows)
                {
                    //jika ada, maka tampilkan datanya
                    sqlDataReader.Read();

                    Console.WriteLine("Id : " + sqlDataReader[0]);
                    Console.WriteLine("Name : " + sqlDataReader[1]);
                }
                else
                {
                    Console.WriteLine("Id Tidak Ditemukan");
                }
                sqlDataReader.Close();
            }
            sqlConnection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    // Update
    // Delete
}
