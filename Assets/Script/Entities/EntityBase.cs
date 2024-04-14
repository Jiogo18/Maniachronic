using Assets.Script.Classes;
using UnityEngine;
namespace Assets.Script.Entities
{

    public class EntityBase : MonoBehaviour
    {
        public bool IsOriginal { get; private set; } = true;
        public bool IsDying { get; private set; } = false;

        // Données du personnage
        public virtual int Health { get; internal set; }
        public float LastAttack { get; private set; }

        public ClasseBase Classe { get; private set; }
        public TypeClasse typeClasse;

        public void SetClasse(TypeClasse typeClasse)
        {
            this.typeClasse = typeClasse;
            Classe = ClassesManager.GetClasse(typeClasse);
            Dead = GameObject.Find(Classe.ClassePrefabName).GetComponent<EntityBase>().Dead;
        }

        public virtual int BaseHealth => Classe.BaseHealth;
        public virtual float Speed => Classe.BaseSpeed;
        public virtual float AttackDelay => Classe.AttackDelay;
        public Sprite Dead;
        protected Vector3 velocity;

        public void SetIsClone(bool isClone)
        {
            IsOriginal = !isClone;
        }

        public bool CanAttack()
        {
            if (IsOriginal || IsDying) return false;
            return LastAttack + AttackDelay < Time.time;
        }

        public virtual bool TryAttack()
        {
            if (CanAttack())
            {
                LastAttack = Time.time;

                Classe.Attack(this);
                return true;
            }
            return false;
        }

        // Awake is called when the script instance is being loaded
        internal void Awake()
        {
        }

        // Start is called before the first frame update
        internal void Start()
        {
            SetClasse(typeClasse);
            Health = BaseHealth;
            LastAttack = Time.time;
        }

        // Update is called once per frame
        internal void Update()
        {
            // Out of bounds
            if (SpriteManager.IsOutOfBound(gameObject))
            {
                Destroy(gameObject);
                return;
            }

            if (IsDying)
            {
                // Apply gravity
                velocity = velocity * 0.999f;
                velocity = velocity + Vector3.down * 1e-4f;
                transform.position = transform.position + velocity;
            }
        }

        protected virtual void OnDeath()
        {
            if (Dead != null)
                gameObject.GetComponent<SpriteRenderer>().sprite = Dead;
            // Start the animation where the entity dies
            // Jump to the top-left or top-right with an angle
            float angle = Random.Range(-70, 70);
            velocity = Quaternion.Euler(0, 0, angle) * Vector3.up * 0.03f;
        }

        public virtual bool DoDamageAndDie(int damage)
        {
            if (IsDying) return false;
            Health -= damage;
            if (Health <= 0)
            {
                IsDying = true;
                gameObject.tag = "Untagged";
                OnDeath();
                Destroy(gameObject, 5);
            }
            return true;
        }

        public bool DoDamageByProjectile(Projectile projectile)
        {
            return DoDamageAndDie(projectile.damage);
        }
    }
}