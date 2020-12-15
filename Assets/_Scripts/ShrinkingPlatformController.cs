//------------------------------------------
// Gabriel Villeneuve, 1201212886
//  Platforms that shrink over time
//------------------------------------------




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShrinkingPlatformController : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public bool isActive;
    public float platformTimer;
    public float threshold;

    public PlayerBehaviour player;

    private Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();

        platformTimer = 10.0f;
        platformTimer = 0;
        isActive = false;
        distance = end.position - start.position;
    }

    // Update is called once per frame
    void Update()
    {

        //If the player is on it start shrinking
        if (isActive)
        {
            _Shrink();
        }
        else
        {
            //If the player is not on the platform, grow again
            _Grow();
        }


            platformTimer += Time.deltaTime * 0.1f;
            _Move();
        

        
    }

    private void _Move()
    {
        var distanceX = (distance.x > 0) ? start.position.x + Mathf.PingPong(platformTimer, distance.x) : start.position.x;
        var distanceY = (distance.y > 0) ? start.position.y + Mathf.PingPong(platformTimer, distance.y) : start.position.y;

        transform.position = new Vector3(distanceX, distanceY, 0.0f);
    }

    public void Reset()
    {
        transform.position = start.position;
        platformTimer = 0;
    }

    //Get slightly smaller every frame, without ever disapearing 
    private void _Shrink()
    {
        if (transform.localScale.x > 0.001f || transform.localScale.y > 0.001f)
        {
            transform.localScale = new Vector3(transform.localScale.x * 0.999f, transform.localScale.y * 0.999f, transform.localScale.z);
        }
    }

    //Shrink but backwards lol
    private void _Grow()
    {
        if (transform.localScale.x < 1.0f || transform.localScale.y < 1.0f)
        {
            transform.localScale = new Vector3(transform.localScale.x * 1.01f, transform.localScale.y * 1.01f, transform.localScale.z);
        }
    }
}
