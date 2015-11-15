using UnityEngine;

namespace Assets.MineMinecraft.MathUtils
{
    public static class MathExtensions
    {
        public static Vector3 Round(this Vector3 v)
        {
            return new Vector3(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
        }
    }
}