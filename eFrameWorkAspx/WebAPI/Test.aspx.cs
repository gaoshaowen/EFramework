using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using EKETEAM.FrameWork;
using EKETEAM.Data;



namespace eFrameWork.WebAPI
{
    public partial class Test : System.Web.UI.Page
    {
        public string tokenstr = "";
        protected void Page_Load(object sender, EventArgs e)
        {


            eToken token_default = new eToken();
            token_default.Exp = 60 * 60;
            //token_default.Add("id", "f9ca0e08-0ce6-433e-8c0a-b08b56c86e4b");
            //token_default.Add("nickname", "东东");
            token_default.Add("id", "f157d9e6-111b-4d0f-b22b-bb1ebc77a486");            
            token_default.Add("nickname", "333");
            tokenstr = token_default.Create();

            //eBase.Write(tokenstr);





            //string tokenstr = "06841095ADDB705B76053D24C19BF707ED46C19D50CD552953B29A5B52A40AAD35CD1D9B25C64020736334FC68493C42155EF07AC8A47BC371E226A2D8FE7FE3E08BA9CC8D313645F324D49735F8D89514661209874372B5CB2D3504CEB991AAD1056F03E50CD1373D4A6D538E803546DEC6B0DF37CDFEDF78C51E035BA2C79923B8DBEE4E36CE56A6195000C410A2A0EEF23B623A483D632ACA0F3E91FE85EE";
            //eToken token1 = new eToken("" + tokenstr);


            string act = eParameters.QueryString("act");
            if (act == "get")
            {
                //if(Request.Headers["auth"]!=null) Response.Write(Request.Headers["auth"].ToString() + "\r\n");
                //Response.Headers.Add("refresh_auth", "333"); 要集成模式，小程序不支持
                string auth = Request.Headers["auth"].ToString();
                eToken token = new eToken(auth);
                eUser user = new eUser(token);
                
                if (Request.UrlReferrer != null) Response.Write(Request.UrlReferrer.ToString());
                Response.Write(user.ID);
                Response.End();
            }
        }
    }
}