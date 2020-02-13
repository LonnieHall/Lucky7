using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Drawing;

namespace Lucky7
{
    public class Game
    {
        public List<Player> Players = new List<Player>();
        Board GBoard = new Board();
        public Random Rnd = new Random();
        int ActivePlayerIndex = 0, CurrentBid;
        public int D1, D2, BidPlayerIndex = 0, CurrentStock;
        //bool GameOver = false;
        public bool RollReady = true;
        public bool RedrawNeeded = false;
        TextBox Log;        
        public List<Die> Dice = new List<Die>();
        public int MaxRollAnimation = 5;
        public int RollAnimation = 0;

        public Game(List<Player> _players, TextBox _log, Random _rnd)
        {
            Players = _players;
            Log = _log;
            Rnd = _rnd;
            Dice.Add(new Die());
            Dice.Add(new Die());            
        }

        public int RollDie() { return Rnd.Next(6) + 1; }
        public Player AP() { return Players[ActivePlayerIndex]; }
        public void MSG(string txt) { Log.AppendText(txt + Environment.NewLine); }
        public Player NextPlayer() { return Players[NextPlayerIndex()]; }
        private void Forward3() { AP().BoardPosition += 3; GiveStock(); }
        private void Backward3() { AP().BoardPosition -= 3; TakeStock(); }
        private bool AITurn() { return AP().AI != 0; }
        private void TakeStock() { TransferStock("take"); }
        private void GiveStock() { TransferStock("give"); }
        private void LoseStock() { TransferStock("lose"); }

        public void ResolveRoll()
        {
            D1 = RollDie();
            D2 = RollDie();
            RollAnimation = MaxRollAnimation;
            int roll = D1 + D2;            
            MSG(AP().Name + " rolls: " + D1 + " + " + D2 + " => " + roll.ToString());

            PayOutForRoll();
            MovePlayer();
            PayOutForPosition();
        }        

        private void PayOut(int stockIndex)
        {
            if (stockIndex < 0 || stockIndex > 11) { return; }
            foreach (Player p in Players)
            {
                if (p.Stocks[stockIndex] > 0)
                {
                    p.Coins += p.Stocks[stockIndex];
                    MSG(p.Name + " gains +" + p.Stocks[stockIndex]);
                }
            }
        }

        private void PayOutForRoll()
        {
            if (D1 == D2) { PayOut(0); } //Doubles
            PayOut(D1 + D2 - 1);
        }

        private void PayOutForPosition()
        {            
            string spaceText = GBoard.Spaces[AP().BoardPosition];
            switch (spaceText)
            {
                case "Start": NextTurn(); break;
                case "Lose 1 Stock": LoseStock(); break;
                case "2 Stock": PayOut(1); Auction(1); break;
                case "3 Stock": PayOut(2); Auction(2); break;
                case "4 Stock": PayOut(3); Auction(3); break;
                case "X Stock": PayOut(D1 + D2 - 1); Auction(D1 + D2 - 1); break;
                case "Move Forward 3": Forward3(); break;
                case "5 Stock": PayOut(4); Auction(4); break;
                case "6 Stock": PayOut(5); Auction(5); break;
                case "Give Away a Stock": GiveStock(); break;
                case "7 Stock": PayOut(6); Auction(6); break;
                case "Take a Stock": TakeStock(); break;
                case "8 Stock": PayOut(7); Auction(7); break;
                case "9 Stock": PayOut(8); Auction(8); break;
                case "Move Backward 3": Backward3(); break;
                case "Doubles Stock": PayOut(0); Auction(0); break;
                case "+1 Coin": AP().Coins++; NextTurn(); break;
                case "10 Stock": PayOut(9); Auction(9); break;
                case "11 Stock": PayOut(10); Auction(10); break;
                case "12 Stock": PayOut(11); Auction(11); break;
            }
        }

