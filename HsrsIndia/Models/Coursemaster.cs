using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
namespace HsrsIndia.Models
{
    public class Coursemaster
    {
        public string Action { get; set; }
        public int C_Id { get; set; }
        public int Trainingid { get; set; }
        public string Name { get; set; }
        public string SlugName { get; set; }
        public string Date { get; set; }
        public string Location { get; set; }
        public double Duration { get; set; }
        public double Price { get; set; }
        public bool Active { get; set; }

        public DataSet coursemaster(Coursemaster obj)
        {
            DataSet ds =null;
            try
            {
                #region Parameters
                string[,] str = new string[10, 2];
                str[0, 0] = "@Action";
                str[0, 1] = obj.Action;

                str[1, 0] = "@C_Id";
                str[1, 1] = obj.C_Id.ToString();

                str[2, 0] = "@Trainingid";
                str[2, 1] = obj.Trainingid.ToString();

                str[3, 0] = "@Name";
                if (obj.Name == null)
                {
                    str[3, 1] = "";
                }
                else
                {
                    str[3, 1] = obj.Name;
                }
                str[4, 0] = "@Date";
                
                if (obj.Date == null)
                {
                    str[4, 1] = "";
                }
                else
                {
                    str[4, 1] = obj.Date;
                }

                str[5, 0] = "@Location";
                if (obj.Location == null)
                {
                    str[5, 1] = "";
                }
                else
                {
                    str[5, 1] = obj.Location;
                }
               

                str[6, 0] = "@Duration";
                str[6, 1] = obj.Duration.ToString();

                str[7, 0] = "@Price";
                str[7, 1] = obj.Price.ToString();

                str[8, 0] = "@Active";
                str[8, 1] = obj.Active.ToString();

                str[9, 0] = "@SlugName";
                if (obj.SlugName == null)
                {
                    str[9, 1] = "";
                }
                else
                {
                    str[9, 1] = obj.SlugName;
                }
                #endregion
                Database db = new Database();
                ds = db.StoreprocedureExecuteDsReturned("[SP_HSRS_Course_Masters]", str);
                return ds;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}