using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKYPE4COMLib;


namespace SkypeBot
{
    class Program
    {
        private static Skype skype;
        private const string trigger = "!";
        private const string nick = "BOT";
        static void Main(string[] args)
        {
            skype = new Skype();
            skype.Attach(7, false);
            skype.MessageStatus += new _ISkypeEvents_MessageStatusEventHandler(skype_MessageStatus);
            while (Console.ReadKey(true).KeyChar != 's') ;
        }

        private static string ProcessCommand(string str)
        {
            string result;
            switch (str)
            {
                case "hello":
                    result = "Hello!";
                    break;
                case "hi":
                    result = "Hi!";
                    break;
                default:
                    result = "Nothing";
                    break;
            }
            return result;
        }


        static void skype_MessageStatus(ChatMessage pMessage, TChatMessageStatus Status)
        {
            if (pMessage.Body.IndexOf(trigger) == 0)
            {
                string command = pMessage.Body.Remove(0, trigger.Length).ToLower();
                //skype.SendMessage(pMessage.Sender.Handle, nick + " says: " + ProcessCommand(command));
                //skype.SendMessage(pMessage.Sender.Handle, skype.Friends.Count.ToString());
                //foreach (User name in skype.Friends)
                //{
                //    skype.SendMessage(pMessage.Sender.Handle, name.Handle + ":" + name.FullName);
                //}
                skype.SendMessage(pMessage.Sender.Handle, "I am a Bot");
                skype.SendMessage("nhattuan.tran.en", "Hello: " + command);
            }
        }
    }
}
