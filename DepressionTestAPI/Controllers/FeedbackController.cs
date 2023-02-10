using DepressionTestLib.Data;
using DepressionTestLib.Helpers;
using DepressionTestLib.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DepressionTestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FeedbackController : Controller
    {
        FeedbackManager FeedbackManager;

        public FeedbackController(FeedbackManager feedbackManager)
        {
            this.FeedbackManager = feedbackManager;
        }
        [HttpGet]
        public List<Feedback> GetFeedbackByDateTime(DateTime startGetDate, DateTime endGetDate)
        {
            return FeedbackManager.GetFeedbackByDateTime(startGetDate, endGetDate);
        }
        [HttpPost]
        public Result AddFeedback(AddFeedbackMessageRequest addFeedbackRequest)
        {
            return FeedbackManager.AddFeedbackMessage(addFeedbackRequest);
        }

        [HttpDelete]
        public Result DeleteFeedback(string UserId)
        {
            return FeedbackManager.DeleteFeedback(UserId);
        }
    }
}
