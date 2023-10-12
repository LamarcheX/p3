using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p3
{
    class Conect
    {
        public static SqlConnection GetConnection()
        {

            SqlConnection con = new SqlConnection("SERVER=ELIANA\\MSSQLSERVER01; DATABASE=P3; INTEGRATED SECURITY=True");


            con.Open();

            return con;


        }
    }
}