using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            player.IsAbleMoveInto = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement player))
        {
            player.IsAbleMoveInto = false;
        }
    }
}
