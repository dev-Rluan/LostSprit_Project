using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEX : MonoBehaviour
{
    public float speed;
    public GameObject[] weapons;    //무기배열
    public bool[] hasWeapons;       //현재 가지고있는 무기배열
    public GameObject[] grenades;
    public GameObject[] itemObj;
    public Transform[] itemPos;
    Transform trans;

    public int ammo;
    public int coin;
    public int health;
    public int hasGrenades;
    public int score;

    public int cntitem = 0;
    public int locitem = 0;

    public int maxAmmo;
    public int maxCoin;
    public int maxHealth;
    public int maxHasGrenades;

    float hAxis;
    float vAxis;

    bool sDown; //테스트용
    bool save;
    bool wDown;
    bool jDown;
    bool iDown;
    bool sDown1;
    bool sDown2;
    bool sDown3;

    bool isJump;
    bool isSwap;
    bool isBorder;  //경계선에 닿았는지 확인하는 변수
                    //  bool isShop;    //쇼핑중일때

    Vector3 moveVec;
    Rigidbody rigid;    //물리효과를 사용하기 위한 변수
    Animator anim;

    GameObject nearObject;
    public GameObject equipWeapon;

    int equipWeaponIndex = -1;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();  //초기화
        anim = GetComponentInChildren<Animator>();  //초기화

        Debug.Log(PlayerPrefs.GetInt("MaxScore"));    //저장된 데이터 확인
        PlayerPrefs.SetInt("MaxScore", 112500); //PlayerPrefs = 유니티에서 제공하는 간단한 저장 기능
    }

    // GetAxisRaw() = Axis 값을 정수로 반환하는 함수
    void Update()
    {
        GetInput(); //초기화된 변수들이 아래함수에서 사용되야 하므로 첫번째에 위치
        Move();
        Turn();
        Jump();
        Swap();
        Interation();
        Putitem();
    }

    void GetInput()
    {   //키입력 이벤트
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        //미작성wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");    //스페이스바를 누르는 순간
        //미작성        iDown = Input.GetButtonDown("Interation");
        //미작성        sDown1 = Input.GetButtonDown("Swap1");
        //미작성        sDown2 = Input.GetButtonDown("Swap2");
        //미작성        sDown3 = Input.GetButtonDown("Swap3");
        //미작성        sDown = Input.GetButtonDown("Putitem");
    }

    void Move() //이동
    {
        // x축, y축, z축                   //모든 방향이 같은값을 가짐(방향값이 1로 보정된 벡터)
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isSwap) //무기교체중일때   
            moveVec = Vector3.zero; //움직이지 못함      
        if (!isBorder)  //벽에 닿았을때   > 176줄에 해당코드
        {/*
            if (wDown)  //걷기 키를 눌렀을 때 (shift)          
                transform.position += moveVec * speed * 0.3f * Time.deltaTime;
            else*/
                transform.position += moveVec * speed * Time.deltaTime;
        }
        //미작성        anim.SetBool("isRun", moveVec != Vector3.zero); //파라미터값 설정
        //미작성        anim.SetBool("isWalk", wDown); //파라미터값 설정
    }

    void Turn() //회전
    {
        //LookAt() = 지정된 벡터를 향해서 회전시켜주는 함수
        transform.LookAt(transform.position + moveVec);//나아가는 방향으로 바라봄 
    }
    void Jump() //점프
    {
        if (jDown && moveVec == Vector3.zero && !isJump && !isSwap)    //점프키를 누르고 isJump가 false일때, isSwap이 false일때
        {
            //AddForce() = 물리적인 힘을 가하는 함수
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse); //점프
//미작성           anim.SetBool("isJump", true);
//미작성           anim.SetTrigger("doJump");  //애니메이션 트리거
            isJump = true;
        }
    }
    void Swap() //무기교체
    {
        if (sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0)) //1번키를 눌렀을 때 1번무기가 없을때나 들어진무기가 같은 무기일때
            return;
        if (sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;
        if (sDown3 && (!hasWeapons[2] || equipWeaponIndex == 2))
            return;
        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;    //1번키를 눌렀으면 
        if (sDown2) weaponIndex = 1;    //2번키를 눌렀으면 
        if (sDown3) weaponIndex = 2;    //3번키를 눌렀으면 

        if ((sDown1 || sDown2 || sDown3) && !isJump)    //1, 2, 3번키중 하나를 눌렀고 isJump가 아닐때
        {
            if (equipWeapon != null)
                equipWeapon.SetActive(false);   //먼저 착용중인 무기를 해제함

            equipWeaponIndex = weaponIndex; //누른키에 해당하는 값을 대입함
            equipWeapon = weapons[weaponIndex]; //무기배열에서 해당하는 값을 대입함
            equipWeapon.SetActive(true);    //해당 무기를 보여줌

            anim.SetTrigger("doSwap");  //애니메이션 트리거

            isSwap = true;

            Invoke("SwapOut", 0.4f);    //함수시작시간 지연 0.4초
        }
    }
    void SwapOut()  //무기교체 끝
    {
        isSwap = false;
    }
    void Interation()   //E키를 눌렀을 때 실행되는 상호작용
    {
        if (iDown && nearObject != null && !isJump)  //E키가 눌렸고, 가까운물체가 존재하고, isJump가 아닐때
        {
            if (nearObject.tag == "Weapon")      //접촉한 물체가 무기이면
            {
                save = nearObject;
//미작성      Item item = nearObject.GetComponent<Item>();   //Item 컴포넌트를 받아옴 
//미작성      int weaponIndex = item.value;   //item의 value값을 대입함
//미작성               hasWeapons[weaponIndex] = true;     //현재 착용중인 무기배열의 값을 활성화 시킴 

                Destroy(nearObject);    //상호작용한 물체를 삭제함
            }
            else if (nearObject.tag == "Shop")  //접촉한 물체가 상점이면
            {
//미작성      Shop shop = nearObject.GetComponent<Shop>();
//미작성      shop.Enter(this);   //플레이어 정보를 집어 넣어줌 //this = Player
                // isShop = true;
            }

        }
    }
    void Putitem()    //아이템 드랍하기
    {
        if (sDown && moveVec == Vector3.zero && !isJump && cntitem != 0)
        {
            Instantiate(itemObj[locitem], itemPos[0].position, Quaternion.identity);
            cntitem -= 1;

        }
    }
    void StopToWall()   //벽에 닿았을 시 이동제한
    {
        //Scene 내에서 Ray를 보여주는 함수 / 시작위치 / 쏘는방향 / Ray의 길이 / 색깔
        Debug.DrawRay(transform.position, transform.forward * 2, Color.green);
        // Wall이라는 LayerMask를 가진 물체랑 충돌하면 bool값이 true로 바뀜
        isBorder = Physics.Raycast(transform.position, transform.forward, 2, LayerMask.GetMask("Wall"));
    }
    void FreezeRotation() //회전방지(캐릭터가 물체와 닿았을 때 의도치 않게 회전되는 오류현상 방지)
    {
        rigid.angularVelocity = Vector3.zero;   //회전속도를 0으로 바꿈 > 스스로 도는현상 X 
    }
    void FixedUpdate()  //함수실행
    {
        FreezeRotation();
        StopToWall();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")     //바닥과 닿았을 때
        {
//미작성            anim.SetBool("isJump", false);
            isJump = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        /*
        if (other.tag == "Item")     //아이템과 닿았을 때
        {
        Item item = other.GetComponent<Item>();     //Item 스크립트 가져오기
            if (cntitem == 0)
            {
                switch (item.type)
                {
                    case Item.Type.Ammo:
                        cntitem += 1;
                        locitem = 0;
                        ammo += item.value;     //아이템의 value 값 만큼 ammo값을 더함
                        if (ammo > maxAmmo)     //현재 ammo의 값이 최대치보다 크면
                            ammo = maxAmmo;     //값을 최대치에 맞춤
                        break;
                    case Item.Type.Coin:
                        coin += item.value;
                        if (coin > maxCoin)
                            coin = maxCoin;
                        break;
                    case Item.Type.Heart:
                        cntitem += 1;
                        locitem = 2;
                        health += item.value;
                        if (health > maxHealth)
                            health = maxHealth;
                        break;
                    case Item.Type.Grenade:
                        cntitem += 1;
                        locitem = 1;
                        grenades[hasGrenades].SetActive(true);      //캐릭터 주위에서 공전하는 수류탄 활성화
                        hasGrenades += item.value;
                        if (hasGrenades > maxHasGrenades)
                            hasGrenades = maxHasGrenades;
                        break;
                }
                Destroy(other.gameObject);  //접촉한 물체 삭제
            }
        }*/
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon" || other.tag == "Shop")   //무기나 상점에 닿았을 때
            nearObject = other.gameObject;  //nearObject에 값 저장
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")  // 무기가 닿는 영역에서 벗어났을 때
            nearObject = null;  //값을 비워줌
        else if (other.tag == "Shop")    //상점영역에서 벗어났을 때
        {
//미작성           Shop shop = nearObject.GetComponent<Shop>();
//미작성            shop.Exit();
            // isShop = false;
            nearObject = null;
        }
    }
}
