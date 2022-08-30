using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using FPS_Prototype.General;

namespace FPS_Prototype.Player
{
    public class ReadPlayerInput : MonoBehaviour
{
    #region Variables
    [Header("Components")]    /********/
    [SerializeField]
    PlayerComponents player;


    private InputMap keyMap;

    #endregion

    #region Base Methods
    void Awake()
    {
        keyMap = new InputMap();

    }
    void OnEnable()
    {
        keyMap.Enable();
    }
    void OnDisable()
    {
        keyMap.Disable();
    }


    #endregion



    #region Action Inputs

    public void RunContext(CallbackContext context)
    {
        player.run.inputDirection = context.ReadValue<Vector2>();
    }



    public void JumpContext(CallbackContext context)
    {
        if (context.started) player.jump.ButtonDown();
        else if (context.canceled) player.jump.ButtonUp();
    }

    public void ShootContext(CallbackContext context)
    {
        if (context.started) player.shoot.ButtonDown();
        else if (context.canceled) player.shoot.ButtonUp();
    }

    public void WeaponSlot1(CallbackContext context)
    {
        if (context.started) player.inventory.EquipWeapon(0);
    }

    public void WeaponSlot2(CallbackContext context)
    {
        if (context.started) player.inventory.EquipWeapon(1);
    }

    public void WeaponSlot3(CallbackContext context)
    {
        if (context.started) player.inventory.EquipWeapon(2);
    }


    public void WeaponMouseScroll(CallbackContext context)
    {
        player.inventory.MouseScrollSwitch(context.ReadValue<float>());
    }
    public void PauseContext(CallbackContext context)
    {
        if (context.started) StatsEventManager.PauseToggle();

    }


    #endregion

}

}