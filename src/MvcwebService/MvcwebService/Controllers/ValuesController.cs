
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;



namespace MvcwebService.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        /*
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        */
        public IEnumerable<Person> Get()
        {
            return getPersons();
        }

        // GET api/values/5
        public Person Get(int id)
        {
            using (LassiterEntities entity = new LassiterEntities())
            {
                // entity.People pObj = new PersonEntity();
                return entity.People.Where(u => u.ID == id).FirstOrDefault();
               
            }
        

         
        }



        private IList<Person> getPersons()
        {
            using (LassiterEntities entity = new LassiterEntities())
            {
               // entity.People pObj = new PersonEntity();
                return entity.People.ToList();
                
            }
          
        }


        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}