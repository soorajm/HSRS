using HSRS_Global.Seo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HSRS_Global
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //generic URLs
            routes.Add("CommonPathRoute",
                            new CommonPathRoute(
                                        "{generic_se_name}",
                                        new RouteValueDictionary(new { controller = "Home", action = "Index" }))
                                        );

            routes.Add("CommonPathRoute1",
                new CommonPathRoute(
                            "{generic_se_name}/{se_name}",
                            new RouteValueDictionary(new { controller = "Home", action = "Index" }))
                            );

            routes.Add("CommonPathRoute2",
               new CommonPathRoute(
                           "{generic_se_name}/{se_name}/{se_namelocation}",
                           new RouteValueDictionary(new { controller = "Home", action = "Index" }))
                           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Onlinebooking",
                url: "{controller}/{action}/{ObType}/{id}/{price}",
                defaults: new { controller = "Home", action = "Index", ObType = UrlParameter.Optional, id = UrlParameter.Optional, price = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Payment",
                url: "{controller}/{action}/{vpc_AVSRequestCode}/{vpc_AVSResultCode}/{vpc_AcqAVSRespCod}/{vpc_AcqCSCRespCode}/{vpc_AcqResponseCode}/{vpc_Amount}/{vpc_AuthorizeId}/{vpc_BatchNo}/{vpc_CSCResultCode}/{vpc_Card}/{vpc_Command}/{vpc_Locale}/{vpc_MerchTxnRef}/{vpc_Merchant}/{vpc_Message}/{vpc_OrderInf}/{vpc_ReceiptNo}/{vpc_SecureHash}/{vpc_TransactionNo}/{vpc_TxnResponseCode}/{vpc_Version}",

                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    vpc_AVSRequestCode = UrlParameter.Optional,
                    vpc_AVSResultCode = UrlParameter.Optional,
                    vpc_AcqAVSRespCod = UrlParameter.Optional,
                    vpc_AcqCSCRespCode = UrlParameter.Optional,
                    vpc_AcqResponseCode = UrlParameter.Optional,
                    vpc_Amount = UrlParameter.Optional,
                    vpc_AuthorizeId = UrlParameter.Optional,
                    vpc_BatchNo = UrlParameter.Optional,
                    vpc_CSCResultCode = UrlParameter.Optional,
                    vpc_Card = UrlParameter.Optional,
                    vpc_Command = UrlParameter.Optional,
                    vpc_Locale = UrlParameter.Optional,
                    vpc_MerchTxnRef = UrlParameter.Optional,
                    vpc_Merchant = UrlParameter.Optional,
                    vpc_Message = UrlParameter.Optional,
                    vpc_OrderInf = UrlParameter.Optional,
                    vpc_ReceiptNo = UrlParameter.Optional,
                    vpc_SecureHash = UrlParameter.Optional,
                    vpc_TransactionNo = UrlParameter.Optional,
                    vpc_TxnResponseCode = UrlParameter.Optional,
                    vpc_Version = UrlParameter.Optional
                });
        }
    }
}