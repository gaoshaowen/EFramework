using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.FrameWork;
using EKETEAM.Data;

namespace eFrameWork.Manage
{
    public partial class ModelItems_Layout : System.Web.UI.Page
    {
        public string modelid = eParameters.QueryString("modelid");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (eBase.showHelp())
            {
                Response.Write("<div class=\"tips\" style=\"margin-bottom:6px;\">");
                Response.Write("<b>布局</b><br>");
                Response.Write("为表单添加选项卡、面板、列显示顺序、跨行、跨列等。<br>");
                Response.Write("建议完成表单所有功能再为表单布局。");
                Response.Write("</div> ");
            }

            eList elist = new eList("a_eke_sysModelTabs");
            elist.Where.Add("ModelID='" + modelid + "'");
            elist.OrderBy.Add("px,addTime");
            elist.Bind(RepTags);


            eList elistg = new eList("a_eke_sysModelPanels");
            elistg.Where.Add("ModelID='" + modelid + "'");
            elistg.OrderBy.Add("px,addTime");
            RepGroups.ItemDataBound += new RepeaterItemEventHandler(RepGroups_ItemDataBound);
            elistg.Bind(RepGroups);

            /*
            eList datalist = new eList("a_eke_sysModelItems");
            datalist.Where.Add("ModelID='" + modelid + "' and showadd=1");
            datalist.OrderBy.Add("case when ModelTabID Is Null then 1 else 0 end,case when ModelPanelID Is Null then 1 else 0 end ,addorder");
            RepColumns.ItemDataBound += new RepeaterItemEventHandler(RepColumns_ItemDataBound);
            datalist.Bind(RepColumns);
            */
            #region 所有列
            string sql = "SELECT d.mc as modelName,a.MC, a.ModelItemID,a.ModelTabID,a.ModelPanelID,a.addrowspan,a.addcolspan,a.addorder FROM a_eke_sysModelItems a ";
            sql += " inner join a_eke_sysModels d on d.ModelID=a.ModelID ";
            sql += " left join a_eke_sysModelTabs b on a.ModelTabID=b.ModelTabID ";
            sql += " left join a_eke_sysModelPanels c on a.ModelPanelID=c.ModelPanelID ";
            sql += " where a.ModelID='" + modelid + "' and a.showAdd=1 and a.controlType<>'hidden' ";
            sql += " order by ISNULL(b.px,999999),ISNULL(c.px,999999),a.AddOrder, a.PX ";

            sql="select * from ";
            sql += " (";
            sql += " SELECT d.mc as modelName,a.MC, a.ModelItemID,a.ModelTabID,a.ModelPanelID,a.addrowspan,a.addcolspan,a.addorder,ISNULL(b.px,999999)as bpx,ISNULL(c.px,999999) as cpx,a.PX,a.addTime  FROM a_eke_sysModelItems a ";
            sql += " inner join a_eke_sysModels d on d.ModelID=a.ModelID";
            sql += " left join a_eke_sysModelTabs b on a.ModelTabID=b.ModelTabID ";
            sql += " left join a_eke_sysModelPanels c on a.ModelPanelID=c.ModelPanelID ";
            sql += " where a.ModelID='" + modelid + "' and a.delTag=0 and a.showAdd=1 and a.controlType<>'hidden' ";
            sql += " union ";
            sql += " SELECT d.mc as modelName,a.MC, a.ModelItemID,a.ModelTabID,a.ModelPanelID,a.addrowspan,a.addcolspan,a.addorder,ISNULL(b.px,999999)as bpx,ISNULL(c.px,999999) as cpx,a.PX,a.addTime  FROM a_eke_sysModelItems a ";
            sql += " inner join a_eke_sysModels d on d.ModelID=a.ModelID";
            sql += " left join a_eke_sysModelTabs b on a.ModelTabID=b.ModelTabID ";
            sql += " left join a_eke_sysModelPanels c on a.ModelPanelID=c.ModelPanelID ";
            sql += " where d.delTag=0 and d.ParentID ='" + modelid + "' and d.JoinMore=0 and a.showAdd=1 and a.controlType<>'hidden' ";
            sql += " ) as t";
            sql += " order by t.bpx,t.cpx,t.AddOrder,t.px,t.addTime";

