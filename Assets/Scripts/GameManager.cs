using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //UI 클래스 사용을 위해 UnityEngine.UI 네임스페이스 호출

public class GameManager : MonoBehaviour
{
    public GameObject menuCam;
    public GameObject gameCam;
    public Player player;
  //  public Boss boss;
    public int stage;
    public float playTime;
  //  public bool isBattle;
  //  public int enemyCntA;
  //  public int enemyCntB;
  //  public int enemyCntC;

    public GameObject menuPanel;
    public GameObject gamePanel;
    public Text maxScoreTxt;
    public Text scoreTxt;
    public Text stageTxt;
    public Text playTimeTxt;
    public Text playerHealthTxt;
    public Text playerAmmoTxt;
    public Text playerCoinTxt;
    public Image weapon1Img;
    public Image weapon2Img;
    public Image weapon3Img;
    public Image weaponRImg;
   /* public Text enemyAtxt;
    public Text enemyBtxt;
    public Text enemyCtxt;
    public RectTransform bossHealthGroup;
    public RectTransform bossHealthBar;
   */

    void Awake()
    {
        maxScoreTxt.text = string.Format("{0:n0}", PlayerPrefs.GetInt("MaxScore"));
    }
    public void GameStart()
    {
        menuCam.SetActive(false);
        gameCam.SetActive(true);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);

        player.gameObject.SetActive(true);
    }
    void Update()
    {
        //if(isBattle)
            playTime += Time.deltaTime;
    }

    void LateUpdate()   //Update() 가 끝난 후 호출됨
    {
        //상단UI
        scoreTxt.text = string.Format("{0:n0}", player.score);
        stageTxt.text = "STAGE " + stage;

        int hour = (int)(playTime / 3600);
        int min = (int)((playTime - hour * 3600) / 60);
        int second = (int)(playTime % 60);

        playTimeTxt.text = string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second);

        //플레이어UI
        playerHealthTxt.text = player.health + " / " + player.maxHealth;
        playerCoinTxt.text = string.Format("{0:n0}", player.coin);
        if (player.equipWeapon == null)
        {
            playerAmmoTxt.text = "- / " + player.ammo;
        }
        else
            playerAmmoTxt.text = string.Format("{0:n0}", player.ammo);
        /*  else if (player.equipWeapon.type == Weapon.Type.Melee)
          {
              playerAmmoTxt.text = "- / " + player.ammo;
          } 
          else
          {
              playerAmmoTxt.text = player.equipWeapon.curAmmo + " / " + player.ammo;
          }*/

        //무기UI
        weapon1Img.color = new Color(1, 1, 1, player.hasWeapons[0] ? 1 : 0); //RGB는 건들이지 않고 alpha값만 변경
        weapon2Img.color = new Color(1, 1, 1, player.hasWeapons[1] ? 1 : 0);
        weapon3Img.color = new Color(1, 1, 1, player.hasWeapons[2] ? 1 : 0);
        weaponRImg.color = new Color(1, 1, 1, player.hasGrenades > 0 ? 1 : 0);

        //몬스터 숫자 UI
        /*
        enemyATxt.text = enemyCntA.ToString();
        enemyATxt.text = enemyCntB.ToString();
        enemyATxt.text = enemyCntC.ToString();

        bossHealthBar.localScale = new Vector3(boss.curHealth / boss.maxHealth, 1, 1);
        */
    }

}
