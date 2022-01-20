using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicCalculator
{
    /// <summary>
    /// Holds the information about a single calculator operation
    /// </summary>
    public class Operation
    {
        #region Public Properties

        /// <summary>
        /// The left side of the operation
        /// </summary>
        public string LeftSide { get; set; }
        /// <summary>
        /// The right side of the operation
        /// </summary>
        public string RightSide { get; set; }
        /// <summary>
        /// The types of operator to prefrom
        /// </summary>
        public OperationType OperationType { get; set; }
        /// <summary>
        /// an inner operation to be performed before this operation
        /// </summary>
        public Operation InnerOperator { get; set; }

        #endregion
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public Operation()
        {
            //Create empty string instead of nulls
            this.LeftSide = string.Empty;
            this.RightSide = string.Empty;
        }

        #endregion
    }
}
