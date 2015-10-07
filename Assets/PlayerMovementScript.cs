using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets;

public class PlayerMovementScript : MonoBehaviour
{

    public float MoveSpeed = 3;
    public Vector3 TargetedPosition;
    [SerializeField]
    private ClickedPositionIndicatorScript ClickedPositionIndicator;

    [SerializeField]
    private Transform PlayerTransform;

    public Vector3 TargetMovePosition;

    // Use this for initialization
    void Start()
    {
        TargetMovePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var dir = TargetMovePosition - PlayerTransform.position;

        var f = Time.deltaTime * MoveSpeed;
        PlayerTransform.position = dir.magnitude < f ? TargetMovePosition : PlayerTransform.position + dir.normalized * f;




        var hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition)).OrderBy(h => h.distance);
        if (!hits.Any()) return;

        // Filter hits

        TargetedPosition = hits.First().point;

        if (Input.GetMouseButtonDown(0))
        {
            var interactables = hits.First().collider.GetComponentsInParent(typeof(IPlayerInteractable)).Cast<IPlayerInteractable>().ToArray();
            if (!interactables.Any())
            {
                // Move towards
                MoveTo(TargetedPosition);
                return;
            }
            if (interactables.Count() > 1) throw new InvalidOperationException("Unexpected behavioru with nested interactbales, not supported");

            interactables.First().Interact(this);

        }

        /*foreach (var hit in hits)
	    {
	        
	    }*/


    }


    public void MoveTo(Vector3 position)
    {
        TargetMovePosition = position;
        ClickedPositionIndicator.Show(position);
    }
}
