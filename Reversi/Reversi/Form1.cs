using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reversi
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Form Size
            this.Height = 480;
            this.Width = 530;

            Initialize();
            //RestartGame();
            
        }

        void Form1_Click(object sender, EventArgs e)
        {
            PictureBox box = (PictureBox)sender;
            int row = int.Parse(box.Name) / Constant.SIZE;
            int col = int.Parse(box.Name) % Constant.SIZE;
            if (!Resource.stop_game && Resource.available[row, col] == true)
            {
                // Put disk
                Resource.status[row, col] = (int)Resource.current_player;
                // Convert enemy's disks
                GamePlay.Reverse(row, col);
                UpdateBoard();
                CheckEndGame();
                // In case of AI
                if (!Resource.stop_game && Resource.mode != (int)Constant.MODE.PLAYERS)
                {
                    AI.MinimaxAlgo();
                    UpdateBoard();
                    CheckEndGame();
                }
            }
        }

        void Form1_MouseEnter(object sender, EventArgs e)
        {
            PictureBox box = (PictureBox)sender;
            int row = int.Parse(box.Name) / Constant.SIZE;
            int col = int.Parse(box.Name) % Constant.SIZE;
            textBox1.Text = row.ToString() + ", " + col.ToString();
            if (Resource.available[row, col] == true)
                box.Image = imgList.Images[(int)Constant.HOVER];
        }

        void Form1_MouseLeave(object sender, EventArgs e)
        {
            PictureBox box = (PictureBox)sender;
            int row = int.Parse(box.Name) / Constant.SIZE;
            int col = int.Parse(box.Name) % Constant.SIZE;
            if (Resource.available[row, col] == true)
                box.Image = imgList.Images[(int)Constant.STATUS.BLANK];
        }

        private void cmbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Resource.mode = cmbMode.SelectedIndex;

            if (Resource.mode == (int)Constant.MODE.PLAYERS)
            {
                cmbHumanColor.Enabled = false;
                lblHumanColor.Enabled = false;

            }
            else
            {
                cmbHumanColor.SelectedIndex = -1;
                cmbHumanColor.Enabled = true;
                lblHumanColor.Enabled = true;
                Resource.ab_pruning = (Resource.mode == (int)Constant.MODE.ABPRUNING);
                // Stop game and wait until player selects color
                Resource.stop_game = true;
            }
            btNew_Click(sender, e);
        }

        private void cmbHumanColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHumanColor.SelectedIndex != -1)
            {
                Resource.player.human = cmbHumanColor.SelectedIndex + 1;
                Resource.player.ai = GamePlay.EnemyOf(Resource.player.human);
                cmbHumanColor.Enabled = false;
                lblHumanColor.Enabled = false;
                Resource.stop_game = false;
                // AI runs if it's player 1:
                if (Resource.current_player == Resource.player.ai)
                {
                    AI.MinimaxAlgo();
                    UpdateBoard();
                    CheckEndGame();
                }
            }
        }

        private void btNew_Click(object sender, EventArgs e)
        {
            ClearBoard();
            RestartGame();
        }

        private void Initialize()
        {
            
            for (int i = 0; i < Constant.SIZE; i++)
                for (int j = 0; j < Constant.SIZE; j++)
                {
                    // Initialize Board
                    Resource.board[i, j] = new PictureBox();
                    Resource.board[i, j].Image = imgList.Images[(int)Constant.STATUS.BLANK];
                    Resource.board[i, j].Size = new System.Drawing.Size(Constant.PIC_SIZE, Constant.PIC_SIZE);
                    Resource.board[i, j].Location = new System.Drawing.Point(30 + Constant.PIC_SIZE * j, 30 + Constant.PIC_SIZE * i);
                    Resource.board[i, j].Name = (Constant.SIZE * i + j).ToString();
                    Resource.board[i, j].BorderStyle = BorderStyle.None;
                    this.Controls.Add(Resource.board[i, j]);
                    Resource.board[i, j].Enabled = true;
                    Resource.board[i, j].MouseEnter += Form1_MouseEnter;
                    Resource.board[i, j].MouseLeave += Form1_MouseLeave;
                    Resource.board[i, j].Click += Form1_Click;

                    //
                    Resource.status[i, j] = (int)Constant.STATUS.BLANK;
                }
            cmbMode.SelectedIndex = (int)Constant.MODE.PLAYERS;
        }

        private bool SwitchPlayerAndCheck()
        {
            SwitchPlayer();
            GamePlay.UpdateAvailableBoxes();
            return GamePlay.CheckAvailableBoxes();
        }

        private void CheckEndGame()
        {
            /* Change to 2nd player
                   Update Available Boxes for the 2nd player
                   Check if the 2nd player can put new disk */
            bool no_more_disk_1 = SwitchPlayerAndCheck();
            // If not
            if (no_more_disk_1)
            {
                /* Switch back to 1st player
                   Update Available Boxes again
                   Check if the 1st player can put new disk this time */
                bool no_more_disk_2 = SwitchPlayerAndCheck();
                // If not, end game
                if (no_more_disk_2)
                {
                    Resource.stop_game = true;
                    MessageBox.Show("End game");
                }
                // In yes, either wait until that player continue to click, or AI will continue automatically
                else if (Resource.mode != (int)Constant.MODE.PLAYERS)
                {
                    AI.MinimaxAlgo();
                    UpdateBoard();
                    CheckEndGame();
                }
            }
        }

        private void SwitchPlayer()
        {
            Resource.current_player = GamePlay.EnemyOf(Resource.current_player);
            Resource.picCurrentPlayer.Image = imgList.Images[Resource.current_player];
        }

        private void UpdateBoard()
        {
            Resource.count_ply1 = Resource.count_ply2 = 0;
            for (int i = 0; i < Constant.SIZE; i++)
                for (int j = 0; j < Constant.SIZE; j++)
                {
                    Resource.board[i, j].Image = imgList.Images[Resource.status[i, j]];
                    this.Update();
                    if (Resource.status[i, j] == (int)Constant.STATUS.PLY1)
                        Resource.count_ply1++;
                    if (Resource.status[i, j] == (int)Constant.STATUS.PLY2)
                        Resource.count_ply2++;
                }
            txtPlayer1.Text = Resource.count_ply1.ToString();
            txtPlayer2.Text = Resource.count_ply2.ToString();
        }

        private void DisplayCurrentPlayer()
        {
            Resource.picCurrentPlayer.Image = imgList.Images[Resource.current_player];
            Resource.picCurrentPlayer.Size = new System.Drawing.Size(Constant.PIC_SIZE, Constant.PIC_SIZE);
            Resource.picCurrentPlayer.Location = new System.Drawing.Point(lblCurrentPlayer.Location.X + 10, lblCurrentPlayer.Location.Y + 20);
            Resource.picCurrentPlayer.BorderStyle = BorderStyle.None;
            this.Controls.Add(Resource.picCurrentPlayer);
            Resource.picCurrentPlayer.Enabled = true;
        }

        private void ClearBoard()
        {
            for (int i = 0; i < Constant.SIZE; i++)
                for (int j = 0; j < Constant.SIZE; j++)
                    Resource.status[i, j] = (int)Constant.STATUS.BLANK;
        }

        private void RestartGame()
        {
            Resource.current_player = (int)Constant.STATUS.PLY1;
            DisplayCurrentPlayer();
            GamePlay.PutFirstDisks();
            UpdateBoard();
            GamePlay.UpdateAvailableBoxes();
            if (Resource.mode != (int)Constant.MODE.PLAYERS)
                cmbHumanColor_SelectedIndexChanged(null, null);
            else
                Resource.stop_game = false;
        }

        
    }
}
