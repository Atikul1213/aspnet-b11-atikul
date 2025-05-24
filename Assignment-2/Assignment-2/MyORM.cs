using Assignment_2.Models;
using Microsoft.Data.SqlClient;

namespace Assignment_2
{
    public class MyORM<G, T> where T : class, TEntity<G>, new()
    {
        private string cs = "Data Source=BS-01344;Initial Catalog=AspnetB11;User ID=developer; Password=123456;TrustServerCertificate=True;";
        SqlConnection con = null;
        public void Insert(T entity)
        {

            using (con = new SqlConnection(cs))
            {

            }
        }

        public void Update(T entity)
        {

        }

        public void Delete(T entity)
        {

        }

        public void Delete(G id)
        {

        }
        //public T GetById(G id)
        //{

        //    return null;
        //}
        public List<T> GetAll()
        {

            return new List<T>();
        }
    }
}
