using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketLottery.Viewmodels
{
    public partial class TicketInputViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? _username;

        [ObservableProperty]
        private int _count;
    }
}
