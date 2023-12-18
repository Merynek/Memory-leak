using Common.Dto;
using Common.Enums;
using GoogleApi.Entities.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Data.Entity
{
    public class PlaceEntity
    {
        [Key]
        public string PlaceId { get; set; }
        public GeoPoint Point { get; set; }
        public Country? Country { get; set; }        

        // relations
        public virtual ICollection<PlaceTranslationEntity> Translations { get; set; }

        public PlaceEntity(PlaceRequestDto req)
        {
            this.PlaceId = req.PlaceId;
        }

        public PlaceEntity()
        { }

        public PlaceResponseDto convertToResponse(Language lang = Language.Czech)
        {
            var translation = this.Translations.Where(t => t.Language == lang).FirstOrDefault();
            var place = new PlaceResponseDto();
            place.PlaceId = this.PlaceId;
            place.Name = translation.Name;
            place.LocalityName = translation.LocalityName;
            place.Country = this.Country;
            place.Point = this.Point;
            return place;
        }
    }
}
