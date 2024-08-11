using System.Linq;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    bool _isActive;
    float _time;
    public float IterationIntervalInSeconds;

    void Start()
    {
        _isActive = false;
    }

    void Update()
    {
        //TODO: this timing isn't right
        if (_isActive)
        {
            _time += Time.deltaTime;

            if (_time >= IterationIntervalInSeconds)
            {
                PerformIteration();
                _time -= IterationIntervalInSeconds;
            }
        }
    }

    public void OnStartButtonClick()
    {
        _isActive = true;
        _time = 0;
    }

    //TODO: this is not working
    public void OnStopButtonClick()
    {
        Debug.Log("clicked stop.");
        _isActive = false;
        _time = 0;
    }

    public void PerformIteration()
    {
        if (_isActive)
        {
            var gameTiles = FindObjectsOfType<GameTile>().ToList();

            foreach (var tile in gameTiles)
            {
                //TODO: get current state
                //tile.isAlive;
                //TODO: check whether should live or die and save the future state for updating later    
                tile.shouldLive = tile.DetermineIfShouldLive();
            }

            //TODO: update states
            //update all the tiles shouldlives to isalives
            foreach (var tile in gameTiles)
            {
                tile.isAlive = tile.shouldLive;

                var spriteRenderer = tile.GetComponentInParent<SpriteRenderer>();
                spriteRenderer.color = !tile.isAlive
                    ? Color.black
                    : Color.white;
            }
        }
    }


    public (int gridHeight, int gridWidth) GetGridDimensions()
    {
        int gameHeight, gameWidth;

        var gridGenerator = GameObject.Find("Grid Generator");
        if (gridGenerator.gameObject.TryGetComponent<GenerateGrid>(out var gridGenScript))
        {
            gameHeight = gridGenScript.GameHeight;
            gameWidth = gridGenScript.GameWidth;
        }
        else
        {
            throw new MissingComponentException("Grid Generator is missing the grid generation script");
        }

        return (gameHeight, gameWidth);
    }


}
