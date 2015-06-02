using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShawInterviewExercise.DAL
{
    public class Show
    {
        //[Required(ErrorMessage = "ID is required.")]
        public int Id { get; set; }
        //[Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "NameReadable is required.")]
        public string NameReadable { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
    }

/*--- Assuming next stage would be something like this ---*
    public class ShowAsset
    {
        [Required(ErrorMessage = "ID is required.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "ShowID is required.")]
        public int ShowId { get; set; }
        [Required(ErrorMessage = "URL is required.")]
        public string Url { get; set; }
        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; }
        public string Name { get; set; }
        public string Alt { get; set; }
    }
 */
}