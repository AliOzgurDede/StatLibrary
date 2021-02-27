using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace StatLibrary
{
    /// <summary>
    /// Primary data storing class of this library. Inherits from .NET List Collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataSet<T> : List<T>
    {
        public DataSet()
        {
            this.Pattern = DataSetPattern.None;
        }
        
        /// <summary>
        /// Gets or sets the pattern characteristic of DataSet
        /// </summary>
        public DataSetPattern Pattern
        {
            get
            {
                return pattern;
            }
            set
            {
                pattern = value;
            }
        }
        private DataSetPattern pattern;
        public enum DataSetPattern
        {
            Stationary,
            Trending,
            Seasonal
        }

        /// <summary>
        /// Gets the minimum value of DataSet
        /// </summary>
        public double MinimumValue
        {
            get
            {
                return Convert.ToDouble(this.Min());
            }
        }

        /// <summary>
        /// Gets the maximum value of DataSet
        /// </summary>
        public double MaximumValue
        {
            get
            {
                return Convert.ToDouble(this.Max());
            }
        }

        /// <summary>
        /// Gets the range of DataSet
        /// </summary>
        public double Range
        {
            get
            {
                return (this.MaximumValue - this.MinimumValue);
            }
        }
    }
}
