using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSetting : MonoBehaviour
{
    public CharacterSpecialKey playerSpeicalKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveSkill(string skill)
    {
        switch(skill)
        {
            case "DoubleJump":
                SaveBool("OnDoubleJump", true);
                break;

            case "Dash":
                SaveBool("OnDash", true);
                break;

            case "Glider":
                SaveBool("OnGlide", true);
                break;
        }
    }

    public void LoadSkill(string skill)
    {
        switch (skill)
        {
            case "DoubleJump":
                playerSpeicalKey.onDoubleJump = LoadBool("OnDoubleJump", false);
                break;

            case "Dash":
                playerSpeicalKey.onDash = LoadBool("OnDash", false);
                break;

            case "Glider":
                playerSpeicalKey.onGlider = LoadBool("OnGlide", false);
                break;
        }
    }

    public void ResetSkill()
    {
        SaveBool("OnDoubleJump", false);
        SaveBool("OnDash", false);
        SaveBool("OnGlide", false);
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
