using UnityEngine;
using System.Collections;

public class RotateAvatar : MonoBehaviour {

    protected Transform m_transform;
    private bool isAndroidOrder=false;
	// Use this for initialization
	void Start () {

        m_transform = this.transform;
	}
	
	// Update is called once per frame
        void Update () {
           // Debug.Log("------test");
            if (isAndroidOrder)
            {
                m_transform.Rotate(new Vector3(0, 100 * Time.deltaTime, 0));
                isAndroidOrder = false;
            }
            else {
                m_transform.Rotate(new Vector3(0, 10 * Time.deltaTime, 0));
            }
        }
    //tainjia
    public void changeOrder(bool order) {
        isAndroidOrder = order;
    }
}
