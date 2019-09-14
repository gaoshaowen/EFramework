using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using EKETEAM.FrameWork;

namespace eFrameWork.Plugins
{
    public partial class FontIco : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            string str = eBase.ReadFile(Server.MapPath("fontico.txt"));
            Regex regx = new Regex(@"<section[^>]*?>([\s\S]*?)</section>", RegexOptions.IgnoreCase); //有换行
            for (Match mn = regx.Match(str); mn.Success; mn = mn.NextMatch())
            {
                string html = mn.Groups[0].Value;

                if (html.IndexOf("mainParts") > -1 && html.IndexOf("fontawesome-icon-list") > -1)
                {

                    Match _m = Regex.Match(html, @"<h2[^>]*?>.*?</h2>");
                    if (_m.Success)
                    {

                        sb.Append(_m.Value + "\r\n");
                    }
                    Regex reg = new Regex(@"<i[^>]*?>.*?</i>", RegexOptions.IgnoreCase); //无换行
                    for (Match m = reg.Match(html); m.Success; m = m.NextMatch())
                    {
                        sb.Append("<a class=\"fa-hover\" href=\"javascript:;\" onclick=\"parent.setIco(this);\">" + m.Groups[0].Value + "</a>\r\n");
                    }
                }
            }

            LitBody.Text = sb.ToString();
        }
    }
}