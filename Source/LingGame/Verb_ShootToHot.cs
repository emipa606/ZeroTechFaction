using Verse;

namespace LingGame;

public class Verb_ShootToHot : Verb_Shoot
{
    private int aassdw;

    protected override bool TryCastShot()
    {
        if (EquipmentSource.GetComp<LingMoonRoarComp>().ShootAmount > 50f)
        {
            if (caster.Faction.IsPlayer)
            {
                return false;
            }

            aassdw++;
            if (aassdw <= 200)
            {
                return false;
            }

            aassdw = 0;
            EquipmentSource.GetComp<LingMoonRoarComp>().ShootAmount = 0f;

            return false;
        }

        var result = base.TryCastShot();
        EquipmentSource.GetComp<LingMoonRoarComp>().ShootAmount += 1f;
        return result;
    }
}