using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{

    public LineRenderer line; //to hold the line Renderer
    public Transform target; //to hold the transform of the target
    public NavMeshAgent agent; //to hold the agent of this gameObject
    public Vector3 navlineOffset = new Vector3(0f, 1f, 0f);
    public GameObject bruh;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetPath();
    }

    public void GetPath()
    {
        if (target.name == "class_12" || target.name == "class_13" || target.name == "class_18")
        {
            bruh.gameObject.SetActive(true);
        }
        else
        {
            bruh.gameObject.SetActive(false);
        }
        
        line.SetPosition(0, transform.position - navlineOffset); //set the line's origin

        agent.SetDestination(target.position); //create the path
        //yield WaitForEndOfFrame(); //wait for the path to generate

        DrawPath(agent.path);

        agent.isStopped = true; //add this if you don't want to move the agent
    }

    public void DrawPath(NavMeshPath path)
    {
        if(path.corners.Length < 2) //if the path has 1 or no corners, there is no need
            return;

        line.positionCount = path.corners.Length; //set the array of positions to the amount of corners

        for(int i = 1; i < path.corners.Length; i++)
            line.SetPosition(i, path.corners[i]); //go through each corner and set that to the line renderer's position*
    }
}