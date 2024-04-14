using UnityEngine;

namespace Assets.Script
{
    public static class SpriteManager
    {
        /// <summary>
        /// Check if the game object is valid
        /// Below y=-10 is the storage area
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static bool IsValid(GameObject gameObject)
        {
            return gameObject != null && gameObject.transform.position.y > -10;
        }

        public static bool IsPlayer(GameObject gameObject)
        {
            return IsValid(gameObject) && gameObject.CompareTag("Player");
        }

        public static bool IsEnemy(GameObject gameObject)
        {
            return IsValid(gameObject) && gameObject.CompareTag("Enemy");
        }

        public static bool IsOpponent(GameObject gameObject, bool IAmAPlayer)
        {
            return IAmAPlayer ? IsEnemy(gameObject) : IsPlayer(gameObject);
        }

        public static GameObject GetPlayer()
        {
            GameObject gameObject = GameObject.Find("Player_active");
            if (gameObject != null && gameObject.CompareTag("Player"))
            {
                return gameObject;
            }
            else
            {
                return null;
            }
        }

        public static GameObject GetOriginalPlayer()
        {
            return GameObject.Find("Player");
        }

        public static GameObject GetOriginalEnemy()
        {
            return GameObject.Find("Enemy");
        }

        public static GameObject GetOriginalArrow()
        {
            return GameObject.Find("ProjectileArrow");
        }

        internal static bool IsOutOfBound(GameObject gameObject)
        {
            return gameObject.transform.position.x < -11 || gameObject.transform.position.x > 11 || gameObject.transform.position.y < -5 || gameObject.transform.position.y > 5;
        }
    }
}
