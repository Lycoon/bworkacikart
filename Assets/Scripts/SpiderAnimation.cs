using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimation : MonoBehaviour
{
    public GameObject fakeTargetPrefab;
    public List<IKLeg> legControllers;

    private bool isDead = false;
    private List<Transform> fakeTargets = new List<Transform>();

    void Start()
    {
        GetComponentsInChildren(legControllers);
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
            for (int i = 0; i < legControllers.Count; i++)
            {
                IKLeg leg = legControllers[i];
                Transform fakeTarget = fakeTargets[i];
                if (fakeTarget != null)
                    leg.LegIK.Solve(fakeTarget.position);
            }
        }
    }
}
