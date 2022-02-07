using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class TimeDelay:Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public float InputSamplingPeriod { get; set; }
        public float OutputTimeDelay { get; set; }

        public override void Run()
        {
            DirectCorrelation D_C = new DirectCorrelation();
            D_C.InputSignal1 = InputSignal1;
            D_C.InputSignal2 = InputSignal2;
            D_C.Run();
            int idex = 0;
            float max = 0;
            for (int i = 0; i < D_C.OutputNonNormalizedCorrelation.Count; i++)
            {
                if(Math.Abs(D_C.OutputNonNormalizedCorrelation[i])>max)
                {
                    max = Math.Abs(D_C.OutputNonNormalizedCorrelation[i]);
                    idex = i;

                }
            }
            OutputTimeDelay = idex * InputSamplingPeriod;


        }
    }
}
