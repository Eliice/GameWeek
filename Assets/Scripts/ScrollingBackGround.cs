using UnityEngine;

public class ScrollingBackGround : MonoBehaviour {



    [SerializeField]
    private GameObject m_bgFirstLayer1 = null;
    [SerializeField]
    private GameObject m_bgFirstLayer2 = null;
    [SerializeField]
    private GameObject m_bgFirstLayer3 = null;



    [SerializeField]
    private Transform m_frontAnchor = null;
    [SerializeField]
    private Transform m_backAnchor = null;

    private Player m_player;


    private void start()
    {
        m_player = Player.Instance;
    }



    void Update ()
    {
        if(!m_player)
            m_player = Player.Instance;
        if (m_player.CharacterAnimator.GetBool("Move") || m_player.CharacterAnimator.GetBool("Dash"))
        {
            switch (m_player.Direction)
            {
                case Player.E_Direction.LEFT:
                    m_bgFirstLayer1.transform.Translate(m_player.Speed * Time.deltaTime, 0, 0);
                    m_bgFirstLayer2.transform.Translate(m_player.Speed * Time.deltaTime, 0, 0);
                    m_bgFirstLayer3.transform.Translate(m_player.Speed * Time.deltaTime, 0, 0);
                    break;
                case Player.E_Direction.RIGHT:
                    m_bgFirstLayer1.transform.Translate(m_player.Speed * -1 * Time.deltaTime, 0, 0);
                    m_bgFirstLayer2.transform.Translate(m_player.Speed * -1 * Time.deltaTime, 0, 0);
                    m_bgFirstLayer3.transform.Translate(m_player.Speed * -1 * Time.deltaTime, 0, 0);
                    break;
            }
        }
        if (checkPos())
            Debug.Log("replace needed !");
	}



    private bool checkPos()
    {
        switch (m_player.Direction)
        {
            case Player.E_Direction.LEFT:
                if (m_frontAnchor.position.x <= m_bgFirstLayer2.transform.position.x)
                    return true;
                else
                    return false;
            case Player.E_Direction.RIGHT:
                if (m_backAnchor.position.x >= m_bgFirstLayer2.transform.position.x)
                    return true;
                else
                    return false;
            default:
                return false;
        }
    }

}
