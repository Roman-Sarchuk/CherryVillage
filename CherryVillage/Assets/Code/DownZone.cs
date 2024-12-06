using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            player.IsAbleMoveOut = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            player.IsAbleMoveOut = false;
        }
    }
}
