using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Sampling : Algorithm
    {
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }




        public override void Run()
        {
            List<float> output = new List<float>();
            FIR fir = new FIR();
            fir.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
            fir.InputFS = 8000;
            fir.InputStopBandAttenuation = 50;
            fir.InputCutOffFrequency = 1500;
            fir.InputTransitionBand = 500;

            // throw new NotImplementedException();
            if (M == 0 && L != 0)
            {
                L = L - 1;
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    if (i == InputSignal.Samples.Count - 1)
                    {
                        break;
                    }
                    output.Add(InputSignal.Samples[i]);
                    for (int j = 0; j < L; j++)
                    {
                        output.Add(0);

                    }
                }
                fir.InputTimeDomainSignal = new Signal(output, false);
                fir.Run();
                OutputSignal = fir.OutputYn;

            }
            else if (L == 0 && M != 0)
            {
                M = M - 1;
                fir.InputTimeDomainSignal = InputSignal;

                fir.Run();
                OutputSignal = fir.OutputYn;

                int count = M;


                for (int i = 0; i < OutputSignal.Samples.Count; i++)
                {
                    if (count == M)
                    {
                        output.Add(OutputSignal.Samples[i]);
                        count = 0;
                    }
                    else
                    {
                        count++;
                    }


                }

                OutputSignal = new Signal(output, false);

            }
            else if (M != 0 && L != 0)
            {
                L = (L - 1);
                M = (M - 1);


                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    output.Add(InputSignal.Samples[i]);
                    for (int j = 0; j < L; j++)
                    {
                        output.Add(0);
                    }
                }

                fir.InputTimeDomainSignal = new Signal(output, false);

                fir.Run();
                OutputSignal = fir.OutputYn;

                int count = M;

                output = new List<float>();

                for (int i = 0; i < OutputSignal.Samples.Count; i++)
                {
                    if (count == M)
                    {
                        output.Add(OutputSignal.Samples[i]);
                        count = 0;
                    }
                    else
                    {
                        count++;
                    }

                }

                OutputSignal = new Signal(output, false);

            }
            else if (M == 0 && L == 0)
            {
                Console.WriteLine("Error");
            }

        }
    }

}