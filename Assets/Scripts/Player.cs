﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    private Controller m_controller;
    public Controller Controller { set { m_controller = value; } }

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
        instance = this;
    }

    public void AddForce()
    {
        m_force += m_forceIncrement * Time.deltaTime;
        if (m_force >= m_forceLimit)
            m_force = m_forceLimit;
            m_strenghtBar.value = (m_force / m_forceLimit) * 100;
    }

    public void ResetForce()
    {
        m_force = 0f;
        m_strenghtBar.value = 0;
    }
    
    public void MoveHorizontal (bool isGoingRight)
    {
        if (!isGoingRight)
            m_direction.x = -m_speed;
        else
            m_direction.x = m_speed;

        transform.Translate(m_direction * Time.deltaTime);
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
        Debug.Log("Dash power : " + m_force);
        Vector3 dashDirection = new Vector3(m_force, 0, 0);
        ResetForce();

        transform.Translate(dashDirection * Time.deltaTime);
    }

}