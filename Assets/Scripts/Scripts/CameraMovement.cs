using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform objectTofollow; //따라갈 오브젝트 정보
    public float followSpeed = 10f; //따라가는 속도
    public float sensitivity = 100f; //마우스 감도
    public float clampAngle = 70f; // 카메라 제한 각도

    private float rotX; //마우스 값
    private float rotY;

    public Transform realCamera; //카메라의 정보
    public Vector3 dirNormalized; //방향
    public Vector3 finalDir; //최종방향
    public float minDistance; //최소거리
    public float maxDistance; //최대거리
    public float finalDistance; //최종거리
    public float smoothness = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x; //초기화
        rotY = transform.localRotation.eulerAngles.y; 

        dirNormalized = realCamera.localPosition.normalized; //초기화 normalized = 크기 0으로(방향만 지정)
        finalDistance = realCamera.localPosition.magnitude; //magnitude = 크기
    }

    // Update is called once per frame
    void Update()
    {
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime; //감도 * 현재까지의 시간
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime; //y축을 마우스로 x축을 그려서 돈다

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate() //카메라 움직임
    {
        transform.position = Vector3.MoveTowards(transform.position, objectTofollow.position, followSpeed * Time.deltaTime);

        finalDir = transform.TransformPoint(dirNormalized * maxDistance);

        RaycastHit hit; //장애물

        
        if(Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, minDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }
        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);

    }
}
