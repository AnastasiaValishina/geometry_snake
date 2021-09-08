using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] public int width;
    [SerializeField] public int height;
    [SerializeField] private int startSize;
    [SerializeField] private ColorType[] colors;
    [SerializeField] private float stepDuration;
    [SerializeField] private float newJointDelay;
    [SerializeField] private Joint jointPrefab;

    private List<Joint> joints = new List<Joint>();
    private float nextStep;
    private float nextSpawn;
    private bool isSet = false;
    private bool isStarted = false;

    static Board instance;
    public static Board Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Board>();
            }
            return instance;
        }
    }

    void Start()
    {
        CreateStartingSnake();
        ColorStartingSnake();
        nextSpawn = Time.time + newJointDelay*2;
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
        if (isStarted && isSet && Time.time > nextSpawn)
        {
            ContinueSnake();
            nextSpawn = Time.time + newJointDelay;
        }
    }
    private void CreateStartingSnake()
    {
        if (width < startSize)
        {
            int startX = 2 * width - startSize;
            for (int x = startX; x < width; x++)
            {
                CreateJointAt(x, 1);
            }
            for (int x = width - 1; x >= 0; x--)
            {
                CreateJointAt(x, 0);
            }
        }
        else if (width >= startSize)
        {
            for (int x = startSize - 1; x >= 0; x--)
            {
                CreateJointAt(x, 0);
            }
        }
        isSet = true;
    }

    void ColorStartingSnake()
    {
        int jointsInGroup = joints.Count / colors.Length;
        int startingJoint = 0;
        for (int color = 0; color < colors.Length; color++)
        {
            for (int i = 0; i < jointsInGroup; i++)
            {
                joints[startingJoint].SetColor(colors[color]);
                startingJoint++;
            }
        }
        if (startingJoint <= joints.Count)
        {
            joints[startingJoint].SetColor(colors[colors.Length - 1]);
        }
    }

    void ContinueSnake()
    {
        Vector2 pos = joints[joints.Count - 1].transform.position;
        Joint joint = CreateJointAt((int)pos.x, (int)pos.y);
        SetPosition(joint);
        int randomColor = Random.Range(0, colors.Length);
        joint.SetColor(colors[randomColor]);
    }

    void SetPosition(Joint joint)
    {
        Vector2 prevJoint = joints[joints.Count - 1].transform.position;
        Vector2 prevPrevJoint = joints[joints.Count - 2].transform.position;
        int x = 0;
        int y = 0;
        bool goLeft = prevJoint.y % 2 == 0;
        if (goLeft)
        {
            x = (int)prevJoint.x - 1;
        } 
        else if (!goLeft)
        {
            x = (int)prevJoint.x + 1;
        }
        else if (prevJoint.x == width - 1 && prevPrevJoint.x == width - 1)
        {
            x = (int)prevJoint.x - 1;
            y = (int)prevJoint.y;            
        }
        else if (prevJoint.x == 0 && prevPrevJoint.x == 0)
        {
            x = (int)prevJoint.x + 1;
            y = (int)prevJoint.y;            
        }
        if (prevJoint.y == prevPrevJoint.y)
        {
            y = (int)prevJoint.y;
        } 
        joint.transform.position = new Vector2(x, y);
    }

    private Joint CreateJointAt(int x, int y)
    {
        Vector2 pos = new Vector2(x, y);
        Joint joint = Instantiate(jointPrefab, pos, Quaternion.identity);
        joints.Add(joint);
        return joint;
    }

    private void MoveSnake()
    {
        foreach (Joint j in joints)
        {
            bool goLeft = j.transform.position.y % 2 == 0;

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
                if (j.transform.position.y <= height - 1)
                {
                    j.transform.position = new Vector2(j.transform.position.x, j.transform.position.y + 1);
                }

            }
        }
    }
}
