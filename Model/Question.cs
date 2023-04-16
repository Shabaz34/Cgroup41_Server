using UniServer.Models.DAL;

namespace UniServer.Model
{
    public class Question
    {
        //level:1 -> Easy
        // level:2 -> Hard



        //field//
        int questionId;
        string questionBody;
        string questionCurrectAnswer;
        string questionDistractingA;
        string questionDistractingB;
        string questionDistractingC;
        string questionCategory;
        int questionDifficulty;
        int photoId;


        //property//
        public int QuestionId { get => questionId; set => questionId = value; }
        public string QuestionBody { get => questionBody; set => questionBody = value; }
        public string QuestionCurrectAnswer { get => questionCurrectAnswer; set => questionCurrectAnswer = value; }
        public string QuestionDistractingA { get => questionDistractingA; set => questionDistractingA = value; }
        public string QuestionDistractingB { get => questionDistractingB; set => questionDistractingB = value; }
        public string QuestionDistractingC { get => questionDistractingC; set => questionDistractingC = value; }
        public string QuestionCategory { get => questionCategory; set => questionCategory = value; }
        public int QuestionDifficulty { get => questionDifficulty; set => questionDifficulty = value; }
        public int PhotoId { get => photoId; set => photoId = value; }

        //constarctor//
        public Question(string questionBody, string questionCurrectAnswer, string questionDistractingA, string questionDistractingB, string questionDistractingC, string questionCategory, int questionDifficulty, int photoId)
        {
            QuestionBody = questionBody;
            QuestionCurrectAnswer = questionCurrectAnswer;
            QuestionDistractingA = questionDistractingA;
            QuestionDistractingB = questionDistractingB;
            QuestionDistractingC = questionDistractingC;
            QuestionCategory = questionCategory;
            QuestionDifficulty = questionDifficulty;
            PhotoId = photoId;
        }
        //OverLoad on constractor//
        public Question(string questionBody, string questionCurrectAnswer, string questionDistractingA, string questionDistractingB, string questionDistractingC, string questionCategory, int questionDifficulty)
        {
            QuestionBody = questionBody;
            QuestionCurrectAnswer = questionCurrectAnswer;
            QuestionDistractingA = questionDistractingA;
            QuestionDistractingB = questionDistractingB;
            QuestionDistractingC = questionDistractingC;
            QuestionCategory = questionCategory;
            QuestionDifficulty = questionDifficulty;
            
        }
        public Question() { 
        }
        //methods//

