﻿using UnityEngine;
using System.Collections;
using Roguelike.Actions;
using System.Collections.Generic;
using Roguelike.LoadSave;

namespace Roguelike.Entities
{
    public class Fighter : AEntityComponent
    {
        [SerializeField] int maxHP;
        [SerializeField] int defense;
        [SerializeField] int power;

        public int MaxHP { get { return maxHP; } }
        public int HP { get; private set; }
        public int Defense { get { return defense; } }
        public int Power { get { return power; } }

        public bool Dead { get { return HP <= 0; } }

        private void Awake()
        {
            HP = MaxHP;
        }

        public IActionResult[] TakeDamage(int amount)
        {
            HP -= amount;

            if (HP <= 0)
            {
                var animator = GetComponentInChildren<Animator>();

                if (animator != null)
                {
                    animator.SetBool("Dead", true);
                }

                return new IActionResult[] { new DeadActionResult(GetComponent<Entity>()) };
            }

            return new IActionResult[0];
        }

        public IActionResult[] Heal(int amount)
        {
            HP += amount;

            if (HP > MaxHP)
            {
                HP = MaxHP;
            }

            return new IActionResult[0];
        }

        public IActionResult[] Attack(Fighter target)
        {
            List<IActionResult> results = new List<IActionResult>();

            var targetEntity = target.GetComponent<Entity>();

            int damage = Power - target.Defense;

            if (damage > 0)
            {
                results.Add(new MessageActionResult(
                    string.Format("{0} attacks {1} for {2} hit points.", 
                    Entity.DisplayName, 
                    targetEntity.DisplayName, 
                    damage)));

                results.AddRange(target.TakeDamage(damage));
            }
            else
            {
                results.Add(new MessageActionResult(
                    string.Format("{0} attacks {1} but does no damage.", Entity.DisplayName, targetEntity.DisplayName)));
            }

            return results.ToArray();
        }

        public override AEntityComponentData GetData()
        {
            return new FighterComponentData() { HP = this.HP };
        }

        public override void SetData(EntityInstanceData data)
        {
            var fighterData = data.GetComponentData<FighterComponentData>();

            if (fighterData != null)
            {
                HP = fighterData.HP;

                if (HP <= 0)
                {
                    var animator = GetComponentInChildren<Animator>();

                    if (animator != null)
                    {
                        animator.SetBool("Dead", true);
                    }
                }
            }
        }
    }
}
