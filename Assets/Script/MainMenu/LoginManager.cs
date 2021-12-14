using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [Header("LoginPanel")]
    public InputField InputField_ID;
    public InputField InputField_PW;

    [Header("OnOffControll")]
    public GameObject LoginView;
    public GameObject MainBtn;
    public GameObject Option;
    public GameObject Title;
    public GameObject RankList;

   


    private string ID = "Host";
    private string PW = "1234";

    private void Start()
    {
       
    }
    public void LoginBtn()
    {
        StartCoroutine(Login());
    }

    public void LoginOk()
    {
        Debug.Log("로그인 성공");
        NetworkManager.Instance.RoomListRequest();
        LoginView.SetActive(false);
        MainBtn.SetActive(false);
        Option.SetActive(true);
        Title.SetActive(false);
        RankList.SetActive(false);
    }
    public void ShowRank()
    {
        NetworkManager.Instance.RankListRequest("1");
        LoginView.SetActive(false);
        MainBtn.SetActive(false);
        Option.SetActive(true);
        Title.SetActive(false);
        RankList.SetActive(false);
    }
    IEnumerator Login()
    {
        //if (InputField_ID.text == ID && InputField_PW.text == PW)
        //{
        //    Debug.Log("로그인 성공");
        //    LoginView.SetActive(false);
        //    MainBtn.SetActive(false);
        //    Option.SetActive(true);
        //    Title.SetActive(false);

        //}
        //else
        //{
        //    Debug.Log("로그인 실패");
        //}


        NetworkManager.Instance.Login(InputField_ID.text, InputField_PW.text);

        yield return null;
    }
}
