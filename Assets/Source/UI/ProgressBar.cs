using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        protected Image progressImage;
        protected Text progressText;

        private float value;

        public float Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = Mathf.Clamp01(value);

                progressImage.fillAmount = this.value;
                SetText(this.value);
            }
        }

        protected virtual void SetText(float value)
        {
            string text = string.Format("{0}%", Mathf.FloorToInt(value * 100));
            progressText.text = text;
        }

        private void Awake()
        {
            progressText = GetComponentInChildren<Text>();

            Value = 1;
        }
    }
}
