using Assets.Script.Entities;
using UnityEngine;

namespace Assets.Script.Classes
{
    public abstract class ClasseDistance<T> : ClasseBase where T : Projectile
    {
        public abstract string ProjectilePrefabName { get; }

        public override bool ShouldAttackThePlayer(EntityBase current)
        {
            GameObject player = SpriteManager.GetPlayer();
            if (player == null) return false;
            float distance = Vector3.Distance(player.transform.position, current.transform.position);
            return distance <= 4;
        }

        public override void Attack(EntityBase current)
        {
            SummonProjectile(current);
        }

        private void SummonProjectile(EntityBase owner)
        {
            // Summon a projectile
            GameObject projectilePrefab = GameObject.Find(ProjectilePrefabName);
            GameObject clone = Instantiate(projectilePrefab, owner.transform.position, owner.transform.rotation);
            Projectile projectile = clone.GetComponent<Projectile>();
            projectile.Owner = owner;
            projectile.tag = owner.tag;
            if (owner is EntityPlayer)
            {
                // Use the direction of the owner as the velocity of the projectile
                Quaternion rotation = owner.transform.rotation;
                // -90 degrees because the sprite is not facing the same direction as the projectile
                projectile.SetRotation(Quaternion.Euler(0, 0, rotation.eulerAngles.z - 90));
            }
            else
            {
                // Fire towards the player
                GameObject player = SpriteManager.GetPlayer();
                if (player != null)
                {
                    Vector3 playerForce = player.transform.position - owner.transform.position;
                    playerForce = playerForce.normalized;
                    projectile.SetRotation(Quaternion.LookRotation(Vector3.forward, playerForce));
                }
            }
            projectile.damage = AttackDamage;
            projectile.SetIsClone(true);
        }
    }
}
