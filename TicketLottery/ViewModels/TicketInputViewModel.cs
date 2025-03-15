using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TicketLottery.Models;
using TicketLottery.Utilities;

namespace TicketLottery.ViewModels
{
    public partial class TicketInputViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddInputHistoryCommand))]
        private string? _username;

        [ObservableProperty]
        private int _count;

        [ObservableProperty]
        private bool _moneySwitch;

        [ObservableProperty]
        private ObservableCollection<InsertHistoryModel> _insertHistory;

        [ObservableProperty]
        private List<TicketModel> _tickets;

        public TicketInputViewModel()
        {
            InsertHistory = new ObservableCollection<InsertHistoryModel>();
            Tickets = new List<TicketModel>();
        }

        [RelayCommand]
        private void Initialize()
        {
            InsertHistory = [.. TicketWriter.Load()];
            Tickets = TicketCalculator.GetTickets(InsertHistory).ToList();
        }

        [RelayCommand(CanExecute = nameof(CanAddInputHistory))]
        private void AddInputHistory()
        {
            InsertHistory.Add(new InsertHistoryModel
            {
                Username = Username!,
                Count = Count,
                Id = Guid.NewGuid(),
                InsertTime = DateTime.Now,
                Type = MoneySwitch ? Enums.TicketType.Donation : Enums.TicketType.Subscribe,
            });

            Save();

            Username = string.Empty;
            Count = 0;
        }

        private bool CanAddInputHistory()
        {
            return !string.IsNullOrEmpty(Username);
        }

        [RelayCommand]
        private void HandleInput(Key key)
        {
            if (key == Key.Enter && CanAddInputHistory())
            {
                AddInputHistory();
            }
        }

        [RelayCommand]
        private void Save()
        {
            TicketWriter.Save(InsertHistory);
            Tickets = TicketCalculator.GetTickets(InsertHistory).ToList();
        }
    }
}
