using HashProperty.Attribute;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq;

namespace HashProperty.Extension
{
    public static class BuilderExtension
    {
        public static void UseHash(this ModelBuilder modelBuilder, Provider.IHashProvider provider)
        {
            if (modelBuilder is null)
                throw new ArgumentNullException(nameof(modelBuilder), "There is not ModelBuilder object.");
            if (provider is null)
                throw new ArgumentNullException(nameof(provider), "You should create encryption provider.");

            var converter = new Converter.HashConverter(provider);
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (IMutableProperty property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(string) && !IsDiscriminator(property))
                    {
                        object[] attributes = property.PropertyInfo.GetCustomAttributes(typeof(HashPropertyAttribute), false);
                        if (attributes.Any())
                            property.SetValueConverter(converter);
                    }
                }
            }
        }

        private static bool IsDiscriminator(IMutableProperty property) => property.Name == "Discriminator" || property.PropertyInfo == null;


        public static DbContextOptionsBuilder UseHash(this DbContextOptionsBuilder optionsBuilder, string saltKey, bool isHash)
        {
            Config.HashConfig.SetConfig(saltKey, isHash);
            return optionsBuilder;
        }
    }
}
