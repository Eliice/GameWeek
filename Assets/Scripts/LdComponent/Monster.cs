using UnityEngine;

public class Monster : MonoBehaviour {

    private Transform m_target = null;

    void Update () {
        if (!m_target)
            m_target = Player.Instance.transform;
       Vector3 pos = transform.position;
       pos.y = m_target.position.y;
       transform.position = pos;
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            GameMng.Instance.SpawnPlayer();
    }
}
