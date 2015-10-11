using UnityEngine;
using System.Collections;
using Assets;

public class ResourceProducerScript : MonoBehaviour, IPlayerInteractable
{
    [SerializeField]
    private Canvas WindowCanvas;

    public bool TradeEnabled;

    // Use this for initialization
    void Start()
    {
        HideWindow();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact(PlayerMovementScript playerMovementScript)
    {
        playerMovementScript.MoveTo(transform.position, () =>
        {
            ShowWindow();
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
