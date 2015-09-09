using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HsrsIndia.Models
{
    public class OnlineCourseMaster
    {
        public string Action { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug_Name { get; set; }
        public string Details { get; set; }
        public string C_Price { get; set; }
        public bool Active { get; set; }

        public DataSet OnlineCourseMasters(OnlineCourseMaster obj)
        {
            DataSet ds = null;
            try
            {
                #region Parameters
                string[,] str = new string[7, 2];
                str[0, 0] = "@Action";
                str[0, 1] = obj.Action;

                str[1, 0] = "@id";
                str[1, 1] = obj.Id.ToString();

                str[2, 0] = "@Name";
                if (obj.Name == null)
                {
                    str[2, 1] = "";
                }
                else
                {
                    str[2, 1] = obj.Name;
                }

                str[3, 0] = "@Details";
                if (obj.Details == null)
                {
                    str[3, 1] = "";
                }
                else
                {
                    str[3, 1] = obj.Details;
                }
                str[4, 0] = "@C_Price";
                if (obj.C_Price == null)
                {
                    str[4, 1] = "";
                }
                else
                {
                    str[4, 1] = obj.C_Price;
                }
                str[5, 0] = "@Active";
                str[5, 1] = obj.Active.ToString();

                str[6, 0] = "@slugName";
                if (obj.Slug_Name == null)
                {
                    str[6, 1] = "";
                }
                else
                {
                    str[6, 1] = obj.Slug_Name;
                }

                #endregion
                Database db = new Database();
                ds = db.StoreprocedureExecuteDsReturned("[SP_HSRS_Onlinecourse_Masters]", str);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}