using Roguelike.Entities;
using UnityEngine;

namespace Roguelike.UI
{
    public class HPBar : ProgressBar
    {
        [SerializeField]
        Fighter character;

        protected override void SetText(float value)
        {
            int maxHP = character.MaxHP;
            int hp = character.HP;

            progressText.text = string.Format("{0} / {1}", hp, maxHP);
        }

        private void Update()
        {
            Value = (float)character.HP / (float)character.MaxHP;
        }
    }
}
