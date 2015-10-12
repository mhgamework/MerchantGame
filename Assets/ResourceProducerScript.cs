using UnityEngine;
using System.Collections;
using Assets;

[RequireComponent(typeof(InventoryScript))]
public class ResourceProducerScript : MonoBehaviour, IPlayerInteractable
{
    [SerializeField]
    private Canvas WindowCanvas;

    public string ProducedType;
    public int Max;
    public float ItemsPerMin;


    public bool TradeEnabled;


    private float nextItem;
    private float itemInterval;

    private InventoryScript inventory;


    // Use this for initialization
    void Start()
    {
        //HideWindow();
        itemInterval = 1 / (ItemsPerMin / 60);
        nextItem = Time.timeSinceLevelLoad + itemInterval;
        inventory = GetComponent<InventoryScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nextItem <= Time.timeSinceLevelLoad)
        {
            nextItem += itemInterval;
            if (inventory.GetResourceCount(ProducedType) < Max)
                inventory.AddResources(ProducedType, 1);
        }
    }

    public void Interact(PlayerMovementScript playerMovementScript)
    {
        playerMovementScript.MoveTo(transform.position, () =>
        {
            playerMovementScript.PickupResources(ProducedType, inventory.GetResourceCount(ProducedType));
            inventory.RemoveResourcse(ProducedType, inventory.GetResourceCount(ProducedType));
        });
    }

    public void ShowWindow()
    {
        WindowCanvas.gameObject.SetActive(true);
    }

    public void HideWindow()
    {
        WindowCanvas.gameObject.SetActive(false);

    }

    public void SetTradeEnabled(bool enabled)
    {
        TradeEnabled = enabled;
    }
}
