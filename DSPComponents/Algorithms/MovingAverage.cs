using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MovingAverage : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int InputWindowSize { get; set; }
        public Signal OutputAverageSignal { get; set; }
 
        public override void Run()
        {
            List<int> indcies = new List<int>();
            List<float> samples = new List<float>();
            
            int y = InputWindowSize / 2; int c = 0;
            for(int i=y;i<InputSignal.Samples.Count-y;i++)
            {
                float result = 0;
                for (int k=0;k<InputWindowSize;k++)
                {
                    result = result + InputSignal.Samples[k+c];
                    

                }
                c++;
                samples.Add(result / InputWindowSize);
                indcies.Add(InputSignal.SamplesIndices[i]);    
                OutputAverageSignal = new Signal(samples,indcies, false);
            }
        }
    }
}
