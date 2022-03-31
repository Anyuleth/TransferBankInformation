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
using TranferBankInformation.Services.Services;
using TransferBankInformation.Helpers;
using TransferBankInformation.ViewModels;
namespace TransferBankInformation.Pages.SupplierBank
{
    public class IndexModel : PageModel
    {
        private readonly ISupplierBankServices _supplierBankServices;
        public IHtmlLocalizer<Resources.SharedMessages> _sharedMessagesLocalizer { get; private set; }

        [BindProperty]
        public List<SupplierBankInfo> SupplierBankInfo { get; set; }

        public SupplierBankInfoModal SupplierBankInfoItem { get; set; }

        public IndexModel(IHtmlLocalizer<Resources.SharedMessages> sharedMessagesLocalizer, ISupplierBankServices supplierBankServices)
        {
            _supplierBankServices = supplierBankServices;
            _sharedMessagesLocalizer = sharedMessagesLocalizer;

            SupplierBankInfoItem = new SupplierBankInfoModal();

            var Bank = _supplierBankServices.GetBanksList().Result;
            var Bancos = Bank.Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Nombre,
                                      Text = a.Nombre
                                  }).ToList();

            var Coin = _supplierBankServices.GetCoinsList().Result;
            var Monedas = Coin.Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Descripcion,
                                      Text = a.Descripcion
                                  }).ToList();

            SupplierBankInfoItem.Monedas = Monedas;
            SupplierBankInfoItem.Bancos = Bancos;

        }


        public async Task<ActionResult> OnGetAsync(string cedula)
        
        
        {
            PageMessage msg;
            try
            {
                SupplierBankInfo = new List<SupplierBankInfo>();
                await GetRequiredDataToLoadPage(cedula);
            }
            catch (Exception ex)
            {
                msg = new PageMessage(MessageType.Danger, _sharedMessagesLocalizer.GetString("LoadingDataError", HtmlEncoder.Default.Encode(ex.Message)));
                TempData["PageMessage"] = JsonConvert.SerializeObject(msg);
            }

            return Page();

        }


        private async Task GetRequiredDataToLoadPage( string cedula)
        {
            var supplierBank = await _supplierBankServices.GetSuppliersBank();
            SupplierBankInfo = (from a in supplierBank
                               select new SupplierBankInfo()
                               {
                                   Cedula = a.Cedula,
                                   Banco = a.Banco,
                                   NombreRegistral = a.NombreRegistral,
                                   NombreComercial = a.NombreComercial,
                                   Moneda =a.Moneda,
                                   CodigoSwift = a.CodigoSwift,
                                   CodigoAba = a.CodigoAba,
                                   Direccion = a.Direccion,
                                   Principal = a.Principal,
                                   Secundaria = a.Secundaria,
                                   Auxiliar = a.Auxiliar
                               }).ToList();


           

        }
    }
}
