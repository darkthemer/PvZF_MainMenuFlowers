using MelonLoader;
using UnityEngine;

namespace PvZF_MainMenuFlowers
{
    public class FlowerAnimation : MonoBehaviour
    {
        private RectTransform rect1;
        private RectTransform rect2;
        private RectTransform rect3;

        private AnimationCurve flower1PosX;
        private AnimationCurve flower1PosY;
        private AnimationCurve flower1RotationZ;
        private AnimationCurve flower1ScaleY;

        private AnimationCurve flower2PosX;
        private AnimationCurve flower2PosY;
        private AnimationCurve flower2SizeX;

        private AnimationCurve flower3PosX;
        private AnimationCurve flower3PosY;
        private AnimationCurve flower3SizeX;

        private Vector2 flower1BasePosition;
        private Vector2 flower2BasePosition;
        private Vector2 flower3BasePosition;

        private Vector3 flower1BaseScale;
        private Vector2 flower2BaseSize;
        private Vector2 flower3BaseSize;

        private float playbackFlower1;
        private float playbackFlower2;
        private float playbackFlower3;

        private float playbackSpeed = 0.75f;

        private bool playingFlower1;
        private bool playingFlower2;
        private bool playingFlower3;

        public void Initialize(Transform flower1, Transform flower2, Transform flower3)
        {
            rect1 = flower1.GetComponent<RectTransform>();
            rect2 = flower2.GetComponent<RectTransform>();
            rect3 = flower3.GetComponent<RectTransform>();

            if (rect1 == null || rect2 == null || rect3 == null)
            {
                MelonLogger.Error("Cannot find RectTransform of all flowers");

                return;
            }

            flower1BasePosition = rect1.anchoredPosition;
            flower2BasePosition = rect2.anchoredPosition;
            flower3BasePosition = rect3.anchoredPosition;

            flower1BaseScale = rect1.localScale;
            flower2BaseSize = rect2.sizeDelta;
            flower3BaseSize = rect3.sizeDelta;

            CreateCurves();
        }

        private void CreateCurves()
        {
            // Flower1
            // Values adapted from PvZ Replanted, A_MainMenu_Flower01_Anim.anim

            flower1PosX = new AnimationCurve(
                new Keyframe(0f, -184.3f),
                new Keyframe(0.016666668f, -186.5f),
                new Keyframe(0.5f, -206.8f),
                new Keyframe(1.1666666f, -158f),
                new Keyframe(1.3333334f, -151.3f)
            );

            flower1PosY = new AnimationCurve(
                new Keyframe(0f, 15.5f),
                new Keyframe(0.016666668f, 15.264191f),
                new Keyframe(1.1666666f, -141f),
                new Keyframe(1.3333334f, -163.4f),
                new Keyframe(1.4f, -170f),
                new Keyframe(1.4666667f, -162.8f)
            );

            flower1RotationZ = new AnimationCurve(
                new Keyframe(0f, 0f),
                new Keyframe(0.5f, -33.229f),
                new Keyframe(1.3333334f, -7.044f)
            );

            flower1ScaleY = new AnimationCurve(
                new Keyframe(1.3333334f, 1f),
                new Keyframe(1.4f, 0.89294f),
                new Keyframe(1.4666667f, 1f)
            );

            // Flower2
            // Values adapted from PvZ Replanted, A_MainMenu_Flower02_Anim.anim

            flower2PosX = new AnimationCurve(
                new Keyframe(0f, -54.6f),
                new Keyframe(0.05f, -51.671783f),
                new Keyframe(0.1f, -54.6f),
                new Keyframe(0.15f, -50.017487f),
                new Keyframe(0.2f, -54.6f),
                new Keyframe(1f, 2765f)
            );

            flower2PosY = new AnimationCurve(
                new Keyframe(0f, 92.2f),
                new Keyframe(0.05f, 92.20001f),
                new Keyframe(0.1f, 92.2f),
                new Keyframe(0.2f, 92.2f),
                new Keyframe(1f, 112.2f)
            );

            flower2SizeX = new AnimationCurve(
                new Keyframe(0f, 134f),
                new Keyframe(0.05f, 140.1433f),
                new Keyframe(0.1f, 134f),
                new Keyframe(0.15f, 142.9785f),
                new Keyframe(0.2f, 132.8f),
                new Keyframe(1f, 134f)
            );

            // Flower3
            // Values adapted from PvZ Replanted, A_MainMenu_Flower03_Anim.anim

            flower3PosX = new AnimationCurve(
                new Keyframe(0f, 172.54996f),
                new Keyframe(0.05f, 180.24747f),
                new Keyframe(0.1f, 172.54996f),
                new Keyframe(0.15f, 178.93326f),
                new Keyframe(0.8333333f, 2555f)
            );

            flower3PosY = new AnimationCurve(
                new Keyframe(0f, -110.86751f),
                new Keyframe(0.15f, -110.86751f),
                new Keyframe(0.8333333f, -130.86751f)
            );

            flower3SizeX = new AnimationCurve(
                new Keyframe(0f, 177.9f),
                new Keyframe(0.05f, 193.295f),
                new Keyframe(0.1f, 177.9f),
                new Keyframe(0.15f, 190.6666f)
            );
        }

