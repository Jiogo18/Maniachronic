using Assets.Script.Classes;
using UnityEngine;

namespace Assets.Script.Entities
{
    public class Projectile : MonoBehaviour
    {
        private EntityBase owner;
        public EntityBase Owner
        {
            get => owner;
            set
            {
                owner = value;
                isOwnerPlayer = owner is EntityPlayer;
                if (isOwnerPlayer)
                {
                    gameObject.tag = "Player";
                }
                else
                {
                    gameObject.tag = "Enemy";
                }
            }
        }

        public bool isOwnerPlayer;
        public Vector3 velocity;
        public int damage = 1;
        private bool isOriginal = true;

        internal void SetRotation(Quaternion rotation)
        {
            velocity = rotation * Vector3.up * Time.deltaTime * 5 * (isOwnerPlayer ? 1 : 0.5f);
            transform.rotation = rotation * Quaternion.Euler(0, 0, 90);
        }

        internal void SetIsClone(bool isClone)
        {
            this.isOriginal = !isClone;
        }

        void Update()
        {
            if (isOriginal) return;

            transform.position = transform.position + velocity;

            // Remove if out of bounds
            if (transform.position.x < -11 || transform.position.x > 11 || transform.position.y < -5 || transform.position.y > 5)
            {
                Destroy(gameObject);
            }

            if (owner.Classe is ClasseSamourai)
            {
                // Rotate the shuriken
                transform.Rotate(new Vector3(0, 0, 360) * Time.deltaTime);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent<EntityBase>(out var entity)) return;

            // Friendly fire is disabled
            bool collide = SpriteManager.IsOpponent(other.gameObject, isOwnerPlayer);

            if (collide)
            {
                // Damage the other entity
                if (entity.DoDamageByProjectile(this))
                {
                    Destroy(gameObject);
                }
            }
        }

        /// <summary>
        /// Kill this projectile
        /// </summary>
        /// <param name="attackDamage"></param>
        internal void DoDamageAndDie()
        {
            Destroy(gameObject);
        }
    }
}
