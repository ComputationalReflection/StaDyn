//////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- 
// Project Stadyn                                                             
// -------------------------------------------------------------------------- 
// File: ILReservedWords.cs                                                     
// Author: Daniel Zapico -  daniel.zapico.rodriguez@gmail.com                    
// Description:                                                               
//    Esta clase sirve para almacenar las palabras reservadas de un lenguaje
// Está un continuno crecimiento, porque lo que interesa en este momento, es que funcionen
// los tests. 
// Se hará una ampliación, seguramente basada en herencia para cada plataforma, 
// cuando se empiece a generar código para más plataformas      
// -------------------------------------------------------------------------- 
// Create date: 21-08-2007                                                    
// Modification date: 21-08-2007                                              
//////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using TypeSystem;

namespace CodeGeneration {

    class ILReservedWords {
        /// <summary>
        /// Ordered array containing reserved words
        /// </summary>
        static readonly string [] reservedWords = { "add", "field" };
        /// <summary>
        /// Checks wether  parameter word is a reserved word
        /// </summary>
        /// <param name="word">string to find out if it is a reserved word</param>
        /// <returns>True if 'word' is a reserfed word, false it not</returns>
        public bool CheckReserved(string word) {
            return Array.BinarySearch(reservedWords, word) == 0;
        
        }
        /// <summary>
        /// Receives a parameter to use en IL. If this parameter is tmpName word in IL it's surrounded by a pair of 's. 
        /// Otherwise it returns the parameter itself
        /// </summary>
        /// <param name="word">a candidate parameter to use en IL</param>
        /// <returns>If parameter word is tmpName word in IL it's surrounded by a pair of 's. 
        /// Otherwise it returns the parameter itself</returns>
        public string MakeILComplaint(String word) {
            return CheckReserved(word) ? "'" + word + "'" : word;
        }

    }
}
