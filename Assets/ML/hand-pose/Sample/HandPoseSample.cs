/* 
*   Hand Pose
*   Copyright (c) 2022 NatML Inc. All Rights Reserved.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
namespace NatSuite.Examples {


    using UnityEngine;
    using NatSuite.ML;
    using NatSuite.ML.Features;
    using NatSuite.ML.Vision;
    using NatSuite.ML.Visualizers;
    using Stopwatch = System.Diagnostics.Stopwatch;

    public sealed class HandPoseSample : MonoBehaviour {
        
        [Header(@"NatML")]
        public string accessKey;

        [Header(@"Prediction")]
        public Texture2D image;
        public HandPoseVisualizer visualizer;
        public Visualtion Estimate;

        protected MLModelData modelData;
        protected MLModel model;
        protected HandPosePredictor predictor; // code
        public WebCamTexture webcame;    
        WebCamTexture[] devices;
        public int Granularity = 5;    
        string fileName = "text.txt";
        
        async void Start () {
        
            webcame = new WebCamTexture();
            webcame.Play();

            Debug.Log("Fetching model data from NatML...");
            // Fetch the model data from NatML
            modelData = await MLModelData.FromHub("@natsuite/hand-pose", accessKey);        
            // Deserialize the model
            model = modelData.Deserialize();
            // Create the hand pose predictor
            predictor = new HandPosePredictor(model);
            // Create input feature
            // var input = new MLImageFeature(image);
            // Predict
            // var watch = Stopwatch.StartNew();
            // var hand = predictor.Predict(input);
            // watch.Stop();
            // // Visualize
            // Debug.Log($"Detected {hand.handedness} hand with score {hand.score:0.##} after {watch.Elapsed.TotalMilliseconds}ms");
            // visualizer.Render(hand);
        }

        void Update(){
            if(predictor == null){
                return;
            }
            if(!webcame.isPlaying){
                return;
            }
            var feature = new MLImageFeature(webcame.GetPixels32(), webcame.width, webcame.height);
            var watch = Stopwatch.StartNew();
            var hand = predictor.Predict(feature);
            watch.Stop();

            if(hand != null){
                Debug.Log($"Detected {hand.handedness} hand with score {hand.score:0.##} after {watch.Elapsed.TotalMilliseconds}ms");             
            }
            var ImageTexture = feature.ToTexture();
            visualizer.ImageRender(ImageTexture);
            Estimate.HandRender(hand);

        }
    }
}