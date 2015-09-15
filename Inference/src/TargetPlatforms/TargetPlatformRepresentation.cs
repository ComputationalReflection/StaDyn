////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: TargetPlatformRepresentation.cs                                             //
// Authors:  Francisco Ortin - francisco.ortin@gmail.com                       //
// Description:                                                               //
//    Translates the type TargetPlatform to string and vice versa
//    Used for command line optiones and for the visual IDE
// -------------------------------------------------------------------------- //
// Create date: 25-01-2007                                                    //
// Modification date: 26-03-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

namespace TargetPlatforms {

    public class TargetPlatformRepresentation {
            static string[] names = {"clr", "rrotor"};
            static TargetPlatform[] platforms = {TargetPlatform.CLR, TargetPlatform.RRotor};

        
        // * Singleton        
        private static TargetPlatformRepresentation instance = new TargetPlatformRepresentation();
        private TargetPlatformRepresentation() {
            System.Diagnostics.Debug.Assert(names.Length==platforms.Length);
            for (int i=0;i<names.Length;i++) {
                this.representations[platforms[i]] = names[i];
                this.platformsFromName[names[i]] = platforms[i];
            }
        }
        public static TargetPlatformRepresentation Instance {
            get { return instance; }
        }

        /// <summary>
        /// Representations of each target platform (to be used in the command line)
        /// </summary>
        private IDictionary<TargetPlatform, string> representations = new Dictionary<TargetPlatform, string>();

        /// <summary>
        /// Target platforms from their representation
        /// </summary>
        private IDictionary<string,TargetPlatform> platformsFromName = new Dictionary<string,TargetPlatform>();

        /// <summary>
        /// Gets the command line command of each target platform
        /// </summary>
        /// <param name="targetPlatform">The enum entry</param>
        /// <returns>Its textual representation as a option to the command line</returns>
        public string getRepresentation(TargetPlatform targetPlatform) {
            return this.representations[targetPlatform];
        }

        public TargetPlatform getPlatformFromName(string name) {
            return this.platformsFromName[name];
        }


    }

}
