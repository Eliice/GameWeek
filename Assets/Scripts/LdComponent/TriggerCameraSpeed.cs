using UnityEngine;

public class TriggerCameraSpeed : MonoBehaviour {


    [SerializeField]
    private float m_newCameraSpeed = 1f;


    private Scrolling m_CameraScrolling = null;

	void Start () {
        m_CameraScrolling = Camera.main.GetComponent<Scrolling>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            m_CameraScrolling.CameraSpeed = m_newCameraSpeed;
    }

}

