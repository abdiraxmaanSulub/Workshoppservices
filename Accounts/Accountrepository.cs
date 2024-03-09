using Microsoft.Data.SqlClient;
using Workshoppservices.Models;

namespace Workshoppservices.Accounts
{
    public class Accountrepository
    {
        SqlConnection con;
        SqlCommand cmd;
        public Accountrepository()  //constructor
        {
            con = new SqlConnection("server=DESKTOP-P92ANDM\\SQLEXPRESS; database=asp; Integrated security=true; TrustServerCertificate=True");
        }

        public List<services> getAll()
        {
            List<services> data = new List<services>();
            using (con)
            {
                con.Open();
                string _query = "SELECT TOP 50 * FROM servicess order by ser_name desc";
                using (SqlCommand cmd = new SqlCommand(_query, con))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        data.Add(new services() { Id = Convert.ToInt32(dr["id"]), ser_name = dr["ser_name"].ToString(), price = Convert.ToInt32(dr["price"]) });
                    }

                }
            }
            return data;

        }


        public services get_by_id(int id)
        {
            services data = new services();
            using (con)
            {
                con.Open();
                string _query = $"select * from servicess where id={id}";
                cmd = new SqlCommand(_query, con);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data = new services() { Id = Convert.ToInt32(dr["id"]),ser_name = dr["ser_name"].ToString(), price = Convert.ToInt32(dr["price"]) };
                }
            }
            return data;
        }

        public bool create(string ser_name , float price)
        {
            using (con)
            {
                con.Open();
                string _query = $"insert into servicess values('{ser_name}', {price})";
                cmd = new SqlCommand(_query, con);

                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool update(int id, string ser_name, float price)
        {
            using (con)
            {
                con.Open();
                string _query = $"update servicess set ser_name='{ser_name}',price={price}where Id={id}";
                cmd = new SqlCommand(_query, con);

                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool delete(int id)
        {
            using (con)
            {
                con.Open();
                string _query = $"delete from servicess where id={id}";
                cmd = new SqlCommand(_query, con);

                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


    }
}
