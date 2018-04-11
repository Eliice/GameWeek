using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    private Transform m_target = null;



	// Use this for initialization
	void Start () {
        m_target = Player.Instance.transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        pos.y = m_target.position.y;
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            GameMng.Instance.SpawnPlayer();
    }
}
