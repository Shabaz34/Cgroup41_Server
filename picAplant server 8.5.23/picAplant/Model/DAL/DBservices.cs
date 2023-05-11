using System.Data.SqlClient;
using System.Data;
using UniServer.Model;
using System.Diagnostics;
using System.Collections.Generic;

namespace UniServer.Models.DAL
{

    public class DBservices
    {

        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            string cStr = configuration.GetConnectionString("myProjDB");
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        //Dor
        // --------------------------------------------------------------------------------------------------
        // This method reads a specific user based on his id number
        // --------------------------------------------------------------------------------------------------
        public User readUserById(int id)
        {
            SqlConnection con;
            SqlCommand cmd;
            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithSPReadSpecificId("spGetUserInfoById", con, id);             // create the command


            SqlDataReader dataReader2 = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            try
            {

                User user = new User();

                while (dataReader2.Read())
                {
                    user.UserId = Convert.ToInt32(dataReader2["userId"]);
                    user.UserCoins = Convert.ToInt32(dataReader2["userCoins"]);
                    user.UserRating = Convert.ToInt32(dataReader2["userRating"]);
                    user.UserEmail = dataReader2["userEmail"].ToString();
                    user.UserName = dataReader2["userName"].ToString();
                    user.UserType = dataReader2["userType"].ToString();
                    user.UserPassword = "";
                    user.UserToken = dataReader2["userToken"].ToString();

                }

                return user;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //Dor
        //--------------------------------------------------------------------------------------------------
        // This method update a user 
        //--------------------------------------------------------------------------------------------------
        public int UpdateUser(User user)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            //String cStr = BuildUpdateCommand(student);      // helper method to build the insert string

            cmd = CreateCommandWithStoredProcedureEditAndInsertUser("spUpdateUser", con, user);             // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //Guy
        //---------------------------------------------------------------------------------
        // This method inserts identification
        //---------------------------------------------------------------------------------
        public Identification InsertIdentification(Identification identify)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithSPInsertIdentification("spInsertIdentification", con, identify);             // create the command


            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                int id = -1;

                while (dataReader.Read())
                {
                    id = Convert.ToInt32(dataReader["id"]);
                }
                return readSpecificIdentification(id);

            }

            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //Guy
        //---------------------------------------------------------------------------------
        // This method inserts photo
        //---------------------------------------------------------------------------------
        public Photo InsertPhoto(Photo pic)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            //String cStr = BuildUpdateCommand(student);      // helper method to build the insert string

            cmd = CreateCommandWithSPInsertPhoto("spInsertPhoto", con, pic);             // create the command



            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                int id = -1;

                while (dataReader.Read())
                {
                    id = Convert.ToInt32(dataReader["id"]);
                }
                return readPhotoByPhotoId(id);

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //Dor
        //---------------------------------------------------------------------------------
        // This method inserts User
        //---------------------------------------------------------------------------------
        public User InsertUser(User user)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }


            cmd = CreateCommandWithStoredProcedureEditAndInsertUser("spInsertUser", con, user);             // create the command

            int id = -1;
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            try
            {

                while (dataReader.Read())
                {


                    id = Convert.ToInt32(dataReader["id"]);

                }
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

            return readUserById(id);

        }

        //Guy
        // --------------------------------------------------------------------------------------------------
        // This method post to plantIdentification table
        // --------------------------------------------------------------------------------------------------
        public int postPlantIdentification(int plantId, int identificationId, double probability)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithSPInsertPlantIdentification("spInsertPlantIdentification", con, plantId, identificationId, probability);             // create the command


            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }

            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //Guy
        //---------------------------------------------------------------------------------
        // This method inserts plant and return his ID
        //---------------------------------------------------------------------------------
        public int InsertPlant(string plantScientificName)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithSPInsertPlant("spInsertPlant", con, plantScientificName);             // create the command


            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                int id = -1;

                while (dataReader.Read())
                {
                    id = Convert.ToInt32(dataReader["id"]);
                }

