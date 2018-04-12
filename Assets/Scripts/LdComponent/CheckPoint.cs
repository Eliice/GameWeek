using UnityEngine;

public class CheckPoint : MonoBehaviour {

    ParticleSystem Particles;


    private void Start()
    {
        Particles = this.GetComponent<ParticleSystem>();
        Particles.Stop();
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
    }
}
