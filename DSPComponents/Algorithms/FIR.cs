using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; } // CONV
        public FILTER_TYPES InputFilterType { get; set; } // TYPE , EQUATIONS FROM TYPES
        public float InputFS { get; set; } // SAMPLING FREQYENCY
        public float InputCutOffFrequency { get; set; } // CUT OFF FREQYENCY
        public float InputF1 { get; set; } // PASS BAND
        public float InputF2 { get; set; } // PASS BAND 
        public float InputStopBandAttenuation { get; set; } //  CONDITION FROM WINDOW TABLE
        public float InputTransitionBand { get; set; } // FROM WINDOW SDFSFS
        public Signal OutputHn { get; set; }
        public Signal OutputYn { get; set; }





        public override void Run()
        {
            List<float> h = new List<float>();
            List<float> output = new List<float>();
            float N = 0;
            List<float> windows = new List<float>();
            InputTransitionBand /= InputFS;
            DirectConvolution d = new DirectConvolution();
            if (InputStopBandAttenuation >= 0 && InputStopBandAttenuation <= 21)
            {
                N = 0.9f / InputTransitionBand;
                N = (int)N;
                if (N % 2 == 0)
                {
                    N += 1;
                }

                N /= 2;
                N = (int)N;



                for (int i = (int)(N * -1); i <= N; i++)
                {
                    windows.Add(1);
                    
                }

            }

            else if (InputStopBandAttenuation > 21 && InputStopBandAttenuation <= 44)
            {
                N = 3.1f / InputTransitionBand;
                N = (int)N;

                if (N % 2 == 0)
                {
                    N += 1;
                }

                N /= 2;
                N = (int)N;

                for (int i = (int)(N * -1); i <= N; i++)
                {
                    windows.Add(0.5f + (0.5f * ((float)Math.Cos((2 * Math.PI * i)) / ((N * 2) + 1))));
                }
            }


            else if (InputStopBandAttenuation > 44 && InputStopBandAttenuation <= 53)
            {

                N = 3.3f / InputTransitionBand;
                N = (int)N;
                if (N % 2 == 0)
                {
                    N += 1;
                }

                N /= 2;

                N = (int)N;


                for (int i = (int)(N * -1); i <= N; i++)
                {
                    windows.Add(0.54f + (0.46f * (float)(Math.Cos((2 * Math.PI * i) / ((N * 2) + 1)))));
                }

            }

            else if (InputStopBandAttenuation > 53 && InputStopBandAttenuation <= 74)
            {

                N = 5.5f / InputTransitionBand;

                N = (int)N;

                if (N % 2 == 0)
                {
                    N += 1;
                }

                N /= 2;
                N = (int)N;

                for (int i = (int)(N * -1); i <= N; i++)
                {
                    windows.Add(0.42f + (0.5f * (float)(Math.Cos((2 * Math.PI * i) / (N * 2)))) + ((float)(0.08 * Math.Cos((4 * Math.PI * i) / (N * 2)))));


                }

            }



            if (InputFilterType == FILTER_TYPES.LOW)
            {
                InputCutOffFrequency = (InputCutOffFrequency + ((InputTransitionBand * InputFS) / 2)) / InputFS;


                for (int i = (int)(N * -1); i <= N; i++)
                {
                    if (i == 0)
                    {
                        h.Add(2 * InputCutOffFrequency);

                    }
                    else
                    {
                        h.Add((2 * InputCutOffFrequency * (float)(Math.Sin(i * 2 * (float)Math.PI * InputCutOffFrequency)) / (i * 2 * (float)(Math.PI) * InputCutOffFrequency)));

                    }

                }



            }

            else if (FILTER_TYPES.HIGH == InputFilterType)
            {
                InputCutOffFrequency = (InputCutOffFrequency +((InputTransitionBand * InputFS) / 2)) / InputFS;



                for (int i = (int)(N * -1); i <= N; i++)
                {
                    if (i == 0)
                    {
                        h.Add(1 - (2 * InputCutOffFrequency));

                    }
                    else
                    {
                        h.Add((-2 * InputCutOffFrequency * (float)(Math.Sin(i * 2 * (float)Math.PI * InputCutOffFrequency)) / (i * 2 * (float)(Math.PI) * InputCutOffFrequency)));

                    }

                }



            }



            else if (FILTER_TYPES.BAND_PASS == InputFilterType)
            {
                //InputCutOffFrequency = (InputCutOffFrequency - InputTransitionBand * InputFS / 2) / InputFS;

                InputF1 = (InputF1 - (InputTransitionBand * InputFS / 2)) / InputFS;
                InputF2 = (InputF2 + (InputTransitionBand * InputFS / 2)) / InputFS;


                for (int i = (int)(N * -1); i <= N; i++)
                {
                    if (i == 0)
                    {
                        h.Add(2 * (InputF2 - InputF1));

                    }
                    else
                    {
                        h.Add(((2 * InputF2 * (float)(Math.Sin(i * 2 * (float)Math.PI * InputF2)) / (i * 2 * (float)(Math.PI) * InputF2)) -
                 (2 * InputF1 * (float)(Math.Sin(i * 2 * (float)Math.PI * InputF1)) / (i * 2 * (float)(Math.PI) * InputF1))));

                    }

                }



            }


            else if (FILTER_TYPES.BAND_STOP == InputFilterType)
            {
              

                InputF1 = (InputF1 - (InputTransitionBand * InputFS / 2)) / InputFS;
                InputF2 = (InputF2 + (InputTransitionBand * InputFS / 2)) / InputFS;


                for (int i = (int)(N * -1); i <= N; i++)
                {
                    if (i == 0)
                    {
                        h.Add(1 - (2 * (InputF2 - InputF1)));

                    }
                    else
                    {
                        h.Add(((2 * InputF1 * (float)(Math.Sin(i * 2 * (float)Math.PI * InputF1)) / (i * 2 * (float)(Math.PI) * InputF1)) -
                   (2 * InputF2 * (float)(Math.Sin(i * 2 * (float)Math.PI * InputF2)) / (i * 2 * (float)(Math.PI) * InputF2))));

                    }
                }

            }

            for (int i = 0; i < h.Count; i++)
            {
                output.Add((h[i] * windows[i]));
            }
            List<int> indices = new List<int>();

            for (int i = (int)(N * -1); i <= N; i++)
            {


                indices.Add(i);
            }
            
            OutputHn = new Signal(output, indices, false);
            d.InputSignal1 = OutputHn;
            d.InputSignal2 = InputTimeDomainSignal;


            d.Run();
            OutputYn = new Signal(d.OutputConvolvedSignal.Samples, d.OutputConvolvedSignal.SamplesIndices, false);


        }
    }
}