                return id;

            }

            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //Guy
        // --------------------------------------------------------------------------------------------------
        // This method reads all the plants by identification id
        // --------------------------------------------------------------------------------------------------
        public object readAllPlantsByIdentificationId(int id)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithSPReadSpecificId("spReadAllPlantsBasedOnIdentificationId", con, id);             // create the command

            List<object> list = new List<object>();

            try
            {

                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    list.Add(new
                    {
                        PlantId = (int)dataReader["plantId"],
                        PlantName = (string)dataReader["plantName"],
                        PlantScientificName = (string)dataReader["plantScientificName"],
                        PlantFamily = (string)dataReader["plantFamily"],
                        PlantNumOfPetals = (string)dataReader["plantNumOfPetals"],
                        PlantLeafShape = (string)dataReader["plantLeafShape"],
                        PlantLeafMargin = (string)dataReader["PlantLeafMargin"],
                        PlantHabitat = (string)dataReader["plantHabitat"],
                        PlantStemShape = (string)dataReader["plantStemShape"],
                        PlantLifeForm = (string)dataReader["plantLifeForm"],
                        PlantBloomingSeason = (string)dataReader["plantBloomingSeason"],
                        PlantMedic = (bool)dataReader["PlantMedic"],
                        PlantMoreInfo = (string)dataReader["plantMoreInfo"],
                        PlantIsEatable = (bool)dataReader["plantIsEatable"],
                        PlantIsToxic = (bool)dataReader["plantIsToxic"],
                        PlantIsEndangered = (bool)dataReader["plantIsEndangered"],
                        PlantIsProtected = (bool)dataReader["plantIsProtected"],
                        PlantIsProvidedHoneydew = (bool)dataReader["plantIsProvidedHoneydew"],
                        IdentificationPercentage = (double)dataReader["IdentificationPercentage"],
                        PlantIsAllergenic = (bool)dataReader["PlantIsAllergenic"],




                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //Dor
        // --------------------------------------------------------------------------------------------------
        // This method reads all the identification by user id
        // --------------------------------------------------------------------------------------------------
        public object readAllIdentificationByUserId(int id)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithSPReadSpecificId("spReadUseridentification", con, id);             // create the command

            List<object> list = new List<object>();

            try
            {

                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    list.Add(new
                    {
                        IdentificationPercentage = Convert.ToDouble(dataReader["identificationPercentage"]),
                        PlantName = dataReader["plantName"].ToString(),
                        PlantScientificName = dataReader["plantScientificName"].ToString(),
                        PhotoUri = dataReader["photoUri"].ToString(),
                        IdentificationId = Convert.ToInt32(dataReader["identificationId"]),
                        TimeStamp = (dataReader["PhotoTimestamp"]).ToString(),
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //Dor
        // --------------------------------------------------------------------------------------------------
        // This method reads all the Forum By User Id
        // --------------------------------------------------------------------------------------------------
        public object readAllForumByUserId(int id)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithSPReadSpecificId("ReadSocialFormByUserId", con, id);             // create the command

            List<object> list = new List<object>();

            try
            {

                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    list.Add(new
                    {

                        NumberOfParticipant = Convert.ToInt32(dataReader["numberOfUsers"]),
                        Title = dataReader["socialForumName"].ToString(),
                        Description = dataReader["socialForumDiscription"].ToString(),

                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //Guy
        // --------------------------------------------------------------------------------------------------
        // This method reads a specific identification 
        // --------------------------------------------------------------------------------------------------
        public Identification readSpecificIdentification(int id)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithSPReadSpecificId("spReadASpecificIdentification", con, id);             // create the command


            try
            {


                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                Identification identify = new Identification();

                while (dataReader.Read())
                {
                    identify.IdentificationId = (int)dataReader["IdentificationId"];
                    identify.UserId = (int)dataReader["UserId"];
                    identify.PhotoId = (int)dataReader["PhotoId"];

                }

                return identify;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //Guy
        // --------------------------------------------------------------------------------------------------
        // This method reads a specific identification 
        // --------------------------------------------------------------------------------------------------
        public Photo readPhotoByPhotoId(int photoId)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithSPReadSpecificId("spReadPhotosByPhotoId", con, photoId);             // create the command


            Photo pic = new Photo();

            try
            {


                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    pic.PhotoUri = (string)dataReader["PhotoUri"];
                    pic.PhotoId = (int)dataReader["PhotoId"];
                    pic.PhotoTimestamp = (dataReader["PhotoTimestamp"]).ToString();

                    pic.PostId = (dataReader["PostId"] != DBNull.Value) ? (int)dataReader["PostId"] : null;
                    pic.UserId = (dataReader["UserId"] != DBNull.Value) ? (int)dataReader["UserId"] : null;
                    pic.Latitude = (dataReader["Latitude"] != DBNull.Value) ? (double)dataReader["Latitude"] : null;
                    pic.Longitude = (dataReader["Longitude"] != DBNull.Value) ? (double)dataReader["Longitude"] : null;

                }

                return pic;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //Dor
        //--------------------------------------------------------------------------------------------------
        // This method Read Forms by user id
        //--------------------------------------------------------------------------------------------------
        public List<object> LogIn(string password, string email)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            cmd = CreateCommandWithStoredProcedureReadUsers("spLogIn", con, password, email);


            List<object> listUser = new List<object>();

            try
            {


                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {

                    listUser.Add(new
                    {

                        UserId = Convert.ToInt32(dataReader["userId"]),
                    UserCoins = Convert.ToInt32(dataReader["userCoins"]),
                    UserRating = Convert.ToInt32(dataReader["userRating"]),
                    UserEmail = dataReader["userEmail"].ToString(),
                    UserName = dataReader["userName"].ToString(),
                    UserType = dataReader["userType"].ToString(),
                    UserPassword = "",
                    UserToken = dataReader["userToken"].ToString(),
                    photoURI = dataReader["photoURI"].ToString(),

                    });


                }

                return listUser;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //Guy
        // --------------------------------------------------------------------------------------------------
        // This method reads all the plants Guy
        // --------------------------------------------------------------------------------------------------
        public List<Plant> readAllPlant()
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithSPRead("spReadPlants", con);             // create the command


            List<Plant> list = new List<Plant>();

            try
            {

                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {

                    Plant plant = new Plant();
                    plant.PlantStemShape = (dataReader["PlantStemShape"] != DBNull.Value) ? (string)dataReader["PlantStemShape"] : null;
                    plant.PlantId = (int)dataReader["plantId"];
                    plant.PlantName = (dataReader["PlantName"] != DBNull.Value) ? (string)dataReader["PlantName"] : null;
                    plant.PlantScientificName = (string)dataReader["plantScientificName"];
                    plant.PlantIsEatable = (dataReader["PlantIsEatable"] != DBNull.Value) ? (bool)dataReader["PlantIsEatable"] : false;
                    plant.PlantIsToxic = (dataReader["PlantIsToxic"] != DBNull.Value) ? (bool)dataReader["PlantIsToxic"] : false;
                    plant.PlantIsEndangered = (dataReader["PlantIsEndangered"] != DBNull.Value) ? (bool)dataReader["PlantIsEndangered"] : false;
                    plant.PlantIsProtected = (dataReader["PlantIsProtected"] != DBNull.Value) ? (bool)dataReader["PlantIsProtected"] : false;
                    plant.PlantIsProvidedHoneydew = (dataReader["PlantIsProvidedHoneydew"] != DBNull.Value) ? (bool)dataReader["PlantIsProvidedHoneydew"] : false;
                    plant.PlantIsAllergenic = (dataReader["PlantIsAllergenic"] != DBNull.Value) ? (bool)dataReader["PlantIsAllergenic"] : false;
                    plant.PlantMedic = (dataReader["PlantMedic"] != DBNull.Value) ? (bool)dataReader["PlantMedic"] : false;
                    plant.PlantFamily = (dataReader["PlantFamily"] != DBNull.Value) ? (string)dataReader["PlantFamily"] : null;
                    plant.PlantNumOfPetals = (dataReader["PlantNumOfPetals"] != DBNull.Value) ? (string)dataReader["PlantNumOfPetals"] : null;
                    plant.PlantLeafShape = (dataReader["PlantLeafShape"] != DBNull.Value) ? (string)dataReader["PlantLeafShape"] : null;
                    plant.PlantLeafMargin = (dataReader["PlantLeafMargin"] != DBNull.Value) ? (string)dataReader["PlantLeafMargin"] : null;
                    plant.PlantHabitat = (dataReader["PlantHabitat"] != DBNull.Value) ? (string)dataReader["PlantHabitat"] : null;
                    plant.PlantLifeForm = (dataReader["PlantLifeForm"] != DBNull.Value) ? (string)dataReader["PlantLifeForm"] : null;
                    plant.PlantBloomingSeason = (dataReader["PlantBloomingSeason"] != DBNull.Value) ? (string)dataReader["PlantBloomingSeason"] : null;
                    plant.PlantMoreInfo = (dataReader["PlantMoreInfo"] != DBNull.Value) ? (string)dataReader["PlantMoreInfo"] : null;
                    plant.PlantImage = (dataReader["PlantImage"] != DBNull.Value) ? (string)dataReader["PlantImage"] : null; //NEW ROW


                    list.Add(plant);
                }

                return list;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //Gilad
        // --------------------------------------------------------------------------------------------------
        // This method return all the plants with hebrew propertys.
        // --------------------------------------------------------------------------------------------------
        public List<Plant> GetPlantforQuiz()
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithSPRead("spGetValidQuizPlant", con);             // create the command


            List<Plant> list = new List<Plant>();

            try
            {

                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {

                    Plant plant = new Plant();
                    plant.PlantId = (int)dataReader["plantId"];
                    plant.PlantName = (string)dataReader["plantName"];
                    plant.PlantScientificName = (string)dataReader["plantScientificName"];
                    plant.PlantFamily = (string)dataReader["plantFamily"];
                    plant.PlantNumOfPetals = (string)dataReader["plantNumOfPetals"];
                    plant.PlantLeafShape = (string)dataReader["plantLeafShape"];
                    plant.PlantLeafMargin = (string)dataReader["PlantLeafMargin"];
                    plant.PlantHabitat = (string)dataReader["plantHabitat"];
                    plant.PlantStemShape = (string)dataReader["plantStemShape"];
                    plant.PlantLifeForm = (string)dataReader["plantLifeForm"];
                    plant.PlantBloomingSeason = (string)dataReader["plantBloomingSeason"];
                    plant.PlantMedic = (bool)dataReader["plantMedic"];
                    plant.PlantMoreInfo = (string)dataReader["plantMoreInfo"];
                    plant.PlantIsEatable = (bool)dataReader["plantIsEatable"];
                    plant.PlantIsToxic = (bool)dataReader["plantIsToxic"];
                    plant.PlantIsEndangered = (bool)dataReader["plantIsEndangered"];
                    plant.PlantIsProtected = (bool)dataReader["plantIsProtected"];
                    plant.PlantIsAllergenic = (bool)dataReader["plantIsAllergenic"];
                    plant.PlantIsProvidedHoneydew = (bool)dataReader["plantIsProvidedHoneydew"];
                    plant.PlantImage = (dataReader["PlantImage"] != DBNull.Value) ? (string)dataReader["PlantImage"] : null; //NEW ROW


                    list.Add(plant);
                }

                return list;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //Guy
        // --------------------------------------------------------------------------------------------------
        // This method reads a specific plant Guy
        // --------------------------------------------------------------------------------------------------
        public Plant readSpecificPlant(int id)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }


            cmd = CreateCommandWithSPReadSpecificId("spReadASpecificPlant", con, id);             // create the command


            try
            {


                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                Plant plant = new Plant();

                while (dataReader.Read())
                {

                    plant.PlantStemShape = (dataReader["PlantStemShape"] != DBNull.Value) ? (string)dataReader["PlantStemShape"] : null;
                    plant.PlantId = (int)dataReader["plantId"];
                    plant.PlantName = (dataReader["PlantName"] != DBNull.Value) ? (string)dataReader["PlantName"] : null;
                    plant.PlantScientificName = (string)dataReader["plantScientificName"];
                    plant.PlantIsEatable = (dataReader["PlantIsEatable"] != DBNull.Value) ? (bool)dataReader["PlantIsEatable"] : false;
                    plant.PlantIsToxic = (dataReader["PlantIsToxic"] != DBNull.Value) ? (bool)dataReader["PlantIsToxic"] : false;
                    plant.PlantIsEndangered = (dataReader["PlantIsEndangered"] != DBNull.Value) ? (bool)dataReader["PlantIsEndangered"] : false;
                    plant.PlantIsProtected = (dataReader["PlantIsProtected"] != DBNull.Value) ? (bool)dataReader["PlantIsProtected"] : false;
                    plant.PlantIsProvidedHoneydew = (dataReader["PlantIsProvidedHoneydew"] != DBNull.Value) ? (bool)dataReader["PlantIsProvidedHoneydew"] : false;
                    plant.PlantIsAllergenic = (dataReader["PlantIsAllergenic"] != DBNull.Value) ? (bool)dataReader["PlantIsAllergenic"] : false;
                    plant.PlantMedic = (dataReader["PlantMedic"] != DBNull.Value) ? (bool)dataReader["PlantMedic"] : false;
                    plant.PlantFamily = (dataReader["PlantFamily"] != DBNull.Value) ? (string)dataReader["PlantFamily"] : null;
                    plant.PlantNumOfPetals = (dataReader["PlantNumOfPetals"] != DBNull.Value) ? (string)dataReader["PlantNumOfPetals"] : null;
                    plant.PlantLeafShape = (dataReader["PlantLeafShape"] != DBNull.Value) ? (string)dataReader["PlantLeafShape"] : null;
                    plant.PlantLeafMargin = (dataReader["PlantLeafMargin"] != DBNull.Value) ? (string)dataReader["PlantLeafMargin"] : null;
                    plant.PlantHabitat = (dataReader["PlantHabitat"] != DBNull.Value) ? (string)dataReader["PlantHabitat"] : null;
                    plant.PlantLifeForm = (dataReader["PlantLifeForm"] != DBNull.Value) ? (string)dataReader["PlantLifeForm"] : null;
                    plant.PlantBloomingSeason = (dataReader["PlantBloomingSeason"] != DBNull.Value) ? (string)dataReader["PlantBloomingSeason"] : null;
                    plant.PlantMoreInfo = (dataReader["PlantMoreInfo"] != DBNull.Value) ? (string)dataReader["PlantMoreInfo"] : null;
                    plant.PlantImage = (dataReader["PlantImage"] != DBNull.Value) ? (string)dataReader["PlantImage"] : null; //NEW ROW


                }

                return plant;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //Gilad
        // --------------------------------------------------------------------------------------------------
        // This method reads all Quizes of specific user by UserID
        // --------------------------------------------------------------------------------------------------
        public object readAllQuizesByUserID(int userID)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }


            cmd = CreateCommandWithSPReadSpecificId("spGetQuizsByUserID", con, userID); // create the command

            List<object> list = new List<object>();
            try
            {


                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);



                while (dataReader.Read())
                {
                    var Res = new
                    {
                        quizId = Convert.ToInt32(dataReader["quizId"]),
                        useriD = (int)(dataReader["userId"]),
                        quizName = dataReader["quizName"].ToString(),
                        grade = Convert.ToInt32(dataReader["grade"]),

                    };
                    list.Add(Res);

                }

                return list;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //Gilad
        // --------------------------------------------------------------------------------------------------
        // This method reads all Quizes of specific user by UserID
        // --------------------------------------------------------------------------------------------------
        public object GetIDEwithPhotoObj(int ideID)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }


            cmd = CreateCommandWithSPReadSpecificId("sp_GetIdentificationObjectWithPhoto", con, ideID); // create the command

            List<object> list = new List<object>();
            try
            {


                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);



                while (dataReader.Read())
                {
                    var Res = new
                    {
                        identificationId = Convert.ToInt32(dataReader["identificationId"]),
                        identificationPercentage = (double)(dataReader["identificationPercentage"]),
                        photoUri = dataReader["photoUri"].ToString(),
                        requestUserId = Convert.ToInt32(dataReader["userId"]),
                        photoId = Convert.ToInt32(dataReader["photoId"]),
                        plantId = Convert.ToInt32(dataReader["plantId"]),


                    };
                    list.Add(Res);

                }

                return list[0];
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //Gilad
        // --------------------------------------------------------------------------------------------------
        // This method Insert new question and return id
        // --------------------------------------------------------------------------------------------------
        public int InsertQuestion(Question quest)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            // helper method to build the insert string

            cmd = CreateCommandSP_insertQuestion("spInsertQuestion", con, quest); // create the command

            int id = -1;
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {


                    id = Convert.ToInt32(dataReader["id"]);

                }
                return id;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }



        }

        //Gilad
        // --------------------------------------------------------------------------------------------------
        // This method insert to connecting table quiz-question
        // --------------------------------------------------------------------------------------------------
        public int InsertQuizandQuestion(int idquiz, int idquest)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            // helper method to build the insert string

            cmd = CreateCommandSP_insertQuestionandQuiz("spInsertQuizQuestion", con, idquiz, idquest); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //Gilad
        // --------------------------------------------------------------------------------------------------
        // This method insert to QuestionForExperts Table return 1 if success
        // --------------------------------------------------------------------------------------------------
        public int insertQuestionToExpert(int plantid, int ideId)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            // helper method to build the insert string

            cmd = CreateCommandWithSPInsertQuestExpert("spInsertQuestionToExpert", con, plantid, ideId); // create the command

            int id = -1;
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {


                    id = Convert.ToInt32(dataReader["id"]);

                }
                return id;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //Gilad
        // --------------------------------------------------------------------------------------------------
        // This method insert new quiz and return id
        // --------------------------------------------------------------------------------------------------
        public int InsertQuiz(string name)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            // helper method to build the insert string

            cmd = CreateCommandSP_insertQuiz("spInsertQuiz", con, name); // create the command


            int id = -1;
            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {


                    id = Convert.ToInt32(dataReader["id"]);

                }
                return id;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //Gilad
        // --------------------------------------------------------------------------------------------------
        // after complete the quiz user will submit his answers this method insert to user quiz the data.
        // --------------------------------------------------------------------------------------------------
        public int SubmitQuiz(int userid, int quizid, int grade)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            // helper method to build the insert string

            cmd = Create_SP_SubmitQuiz("spSubmitQuiztoQuizUser", con, quizid, userid, grade); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //Gilad
        // --------------------------------------------------------------------------------------------------
        // insert into QuestionExpertUser_expertUser Table 
        // --------------------------------------------------------------------------------------------------
        public int AskExpertInsert(int userid, int QuestId, string Answer)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            // helper method to build the insert string

            cmd = CreateCommandWithSPInsertQuestWithExpertUser("spInsertToQuestExpertsUser", con, Answer, userid, QuestId); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }



        //Gilad
        // --------------------------------------------------------------------------------------------------
        // insert into Expert User Table Table 
        // --------------------------------------------------------------------------------------------------
        public int InsertNewExpert(int userid)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            // helper method to build the insert string

            cmd = CreateCommandWithSPReadSpecificId("spInsertToExpertUser", con, userid); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //Gilad
        // --------------------------------------------------------------------------------------------------
        // This method Get list of question by Quiz id.
        // --------------------------------------------------------------------------------------------------
        public List<Question> GetQuestionByQuizID(int QuizId)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            //String cStr = BuildUpdateCommand(student);      // helper method to build the insert string

            cmd = Create_SP_ReadQuestByQuizID("spGetQuestionByQuizID", con, QuizId);             // create the command


            List<Question> questlist = new List<Question>();

            try
            {


                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {

                    Question quest = new Question();
                    quest.QuestionId = Convert.ToInt32(dataReader["questionId"]);
                    quest.QuestionBody = dataReader["questionBody"].ToString();
                    quest.QuestionCurrectAnswer = dataReader["questionCurrectAnswer"].ToString();
                    quest.QuestionDistractingA = dataReader["questionDistractingA"].ToString();
                    quest.QuestionDistractingB = dataReader["questionDistractingB"].ToString();
                    quest.QuestionDistractingC = dataReader["questionDistractingC"].ToString();
                    quest.QuestionCategory = dataReader["questionCategory"].ToString();
                    quest.QuestionDifficulty = Convert.ToInt32(dataReader["questionDifficulty"]);
                    //quest.PhotoId = Convert.ToInt32(dataReader["photoId"]);

                    questlist.Add(quest);
                }

                return questlist;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //Dor
        //--------------------------------------------------------------------------------------------------
        // This method return user password
        //--------------------------------------------------------------------------------------------------
        public List<User> GetUserPass(string email)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            cmd = CreateCommandWithStoredProcedureReadUsersPass("spResetPassAndGetUser", con, email);


            List<User> listUser = new List<User>();

            try
            {


                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {

                    User user = new User();
                    user.UserId = Convert.ToInt32(dataReader["userId"]);
                    user.UserCoins = Convert.ToInt32(dataReader["userCoins"]);
                    user.UserRating = Convert.ToInt32(dataReader["userRating"]);
                    user.UserEmail = dataReader["userEmail"].ToString();
                    user.UserName = dataReader["userName"].ToString();
                    user.UserType = dataReader["userType"].ToString();
                    user.UserPassword = dataReader["userPassword"].ToString();

                    listUser.Add(user);
                }

                return listUser;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //Gilad
        //--------------------------------------------------------------------------------------------------
        // This method can add coins or set coins and rating for spesific user.
        //--------------------------------------------------------------------------------------------------
        public int SetAddRatingtoUser(int id,int value,bool isAdd,bool isCoins)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            // helper method to build the insert string
            string spname = "";
            if (isAdd)
            {
                spname = "spAddRatingUser";
            }
            else
            {
                spname = "spSetRatingUser";
                
            }
            if (isCoins)
            {
                spname = "spAddCoinsUser";
            }

            cmd = CreateCommandSP_SetAddRating(spname, con, id, value); // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (isAdd)
                {
                    int Rating = -1;
                    while (dataReader.Read())
                    {

                        Rating = Convert.ToInt32(dataReader["userRating"]);

                    }
                    return Rating;
                }
                else
                {
                    int numEffected = cmd.ExecuteNonQuery(); 
                    return numEffected;
                }
    
                

            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }


        //Gilad
        //--------------------------------------------------------------------------------------------------
        // return list of expert user by order of the rating.
        //--------------------------------------------------------------------------------------------------
        public List<User> GetExpertsUsersbyOrder()
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


            cmd = CreateCommandWithSPRead("spGetExpetsUser", con);


            List<User> listUser = new List<User>();

            try
            {


                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {

                    User user = new User();
                    user.UserId = Convert.ToInt32(dataReader["userId"]);
                    user.UserCoins = Convert.ToInt32(dataReader["userCoins"]);
                    user.UserRating = Convert.ToInt32(dataReader["userRating"]);
                    user.UserEmail = dataReader["userEmail"].ToString();
                    user.UserName = dataReader["userName"].ToString();
                    user.UserType = dataReader["userType"].ToString();
                    user.UserPassword = dataReader["userPassword"].ToString();

                    listUser.Add(user);
                }

                return listUser;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }



        //Gilad
        //--------------------------------------------------------------------------------------------------
        // return list of bad Identification by given userExpertid
        //--------------------------------------------------------------------------------------------------
        public List<object> GetBadIdeForSpesificExpert(int userExpertId)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }


            cmd = CreateCommandWithSPReadSpecificId("spGetBadIdeforSpesificExpert", con, userExpertId); // create the command

            List<object> list = new List<object>();
            try
            {


                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);



                while (dataReader.Read())
                {
                    var Res = new
                    {
                        identificationId = Convert.ToInt32(dataReader["identificationId"]),
                        ExpertId = Convert.ToInt32(dataReader["ExpertId"]),
                        RequestId = Convert.ToInt32(dataReader["RequestId"]),
                        photoUri = dataReader["photoUri"].ToString(),
                        photoId = Convert.ToInt32(dataReader["photoId"]),
                        plantId = Convert.ToInt32(dataReader["plantId"]),
                        SendToToken = (dataReader["userToken"]).ToString(),
                        questionsForExpertsId = Convert.ToInt32(dataReader["questionsForExpertsId"]),


                    };
                    list.Add(Res);

                }

                return list;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }



        //Gilad
        //--------------------------------------------------------------------------------------------------
        // return list of answer about bad Identification from expert by given user request id.
        //--------------------------------------------------------------------------------------------------

        public List<object> GetAnswerFromExpertToUser(int userID)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }


            cmd = CreateCommandWithSPReadSpecificId("spGetAnswerFromExpertToUser", con, userID); // create the command

            List<object> list = new List<object>();
            try
            {


                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);



                while (dataReader.Read())
                {
                    var Res = new
                    {
                        identificationId = Convert.ToInt32(dataReader["identificationId"]),
                        expertID = Convert.ToInt32(dataReader["expertID"]),
                        userId = Convert.ToInt32(dataReader["userId"]),
                        ExpertAnswer = dataReader["questionsForExpertsAnswer"].ToString(),



                    };
                    list.Add(Res);

                }

                return list;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }


        public List<object> GetListOfBonusQuest()
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }


            cmd = CreateCommandWithSPRead("spGetBonusQuestData", con); // create the command

            List<object> list = new List<object>();
            try
            {


                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);



                while (dataReader.Read())
                {
                    var Res = new
                    {
                        plantId = Convert.ToInt32(dataReader["plantId"]),
                        photoId = Convert.ToInt32(dataReader["photoId"]),
                        plantScientificName = dataReader["plantScientificName"].ToString(),
                        photoUri = dataReader["photoUri"].ToString(),



                    };
                    list.Add(Res);

                }

                return list;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }



        //Gilad
        //--------------------------------------------------------------------------------------------------
        // Submit the answer of expert about bad Identification
        //--------------------------------------------------------------------------------------------------
        public int SubmitAnswerOfExpert(int expertID, int QuestID, string answer)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            // helper method to build the insert string

            cmd = CreateCommandWithSPSubmitExpertAnswer("spSubmitAnswerasExpert", con, expertID, QuestID, answer); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        // --------------------------------------------------------------------------------------------------
        // 
        // --------------------------------------------------------------------------------------------------
        // 
        //        SQL COMMAND                        SQL COMMAND                                  SQL COMMAND
        // 
        // --------------------------------------------------------------------------------------------------
        // 
        // --------------------------------------------------------------------------------------------------


        private SqlCommand CreateCommandWithSPRead(string spName, SqlConnection con)                          
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            return cmd;
        }
        private SqlCommand CreateCommandWithSPReadSpecificId(string spName, SqlConnection con, int id)        
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@id", id);

            return cmd;
        }

        private SqlCommand CreateCommandWithSPSubmitExpertAnswer(string spName, SqlConnection con, int expertID,int QuestID,string answer)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@userId", expertID);
            cmd.Parameters.AddWithValue("@questionsForExpertsId", QuestID);
            cmd.Parameters.AddWithValue("@questionsForExpertsAnswer", answer);

            return cmd;
        }
        private SqlCommand CreateCommandWithStoredProcedureReadUsers(string spName, SqlConnection con, string password, string email)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            cmd.CommandText = spName;

            cmd.CommandTimeout = 10;

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@userPassword", password);

            cmd.Parameters.AddWithValue("@userEmail", email);

            return cmd;
        }

        private SqlCommand CreateCommandWithSPInsertQuestExpert(string spName, SqlConnection con, int plantid, int ideId)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            cmd.CommandText = spName;

            cmd.CommandTimeout = 10;

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@plantId", plantid);

            cmd.Parameters.AddWithValue("@identificationId", ideId);

            return cmd;
        }

        private SqlCommand CreateCommandWithSPInsertPlantIdentification(String spName, SqlConnection con, int plantId, int identificationId, double probability)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@plantId", plantId);
            cmd.Parameters.AddWithValue("@identificationId", identificationId);
            cmd.Parameters.AddWithValue("@identificationPercentage", probability);

            return cmd;
        }

        private SqlCommand CreateCommandWithSPInsertIdentification(String spName, SqlConnection con, Identification identity)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@userId", identity.UserId);
            cmd.Parameters.AddWithValue("@photoId", identity.PhotoId);


            return cmd;
        }


        private SqlCommand CreateCommandWithStoredProcedureReadUsersPass(string spName, SqlConnection con, string email)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            cmd.CommandText = spName;

            cmd.CommandTimeout = 10;

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@userEmail", email);

            return cmd;
        }

        private SqlCommand CreateCommandWithSPInsertPhoto(String spName, SqlConnection con, Photo pic)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure
            cmd.Parameters.AddWithValue("@photoUri", pic.PhotoUri);
            cmd.Parameters.AddWithValue("@PhotoTimestamp", pic.PhotoTimestamp);


            if (pic.UserId <= 0)
            {
                pic.UserId = null;
            }
            if (pic.PostId <= 0)
            {
                pic.PostId = null;
            }
            if (pic.Latitude <= 0)
            {
                pic.PostId = null;
            }
            if (pic.Longitude <= 0)
            {
                pic.PostId = null;
            }
            cmd.Parameters.AddWithValue("@userId", pic.UserId.HasValue ? pic.UserId : DBNull.Value);
            cmd.Parameters.AddWithValue("@postId", pic.PostId.HasValue ? pic.PostId : DBNull.Value);
            cmd.Parameters.AddWithValue("@Latitude", pic.Latitude.HasValue ? pic.Latitude : DBNull.Value);
            cmd.Parameters.AddWithValue("@Longitude", pic.Longitude.HasValue ? pic.Longitude : DBNull.Value);


            return cmd;
        }

        private SqlCommand CreateCommandWithStoredProcedureEditAndInsertUser(String spName, SqlConnection con, User user)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@userName", user.UserName);

            cmd.Parameters.AddWithValue("@userPassword", user.UserPassword);

            cmd.Parameters.AddWithValue("@userEmail", user.UserEmail);

            cmd.Parameters.AddWithValue("@userType", user.UserType);
            cmd.Parameters.AddWithValue("@userToken", user.UserToken);

            return cmd;
        }


        private SqlCommand Create_SP_ReadQuestByQuizID(string spName, SqlConnection con, int quizID)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@quizID", quizID);

            return cmd;
        }

        private SqlCommand Create_SP_SubmitQuiz(string spName, SqlConnection con, int quizID, int UserId, int score)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@userid", UserId);
            cmd.Parameters.AddWithValue("@quizId", quizID);
            cmd.Parameters.AddWithValue("@score", score);


            return cmd;
        }


        private SqlCommand Create_SP_FirstPlantofIDE(string spName, SqlConnection con, int ideID)
            //same as row 2053
            //
            //
            //
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@id", ideID);

            return cmd;
        }

        private SqlCommand CreateCommandSP_insertQuestion(string spName, SqlConnection con, Question quest)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@QuestionBody", quest.QuestionBody);
            cmd.Parameters.AddWithValue("@QuestionCurrectAnswer", quest.QuestionCurrectAnswer);
            cmd.Parameters.AddWithValue("@QuestionDistractingA", quest.QuestionDistractingA);
            cmd.Parameters.AddWithValue("@QuestionDistractingB", quest.QuestionDistractingB);
            cmd.Parameters.AddWithValue("@QuestionDistractingC", quest.QuestionDistractingC);
            cmd.Parameters.AddWithValue("@QuestionDifficulty", quest.QuestionDifficulty);
            cmd.Parameters.AddWithValue("@QuestionCategory", quest.QuestionCategory);


            return cmd;
        }

        private SqlCommand CreateCommandSP_insertQuestionandQuiz(string spName, SqlConnection con, int idQuiz, int idQuest)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@idQuiz", idQuiz);
            cmd.Parameters.AddWithValue("@idQuestion", idQuest);



            return cmd;
        }


        private SqlCommand CreateCommandSP_SetAddRating(string spName, SqlConnection con, int userid, int value)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

      
            cmd.Parameters.AddWithValue("@id", userid);
            cmd.Parameters.AddWithValue("@Rate", value);



            return cmd;
        }

        private SqlCommand CreateCommandSP_insertQuiz(string spName, SqlConnection con, string name)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@name", name);

            return cmd;
        }


        private SqlCommand CreateCommandWithSPInsertPlant(String spName, SqlConnection con, string plantScientificName)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@PlantScientificName", plantScientificName);

            return cmd;
        }


        private SqlCommand CreateCommandWithSPInsertQuestWithExpertUser(String spName, SqlConnection con, string answer,int userid,int QuestId)
        {
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

            cmd.Parameters.AddWithValue("@questionsForExpertsId", QuestId);
            cmd.Parameters.AddWithValue("@userId", userid);
            cmd.Parameters.AddWithValue("@questionsForExpertsAnswer", answer);

            return cmd;
        }
    }
}