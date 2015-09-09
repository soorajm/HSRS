using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace HSRS_Global.Models
{
    public class Customerdata
    {
        public string Action { get; set; }
        public int C_Id { get; set; }
        public string Cus_id { get; set; }
        public string Title { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string cmpname { get; set; }
        public string add1 { get; set; }
        public string add2 { get; set; }
        public string city { get; set; }
        public string postcode { get; set; }
        public string country { get; set; }
        public string tele { get; set; }
        public string mobile { get; set; }
        public string mail { get; set; }
        public string Transactionnumber { get; set; }
        public string TotalAmount { get; set; }
        public string paymentst { get; set; }
        public string Tdate { get; set; }

        public DataSet custmaster(Customerdata obj)
        {
            DataSet ds = null;
            try
            {
                #region Parameters
                string[,] str = new string[19, 2];
                str[0, 0] = "@Action";
                str[0, 1] = obj.Action;

                str[1, 0] = "@C_Id";
                str[1, 1] = obj.C_Id.ToString();

                str[2, 0] = "@Cus_id";
                if (obj.Cus_id == null)
                {
                    str[2, 1] = "";
                }
                else
                {
                    str[2, 1] = obj.Cus_id;
                }

                str[3, 0] = "@Title";
                if (obj.Title == null)
                {
                    str[3, 1] = "";
                }
                else
                {
                    str[3, 1] = obj.Title;
                }
                str[4, 0] = "@Fname";

                if (obj.Fname == null)
                {
                    str[4, 1] = "";
                }
                else
                {
                    str[4, 1] = obj.Fname;
                }

                str[5, 0] = "@Lname";
                if (obj.Lname == null)
                {
                    str[5, 1] = "";
                }
                else
                {
                    str[5, 1] = obj.Lname;
                }

                str[6, 0] = "@cmpname";
                if (obj.cmpname == null)
                {
                    str[6, 1] = "";
                }
                else
                {
                    str[6, 1] = obj.cmpname;
                }
                str[7, 0] = "@add1";
                if (obj.add1 == null)
                {
                    str[7, 1] = "";
                }
                else
                {
                    str[7, 1] = obj.add1;
                }
                str[8, 0] = "@add2";
                if (obj.add2 == null)
                {
                    str[8, 1] = "";
                }
                else
                {
                    str[8, 1] = obj.add2;
                }
                str[9, 0] = "@city";
                if (obj.city == null)
                {
                    str[9, 1] = "";
                }
                else
                {
                    str[9, 1] = obj.city;
                }
                str[10, 0] = "@postcode";
                if (obj.postcode == null)
                {
                    str[10, 1] = "";
                }
                else
                {
                    str[10, 1] = obj.postcode;
                }
                str[11, 0] = "@country";
                if (obj.country == null)
                {
                    str[11, 1] = "";
                }
                else
                {
                    str[11, 1] = obj.country;
                }
                str[12, 0] = "@tele";
                if (obj.tele == null)
                {
                    str[12, 1] = "";
                }
                else
                {
                    str[12, 1] = obj.tele;
                }
                str[13, 0] = "@mobile";
                if (obj.mobile == null)
                {
                    str[13, 1] = "";
                }
                else
                {
                    str[13, 1] = obj.mobile;
                }
                str[14, 0] = "@mail";
                if (obj.mail == null)
                {
                    str[14, 1] = "";
                }
                else
                {
                    str[14, 1] = obj.mail;
                }
                str[15, 0] = "@Transactionnumber";
                if (obj.Transactionnumber == null)
                {
                    str[15, 1] = "";
                }
                else
                {
                    str[15, 1] = obj.Transactionnumber;
                }
                str[16, 0] = "@TotalAmount";
                if (obj.TotalAmount == null)
                {
                    str[16, 1] = "";
                }
                else
                {
                    str[16, 1] = obj.TotalAmount;
                }
                str[17, 0] = "@Date";
                if (obj.Tdate == null)
                {
                    str[17, 1] = "";
                }
                else
                {
                    str[17, 1] = obj.Tdate;
                }
                str[18, 0] = "@paymentst";
                if (obj.paymentst == null)
                {
                    str[18, 1] = "";
                }
                else
                {
                    str[18, 1] = obj.paymentst;
                }
                #endregion
                Database db = new Database();
                ds = db.StoreprocedureExecuteDsReturned("[SP_HSRS_customer_det]", str);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}