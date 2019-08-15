using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeParticle : MonoBehaviour
{

    //List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
   // List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();
   // ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
       // ps = GetComponent<ParticleSystem>();
       // ps.trigger.SetCollider(0, GameObject.Find("Fan").GetComponent<BoxCollider>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    void OnParticleTrigger()
    {
        // get the particles which matched the trigger conditions this frame
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        // iterate through the particles which entered the trigger and make them red
        
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            //p.startColor = new Color32(255, 0, 0, 255);
            //p.velocity = new Vector3(0, 10, 0);
            p.velocity.Set(0, 10, 0);
            enter[i] = p;
        }

        // iterate through the particles which exited the trigger and make them green
        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p = exit[i];
            p.startColor = new Color32(0, 255, 0, 255);
            exit[i] = p;
        }

        // re-assign the modified particles back into the particle system
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    }
    */
}
