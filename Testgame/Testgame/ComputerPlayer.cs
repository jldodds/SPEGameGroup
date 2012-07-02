using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Testgame
{
    class ComputerPlayer : Player
    {
        bool compMoves;
        Timer moveDelay;
        int pileNumber;
        bool isLeftPile;

        public ComputerPlayer(String name, bool isPlayer1) : base(name, isPlayer1)
        {
            
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

        public int FindPileNumber(Pile[] oppCards, Pile rgamestack, Pile lgamestack)
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
                    if ((cv == 0 && leftValue == 12) || (cv == 12 && leftValue == 0) || (cv == leftValue + 1 || cv == leftValue - 1)) compMoves = true;
                    if ((cv == 0 && rightValue == 12) || (cv == 12 && rightValue == 0) || (cv == rightValue + 1 || cv == rightValue - 1)) compMoves = true;
                    pileNumber++;
                    if (compMoves) break;
                }
            }
            return pileNumber;
        }

        /*public bool FindGamePile(Pile[] oppCards, Pile rgamestack, Pile lgamestack)
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
            return isLeftPile;
        }*/

        public void FindAndPlayCard()
        {
            Random random = new Random();
            if (compMoves)
            {
                if (random.NextDouble() < .5)
                {
                    MoveSelectorLeft(pileNumber);
                }
                else 
                {
                    MoveSelectorRight(pileNumber);
                }
            }
            SelectCard(isLeftPile);
        }

        public override void  Update(GameTime gameTime)
        {

            FindAndPlayCard();
 	        base.Update(gameTime);
        }
    }
}
