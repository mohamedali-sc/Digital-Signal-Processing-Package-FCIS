using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;


namespace DSPAlgorithms.Algorithms
{
    public class AccumulationSum : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float> samples = new List<float>();
            for(int j=0;j<InputSignal.Samples.Count;j++)
            {
                float sum=0;
                for(int i=0;i<=j;i++)
                {
                    sum = sum + InputSignal.Samples[i];
                }
                samples.Add(sum);
                OutputSignal = new Signal(samples, false);
            }
        }
    }
}
