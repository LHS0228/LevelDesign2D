using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Level2_Event : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private GameObject animCamera;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject upBlock;
    [SerializeField]
    private GameObject downBlock;

    private float runTime;
    [SerializeField]
    private float animCount;
    [SerializeField]
    private bool onEvent;
    [SerializeField]
    private bool endEvent;

    private void Start()
    {
        endEvent = LoadBool("Level2_Event", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!endEvent && onEvent)
        {
            Anim();
            runTime += Time.deltaTime;
        }
    }

    private void Anim()
    {
        switch (animCount)
        {
            case 0:
                player.GetComponent<PlayerCharacter>().onPlayerStop = true;
                NextCount(1);
                break;
            case 1:
                if(runTime >= 2f)
                {
                    animCamera.transform.position = mainCamera.transform.position;
                    animCamera.SetActive(true);
                    mainCamera.SetActive(false);
                    NextCount(2);
                }
                break;
            case 2:
                if(runTime >= 1)
                {
                    upBlock.transform.position += new Vector3(0, 5, 0);
                    downBlock.transform.position += new Vector3(0, -5, 0);
                    NextCount(3);
                }
                break;
            case 3:
                player.GetComponent<SkillSetting>().AddSkill("DoubleJump");
                //player.GetComponent<CharacterSpecialKey>().onDoubleJump = true;
                player.GetComponent<PlayerCharacter>().onPlayerStop = false;
                animCamera.SetActive(false);
                mainCamera.SetActive(true);
                endEvent = true;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onEvent = true;
        //SaveBool("Level2_Event", false);
    }

    public void SaveBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool LoadBool(string key, bool defaultValue = false)
    {
        return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
    }

    private void NextCount(int num)
    {
        animCount = num;
        runTime = 0;
    }
}
