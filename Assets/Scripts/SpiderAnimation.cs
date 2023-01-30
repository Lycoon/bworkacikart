using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimation : MonoBehaviour
{
    public GameObject fakeTargetPrefab;
    public List<IKLeg> legControllers;
    public Transform body;

    private bool isDead = false;
    private List<Transform> fakeTargets = new List<Transform>();

    private int nbLegs;
    private static double[][] A, b;

    private Vector3 up;
    private Vector3 right;
    private Vector3 forward;

    void Start()
    {
        GetComponentsInChildren(legControllers);

        nbLegs = legControllers.Count;
        A = Matrix.MatrixCreate(nbLegs, 3); // N x 3
        b = Matrix.MatrixCreate(nbLegs, 1); // N x 1
    }

    void PlayDeadAnimation()
    {
        foreach (IKLeg leg in legControllers)
        {
            Vector3 targetDirection = leg.WorldTargetPosition - leg.transform.position;
            targetDirection.y = 0;

            Transform fakeTarget = Instantiate(fakeTargetPrefab, leg.target.position, Quaternion.identity).transform;
            fakeTarget.GetComponent<Rigidbody>().AddForce(transform.up * 3 + targetDirection, ForceMode.Impulse);
            fakeTargets.Add(fakeTarget);
        }
    }

    void Update()
    {
        for (int i = 0; i < nbLegs; i++)
        {
            IKLeg leg = legControllers[i];

            Vector3 targetDirection = leg.transform.parent.position - leg.WorldTargetPosition;

            A[i][0] = targetDirection.x;
            A[i][1] = targetDirection.z;
            A[i][2] = 1;

            b[i][0] = targetDirection.y;
        }

        // (A.T * A).I * A.T * b
        double[][] A_T = Matrix.MatrixTranspose(A); // 3 x N
        double[][] A_T_A_I = Matrix.MatrixInverse(Matrix.MatrixProduct(A_T, A)); // 3 x 3
        double[][] S = Matrix.MatrixProduct(Matrix.MatrixProduct(A_T_A_I, A_T), b); // 3 x 1

        // S = [x, z, y]
        // ax + bz = y
        up = new Vector3(-(float)S[0][0], (float)S[2][0], -(float)S[1][0]).normalized;

        right = Vector3.Cross(up.normalized, transform.parent.forward).normalized;
        forward = Vector3.Cross(right, up).normalized;

        body.transform.LookAt(transform.position + forward, up);

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!isDead)
            {
                PlayDeadAnimation();
                isDead = true;
            }
            else
            {
                fakeTargets.Clear();
                isDead = false;
            }
        }

        if (!isDead)
        {
            foreach (IKLeg leg in legControllers)
                leg.UpdateTarget();
        }
        else
        {
            for (int i = 0; i < nbLegs; i++)
            {
                IKLeg leg = legControllers[i];
                Transform fakeTarget = fakeTargets[i];
                if (fakeTarget != null)
                    leg.LegIK.Solve(fakeTarget.position);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, right);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, up);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, forward);
    }
}
