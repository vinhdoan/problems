using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Net;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Drive.v2.Data;

namespace GoogleDrive_notifications
{
    class Program
    {
        private static bool stop_sign = false;
        private static long? current_change_id = GetCurrentChangeId();
        private static List<String> list_of_fileids = GetCurrentListOfFileIds();

        public static List<Change> RetrieveAllChanges(DriveService service, long? startChangeId)
        {
            List<Change> result = new List<Change>();
            ChangesResource.ListRequest request = service.Changes.List();
            request.IncludeDeleted = true;

            if (!String.IsNullOrEmpty(startChangeId.ToString()))
            {
                request.StartChangeId = startChangeId;
            }
            do
            {
                try
                {
                    ChangeList changes = request.Execute();
                    result.AddRange(changes.Items);
                    request.PageToken = changes.NextPageToken;
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);
                    request.PageToken = null;
                }
            } while (!String.IsNullOrEmpty(request.PageToken));
            return result;
        }

        public static void SendToSlack(string text)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://hooks.slack.com/services/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");
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"text\":\"" + text + "\"}";
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }

        public static bool IsAChild(DriveService service, string file_id, string dir_id)
        {
            ParentReference parent = service.Parents.List(file_id).Execute().Items[0];
            if (parent.IsRoot == true) return false;
            else
                if (parent.Id == dir_id) return true;
            else
                return IsAChild(service, parent.Id, dir_id);
        }

        public static bool IsMonitored(string FileId)
        {
            bool result = false;
            for (int i = 0; i < list_of_fileids.Count; i++)
            {
                if (FileId == list_of_fileids[i])
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public static void WriteToFile()
        {
            FileStream fs = new FileStream("stored", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            foreach (String str in list_of_fileids)
                sw.WriteLine(str);
            sw.Close();
            fs.Close();
        }

        public static void StoreCurrentChangeId()
        {
            FileStream fs = new FileStream("changeid", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(current_change_id);
            sw.Close();
            fs.Close();
        }

        public static long? GetCurrentChangeId()
        {
            FileStream fs = new FileStream("changeid", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            long? current_change_id = long.Parse(sr.ReadLine());
            sr.Close();
            fs.Close();
            return current_change_id;
        }

        public static List<String> GetCurrentListOfFileIds()
        {
            List<String> result = new List<String>();
            FileStream fs = new FileStream("stored", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            while (!sr.EndOfStream) result.Add(sr.ReadLine());
            sr.Close();
            fs.Close();
            foreach (String str in result)
                Console.WriteLine(str);
            //result.Add("0B76mTNiHDqCfazhVdklsMGxFbGs");         //liGhtC folder
            //result.Add("0B76mTNiHDqCfbi1sa2VENV9sb0U");         //Light Team.docx
            return result;
        }

        public static void Transaction(DriveService service)
        {
            try
            {
                List<Change> result = RetrieveAllChanges(service, current_change_id + 1);
                if (result.Count == 0)
                {
                    Console.WriteLine("Nothing changed.");
                }
                else
                {
                    current_change_id = result[result.Count - 1].Id;
                    StoreCurrentChangeId();
                    result.ForEach(delegate(Change change)
                    {
                        if (IsMonitored(change.FileId))
                            if (change.File != null && IsAChild(service, change.FileId, list_of_fileids[0]))
                                if (change.File.Labels.Trashed == true)
                                {
                                    Console.WriteLine("File " + change.File.Title + " has been TRASHED. ChangeId = " + change.Id);
                                    SendToSlack("File " + change.File.Title + " has been TRASHED. ChangeId = " + change.Id);
                                    //list_of_fileids.Remove(change.File.Id);
                                    //WriteToFile(list_of_fileids);
                                }
                                else
                                {
                                    Console.WriteLine("File " + change.File.Title + " has been UPDATED. ChangeId = " + change.Id);
                                    SendToSlack("File " + change.File.Title + " has been UPDATED. ChangeId = " + change.Id);
                                }
                            else
                            {
                                Console.WriteLine("File has been DELETED. ChangeId = " + change.Id);
                                SendToSlack("File has been DELETED. ChangeId = " + change.Id);
                                list_of_fileids.Remove(change.FileId);
                                WriteToFile();
                            }
                        else
                            if (change.File != null && IsAChild(service, change.FileId, list_of_fileids[0]))
                            {
                                Console.WriteLine("File " + change.File.Title + " has been CREATED. ChangeId = " + change.Id);
                                SendToSlack("File " + change.File.Title + " has been CREATED. ChangeId = " + change.Id);
                                list_of_fileids.Add(change.File.Id);
                                WriteToFile();
                            }
                            else
                            {
                                Console.WriteLine("File not monitored. ChangeId = " + change.Id);
                                SendToSlack("File not monitored. ChangeId = " + change.Id);
                            }
                    });
                }
                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }

        public static void WaitToStop()
        {
            while (Console.ReadKey(true).KeyChar != 's') ;
            stop_sign = true;
        }

        static void Main(string[] args)
        {
            //Authorization
            UserCredential credential;
            using (var filestream = new FileStream("../../client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(filestream).Secrets,
                    new[] { DriveService.Scope.Drive },
                    "user",
                    CancellationToken.None,
                    new FileDataStore("DriveCommandLineSample")).Result;
            }

            //Create the service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Drive API Sample",
            });

            //Test
            //Console.WriteLine(service.Changes.List().Execute().LargestChangeId);
            //Console.ReadLine();

            //Run
            Thread thread = new Thread(new ThreadStart(WaitToStop));
            thread.Start();
            while (!stop_sign)
                Transaction(service);   
        }
    }
}
