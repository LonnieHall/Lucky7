using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lucky7
{
    public partial class BidStocks : Form
    {        
        Game game;
        Player currentBidder;
        Random rnd;
        int currentBid = 0;

        public BidStocks(Game _game)
        {
            InitializeComponent();            
            game = _game;
            rnd = game.Rnd;
            game.RollReady = false;            
            Initialize();
        }

        private void Initialize()
        {           
            currentBidder = game.Players[game.BidPlayerIndex];            
            lblPlayerName.Text = currentBidder.Name;
            if (game.CurrentStock == 0) { lblAuction.Text = "Auction: Doubles Stock"; }
            else { lblAuction.Text = "Auction: " + (game.CurrentStock + 1).ToString() + " Stock"; }
            
            lblCoins.Text = "Coins: " + currentBidder.Coins.ToString();
            lblCurrentBid.Text = "Current Bid: " + currentBid.ToString();
            nudBid.Minimum = currentBid + 1;
            nudBid.Maximum = currentBidder.Coins;
            CheckBidValue();

            if (currentBidder.AI > 0) 
            { 
                int aiBid = game.AIBid(currentBidder);
                if (aiBid > currentBid && aiBid <= currentBidder.Coins) { nudBid.Value = aiBid; Bid(); }
                else { Pass(); }                
            }
            
        }

        private void CheckBidValue()
        {
            if (nudBid.Value > currentBid && currentBidder.Coins >= nudBid.Value) { btnBid.Enabled = true; }
            else { btnBid.Enabled = false; }
        }

        private void Pass()
        {
            currentBidder.Bidding = false;
            game.MSG(currentBidder.Name + " passes.");
            if (game.NumberStillBidding() == 1 && currentBid > 0) { ResolveWinningBid(); }
            else if (game.NumberStillBidding() < 1) { NoWinningBid(); }
            else { nextBidder(); }
        }

        private void Bid()
        {
            currentBid = Convert.ToInt32(nudBid.Value);
            game.MSG(currentBidder.Name + " bids: " + currentBid.ToString());
            if (game.NumberStillBidding() == 1 && currentBid > 0) { ResolveWinningBid(); }
            else { nextBidder(); }            
        }

        private void btnPass_Click(object sender, EventArgs e) { Pass(); }
        private void btnBid_Click(object sender, EventArgs e) { Bid(); }

        private void nudBid_ValueChanged(object sender, EventArgs e) { CheckBidValue(); }

        private void nextBidder()
        {
            do
            {
                game.BidPlayerIndex++;
                if (game.BidPlayerIndex >= game.Players.Count) { game.BidPlayerIndex = 0; }
            }
            while (!game.Players[game.BidPlayerIndex].Bidding);

            Initialize();
        }

        private void ResolveWinningBid()
        {
            Player winner = FindWinner();            
            if (winner == null) { return; }
            game.MSG(winner.Name + " wins the bid for " + currentBid + " coins!");
            winner.Coins -= currentBid;
            winner.Stocks[game.CurrentStock]++;
            game.NextTurn();
            game.RollReady = true;            
            this.Close();
        }

        private void NoWinningBid()
        {
            game.MSG("No winning bid...");
            game.NextPlayer();
            game.RollReady = true;
            game.RedrawNeeded = true;
            this.Close();
        }

        private Player FindWinner()
        {
            foreach (Player p in game.Players) { if (p.Bidding) { return p; } }
            return null;
        }

        private void nudBid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (btnBid.Enabled && (e.KeyChar == 'b' || e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Space)) { Bid(); }            
            else if (e.KeyChar == 'p' || e.KeyChar == (char)Keys.Escape) { Pass(); }
        }
    }
}
