using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dto
{
    public class GeoPoint
    {
        [Required]
        public double Lng { get; set; }
        [Required]
        public double Lat { get; set; }
        public GeoPoint(double lng, double lat)
        {
            this.Lat = lat;
            this.Lng = lng;
        }
    }
}
