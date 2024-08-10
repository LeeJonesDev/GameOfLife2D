using System;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    public GameTile GameTilePrefab;

    public int GameWidth;

    public int GameHeight;

    public int TileWidth = 5;

    public void OnButtonClick()
    {
        //TODO: don't do this on each click necessarily
        SetupCamera();


        //TODO: don't do this on each click necessarily
        //TODO: this is not performant, may need to store neighbors in 2D array or something instead of the game objects?
        //TODO: this is not performant, may need to instantiate objects differently or just do more research to understand why
        //TODO: nest generated tiles under an empty game object or something... clutter clutter clutter...
        GenerateGrid();

        //TODO: run game /or stop game
        //TODO: implement algorithm

    }

    /* TODO: camera isn't working quite like i want :(, but i got it positioned on 
        the grid but don't love the math here or the positioning but it works well 
        enough to move on for now and make it better later after I have the guts working.
    */
    private void SetupCamera()
    {
        var camera = GameObject.Find("Main Camera");
        camera.transform.position = new Vector3(
            TileWidth * ((GameHeight - 1) / 2),
            TileWidth * ((GameWidth - 1) / 2),
            -1);
    }

    private void GenerateGrid()
    {
        for (var y = 0; y < (GameHeight - 1); y++)
        {
            for (var x = 0; x < (GameWidth - 1); x++)
            {
                Vector3 position;
                position.x = TileWidth * x;
                position.y = TileWidth * y;
                position.z = 0.1f;

                var tile = GameTilePrefab;


                // note: only using width because these are square tiles
                tile.transform.localScale = new Vector3(TileWidth, TileWidth, 1);

                tile.Coordinates = new CartesianCoordinates(x, y);

                tile.gameObject.name = $"Tile - {x}, {y} ";

                // TODO: handle grid edges
                tile.AboveNeighbor = new CartesianCoordinates(x, y + 1);
                tile.BelowNeighbor = new CartesianCoordinates(x, y - 1);

                tile.RightNeighbor = new CartesianCoordinates(x + 1, y);
                tile.LeftNeighbor = new CartesianCoordinates(x - 1, y);

                //TODO: determine lifecycle
                tile.isAlive = new System.Random().NextDouble() >= 0.5; //TESTING COLORS

                //Set lifecycle
                if (tile.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
                {
                    if (!tile.isAlive)
                    {
                        spriteRenderer.color = Color.black;
                    }
                    else
                    {
                        spriteRenderer.color = Color.white;
                    }
                }




                Instantiate(tile, position, Quaternion.identity);

            }
        }
    }
}
