using System.Linq;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    // typically set to 0.5 for 500 ms
    public float IterationIntervalInSeconds;
    bool _isActive;
    float _time;


    void Start()
    {
        _isActive = false;
        _time = 0;
    }

    void Update()
    {
        //TODO: this timing isn't right I don't think? probably need a while loop
        if (_isActive)
        {
            _time += Time.deltaTime;

            // essentially i think this works like a semaphore
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
    }

    //TODO: this is not working, revisit
    public void OnStopButtonClick()
    {
        _isActive = false;
        _time = 0;
    }

    public void PerformIteration()
    {
        if (_isActive)
        {
            var gameTiles = FindObjectsOfType<GameTile>().ToList();

            //TODO: do this recursively? it may simplify logic / iterations
            // Get the future states
            foreach (var tile in gameTiles)
            {
                tile.shouldLive = tile.DetermineIfShouldLive();
            }

            // Reiterate :( and get set the current states now that we have all the future states
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
}
