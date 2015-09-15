////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: SSAMap.cs                                                            //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    Implementation of a map to use in static single assignment algorithm.   //
// -------------------------------------------------------------------------- //
// Create date: 07-04-2007                                                    //
// Modification date: 17-05-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using AST;
using ErrorManagement;
using TypeSystem;

namespace Semantic.SSAAlgorithm
{
   /// <summary>
   /// Implementation of a map to use in static single assignment algorithm.
   /// </summary>
   class SSAMap
   {
      #region Fields

      /// <summary>
      /// Mapping between identifiers and its current number in SSA algorithm.
      /// </summary>
      private List<Dictionary<string, SSAElement>> map;

      #endregion

      #region Properties

      /// <summary>
      /// Gets the number of scopes in the SSAMap.
      /// </summary>
      public int ScopesCount
      {
         get { return this.map.Count; }
      }

      /// <summary>
      /// Gets the element stored in specified position.
      /// </summary>
      /// <param name="i">Position.</param>
      /// <returns>Retuns the element stored in specified position.</returns>
      private Dictionary<string, SSAElement> this[int i]
      {
         get
         {
            if ((i >= 0) && (i < this.map.Count))
               return this.map[i];
            return null;
         }
      }

      #endregion

      #region Constructors

      /// <summary>
      /// Constructor of SSAMap.
      /// </summary>
      public SSAMap()
      {
         this.map = new List<Dictionary<string, SSAElement>>();
      }

      #endregion

      #region SetScope()

      /// <summary>
      /// Adds a new scope
      /// </summary>
      public void SetScope()
      {
         this.map.Add(new Dictionary<string, SSAElement>());
      }

      #endregion

      #region ResetScope()

      /// <summary>
      /// Removes the last scope
      /// </summary>
      public Dictionary<string, SSAElement> ResetScope()
      {
         Dictionary<string, SSAElement> aux = this.map[this.map.Count - 1];
         this.map.RemoveAt(this.map.Count - 1);
         return aux;
      }

      #endregion

      #region AddNewVariable()

      /// <summary>
      /// Adds a new identifier.
      /// </summary>
      /// <param name="tmpName">Identifier name.</param>
      /// <param name="type">WriteType of the declaration.</param>
      /// <param name="filename">File name.</param>
      /// <param name="line">Line number.</param>
      /// <param name="column">Column number.</param>
      public void AddNewVariable(string key, string type, Location loc)
      {
         if (!this.map[this.map.Count - 1].ContainsKey(key))
            this.map[this.map.Count - 1].Add(key, new SSAElement(type, loc));
      }

      #endregion

      #region Search()

      /// <summary>
      /// Searches the variable specified and returns its associated number.
      /// </summary>
      /// <param name="id">Identifier name.</param>
      /// <returns>Returns the number of the identifier.</returns>
      public int Search(string id)
      {
         int scope = this.map.Count - 1;

         while (scope >= 0)
         {
            if (this.map[scope].ContainsKey(id))
               return this.map[scope][id].IndexSSA;
            else
               scope--;
         }
         return -1;
      }

      #endregion

      #region Increment()

      /// <summary>
      /// Increments the value of the specified identifier.
      /// </summary>
      /// <param name="id">Identifier name.</param>
      /// <returns>True if the identifier exists, false otherwise.</returns>
      public bool Increment(string id)
      {
         int scope = this.map.Count - 1;

         while (scope >= 0)
         {
            if (this.map[scope].ContainsKey(id))
            {
               this.map[scope][id].UpdateIndexSSA(this.map[scope][id].IndexSSA + 1);
               return true;
            }
            else
               scope--;
         }

         return false;
      }

      #endregion

      #region Clone()

      /// <summary>
      /// Clones the current SSAMap
      /// </summary>
      /// <returns>Returns the clone.</returns>
      public SSAMap Clone()
      {
         SSAMap aux = new SSAMap();

         for (int i = 0; i < this.map.Count; i++)
         {
            aux.SetScope();

            Dictionary<string, SSAElement>.KeyCollection keys = this.map[i].Keys;

            foreach (string key in keys)
            {
               aux.map[i].Add(key, this.map[i][key].Clone());
            }
         }

         return aux;
      }

      #endregion

      #region ToString()

