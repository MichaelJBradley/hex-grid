using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grid;

public class RandomSelect : MonoBehaviour
{
    public HexGrid grid;
    public float timeBetween = .5f;

    private Hex current;
    private Hex min;
    private Hex max;
    private Color previousColor;

    private float currentTime;
    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        SingleMaterialDiamondShapedGridGenerator generator = grid.GetComponent<SingleMaterialDiamondShapedGridGenerator>();
        min = generator.min;
        max = generator.max;
        current = new Hex();
    }

    // Update is called once per frame
    void Update()
    {
        if (once && GetHexTileMaterial(current))
        {
            previousColor = GetHexTileMaterial(current).color;
            once = false;
        }
        
        if (!once && currentTime >= timeBetween)
        {
            currentTime = 0.0f;
            Select();
        }

        currentTime += Time.deltaTime;
    }

    void Select()
    {
        GetHexTileMaterial(current).color = previousColor;
        current.Q = Random.Range(min.Q, max.Q);
        current.R = Random.Range(min.R, max.R);
        Material selectedMaterial = GetHexTileMaterial(current);
        if (!selectedMaterial)
        {
            Debug.LogError("No material on HexTile " + current);
        }
        previousColor = selectedMaterial.color;
        selectedMaterial.color = new Color(Random.value, Random.value, Random.value);
    }

    Material GetHexTileMaterial(Hex pos)
    {
        HexTile selected = grid.Hexes[pos];
        return selected.GetComponent<MeshRenderer>().material;
    }
}
