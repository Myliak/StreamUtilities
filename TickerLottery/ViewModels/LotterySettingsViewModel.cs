using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketLottery.ViewModels
{
    public partial class LotterySettingsViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _donationSplit;

        public LotterySettingsViewModel()
        {
            DonationSplit = Properties.Settings.Default.DonationSplit;
        }


    }
}
