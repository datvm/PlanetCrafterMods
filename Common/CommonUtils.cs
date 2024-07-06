using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;

namespace CommonPlugin
{

    public abstract class BasePlugin : BaseUnityPlugin
    {
        internal static BasePlugin instance;

        public static ManualLogSource Log => instance.Logger;

        protected virtual void Awake()
        {
            instance = this;

            OnAwake();

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);
        }

        protected abstract void OnAwake();

    }

    public static class CommonUtils
    {

    }

}