using UniServer.Models.DAL;

namespace UniServer.Models {

    public class Photo
    {
        public string PhotoUri { get; set; }
        public int? tripDayId { get; set; }
        public int? PostId { get; set; }
        public int PhotoId { get; set; }
        public int? UserId { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string PhotoTimestamp { get; set; }



        //Guy
        //--------------------------------------------------------------------------------------------------
        // insert Photo and return it.
        //--------------------------------------------------------------------------------------------------
        public Photo InsertPhoto(Photo pic)
        {
                DBservices dbs = new DBservices();
                return dbs.InsertPhoto(pic);
        }


        //Guy
        //--------------------------------------------------------------------------------------------------
        // return photo by given id 
        //--------------------------------------------------------------------------------------------------

        public Photo readPhotoByPhotoId(int photoId)
        {
            DBservices dbs = new DBservices();
            return dbs.readPhotoByPhotoId(photoId);
        }
    }
}