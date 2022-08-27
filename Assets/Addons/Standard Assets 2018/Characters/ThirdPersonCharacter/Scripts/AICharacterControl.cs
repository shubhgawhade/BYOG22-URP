using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        [SerializeField] private GameObject aiController;

        
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for


        private ThirdPersonCharacter tpc;
        private AIController aic;

        public int randomTarget;
        public int prevTarget = -1;

        public bool patrol;
        public bool chase;
        
        private void Start()
        {
            aic = aiController.GetComponent<AIController>(); 
            tpc = GetComponent<ThirdPersonCharacter>();
            
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
        }


        private void Update()
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
            }
            else
            {
                ChooseTarget();
            }

            if (Mathf.Abs((transform.position - target.transform.position).magnitude) > 2)
            {
                character.Move(agent.desiredVelocity, false, false);
            }
            else
            {
                character.Move(Vector3.zero, false, false);
                patrol = false;
                ChooseTarget();
            }
            
            
            
            if (!tpc.detected)
            {
                ChooseTarget();
            }
            else
            {
                patrol = false;
                SetTarget(aic.player.transform);
            }
        }

        private void ChooseTarget()
        {
            if (!patrol)
            {
                do
                {
                    randomTarget = Random.Range(0, aic.targets.Length);
                } while (randomTarget == prevTarget);

                target = aic.targets[randomTarget];
                prevTarget = randomTarget;
                patrol = true;
            }
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
