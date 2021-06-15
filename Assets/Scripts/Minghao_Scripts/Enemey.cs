using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemey : MonoBehaviour
{
    private Animator Anim;
    private NavMeshAgent Agent;
    float Magnitude;

   private Transform Destination;

    public Transform Destination1 { get => Destination; set => Destination = value; }

    void Start()
    {
        Anim = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Destination1!=null)
        {
            Agent.destination = Destination1.position;
        }
         
        
        
        
        Magnitude = Agent.velocity.magnitude;
        Anim.SetFloat("Magnitude", Magnitude);
    }
}
