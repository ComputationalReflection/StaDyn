////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: TargetPlatformRepresentation.cs                                      //
// Authors:  Daniel Zapico Rodríguez Ortin - daniel.zapico.rodriguez@gmail.com                       //
// Description:                                                               //
//    there is an object array that contains objects. This enum typè avoids having to remember
// the position in the array
//      enum type                                                     //
// Create date: 4-03-2010                                                    //
// Modification date: 4-03-2010                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGeneration {

    public enum MethodInvocationArgument {
        Clean = 0, 
        MakeBox, 
        DecorationAttributes
    }

}