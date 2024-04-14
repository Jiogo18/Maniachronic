using Assets.Script.Classes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Entities
{
    public class EntityPlayer : EntityBase
    {
        public override float Speed => 10 * Classe.BaseSpeed;
        public GameObject textScorePrefab;
        // 2 times the health of the enemies
        public override int BaseHealth => Classe.BaseHealth * 2;
        private int score;
        public int Score
        {
            get => score;
            set
            {
                score = value;
                EntityEnemy.SetSpeed(score);
                UpdateTextScore();
            }
        }
        public override int Health
        {
            get => base.Health;
            internal set
            {
                base.Health = value;
                if (TryGetComponent<Health>(out var healthComponent))
                {
                    healthComponent.health = value;
                }
            }
        }

        // Start is called before the first frame update
        new void Start()
        {
            base.Start();
            UpdateTextScore();
            Score = 0;
        }

        // Update is called once per frame
        new void Update()
        {
            if (IsOriginal)
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    ScèneLoad.GoToMenuPrincipale();
                }
                //if (Input.GetKey(KeyCode.M))
                //{
                //    EntityMenu.SpawnMenu();
                //}
                return;
            }

            base.Update();

            if (IsDying) return;

            // Move the player with the arrow keys / WASD (or ZQSD)
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new(moveHorizontal, moveVertical, 0);
            Vector3 newPosition = transform.position + Speed * Time.deltaTime * movement;
            // Clamp the new position to the screen boundaries
            newPosition.x = Mathf.Clamp(newPosition.x, -9, 9);
            newPosition.y = Mathf.Clamp(newPosition.y, -4, 4);
            transform.position = newPosition;

            // Attack with the space key
            if (Input.GetKey(KeyCode.Space))
            {
                TryAttack();
            }
            // Rotate with Q and E (or A and E)
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(0, 0, 0.5f);
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(0, 0, -0.5f);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (IsOriginal || IsDying) return;

            if (SpriteManager.IsEnemy(other.gameObject))
            {
                EntityEnemy enemy = other.gameObject.GetComponent<EntityEnemy>();
                if (enemy)
                {
                    enemy.TryAttack();
                    Destroy(other.gameObject);
                }
                // Else projectile...
            }
        }

        public override bool DoDamageAndDie(int damage)
        {
            bool retour = base.DoDamageAndDie(damage);
            UpdateTextScore();
            return retour;
        }

        void UpdateTextScore()
        {
            if (IsOriginal) return;

            if (textScorePrefab != null)
            {
                // Change the text with x and y position
                textScorePrefab.GetComponent<Text>().text = $"Score: {Score}";
            }
        }

        private EntityPlayer CreateNewPlayer()
        {
            GameObject previousClone = SpriteManager.GetPlayer();
            if (previousClone != null)
            {
                Destroy(previousClone);
            }

            GameObject clone = Instantiate(gameObject, new Vector3(-10, 0, 0), transform.rotation);
            EntityPlayer entity = clone.GetComponent<EntityPlayer>();
            entity.SetIsClone(true);
            entity.textScorePrefab = textScorePrefab;
            clone.name = "Player_active";
            clone.tag = "Player";

            Health health = clone.GetComponent<Health>();
            if (health != null)
            {
                health.enabled = true;
            }

            return entity;
        }

        public static void CreateNewPlayer(TypeClasse typeClasse)
        {
            // Create a new player
            EntityPlayer originalPlayer = SpriteManager.GetOriginalPlayer().GetComponent<EntityPlayer>();
            EntityPlayer newPlayer = originalPlayer.CreateNewPlayer();

            // Change the class of the player
            newPlayer.SetClasse(typeClasse);
            ClasseBase classe = newPlayer.Classe;

            // Get the sprite of the enemy
            GameObject entityPrefab = GameObject.Find(classe.ClassePrefabName);
            newPlayer.GetComponent<SpriteRenderer>().sprite = entityPrefab.GetComponent<SpriteRenderer>().sprite;
        }

        protected override void OnDeath()
        {
            base.OnDeath();

            // Go the the main menu in 5 seconds
            EntityPlayer oplayer = SpriteManager.GetOriginalPlayer().GetComponent<EntityPlayer>();
            oplayer.Invoke(nameof(GoToMenuPrincipale), 3);
        }

        public void GoToMenuPrincipale()
        {
            ScèneLoad.GoToMenuPrincipale();
        }
    }
}