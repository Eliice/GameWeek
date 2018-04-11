using UnityEngine;

public class Monster : MonoBehaviour {

    private Transform m_target = null;



	void Start () {
        m_target = Player.Instance.transform;
	}
	
	void Update () {
        if(!m_target)
            m_target = Player.Instance.transform;
        else
        {
            Vector3 pos = transform.position;
            pos.y = m_target.position.y;
            transform.position = pos;
        }
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            GameMng.Instance.SpawnPlayer();
    }
}
