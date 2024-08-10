using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{

    public GameTile GameTilePrefab;

    public int GameWidth;

    public int GameHeight;

    public int TileWidth = 5;

    // Start is called before the first frame update
    void Start()
    {
        SetupCamera();

        //TODO: this is not performant, may need to store neighbors in 2D array or something instead of the game objects?
        //TODO: this is not performant, may need to instantiate objects differently or just do more research to understand why
        //TODO: nest generated tiles under an empty game object or something... clutter clutter clutter...
        GenerateCartesianGrid();

        //TODO: implement algorithm
    }

    // Update is called once per frame
    void Update()
    {

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

    private void GenerateCartesianGrid()
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

                // seed the grid
                tile.isAlive = new System.Random().NextDouble() >= 0.5; //TESTING COLORS

                //TODO: determine lifecycle
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

                // TODO: can this be async?
                Instantiate(tile, position, Quaternion.identity);

            }
        }
    }
}
