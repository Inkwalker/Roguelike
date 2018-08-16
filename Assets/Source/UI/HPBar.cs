using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.UI
{
    public class HPBar : ProgressBar
    {
        public Fighter Character { get; set; }

        protected override void SetText(float value)
        {
            if (Character != null)
            {
                int maxHP = Character.MaxHP;
                int hp = Character.HP;

                progressText.text = string.Format("{0} / {1}", hp, maxHP);
            }
            else
            {
                progressText.text = "";
            }
        }

        private void Update()
        {
            if (Character != null)
            {
                Value = (float)Character.HP / (float)Character.MaxHP;
            }
            else
            {
                Value = 1f;
            }
        }
    }
}
