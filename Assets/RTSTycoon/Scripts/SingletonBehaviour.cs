using UnityEngine;

namespace Assets
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance()
        {
            var ret = FindObjectOfType<T>();
            if (!ret) Debug.LogError("No instance of RenderToTextureScript found in scene!");
            return ret;
        }
    }
}