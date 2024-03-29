﻿using DepressionTestLib.Data;
using DepressionTestLib.Helpers;
using DepressionTestLib.Models;
using Microsoft.AspNetCore.Mvc;

namespace DepressionTestAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DepressionTestController : Controller
    {
        DepressionTestManager DepressionTestManager;
        public DepressionTestController(DepressionTestManager depressionTestManager)
        {

            this.DepressionTestManager = depressionTestManager;
        }
    
        [HttpPost]
        public ResultTest AddDepressionTest([FromBody] AddDepressionTestRequest addDepressionTestRequest)
        {
            return DepressionTestManager.AddDepressionTest(addDepressionTestRequest);
        }
        [HttpPost]
        public Result EditComment(EditCommentRequest editCommentRequest)
        {
            return DepressionTestManager.EditComment(editCommentRequest);
        }
        [HttpDelete]
        public Result DeleteDepressionTest(string id)
        {
            return DepressionTestManager.DeleteDepressionTest(id);
        }

        [HttpGet]
        public List<DepressionTestHistory> GetDepressionTestByStudent(string userId, DateTime startTestDate, DateTime endTestDate)
        {
            return DepressionTestManager.GetDepressionTestByStudent(userId, startTestDate, endTestDate);
        }
        [HttpGet]
        public List<DepressionTestHistory> GetDepressionTestByTeacher(DateTime startTestDate, DateTime endTestDate)
        {
            return DepressionTestManager.GetDepressionTestByTeacher(startTestDate, endTestDate);
        }
    }
}
