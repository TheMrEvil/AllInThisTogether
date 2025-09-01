using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000649 RID: 1609
	internal sealed class QueryBuilder
	{
		// Token: 0x06004160 RID: 16736 RVA: 0x00166E45 File Offset: 0x00165045
		private void Reset()
		{
			this._parseDepth = 0;
			this._needContext = false;
		}

		// Token: 0x06004161 RID: 16737 RVA: 0x00166E58 File Offset: 0x00165058
		private Query ProcessAxis(Axis root, QueryBuilder.Flags flags, out QueryBuilder.Props props)
		{
			if (root.Prefix.Length > 0)
			{
				this._needContext = true;
			}
			this._firstInput = null;
			Query query;
			Query query2;
			if (root.Input != null)
			{
				QueryBuilder.Flags flags2 = QueryBuilder.Flags.None;
				if ((flags & QueryBuilder.Flags.PosFilter) == QueryBuilder.Flags.None)
				{
					Axis axis = root.Input as Axis;
					if (axis != null && root.TypeOfAxis == Axis.AxisType.Child && axis.TypeOfAxis == Axis.AxisType.DescendantOrSelf && axis.NodeType == XPathNodeType.All)
					{
						Query qyParent;
						if (axis.Input != null)
						{
							qyParent = this.ProcessNode(axis.Input, QueryBuilder.Flags.SmartDesc, out props);
						}
						else
						{
							qyParent = new ContextQuery();
							props = QueryBuilder.Props.None;
						}
						query = new DescendantQuery(qyParent, root.Name, root.Prefix, root.NodeType, false, axis.AbbrAxis);
						if ((props & QueryBuilder.Props.NonFlat) != QueryBuilder.Props.None)
						{
							query = new DocumentOrderQuery(query);
						}
						props |= QueryBuilder.Props.NonFlat;
						return query;
					}
					if (root.TypeOfAxis == Axis.AxisType.Descendant || root.TypeOfAxis == Axis.AxisType.DescendantOrSelf)
					{
						flags2 |= QueryBuilder.Flags.SmartDesc;
					}
				}
				query2 = this.ProcessNode(root.Input, flags2, out props);
			}
			else
			{
				query2 = new ContextQuery();
				props = QueryBuilder.Props.None;
			}
			switch (root.TypeOfAxis)
			{
			case Axis.AxisType.Ancestor:
				query = new XPathAncestorQuery(query2, root.Name, root.Prefix, root.NodeType, false);
				props |= QueryBuilder.Props.NonFlat;
				break;
			case Axis.AxisType.AncestorOrSelf:
				query = new XPathAncestorQuery(query2, root.Name, root.Prefix, root.NodeType, true);
				props |= QueryBuilder.Props.NonFlat;
				break;
			case Axis.AxisType.Attribute:
				query = new AttributeQuery(query2, root.Name, root.Prefix, root.NodeType);
				break;
			case Axis.AxisType.Child:
				if ((props & QueryBuilder.Props.NonFlat) != QueryBuilder.Props.None)
				{
					query = new CacheChildrenQuery(query2, root.Name, root.Prefix, root.NodeType);
				}
				else
				{
					query = new ChildrenQuery(query2, root.Name, root.Prefix, root.NodeType);
				}
				break;
			case Axis.AxisType.Descendant:
				if ((flags & QueryBuilder.Flags.SmartDesc) != QueryBuilder.Flags.None)
				{
					query = new DescendantOverDescendantQuery(query2, false, root.Name, root.Prefix, root.NodeType, false);
				}
				else
				{
					query = new DescendantQuery(query2, root.Name, root.Prefix, root.NodeType, false, false);
					if ((props & QueryBuilder.Props.NonFlat) != QueryBuilder.Props.None)
					{
						query = new DocumentOrderQuery(query);
					}
				}
				props |= QueryBuilder.Props.NonFlat;
				break;
			case Axis.AxisType.DescendantOrSelf:
				if ((flags & QueryBuilder.Flags.SmartDesc) != QueryBuilder.Flags.None)
				{
					query = new DescendantOverDescendantQuery(query2, true, root.Name, root.Prefix, root.NodeType, root.AbbrAxis);
				}
				else
				{
					query = new DescendantQuery(query2, root.Name, root.Prefix, root.NodeType, true, root.AbbrAxis);
					if ((props & QueryBuilder.Props.NonFlat) != QueryBuilder.Props.None)
					{
						query = new DocumentOrderQuery(query);
					}
				}
				props |= QueryBuilder.Props.NonFlat;
				break;
			case Axis.AxisType.Following:
				query = new FollowingQuery(query2, root.Name, root.Prefix, root.NodeType);
				props |= QueryBuilder.Props.NonFlat;
				break;
			case Axis.AxisType.FollowingSibling:
				query = new FollSiblingQuery(query2, root.Name, root.Prefix, root.NodeType);
				if ((props & QueryBuilder.Props.NonFlat) != QueryBuilder.Props.None)
				{
					query = new DocumentOrderQuery(query);
				}
				break;
			case Axis.AxisType.Namespace:
				if ((root.NodeType == XPathNodeType.All || root.NodeType == XPathNodeType.Element || root.NodeType == XPathNodeType.Attribute) && root.Prefix.Length == 0)
				{
					query = new NamespaceQuery(query2, root.Name, root.Prefix, root.NodeType);
				}
				else
				{
					query = new EmptyQuery();
				}
				break;
			case Axis.AxisType.Parent:
				query = new ParentQuery(query2, root.Name, root.Prefix, root.NodeType);
				break;
			case Axis.AxisType.Preceding:
				query = new PrecedingQuery(query2, root.Name, root.Prefix, root.NodeType);
				props |= QueryBuilder.Props.NonFlat;
				break;
			case Axis.AxisType.PrecedingSibling:
				query = new PreSiblingQuery(query2, root.Name, root.Prefix, root.NodeType);
				break;
			case Axis.AxisType.Self:
				query = new XPathSelfQuery(query2, root.Name, root.Prefix, root.NodeType);
				break;
			default:
				throw XPathException.Create("The XPath query '{0}' is not supported.", this._query);
			}
			return query;
		}

		// Token: 0x06004162 RID: 16738 RVA: 0x0016721B File Offset: 0x0016541B
		private static bool CanBeNumber(Query q)
		{
			return q.StaticType == XPathResultType.Any || q.StaticType == XPathResultType.Number;
		}

		// Token: 0x06004163 RID: 16739 RVA: 0x00167234 File Offset: 0x00165434
		private Query ProcessFilter(Filter root, QueryBuilder.Flags flags, out QueryBuilder.Props props)
		{
			bool flag = (flags & QueryBuilder.Flags.Filter) == QueryBuilder.Flags.None;
			QueryBuilder.Props props2;
			Query query = this.ProcessNode(root.Condition, QueryBuilder.Flags.None, out props2);
			if (QueryBuilder.CanBeNumber(query) || (props2 & (QueryBuilder.Props)6) != QueryBuilder.Props.None)
			{
				props2 |= QueryBuilder.Props.HasPosition;
				flags |= QueryBuilder.Flags.PosFilter;
			}
			flags &= (QueryBuilder.Flags)(-2);
			Query query2 = this.ProcessNode(root.Input, flags | QueryBuilder.Flags.Filter, out props);
			if (root.Input.Type != AstNode.AstType.Filter)
			{
				props &= (QueryBuilder.Props)(-2);
			}
			if ((props2 & QueryBuilder.Props.HasPosition) != QueryBuilder.Props.None)
			{
				props |= QueryBuilder.Props.PosFilter;
			}
			FilterQuery filterQuery = query2 as FilterQuery;
			if (filterQuery != null && (props2 & QueryBuilder.Props.HasPosition) == QueryBuilder.Props.None && filterQuery.Condition.StaticType != XPathResultType.Any)
			{
				Query query3 = filterQuery.Condition;
				if (query3.StaticType == XPathResultType.Number)
				{
					query3 = new LogicalExpr(Operator.Op.EQ, new NodeFunctions(Function.FunctionType.FuncPosition, null), query3);
				}
				query = new BooleanExpr(Operator.Op.AND, query3, query);
				query2 = filterQuery.qyInput;
			}
			if ((props & QueryBuilder.Props.PosFilter) != QueryBuilder.Props.None && query2 is DocumentOrderQuery)
			{
				query2 = ((DocumentOrderQuery)query2).input;
			}
			if (this._firstInput == null)
			{
				this._firstInput = (query2 as BaseAxisQuery);
			}
			bool flag2 = (query2.Properties & QueryProps.Merge) > QueryProps.None;
			bool flag3 = (query2.Properties & QueryProps.Reverse) > QueryProps.None;
			if ((props2 & QueryBuilder.Props.HasPosition) != QueryBuilder.Props.None)
			{
				if (flag3)
				{
					query2 = new ReversePositionQuery(query2);
				}
				else if ((props2 & QueryBuilder.Props.HasLast) != QueryBuilder.Props.None)
				{
					query2 = new ForwardPositionQuery(query2);
				}
			}
			if (flag && this._firstInput != null)
			{
				if (flag2 && (props & QueryBuilder.Props.PosFilter) != QueryBuilder.Props.None)
				{
					query2 = new FilterQuery(query2, query, false);
					Query qyInput = this._firstInput.qyInput;
					if (!(qyInput is ContextQuery))
					{
						this._firstInput.qyInput = new ContextQuery();
						this._firstInput = null;
						return new MergeFilterQuery(qyInput, query2);
					}
					this._firstInput = null;
					return query2;
				}
				else
				{
					this._firstInput = null;
				}
			}
			return new FilterQuery(query2, query, (props2 & QueryBuilder.Props.HasPosition) == QueryBuilder.Props.None);
		}

		// Token: 0x06004164 RID: 16740 RVA: 0x001673D0 File Offset: 0x001655D0
		private Query ProcessOperator(Operator root, out QueryBuilder.Props props)
		{
			QueryBuilder.Props props2;
			Query query = this.ProcessNode(root.Operand1, QueryBuilder.Flags.None, out props2);
			QueryBuilder.Props props3;
			Query query2 = this.ProcessNode(root.Operand2, QueryBuilder.Flags.None, out props3);
			props = (props2 | props3);
			switch (root.OperatorType)
			{
			case Operator.Op.OR:
			case Operator.Op.AND:
				return new BooleanExpr(root.OperatorType, query, query2);
			case Operator.Op.EQ:
			case Operator.Op.NE:
			case Operator.Op.LT:
			case Operator.Op.LE:
			case Operator.Op.GT:
			case Operator.Op.GE:
				return new LogicalExpr(root.OperatorType, query, query2);
			case Operator.Op.PLUS:
			case Operator.Op.MINUS:
			case Operator.Op.MUL:
			case Operator.Op.DIV:
			case Operator.Op.MOD:
				return new NumericExpr(root.OperatorType, query, query2);
			case Operator.Op.UNION:
				props |= QueryBuilder.Props.NonFlat;
				return new UnionExpr(query, query2);
			default:
				return null;
			}
		}

		// Token: 0x06004165 RID: 16741 RVA: 0x00167486 File Offset: 0x00165686
		private Query ProcessVariable(Variable root)
		{
			this._needContext = true;
			if (!this._allowVar)
			{
				throw XPathException.Create("'{0}' is an invalid key pattern. It either contains a variable reference or 'key()' function.", this._query);
			}
			return new VariableQuery(root.Localname, root.Prefix);
		}

		// Token: 0x06004166 RID: 16742 RVA: 0x001674BC File Offset: 0x001656BC
		private Query ProcessFunction(Function root, out QueryBuilder.Props props)
		{
			props = QueryBuilder.Props.None;
			switch (root.TypeOfFunction)
			{
			case Function.FunctionType.FuncLast:
			{
				Query result = new NodeFunctions(root.TypeOfFunction, null);
				props |= QueryBuilder.Props.HasLast;
				return result;
			}
			case Function.FunctionType.FuncPosition:
			{
				Query result2 = new NodeFunctions(root.TypeOfFunction, null);
				props |= QueryBuilder.Props.HasPosition;
				return result2;
			}
			case Function.FunctionType.FuncCount:
				return new NodeFunctions(Function.FunctionType.FuncCount, this.ProcessNode(root.ArgumentList[0], QueryBuilder.Flags.None, out props));
			case Function.FunctionType.FuncID:
			{
				Query result3 = new IDQuery(this.ProcessNode(root.ArgumentList[0], QueryBuilder.Flags.None, out props));
				props |= QueryBuilder.Props.NonFlat;
				return result3;
			}
			case Function.FunctionType.FuncLocalName:
			case Function.FunctionType.FuncNameSpaceUri:
			case Function.FunctionType.FuncName:
				if (root.ArgumentList != null && root.ArgumentList.Count > 0)
				{
					return new NodeFunctions(root.TypeOfFunction, this.ProcessNode(root.ArgumentList[0], QueryBuilder.Flags.None, out props));
				}
				return new NodeFunctions(root.TypeOfFunction, null);
			case Function.FunctionType.FuncString:
			case Function.FunctionType.FuncConcat:
			case Function.FunctionType.FuncStartsWith:
			case Function.FunctionType.FuncContains:
			case Function.FunctionType.FuncSubstringBefore:
			case Function.FunctionType.FuncSubstringAfter:
			case Function.FunctionType.FuncSubstring:
			case Function.FunctionType.FuncStringLength:
			case Function.FunctionType.FuncNormalize:
			case Function.FunctionType.FuncTranslate:
				return new StringFunctions(root.TypeOfFunction, this.ProcessArguments(root.ArgumentList, out props));
			case Function.FunctionType.FuncBoolean:
			case Function.FunctionType.FuncNot:
			case Function.FunctionType.FuncLang:
				return new BooleanFunctions(root.TypeOfFunction, this.ProcessNode(root.ArgumentList[0], QueryBuilder.Flags.None, out props));
			case Function.FunctionType.FuncNumber:
			case Function.FunctionType.FuncSum:
			case Function.FunctionType.FuncFloor:
			case Function.FunctionType.FuncCeiling:
			case Function.FunctionType.FuncRound:
				if (root.ArgumentList != null && root.ArgumentList.Count > 0)
				{
					return new NumberFunctions(root.TypeOfFunction, this.ProcessNode(root.ArgumentList[0], QueryBuilder.Flags.None, out props));
				}
				return new NumberFunctions(Function.FunctionType.FuncNumber, null);
			case Function.FunctionType.FuncTrue:
			case Function.FunctionType.FuncFalse:
				return new BooleanFunctions(root.TypeOfFunction, null);
			case Function.FunctionType.FuncUserDefined:
			{
				this._needContext = true;
				if (!this._allowCurrent && root.Name == "current" && root.Prefix.Length == 0)
				{
					throw XPathException.Create("The 'current()' function cannot be used in a pattern.");
				}
				if (!this._allowKey && root.Name == "key" && root.Prefix.Length == 0)
				{
					throw XPathException.Create("'{0}' is an invalid key pattern. It either contains a variable reference or 'key()' function.", this._query);
				}
				Query result4 = new FunctionQuery(root.Prefix, root.Name, this.ProcessArguments(root.ArgumentList, out props));
				props |= QueryBuilder.Props.NonFlat;
				return result4;
			}
			default:
				throw XPathException.Create("The XPath query '{0}' is not supported.", this._query);
			}
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x00167720 File Offset: 0x00165920
		private List<Query> ProcessArguments(List<AstNode> args, out QueryBuilder.Props props)
		{
			int num = (args != null) ? args.Count : 0;
			List<Query> list = new List<Query>(num);
			props = QueryBuilder.Props.None;
			for (int i = 0; i < num; i++)
			{
				QueryBuilder.Props props2;
				list.Add(this.ProcessNode(args[i], QueryBuilder.Flags.None, out props2));
				props |= props2;
			}
			return list;
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x00167770 File Offset: 0x00165970
		private Query ProcessNode(AstNode root, QueryBuilder.Flags flags, out QueryBuilder.Props props)
		{
			int num = this._parseDepth + 1;
			this._parseDepth = num;
			if (num > 1024)
			{
				throw XPathException.Create("The xpath query is too complex.");
			}
			Query result = null;
			props = QueryBuilder.Props.None;
			switch (root.Type)
			{
			case AstNode.AstType.Axis:
				result = this.ProcessAxis((Axis)root, flags, out props);
				break;
			case AstNode.AstType.Operator:
				result = this.ProcessOperator((Operator)root, out props);
				break;
			case AstNode.AstType.Filter:
				result = this.ProcessFilter((Filter)root, flags, out props);
				break;
			case AstNode.AstType.ConstantOperand:
				result = new OperandQuery(((Operand)root).OperandValue);
				break;
			case AstNode.AstType.Function:
				result = this.ProcessFunction((Function)root, out props);
				break;
			case AstNode.AstType.Group:
				result = new GroupQuery(this.ProcessNode(((Group)root).GroupNode, QueryBuilder.Flags.None, out props));
				break;
			case AstNode.AstType.Root:
				result = new AbsoluteQuery();
				break;
			case AstNode.AstType.Variable:
				result = this.ProcessVariable((Variable)root);
				break;
			}
			this._parseDepth--;
			return result;
		}

		// Token: 0x06004169 RID: 16745 RVA: 0x0016786C File Offset: 0x00165A6C
		private Query Build(AstNode root, string query)
		{
			this.Reset();
			this._query = query;
			QueryBuilder.Props props;
			return this.ProcessNode(root, QueryBuilder.Flags.None, out props);
		}

		// Token: 0x0600416A RID: 16746 RVA: 0x00167890 File Offset: 0x00165A90
		internal Query Build(string query, bool allowVar, bool allowKey)
		{
			this._allowVar = allowVar;
			this._allowKey = allowKey;
			this._allowCurrent = true;
			return this.Build(XPathParser.ParseXPathExpression(query), query);
		}

		// Token: 0x0600416B RID: 16747 RVA: 0x001678B4 File Offset: 0x00165AB4
		internal Query Build(string query, out bool needContext)
		{
			Query result = this.Build(query, true, true);
			needContext = this._needContext;
			return result;
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x001678C7 File Offset: 0x00165AC7
		internal Query BuildPatternQuery(string query, bool allowVar, bool allowKey)
		{
			this._allowVar = allowVar;
			this._allowKey = allowKey;
			this._allowCurrent = false;
			return this.Build(XPathParser.ParseXPathPattern(query), query);
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x001678EB File Offset: 0x00165AEB
		internal Query BuildPatternQuery(string query, out bool needContext)
		{
			Query result = this.BuildPatternQuery(query, true, true);
			needContext = this._needContext;
			return result;
		}

		// Token: 0x0600416E RID: 16750 RVA: 0x0000216B File Offset: 0x0000036B
		public QueryBuilder()
		{
		}

		// Token: 0x04002E77 RID: 11895
		private string _query;

		// Token: 0x04002E78 RID: 11896
		private bool _allowVar;

		// Token: 0x04002E79 RID: 11897
		private bool _allowKey;

		// Token: 0x04002E7A RID: 11898
		private bool _allowCurrent;

		// Token: 0x04002E7B RID: 11899
		private bool _needContext;

		// Token: 0x04002E7C RID: 11900
		private BaseAxisQuery _firstInput;

		// Token: 0x04002E7D RID: 11901
		private int _parseDepth;

		// Token: 0x04002E7E RID: 11902
		private const int MaxParseDepth = 1024;

		// Token: 0x0200064A RID: 1610
		private enum Flags
		{
			// Token: 0x04002E80 RID: 11904
			None,
			// Token: 0x04002E81 RID: 11905
			SmartDesc,
			// Token: 0x04002E82 RID: 11906
			PosFilter,
			// Token: 0x04002E83 RID: 11907
			Filter = 4
		}

		// Token: 0x0200064B RID: 1611
		private enum Props
		{
			// Token: 0x04002E85 RID: 11909
			None,
			// Token: 0x04002E86 RID: 11910
			PosFilter,
			// Token: 0x04002E87 RID: 11911
			HasPosition,
			// Token: 0x04002E88 RID: 11912
			HasLast = 4,
			// Token: 0x04002E89 RID: 11913
			NonFlat = 8
		}
	}
}
