using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carServiceApp
{
     class Work
    {
        private int mins;
        public int Hours
        {
            get => mins / 60;
            set => mins = value;
        }

        public int Mins
        {
            get => mins % 60;    
            set => mins = value; 
        }
      
        public string Services { get; set; }
        public double MaterialCost { get; set; }
    }
}
