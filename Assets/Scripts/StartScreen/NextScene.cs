using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour {
	
	void Update () {
		if(Input.anyKey)
        {
            Debug.Log("go to next");
        }
	}
}
