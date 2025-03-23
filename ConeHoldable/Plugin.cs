using BepInEx;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace ConeHoldable
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool done;
        void Update()
        {
            if (done || GorillaTagger.Instance.offlineVRRig == null || GorillaLocomotion.GTPlayer.Instance == null)
                return;

            OnGameInitialized();
            done = true;
        }

        void OnGameInitialized()
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
            gameObject = Instantiate<GameObject>(assetBundle.LoadAsset<GameObject>(assetName));

            return gameObject;
        }
    }
}
