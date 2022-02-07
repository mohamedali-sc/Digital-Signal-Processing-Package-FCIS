using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class QuantizationAndEncoding : Algorithm
    {
        // You will have only one of (InputLevel or InputNumBits), the other property will take a negative value
        // If InputNumBits is given, you need to calculate and set InputLevel value and vice versa
        public int InputLevel { get; set; }
        public int InputNumBits { get; set; }
        public Signal InputSignal { get; set; }
        public Signal OutputQuantizedSignal { get; set; }
        public List<int> OutputIntervalIndices { get; set; }
        public List<string> OutputEncodedSignal { get; set; }
        public List<float> OutputSamplesError { get; set; }

        public override void Run()
        {
            List<float> result = new List<float>();

            
            if (InputNumBits == 0)
            {
                InputNumBits = (int)Math.Log(InputLevel, 2);
            }
            if(InputLevel==0)
            {
                InputLevel = (int)Math.Pow(2, InputNumBits);
            }
            float resluotion = 0;
            List<float> interval = new List<float>();
            List<float> midpoint= new List<float>();
             OutputIntervalIndices = new List<int>();
             OutputSamplesError = new List<float>();
             OutputEncodedSignal = new List<string>();

           

            resluotion = (InputSignal.Samples.Max() - InputSignal.Samples.Min() )/ InputLevel;
           interval.Add(InputSignal.Samples.Min());
            for (int i=1;i<=InputLevel;i++)
            {
                interval.Add(interval[i-1] + resluotion);
                
            }
            for(int j=0;j<InputLevel;j++)
            {
                midpoint.Add((interval[j] +interval[j+1])/ 2);
             
            }
            for(int k=0;k<InputSignal.Samples.Count;k++)
            {
                for(int t=0;t<InputLevel;t++)
                {
                    if(InputSignal.Samples[k]>=interval[t]&& InputSignal.Samples[k] <= interval[t+1]+0.0001)
                    {

                        
                        result.Add(midpoint[t]);
                        OutputIntervalIndices.Add(t+ 1);


                        OutputEncodedSignal.Add( Convert.ToString(t, 2).PadLeft(InputNumBits,'0'));
                        break;
                    }
                }
            }
            OutputQuantizedSignal = new Signal(result, false);
            for (int i=0;i<InputSignal.Samples.Count; i++)
            {
                OutputSamplesError.Add(result[i] - InputSignal.Samples[i]);
            }

           

        }
    }
}
