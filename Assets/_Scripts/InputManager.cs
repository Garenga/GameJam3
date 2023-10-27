using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputMap InputMap;

    private static InputManager _instance;

    public static InputManager Instance { get { return _instance; } }



    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
        InputMap = new InputMap();
    }

    private void OnEnable()
    {
        InputMap.Enable();
    }

    private void OnDisable()
    {
        InputMap.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return InputMap.Input.Movement.ReadValue<Vector2>();
    }
    public Vector2 GetMouseDelta()
    {
        return InputMap.Input.Look.ReadValue<Vector2>();
    }
    public bool PlayerJumped()
    {
        return InputMap.Input.Jump.triggered;
    }

    public bool PlayerSprint()
    {
        return InputMap.Input.Sprint.inProgress;
    }
    public bool PlayerPickUp()
    {
        return InputMap.Input.Pickup.IsPressed();
    }
    public bool PlayerUseAbility(AbilityType ability)
    {
        if (ability == AbilityType.Gravity)
        {
            return InputMap.Input.Ability.triggered;
        }
        else if (ability == AbilityType.Space)
        {
            return InputMap.Input.Ability.triggered;
        }
        else if (ability == AbilityType.Time)
        {
            return InputMap.Input.Ability.triggered;
        }
        else
        {
            return false;

        }
    }


}
