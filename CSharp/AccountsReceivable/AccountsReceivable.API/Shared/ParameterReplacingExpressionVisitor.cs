using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace ExpressionModifier
{
    public class MyExpressionVisitor : ExpressionVisitor
    {
        string _varName = "t";

        public ParameterExpression shiftExpression = Expression.Parameter(typeof(double), "shift");

        public Expression Modify(LambdaExpression expression)
        {
            SubstExpression = Expression.Add(expression.Parameters[0], shiftExpression);

            return Visit(expression);
        }

        Expression SubstExpression = null;

        protected override Expression VisitBinary(BinaryExpression node)
        {
            Expression left = null, right = null;
            bool substLeft = false;
            bool substRight = false;

            if (node.Left is ParameterExpression)
            {
                left = SubstExpression;
                substLeft = true;
            }
            else
                left = node.Left;

            if (node.Right is ParameterExpression)
            {
                right = SubstExpression;
                substRight = true;
            }
            else
                right = node.Right;

            if (substLeft || substRight)
            {
                if (!substLeft)
                {
                    left = Visit(left);
                }

                if (!substRight)
                {
                    right = Visit(right);
                }

                return Expression.MakeBinary(node.NodeType, left, right, node.IsLiftedToNull, node.Method);
            }

            return base.VisitBinary(node);
        }
    }
}