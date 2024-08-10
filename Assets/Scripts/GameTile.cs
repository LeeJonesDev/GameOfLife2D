using UnityEngine;

public class GameTile : MonoBehaviour
{
    public bool isAlive = true;

    public CartesianCoordinates Coordinates;

    public CartesianCoordinates LeftNeighbor;
    public CartesianCoordinates RightNeighbor;
    public CartesianCoordinates AboveNeighbor;
    public CartesianCoordinates BelowNeighbor;

    public void DetermineLifecycle()
    {
        //TODO: determine lifecycle
        //Set lifecycle
        if (TryGetComponent<SpriteRenderer>(out var spriteRenderer))
        {
            if (!isAlive)
            {
                spriteRenderer.color = Color.white;
            }
            else
            {
                spriteRenderer.color = Color.black;
            }
        }

    }
}
