using System;
using System.Net;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Xml.Linq;
using Facebook;
using static System.Net.Mime.MediaTypeNames;

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

            FacebookClient myfbclient = new FacebookClient("EAAGWFFffwAABOZBZBYZCSrZAp6OQQZBwZApyD4IOU0zEvUjPBdTRMdZBEg2qvhFZCewYMmUVZAndRtKpdoFH9IYfopQeMruFn6qTdRh770IlPJ16KFBeflCH1MYQLTG8ZCRvh58ZBOporY0WC2Vl4VjcOi7sYHro2FU4nqLZBIiRsFxWjwtZCPZASI9TBj0Ug96JxnHbwGodrUIoxJScG98Lq7z8DGd83u");
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

            // get conversationays
            parameters["fields"] = "messages{ message},subject,unread_count,name";
            dynamic conv = myfbclient.Get(myPage + "/conversations", parameters);

            int myConvCount = conv.data.Count;
            List<InboxMessage> inboxMessages = new List<InboxMessage>();
            for (int i = 0; i < conv.data.Count; i++) // tat ca convs
            {
                InboxMessage inboxMessage = new InboxMessage();

                inboxMessage.Id = conv.data[i].id;
                inboxMessage.Message = conv.data[i].message;
                inboxMessage.CreatedTime = conv.data[i].message;


                inboxMessages.Add(inboxMessage);
            }

            // response 1 conversation
            var parametersP = new Dictionary<string, object>();
            parametersP["recipient"] = "{\"id\": \"7868047756563095\"}";
            parametersP["messaging_type"] = "RESPONSE";
            parametersP["message"] = "{\"text\": \"Nam add vao\"}";
            dynamic convP = myfbclient.Post(myPage + "/messages", parametersP);

            return postsList;

        }
    }
}

