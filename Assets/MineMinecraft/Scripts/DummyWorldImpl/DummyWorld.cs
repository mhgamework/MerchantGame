using System;
using System.Collections.Generic;
using Assets.MineMinecraft;
using Assets.MineMinecraft.MathUtils;
using UnityEngine;
using Object = UnityEngine.Object;

public class DummyWorld : IWorld
{
    private Dictionary<Vector3, BlockData> blocks = new Dictionary<Vector3, BlockData>();

    public static Vector3[] CubeSides = new Vector3[] { Vector3.right, Vector3.up, Vector3.forward, -Vector3.right, -Vector3.up, -Vector3.forward };

    private BlockData GetBlockData(IBlock block)
    {
        var bd = blocks[block.Position];
        if (bd == null || bd.Block != block) throw new InvalidOperationException();
        return bd;
    }
    private BlockData GetBlockData(Vector3 v)
    {
        if (!blocks.ContainsKey(v)) return null;
        var bd = blocks[v];
        return bd;
    }

    public IBlock GetBlockAt(Vector3 v)
    {
        if (!blocks.ContainsKey(v)) return null;
        return blocks[v.Round()].Block;
    }

    public void SetBlockAt(Vector3 v, IBlock block)
    {
        Console.WriteLine("WORLD: Set block at " + v + " - " + block);
        v = v.Round();
        var oldbd = GetBlockData(v);
        if (oldbd != null)
        {
            oldbd.Block.OnDestroy();
            oldbd.DestroyModel();
            
        }
        blocks[v] = null;
        if (block != null)
        {
            var bd = new BlockData();
            blocks[v] = bd;
            bd.Block = block;
            block.Position = v;
            block.OnCreate();

            
        }
        // Update the block, and its neighbours (incase they are now hidden or visible)
        updateBlockModelAndNeighbours(v);
    }

    private void updateBlockModelAndNeighbours(Vector3 v)
    {
        UpdateBlockModel(v);
        for (int i = 0; i < CubeSides.Length; i++)
        {
            UpdateBlockModel(v + CubeSides[i]);
        }
    }

    public void InvalidateBlockModel(IBlock block)
    {
        var bd = GetBlockData(block);

        if (bd.Model != null)
            bd.DestroyModel();

        UpdateBlockModel(bd.Block.Position);

    }


    private void UpdateBlockModel(Vector3 pos)
    {
        BlockData bd;
        if (!blocks.TryGetValue(pos, out bd)) return;
        if (isHidden(pos)) bd.DestroyModel();
        else
        {
            // Assume unchanged, if the model needs refresh, the invalidatelbockmodel should've taken care of the removal
            if (bd.Model != null)
                return;

            var block = bd.Block;

            bd.Model = Object.Instantiate(block.GetModel());
            bd.Model.position = block.Position;
        }
    }

    private bool isHidden(Vector3 pos)
    {
        for (int i = 0; i < CubeSides.Length; i++)
        {
            if (!blocks.ContainsKey(pos + CubeSides[i]))
                return false;
        }
        return true;
    }

    private class BlockData
    {
        public IBlock Block;
        public Transform Model;


        public void DestroyModel()
        {
            if (Model == null) return;
            GameObject.Destroy(Model.gameObject);
            Model = null;
        }

    }

}