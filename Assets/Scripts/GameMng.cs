using UnityEngine;

public class GameMng : MonoBehaviour {

    private GameMng m_instance;
    public GameMng Instance { get { return m_instance; } }

    private Player m_player = null;
    private GameObject m_playerObject;
    
    [SerializeField]
    private GameObject m_playerPrefab = null;

    private Vector3 m_respawn = new Vector3();
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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnPlayer()
    {
        if(!m_player)
        {
            m_playerObject = Instantiate<GameObject>(m_playerPrefab);
            m_player = Player.Instance;
        }
        else
        {
            m_playerObject.transform.position = m_respawn;
            Debug.LogAssertion("player respawn state unimplemented");
        }
    }
}
