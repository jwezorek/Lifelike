using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lifelike
{
    public class RulesSaveData
    {
        public string CellStructure
        {
            set;
            get;
        }

        public string NeighborhoodFunction
        {
            set;
            get;
        }

        public int[,] StateTable
        {
            set;
            get;
        }

        public RulesSaveData()
        {
        }

        public RulesSaveData(CellularAutomataSettings settings, int[,] stateTable)
        {
            CellStructure = settings.CellStructure.Name;
            NeighborhoodFunction = settings.NeighborhoodFunction.Name;
            StateTable = stateTable;
        }

        public void Save(string filename)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(filename))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, this);
            }
        }

        public static RulesSaveData Load(string filename)
        {
            string json = File.ReadAllText(filename);
            if (!string.IsNullOrEmpty(json))
                return JsonConvert.DeserializeObject<RulesSaveData>(json);
            return null;
        }
    }
}
