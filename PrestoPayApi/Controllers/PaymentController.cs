

using PrestoPayApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PrestoPayApi.Controllers
{
    //[EnableCors(origins: "http://prestomerchantweb.azurewebsites.net", headers: "*", methods: "*")]
    public class PaymentController : ApiController
    {
        
        public object Post([FromBody]PaymentInformation PaymentDetails)
        {

            string redirectUrl = "";
            string message = "";
            bool valid = true;
            string baseUrl = "http://localhost:4000/PaymentGateway.aspx?id=";
           // "http://localhost:4000/PaymentGateway.aspx?id="https://prestopay.azurewebsites.net/PaymentGateway.aspx?id=
            var dao = new BusinessApiKeyDAO();
            // get db api key 
            string busi_id = dao.RetrieveBusinessIdByApiKey(PaymentDetails.ApiKey);
            string a = dao.RetrieveLatestKeyOfBusiness(busi_id);
                
            if (String.IsNullOrEmpty(busi_id))
            {


                valid = false;
                message += "Invalid ApiKey "+busi_id +" " + PaymentDetails.ApiKey;
                new APIErrorLogDAO().AddErrorLog(PaymentDetails.ApiKey, message);

            }

            Uri uriResult;
            if (!(Uri.TryCreate(PaymentDetails.OnCompleteUrl, UriKind.RelativeOrAbsolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)))
            {
                valid = false;
                message += "Invalid Url ";
            }
            if (PaymentDetails.MerchantReferenceNo == null)
            {
                valid = false;
                message += "No Reference Number ";
            }
            if (PaymentDetails.ItemList == null)
            {
                valid = false;
                message += "No Items Specified ";
            }


            HttpResponseMessage apiResponse;
            if (!valid)
            {


                apiResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, message);
                // insert error log message 
                new APIErrorLogDAO().AddErrorLog(PaymentDetails.ApiKey, message);

            }
            else
            {
                var OrderItemDAO = new Payment();
                OrderItemDAO.ItemList = PaymentDetails.ItemList;
                OrderItemDAO.OrderDetails.Order_ApiKey = PaymentDetails.ApiKey;
                OrderItemDAO.OrderDetails.Order_DateOrdered = DateTime.Now;
                OrderItemDAO.OrderDetails.Order_Paid = 0;
                OrderItemDAO.OrderDetails.Order_RefNo = PaymentDetails.MerchantReferenceNo;
                OrderItemDAO.OrderDetails.Order_UrlRedirect = PaymentDetails.OnCompleteUrl;

                string newOrderId = OrderItemDAO.InsertOrderAndItems();

                if (String.IsNullOrEmpty(newOrderId))
                {
               
                    apiResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, message);
                    message += "Failed to create entry";
                    new APIErrorLogDAO().AddErrorLog(PaymentDetails.ApiKey,message);
                    //error log
                }
                redirectUrl = new OrderDAO().RetrievePrestoKeyByOrderId(newOrderId);
                if (String.IsNullOrEmpty(redirectUrl))
                {
                    apiResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, message);
                    message += "Failed to create entry";
                    new APIErrorLogDAO().AddErrorLog(PaymentDetails.ApiKey, message);
                }      
                else
                    apiResponse = Request.CreateResponse(HttpStatusCode.OK, baseUrl + redirectUrl);
            }

            return apiResponse;

        }

        public object Get(string abc)
        {
            return Request.CreateResponse( HttpStatusCode.OK ,  "abc");

        }

            /*

            public object Get([ FromUri] PaymentInformation PaymentDetails)
            {

                string redirectUrl = "";
                string message = "";
                bool valid = true;
                string baseUrl = "http://localhost:4000/PaymentGateway.aspx?id=";
                var dao = new BusinessApiKeyDAO();
                // get db api key 
                string busi_id = dao.RetrieveBusinessIdByApiKey(PaymentDetails.ApiKey);
                string a = dao.RetrieveLatestKeyOfBusiness(busi_id);
                if (String.IsNullOrEmpty(busi_id))
                {


                    valid = false;
                    message += "Invalid ApiKey ";


                }

                Uri uriResult;
                if (!(Uri.TryCreate(PaymentDetails.OnCompleteUrl, UriKind.RelativeOrAbsolute, out uriResult)
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)))
                {
                    valid = false;
                    message += "Invalid Url ";
                }
                if (PaymentDetails.MerchantReferenceNo == null)
                {
                    valid = false;
                    message += "No Reference Number ";
                }
                if (PaymentDetails.ItemList == null)
                {
                    valid = false;
                    message += "No Items Specified ";
                }


                HttpResponseMessage apiResponse;
                if (!valid)
                {


                    apiResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, message);
                    // insert error log message 


                }
                else
                {
                    var OrderItemDAO = new Payment();
                    OrderItemDAO.ItemList = PaymentDetails.ItemList;
                    OrderItemDAO.OrderDetails.Order_ApiKey = PaymentDetails.ApiKey;
                    OrderItemDAO.OrderDetails.Order_DateOrdered = DateTime.Now;
                    OrderItemDAO.OrderDetails.Order_Paid = 0;
                    OrderItemDAO.OrderDetails.Order_RefNo = PaymentDetails.MerchantReferenceNo;
                    OrderItemDAO.OrderDetails.Order_UrlRedirect = PaymentDetails.OnCompleteUrl;

                    string newOrderId = OrderItemDAO.InsertOrderAndItems();

                    if (String.IsNullOrEmpty(newOrderId))
                    {
                        apiResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Failed to create entry");
                        //error log
                    }
                    redirectUrl = new OrderDAO().RetrievePrestoKeyByOrderId(newOrderId);
                    if (String.IsNullOrEmpty(redirectUrl))
                        apiResponse = Request.CreateResponse(HttpStatusCode.InternalServerError, "Failed to create entry");
                    else
                        apiResponse = Request.CreateResponse(HttpStatusCode.OK, baseUrl + redirectUrl);
                }

                return apiResponse;

            }
            */



        }
    }