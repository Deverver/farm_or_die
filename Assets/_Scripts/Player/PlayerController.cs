using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField] private float Speed { get; set; } = 2f;

    private Rigidbody2D rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = new Vector2();

        if (Input.GetKey(KeyCode.W))
        {
            move.y += Speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move.y -= Speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move.x += Speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move.x -= Speed;
        }


        rb.MovePosition(rb.position + move * Time.deltaTime);
    }
}
