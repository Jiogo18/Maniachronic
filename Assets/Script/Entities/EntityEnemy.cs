using UnityEngine;

namespace Assets.Script.Entities
{
    public class EntityEnemy : EntityBase
    {
        private int seed = 0;
        // Attack slower than the player
        public override float AttackDelay => Classe.AttackDelay * 4;
        private static float SpeedFactor = 1;
        public override float Speed => base.Speed * SpeedFactor;

        new void Awake()
        {
            base.Awake();
        }

        // Start is called before the first frame update
        new void Start()
        {
            base.Start();
            seed = Random.Range(0, 1000000);
            velocity = new Vector3(-Speed * 0.005f, 0, 0);
        }

        // Update is called once per frame
        new void Update()
        {
            if (IsOriginal) return;
            base.Update();
            if (IsDying) return;

            if (Classe.ShouldAttackThePlayer(this))
            {
                // Try to attack
                TryAttack();
                return;
            }

            // Change the velocity depending on the player position
            GameObject player = SpriteManager.GetPlayer();
            if (player != null)
            {
                Vector3 playerForce = player.transform.position - transform.position;
                velocity += playerForce.normalized * 0.0001f;
            }

            // Add a random force
            float perlinNoisePlusMinus = Mathf.PerlinNoise(seed, Time.time) - 0.47f;
            velocity += new Vector3(0, perlinNoisePlusMinus, 0) * 0.00005f;

            // Cap the velocity
            velocity = Vector3.ClampMagnitude(velocity, Speed * 0.005f);

            // Move the enemy
            transform.position = transform.position + velocity;
        }

        protected override void OnDeath()
        {
            base.OnDeath();
            // Add a score to the player
            GameObject player = SpriteManager.GetPlayer();
            if (player != null)
            {
                player.GetComponent<EntityPlayer>().Score += 1;
            }
        }

        internal static void SetSpeed(int score)
        {
            // Increase the speed of the enemies by .2 every 20 points
            // => *2 at 100 points
            SpeedFactor = 1f + Mathf.Floor(score / 20) * 0.20f;
        }
    }
}