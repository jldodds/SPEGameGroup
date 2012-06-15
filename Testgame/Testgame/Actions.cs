using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testgame
{
    class Actions
    {
        public static float LinearMove(float start, float end, float d, float t)
        {
            return (end - start) * t / d + start;
        }

        public static float QuadMove(float start, float end, float d, float t)
        {
            t /= d/2;
            if ((t) < 1) return (end - start) / 2 * t * t + start;
            return -(end - start) / 2 * ((--t) * (t - 2) - 1) + start;
        }  
        
        public static MoveDel GetBackMove(float s)
        {
            return delegate(float start, float end, float d, float t)
            {
                t = t / d - 1;
                return (end - start) * ((t - 1) * t * ((s + 1) * t + s) + 1) + start;
            };
        }

        public static float ExpoMove(float start, float end, float d, float t)
        {
            return (t == d) ? start + (end - start) : (end - start) * (-(float)Math.Pow(2, -10 * t / d) + 1) + start;
        }
    }
}
