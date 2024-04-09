using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using TicketLottery.Models;

namespace TicketLottery.Viewmodels
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

        public TicketInputViewModel()
        {
            InsertHistory = new ObservableCollection<InsertHistoryModel>();
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

            using (var fileStream = new FileStream(Path.Combine(Environment.CurrentDirectory, "data.json"), FileMode.OpenOrCreate))
            using (Utf8JsonWriter writer = new Utf8JsonWriter(fileStream))
            {
                JsonDocument json = JsonSerializer.SerializeToDocument(InsertHistory, InsertHistoryJsonContext.Default.IEnumerableInsertHistoryModel);
                json.WriteTo(writer);
            }

            Username = string.Empty;
            Count = 0;
        }

        private bool CanAddInputHistory()
        {
            return !string.IsNullOrEmpty(Username);
        }
    }
}
