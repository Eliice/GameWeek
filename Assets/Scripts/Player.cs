using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Timer
{
    float m_targetTime = 2;
    float m_currentTime = 0;
    
    public void InitTimer(float targetTime)
    {
        m_targetTime = targetTime;
    }

    public  void updateTimer()
    {
        m_currentTime += Time.deltaTime;
    }

    public bool TimerComplete()
    {
        return m_currentTime >= m_targetTime;
    }

    public void ResetTimer()
    {
        m_currentTime = 0;
    }
}

public class Player : MonoBehaviour
{
    public enum E_Direction
    {
        LEFT,
        RIGHT
    };


    [SerializeField]
    private List<string> m_frameName;
    [SerializeField]
    private List<AudioClip> m_audioData;

    [SerializeField]
    private float m_speed = 10f;
    public float Speed { get { return m_speed; } }


    [SerializeField]
    private float m_forceIncrement = 1.5f;
    [SerializeField]
    private float m_forceLimit = 15f;
    [SerializeField]
    private float m_minForce = 1.5f;
    [SerializeField]
    private float spentStaminaFactor = 7;

    private Slider m_strenghtBar = null;
    private Slider m_Stamina_Bar = null;

    [SerializeField]
    float dashFactor = 1f;

    private Animator m_animator = null;
    public Animator CharacterAnimator { get { return m_animator; } }

    private SpriteRenderer m_renderer = null;
    private AudioSource m_audioPlayer = null;

    private Rigidbody m_rigidBody = null;
    Ray rayBottomForward;
    Ray rayTopForward;

    private bool canGoForward; 

    private bool noStamina = false;

    private Vector3 m_direction = new Vector3();
    Vector3 dashDirection = new Vector3();
    private float m_force ;
    [SerializeField]
    private float dashTime = 1f;

    private Vector3 strenghtBarOffset;

    private Timer dashTimer = new Timer();
    private E_Direction m_previousDirection = E_Direction.RIGHT;
    public E_Direction Direction { get { return m_previousDirection; } }

    private static Player instance;



    private RigidbodyConstraints m_dashConstraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
    private RigidbodyConstraints m_standardConstraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;

    private bool canMove = true;


    public static Player Instance
    {
        get
        {
            if (instance != null)
                return instance;
            else
            {
                return null;
            }
        }
    }

    private void Awake()
    {
       if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        m_animator = gameObject.GetComponent<Animator>();
        m_rigidBody = gameObject.GetComponent<Rigidbody>();
        m_strenghtBar = GameObject.FindGameObjectWithTag("ForceBar").GetComponent<Slider>();
        if (m_strenghtBar != null)
            m_strenghtBar.gameObject.SetActive(false);
        strenghtBarOffset = new Vector3(0,transform.localScale.y * 3, 0);
        m_Stamina_Bar = GameObject.FindGameObjectWithTag("StaminaBar").GetComponent<Slider>();
        if(m_Stamina_Bar != null)
            m_Stamina_Bar.value = m_Stamina_Bar.maxValue;

        dashTimer.InitTimer(dashTime);
        m_force = m_minForce;
        m_renderer = GetComponent<SpriteRenderer>();
        m_audioPlayer = GetComponent<AudioSource>();
    }

    private void Start()
    {
        m_Stamina_Bar.transform.Rotate(0, -90, 0);
    }

    private void Update()
    {
        dashTimer.updateTimer();
        if (dashTimer.TimerComplete())
        {
            m_animator.SetBool("Dash", false);
            m_rigidBody.constraints = m_standardConstraints;
        }

        m_strenghtBar.transform.position = transform.position + strenghtBarOffset;
        
        //-------- ATTENTION c'est du sale
        rayBottomForward = new Ray(new Vector3(transform.position.x, transform.position.y + transform.localScale.y * 1.5f, 0), transform.right);
        rayTopForward = new Ray(new Vector3(transform.position.x, transform.position.y + transform.localScale.y * 28, 0), transform.right);

        Debug.DrawLine(rayBottomForward.origin, rayBottomForward.GetPoint(0.70f), Color.blue);
        Debug.DrawLine(rayTopForward.origin, rayTopForward.GetPoint(0.70f), Color.green);

        RaycastHit hitInfoBottomForward;
        RaycastHit hitInfoTopForward;

        if (Physics.Raycast(rayBottomForward, out hitInfoBottomForward, 0.70f))
        {
            if (hitInfoBottomForward.transform.tag == "Wall")
            {
                canGoForward = false;
                m_direction.x = 0;
            }
        }
        else if (Physics.Raycast(rayTopForward, out hitInfoTopForward, 0.70f))
        {
            if (hitInfoTopForward.transform.tag == "Wall")
            {
                canGoForward = false;
                m_direction.x = 0;
            }
        }
        else
            canGoForward = true;

        //Debug.Log("Forward : " + canGoForward);
    }

