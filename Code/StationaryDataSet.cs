using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatLibrary
{
    /// <summary>
    /// Primary data storing class of this library. Inherits from .NET List Collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class DataSet<T> : List<T>
    {
        /// <summary>
        /// Conducts point estimation of selected index using n-step moving averages 
        /// </summary>
        /// <remarks> DataSet is required to have stationary pattern </remarks>
        /// <param name="Index"></param>
        /// <param name="Step"></param>
        /// <returns> Estimated Value </returns>
        public double MovingAverages(int Index, int Step)
        {
            if (this.Pattern == DataSetPattern.Stationary)
            {
                double Average = 0;
                for (int i = 1; i <= Step; i++)
                {
                    Average += Convert.ToDouble(this[Index - i]);
                }
                return Average;
            }
            else
            {
                throw new Exception("DataSet pattern is not valid. Stationary pattern expected");
            }
        }

        /// <summary>
        /// Conducts point estimation of selected index using simple exponential smoothing with the alpha parameter
        /// </summary>
        /// <remarks> DataSet is required to have stationary pattern </remarks>
        /// <param name="Index"></param>
        /// <param name="Alpha"></param>
        /// <returns> Estimated Value</returns>
        public double ExponentialSmoothing(int Index, double Alpha)
        {
            if (this.Pattern == DataSetPattern.Stationary)
            {
                if (Alpha < 1 && Alpha > 0)
                {
                    double Forecast = 0;

                    for (int i = 0; i < Index; i++)
                    {
                        Forecast += Alpha * Math.Pow((1 - Alpha), i) * Convert.ToDouble(this[Index - 1 - i]);
                    }

                    return Forecast;
                }
                else
                {
                    throw new Exception("Invalid Alpha Value");
                }
            }
            else
            {
                throw new Exception("DataSet pattern is not valid. Stationary pattern expected");
            }
        }
    }
}
