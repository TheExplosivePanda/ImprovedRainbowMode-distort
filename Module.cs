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
using RainbowModePlus;


namespace RainbowModePlus
{
    public class Module : ETGModule
    {
        public static readonly string MOD_NAME = "IMPROVED RAINBOW MODE +";
        public static readonly string VERSION = "1.1.0";
        public static readonly string TEXT_COLOR = "#00FFFF";

        public override void Start()
        {
            Hook hook = new Hook(
               typeof(GameStatsManager).GetProperty("IsRainbowRun", BindingFlags.Public | BindingFlags.Instance).GetGetMethod(),
               typeof(Module).GetMethod("RainbowButBetter"));
            var instance = this;
           

            rainbowUpdate = ETGModMainBehaviour.Instance.gameObject.AddComponent<RainbowUpdate>();
            string color1 = "#00FFFF";
            string color2 = "#FFFF00";
            string color3 = "#FF00FF";
            ETGModConsole.Log($"<color={color1}>IMPROVED</color>" + $"<color={color2}> RAINBOW MODE +</color>" + $"<color={color3}> v1.1.0</color>");

        }
        public static RainbowUpdate rainbowUpdate; 
        public static Material RainbowMat = new Material(ShaderCache.Acquire("Brave/Internal/RainbowChestShader"));
        public static Material GonnerMat = new Material((PickupObjectDatabase.GetById(602) as Gun).sprite.renderer.material.shader);
        public static Material line = new Material(ShaderCache.Acquire("Brave/Internal/DistortionLine"));
        public static Material radius = new Material(ShaderCache.Acquire("Brave/Internal/DistortionRadius"));
        public static bool RainbowButBetter(Func<GameStatsManager, bool> orig, GameStatsManager self)
        {
           
            


            

            if (orig(self) && Pixelator.HasInstance)
            {
                radius.SetVector("_WaveCenter", new Vector4(0.5f, 0.5f, 0f, 0f));
                radius.SetFloat("_DistortProgress", 0.5f);
                radius.SetFloat("_Strength", 0.4f);
                radius.SetFloat("_TimePulse", 1f);
                Pixelator.Instance.RegisterAdditionalRenderPass(radius);


                line.SetVector("_WavePoint1", new Vector4(0f, 0f, 1f, 1f));
                line.SetVector("_WavePoint2", new Vector4(1f, 1f, 1f, 1f));
                line.SetFloat("_DistortProgress", 0.5f);
                Pixelator.Instance.RegisterAdditionalRenderPass(line);

                rainbowUpdate.enabled = true;

                RainbowMat.SetFloat("_AllColorsToggle", 1f);

                Pixelator.Instance.RegisterAdditionalRenderPass(RainbowMat);
                Pixelator.Instance.RegisterAdditionalRenderPass(GonnerMat);

                

                if (GameManager.Instance.PrimaryPlayer != null)
                {
                    GameManager.Instance.PrimaryPlayer.SetOverrideShader(GonnerMat.shader);
                }
                if (GameManager.Instance.SecondaryPlayer != null)
                {
                    GameManager.Instance.SecondaryPlayer.SetOverrideShader(GonnerMat.shader);
                }
            }
            else
            {
                rainbowUpdate.enabled = false;
            }
            return orig(self);
        }

        float degrees = 0f;
        float magnitude = 0.25f; //you will probably need to change this
        float degreesPerSecond = 60f; //degrees per second
        Vector2 screenCenter = new Vector2(0.5f, 0.5f);
       
       
        public static void Log(string text, string color="FFFFFF")
        {
            ETGModConsole.Log($"<color={color}>{text}</color>"  + " " + " " + " " );
        }

        public override void Exit() { }
        public override void Init() { }
    }
}
