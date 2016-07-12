using UnityEngine;
using System.Collections;

//CURRENTLY NOT IMPLEMENTED
public class Powerup : MonoBehaviour
{
    private float speedMultiplier;
    private int duration;

    public float SpeedMultiplier { get; set; }

    public int Duration { get; set; }

    public Powerup(float speedmultiplier)//, int duration)
    {
        this.speedMultiplier = speedmultiplier;
        //this.duration = duration;
    }

    public void Pickup(Transform player)
    {
        PlayerMovementPerPlayer scr = (PlayerMovementPerPlayer) player.GetComponent(typeof(PlayerMovementPerPlayer));
        scr.speedmult += speedMultiplier;
        StartCoroutine(Waiting());
        scr.speedmult -= speedMultiplier;
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(5);
    }
}
