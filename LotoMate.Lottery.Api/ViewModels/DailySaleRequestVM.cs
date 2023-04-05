using System;
using System.Collections.Generic;
using System.Text;

namespace LotoMate.Lottery.Api.ViewModels
{
  public  class DailySaleRequestVM
    {
        public int StoreId { get; set; }
        public DailySaleState SaleState { get; set; }
    }
}
