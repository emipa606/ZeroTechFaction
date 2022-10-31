using Verse;

namespace LingGame;

public class LingMoonSuoTou : DamageWorker_AddInjury
{
    public override DamageResult Apply(DamageInfo dinfo, Thing thing)
    {
        if (thing is Pawn pawn)
        {
            dinfo.SetHitPart(pawn.health.hediffSet.GetBrain());
        }

        return base.Apply(dinfo, thing);
    }
}