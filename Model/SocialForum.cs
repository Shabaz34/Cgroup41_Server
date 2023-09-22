using UniServer.Models.DAL;

namespace picAplant.Model
{
    public class SocialForum
    {
        int socialForumID;
        string socialForumTimeStamp;
        string socialForumName;
        string socialForumDisc;
        int profilePhotoID;

        public int SocialForumID { get => socialForumID; set => socialForumID = value; }
        public string SocialForumTimeStamp { get => socialForumTimeStamp; set => socialForumTimeStamp = value; }
        public string SocialForumName { get => socialForumName; set => socialForumName = value; }
        public string SocialForumDisc { get => socialForumDisc; set => socialForumDisc = value; }
        public int ProfilePhotoID { get => profilePhotoID; set => profilePhotoID = value; }


        static public List <object> GetForumByUseridFollowORnot(int userID)
        {
            DBservices db = new DBservices();
            return db.GetListOfUNforums(userID);
        }
        static public int FollowThis(int userID,int forumid) {
           
            DBservices db = new DBservices();
            return db.Followthis(userID, forumid);
        
        }

        static public int CreateNewForum(int useid,string forumname,string forumdis,int photoid)
        {
            DBservices db = new DBservices();
            return db.OpenForum(useid,forumname,forumdis,photoid);
        }
        static public int InsertNewPost(int userId,int forumID,string content)
        {
            DBservices dBservices= new DBservices();
            return dBservices.SendPpost(userId,forumID,content);
        }

        static public List<object> ReadPostByForumId(int forumId)
        {
            DBservices dBservices= new DBservices();
            return dBservices.GetPostBySpecificForum(forumId);
        }
        static public int SendReplay(int postId ,int userId, string content)
        {
            DBservices dBservices= new DBservices();
            return dBservices.SendReplay(userId,postId,content);
        }
    }
}
