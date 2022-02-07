using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()

        {
             OutputNonNormalizedCorrelation = new List<float>();
             OutputNormalizedCorrelation = new List<float>();
            if (InputSignal2 ==null)//auto
            {
                List<float> auto_corr = new List<float>();
                List<double> Input_signal1 = new List<double>();
                List<double> Input_signal2_copy = new List<double>();

                for (int i=0;i<InputSignal1.Samples.Count;i++)
                {
                    Input_signal1.Add(InputSignal1.Samples[i]);
                    Input_signal2_copy.Add(InputSignal1.Samples[i]);
                    
                }
                double nor_summation = 0, signal1_sum= 0, signa2_copy_sum= 0;
                for (int i=0;i<InputSignal1.Samples.Count;i++)
                {
                    signal1_sum = signal1_sum + (Input_signal1[i]*Input_signal1[i]);
                    signa2_copy_sum = signa2_copy_sum + (Input_signal2_copy[i]*Input_signal2_copy[i]);
                }
                nor_summation = signal1_sum * signa2_copy_sum;
                nor_summation = Math.Sqrt(nor_summation);
                nor_summation /= Input_signal1.Count;
                if(InputSignal1.Periodic!=true)
                {
                    for(int i=0;i<Input_signal1.Count;i++)
                    {
                        double sum = 0;
                        if(i!=0)
                        {
                            double f_element = 0;
                            for (int j = 0; j < Input_signal2_copy.Count - 1; j++)
                            {
                                Input_signal2_copy[j] = Input_signal2_copy[j + 1];
                                sum = sum + (Input_signal2_copy[j] * Input_signal1[j]);

                            }
                            Input_signal2_copy[Input_signal2_copy.Count - 1] = f_element;
                            sum += Input_signal2_copy[Input_signal2_copy.Count - 1] * Input_signal1[Input_signal1.Count - 1];

                        }
                        else
                            for(int k=0;k<Input_signal2_copy.Count;k++)
                            {
                                sum += Input_signal2_copy[k] * Input_signal2_copy[k];
                            }
                        auto_corr.Add((float)sum / Input_signal2_copy.Count);

                    }
                }
                else
                {

                    for (int i = 0; i < Input_signal1.Count; i++)
                    {
                        double sum = 0;
                        if (i != 0)
                        {
                            double f_element = Input_signal2_copy[0];
                            for (int j = 0; j < Input_signal2_copy.Count - 1; j++)
                            {
                                Input_signal2_copy[j] = Input_signal2_copy[j + 1];
                                sum = sum + (Input_signal2_copy[j] * Input_signal1[j]);

                            }
                            Input_signal2_copy[Input_signal2_copy.Count - 1] = f_element;
                            sum += Input_signal2_copy[Input_signal2_copy.Count - 1] * Input_signal1[Input_signal1.Count - 1];

                        }
                        else
                            for (int k = 0; k < Input_signal2_copy.Count; k++)
                            {
                                sum += Input_signal2_copy[k] * Input_signal2_copy[k];
                            }
                        auto_corr.Add((float)sum / Input_signal2_copy.Count);

                    }
                }
                OutputNonNormalizedCorrelation = auto_corr;
                for (int i = 0; i < OutputNonNormalizedCorrelation.Count; i++)
                    OutputNormalizedCorrelation.Add((float)(OutputNonNormalizedCorrelation[i] / nor_summation));
            }






        }
    }
}