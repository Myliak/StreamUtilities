using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Windows.Threading;
using TicketLottery.Models;

namespace TicketLottery.ViewModels
{
    public partial class SpinWinnerViewModel : ObservableObject
    {
        [ObservableProperty]
        private IEnumerable<TicketModel> _tickets;

        [ObservableProperty]
        private string? _winnerUserName;

        [ObservableProperty]
        private string _header;

        public SpinWinnerViewModel(IEnumerable<TicketModel> tickets)
        {
            _tickets = tickets;
            _header = "And the winner is ...";
        }

        [RelayCommand]
        public async Task Initialize()
        {
            // Create a weighted list based on the number of tickets each user has
            List<string> weightedTickets = Tickets.SelectMany(ticket => Enumerable.Repeat(ticket.Username, ticket.Count)).ToList();

            // Randomly select a winner from the weighted list
            Random random = new Random();

            int currentValue = 0;
            int currentIncrement = 0;

            while (currentValue < 500)
            {
                currentValue += currentIncrement;

                currentIncrement += 1;

                await Task.Delay(currentValue);

                string newWinnerUserName = string.Empty;
                do
                {
                    newWinnerUserName = weightedTickets[random.Next(weightedTickets.Count)];
                }
                while (newWinnerUserName == WinnerUserName);
                
                WinnerUserName = newWinnerUserName;
            }

            Header = "Congratulations!";
        }
    }
}
