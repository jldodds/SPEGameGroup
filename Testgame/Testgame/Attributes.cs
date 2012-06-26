using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Testgame
{
    public class Attributes
    {
       //getters and setters for attributes of drawables
        
       public Color color{ get; set;}
       public Vector2 position { get; set; }
       public Vector2 scale { get; set; }
       public float height
       {
           get
           {
               return scale.Y * texture.Height;
           }
           set
           {
               scale = new Vector2(scale.X, value / texture.Height);
           }
       }
        public float width
        {
            get{
                return scale.X * texture.Width;
            }
            set{
                scale = new Vector2(value / texture.Width, scale.Y);
            }
        }
        public Texture2D texture { get; set; }
        public float rotation { get; set; }
       public Vector2 origin
       {
           //centers the origin
           get
           {
               return new Vector2(texture.Width / 2, texture.Height / 2);
           }
       }
       public float depth { get; set; }

       public Attributes()
       {
           //makes scalings "true" scalings
           //default scale is just image size
           scale = new Vector2(1, 1);
       }
    }


}
