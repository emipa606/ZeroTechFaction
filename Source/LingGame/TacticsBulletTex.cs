using Verse;

namespace LingGame;

[StaticConstructorOnStartup]
public class TacticsBulletTex
{
    public static Graphic laser =
        GraphicDatabase.Get<Graphic_Single>("Ling/Projectile/RainbawLuncer", ShaderDatabase.TransparentPostLight);
}