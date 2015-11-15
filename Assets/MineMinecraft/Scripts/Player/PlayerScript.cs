using Assets.MineMinecraft.DummyWorldImpl;
using UnityEngine;

namespace Assets.MineMinecraft.Scripts.Player
{
    public class PlayerScript : MonoBehaviour
    {

        public void Start()
        {

        }

        public void Update()
        {
            var world = DummyWorldManager.Instance().World;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var result = world.Raycast(ray);

            if (!result.IsHit) return;
            onBlockRaycast(world, result);
        }

        private static void onBlockRaycast(IWorld world, WorldRaycastResult result)
        {
            // Do something smart
            //world.SetBlockAt(result.Block.Position, null);
        }
    }
}