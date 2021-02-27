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
        /// Gets or sets the number of seasons in a seasonal DataSet
        /// </summary>
        /// <remarks> DataSet is required to have seasonal pattern </remarks>
        public int NumberOfSeasons
        {
            get
            {
                if (this.Pattern == DataSetPattern.Seasonal)
                {
                    return numberOfSeasons;
                }
                else
                {
                    throw new Exception("DataSet pattern is not valid. Seasonal pattern expected");
                }
            }
            set
            {
                if (this.Pattern == DataSetPattern.Seasonal)
                {
                    if (this.Count % value == 0)
                    {
                        numberOfSeasons = value;
                    }
                    else
                    {
                        throw new Exception("DataSetSize = 0(mod NumberOfSeasons) should be satisfied");
                    }
                }
                else
                {
                    throw new Exception("DataSet pattern is not valid. Seasonal pattern expected");
                }
            }
        }
        private int numberOfSeasons;

        /// <summary>
        /// Gets or sets the size of each season in a seasonal DataSet
        /// </summary>
        /// <remarks> DataSet is required to have seasonal pattern </remarks>
        public int SeasonSize
        {
            get
            {
                if (this.Pattern == DataSetPattern.Seasonal)
                {
                    return seasonSize;
                }
                else
                {
                    throw new Exception("DataSet pattern is not valid. Seasonal pattern expected");
                }
            }
            set
            {
                if (this.Pattern == DataSetPattern.Seasonal)
                {
                    if (this.Count % value == 0)
                    {
                        seasonSize = value;
                    }
                    else
                    {
                        throw new Exception("DataSetSize = 0(mod SeasonSize) should be satisfied");
                    }
                }
                else
                {
                    throw new Exception("DataSet pattern is not valid. Seasonal pattern expected");
                }
            }
        }
        private int seasonSize;

        /// <summary>
        /// Gets the seasonal factors of a seasonal DataSet
        /// </summary>
        /// <remarks> DataSet is required to have seasonal pattern </remarks>
        public double[] SeasonalFactors
        {
            get
            {
                if (this.Pattern == DataSetPattern.Seasonal)
                {
                    try
                    {
                        return seasonalFactors = this.GetSeasonalFactors();
                    }
                    catch
                    {
                        throw new Exception("Unassigned properties");
                    }
                }
                else
                {
                    throw new Exception("DataSet pattern is not valid. Seasonal pattern expected");
                }
            }
        }
        private double[] seasonalFactors;
        private double average;
        private double[] GetSeasonalFactors()
        {
            double[] averageRatios = new double[this.SeasonSize];
            average = 0;

            for (int i = 0; i < this.Count; i++)
            {
                average = average + Convert.ToDouble(this[i]);
            }
            average = Math.Round(average / this.Count, 2);

            for (int i = 0; i < this.SeasonSize; i++)
            {
                for (int j = i; j < this.Count; j += this.SeasonSize)
                {
                    averageRatios[i] += (Convert.ToDouble(this[j]) / average);
                }

                averageRatios[i] /= this.NumberOfSeasons;
            }

            return averageRatios;
        }

        /// <summary>
        /// Conducts point estimation of selected index using seasonal factors
        /// </summary>
        /// <param name="Index"></param>
        /// <returns> Estimated Value </returns>
        public double Estimate(int Index)
        {
            double estimate;
            estimate = average * SeasonalFactors[Index % this.SeasonSize];
            return estimate;
        }
    }
}