        private void MovePlayer()
        {
            int newPosition = AP().BoardPosition + D1 + D2;
            while (newPosition > 19) { AP().Coins++; newPosition -= 20; }
            if (AP().BoardPosition < 5) { AP().Moving = "left"; }
            else if (AP().BoardPosition < 10) { AP().Moving = "up"; }
            else if (AP().BoardPosition < 15) { AP().Moving = "right"; }
            else { AP().Moving = "down"; }
            AP().BoardPosition = newPosition;            
            MSG(AP().Name + " lands on " + GBoard.Spaces[AP().BoardPosition]);
            RedrawNeeded = true;            
        }

        public void NextTurn()
        {
            if (TheWinnerIs() != null)
            {
                Player winner = TheWinnerIs();
                MSG(winner.Name + " WINS!" + Environment.NewLine + "CONGRATULATIONS!!!");
                //GameOver = true;
                RollReady = false;
                return;                
            }
            ActivePlayerIndex++;
            if (ActivePlayerIndex >= Players.Count) { ActivePlayerIndex = 0; }            
            MSG(AP().Name + "'s turn!");
            RedrawNeeded = true;            
        }

        public Player TheWinnerIs()
        {
            int minWin = 100;
            int winningAmount = 0;
            bool draw = false;

            Player p;
            Player winningPlayer = Players[0];
            int c;

            for (int i = 0; i < Players.Count; i++)
            {
                p = Players[i];
                c = p.Coins;

                if (c < minWin) { continue; }
                if (c == winningAmount) { draw = true; }
                else if (c > winningAmount) { winningAmount = c; winningPlayer = p; }
            }

            if (winningAmount > 0 && !draw) { return winningPlayer; }
            return null;
        }

        private void TransferStock(string action)
        {
            bool showWindow = !AITurn(); //Establish the bool value now, before creating the Browse Stocks window
            Player targetPlayer;
            if (action == "take") { targetPlayer = NextPlayer(); } else { targetPlayer = AP(); }
            if (!HasStock(targetPlayer)) { MSG("Nothing to " + action + "!"); NextTurn(); return; }
            BrowseStocks bs = new BrowseStocks(targetPlayer, this, action);
            bs.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            if (showWindow) { try { bs.Show(); } catch (Exception ex) { MSG("Failed to show " + action + " Stock window:" + Environment.NewLine + ex.ToString()); } }
        }

        private bool HasStock(Player p)
        {
            for (int i = 0; i < p.Stocks.Length; i++) { if (p.Stocks[i] > 0) { return true; } }
            return false;
        }

        public int NextPlayerIndex()
        {
            int index = ActivePlayerIndex + 1;
            if (index >= Players.Count) { index = 0; }
            return index;
        }

        private void Auction(int stockIndex)
        {
            foreach (Player p in Players) { p.Bidding = true; }

            CurrentBid = 0;
            BidPlayerIndex = ActivePlayerIndex;
            CurrentStock = stockIndex;

            BidStocks bs = new BidStocks(this);
            bs.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            if (!AllAI())
            {
                try { bs.Show(); }
                catch (Exception ex) { MSG("Failed to show Auction window:" + Environment.NewLine + ex.ToString()); }
            }
        }

        private bool AllAI()
        {
            foreach (Player p in Players) { if (p.AI == 0) { return false; } }
            return true;
        }

        public int AIBid(Player p)
        {
            int stockIndex = CurrentStock;
            int coins = p.Coins;
            int maxBid = 0;

            if (coins <= CurrentBid) { return -1; }

            switch (p.AI)
            {
                case 1: //Bid a static amount
                    switch (stockIndex)
                    {
                        case 0:
                        case 6: maxBid = 6; break;
                        case 5:
                        case 7: maxBid = 5; break;
                        case 4:
                        case 8: maxBid = 4; break;
                        case 3:
                        case 9: maxBid = 3; break;
                        case 2:
                        case 10: maxBid = 2; break;
                        case 1:
                        case 11: maxBid = 1; break;
                        default: maxBid = 0; break;
                    }
                    if (CurrentBid >= maxBid) { return -1; }
                    else if (coins > maxBid) { return maxBid; }
                    else { return coins; }
                case 2: return CurrentBid + 1; //Always bid 1 more if possible
                case 3: //Only bid 1
                    if (CurrentBid == 0) { return 1; }
                    else { return -1; }
                case 4: //Bid a random value
                    maxBid = RollDie();
                    if (CurrentBid >= maxBid) { return -1; }
                    else if (coins > maxBid) { return maxBid; }
                    else { return coins; }
                case 5: return -1; //Never bid
                case 6: //Only bid if wealthiest
                    foreach (Player pl in Players) { if (pl.Coins > coins) { return -1; } }
                    return CurrentBid + 1;
                default: break;
            }
            return -1;
        }

