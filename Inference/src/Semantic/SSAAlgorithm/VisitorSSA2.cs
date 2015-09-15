////////////////////////////////////////////////////////////////////////////////
// -------------------------------------------------------------------------- //
// Project rROTOR                                                             //
// -------------------------------------------------------------------------- //
// File: VisitorSSA2.cs                                                       //
// Author: Cristina Gonzalez Muñoz  -  cristi.gm@gmail.com                    //
// Description:                                                               //
//    This class makes the second part of static single assignment algorithm  //
// in which every variable is assigned exactly once.                          //
//    Inheritance: VisitorAdapter.                                            //
//    Implements Visitor pattern [Concrete Visitor].                          //
// -------------------------------------------------------------------------- //
// Create date: 06-04-2007                                                    //
// Modification date: 03-05-2007                                              //
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;

using AST;
using Tools;
using ErrorManagement;

namespace Semantic.SSAAlgorithm
{
   /// <summary>
   /// This class makes the second part of static single assignment algorithm 
   /// in which every variable is assigned exactly once.
   /// </summary>
   /// <remarks>
   /// Inheritance: VisitorAdapter
   /// Implements Visitor pattern [Concrete Visitor].
   /// </remarks>
   class VisitorSSA2 : VisitorAdapter
   {
      #region Constructor

      /// <summary>
      /// Constructor of VisitorSSA2
      /// </summary>
      public VisitorSSA2()
      {
      }

      #endregion 

      // Declarations

      #region Visit(Definition node, Object obj)

      public override Object Visit(Definition node, Object obj)
      {
         node.Init.Accept(this, obj);
         return null;
      }

      #endregion

      #region Visit(ConstantDefinition node, Object obj)

      public override Object Visit(ConstantDefinition node, Object obj)
      {
         node.Init.Accept(this, obj);
         return null;
      }

      #endregion

      #region Visit(FieldDefinition node, Object obj)

      public override Object Visit(FieldDefinition node, Object obj)
      {
         node.Init.Accept(this, obj);
         return null;
      }

      #endregion

      #region Visit(ConstantFieldDefinition node, Object obj)

      public override Object Visit(ConstantFieldDefinition node, Object obj)
      {
         node.Init.Accept(this, obj);
         return null;
      }

      #endregion

      // Expressions

      #region createMoveStatement

      private MoveStatement createMoveStatement(string id, SSAMap mapX, SSAMap mapY, string filename, int line)
      {
         SingleIdentifierExpression left = new SingleIdentifierExpression(id, new Location(filename, line, 0));
         left.IndexOfSSA = mapY.Search(id) + 1;
         SingleIdentifierExpression right = new SingleIdentifierExpression(id, new Location(filename, line, 0));
         right.IndexOfSSA = mapX.Search(id);
         return new MoveStatement(left, right, filename,line);
      }

      private MoveStatement createMoveStatement(string id, SSAMap mapX, string filename, int line)
      {
          SingleIdentifierExpression left = new SingleIdentifierExpression(id, new Location(filename, line, 0));
         left.IndexOfSSA = mapX.Search(id);
         SingleIdentifierExpression right = new SingleIdentifierExpression(id, new Location(filename, line, 0));
         right.IndexOfSSA = mapX.Search(id) - 1;
         return new MoveStatement(left, right, filename, line);
      }

      #endregion
      
      #region Visit(AssignmentExpression node, Object obj)

      public override Object Visit(AssignmentExpression node, Object obj)
      {
         SSAMap mapX;
         SSAMap mapY;

         if (obj is SSAInfo)
         {
            if (node.FirstOperand is SingleIdentifierExpression)
            {
               if ((mapX = ((SSAInfo)obj).FirstOperandToMove) != null)
               {
                  if (mapX.Search(((SingleIdentifierExpression)node.FirstOperand).Identifier) != -1) // <-- Modified
                  {
                     if ((mapY = ((SSAInfo)obj).SecondOperandToMove) != null)
                     {
                        if (((SingleIdentifierExpression)node.FirstOperand).IndexOfSSA == mapX.Search(((SingleIdentifierExpression)node.FirstOperand).Identifier))
                            node.MoveStat = createMoveStatement(((SingleIdentifierExpression)node.FirstOperand).Identifier, mapX, mapY, node.Location.FileName, node.Location.Line);
                     }
                     else
                     {
                        if (((SingleIdentifierExpression)node.FirstOperand).IndexOfSSA == mapX.Search(((SingleIdentifierExpression)node.FirstOperand).Identifier) - 1)
                            node.MoveStat = createMoveStatement(((SingleIdentifierExpression)node.FirstOperand).Identifier, mapX, node.Location.FileName, node.Location.Line);
                     }
                  }
               }
            }
            else
               node.FirstOperand.Accept(this, obj);
            node.SecondOperand.Accept(this, obj);
         }
         if (node.MoveStat != null)
            node.MoveStat.Accept(this, obj);
         return null;
      }

      #endregion

      #region Visit(MoveStatement node, Object obj)

      public override Object Visit(MoveStatement node, Object obj)
      {
         SSAMap mapX;
         SSAMap mapY;

         if (obj is SSAInfo)
         {
            if ((mapX = ((SSAInfo)obj).FirstOperandToMove) != null)
            {
               if (mapX.Search(node.LeftExp.Identifier) != -1) // <-- Modified
               {
                  if ((mapY = ((SSAInfo)obj).SecondOperandToMove) != null)
                  {
                     if (node.LeftExp.IndexOfSSA == mapX.Search(node.LeftExp.Identifier))
                         node.MoveStat = createMoveStatement(node.LeftExp.Identifier, mapX, mapY, node.Location.FileName, node.Location.Line);
                  }
                  else
                  {
                     if (node.LeftExp.IndexOfSSA == mapX.Search(node.LeftExp.Identifier) - 1)
                        node.MoveStat = createMoveStatement(node.LeftExp.Identifier, mapX, node.Location.FileName, node.Location.Line);
                  }
               }
            }
         }
         node.RightExp.Accept(this, obj);

         if (node.MoveStat != null)
            node.MoveStat.Accept(this, obj);

         return null;
      }

      #endregion

      #region Visit(SingleIdentifierExpression node, Object obj)

      public override Object Visit(SingleIdentifierExpression node, Object obj)
      {
         SSAMap mapX;
         SSAMap mapY;

         if (obj is SSAInfo)
         {
            if (((mapX = ((SSAInfo)obj).FirstOperandToUpdateId) != null) && ((mapY = ((SSAInfo)obj).SecondOperandToUpdateId) != null))
            {
               if (!(node.LeftExpression))
               {
                  int iValueX;
                  int iValueY;

                  if (((iValueX = mapX.Search(node.Identifier)) != -1) && ((iValueY = mapY.Search(node.Identifier)) != -1))
                  {
                     if (node.IndexOfSSA == iValueX)
                     {
                        if (iValueX != iValueY)
                        {
                           node.IndexOfSSA = iValueY;
                        }
                     }
                  }
               }
            }
         }
         return null;
      }

      #endregion
   }
}
