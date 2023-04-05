using LotoMate.Lottery.Api.ViewModels;
using LotoMate.Lottery.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LotoMate.Lottery.Api.AutomapperProfiles
{
    public class CategorisedSalesProfile
    {
        //preparing CategorisedSalesHeader object from GameSalesCategory
        public static CategorisedSalesHeader GSCtoCSHeader(ICollection<GameSalesCategory> sales)
        {
            if (sales?.Count == 0) return null;
            var first = sales.FirstOrDefault();
            var header = new CategorisedSalesHeader()
            {
                StoreId = first.StoreId,
                StoreName = first?.Store?.StoreName,
                TransactionDate = DateTime.Now,
                CreditSalesDetail = sales.Where(x => x.DebitOrCredit == true) //for credit list
                                    .Select(src =>
                                     new CategorisedSalesViewModel()
                                     {
                                         Id = 0,
                                         GameSalesCategoryId = src.Id,
                                         CategoryName = src.CategoryName,
                                         Total = 0
                                     }).ToList(),
                DebitSalesDetail = sales.Where(x => x.DebitOrCredit == false) //for debit list
                                   .Select(src =>
                                     new CategorisedSalesViewModel()
                                     {
                                         Id = 0,
                                         GameSalesCategoryId = src.Id,
                                         CategoryName = src.CategoryName,
                                         Total = 0
                                     }).ToList()
            };
            

            return header;
        }
        //preparing CategorisedSalesHeader object from CategorisedSales
        public static CategorisedSalesHeader GStoCSHeader(ICollection<CategorisedSale> sales)
        {
            if (sales?.Count == 0) return null;
            var first = sales.FirstOrDefault();
            var header = new CategorisedSalesHeader()
            {
                StoreId = first.StoreId,
                StoreName = first?.Store?.StoreName,
                TransactionDate = DateTime.Now,
                CreditSalesDetail = sales.Where(x => x.GameSalesCategory.DebitOrCredit == true) //for credit list
                                    .Select(src =>
                                     new CategorisedSalesViewModel()
                                     {
                                         Id = src.Id,
                                         GameSalesCategoryId = src.GameSalesCategoryId,
                                         CategoryName = src.GameSalesCategory.CategoryName,
                                         Total = src.Total
                                     }).ToList(),
                DebitSalesDetail = sales.Where(x => x.GameSalesCategory.DebitOrCredit == false) //for debit list
                                    .Select(src =>
                                     new CategorisedSalesViewModel()
                                     {
                                         Id = src.Id,
                                         GameSalesCategoryId = src.GameSalesCategoryId,
                                         CategoryName = src.GameSalesCategory.CategoryName,
                                         Total = src.Total
                                     }).ToList()
            };
            return header;
        }
        //preparing to save CategorisedSales from CategorisedSalesHeader object 
        public static ICollection<CategorisedSale> CSHeadertoGS(CategorisedSalesHeader sales)
        {
            var list = new List<CategorisedSalesHeader>();
            list.Add(sales);
            var result1 = list.SelectMany(x => x.CreditSalesDetail, (parent, child) =>
                         new CategorisedSale()
                         {
                             Id = child.Id,
                             GameSalesCategoryId = child.GameSalesCategoryId,
                             Total = child.Total,
                             StoreId = parent.StoreId,
                             TransactionDate = parent.TransactionDate
                         }).ToList();
            var result2 = list.SelectMany(x => x.DebitSalesDetail, (parent, child) =>
                        new CategorisedSale()
                        {
                            Id = child.Id,
                            GameSalesCategoryId = child.GameSalesCategoryId,
                            Total = child.Total,
                            StoreId = parent.StoreId,
                            TransactionDate = parent.TransactionDate
                        }).ToList();

             result1.AddRange(result2);
            return result1;
            //var result = result2.ToArray();
            //result1.CopyTo(result);
            //return result;

        }
    }
}
