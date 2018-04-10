using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float m_speed = 10f;
    [SerializeField]
    private float m_forceIncrement = 2f;
    [SerializeField]
    private float m_forceLimit = 10f;

    private Slider m_strenghtBar = null;

    [SerializeField]
    float dashFactor = 1f;

    private Animator m_animator = null;
    public Animator CharacterAnimator { get { return m_animator; } }

    private Rigidbody m_rigidBody = null;
    private Ray rayLeft; 
    private Ray rayRight;
    private bool canGoLeft = true;

    private Vector3 m_direction = new Vector3();
    Vector3 dashDirection = new Vector3();
    private float m_force = 0;
    [SerializeField]
    private float dashTime = 5f;

    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance != null)
                return instance;
            else
            {
                Debug.LogAssertion("try to get null player");
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
        //colliders = gameObject.GetComponentsInChildren<BoxCollider>();
    }

    private void Update()
    {
        rayLeft = new Ray(new Vector3(transform.position.x - transform.localScale.x / 2, transform.position.y + transform.localScale.y / 2, 0), -transform.up);
        Debug.DrawLine(rayLeft.origin, rayLeft.GetPoint(transform.localScale.y), Color.red);

        RaycastHit hitInfoLeft;

        if (Physics.Raycast(rayLeft, out hitInfoLeft, transform.localScale.y))
        {
            Debug.Log(hitInfoLeft.transform.name);
            if (hitInfoLeft.transform.tag == "Wall")
            {
                canGoLeft = false;
                Debug.Log("AAAAAAAAAAAAAAAAAAAA");
                m_direction.x = 0;
            }
            else
                canGoLeft = true;
        }
    }

    public void AddForce()
    {
        Debug.Log(m_force);
        if (m_force < m_forceLimit)
        {
            m_strenghtBar.value += (m_forceIncrement / m_forceLimit * 100) * Time.deltaTime;
            m_force += m_forceIncrement * Time.deltaTime;
        }
    }

    public void ResetForce()
    {
        m_force = 0f;
        if (m_strenghtBar != null)
            m_strenghtBar.value = 0;
    }

    public void MoveHorizontal(bool isGoingRight)
    {
        m_animator.SetBool("Move", true);
        if (!m_animator.GetBool("Dash"))
        {
            if (!isGoingRight)
            {
                if (canGoLeft)
                    m_direction.x = -m_speed;
                else
                    m_direction.x = 0;
            }
            else
                m_direction.x = m_speed;

            transform.Translate(m_direction * Time.deltaTime);
        }
        /*if(m_rigidBody.velocity.magnitude < m_speed)
            m_rigidBody.AddRelativeForce(m_direction);*/
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
            m_animator.SetBool("Dash",false);
        /*foreach (BoxCollider col in colliders)
        {
            if (col.name == "LeftCol" || col.name == "RightCol")
            {
                if (collision.gameObject)
            }
        }*/
    }

    public void Dash()
    {
        m_animator.SetBool("Dash", true);
        float sign = m_direction.x < 0 ? -1 : 1; // if the direction.x = 0, will go right
        dashDirection.x = sign * m_force * dashFactor;
        m_rigidBody.AddForce(dashDirection, ForceMode.Impulse);
        ResetForce();
        StartCoroutine(DashCoroutine(dashTime));
    }

    IEnumerator DashCoroutine(float time)
    {
        float currentTime = 0;

        while (currentTime < time)
        {
            yield return new WaitForEndOfFrame();
            currentTime += Time.deltaTime;
        }
        m_animator.SetBool("Dash", false);
    }

}