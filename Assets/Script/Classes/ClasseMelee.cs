using Assets.Script.Entities;
using UnityEngine;

namespace Assets.Script.Classes
{
    public abstract class ClasseMelee : ClasseBase
    {
        public abstract float AttackRange { get; }

        public override void Attack(EntityBase current)
        {
            bool IAmAPlayer = current.CompareTag("Player");

            // Kill enemies in range
            Collider[] enemies = Physics.OverlapSphere(current.transform.position, AttackRange);
            foreach (var enemy in enemies)
            {
                if (SpriteManager.IsOpponent(enemy.gameObject, IAmAPlayer))
                {
                    EntityBase entity = enemy.gameObject.GetComponent<EntityBase>();
                    if (entity != null)
                    {
                        entity.DoDamageAndDie(AttackDamage);
                        continue;
                    }
                    Projectile projectile = enemy.gameObject.GetComponent<Projectile>();
                    if (projectile != null)
                    {
                        projectile.DoDamageAndDie();
                        continue;
                    }
                }
            }

            // For the debug, rotate by 90 degrees
            current.transform.Rotate(0, 0, 90);
        }
    }
}