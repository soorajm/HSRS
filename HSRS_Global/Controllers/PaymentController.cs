using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HSRS_Global.Controllers
{
    public class PaymentController : Controller
    {
        public string vpc_amount="";
        //
        // GET: /Payment/

        public ActionResult OrderConfirm()
        {
            return View();
        }
        [HttpGet]
        public ActionResult InitiatePayment()
        {
            try
            {
                #region parameters
                string cust_id = (!string.IsNullOrEmpty(Request.QueryString["C_ID"])) ? Request.QueryString["C_ID"].ToString() : "0";
                if (TempData.ContainsKey("vpc_Amount"))
                    vpc_amount = TempData["vpc_Amount"].ToString();//(!string.IsNullOrEmpty(Request.QueryString["vpc_Amount"]))?Request.QueryString["vpc_Amount"].ToString():"0";
                string amt =Convert.ToDouble(vpc_amount).ToString("0.00");
                double totamt = Convert.ToDouble(amt) * 100;
                var VPC_URL = System.Configuration.ConfigurationManager.AppSettings["VPC_URL"];
                var paymentRequest = new PaymentRequest(totamt.ToString())
                {
                    Amount = totamt.ToString(),
                    ReturnUrl = System.Configuration.ConfigurationManager.AppSettings["Path"] + "/payment",
                    OrderInfo = System.Configuration.ConfigurationManager.AppSettings["OrderInfo"],
                };
                // SECURE_SECRET can be found in Merchant Administration/Setup page
                string hashSecrest = System.Configuration.ConfigurationManager.AppSettings["SecretCode"];
                #endregion
                #region redirect to payment gateway
                var transactionData = paymentRequest.GetParameters().OrderBy(t => t.Key, new VPCStringComparer()).ToList();
                // Add custom data, transactionData.Add(new KeyValuePair<string, string>("Title", title));
                var redirectUrl = VPC_URL + "?" + string.Join("&", transactionData.Select(item => HttpUtility.UrlEncode(item.Key) + "=" + HttpUtility.UrlEncode(item.Value)));
                if (!string.IsNullOrEmpty(hashSecrest))
                    redirectUrl += "&vpc_SecureHash=" + PaymentHelperMethods.CreateMD5Signature(hashSecrest + string.Join("", transactionData.Select(item => item.Value)));
                return Redirect(redirectUrl);
                #endregion
            }
            catch (Exception ex)
            {
                var message = "(51) Exception encountered. " + ex.Message;
                return View("PaymentError", ex);
            }
        }
        public ActionResult PaymentConfirm()
        {
            try
            {
                // SECURE_SECRET can be found in Merchant Administration/Setup page
                string hashSecrest = System.Configuration.ConfigurationManager.AppSettings["SecretCode"];
                var secureHash = Request.QueryString["vpc_SecureHash"];
                if (!string.IsNullOrEmpty(secureHash))
                {
                    if (!string.IsNullOrEmpty(hashSecrest))
                    {
                        var rawHashData = hashSecrest + string.Join("", Request.QueryString.AllKeys.Where(k => k != "vpc_SecureHash").Select(k => Request.QueryString[k]));
                        var signature = PaymentHelperMethods.CreateMD5Signature(rawHashData);
                        if (signature != secureHash)
                            return View("Error", new ApplicationException("Invalid request."));
                    }
                }

                var vpcResponse = new PaymentResponse(Request);
                return View(vpcResponse);
            }
            catch (Exception ex)
            {
                var message = "(51) Exception encountered. " + ex.Message;
                return View("Error", ex);
            }
        }
    }
    public class VPCStringComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            var myComparer = CompareInfo.GetCompareInfo("en-US");
            return myComparer.Compare(x, y, System.Globalization.CompareOptions.Ordinal);
        }
    }
    public class PaymentHelperMethods
    {
        public static string getResponseDescription(string vResponseCode)
        {
            string result = "Unknown";
            if (vResponseCode.Length > 0)
            {
                switch (vResponseCode)
                {
                    case "0": result = "Transaction Successful"; break;
                    case "1": result = "Transaction Declined"; break;
                    case "2": result = "Bank Declined Transaction"; break;
                    case "3": result = "No Reply from Bank"; break;
                    case "4": result = "Expired Card"; break;
                    case "5": result = "Insufficient Funds"; break;
                    case "6": result = "Error Communicating with Bank"; break;
                    case "7": result = "Payment Server detected an error"; break;
                    case "8": result = "Transaction Type Not Supported"; break;
                    case "9": result = "Bank declined transaction (Do not contact Bank)"; break;
                    case "A": result = "Transaction Aborted"; break;
                    case "B": result = "Transaction Declined - Contact the Bank"; break;
                    case "C": result = "Transaction Cancelled"; break;
                    case "D": result = "Deferred transaction has been received and is awaiting processing"; break;
                    case "F": result = "3-D Secure Authentication failed"; break;
                    case "I": result = "Card Security Code verification failed"; break;
                    case "L": result = "Shopping Transaction Locked (Please try the transaction again later)"; break;
                    case "N": result = "Cardholder is not enrolled in Authentication scheme"; break;
                    case "P": result = "Transaction has been received by the Payment Adaptor and is being processed"; break;
                    case "R": result = "Transaction was not processed - Reached limit of retry attempts allowed"; break;
                    case "S": result = "Duplicate SessionID"; break;
                    case "T": result = "Address Verification Failed"; break;
                    case "U": result = "Card Security Code Failed"; break;
                    case "V": result = "Address Verification and Card Security Code Failed"; break;
                    default: result = "Unable to be determined"; break;
                }
            }
            
            return result;
        }
        public static string displayAVSResponse(string vAVSResultCode)
        {
            string result = "Unknown";

            if (vAVSResultCode.Length > 0)
            {
                if (vAVSResultCode.Equals("Unsupported"))
                {
                    result = "AVS not supported or there was no AVS data provided";
                }
                else
                {
                    switch (vAVSResultCode)
                    {
                        case "X": result = "Exact match - address and 9 digit ZIP/postal code"; break;
                        case "Y": result = "Exact match - address and 5 digit ZIP/postal code"; break;
                        case "S": result = "Service not supported or address not verified (international transaction)"; break;
                        case "G": result = "Issuer does not participate in AVS (international transaction)"; break;
                        case "A": result = "Address match only"; break;
                        case "W": result = "9 digit ZIP/postal code matched, Address not Matched"; break;
                        case "Z": result = "5 digit ZIP/postal code matched, Address not Matched"; break;
                        case "R": result = "Issuer system is unavailable"; break;
                        case "U": result = "Address unavailable or not verified"; break;
                        case "E": result = "Address and ZIP/postal code not provided"; break;
                        case "N": result = "Address and ZIP/postal code not matched"; break;
                        case "0": result = "AVS not requested"; break;
                        default: result = "Unable to be determined"; break;
                    }
                }
            }
            return result;
        }
        public static string displayCSCResponse(string vCSCResultCode)
        {
            string result = "Unknown";
            if (vCSCResultCode.Length > 0)
            {
                if (vCSCResultCode.Equals("Unsupported"))
                {
                    result = "CSC not supported or there was no CSC data provided";
                }
                else
                {
                    switch (vCSCResultCode)
                    {
                        case "M": result = "Exact code match"; break;
                        case "S": result = "Merchant has indicated that CSC is not present on the card (MOTO situation)"; break;
                        case "P": result = "Code not processed"; break;
                        case "U": result = "Card issuer is not registered and/or certified"; break;
                        case "N": result = "Code invalid or not matched"; break;
                        default: result = "Unable to be determined"; break;
                    }
                }
            }
            return result;
        }
        public static System.Collections.Hashtable splitResponse(string rawData)
        {
            System.Collections.Hashtable responseData = new System.Collections.Hashtable();
            try
            {
                if (rawData.IndexOf("=") > 0)
                {
                    // Extract the key/value pairs for each parameter
                    foreach (string pair in rawData.Split('&'))
                    {
                        int equalsIndex = pair.IndexOf("=");
                        if (equalsIndex > 1 && pair.Length > equalsIndex)
                        {
                            string paramKey = System.Web.HttpUtility.UrlDecode(pair.Substring(0, equalsIndex));
                            string paramValue = System.Web.HttpUtility.UrlDecode(pair.Substring(equalsIndex + 1));
                            responseData.Add(paramKey, paramValue);
                        }
                    }
                }
                else
                {
                    responseData.Add("vpc_Message", "The data contained in the response was not a valid receipt.<br/>\nThe data was: <pre>" + rawData + "</pre><br/>\n");
                }
                return responseData;
            }
            catch (Exception ex)
            {
                // There was an exception so create an error
                responseData.Add("vpc_Message", "\nThe was an exception parsing the response data.<br/>\nThe data was: <pre>" + rawData + "</pre><br/>\n<br/>\nException: " + ex.ToString() + "<br/>\n");
                return responseData;
            }
        }
        public static string CreateMD5Signature(string RawData)
        {
            var hasher = System.Security.Cryptography.MD5CryptoServiceProvider.Create();
            var HashValue = hasher.ComputeHash(Encoding.ASCII.GetBytes(RawData));
            return string.Join("", HashValue.Select(b => b.ToString("x2"))).ToUpper();
            #region commented code
            //string strHex = "";
            //foreach(byte b in HashValue) 
            //{
            //    strHex += b.ToString("x2");
            //}
            //return strHex.ToUpper();
            #endregion
        }
    }
    public class PaymentRequest
    {

        public PaymentRequest(string amount)
        {
            Version = "1";
            Command = "pay";
            AccessCode = System.Configuration.ConfigurationManager.AppSettings["AccessCode"];
            MerchTxnRef = "1234/1";
            Merchant = System.Configuration.ConfigurationManager.AppSettings["MerchantID"];
            OrderInfo = System.Configuration.ConfigurationManager.AppSettings["OrderInfo"];
            Amount = amount;
            ReturnUrl = System.Configuration.ConfigurationManager.AppSettings["Path"] + "/payment";
            Locale = "en";
        }
        public PaymentRequest(int dollars, int cents, string orderInfo)
            : this("")
        {
            this.Amount = (dollars * 100 + cents).ToString();
            this.OrderInfo = orderInfo;
        }
        public string Version { get; set; }
        public string Command { get; set; }
        public string AccessCode { get; set; }
        public string MerchTxnRef { get; set; }
        public string Merchant { get; set; }
        public string OrderInfo { get; set; }
        public string Amount { get; set; }
        public string ReturnUrl { get; set; }
        public string Locale { get; set; }
        public Dictionary<string, string> GetParameters()
        {
            var parameters = new Dictionary<string, string> {
                { "vpc_Version" ,Version},
                { "vpc_Command",Command},
                { "vpc_AccessCode" ,AccessCode},
                { "vpc_MerchTxnRef" ,MerchTxnRef},
                { "vpc_Merchant" ,Merchant},
                { "vpc_OrderInfo",OrderInfo},
                { "vpc_Amount" ,Amount},
                { "vpc_ReturnURL", ReturnUrl},
                { "vpc_Locale",Locale}
            };
            return parameters;
        }
    }
    public class PaymentResponse
    {
        #region common properties
        public string ResponseCode { get; set; }
        public string ResponseCodeDescription { get; set; }
        public string Amount { get; set; }
        public string Command { get; set; }
        public string Version { get; set; }
        public string OrderInfo { get; set; }
        public string MerchantID { get; set; }
        #endregion
        #region on-successful-payment properties
        public string BatchNo { get; set; }
        public string CardType { get; set; }
        public string ReceiptNo { get; set; }
        public string AuthorizeID { get; set; }
        public string MerchTxnRef { get; set; }
        public string AcqResponseCode { get; set; }
        public string TransactionNo { get; set; }
        #endregion
        public string Message { get; set; }
        public PaymentResponse(HttpRequestBase Request)
        {
            Func<string, string> GetQueryStringValue = key =>
            {
                if (Request.QueryString.AllKeys.Contains(key))
                {
                    var result = Request.QueryString[key];
                    return result;
                }
                return "Unknown";
            };
            // Get the standard receipt data from the parsed response
            this.ResponseCode = GetQueryStringValue("vpc_TxnResponseCode");
            this.ResponseCodeDescription = PaymentHelperMethods.getResponseDescription(ResponseCode);
            this.Amount = GetQueryStringValue("vpc_Amount");
            this.Command = GetQueryStringValue("vpc_Command");
            this.Version = GetQueryStringValue("vpc_Version");
            this.OrderInfo = GetQueryStringValue("vpc_OrderInfo");
            this.MerchantID = GetQueryStringValue("vpc_Merchant");

            // only display this data if not an error condition
            if (this.ResponseCode.Equals("7"))
            {
                this.BatchNo = GetQueryStringValue("vpc_BatchNo");
                this.CardType = GetQueryStringValue("vpc_Card");
                this.ReceiptNo = GetQueryStringValue("vpc_ReceiptNo");
                this.AuthorizeID = GetQueryStringValue("vpc_AuthorizeId");
                this.MerchTxnRef = GetQueryStringValue("vpc_MerchTxnRef");
                this.AcqResponseCode = GetQueryStringValue("vpc_AcqResponseCode");
                this.TransactionNo = GetQueryStringValue("vpc_TransactionNo");
            }
            var message = GetQueryStringValue("vpc_Message");
        }
    }
}