using System;
using Microsoft.Xna.Framework;

namespace Testgame
{

    public class Tweener
    {
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

        #region Properties
        private float _position;
        public float Position
        {
            get
            {
                return _position;
            }
            protected set
            {
                _position = value;
            }
        }

        private float _from;
        protected float from
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value;
            }
        }

        private float _change;
        protected float change
        {
            get
            {
                return _change;
            }
            set
            {
                _change = value;
            }
        }

        private float _duration;
        protected float duration
        {
            get
            {
                return _duration;
            }
        }

        private float _elapsed = 0.0f;
        protected float elapsed
        {
            get
            {
                return _elapsed;
            }
            set
            {
                _elapsed = value;
            }
        }

        private bool _running = true;
        public bool Running
        {
            get { return _running; }
            protected set { _running = value; }
        }

        private MoveDel _tweeningFunction;
        protected MoveDel tweeningFunction
        {
            get
            {
                return _tweeningFunction;
            }
        }

        public delegate void EndHandler();
        public event EndHandler Ended;
        #endregion

        #region Methods
        public void Update(GameTime gameTime)
        {
            if (!Running || (elapsed == duration))
            {
                return;
            }
            Position = tweeningFunction(from, from + change, duration, elapsed);
            elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsed >= duration)
            {
                elapsed = duration;
                Position = from + change;
                OnEnd();
            }
        }

        protected void OnEnd()
        {
            if (Ended != null)
            {
                Ended();
            }
        }

        public void Start()
        {
            Running = true;
        }

        public void Stop()
        {
            Running = false;
        }

        public void Reset()
        {
            elapsed = 0.0f;
            from = Position;
        }

        public void Reset(float to)
        {
            change = to - Position;
            Reset();
        }

        public void Reverse()
        {
            elapsed = 0.0f;
            change = from - Position;
            from = Position;
        }

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
        #endregion
    }
}
