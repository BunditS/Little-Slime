using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameObject Stamina;
    public int maxStamina = 50;
    public int currenStamina = 50;
    public int StaminaRecoveryRate = 2;
    public int Jpu = 10;
    public bool canSuperJump;

    public float MinutePerDay; // 1 วันมี 86400 วินาที // 1 วันต้องการให้ยาวนานกี่นาที
    public int hour;
    public int minute;
    private float countTime;

    private void Awake()
    {
        instance = this;
        Stamina = GameObject.Find("Stamina");
    }
    void Start()
    {
        MinutePerDay = (1 / (1440 / MinutePerDay)) * 60;
        InvokeRepeating("StaminaRecovery", 1f, 2f);
    }
    void Update()
    { 
        //เวลาในเกม 
        countTime += Time.deltaTime;
        if (countTime >= MinutePerDay)
        {
            minute++;
            countTime = 0;
            hour = minute / 60;
        }
        if (minute >= 1440)
        {
            minute = 0;
        }
        hour = minute / 60;

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        StaminaManager();

        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCount) 
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
          
        }
    }

private void StaminaManager()
    {
        if(currenStamina >= Jpu)
        {
            canSuperJump = true;
        }
        else
        {
            canSuperJump = false;
        }

        if (currenStamina >= maxStamina)
        {
            currenStamina = maxStamina;
        }
        if (currenStamina <= 0)
        {
            currenStamina = 0;
        }

        float radio = (float)currenStamina / (float)maxStamina;
        Stamina.GetComponentsInChildren<RectTransform>()[1].localScale = new Vector3(radio, 1, 1);
        Stamina.GetComponentInChildren<Text>().text = currenStamina.ToString();

        
    }
    private void StaminaRecovery()
    {
        currenStamina += StaminaRecoveryRate;
    }
}

