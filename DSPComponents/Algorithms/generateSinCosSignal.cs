using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class generateSinCosSignal  : Algorithm
    {
        public int SignalType { get; set; }
        public float Amplitude { get; set; }
	    public float phaseshift { get; set; }
	    public float F_Anlog { get; set; }
	    public float F_Sampling { get; set; }
        public float Output { get; set; }

        public override void Run()
        {
            
        }
    }
}

