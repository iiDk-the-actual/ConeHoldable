using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace ConeHoldable
{
    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("Awake", MethodType.Normal)]
    internal class ExamplePatch
    {
        private static void Postfix(GorillaLocomotion.Player __instance)
        {
            OnGameInitialized();
        }

        static void OnGameInitialized()
        {
            GameObject cone = LoadAsset("ConeHold");
            cone.transform.SetParent(GorillaTagger.Instance.offlineVRRig.transform.Find("RigAnchor/rig/body/shoulder.R/upper_arm.R/forearm.R/hand.R"), false);
        }

        static AssetBundle assetBundle = null;
        public static GameObject LoadAsset(String assetName)
        {
            GameObject gameObject = null;

            if (assetBundle == null)
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ConeHoldable.Resources.conehold");
                assetBundle = AssetBundle.LoadFromStream(stream);
                stream.Close();
            }
            gameObject = UnityEngine.Object.Instantiate<GameObject>(assetBundle.LoadAsset<GameObject>(assetName));

            return gameObject;
        }
    }
}