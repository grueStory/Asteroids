using UnityEngine;

public class Wrap : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Vector3 viewportPosition = _camera.WorldToViewportPoint(transform.position);
        Vector3 moveAdjusment = Vector3.zero;

        if (viewportPosition.x < 0)
        {
            moveAdjusment.x += 1;
        }

        else if (viewportPosition.x > 1)
        {
            moveAdjusment.x -= 1;
        }
        
        else if (viewportPosition.y < 0)
        {
            moveAdjusment.y += 1;
        }

        else if (viewportPosition.y > 1)
        {
            moveAdjusment.y -= 1;
        }

        transform.position = _camera.ViewportToWorldPoint(viewportPosition + moveAdjusment);
    }
}