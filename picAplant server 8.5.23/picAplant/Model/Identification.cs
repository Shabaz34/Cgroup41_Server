using System.Reflection;
using UniServer.Models.DAL;

namespace UniServer.Models
{
    public class Identification
    {
        public int IdentificationId { get; set; }
        public int PhotoId { get; set; }
        public int UserId { get; set; }


        //Guy
        //--------------------------------------------------------------------------------------------------
        // insert Identification and return it.
        //--------------------------------------------------------------------------------------------------
        public Identification InsertIdentification(Identification identify)
        {
            DBservices dbs = new DBservices();
            return dbs.InsertIdentification(identify);
        }
        //Guy
        //--------------------------------------------------------------------------------------------------
        // return Identification by given Identification id 
        //--------------------------------------------------------------------------------------------------
        public Identification readSpecificIdentification(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.readSpecificIdentification(id);
        }

        //Guy
        //--------------------------------------------------------------------------------------------------
        // return list of plants and the probability for each plant.
        //--------------------------------------------------------------------------------------------------
        public object readAllPlantsByIdentificationId(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.readAllPlantsByIdentificationId(id);
        }

        //Guy
        //--------------------------------------------------------------------------------------------------
        // insert to  PlantIdentification table. return num of effect.
        //--------------------------------------------------------------------------------------------------
        public int postPlantIdentification(int plantId, int identificationId, double probability)
        {
            DBservices dbs = new DBservices();
            return dbs.postPlantIdentification(plantId, identificationId, probability);
        }


        //Gilad
        //--------------------------------------------------------------------------------------------------
        // Get the First Identification-plant for experts users.
        //--------------------------------------------------------------------------------------------------

        static public object CheckIDE(int ideId)
        {
            DBservices dbs = new DBservices();
            List<User> expertUsers = new List<User>();
            expertUsers = User.GetExpertUsers();
            int length = expertUsers.Count();
            int minHighExpertIndex = Convert.ToInt32(Math.Floor(Convert.ToDouble(length / 2)));
            Random rand = new Random();
            int highExpertIndex=rand.Next(minHighExpertIndex, length);
            int lowExpertIndex = rand.Next(0,minHighExpertIndex);
            try
            {
                var objReconz= dbs.GetIDEwithPhotoObj(ideId);
                PropertyInfo t = objReconz.GetType().GetProperty("identificationPercentage"); //???
                object itemValue = t.GetValue(objReconz, null);
                double precent = Convert.ToDouble(itemValue);

                t = objReconz.GetType().GetProperty("identificationId");
                object itemValue2 = t.GetValue(objReconz, null);
                int identificationId = Convert.ToInt32(itemValue2);

                t = objReconz.GetType().GetProperty("plantId");
                object itemValue3 = t.GetValue(objReconz, null);
                int plantId = Convert.ToInt32(itemValue3);
                string answer = "null";

                if (precent <= 15 && precent > 7)
                {
                    //low level expert
                    int QuestId=dbs.insertQuestionToExpert(plantId, identificationId);
                    if (QuestId!=-1)
                    {
                        User lowExpertUSER = expertUsers[lowExpertIndex];
                        int numEff = lowExpertUSER.InsertQuestionForExpertUser(answer, QuestId);
                        if (numEff==0)
                        {
                            return false;
                        }
                    }
                    return objReconz;
                }
                else if (precent <= 7)
                {
                    //high level expert
                    int QuestId = dbs.insertQuestionToExpert(plantId, identificationId);
                    User highExpertUSER = expertUsers[highExpertIndex];
                    int numEff = highExpertUSER.InsertQuestionForExpertUser(answer, QuestId);
                    if (numEff == 0)
                    {
                        return false;
                    }
                    return objReconz;
                }
                else
                {
                    return false;

                }


            }
            catch(Exception ex)
            {
                return ex;
            }
            
        }



    }
}
