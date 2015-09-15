using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace DynVarManagement {
    /// <summary>
    /// New implementation of DynVarManagement:
    ///     -Ignores "block" elements: not used anymore.
    ///     -Provides wider interface:
    ///         -Dynamic method return (dynreturn attribute in method elements)
    ///         -Interface elements
    /// Includes set of old fields, properties and methods for retro-compatibility reasons.
    /// </summary>
    public class DynVarManager {
        #region Fields

        private XmlDocument document;
        private string fileName;

        #endregion

        #region ".dyn" XML constants: Extension, tags and attributes

        /// <summary>
        /// File extension
        /// </summary>
        public static readonly string DynVarFileExt = ".dyn";

        public static readonly string ApplicationTag = "application";
        public static readonly string NamespaceTag = "namespace";
        public static readonly string ClassTag = "class";
        public static readonly string InterfaceTag = "interface";
        public static readonly string MethodTag = "method";
        public static readonly string DynVarTag = "dynvar";

        public static readonly string NameAtt = "name";
        public static readonly string DynReturnAtt = "dynreturn";

        #endregion

        #region Properties

        public string FileName {
            get { return fileName; }
        }

        #endregion

        #region LoadOrCreate

        /// <summary>
        /// Attemps to load the specified ".dyn" file.
        /// If the file does not exist or an exception is thrown, a new file is created in its place.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool LoadOrCreate(string fileName) {
            this.fileName = fileName;
            document = new XmlDocument();
            try {
                document.Load(fileName);
                //Compatibility:
                this.ready = true;
                return true;
            } catch (Exception) {
                XmlElement el = document.CreateElement(ApplicationTag);
                el.SetAttribute(NameAtt, Path.GetFileNameWithoutExtension(fileName));
                document.AppendChild(el);
                //Compatibility:
                this.ready = true;
                return false;
            }
        }

        #endregion

        #region Save

        public void Save() {
            if (document != null) {
                document.Save(FileName);
            }
        }

        #endregion

        #region SetDynamic

        public void SetDynamic(VarPath path) {
            //Get/Create nodes until parent element is reached
            XmlNode node = getOrCreateUntilParent(path);
            //Set dynamic (field or variable)
            if (path.VarName != null)
                getOrCreateNode(node, DynVarTag, NameAtt, path.VarName);
            //Set dynamic (method return)
            else if (path.MethodName != null && node.Name.Equals(MethodTag) && node is XmlElement)
                (node as XmlElement).SetAttribute(DynReturnAtt, true.ToString());
        }

        #endregion

        #region SetStatic

        public void SetStatic(VarPath path) {
            //Get/Create nodes until parent element is reached
            XmlNode node = getOrCreateUntilParent(path);
            //Set static (field or variable)
            if (path.VarName != null) {
                XmlNode varNode = node.SelectSingleNode(
                    DynVarTag + "[@" + NameAtt + "= '" + path.VarName + "']");
                node.RemoveChild(varNode);
            }
            //Set static (method return)
            else if (path.MethodName != null) {
                XmlNode methodNode = node.SelectSingleNode(
                    MethodTag + "[@" + NameAtt + "= '" + path.MethodName + "']");
                XmlElement methodElement = methodNode as XmlElement;
                if (methodElement != null)
                    methodElement.SetAttribute(DynReturnAtt, false.ToString());
            }
        }

        #endregion

        #region IsDynamic

        public bool IsDynamic(VarPath path) {
            //Get/Create nodes until parent element is reached
            XmlNode node = getOrCreateUntilParent(path);
            //Check dynamic (field or variable)
            if (path.VarName != null) {
                node = node.SelectSingleNode(
                    DynVarTag + "[@" + NameAtt + "= '" + path.VarName + "']");
                //Var is dynamic if and only if node exists
                return node != null;
            }
            //Check dynamic (method return)
            else if (path.MethodName != null && node.Name.Equals(MethodTag) && node is XmlElement) {
                XmlElement methodElement = node as XmlElement;
                return true.ToString().Equals(methodElement.GetAttribute(DynReturnAtt));
            }
            else
                return false;
        }

        #endregion

        #region Private methods

        private XmlNode getOrCreateNode(XmlNode node, string name, string attribute, string value) {
            XmlNode newNode = node.SelectSingleNode(name + "[@" + attribute + " = '" + value + "']");
            if (newNode == null) {
                newNode = document.CreateElement(name);
                ((XmlElement)newNode).SetAttribute(attribute, value);
                node.AppendChild(newNode);
            }
            return newNode;
        }

        private XmlNode getOrCreateUntilParent(VarPath path) {
            XmlNode node = document.DocumentElement;
            if (path.NamespaceName != null) {
                node = getOrCreateNode(node, NamespaceTag, NameAtt, path.NamespaceName);
            }
            if (path.ClassName != null) {
                node = getOrCreateNode(node, ClassTag, NameAtt, path.ClassName);
            }
            else if (path.InterfaceName != null) {
                node = getOrCreateNode(node, InterfaceTag, NameAtt, path.InterfaceName);
            }
            if (path.MethodName != null) {
                node = getOrCreateNode(node, MethodTag, NameAtt, path.MethodName);
            }
            // * "block" element is not used anymore.
            // *
            // *
            //if (path.Blocks.Count > 0)
            //{
            //    XmlNodeList nodeList;
            //    XmlNode blockNode;
            //    for (int blockLevel = 0; blockLevel < path.Blocks.Count; blockLevel++)
            //    {
            //        nodeList = node.SelectNodes("block");
            //        blockNode = null;
            //        for (int i = 0; i <= path.Blocks[blockLevel] - nodeList.Count; i++)
            //        {
            //            blockNode = document.CreateElement("block");
            //            node.AppendChild(blockNode);
            //        }
            //        node = blockNode == null ? nodeList[path.Blocks[blockLevel]] : blockNode;
            //    }
            //}            
            return node;
        }

        #endregion

        //COMPATIBILITY

        
         //* These members provide compatibility with the old DynVarManagement implementation, using the same interface.
         //*  - Instead of Load, LoadOrCreate can be used.
         //*  - Instead of SearchDynVar, IsDynamic can be used (VarPath argument instead of various string arguments)
         //*  - Constructor and Ready property added.
         

        #region Fields

        /// <summary>
        /// True if the xml document is loaded. Otherwise false
        /// </summary>
        private bool ready;

        #endregion

        #region Properties

        /// <summary>
        /// Gets true if the xml document is loaded. Otherwise false.
        /// </summary>
        public bool Ready {
            get { return this.ready; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of DynVarManager
        /// </summary>
        public DynVarManager() {
            this.ready = false;
        }

        #endregion

        #region Load

        /// <summary>
        /// Loads the xml file associated to the source code file
        /// </summary>
        public void Load(string name) {
            this.fileName = name;
            document = new XmlDocument();
            try {
                document.Load(name);
                this.ready = true;
            } catch (Exception e) {
                throw new DynVarException(e.Message);
            }
        }

        /// <summary>
        /// Create de XmlDocument
        /// </summary>
        public void Load()
        {            
            fileName = "stadyn.dyn";
            document = new XmlDocument();
            try
            {         
                this.ready = true;
            }
            catch (Exception e)
            {
                throw new DynVarException(e.Message);
            }
        }

        #endregion

        #region SearchDynVar

        public bool SearchDynVar(string ns, string classId, string varId) {
            VarPath path = new VarPath();
            path.NamespaceName = ns;
            path.ClassName = classId;
            path.VarName = varId;
            return IsDynamic(path);
        }

        public bool SearchDynVar(string ns, string classId, string methodId, string varId) {
            VarPath path = new VarPath();
            path.NamespaceName = ns;
            path.ClassName = classId;
            path.MethodName = methodId;
            path.VarName = varId;
            return IsDynamic(path);
        }

        public bool SearchDynVar(string ns, string classId, string methodId, List<int> blockList, string varId) {
            //Blocks not used any more
            return SearchDynVar(ns, classId, methodId, varId);
        }

        #endregion

        //*
        // * Extended interface with new SearchDynVar overloading (unnecessary since IsDynamic accepts VarPath)
        // *  - Dynamic method return managing
        // *  - Interface members managing
        // *

        #region SearchDynVar

        public bool SearchDynVar(string ns, string classOrInterfaceId, string methodId, bool isClassId) {
            VarPath path = new VarPath();
            path.NamespaceName = ns;
            if (isClassId)
                path.ClassName = classOrInterfaceId;
            else
                path.InterfaceName = classOrInterfaceId;
            path.MethodName = methodId;
            return IsDynamic(path);
        }

        #endregion

    }

    #region class VarPath

    public class VarPath {
        #region Fields

        private string namespaceName;
        private string className;
        private string interfaceName;
        private string methodName;
        //Not used anymore
        //private List<int> blocks;
        private string varName;

        #endregion

        #region Properties
        public string NamespaceName {
            get { return namespaceName; }
            set { namespaceName = value; }
        }
        public string ClassName {
            get { return className; }
            set { className = value; }
        }
        public string InterfaceName {
            get { return interfaceName; }
            set { interfaceName = value; }
        }
        public string MethodName {
            get { return methodName; }
            set { methodName = value; }
        }

        //* Not used anymore
        //public List<int> Blocks
        //{
        //    get { return blocks; }
        //}
        
        public string VarName {
            get { return varName; }
            set { varName = value; }
        }

        #endregion

        #region Constructor

        public VarPath() {
            //Not used anymore
            //blocks = new List<int>();
        }

        #endregion
    }

    #endregion
}


/*
// * Old implementation
 
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;
using System.IO;

namespace DynVarManagement
{
   /// <summary>
   /// Parses the associated file (xml) in order to
   /// load information about those references which are dynamic. 
   /// </summary>
   public class DynVarManager
   {
      #region Fields

      /// <summary>
      /// The name of the associated file
      /// </summary>
      private string fileName;

      /// <summary>
      /// Stores the informacion of dynamic references
      /// </summary>
      private XmlDocument document;

      /// <summary>
      /// True if the xml document is loaded. Otherwise false
      /// </summary>
      private bool ready;

      /// <summary>
      /// File extension
      /// </summary>
      public const string DynVarFileExt = ".dyn";

      #endregion

      #region Properties

      /// <summary>
      /// Gets true if the xml document is loaded. Otherwise false.
      /// </summary>
      public bool Ready
      {
         get { return this.ready; }
      }

      #endregion

      #region Constructor

      /// <summary>
      /// Constructor of DynVarManager
      /// </summary>
      public DynVarManager()
      {
         this.ready = false;
      }

      #endregion

      #region Load()

      /// <summary>
      /// Loads the xml file associated to the source code file
      /// </summary>
      public void Load(string name)
      {
         this.fileName = name;
         document = new XmlDocument();
         try
         {
            document.Load(name);
            this.ready = true;
         }
         catch (Exception e)
         {
            throw new DynVarException(e.Message);
         }
      }

      #endregion

      #region SearchDynVar

      public bool SearchDynVar(string ns, string classId, string varId)
      {
         XmlNode node = document.DocumentElement.SelectSingleNode("namespace[@name = '" + ns + "']");
         if (node != null)
         {
            node = node.SelectSingleNode("class[@name = '" + classId + "']");
            if (node != null)
            {
               node = node.SelectSingleNode("dynvar[@name = '" + varId + "']");
               if (node != null)
                  return true;
            }
         }
         return false;
      }

      public bool SearchDynVar(string ns, string classId, string methodId, string varId)
      {
         XmlNode node = document.DocumentElement.SelectSingleNode("namespace[@name = '" + ns + "']");
         if (node != null)
         {
            node = node.SelectSingleNode("class[@name = '" + classId + "']");
            if (node != null)
            {
               node = node.SelectSingleNode("method[@name = '" + methodId + "']");
               if (node != null)
               {
                  node = node.SelectSingleNode("dynvar[@name = '" + varId + "']");
                  if (node != null)
                     return true;
               }
            }
         }
         return false;
      }

      public bool SearchDynVar(string ns, string classId, string methodId, List<int> blockList, string varId)
      {
         XmlNode node = document.DocumentElement.SelectSingleNode("namespace[@name = '" + ns + "']");
         if (node != null)
         {
            node = node.SelectSingleNode("class[@name = '" + classId + "']");
            if (node != null)
            {
               node = node.SelectSingleNode("method[@name = '" + methodId + "']");
               if (node != null)
               {
                  XmlNodeList nodeList = node.SelectNodes("block");
                  for (int i = 0; i < blockList.Count; i++)
                  {
                     if (blockList[i] < nodeList.Count)
                     {
                        node = nodeList.Item(blockList[i]);
                        nodeList = node.SelectNodes("block");
                     }
                     else
                        return false;
                  }
                  node = node.SelectSingleNode("dynvar[@name = '" + varId + "']");
                  if (node != null)
                     return true;
               }
            }
         }
         return false;
      }

      #endregion
   }
}
*/