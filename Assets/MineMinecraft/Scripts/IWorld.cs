using UnityEngine;
using System.Collections;
using Assets.MineMinecraft;

public interface IWorld
{
    IBlock GetBlockAt(Vector3 v);
    void SetBlockAt(Vector3 v,IBlock block);

    /// <summary>
    /// Call this when the block model has changed
    /// </summary>
    /// <param name="block"></param>
    void InvalidateBlockModel(IBlock block);

}