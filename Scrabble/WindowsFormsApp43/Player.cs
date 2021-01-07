using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp43
{
    public class Player
    {
        public const int countLetters = 7;
        public string[] PlayerLetters = new string[countLetters];
      
        public int Points = 0;
        public string Name;


        public Player( string Name, int Points, string[] PlayerLetters)
        {
            this.PlayerLetters = PlayerLetters;
            this.Points = Points;
            this.Name = Name;
        }

       

    }

}
