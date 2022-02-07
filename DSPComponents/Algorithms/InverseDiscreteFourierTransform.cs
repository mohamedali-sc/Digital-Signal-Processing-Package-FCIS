using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }

        public List<float> freqq;
        public List<float> ampl;
        public List<float> shiftt;
        public List<Complex> c;
        public List<float> s;

        public override void Run()
        {
            c = new List<Complex>();

            freqq = new List<float>();
            ampl = new List<float>();
            shiftt = new List<float>();
            s = new List<float>();

            for (int i = 0; i < InputFreqDomainSignal.FrequenciesAmplitudes.Count; i++)
            {
                double r = InputFreqDomainSignal.FrequenciesAmplitudes[i] * Math.Cos(InputFreqDomainSignal.FrequenciesPhaseShifts[i]);
                double im = InputFreqDomainSignal.FrequenciesAmplitudes[i] * Math.Sin(InputFreqDomainSignal.FrequenciesPhaseShifts[i]);

                c.Add(new Complex(r, -im));
            }

            int sum = InputFreqDomainSignal.FrequenciesAmplitudes.Count;


            for (int i = 0; i < sum; i++)
            {
                double z = 0; ;
                for (int j = 0; j < sum; j++)
                {
                    double real = Math.Cos(2 * Math.PI * i * j / sum);
                    double img = Math.Sin(2 * Math.PI * i * j / sum);
                    Complex ll = new Complex(real, img);


                    z += c[j].Real * real + c[j].Imaginary * img;


                }
                s.Add((float)z / sum);
            }
            OutputTimeDomainSignal = new Signal(s, false);
        }
        
    }
}