      /// <summary>
      /// Dumps the current state of the SSAMap.
      /// </summary>
      /// <returns>String with the information.</returns>
      public override string ToString()
      {
         StringBuilder aux = new StringBuilder();
         for (int i = 0; i < this.map.Count; i++)
         {
            aux.AppendFormat("Scope {0}", i);
            Dictionary<string, SSAElement>.KeyCollection keys = this.map[i].Keys;
            foreach (string key in keys)
               aux.AppendFormat("\t{0}{1}", key, this.map[i][key].IndexSSA);
         }
         return aux.ToString();
      }

      #endregion

      #region createNewExpression

      /// <summary>
      /// Creates a new SingleIdentifierExpression to use in the new statement
      /// </summary>
      /// <param name="map">Map to use.</param>
      /// <param name="tmpName">Key to access to the specified position in the map.</param>
      /// <param name="position">Position to access in the map.</param>
      /// <param name="fileName">File name.</param>
      /// <param name="line">Line to assign.</param>
      /// <returns>Returns the SingleIdentifierExpression created.</returns>
      private SingleIdentifierExpression createNewExpression(SSAMap map, string key, int position, string filename, int line)
      {
         SingleIdentifierExpression id;

         //if ((position == 0) && (map[position][tmpName].IndexSSA == 0)) // use a real field
         //{
         //   id = new SingleIdentifierExpression(tmpName.Substring(6, tmpName.Length - 6), filename, line, 0);
         //   id.IndexOfSSA = -1;
         //}
         //else
         //{
            id = new SingleIdentifierExpression(key, new Location(filename, line, 0));
            id.IndexOfSSA = map[position][key].IndexSSA;
         //}
         return id;
      }

      #endregion

      #region GetMoveStatements

      #region List<MoveStatement> GetMoveStatementsForIf(SSAMap ifBlockMap, SSAMap elseBlockMap, string filename, int line)

      /// <summary>
      /// Compares the current map with the argument map to create a list of
      /// MoveStatement in base of their information.
      /// </summary>
      /// <param name="ifBlockMap">SSAMap to if block.</param>
      /// <param name="elseBlockMap">SSAMap to else block.</param>
      /// <param name="filename">File name.</param>
      /// <param name="line">Line to assign.</param>
      /// <returns>Returns the list of MoveStatement.</returns>
      public List<MoveStatement> GetMoveStatementsForIf(SSAMap ifBlockMap, SSAMap elseBlockMap, string filename, int line)
      {
         List<MoveStatement> stat = new List<MoveStatement>();

         if ((this.map.Count == ifBlockMap.ScopesCount) && (this.map.Count == elseBlockMap.ScopesCount))
         {
            for (int i = 0; i < this.map.Count; i++)
            {
               if ((this.map[i].Count == ifBlockMap[i].Count) && (this.map[i].Count == elseBlockMap[i].Count))
               {
                  Dictionary<string, SSAElement>.KeyCollection keys = this.map[i].Keys;
                  foreach (string key in keys)
                  {
                     if (((this.map[i][key].IndexSSA == ifBlockMap[i][key].IndexSSA) && (this.map[i][key].IndexSSA != elseBlockMap[i][key].IndexSSA)) ||
                         ((ifBlockMap[i][key].IndexSSA == elseBlockMap[i][key].IndexSSA) && (this.map[i][key].IndexSSA != elseBlockMap[i][key].IndexSSA)))
                     {
                        SingleIdentifierExpression right = this.createNewExpression(this, key, i, filename, line);
                        SingleIdentifierExpression left = new SingleIdentifierExpression(key, new Location(filename, 0, 0));
                        left.IndexOfSSA = elseBlockMap[i][key].IndexSSA + 1;
                        stat.Add(new MoveStatement(left, right, filename, line));
                     }
                  }
               }
            }
         }

         return stat;
      }

      #endregion

      #region List<MoveStatement> GetMoveStatementsForSwitch(List<SSAMap> maps, string filename, int line)

