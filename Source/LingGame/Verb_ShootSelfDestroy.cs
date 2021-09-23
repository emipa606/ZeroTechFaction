using Verse;

namespace LingGame
{
    public class Verb_ShootSelfDestroy : Verb_Shoot
    {
        protected override bool TryCastShot()
        {
            var result = base.TryCastShot();
            if (!CasterPawn.Faction.IsPlayer)
            {
                return result;
            }

            EquipmentSource.HitPoints--;
            if (EquipmentSource.HitPoints <= 0)
            {
                EquipmentSource.Destroy();
            }

            return result;
        }
    }
}