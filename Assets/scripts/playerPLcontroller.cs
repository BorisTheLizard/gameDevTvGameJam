using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPLcontroller : MonoBehaviour
{

    [Header("references")]
    private Camera mainCamera;
    [SerializeField] CharacterController cc;
    public Vector3 moveVelocity;
    [SerializeField] LayerMask groundLayers;
    public float moveSpeed;
    [SerializeField] float zPosition;
    [SerializeField] GameObject shootingSphere;
    [SerializeField] GameObject playerModel;
    [SerializeField] GameObject headColCastPoint;

    [Header("bools")]
    public bool canControl=true;

    [Header("JUMP")]
    [SerializeField] float jumpForce = 10f;
    public float gravity=10f;
    public bool makeADoubleJump = false;


    [SerializeField] Animator anim;
    public float CCSpeed;
    [SerializeField] Vector3 lastPosition;

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
    }


    private void Update()
	{
        CalculateSpeed();

        if (canControl)
        {
            playerTransform();
            
            
            if (cc.isGrounded)
            {
                Jump();
            }
			else
			{
                headCollisionCast();
                DoubleJump();
            }

            Aim();
        }

        if (!cc.isGrounded)
        {
            applyGravity();
            anim.SetBool("inAir", true);
        }
		else
		{
            anim.SetBool("inAir", !true);
        }
    }

	private void playerTransform()
	{
        if (transform.position.x != zPosition)
        {
            moveVelocity.x = (zPosition - transform.position.x) * 10f;
        }

        float xValue = Input.GetAxis("Horizontal");

        moveVelocity.z = moveSpeed * xValue;
        cc.Move(moveVelocity * Time.deltaTime);

        if (moveVelocity != Vector3.zero)
        {
            RotateHorizontally(xValue);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            makeADoubleJump = true;
            moveVelocity.y = jumpForce;
            anim.SetTrigger("jump");
            //jumpAudioSource.clip = jumpClip[Random.Range(0, jumpClip.Length)];
            //jumpAudioSource.Play();
        }
    }
    private void DoubleJump()
    {
        if (makeADoubleJump & Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("jump");
            moveVelocity.y = 0;
            moveVelocity.y = jumpForce * 0.75f;
            makeADoubleJump = false;
        }
    }
    private void applyGravity()
	{
        moveVelocity.y -= gravity * Time.deltaTime;
    }
    void RotateHorizontally(float horizontalInput)
    {
        if (horizontalInput > 0)
        {
            // Calculate target angle
            float targetAngle = Mathf.LerpAngle(playerModel.transform.localEulerAngles.y, 0f, Mathf.Clamp01(horizontalInput));

            // Apply rotation smoothly
            playerModel.transform.localEulerAngles = new Vector3(0f, targetAngle, 0f);
        }
        else if (horizontalInput < 0)
        {
            // Calculate target angle
            float targetAngle = Mathf.LerpAngle(playerModel.transform.localEulerAngles.y, -180f, Mathf.Clamp01(Mathf.Abs(horizontalInput)));

            // Apply rotation smoothly
            playerModel.transform.localEulerAngles = new Vector3(0f, targetAngle, 0f);
        }
    }
    private void Aim()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(-Vector3.right, Vector3.zero);
        float rayLength = 9999;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, color: Color.red);
            Vector3 lookOnPoint = (new Vector3(shootingSphere.transform.position.x, pointToLook.y, pointToLook.z));
            shootingSphere.transform.LookAt(lookOnPoint);
        }
    }

    private void headCollisionCast()
	{
		if (Physics.Raycast(headColCastPoint.transform.position, headColCastPoint.transform.up, 0.25f, groundLayers))
		{
            Debug.DrawRay(headColCastPoint.transform.position, headColCastPoint.transform.up, Color.red, 1);
            moveVelocity.y = -1;
		}
	}

    private void CalculateSpeed()
    {
        CCSpeed = Mathf.Lerp(CCSpeed, (transform.position - lastPosition).magnitude / Time.unscaledDeltaTime, 6f);
        lastPosition = transform.position;

        anim.SetFloat("moveSpeed", CCSpeed);
    }

    private void animationController()
	{

	}
}
