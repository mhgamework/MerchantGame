using UnityEngine;
using System.Collections;
using Assets.MineMinecraft;

public interface IWorld
{
    IBlock GetBlockAt(Vector3 v);
    void SetBlockAt(Vector3 v,IBlock block);

}