using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NPCController : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    Vector2 motionVector;
    Animator animator;
    Rigidbody2D rigid;
    float horizontal = 0;
    float vertical = 0;
    bool isMoving = false;
    Vector2 movePosition;

    public NPCFindPath npcFindPath;

    float preHorizontal = 0, preVertical = 0;

    public Vector2 FbottomLeft, FtopRight;
    private Vector2 FstartPos, FtargetPos;

    private Vector2Int bottomLeft, topRight, startPos, targetPos;

    public Vector2Int LocationToIndex(Vector2 input)
    {
        int x = (int)((input.x + 12.75) / 0.5);
        int y = (int)((input.y + 10.25) / 0.5);
        return new Vector2Int(x, y);
    }

    public Vector2 IndexToLocation(Vector2Int input)
    {
        float x = (float)(input.x * 0.5 - 12.75);
        float y = (float)(input.y * 0.5 - 10.25);
        return new Vector2(x, y);
    }

    Vector2Int GetRandomPosition(Vector2Int bottomLeft, Vector2Int topRight)
    {
        int randomX = Random.Range(bottomLeft.x, topRight.x);
        int randomY = Random.Range(bottomLeft.y, topRight.y);
        return new Vector2Int(randomX, randomY);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        npcFindPath = new NPCFindPath();

        targetPos = LocationToIndex(transform.position);
        topRight = LocationToIndex(FtopRight);
        bottomLeft = LocationToIndex(FbottomLeft);

        MoveStart();
    }

    public void MoveStart()
    {
        startPos = targetPos;
        targetPos = GetRandomPosition(bottomLeft, topRight);

        while (IsObstacle(targetPos))
        {
            Debug.Log("Obstacle detected at the new targetPos. Finding a new one...");
            targetPos = GetRandomPosition(bottomLeft, topRight);
        }
        Debug.Log("start : " + startPos + ", " + "end : " + targetPos);
        npcFindPath.SetPos(bottomLeft, topRight, startPos, targetPos);
        Move();
    }

    private bool IsObstacle(Vector2Int position)
    {
        Vector2 nodePosition = IndexToLocation(position);

        // Check for obstacles around the node position
        foreach (Collider2D col in Physics2D.OverlapCircleAll(nodePosition, 0.4f))
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                return true;
            }
        }

        return false;
    }

    void Update()
    {

        /*
         * down : 0, -1
         * up : 0, 1
         * left : -1, 0
         * right : 1, 0
         */

        if (horizontal != 0 || vertical != 0)
        {
            motionVector = new Vector2(horizontal, vertical);
            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);
            animator.SetBool("Walking", true);

            preHorizontal = horizontal;
            preVertical = vertical;
        }
        else
        {
            animator.SetBool("Walking", false);
            animator.SetFloat("Horizontal", preHorizontal);
            animator.SetFloat("Vertical", preVertical);
        }

        if(isMoving)
        {
            NPCMove(movePosition);
        }

    }

    private void Move()
    {

        List<Node> path = npcFindPath.PathFinding();
        StartCoroutine(FollowPath(path));

    }

    void NPCMove(Vector2 position)
    {
        transform.Translate((position - (Vector2)transform.position).normalized *
            speed * Time.deltaTime);

        if (position == (Vector2)transform.position)
            isMoving = false;
    }

    IEnumerator FollowPath(List<Node> path)
    {
        Vector2 prePoint = IndexToLocation(startPos);
        foreach (Node waypoint in path)
        {

            Vector2 point = IndexToLocation(new Vector2Int(waypoint.x, waypoint.y));

            SetDirection(prePoint, point);

            while (Vector2.Distance(transform.position, point) > 0.1f)
            {
                isMoving = true;
                movePosition = point;
                yield return null;
            }


            //transform.position = point;
            //yield return new WaitForSeconds(1f);

            prePoint = point;
        }

        horizontal = 0;
        vertical = 0;
        yield return new WaitForSeconds(5f);

        MoveStart();
    }

    void SetDirection(Vector2 prePoint, Vector2 point)
    {
        if (point.x - prePoint.x > 0)
        {
            // right
            horizontal = 1;
            vertical = 0;

        }
        else if (point.x - prePoint.x < 0)
        {
            // left

            horizontal = -1;
            vertical = 0;

        }

        if (point.y - prePoint.y > 0)
        {
            // up
            horizontal = 0;
            vertical = 1;
        }
        else if (point.y - prePoint.y < 0)
        {
            // down
            horizontal = 0;
            vertical = -1;
        }

    }
}


