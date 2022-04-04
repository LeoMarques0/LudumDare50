using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Player_InputSystem : MonoBehaviour
{
    private Player_Input_Actions playerInputsActions;

    [HideInInspector] public InputAction movement;
    [HideInInspector] public InputAction shoot;
    [HideInInspector] public InputAction aim;

    public void SetInputs()
    {
        playerInputsActions = new Player_Input_Actions();

        movement = playerInputsActions.Player.Movement;
        shoot = playerInputsActions.Player.Shoot;
        aim = playerInputsActions.Player.Aim;

        movement.Enable();
        shoot.Enable();
        aim.Enable();
    }

    public void SetDash(Action<CallbackContext> dashAction)
    {
        playerInputsActions.Player.Dash.performed += dashAction;
        playerInputsActions.Player.Dash.Enable();
    }

    public void SetShoot(Action<CallbackContext> startShoot, Action<CallbackContext> stopShoot)
    {
        shoot.started += startShoot;
        shoot.canceled += stopShoot;
        shoot.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        shoot.Disable();
        aim.Disable();
        playerInputsActions.Player.Dash.Disable();
    }
}
