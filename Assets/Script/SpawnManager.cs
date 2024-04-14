namespace Assets.Script
{
    using Assets.Script.Entities;
    using UnityEngine;

    public class SpawnManager : MonoBehaviour
    {
        public GameObject[] animalPrefabs;
        public float spawnMidX = 6;
        public float spawnMidY = 0;
        public float spawnMidZ = 20;
        public float spawnRangeX = 2;
        public float spawnRangeY = 6;
        public float delay = 2f;
        public float spawnInterval = 0.5f;
        public float spawnChance = 0.3f;

        // Start is called before the first frame update
        void Start()
        {
            InvokeRepeating(nameof(SpawnRandomAnimal), delay, spawnInterval);
            EntityPlayer.CreateNewPlayer(ScèneLoad.typeClasse);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SpawnRandomAnimal()
        {
            // Don't spawn if the player is dead
            if (SpriteManager.GetPlayer() == null) return;

            // 60 % chance to spawn an entity
            if (Random.value > spawnChance)
            {
                return;
            }

            // spawnPos from the current geometry, without using spawnRangeX and spawnRangeY
            // get the x, y, z and width, height, depth of the object
            Vector3 size = GetComponent<Renderer>().bounds.size;
            float x = transform.position.x + Random.Range(-size.x / 2, size.x / 2);
            float y = transform.position.y + Random.Range(-size.y / 2, size.y / 2);
            float z = transform.position.z + Random.Range(-size.z / 2, size.z / 2);
            Vector3 spawnPos = new(x, y, z);
            int animalIndex = Random.Range(0, animalPrefabs.Length);
            GameObject clone = Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);
            EntityEnemy entity = clone.GetComponent<EntityEnemy>();
            entity.SetIsClone(true);
            clone.tag = "Enemy";
        }
    }
}
