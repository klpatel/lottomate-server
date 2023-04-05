using System;

namespace LotoMate.Lottery.Api.ViewModels
{
    public class InstanceGameMasterViewModel
    {
        public long Id { get; set; }
        public int? StoreId { get; set; }
        public string GameNumber { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public bool? IsActive { get; set; }
        public int? BookTotal { get; set; }

    }
}
