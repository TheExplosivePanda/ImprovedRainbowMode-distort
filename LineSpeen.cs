using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using MonoMod.RuntimeDetour;
using MonoMod.Utils;
using System.Reflection;
using UnityEngine.Collections;
using Gungeon;
using System.Collections;
using MonoMod;
using UnityEngineInternal;
using Mod;
namespace mod
{
    public class LineSpeen : MonoBehaviour
    {
        private void Start()
        {
           mat = base.gameObject.GetComponent<Material>();

        }
        private void Update()
        {
            degrees = degrees + (BraveTime.DeltaTime * degreesPerSecond);
            if (degrees >= 360)
            {
                degrees -= 360;
            }
            Vector2 vector = BraveMathCollege.DegreesToVector(degrees, magnitude); //creates vector for the first point
            mat.SetVector("_WavePoint1", new Vector4(screenCenter.x + vector.x, screenCenter.y + vector.y, 1f, 1f)); //Sets first point
            mat.SetVector("_WavePoint2", new Vector4(screenCenter.x - vector.x, screenCenter.y - vector.y, 1f, 1f)); //sets second point
        }
        Material mat;
        float degrees = 0f;
        float magnitude = 0.25f; //you will probably need to change this
        float degreesPerSecond = 60f; //degrees per second
        Vector2 screenCenter = new Vector2(0.5f, 0.5f);
       

    }
}
