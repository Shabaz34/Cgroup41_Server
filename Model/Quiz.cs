
using System.Reflection;
using UniServer.Models.DAL;

namespace UniServer.Model
{
    public class Quiz
    {
        //level:1 -> Easy
        // level:2 -> Hard



        //Feilds//
        string name;
        int quizId;

        //Constractor//
        public Quiz(string name)
        {
            Name = name;
        }

        //properties//
        public string Name { get => name; set => name = value; }
        public int QuizId { get => quizId; set => quizId = value; }

        //Methods//

        //Gilad
        //--------------------------------------------------------------------------------------------------
        //Create a new quiz and generate new questions take level 1 or 2 name and userid.
        //--------------------------------------------------------------------------------------------------
        static public object CreateQuiz(int level,string name,int userId)
        {
            DBservices dBservice = new DBservices();
            List<Question> Quiz_questions = new List<Question>();
            List <int> QuestinIDs= new List<int>();
            Random rand= new Random();
            bool flagMulti;

            //Generate 5 Questions 
            for (int i = 0; i < 5; i++)
            {
                if (rand.NextDouble()>0.2)
                {
                    flagMulti = true;
                }
                else
                {
                    flagMulti = false;
                }
                
                    
                    if (level == 1)
                    {
                        //Easy -> 3 easy + 2 hard

                        if (i < 3)
                        {
                            Quiz_questions.Add(Question.GenerateQuest(1, flagMulti));
                        }
                        else
                        {
                            Quiz_questions.Add(Question.GenerateQuest(2, flagMulti));
                        }
                       
                    }
                    else
                    {
                        //Hard -> 2 easy + 3 hard
                        if (i < 2)
                        {
                            Quiz_questions.Add(Question.GenerateQuest(1, flagMulti));
                        }
                        else
                        {
                            Quiz_questions.Add(Question.GenerateQuest(2, flagMulti));
                        }
                    }

                
            }

            //List<object> BonusQuestListData= new List<object>();
            //BonusQuestListData = dBservice.GetListOfBonusQuest();
            var QuestImgList = dBservice.GetListOfBonusQuest();
            if (QuestImgList.Count>4)
            {
                int RandIndex=rand.Next(QuestImgList.Count);

                PropertyInfo t = QuestImgList[RandIndex].GetType().GetProperty("photoUri");
                object itemValue = t.GetValue(QuestImgList[RandIndex], null);
                string QuestionBody = (itemValue).ToString();
                
                t = QuestImgList[RandIndex].GetType().GetProperty("plantScientificName");
                object itemValue2 = t.GetValue(QuestImgList[RandIndex], null);
                string correctAnswer = (itemValue2).ToString();

                t = QuestImgList[RandIndex].GetType().GetProperty("photoId");
                object itemValue3 = t.GetValue(QuestImgList[RandIndex], null);
                int photoId = Convert.ToInt32(itemValue3);

                List<string> DistrictUniqAnswerList = new List<string>();
                foreach (var item in QuestImgList)
                {
                    t = item.GetType().GetProperty("plantScientificName");
                    object itemValue4 = t.GetValue(item, null);
                    string district = (itemValue4).ToString();

                    if (district!=correctAnswer && district!=null && DistrictUniqAnswerList.Contains(district)==false)
                    {
                        DistrictUniqAnswerList.Add(district);
                    }
                }
                if (correctAnswer!=null && QuestionBody!=null && DistrictUniqAnswerList.Count>=3)
                {
                    Question BonusQuest = new Question(QuestionBody, correctAnswer, DistrictUniqAnswerList[0], DistrictUniqAnswerList[1], DistrictUniqAnswerList[2], "רב-ברירה", 2, photoId);
                    Quiz_questions.Add(BonusQuest);
                }

                


                //t = stam[RandIndex].GetType().GetProperty("plantId");
                //object itemValue4 = t.GetValue(stam, null);
                //int plantId = Convert.ToInt32(itemValue3);





            }
            

