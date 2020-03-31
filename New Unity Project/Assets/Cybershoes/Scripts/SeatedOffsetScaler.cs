using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cybershoes
{
    public class SeatedOffsetScaler : MonoBehaviour
    {
        private bool initialized = false;
        private bool autoScaling = false;

        private Transform hmdTransform;
        private Transform offsetTransform;
        private float standingHeight;
        private float maxOffset;
        private float duckMax;
        private float duckMin;

        //INIT FUNCTIONS
        #region

        /// <summary>
        /// Calculates the scaling data to offset the player to the standing height from their quest profile.
        /// </summary>
        /// <param name="hmd">The Transform of the hmd/camera object.</param>
        public void InitSeatedOffset(Transform hmd)
        {
            InitSeatedOffset(hmd, OVRManager.profile.eyeHeight);
        }

        /// <summary>
        /// Calculates the scaling data to offset the player to the standing height from their quest profile.
        /// </summary>
        /// <param name="hmd">The Transform of the hmd/camera object.</param>
        /// <param name="offsetObject">The Transform that the offset should be applied to.</param>
        /// <param name="enableOnInit">If true scaling will automatically start to be applied.</param>
        public void InitSeatedOffset(Transform hmd, Transform offsetObject, bool enableOnInit)
        {
            InitSeatedOffset(hmd, offsetObject, OVRManager.profile.eyeHeight, enableOnInit);
        }

        /// <summary>
        /// Calculates the scaling data to offset the player to the desired height of the character.
        /// </summary>
        /// <param name="hmd">The Transform of the hmd/camera object.</param>
        /// <param name="targetCharacterHeight">The target height of the character.</param>
        public void InitSeatedOffset(Transform hmd, float targetCharacterHeight)
        {
            hmdTransform = hmd;

            standingHeight = targetCharacterHeight;
            maxOffset = targetCharacterHeight - hmdTransform.localPosition.y;
            duckMax = hmdTransform.localPosition.y - 0.05f;
            duckMin = hmdTransform.localPosition.y - 0.40f;
            initialized = true;
        }

        /// <summary>
        /// Calculates the scaling data to offset the player to the desired height of the character.
        /// </summary>
        /// <param name="hmd">The Transform of the hmd/camera object.</param>
        /// <param name="offsetObject">The Transform that the offset should be applied to.</param>
        /// <param name="targetCharacterHeight">The target height of the character.</param>
        /// <param name="enableOnInit">If true scaling will automatically start to be applied.</param>
        public void InitSeatedOffset(Transform hmd, Transform offsetObject, float targetCharacterHeight, bool enableOnInit)
        {
            offsetTransform = offsetObject;

            InitSeatedOffset(hmd, targetCharacterHeight);

            if (enableOnInit)
            {
                autoScaling = true;
            }
        }

        #endregion

        /// <summary>
        /// When active, The seated offset will update your specified HMD Transform every Frame.
        /// </summary>
        /// <param name="active"></param>
        public void SetAutoScaling(bool active)
        {
            autoScaling = active;

            if (autoScaling && !initialized)
            {
                Debug.LogError("AutoScaling can't be enabled because the Seated Offset has not been initialized! Use InitSeatedOffset()!");
            }
            else if (autoScaling && offsetTransform == null)
            {
                Debug.LogError("AutoScaling can't be enabled because no offset Object has been assigned! Use an appropriate overload of InitSeatedOffset()!");
            }
        }

        /// <summary>
        /// Get the appropriate offset for the current height of the player.
        /// </summary>
        /// <returns></returns>
        public float CalculateOffset()
        {
            if (hmdTransform == null)
            {
                Debug.LogError("Cannot calculate the Seated Offset becasue it has not been initialized!  Use InitSeatedOffset()!");
                return 0;
            }
            else if (hmdTransform.localPosition.y < duckMin)
            {
                return 0.0f;
            }
            else if (hmdTransform.localPosition.y < duckMax)
            {
                return MapClamped(hmdTransform.localPosition.y, duckMin, duckMax, duckMin, duckMax + maxOffset) - hmdTransform.localPosition.y;
            }
            else
            {
                return maxOffset;
            }
        }

        private void Update()
        {
            if (initialized && autoScaling)
            {
                Vector3 newHeightPos = offsetTransform.localPosition;
                newHeightPos.y = CalculateOffset();

                offsetTransform.localPosition = newHeightPos;
            }
        }

        private static float MapClamped(float input, float input_start, float input_end, float output_start, float output_end)
        {
            float output = Map(input, input_start, input_end, output_start, output_end);
            output = Mathf.Max(output, output_start);
            output = Mathf.Min(output, output_end);
            return output;
        }

        private static float Map(float input, float input_start, float input_end, float output_start, float output_end)
        {
            return output_start + ((output_end - output_start) / (input_end - input_start)) * (input - input_start);
        }
    }
}