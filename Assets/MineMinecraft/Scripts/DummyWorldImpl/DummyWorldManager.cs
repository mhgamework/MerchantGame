using Assets.MineMinecraft.Blocks;
using UnityEngine;

namespace Assets.MineMinecraft.DummyWorldImpl
{
    public class DummyWorldManager:SingletonBehaviour<DummyWorldManager>
    {
        public Transform BlockPrefab;

        private DummyWorld world;

        public IWorld World
        {
            get { return world; }
        }

        public void Start()
        {
            world = new DummyWorld();

            int size = 30;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        if (x +y+z< 30
                            || y < 5)
                            world.SetBlockAt(new Vector3(x,y,z),new SimpleBlock(BlockPrefab));
                    }
                }
            }

        }

        public void Update()
        {

        }
    }
}