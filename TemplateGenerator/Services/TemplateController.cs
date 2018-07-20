using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TemplateGenerator.Classes;

namespace TemplateGenerator.Services
{
    public class TemplateController : ApiController
    {
        [HttpGet]
        public filter_template show(int id)
        {
            try
            {

                DataService objtmp = new DataService();
                filter_template modelCust = objtmp.GetTemplate(Convert.ToString(id));
                JsonConvert.SerializeObject(modelCust);
                return modelCust;
            }
            catch
            {
                throw;
            }
        }

        //[HttpPost]
        //public List<searchcombobox> searchvaluefordropdown([FromBody]searchvaluequery query)
        //{
        //    try
        //    {

        //        DataService objtmp = new DataService();
        //        List<searchcombobox> modelCust = objtmp.selectapiUtil(query);
        //        JsonConvert.SerializeObject(modelCust);
        //        return modelCust;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}


        [HttpPost]
        public List<result> test_api([FromBody]Query query)
        {
            try
            {

                DataService objtmp = new DataService();
                List<result> modelCust = objtmp.GetResult(query);
                JsonConvert.SerializeObject(modelCust);
                return modelCust;
            }
            catch
            {
                throw;
            }
        }
    }
}
