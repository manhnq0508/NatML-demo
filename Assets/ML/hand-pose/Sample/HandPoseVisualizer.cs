/*
*   Hand Pose
*   Copyright (c) 2022 NatML Inc. All Rights Reserved.
*/

namespace NatSuite.ML.Visualizers {

    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using Vision;

    /// <summary>
    /// Hand skeleton visualizer.
    /// </summary>
    public sealed class HandPoseVisualizer : MonoBehaviour {

        #region --Client API--
        /// <summary>
        /// Render a hand.
        /// </summary>
        /// <param name="image">Image which hand is detected from.</param>
        /// <param name="hand">Hand to render.</param>
        public void ImageRender(Texture Image){
            if(Image == null){
                return;
            }
            var rawImage = GetComponent<RawImage>();
            rawImage.texture = Image;
        }
        #endregion

    }
}