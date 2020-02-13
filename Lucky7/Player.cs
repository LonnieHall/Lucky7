using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lucky7
{
    public class Player
    {
        public string Name { get; set; }
        public int Coins { get; set; }
        public int BoardPosition { get; set; } // 0-19. 0 = Start.
        public Point DrawPoint { get; set; }        
        public int[] Stocks { get; set; } // 0 = doubles. 1-11 = 2-12 result
        public int AI { get; set; } // 0 = human
        public bool Bidding { get; set; }        
        public Bitmap MarkerBitmap { get; set; }
        public string Moving { get; set; }

        public Player(string name = "Incognito", int ai = 0, int markerNumber = 0)
        {
            Name = name;
            AI = ai;
            Coins = 10;
            BoardPosition = 0;
            Moving = "no";
            Bidding = false;
            Stocks = new int[12];
            for (int i = 0; i < 12; i++) { Stocks[i] = 0; }

            string imageFolder = AppDomain.CurrentDomain.BaseDirectory + "Resources\\";

            switch (markerNumber % 8)
            {
                case 0: MarkerBitmap = new Bitmap(imageFolder + @"PlayerBlue.png"); break;
                case 1: MarkerBitmap = new Bitmap(imageFolder + @"PlayerRed.png"); break;
                case 2: MarkerBitmap = new Bitmap(imageFolder + @"PlayerYellow.png"); break;
                case 3: MarkerBitmap = new Bitmap(imageFolder + @"PlayerPink.png"); break;
                case 4: MarkerBitmap = new Bitmap(imageFolder + @"PlayerGold.png"); break;                
                case 5: MarkerBitmap = new Bitmap(imageFolder + @"PlayerGrey.png"); break;
                case 6: MarkerBitmap = new Bitmap(imageFolder + @"PlayerBlack.png"); break;
                case 7: MarkerBitmap = new Bitmap(imageFolder + @"PlayerWhite.png"); break;
            }
        }

        private int DrawPointToPosition(Point point)
        {
            return -1;
        }

        public Point DestinationPositionToPoint(int playerIndex, int position = -1)
        {            
            int sw = 83;
            int x = -1;
            int y = -1;
            int xOffset = (playerIndex % 4) * 16;
            int yOffset = 0;
            if (playerIndex >= 4) { yOffset = 8; }
            if (position == -1) { position = BoardPosition; }

            switch (position)
            {
                case 0:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19: x = 420 + xOffset; break;
                case 1:
                case 14: x = 420 - 1 * sw + xOffset; break;
                case 2:
                case 13: x = 420 - 2 * sw + xOffset; break;
                case 3:
                case 12: x = 420 - 3 * sw + xOffset; break;
                case 4:
                case 11: x = 420 - 4 * sw + xOffset; break;
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10: x = 420 - 5 * sw + xOffset; break;

            }

            switch (position)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5: y = 460 + yOffset; break;
                case 6:
                case 19: y = 460 - 1 * sw + yOffset; break;
                case 7:
                case 18: y = 460 - 2 * sw + yOffset; break;
                case 8:
                case 17: y = 460 - 3 * sw + yOffset; break;
                case 9:
                case 16: y = 460 - 4 * sw + yOffset; break;
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15: y = 460 - 5 * sw + yOffset; break;
            }

            return new Point(x, y);
        }
    }
}
