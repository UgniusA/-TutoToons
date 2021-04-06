using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScript : MonoBehaviour
{

    public void OnMouseDragInventoryOpen()
    {
        Time.timeScale = 0f;
    }
    public void OnMouseDragInventoryClose()
    {
        Time.timeScale = 1f;
    }

}
