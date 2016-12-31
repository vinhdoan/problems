using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ChatOnCom
{
    class TextProcess
    {
        //mảng chứa các biểu tưởng smile được dùng
        private string ImageDir;
        string[] SmilesArray = { ":P", ":)]", ":-c", ":-h", ":-t", "8->", "x_x", ":-*", ":-q", "=((", ":-o", ":>", "B-)", ":-S", ":((", ":))", ":(", ":)", "=))", "O:-)", ";)", "(:|", ":O)", ":-$", "[-(", ":ar!", "I-)", ":-w", "8-}", ":D", "@};-" };

        public TextProcess()
        {
            ImageDir = Environment.CurrentDirectory + @"\Images";
        }

        public TextProcess(string ImageDirUrl)
        {
            ImageDir = ImageDirUrl;
        }
        public string GetSmileURL(int index,string ImageType)
        {
            return string.Format("file:///{0}\\Smiles\\{1}.{2}", ImageDir, index.ToString(),ImageType);
        }

        public string GetImageURL(string ImageName)
        {
            return string.Format("file:///{0}\\{1}", ImageDir, ImageName);
        }

        public string GetFlashURL(string flash)
        {
            return "file:///" + ImageDir + "\\Flashs\\" + flash;
        }

        public string GetFlashHTML(string Flash)
        {
            StringBuilder result=new StringBuilder();
            //result.Append("<object>");
            //result.Append("<param name=\"movie\" value=\"" + GetFlashURL(Flash) + "\"/><param name=\"wmode\" value=\"transparent\" /><embed src=\"" + GetFlashURL(Flash) + "\" menu=\"false\" quality=\"high\" wmode=\"transparent\" width=\"400\" height=\"50\" align=\"middle\" allowScriptAccess=\"sameDomain\" type=\"application/x-shockwave-flash\"/></object>");
            //result.Append("<embed src=\"" + GetFlashURL(Flash) + "\" menu=\"false\" wmode=\"transparent\" width=\"400\" height=\"50\"/>");
            //result.Append("<embed src=\"" + GetFlashURL(Flash) + "\" width = \"400\" height =\"50\"/>");
            //result.Append("<embed src=\"file:///D:\\HOC TAP\\CAC KY HOC\\KY 8\\Thiet bi ngoai vi\\ComChatG8\\Flash\\co_len_em_yeu.swf\" height = 50 width = 200></embed>");
            result.Append("<OBJECT classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0\" WIDTH=\"400px\" HEIGHT=\"50px\" id=\"FlashMovie1\" ALIGN=\"none\"><PARAM NAME=movie VALUE=\"" + GetFlashURL(Flash) + "\"><PARAM NAME=scale VALUE=exactfit /> <PARAM NAME=wmode VALUE=transparent /> <PARAM NAME=devicefont VALUE=true /> <PARAM NAME=quality VALUE=AutoHigh/> <PARAM NAME=bgcolor VALUE=#ffffff/> <PARAM NAME=\"allowScriptAccess\" value=\"SameDomain\" /><EMBED src=\"" + GetFlashURL(Flash) + "\" scale=\"exactfit\" wmode=\"transparent\" devicefont=\"true\" quality=\"autohigh\" bgcolor=\"#ffffff\" allowScriptAccess=\"SameDomain\" WIDTH=\"400px\" HEIGHT=\"50px\" id=\"FlashMovie1\" ALIGN=\"none\" TYPE=\"application/x-shockwave-flash\" PLUGINSPAGE=\"http://www.macromedia.com/go/getflashplayer\"></EMBED></OBJECT><br>");
            return result.ToString();
        }
        public string GetSmileHTML(int index)
        {
            return string.Format("<img src = \"{0}\"/>", GetSmileURL(index,"gif"));
        }

        public string GetHexCodeColor(Color color)
        {
            return string.Format("#{0}{1}{2}", color.R.ToString("x2"), color.G.ToString("x2"), color.B.ToString("x2"));
        }

        public string ReplaceSmileURL(string msg)
        {
            for (int sm = 0; sm < SmilesArray.Length; sm++)
            {
                if (msg.ToLower().Contains(SmilesArray[sm].ToLower()))
                {
                    msg = msg.Replace(SmilesArray[sm], GetSmileHTML(sm));
                }
            }
            return msg;
        }

        public string GetFontHTMLFormat(string FontName,string FontColor,int FontSize)
        {
            return string.Format("<font face = \"{0}\" color=\"{1}\" size=\"{2}\">", FontName, FontColor, FontSize);
        }
        private List<string> listMessages;

       
        #region Constructor

        public TextProcess(List<string> lstMsg)
        {
            listMessages = lstMsg;
        }
        #endregion

        #region Properties
        public List<string> ListMessages
        {
            set { listMessages = value; }
            get { return listMessages; }
        }
        #endregion

        #region Methods
        public string MessageProcess(string msg)
        {
            if (listMessages == null)
                listMessages = new List<string>();
            listMessages.Add(msg);
            StringBuilder msgTmp = new StringBuilder(msg);
            return msgTmp.ToString();
        }

        public string GetSmileUrl(string msg)
        {
            string tmpURL="";
            return tmpURL;
        }
        #endregion
    }
}
