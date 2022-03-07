using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HashProperty.Converter
{
    internal sealed class HashConverter : ValueConverter<string, string>
    {
        public HashConverter(Provider.IHashProvider provider, ConverterMappingHints mappingHints = null) :
            base(x => provider.Hash(x), x => x, mappingHints)
        {
        }
    }
}