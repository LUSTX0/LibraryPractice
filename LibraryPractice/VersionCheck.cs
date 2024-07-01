using Newtonsoft.Json;
using System;

namespace LIBRARY2
{
    public static class VersionCheck
    {
        public static string Check(string testVersion)
        {
            string jsonString = File.ReadAllText("version.json");
            var obj = JsonConvert.DeserializeObject<dynamic>(jsonString);
            if (obj == null)
            {
                return testVersion;
            }

            string version = obj.version;
            if (testVersion == version + ".0")
            {
                jsonString = File.ReadAllText("versionCI.json");
                obj = JsonConvert.DeserializeObject<dynamic>(jsonString);
                if (obj == null)
                {
                    return testVersion;                    
                }

                if (((string)obj.version).Contains('+'))
                {
                    version = ((string)obj.version).Split('+')[0];
                }
                else
                {
                    version = obj.version;
                }

                bool status = obj.status;
                if (!status)
                {
                    int patch = int.Parse(version.Split('.')[2]);
                    version = string.Join('.', [version.Split('.')[0], version.Split('.')[1], (patch + 1).ToString()]);
                    obj.version = version;
                    obj.status = true;
                    File.WriteAllText("versionCI.json", JsonConvert.SerializeObject(obj));
                }

                testVersion = version;
            }
            else
            {
                var data = new
                {
                    version = testVersion,
                    status = false
                };
                File.WriteAllText("versionCI.json", JsonConvert.SerializeObject(data));
            }

            return testVersion;
        }
    }
}
