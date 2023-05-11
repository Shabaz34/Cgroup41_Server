using UniServer.Models.DAL;

namespace UniServer.Model
{
    public class Plant
    {
        public int PlantId { get; set; }
        public string? PlantName { get; set; }
        public string? PlantScientificName { get; set; }
        public string? PlantFamily { get; set; }
        public string? PlantNumOfPetals { get; set; }
        public string? PlantLeafShape { get; set; }
        public string? PlantLeafMargin { get; set; }
        public string? PlantHabitat { get; set; }
        public string? PlantStemShape { get; set; }
        public string? PlantLifeForm { get; set; }
        public string? PlantBloomingSeason { get; set; }
        public bool PlantMedic { get; set; }
        public string? PlantMoreInfo { get; set; }
        public bool PlantIsEatable { get; set; }
        public bool PlantIsToxic { get; set; }
        public bool PlantIsEndangered { get; set; }
        public bool PlantIsProtected { get; set; }
        public bool PlantIsProvidedHoneydew { get; set; }
        public bool PlantIsAllergenic { get; set; }
        public string? PlantImage { get; set; }



        //Guy
        //--------------------------------------------------------------------------------------------------
        // return all the plants in the DB
        //--------------------------------------------------------------------------------------------------

        public List<Plant> readAllPlant()
        {
            DBservices dbs = new DBservices();
            return dbs.readAllPlant();
        }

        //Guy
        //--------------------------------------------------------------------------------------------------
        // return Plant by given id 
        //--------------------------------------------------------------------------------------------------
        public Plant readSpecificPlant(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.readSpecificPlant(id);
        }


        //Gilad
        //--------------------------------------------------------------------------------------------------
        // internal function return all the plants that will be ask in quizes.
        //--------------------------------------------------------------------------------------------------
        static public List<Plant> GetPlantQuiz()
        {
            DBservices dbs = new DBservices();
           return  dbs.GetPlantforQuiz();
        }
       
        
        
        //Guy
        //--------------------------------------------------------------------------------------------------
        //insert plant from smart api 
        //--------------------------------------------------------------------------------------------------

        public int InsertPlant(string plantScientificName)
        {
            DBservices dbs = new DBservices();
            return dbs.InsertPlant(plantScientificName);
        }


    }
}