      /// <summary>
      /// Compares the current map with the argument map to create a list of
      /// MoveStatement in base of their information.
      /// </summary>
      /// <param name="maps">List of SSAMap.</param>
      /// <param name="filename">File name.</param>
      /// <param name="line">Line to assign.</param>
      /// <returns>Returns the list of MoveStatement.</returns>
      public List<MoveStatement> GetMoveStatementsForSwitch(List<SSAMap> maps, string filename, int line)
      {
         List<MoveStatement> stat = new List<MoveStatement>();
         SSAMap finalMap = maps[maps.Count - 1];

         if (this.map.Count == finalMap.ScopesCount)
         {
            for (int i = 0; i < this.map.Count; i++)
            {
               if (this.map[i].Count == finalMap[i].Count)
               {
                  Dictionary<string, SSAElement>.KeyCollection keys = this.map[i].Keys;
                  foreach (string key in keys)
                  {
                     if (this.map[i][key].IndexSSA != finalMap[i][key].IndexSSA)
                     {
                        for (int j = 0; j < maps.Count; j++)
                        {
                           // No updates. I need to use the condition value
                           if (((j == 0) && (this.map[i][key].IndexSSA == maps[j][i][key].IndexSSA)) ||
                               ((j != 0) && (maps[j - 1][i][key].IndexSSA == maps[j][i][key].IndexSSA)))
                           {
                              SingleIdentifierExpression right = this.createNewExpression(this, key, i, filename, line);
                              SingleIdentifierExpression left = new SingleIdentifierExpression(key, new Location(filename, 0, 0));
                              left.IndexOfSSA = finalMap[i][key].IndexSSA + 1;
                              stat.Add(new MoveStatement(left, right, filename, line));
                              break;
                           }
                        }
                     }
                  }
               }
            }
         }
         return stat;
      }

      #endregion

      #region List<MoveStatement> GetMoveStatements(SSAMap mapX, string filename, int line)

      /// <summary>
      /// Compares the current map with the argument map to create a list of
      /// MoveStatement in base of their information.
      /// </summary>
      /// <param name="mapX">SSAMap to compare.</param>
      /// <param name="filename">File name</param>
      /// <param name="line">Line to assign</param>
      /// <returns>Returns the list of MoveStatement.</returns>
      public List<MoveStatement> GetMoveStatements(SSAMap mapX, string filename, int line)
      {
         List<MoveStatement> stat = new List<MoveStatement>();

         if (this.map.Count == mapX.ScopesCount)
         {
            for (int i = 0; i < this.map.Count; i++)
            {
               if (this.map[i].Count == mapX[i].Count)
               {
                  Dictionary<string, SSAElement>.KeyCollection keys = this.map[i].Keys;
                  foreach (string key in keys)
                  {
                     if (this.map[i][key].IndexSSA != mapX[i][key].IndexSSA)
                     {
                        SingleIdentifierExpression right = createNewExpression(this, key, i, filename, line);
                        SingleIdentifierExpression left = new SingleIdentifierExpression(key, new Location(filename, line, 0));
                        left.IndexOfSSA = mapX[i][key].IndexSSA + 1;
                        stat.Add(new MoveStatement(left, right, filename, line));
                     }
                  }
               }
            }
         }

         return stat;
      }

      #endregion

      #region List<MoveStatement> GetMoveStatements(SSAMap mapX, SSAMap mapY, string filename, int line)

      /// <summary>
      /// Compares the current map with the argument map to create a list of
      /// MoveStatement in base of their information.
      /// </summary>
      /// <param name="mapX">SSAMap to compare.</param>
      /// <param name="mapY">SSAMap to obtain their values to assign in Move Statements.</param>
      /// <param name="filename">File name.</param>
      /// <param name="line">Line to assign.</param>
      /// <returns>Returns the list of MoveStatement.</returns>
      public List<MoveStatement> GetMoveStatements(SSAMap mapX, SSAMap mapY, string filename, int line)
      {
         List<MoveStatement> stat = new List<MoveStatement>();

         if (this.map.Count == mapX.ScopesCount)
         {
            for (int i = 0; i < this.map.Count; i++)
            {
               if (this.map[i].Count == mapX[i].Count)
               {
                  Dictionary<string, SSAElement>.KeyCollection keys = this.map[i].Keys;
                  foreach (string key in keys)
                  {
                     if (this.map[i][key].IndexSSA != mapX[i][key].IndexSSA)
                     {
                        SingleIdentifierExpression right = this.createNewExpression(mapX, key, i, filename, line);
                        SingleIdentifierExpression left = new SingleIdentifierExpression(key, new Location( filename, 0, 0));
                        left.IndexOfSSA = mapY[i][key].IndexSSA;
                        stat.Add(new MoveStatement(left, right, filename, line));
                     }
                  }
               }
            }
         }

         return stat;
      }

      #endregion

      #endregion

      #region GetThetaStatements

      #region List<ThetaStatement> GetThetaStatements(List<SSAMap> maps, ref SSAMap map3, bool defaultFound, string filename, int line)

