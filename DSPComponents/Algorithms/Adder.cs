using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float> result= new List<float>();
            
            for (int i = 0; i < InputSignals[0].Samples.Count; i++) {

                float sample = 0;
                for (int j = 0; j < InputSignals.Count; j++)
                {
                    sample += InputSignals[j].Samples[i];
                    
                }
                result.Add(sample);
            }
            
            OutputSignal = new Signal(result, false);
        }
    }
}