using Il2CppInterop.Runtime.Injection;
using MelonLoader;

[assembly: MelonInfo(typeof(PvZF_MainMenuFlowers.MainMenuFlowers), "Main Menu Flowers Easter Egg", "1.0.0", "darkthemer")]
[assembly: MelonGame("LanPiaoPiao", "PlantsVsZombiesRH")]

namespace PvZF_MainMenuFlowers
{
    public class MainMenuFlowers : MelonMod
    {
        public override void OnInitializeMelon()
        {
            ClassInjector.RegisterTypeInIl2Cpp<FlowerAnimation>();

            MelonLogger.Msg("Loaded Main Menu Flowers Easter Egg");
        }
    }
}
