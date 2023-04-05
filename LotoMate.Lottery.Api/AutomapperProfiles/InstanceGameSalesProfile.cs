using AutoMapper;
using LotoMate.Lottery.Api.ViewModels;
using LotoMate.Lottery.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LotoMate.Lottery.Api.AutomapperProfiles
{
    //public class InstanceGameSalesProfile : Profile
    //{
    //    public InstanceGameSalesProfile()
    //    {
    //        CreateMap<InstanceDailySale, InstanceGameSalesHeader>(MemberList.None)
    //         .ForMember(x => x.StoreId, m => m.MapFrom(x => x.StoreId))
    //         .ForMember(x => x.OpenUserId, m => m.MapFrom(x => x.OpenUserId))
    //         .ForMember(x => x.ClosedUserId, m => m.MapFrom(x => x.ClosedUserId))
    //         .ForMember(x => x.TransactionDate, m => m.MapFrom(x => x.TransactionDate))
    //         .ForPath(x => x.StoreName, m => m.MapFrom(x => x.Store.StoreName))
    //         .ForMember(d => d.SalesDetail,
    //                opt => opt.MapFrom(src => new List<InstanceGameSalesViewModel>()
    //                {
    //                    new InstanceGameSalesViewModel()
    //                    { Id = src.Id, CloseNo = src.CloseNo, OpenNo = src.OpenNo,
    //                         InstanceGameBookId = src.InstanceGameBookId, TotalSale = src.TotalSale,
    //                     TotalSalePrice = src.TotalSalePrice, GameName = src.InstanceGameBook.InstanceGame.Name}
    //                }));

    //    }
    //}
    //public class InstanceGameSalesProfileFromBook : Profile
    //{
    //    public InstanceGameSalesProfileFromBook()
    //    {
    //        //CreateMap<InstanceGameBook, InstanceGameSalesHeader>(MemberList.None)
    //        // .ForMember(x => x.StoreId, m => m.MapFrom(x => x.StoreId))
    //        // .ForPath(x => x.StoreName, m => m.MapFrom(x => x.Store.StoreName))
    //        // .ForMember(d => d.SalesDetail,
    //        //        opt => opt.MapFrom(src => new List<InstanceGameSalesViewModel>()
    //        //        {
    //        //            new InstanceGameSalesViewModel()
    //        //            {  Id = 0, CloseNo = 0, OpenNo = 0,
    //        //                 InstanceGameBookId = src.Id, TotalSale = 0,
    //        //             TotalSalePrice = 0, GameName = src.InstanceGame.Name,
    //        //            }
    //        //        }));

    //        CreateMap<InstanceGameBook, InstanceGameSalesHeader>()
    //             .ConstructUsing(itm => new InstanceGameSalesHeader
    //             {
    //                 StoreId = itm.StoreId,
    //                 StoreName = itm.Store.StoreName,
    //                 SalesDetail = new List<InstanceGameSalesViewModel>()
    //                    {
    //                        new InstanceGameSalesViewModel()
    //                        {
    //                            Id = 0, CloseNo = 0, OpenNo = 0,
    //                            InstanceGameBookId = itm.Id, TotalSale = 0,
    //                            TotalSalePrice = 0, GameName = itm.InstanceGame.Name,
    //                        }
    //                    }
    //             });

    //    }
    //}

    public class DailySlesMapper
    {
        public static ICollection<InstanceDailySale> IGSHeadertoIDS(InstanceGameSalesHeader sales)
        {
            var list = new List<InstanceGameSalesHeader>();
            list.Add(sales);
            var result = list.SelectMany(x => x.SalesDetail, (parent, child) =>
                  new InstanceDailySale()
                  {
                      Id = child.Id,
                      OpenNo = child.OpenNo,
                      CloseNo = child.CloseNo,
                      Price = child.Price,
                      //TotalSale = child.TotalSale,
                      //TotalSalePrice = child.TotalSalePrice,
                      InstanceGameBookId = child.InstanceGameBookId,
                      OpenUserId = parent.OpenUserId,
                      ClosedUserId = parent.ClosedUserId,
                      StoreId = parent.StoreId,
                      TransactionDate = parent.TransactionDate
                  }).ToList();
            return result;
        }

        public static InstanceGameSalesHeader IDStoIGSHeader(ICollection<InstanceDailySale> sales)
        {
            if (sales?.Count == 0) return null;
            var first = sales.FirstOrDefault();
            var header = new InstanceGameSalesHeader()
            {
                StoreId = first.StoreId,
                StoreName = first?.Store?.StoreName,
                ClosedUserId = first.ClosedUserId,
                OpenUserId = first.OpenUserId,
                CloseUserName = first?.ClosedUser?.UserName,
                OpenUserName = first?.OpenUser?.UserName,
                //TransactionDate = first.TransactionDate,
                TransactionDate = DateTime.Now,
                SalesDetail = sales.Select(src =>
                    new InstanceGameSalesViewModel()
                    {
                        Id = src.Id,
                        OpenNo = src.OpenNo,
                        CloseNo = src.CloseNo,
                        GameName = src?.InstanceGameBook?.InstanceGame.Name,
                        Price = src?.InstanceGameBook?.InstanceGame.Price,
                        InstanceGameBookId = src.InstanceGameBookId,
                        TotalSale = src.CloseNo - src.OpenNo,
                        TotalSalePrice = src.Price.Value *  (src.CloseNo.Value - src.OpenNo.Value)

                    }).ToList()
            };

            return header;
        }

        public static InstanceGameSalesHeader IGBtoIGSHeader(ICollection<InstanceGameBook> sales, int? userId, string userName)
        {
            if (sales?.Count == 0) return null;
            var first = sales.FirstOrDefault();
            var header = new InstanceGameSalesHeader()
            {
                StoreId = first.StoreId,
                StoreName = first?.Store?.StoreName,
                ClosedUserId = 0,
                OpenUserId = userId,
                OpenUserName = userName,
                TransactionDate = DateTime.Now,
                SalesDetail = sales.Select(src =>
                    new InstanceGameSalesViewModel()
                    {
                        Id = 0,
                        OpenNo = 0,
                        CloseNo = 0,
                        GameName = src?.InstanceGame?.Name,
                        Price = src?.InstanceGame?.Price,
                        InstanceGameBookId = src.Id,
                        TotalSale = 0,
                        TotalSalePrice = 0
                    }).ToList()
            };
            return header;
        }
    }
}
