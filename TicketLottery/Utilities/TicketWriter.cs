using System.IO;
using System.Text.Json;
using TicketLottery.Models;

namespace TicketLottery.Utilities
{
    public static class TicketWriter
    {
        private static string JsonPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "data.json");

        public static void Save(IEnumerable<InsertHistoryModel> data)
        {
            using (FileStream fileStream = new FileStream(JsonPath, FileMode.OpenOrCreate, FileAccess.Write))
            using (Utf8JsonWriter writer = new Utf8JsonWriter(fileStream))
            {
                JsonDocument json = JsonSerializer.SerializeToDocument(data, InsertHistoryJsonContext.Default.IEnumerableInsertHistoryModel);
                json.WriteTo(writer);
            }
        }

        public static IEnumerable<InsertHistoryModel> Load()
        {
            using (FileStream fileStream = new FileStream(JsonPath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                if (fileStream.Length > 0)
                {
                    return JsonSerializer.Deserialize(fileStream, InsertHistoryJsonContext.Default.IEnumerableInsertHistoryModel)!;
                }
                else
                {
                    return new List<InsertHistoryModel>();
                }
            }
        }
    }
}
