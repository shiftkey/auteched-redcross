
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;



namespace MvcwebService.Controllers
{
    public class PostCodeController : ApiController
    {

        public IEnumerable<Postcode> Get(int pCode1 = 0)
        {
            
            using (LassiterEntities entity = new LassiterEntities())
                if (pCode1 == 0)
                {
                    return entity.Postcodes.ToList();
                }
                else
                {
                    var location = (from p in entity.Postcodes
                                            where p.PostCode1 == pCode1
                                            select p).ToList();
                    return location;
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