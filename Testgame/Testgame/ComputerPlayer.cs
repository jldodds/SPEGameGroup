using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Testgame
{
    class ComputerPlayer : Player
    {
        float timeDelay;
        Timer moveDelay;
        int pileNumber;
        bool isLeftPile;
        CompState myState;

        public ComputerPlayer(String name, bool isPlayer1) : base(name, isPlayer1)
        {
            myState = CompState.normal;
            timeDelay = .18f;
        }

        public enum CompState
        {
            moving,
            normal,
        }

        /*public bool MoveExists(Pile[] oppCards, Pile rgamestack, Pile lgamestack)
        {
            compMoves = false;
            for (int i = 0; i < oppCards.Length; i++)
            {
                if (oppCards[i].Count() != 0)
                {
                    int cv = oppCards[i].Peek().cardValue;
                    int leftValue = lgamestack.Peek().cardValue;
                    int rightValue = rgamestack.Peek().cardValue;
                    if ((cv == 0 && leftValue == 12) || (cv == 12 && leftValue == 0) || (cv == leftValue + 1 || cv == leftValue - 1)) compMoves = true;
                    if ((cv == 0 && rightValue == 12) || (cv == 12 && rightValue == 0) || (cv == rightValue + 1 || cv == rightValue - 1)) compMoves = true;
                }
            }
            return compMoves;
        }*/

       /* public void FindPileNumber(Pile[] oppCards, Pile rgamestack, Pile lgamestack)
        {
            compMoves = false;
            pileNumber = 0;
            isLeftPile = false;
            for (int i = 0; i < oppCards.Length; i++)
            {
                if (oppCards[i].Count() != 0)
                {
                    int cv = oppCards[i].Peek().cardValue;
                    int leftValue = lgamestack.Peek().cardValue;
                    int rightValue = rgamestack.Peek().cardValue;
                    if ((cv == 0 && leftValue == 12) || (cv == 12 && leftValue == 0) || (cv == leftValue + 1 || cv == leftValue - 1))
                    {
                        compMoves = true;
                        isLeftPile = true;
                    }
                    if ((cv == 0 && rightValue == 12) || (cv == 12 && rightValue == 0) || (cv == rightValue + 1 || cv == rightValue - 1)) compMoves = true;
                    pileNumber++;
                    if (compMoves) break;
                }
            }
            pileNumber =  Math.Abs(pileNumber - selector);
        }*/

        public void BensMethod(Pile[] Hand, Pile rgamestack, Pile lgamestack)
        {
            if (myState == CompState.moving) return;
            if (ExistAMove(Hand, rgamestack, lgamestack))
            {
                myState = CompState.moving;
                if (pileNumber != selector)
                {
                    moveDelay = new Timer(1);
                    moveDelay.SetTimer(0, timeDelay, delegate()
                    {
                        MoveSelectorRight(); myState = CompState.normal;
                    });
                }
                else if (pileNumber == selector)
                {
                    moveDelay = new Timer(1);
                    moveDelay.SetTimer(0, timeDelay, delegate()
                    { SelectCard(isLeftPile); myState = CompState.normal; });
                }
            }
        }

        public bool ExistAMove(Pile[] Hand, Pile rgamestack, Pile lgamestack)
        {
            bool moves = false;
            for (int i = 0; i < Hand.Length; i++)
            {
                if (!moves)
                {
                    if (Hand[i].Count() != 0)
                    {
                        int cv = Hand[i].Peek().cardValue;
                        int value1 = lgamestack.Peek().cardValue;
                        int value2 = rgamestack.Peek().cardValue;
                        if ((cv == 0 && value1 == 12) || (cv == 12 && value1 == 0) || (cv == value1 + 1 || cv == value1 - 1))
                        {
                            moves = true;
                            isLeftPile = true;
                            pileNumber = i;
                        }
                        if ((cv == 0 && value2 == 12) || (cv == 12 && value2 == 0) || (cv == value2 + 1 || cv == value2 - 1))
                        {
                            moves = true;
                            isLeftPile = false;
                            pileNumber = i;
                        }
                    }
                }
            }
            return moves;
        }

        /*public void FindAndPlayCard(Pile[] Hand, Pile rgamestack, Pile lgamestack)
        {
            Random random = new Random();
            FindPileNumber(Hand, rgamestack, lgamestack);
            if (compMoves)
            {
                if (random.NextDouble() < .5)
                {
                    MoveSelectorLeft(pileNumber);
                    SelectCard(isLeftPile);
                }
                else 
                {
                    MoveSelectorRight(pileNumber);
                    SelectCard(isLeftPile);
                }
            }
            
        }*/

        public override void  Update(Pile[] Hand, Pile rgamestack, Pile lgamestack, GameTime gameTime)
        {

            if (moveDelay != null) moveDelay.Update(gameTime);
            BensMethod(Hand, rgamestack, lgamestack);
 	        base.Update(Hand, rgamestack, lgamestack, gameTime);
        }
    }
}
