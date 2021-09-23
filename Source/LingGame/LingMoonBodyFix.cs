using RimWorld;
using Verse;

namespace LingGame
{
    public class LingMoonBodyFix : Apparel
    {
        public override void Tick()
        {
            base.Tick();
            if (Wearer == null)
            {
                return;
            }

            if (Wearer.RaceProps.Humanlike && Wearer.Faction != Faction.OfInsects)
            {
                GetSkills();
                GetTrait();
                AddHediff1();
                GetBodyTape();
            }

            Wearer.apparel.Remove(this);
            Destroy();
        }

        protected virtual void AddHediff1()
        {
            Wearer.health.AddHediff(ZeroTechFactionDefOf.WakeUpHigh);
            Wearer.health.AddHediff(ZeroTechFactionDefOf.LuciferiumHigh);
            Wearer.health.AddHediff(ZeroTechFactionDefOf.GoJuiceHigh);
        }

        private void GetBodyTape()
        {
            if (Wearer.story.bodyType == BodyTypeDefOf.Fat || Wearer.story.bodyType == BodyTypeDefOf.Hulk)
            {
                Wearer.story.bodyType = BodyTypeDefOf.Thin;
            }

            Wearer.Drawer.renderer.graphics.ResolveAllGraphics();
        }

        private void GetSkills()
        {
            var skills = Wearer.skills.skills;
            foreach (var item in skills)
            {
                if (item.Level is >= 0 and <= 10)
                {
                    item.Level += 10;
                    switch (item.Level)
                    {
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                            item.passion = Passion.Minor;
                            break;
                        case 16:
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                            item.passion = Passion.Major;
                            break;
                    }
                }
            }
        }

        private void GetTrait()
        {
            if (Wearer.story.traits.allTraits.Count > 2)
            {
                Wearer.story.traits.allTraits.Remove(Wearer.story.traits.allTraits[1]);
            }

            if (!Wearer.story.traits.HasTrait(TraitDefOf.Beauty))
            {
                if (Rand.Chance(0.5f))
                {
                    var trait = new Trait(TraitDefOf.Beauty, 1);
                    Wearer.story.traits.GainTrait(trait);
                }
                else if (Rand.Chance(0.5f))
                {
                    var trait2 = new Trait(TraitDefOf.Beauty, 2);
                    Wearer.story.traits.GainTrait(trait2);
                }
            }

            if (Wearer.story.traits.HasTrait(TraitDefOf.PsychicSensitivity))
            {
                return;
            }

            if (Rand.Chance(0.5f))
            {
                var trait3 = new Trait(TraitDefOf.PsychicSensitivity, 1);
                Wearer.story.traits.GainTrait(trait3);
            }
            else if (Rand.Chance(0.5f))
            {
                var trait4 = new Trait(TraitDefOf.PsychicSensitivity, 2);
                Wearer.story.traits.GainTrait(trait4);
            }
        }

        protected BodyPartRecord ccc(string bodypart)
        {
            BodyPartRecord result = null;
            foreach (var notMissingPart in Wearer.health.hediffSet.GetNotMissingParts())
            {
                if (notMissingPart.def.defName.Contains(bodypart))
                {
                    result = notMissingPart;
                }
            }

            return result;
        }
    }
}