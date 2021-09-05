using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private int width = 6;
    [SerializeField] private int height = 10;
    [SerializeField] private int startSize = 4;
    [SerializeField] private float stepDuration = 0.5f;
    [SerializeField] private Joint jointPrefab;

    private List<Joint> joints = new List<Joint>();
    private float nextStep;
    private bool isSet = false;
    private bool isStarted = false;

    void Start()
    {
        for (int x = startSize - 1; x >= 0; x--)
        {
            Vector2 pos = new Vector2(x, 0);
            Joint joint = Instantiate(jointPrefab, pos, Quaternion.identity);
            joints.Add(joint);
            isSet = true;
        }
    }

    void Update()
    {
        if (!isStarted && Input.GetMouseButtonDown(0))
        {
            isStarted = true;
        }
        if (isStarted && isSet && Time.time > nextStep)
        {
            MoveSnake();
            nextStep = Time.time + stepDuration;
        }
    }

    private void MoveSnake()
    {
        foreach (Joint j in joints)
        {

            bool goLeft = true;
            if (j.transform.position.y % 2 != 0)
            {
                goLeft = false;
            }
            if (j.transform.position.x < width - 1 && goLeft)
            {
                j.transform.position = new Vector2(j.transform.position.x + 1, j.transform.position.y);
            }
            else if (j.transform.position.x > 0 && !goLeft)
            {
                j.transform.position = new Vector2(j.transform.position.x - 1, j.transform.position.y);
            }
            else if (j.transform.position.x == width - 1 || j.transform.position.x == 0)
            {
                j.transform.position = new Vector2(j.transform.position.x, j.transform.position.y + 1);
            }
        }
    }
}
