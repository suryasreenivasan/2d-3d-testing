using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool x;
    public CharacterController controller;
    public Vector3 playerVelocity;
    public bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    public Transform dd;
    public Transform ddd;
    public float speed = 1.0f;
    public float startTime;
    public float journeyLength;
    public new Transform camera;
    public new Transform light;
    public bool isCameraMoving;
    public Vector3 targetPosition;
    public float timeCount;
    public bool isCameraRotating;
    public Quaternion targetRotation;
    public Vector3 offset;
    public Vector3 offset2;
    public Vector3 offset3;

    void Update()
    {
        light.position = transform.position + offset;
        dd.position = transform.position + offset2;
        ddd.position = transform.position + offset3;
        if (!x)
        {
            Camera.main.orthographic = true;
            if (isCameraMoving)
            {
                MoveCameraTowards(targetPosition);
            }
            else
            {
                camera.position = dd.position;
            }
            if (isCameraRotating)
            {
                RotateCameraTowards(targetRotation);
            }
            groundedPlayer = controller.isGrounded;
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector3 move = new Vector3(0, 0, Input.GetAxis("Horizontal"));
            controller.Move(move * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            if (Input.GetButtonDown("Jump") && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            if (Input.GetKeyDown("x"))
            {
                x = true;
                isCameraMoving = true;
                isCameraRotating = true;
                targetPosition = ddd.position;
                targetRotation = ddd.rotation;
                startTime = Time.time;
                timeCount = 0;
                journeyLength = Vector3.Distance(camera.position, targetPosition);
            }
        }

        else
        {
            Camera.main.orthographic = false;
            if (isCameraMoving)
            {
                MoveCameraTowards(targetPosition);
            }
            else
            {
                camera.position = ddd.position;
            }
            if (isCameraRotating)
            {
                RotateCameraTowards(targetRotation);
            }
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(move * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }

            if (Input.GetButtonDown("Jump") && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            if (Input.GetKeyDown("x"))
            {
                x = false;
                isCameraMoving = true;
                isCameraRotating = true;
                targetPosition = dd.position;
                targetRotation = dd.rotation;
                startTime = Time.time;
                timeCount = 0;
                journeyLength = Vector3.Distance(camera.position, targetPosition);
            }
        }
    }
    void MoveCameraTowards(Vector3 targetPosition)
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;
        camera.position = Vector3.Lerp(camera.position, targetPosition, fractionOfJourney);

        if (camera.position == targetPosition)
        {
            isCameraMoving = false;
        }
    }

    void RotateCameraTowards(Quaternion targetRotation)
    {
        camera.rotation = Quaternion.Slerp(camera.rotation, targetRotation, timeCount);
        timeCount += Time.deltaTime;

        if (camera.rotation == targetRotation)
        {
            isCameraRotating = false;
        }
    }
}