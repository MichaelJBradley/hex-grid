using UnityEngine;
using UnityEngine.UI;

public class HexSelector : MonoBehaviour
{
    public Camera cam;
    public Text hexText;

    private Ray ray;

    void Start()
    {
        UpdateHexText("no hex selected");
    }

    // Update is called once per frame
    void Update()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool didHit = Physics.Raycast(ray, out hit, 100f);

        if (didHit)
        {
            HexTile hexTile = hit.collider.GetComponent<HexTile>();
            UpdateHexText(hexTile);
        }
        else
        {
            UpdateHexText("no hex selected");
        }
    }

    private void UpdateHexText(string text)
    {
        hexText.text = "Hex: " + text;
    }

    private void UpdateHexText(HexTile hex)
    {
        string desc = hex.PropertyBoolean["Impassable"] ? "Impassable" : hex.PropertyInt32["MovementCost"].ToString();
        hexText.text = "Hex: " + hex.pos + ": " + desc;
    }
}
