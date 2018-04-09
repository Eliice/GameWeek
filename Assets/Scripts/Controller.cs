using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    protected Player m_player;

    protected void Update()
    {
        Move();
        checkDash();
        checkJump();
    }

    virtual protected void Move()
    {

    }

    virtual protected void checkJump()
    {

    }

    virtual protected void checkDash()
    {

    }

    protected IEnumerator Jump()
    {
        float force = m_player.Force;
        m_player.Force = 0;
        Vector3 initalPos = transform.position;
        Vector3 pos;
        do
        {
            pos = gameObject.transform.position;
            pos.y += force * Time.deltaTime;
            transform.position = pos;
            force -= m_player.Gravity * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        } while (initalPos.y < transform.position.y);

        if (transform.position.y < initalPos.y)
        {
            pos.y = initalPos.y;
            transform.position = pos;
        }
        m_player.CanJump = true;
    }
}
