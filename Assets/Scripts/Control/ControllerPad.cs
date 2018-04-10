using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPad : Controller {

    private static ControllerPad instance;

    #region inputs
    private const string horizontalAxis = "Horizontal";
    private const string A_Button = "A Button";
    private const string B_Button = "B Button";
    private const string X_Button = "X Button";
    private const string Y_Button = "Y Button";
    #endregion 

    /// instance unique de la classe    
    public static ControllerPad Instance
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
            Destroy(gameObject);
        }
        else 
            instance = this;

        m_player = GetComponent<Player>();
    }

    override protected void Move()
    {
        if (Input.GetAxis(horizontalAxis) < -0.25f)
            m_player.MoveHorizontal(false);

        else if (Input.GetAxis(horizontalAxis) > 0.25f)
            m_player.MoveHorizontal(true);

        else
            m_player.Stand();
    }

    override protected void checkJump()
    {
        if (!m_player.CharacterAnimator.GetBool("Jump") && (Input.GetButton(A_Button) || Input.GetButton(B_Button)))
        {
            m_player.AddForce();
        }

        if (Input.GetButtonUp(A_Button) || Input.GetButtonUp(B_Button))
        {
            m_player.Jump();
        }
    }

    override protected void checkDash()
    {
        if (!m_player.CharacterAnimator.GetBool("Dash") && !m_player.CharacterAnimator.GetBool("Jump") && (Input.GetButton(X_Button) || Input.GetButton(Y_Button)))
        {
            m_player.AddForce();
        }

        if (Input.GetButtonUp(X_Button) || Input.GetButtonUp(Y_Button))
        {
            m_player.Dash();
        }
    }

    protected void OnDestroy()
    {
        instance = null;
    }
}
