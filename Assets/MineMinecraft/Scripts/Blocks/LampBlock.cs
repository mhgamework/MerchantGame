using Assets.MineMinecraft.Blocks;
using Assets.MineMinecraft.DummyWorldImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.MineMinecraft.Scripts.Blocks
{
    class LampBlock : SimpleBlock
    {
        private Boolean lightOn = true;

        public LampBlock(Transform modelPrefab): base(modelPrefab)
        {
        }


        private void switchLight()
        {
            var world = DummyWorldManager.Instance().World;
            var meshRender = world.getBlockTransform(this).GetComponent<MeshRenderer>();
            var light = world.getBlockTransform(this).GetComponentInChildren<Light>();
            if (lightOn)
            {
                meshRender.material = Resources.Load("LampOff") as Material; ;
                light.enabled = false;
            }
            else
            {
                meshRender.material = Resources.Load("LampOn") as Material; ;
                light.enabled = true;
            }
            lightOn = !lightOn;
        }

        public override void OnInteract(Vector3 localMousePoint, IPlayer player)
        {
            switchLight();
        }
    }
}
