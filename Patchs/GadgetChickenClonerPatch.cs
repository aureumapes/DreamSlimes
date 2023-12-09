using System.Collections;
using HarmonyLib;
using MonomiPark.SlimeRancher.Regions;
using UnityEngine;

namespace DreamSlimes.Patchs
{
    [HarmonyPatch(typeof(GadgetChickenCloner), "OnTriggerEnter")]
    internal static class GadgetChickenClonerPatch
    {
        public static void Postfix(GadgetChickenCloner __instance, Collider collider)
        {
            if (Identifiable.GetId(collider.gameObject) == Identifiable.Id.QUANTUM_SLIME)
            {
                var gameObject = collider.gameObject;
                var val = SRBehaviour.InstantiateActor(
                    SRSingleton<GameContext>.Instance.LookupDirector.GetPrefab(Id.DREAM_SLIME),
                    collider.gameObject.GetComponent<RegionMember>().setId);
                val.transform.position = gameObject.transform.position;
                val.transform.rotation = gameObject.transform.rotation;
                SRBehaviour.SpawnAndPlayFX(gameObject.GetComponent<SlimeEat>().TransformFX, val);
                var rotation = Quaternion.LookRotation(
                    Vector3.Angle(collider.gameObject.GetComponent<Rigidbody>().velocity,
                        __instance.transform.forward) <= 90f
                        ? Vector3.forward
                        : Vector3.back);
                SRBehaviour.SpawnAndPlayFX(__instance.onSuccessFX, __instance.gameObject, Vector3.zero, rotation);
                var component = gameObject.GetComponent<Rigidbody>();
                var component2 = val.GetComponent<Rigidbody>();
                component2.velocity = component.velocity;
                component2.angularVelocity = component.angularVelocity;
                __instance.animator.SetBool(GadgetChickenCloner.ANIMATION_ACTIVE, true);
                Destroyer.DestroyActor(gameObject, "GadgetChickenCloner", true);
                __instance.StartCoroutine(DisableSpin(__instance));
            }
        }

        public static IEnumerator DisableSpin(GadgetChickenCloner __instance)
        {
            yield return new WaitForSeconds(0.35f);
            __instance.GetComponent<Animator>().SetBool(GadgetChickenCloner.ANIMATION_ACTIVE, false);
        }
    }
}