using LotoMate.Lottery.Infrastructure.Models;
using LotoMate.Lottery.Reports.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace LotoMate.Lottery.Reports.AutomapperProfiles
{

    public class DailySalesMapper
    {
        public static InstanceSalesHeader IDStoIGSHeader(ICollection<InstanceDailySale> sales)
        {
            if (sales?.Count == 0) return null;
            var first = sales.FirstOrDefault();
            var header = new InstanceSalesHeader()
            {
                StoreId = first.StoreId,
                StoreName = first?.Store?.StoreName,
                ClosedUserId = first.ClosedUserId,
                OpenUserId = first.OpenUserId,
                CloseUserName = first?.ClosedUser?.UserName,
                OpenUserName = first?.OpenUser?.UserName,
                TransactionDate = first.TransactionDate,
                SalesDetail = sales.Select(src =>
                    new InstanceSalesReportVM()
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

        public static CategorySalesHeader CatSaletoCatHeader(ICollection<CategorisedSale> sales)
        {
            if (sales?.Count == 0) return null;
            var first = sales.FirstOrDefault();
            var header = new CategorySalesHeader()
            {
                StoreId = first.StoreId,
                StoreName = first?.Store?.StoreName,
                TransactionDate = first.TransactionDate, 
                 DailySales = sales.Select(src =>
                    new CategorySalesReportVM()
                    {
                        Id = src.Id,
                         Category = src.GameSalesCategory.CategoryName,
                          DebitOrCredit = src.GameSalesCategory.DebitOrCredit,
                           GameSalesCategoryId = src.GameSalesCategoryId, 
                            Total = src.Total

                    }).ToList()
            };
            return header;
        }

    }
}
