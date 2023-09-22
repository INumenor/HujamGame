using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private float Move;
    public Vector2Int mousePos;
    [SerializeField] float speed;
    [SerializeField] float jump;
    [SerializeField] bool isFacingRight;
    [SerializeField] Animator Anim;
    private bool isHitting = false;
    public TerrianGen terrianGene;
    public EnergyBarMan Energy;
    [SerializeField] GameObject Inventory;
    void Start()
    {
        TerrianGen terrianGene = new TerrianGen();
        isFacingRight = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(Move * speed, rb.velocity.y);   
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }
        if (Move >= 0.1f || Move <= -0.1f)
        {
            Anim.SetBool("isWalking", true);
        }
        else
        {
            Anim.SetBool("isWalking", false);
        }
        if (!isFacingRight && Move > 0f)
        {
            Flip();
        }
        else if (isFacingRight && Move < 0f)
        {
            Flip();
        }
        if (Input.GetButtonDown("Fire1") && !isHitting && Energy._EnergyBarAmount > 0f)
        {
            Anim.SetBool("isHitting", true);
            isHitting = true;
            terrianGene.RemoveTile(mousePos.x, mousePos.y);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            Anim.SetBool("isHitting", false);
            isHitting = false;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(Inventory.active == true)
            {
                Inventory.active = false;
            }
            else
            {
                Inventory.active = true;
            }
        }
        mousePos.x = Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
        mousePos.y = Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Anim.SetBool("isJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Anim.SetBool("isJumping", true);
        }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
