﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PressurePlateConnection : MonoBehaviour
{

    public GameObject plate;
    public GameObject door;
    public ParticleSystem m_currentParticleEffect;

    Vector3 doorStartPos;
    Vector3 plateStartPos;
    public enum  AvailableColors { blue, cyan, green, yellow, white, red, magenta };
    public AvailableColors chosenColor;

    Color particleColor;

    // Use this for initialization
    void Start()
    {
        doorStartPos = door.transform.position;
        plateStartPos = plate.transform.position;
        m_currentParticleEffect.gameObject.transform.position = plateStartPos;
        switch (chosenColor)
        {
            case AvailableColors.blue:
                {
                    particleColor = Color.blue;
                    return;
                }
            case AvailableColors.cyan:
                {
                    particleColor = Color.cyan;
                    return;
                }
            case AvailableColors.green:
                {
                    particleColor = Color.green;
                    return;
                }
            case AvailableColors.yellow:
                {
                    particleColor = Color.yellow;
                    return;
                }
            case AvailableColors.white:
                {
                    particleColor = Color.white;
                    return;
                }
            case AvailableColors.red:
                {
                    particleColor = Color.red;
                    return;
                }
            case AvailableColors.magenta:
                {
                    particleColor = Color.magenta;
                    return;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem.Particle[] ParticleList = new ParticleSystem.Particle[m_currentParticleEffect.particleCount];
        m_currentParticleEffect.GetParticles(ParticleList);
        for (int i = 0; i < ParticleList.Length; ++i)
        {
            if(i == 1)
            {
                Debug.Log(ParticleList[i].position);
            }
            ParticleList[i].rotation = 0;
            ParticleList[i].color = particleColor;
            Vector3 tempVec = (doorStartPos - plateStartPos);
            Debug.Log(tempVec);
            if (Mathf.Abs(ParticleList[i].position.x - tempVec.x) >= 0.25f)
            {
                if (i == 1)
                {
                    Debug.Log(ParticleList[i].position);
                    Debug.Log("moving in X direction");
                }
                
                ParticleList[i].velocity = new Vector3(4 * Mathf.Sign(tempVec.x), 0, 0);
                //ParticleList[i].position = Vector3.Lerp(ParticleList[i].position, new Vector3(tempVec.x, 0, ParticleList[i].position.z), Time.deltaTime);
            }
            else if (Mathf.Abs(ParticleList[i].position.z - tempVec.z) >= 0.25f)
            {
                Debug.Log("moving in Z direction");
                ParticleList[i].velocity = new Vector3(0, 0, 4 * Mathf.Sign(tempVec.z));
                //ParticleList[i].position = Vector3.Lerp(ParticleList[i].position, new Vector3(ParticleList[i].position.x, 0, tempVec.z), Time.deltaTime);
            }
            else
            {
                ParticleList[i].lifetime = 0;
            }
            //ParticleList[i].velocity = new Vector3(tempVec.x,0,tempVec.z);
        }

        m_currentParticleEffect.SetParticles(ParticleList, m_currentParticleEffect.particleCount);

    }
}
