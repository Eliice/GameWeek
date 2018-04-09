using UnityEngine;

public class Scrolling : MonoBehaviour {

    #region mainControle

    private GameObject m_target = null;

    [SerializeField]
    private float m_cameraSpeed = 1.0f;

    public float CameraSpeed { get { return m_cameraSpeed; } set { m_cameraSpeed = value; } }

	void Update () {
        UpdateCam();
	}

    private void UpdateCam()
    {
        Vector3 pos = transform.position;
        pos.x += m_cameraSpeed * Time.deltaTime;
        transform.position = pos;
    }
    #endregion


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            /// Trigger GameOver & retry screen
        }
    }

}
