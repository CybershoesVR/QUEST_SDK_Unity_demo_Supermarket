using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cybershoes
{
    public class CybershoesInput
    {
        private static Vector2 cybershoesInputAxisPrevious;
        private static Quaternion hmdForwardPrevious;
        private static Queue<Quaternion> hmdFWDPreviousFrames = new Queue<Quaternion>();
        private const int pastFrameAmount = 2;

        /// <summary>
        /// Returns the Cybershoes Input rotated relative to the HMD.
        /// </summary>
        /// <param name="hmdForward">The rotation of the HMD in world space.</param>
        /// <returns></returns>
        public static Vector2 GetRotatedShoeVector(Quaternion hmdForward)
        {
            Vector2 cybershoesInputAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.Gamepad);

            if (cybershoesInputAxis.x == 0 || !Mathf.Approximately(cybershoesInputAxis.magnitude, cybershoesInputAxisPrevious.magnitude))
            {
                cybershoesInputAxisPrevious = cybershoesInputAxis;
                hmdForwardPrevious = hmdForward; //blickrichtung zum zeitpunkt des empfangs des BT pakets
                //hmdForwardPrevious = hmdFWDPreviousFrames.Peek();
                
            }
            else
            {
                //float diffSinceBTUpdate = playerForward.eulerAngles.y - hmdFWDPreviousFrames.Peek().eulerAngles.y;

                float diffSinceBTUpdate = hmdForward.eulerAngles.y - hmdForwardPrevious.eulerAngles.y;
                cybershoesInputAxis = RotateVector(cybershoesInputAxis, diffSinceBTUpdate);
            }


            //hmdFWDPreviousFrames.Enqueue(hmdForward);

            //if (hmdFWDPreviousFrames.Count >= pastFrameAmount)
            //{
            //    hmdFWDPreviousFrames.Dequeue();
            //}

            return cybershoesInputAxis;
        }

        private static Vector2 RotateVector(Vector2 vector, float _direction)
        { // direction in degree

            float theta = (_direction * Mathf.PI / 180);

            Vector2 oldVector = vector;   //we rotate an existing set of x,y and to not want to mess up input with result during caluclation step
                                          //float oldy = _primaryInputAxis.y;   //we rotate an existing set of x,y

            float cs = Mathf.Cos(theta);
            float sn = Mathf.Sin(theta);

            vector.x = oldVector.x * cs - oldVector.y * sn;
            vector.y = oldVector.x * sn + oldVector.y * cs;
            return vector;
        }
    }

}