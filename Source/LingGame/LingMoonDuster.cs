using RimWorld;
using Verse;

namespace LingGame
{
    public class LingMoonDuster : Apparel
    {
        public override bool CheckPreAbsorbDamage(DamageInfo dinfo)
        {
            if (!Wearer.Faction.IsPlayer && Rand.Chance(0.5f))
            {
                return true;
            }

            return base.CheckPreAbsorbDamage(dinfo);
        }
    }
}