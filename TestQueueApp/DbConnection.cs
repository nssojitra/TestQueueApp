using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestQueueApp
{
          public class DbConnection
          {
                    public DbConnection()
                    {

                    }

                    public int GetEmployyeeCount()
                    {
                              var ret = -1;
                              using(var conn=new SqlConnection("Server=tcp:nsojitra-dbserver.database.windows.net,1433;Initial Catalog=mydb;Persist Security Info=False;User ID=nssojitra;Password=yash1234+;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                              {
                                        conn.Open();
                                        var comm = new SqlCommand("select count(1) from employee", conn);
                                        ret = Convert.ToInt32(comm.ExecuteScalar());
                                        conn.Close();

                              }
                              return ret;
                    }
          }
}
