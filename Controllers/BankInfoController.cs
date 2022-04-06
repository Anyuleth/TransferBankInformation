using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TranferBankInformation.Domain.DTO.SupplierBank;
using TranferBankInformation.Services.Services;
using TransferBankInformation.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TransferBankInformation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankInfoController : ControllerBase
    {

        private readonly ISupplierBankServices _supplierBankServices;

        public BankInfoController(ISupplierBankServices supplierBankServices)
        {
            _supplierBankServices = supplierBankServices;
        }
        
        // GET api/<BankInfoController>/5
        [HttpGet("{cedula}")]
        public async Task<ActionResult> GetAsync(string cedula)
        {
            var supplierBank = await _supplierBankServices.GetSuppliersBank(cedula);
            var SupplierBankInfo = (from a in supplierBank
                                select new SupplierBankInfo()
                                {
                                    Cedula = a.Cedula,
                                    Banco = a.Banco,
                                    NombreRegistral = a.NombreRegistral,
                                    NombreComercial = a.NombreComercial,
                                    Moneda = a.Moneda,
                                    CodigoSwift = a.CodigoSwift,
                                    CodigoAba = a.CodigoAba,
                                    Direccion = a.Direccion,
                                    Principal = a.Principal,
                                    Secundaria = a.Secundaria,
                                    Auxiliar = a.Auxiliar
                                }).ToList();
            return Ok(await _supplierBankServices.SearchSupplierBank(cedula));
        }

        // POST api/<BankInfoController>
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] SupplierBankInfoModal datos)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var t = datos.Cedula;
                    var registerResponse = await _supplierBankServices.SaveSupplierBank(new SupplierBankDTO()
                    {
                        Cedula = datos.Cedula,
                        Banco = datos.Banco,
                        NombreRegistral = datos.NombreRegistral,
                        NombreComercial = datos.NombreComercial,
                        Moneda = datos.Moneda,
                        CodigoSwift = datos.CodigoSwift,
                        CodigoAba = datos.CodigoAba,
                        Direccion = datos.Direccion,
                        Principal = datos.Principal,
                        Secundaria = datos.Secundaria,
                        Auxiliar = datos.Auxiliar

                    });

                    //msg = new PageMessage(registerResponse.Type.Equals("Success") ? MessageType.Success : MessageType.Danger, registerResponse.Message);
                    //TempData["PageMessage"] = JsonConvert.SerializeObject(msg);
                    //return RedirectToPage("./Index");




                }
            }
            catch (Exception ex)
            {
                //msg = new PageMessage(MessageType.Danger, _sharedMessagesLocalizer.GetString("RegisterErrorMessage", HtmlEncoder.Default.Encode(ex.Message)));
                //TempData["PageMessage"] = JsonConvert.SerializeObject(msg);
                //return RedirectToPage("Create", new { cedula = SupplierBankInfo.Cedula });
            }

            return Ok();
        }

    }
}
