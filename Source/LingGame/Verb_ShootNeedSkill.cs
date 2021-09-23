using RimWorld;
using Verse;

namespace LingGame
{
    public class Verb_ShootNeedSkill : Verb_Shoot
    {
        protected override bool TryCastShot()
        {
            if (!CasterPawn.RaceProps.Humanlike || CasterPawn.skills.GetSkill(SkillDefOf.Shooting).levelInt >= 10 ||
                !CasterPawn.Faction.IsPlayer)
            {
                return base.TryCastShot();
            }

            MoteMaker.ThrowText(caster.DrawPos, caster.Map, "Verb_ShootNeedSkillLowSkill".Translate());
            return false;
        }
    }
}