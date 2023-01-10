using HarmonyLib;
using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

namespace DreamSlimes.Patchs
{
    [HarmonyPatch(typeof(GadgetChickenCloner), "OnTriggerEnter")]
    internal static class GadgetChickenClonerPatch
    {
        public static void Postfix(GadgetChickenCloner __instance, Collider collider)        {
            if (Identifiable.GetId(collider.gameObject) == Identifiable.Id.QUANTUM_SLIME)
            {
                GameObject collidingGO = collider.gameObject;
                GameObject dreamSLimeInstance = SRBehaviour.InstantiateActor(SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Id.DREAM_SLIME), collider.gameObject.GetComponent<RegionMember>().setId, false);
                dreamSLimeInstance.transform.position = collidingGO.transform.position;
                dreamSLimeInstance.transform.rotation = collidingGO.transform.rotation;
                SRBehaviour.SpawnAndPlayFX(collidingGO.GetComponent<SlimeEat>().TransformFX, dreamSLimeInstance);
                Quaternion quaternion = Quaternion.LookRotation((Vector3.Angle((collider).gameObject.GetComponent<Rigidbody>().velocity, (__instance).transform.forward) <= 90f) ? Vector3.forward : Vector3.back);
                SRBehaviour.SpawnAndPlayFX(__instance.onSuccessFX, (__instance).gameObject, Vector3.zero, quaternion);
                Rigidbody colliderRigidbody = collidingGO.GetComponent<Rigidbody>();
                Rigidbody dreamSlimeInstanceRigidbody = dreamSLimeInstance.GetComponent<Rigidbody>();
                dreamSlimeInstanceRigidbody.velocity = colliderRigidbody.velocity;
                dreamSlimeInstanceRigidbody.angularVelocity = (colliderRigidbody.angularVelocity);
                __instance.GetComponent<Animator>().SetBool(Animator.StringToHash("ACTIVE"), true);
                Destroyer.DestroyActor(collidingGO, "GadgetChickenCloner", true);
                __instance.StartCoroutine(DisableSpin(__instance));
            }
        }
        
        public static System.Collections.IEnumerator DisableSpin(GadgetChickenCloner __instance)
        {
            yield return new WaitForSeconds(0.35f);
            __instance.GetComponent<Animator>().SetBool(Animator.StringToHash("ACTIVE"), false);
        }
    }
}