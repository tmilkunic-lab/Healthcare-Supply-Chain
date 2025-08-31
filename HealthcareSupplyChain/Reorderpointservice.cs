using System;

namespace HealthcareSupplyChain.Core;

public class ReorderPointService : IReorderPointService
{
    public int CalculateReorderPoint(double averageDailyUsage, int leadTimeDays, int safetyStockDays)
    {
        if (averageDailyUsage < 0) throw new ArgumentOutOfRangeException(nameof(averageDailyUsage));
        if (leadTimeDays < 0) throw new ArgumentOutOfRangeException(nameof(leadTimeDays));
        if (safetyStockDays < 0) throw new ArgumentOutOfRangeException(nameof(safetyStockDays));
        return (int)Math.Ceiling(averageDailyUsage * (leadTimeDays + safetyStockDays));
    }

    public int CalculateProjectedDaysLeft(int onHandQty, double averageDailyUsage)
    {
        if (onHandQty < 0) throw new ArgumentOutOfRangeException(nameof(onHandQty));
        if (averageDailyUsage <= 0) return int.MaxValue;
        return (int)Math.Floor(onHandQty / averageDailyUsage);
    }

    public bool ShouldReorder(int onHandQty, double averageDailyUsage, int leadTimeDays, int safetyStockDays)
    {
        var rop = CalculateReorderPoint(averageDailyUsage, leadTimeDays, safetyStockDays);
        return onHandQty <= rop;
    }
}
