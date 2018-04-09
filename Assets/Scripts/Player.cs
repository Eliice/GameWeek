using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    //[SerializeField]
    private Controller controller; 
    [SerializeField]
    private float m_speed = 10f;
    [SerializeField]
    private float m_forceIncrement = 2;
    [SerializeField]
    private float m_forceLimit = 100;

    public float Gravity { get { return gravity; } }
    public float ForceLimit { get { return m_force ; } }
    public float ForceIncrement { get { return m_forceIncrement; } }
    public float Speed
    {
        get { return m_speed; }
        set { m_speed = value; }
    }
    public float Force
    {
        get { return m_force; }
        set { m_force = value; }
    }
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

    #region moveActions
    [SerializeField]
    float gravity = 9.81f;
    bool canJump = true;
    bool canDash = true;
    #endregion

    private static Player instance;

    /// instance unique de la classe    
    public static Player Instance
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
            throw new Exception("Tentative de création d'une autre instance de Player alors que c'est un singleton.");
        }
        instance = this;

        controller = GetComponent<ControllerKeyboard>();
    }

    private Vector3 m_direction = new Vector3();
    private float m_force = 0;

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

    protected void OnDestroy()
    {
        instance = null;
    }
}
