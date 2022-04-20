
namespace NatSuite.ML.Visualizers {
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using Vision;


    public class Visualtion : MonoBehaviour
    {
        // Start is called before the first frame update

        public void HandRender(HandPosePredictor.Hand hand){
            foreach (var t in HandTransform){
                GameObject.Destroy(t.gameObject);
            }
            HandTransform.Clear();
            if (hand == default)
                return;
            // Create anchors
            foreach (var p in hand) {
                Vector3 temmPos = new Vector3(p.x, p.y,1);
                Vector3 offset = new Vector3(200,200,0);
                temmPos += offset;
                var anchor = Instantiate(anchorPrefab, temmPos, Quaternion.identity, transform);
                anchor.gameObject.SetActive(true);
                HandTransform.Add(anchor);
            }
            // Create bones
            foreach (var positions in new [] {
                new [] { hand.wrist, hand.thumbCMC, hand.thumbMCP, hand.thumbIP, hand.thumbTip },
                new [] { hand.wrist, hand.indexMCP, hand.indexPIP, hand.indexDIP, hand.indexTip },
                new [] { hand.middleMCP, hand.middlePIP, hand.middleDIP, hand.middleTip },
                new [] { hand.ringMCP, hand.ringPIP, hand.ringDIP, hand.ringTip },
                new [] { hand.wrist, hand.pinkyMCP, hand.pinkyPIP, hand.pinkyDIP, hand.pinkyTip },
                new [] { hand.indexMCP, hand.middleMCP, hand.ringMCP, hand.pinkyMCP }
            }) {
                Vector3 tempPosition = new Vector3(transform.position.x,transform.position.y, transform.position.z);
                var bone = Instantiate(Bone, tempPosition, Quaternion.identity, this.gameObject.transform);
                bone.gameObject.SetActive(true);
                bone.positionCount = positions.Length;
                bone.SetPositions(positions);
                
                bone.transform.position = tempPosition;
                HandTransform.Add(bone.transform);
                Debug.Log(bone.transform.position);
            }
        }
        // Update is called once per frame
        [SerializeField] Transform anchorPrefab;
        [SerializeField] LineRenderer Bone;
        List<Transform> HandTransform = new List<Transform>();
    }
}