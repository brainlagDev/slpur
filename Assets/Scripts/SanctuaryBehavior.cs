using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanctuaryBehavior : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision){
        var go = collision.gameObject;
        if(go.CompareTag("Player")){
            Debug.Log("Player is near Sanct");
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        var go = collision.gameObject;
        if(go.CompareTag("Player")){
            Debug.Log("Player has left the Sanct");
        }
    }
}
