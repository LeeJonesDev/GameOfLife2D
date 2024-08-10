using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{

    public void OnStartButtonClick()
    {
        Debug.Log("clicked start.");


        // var (gameHeight, gameWidth) = GetGridDimensions();

        // //Iteration
        // var states = new List<(bool isAlive, bool newState, CartesianCoordinates coordinates)>();

        // //TODO: recursion?
        // for (var y = 0; y < (gameHeight - 1); y++)
        // {
        //     for (var x = 0; x < (gameWidth - 1); x++)
        //     {

        //         //TODO: get current state


        //         //TODO: check whether should live or die and save the future state for updating later

        //     }
        // }
        // //TODO: update state


        var gameTiles = FindObjectsOfType<GameTile>();

        foreach (var tile in gameTiles)
        {
            //TODO: get current state
            //tile.isAlive;
            //TODO: check whether should live or die and save the future state for updating later    
            //tile.shouldLive = tile.DetermineIfShouldLive();
        }

        //TODO: update states
        //update all the tiles shouldlives to isalives


    }

    public void OnStopButtonClick()
    {
        Debug.Log("clicked stop.");
    }


    // public (bool isAlive, bool newState, CartesianCoordinates coordinates) CheckTile()
    // {
    //     var gridGenerator = ;
    // }

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
