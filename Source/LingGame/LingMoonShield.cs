using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace LingGame;

[StaticConstructorOnStartup]
public class LingMoonShield : Apparel
{
    private static readonly SoundDef energyShield_Broken = SoundDef.Named("EnergyShield_Broken");
    private Vector3 impactAngleVect;

    private int lastAbsorbDamageTick = -1;

    private int lastKeepDisplayTick = -9999;
    private float power;

    private int Sleeptick = -1;

    private static int StartingTicksToReset => 600;

    public float MaxPower
    {
        get
        {
            if (Wearer != null)
            {
                return this.GetStatValue(StatDefOf.EnergyShieldEnergyMax) +
                       ((float)Wearer.skills.GetSkill(SkillDefOf.Intellectual).levelInt / 5);
            }

            return this.GetStatValue(StatDefOf.EnergyShieldEnergyMax);
        }
    }

    private float RePowerRate
    {
        get
        {
            if (Wearer != null)
            {
                return (this.GetStatValue(StatDefOf.EnergyShieldRechargeRate) +
                        (Wearer.health.capacities.GetLevel(PawnCapacityDefOf.Consciousness) / 4f)) / 60f;
            }

            return this.GetStatValue(StatDefOf.EnergyShieldRechargeRate) / 60f;
        }
    }

    public float Power => power;

    private ShieldState shieldState => Sleeptick > 0 ? ShieldState.Resetting : ShieldState.Active;

    private bool ShouldDisplay
    {
        get
        {
            var wearer = Wearer;
            if (!wearer.Spawned || wearer.Downed || wearer.Dead)
            {
                return false;
            }

            if (wearer.InAggroMentalState)
            {
                return true;
            }

            if (wearer.Drafted)
            {
                return true;
            }

            if (wearer.Faction.HostileTo(Faction.OfPlayer) && !wearer.IsPrisoner)
            {
                return true;
            }

            return Find.TickManager.TicksGame < lastKeepDisplayTick + 300 || wearer.IsFighting();
        }
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref power, "power");
        Scribe_Values.Look(ref Sleeptick, "Sleeptick", -1);
        Scribe_Values.Look(ref lastKeepDisplayTick, "lastKeepDisplayTick");
    }

    public override IEnumerable<Gizmo> GetWornGizmos()
    {
        if (Find.Selector.SingleSelectedThing == Wearer)
        {
            yield return new Gizmo_LingMoon_ShieldStatus
            {
                shield = this
            };
        }
    }

    protected override void Tick()
    {
        base.Tick();
        if (Wearer == null)
        {
            power = 0f;
            return;
        }

        switch (shieldState)
        {
            case ShieldState.Resetting:
            {
                Sleeptick--;
                if (Sleeptick <= 0)
                {
                    Reset();
                }

                break;
            }
            case ShieldState.Active:
            {
                power += RePowerRate;
                if (power >= MaxPower)
                {
                    power = MaxPower;
                }

                break;
            }
        }
    }

    public override bool CheckPreAbsorbDamage(DamageInfo dinfo)
    {
        if (dinfo.Instigator == Wearer)
        {
            AbsorbedDamage(dinfo);
            return true;
        }

        if (dinfo.Def == DamageDefOf.SurgicalCut)
        {
            return false;
        }

        if (dinfo.Def == DamageDefOf.EMP)
        {
            return true;
        }

        if (Wearer.Downed)
        {
            return false;
        }

        if (shieldState == ShieldState.Resetting)
        {
            return false;
        }

        AbsorbedDamage(dinfo);
        power -= 1f;
        if (power <= 0f)
        {
            Break();
        }

        return true;
    }

    private void AbsorbedDamage(DamageInfo dinfo)
    {
        if (Wearer.Map != null)
        {
            SoundDefOf.EnergyShield_AbsorbDamage.PlayOneShot(new TargetInfo(Wearer.Position, Wearer.Map));
            impactAngleVect = Vector3Utility.HorizontalVectorFromAngle(dinfo.Angle);
            var vector = Wearer.TrueCenter() + (impactAngleVect.RotatedBy(180f) * 0.5f);
            var num = Mathf.Min(10f, 2f + (dinfo.Amount / 10f));
            FleckMaker.Static(vector, Wearer.Map, FleckDefOf.ExplosionFlash, num);
            var num2 = (int)num;
            for (var i = 0; i < num2; i++)
            {
                FleckMaker.ThrowDustPuff(vector, Wearer.Map, Rand.Range(0.8f, 1.2f));
            }
        }

        lastAbsorbDamageTick = Find.TickManager.TicksGame;
    }

    private void Break()
    {
        if (Wearer.Map != null)
        {
            energyShield_Broken.PlayOneShot(new TargetInfo(Wearer.Position, Wearer.Map));
            FleckMaker.Static(Wearer.TrueCenter(), Wearer.Map, FleckDefOf.ExplosionFlash, 12f);
            for (var i = 0; i < 6; i++)
            {
                var vector = Wearer.TrueCenter() + (Vector3Utility.HorizontalVectorFromAngle(Rand.Range(0, 360)) *
                                                    Rand.Range(0.3f, 0.6f));
                FleckMaker.ThrowDustPuff(vector, Wearer.Map, Rand.Range(0.8f, 1.2f));
            }
        }

        power = 0f;
        Sleeptick = StartingTicksToReset;
    }

    private void Reset()
    {
        if (Wearer.Spawned)
        {
            SoundDefOf.EnergyShield_Reset.PlayOneShot(new TargetInfo(Wearer.Position, Wearer.Map));
            FleckMaker.ThrowLightningGlow(Wearer.TrueCenter(), Wearer.Map, 3f);
        }

        Sleeptick = -1;
        power = 0.1f;
    }

    public override void DrawWornExtras()
    {
        if (shieldState != ShieldState.Active || !ShouldDisplay)
        {
            return;
        }

        var num = Mathf.Lerp(1.5f, 1.7f, power);
        var drawPos = Wearer.Drawer.DrawPos;
        drawPos.y = AltitudeLayer.PawnUnused.AltitudeFor();
        var num2 = Find.TickManager.TicksGame - lastAbsorbDamageTick;
        if (num2 < 8)
        {
            var num3 = (8 - num2) / 8f * 0.05f;
            drawPos += impactAngleVect * num3;
            num -= num3;
        }

        var angle = Find.TickManager.TicksGame % 360f;
        var num4 = (Mathf.Sin((float)Find.TickManager.TicksGame / 30) / 3f) + 0.66f;
        var s = new Vector3(num, 1f, num);
        var matrix = default(Matrix4x4);
        matrix.SetTRS(drawPos, Quaternion.AngleAxis(angle, Vector3.up), s);
        Graphics.DrawMesh(MeshPool.plane10, matrix,
            MaterialPool.MatFrom("Other/ShieldBubble", ShaderDatabase.Transparent,
                new Color(num4, num4, num4, num4)), 0);
    }
}