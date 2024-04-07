using Verse;

namespace LingGame;

public class LingMoonRoarComp : ThingComp
{
    public float ShootAmount;

    private int Tiick;

    public override void CompTick()
    {
        base.CompTick();
        Tiick++;
        if (Tiick <= 30)
        {
            return;
        }

        if (ShootAmount > 0f)
        {
            ShootAmount -= 1f;
        }

        Tiick = 0;
    }

    public override string TransformLabel(string label)
    {
        return ShootAmount > 0f ? $"{base.TransformLabel(label)}(ToHot{ShootAmount}/50)" : base.TransformLabel(label);
    }
}