        public void PlayFlower(int flowerNumber)
        {
            if (flowerNumber < 1 || flowerNumber > 3)
                return;

            switch (flowerNumber)
            {
                case 1:
                    playbackFlower1 = 0f;
                    playingFlower1 = true;
                    break;

                case 2:
                    playbackFlower2 = 0f;
                    playingFlower2 = true;
                    break;

                case 3:
                    playbackFlower3 = 0f;
                    playingFlower3 = true;
                    break;
            }
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            // Call animation on update
            // Enforce evaluation only until final keyframe

            if (playingFlower1)
            {
                playbackFlower1 += deltaTime * playbackSpeed;
                playbackFlower1 = Mathf.Min(playbackFlower1, 1.4666667f);
                AnimateFlower1(playbackFlower1);
                if (playbackFlower1 >= 1.4666667f)
                    playingFlower1 = false;
            }

            if (playingFlower2)
            {
                playbackFlower2 += deltaTime * playbackSpeed;
                playbackFlower2 = Mathf.Min(playbackFlower2, 1f);
                AnimateFlower2(playbackFlower2);
                if (playbackFlower2 >= 1f)
                    playingFlower2 = false;
            }

            if (playingFlower3)
            {
                playbackFlower3 += deltaTime * playbackSpeed;
                playbackFlower3 = Mathf.Min(playbackFlower3, 0.8333333f);
                AnimateFlower3(playbackFlower3);
                if (playbackFlower3 >= 0.8333333f)
                    playingFlower3 = false;
            }
        }

        private void AnimateFlower1(float time)
        {
            // Adjusted to fit flower translation & transformation in PvZ Fusion
            float x = flower1BasePosition.x + (flower1PosX.Evaluate(time) - (-184.3f));
            float y = flower1BasePosition.y + (flower1PosY.Evaluate(time) - 15.5f);
            rect1.anchoredPosition = new Vector2(x, y);

            Vector3 rotation = rect1.localEulerAngles;
            rotation.z = flower1RotationZ.Evaluate(time);
            rect1.localEulerAngles = rotation;

            Vector3 scale = flower1BaseScale;
            scale.y *= flower1ScaleY.Evaluate(time);
            rect1.localScale = scale;
        }

        private void AnimateFlower2(float time)
        {
            // Adjusted to fit flower translation & transformation in PvZ Fusion
            float x = flower2BasePosition.x + (flower2PosX.Evaluate(time) - (-54.6f));
            float y = flower2BasePosition.y + (flower2PosY.Evaluate(time) - 92.2f);
            rect2.anchoredPosition = new Vector2(x, y);

            float widthMultiplier = flower2SizeX.Evaluate(time) / 134f;
            rect2.sizeDelta = new Vector2(flower2BaseSize.x * widthMultiplier, flower2BaseSize.y);
        }

        private void AnimateFlower3(float time)
        {
            // Adjusted to fit flower translation & transformation in PvZ Fusion
            float x = flower3BasePosition.x + (flower3PosX.Evaluate(time) - 172.54996f);
            float y = flower3BasePosition.y + (flower3PosY.Evaluate(time) - (-110.86751f));
            rect3.anchoredPosition = new Vector2(x, y);

            float widthMultiplier = flower3SizeX.Evaluate(time) / 177.9f;
            rect3.sizeDelta = new Vector2(flower3BaseSize.x * widthMultiplier, flower3BaseSize.y);
        }
    }
}
