using UnityEngine;

public class HeadBob : MonoBehaviour
{
    public float bobSpeed = 14f;
    public float bobAmount = 0.05f;

    public CharacterController player;
    private float defaultY;
    private float timer;

    void Start()
    {
        defaultY = transform.localPosition.y;
    }

    void Update()
    {
        bool isMoving = player.velocity.magnitude > 0.1f && player.isGrounded;

        if (isMoving)
        {
            timer += Time.deltaTime * bobSpeed;
            float newY = defaultY + Mathf.Sin(timer) * bobAmount;

            transform.localPosition = new Vector3(
                transform.localPosition.x,
                newY,
                transform.localPosition.z
            );
        }
        else
        {
            // Return smoothly when stopping
            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                new Vector3(transform.localPosition.x, defaultY, transform.localPosition.z),
                Time.deltaTime * bobSpeed
            );
        }
    }
}
