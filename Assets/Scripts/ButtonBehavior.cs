using System.Linq;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    // typlically set to 0.5 for 500 ms
    public float IterationIntervalInSeconds;
    bool _isActive;
    float _time;

    void Start()
    {
        _isActive = false;
    }

    void Update()
    {
        //TODO: this timing isn't right, revisit that math.
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

    //TODO: this is not working, revisit
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
