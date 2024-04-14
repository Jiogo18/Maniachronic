using Assets.Script.Entities;
using UnityEngine;

namespace Assets.Script.Classes
{
    /// <summary>
    /// First time using Unity, this is a mess.
    /// This class is meant for the data you can find with every persos/classes/sprites.
    /// </summary>
    public abstract class ClasseBase : ScriptableObject
    {
        // Abstract properties depending on the class
        public abstract string ClassePrefabName { get; }
        public abstract int BaseHealth { get; }
        public abstract float BaseSpeed { get; }
        public abstract int AttackDamage { get; }
        public abstract float AttackDelay { get; }

        public virtual bool ShouldAttackThePlayer(EntityBase current)
        {
            return false;
        }

        public abstract void Attack(EntityBase current);
    }
}