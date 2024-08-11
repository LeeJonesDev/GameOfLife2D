[System.Serializable] //allows this struct to display in UI
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
