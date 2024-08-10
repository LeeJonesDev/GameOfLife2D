using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float PlayerSpeed = 10;

    // TODO: player movement isn't working, This isn non mvp, so I stopped messing with it
    void Update()
    {
        Vector3 playerMove = new Vector3(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"),
            0);

        transform.position += PlayerSpeed * Time.deltaTime * playerMove;
    }
}
