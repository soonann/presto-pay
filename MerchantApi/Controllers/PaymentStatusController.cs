using MerchantWebsite.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MerchantApi.Controllers
{

    public class PaymentStatusController : ApiController
    {
        // GET api/<controller>



        public object Get([FromUri]UpdateStatus us )
        {
            
            HttpResponseMessage res;
            if(us == null || String.IsNullOrEmpty(us.apiKey) || String.IsNullOrEmpty(us.referenceNo))
            {
                res = Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Api Call");

            }
            else
            {

                Business biz = new Business();
                if(!biz.CheckIfValidKey(us.apiKey)){

                    res = Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Api Call");

                }
                else {
                    MerchantOrders mo = new MerchantOrders();

                    if (mo.RetrieveMerchantOrderByReferenceNo(us.referenceNo) == null)
                    {
                        res = Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Api Call");
                    }
                    else
                    {
                        
                        if(mo.UpdateOrderPaidByReferenceNo(us.apiKey, us.referenceNo, 1)>0)
                            res = Request.CreateResponse(HttpStatusCode.OK, "Success");
                        else
                            res = Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Api Call");


                    }
                }

            }
            // check if api key valid



            return res;
        }

    }
}