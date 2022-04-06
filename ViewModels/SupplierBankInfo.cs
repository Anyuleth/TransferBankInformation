using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransferBankInformation.ViewModels
{
    public class SupplierBankInfo
    {
        public string Cedula { get; set; }
        public string Banco { get; set; }
        public string NombreRegistral { get; set; }
        public string NombreComercial { get; set; }
        public string Moneda { get; set; }
        public string CodigoSwift { get; set; }
        public string CodigoAba { get; set; }
        public string Direccion { get; set; }
        public string Principal { get; set; }
        public string Secundaria { get; set; }
        public string Auxiliar { get; set; }
    }

    public class SupplierInfo
    {
        public string Cedula{ get; set; }
        public string Nombre { get; set; }
        public string Pais { get; set; }

    }

    public class SupplierBankInfoModal
    {
        [Required]
        public string Cedula { get; set; }
        public string Banco { get; set; }
        public string NombreRegistral { get; set; }
        public string NombreComercial { get; set; }
        public string Moneda { get; set; }
        public string CodigoSwift { get; set; }
        public string CodigoAba { get; set; }
        public string Direccion { get; set; }
        public string Principal { get; set; }
        public string Secundaria { get; set; }
        public string Auxiliar { get; set; }

        public List<SelectListItem> Bancos { get; set; }
        public List<SelectListItem> Monedas { get; set; }
    }
}
