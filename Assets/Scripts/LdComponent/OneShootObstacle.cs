using UnityEngine;

public class OneShootObstacle : MonoBehaviour {

    [SerializeField]
    private float m_speed = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Animator Anim = other.GetComponent<Animator>();
            Anim.SetTrigger("Death");
        }
    }


    private void Update()
    {
        Vector3 pos = transform.position;
        pos.x += m_speed * Time.deltaTime;
        transform.position = pos;
    }
}
