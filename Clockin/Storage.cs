using System.IO;
using System.Text.Json;
using Clockin.JsonConverters;
using Clockin.Models;

namespace Clockin
{
    public interface IStorage
    {
        void Save(DataRoot dataRoot);
        DataRoot Get();
    }
    
    public class Storage: IStorage
    {
        private readonly JsonSerializerOptions _serializerOptions;
        private const string StorageFileName = "clockin.json";

        public Storage()
        {
            _serializerOptions = new JsonSerializerOptions();
            _serializerOptions.Converters.Add(new DateTimeConverter());
            _serializerOptions.Converters.Add(new TimespanConverter());
        }
        
        public void Save(DataRoot dataRoot)
        {
            File.WriteAllText(StorageFileName, JsonSerializer.Serialize(dataRoot, _serializerOptions));
        }

        public DataRoot Get()
        {
            return File.Exists(StorageFileName) 
                ? JsonSerializer.Deserialize<DataRoot>(File.ReadAllText(StorageFileName), _serializerOptions) ?? new DataRoot()
                : new DataRoot();
        }
    }
}