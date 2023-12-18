using Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entity
{
    public class RouteEntity
    {
        public int Id { get; set; }
        public DateTimeOffset Start { get; set; }        
        public DateTimeOffset End { get; set; }

        // relations
        public int? TripId { get; set; }
        public virtual TripEntity Trip { get; set; }
        public int? FromId { get; set; }
        public virtual StopEntity From { get; set; }
        public int? ToId { get; set; }
        public virtual StopEntity To { get; set; }

        public RouteEntity()
        { }

        public void InitEntity(RouteRequestDto req)
        {
            this.Start = req.Start;
            this.From = new StopEntity(req.From);
            this.To = new StopEntity(req.To);
            this.End = req.End;
        }

        public RouteResponseDto ConvertToResponse()
        {
            var response = new RouteResponseDto();
            response.Start = this.Start;
            response.From = this.From.ConvertToResponse();
            response.To = this.To.ConvertToResponse();
            response.End = this.End;
            return response;
        }
    }
}
