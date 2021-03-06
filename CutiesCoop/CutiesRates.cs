using Keplerth;
using System;

namespace CutiesCoop
{
    public static class CutiesRates
    {

        public static void ApplyDropRate(ref ItemData item)
        {
            if (item.count > 0 && IsDropRateAffected(item))
            {
                item.count *= CutiesCoopInit.config.dropRate;
            }
        }

        private static bool IsDropRateAffected(ItemData item)
        {
            return Array.Exists(CutiesCoopInit.config.allowedItemIDs, element => element == item.id);
        }

    }
}
