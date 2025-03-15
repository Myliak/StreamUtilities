using TicketLottery.Enums;
using TicketLottery.Models;

namespace TicketLottery.Utilities
{
    public static class TicketCalculator
    {
        public static int CalculateTickets(IEnumerable<InsertHistoryModel> insertHistories)
        {
            int subscribeCount = insertHistories.Where(x => x.Type == TicketType.Subscribe).Sum(x => x.Count);
            int donationCount = insertHistories.Where(x => x.Type == TicketType.Donation).Sum(x => x.Count);

            return subscribeCount + (donationCount / Properties.Settings.Default.DonationSplit);
        }

        public static IEnumerable<TicketModel> GetTickets(IEnumerable<InsertHistoryModel> insertHistories)
        {
            return insertHistories
                .GroupBy(x => x.Username)
                .Select(x => new TicketModel()
                {
                    Username = x.Key,
                    Count = CalculateTickets(x)
                });
        }
    }
}
