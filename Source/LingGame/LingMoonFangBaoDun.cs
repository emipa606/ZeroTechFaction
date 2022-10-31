using RimWorld;
using Verse;

namespace LingGame;

public class LingMoonFangBaoDun : Apparel
{
    public override bool CheckPreAbsorbDamage(DamageInfo dinfo)
    {
        HitPoints -= (int)dinfo.Amount;
        if (HitPoints > 0)
        {
            return true;
        }

        Wearer.apparel.Remove(this);
        Destroy();

        return true;
    }
}