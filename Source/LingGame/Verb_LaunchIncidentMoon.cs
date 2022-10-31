using RimWorld;
using Verse;

namespace LingGame;

public class Verb_LaunchIncidentMoon : Verb
{
    protected override bool TryCastShot()
    {
        var incidentDef = IncidentDefOf.RaidEnemy;
        var faction = Faction.OfMechanoids;
        if (CasterPawn.Faction != null)
        {
            faction = CasterPawn.Faction;
            if (caster.Faction.IsPlayer)
            {
                faction = Faction.OfMechanoids;
            }
        }

        if (!faction.HostileTo(Faction.OfPlayer) && incidentDef == IncidentDefOf.RaidEnemy)
        {
            incidentDef = IncidentDefOf.RaidFriendly;
        }

        var incidentParms = StorytellerUtility.DefaultParmsNow(incidentDef.category, caster.Map);
        incidentParms.faction = faction;
        incidentParms.points *= 2f;
        if (incidentParms.points <= 1000f)
        {
            incidentParms.points = 1000f;
        }

        incidentDef.Worker.TryExecute(incidentParms);
        EquipmentSource.Destroy();
        return true;
    }
}