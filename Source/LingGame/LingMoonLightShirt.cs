using RimWorld;
using Verse;

namespace LingGame;

public class LingMoonLightShirt : Apparel
{
    public override bool CheckPreAbsorbDamage(DamageInfo dinfo)
    {
        if (dinfo.Instigator != null && dinfo.Instigator.Faction == Wearer.Faction)
        {
            return true;
        }

        return base.CheckPreAbsorbDamage(dinfo);
    }
}