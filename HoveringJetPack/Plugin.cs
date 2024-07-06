using BepInEx;
using BepInEx.Configuration;
using CommonPlugin;
using UnityEngine.InputSystem;

namespace HoveringJetPack
{

    [BepInPlugin("com.lukev.hoveringjetpack", "Hovering Jetpack", "1.0.0")]
    public partial class Plugin : BasePlugin
    {
        static ConfigEntry<bool> smoothEnabled;
        static ConfigEntry<bool> enableOnStart;

        static ConfigEntry<string> toggleButton;
        static bool hoveringEnabled;
        static InputAction actionToggle;


        protected override void OnAwake()
        {
            smoothEnabled = Config.Bind("General", "SmoothDropEnabled", true, "Enable smooth drop when going down without hovering");
            toggleButton = Config.Bind("General", "ToggleButton", "<Keyboard>/c", "Button to toggle hovering");
            enableOnStart = Config.Bind("General", "EnableOnStart", true, "Enable hovering when the game starts");

            hoveringEnabled = enableOnStart.Value;
            actionToggle = new InputAction(binding: toggleButton.Value);
            actionToggle.Enable();
        }

        void Update()
        {
            if (actionToggle.WasPressedThisFrame())
            {
                hoveringEnabled = !hoveringEnabled;
                Log.LogInfo("Hovering toggled: " + hoveringEnabled);
            }
        }

    }
}
