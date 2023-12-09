using System.IO;
using System.Reflection;
using DreamSlimes.Patchs;
using DreamSlimes.Slimes;
using SRML;
using SRML.SR;
using SRML.SR.Translation;
using UnityEngine;

namespace DreamSlimes
{
    public class Main : ModEntryPoint
    {
        private static Stream manifestResourceStream;
        public static AssetBundle assetBundle;
        public static Color color1 = new Color32(0xe4, 0xb0, 0xfc, 0xff);
        public static Color color2 = new Color32(0xb9, 0xb0, 0xfc, 0xff);
        public static Color color3 = new Color32(0xa0, 0xa0, 0xff, 0xff);

        public override void PreLoad()
        {
            HarmonyInstance.PatchAll();
            manifestResourceStream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("DreamSlimes.dreamslimes1");
            assetBundle = AssetBundle.LoadFromStream(manifestResourceStream);

            PediaRegistry.RegisterIdentifiableMapping(PediaDirector.Id.PLORTS, Id.DREAM_SLIME);
            PediaRegistry.RegisterIdentifiableMapping(Id.DREAM_SLIME_ENTRY, Id.DREAM_PLORT);
            PediaRegistry.SetPediaCategory(Id.DREAM_SLIME_ENTRY, PediaRegistry.PediaCategory.SLIMES);
            new SlimePediaEntryTranslation(Id.DREAM_SLIME_ENTRY)
                .SetTitleTranslation("Dream Slimes")
                .SetIntroTranslation("Even slimes have dreams…")
                .SetDietTranslation("Plort and Slimes")
                .SetFavoriteTranslation("Sabber Plort")
                .SetSlimeologyTranslation(
                    "Its unknown from which dimension they came, but it's possible the same as Quantum Slimes go to when their are shifting. You can obtain one, by throwing a Quantum Slime into the Chicken Cloner.")
                .SetRisksTranslation("There are no specific risk, coming from Dream Slimes")
                .SetPlortonomicsTranslation(
                    "Dream Plort can't be sold at the market, because it's useless for non-Slime Ranchers. If you give it to any Slime, their dream come true, very true…");
        }

        public override void Load()
        {
            CreatePlort.CreateDreamPlort();
            CreateSlime.CreateDreamSlime();
            AddEatMap.PatchEatMaps();
            ConsoleInstance.Log("Loaded Dream SLimes successfully");
        }
    }
}