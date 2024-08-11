using System;
using System.Linq;
using UnityEngine;

public class GenerateGrid : MonoBehaviour
{
    public GameTile GameTilePrefab; // This object is will be generated for each tile

    public int GameWidth; // number of tiles

    public int GameHeight; // number of tiles

    public int TileWidth = 5; // how big the tiles are

    /// <summary>
    /// Generate the grid and initialize the camera
    /// </summary>
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

    /// <summary>
    /// Gets the camera viewport to contain the whole grid
    /// </summary>
    private void SetupCamera()
    {
        var camera = GameObject.Find("Main Camera");
        camera.transform.position = new Vector3(
            TileWidth * GameHeight / 2,
            TileWidth * GameWidth / 2,
            -1);

        if (camera.TryGetComponent<Camera>(out var cameraComponent))
        {
            var maxSize = Math.Max(GameWidth, GameHeight);
            cameraComponent.orthographicSize = TileWidth * maxSize / 2 + TileWidth;
        }
        else
        {
            throw new MissingComponentException("missing camera on the main camera. we have a bigger problem here...");
        }
    }

    /// <summary>
    /// This generates the initial grid
    /// </summary>
    private void GenerateCartesianGrid()
    {
        for (var y = 0; y < GameHeight; y++)
        {
            for (var x = 0; x < GameWidth; x++)
            {
                //calculate the position of the tile
                Vector3 position;
                position.x = TileWidth * x;
                position.y = TileWidth * y;
                position.z = 0.1f;

                var tile = GameTilePrefab;


                // note: only using width because these are square tiles
                tile.transform.localScale = new Vector3(TileWidth, TileWidth, 1);

                // set the base coordinates for the tile
                tile.Coordinates = new CartesianCoordinates(x, y);

                // change the name of the GameObject to be human readable
                tile.gameObject.name = $"Tile - {x}, {y} ";

                // TODO: handle grid edges?
                // generate the neighbors
                tile.AboveNeighborCoords = new CartesianCoordinates(x, y + 1);
                tile.BelowNeighborCoords = new CartesianCoordinates(x, y - 1);
                tile.RightNeighborCoords = new CartesianCoordinates(x + 1, y);
                tile.LeftNeighborCoords = new CartesianCoordinates(x - 1, y);

                // seed the grid with random starting data
                tile.isAlive = new System.Random().NextDouble() >= 0.25;


                //TODO: determine lifecycle
                //Set lifecycle
                if (tile.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
                {
                    spriteRenderer.color = !tile.isAlive
                        ? Color.black
                        : Color.white;
                }
                else
                {
                    throw new MissingComponentException("No SpriteRenderer on Grid Generator");
                }

                // TODO: can this be async?
                Instantiate(tile, position, Quaternion.identity);

            }
        }

        // Note: aww, I just read unity is not thread safe, this should be parallel though.
        //Set neighbor tiles
        var gameTiles = FindObjectsOfType<GameTile>();
        for (int i = 0; i < gameTiles.Length; i++)
        {
            GameTile tile = gameTiles[i];
            tile.AboveNeighborTile = gameTiles.SingleOrDefault(t =>
                t.Coordinates.X == tile.AboveNeighborCoords.X &&
                t.Coordinates.Y == tile.AboveNeighborCoords.Y);

            tile.BelowNeighborTile = gameTiles.SingleOrDefault(t =>
                t.Coordinates.X == tile.BelowNeighborCoords.X &&
                t.Coordinates.Y == tile.BelowNeighborCoords.Y);

            tile.LeftNeighborTile = gameTiles.SingleOrDefault(t =>
                t.Coordinates.X == tile.LeftNeighborCoords.X &&
                t.Coordinates.Y == tile.LeftNeighborCoords.Y);

            tile.RightNeighborTile = gameTiles.SingleOrDefault(t =>
                t.Coordinates.X == tile.RightNeighborCoords.X &&
                t.Coordinates.Y == tile.RightNeighborCoords.Y);
        }
    }
}
