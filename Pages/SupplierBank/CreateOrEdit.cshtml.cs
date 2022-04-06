using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using TranferBankInformation.Domain.DTO.SupplierBank;
using TranferBankInformation.Services.Services;
using TransferBankInformation.Helpers;
using TransferBankInformation.ViewModels;

namespace TransferBankInformation.Pages.SupplierBank
{
    public class CreateModel/* : PageModel*/
    {
        private readonly ISupplierBankServices _supplierBankServices;
        public IHtmlLocalizer<Resources.SharedMessages> _sharedMessagesLocalizer { get; private set; }
        public IHtmlLocalizer<IndexModel> _localizer { get; private set; }
        public List<SelectListItem> Bancos { get; set; }

        public List<SelectListItem> Monedas { get; set; }
        [BindProperty]
        public List<SupplierBankInfo> SupplierBankInfoList { get; set; }

        public SupplierBankInfoModal SupplierBankInfoItem { get; set; }
        public SupplierBankInfo SupplierBankInfo { get; set; }

   
        #region comments
        //public CreateModel(IHtmlLocalizer<Resources.SharedMessages> sharedMessagesLocalizer, ISupplierBankServices supplierBankServices, IHtmlLocalizer<IndexModel> Localizer)
        //{
        //    _supplierBankServices = supplierBankServices;
        //    _sharedMessagesLocalizer = sharedMessagesLocalizer;
        //    _localizer = Localizer;
        //}
        //public async Task<ActionResult> OnGetAsync(string cedula)
        //{
        //    PageMessage msg;
        //    try
        //    {
        //        SupplierBankInfo = new SupplierBankInfo();
        //        var Bank = await _supplierBankServices.GetBanksList();
        //        Bancos = Bank.Select(a =>
        //                              new SelectListItem
        //                              {
        //                                  Value = a.Nombre,
        //                                  Text = a.Nombre
        //                              }).ToList();

        //        var Coin = await _supplierBankServices.GetCoinsList();
        //        Monedas = Coin.Select(a =>
        //                              new SelectListItem
        //                              {
        //                                  Value = a.Descripcion,
        //                                  Text = a.Descripcion
        //                              }).ToList();
        //        await GetRequiredDataToLoadPage(cedula);

        //    }
        //    catch (Exception ex)
        //    {
        //        msg = new PageMessage(MessageType.Danger, _sharedMessagesLocalizer.GetString("LoadingDataError", HtmlEncoder.Default.Encode(ex.Message)));
        //        TempData["PageMessage"] = JsonConvert.SerializeObject(msg);
        //    }

        //    return Page();

        //}

        //private async Task GetRequiredDataToLoadPage(string cedula)
        //{


        //    var supplierBank = _supplierBankServices.SearchSupplierBank(cedula);



        //    await Task.WhenAll(supplierBank);
        //    if (supplierBank.Result != null)
        //    {
        //        SupplierBankInfo = new SupplierBankInfo()
        //        {
        //            Cedula = supplierBank.Result.Cedula,
        //            Banco = supplierBank.Result.Banco,
        //            NombreRegistral = supplierBank.Result.NombreRegistral,
        //            NombreComercial = supplierBank.Result.NombreComercial,
        //            Moneda = supplierBank.Result.Moneda,
        //            CodigoSwift = supplierBank.Result.CodigoSwift,
        //            CodigoAba = supplierBank.Result.CodigoAba,
        //            Direccion = supplierBank.Result.Direccion,
        //            Principal = supplierBank.Result.Principal,
        //            Secundaria = supplierBank.Result.Secundaria,
        //            Auxiliar = supplierBank.Result.Auxiliar

        //        };
        //    }
        //    else
        //    {
        //        SupplierBankInfo.Cedula = cedula;
        //    }


        //}

        //public async Task<IActionResult> OnPostAsync()
        //{

        //    PageMessage msg = null;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {


        //           // var existClient = await _supplierBankServices.SearchSupplierBank(SupplierBankInfo.Cedula);


        //                var t = Request.Form["Cedula"];
        //                var registerResponse = await _supplierBankServices.SaveSupplierBank(new SupplierBankDTO()
        //                {
        //                    Cedula = Request.Form["Cedula"],
        //                    Banco = Request.Form["Banco"],
        //                    NombreRegistral = Request.Form["NombreRegistral"],
        //                    NombreComercial = Request.Form["NombreComercial"],
        //                    Moneda = Request.Form["Moneda"],
        //                    CodigoSwift = Request.Form["CodigoSwift"],
        //                    CodigoAba = Request.Form["CodigoAba"],
        //                    Direccion = Request.Form["Direccion"],
        //                    Principal = Request.Form["Principal"],
        //                    Secundaria = Request.Form["Secundaria"],
        //                    Auxiliar = Request.Form["Auxiliar"]

        //                });

        //                //msg = new PageMessage(registerResponse.Type.Equals("Success") ? MessageType.Success : MessageType.Danger, registerResponse.Message);
        //                //TempData["PageMessage"] = JsonConvert.SerializeObject(msg);
        //                return RedirectToPage("./Index");




        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = new PageMessage(MessageType.Danger, _sharedMessagesLocalizer.GetString("RegisterErrorMessage", HtmlEncoder.Default.Encode(ex.Message)));
        //        TempData["PageMessage"] = JsonConvert.SerializeObject(msg);
        //        return RedirectToPage("Create", new { cedula = SupplierBankInfo.Cedula });
        //    }

        //    return Page();

        //}
        #endregion
    }
}