        public int NumberStillBidding()
        {
            int result = 0;
            foreach (Player p in Players) { if (p.Bidding) { result++; } }
            return result;
        }

        public int MostValueStockIndex(Player p)
        {
            if (p.Stocks[0] > 0) { return 0; }
            else if (p.Stocks[6] > 0) { return 6; }
            else if (p.Stocks[5] > 0) { return 5; }
            else if (p.Stocks[7] > 0) { return 7; }
            else if (p.Stocks[4] > 0) { return 4; }
            else if (p.Stocks[8] > 0) { return 8; }
            else if (p.Stocks[3] > 0) { return 3; }
            else if (p.Stocks[9] > 0) { return 9; }
            else if (p.Stocks[2] > 0) { return 2; }
            else if (p.Stocks[10] > 0) { return 10; }
            else if (p.Stocks[1] > 0) { return 1; }
            else if (p.Stocks[11] > 0) { return 11; }
            return -1;
        }

        public int LeastValueStockIndex(Player p)
        {
            if (p.Stocks[11] > 0) { return 11; }
            else if (p.Stocks[1] > 0) { return 1; }
            else if (p.Stocks[10] > 0) { return 10; }
            else if (p.Stocks[2] > 0) { return 2; }
            else if (p.Stocks[9] > 0) { return 9; }
            else if (p.Stocks[3] > 0) { return 3; }
            else if (p.Stocks[8] > 0) { return 8; }
            else if (p.Stocks[4] > 0) { return 4; }
            else if (p.Stocks[7] > 0) { return 7; }
            else if (p.Stocks[5] > 0) { return 5; }
            else if (p.Stocks[6] > 0) { return 6; }
            else if (p.Stocks[0] > 0) { return 0; }
            return -1;
        }

        public string StockNameFromStockIndex(int stockIndex)
        {
            switch (stockIndex)
            {
                case 0: return "Doubles";
                case 1: return "2";
                case 2: return "3";
                case 3: return "4";
                case 4: return "5";
                case 5: return "6";
                case 6: return "7";
                case 7: return "8";
                case 8: return "9";
                case 9: return "10";
                case 10: return "11";
                case 11: return "12";
                default: return "??";
            }
        }

        public DataTable DTPlayerDisplay()
        {
            DataTable dtPlayerDisplay = new DataTable();
            dtPlayerDisplay.Columns.Add("Name");
            dtPlayerDisplay.Columns.Add("Coins");
            dtPlayerDisplay.Columns.Add("Piece", typeof(Bitmap));
            dtPlayerDisplay.Columns.Add("2");
            dtPlayerDisplay.Columns.Add("3");
            dtPlayerDisplay.Columns.Add("4");
            dtPlayerDisplay.Columns.Add("5");
            dtPlayerDisplay.Columns.Add("6");
            dtPlayerDisplay.Columns.Add("7");
            dtPlayerDisplay.Columns.Add("8");
            dtPlayerDisplay.Columns.Add("9");
            dtPlayerDisplay.Columns.Add("10");
            dtPlayerDisplay.Columns.Add("11");
            dtPlayerDisplay.Columns.Add("12");
            dtPlayerDisplay.Columns.Add("2x");
            Player p;
            for (int i = 0; i < Players.Count; i++)
            {
                p = Players[i];
                dtPlayerDisplay.Rows.Add(p.Name, p.Coins, p.MarkerBitmap, p.Stocks[1], p.Stocks[2],
                    p.Stocks[3], p.Stocks[4], p.Stocks[5], p.Stocks[6], p.Stocks[7], p.Stocks[8],
                    p.Stocks[9], p.Stocks[10], p.Stocks[11], p.Stocks[0]);
            }
            return dtPlayerDisplay;
        }
    }
}
