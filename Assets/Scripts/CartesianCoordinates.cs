using UnityEngine;

[System.Serializable]
public struct CartesianCoordinates
{
    public int X;
    public int Y;

    public CartesianCoordinates(int xCoordinate, int yCoordinate)
    {
        X = xCoordinate;
        Y = yCoordinate;
    }
}
