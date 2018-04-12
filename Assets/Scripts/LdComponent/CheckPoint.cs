using UnityEngine;

public class CheckPoint : MonoBehaviour {

    ParticleSystem Particles;

    private AudioSource m_audiSource = null;


    private void Start()
    {
        Particles = this.GetComponent<ParticleSystem>();
        Particles.Stop();
        m_audiSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameMng.Instance.Respawn = transform.position;
            Player.Instance.ResetStamina();
            Activated();
        }
    }


    void Activated ()
    {
        Particles.Play();
        m_audiSource.Play();
    }
}
