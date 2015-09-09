using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HSRS_Global.Seo
{
    /// <summary>
    /// Provides properties and methods for defining a SEO friendly route, and for getting information about the route.
    /// </summary>
    public class CommonPathRoute : Route
    {
        #region Constructors

        public CommonPathRoute(string url, RouteValueDictionary defaultValues)
            : base(url, new RouteValueDictionary(defaultValues), new MvcRouteHandler())
        {
        }
        #endregion

        #region Methods

        /// <summary>
        /// Returns information about the requested route.
        /// </summary>
        /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
        /// <returns>
        /// An object that contains the values from the route definition.
        /// </returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var data = base.GetRouteData(httpContext);
            var path = "";
            var path2 = "";
            var path3 = "";
            if (data != null)
            {
                path = data.Values["generic_se_name"] as string;
                path2 = data.Values["se_name"] as string;
                path3 = data.Values["se_namelocation"] as string;
                switch (path)
                {
                    case "home":
                        {
                            data.Values["controller"] = "Home";
                            data.Values["action"] = "Index";
                        }
                        break;
                    case "prince2coursemethodology":
                        {
                            data.Values["controller"] = "coursemethodology";
                            data.Values["action"] = "Index";
                        }
                        break;
                    case "prince2onlinetraining":
                        {
                            data.Values["controller"] = "Online";
                            data.Values["action"] = "Index";
                        }
                        break;
                    case "downloadprince2":
                        {
                            data.Values["controller"] = "Download";
                            data.Values["action"] = "Index";
                        }
                        break;
                    case "prince2onsite":
                        {
                            data.Values["controller"] = "onsite";
                            data.Values["action"] = "Index";
                        }
                        break;
                    case "aboutus":
                        {
                            data.Values["controller"] = "About";
                            data.Values["action"] = "Index";
                        }
                        break;
                    case "contactus":
                        {
                            data.Values["controller"] = "Home";
                            data.Values["action"] = "Contact";
                        }
                        break;
                    case "careers":
                        {
                            data.Values["controller"] = "Home";
                            data.Values["action"] = "Career";
                        }
                        break;
                    case "highlyinteractive":
                        {
                            data.Values["controller"] = "Home";
                            data.Values["action"] = "Highlyinit";
                        }
                        break;
                    case "prince2exam":
                        {
                            data.Values["controller"] = "Home";
                            data.Values["action"] = "Exam";

                        }
                        break;
                    case "prince2presentation":
                        {
                            data.Values["controller"] = "Home";
                            data.Values["action"] = "Presentation";

                        }
                        break;
                    case "course":
                        {
                            data.Values["controller"] = "Bookonline";
                            data.Values["action"] = "Index";
                            data.Values["seName"] = path2;
                            data.Values["location"] = path3;
                            data.Values["ObType"] = "Course";
                        }
                        break;
                    case "onlinecourse":
                        {
                            data.Values["controller"] = "Bookonline";
                            data.Values["action"] = "Index";
                            data.Values["seName"] = path2;
                            data.Values["ObType"] = "Online";

                        }
                        break;
                    case "managing-sucessful-projects-with-prince2-2009-edition":
                        {
                            data.Values["controller"] = "Bookonline";
                            data.Values["action"] = "Index";
                            data.Values["seName"] = "Managing Sucessful Projects with PRINCE2 2009 Edition";
                            data.Values["price"] = "155";
                            data.Values["ObType"] = "Book";

                        }
                        break;
                    case "prince2-pocket-book-2009-edition":
                        {
                            data.Values["controller"] = "Bookonline";
                            data.Values["action"] = "Index";
                            data.Values["seName"] = "PRINCE2 Pocket Book -2009 Edition";
                            data.Values["price"] = "35";
                            data.Values["ObType"] = "Book";

                        }
                        break;
                    case "passing-the-prince2-examinations-2009-edition":
                        {
                            data.Values["controller"] = "Bookonline";
                            data.Values["action"] = "Index";
                            data.Values["seName"] = "Passing the PRINCE2 Examinations 2009 Edition";
                            data.Values["price"] = "95";
                            data.Values["ObType"] = "Book";

                        }
                        break;
                    case "core-prince2-exam-book-bundle-prince2-2009":
                        {
                            data.Values["controller"] = "Bookonline";
                            data.Values["action"] = "Index";
                            data.Values["seName"] = "Core PRINCE2 Exam Book Bundle (PRINCE2 2009)";
                            data.Values["price"] = "240";
                            data.Values["ObType"] = "Book";

                        }
                        break;
                    case "directing-successful-projects-with-prince2-2009-edition":
                        {
                            data.Values["controller"] = "Bookonline";
                            data.Values["action"] = "Index";
                            data.Values["seName"] = "Directing Successful Projects with PRINCE2 2009 Edition";
                            data.Values["price"] = "155";
                            data.Values["ObType"] = "Book";

                        }
                        break;
                    case "prince2books":
                        {
                            data.Values["controller"] = "Book";
                            data.Values["action"] = "Index";
                        }
                        break;
                    case "privacypolicy":
                        {
                            data.Values["controller"] = "Home";
                            data.Values["action"] = "Policy";
                        }
                        break;
                    case "termsandconditions":
                        {
                            data.Values["controller"] = "Home";
                            data.Values["action"] = "Terms";
                        }
                        break;
                    case "bookonline":
                        {
                            data.Values["controller"] = "Bookonline";
                            data.Values["action"] = "Index";
                        }
                        break;
                    case "paymentdetail":
                        {
                            data.Values["controller"] = "Payment";
                            data.Values["action"] = "InitiatePayment";
                        }
                        break;
                    case "payment":
                        {
                            data.Values["controller"] = "Paymentsuccess";
                            data.Values["action"] = "Index";
                        }
                        break;
                    case "Hsrsadmin":
                        {
                            data.Values["controller"] = "Hsrsadmin";
                            data.Values["action"] = "Index";
                        }
                        break;
                    case "Coursemaster":
                        {
                            data.Values["controller"] = "Coursemaster";
                            data.Values["action"] = "Index";
                        }
                        break;
                    case "editcourse":
                        {
                            data.Values["controller"] = "Coursemaster";
                            data.Values["action"] = "Edit";
                            data.Values["Id"] = path2;
                        }
                        break;
                    case "deletecourse":
                        {
                            data.Values["controller"] = "Coursemaster";
                            data.Values["action"] = "Delete";
                            data.Values["Id"] = path2;
                        }
                        break;
                    case "OnlineCoursemaster":
                        {
                            data.Values["controller"] = "OnlineCoursemaster";
                            data.Values["action"] = "Index";
                        }
                        break;
                    case "editonlinecourse":
                        {
                            data.Values["controller"] = "OnlineCoursemaster";
                            data.Values["action"] = "Edit";
                            data.Values["Id"] = path2;
                        }
                        break;
                    case "deleteonlinecourse":
                        {
                            data.Values["controller"] = "OnlineCoursemaster";
                            data.Values["action"] = "Delete";
                            data.Values["Id"] = path2;
                        }
                        break;
                    case "Trainingmaster":
                        {
                            data.Values["controller"] = "Trainingmaster";
                            data.Values["action"] = "Index";
                        }
                        break;
                    case "edittraining":
                        {
                            data.Values["controller"] = "Trainingmaster";
                            data.Values["action"] = "Edit";
                            data.Values["Id"] = path2;
                        }
                        break;
                    case "deletetraining":
                        {
                            data.Values["controller"] = "Trainingmaster";
                            data.Values["action"] = "Delete";
                            data.Values["Id"] = path2;
                        }
                        break;
                    case "logout":
                        {
                            data.Values["controller"] = "Hsrsadmin";
                            data.Values["action"] = "Logout";
                        }
                        break;
                    case "login":
                        {
                            data.Values["controller"] = "Home";
                            data.Values["action"] = "Login";
                        }
                        break;
                    case "sitemap":
                        {
                            data.Values["controller"] = "Home";
                            data.Values["action"] = "Sitemap";
                        }
                        break;
                    default:
                        //data.Values["controller"] = "Home";
                        //data.Values["action"] = "Index";
                        httpContext.Response.Redirect("http://www.hsrsglobal.com.au");
                        break;
                }

                //return data;
            }
            return data;
        }

        #endregion
    }
}
