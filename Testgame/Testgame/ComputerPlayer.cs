using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Testgame
{
    class ComputerPlayer : Player
    {
        public float timeDelay { get; set; }
        Timer moveDelay;
        int pileNumber;
        bool isLeftPile;
        CompState myState;
        Random random;

        // constructor, sets compstate to normal & establishes a delay for computer's reaction
        public ComputerPlayer(String name, bool isPlayer1) : base(name, isPlayer1)
        {
            myState = CompState.normal;
            if (isPlayer1) timeDelay = .4f;
            else timeDelay = .39f;
            random = new Random();
        }

        public enum CompState
        {
            moving,
            normal,
        }

        // computer logic to know when to play cards
        public void Move(Pile[] Hand, Pile rgamestack, Pile lgamestack)
        {
            if (myState == CompState.moving) return;

                if (ExistAMove2(Hand, rgamestack, lgamestack))
                {
                    myState = CompState.moving;
                    // moves left if that is fastest way to closest playable card
                    if (pileNumber != selector)
                    {
                        int x = pileNumber - selector;
                        if (x >= 3 || (x < 0 && x >= -2))
                        {
                            moveDelay = new Timer(1);
                            moveDelay.SetTimer(0, timeDelay + ((float)random.NextDouble() - .5f) * timeDelay, delegate()
                            {
                                if (!isPlayer1) MoveSelectorLeft();
                                else MoveSelectorRight();
                                myState = CompState.normal;
                            });
                        }
                        // moves right if otherwise
                        else if (x <= -3 || (x > 0 && x < 3))
                        {
                            moveDelay = new Timer(1);
                            moveDelay.SetTimer(0, timeDelay + ((float)random.NextDouble() - .5f) * timeDelay, delegate()
                            {
                                if (!isPlayer1) MoveSelectorRight();
                                else MoveSelectorLeft();
                                myState = CompState.normal;
                            });
                        }
                    }
                    // stays on same pile & plays it if closest card is there
                    else if (pileNumber == selector)
                    {
                        moveDelay = new Timer(1);
                        moveDelay.SetTimer(0, timeDelay + ((float)random.NextDouble() - .5f) * timeDelay, delegate()
                        { SelectCard(isLeftPile); myState = CompState.normal; });
                    }
                }
                else if (Hand[selector].Count() == 0)
                {
                    myState = CompState.moving;
                    moveDelay = new Timer(1);
                    moveDelay.SetTimer(0, timeDelay + ((float)random.NextDouble() - .5f) * timeDelay, delegate()
                    {
                        if (!isPlayer1) MoveSelectorRight();
                        else MoveSelectorLeft();
                        myState = CompState.normal;
                    });
                }
        }
        
        // determines if a move is available based on card values & dealt hand
        public bool ExistAMove(Pile[] Hand, Pile rgamestack, Pile lgamestack)
        {
            bool moves = false;
            for (int i = 0; i < Hand.Length; i++)
            {
                
                    if (Hand[i].Count() != 0)
                    {
                        int cv = Hand[i].Peek().cardValue;
                        int value1 = lgamestack.Peek().cardValue;
                        int value2 = rgamestack.Peek().cardValue;
                        if ((cv == 0 && value1 == 12) || (cv == 12 && value1 == 0) || (cv == value1 + 1 || cv == value1 - 1))
                        {
                            if (moves)
                            {
                                int olddistance = Math.Abs(pileNumber - selector);
                                if (olddistance > 2) olddistance = Math.Abs(olddistance - 5);
                                int newdistance = Math.Abs(i - selector);
                                if (newdistance > 2) newdistance = Math.Abs(newdistance - 5);
                                if (newdistance < olddistance) { isLeftPile = true; pileNumber = i; }
                            }
                            else
                            {
                                moves = true;
                                isLeftPile = true;
                                pileNumber = i;
                            }
                        }
                        if ((cv == 0 && value2 == 12) || (cv == 12 && value2 == 0) || (cv == value2 + 1 || cv == value2 - 1))
                        {
                            if (moves)
                            {
                                int olddistance = Math.Abs(pileNumber - selector);
                                if (olddistance > 2) olddistance = Math.Abs(olddistance - 5);
                                int newdistance = Math.Abs(i - selector);
                                if (newdistance > 2) newdistance = Math.Abs(newdistance - 5);
                                if (newdistance < olddistance) { isLeftPile = false; pileNumber = i; }
                            }
                            else
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

        public bool ExistAMove2(Pile[] Hand, Pile rgamestack, Pile lgamestack)
        {
            bool moves = false;
            int maxPile = -2;
            int maxFurtherMoves = -1;
            Card[] Hands = new Card[Hand.Length];
            for (int i = 0; i < Hand.Length; i++)
            {
                if (Hand[i].Count() != 0) Hands[i] = Hand[i].Peek();
            }
            Card right = rgamestack.Peek();
            Card left = lgamestack.Peek();
            for (int i = 0; i < Hands.Length; i++)
            {
                if (Hands[i] != null)
                {
                    int cv = Hands[i].cardValue;
                    int value1 = left.cardValue;
                    int value2 = right.cardValue;
                    if ((cv == 0 && value1 == 12) || (cv == 12 && value1 == 0) || (cv == value1 + 1 || cv == value1 - 1))
                    {
                        Card[] newHand = new Card[Hands.Length];
                        for (int j = 0; j < Hands.Length; j++)
                        {
                            if (j != i) newHand[j] = Hands[j];
                        }
                        int tempFurtherMoves = FurtherMoves(newHand, Hands[i], right);
                        
                        if (moves)
                        {
                            if (tempFurtherMoves > maxFurtherMoves)
                            {
                                maxFurtherMoves = tempFurtherMoves;
                                maxPile = i;
                                isLeftPile = true;
                            }
                            if (tempFurtherMoves == maxFurtherMoves)
                            {
                                int olddistance = Math.Abs(maxPile - selector);
                                if (olddistance > 2) olddistance = Math.Abs(olddistance - 5);
                                int newdistance = Math.Abs(i - selector);
                                if (newdistance > 2) newdistance = Math.Abs(newdistance - 5);
                                if (newdistance < olddistance) { isLeftPile = true; maxPile = i; }
                            }
                        }
                        else
                        {
                            moves = true;
                            isLeftPile = true;
                            maxPile = i;
                            maxFurtherMoves = tempFurtherMoves;
                        }
                        pileNumber = maxPile;
                    }
                    if ((cv == 0 && value2 == 12) || (cv == 12 && value2 == 0) || (cv == value2 + 1 || cv == value2 - 1))
                    {
                        Card[] newHand = new Card[Hands.Length];
                        for (int j = 0; j < Hands.Length; j++)
                        {
                            if (j != i) newHand[j] = Hands[j];
                        }
                        int tempFurtherMoves = FurtherMoves(newHand, left, Hands[i]);
                        if (moves)
                        {
                            if (tempFurtherMoves > maxFurtherMoves)
                            {
                                maxFurtherMoves = tempFurtherMoves;
                                maxPile = i;
                                isLeftPile = false;
                            }
                            if (tempFurtherMoves == maxFurtherMoves)
                            {
                                int olddistance = Math.Abs(maxPile - selector);
                                if (olddistance > 2) olddistance = Math.Abs(olddistance - 5);
                                int newdistance = Math.Abs(i - selector);
                                if (newdistance > 2) newdistance = Math.Abs(newdistance - 5);
                                if (newdistance < olddistance) { isLeftPile = false; maxPile = i; }
                            }
                        }
                        else
                        {
                            moves = true;
                            isLeftPile = false;
                            maxPile = i;
                            maxFurtherMoves = tempFurtherMoves;
                        }
                        pileNumber = maxPile;
                    }
                }
            }
            
            return moves;
        }

        private bool ExistMove(Card[] Hand, Card lgamecard, Card rgamecard)
        {
            bool moves = false;
            if (lgamecard == null || rgamecard == null) return false;
            for (int i = 0; i < Hand.Length; i++)
            {
                if (Hand[i] != null)
                {
                    int cv = Hand[i].cardValue;
                    int value1 = lgamecard.cardValue;
                    int value2 = rgamecard.cardValue;
                    if ((cv == 0 && value1 == 12) || (cv == 12 && value1 == 0) || (cv == value1 + 1 || cv == value1 - 1)) moves = true;
                    if ((cv == 0 && value2 == 12) || (cv == 12 && value2 == 0) || (cv == value2 + 1 || cv == value2 - 1)) moves = true;
                }
            }
            return moves;
        }

        private int FurtherMoves(Card[] Hand, Card lgamecard, Card rgamecard)
        {
            if (!ExistMove(Hand, lgamecard, rgamecard)) return 0;
            int maxFurtherMoves = 0;
            for (int i = 0; i < Hand.Length; i++)
            {
                Card[] newHand = new Card[Hand.Length - 1];
                for (int j = 0; j < newHand.Length; j++)
                {
                    if (i != j)
                    {
                        int k = 0;
                        while (newHand[k] != null)
                        {
                            k++;
                        }
                        newHand[k] = Hand[j];
                    }
                }

                int left = FurtherMoves(newHand, Hand[i], rgamecard);
                int right = FurtherMoves(newHand, lgamecard, Hand[i]);
                if (left > maxFurtherMoves) maxFurtherMoves = left;
                if (right > maxFurtherMoves) maxFurtherMoves = right;
            }
            return maxFurtherMoves + 1;
        }

        // update method
        public override void Update(Pile[] Hand, Pile rgamestack, Pile lgamestack, GameTime gameTime)
        {
            if (moveDelay != null) moveDelay.Update(gameTime);
            Move(Hand, rgamestack, lgamestack);
 	        base.Update(Hand, rgamestack, lgamestack, gameTime);
        }

        // resets state to initial normal one
        public override void Reset()
        {
            myState = CompState.normal;            
            base.Reset();
        }
    }
}
