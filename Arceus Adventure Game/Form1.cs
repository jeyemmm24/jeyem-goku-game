using Arceus_Adventure_Game.Properties;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Arceus_Adventure_Game
{
    public partial class Form1 : Form
    {
        private Player player = new Player("Trunks", 100, 10, 100, 100);
        private Enemy enemy = new Enemy("Black Goku", 100, 10, 100, 100);

        public WindowsMediaPlayer backgroundPlayer = new WindowsMediaPlayer();
        public WindowsMediaPlayer soundEffectPlayer = new WindowsMediaPlayer();
        public WindowsMediaPlayer healingsound = new WindowsMediaPlayer();
        public WindowsMediaPlayer attacksound = new WindowsMediaPlayer();

        public Form1()
        {
            InitializeComponent();
            GameStart();
            UpdateUi();
            PlayBackground();
        }

        
        private void PlayBackground()
        {
            backgroundPlayer.URL = "C:\\Users\\User\\Downloads\\Arceus-Adventure-Game-master\\Arceus-Adventure-Game-master\\Arceus Adventure Game\\Resources\\ytmp3free.cc_dragon-ball-z-soundtrack-battle-theme-majin-vegeta-youtubemp3free.org.wav"; // Use relative path
            backgroundPlayer.settings.setMode("loop", true);
            backgroundPlayer.controls.play();
        }

        
        public async Task PlaySoundEffect()
        {
            soundEffectPlayer.URL = "C:\\Users\\User\\Downloads\\Arceus-Adventure-Game-master\\Arceus-Adventure-Game-master\\Arceus Adventure Game\\Resources\\ytmp3free.cc_dragon-ball-z-soundtrack-battle-theme-majin-vegeta-youtubemp3free.org.wav";
            soundEffectPlayer.controls.play();
            await Task.Delay(500); 
        }


        // Delay method
        public async Task Delay(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }

        // Update the UI
        public void UpdateUi()
        {
            // Update HP and Mana labels
            PlayerHpLabel.Text = $"{player.Name}: {player.Health} HP";
            EnemyHpLabel.Text = $"{enemy.Name}: {enemy.Health} HP";
            PlayerManaLabel.Text = $"Mana: {player.Mana}";
            EnemyManaLabel.Text = $"Mana: {enemy.Mana}";

            // Update progress bars
            PlayerHpProgressBar.Value = player.Health;
            PlayerManaProgressBar.Value = player.Mana;
            EnemyHpProgressBar.Value = enemy.Health;
            EnemyManaProgressBar.Value = enemy.Mana;

            // Enable/disable buttons based on mana
            HealButton.Enabled = player.Mana > 19;
            CriticalButton.Enabled = player.Mana > 19;
        }

        // Initialize game settings
        public void GameStart()
        {
            PlayerHpProgressBar.Maximum = PlayerManaProgressBar.Maximum = 100;
            EnemyHpProgressBar.Maximum = EnemyManaProgressBar.Maximum = 100;
            UpdateUi();
        }

        // Handle game over
        private void CheckGameOver()
        {
            UpdateUi();
            if (enemy.Health <= 0)
            {
                enemy.Health = 0;
                MessageBox.Show($"{player.Name} wins!", "Game Over");
                Close();
            }
            else if (player.Health <= 0)
            {
                player.Health = 0;
                MessageBox.Show($"{enemy.Name} wins!", "Game Over");
                Close();
            }
            else if (enemy.Mana <= 0)
            {
                enemy.Mana = 0;
            }
            else if (player.Mana <= 0)
            {
                player.Mana = 0;
            }

        }

        // Handle player attack
        private async Task HandlePlayerAttack(Func<Task<bool>> attackAction)
        {
            bool attackSuccess = await attackAction();

            if (attackSuccess)
            {

                UpdateUi();
                CheckGameOver();
                EnemyPictureBox.Image = Properties.Resources._234231b2_1232_4e38_96b8_41e6fa27f18c;
                
                await Delay(500);
                EnemyPictureBox.Image = Properties.Resources.kalabanmm;

                await enemy.Attackai(player, enemy, healingsound, attacksound, PlayerPictureBox, EnemyPictureBox);

                PlayerPictureBox.Image = Properties.Resources.d3eoqga_cb33bd3d_64bf_44a5_a12c_a41c3e14216a;
                await Delay(500);



                CheckGameOver();
                UpdateUi();
            }
            else
            {

                await enemy.Attackai(player, enemy, healingsound, attacksound, PlayerPictureBox, EnemyPictureBox);
                PlayerPictureBox.Image = Properties.Resources._56ef50b7_3ad3_4956_8064_ac4fa96872c2;
                await Delay(500);
                PlayerPictureBox.Image = Properties.Resources.d3eoqga_cb33bd3d_64bf_44a5_a12c_a41c3e14216a;
                UpdateUi();
                CheckGameOver();
            }
            UpdateUi();
            CheckGameOver();
        }

        
        private async void AttackButton_Click(object sender, EventArgs e)
        {

            await HandlePlayerAttack(() => player.AttackEnemyAsync(enemy, soundEffectPlayer));
            UpdateUi();
            CheckGameOver();
        }

       
        private async void CriticalButton_Click(object sender, EventArgs e)
        {
            await HandlePlayerAttack(() => player.CriticalAttackAsync(enemy));
            UpdateUi();
            CheckGameOver();
        }

        
        private async void HealButton_Click(object sender, EventArgs e)
        {
            if (player.HpRecovery(healingsound))
            {
                CheckGameOver();
                UpdateUi();
                

                await enemy.Attackai(player, enemy, healingsound, soundEffectPlayer, PlayerPictureBox, EnemyPictureBox);

                PlayerPictureBox.Image = Properties.Resources.d3eoqga_cb33bd3d_64bf_44a5_a12c_a41c3e14216a;
                UpdateUi();


            }
            UpdateUi();
            CheckGameOver();
        }

        
        private async void BlockButton_Click(object sender, EventArgs e)
        {
            if (player.Block(enemy))
            {
                CheckGameOver();
                UpdateUi();
                PlayerPictureBox.Image = Properties.Resources._56ef50b7_3ad3_4956_8064_ac4fa96872c2;
                await Delay(500);
                PlayerPictureBox.Image = Properties.Resources.d3eoqga_cb33bd3d_64bf_44a5_a12c_a41c3e14216a;


                UpdateUi();
            }
            UpdateUi();
            CheckGameOver();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}