           // eBase.Writeln(sql);

            DataTable dt = eOleDB.getDataTable(sql);
            RepColumns.ItemDataBound += new RepeaterItemEventHandler(RepColumns_ItemDataBound);
            RepColumns.DataSource = dt;
            RepColumns.DataBind();
            #endregion

            #region 子模块
            sql = "SELECT ModelID,ModelTabID,ModelPanelID,MC FROM  a_eke_sysModels";
            sql += " where ParentID='" + modelid + "' and JoinMore=1 and delTag=0";
            sql += " order by addTime";
            DataTable tb = eOleDB.getDataTable(sql);
            RepModels.ItemDataBound += new RepeaterItemEventHandler(RepModels_ItemDataBound);
            RepModels.DataSource = tb;
            RepModels.DataBind();
            #endregion
        }
        protected void RepModels_ItemDataBound(object sender, RepeaterItemEventArgs e)
        { 
             string sql = "";
             if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
             {
                 Control ctrl = e.Item.Controls[0];
                 Literal lit = (Literal)ctrl.FindControl("LitTags");
                 if (lit != null)
                 {
                     sql = "select ModelTabID,MC from a_eke_sysModelTabs where ModelID='" + modelid + "' order by px,addTime";
                     lit.Text = eOleDB.getOptions(sql, "MC", "ModelTabID", DataBinder.Eval(e.Item.DataItem, "ModelTabID").ToString());
                 }
                 lit = (Literal)ctrl.FindControl("LitGroups");
                 if (lit != null)
                 {
                     if (DataBinder.Eval(e.Item.DataItem, "ModelTabID").ToString().Length > 0)
                     {
                         sql = "select ModelPanelID,MC from a_eke_sysModelPanels where delTag=0 and ModelID='" + modelid + "' and ModelTabID='" + DataBinder.Eval(e.Item.DataItem, "ModelTabID").ToString() + "'  order by px,addTime";
                     }
                     else
                     {
                         sql = "select ModelPanelID,MC from a_eke_sysModelPanels where delTag=0 and ModelID='" + modelid + "' and  ModelTabID is Null order by px,addTime";
                     }
                     lit.Text = eOleDB.getOptions(sql, "MC", "ModelPanelID", DataBinder.Eval(e.Item.DataItem, "ModelPanelID").ToString());
                 }
             }
        }
        protected void RepGroups_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string sql = "";
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Control ctrl = e.Item.Controls[0];
                Literal lit = (Literal)ctrl.FindControl("LitTags");
                if (lit != null)
                {
                    sql = "select ModelTabID,MC from a_eke_sysModelTabs where ModelID='" + modelid + "' order by px,addTime";
                    lit.Text = eOleDB.getOptions(sql, "MC", "ModelTabID", DataBinder.Eval(e.Item.DataItem, "ModelTabID").ToString());
                }
            }
        }
        protected void RepColumns_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string sql = "";
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Control ctrl = e.Item.Controls[0];
                Literal lit = (Literal)ctrl.FindControl("LitTags");
                if (lit != null)
                {
                    sql = "select ModelTabID,MC from a_eke_sysModelTabs where ModelID='" + modelid + "' order by px,addTime";
                    lit.Text = eOleDB.getOptions(sql, "MC", "ModelTabID", DataBinder.Eval(e.Item.DataItem, "ModelTabID").ToString());
                }

                lit = (Literal)ctrl.FindControl("LitGroups");
                if (lit != null)
                {
                    if (DataBinder.Eval(e.Item.DataItem, "ModelTabID").ToString().Length > 0)
                    {
                        sql = "select ModelPanelID,MC from a_eke_sysModelPanels where delTag=0 and ModelID='" + modelid + "' and ModelTabID='" + DataBinder.Eval(e.Item.DataItem, "ModelTabID").ToString() + "'  order by px,addTime";
                    }
                    else
                    {
                        sql = "select ModelPanelID,MC from a_eke_sysModelPanels where delTag=0 and ModelID='" + modelid + "' and  ModelTabID is Null order by px,addTime";
                    }
                    lit.Text = eOleDB.getOptions(sql, "MC", "ModelPanelID", DataBinder.Eval(e.Item.DataItem, "ModelPanelID").ToString());
                }
            }
        }
    }
}