        //this method Generate new question take level 1 or 2 , multi is boolean true=multi answers false=yes or no question.
        static public Question GenerateQuest(int level,bool multi)
        {
            //get all plant from data base//
            List<Plant> allPlants = new List<Plant>();
            allPlants = Plant.GetPlantQuiz();
            //reuse the methods from guy modols//


            //Prefix Package Not Final//
            string questPrefix1 = "כמה עלי כותרת יש ל";
            string questPrefix2 = "באיזה אזור גידול נוכל לראות את";
            string questPrefix31 = "היא הכמות עלי כותרת של";
            string questPrefix32 = "הוא האזור גידול של";
            string questPrefix33 = "הוא השם המדעי של";

            string questPrefix3 = "היא תקופת הפריחה של";
            string questPrefix4 = "מה תקופת הפריחה של";
            string questPrefix5 = "לאיזה משפחה שייך הצמח";
            string questPrefix6 = "היא המשפחה של";
            string questPrefix7 = "מהי צורת העלים של";
            string questPrefix8 = "היא צורת עלים של";
            string questPrefix9 = "שמיש לרפואה";
            string questPrefix10 = "ראוי למאכל";
            string questPrefix11 = "הוא צמח אלרגני";
            string questPrefix12 = "הוא צמח רעיל";
            string questPrefix13 = "מייצר צוף";
            string questPrefix14 = "נמצא בסכנת הכחדה";
            string questPrefix15 = "הוא מוגדר כצמח מוגן";
            string questPrefix17 = "מהי צורת הגבעול של";

            string questPrefix34 = "מי מהצמחים הבאים שייך למשפחת";
            string questPrefix35 = "ומשמש כצמח מאכל או תבלין";

            string questPrefix16 = "מה השם המדעי של";
            string questPrefix19 = "מהי צורת החיים של";
            string questPrefix20 = "היא שפת העלה של";
            string questPrefix21 = "היא צורת החיים של";
            string questPrefix24 = "מי מהצמחים הבאים שמיש לרפואה";
            string questPrefix25 = "מי מהצמחים הבאים שמיש כצמח תבלין או מאכל";
            string questPrefix26 = "מי מהצמחים הבאים הוא צמח אלרגני";
            string questPrefix27 = "מי מהצמחים הבאים הוא צמח רעיל";
            string questPrefix28 = "מי מהצמחים הבאים נמצאים בסכנת הכחדה";
            string questPrefix29 = "מי מהצמחים הבאים מוגדר כצמח מוגן";
            string questPrefix30 = "מי מהצמחים הבאים יכול לייצר צוף";

            string prefix = "האם";
            string Questionbody;
            string a = "";
            string b = "";
            string c = "";
            string d = "";
            string cat;
            //Prefix Package Not Final END//

            //Local Vars to build Questions
            Random rand = new Random();
            int randIndex = rand.Next(allPlants.Count);
            Plant RandPlant = allPlants[randIndex];
           
            
            if (multi)
            {
                //category = multi choise question
                cat = "רב-ברירה";

                if (level == 1)
                {
                    int randQuest = rand.Next(11);
                    //Level:Easy -> multi quest//
                    //prefix: 27,25,30,1,2,3,19,21;
                    if(randQuest==0)
                    {
                        //prefix27//
                        Questionbody = questPrefix27 ;
                        List<Plant> ToxicPlants = new List<Plant>();
                        List<Plant> NonToxicPlants = new List<Plant>();

                        foreach (var pl in allPlants)
                        {
                            if (pl.PlantIsToxic)
                            {
                                ToxicPlants.Add(pl);
                            }
                            else
                            {
                                NonToxicPlants.Add(pl);
                            }

                        }
                        int randPlantforToxic = rand.Next(ToxicPlants.Count);
                        List<int> indexsDistrict= new List<int>();
                        while (indexsDistrict.Count<3)
                        {
                            int indexUniq = rand.Next(NonToxicPlants.Count);
                            if (indexsDistrict.Contains(indexUniq)==false)
                            {
                                indexsDistrict.Add(indexUniq);
                            }
                            
                        }
                        a = ToxicPlants[randPlantforToxic].PlantName;
                        b = NonToxicPlants[indexsDistrict[0]].PlantName;
                        c = NonToxicPlants[indexsDistrict[1]].PlantName;
                        d = NonToxicPlants[indexsDistrict[2]].PlantName;

                    }
                    else if(randQuest==1)
                    {
                        //prefix25//
                        Questionbody = questPrefix25 ;
                        List<Plant> EatablePlants = new List<Plant>();
                        List<Plant> NonEatablePlants = new List<Plant>();

                        foreach (var pl in allPlants)
                        {
                            if (pl.PlantIsEatable)
                            {
                                EatablePlants.Add(pl);
                            }
                            else
                            {
                                NonEatablePlants.Add(pl);
                            }

                        }
                        int randPlantforEAT = rand.Next(EatablePlants.Count);
                        List<int> indexsDistrict = new List<int>();
                        while (indexsDistrict.Count<3)
                        {
                            int indexUniq = rand.Next(NonEatablePlants.Count);
                            if (indexsDistrict.Contains(indexUniq)==false)
                            {
                                indexsDistrict.Add(indexUniq);
                            }
                            
                        }
                        a = EatablePlants[randPlantforEAT].PlantName;
                        b = NonEatablePlants[indexsDistrict[0]].PlantName;
                        c = NonEatablePlants[indexsDistrict[1]].PlantName;
                        d = NonEatablePlants[indexsDistrict[2]].PlantName;


                    }
                    else if (randQuest == 2)
                    {
                        //prefix30//

                        Questionbody = questPrefix30 ;
                        List<Plant> HoneyPlants = new List<Plant>();
                        List<Plant> NonHoneyPlants = new List<Plant>();

                        foreach (var pl in allPlants)
                        {
                            if (pl.PlantIsProvidedHoneydew)
                            {
                                HoneyPlants.Add(pl);
                            }
                            else
                            {
                                NonHoneyPlants.Add(pl);
                            }

                        }
                        int randPlantHoney = rand.Next(HoneyPlants.Count);
                        List<int> indexsDistrict = new List<int>();
                        while (indexsDistrict.Count<3)
                        {
                            int indexUniq = rand.Next(NonHoneyPlants.Count);
                            if (indexsDistrict.Contains(indexUniq)==false)
                            {
                                indexsDistrict.Add(indexUniq);
                            }
                            
                        }
                        a = HoneyPlants[randPlantHoney].PlantName;
                        b = NonHoneyPlants[indexsDistrict[0]].PlantName;
                        c = NonHoneyPlants[indexsDistrict[1]].PlantName;
                        d = NonHoneyPlants[indexsDistrict[2]].PlantName;


                    }
                    else if (randQuest == 3)
                    {
                        //prefix1//
                        Questionbody = questPrefix1 + " " + RandPlant.PlantName;
                        List<string> AnsDistrict= new List<string>();
                        foreach (var pl in allPlants)
                        {
                            if (pl.PlantNumOfPetals != RandPlant.PlantNumOfPetals && AnsDistrict.Contains(pl.PlantNumOfPetals)==false)
                            {
                                AnsDistrict.Add(pl.PlantNumOfPetals);
                            }
                        }
                        
                        List<int> Ultraindex= new List<int>();
                        while (Ultraindex.Count<3)
                        {
                            int indexUniq = rand.Next(AnsDistrict.Count);
                            if (Ultraindex.Contains(indexUniq)==false)
                            {
                                Ultraindex.Add(indexUniq);
                            }
                            
                        }
                        a = RandPlant.PlantNumOfPetals;
                        b = AnsDistrict[Ultraindex[0]];
                        c = AnsDistrict[Ultraindex[1]];
                        d = AnsDistrict[Ultraindex[2]];





                    }
                    else if (randQuest == 4)
                    {
                        //prefix2//
                        Questionbody = questPrefix2 + " " + RandPlant.PlantName;
                        List<string> districtanswer = new List<string>();
                        foreach (var pl in allPlants)
                        {
                            if (pl.PlantHabitat != RandPlant.PlantHabitat)
                            {
                                if (districtanswer.Contains(pl.PlantHabitat)==false)
                                {
                                    districtanswer.Add(pl.PlantHabitat);
                                }
                            }
                        }

                        List<int> Ultraindex = new List<int>();
                        while (Ultraindex.Count < 3)
                        {
                            int indextoDis = rand.Next(districtanswer.Count);
                            if (Ultraindex.Contains(indextoDis)==false)
                            {
                                Ultraindex.Add(indextoDis);
                            }
                            
                        }
                        a = RandPlant.PlantHabitat;
                        b = districtanswer[Ultraindex[0]];
                        c = districtanswer[Ultraindex[1]];
                        d = districtanswer[Ultraindex[2]];


                    }
                    else if (randQuest == 5)
                    {
                        //prefix3//
                        Questionbody = RandPlant.PlantBloomingSeason+", "+questPrefix3;
                        List <string> DistrictAnswers= new List<string>();
                        List <int> UltraIndex= new List<int>();
                        foreach (var pl in allPlants)
                        {
                            if (pl.PlantBloomingSeason!=RandPlant.PlantBloomingSeason && DistrictAnswers.Contains(pl.PlantName)==false)
                            {
                                //beacuse the condition in if above,now we can random just 3 numbers and stay UNIQ//
                                DistrictAnswers.Add(pl.PlantName);
                            }
                        }
                        //need only 3 times beacuse its already uniq//
                        while (UltraIndex.Count<3)
                        {
                            int indexUniq = rand.Next(DistrictAnswers.Count);
                            if (UltraIndex.Contains(indexUniq)==false)
                            {
                                UltraIndex.Add(indexUniq);
                            }
                           
                        }
                        a = RandPlant.PlantName;
                        b = DistrictAnswers[UltraIndex[0]];
                        c = DistrictAnswers[UltraIndex[1]];
                        d = DistrictAnswers[UltraIndex[2]];





                    }
                    else if (randQuest == 6)
                    {
                        //prefix19//
                        Questionbody = questPrefix19 + " " + RandPlant.PlantName;
                        List<string> districtAnswers = new List<string>();
                        List<int> ultraIndex= new List<int>();
                        foreach (var pl in allPlants)
                        {
                            if (RandPlant.PlantLifeForm!=pl.PlantLifeForm && districtAnswers.Contains(pl.PlantLifeForm)==false)
                            {
                                //beacuse the condition in if above,now we can random just 3 numbers and stay UNIQ//
                                districtAnswers.Add(pl.PlantLifeForm);

                            }
                        }
                        while (ultraIndex.Count<3)
                        {
                            int indexUniq = rand.Next(districtAnswers.Count);
                            if (ultraIndex.Contains(indexUniq)==false)
                            {
                                ultraIndex.Add(indexUniq);
                            }
                            
                        }
                        a = RandPlant.PlantLifeForm;
                        b = districtAnswers[ultraIndex[0]];
                        c = districtAnswers[ultraIndex[1]];
                        d = districtAnswers[ultraIndex[2]];


                    }
                    else if (randQuest == 7)
                    {
                        //prefix5//
                        Questionbody = questPrefix5 + " " + RandPlant.PlantName;
                        List<string> districtAnswers = new List<string>();
                        List<int> ultraIndex = new List<int>();
                        foreach (var pl in allPlants)
                        {
                            if (RandPlant.PlantFamily != pl.PlantFamily && districtAnswers.Contains(pl.PlantFamily) == false)
                            {
                                //beacuse the condition in if above,now we can random just 3 numbers and stay UNIQ//
                                districtAnswers.Add(pl.PlantFamily);

                            }
                        }
                        while (ultraIndex.Count < 3)
                        {
                            int indexUniq = rand.Next(districtAnswers.Count);
                            if (ultraIndex.Contains(indexUniq) == false)
                            {
                                ultraIndex.Add(indexUniq);
                            }

                        }
                        a = RandPlant.PlantFamily;
                        b = districtAnswers[ultraIndex[0]];
                        c = districtAnswers[ultraIndex[1]];
                        d = districtAnswers[ultraIndex[2]];


                    }
                    else if (randQuest == 8)
                    {
                        //prefix31//
                        Questionbody = RandPlant.PlantNumOfPetals + ", " + questPrefix31;
                        List<string> districtAnswers = new List<string>();
                        List<int> UltraIndex = new List<int>();



                        foreach (var pl in allPlants)
                        {
                            if (pl.PlantNumOfPetals != RandPlant.PlantNumOfPetals && districtAnswers.Contains(pl.PlantName) == false)
                            {
                                districtAnswers.Add(pl.PlantName);
                            }

                        }
                        while (UltraIndex.Count < 3)
                        {
                            int indexUniq = rand.Next(districtAnswers.Count);
                            if (UltraIndex.Contains(indexUniq) == false)
                            {
                                UltraIndex.Add(indexUniq);
                            }
                        }
                        a = RandPlant.PlantName;
                        b = districtAnswers[UltraIndex[0]];
                        c = districtAnswers[UltraIndex[1]];
                        d = districtAnswers[UltraIndex[2]];
                    }
                    else if (randQuest == 9)
                    {
                        //prefix34 + prefix35//
                        
                        
                        List<int> UltraIndex = new List<int>();
                        List <Plant> options= new List<Plant>();
                        List<Plant> noneatable = new List<Plant>();



                        foreach (var pl in allPlants)
                        {
                            if (pl.PlantIsEatable)
                            {
                                //districtAnswers.Add(pl.PlantName);
                                options.Add(pl);
                            }
                            else
                            {
                                noneatable.Add(pl);
                            }

                        }
                        int randomOptionIndex = rand.Next(options.Count);
                        Questionbody = questPrefix34 + " " + options[randomOptionIndex].PlantFamily +" "+questPrefix35;

                        while (UltraIndex.Count < 2)
                        {
                            int indexUniq = rand.Next(noneatable.Count);
                            if (UltraIndex.Contains(indexUniq) == false)
                            {
                                UltraIndex.Add(indexUniq);
                            }
                        }
                        int districtEATable = rand.Next(options.Count);
                        while (districtEATable== randomOptionIndex)
                        {
                            districtEATable = rand.Next(options.Count);
                        }
                        a = options[randomOptionIndex].PlantName;
                        b = noneatable[UltraIndex[0]].PlantName;
                        c = noneatable[UltraIndex[1]].PlantName;
                        d = options[districtEATable].PlantName;
                    }
                    else
                    {
                        //prefix21//
                        Questionbody = RandPlant.PlantLifeForm + ", " + questPrefix21;
                        List<string> disAnswer = new List<string>();
                        List<int> ultraIndex = new List<int>();
                        foreach (var pl in allPlants)
                        {
                            if (pl.PlantLifeForm!=RandPlant.PlantLifeForm && disAnswer.Contains(pl.PlantName)==false)
                            {
                                //beacuse the condition in 'if' above,now we can random just 3 numbers and stay UNIQ//
                                disAnswer.Add(pl.PlantName);

                            }
                        }
                        while (ultraIndex.Count<3)
                        {
                            int indexUniq = rand.Next(disAnswer.Count);
                            if (ultraIndex.Contains(indexUniq)==false)
                            {
                                ultraIndex.Add(indexUniq);
                            }
                            
                        }
                        a = RandPlant.PlantName;
                        b = disAnswer[ultraIndex[0]];
                        c = disAnswer[ultraIndex[1]];
                        d = disAnswer[ultraIndex[2]];


                    }


                }
                else
                {
                    //Hard - multi quest//
                    //level=='2'//
                    //prefix: 7,8,16,20,24,26,28,29, 4? //
                    int randQuest = rand.Next(12);
                    if (randQuest==0)
                    {
                        //prefix7//
                        Questionbody = questPrefix7 + " " + RandPlant.PlantName;
                        List<string> DistrictAnswers = new List<string>();
                        List<int> UltraIndex = new List<int>();
                        foreach (var pl in allPlants)
                        {
                            if (pl.PlantLeafShape!=RandPlant.PlantLeafShape && DistrictAnswers.Contains(pl.PlantLeafShape)==false)
                            {
                                //beacuse the condition in 'if' above,now we can random just 3 numbers and stay UNIQ//
                                DistrictAnswers.Add(pl.PlantLeafShape);

                            }
                        }
                        while (UltraIndex.Count<3)
                        {
                            int indexUniq = rand.Next(DistrictAnswers.Count);
                            if (UltraIndex.Contains(indexUniq)==false)
                            {
                                UltraIndex.Add(indexUniq);
                            }
                            
                        }
                        a = RandPlant.PlantLeafShape;
                        b = DistrictAnswers[UltraIndex[0]];
                        c = DistrictAnswers[UltraIndex[1]];
                        d = DistrictAnswers[UltraIndex[2]];





                    }
                    else if (randQuest==1)
                    {
                        //prefix8//
                        Questionbody = RandPlant.PlantLeafShape + ", " + questPrefix8;
                        List<string> DistrictAnswers = new List<string>();
                        List<int> UltraIndex = new List<int>();
                        foreach (var pl in allPlants)
                        {
                            if (pl.PlantLeafShape != RandPlant.PlantLeafShape && DistrictAnswers.Contains(pl.PlantName) == false)
                            {
                                //beacuse the condition in 'if' above,now we can random just 3 numbers and stay UNIQ//
                                DistrictAnswers.Add(pl.PlantName);

                            }
                        }
                        while (UltraIndex.Count < 3)
                        {
                            int indexUniq = rand.Next(DistrictAnswers.Count);
                            if (UltraIndex.Contains(indexUniq) == false)
                            {
                                UltraIndex.Add(indexUniq);
                            }

                        }
                        a = RandPlant.PlantName;
                        b = DistrictAnswers[UltraIndex[0]];
                        c = DistrictAnswers[UltraIndex[1]];
                        d = DistrictAnswers[UltraIndex[2]];



                    }
                    else if (randQuest == 2)
                    {
                        //prefix16//
                        // there is no enogth data base for 3 district !!! // ***
                        Questionbody = questPrefix16 + " " + RandPlant.PlantName;
                        List<string> DistrictAnswers = new List<string>();
                        List<int> UltraIndex = new List<int>();
                        foreach (var pl in allPlants)
                        {
                            if (pl.PlantScientificName != RandPlant.PlantScientificName && DistrictAnswers.Contains(pl.PlantScientificName) == false)
                            {
                                //beacuse the condition in 'if' above,now we can random just 3 numbers and stay UNIQ//
                                DistrictAnswers.Add(pl.PlantScientificName);

                            }
                        }
                        while (UltraIndex.Count < 3)
                        {
                            int indexUniq = rand.Next(DistrictAnswers.Count);
                            if (UltraIndex.Contains(indexUniq) == false)
                            {
                                UltraIndex.Add(indexUniq);
                            }

                        }
                        a = RandPlant.PlantScientificName;
                        b = DistrictAnswers[UltraIndex[0]];
                        c = DistrictAnswers[UltraIndex[1]];
                        d = DistrictAnswers[UltraIndex[2]]; ;



                    }
                    else if (randQuest == 3)
                    {
                        //prefix20//
                        Questionbody = RandPlant.PlantLeafMargin + ", " + questPrefix20;
                        List<string> DistrictAnswers = new List<string>();
                        List<int> UltraIndex = new List<int>();
                        foreach (var pl in allPlants)
                        {
                            if (pl.PlantLeafMargin != RandPlant.PlantLeafMargin && DistrictAnswers.Contains(pl.PlantName) == false)
                            {
                                //beacuse the condition in 'if' above,now we can random just 3 numbers and stay UNIQ//
                                DistrictAnswers.Add(pl.PlantName);

                            }
                        }
                        while (UltraIndex.Count < 3)
                        {
                            int indexUniq = rand.Next(DistrictAnswers.Count);
                            if (UltraIndex.Contains(indexUniq) == false)
                            {
                                UltraIndex.Add(indexUniq);
                            }

                        }
                        a = RandPlant.PlantName;
                        b = DistrictAnswers[UltraIndex[0]];
                        c = DistrictAnswers[UltraIndex[1]];
                        d = DistrictAnswers[UltraIndex[2]];



                    }
                    else if (randQuest == 4)
                    {
                        //prefix24//
                        Questionbody = questPrefix24;
                        List<string> MedicPlantNames = new List<string>();
                        List<string> NONMedicPlantNames = new List<string>();
                        List<int> UltraIndex = new List<int>();

                       

                        foreach (var pl in allPlants)
                        {
                            if (pl.PlantMedic)
                            {
                                MedicPlantNames.Add(pl.PlantName);
                            }
                            else
                            {
                                NONMedicPlantNames.Add(pl.PlantName);
                            }

                        }
                        while (UltraIndex.Count<3)
                        {
                            int indexUniq = rand.Next(NONMedicPlantNames.Count);
                            if (UltraIndex.Contains(indexUniq)==false)
                            {
                                UltraIndex.Add(indexUniq);
                            }
                        }
                        a = MedicPlantNames[rand.Next(MedicPlantNames.Count)];
                        b = NONMedicPlantNames[UltraIndex[0]];
                        c = NONMedicPlantNames[UltraIndex[1]];
                        d = NONMedicPlantNames[UltraIndex[2]];


                    }
                    else if (randQuest == 5)
                    {
                        //prefix26//
                        Questionbody = questPrefix26;
                        List<string> AllePlantNames = new List<string>();
                        List<string> NONallePlantNames = new List<string>();
                        List<int> UltraIndex = new List<int>();



                        foreach (var pl in allPlants)
                        {
                            if (pl.PlantIsAllergenic)
                            {
                                AllePlantNames.Add(pl.PlantName);
                            }
                            else
                            {
                                NONallePlantNames.Add(pl.PlantName);
                            }

                        }
                        while (UltraIndex.Count < 3)
                        {
                            int indexUniq = rand.Next(NONallePlantNames.Count);
                            if (UltraIndex.Contains(indexUniq) == false)
                            {
                                UltraIndex.Add(indexUniq);
                            }
                        }
                        a = AllePlantNames[rand.Next(AllePlantNames.Count)];
                        b = NONallePlantNames[UltraIndex[0]];
                        c = NONallePlantNames[UltraIndex[1]];
                        d = NONallePlantNames[UltraIndex[2]];

                    }
                    else if (randQuest == 6)
                    {
                        //prefix28//
                        Questionbody = questPrefix28;
                        List<string> DANPlantNames = new List<string>();
                        List<string> NONDanPlantNames = new List<string>();
                        List<int> UltraIndex = new List<int>();



                        foreach (var pl in allPlants)
                        {
                            if (pl.PlantIsEndangered)
                            {
                                DANPlantNames.Add(pl.PlantName);
                            }
                            else
                            {
                                NONDanPlantNames.Add(pl.PlantName);
                            }

                        }
                        while (UltraIndex.Count < 3)
                        {
                            int indexUniq = rand.Next(NONDanPlantNames.Count);
                            if (UltraIndex.Contains(indexUniq) == false)
                            {
                                UltraIndex.Add(indexUniq);
                            }
                        }
                        a = DANPlantNames[rand.Next(DANPlantNames.Count)];
                        b = NONDanPlantNames[UltraIndex[0]];
                        c = NONDanPlantNames[UltraIndex[1]];
                        d = NONDanPlantNames[UltraIndex[2]];

                    }
                    else if (randQuest == 7)
                    {
                        //prefix29//
                        Questionbody = questPrefix29;
                        List<string> ProtectedPlantNames = new List<string>();
                        List<string> NONProtectedPlantNames = new List<string>();
                        List<int> UltraIndex = new List<int>();



                        foreach (var pl in allPlants)
                        {
                            if (pl.PlantIsProtected)
                            {
                                ProtectedPlantNames.Add(pl.PlantName);
                            }
                            else
                            {
                                NONProtectedPlantNames.Add(pl.PlantName);
                            }

                        }
                        while (UltraIndex.Count < 3)
                        {
                            int indexUniq = rand.Next(NONProtectedPlantNames.Count);
                            if (UltraIndex.Contains(indexUniq) == false)
                            {
                                UltraIndex.Add(indexUniq);
                            }
                        }
                        a = ProtectedPlantNames[rand.Next(ProtectedPlantNames.Count)];
                        b = NONProtectedPlantNames[UltraIndex[0]];
                        c = NONProtectedPlantNames[UltraIndex[1]];
                        d = NONProtectedPlantNames[UltraIndex[2]];


                    }
                    else if (randQuest == 8)
                    {
                        //prefix6//
                        Questionbody = RandPlant.PlantFamily + " " + questPrefix6;
                        List<string> districtAnswers = new List<string>();
                        List<int> UltraIndex = new List<int>();



                        foreach (var pl in allPlants)
                        {
                            if (pl.PlantFamily!=RandPlant.PlantFamily && districtAnswers.Contains(pl.PlantName)==false)
                            {
                                districtAnswers.Add(pl.PlantName);
                            }

                        }
                        while (UltraIndex.Count < 3)
                        {
                            int indexUniq = rand.Next(districtAnswers.Count);
                            if (UltraIndex.Contains(indexUniq) == false)
                            {
                                UltraIndex.Add(indexUniq);
                            }
                        }
                        a = RandPlant.PlantName;
                        b = districtAnswers[UltraIndex[0]];
                        c = districtAnswers[UltraIndex[1]];
                        d = districtAnswers[UltraIndex[2]];


                    }
                    else if (randQuest==9)
                    {
                        //prefix32//
                        Questionbody = RandPlant.PlantHabitat + ", " + questPrefix32;
                        List<string> districtAnswers = new List<string>();
                        List<int> UltraIndex = new List<int>();



                        foreach (var pl in allPlants)
                        {
                            if (pl.PlantHabitat != RandPlant.PlantHabitat && districtAnswers.Contains(pl.PlantName) == false)
                            {
                                districtAnswers.Add(pl.PlantName);
                            }

                        }
                        while (UltraIndex.Count < 3)
                        {
                            int indexUniq = rand.Next(districtAnswers.Count);
                            if (UltraIndex.Contains(indexUniq) == false)
                            {
                                UltraIndex.Add(indexUniq);
                            }
                        }
                        a = RandPlant.PlantName;
                        b = districtAnswers[UltraIndex[0]];
                        c = districtAnswers[UltraIndex[1]];
                        d = districtAnswers[UltraIndex[2]];
                    }
                    else if (randQuest == 10)
                    {
                        //prefix33//
                        Questionbody = RandPlant.PlantScientificName + ", " + questPrefix33;
                        List<string> districtAnswers = new List<string>();
                        List<int> UltraIndex = new List<int>();



                        foreach (var pl in allPlants)
                        {
                            if (pl.PlantScientificName != RandPlant.PlantScientificName && districtAnswers.Contains(pl.PlantName) == false)
                            {
                                districtAnswers.Add(pl.PlantName);
                            }

                        }
                        while (UltraIndex.Count < 3)
                        {
                            int indexUniq = rand.Next(districtAnswers.Count);
                            if (UltraIndex.Contains(indexUniq) == false)
                            {
                                UltraIndex.Add(indexUniq);
                            }
                        }
                        a = RandPlant.PlantName;
                        b = districtAnswers[UltraIndex[0]];
                        c = districtAnswers[UltraIndex[1]];
                        d = districtAnswers[UltraIndex[2]];
                    }
                    else
                    {
                        //prefix4//
                        Questionbody = questPrefix4 + " " + RandPlant.PlantName;
                        List<string> DistrictAnswers = new List<string>();
                        List <int> ultraIndex = new List<int>();

                        foreach (var pl in allPlants)
                        {
                            if (pl.PlantBloomingSeason!=RandPlant.PlantBloomingSeason && DistrictAnswers.Contains(pl.PlantBloomingSeason)==false)
                            {
                                DistrictAnswers.Add(pl.PlantBloomingSeason);
                            }
                        }
                        while (ultraIndex.Count<3)
                        {
                            int indexUniq = rand.Next(DistrictAnswers.Count);
                            if (ultraIndex.Contains(indexUniq)==false)
                            {
                                ultraIndex.Add(indexUniq);
                            }
                        }
                        a = RandPlant.PlantBloomingSeason;
                        b = DistrictAnswers[ultraIndex[0]];
                        c = DistrictAnswers[ultraIndex[1]];
                        d = DistrictAnswers[ultraIndex[2]];



                    }


                }
            }
            else
            {
                //multi==false//
                cat = "כן או לא";
                //Easy question - yes or no//
                //prefix: 10,12,13;
                if (level == 1)
                {
                    int randQuest = rand.Next(4);
                    if(randQuest==0)
                    {
                        //prefix10//
                        Questionbody = prefix+" "+ RandPlant.PlantName+" "+ questPrefix10;
                        if (RandPlant.PlantIsEatable)
                        {
                            // a correct
                            a = "כן";
                            b = "לא";
                        }
                        else
                        {
                            //  correct
                            b = "כן";
                            a = "לא";
                        }

                    }
                    else if(randQuest==1)
                    {
                        //prefix12//
                        Questionbody = prefix+" "+ RandPlant.PlantName+" "+ questPrefix12;
                        
                        if (RandPlant.PlantIsToxic)
                        {
                            // a correct
                            a = "כן";
                            b = "לא";
                        
                        }
                        else
                        {
                            //  correct
                            b = "כן";
                            a = "לא";
                        }
                    }
                    else if (randQuest == 2)
                    {
                        //prefix17//
                        Questionbody = questPrefix17+" "+RandPlant.PlantName+" ";

                        if (RandPlant.PlantStemShape=="עגול")
                        {
                            // a correct
                            a = "עגול";
                            b = "מרובע";

                        }
                        else
                        {
                            //  correct
                            b = "עגול";
                            a = "מרובע";
                        }

                    }
                    else
                    {
                        //prefix13//
                        Questionbody = prefix+" "+ RandPlant.PlantName+" "+ questPrefix13;
                     
                        if (RandPlant.PlantIsProvidedHoneydew)
                        {
                            // a correct
                            a = "כן";
                            b = "לא";
                        }
                        else
                        {
                            //  correct
                            b = "כן";
                            a = "לא";
                        }

                    }
                }
                else
                {
                    //level="h"
                    //Hard question - yes or no//
                    //prefix: 9,11,14,15;
                    int randQuest = rand.Next(4);
                    if (randQuest == 0)
                    {
                        //prefix9//
                        Questionbody = prefix+" "+ RandPlant.PlantName+" "+ questPrefix9;
                        if (RandPlant.PlantMedic)
                        {
                            a = "כן";
                            b = "לא";
                        }
                        else
                        {
                            b = "כן";
                            a = "לא";

                        }

                    }
                    else if(randQuest == 1)
                    {
                        //prefix11//
                        Questionbody = prefix+" "+ RandPlant.PlantName+" "+ questPrefix11;
                        if (RandPlant.PlantIsAllergenic)
                        {
                            a = "כן";
                            b = "לא";
                        }
                        else
                        {
                            b = "כן";
                            a = "לא";

                        }

                    }
                    else if (randQuest == 2)
                    {
                        //prefix14//
                        Questionbody = prefix+" "+ RandPlant.PlantName+" "+ questPrefix14;
                        if (RandPlant.PlantIsEndangered)
                        {
                            a = "כן";
                            b = "לא";
                        }
                        else
                        {
                            b = "כן";
                            a = "לא";

                        }

                    }
                    else
                    {
                        //prefix15//
                        Questionbody = prefix+" "+ RandPlant.PlantName+" "+ questPrefix15;
                        if (RandPlant.PlantIsProtected)
                        {
                            a = "כן";
                            b = "לא";
                        }
                        else
                        {
                            b = "כן";
                            a = "לא";

                        }

                    }


                }

            }




            Question questToReturn = new Question(Questionbody, a, b, c, d, cat, level);
            return questToReturn;
            

        }

        //insert specific question to db
        public int insertQuestion()
        {
            DBservices dBservice = new DBservices();
            return dBservice.InsertQuestion(this);
        }
    }
}
