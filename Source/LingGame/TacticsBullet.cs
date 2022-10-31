using RimWorld;
using UnityEngine;
using Verse;

namespace LingGame;

public class TacticsBullet : Projectile
{
    private readonly int counter = 0;
    private Vector3 BeamDrawPos;

    private float BeamFadeIn;

    private Vector3 BeamLenght;

    private Vector3 BeamLine;

    private Matrix4x4 BeamMatrix;
    private Graphic beamTexture;

    private bool SingleCast = true;

    public override void SpawnSetup(Map map, bool respawningAfterLoad)
    {
        base.SpawnSetup(map, respawningAfterLoad);
        beamTexture = TacticsBulletTex.laser;
    }

    public override void Draw()
    {
        if (SingleCast)
        {
            OneTimeData();
            SingleCast = false;
        }

        DrawProjectile();
    }

    public override void Tick()
    {
        base.Tick();
        if (SingleCast)
        {
            OneTimeData();
            SingleCast = false;
        }

        DrawProjectile();
        if (counter > 5)
        {
            FleckMaker.ThrowMicroSparks(destination, Map);
        }
    }

    private void OneTimeData()
    {
        if (launcher == null)
        {
            return;
        }

        BeamLine = destination - launcher.TrueCenter();
        var normalized = (destination - launcher.TrueCenter()).normalized;
        BeamDrawPos = launcher.TrueCenter() + (normalized / 2f) + (BeamLine / 2f) +
                      new Vector3(0f, (int)def.Altitude, 0f);
        BeamLenght = new Vector3(0.8f, 1f, (destination - origin).magnitude - normalized.magnitude);
        BeamMatrix.SetTRS(BeamDrawPos, Quaternion.LookRotation(BeamLine), BeamLenght);
    }

    private void DrawProjectile()
    {
        FadeInCalc();
        Graphics.DrawMesh(MeshPool.plane10, BeamMatrix,
            FadedMaterialPool.FadedVersionOf(beamTexture.MatSingle, BeamFadeIn), 0);
    }

    private void FadeInCalc()
    {
        if (BeamFadeIn < 1f)
        {
            BeamFadeIn += 0.2f;
        }
    }

    protected override void Impact(Thing hitThing, bool blockedByShield = false)
    {
        var unused = Map;
        base.Impact(hitThing, blockedByShield);
        var battleLogEntry_RangedImpact = new BattleLogEntry_RangedImpact(launcher, hitThing, intendedTarget.Thing,
            equipmentDef, def, targetCoverDef);
        Find.BattleLog.Add(battleLogEntry_RangedImpact);
        if (hitThing == null)
        {
            return;
        }

        var damageDef = def.projectile.damageDef;
        float num = DamageAmount;
        var armorPenetration = ArmorPenetration;
        var y = ExactRotation.eulerAngles.y;
        var thing = launcher;
        var thingDef = equipmentDef;
        var dinfo = new DamageInfo(damageDef, num, armorPenetration, y, thing, null, thingDef,
            DamageInfo.SourceCategory.ThingOrUnknown, intendedTarget.Thing);
        var unused1 = hitThing as Pawn;
        hitThing.TakeDamage(dinfo).AssociateWithLog(battleLogEntry_RangedImpact);
    }
}