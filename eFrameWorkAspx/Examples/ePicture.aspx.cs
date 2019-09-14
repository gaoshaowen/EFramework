using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EKETEAM.FrameWork;

namespace eFrameWork.Examples
{
    public partial class _ePicture : System.Web.UI.Page
    {
        private eAction action;
        protected void Page_Load(object sender, EventArgs e)
        {
            action = new eAction();
            action.Actioning += action_Actioning;
            action.Listen();
        }
        protected void action_Actioning(string Action)
        {
            string source = "";
            string dest;
            switch (Action)
            {
                case "step1":
                    source = Server.MapPath("images/pic.bmp");
                    dest = Server.MapPath("images/pic_1.bmp");
                    if (System.IO.File.Exists(dest)) System.IO.File.Delete(dest);//如果有，则删除之前生成的文件。
                    System.IO.File.Copy(source, dest);//复制文件来操作，否则源文件将被替换。
                    //ePicture.SaveToJPG(dest, 100);//指定图片品质为：100,默认为80
                    ePicture.SaveToJPG(dest);
                    litBody.Text = "操作成功!<br>路径为：" + Server.MapPath("images/pic_1.jpg");
                    break;
                case "step2":
                    source = Server.MapPath("images/pic.jpg");
                    dest = Server.MapPath("images/pic_2.jpg");
                    if (System.IO.File.Exists(dest)) System.IO.File.Delete(dest);//如果有，则删除之前生成的文件。
                    System.IO.File.Copy(source, dest);//复制文件来操作，否则源文件将被替换
                    ePicture.ToWidth(dest, 800);
                    litBody.Text = "操作成功!<br>路径为：" + dest;
                    break;
                case "step3":
                    source = Server.MapPath("images/pic.jpg");
                    dest = Server.MapPath("images/pic_3.jpg");
                    if (System.IO.File.Exists(dest)) System.IO.File.Delete(dest);//如果有，则删除之前生成的文件。
                    System.IO.File.Copy(source, dest);//复制文件来操作，否则源文件将被替换
                    ePicture.ToHeight(dest, 800);
                    litBody.Text = "操作成功!<br>路径为：" + dest;
                    break;
                case "step4":
                    source = Server.MapPath("images/pic.jpg");
                    dest = Server.MapPath("images/pic_4.jpg");
                    if (System.IO.File.Exists(dest)) System.IO.File.Delete(dest);//如果有，则删除之前生成的文件。
                    System.IO.File.Copy(source, dest);//复制文件来操作，否则源文件将被替换
                    ePicture.ToSize(dest, 800, 600);
                    litBody.Text = "操作成功!<br>路径为：" + dest;
                    break;
                case "step5":
                    source = Server.MapPath("images/pic.jpg");
                    dest = Server.MapPath("images/pic_5.jpg");
                    if (System.IO.File.Exists(dest)) System.IO.File.Delete(dest);//如果有，则删除之前生成的文件。
                    System.IO.File.Copy(source, dest);//复制文件来操作，否则源文件将被替换
                    ePicture.Cut(dest,100, 100, 500, 500, 500, 500);
                    litBody.Text = "操作成功!<br>路径为：" + dest;
                    break;
                case "step6":
                    source = Server.MapPath("images/pic.jpg");
                    dest = Server.MapPath("images/pic_6.jpg");
                    if (System.IO.File.Exists(dest)) System.IO.File.Delete(dest);//如果有，则删除之前生成的文件。
                    ePicture.CreateThumbs(source, dest, 150, 0, "W");
                    //Mode说明:CUT(按宽高剪切),H(指定高，宽按比例 ),W(指定宽，高按比例 ),HW(指定高宽缩放,可能变形),DB(等比缩放)
                    litBody.Text = "操作成功!<br>路径为：" + dest;
                    break;
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Literal lit = (Literal)Master.FindControl("LitTitle");
            if (lit != null)
            {
                lit.Text = "ePicture图片处理类-eFrameWork示例中心";
            }
        }
    }   
}