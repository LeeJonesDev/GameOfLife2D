using UnityEngine;

public class GameTile : MonoBehaviour
{
    public bool isAlive;
    public bool shouldLive;

    public CartesianCoordinates Coordinates;

    public CartesianCoordinates LeftNeighborCoords;
    public GameTile LeftNeighborTile;

    public CartesianCoordinates RightNeighborCoords;
    public GameTile RightNeighborTile;

    public CartesianCoordinates AboveNeighborCoords;
    public GameTile AboveNeighborTile;

    public CartesianCoordinates BelowNeighborCoords;
    public GameTile BelowNeighborTile;

    public bool DetermineIfShouldLive()
    {
        /*
            1. Any live cell with fewer than two live neighbors dies, as if by underpopulation.

            2. Any live cell with two or three live neighbors lives on to the next generation.

            3. Any live cell with more than three live neighbors dies, as if by overpopulation.

            4. Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
        */

        //if(isAlive && )
        return true;
    }
}
