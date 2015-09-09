using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace HSRS_Global.Models
{
    public class Trainingmaster
    {
        public string Action { get; set; }
        public int T_Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }


        public DataSet FTrainingmaster(Trainingmaster obj)
        {
            DataSet ds = null;
            try
            {
                #region Parameters
                string[,] str = new string[4, 2];
                str[0, 0] = "@Action";
                str[0, 1] = obj.Action;

                str[1, 0] = "@Tid";
                str[1, 1] = obj.T_Id.ToString();

                str[2, 0] = "@Name";
                if (obj.Name == null)
                {
                    str[2, 1] = "";
                }
                else
                {
                    str[2, 1] = obj.Name;
                }

                str[3, 0] = "@Active";
                str[3, 1] = obj.Active.ToString();
                #endregion
                Database db = new Database();
                ds = db.StoreprocedureExecuteDsReturned("[SP_HSRS_Training_Masters]", str);
                return ds;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}