using SRML.SR;
using SRML.Utils;
using UnityEngine;

namespace DreamSlimes.Slimes
{
    public class CreateSlime
    {
        public static void CreateDreamSlime()
        {
            SlimeDefinition slimeByIdentifiableId =
                SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.PINK_SLIME);
            SlimeDefinition slimeDefinition = (SlimeDefinition)PrefabUtils.DeepCopyObject(slimeByIdentifiableId);
            slimeDefinition.AppearancesDefault = new SlimeAppearance[1];

            slimeDefinition.Diet.Produces = new[] { Id.DREAM_PLORT };
            slimeDefinition.Diet.MajorFoodGroups = new[] { SlimeEat.FoodGroup.PLORTS };
            slimeDefinition.Diet.Favorites = new[] { Identifiable.Id.SABER_PLORT };
            slimeDefinition.Diet.EatMap?.Clear();

            slimeDefinition.CanLargofy = false;
            slimeDefinition.FavoriteToys = System.Array.Empty<Identifiable.Id>();
            slimeDefinition.Name = "Dream Slime";
            slimeDefinition.IdentifiableId = Id.DREAM_SLIME;

            GameObject go =
                PrefabUtils.CopyPrefab(
                    SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Identifiable.Id.PINK_SLIME));
            go.name = ("Dream_Slime");
            go.GetComponent<PlayWithToys>().slimeDefinition = slimeDefinition;
            go.GetComponent<SlimeAppearanceApplicator>().SlimeDefinition = slimeDefinition;
            go.GetComponent<SlimeEat>().slimeDefinition = slimeDefinition;
            go.GetComponent<Identifiable>().id = Id.DREAM_SLIME;
            go.GetComponent<Vacuumable>().size = Vacuumable.Size.LARGE;
            go.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            Object.Destroy(go.GetComponent<PinkSlimeFoodTypeTracker>());

            SlimeAppearance slimeAppearance =
                (SlimeAppearance)PrefabUtils.DeepCopyObject(slimeByIdentifiableId.AppearancesDefault[0]);
            slimeDefinition.AppearancesDefault[0] = slimeAppearance;
            SlimeAppearanceStructure[] structures = slimeAppearance.Structures;
            SlimeAppearanceStructure[] structures2 = structures;
            foreach (SlimeAppearanceStructure structure in structures2)
            {
                Material[] defaultMaterials = structure.DefaultMaterials;
                if (defaultMaterials != null && defaultMaterials.Length != 0)
                {
                    Material val8 = Object.Instantiate(SRSingleton<GameContext>.Instance
                        .SlimeDefinitions.GetSlimeByIdentifiableId(Identifiable.Id.PINK_SLIME).AppearancesDefault[0]
                        .Structures[0]
                        .DefaultMaterials[0]);
                    val8.SetColor("_TopColor", Main.color1);
                    val8.SetColor("_MiddleColor", Main.color2);
                    val8.SetColor("_BottomColor", Main.color3);
                    val8.SetFloat("_Shininess", 1f);
                    val8.SetFloat("_Gloss", 0f);
                    structure.DefaultMaterials[0] = val8;
                }
            }

            SlimeExpressionFace[] expressionFaces = slimeAppearance.Face.ExpressionFaces;
            foreach (SlimeExpressionFace val9 in expressionFaces)
            {
                if (val9.Mouth)
                {
                    val9.Mouth.SetColor("_MouthBot",
                        new Color32(0x82, 0x82, 0xef, 0xff));
                    val9.Mouth.SetColor("_MouthMid",
                        new Color32(0xa0, 0xa0, 0xff, 0xff));
                    val9.Mouth.SetColor("_MouthTop",
                        new Color32(0xa0, 0xa0, 0xff, 0xff));
                }

                if (val9.Eyes)
                {
                    val9.Eyes.SetColor("_EyeRed", new Color(26, 45, 56, byte.MaxValue));
                    val9.Eyes.SetColor("_EyeGreen", new Color32(94, 141, 160, byte.MaxValue));
                    val9.Eyes.SetColor("_EyeBlue", new Color32(40, 60, 68, byte.MaxValue));
                }
            }

            slimeAppearance.Face.OnEnable();
            SlimeAppearance val10 = slimeAppearance;
            SlimeAppearance.Palette colorPalette = default(SlimeAppearance.Palette);
            colorPalette.Top = (new Color32(252, 254, byte.MaxValue, byte.MaxValue));
            colorPalette.Middle = (new Color32(210, 236, 247, byte.MaxValue));
            colorPalette.Bottom = (new Color32(124, 138, 163, byte.MaxValue));
            val10.ColorPalette = colorPalette;
            go.GetComponent<SlimeAppearanceApplicator>().Appearance = slimeAppearance;
            LookupRegistry.RegisterIdentifiablePrefab(go);
            SlimeRegistry.RegisterSlimeDefinition(slimeDefinition);
            AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT,
                SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Id.DREAM_SLIME));
            Sprite val11 = slimeAppearance.Icon = Main.assetBundle.LoadAsset<Sprite>("dreamPlortIcon");
            slimeAppearance.ColorPalette.Ammo = (new Color32(210, 236, 247, 225));
            SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Id.DREAM_SLIME)
                .GetComponent<Vacuumable>()
                .size = Vacuumable.Size.LARGE;
            TranslationPatcher.AddActorTranslation("l.dream_slime", "Dream Slime");
            PediaRegistry.RegisterIdEntry(Id.DREAM_SLIME_ENTRY, val11);
        }
    }
}