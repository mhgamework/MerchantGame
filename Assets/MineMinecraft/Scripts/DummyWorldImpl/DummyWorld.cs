using System;
using System.Collections.Generic;
using Assets.MineMinecraft;
using Assets.MineMinecraft.MathUtils;
using UnityEngine;
using Object = UnityEngine.Object;

public class DummyWorld : IWorld
{
    private Dictionary<Vector3, BlockData> blocks = new Dictionary<Vector3, BlockData>();



    public IBlock GetBlockAt(Vector3 v)
    {
        if (!blocks.ContainsKey(v)) return null;
        return blocks[v.Round()].Block;
    }

    public void SetBlockAt(Vector3 v, IBlock block)
    {
        Console.WriteLine("WORLD: Set block at " + v + " - " + block);
        v = v.Round();
        var old = GetBlockAt(v);
        if (old != null)
        {
            old.OnDestroy();
        }
        var bd = new BlockData();
        blocks[v] = bd;
        bd.Block = block;
        block.Position = v;
        UpdateBlockModel(block);
        block.OnCreate();
    }

    public void UpdateBlockModel(IBlock block)
    {
        var bd = blocks[block.Position.Round()];
        if (bd == null || bd.Block != block) throw new InvalidOperationException();
        if (bd.Model != null)
        GameObject.Destroy(bd.Model.gameObject);
        bd.Model = Object.Instantiate(block.GetModel());
        bd.Model.position = block.Position;

    }


    private class BlockData
    {
        public IBlock Block;
        public Transform Model;
    }

}