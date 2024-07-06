using BepInEx;
using BepInEx.Configuration;
using CommonPlugin;

namespace ExtraArmLength
{

    [BepInPlugin("com.lukev.extraarmlength", "Extra Arm Length", "1.0.0")]
    public partial class Plugin : BasePlugin
    {
        static ConfigEntry<float> distance;


        protected override void OnAwake()
        {
            distance = Config.Bind("General", "TotalLength", 15f, "Distance you can interact with things");
        }
    }
}
