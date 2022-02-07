using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Folder : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputFoldedSignal { get; set; }

        public override void Run()
        {
            List<int> indcies = new List<int>();
            List<float> samples = new List<float>();

            int t = InputSignal.Samples.Count - 1;

            for (int i =0;i<InputSignal.Samples.Count;i++)
            {

                samples.Add(InputSignal.Samples[t]);
                indcies.Add(InputSignal.SamplesIndices[i]);
                t--;

            }
            OutputFoldedSignal = new Signal(samples,indcies, false);
          
        }
    }
}
