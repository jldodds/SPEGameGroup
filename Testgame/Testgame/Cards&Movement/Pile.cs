
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Speed
{
    class Pile
    {
        Stack<Card> stack;
        public readonly Vector2 position;
        public bool drawnTo { get; set; }    // boolean for whether or not drawn to
        public bool hasPowerUp { get; set; }
        PowerUp powerUp;

        // places a stack at the position, pile isn't drawn to
        public Pile(Vector2 p)
        {
            stack = new Stack<Card>();
            position = p;
            drawnTo = false;
            hasPowerUp = false;
        }

        // adds card to top of pile
        public void Add(Card c)
        {
            stack.Push(c);
        }

        // takes card from top of pile
        public Card Take()
        {
            Card c = stack.Pop();
            return c;
        }

        // returns amount of cards in pile
        public int Count()
        {
            return stack.Count;
        }

        // returns card at top of pile
        public Card Peek()
        {
            return stack.Peek();
        }

        public void GivePowerUp(PowerUp powerup)
        {
            this.powerUp = powerup;
            hasPowerUp = true;
            this.powerUp.attributes.depth = .1f;
           
            this.powerUp.isSeeable = true;
            this.powerUp.attributes.position = this.position;
        }

        public void PlayPowerUp(Player victim)
        {
            if (!hasPowerUp) return;
            powerUp.HitPlayer(victim);
            powerUp = null;
            hasPowerUp = false;
        }

        public void RemovePowerUp()
        {
            if (!hasPowerUp) return;
            powerUp.isSeeable = false;
            powerUp.attributes.position = new Vector2(-200, -200);
            powerUp = null;
            hasPowerUp = false;
        }

    }
}
