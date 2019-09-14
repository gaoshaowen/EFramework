using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EKETEAM.FrameWork;

namespace eFrameWork.Examples
{
    public partial class _eJson : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Example1();
        }
        private void Example1()
        {
            StringBuilder sb = new StringBuilder();
            #region 例1
            sb.Append("<font color=\"#ff0000\"><b>例1：</b></font><br />\r\n");
            eJson item = new eJson();
            item.Add("Name", "eFrameWork");
            item.Add("Version", "V1.0");
            sb.Append("生成：" + item.ToString() + "<br />\r\n");
            sb.Append("解析：<br />\r\n");
            eJson model1 = new eJson(item.ToString());
            foreach (string key in model1.GetKeys())
            {
                sb.Append(key + " = " + model1.GetValue(key) + "<br />\r\n");
            }
            #endregion
            #region 例2
            sb.Append("<font color=\"#ff0000\"><b>例2：</b></font><br />\r\n");
            eJson json = new eJson();
            eJson item1 = new eJson();
            item1.Add("Name", "李先生");
            item1.Add("Sex", "男");
            json.Add(item1);

            eJson item2 = new eJson();
            item2.Add("Name", "韩小姐");
            item2.Add("Sex", "女");
            json.Add(item2);

            sb.Append("生成：" + json.ToString() + "<br />\r\n");

            sb.Append("解析：<br />\r\n");

            eJson model2 = new eJson(json.ToString());
            foreach (eJson m in model2.GetCollection())
            {
                foreach (string key in m.GetKeys())
                {
                   sb.Append( key + " = " + m.GetValue(key) + "<br>\r\n");
                }
            }
            #endregion
            #region 例3
            sb.Append("<font color=\"#ff0000\"><b>例3：</b></font><br />\r\n");
            eJson itemAll = new eJson();
            itemAll.Add("Name", "所有");
            itemAll.Add("Items", json);



            sb.Append("生成：" + itemAll.ToString() + "<br />\r\n");

            sb.Append("解析：<br />\r\n");

            eJson model3 = new eJson(itemAll.ToString());

            foreach (string key in model3.GetKeys())
            {
                if (model3.IsValue(key))
                {
                    sb.Append(key + " = " + model3.GetValue(key) + "<br />\r\n");
                }
                if (model3.IsCollection(key))
                {
                    sb.Append(key + ":<br>");
                    foreach (eJson m in model3.GetCollection(key).GetCollection())
                    {
                        foreach (string key1 in m.GetKeys())
                        {
                            sb.Append(key1 + " = " + m.GetValue(key1) + "<br>\r\n");
                        }
                        
                    }
                }
            }
            #endregion

            #region 例4


            



            #endregion

            litSingle1.Text = sb.ToString();
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "Json处理-eFrameWork示例中心";
            }
        }
    }
}