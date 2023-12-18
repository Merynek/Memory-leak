using Common.Dto;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Entity
{
    public class DirectionEntity
    {
        public int Id { get; set; }
        public int Time { get; set; }
        public int Distance { get; set; }
        public TransportType TransportType { get; set; }
        public string Polyline { get; set; }
        public ICollection<GeoPoint> Points { get; set; }

        // relations
        public int? TripId { get; set; }
        public virtual TripEntity Trip { get; set; }        

        public DirectionEntity()
        { }

        public void InitEntity(DirectionRequestDto req)
        {
            this.Time = req.Time;
            this.Distance = req.Distance;
            this.TransportType = req.TransportType;
            this.Polyline = req.Polyline;
            this.Points = req.Points;
        }

        public DirectionResponseDto convertToResponse()
        {
            var response = new DirectionResponseDto();
            response.Time = this.Time;
            response.Distance = this.Distance;
            response.TransportType = this.TransportType;
            response.Polyline = this.Polyline;
            response.Points = this.Points;
            return response;
        }
    }
}
