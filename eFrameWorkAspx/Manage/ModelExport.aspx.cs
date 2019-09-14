using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EKETEAM.Data;
using EKETEAM.FrameWork;

namespace eFrameWork.Manage
{
    public partial class ModelExport : System.Web.UI.Page
    {
        bool outFile = true;

        private string getModelJson(string ModelID)
        {
            eMTable models = new eMTable("a_eke_sysModels");
            models.Where.Add("ModelID='" + ModelID + "'");

            eMTable Items = new eMTable("a_eke_sysModelItems");
            models.AddChild(Items);
            
            eMTable Conds = new eMTable("a_eke_sysModelConditions");
            eMTable CondItems = new eMTable("a_eke_sysModelConditionItems");
            Conds.AddChild(CondItems);
            models.AddChild(Conds);

            eMTable action = new eMTable("a_eke_sysActions");
            models.AddChild(action);

            eMTable modelcond = new eMTable("a_eke_sysConditions");
            models.AddChild(modelcond);

            eMTable tabs = new eMTable("a_eke_sysModelTabs");
            models.AddChild(tabs);

            eMTable groups = new eMTable("a_eke_sysModelPanels");
            models.AddChild(groups);

            string ct = eOleDB.getValue("select count(*) from a_eke_sysCheckUps where ModelID='" + ModelID + "'");
            if (ct.Length > 0 && ct != "0")
            {
                eMTable checkups = new eMTable("a_eke_sysCheckUps");
                models.AddChild(checkups);
            }


            string json = models.ExportJson();
           
            eJson _json = new eJson(json);
            _json.Convert = true;
            string code = eOleDB.getValue("select Code from a_eke_sysModels where ModelID='" + ModelID + "'");
            string modelSQL = eOleDB.getTableSql(code);
            _json.Add("modelSQL", HttpUtility.UrlEncode(modelSQL));

            DataTable dt = eOleDB.getDataTable("select ModelID,MC,Code,Auto,AspxFile from a_eke_sysModels where ParentID='" + ModelID + "' and delTag=0");
            foreach (DataRow dr in dt.Rows)
            {
                string js = getModelJson(dr["ModelID"].ToString());
                eJson _js = new eJson(js);
                _js.Convert = true;
                _json.Add("subModels", _js);
            }
            return _json.ToString(); 
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            string ModelID = eParameters.QueryString("ModelID");
            DataTable dt = eOleDB.getDataTable("select ModelID,MC,Code,Auto,AspxFile from a_eke_sysModels where ModelID='" + ModelID + "'");
            if (dt.Rows.Count == 0)
            {
                Response.End();
            }



           
            string json = "";
            if (dt.Rows[0]["Auto"].ToString() == "True") //自动模块
            {
                json = getModelJson(ModelID);
            }
            else //自定义模块
            {
                #region 自定义模块
                eMTable models = new eMTable("a_eke_sysModels");
                models.Where.Add("ModelID='" + ModelID + "'");

                json = models.ExportJson();
                eJson _json = new eJson(json);
                _json.Convert = true;
                string text = "";
                string file = dt.Rows[0]["AspxFile"].ToString();
                string aspxFile = Server.MapPath("~/System/") + file;
                if (System.IO.File.Exists(aspxFile))
                {
                    text = eBase.ReadFile(aspxFile);
                    text = eBase.encode(text);
                    _json.Add("aspxFile", text);
                }
                string csFile = Server.MapPath("~/System/") + file + ".cs";
                if (System.IO.File.Exists(csFile))
                {
                    text = eBase.ReadFile(csFile);
                    text = eBase.encode(text);
                    _json.Add("csFile", text);
                }
                string desFile = Server.MapPath("~/System/") + file + ".designer.cs";
                if (System.IO.File.Exists(desFile))
                {
                    text = eBase.ReadFile(desFile);
                    text = eBase.encode(text);
                    _json.Add("desFile", text);
                }
                json = _json.ToString();
                #endregion
            }
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            byte[] outBuffer = new byte[buffer.Length + 3];
            outBuffer[0] = (byte)0xEF;
            outBuffer[1] = (byte)0xBB;
            outBuffer[2] = (byte)0xBF;
            Array.Copy(buffer, 0, outBuffer, 3, buffer.Length);
            if (outFile)
            {

                string fileName = dt.Rows[0]["mc"].ToString() + ".efw";
                if (Request.ServerVariables["HTTP_USER_AGENT"].ToString().ToLower().IndexOf("msie") > -1) fileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);  //IE需要编码
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Accept-Ranges", "bytes");
                Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                Response.Write(Encoding.UTF8.GetString(outBuffer));
            }
            else
            {
                Response.Write(json);
            }
            Response.End();



        }
    }
}