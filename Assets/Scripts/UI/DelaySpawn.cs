using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelaySpawn : MonoBehaviour {

    [SerializeField]
    private float m_delayBeforSpawn = 2.9f;


	// Use this for initialization
	void Start () {
        transform.Rotate(0, 90, 0);
	}
	
}




