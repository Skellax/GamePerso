using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController player;
    private Vector3 tartgetPoint = Vector3.zero;
    [SerializeField] float topPosition;


    //Camera Moving 
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lookAheadDistance, lookAheadSpeed;

    private float lookOffset;


    // Start is called before the first frame update
    void Start()
    {
        tartgetPoint = new Vector3(player.transform.position.x, player.transform.position.y + topPosition, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //move camera axe y
        tartgetPoint.y = player.transform.position.y + topPosition;

        if (player.GetRB().velocity.x > 0f)
        {
            lookOffset = Mathf.Lerp(lookOffset, lookAheadDistance, lookAheadSpeed * Time.deltaTime);
        }
        if (player.GetRB().velocity.x < 0f)
        {
            lookOffset = Mathf.Lerp(lookOffset, -lookAheadDistance, lookAheadSpeed * Time.deltaTime);
        }

        //move camera axe x 
        tartgetPoint.x = player.transform.position.x + lookOffset;

        transform.position = Vector3.Lerp(transform.position, tartgetPoint, moveSpeed * Time.deltaTime);
        
    }
}
