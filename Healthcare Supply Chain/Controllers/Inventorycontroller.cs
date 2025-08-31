using HealthcareSupplyChain.Core;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare_Supply_Chain.Web.Controllers
{
    public class InventoryController : Controller

    {
        private readonly IReorderPointService _reorder;
        public InventoryController(IReorderPointService reorder) => _reorder = reorder;

        [HttpGet("/inventory/reorder")]
        public IActionResult Reorder(string item, int onHand, double adu, int lead, int safety = 3)
        {
            var rop = _reorder.CalculateReorderPoint(adu, lead, safety);
            var should = _reorder.ShouldReorder(onHand, adu, lead, safety);
            var daysLeft = _reorder.CalculateProjectedDaysLeft(onHand, adu);

            // Example item is a mask used for isolation patients
            return Ok(new
            {
                item = string.IsNullOrWhiteSpace(item) ? "N95 Masks" : item,
                reorderPoint = rop,
                projectedDaysLeft = daysLeft,
                shouldReorder = should
            });
        }
    }
}