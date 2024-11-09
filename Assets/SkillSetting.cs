using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSetting : MonoBehaviour
{
    private CharacterSpecialKey playerSpeicalKey;
    public bool isStart = false;

    private void Awake()
    {
        playerSpeicalKey = GetComponent<CharacterSpecialKey>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(isStart)
        {
            ResetSkill();
        }
        else
        {
            LoadSkill();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSkill(string skill)
    {
        switch(skill)
        {
            case "DoubleJump":
                SaveBool("OnDoubleJump", true);
                playerSpeicalKey.onDoubleJump = true;
                break;

            case "Dash":
                SaveBool("OnDash", true);
                playerSpeicalKey.onDash = true;
                break;

            case "Glider":
                SaveBool("OnGlide", true);
                playerSpeicalKey.onGlider = true;
                break;
        }
    }

    public void LoadSkill()
    {
        playerSpeicalKey.onDoubleJump = LoadBool("OnDoubleJump", false);
        playerSpeicalKey.onDash = LoadBool("OnDash", false);
        playerSpeicalKey.onGlider = LoadBool("OnGlide", false);
    }

    public void ResetSkill()
    {
        SaveBool("OnDoubleJump", false);
        SaveBool("OnDash", false);
        SaveBool("OnGlide", false);

        playerSpeicalKey.onDoubleJump = false;
        playerSpeicalKey.onDash = false;
        playerSpeicalKey.onGlider = false;
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
}
