using System;
using System.Net;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Xml.Linq;
using Facebook;
using Common;
using Microsoft.AspNetCore.SignalR.Client;

namespace FB_Connector
{
    public class Domain
    {

        public string Id { get; set; }

        public string Name { get; set; }
    }
    public class InboxMessage
    {

        public string Id { get; set; }

        public string CreatedTime { get; set; }

        public Domain From { get; set; }

        public Domain[] To { get; set; }

        public string Message { get; set; }
    }
    public class Posts
    {
        public string PostId { get; set; }
        public string PostStory { get; set; }
        public string PostMessage { get; set; }
        public string PostPictureUri { get; set; }
        // public Image PostImage { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

    }

    public class AccessToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }

    }

    public class FBService
    {
        public FBService()
        {
        }

        public List<Posts> getFBPosts()
        {
            //Facebook.FacebookClient myfacebook = new Facebook.FacebookClient();
            string AppId = "446489094504448";
            string AppSecret = "c429543ace9f36bbab030e8c1327ac4a";
            var client = new WebClient();

            // string oauthUrl = string.Format("https://graph.facebook.com/oauth/access_token?type=client_cred&client_id={0}&client_secret={1}", AppId, AppSecret);
            string oauthUrl = string.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&client_secret={1}&grant_type=client_credentials", AppId, AppSecret);
            string accessToken = client.DownloadString(oauthUrl);

            var jsAT = JsonSerializer.Deserialize<AccessToken>(accessToken);

            string _token = "EAAGWFFffwAABO2ZCRZC25cJHMsAO9xWZAKtWMM7lht71DWZBcZAKG18fSETqEwIKwDKfpJKTlVFIATzjydAKognKwCtkn43hbyAHWHEcw5KXhvtsFydRXXhOlTMgGkpUv3mqn4EZAaswtGzVerwdFm2QaWr6dCzZBVo81AaZARqBJ8Gpj6J1KcX9h41utaM88ZCeCcyCMi5Ka6ZBprpGpn7ZB2zyTgZD";
            FacebookClient myfbclient = new FacebookClient(_token);
            string versio = myfbclient.Version;
            var parameters = new Dictionary<string, object>();
            parameters["fields"] = "id,message,picture";
            string myPage = "263738853489128"; // put your page name

            // get posts
            dynamic result = myfbclient.Get(myPage + "/posts", parameters);
            List<Posts> postsList = new List<Posts>();
            int mycount = result.data.Count;
            for (int i = 0; i < result.data.Count; i++)
            {
                Posts posts = new Posts();

                posts.PostId = result.data[i].id;
                posts.PostPictureUri = result.data[i].picture;
                posts.PostMessage = result.data[i].message;

                //var request = WebRequest.Create(posts.PostPictureUri);
                //using (var response = request.GetResponse())
                //using (var stream = response.GetResponseStream())
                //{
                //    posts.PostImage = Bitmap.FromStream(stream);
                //}
                postsList.Add(posts);
            }

            var connection = new HubConnectionBuilder()
            .WithUrl("https://stg.api.ce.fortytwo-ai.com/chatbot") // URL to your SignalR hub
            .Build();

            connection.StartAsync();

            // get conversationays
            int iLoop = 0;
            while (true)
            {
                iLoop++;
                parameters["fields"] = "messages{message,from,created_time,to},subject,unread_count,name,message_count,senders,participants,id";
                dynamic conv = myfbclient.Get(myPage + "/conversations", parameters);

                string _dataJson = Convert.ToString(conv.data);
                //int myConvCount = conv.data.Count;
                //var _data = conv.data.ToEmptyString();
                //string inboxMessages = conv.data.ToSerialize();
                HttpClientRequest httpClientRequest = new HttpClientRequest();
                var _lst = _dataJson.ToDeserialize<List<FbMessage>>();
                foreach (var _group in _lst)
                {
                    ChatbotGroupAddRequest _req = new ChatbotGroupAddRequest
                    {
                        Channel = "Facebook",
                        ClientEmail = _group.senders.data[0].email,
                        ClientId = _group.senders.data[0].id,
                        ClientName = _group.senders.data[0].name,
                        DeviceId = _group.senders.data[0].id
                    };

                    httpClientRequest.PostRequest("https://stg.api.ce.fortytwo-ai.com/c/Chatbot/addGroup", null, _req.ToSerialize(), null);
                    connection.InvokeAsync("AddToGroup", _group.senders.data[0].id);

                    foreach (var _msg in _group.messages.data)
                    {
                        ChatbotSendMessageRequest _reqMsg = new ChatbotSendMessageRequest
                        {
                            DeviceId = _group.senders.data[0].id,
                            Content = _msg.message,
                            MsgType = "MESSAGE",
                            UserId = 0,
                            CreatedAt = _msg.created_time
                        };

                        var _resMsg = httpClientRequest.PostRequest<string>($"https://stg.api.ce.fortytwo-ai.com/c/Chatbot/{_reqMsg.DeviceId}/push", null, _reqMsg.ToSerialize(), null);
                        if (_resMsg.RespMsg == "1")
                            connection.SendAsync("SendMessage", _reqMsg);
                    }
                }
                //for (int i = 0; i < conv.data.Count; i++) // tat ca convs
                //{
                //    InboxMessage inboxMessage = new InboxMessage();

                //    inboxMessage.Id = conv.data[i].id;
                //    inboxMessage.Message = conv.data[i].message;
                //    inboxMessage.CreatedTime = conv.data[i].message;

                Thread.Sleep(5000);
                Console.WriteLine($"loop. {iLoop}");
            }

            //    inboxMessages.Add(inboxMessage);
            //}

            // response 1 conversation
            //var parametersP = new Dictionary<string, object>();
            //parametersP["recipient"] = "{\"id\": \"7868047756563095\"}";
            //parametersP["messaging_type"] = "RESPONSE";
            //parametersP["message"] = "{\"text\": \"Nam add vao\"}";
            //dynamic convP = myfbclient.Post(myPage + "/messages", parametersP);

        }
    }
}

