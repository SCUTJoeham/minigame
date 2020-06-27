using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    bool m_isGrounded;
    bool m_isWalled;

    public LayerMask m_groundLayer;
    public float m_groundCheckDistance = 1.2f;

    public Transform m_headCheck;
    public Transform m_footCheck;
    public float m_wallCheckDistance = 1.2f;

    Animator m_anim;
    Rigidbody2D m_body;

    bool m_FacingRight = true;

    public float m_Speed = 1000f;
    public float m_jumpForce = 9.0f;

    public float m_CanJumpTime = 0.2f;
    float m_JumpTimer;
    bool m_isJumping;

    Vector2 m_vec;
    float m_input_h;

    // 二段跳
    int m_jumpTimes;

    public Rigidbody2D pfb_bullet;
    public Rigidbody2D pfb_bullet2;
    public Vector2 bulletSpeed = new Vector2(15, 0);

    bool change = false;
    float ShootInterval = 1.5f;  //控制射速
    float ShootTime = 0;

    public int fuel = 0;
    public Text fuelNum;

    public AudioSource jumpAudio;
    public AudioSource fireAudio;
    public AudioSource upgradeAudio;
    public AudioSource changeAudio;

    void Awake()
    {
        m_anim = GetComponent<Animator>();
        m_body = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_JumpTimer = 0f;
        m_isJumping = false;
        m_vec = new Vector2(0, m_jumpForce);
        m_jumpTimes = 0;
    }


    void Update()
    {
        m_isGrounded = IsGrounded();


        if (m_anim.GetBool("Ground") != m_isGrounded)
        {
            m_anim.SetBool("Ground", m_isGrounded);
        }

        #region 跳跃代码
        // 跳跃
        if (m_isJumping && Input.GetButton("Jump"))
        {
            if (m_JumpTimer <= m_CanJumpTime)
            {
                m_vec.x = m_body.velocity.x;
                m_body.velocity = m_vec;
                m_JumpTimer += Time.deltaTime;
            }
            else
            {
                m_isJumping = false;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (m_isGrounded)
            {
                m_jumpTimes = 1;

                m_isJumping = true;
                m_JumpTimer = 0f;
                m_isGrounded = false;
                m_vec.x = m_body.velocity.x;
                m_body.velocity = m_vec;
                jumpAudio.Play();
            }
            else if (m_jumpTimes == 1)
            {
                m_jumpTimes = 2;

                m_isJumping = true;
                m_JumpTimer = 0f;
                m_isGrounded = false;
                m_vec.x = m_body.velocity.x;
                m_body.velocity = m_vec;
                jumpAudio.Play();
            }

        }

        if (Input.GetButtonUp("Jump"))
        {
            m_isJumping = false;
        }

        m_anim.SetFloat("vSpeed", m_body.velocity.y);
        #endregion


        m_input_h = Input.GetAxisRaw("Horizontal");
        Move(m_input_h);

        UpgardeShoot();
        ChangeBullet();
        CheckShoot();
    }

    void UpdateUI()
    {
        fuelNum.text = fuel.ToString();
    }

    void GetFuel()
    {
        fuel = 1;
        UpdateUI();
    }

    void UseFuel()
    {
        //Debug.Log("use");
        fuel -= 1;
        UpdateUI();
    }

    void ChangeBullet()
    {
        if (Input.GetButtonDown("Change"))
        {
            change = change ^ true;
            changeAudio.Play();
        }
    }

    void CheckShoot()
    {
        ShootTime += Time.deltaTime;
        if (Input.GetButtonDown("Shoot") && ShootTime >= ShootInterval)
        {
            ShootTime = 0;
            Rigidbody2D obj;
            if (change)
                obj = (Rigidbody2D)Instantiate(pfb_bullet2, transform.position, Quaternion.identity);
            else
                obj = (Rigidbody2D)Instantiate(pfb_bullet, transform.position, Quaternion.identity);
            fireAudio.Play();
            obj.velocity = m_FacingRight ? bulletSpeed : -1 * bulletSpeed;
        }
    }

    //升级射速
    void UpgardeShoot()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (ShootInterval == 1.5f)
            {
                if (Health.DNA >= 10)
                {
                    Health.DNA -= 10;
                    ShootInterval -= 0.5f;
                    upgradeAudio.Play();
                }
            }
            else if (ShootInterval == 1.0f)
            {
                if (Health.DNA >= 20)
                {
                    Health.DNA -= 20;
                    ShootInterval -= 0.5f;
                    upgradeAudio.Play();
                }
            }
        }
    }

    void Move(float h)
    {
        m_isWalled = IsWalled(m_FacingRight ? 1 : -1);

        if (m_FacingRight)
        {
            if (h < 0)
            {
                Flip();
            }
            else if (m_isWalled)
            {
                m_anim.SetBool("run", false);
                return;
            }
        }
        else
        {
            if (h > 0)
            {
                Flip();
            }
            else if (m_isWalled)
            {
                m_anim.SetBool("run", false);
                return;
            }
        }

        Vector2 v = m_body.velocity;
        v.x = h * m_Speed * Time.deltaTime;
        m_body.velocity = v;


        m_anim.SetBool("run", !(h == 0));
    }

    void Flip()
    {
        m_FacingRight = !m_FacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    bool IsGrounded()
    {
        Debug.DrawRay(transform.position, Vector2.down, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, m_groundCheckDistance, m_groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    bool IsWalled(float dir)
    {
        RaycastHit2D hit1 = Physics2D.Raycast(m_headCheck.position, dir * Vector2.right, m_wallCheckDistance, m_groundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(m_footCheck.position, dir * Vector2.right, m_wallCheckDistance, m_groundLayer);
        if ((hit1.collider == null) && (hit2.collider == null))
        {
            return false;
        }
        return true;
    }

}
