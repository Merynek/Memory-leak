using Common.Dto;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entity
{
    public class StopEntity
    {
        public int Id { get; set; }
        public PlaceResolution Resolution { get; set; }

        // relations
        public int? RouteId { get; set; }
        public string PlaceId { get; set; } = null;
        public virtual PlaceEntity Place { get; set; }

        public StopEntity(StopRequestDto req)
        {
            this.PlaceId = req.Place.PlaceId;
            this.Resolution = req.Resolution;
        }

        public StopEntity()
        { }

        public StopResponseDto ConvertToResponse()
        {
            var response = new StopResponseDto();
            response.Resolution = this.Resolution;
            response.Place = this.Place.convertToResponse();
            return response;
        }
    }
}
