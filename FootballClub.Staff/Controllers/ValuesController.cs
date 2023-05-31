using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Data.SqlClient;
using Npgsql;
using FootballClub.Staff.Database_Logic.DBHandlers;

namespace FootballClub.Staff.Controllers
{
    public class ValuesController : ApiController
    {
        private static DatabaseHandler databaseHandler = new DatabaseHandler();
        // GET api/values
        private List<string> names;

        public ValuesController()
        {
            names = new List<string>();
        }
        public IEnumerable<string> Get()
        {
            names = new List<string>() {"pero","Mato","josip" };
            if (names == null)
            {
                return new List<string>();
            }
            return names;
        }

        // GET api/values/5
        public string Get(int index)
        {
            //return "ss";
            return names[index];
        }

        // POST api/values
        public string Post(string value)
        {
            databaseHandler.InsertName(value);
            names.Add(value);
            return value;
        }

        // PUT api/values/5
        public void Put(int index, [FromBody] string value)
        {
            names[index] = value;
        }

        // DELETE api/values/5
        public void Delete(int index)
        {
            names.RemoveAt(index);
        }
    }
}
