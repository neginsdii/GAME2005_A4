using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerBehavior : MonoBehaviour
{

    public Transform BulletSpawn;
    public GameObject bullet;
    public int FireRate;
    private Vector3 Pos;
    public BulletManager bulletManager;
    // Start is called before the first frame update

    
    void Start()
    {
      
       
    }

    // Update is called once per frame
    void Update()
    {
        _Fire();
      
    }

    private void _Fire()
    {
        
        if (Input.GetMouseButtonDown(0))
        {

          //  if (Time.frameCount % FireRate == 0)
          //  {
             
                GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();
                if (bullet != null)
                {
                    bullet.transform.position = BulletSpawn.position;
                    bullet.transform.rotation = Quaternion.identity;
                    bullet.transform.SetParent(bulletManager.gameObject.transform);
                    bullet.GetComponent<SphereBehavior>().direction = BulletSpawn.forward;
                    bullet.SetActive(true);
                }
              
         //   }
        }
    }
    
}
