using System.Reflection;
using UniServer.Models.DAL;

namespace UniServer.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
        public string UserType { get; set; }
        public int UserRating { get; set; }
        public int UserCoins { get; set; }
        public string? UserToken { get; set; }





        //Dor
        //--------------------------------------------------------------------------------------------------
        // registraion user for client
        //--------------------------------------------------------------------------------------------------
        public User InsertUser()
        {
            DBservices dbs = new DBservices();
            return dbs.InsertUser(this);
        }


        //Guy
        //--------------------------------------------------------------------------------------------------
        // update User propertys
        //--------------------------------------------------------------------------------------------------
        public int UpdateUser()
        {
            DBservices dbs = new DBservices();
            int res=dbs.UpdateUser(this);
            return res;
        }



        //Dor
        //--------------------------------------------------------------------------------------------------
        // Login for User
        //--------------------------------------------------------------------------------------------------
        public List<User> LogIn(string password, string email)
        {
            List<User> UserList = new List<User>();
            DBservices dbs = new DBservices();
            UserList = dbs.LogIn(password, email);

            return UserList;

        }

        //Dor
        //--------------------------------------------------------------------------------------------------
        // return all idefication by given user id 
        //--------------------------------------------------------------------------------------------------
        public object readAllIdentificationByUserId(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.readAllIdentificationByUserId(id);
        }



        //Dor
        //--------------------------------------------------------------------------------------------------
        // return all forum that followed by given user id 
        //--------------------------------------------------------------------------------------------------
        public object readAllForumByUserId(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.readAllForumByUserId(id);
        }


        //Dor
        //--------------------------------------------------------------------------------------------------
        // return User Password for "forgot password component" by given user email 
        //--------------------------------------------------------------------------------------------------
        public string GetUserPass(string email)
        {
            DBservices dbs = new DBservices();
            List<User> UserList = new List<User>();
            UserList = dbs.GetUserPass(email);
            if (UserList.Count == 0)
                return "0";
            else
                return UserList[0].UserPassword;
        }



        //Gilad
        //--------------------------------------------------------------------------------------------------
        // can add coins\rate or set rate for given user id  and check if the rating is over 650 opreate the InsertNewExpert()
        //--------------------------------------------------------------------------------------------------
        public bool AddSetUserRating(int id,int value,bool isAdd,bool isCoins)
        {
            DBservices dbs = new DBservices();
            List <User> Experts= new List<User>();
            int res = dbs.SetAddRatingtoUser(id, value, isAdd, isCoins);
            if (res>650)
            {
                bool flag = false;
                Experts = GetExpertUsers();
                foreach (var us in Experts)
                {
                    if (us.UserId==id)
                    {
                        flag = true;
                    }
                }
                if (flag==false)
                {
                    //insert Expert!
                   int num =  dbs.InsertNewExpert(id);
                    if (num>0)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception("Cant insert this user");
                    }
                }
            }
            if (res>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //Gilad
        //--------------------------------------------------------------------------------------------------
        // return all the experts users by order that [0] is the most expert. 
        //--------------------------------------------------------------------------------------------------

        static public List<User> GetExpertUsers()
        {
            DBservices dbs = new DBservices();
            return dbs.GetExpertsUsersbyOrder();
        }


        //Gilad
        // --------------------------------------------------------------------------------------------------
        // insert into QuestionExpertUser_expertUser Table 
        // --------------------------------------------------------------------------------------------------
        public int InsertQuestionForExpertUser(string UserAnswer,int QuestId)
        {
            DBservices dbs = new DBservices();
            return dbs.AskExpertInsert(this.UserId, QuestId, UserAnswer);
        }





        //Gilad
        //--------------------------------------------------------------------------------------------------
        // return list of bad Identification by given userExpertid
        //--------------------------------------------------------------------------------------------------
        public object GetBadIdeForSpesificUser(int expertId)
        {
            try
            {
                DBservices dbs = new DBservices();
                List <object> list= new List<object>();
                list = dbs.GetBadIdeForSpesificExpert(expertId);
                if (list.Count>0)
                {
                    return list;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }

            
        }


        //Gilad
        //--------------------------------------------------------------------------------------------------
        // Submit the answer of the expert if success he get more rating. else return false.
        //--------------------------------------------------------------------------------------------------
        public bool SubmitExpertAnswer(int expertID, int QuestID, string answer)
        {
            DBservices dBservices= new DBservices();
            Random rand = new Random();
            int res = dBservices.SubmitAnswerOfExpert(expertID, QuestID, answer);
            if (res==0)
            {
                return false;
            }
            else
            {
                bool res2=AddSetUserRating(expertID, 15, true, false);
                bool res3 = AddSetUserRating(expertID, 100, true, true);
                if (res2&&res3)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        //Gilad
        //--------------------------------------------------------------------------------------------------
        // return list of answer about bad Identification from expert by given user request id.
        //--------------------------------------------------------------------------------------------------
        public object GetAnswerForMe(int userId)
        {
            DBservices dbs = new DBservices();
            List<object> list= new List<object>();
            list = dbs.GetAnswerFromExpertToUser(userId);
            if (list.Count==0)
            {
                return false;
            }
            else
            {
                return list;
            }
        }


         public bool isExpert(int id)
        {
            List<User> experts = new List<User>();
            experts = GetExpertUsers();
            foreach (User us in experts)
            {
                if (us.UserId== id)
                {
                    return true;
                }
            }
            return false;

        }
    }
}
