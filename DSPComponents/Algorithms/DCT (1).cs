using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DSPAlgorithms.Algorithms
{
    public class DCT : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float> output = new List<float>();






            int N = InputSignal.Samples.Count;



            double sqrt = Math.Sqrt(2f / N);
            for (int i = 0; i < N; i++)
            {
                double sum = 0;
                for (int j = 0; j < N; j++)
                {
                    sum += InputSignal.Samples[j] * Math.Cos((((2 * j) + 1) * (i * Math.PI)) / (2 * N));
                }

                if (i == 0)
                {
                    output.Add((float)(sum * Math.Sqrt(1f / N)));

                }
                else
                {
                    output.Add((float)(sum * sqrt));
                }
            }

            OutputSignal = new Signal(output, false);

            StreamWriter str = new StreamWriter("output.txt", false);
            for (int i = 0; i < 5; i++)
            {
                str.WriteLine("output:" + output[i]);
            }
            str.Close();

        }
    }
}
