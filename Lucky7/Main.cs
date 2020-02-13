using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Lucky7
{
    public partial class Main : Form
    {        
        Random rnd = new Random(); //This is defined as early as possible to maximize randomization
        public Game game;
        
        public Main() { InitializeComponent(); }

        public void Initialize()
        {
            dgvPlayers.DataSource = game.DTPlayerDisplay();
            dgvPlayers.AllowUserToAddRows = false;   
            txtLog.BringToFront();
            timer.Start();
            UpdateUI();
            this.Focus();
        }
        
        public void MSG(string txt) { txtLog.AppendText(txt + Environment.NewLine); }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            NewGame ng = new NewGame(this, txtLog, rnd);
            ng.Show();        
        }

        private void playersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = "";
            foreach (Player p in game.Players) 
            { 
                text += p.Name;
                if (p.AI > 0) { text += " (AI)"; }
                text += " " + Environment.NewLine;
            }
            MSG(text);            
        }

        private void Roll_Click(object sender, EventArgs e) { RollEm(); }

        private void RollEm()
        {
            txtLog.Clear();
            game.ResolveRoll();
            UpdateUI();
        }

        private void dgvPlayers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            BrowseStocks bi = new BrowseStocks(game.Players[e.RowIndex], game, "view");
            bi.Show();
        }

        private void Main_Shown(object sender, EventArgs e) { dgvPlayers.ClearSelection(); }

        public void UpdateUI()
        {   
            dgvPlayers.DataSource = game.DTPlayerDisplay();
            dgvPlayers.Update();
            dgvPlayers.Refresh();            
            pbGameBoard.Update();
            pbGameBoard.Refresh();
            btnRoll.Enabled = game.RollReady;
        }

        private void pbGameBoard_Paint(object sender, PaintEventArgs e)
        {
            if (game == null) { return; }
            
            Player p;
            for (int i = 0; i < game.Players.Count; i++ )
            {
                p = game.Players[i];                
                try { e.Graphics.DrawImage(p.MarkerBitmap, p.DrawPoint); }
                catch (Exception ex) { MSG(ex.ToString()); }
            }
            
            Die d;
            for (int i = 0; i < game.Dice.Count; i++ ) 
            {
                d = game.Dice[i];                
                try { e.Graphics.DrawImage(d.GetImage(), d.DrawPoint); }
                catch (Exception ex) { MSG(ex.ToString()); }
            }
        }

        

        private void timer_Tick(object sender, EventArgs e) 
        { 
            if (game.RedrawNeeded) 
            { 
                UpdateUI();
                game.RedrawNeeded = false;
                
                //int speed = 7 * (game.D1 + game.D2);
                int speed = 50;
                Point destination;

                Die d;
                if (game.RollAnimation > 0) 
                {
                    game.RedrawNeeded = true;
                    game.RollAnimation--;
                    for (int i = 0; i < game.Dice.Count; i++)
                    {
                        d = game.Dice[i];
                        if (game.RollAnimation == game.MaxRollAnimation - 1) { d.DrawPoint = new Point(20*i + 20, 425);}
                        if (game.RollAnimation <= 0 && i == 0) { d.Value = game.D1; }
                        else if (game.RollAnimation <= 0 && i == 1) { d.Value = game.D2; }
                        else { d.Value = game.RollDie(); }
                        d.DrawPoint = new Point(d.DrawPoint.X, d.DrawPoint.Y + rnd.Next(10) + 1);
                    }
                }
                

                Player p;
                for (int i = 0; i < game.Players.Count; i++ )
                {
                    p = game.Players[i];                    
                    if (p.Moving == "no") { continue; }
                    destination = p.DestinationPositionToPoint(i);
                    
                    game.RedrawNeeded = true;
                    
                    if (p.Moving == "left") 
                    { 
                        p.DrawPoint = new Point(p.DrawPoint.X - speed, p.DrawPoint.Y);

                        if (p.BoardPosition <= 5) //Land somewhere on this row
                        { 
                            if (p.DrawPoint.X <= p.DestinationPositionToPoint(i).X) { p.DrawPoint = p.DestinationPositionToPoint(i); } 
                        }
                        else //Can pivot on corner to next row
                        {
                            int pivotX = p.DestinationPositionToPoint(i, 5).X;
                            if (p.DrawPoint.X <= pivotX) { p.DrawPoint = new Point(pivotX, p.DrawPoint.Y); p.Moving = "up"; }
                        }
                    }
                    else if (p.Moving == "up") 
                    { 
                        p.DrawPoint = new Point(p.DrawPoint.X, p.DrawPoint.Y - speed);
                        if (p.BoardPosition <= 10 && p.BoardPosition >= 5) //Land somewhere on this row
                        {
                            if (p.DrawPoint.Y <= p.DestinationPositionToPoint(i).Y) { p.DrawPoint = p.DestinationPositionToPoint(i); }
                        }
                        else //Can pivot on corner to next row
                        {
                            int pivotY = p.DestinationPositionToPoint(i, 10).Y;
                            if (p.DrawPoint.Y <= pivotY) { p.DrawPoint = new Point(p.DrawPoint.X, pivotY); p.Moving = "right"; }
                        }
                    }
                    else if (p.Moving == "right") 
                    { 
                        p.DrawPoint = new Point(p.DrawPoint.X + speed, p.DrawPoint.Y);
                        if (p.BoardPosition <= 15 && p.BoardPosition >= 10) //Land somewhere on this row
                        {
                            if (p.DrawPoint.X >= p.DestinationPositionToPoint(i).X) { p.DrawPoint = p.DestinationPositionToPoint(i); }
                        }
                        else //Can pivot on corner to next row
                        {
                            int pivotX = p.DestinationPositionToPoint(i, 15).X;
                            if (p.DrawPoint.X >= pivotX) { p.DrawPoint = new Point(pivotX, p.DrawPoint.Y); p.Moving = "down"; }
                        }
                    }
                    else if (p.Moving == "down") 
                    { 
                        p.DrawPoint = new Point(p.DrawPoint.X, p.DrawPoint.Y + speed);
                        if (p.BoardPosition >= 15) //Land somewhere on this row
                        {
                            if (p.DrawPoint.Y >= p.DestinationPositionToPoint(i).Y) { p.DrawPoint = p.DestinationPositionToPoint(i); }
                        }
                        else //Can pivot on corner to next row
                        {
                            int pivotY = p.DestinationPositionToPoint(i, 0).Y;
                            if (p.DrawPoint.Y >= pivotY) { p.DrawPoint = new Point(p.DrawPoint.X, pivotY); p.Moving = "left"; }
                        }
                    }

                    if (p.DrawPoint == destination) { p.Moving = "no"; }                    
                }                
            }
        }

        private void Main_KeyPress(object sender, KeyPressEventArgs e) 
        { 
            if (game == null) { return; } 
            if (game.RollReady && (e.KeyChar == 'r' || e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Space)) { RollEm(); } 
        }
    }
}

