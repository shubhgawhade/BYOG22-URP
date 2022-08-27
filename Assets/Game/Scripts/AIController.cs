using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class AIController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    [SerializeField] private Transform[] targets;
    
    private ThirdPersonCharacter tpc;
    private AICharacterControl acc;
    
    // Start is called before the first frame update
    void Start()
    {
        acc = GetComponent<AICharacterControl>();
        tpc = GetComponent<ThirdPersonCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!tpc.detected)
        {
            int randomTarget = Random.Range(0, targets.Length);

            acc.target = targets[randomTarget];
        }
        else
        {
            acc.SetTarget(player.transform);
        }
    }
    
    
}
