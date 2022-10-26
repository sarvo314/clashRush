using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();

    [SerializeField][Range(0f,5f)]float speed = 1f;
    Enemy enemy;
    void OnEnable()
    {
        RecalculatePath();
        ReturnToStart();
        StartCoroutine(FollowWayPoint());
    }
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
    void ReturnToStart()
    {
        transform.position = getStartPosition().position;
    }
    public Transform getStartPosition()
    {
        return path[0].transform;
    }

    void RecalculatePath()
    {
        path.Clear();
        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach(Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();

            if(waypoint != null)
            {
                path.Add(waypoint);
            }
        }
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowWayPoint()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;
            transform.LookAt(endPosition);

            while(travelPercent<1f)
            {
                travelPercent += Time.deltaTime*speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }
}
