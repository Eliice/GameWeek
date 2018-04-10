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
}
