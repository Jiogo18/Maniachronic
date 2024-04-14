using Assets.Script.Classes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.Entities
{
    /// <summary>
    /// A class for the selection of the player's class
    /// </summary>
    public class EntityMenu : EntityBase
    {
        private static readonly List<EntityMenu> menuEntities = new();
        private static float previousOpen;

        // Start is called before the first frame update
        new void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        new void Update()
        {
            if (IsOriginal) return;

            // When the player clicks on the entity
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D collider = Physics2D.OverlapPoint(mousePos);
                if (collider != null && collider.gameObject == gameObject)
                {
                    // Destroy the menu
                    DestroyMenu();
                    // Spawn the player
                    EntityPlayer.CreateNewPlayer(typeClasse);
                }
            }
        }

        public static void Spawn(Vector3 spawnPos, TypeClasse typeClasse)
        {
            GameObject entityPrefab = GameObject.Find("ButtonMenu");
            GameObject clone = Instantiate(entityPrefab, spawnPos, entityPrefab.transform.rotation);
            EntityMenu entity = clone.GetComponent<EntityMenu>();
            entity.SetClasse(typeClasse);
            entity.SetIsClone(true);
            menuEntities.Add(entity);

            // Get and set the sprite
            GameObject gameObjectEnemy = GameObject.Find(entity.Classe.ClassePrefabName);
            SpriteRenderer spriteRenderer = gameObjectEnemy.GetComponent<SpriteRenderer>();
            entity.GetComponentInChildren<Image>().sprite = spriteRenderer.sprite;
            float ratioWH = spriteRenderer.sprite.rect.width / spriteRenderer.sprite.rect.height;
            float height = entity.GetComponent<RectTransform>().rect.height;
            float newWidth = height * ratioWH;
            entity.GetComponent<RectTransform>().sizeDelta = new Vector2(newWidth, height);

            // Place the button in the canvas
            clone.transform.SetParent(entityPrefab.transform.parent);
        }

        public static void DestroyMenu()
        {
            // Clear the menu
            foreach (EntityMenu existingEntity in menuEntities)
            {
                if (existingEntity == null) continue;
                Destroy(existingEntity.gameObject);
                Destroy(existingEntity);
            }
            menuEntities.Clear();

            // Remove the background
            GameObject background = GameObject.Find("BackgroundMenu");
            if (background != null)
            {
                background.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        public static void SpawnMenu()
        {
            if (previousOpen + 1 > Time.time) return;
            previousOpen = Time.time;

            // Clear the previous menu
            DestroyMenu();

            // Spawn the menu
            TypeClasse[] typeClasses = ClassesManager.GetPlayerClasses();
            for (int i = 0; i < typeClasses.Length; i++)
            {
                TypeClasse typeClasse = typeClasses[i];
                Vector3 pos = new(350 + i * 225, 250, 0);
                Spawn(pos, typeClasse);
            }

            // Place the background
            GameObject background = GameObject.Find("BackgroundMenu");
            if (background != null)
            {
                background.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        public void OnClick()
        {
            // Close the menu
            DestroyMenu();

            // Spawn the player
            EntityPlayer.CreateNewPlayer(typeClasse);
        }
    }
}