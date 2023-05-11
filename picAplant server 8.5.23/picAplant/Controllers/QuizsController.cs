using Microsoft.AspNetCore.Mvc;
using UniServer.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizsController : ControllerBase
    {
   

        // GET api/<QuizsController>/5
        [HttpGet("/GetQuizByuserId")]
        public IActionResult Get(int userID)
        {
            var res=Quiz.GetQuizesByUserId(userID);
            if (res==null)
            {
                return NotFound("Problam with object type.");
            }
            else
            {
                return Ok(res);
            }
        }

        // POST api/<QuizsController>
        [HttpPost("CreateNewQuiz")]
        public IActionResult Post(string name, int userid,int level)
        {
            //This controller is Create new Quiz and save the data in 3 Tabels.
            //Generate new Question 5 times each creation.

            //After creations of questions and quiz performing insert into:
            //Quiz Table.
            //Question Table.
            //QuizQuestions Table.

            //for validation the controller Return Hoc Object with all the necescery information.

            var obj = Quiz.CreateQuiz(level, name, userid);
            if (obj==null)
            {
                return NotFound("Null Error_Gilad");
            }
           return Ok(obj);
        }

        // POST api/<QuizsController>
        [HttpPost("Finish&SaveQuiz")]
        public IActionResult SubmitQuizUser([FromBody] List<string> UserAnswers,int quizID,int UserId)
        {
            //this controller is submit a quiz of a specific user Check the grade and return it
            //in case of an error will return -1 . 
           
            var data = Quiz.CheckQuiz(UserAnswers, quizID, UserId);
            if (data.Equals(false))
            {
                return NotFound("From QuizController get false From CheckQuiz"+data);
            }
            //if (Convert.ToBoolean(data) == false)
            //{
            //   return NotFound("something goes wrong the grade is -1");
            //}
            //else
            //{
            //    return Ok(data);
            //}
            return Ok(data);

        }
  
    }
}
