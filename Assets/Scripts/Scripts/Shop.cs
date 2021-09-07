using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Shop : MonoBehaviour
{
    public RectTransform uiGroup;
    public Animator anim;

    public GameObject[] itemObj;    //아이템프리펩 변수
    public int[] itemPrice;         //가격 변수
    public Transform[] itemPos;     //위치 변수
    public string[] talkData;
    public Text talkText;           //대사


    Player enterPlayer;

    public void Enter(Player player)
    {
        enterPlayer = player;
        uiGroup.anchoredPosition = Vector3.zero;    //화면정중앙에 띄움
    }

    
    public void Exit()
    {
        anim.SetTrigger("doHello");     //애니메이션 트리거
        uiGroup.anchoredPosition = Vector3.down * 1000;
    }

    public void Buy(int index)
    {
        int price = itemPrice[index];
        if(price > enterPlayer.coin)
        {
            StopCoroutine(Talk());  //사용자가 같은 버튼을 계속 누를 수 있기 때문에 한번 종료후 실행
            StartCoroutine(Talk());
            return;
        }

        enterPlayer.coin -= price;
        Vector3 ranVec = Vector3.right * Random.Range(-3, 3) + Vector3.forward * Random.Range(-3, 3);   //랜덤벡터 생성

        Instantiate(itemObj[index], itemPos[index].position + ranVec, itemPos[index].rotation);  //구입성공시 아이템생성
    }

    IEnumerator Talk()
    {
        talkText.text = talkData[1];
        yield return new WaitForSeconds(2f);    //2초동안 다른대사 후 원래대사로 변경
        talkText.text = talkData[0];
    }
}
