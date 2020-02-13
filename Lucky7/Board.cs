using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucky7
{
    //The Board is just a string array with the names of spaces on the board
    // Maybe shouldn't be a class?
    public class Board
    {
        public string[] Spaces { get; set; }
        public string ItemName = "Stock";

        public Board()
        {
            Spaces = new string[20];
            Spaces[0] = "Start";
            Spaces[1] = "Lose 1 " + ItemName;
            Spaces[2] = "2 " + ItemName;
            Spaces[3] = "3 " + ItemName;
            Spaces[4] = "4 " + ItemName;
            Spaces[5] = "X " + ItemName;
            Spaces[6] = "Move Forward 3";
            Spaces[7] = "5 " + ItemName;
            Spaces[8] = "6 " + ItemName;
            Spaces[9] = "Give Away a " + ItemName;
            Spaces[10] = "7 " + ItemName;
            Spaces[11] = "Take a " + ItemName;
            Spaces[12] = "8 " + ItemName;
            Spaces[13] = "9 " + ItemName;
            Spaces[14] = "Move Backward 3";
            Spaces[15] = "Doubles " + ItemName;
            Spaces[16] = "+1 Coin";
            Spaces[17] = "10 " + ItemName;
            Spaces[18] = "11 " + ItemName;
            Spaces[19] = "12 " + ItemName;            
        }
    }        
}