      /// <summary>
      /// Compares the current map with both maps to create a list of
      /// ThetaStatement in base of their information.
      /// </summary>
      /// <param name="maps">List of SSMap to compare.</param>
      /// <param name="map3">SSMap to compare and updates their values.</param>
      /// <param name="defaultFound">True if default case is found.</param>
      /// <param name="filename">File name.</param>
      /// <param name="line">Line to assign.</param>
      /// <returns>Returns the list of ThetaStatement.</returns>
      public List<ThetaStatement> GetThetaStatements(List<SSAMap> maps, ref SSAMap map3, bool defaultFound, string filename, int line)
      {
         List<ThetaStatement> stat = new List<ThetaStatement>();
         bool useCondition = !defaultFound;

         if (this.map.Count == map3.ScopesCount)
         {
            for (int i = 0; i < this.map.Count; i++)
            {
               if (this.map[i].Count == map3[i].Count)
               {
                  Dictionary<string, SSAElement>.KeyCollection keys = this.map[i].Keys;
                  foreach (string key in keys)
                  {
                     if (this.map[i][key].IndexSSA != map3[i][key].IndexSSA)
                     {
                        List<SingleIdentifierExpression> list = new List<SingleIdentifierExpression>();

                        for (int j = 0; j < maps.Count; j++)
                        {
                           if ((this.map.Count == maps[j].ScopesCount) && (this.map[i].Count == maps[j][i].Count))
                           {
                              if (((j == 0) && (this.map[i][key].IndexSSA != maps[j][i][key].IndexSSA)) ||
                                  ((j != 0) && (maps[j - 1][i][key].IndexSSA != maps[j][i][key].IndexSSA)))
                                 list.Add(createNewExpression(maps[j], key, i, filename, line));
                              else
                                 useCondition = true;
                           }
                        }

                        if (useCondition)
                        {
                           list.Add(createNewExpression(this, key, i, filename, line));
                           useCondition = !defaultFound;
                        }

                        list.Add(createNewExpression(map3, key, i, filename, line));
                        SingleIdentifierExpression id = new SingleIdentifierExpression(key, new Location(filename, line, 0));
                        id.IndexOfSSA = map3[i][key].IndexSSA + 1;
                        stat.Add(new ThetaStatement(id, list, new Location(filename, line, 0)));

                        map3[i][key].UpdateIndexSSA(map3[i][key].IndexSSA + 1);
                     }
                  }
               }
            }
         }

         return stat;
      }

      #endregion

      #region List<ThetaStatement> GetThetaStatements(SSAMap map2, ref SSAMap map3, string filename, int line)

      /// <summary>
      /// Compares the current map with both maps to create a list of
      /// ThetaStatement in base of their information.
      /// </summary>
      /// <param name="map2">SSMap to compare and updates their values.</param>
      /// <param name="map3">SSMap to compare and updates their values.</param>
      /// <param name="filename">File name.</param>
      /// <param name="line">Line to assign.</param>
      /// <returns>Returns the list of ThetaStatement.</returns>
      public List<ThetaStatement> GetThetaStatements(SSAMap map2, ref SSAMap map3, string filename, int line)
      {
         List<ThetaStatement> stat = new List<ThetaStatement>();

         if ((this.map.Count == map3.ScopesCount) && (this.map.Count == map2.ScopesCount))
         {
            for (int i = 0; i < this.map.Count; i++)
            {
               if ((this.map[i].Count == map3[i].Count) && (this.map[i].Count == map2[i].Count))
               {
                  Dictionary<string, SSAElement>.KeyCollection keys = this.map[i].Keys;
                  foreach (string key in keys)
                  {
                     if (this.map[i][key].IndexSSA != map3[i][key].IndexSSA)
                     {
                        List<SingleIdentifierExpression> list = new List<SingleIdentifierExpression>();
                        list.Add(createNewExpression(this, key, i, filename, line));

                        if (this.map[i][key].IndexSSA != map2[i][key].IndexSSA)
                           list.Add(createNewExpression(map2, key, i, filename, line));

                        list.Add(createNewExpression(map3, key, i, filename, line));
                        SingleIdentifierExpression id = new SingleIdentifierExpression(key, new Location(filename, line, 0));
                        id.IndexOfSSA = map3[i][key].IndexSSA + 1;
                        stat.Add(new ThetaStatement(id, list, new Location(filename, line, 0)));

                        map3[i][key].UpdateIndexSSA(map3[i][key].IndexSSA + 1);
                     }
                  }
               }
            }
         }

         return stat;
      }

