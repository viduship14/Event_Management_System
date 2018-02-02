using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagementSystem.Models
{
    public class ImageVM
    {
        public int E_id { get; set; }
        public int Image_id { get; set; }
        public string Path { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
        HttpFileCollectionBase files { get; }
    }
}