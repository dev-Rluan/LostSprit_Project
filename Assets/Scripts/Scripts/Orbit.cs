using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform target;    //공전목표
    public float orbitSpeed;    //공전속도
    Vector3 offset;             //목표와의 거리

    void Start()
    {
        offset = transform.position - target.position;  //현재 수류탄 위치에서 타겟 위치를 뺀 값
    }

    void Update()
    {
        transform.position = target.position + offset;
        //타겟 주위를 회전하는 함수 // 위치, 회전 축, 회전하는 수치
        transform.RotateAround(target.position, Vector3.up, orbitSpeed * Time.deltaTime);
        offset = transform.position - target.position;
    }
}
