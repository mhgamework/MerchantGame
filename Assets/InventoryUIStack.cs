using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryUIStack : MonoBehaviour
{
    [SerializeField]
    private RawImage img;
    [SerializeField]
    private Text text;

    private string resourceType;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateUI(string type, int amount)
    {
        if (resourceType != type)
        {
            resourceType = type;
            updateImage();
        }
        text.text = amount.ToString();
    }

    private void updateImage()
    {
        var type = ResourceTypesScript.Instance().Find(resourceType);
        var tex = RenderToTextureScript.Instance().PrefabToTexture(type.UIPrefab);
        img.texture = tex;
    }
}
