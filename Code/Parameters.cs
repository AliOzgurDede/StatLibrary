using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StatLibrary
{
    /// <summary>
    /// Class that consists of particular parameter calculation methods
    /// </summary>
    public static class Parameters
    {
        /// <summary>
        /// Calculating mean of dataset
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns> mean value </returns>
        public static double Mean(DataSet<double> dataset)
        {
            double mean;
            double total = 0;

            for (int i = 0; i < dataset.Count; i++)
            {
                total = total + dataset[i];
            }

            mean = Math.Round(total / dataset.Count, 2);
            return mean;
        }

        /// <summary>
        /// Calculating standart deviation of dataset
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns> standart deviation value </returns>
        public static double StandartDeviation(DataSet<double> dataset)
        {
            double standartDeviation;
            double sumOfSquares = 0;
            double mean = Mean(dataset);

            for (int i = 0; i < dataset.Count; i++)
            {
                sumOfSquares = sumOfSquares + Math.Pow(dataset[i] - mean, 2);
            }

            standartDeviation = Math.Round(Math.Sqrt(sumOfSquares / dataset.Count), 2);
            return standartDeviation;
        }

        /// <summary>
        /// Calculating skewness of dataset
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns> skewness value </returns>
        public static double Skewness(DataSet<double> dataset)
        {
            double skewness;
            double mean = Mean(dataset);
            double sumOfSquares = 0;
            double sumOfCubes = 0;

            for (int i = 0; i < dataset.Count; i++)
            {
                sumOfCubes = sumOfCubes + Math.Pow(dataset[i] - mean, 3);
                sumOfSquares = sumOfSquares + Math.Pow(dataset[i] - mean, 2);
            }

            sumOfCubes = sumOfCubes / dataset.Count;
            sumOfSquares = Math.Pow(Math.Sqrt(sumOfSquares / (dataset.Count - 1)), 3);
            skewness = sumOfCubes / sumOfSquares;
            skewness = Math.Round(skewness, 2);
            return skewness;
        }

        /// <summary>
        /// Calculating covariance between two datasets
        /// </summary>
        /// <param name="dataset1"></param>
        /// <param name="dataset2"></param>
        /// <returns> covariance value </returns>
        public static double Covariance(DataSet<double> dataset1, DataSet<double> dataset2)
        {
            double covariance;
            double total = 0;

            for (int i = 0; i < dataset1.Count; i++)
            {
                total = total + ((dataset1[i] - Mean(dataset1)) * (dataset2[i] - Mean(dataset2)));
            }

            covariance = total / dataset1.Count;
            return covariance;
        }

        /// <summary>
        /// Calculating correlation coefficient between two datasets
        /// </summary>
        /// <param name="dataset1"></param>
        /// <param name="dataset2"></param>
        /// <returns> correlation coefficient </returns>
        public static double CorrelationCoefficient(DataSet<double> dataset1, DataSet<double> dataset2)
        {
            double CC;
            CC = Covariance(dataset1, dataset2) / StandartDeviation(dataset1) * StandartDeviation(dataset2);
            CC = Math.Round(CC, 2);
            return CC;
        }

        /// <summary>
        /// Calculating standardized Z value of a sample dataset
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="PopulationMean"></param>
        /// <param name="PopulaitonStandartDeviation"></param>
        /// <returns> Z value </returns>
        public static double Zvalue(DataSet<double> dataset, double PopulationMean, double PopulaitonStandartDeviation)
        {
            double Z;
            Z = (Mean(dataset) - PopulationMean) / (PopulaitonStandartDeviation / Math.Sqrt(dataset.Count));
            return Z;
        }

        /// <summary>
        /// Conducting Z test
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="PopulationMean"></param>
        /// <param name="PopulaitonStandartDeviation"></param>
        /// <param name="Z"></param>
        /// <param name="Significance"></param>
        /// <param name="OneTailed"></param>
        /// <returns> Hypothesis boolean value </returns>
        public static bool Ztest(DataSet<double> dataset, double PopulationMean, double PopulaitonStandartDeviation, double Z, double Significance, bool OneTailed)
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

            Z = Zvalue(dataset, PopulationMean, PopulaitonStandartDeviation);
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
