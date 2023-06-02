using UnityEngine;

public class HeadBobbing : PlayerComponent
{

    private Rigidbody rb;

    [SerializeField] private Transform camHolder;
    [SerializeField] private float speed;
    [SerializeField] private float amount;

    private float timer;
    private float originY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originY = camHolder.position.y;
        print(originY);
    }

    private void Update()
    {
        Headbob();
    }

    private void Headbob()
    {
        if (player.State == PlayerState.Airborne) return;

        if(rb.velocity.magnitude > .1f && player.State == PlayerState.Sprinting)
        {
            timer += Time.deltaTime * speed;
            camHolder.localPosition = new Vector3(
                camHolder.localPosition.x,
                originY + Mathf.Sin(timer) * amount,
                camHolder.localPosition.z);
        }

    }
}
