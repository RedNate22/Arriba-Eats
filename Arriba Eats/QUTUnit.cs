using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace CAB201Menu
{
    /// <summary>
    /// Represents a unit (a subject) at QUT
    /// </summary>
    public class QUTUnit
    {
        // A set of fields to represent the unit. At the moment they are all private
        // but this could change as the program develops
        private string Code;
        private string Name;
        private int NumberOfStudents;
        private DateTime First;
        private DateTime Last;


        /// <summary>
        /// A constructor for the QUT unit
        /// </summary>
        /// <param name="code">The QUT Code for the unit</param>
        /// <param name="name">The name of the unit</param>
        /// <param name="initialStudents">The initial number of students</param>
        /// <param name="first">The date and time of the first lecture</param>
        /// <param name="last">The date and time of the last lecture</param>
        public QUTUnit(string code, string name, int initialStudents, DateTime first, DateTime last)
        {
            this.Code = code;  
            this.Name = name;  
            this.NumberOfStudents = initialStudents;
            this.First = first;
            this.Last = last;
        }

        /// <summary>
        /// Increases the student cohort by number 
        /// </summary>
        /// <param name="number">The number of students to increase the cohort</param>
        public void IncreaseStudents(int number)
        {
            this.NumberOfStudents += number;
        }

        /// <summary>
        /// Decreases the student cohort by number
        /// </summary>
        /// <param name="number">The number of students to decrease the cohort</param>
        public void DecreaseStudents(int number)
        {
            this.NumberOfStudents -= number;
        }

        /// <summary>
        /// Create a string that is human readable
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // Converts the DateTimes to a human readable string 
            string firstDateTime = First.ToString(QUTConsts.DATETIMEFORMAT);
            string lastDateTime = Last.ToString(QUTConsts.DATETIMEFORMAT);

            return $"{Code} {Name} Students {NumberOfStudents}.\nRunning from {firstDateTime} to {lastDateTime}.";
        }

    }
}
