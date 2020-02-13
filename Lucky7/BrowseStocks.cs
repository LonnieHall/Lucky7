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
    public partial class BrowseStocks : Form
    {
        Game game;
        Player p;
        string action;        

        public BrowseStocks(Player _p, Game _game, string _action = "view")
        {
            InitializeComponent();
            p = _p;
            game = _game;
            action = _action;
            UpdateValues();
        }

        private void UpdateValues()
        {
            lblPlayerName.Text = p.Name;
            lblCoins.Text = "Coins: " + p.Coins.ToString();
            lblDoubles.Text = p.Stocks[0].ToString();
            lbl2.Text = p.Stocks[1].ToString();
            lbl3.Text = p.Stocks[2].ToString();
            lbl4.Text = p.Stocks[3].ToString();
            lbl5.Text = p.Stocks[4].ToString();
            lbl6.Text = p.Stocks[5].ToString();
            lbl7.Text = p.Stocks[6].ToString();
            lbl8.Text = p.Stocks[7].ToString();
            lbl9.Text = p.Stocks[8].ToString();
            lbl10.Text = p.Stocks[9].ToString();
            lbl11.Text = p.Stocks[10].ToString();
            lbl12.Text = p.Stocks[11].ToString();
            if (action == "give") { lblAction.Text = "Give Away a Stock";}
            else if (action == "take") { lblAction.Text = "Take Away a Stock";}
            else if (action == "lose") { lblAction.Text = "Lose a Stock"; }
            else { lblAction.Text = "Just Browsing"; }

            if (game.AP().AI > 0) 
            {
                switch (action) 
                {
                    case "lose": case "give": ClickedIndex(game.LeastValueStockIndex(p)); break;
                    case "take": ClickedIndex(game.MostValueStockIndex(p)); break;
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            int stockIndex = 0;
            if (sender == btnDoubles) { stockIndex = 0; }
            else if (sender == btn2) { stockIndex = 1; }
            else if (sender == btn3) { stockIndex = 2; }
            else if (sender == btn4) { stockIndex = 3; }
            else if (sender == btn5) { stockIndex = 4; }
            else if (sender == btn6) { stockIndex = 5; }
            else if (sender == btn7) { stockIndex = 6; }
            else if (sender == btn8) { stockIndex = 7; }
            else if (sender == btn9) { stockIndex = 8; }
            else if (sender == btn10) { stockIndex = 9; }
            else if (sender == btn11) { stockIndex = 10; }
            else if (sender == btn12) { stockIndex = 11; }
            ClickedIndex(stockIndex);
        }

        private void ClickedIndex(int stockIndex)
        {
            string stockName = game.StockNameFromStockIndex(stockIndex);
            string apName = game.AP().Name;
            string npName = game.NextPlayer().Name;
            switch (action)
            {
                case "view": break;
                case "lose": p.Stocks[stockIndex]--; game.MSG(p.Name + " loses a " + stockName + " stock."); game.NextTurn(); this.Close(); break;
                case "take": p.Stocks[stockIndex]--; game.AP().Stocks[stockIndex]++; game.MSG(apName + " takes a " + stockName + " stock from " + p.Name); game.NextTurn(); this.Close(); break;
                case "give": p.Stocks[stockIndex]--; game.NextPlayer().Stocks[stockIndex]++; game.MSG(p.Name + " gives a " + stockName + " stock to " + npName); game.NextTurn(); this.Close(); break;
                default: break;
            }      
        }
    }
}
