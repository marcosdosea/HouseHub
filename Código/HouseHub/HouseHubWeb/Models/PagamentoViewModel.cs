using Core;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HouseHubWeb.Models
{
    public class PagamentoViewModel
    {
        public uint Id { get; set; }
        public string FormaPagamento { get; set; } = null!;
        public uint IdLocacao { get; set; }

        public List<SelectListItem> FormasDePagamento = new List<SelectListItem>
        {
            new SelectListItem { Value = "Dinheiro", Text = "Dinheiro" },
            new SelectListItem { Value = "Cartão de Crédito", Text = "Cartão de Credito" },
            new SelectListItem { Value = "Cartão de Débito", Text = "Cartão de Debito" },
            new SelectListItem { Value = "Pix", Text = "Pix" },
            new SelectListItem { Value = "Boleto", Text = "Boleto" },
            new SelectListItem { Value = "Transferência Bancária", Text = "Transferência Bancaria" }
        };
    }
}
