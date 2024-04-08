using TicketLottery.Enums;

namespace TicketLottery.Models
{
    public class InsertHistoryModel
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public int Count { get; set; }
        public TicketType Type { get; set; }
        public DateTime InsertTime { get; set; }
    }
}