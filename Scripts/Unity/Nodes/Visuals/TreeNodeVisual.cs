using System;
using CardGame.Core.Tree.Base;
using CardGame.Unity.Registries;
using CardGame.Unity.Utilities;
using UnityEngine;
using DG.Tweening;

namespace CardGame.Unity.Nodes.Visuals
{
    public class TreeNodeVisual : MonoBehaviour
    {
        [Header("Parent References")]
        [SerializeField] private Transform closedBranchParent;
        [SerializeField] private Transform openBranchParent;

        [Header("Visual References")]
        [SerializeField] private Light mainLight;
        [SerializeField] private MeshRenderer doorRenderer;
        [SerializeField] private MeshRenderer roadRenderer;

        [Header("Settings")]
        [SerializeField] private float transitionDuration = 0.5f;
        
        // Internal Variables
        private MaterialPropertyTransition _doorTransition;
        private MaterialPropertyTransition _roadTransition;

        private void Awake()
        {
            if (doorRenderer != null)
                _doorTransition = new MaterialPropertyTransition(doorRenderer, "_Alpha", transitionDuration);
            
            if (roadRenderer != null)
                _roadTransition = new MaterialPropertyTransition(roadRenderer, "_Alpha", transitionDuration);
        }

        public void SetActive(bool active)
        {
            closedBranchParent.gameObject.SetActive(!active);
            openBranchParent.gameObject.SetActive(active);
        }

        public void SetConfig(TreeNodeVisualConfig config)
        {
            if (mainLight != null)  
                mainLight.intensity = config.lightIntensity;

            if (_doorTransition != null)
                _doorTransition.SetValue(config.doorAlpha);

            if (_roadTransition != null)
                _roadTransition.SetValue(config.roadAlpha);
        }

        public void TransitionToConfig(TreeNodeVisualConfig config)
        {
            if (mainLight != null)
                mainLight.DOIntensity(config.lightIntensity, transitionDuration);

            if (_doorTransition != null)
                _doorTransition.TransitionToFloat(config.doorAlpha);

            if (_roadTransition != null)
                _roadTransition.TransitionToFloat(config.roadAlpha);
        }

        void OnDestroy()
        {
            TreeNodeRegistry.NodeVisualMap.TryRemove(this);
        }
    }
} 