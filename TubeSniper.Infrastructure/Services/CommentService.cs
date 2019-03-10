using System.IO;
using TubeSniper.Application;
using TubeSniper.Application.Youtube;
using TubeSniper.Domain.Common.Helpers;
using TubeSniper.Domain.Services;
using TubeSniper.YouTubeBot.Comments;
using TubeSniper.YouTubeBot.Youtube;

namespace TubeSniper.Infrastructure.Services
{
	public class CommentService : ICommentService
	{
		private readonly ICaptchaService _captchaService;

		public CommentService(ICaptchaService captchaService)
		{
			_captchaService = captchaService;
		}

		public CommentServiceResult PostComment(Domain.Campaigns.CommentJob commentJob)
		{
			var cachePath = CreatePath(commentJob);
			var bot = new YoutubeBot(ConvertCommentJob(commentJob), cachePath);
			bot.CaptchaReceived += (sender, args) =>
			{
				args.Solution = _captchaService.SolveCaptcha(args.ImageUrl);
			};
			bot.Run();
			return null;
		}

		private string CreatePath(Domain.Campaigns.CommentJob commentJob)
		{
			var currentDirectory = Directory.GetCurrentDirectory();
			var proxyString = "";
			if (commentJob.Proxy != null)
			{
				proxyString = commentJob.Proxy.Address.Host + "-" + commentJob.Proxy.Address.Host;
			}

			var hashedPath = commentJob.AccountEntry.Credentials.Username + proxyString;
			hashedPath = hashedPath.CalculateMd5Hash();
			var newPath = Path.Combine(currentDirectory, "cache", hashedPath);

			return newPath;
		}

		private CommentJob ConvertCommentJob(Domain.Campaigns.CommentJob commentJob)
		{
			CommentMethod commentMethod;
			if (commentJob.CommentMethod == Domain.Youtube.CommentMethod.Comment)
			{
				commentMethod = CommentMethod.Comment;
			}
			else
			{
				commentMethod = CommentMethod.Reply;
			}

			return new CommentJob(new YoutubeAccount(new YoutubeCredentials(commentJob.AccountEntry.Credentials.Username.Value, commentJob.AccountEntry.Credentials.Password.Value), commentJob.AccountEntry?.RecoveryEmail.Value), commentJob.Proxy?.ToWebProxy(), new VideoId(commentJob.Video.Id), new Comment(commentJob.Comment.Value), commentMethod);
		}
	}

	public class BotReceiver
	{
		//public event EventHandler<> 
	}
}
