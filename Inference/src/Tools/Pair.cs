//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project rROTOR                                                             
// -------------------------------------------------------------------------- 
// File: Pair.cs                                                           
// Author: Francisco Ortin - francisco.ortin@gmail.com                       
// Description:                                                               
//    Generic pair of objects
// -------------------------------------------------------------------------- 
// Create date: 24-05-2006                                                    
// Modification date: 24-05-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;

namespace Tools {
    /// <summary>
    /// Generic pair of objects
    /// </summary>
    public class Pair<FirstType,SecondType> {

        /// <summary>
        /// The first element of the pair
        /// </summary>
        private FirstType first;

        /// <summary>
        /// The second element of the pair
        /// </summary>
        private SecondType second;

        /// <summary>
        /// The first element of the pair
        /// </summary>
        public FirstType First {
            get { return first; }
            set { first = value; }
        }

        /// <summary>
        /// The second element of the pair
        /// </summary>
        public SecondType Second {
            get { return second; }
            set { second = value; }
        }

        public Pair(FirstType first, SecondType second) {
            this.first = first;
            this.second = second;
        }

        public override bool Equals(object obj) {
            Pair<FirstType, SecondType> pair = obj as Pair<FirstType, SecondType>;
            if (pair == null)
                return false;
            return pair.First.Equals(First) && pair.Second.Equals(Second);
        }

        public override int GetHashCode() {
            return First.GetHashCode() * Second.GetHashCode();
        }
    }
}