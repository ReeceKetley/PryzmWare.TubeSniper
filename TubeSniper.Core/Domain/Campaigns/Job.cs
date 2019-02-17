using System;
using System.Net;
using TubeSniper.Core.Domain.Youtube;
using TubeSniper.Core.Services;

namespace TubeSniper.Core.Domain.Campaigns
{
    public class Job : IJob
    {
        private YoutubeAccount _account;
        private WebProxy _proxy;
	    public event EventHandler<FatalErrorEventArgs> FatalError;
	    public event EventHandler<StatusChangedEventArgs> StatusChanged;
	    public event EventHandler<VideoProcessedEventArgs> VideoProcessed;
		public void Run( YoutubeAccount account, WebProxy proxy, string videoId, CommentRegister comment, bool asReply)
        {
			Console.WriteLine("RUN1");
            _account = account;
			//Console.WriteLine("Proxy: Job.cs"  + proxy.Address.Host);
            _proxy = proxy;
            var bot = new YoutubeBot(account, proxy, videoId, comment.Generate(), true, asReply);
            bot.StatusChanged += Bot_StatusChanged;
            bot.Error += Bot_Error;
            bot.VideoProcessed += Bot_VideoProcessed;
            bot.Run();
        }

        private void Bot_VideoProcessed(object sender, VideoProcessedEventArgs e)
        {
			var comment = new SuccessComment();
	        comment.Proxy = "N/A";
	        if (_proxy != null)
	        {
		        comment.Proxy = _proxy.Address.Host + ":" + _proxy.Address.Port;
	        }

	        comment.Comment = e.Comment;
	        comment.Email = _account.Credentials.Email;
	        var commentThumbnail = e.Meta.GetThumbnailUrl();
			//GeneralHelpers.
	        //comment.Thumbnail = commentThumbnail;

	        //Console.WriteLine("Job: [Comment Posted] :  {0} - {1}", e.Meta.GetTitle(), e.Comment);
	        OnVideoProcessed(e);
        }

        private void Bot_Error(object sender, FatalErrorEventArgs e)
        {
            //Console.WriteLine("Job: [Error Event] : {0}", e.Error);
			OnFatalError(e);
        }

        private void Bot_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            //Console.WriteLine("Job: [Status Event] : {0}", e.Status);
			OnStatusChanged(e);
        }

	    protected virtual void OnFatalError(FatalErrorEventArgs e)
	    {
		    FatalError?.Invoke(this, e);
	    }

	    protected virtual void OnStatusChanged(StatusChangedEventArgs e)
	    {
		    StatusChanged?.Invoke(this, e);
	    }

	    protected virtual void OnVideoProcessed(VideoProcessedEventArgs e)
	    {
		    VideoProcessed?.Invoke(this, e);
	    }
    }
}
