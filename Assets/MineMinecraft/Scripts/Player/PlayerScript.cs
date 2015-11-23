using Assets.MineMinecraft.Blocks;
using Assets.MineMinecraft.DummyWorldImpl;
using Assets.MineMinecraft.MathUtils;
using Assets.MineMinecraft.Scripts.Blocks;
using UnityEngine;

namespace Assets.MineMinecraft.Scripts.Player
{
    public class PlayerScript : MonoBehaviour,IPlayer
    {

        public Transform BlockPrefab;

        public void Start()
        {

        }

        public void Update()
        {
            var world = DummyWorldManager.Instance().World;
            var cam  = Camera.main.transform;
            var result = world.Raycast(new Ray(cam.position, cam.forward));

            if (!result.IsHit) return;
            onBlockRaycast(world, result);
        }

        private void onBlockRaycast(IWorld world, WorldRaycastResult result)
        {

            // Do something smart
            if (Input.GetMouseButtonDown(0))
            {
                world.SetBlockAt(result.Block.Position, null);
            }
            if (Input.GetMouseButtonDown(1))
            {
                var hitPoint = result.HitPoint;
                var blockPos = result.Block.Position;
                var direction = ((hitPoint - blockPos)*2.001f).IntegerPart();
                world.SetBlockAt(blockPos+direction, new LampBlock(BlockPrefab));
                

            }
            if (Input.GetMouseButtonDown(2))
            {
                result.Block.OnInteract(result.HitPoint,this );
            }

            //world.SetBlockAt(result.Block.Position, null);
        }
    }
}