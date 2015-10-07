using UnityEngine;

namespace Assets
{
    public static class GameObjectExtensions
    {
        public static T GetOrCreateComponent<T>(this Component b) where T : Component
        {
            var ret = b.GetComponent<T>();
            if (ret == null)
                ret = b.gameObject.AddComponent<T>();

            return ret;
        }
    }
}