      #endregion

      #region List<ThetaStatement> GetThetaStatements(ref SSAMap map3, string filename, int line)

      /// <summary>
      /// Compares the current map with both maps to create a list of
      /// ThetaStatement in base of their information.
      /// </summary>
      /// <param name="mapX">SSMap to compare.</param>
      /// <param name="filename">File name.</param>
      /// <param name="line">Line to assign.</param>
      /// <returns>Returns the list of ThetaStatement.</returns>
      public List<ThetaStatement> GetThetaStatements(ref SSAMap mapX, string filename, int line)
      {
         List<ThetaStatement> stat = new List<ThetaStatement>();

         if (this.map.Count == mapX.ScopesCount)
         {
            for (int i = 0; i < this.map.Count; i++)
            {
               if (this.map[i].Count == mapX[i].Count)
               {
                  Dictionary<string, SSAElement>.KeyCollection keys = this.map[i].Keys;
                  foreach (string key in keys)
                  {
                     if (this.map[i][key].IndexSSA != mapX[i][key].IndexSSA)
                     {
                        List<SingleIdentifierExpression> list = new List<SingleIdentifierExpression>();

                        list.Add(createNewExpression(this, key, i, filename, line));
                        list.Add(createNewExpression(mapX, key, i, filename, line));
                        SingleIdentifierExpression id = new SingleIdentifierExpression(key, new Location(filename, line, 0));
                        id.IndexOfSSA = mapX[i][key].IndexSSA + 1;
                        stat.Add(new ThetaStatement(id, list, new Location(filename, line, 0)));

                        mapX[i][key].UpdateIndexSSA(mapX[i][key].IndexSSA + 1);
                     }
                  }
               }
            }
         }

         return stat;
      }

      #endregion

      #region List<ThetaStatement> GetThetaStatements(ref SSAMap map3, SSAMap condMap, string filename, int line)

      /// <summary>
      /// Compares the current map with both maps to create a list of
      /// ThetaStatement in base of their information.
      /// </summary>
      /// <param name="mapX">SSMap to compare.</param>
      /// <param name="condMap">Condition SSMap to use if this.map and mapX have the same value.</param>
      /// <param name="filename">File name.</param>
      /// <param name="line">Line to assign.</param>
      /// <returns>Returns the list of ThetaStatement.</returns>
      public List<ThetaStatement> GetThetaStatements(ref SSAMap mapX, SSAMap condMap, string filename, int line)
      {
         List<ThetaStatement> stat = new List<ThetaStatement>();

         if ((this.map.Count == mapX.ScopesCount) && ((this.map.Count == condMap.ScopesCount)))
         {
            for (int i = 0; i < this.map.Count; i++)
            {
               if ((this.map[i].Count == mapX[i].Count) && (this.map[i].Count == condMap[i].Count))
               {
                  Dictionary<string, SSAElement>.KeyCollection keys = this.map[i].Keys;
                  foreach (string key in keys)
                  {
                     List<SingleIdentifierExpression> list = new List<SingleIdentifierExpression>();
                     if (this.map[i][key].IndexSSA != mapX[i][key].IndexSSA)
                     {
                        list.Add(createNewExpression(this, key, i, filename, line));
                        list.Add(createNewExpression(mapX, key, i, filename, line));
                        SingleIdentifierExpression id = new SingleIdentifierExpression(key, new Location(filename, line, 0));
                        id.IndexOfSSA = mapX[i][key].IndexSSA + 1;
                        stat.Add(new ThetaStatement(id, list, new Location(filename, line, 0)));
                        mapX[i][key].UpdateIndexSSA(mapX[i][key].IndexSSA + 1);
                     }
                     else // this.map == mapX
                     {
                        if (condMap[i][key].IndexSSA != mapX[i][key].IndexSSA)
                        {
                           list.Add(createNewExpression(condMap, key, i, filename, line));
                           list.Add(createNewExpression(mapX, key, i, filename, line));
                           SingleIdentifierExpression id = new SingleIdentifierExpression(key, new Location(filename, line, 0));
                           id.IndexOfSSA = mapX[i][key].IndexSSA + 1;
                           stat.Add(new ThetaStatement(id, list, new Location(filename, line, 0)));
                           mapX[i][key].UpdateIndexSSA(mapX[i][key].IndexSSA + 1);
                        }
                     }
                  }
               }
            }
         }

         return stat;
      }

      #endregion
      #endregion
   }
}
