using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private PlayerController player;
    private GameObject slime;
    public float speed;
    private Vector3 startLocation, mousePosition, objectPosition, targetPosition;
    private float angle, rotateSpeed = 100f;
    public float rotationOfset = 270;
    public TrailRenderer arrowTrailRenderer;
    [HideInInspector]  public bool plannedtwo = false;
    public float checkRadius;
    public bool isColliding;
    public bool isCollidingEnemy;
    public LayerMask wallLayer;
    public LayerMask enemyLayer;
    public Transform checkTransform;
    public bool isStuck;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        slime = GameObject.FindGameObjectWithTag("Enemy");
        startLocation = transform.localPosition;
        isStuck = false;
    }

    void Update()
    {
        if(!player.arrowReturn && player.isControllingArrow)
        {
            isColliding = Physics2D.OverlapCircle(checkTransform.position, checkRadius, wallLayer);
            isCollidingEnemy = Physics2D.OverlapCircle(checkTransform.position, checkRadius, enemyLayer);
            if(isColliding)
            {
                GetComponent<BoxCollider2D>().isTrigger = false;
                isStuck = true;
                //transform.eulerAngles = Vector3.zero;
            }
        }
        if (!plannedtwo)
            unPlanned();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(checkTransform.position, checkRadius);
    }
    public void unPlanned()
    {
        if (player.isControllingArrow && !isStuck)
        {
            rotationOfset =0;
            arrowTrailRenderer.time = 0.5f;
            transform.parent = null;
            mousePosition = Input.mousePosition;

            mousePosition.z = 0;
            objectPosition = Camera.main.WorldToScreenPoint(transform.position);
            mousePosition.x -= objectPosition.x;
            mousePosition.y -= objectPosition.y;

            angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotationOfset));

            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else if (player.arrowReturn)
        {
            arrowTrailRenderer.time = 0.1f;

            transform.SetParent(player.gameObject.transform, true);
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, startLocation, speed * Time.deltaTime * 2);

            if (transform.localPosition == startLocation)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                player.arrowReturn = false;
            }
        }
    }
   public void plannedTwo(Vector2 mousePosition)
    {
        transform.parent = null;
        rotationOfset = 90;
        angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotationOfset));
        transform.position = new Vector2(mousePosition.x,mousePosition.y);
    }
}
