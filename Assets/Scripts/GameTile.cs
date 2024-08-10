using UnityEngine;

public class GameTile : MonoBehaviour
{
    public bool isAlive = true;

    public CartesianCoordinates Coordinates;

    public CartesianCoordinates LeftNeighbor;
    public CartesianCoordinates RightNeighbor;
    public CartesianCoordinates AboveNeighbor;
    public CartesianCoordinates BelowNeighbor;

    //TODO: methods for onClick
}