/**************** Scrap Heap *****************************************************************************
 * //public int yOffset = 0, xOffset = 0;
        //public void MSG(string txt) { MessageBox.Show(txt, "Lucky 7"); }
 * 
 * 
           //xOffset = pbGameBoard.Location.X;
            //yOffset = pbGameBoard.Location.Y;
            //Players.Add(new Player("Geordi", 0));
            //for (int i = 1; i <= 6; i++) { Players.Add(new Player("Bot " + i.ToString(), i, i)); }

 * //dgvPlayers.DataSource = Players;
 * //if (Players.Count < 7) { dgvPlayers.Height = 25 * (Players.Count + 1); }
 * if (roll < 2 || roll > 12) { return; } //Roll must be 2-12
 * 
 * //int px = xForPlayer(i);
                //int py = yForPlayer(i);
                //MSG("Player Index: " + i + " = " + px.ToString() + ", " + py.ToString());
                //try { e.Graphics.DrawImage(game.Players[i].MarkerBitmap, px, py); }
 * 
 * private int xForPlayer(int playerIndex)
        {
            int sw = 83;
            int position = game.Players[playerIndex].BoardPosition;

            switch (position)
            {
                case 0:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19: return 420 + playerIndex * 16;
                case 1:
                case 14: return 420 - 1 * sw + playerIndex * 16;
                case 2:
                case 13: return 420 - 2 * sw + playerIndex * 16;
                case 3:
                case 12: return 420 - 3 * sw + playerIndex * 16;
                case 4:
                case 11: return 420 - 4 * sw + playerIndex * 16;
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10: return 420 - 5 * sw + playerIndex * 16;

            }
            return 0;
        }

        private int yForPlayer(int playerIndex)
        {

            int sw = 83;
            int position = game.Players[playerIndex].BoardPosition;

            switch (position)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5: return 460;
                case 6:
                case 19: return 460 - 1 * sw;
                case 7:
                case 18: return 460 - 2 * sw;
                case 8:
                case 17: return 460 - 3 * sw;
                case 9:
                case 16: return 460 - 4 * sw;
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15: return 460 - 5 * sw;
            }
            return 0;
        }
*************************************************************************************************************/