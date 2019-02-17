using System;
using System.IO;
using Com.CloudRail.SI;
using Com.CloudRail.SI.ServiceCode.Commands.CodeRedirect;
using Com.CloudRail.SI.Services;
using static TubeSniper.Core.Domain.Auth.SelectorPayload;

namespace TubeSniper.Core.Services
{
    public class YouTubeApi
    {
        private readonly string clLk;
        private readonly string apiKey;
        private readonly string apiSecret;
        public static YouTube YouTubeClient;

        public YouTubeApi(string clLk, string apiKey, string apiSecret)
        {

        }

        public static bool VerifyApi()
        {
            CloudRail.AppKey = CloudRailKey;
	        var clientSecret = CloudRailSecret;
	        YouTubeClient = new YouTube(new LocalReceiver(8082), "162177487375-21r26tqtreha3pra444av9h2ofeogmg8.apps.googleusercontent.com", clientSecret, "http://localhost:8082/auth", "someState");
	        try
            {
	            if (File.Exists("data/connector.dat"))
	            {
					YouTubeClient.LoadAsString(File.ReadAllText("data/connector.dat"));
					//Console.WriteLine("Loaded CloudRail Settings.");
	            }
	            else
	            {
		            YouTubeClient.Login();
	            }
            }
            catch(Exception exception)
            {
				//Console.WriteLine(exception.Message);
                return false;
            }

	        String result = YouTubeClient.SaveAsString();
			//Console.WriteLine(result);
	        if (!string.IsNullOrEmpty(result))
	        {
		        if (!Directory.Exists("data"))
		        {
			        Directory.CreateDirectory("data");
		        }
				File.WriteAllText("data/connector.dat", result);
	        }

			return true;
        }

        public static bool CheckCommentGhostStatus(string commentId)
        {
//            var request = new AdvancedRequestSpecification("https://www.googleapis.com/youtube/v3/comments?part=snippet&id={COMMENT_ID}&textFormat=html&key={YOUR_API_KEY");
//            request.
//            YouTubeClient.AdvancedRequest()
            return false;
        }

    }
}
