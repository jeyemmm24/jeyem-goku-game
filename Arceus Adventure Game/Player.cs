using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Arceus_Adventure_Game
{
    public class Player
    {
        private Random random = new Random();

        public string Name { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Mana { get; set; }

        public Player(string name, int health, int attack, int defense, int mana)
        {
            Name = name;
            Health = health;
            Attack = attack;
            Defense = defense;
            Mana = mana;
        }


        public async Task<bool> AttackEnemyAsync(Player enemy, WindowsMediaPlayer soundEffectPlayer)
        {
            if (random.NextDouble() > 0.2) 
            {
                int damage = Attack;
                enemy.Health = Math.Max(0, enemy.Health - damage);
                Mana = Math.Min(100, Mana + 10); 

                soundEffectPlayer.controls.play(); 
                return true;
            }
            else
            {
                MessageBox.Show($"{Name}'s attack missed!");
                return false;
            }
        }


        public Task<bool> CriticalAttackAsync(Player enemy)
        {
            if (random.NextDouble() > 0.2) 
            {
                int damage = Attack * 2; 
                enemy.Health = Math.Max(0, enemy.Health - damage);
                Mana = Math.Max(0, Mana - 20); 

                
                return Task.FromResult(true);
            }
            else
            {
                MessageBox.Show($"{Name}'s critical attack missed!");
                return Task.FromResult(false);
            }
        }


        public bool HpRecovery(WindowsMediaPlayer healingSound)
        {
            Health = Math.Min(100, Health + 40); 
            Mana = Math.Max(0, Mana - 20); 

            healingSound.controls.play();
            return Health > 0;
        }


        public bool Block(Player enemy)
        {
            int reducedDamage = enemy.Attack / 2;
            Health = Math.Max(0, Health - reducedDamage);

            MessageBox.Show($"{Name} tried to block {enemy.Name}'s attack!");
            return true;
        }
    }
}