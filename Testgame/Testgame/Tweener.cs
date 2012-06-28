using System;
using Microsoft.Xna.Framework;

namespace Testgame
{

    public class Tweener
    {
        // constructor for tweener
        public Tweener(float from, float to, float duration, MoveDel tweeningFunction)
        {
            _from = from;
            _position = from;
            _change = to - from;
            _tweeningFunction = tweeningFunction;
            _duration = duration;
        }


        public Tweener(float from, float to, TimeSpan duration, MoveDel tweeningFunction)
            : this(from, to, (float)duration.TotalSeconds, tweeningFunction)
        {
        }
        
        // overloaded constructor, adds in wait time
        public Tweener(float from, float to, float waitTime, float duration, MoveDel tweeningFunction)
        {
            _from = from;
            _position = from;
            _change = to - from;
            _tweeningFunction = tweeningFunction;
            _duration = duration;
            _wait = waitTime;
        }

        
        #region Properties
        // object's position
        private float _position;
        public float Position
        {
            get {  return _position;  }
            protected set { _position = value; }
        }

        // 
        private float _from;
        protected float from
        {
            get { return _from; }
            set { _from = value; }
        }

        //
        private float _change;
        protected float change
        {
            get { return _change; }
            set { _change = value; }
        }

        //
        private float _duration;
        protected float duration
        {
            get { return _duration; }
        }

        // time elapsed since movement began
        private float _elapsed = 0.0f;
        protected float elapsed
        {
            get { return _elapsed; }
            set { _elapsed = value; }
        }

        // delay to begin movement..?
        private float _wait;
        protected float wait
        {
            get { return _wait; }
            set { _wait = value; }
        }

        // delay to being movement..?
        private float _waitTime = 0.0f;
        protected float waitTime
        {
            get { return _waitTime; }
            set { _waitTime = value; }
        }

        //
        private bool _running = true;
        public bool Running
        {
            get { return _running; }
            protected set { _running = value; }
        }

        //
        private MoveDel _tweeningFunction;
        protected MoveDel tweeningFunction
        {
            get { return _tweeningFunction; }
        }

        //
        public delegate void EndHandler();
        public event EndHandler Ended;
        #endregion

        #region Methods
        // updates object's movement and time since it began
        public void Update(GameTime gameTime)
        {
            if (waitTime < wait) Running = false;
            else Running = true;
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;
            waitTime += time;
            if (!Running || (elapsed == duration))
            {
                return;
            }
            Position = tweeningFunction(from, from + change, duration, elapsed);
            elapsed += time;
            if (elapsed >= duration)
            {
                elapsed = duration;
                Position = from + change;
                OnEnd();
            }
        }

        //
        protected void OnEnd()
        {
            if (Ended != null)
            {
                Ended();
            }
        }

        // begins object's movement
        public void Start()
        {
            Running = true;
        }

        // stops object's movement
        public void Stop()
        {
            Running = false;
        }

        // resets object's movement to starting position
        public void Reset()
        {
            elapsed = 0.0f;
            from = Position;
        }

        // resets object's movement to a given position
        public void Reset(float to)
        {
            change = to - Position;
            Reset();
        }

        // reverses object's movement
        public void Reverse()
        {
            elapsed = 0.0f;
            change = from - Position;
            from = Position;
        }

        // 
        public override string ToString()
        {
            return String.Format("{0}.{1}. Tween {2} -> {3} in {4}s. Elapsed {5:##0.##}s",
                tweeningFunction.Method.DeclaringType.Name,
                tweeningFunction.Method.Name,
                from, 
                from + change, 
                duration, 
                elapsed);
        }

        public float getElapsed()
        {
            return elapsed;
        }
        #endregion
    }
}
