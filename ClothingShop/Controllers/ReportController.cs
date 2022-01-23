using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using ClothingShop.BusinessLogic.Helpers;
using ClothingShop.BusinessLogic.Repositories.Interfaces;
using ClothingShop.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly IShopRepository _shopRepository;

        public ReportController(IShopRepository shopRepository, INotyfService notyf)
        {
            _shopRepository = shopRepository;
            _notyf = notyf;
        }

        //GET: Report/Billing
        [HttpGet]
        public IActionResult Billing()
        {
            var model = new ReportBillingModel();

            return View(model);
        }

        //POST: Report/Billing
        [HttpPost]
        public async Task<IActionResult> Billing(ReportBillingModel model)
        {
            if (!ModelState.IsValid)
            {
                _notyf.Error("Thông tin không hợp lệ");
            }
            else
            {
                model = await _shopRepository.GetBillingReport(model);

                if (model.IsExport) return await ExportBillingReport(model);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<FileContentResult> ExportBillingReport(ReportBillingModel model)
        {
            model.PageSize = -1;

            //Get data
            var rs = await _shopRepository.GetAllBillingReport(model);

            var table = ExportExcelHelper.ToDataTable(rs);

            var columnsTake = table.Columns.Cast<DataColumn>()
                .Select(x => x.ColumnName)
                .ToArray();

            string[] columnsDisplay =
            {
                "Mã đơn hàng",
                "Mã người dùng",
                "Tên khách hàng",
                "Tên người nhận",
                "Địa chỉ",
                "Số điện thoại",
                "Số tiền gốc",
                "Chiết khấu",
                "Tổng số tiền",
                "Ngày lập đơn hàng",
                "Ngày duyệt đơn hàng",
                "Trạng thái đơn hàng",
                "Ghi chú"
            };
            for (var i = 0; i < table.Rows.Count; i++)
            for (var k = 0; k < table.Columns.Count; k++)
            {
                if (table.Columns[k].DataType != typeof(string)) continue;

                var a = "" + table.Rows[i][k];
                if (string.IsNullOrEmpty(a)) table.Rows[i][k] = " - ";
            }

            // build in condition list
            var conditions = new Dictionary<string, string>
            {
                {"Từ ngày", model.FromDate.ToString("dd/MM/yyyy")},
                {"Ðến ngày", model.ToDate.ToString("dd/MM/yyyy")},
                {"Mã người dùng", model.UserId ?? string.Empty},
                {"Mã đơn hàng", model.OrderId == null ? string.Empty : model.OrderId.Value.ToString()}
            };
            var filecontent =
                ExportExcelHelper.ExportExcel(table, conditions, true, "#1fb5ad", columnsDisplay, columnsTake);
            return File(filecontent, ExportExcelHelper.ExcelContentType,
                "Bao_Cao_Ke_Toan_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx");
        }
    }
}