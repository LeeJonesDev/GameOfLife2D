using Unity.VisualScripting;
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

    //TODO: this isn't quite right!! There is an edge case that I'm forgetting
    public bool DetermineIfShouldLive()
    {
        var sumLiveNeighbors = 0 +
            (LeftNeighborTile != null && LeftNeighborTile.isAlive ? 1 : 0) +
            (RightNeighborTile != null && RightNeighborTile.isAlive ? 1 : 0) +
            (AboveNeighborTile != null && AboveNeighborTile.isAlive ? 1 : 0) +
            (BelowNeighborTile != null && BelowNeighborTile.isAlive ? 1 : 0);


        var isDead = !isAlive;

        //has fewer than 2 live neighbors,  can die if alive
        var isUnderPopulated = sumLiveNeighbors < 2;

        //has 2 or 3 live neighbors, can live if alive
        var isSustained = sumLiveNeighbors > 1 && sumLiveNeighbors < 4;

        //has more than 3 live neighbors, can die if alive
        var isOverpopulated = sumLiveNeighbors == 4;

        // has exactly 3 live neighbors, can revive if dead
        var hasSpawn = sumLiveNeighbors == 3;


        // dead state changes
        if (isDead)
        {
            if (hasSpawn) // 3
            {
                return true;
            }

            return false; // 1, 2, 4
        }

        // alive state changes
        if (isAlive)
        {
            if (isSustained) // 2, 3
            {
                return true;
            }

            if (isOverpopulated) // 4
            {
                return false;
            }

            if (isUnderPopulated) // 1
            {
                return false;
            }
        }

        throw new InvalidImplementationException("Invalid logic in iteration, " +
            $"we should never reach this point. Tile {Coordinates.X}, {Coordinates.Y}");

    }
}
