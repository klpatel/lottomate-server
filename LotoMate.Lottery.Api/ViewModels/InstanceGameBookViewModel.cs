using System;

namespace LotoMate.Lottery.Api.ViewModels
{
    public class InstanceGameBookViewModel
    {
        public long Id { get; set; }
        public long InstanceGameId { get; set; }
        public int? StoreId { get; set; }
        public string DisplayNumber { get; set; }
        public string BookNumber { get; set; }
        public DateTime? ActivateDate { get; set; }
        public DateTime? SettleDate { get; set; }
        public bool? IsActive { get; set; }
        public int? ActivateUserId { get; set; }
        public int? SettleUserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string GameNumber { get; set; }
        public string Name { get; set; }
    }
}
