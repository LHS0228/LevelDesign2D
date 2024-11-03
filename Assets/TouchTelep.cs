using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTelep : MonoBehaviour
{
    private float runTime;
    [SerializeField]
    private bool onTouch;
    [SerializeField]
    private PlayerCharacter playerCharacter;

    private void FixedUpdate()
    {
        if(onTouch)
        {
            runTime += Time.deltaTime;
        }

        if(runTime > 0.4f)
        {
            playerCharacter.Respawn(false, true);
            onTouch = false;
            runTime = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerCharacter = collision.gameObject.GetComponent<PlayerCharacter>();
            onTouch = true;
            Debug.Log("°¨ÁöÇÔ");
        }
    }
}
