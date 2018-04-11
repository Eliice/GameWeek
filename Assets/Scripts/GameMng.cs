
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameMng : MonoBehaviour {

    private static GameMng m_instance;
    public static GameMng Instance { get { return m_instance; } }

    private Player m_player = null;
    private GameObject m_playerObject;
    
    [SerializeField]
    private GameObject m_playerPrefab = null;

    private Vector3 m_respawn = new Vector3();

    private GameObject spawnParent = null;

    private bool m_keyboardInput = true;


    private GameObject m_camera = null;
    public Vector3 Respawn
    {
        set
        {
            if (value.x > m_respawn.x)
                m_respawn = value;
        }
    }


    private void Awake()
    {
        if (m_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            m_instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }


    public void SwitchInput()
    {
        Debug.Log("switch controler done");
        if (m_keyboardInput)
            SwithcToPad();
        else
            SwitchToKeyboard();
    }

    private void SwitchToKeyboard()
    {
        m_keyboardInput = true;
        if(m_playerObject)
        {
            m_playerObject.AddComponent<ControllerKeyboard>();
            Destroy(m_playerObject.GetComponent<ControllerPad>());
        }
    }

    private void SwithcToPad()
    {
        m_keyboardInput = false;
        if (m_playerObject)
        {
            m_playerObject.AddComponent<ControllerPad>();
            Destroy(m_playerObject.GetComponent<ControllerKeyboard>());
        }
    }

    public void SpawnPlayer()
    {
        if(!m_player)
        {
            spawnParent = GameObject.FindGameObjectWithTag("Respawn");
            m_playerObject = Instantiate<GameObject>(m_playerPrefab);
            m_player = Player.Instance;
            if (m_keyboardInput)
                m_playerObject.AddComponent<ControllerKeyboard>();
            else
                m_playerObject.AddComponent<ControllerPad>();
            m_playerObject.transform.SetParent(spawnParent.transform);
            m_playerObject.transform.position = spawnParent.transform.position;
            m_camera = Camera.main.gameObject;
        }
        else
        {
            m_playerObject.transform.position = m_respawn;
            Vector3 pos = m_camera.transform.position;
            pos.x = m_respawn.x;
            m_camera.transform.position = pos;
            Debug.Log("player respawn animation");
        }
    }
}
