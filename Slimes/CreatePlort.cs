using SRML.SR;
using SRML.Utils;
using UnityEngine;

namespace DreamSlimes.Slimes
{
    public class CreatePlort
    {
        public static void CreateDreamPlort()
        {
            var go =
                PrefabUtils.CopyPrefab(
                    SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Identifiable.Id.PINK_PLORT));
            go.GetComponent<Identifiable>().id = Id.DREAM_PLORT;
            go.name = "Dream Plort";
            LookupRegistry.RegisterIdentifiablePrefab(go);
            go.GetComponent<MeshRenderer>().material = Object.Instantiate(go.GetComponent<MeshRenderer>().material);
            go.GetComponent<MeshRenderer>().material.SetColor("_TopColor", Main.color1);
            go.GetComponent<MeshRenderer>().material.SetColor("_MiddleColor", Main.color2);
            go.GetComponent<MeshRenderer>().material.SetColor("_BottomColor", Main.color3);
            SlimeEat.FoodGroup.PLORTS.UnregisterId(Id.DREAM_PLORT);
            go.GetComponent<MeshRenderer>().material.SetColor("_CrackColor", Main.color1);

            AmmoRegistry.RegisterAmmoPrefab(PlayerState.AmmoMode.DEFAULT,
                SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Id.DREAM_PLORT));
            SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Id.DREAM_PLORT).GetComponent<Vacuumable>().size =
                Vacuumable.Size.NORMAL;
            TranslationPatcher.AddActorTranslation("l.dream_plort", "Dream Plort");

            var icon = Main.assetBundle.LoadAsset<Sprite>("dreamPlortIcon");
            LookupRegistry.RegisterVacEntry(Id.DREAM_PLORT, Main.color1, icon);
            DroneRegistry.RegisterBasicTarget(Id.DREAM_PLORT);
        }
    }
}