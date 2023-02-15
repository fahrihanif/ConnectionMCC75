using ConnectionMCC75.Command;
using ConnectionMCC75.Models;
using System.Data.SqlClient;

namespace ConnectionMCC75;

public class Program
{
    SqlConnection sqlConnection;
    string connectionString = "Data Source=CAMOUFLY;Initial Catalog=db_hr;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    public static void Main(string[] args)
    {

        Region region = new Region();
        region.Name = "cek 124";
        region.Id = 1;

        RegionCommands commands = new RegionCommands();
        //int result = commands.Insert(region);
        //if (result > 0)
        //{
        //    Console.WriteLine("Data Berhasil Disimpan");
        //}
        //else
        //{
        //    Console.WriteLine("Data Gagal Disimpan");
        //}

        var results = commands.GetAll();
        if (results == null)
        {
            Console.WriteLine("Data tidak ditemukan");
        }
        else
        {
            foreach (var item in results)
            {
                Console.WriteLine("Id : " + item.Id);
                Console.WriteLine("Name : " + item.Name);
            }
        }
    }

    // Region
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
    // Int
    public void UpdateRegion(Region entity)
    {
        using (sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.Transaction = sqlTransaction;

            try
            {
                sqlCommand.CommandText = "UPDATE tb_m_regions SET name = @name WHERE id = @id;";

                // Parameter Name
                SqlParameter pName = new SqlParameter
                {
                    ParameterName = "@name",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = entity.Name
                };
                sqlCommand.Parameters.Add(pName);

                // Parameter Id
                SqlParameter pId = new SqlParameter
                {
                    ParameterName = "@id",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = entity.Id
                };
                sqlCommand.Parameters.Add(pId);

                // Untuk menjalankan perintah transaksi
                sqlCommand.ExecuteNonQuery();
                sqlTransaction.Commit();

                Console.WriteLine("Data Berhasil Di Perbaharui");

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

    // Delete
    // Int
    public void DeleteRegion(int id)
    {
        using (sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.Transaction = sqlTransaction;

            try
            {
                sqlCommand.CommandText = "DELETE FROM tb_m_regions WHERE id = @id;";

                // Parameter Id
                SqlParameter pId = new SqlParameter();
                pId.ParameterName = "@id";
                pId.SqlDbType = System.Data.SqlDbType.Int;
                pId.Value = id;
                sqlCommand.Parameters.Add(pId);

                // Untuk menjalankan perintah transaksi
                sqlCommand.ExecuteNonQuery();
                sqlTransaction.Commit();

                Console.WriteLine("Data Berhasil Di Hapus");

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
}
