using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using NewsPolling.Utility;
using System.Security.Cryptography.Xml;

namespace NewsPolling.Models
{
    public class DataAccessLayer
    {
        string connectionstring = ConnectionString.CName;
        public IEnumerable<News> getNews()
        {
            List<News> lstnews = new List<News>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("show", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    News ne = new News();

                    ne.newsid = Convert.ToInt32(rdr[0]);
                    ne.title = rdr[1].ToString();
                    ne.description = rdr[2].ToString();
                    ne.email = rdr[3].ToString();
                  ne.votes = Convert.ToInt32(rdr[4]);                  // ne.repoterid = string.IsNullOrEmpty(rdr[3].ToString()) ? (int?)null : Convert.ToInt32(rdr[3]);



                    lstnews.Add(ne);
                }
                con.Close();
            }
            return lstnews;



        }

        public int AddNewsInTab (News ne)
        {
            int row;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("Addnewsmodule",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", ne.email);
                cmd.Parameters.AddWithValue("@title", ne.title);
                cmd.Parameters.AddWithValue("@des", ne.description);
                cmd.Parameters.Add("@ret", SqlDbType.Int);
                cmd.Parameters["@ret"].Direction = ParameterDirection.ReturnValue;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                row = Convert.ToInt32(cmd.Parameters["@ret"].Value);
            }
            if (row == 1)
            {
                return 1;
            }
            else
            {
                return -1;
            }

        }

        public Logandvote findvote(int l)
        {
            //  Logandvote vote = new Logandvote();
            Logandvote log = new Logandvote();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("voting", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@wsid", l);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if(rdr.Read())
                {
                    // Logandvote log = new Logandvote();
                    log.votes= string.IsNullOrEmpty(rdr[0].ToString()) ? (int?)null: Convert.ToInt32(rdr[0]);



                }
                con.Close();

            }
            return log ;
            
        }
        public int AddVote(int id)
        {
            int row;
            using(SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("VoteUpdateOrInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@newsid", id);
                cmd.Parameters.Add("@ret", SqlDbType.Int);
                cmd.Parameters["@ret"].Direction = ParameterDirection.ReturnValue;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                row = Convert.ToInt32(cmd.Parameters["@ret"].Value);


            }
            if(row==1)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

       /* public IEnumerable<Repoter> GetEmail()
        {

            
            List<Repoter> emaillst = new List<Repoter>();
            using(SqlConnection con = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("select Email from TabReporterDetail", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while(rdr.Read())
                {
                    Repoter re = new Repoter();
                    re.r_email = rdr[0].ToString();

                    //email = re.r_email;
                    emaillst.Add(re);

                }
                

            }
            return emaillst;
        }
*/
    }
}