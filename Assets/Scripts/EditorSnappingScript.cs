﻿using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Idea: use floor instead of round, this makes it possible to add this script to already placed objects and not have them overlap, since they will all snap in the same direction
    /// </summary>
    [ExecuteInEditMode]
    public class EditorSnappingScript : MonoBehaviour
    {
        public float SnapSize = 1;
        public Vector3 Offset = new Vector3();

        public void Update()
        {
            if (Application.isPlaying) return;
            if (SnapSize == 0) SnapSize = 1;
            var pos = transform.position;
            pos += Offset;
            pos.x = Mathf.Round(pos.x / SnapSize) * SnapSize;
            pos.y = Mathf.Round(pos.y / SnapSize) * SnapSize;
            pos.z = Mathf.Round(pos.z / SnapSize) * SnapSize;

            pos -= Offset;
            transform.position = pos;
        }
    }
}