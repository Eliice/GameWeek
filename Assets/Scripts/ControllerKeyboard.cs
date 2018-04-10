using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerKeyboard : Controller
{
    private static ControllerKeyboard instance;

    /// instance unique de la classe    
    public static ControllerKeyboard Instance
    {
        get
        {
            return instance;
        }
    }

    protected void Awake()
    {
        if (instance != null)
        {
            throw new Exception("Tentative de création d'une autre instance de ControllerKeyboard alors que c'est un singleton.");
        }
        instance = this;

        m_player = GetComponent<Player>();
    }

    override protected void Move()
    {
        if (Input.GetKey(KeyCode.Q))
            m_player.MoveHorizontal(false);

        else if (Input.GetKey(KeyCode.D))
            m_player.MoveHorizontal(true);

        else
            m_player.Stand();
    }

    override protected void checkJump()
    {
        if (m_player.CanJump && Input.GetKey(KeyCode.Space))
        {
            m_player.AddForce();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_player.Jump();
        }
    }

    override protected void checkDash()
    {
        if (m_player.CanDash && Input.GetKey(KeyCode.LeftShift))
        {
            m_player.AddForce();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_player.Dash();
        }
    }

    protected void OnDestroy()
    {
        instance = null;
    }
}
