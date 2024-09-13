using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    Player playerScript;
    public int healAmount;
    public float pickupDuration;

    private void Start(){
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(SelfDestructCountdown());
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Player"){
            playerScript.Heal(healAmount);
            Destroy(gameObject); 
        }
   }
   IEnumerator SelfDestructCountdown()
    {
        yield return new WaitForSeconds(pickupDuration);
        Destroy(gameObject);
    }
}
