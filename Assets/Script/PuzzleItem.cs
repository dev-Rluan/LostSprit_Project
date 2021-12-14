using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleItem : MonoBehaviour
{
    public enum Type { rockitem };
    public Type type;
    public bool redstatus = false;
    public bool bluestatus = false;


    Rigidbody rigid;
    //SphereCollider sphereCollider;
    Material mat;
    PuzzleEvent puzzleEvent;
    GameObject nearObject;

    void Awake()    //초기화
    {
        puzzleEvent = GetComponent<PuzzleEvent>();

        rigid = GetComponent<Rigidbody>();
        mat = GetComponent<MeshRenderer>().material;
        //오브젝트의 콜라이더중 첫번째것만 가져오므로 is Trigger가 포함되지 않은 콜라이더가 위로 올라가야함
        //sphereCollider = GetComponent<SphereCollider>();
    }

    void Update()
    {

    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "rockItem")
        {
            rigid.isKinematic = true;   //더이상 외부 물리효과에 의해서 움직이지 못함
                                        // sphereCollider.enabled = false;
        }
        /*
        if (collision.gameObject.tag == "fireplayer")
        {
            mat.color = Color.red;
        }
        if (collision.gameObject.tag == "waterplayer")
        {
            mat.color = Color.blue;
        }
        */
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "fireplayer")
        {
            redstatus = true;
            mat.color = Color.red;
            bluestatus = false;
        }
        if (other.tag == "waterplayer")
        {
            bluestatus = true;
            mat.color = Color.blue;
            redstatus = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "fireplayer")
            nearObject = other.gameObject;

        if (other.tag == "waterplayer")
            nearObject = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        nearObject = null;
    }


}
