using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    private enum E_Direction
    {
        LEFT,
        RIGHT
    };



    [SerializeField]
    private float m_speed = 10f;
    [SerializeField]
    private float m_forceIncrement = 1.5f;
    [SerializeField]
    private float m_forceLimit = 15f;
    [SerializeField]
    private float m_minForce = 1.5f;

    private Slider m_strenghtBar = null;
    private Slider m_Stamina_Bar = null;

    [SerializeField]
    float dashFactor = 1f;

    private Animator m_animator = null;
    public Animator CharacterAnimator { get { return m_animator; } }

    private Rigidbody m_rigidBody = null;
    //private bool canGoLeft = true;
    //private bool canGoRight = true;

    private Vector3 m_direction = new Vector3();
    Vector3 dashDirection = new Vector3();
    private float m_force ;
    [SerializeField]
    private float dashTime = 1f;


    private Timer dashTimer = new Timer();

    private E_Direction m_previousDirection = E_Direction.RIGHT;


    private static Player instance;
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


        dashTimer.InitTimer(dashTime);
        m_force = m_minForce;
    }

    private void Update()
    {
        dashTimer.updateTimer();
        if (dashTimer.TimerComplete())
        {
            m_animator.SetBool("Dash", false);
        }
    }

    public void AddForce()
    {
        if (m_force < m_forceLimit)
        {
            m_strenghtBar.value += (m_forceIncrement / m_forceLimit * 100) * Time.fixedDeltaTime;
            m_force += m_forceIncrement * Time.fixedDeltaTime;
        }
    }

    public void ResetForce()
    {
        m_force = m_minForce;
        if (m_strenghtBar != null)
            m_strenghtBar.value = 0;
    }

    public void MoveHorizontal(bool isGoingRight)
    {
        m_animator.SetBool("Move", true);
        if (!isGoingRight)
        {
            if (m_previousDirection != E_Direction.LEFT)
            {
                m_previousDirection = E_Direction.LEFT;
                transform.Rotate(0, 180, 0);
            }
            m_direction.x = m_speed;
        }
        else
        {
                if (m_previousDirection != E_Direction.RIGHT)
                {
                    m_previousDirection = E_Direction.RIGHT;
                    transform.Rotate(0, 180, 0);
                }
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
        ResetForce();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor" && m_animator.GetBool("Jump"))
        {
            m_animator.SetBool("Jump", false);
        }

        if (collision.gameObject.tag == "Wall")
        {
            m_direction.x = 0;
        }
        if (m_animator.GetBool("Dash"))
            m_animator.SetBool("Dash", false);
    }

    public void Dash()
    {
        m_animator.SetBool("Dash", true);
        dashTimer.ResetTimer();
        float sign = m_direction.x < 0 ? -1 : 1; // if the direction.x = 0, will go right
        dashDirection.x = sign * m_force * dashFactor;
        m_rigidBody.AddForce(dashDirection, ForceMode.Impulse);
        ResetForce();
    }

}