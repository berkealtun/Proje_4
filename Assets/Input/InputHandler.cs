using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
   private PlayerInputs playerInputs;

    private void Awake()
    {
         playerInputs = new PlayerInputs();
         
    }

   void Update()
    {
        Debug.Log(playerInputs.Player.Move.ReadValue<Vector2>());

    }
    private void OnEnable()
    {
        playerInputs.Enable();
    }

    private void OnDisable()
    {
        playerInputs.Disable();
    }
}
