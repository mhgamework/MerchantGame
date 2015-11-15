﻿using Assets.MineMinecraft.Blocks;
using Assets.MineMinecraft.DummyWorldImpl;
using UnityEngine;

namespace Assets.MineMinecraft.Scripts.Player
{
    public class PlayerScript : MonoBehaviour
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
                if (hitPoint.x + 0.5 > blockPos.x)
                {
                    world.SetBlockAt(new Vector3(blockPos.x+1, blockPos.y, blockPos.z), new SimpleBlock(BlockPrefab));
                }
            }
            //world.SetBlockAt(result.Block.Position, null);
        }
    }
}