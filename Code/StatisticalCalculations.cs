/*
The MIT License(MIT)

Copyright(c) 2021 Ali Özgür Dede

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StatLibrary
{
    public static class StatisticalCalculations
    {
        public static void GenerateCollectionMembers(List<double> list, DataGridView dataGridView)
        {
            int rows = dataGridView.Rows.Count;

            for (int i = 0; i < rows; i++)
            {
                if (dataGridView[0, i].Value != null)
                {
                    double value = Double.Parse(dataGridView[0, i].Value.ToString());
                    list.Add(value);
                }
            }
        }

        public static double Mean(List<double> list)
        {
            double mean;
            double total = 0;

            for (int i = 0; i < list.Count; i++)
            {
                total = total + list[i];
            }

            mean = Math.Round(total / list.Count, 2);
            return mean;
        }

        public static double StandartDeviation(List<double> list)
        {
            double standartDeviation;
            double sumOfSquares = 0;
            double mean = Mean(list);

            for (int i = 0; i < list.Count; i++)
            {
                sumOfSquares = sumOfSquares + Math.Pow(list[i] - mean, 2);
            }

            standartDeviation = Math.Round(Math.Sqrt(sumOfSquares / list.Count), 2);
            return standartDeviation;
        }

        public static double Skewness(List<double> list)
        {
            double skewness;
            double mean = Mean(list);
            double sumOfSquares = 0;
            double sumOfCubes = 0;

            for (int i = 0; i < list.Count; i++)
            {
                sumOfCubes = sumOfCubes + Math.Pow(list[i] - mean, 3);
                sumOfSquares = sumOfSquares + Math.Pow(list[i] - mean, 2);
            }

            sumOfCubes = sumOfCubes / list.Count;
            sumOfSquares = Math.Pow(Math.Sqrt(sumOfSquares / (list.Count - 1)), 3);
            skewness = sumOfCubes / sumOfSquares;
            skewness = Math.Round(skewness, 2);
            return skewness;
        }

        public static double Covariance(List<double> list1, List<double> list2)
        {
            double covariance;
            double total = 0;

            for (int i = 0; i < list1.Count; i++)
            {
                total = total + ((list1[i] - Mean(list1)) * (list2[i] - Mean(list2)));
            }

            covariance = total / list1.Count;
            return covariance;
        }

        public static double CorrelationCoefficient(List<double> list1, List<double> list2)
        {
            double CC;
            CC = Covariance(list1, list2) / StandartDeviation(list1) * StandartDeviation(list2);
            CC = Math.Round(CC, 2);
            return CC;
        }

        public static double Zvalue(List<double> list, double PopulationMean, double PopulaitonStandartDeviation)
        {
            double Z;
            Z = (Mean(list) - PopulationMean) / (PopulaitonStandartDeviation / Math.Sqrt(list.Count));
            return Z;
        }

        public static bool Ztest(List<double> list, double PopulationMean, double PopulaitonStandartDeviation, double Z, double Significance, bool OneTailed)
        {
            bool Hypothesis;

            Dictionary<double, double> Ztable = new Dictionary<double, double>();
            Ztable.Add(0.10, 1.28);
            Ztable.Add(0.05, 1.65);
            Ztable.Add(0.025, 1.96);
            Ztable.Add(0.01, 2.31);
            Ztable.Add(0.005, 2.56);
            double CriticalValue;
            if (OneTailed == true)
            {
                CriticalValue = Ztable[Significance];
            }
            else
            {
                CriticalValue = Ztable[Significance] / 2;
            }
            
            Z = Zvalue(list, PopulationMean, PopulaitonStandartDeviation);
            if (Math.Abs(Z) > Math.Abs(CriticalValue))
            {
                Hypothesis = false;
            }
            else
            {
                Hypothesis = true;
            }

            return Hypothesis;
        }
    }
}
