using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] private bool movementEnabled = true;
    [SerializeField] private CharacterController controller;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GravityStats gravityStats;

    private new Transform camera;
    [SerializeField] private CinemachineFreeLook look;

    [SerializeField] private Vector3 vector;
    public bool inIdleState;
    public bool movable;

    void Start()
    {
        movable = true;
        camera = Camera.main.transform;
        //look = GameObject.FindGameObjectWithTag("Cinemachine").GetComponent<CinemachineFreeLook>();
    }

    void Update()
    {
        if (inIdleState && movable) {
            if (SceneLoader.instance.isLoading) return;
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            bool isGrounded = controller.isGrounded;
            if (isGrounded && playerStats.velocity.y < 0f) playerStats.velocity.y = 0f;
            else if (gravityStats.applyGravity) ApplyGravity();

            Movement(horizontal, vertical);
        }
        inIdleState = false;
    }
    private void ApplyGravity()
    {

        playerStats.velocity.y += gravityStats.GetVerticalDisplacement(Time.deltaTime);
    }

    private void Movement(float horizontal, float vertical)
    {
        bool groundedPlayer = controller.isGrounded;

        Vector3 dir = new Vector3(horizontal, 0, vertical).normalized;
        Vector3 moveDir = Vector3.zero;
        if (dir != Vector3.zero) moveDir = CalcMoveDir(dir);
        vector = moveDir;
        controller.Move(playerStats.movementSpeed * Time.deltaTime * (moveDir + playerStats.velocity));
    }

    private Vector3 CalcMoveDir(Vector3 dir)
    {
        float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref playerStats.turnSmoothVelocity, playerStats.turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        return moveDir;
    }

    private void OnEnable()
    {
        PlayerIdleState.PlayerInIdleState += InIdleState;
        DialogTrigger.EnterDialogMode += DisableMovement;
        DialogManager.EnterDialog += DisableMovement;
        DialogManager.EndOfStory += EnableMovement;
    }

    private void OnDisable()
    {
        PlayerIdleState.PlayerInIdleState -= InIdleState;
        DialogTrigger.EnterDialogMode -= DisableMovement;
        DialogManager.EnterDialog -= DisableMovement;
        DialogManager.EndOfStory -= EnableMovement;
    }

    private void DisableMovement()
    {
        movable = false;
    }

    private void EnableMovement()
    {
        movable = true;
    }

    private void InIdleState()
    {
        inIdleState = true;
    }
}
