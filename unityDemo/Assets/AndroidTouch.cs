using UnityEngine;
using System.Collections;

public class AndroidTouch : MonoBehaviour {

    //test code--Else
    AndroidJavaObject ajo;
    public void callFromAndroid() {
        //Camera.main.transform.Translate(0, 0,  0.2f);
        Debug.Log("call from android------");
    }




    // 记录手指触屏的位置
    Vector2 m_screenpos = new Vector2();
    // 摄像机目标点
    public Transform cameraTarget;

	// Use this for initialization
	void Start () {
        //test code --else
        ajo = new AndroidJavaObject("cn.com.test.UnityPlayerActivity");
        ajo.Call<bool>("callFromUnity", "unity AndroidTouch start----");
        // 允许多点触控
        Input.multiTouchEnabled = true;
	}
	
	// Update is called once per frame
	void Update () {

        // 按手机上的返回键退出当前程序
        if (Input.GetKeyUp(KeyCode.Escape))
            Application.Quit();

#if !UNITY_EDITOR && ( UNITY_IOS || UNITY_ANDROID )

        MobileInput();
#else
        DesktopInput(); 
#endif


    }

    // 桌面系统鼠标操作
    void DesktopInput()
    {
        // 记录鼠标左键的移动距离
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        if (  mx!= 0 || my !=0 )
        {
            // 按住鼠标左键
            if (Input.GetMouseButton(0))
            {
                cameraTarget.Rotate(new Vector3(-my * 10, mx * 10, 0), Space.Self);
            }
        }
    }

    // 移动平台触屏操作
    void MobileInput()
    {

        if (Input.touchCount <= 0)
            return;

        // 1个手指触摸屏幕
        if (Input.touchCount == 1)
        {
            
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                // 记录手指触屏的位置
                m_screenpos = Input.touches[0].position;
                
            }
            // 手指移动
            else if (Input.touches[0].phase == TouchPhase.Moved)
            {
                // 旋转摄像机目标
                cameraTarget.Rotate(new Vector3(-Input.touches[0].deltaPosition.y, Input.touches[0].deltaPosition.x, 0), Space.Self);
            }

            // 手指离开屏幕 判断移动方向
            if (Input.touches[0].phase == TouchPhase.Ended && 
                Input.touches[0].phase != TouchPhase.Canceled)
            {

                Vector2 pos = Input.touches[0].position;

                // 手指水平移动
                if (Mathf.Abs(m_screenpos.x - pos.x) > Mathf.Abs(m_screenpos.y - pos.y))
                {
                    if (m_screenpos.x > pos.x){
                        Debug.Log("向左移动");
                    }
                    else{
                        Debug.Log("向右移动");
                    }
                }
                else   // 手指垂直移动
                {
                    if (m_screenpos.y > pos.y){
                        Debug.Log("向下移动");
                    }
                    else{
                        Debug.Log("向上移动");
                    }
                   
                }

            }

        }
        else if ( Input.touchCount >1 )
        {
            // 记录两个手指的位置
            Vector2 finger1 = new Vector2();
            Vector2 finger2 = new Vector2();

            // 记录两个手指的移动
            Vector2 mov1 = new Vector2();
            Vector2 mov2 = new Vector2();
      
            for (int i=0; i<2; i++ )
            {
                Touch touch = Input.touches[i];

                if (touch.phase == TouchPhase.Ended )
                    break;

                if ( touch.phase == TouchPhase.Moved )
                {
             
                    float mov = 0;
                    if (i == 0)
                    {
                        finger1 = touch.position;
                        mov1 = touch.deltaPosition;
                       
                    }
                    else
                    {
                        finger2 = touch.position;
                        mov2 = touch.deltaPosition;
                        
                        if (finger1.x > finger2.x)
                       {
                           mov = mov1.x;
                       }
                       else
                       {
                           mov = mov2.x;
                       }

                       if (finger1.y > finger2.y)
                       {
                           mov+= mov1.y;
                       }
                       else
                       {
                           mov+= mov2.y;
                       }

                       Camera.main.transform.Translate(0, 0, mov * 0.1f);
                    }
                   
                   
                }
            }
        }

 
    }
}
