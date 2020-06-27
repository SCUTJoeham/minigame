using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int maxLive = 3;
    int initLive = 100;
    int curLive;

    public int maxShield = 1;
    int initShield = 1;
    int curShield;

    float ChargeInterval = 15.0f;
    float ChargeTime = 0;

    public static int DNA = 100;

    public Text liveNum;
    public Text dnaNum;
    public Text shieldNum;

    Renderer myRender;
    public int blinks;
    public float time;

    public AudioSource dnaAudio;
    public AudioSource hurtAudio;
    public AudioSource deathAudio;
    public AudioSource upgradeAudio;

    void Start()
    {
        curLive = initLive;
        curShield = initShield;
        UpdateUI();
        myRender = GetComponent<Renderer>();
    }

    void Update()
    {
        ChargeShield();
        UpgradeShield1();
        UpgradeShield2();
    }


    //更新UI
    void UpdateUI()
    {
        liveNum.text = curLive.ToString();
        dnaNum.text = DNA.ToString();
        shieldNum.text = curShield.ToString();
    }

    //获得生命点
    void GetLive(int live)
    {
        curLive = Mathf.Clamp(curLive + live, 0, maxLive);
        UpdateUI();
    }

    //获得护盾
    void GetShield(int shield)
    {
        curShield = Mathf.Clamp(curShield + shield, 0, maxShield);
        UpdateUI();
    }

    //获得DNA
    void GetDNA(int dna)
    {
        DNA += dna;
        dnaAudio.Play();
        UpdateUI();
    }

    //升级护盾冷缺时间（减少冷却时间）
    void UpgradeShield1()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (ChargeInterval == 15.0f)
            {
                if (DNA >= 10)
                {
                    DNA -= 10;
                    ChargeInterval -= 5.0f;
                    upgradeAudio.Play();
                }
            }
            else if (ChargeInterval == 10.0f)
            {
                if (DNA >= 20)
                {
                    DNA -= 20;
                    ChargeInterval -= 5.0f;
                    upgradeAudio.Play();
                }
            }
        }

    }

    //升级护盾最高充能点数
    void UpgradeShield2()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (maxShield == 1)
            {
                if (DNA >= 10)
                {
                    DNA -= 10;
                    maxShield += 1;
                    upgradeAudio.Play();
                }
            }
            else if (maxShield == 2)
            {
                if (DNA >= 20)
                {
                    DNA -= 20;
                    maxShield += 1;
                    upgradeAudio.Play();
                }
            }
        }

    }

    //护盾充能
    void ChargeShield()
    {
        if (curShield < maxShield)
        {
            ChargeTime += Time.deltaTime;
            if (ChargeTime >= ChargeInterval)
            {
                ChargeTime = 0;
                GetShield(1);
            }
        }
    }

    //玩家受伤
    public void BeDamaged(int damage)
    {
        if (curShield > damage)
        {
            curShield -= damage;
            hurtAudio.Play();
            BlinkPlayer(blinks, time);
            UpdateUI();
            return;
        }

        if (curShield <= damage)
        {
            int rest = damage - curShield;
            curShield -= curShield;
            if (curLive > rest)
            {
                curLive -= rest;
                hurtAudio.Play();
                BlinkPlayer(blinks, time);
                UpdateUI();
                return;
            }
            if (curLive <= rest)
            {
                curLive -= curLive;
                hurtAudio.Play();
                BlinkPlayer(blinks, time);
                UpdateUI();
                BeDie();
            }
        }

    }

    //受伤害闪烁
    void BlinkPlayer(int numBlink, float seconds)
    {
        StartCoroutine(DoBlink(numBlink, seconds));
    }

    IEnumerator DoBlink(int numBlinks, float seconds)
    {
        for (int i = 0; i <= numBlinks; i++)
        {
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }

    // 玩家死亡
    void BeDie()
    {
        deathAudio.Play();
        Invoke("Restart", 1.5f);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
