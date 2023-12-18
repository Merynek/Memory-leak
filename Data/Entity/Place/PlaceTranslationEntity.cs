using GoogleApi.Entities.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entity
{
    public class PlaceTranslationEntity
    {
        public int Id { get; set; }
        public Language Language { get; set; }
        public string Name { get; set; }
        public string LocalityName { get; set; }

        // relations
        public int? PlaceId { get; set; }
        public virtual PlaceEntity Place { get; set; }

        public PlaceTranslationEntity()
        { }
    }
}
