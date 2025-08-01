﻿// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using Boxophobic.StyledGUI;

namespace PolyverseSkiesAsset
{
    [HelpURL("https://docs.google.com/document/d/1z7A_xKNa2mXhvTRJqyu-ZQsAtbV32tEZQbO1OmPS_-s/edit?usp=sharing")]
    [DisallowMultipleComponent]
    [ExecuteInEditMode]
    public class PolyverseSkies : StyledMonoBehaviour
    {
        [StyledBanner(0.968f, 0.572f, 0.890f, "Polyverse Skies")]
        public bool styledBanner;

        [StyledCategory("Scene", 5, 10)]
        public bool categoryScene;

        public GameObject sunDirection;
        public GameObject moonDirection;

        [StyledCategory("Time Of Day")]
        public bool categoryTime;

        [StyledMessage("Info", "The Time Of Day feature will interpolate between two Polyverse Skies materials. Please note that material properties such as textures and keywords will not be interpolated! You will need to enable the same features on both materials in order for the interpolation to work! Toggle Update Lighting to enable Unity's realtime environment lighting! ", 0, 10)]
        public bool categoryTimeMessage = true;

        public Material skyboxDay;
        public Material skyboxNight;
        [Range(0, 1)]
        public float timeOfDay = 0;

        [Space(10)]
        public bool updateLighting = false;

        [StyledSpace(5)]
        public bool styledSpace0;

        Material _skyboxMaterial;

        void Start()
        {
            if (skyboxDay != null && skyboxNight != null)
            {
                _skyboxMaterial = new Material(skyboxDay);
            }
        }

        void Update()
        {
            if (sunDirection != null)
            {
                Shader.SetGlobalVector("PolyverseSunDir", sunDirection.transform.forward);
            }

            if (moonDirection != null)
            {
                Shader.SetGlobalVector("PolyverseMoonDir", moonDirection.transform.forward);
            }

            if (skyboxDay != null && skyboxNight != null)
            {
                _skyboxMaterial.Lerp(skyboxDay, skyboxNight, timeOfDay);
                RenderSettings.skybox = _skyboxMaterial;
            }

            if (updateLighting)
            {
                DynamicGI.UpdateEnvironment();
            }
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            if (skyboxDay != null && skyboxNight != null)
            {
                _skyboxMaterial = new Material(skyboxDay);
            }
        }
#endif
    }
}
