using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets;
using UnityEngine.EventSystems;

public class PlayerMovementScript : MonoBehaviour
{

    public float MoveSpeed = 3;
    public Vector3 TargetedPosition;
    [SerializeField]
    private ClickedPositionIndicatorScript ClickedPositionIndicator;

    [SerializeField]
    private Transform PlayerTransform;

    public Vector3 TargetMovePosition;
    private Action onArriveAction;


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
        if (dir.magnitude < f)
        {
            PlayerTransform.position = TargetMovePosition;
            if (onArriveAction != null) onArriveAction();
            onArriveAction = null;
        }
        else PlayerTransform.position = PlayerTransform.position + dir.normalized*f;


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
        MoveTo(position, null);
    }
    public void MoveTo(Vector3 position, Action onArrive)
    {
        TargetMovePosition = position;
        ClickedPositionIndicator.Show(position);
        onArriveAction = onArrive;
    }


    public List<InventoryItem> Inventory = new List<InventoryItem>();

    public void PickupResources(string resourceType, int amount)
    {
        var i = Inventory.FirstOrDefault(j => j.ResourceType == resourceType);
        if (i == null)
        {
            i = new InventoryItem(resourceType, 0);
            Inventory.Add(i);
        }

        i.Amount += amount;
    }

    [Serializable]
    public class InventoryItem
    {
        public string ResourceType;
        public int Amount;

        public InventoryItem(string resourceType, int amount)
        {
            ResourceType = resourceType;
            Amount = amount;
        }
    }

}
