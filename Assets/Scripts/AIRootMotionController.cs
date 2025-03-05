using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AIRootMotionController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private float maxTarget;

    #region First Implementation
    //private void OnValidate()
    //{
    //    if (!agent) agent = GetComponent<NavMeshAgent>();
    //    if (!animator) animator = GetComponent<Animator>();
    //}
    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        var isHit = Physics.Raycast(ray, out RaycastHit hit, maxTarget);
    //        if (isHit)
    //        {
    //            agent.destination = hit.point;
    //        }
    //    }
    //}
    //private void OnDrawGizmos()
    //{
    //    if (agent.hasPath)
    //    {
    //        for (int i = 0; i < agent.path.corners.Length - 1; i++)
    //        {
    //            Debug.DrawLine(agent.path.corners[i], agent.path.corners[i + 1], Color.blue);
    //        }
    //    }
    //}
    #endregion

    #region Second Implementation

    //private void Update()
    //{
    //    if (agent.hasPath)
    //    {
    //        var dir = (agent.steeringTarget - transform.position).normalized;
    //        var aniDir = transform.InverseTransformDirection(dir);

    //        animator.SetFloat("Horizontal", aniDir.x);
    //        animator.SetFloat("Vertical", aniDir.z);
    //    }
    //    else
    //    {
    //        animator.SetFloat("Horizontal", 0);
    //        animator.SetFloat("Vertical", 0);
    //    }

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        var isHit = Physics.Raycast(ray, out RaycastHit hit, maxTarget);
    //        if (isHit)
    //        {
    //            agent.destination = hit.point;
    //        }
    //    }
    //}
    #endregion

    #region Third Implementation
    private void Update()
    {
        if (agent.hasPath)
        {
            var dir = (agent.steeringTarget - transform.position).normalized;
            var aniDir = transform.InverseTransformDirection(dir);
            var isFacingMoveDirection = Vector3.Dot(dir, transform.forward) > 0.5f;

            animator.SetFloat("Horizontal", isFacingMoveDirection? aniDir.x : 0, 0.5f, Time.deltaTime);
            animator.SetFloat("Vertical", isFacingMoveDirection ? aniDir.z : 0, 0.5f, Time.deltaTime);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), maxTarget * Time.deltaTime);

            if (Vector3.Distance(transform.position, agent.destination) < agent.radius)
            {
                agent.ResetPath();
            }
        }
        else
        {
            animator.SetFloat("Horizontal", 0, 0.25f, Time.deltaTime);
            animator.SetFloat("Vertical", 0, 0.25f, Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var isHit = Physics.Raycast(ray, out RaycastHit hit, maxTarget);
            if (isHit)
            {
                agent.destination = hit.point;
            }
        }
    }
    #endregion
}