using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            List<float> samples = new List<float>();
            List<int> indcies = new List<int>();
            int x = (InputSignal1.Samples.Count + InputSignal2.Samples.Count) - 1;
            for (int i = 0; i < x; i++)
            {
                float sum = 0;
                for (int k = 0; k< InputSignal1.Samples.Count; k++)
                {
                    if ((i - k) >= 0 && (i - k) < InputSignal2.Samples.Count)
                    {
                        sum = sum + InputSignal1.Samples[k] * InputSignal2.Samples[i - k];
                    }
                }
                samples.Add(sum);
            }
                for (int j=InputSignal1.SamplesIndices.Min()+InputSignal2.SamplesIndices.Min();j<(x+ InputSignal1.SamplesIndices.Min() + InputSignal2.SamplesIndices.Min()); j++)
                {
                    indcies.Add(j);
                }
            OutputConvolvedSignal = new Signal(samples, indcies, false);


        }
    }
}
