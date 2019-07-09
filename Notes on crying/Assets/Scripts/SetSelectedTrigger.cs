using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetSelectedTrigger : MonoBehaviour
{

    public EventSystem eventSystem;
    public GameObject button;

    public void SetSelected()
    {
        eventSystem.SetSelectedGameObject(button);
    }

}
