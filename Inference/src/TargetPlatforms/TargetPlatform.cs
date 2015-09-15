////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: TargetPlatformRepresentation.cs                                      //
// Authors:  Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Each target platform of our retargetable compiler must have an entry    //
//      on this enum type                                                     //
//    Remember to add that entry in the two static arrays in                  //
//      the TargetPlaformRepresentation class                                 //
//    Used for command line optiones and for the visual IDE
// -------------------------------------------------------------------------- //
// Create date: 25-01-2007                                                    //
// Modification date: 26-03-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace TargetPlatforms {

    public enum TargetPlatform {
        CLR,
        RRotor,        
    }

}
