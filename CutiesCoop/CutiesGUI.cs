using UnityEngine;

namespace CutiesCoop
{
    public class CutiesGUI : MonoBehaviour
    {
        private bool visible = false;
        private float ratesSlider;
        private int boxX, boxY;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            ratesSlider = CutiesCoopInit.config.dropRate;
            boxX = Screen.width / 2 - 500;
            boxY = Screen.height / 2 - 300;
        }

        private void OnGUI()
        {
            if (visible)
            {
                GUI.changed = false;
                GUI.Box(new Rect(boxX, boxY, 500, 300), "Cuties Config");
                GUI.Label(new Rect(boxX + 10, boxY + 30, 100, 30), "Drop Rate:");
                ratesSlider = GUI.HorizontalSlider(new Rect(boxX + 100, boxY + 35, 100, 30), ratesSlider, 1, 10);
                GUI.Label(new Rect(boxX + 200, boxY + 30, 100, 30), (int)ratesSlider + "x");

                if (GUI.changed)
                {
                    CutiesCoopInit.config.dropRate = (int)ratesSlider;
                }
            }
        }

        private void BoxFunction(int windowID)
        {
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                visible = !visible;
            }
        }
    }
}
