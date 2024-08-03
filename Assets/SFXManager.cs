using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource diveInSource; //smoothRotate
    public AudioSource diveOutSource; //smoothRotate
    public AudioSource magicballSource;
    public AudioSource pickupSource;

    public void PlayDiveIn()
    {
        if (diveInSource != null)
        {
            diveInSource.Play();
        }
    }

    public void PlayDiveOut()
    {
        if (diveOutSource != null)
        {
            diveOutSource.Play();
        }
    }

    public void PlayMagicBall()
    {
        if (magicballSource != null)
        {
            magicballSource.Play();
        }
    }

    public void PlayPickup()
    {
        if (pickupSource != null)
        {
            pickupSource.Play();
        }
    }
}
