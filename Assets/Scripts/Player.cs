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
    [SerializeField]
    private Slider m_strenghtBar = null;
    [SerializeField]
    float gravity = 9.81f;
    [SerializeField]
    float dashFactor = 1f;

    private Animator m_animator = null;
    public Animator CharacterAnimator { get { return m_animator; } }

    private Rigidbody m_rigidBody = null;

    private Vector3 m_direction = new Vector3();
    Vector3 dashDirection = new Vector3();
    private float m_force = 0;
    private float dashTime = 0.35f;

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
                m_direction.x = -m_speed;
            else
                m_direction.x = m_speed;

            transform.Translate(m_direction * Time.deltaTime);
        }
    }

    public void Stand()
    {
        m_animator.SetBool("Move", false);
        m_direction.x = 0;
    }

    public void Jump()
    {
        CameraShake.Shake(0.20f, 0.15f);
        m_animator.SetBool("Jump", true);
        m_rigidBody.AddForce(new Vector3(0, m_force, 0), ForceMode.Impulse);
        ResetForce();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            m_animator.SetBool("Jump", false);
        }
    }


    public void Dash()
    {
        float sign = m_direction.x < 0 ? -1 : 1; // if the direction.x = 0, will go right
        dashDirection.x = sign * m_force * dashFactor;
        ResetForce();
        StartCoroutine(DashCoroutine(dashTime, transform.position + dashDirection));
    }

    IEnumerator DashCoroutine(float time, Vector3 dir)
    {
        m_animator.SetBool("Dash", true);
        float currentTime = 0;
        float normalizedValue;

        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / time;
            transform.position = Vector3.Lerp(transform.position, dir, normalizedValue);
            yield return new WaitForEndOfFrame();
        }
        m_animator.SetBool("Dash", false);
    }
}