using DepressionTestLib.DBContext;
using DepressionTestLib.Helpers;
using DepressionTestLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepressionTestLib.Data
{
    public class FeedbackManager
    {
        DepressionTestDBContext db;

        public FeedbackManager(DepressionTestDBContext db ) 
        {
            this.db = db;
        }

        public List<Feedback> GetFeedbackByDateTime(DateTime startGetDate, DateTime endGetDate)//by datetime 
        {
            List<Feedback> viewFeedbackByDateTime = db.Feedback.Where(f => f.LastUpdated >= startGetDate && f.LastUpdated <= endGetDate).ToList();
            return viewFeedbackByDateTime;
        }
        
        public Result AddFeedbackMessage(AddFeedbackMessageRequest addFeedbackMessageRequest)
        {
            Result res = new Result();
            try
            {

                Feedback feedbackMessage = new Feedback();
                feedbackMessage.UserId = addFeedbackMessageRequest.UserId;
                feedbackMessage.Message = addFeedbackMessageRequest.Message;
                feedbackMessage.FeedbackId = Guid.NewGuid().ToString();
                feedbackMessage.LastUpdated = DateTime.UtcNow;
                db.Feedback.Add(feedbackMessage);
                db.SaveChanges();
                res.Message = "Success";
                res.IsSuccess = true;
                return res;
            }
            catch(Exception ex)
            {
                res.Message = "Can't add";
                res.IsSuccess = false;
                return res;

            }
           
            
        }
        public Result DeleteFeedback(string UserId)//delete feedback
        {
            Result res = new Result();

            Feedback deleteRecordFeedback = db.Feedback.Where(f => f.UserId == UserId).FirstOrDefault();
            db.Feedback.Remove(deleteRecordFeedback);
            db.SaveChanges();

            res.Message = "Remove your feedback already.";
            res.IsSuccess = true;
            return res;

        }

        
    }
}