            //insert each Question to data base and get the id from it.
            foreach (var q in Quiz_questions)
            {
                //Clean the hebrew strings
                q.QuestionBody = q.QuestionBody.Replace("\r", "");
                q.QuestionBody = q.QuestionBody.Replace("\n", "");
                q.QuestionBody = q.QuestionBody.Replace("\t", "");
                q.QuestionBody = q.QuestionBody.Replace("\"", "");



                q.QuestionCurrectAnswer= q.QuestionCurrectAnswer.Replace("\r", "");
                q.QuestionCurrectAnswer= q.QuestionCurrectAnswer.Replace("\n", "");
                q.QuestionCurrectAnswer= q.QuestionCurrectAnswer.Replace("\t", "");
             
                q.QuestionDistractingA = q.QuestionDistractingA.Replace("\r", "");
                q.QuestionDistractingA = q.QuestionDistractingA.Replace("\n", "");
                q.QuestionDistractingA = q.QuestionDistractingA.Replace("\t", "");
          
                q.QuestionDistractingB = q.QuestionDistractingB.Replace("\r", "");
                q.QuestionDistractingB = q.QuestionDistractingB.Replace("\n", "");
                q.QuestionDistractingB = q.QuestionDistractingB.Replace("\t", "");
              
                q.QuestionDistractingC = q.QuestionDistractingC.Replace("\r", "");
                q.QuestionDistractingC = q.QuestionDistractingC.Replace("\n", "");
                q.QuestionDistractingC = q.QuestionDistractingC.Replace("\t", "");
               

                QuestinIDs.Add(q.insertQuestion());
            }
            if (QuestinIDs.Contains(-1) || QuestinIDs.Contains(0))
            {
                return false;
            }

            //Insert Quiz to data base (only name) and get the Id
            int QuizId = dBservice.InsertQuiz(name);
            if (QuizId == 0 | QuizId==-1)
            {
                return false;

            }
            foreach (var questID in QuestinIDs)
            {
                int numEff = dBservice.InsertQuizandQuestion(QuizId, questID);
                if (numEff<1)
                {
                    return false;
                }
            }
            var obj=new { QuizId=QuizId, questIDs= QuestinIDs, QuestList= Quiz_questions};
            



            return obj;

        }

        //Gilad
        //--------------------------------------------------------------------------------------------------
        //Check the Quiz by the quizId find the question and check if the correct answers
        //and Insert to Quiz User Table the data.
        //id somthing goes wrong will return -1 else will returnthe grade (to controller)
        //--------------------------------------------------------------------------------------------------
        static public object CheckQuiz(List<string> UserAnswers, int quizID, int UserId)
        {
            List<Question> QuizQuestions = new List<Question>();
            DBservices db = new DBservices();
            QuizQuestions = db.GetQuestionByQuizID(quizID);
            double score = 0;
            List<object> listToReturn = new List<object>();
            int rateToAdd = 0;
            int moneyToAdd = 0;


            //must be in the same index//
            for (int i = 0; i < QuizQuestions.Count; i++)
            {
                var obj = new
                {
                    QuestId = QuizQuestions[i].QuestionId,
                    indexQuest = i,
                    UserAnswer = UserAnswers[i],
                    CorrectAnswer = QuizQuestions[i].QuestionCurrectAnswer,
                };
                if (QuizQuestions[i].QuestionCurrectAnswer == UserAnswers[i])
                {
                    score++;
                    if (QuizQuestions[i].QuestionDifficulty==1)
                    {
                        //easy case
                        rateToAdd += 2;


                    }
                    else
                    {
                        //hard case
                        rateToAdd += 3;

                    }
                }
                listToReturn.Add(obj);
            }
            score = ((score / QuizQuestions.Count) * 100);
            score = Math.Round(score);
            int grade = (int)score;
            int res=db.SetAddRatingtoUser(UserId, rateToAdd, true, false);

            moneyToAdd = rateToAdd * 10;
            res=db.SetAddRatingtoUser(UserId, moneyToAdd, true, true);



            listToReturn.Add(grade);
            listToReturn.Add(moneyToAdd);
            int NumEff = db.SubmitQuiz(UserId, quizID, grade);
            if (NumEff<1)
            {
                return false;
            }
            else
            {
                return listToReturn;
            }




        }


        //Gilad
        //--------------------------------------------------------------------------------------------------
        //return all the quiz by given user id
        //--------------------------------------------------------------------------------------------------
        static public object GetQuizesByUserId(int userID)
        {
            DBservices dBservice = new DBservices();
            return dBservice.readAllQuizesByUserID(userID);
        }

     

    }
}
