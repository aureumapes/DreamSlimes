using SRML.Console;
using SRML.SR.Utils;
using SRML.Utils;
using UnityEngine;

namespace DreamSlimes.Patchs
{
    public class AddEatMap
    {
        public static void PatchEatMaps()
        {
            Identifiable.Id[] slimes =
            {
                Identifiable.Id.PINK_SLIME,
                Identifiable.Id.ROCK_SLIME,
                Identifiable.Id.PHOSPHOR_SLIME,
                Identifiable.Id.TABBY_SLIME,
                Identifiable.Id.RAD_SLIME,
                Identifiable.Id.HONEY_SLIME,
                Identifiable.Id.BOOM_SLIME,
                Identifiable.Id.PUDDLE_SLIME,
                Identifiable.Id.FIRE_SLIME,
                Identifiable.Id.CRYSTAL_SLIME,
                Identifiable.Id.QUANTUM_SLIME,
                Identifiable.Id.DERVISH_SLIME,
                Identifiable.Id.HUNTER_SLIME,
                Identifiable.Id.MOSAIC_SLIME,
                Identifiable.Id.TANGLE_SLIME,
                Identifiable.Id.SABER_SLIME
            };
            foreach (Identifiable.Id slime in slimes)
            {
                SlimeDefinition slimeDefinition =
                    SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(slime);
                slimeDefinition.AddEatMapEntry(new SlimeDiet.EatMapEntry
                {
                    eats = Id.DREAM_PLORT,
                    becomesId = slimeDefinition.FavoriteToys[0],
                });
            }
            SlimeDefinition tarrDef =  SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.TARR_SLIME);
            tarrDef.AddEatMapEntry(new SlimeDiet.EatMapEntry
            {
                eats = Id.DREAM_PLORT,
                becomesId = Identifiable.Id.PINK_SLIME
            });
        }
    }
}