using Verse;

namespace LingGame;

public class LingMoonChaXue : DamageWorker_AddInjury
{
    public override DamageResult Apply(DamageInfo dinfo, Thing thing)
    {
        if (dinfo.Instigator is not Pawn pawn)
        {
            return base.Apply(dinfo, thing);
        }

        foreach (var hediff in pawn.health.hediffSet.hediffs)
        {
            if (hediff is not Hediff_Injury)
            {
                continue;
            }

            hediff.Heal(dinfo.Amount);
            break;
        }

        return base.Apply(dinfo, thing);
    }
}