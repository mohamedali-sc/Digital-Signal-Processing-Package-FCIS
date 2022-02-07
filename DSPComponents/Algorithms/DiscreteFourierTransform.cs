using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }

        public List<float> amplitude;
        public List<float> phase_shift;
        public List<float> freq;
        public override void Run()
        {



            int n = InputTimeDomainSignal.Samples.Count;
            int k = n;
            double pi = Math.PI;


            amplitude = new List<float>();
            phase_shift = new List<float>();
            freq = new List<float>();

            freq.Add(InputSamplingFrequency);
            for (int i = 1; i < n; i++)
            {
                freq.Add(2 * freq[i - 1]);
            }

            for (int i = 0; i < k; i++)
            {
                float x_r = 0;
                float x_i = 0;
                for (int j = 0; j < n; j++)
                {
                    float real = (float)Math.Cos((2 * pi * i * j) / n);
                    float img = (float)Math.Sin((2 * pi * i * j) / n);

                    x_r += InputTimeDomainSignal.Samples[j] * real;
                    x_i += -InputTimeDomainSignal.Samples[j] * img;



                }
                amplitude.Add((float)Math.Sqrt(Math.Pow(x_i, 2) + Math.Pow(x_r, 2)));
                double phase_shiftt = Math.Atan(x_i / x_r);



                if (x_r > 0)
                    phase_shift.Add((float)(phase_shiftt));
                else if (x_r < 0 && x_i >= 0)
                    phase_shift.Add((float)(phase_shiftt + pi));
                else if (x_r < 0 && x_i < 0)
                    phase_shift.Add((float)(phase_shiftt - pi));





                OutputFreqDomainSignal = new Signal(false, freq, amplitude, phase_shift);





            }
            string path = @"F:\New folder\data.txt";
            using (var sw = new StreamWriter(path))
            {
                for (int i = 0; i < n; i++)
                {

                    string data = Convert.ToString(amplitude[i]) + " " + Convert.ToString(phase_shift[i]);
                    sw.WriteLine(data, i);

                }
            }



        }
    }
}


