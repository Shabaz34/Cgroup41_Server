using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using UniServer.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {



        // GET api/<UsersController>/5
        [HttpGet("/getIdentifiedUser/{id}")]
        public object GetIdentifiedUserId(int id)
        {
            User user = new User();
            return user.readAllIdentificationByUserId(id);

        }

        // GET api/<UsersController>/5
        [HttpGet("/getForumUser/{id}")]
        public object GetForumUserIf(int id)
        {
            User user = new User();
            return user.readAllForumByUserId(id);

        }

        [HttpGet("/isExpert")]
        public bool isExpert(int id)
        {
            User u = new User();
            return u.isExpert(id);
        }

        [HttpGet("/LogIn")]
        public IActionResult Get(string password, string email)
        {
            User user = new User();
            List<object> res = new List<object>();
            //return user coin name rank and type.by the lenght of the list can know if the user exist
            res= user.LogIn(password, email);
            if (res.Count>0)
            {
               return Ok(res);
            }
            else
            {
                return NotFound("user not exist");
            }
        }

        [HttpGet("/GetQuestForExpert")]
        public IActionResult GetQuestForExpert(int ExpertId)
        {
            User u = new User();
            var res = u.GetBadIdeForSpesificUser(ExpertId);
            if (res.Equals(false))
            {
                return NotFound("in User Controller not found id ");
            }
            else
            {
                return Ok(res);
            }
        }

        [HttpGet("/getUserPass")]
        public string GetUserPass(string email)
        {
            User user = new User();
            //return user coin name rank and type.by the lenght of the list can know if the user exist
            return user.GetUserPass(email);
        }

        [HttpGet("/GetAnswerForMe")]
        public IActionResult GetAnswerForMe(int userid)
        {
            User user = new User();
            var res = user.GetAnswerForMe(userid);
            if (res.Equals(false))
            {
                return NotFound("GetAnswerForMe function return false...somthing goes wrong");
            }
            else
            {
                return Ok(res); 
            }
        }

        [HttpPost("/SubmitExpertAnswer")]
        public IActionResult SubmitExpertAnswer(int expertID, int QuestID, string answer) { 
        
           User u = new User();
           bool res = u.SubmitExpertAnswer(expertID, QuestID, answer);
            if (res) {

                return Ok("succsess");
            }
            else
            {
                return NotFound("Error in SubmitExpertAnswer function");
            }
        }

        [HttpPost("/Registrations")]
        public User Post([FromBody] User value)
        {
            return value.InsertUser();
        }

        [HttpPut("/UpdateSettingsUser")]
        public IActionResult Put([FromBody] User value)
        {
            int res=value.UpdateUser();
            if (res==0)
            {
                return NotFound("user not exist from put controller");
            }
            else
            {
                return Ok("update successed");
            }
        }

        [HttpPut("/UpdateRating_Coins")]
        public IActionResult UpdateRating_Coins(int id,int value,bool isAdd,bool AddCoins)
        {
            User us = new User();
            bool res = us.AddSetUserRating(id, value, isAdd, AddCoins);
            if (res)
            {
               return Ok(res);
            }
            else
            {
               return  NotFound("From UserController ----> there is no id or there is no connection." + res);
            }


        }




    }
}
