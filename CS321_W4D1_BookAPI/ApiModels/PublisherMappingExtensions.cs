﻿using CS321_W4D1_BookAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CS321_W4D1_BookAPI.ApiModels
{
    public static class PublisherMappingExtensions
    {

        public static PublisherModel ToApiModel(this Publisher publisher)
        {
            return new PublisherModel
            {
                Id = publisher.Id,
                Name = publisher.Name,
                FoundedYear = publisher.FoundedYear,
                CountryOfOrigin = publisher.CountryOfOrigin,
                HeadQuartersLocation = publisher.HeadQuartersLocation,
                Books = publisher.Books.ToApiModels().ToList()
            };
        }

        public static Publisher ToDomainModel(this PublisherModel publisherModel)
        {
            return new Publisher
            {
                // TODO: map PublisherModel properties to corresponding Publisher props
            };
        }

        public static IEnumerable<PublisherModel> ToApiModels(this IEnumerable<Publisher> publishers)
        {
            return publishers.Select(a => a.ToApiModel());
        }

        public static IEnumerable<Publisher> ToDomainModel(this IEnumerable<PublisherModel> publisherModels)
        {
            return publisherModels.Select(a => a.ToDomainModel());
        }
    }
}
