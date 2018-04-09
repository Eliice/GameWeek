using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [SerializeField]
    private float m_speed = 10f;
    [SerializeField]
    private int m_forceIncrement = 2;
    [SerializeField]
    private int m_forceLimit = 100;

    #region jump
    [SerializeField]
    float gravity = 9.81f;
    bool canJump = true;
    #endregion

    public float Speed { set { m_speed = value; } }
    private Vector3 m_direction = new Vector3();
    private bool m_canDash = true;
    private float m_force = 0;

    void Update ()
    {
        Move();
        checkDash();
        checkJump();
	}

    void Move ()
    {
        if (Input.GetKey(KeyCode.Q))
            m_direction.x = -m_speed;

        else if (Input.GetKey(KeyCode.D))
            m_direction.x = m_speed;

        else
            m_direction.x = 0;          

        transform.Translate(m_direction * Time.deltaTime);
    }

    void checkJump ()
    {
        if (canJump && Input.GetKey(KeyCode.Z) && m_force <= m_forceLimit)
        {
            m_force += m_forceIncrement * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            StartCoroutine(Jump());
        }
    }

    void checkDash ()
    {
        if (m_canDash && Input.GetKey(KeyCode.S) && m_force <= m_forceLimit)
        {
            m_force += m_forceIncrement * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            Debug.Log("Dash Power : " + m_force);
            m_force = 0;
        }
    }

    IEnumerator Jump()
    {
        float force = m_force;
        m_force = 0;
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

}
