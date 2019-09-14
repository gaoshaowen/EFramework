using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Drawing2D;
using EKETEAM.FrameWork;


namespace eFrameWork.Plugins
{
    public partial class RndPic : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string checkCode = eBase.getRnd(4);// CreateRandomCode(4);
            Session["Plugins_RndCode"] = checkCode;
            CreateImage(checkCode);
        }
        private string CreateRandomCode(int codeCount)
        {
            //string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z"; 
            string allChar = "0,1,2,3,4,5,6,7,8,9";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(10);
                if (temp == t)
                {
                    return CreateRandomCode(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }
        private void CreateImage(string checkCode)
        {
            Bitmap bt = new Bitmap(45, 18, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(bt);
            //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;//平滑

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Color.FromArgb(255, 255, 255));//背景颜色
            Font fn1 = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
            System.Drawing.Brush b = new System.Drawing.SolidBrush(Color.FromArgb(153, 0, 0));
            g.DrawString(checkCode, fn1, b, new PointF(0, 0));




            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            myImageCodecInfo = ImageCodecInfo.GetImageEncoders()[0];
            myEncoder = Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bt.Save(ms, myImageCodecInfo, myEncoderParameters);
            Response.ClearContent();
            Response.ContentType = "image/Jpeg";
            Response.BinaryWrite(ms.ToArray());

            myEncoderParameter.Dispose();
            myEncoderParameters.Dispose();
            b.Dispose();
            g.Dispose();
            bt.Dispose();
            Response.End();
        }
        private void CreateImage1(string checkCode)
        {
            int iwidth = (int)(checkCode.Length * 11.5);
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 20);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
            //System.Drawing.Font f = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Regular); 
            System.Drawing.Font f = new System.Drawing.Font("宋体", 12, System.Drawing.FontStyle.Regular);
            System.Drawing.Brush b = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            //g.FillRectangle(new System.Drawing.SolidBrush(Color.Blue),0,0,image.Width, image.Height); 
            //g.Clear(System.Drawing.Color.Blue);
            //g.Clear(System.Drawing.Color.FromName("0000FF"));
            g.Clear(System.Drawing.Color.FromArgb(255, 255, 255));
            g.DrawString(checkCode, f, b, 3, 3);

            System.Drawing.Pen blackPen = new System.Drawing.Pen(System.Drawing.Color.White, 0);
            Random rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                int y = rand.Next(image.Height);
                //g.DrawLine(blackPen, 0, y, image.Width, y); 
            }

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            Response.ClearContent();
            Response.ContentType = "image/Jpeg";
            Response.BinaryWrite(ms.ToArray());

            g.Dispose();
            image.Dispose();
        } 
    }
}