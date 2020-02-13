using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lucky7
{
    public class Die
    {
        public int Value { get; set; }        
        public Point DrawPoint { get; set; }

        public Die(int v = 1) { Value = v; DrawPoint = new Point(0, 0 ); }

        public Bitmap GetImage()
        {
            string imageFolder = AppDomain.CurrentDomain.BaseDirectory + "Resources\\";
            return new Bitmap(imageFolder + "Die" + Value.ToString() + ".png");
        }
    }
}
