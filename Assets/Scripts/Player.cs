using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Player : MonoBehaviour
{


    [SerializeField]
    private float m_speed = 10f;
    [SerializeField]
    private float m_forceIncrement = 2;
    [SerializeField]
    private float m_forceLimit = 100;
    [SerializeField]
    private Slider m_strenghtBar = null;
    [SerializeField]
    float gravity = 9.81f;
    [SerializeField]
    float dashFactor = 1f;



    private Animator m_animator = null;
    public Animator CharacterAnimator { get { return m_animator; } }
    public bool CanJump
    {
        get { return canJump; }
        set { canJump = value; }
    }

    public bool CanDash
    {
        get { return canDash; }
        set { canDash = value; }
    }

    bool canJump = true;
    bool canDash = true;

    private Vector3 m_direction = new Vector3();
    private float m_force = 0;


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
    }

    public void AddForce()
    {

        m_force += m_forceIncrement * Time.deltaTime;
        if (m_force >= m_forceLimit)
            m_force = m_forceLimit;
    }

    public void ResetForce()
    {
        m_force = 0f;
        if (m_strenghtBar != null)
            m_strenghtBar.value = 0;
    }

    public void MoveHorizontal(bool isGoingRight)
    {
        if (canDash)
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
        m_direction.x = 0;
    }

    public void Jump()
    {

        StartCoroutine(JumpCoroutine());
    }

    IEnumerator JumpCoroutine()
    {
        canJump = false;
        float force = m_force;
        ResetForce();
        Vector3 initalPos = transform.position;
        Vector3 pos;
        do 
        {
            pos = gameObject.transform.position;
            pos.y += force * Time.deltaTime;
            transform.position = pos;
            force -= gravity * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        } while (initalPos.y < transform.position.y);

        if (transform.position.y < initalPos.y)
        {
            pos.y = initalPos.y;
            transform.position = pos;
        }
        canJump = true;
    }

    public void Dash()
    {
        Vector3 dashDirection = new Vector3(m_force * dashFactor, 0, 0);
        ResetForce();
        StartCoroutine(DashCoroutine(0.35f, new Vector3(transform.position.x, transform.position.y, 0), new Vector3(transform.position.x + dashDirection.x, transform.position.y, 0)));
    }

    IEnumerator DashCoroutine(float time, Vector3 begin, Vector3 end)
    {
        canDash = false;
        float currentTime = 0;
        float normalizedValue;

        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / time;
            transform.position = Vector3.Lerp(begin, end, normalizedValue);
            yield return new WaitForEndOfFrame();
        }
        canDash = true;
    }
}