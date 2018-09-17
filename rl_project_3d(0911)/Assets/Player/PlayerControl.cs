using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

    public float moveSpeed;
    public float runScale;
    public GameObject hpBar;
    public GameObject staminaBar;
    public float hpMax;
    float hp;
    public float staminaMax;
    float stamina;
    public GameObject bullet;
    public GameObject special;

    public Transform target;
    public LayerMask enemyMask;
    public LayerMask structMask;

    Camera mainCamera;
    Rigidbody rb;
    Vector3 dir;
    float height;
    float width;
    float cameraDistance;
    int weaponNumber = 0;

    // Use this for initialization
    void Start () {
        mainCamera = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
        height = 2 * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;

        print(height);
        print(width);
        cameraDistance = mainCamera.transform.localPosition.y;
        hp = hpMax;
        stamina = staminaMax;
    }

    // Update is called once per frame
    void Update () {
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        dir = new Vector3(h,0,v ).normalized;

        //Vector3 temp = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 temp = Input.mousePosition;
        temp = new Vector3(temp.x / Screen.width * width - width / 2 , temp.y / Screen.height * height - height / 2 , 1);
        
        //clamp(ref temp, .1f);
        target.localPosition = temp;
        mainCamera.transform.localPosition = new Vector3(target.localPosition.x, cameraDistance*2, -cameraDistance *2 + target.localPosition.y) / 2f;
        //target.position = new Vector3(target.position.x, 1, target.position.z);
        
        //무기 바꾸는 부분
        if (Input.GetKeyDown(KeyCode.Alpha1))
            weaponNumber = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            weaponNumber = 2;


        if (Input.GetMouseButtonDown(0))
        {
            Vector3 bulletDir = target.localPosition;                                                                       //총알이 나갈 방향 정함
            bulletDir.z = bulletDir.y;                                                                                      //크로스헤어는 X,Y방향으로만 움직이므로 Y를 Z로 바꿈
            bulletDir.y = 0;                                                                                                //2D로 구현된것이라 위아래로는 날아가면안됨
            switch (weaponNumber)
            {
                case 1:
                    GameObject obj = Instantiate(bullet, transform.position + bulletDir.normalized * .5f, Quaternion.identity);     //총알을 현재위치에서 크로스헤어방향으로 0.5만큼 떨어진 위치에 생성 + 회전정보 없음
                    Rigidbody rbTemp = obj.GetComponent<Rigidbody>();
                    rbTemp.velocity = bulletDir.normalized * 10;                                                                    //총알을 크로스헤어 방향으로 날림
                    Destroy(obj, 5f);                                                                                               //생성된 총알 5초후 제거
                    break;

                case 2:
                    Collider[] colliders = Physics.OverlapSphere(transform.position, 5f/*radius*/, enemyMask);
                    foreach(var col in colliders)
                    {
                        EnemyControler ec = col.GetComponentInParent<EnemyControler>();
                        Vector3 dirFromPlayer = col.GetComponent<Transform>().position - transform.position;
                        if (Vector3.Angle(bulletDir, dirFromPlayer) <= 90)
                        {
                            ec.GetDamage(1);
                            dirFromPlayer.y = 0;
                            ec.KnockBack(dirFromPlayer,10f);
                        }
                    }
                    break;

                default:

                    break;
            }
        }

        

        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(special, transform.position, Quaternion.identity);
        }



        if (0 < Input.GetAxis("Mouse ScrollWheel"))
        {
            mainCamera.orthographicSize -= 0.5f;
            height = 2 * Camera.main.orthographicSize;
            width = height * Camera.main.aspect;
            //print("wheeldown");
        }
        if (0 > Input.GetAxis("Mouse ScrollWheel"))
        { 
            mainCamera.orthographicSize += 0.5f;
            height = 2 * Camera.main.orthographicSize;
            width = height * Camera.main.aspect;
            //print("wheelup");
        }

        HpRezen();

    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (stamina > 0)
            {
                rb.velocity = dir * moveSpeed * runScale * Time.fixedDeltaTime;
                stamina -= Time.fixedDeltaTime;
            }
            else
            {
                rb.velocity = dir * moveSpeed * Time.fixedDeltaTime;
            }
            
        }
        else
        {
            if (stamina < staminaMax)
                stamina += Time.fixedDeltaTime;
            rb.velocity = dir * moveSpeed * Time.fixedDeltaTime;
        }
    }

    // 대미지 받는 부분
    private void OnCollisionEnter(Collision collision)
    {
        if ( hp > 0 )
        {
            print("hp -5");
            float Dmg = collision.collider.GetComponent<EnemyControler>().AttackDamage;
            hp -= Dmg;
            hpBar.GetComponent<Image>().fillAmount = hp / hpMax;
        }
    }

    // hp 리젠
    void HpRezen()
    {
        if ( hp < hpMax)
        {
            print("Rezen +1");
            hp += 5 * Time.deltaTime;
            print("hp: " + hp);
            hpBar.GetComponent<Image>().fillAmount = hp / hpMax;
        }       
    }
}
