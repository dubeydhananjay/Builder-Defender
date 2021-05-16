﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
   [SerializeField] private Gradient gradient;
   private float secondsPerDay = 10f;
   private float dayTime;
   private float dayTimeSpeed;

   private Light2D light2D;

   private void Awake() {
       light2D = GetComponent<Light2D>();
       dayTimeSpeed = 1/secondsPerDay;

   }

   private void Update() {
       dayTime += Time.deltaTime * dayTimeSpeed;
       light2D.color = gradient.Evaluate(dayTime % 1);
   }
}