    public void AddForce()
    {
        if (m_strenghtBar != null && !m_strenghtBar.gameObject.activeSelf)
            m_strenghtBar.gameObject.SetActive(true);

        if (m_force < m_forceLimit)
        {
            m_strenghtBar.value += (m_forceIncrement / m_forceLimit * 100) * Time.fixedDeltaTime;
            m_force += m_forceIncrement * Time.fixedDeltaTime;
        }
    }

    public void ResetForce()
    {
        if (m_strenghtBar != null && m_strenghtBar.gameObject.activeSelf)
            m_strenghtBar.gameObject.SetActive(false);

        m_force = m_minForce;
        if (m_strenghtBar != null)
            m_strenghtBar.value = 0;
    }

    public void MoveHorizontal(bool isGoingRight)
    {
        if (m_animator.GetBool("Dash") && !canMove)
            return;
        m_animator.SetBool("Move", true);
        if (!isGoingRight)
        {
            if (m_previousDirection != E_Direction.LEFT)
            {
                m_previousDirection = E_Direction.LEFT;
                transform.Rotate(0, 180, 0);
            }
            if (canGoForward)
                m_direction.x = m_speed;
        }
        else if (isGoingRight)
        {
            if (m_previousDirection != E_Direction.RIGHT)
            {
                m_previousDirection = E_Direction.RIGHT;
                transform.Rotate(0, 180, 0);
            }
            if (canGoForward)
                m_direction.x = m_speed;
        }
        transform.Translate(m_direction * Time.deltaTime);
    }

    public void Stand()
    {
        m_animator.SetBool("Move", false);
        m_direction.x = 0;
    }

    public void Jump()
    {
        m_animator.SetBool("Jump", true);
        m_rigidBody.AddForce(new Vector3(0, m_force, 0), ForceMode.Impulse);
        SpendStamina(m_force);
        ResetForce();
    }

    void SpendStamina(float forceSpent)
    {
        if (m_Stamina_Bar != null)
        {

            m_Stamina_Bar.value -= forceSpent / spentStaminaFactor * m_Stamina_Bar.maxValue / m_forceLimit;

            if (m_Stamina_Bar.value == m_Stamina_Bar.minValue)
                noStamina = true;
        }
    }

    void CheckStamina ()
    {
        if (noStamina)
            Debug.Log("Boom t'es MORT !");
    }

    public void ResetStamina ()
    {
        if (m_Stamina_Bar != null)
        {
            m_Stamina_Bar.value = 1;
            m_Stamina_Bar.value = m_Stamina_Bar.maxValue;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Wall" && m_animator.GetBool("Jump"))
        {
            CheckStamina();
            m_animator.SetBool("Jump", false);
        }

        if (collision.gameObject.tag == "Wall")
        {
            m_direction.x = 0;
        }
        if (m_animator.GetBool("Dash"))
        {
            m_animator.SetBool("Dash", false);
            m_rigidBody.constraints = m_standardConstraints;
            canMove = true;
        }
    }

    public void Dash()
    {
        if (m_animator.GetBool("Jump") || m_animator.GetCurrentAnimatorStateInfo(0).IsName("On Air"))
            canMove = false;
        m_animator.SetBool("Dash", true);
        m_rigidBody.constraints = m_dashConstraints;
        dashTimer.ResetTimer();
        float sign = m_previousDirection == E_Direction.LEFT ? -1 : 1; 
        dashDirection.x = sign * m_force * dashFactor;
        m_rigidBody.AddForce(dashDirection, ForceMode.Impulse);
        SpendStamina(m_force);
        ResetForce();
    }

    public void ResetMusic()
    {
        //AudioSource.
    }

    private void SoundCheck()
    {
        string name = m_renderer.sprite.name;
        if (m_frameName.Contains(name))
        {
            for (int i = 0; i < m_frameName.Count; i++)
            {
                if (m_frameName[i] == name)
                {
                    m_audioPlayer.PlayOneShot(m_audioData[i]);
                }
            }

        }
    }

    public void RespawnTransition()
    {
        Transitions transition = GameObject.Find("Transitions").GetComponent<Transitions>();
        transition.StartTransition();
    }
}