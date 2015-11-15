using System;
using System.Collections.Generic;
using Assets.MineMinecraft;
using Assets.MineMinecraft.MathUtils;
using UnityEngine;

public class DummyWorld : IWorld
{
    private Dictionary<Vector3, IBlock> blocks = new Dictionary<Vector3, IBlock>();

    public IBlock GetBlockAt(Vector3 v)
    {
        if (!blocks.ContainsKey(v)) return null;
        return blocks[v.Round()];
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
        blocks[v] = block;
        block.Position = v;
        block.OnCreate();
    }

    public void UpdateBlockModel(IBlock block)
    {
        
    }

}