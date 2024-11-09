using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using DG.Tweening;

public class Level1Trigger : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField]
    private GameObject maincamera;
    [SerializeField]
    private GameObject eventCamera;

    [Header("Player")]
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private PlayerCharacter character;

    private laser[] lasers;

    private bool onTrigger = false; //이벤트 여부

    private float currentTime;
    private int animCount;

    private void Awake()
    {
        eventCamera.SetActive(false);
        lasers = FindObjectsOfType<laser>();
    }

    private void FixedUpdate()
    {
        if (onTrigger)
        {
            currentTime += Time.deltaTime;
            StartEvent();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!onTrigger)
        {
            onTrigger = true;
            //CharacterSpecialKey characterSpecialKey = collision.GetComponent<CharacterSpecialKey>();
            //characterSpecialKey.onDash = true;
            player.GetComponent<SkillSetting>().AddSkill("Dash");
            foreach(laser laser in lasers)
            {
                if(laser.CompareTag("Laser"))
                {
                    laser.OnLaser();
                }
            }
        }
    }

    private void StartEvent()
    {
        switch (animCount)
        {
            case 0:
                character.onPlayerStop = true;
                player.GetComponent<Animator>().SetFloat("HorizontalSpeed", 0);
                player.GetComponent<Animator>().SetFloat("VerticalSpeed", 0);
                nextCount(1);
                break;

            case 1:
                if(currentTime > 1f)
                {
                    maincamera.SetActive(false);
                    eventCamera.SetActive(true);
                    eventCamera.transform.DOMove(new Vector3(119.5f, 34.5f, -13.5f), 3);
                    nextCount(2);
                }
                break;
            case 2:
                if (currentTime > 2f)
                {
                    nextCount(3);
                }
                break;
            case 3:
                if (currentTime > 3f)
                {
                    nextCount(4);
                }
                break;
            case 4:
                if (currentTime > 4f)
                {
                    character.onPlayerStop = false;
                    maincamera.SetActive(true);
                    eventCamera.SetActive(false);
                    eventCamera.transform.DOMove(new Vector3(113f, 32.5f, -13.5f), 1);
                    nextCount(5);
                }
                break;
            case 5:
                break;

        }

        

    }

    void nextCount(int num)
    {
        currentTime = 0;
        animCount = num;
    }
}
