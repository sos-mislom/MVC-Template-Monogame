using System.IO;
using Newtonsoft.Json;

namespace GameProject.Managers;

public static class JsonManager
{
    public static void Serialize(string _path, object _obj)
    {
        var dir = Path.GetDirectoryName(_path);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        var json = JsonConvert.SerializeObject(_obj, Formatting.Indented);
        File.WriteAllText(_path, json);
    }

    public static T? Deserialize<T>(string _path)
    {
        if (!File.Exists(_path))
            return default(T);

        var json = File.ReadAllText(_path);
        return JsonConvert.DeserializeObject<T>(json);
    }

}