using RimWorld;
using Verse;

namespace LingGame;

public class Verb_MeleeAttackDamageAndJump : Verb_MeleeAttackDamage
{
    protected override bool TryCastShot()
    {
        Log.Message("卧槽");
        var map = caster.Map;
        var cell = currentTarget.Cell;
        CasterPawn.DeSpawn(DestroyMode.Refund);
        GenSpawn.Spawn(CasterPawn, cell, map);
        return base.TryCastShot();
    }

    private bool HorDistOf(IntVec3 intVec3, IntVec3 otherLoc, float minDist, float maxDist)
    {
        float num = intVec3.x - otherLoc.x;
        float num2 = intVec3.z - otherLoc.z;
        return minDist * minDist <= (num * num) + (num2 * num2) && (num * num) + (num2 * num2) <= maxDist * maxDist;
    }
}