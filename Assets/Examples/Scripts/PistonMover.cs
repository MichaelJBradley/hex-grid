using System.Collections.Generic;
using Grid;
using UnityEngine;

class Piston
{
    public Transform transform;
    public bool movingUp;
    public float time;
    public float speed;

    public Piston(Transform t, bool up, float speed)
    {
        transform = t;
        movingUp = up;
        this.speed = speed;
        time = 0.0f;
    }
}

public class PistonMover : MonoBehaviour
{
    public float pistonSpeed = 0.2f;
    public float maxHeight = 1f;
    public float minHeight = -0.75f;
    public Hex[] pistonsToMove;

    private HexGrid grid;
    private bool once;
    private List<Piston> pistons;
    
    // Start is called before the first frame update
    void Start()
    {
        once = false;
        grid = GetComponent<HexGrid>();
        pistons = new List<Piston>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!once)
        {
            GetPistons();
        }

        if (grid.Loaded)
        {
            MovePistons();
        }
    }

    /// <summary>
    /// Pulls the piston components out of the Hex grid and adds them to the
    /// piston list.
    /// </summary>
    private void GetPistons()
    {
        if (grid.Loaded)
        {
            foreach (Hex hex in pistonsToMove)
            {
                if (grid.Hexes.ContainsKey(hex))
                {
                    HexTile hexTile = grid.Hexes[hex];
                    pistons.Add(new Piston(
                        // GetComponentsInChildren includes its own transform, so the next element is the piston.
                        hexTile.GetComponentsInChildren<Transform>()[1],
                        false,
                        pistonSpeed + (Random.value / 10.0f)
                    ));
                }
            }

            once = true;
        }
    }

    /// <summary>
    /// Move each piston up or down slightly depending on how much time has
    /// passed since the last update.
    /// </summary>
    private void MovePistons()
    {
        foreach (Piston piston in pistons)
        {
            Vector3 curPos = piston.transform.position;
            if (curPos.y <= minHeight)
            {
                piston.movingUp = true;
                piston.time = 0.0f;
            }
            if (curPos.y >= maxHeight)
            {
                piston.movingUp = false;
                piston.time = 0.0f;
            }
            piston.time += Time.deltaTime * piston.speed;

            float fromHeight = piston.movingUp ? minHeight : maxHeight;
            float toHeight = piston.movingUp ? maxHeight : minHeight;
            curPos.y = Mathf.Lerp(fromHeight, toHeight, piston.time);
            
            piston.transform.SetPositionAndRotation(curPos, Quaternion.identity);
        }
    }
}
