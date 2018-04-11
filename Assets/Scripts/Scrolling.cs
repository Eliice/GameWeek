using UnityEngine;
using System.Collections;

public class Scrolling : MonoBehaviour {

    #region mainControle

    private GameObject m_target = null;

    [SerializeField]
    private float m_cameraSpeed = 1.0f;

    [SerializeField]
    private float lerpFactor = 1f;

    public float CameraSpeed { get { return m_cameraSpeed; } set { StartCoroutine(LerpRoutine(value)); } }

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


    private IEnumerator LerpRoutine(float newSpeed)
    {
        while (m_cameraSpeed != newSpeed)
        {
            m_cameraSpeed = Mathf.Lerp(m_cameraSpeed, newSpeed, lerpFactor * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }



}
