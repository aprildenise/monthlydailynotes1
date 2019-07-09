using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneAnimTrigger : MonoBehaviour
{

    public MainManager gameController;
    public PlayerController playerController;


    public void FinishedInteracting()
    {
        if (!gameController.onPhoneEnd)
        {
            gameController.InteractWithPhone(false);
            playerController.onPhone = false;
        }
        
    }

}
