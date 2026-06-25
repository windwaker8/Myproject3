using UnityEngine;
using UnityEngine.InputSystem; // required for new Input System

public class PlayerMovement : MonoBehaviour, PlayerControls.IInputactionsActions
{
    public float moveSpeed = 5f;
    private PlayerControls controls;
    private Vector2 moveInput;
    private Rigidbody2D rb;

    void Awake()
    {
        controls = new PlayerControls();
        controls.inputactions.SetCallbacks(this); // hook into your input map
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable() => controls.inputactions.Enable();
    void OnDisable() => controls.inputactions.Disable();

    // These methods are automatically called when input actions fire:
    public void OnMove_up(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) moveInput.y = 1;
        if (ctx.canceled) moveInput.y = 0;
    }

    public void OnMove_down(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) moveInput.y = -1;
        if (ctx.canceled) moveInput.y = 0;
    }

    public void OnMove_left(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) moveInput.x = -1;
        if (ctx.canceled) moveInput.x = 0;
    }

    public void OnMove_right(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) moveInput.x = 1;
        if (ctx.canceled) moveInput.x = 0;
    }

    void FixedUpdate()
    {
        // Normalize so diagonal speed isn’t faster
        Vector2 normalized = moveInput.normalized;
        rb.MovePosition(rb.position + normalized * moveSpeed * Time.fixedDeltaTime);
    }
}
