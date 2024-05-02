using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlannedAttack : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject currentLine;
    public GameObject arrow;
    public GameObject player;

    public Transform atr;
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider2D;
    public List<Vector2> fingerPositons;
    bool touchButton = false, available = false, check = false;
    int currentWayPoint = 0, totalWayPoint = 0;

    public bool increateline = false;
    public bool canDraw = false;
    Vector2 ortx, orty;
    public float PlannedAttackEnd = 0.0f;

    void Start()
    {
        atr = arrow.GetComponent<Transform>();
    }

    void Update()
    {

        transform.position = player.transform.position;
        if (canDraw)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlannedAttackEnd = Time.time + 3.0f;
                touchButton = true;
                CreateLine();

            }
        }
        if (Input.GetMouseButton(0))
        {
            if (increateline)
            {
                Vector2 tempFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Vector2.Distance(tempFingerPos, fingerPositons[fingerPositons.Count - 1]) > .1f)
                {
                    UpdateLine(tempFingerPos);
                }
            }
        }
        if (Input.GetMouseButtonUp(0) || Time.time >= PlannedAttackEnd)
        {
            if (increateline)
            {
                currentLine.SetActive(false);
                totalWayPoint = fingerPositons.Count;
                touchButton = false;
                available = true;
                check = true;
            }
        }


    }
    void FixedUpdate()
    {
        if (check && totalWayPoint <= 20)
        {
            check = false;
            checkPoints();
        }

        if (!arrow.GetComponent<Arrow>().isStuck && available && currentWayPoint < totalWayPoint)
            moveArrow();
    }
    void checkPoints()
    {
        for (int i = 0; i < totalWayPoint - 1; i++)
        {
            if ((float)(fingerPositons[i + 1].x - fingerPositons[i].x) >= 0.70f)
            {
                ortx = new Vector2((fingerPositons[i].x + fingerPositons[i + 1].x) / 2, fingerPositons[i].y);
                fingerPositons.Insert((i + 1), ortx);
                totalWayPoint++;
            }
            if ((float)(fingerPositons[i + 1].y - fingerPositons[i].y) >= 0.70f)
            {
                orty = new Vector2(fingerPositons[i].x, (fingerPositons[i].y + fingerPositons[i + 1].y) / 2);
                fingerPositons.Insert((i + 1), orty);
                totalWayPoint++;
            }
        }
    }

    void moveArrow()
    {
        arrow.GetComponent<Arrow>().plannedtwo = true;
        arrow.GetComponent<Arrow>().plannedTwo(fingerPositons[currentWayPoint]);
        currentWayPoint++;

    }
    void CreateLine()
    {
        increateline = true;
        currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider2D = currentLine.GetComponent<EdgeCollider2D>();
        fingerPositons.Clear();
        fingerPositons.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        fingerPositons.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        lineRenderer.SetPosition(0, fingerPositons[0]);
        lineRenderer.SetPosition(1, fingerPositons[1]);
    }

    void UpdateLine(Vector2 newFingerPosition)
    {
        fingerPositons.Add(newFingerPosition);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newFingerPosition);
    }

    private void OnMouseOver()
    {
        canDraw = true;
    }

    private void OnMouseExit()
    {
        canDraw = false;
    }
}
