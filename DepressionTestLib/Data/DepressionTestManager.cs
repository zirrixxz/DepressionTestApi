using DepressionTestLib.DBContext;
using DepressionTestLib.Helpers;
using DepressionTestLib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DepressionTestLib.Data
{
    public class DepressionTestManager
    {
        DepressionTestDBContext db;
        public DepressionTestManager(DepressionTestDBContext db)
        {
            this.db = db;
        }

        public string GetAdvice(int level)
             
        {
            if (level < 7)
            {
                return "ไม่มีอาการของโรคซึมเศร้าหรือมีอาการของโรคซึมเศร้าระดับน้อยมาก";
            }
            else if (level == 7 && level <= 12)
            {
                return "มีอาการของโรคซึมเศร้าระดับน้อย ";
            }
            else if (level == 13 && level <= 18)
            {
                return "มีอาการของโรคซึมเศร้าระดับปานกลาง ";
            }
            else if (level >= 19)
            {
                return "มีอาหารของโรคซึมเศร้าระดับรุนแรง และควรปรึกษาแพทย์โดยด่วน";
            }
            else
            {
                return "";
            }
            
        }
     

        public ResultTest AddDepressionTest(AddDepressionTestRequest addDepressionTestRequest )
        {
            ResultTest res = new ResultTest();

            DepressionTestHistory depressionTest = new DepressionTestHistory();
            
            depressionTest.Id = Guid.NewGuid().ToString();
            depressionTest.UserId = addDepressionTestRequest.UserId;
            depressionTest.ScoreResult = addDepressionTestRequest.ScoreResult;
            depressionTest.Comment = null; //รอให้อาจารย์มา comment
            depressionTest.TestDate = DateTime.UtcNow;
            depressionTest.LastUpdated = DateTime.UtcNow;

            //calculate level result            // 1-27 คะแนน
           if (addDepressionTestRequest.ScoreResult >= 19)
            {
                depressionTest.LevelResult = "รุนแรง";
            }
           else if(addDepressionTestRequest.ScoreResult <= 18 && addDepressionTestRequest.ScoreResult >= 13 )
            {
                depressionTest.LevelResult = "ปานกลาง";
            }
           else if(addDepressionTestRequest.ScoreResult <= 12 && addDepressionTestRequest.ScoreResult >= 7)
            {
                depressionTest.LevelResult = "เล็กน้อย";
            }
           else if (addDepressionTestRequest.ScoreResult <=7)
            {
                depressionTest.LevelResult ="ไม่มีอาการ";
            }
           else { }


            
            db.DepressionTestHistory.Add(depressionTest);
            db.SaveChanges(); //execute command 
           
            res.Message = "Add depression test success";
            res.IsSuccess = true;
            res.Level = depressionTest.LevelResult!;
            res.Score = addDepressionTestRequest.ScoreResult;

            //for example 
            //string a = "";  --> empty
            //string a = null; --> null
            //null
            //empty
            
            if(!String.IsNullOrEmpty(addDepressionTestRequest.Feedback))
            {
                //บันทึกลง table feedback
                Feedback feedbackMessage = new Feedback();
                feedbackMessage.UserId = addDepressionTestRequest.UserId;
                feedbackMessage.Message = addDepressionTestRequest.Feedback;
                feedbackMessage.FeedbackId = Guid.NewGuid().ToString();
                feedbackMessage.LastUpdated = DateTime.UtcNow;
                db.Feedback.Add(feedbackMessage);
                db.SaveChanges();
            }  
                
            return res;

        }
        public Result EditComment(EditCommentRequest editCommentRequest) //สำหรับให้อาจารย์มา comment 
        {
            Result res = new Result();
            //linq

            DepressionTestHistory updateRecord = db.DepressionTestHistory.Where(f => f.Id == editCommentRequest.Id).FirstOrDefault();
            updateRecord.Comment = editCommentRequest.Comment;
            db.SaveChanges();

            res.Message = "Edit comment success";
            res.IsSuccess = true;

            return res;
        }
        public Result DeleteDepressionTest(string id)
        {
            Result res =   new Result();

            DepressionTestHistory deleteRecord = db.DepressionTestHistory.Where(f => f.Id == id).FirstOrDefault();
            db.DepressionTestHistory.Remove(deleteRecord);
            db.SaveChanges();

            res.Message = "Remove your Data success!";
            res.IsSuccess = true;

            return res;
        }
        public List<DepressionTestHistory> GetDepressionTestByStudent(string userId,DateTime startTestDate, DateTime endTestDate) 
        {
            List<DepressionTestHistory> viewRocord = db.DepressionTestHistory.Include("User").Where(f => f.UserId == userId && f.TestDate <= startTestDate && f.TestDate >= endTestDate).ToList();
           return viewRocord;

        }
        public List<DepressionTestHistory> GetDepressionTestByTeacher(DateTime startTestDate, DateTime endTestDate)
        {
            List < DepressionTestHistory > viewDateTime = db.DepressionTestHistory.Include("User").Where(f => f.TestDate <= startTestDate && f.TestDate >= endTestDate).ToList();
            return viewDateTime;
        }
    }
}
