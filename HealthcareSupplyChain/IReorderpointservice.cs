namespace HealthcareSupplyChain.Core;

public interface IReorderPointService
{
    // Gather all necessary information needed for calulation
    int CalculateReorderPoint(double averageDailyUsage, int leadTimeDays, int safetyStockDays);
    int CalculateProjectedDaysLeft(int onHandQty, double averageDailyUsage);
    bool ShouldReorder(int onHandQty, double averageDailyUsage, int leadTimeDays, int safetyStockDays);
}
