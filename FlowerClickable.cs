using Il2Cpp;
using Il2CppInterop.Runtime;
using MelonLoader;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using HarmonyLib;
using Il2CppTMPro;

namespace PvZF_MainMenuFlowers
{
    [HarmonyPatch(typeof(MainMenu), "Start")]
    public static class FlowerClickable
    {
        private static FlowerAnimation animation;

        public static void Postfix(MainMenu __instance)
        {
            if (__instance == null)
            {
                MelonLogger.Error($"Cannot find MainMenu");

                return;
            }

            // Setup flowers
            Transform mainMenu = __instance.transform;
            Transform flower1 = mainMenu.Find("Grave/GraveBackground/Flower1");
            Transform flower2 = mainMenu.Find("Grave/GraveBackground/Flower2");
            Transform flower3 = mainMenu.Find("Grave/GraveBackground/Flower3");

            if (mainMenu == null || flower1 == null || flower2 == null || flower3 == null)
            {
                MelonLogger.Error("Cannot find all flowers");

                return;
            }
            
            animation = mainMenu.gameObject.AddComponent<FlowerAnimation>();

            if (animation == null)
            {
                MelonLogger.Error("Cannot create FlowerAnimation");

                return;
            }

            animation.Initialize(flower1, flower2, flower3);

            CreateFlowerButton(flower1);
            CreateFlowerButton(flower2);
            CreateFlowerButton(flower3);

            // Move lower buttons to lower z-index
            Transform lowerButtons = mainMenu.Find("Grave/LowerButtons");
            Transform graveBackground = mainMenu.Find("Grave/GraveBackground");

            if (lowerButtons == null)
            {
                MelonLogger.Warning("Cannot find LowerButtons");
            }
            else
            {
                lowerButtons.SetParent(graveBackground, true);
                lowerButtons.SetAsFirstSibling();
            }

            // Prevent obstruction of languages and changelog text
            Transform languagesButton = mainMenu.Find("Grave/LanguagesButton");
            Transform updateInfoButton = mainMenu.Find("Grave/UpdateInfoButton");

            if (languagesButton == null)
            {
                MelonLogger.Warning("Cannot find LanguagesButton");
            }
            else
            {
                TextMeshProUGUI languagesText =
                    languagesButton.GetComponentInChildren<TextMeshProUGUI>(true);

                if (languagesText != null)
                    languagesText.raycastTarget = false;
            }

            if (updateInfoButton == null)
            {
                MelonLogger.Warning("Cannot find UpdateInfoButton");
            }
            else
            {
                TextMeshProUGUI updateInfoText =
                    updateInfoButton.GetComponentInChildren<TextMeshProUGUI>(true);

                if (updateInfoText != null)
                    updateInfoText.raycastTarget = false;
            }
        }

        private static void CreateFlowerButton(Transform flower)
        {
            Image image = flower.GetComponent<Image>();

            if (image == null)
            {
                MelonLogger.Error($"Cannot find {flower.name} image");

                return;
            }

            image.raycastTarget = true;

            GameObject clickObject = new GameObject($"{flower.name}_Click");
            clickObject.transform.SetParent(flower, false);
            clickObject.SetActive(true);

            Image clickImage = clickObject.AddComponent<Image>();
            clickImage.color = Color.clear;
            clickImage.raycastTarget = true;

            RectTransform clickRect = clickObject.GetComponent<RectTransform>();
            clickRect.anchorMin = Vector2.zero;
            clickRect.anchorMax = Vector2.one;
            clickRect.offsetMin = Vector2.zero;
            clickRect.offsetMax = Vector2.zero;

            Button button = clickObject.AddComponent<Button>();
            button.transition = Selectable.Transition.None;
            button.targetGraphic = null;

            int flowerNumber = flower.name == "Flower1" ? 1 : 
                               flower.name == "Flower2" ? 2 : 
                               flower.name == "Flower3" ? 3 : 0;

            Action callback = () =>
            {
                MelonLogger.Msg($"{flower.name} clicked");

                GameAPP.PlaySound(SoundType.LimbsPop, 0.5f, 1f);

                clickObject.SetActive(false);

                animation.PlayFlower(flowerNumber);
            };

            button.onClick.AddListener(DelegateSupport.ConvertDelegate<UnityAction>(callback));
        }
    }
}
