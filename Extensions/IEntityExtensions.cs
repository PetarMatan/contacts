using manageContacts.Entities;
using System;
using System.Collections.Generic;
using manageContacts.Entities.Models;

namespace manageContacts.Extensions
{
    public static class IEntityExtensions
    {
        public static bool isObjectNull(this MobileNumber entity)
        {
            return null == entity;
        }

        public static bool isEmptyObject(this IEntity entity)
        {
            return (entity.id.Equals(null) || entity.id.Equals(-1));
        }
    }
}