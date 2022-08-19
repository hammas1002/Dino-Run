using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Movement_Player : MonoBehaviour
{
    [SerializeField]
  
    float jumpPower = 300f;
    public LayerMask groundLayerMask;
    float boxCastDistance = 0.03f;
    Collider2D col;
    Animator anim;
    Rigidbody2D rb;
    [SerializeField] private Transform m_GroundCheck;
    const float k_GroundedRadius = .2f;
    private bool m_Grounded;
    private float playerPositionX= -7.83f;

    public GameObject jumpSmoke, landSmoke;

    public GameObject GamePaused;
    public GameObject GameOver;



    private AudioSource audioSource;

    public UnityEvent OnLandEvent;
    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponentInChildren<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (GamePaused.activeSelf||GameOver.activeSelf) return;
        //for pc
        Slide();
        Jump();
        AirDodge();
        //for android
        transform.position = new Vector2(playerPositionX,transform.position.y);

    }

    void AirDodge()
    {
        if ((Input.GetKeyDown(KeyCode.DownArrow)|| Input.GetKeyDown(KeyCode.S) || Input.GetMouseButtonDown(0)) && !IsGrounded())
        {
            if (Input.touchCount>0)
            {
                Touch touch = Input.GetTouch(0);
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                { return; }
            }
            
                Debug.Log("AirDodge");
            rb.AddForce(-transform.up * jumpPower*2);

        }
    }

    void Slide()
    {
        if (IsGrounded() && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)))
        {
            PlayerSlide();
            
        }
    }
    public void PlayerSlide()
    {
        if (IsGrounded()) { 
        anim.SetTrigger("Slide");
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = true;
        StartCoroutine(nameof(SlideEnd));
        }
    }
    IEnumerator SlideEnd()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;
        
    }

    void Jump()
    {
        
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && IsGrounded())
        {
            if (Input.touchCount>0)
            {
                Touch touch = Input.GetTouch(0);
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                { return; }
            }

            if (EventSystem.current.IsPointerOverGameObject()) return;
            anim.SetBool("Jump", true);
            rb.AddForce(transform.up * jumpPower);
            audioSource.Play();
            jumpSmoke.SetActive(true);
            Invoke(nameof(DisableSmokeEffects), 0.7f);
        }


    }

    void DisableSmokeEffects()
    {
        jumpSmoke.SetActive(false);
        landSmoke.SetActive(false);
    }
    bool IsGrounded()
    {
        if (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Cat_Slide"))
        {
            return true;
        }
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0, Vector2.down, boxCastDistance, groundLayerMask);

    }
    public void OnLand()
    {
        anim.SetBool("Jump", false);
        landSmoke.SetActive(true);
        Invoke(nameof(DisableSmokeEffects), 0.7f);
    }

    private void Awake()
    {
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, groundLayerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();

            }
        }
    }
}
