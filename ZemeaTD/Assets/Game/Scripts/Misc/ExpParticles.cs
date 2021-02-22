﻿using UnityEngine;
using System.Collections;

public class ExpParticles : MonoBehaviour {
	public Transform target;

	private ParticleSystem system;

	private static ParticleSystem.Particle[] particles = new ParticleSystem.Particle[1000];

	int count;

    public float chaseTime = 0.5f;
    public float timer;

	void Start() {
		if (system == null)
			system = GetComponent<ParticleSystem>();

		if (system == null){
			this.enabled = false;
		}else{
			system.Play();
		}
	}
	void Update(){
		
        timer += Time.deltaTime;
        if (timer >= chaseTime)
        {
            count = system.GetParticles(particles);

            for (int i = 0; i < count; i++)
            {
                ParticleSystem.Particle particle = particles[i];

                Vector3 v1 = system.transform.TransformPoint(particle.position);
                Vector3 v2 = target.transform.position;
                Vector3 tarPosi = (v2 - v1) *  (particle.remainingLifetime / particle.startLifetime);
                particle.position = system.transform.InverseTransformPoint(v2 - tarPosi);
                particles[i] = particle;
            }

            system.SetParticles(particles, count);
        }


	}
}