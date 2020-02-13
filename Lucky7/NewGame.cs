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
    public partial class NewGame : Form
    {        
        DataTable dtPlayers = new DataTable();        
        Random rnd;
        TextBox log;
        int count = 0;
        List<Player> players = new List<Player>();
        Main mn;
        
        public NewGame(Main _mn, TextBox _log, Random _rnd)
        {
            InitializeComponent();
            mn = _mn;
            log = _log;
            rnd = _rnd;            
            dtPlayers.Columns.Add("Name");
            dtPlayers.Columns.Add("AI");            
        }

        private void AddHuman_Click(object sender, EventArgs e) { AddPlayer(); }
        private void AddAI_Click(object sender, EventArgs e) { AddPlayer(rnd.Next(6) + 1); }

        private void AddPlayer(int ai = 0)
        {            
            string name = txtPlayerName.Text;
            if (name == "" && ai == 0) { name = "Player " + (CountHumans() + 1).ToString(); }
            else if (name == "" && ai > 0) { name = "Bot " + (CountBots() + 1).ToString(); }
            txtPlayerName.Text = "";
            dtPlayers.Rows.Add(name, ai);
            dgvPlayerGrid.DataSource = dtPlayers;
            //dgvPlayerGrid.Columns["AI"].Visible = false;
        }

        public void SetPlayers()
        {
            players.Clear();
            for (int i = 0; i < dtPlayers.Rows.Count; i++)
            { 
                players.Add(new Player(dtPlayers.Rows[i]["Name"].ToString(), Convert.ToInt32(dtPlayers.Rows[i]["AI"]), count++));
                players[i].DrawPoint = players[i].DestinationPositionToPoint(i);
            }

            mn.game = new Game(players, log, rnd);
            mn.Initialize();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            SetPlayers();
            this.Close();
        }

        private int CountHumans() { return CountPlayerType(); }

        private int CountBots() { return CountPlayerType(true); }

        private int CountPlayerType(bool ai = false)
        {
            int result = 0;
            for (int i = 0; i < dtPlayers.Rows.Count; i++) 
            {
                bool isai;
                if (Convert.ToInt32(dtPlayers.Rows[i]["AI"]) == 0) { isai = false; }
                else { isai = true; }
                if (ai && isai) { result++; }
                if (!ai && !isai) { result++; }
            }
            return result;
        }
    }
}
