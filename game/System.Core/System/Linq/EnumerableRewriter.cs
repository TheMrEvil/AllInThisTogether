using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Linq
{
	// Token: 0x02000090 RID: 144
	internal class EnumerableRewriter : ExpressionVisitor
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x0000C044 File Offset: 0x0000A244
		protected internal override Expression VisitMethodCall(MethodCallExpression m)
		{
			Expression expression = this.Visit(m.Object);
			ReadOnlyCollection<Expression> readOnlyCollection = base.Visit(m.Arguments);
			if (expression == m.Object && readOnlyCollection == m.Arguments)
			{
				return m;
			}
			MethodInfo method = m.Method;
			Type[] typeArgs = method.IsGenericMethod ? method.GetGenericArguments() : null;
			if ((method.IsStatic || method.DeclaringType.IsAssignableFrom(expression.Type)) && EnumerableRewriter.ArgsMatch(method, readOnlyCollection, typeArgs))
			{
				return Expression.Call(expression, method, readOnlyCollection);
			}
			if (method.DeclaringType == typeof(Queryable))
			{
				MethodInfo methodInfo = EnumerableRewriter.FindEnumerableMethod(method.Name, readOnlyCollection, typeArgs);
				readOnlyCollection = this.FixupQuotedArgs(methodInfo, readOnlyCollection);
				return Expression.Call(expression, methodInfo, readOnlyCollection);
			}
			MethodInfo methodInfo2 = EnumerableRewriter.FindMethod(method.DeclaringType, method.Name, readOnlyCollection, typeArgs);
			readOnlyCollection = this.FixupQuotedArgs(methodInfo2, readOnlyCollection);
			return Expression.Call(expression, methodInfo2, readOnlyCollection);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000C12C File Offset: 0x0000A32C
		private ReadOnlyCollection<Expression> FixupQuotedArgs(MethodInfo mi, ReadOnlyCollection<Expression> argList)
		{
			ParameterInfo[] parameters = mi.GetParameters();
			if (parameters.Length != 0)
			{
				List<Expression> list = null;
				int i = 0;
				int num = parameters.Length;
				while (i < num)
				{
					Expression expression = argList[i];
					ParameterInfo parameterInfo = parameters[i];
					expression = this.FixupQuotedExpression(parameterInfo.ParameterType, expression);
					if (list == null && expression != argList[i])
					{
						list = new List<Expression>(argList.Count);
						for (int j = 0; j < i; j++)
						{
							list.Add(argList[j]);
						}
					}
					if (list != null)
					{
						list.Add(expression);
					}
					i++;
				}
				if (list != null)
				{
					argList = list.AsReadOnly();
				}
			}
			return argList;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000C1C4 File Offset: 0x0000A3C4
		private Expression FixupQuotedExpression(Type type, Expression expression)
		{
			Expression expression2 = expression;
			while (!type.IsAssignableFrom(expression2.Type))
			{
				if (expression2.NodeType != ExpressionType.Quote)
				{
					if (!type.IsAssignableFrom(expression2.Type) && type.IsArray && expression2.NodeType == ExpressionType.NewArrayInit)
					{
						Type c = EnumerableRewriter.StripExpression(expression2.Type);
						if (type.IsAssignableFrom(c))
						{
							Type elementType = type.GetElementType();
							NewArrayExpression newArrayExpression = (NewArrayExpression)expression2;
							List<Expression> list = new List<Expression>(newArrayExpression.Expressions.Count);
							int i = 0;
							int count = newArrayExpression.Expressions.Count;
							while (i < count)
							{
								list.Add(this.FixupQuotedExpression(elementType, newArrayExpression.Expressions[i]));
								i++;
							}
							expression = Expression.NewArrayInit(elementType, list);
						}
					}
					return expression;
				}
				expression2 = ((UnaryExpression)expression2).Operand;
			}
			return expression2;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000022AA File Offset: 0x000004AA
		protected internal override Expression VisitLambda<T>(Expression<T> node)
		{
			return node;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000C29C File Offset: 0x0000A49C
		private static Type GetPublicType(Type t)
		{
			if (t.IsGenericType && t.GetGenericTypeDefinition().GetInterfaces().Contains(typeof(IGrouping<, >)))
			{
				return typeof(IGrouping<, >).MakeGenericType(t.GetGenericArguments());
			}
			if (!t.IsNestedPrivate)
			{
				return t;
			}
			foreach (Type type in t.GetInterfaces())
			{
				if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				{
					return type;
				}
			}
			if (typeof(IEnumerable).IsAssignableFrom(t))
			{
				return typeof(IEnumerable);
			}
			return t;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000C348 File Offset: 0x0000A548
		private Type GetEquivalentType(Type type)
		{
			if (this._equivalentTypeCache == null)
			{
				this._equivalentTypeCache = new Dictionary<Type, Type>
				{
					{
						typeof(IQueryable),
						typeof(IEnumerable)
					},
					{
						typeof(IEnumerable),
						typeof(IEnumerable)
					}
				};
			}
			Type type2;
			if (!this._equivalentTypeCache.TryGetValue(type, out type2))
			{
				Type publicType = EnumerableRewriter.GetPublicType(type);
				if (publicType.IsInterface && publicType.IsGenericType)
				{
					Type genericTypeDefinition = publicType.GetGenericTypeDefinition();
					if (genericTypeDefinition == typeof(IOrderedEnumerable<>))
					{
						type2 = publicType;
					}
					else if (genericTypeDefinition == typeof(IOrderedQueryable<>))
					{
						type2 = typeof(IOrderedEnumerable<>).MakeGenericType(new Type[]
						{
							publicType.GenericTypeArguments[0]
						});
					}
					else if (genericTypeDefinition == typeof(IEnumerable<>))
					{
						type2 = publicType;
					}
					else if (genericTypeDefinition == typeof(IQueryable<>))
					{
						type2 = typeof(IEnumerable<>).MakeGenericType(new Type[]
						{
							publicType.GenericTypeArguments[0]
						});
					}
				}
				if (type2 == null)
				{
					var source = (from i in publicType.GetInterfaces().Select(new Func<Type, TypeInfo>(IntrospectionExtensions.GetTypeInfo)).ToArray<TypeInfo>()
					where i.IsGenericType && i.GenericTypeArguments.Length == 1
					select new
					{
						Info = i,
						GenType = i.GetGenericTypeDefinition()
					}).ToArray();
					Type type3 = (from i in source
					where i.GenType == typeof(IOrderedQueryable<>) || i.GenType == typeof(IOrderedEnumerable<>)
					select i.Info.GenericTypeArguments[0]).Distinct<Type>().SingleOrDefault<Type>();
					if (type3 != null)
					{
						type2 = typeof(IOrderedEnumerable<>).MakeGenericType(new Type[]
						{
							type3
						});
					}
					else
					{
						type3 = (from i in source
						where i.GenType == typeof(IQueryable<>) || i.GenType == typeof(IEnumerable<>)
						select i.Info.GenericTypeArguments[0]).Distinct<Type>().Single<Type>();
						type2 = typeof(IEnumerable<>).MakeGenericType(new Type[]
						{
							type3
						});
					}
				}
				this._equivalentTypeCache.Add(type, type2);
			}
			return type2;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		protected internal override Expression VisitConstant(ConstantExpression c)
		{
			EnumerableQuery enumerableQuery = c.Value as EnumerableQuery;
			if (enumerableQuery != null)
			{
				if (enumerableQuery.Enumerable != null)
				{
					Type publicType = EnumerableRewriter.GetPublicType(enumerableQuery.Enumerable.GetType());
					return Expression.Constant(enumerableQuery.Enumerable, publicType);
				}
				Expression expression = enumerableQuery.Expression;
				if (expression != c)
				{
					return this.Visit(expression);
				}
			}
			return c;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000C630 File Offset: 0x0000A830
		[PreserveDependency("DefaultIfEmpty`1", "System.Linq.Enumerable")]
		[PreserveDependency("Count`1", "System.Linq.Enumerable")]
		[PreserveDependency("Contains`1", "System.Linq.Enumerable")]
		[PreserveDependency("Concat`1", "System.Linq.Enumerable")]
		[PreserveDependency("Cast`1", "System.Linq.Enumerable")]
		[PreserveDependency("Average`1", "System.Linq.Enumerable")]
		[PreserveDependency("Aggregate`1", "System.Linq.Enumerable")]
		[PreserveDependency("Append`1", "System.Linq.Enumerable")]
		[PreserveDependency("Any`1", "System.Linq.Enumerable")]
		[PreserveDependency("All`1", "System.Linq.Enumerable")]
		[PreserveDependency("Aggregate`3", "System.Linq.Enumerable")]
		[PreserveDependency("Aggregate`2", "System.Linq.Enumerable")]
		[PreserveDependency("GroupJoin`4", "System.Linq.Enumerable")]
		[PreserveDependency("Average", "System.Linq.Enumerable")]
		[PreserveDependency("Distinct`1", "System.Linq.Enumerable")]
		[PreserveDependency("Intersect`1", "System.Linq.Enumerable")]
		[PreserveDependency("Sum", "System.Linq.Enumerable")]
		[PreserveDependency("SkipWhile`1", "System.Linq.Enumerable")]
		[PreserveDependency("Except`1", "System.Linq.Enumerable")]
		[PreserveDependency("Sum`1", "System.Linq.Enumerable")]
		[PreserveDependency("Take`1", "System.Linq.Enumerable")]
		[PreserveDependency("TakeLast`1", "System.Linq.Enumerable")]
		[PreserveDependency("TakeWhile`1", "System.Linq.Enumerable")]
		[PreserveDependency("ThenBy`2", "System.Linq.Enumerable")]
		[PreserveDependency("ThenByDescending`2", "System.Linq.Enumerable")]
		[PreserveDependency("Union`1", "System.Linq.Enumerable")]
		[PreserveDependency("Where`1", "System.Linq.Enumerable")]
		[PreserveDependency("Zip`3", "System.Linq.Enumerable")]
		[PreserveDependency("GroupBy`4", "System.Linq.Enumerable")]
		[PreserveDependency("GroupBy`3", "System.Linq.Enumerable")]
		[PreserveDependency("GroupBy`2", "System.Linq.Enumerable")]
		[PreserveDependency("FirstOrDefault`1", "System.Linq.Enumerable")]
		[PreserveDependency("ElementAt`1", "System.Linq.Enumerable")]
		[PreserveDependency("First`1", "System.Linq.Enumerable")]
		[PreserveDependency("SkipLast`1", "System.Linq.Enumerable")]
		[PreserveDependency("Skip`1", "System.Linq.Enumerable")]
		[PreserveDependency("ElementAtOrDefault`1", "System.Linq.Enumerable")]
		[PreserveDependency("Single`1", "System.Linq.Enumerable")]
		[PreserveDependency("Join`4", "System.Linq.Enumerable")]
		[PreserveDependency("Last`1", "System.Linq.Enumerable")]
		[PreserveDependency("LastOrDefault`1", "System.Linq.Enumerable")]
		[PreserveDependency("LongCount`1", "System.Linq.Enumerable")]
		[PreserveDependency("SingleOrDefault`1", "System.Linq.Enumerable")]
		[PreserveDependency("Max`2", "System.Linq.Enumerable")]
		[PreserveDependency("Min`1", "System.Linq.Enumerable")]
		[PreserveDependency("Min`2", "System.Linq.Enumerable")]
		[PreserveDependency("Max`1", "System.Linq.Enumerable")]
		[PreserveDependency("OrderBy`2", "System.Linq.Enumerable")]
		[PreserveDependency("OrderByDescending`2", "System.Linq.Enumerable")]
		[PreserveDependency("Prepend`1", "System.Linq.Enumerable")]
		[PreserveDependency("Reverse`1", "System.Linq.Enumerable")]
		[PreserveDependency("Select`2", "System.Linq.Enumerable")]
		[PreserveDependency("SequenceEqual`1", "System.Linq.Enumerable")]
		[PreserveDependency("SelectMany`3", "System.Linq.Enumerable")]
		[PreserveDependency("SelectMany`2", "System.Linq.Enumerable")]
		[PreserveDependency("OfType`1", "System.Linq.Enumerable")]
		private static MethodInfo FindEnumerableMethod(string name, ReadOnlyCollection<Expression> args, params Type[] typeArgs)
		{
			if (EnumerableRewriter.s_seqMethods == null)
			{
				EnumerableRewriter.s_seqMethods = typeof(Enumerable).GetStaticMethods().ToLookup((MethodInfo m) => m.Name);
			}
			MethodInfo methodInfo = EnumerableRewriter.s_seqMethods[name].FirstOrDefault((MethodInfo m) => EnumerableRewriter.ArgsMatch(m, args, typeArgs));
			if (typeArgs != null)
			{
				return methodInfo.MakeGenericMethod(typeArgs);
			}
			return methodInfo;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000C6C4 File Offset: 0x0000A8C4
		private static MethodInfo FindMethod(Type type, string name, ReadOnlyCollection<Expression> args, Type[] typeArgs)
		{
			using (IEnumerator<MethodInfo> enumerator = (from m in type.GetStaticMethods()
			where m.Name == name
			select m).GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw Error.NoMethodOnType(name, type);
				}
				MethodInfo methodInfo;
				for (;;)
				{
					methodInfo = enumerator.Current;
					if (EnumerableRewriter.ArgsMatch(methodInfo, args, typeArgs))
					{
						break;
					}
					if (!enumerator.MoveNext())
					{
						goto Block_6;
					}
				}
				return (typeArgs != null) ? methodInfo.MakeGenericMethod(typeArgs) : methodInfo;
				Block_6:;
			}
			throw Error.NoMethodOnTypeMatchingArguments(name, type);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000C764 File Offset: 0x0000A964
		private static bool ArgsMatch(MethodInfo m, ReadOnlyCollection<Expression> args, Type[] typeArgs)
		{
			ParameterInfo[] parameters = m.GetParameters();
			if (parameters.Length != args.Count)
			{
				return false;
			}
			if (!m.IsGenericMethod && typeArgs != null && typeArgs.Length != 0)
			{
				return false;
			}
			if (!m.IsGenericMethodDefinition && m.IsGenericMethod && m.ContainsGenericParameters)
			{
				m = m.GetGenericMethodDefinition();
			}
			if (m.IsGenericMethodDefinition)
			{
				if (typeArgs == null || typeArgs.Length == 0)
				{
					return false;
				}
				if (m.GetGenericArguments().Length != typeArgs.Length)
				{
					return false;
				}
				m = m.MakeGenericMethod(typeArgs);
				parameters = m.GetParameters();
			}
			int i = 0;
			int count = args.Count;
			while (i < count)
			{
				Type type = parameters[i].ParameterType;
				if (type == null)
				{
					return false;
				}
				if (type.IsByRef)
				{
					type = type.GetElementType();
				}
				Expression expression = args[i];
				if (!type.IsAssignableFrom(expression.Type))
				{
					if (expression.NodeType == ExpressionType.Quote)
					{
						expression = ((UnaryExpression)expression).Operand;
					}
					if (!type.IsAssignableFrom(expression.Type) && !type.IsAssignableFrom(EnumerableRewriter.StripExpression(expression.Type)))
					{
						return false;
					}
				}
				i++;
			}
			return true;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000C878 File Offset: 0x0000AA78
		private static Type StripExpression(Type type)
		{
			bool isArray = type.IsArray;
			Type type2 = isArray ? type.GetElementType() : type;
			Type type3 = TypeHelper.FindGenericType(typeof(Expression<>), type2);
			if (type3 != null)
			{
				type2 = type3.GetGenericArguments()[0];
			}
			if (!isArray)
			{
				return type;
			}
			int arrayRank = type.GetArrayRank();
			if (arrayRank != 1)
			{
				return type2.MakeArrayType(arrayRank);
			}
			return type2.MakeArrayType();
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000C8D8 File Offset: 0x0000AAD8
		protected internal override Expression VisitConditional(ConditionalExpression c)
		{
			Type type = c.Type;
			if (!typeof(IQueryable).IsAssignableFrom(type))
			{
				return base.VisitConditional(c);
			}
			Expression test = this.Visit(c.Test);
			Expression expression = this.Visit(c.IfTrue);
			Expression expression2 = this.Visit(c.IfFalse);
			Type type2 = expression.Type;
			Type type3 = expression2.Type;
			if (type2.IsAssignableFrom(type3))
			{
				return Expression.Condition(test, expression, expression2, type2);
			}
			if (type3.IsAssignableFrom(type2))
			{
				return Expression.Condition(test, expression, expression2, type3);
			}
			return Expression.Condition(test, expression, expression2, this.GetEquivalentType(type));
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000C978 File Offset: 0x0000AB78
		protected internal override Expression VisitBlock(BlockExpression node)
		{
			Type type = node.Type;
			if (!typeof(IQueryable).IsAssignableFrom(type))
			{
				return base.VisitBlock(node);
			}
			ReadOnlyCollection<Expression> expressions = base.Visit(node.Expressions);
			ReadOnlyCollection<ParameterExpression> variables = base.VisitAndConvert<ParameterExpression>(node.Variables, "EnumerableRewriter.VisitBlock");
			if (type == node.Expressions.Last<Expression>().Type)
			{
				return Expression.Block(variables, expressions);
			}
			return Expression.Block(this.GetEquivalentType(type), variables, expressions);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000C9F4 File Offset: 0x0000ABF4
		protected internal override Expression VisitGoto(GotoExpression node)
		{
			Type type = node.Value.Type;
			if (!typeof(IQueryable).IsAssignableFrom(type))
			{
				return base.VisitGoto(node);
			}
			LabelTarget target = this.VisitLabelTarget(node.Target);
			Expression expression = this.Visit(node.Value);
			return Expression.MakeGoto(node.Kind, target, expression, this.GetEquivalentType(typeof(EnumerableQuery).IsAssignableFrom(type) ? expression.Type : type));
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000CA70 File Offset: 0x0000AC70
		protected override LabelTarget VisitLabelTarget(LabelTarget node)
		{
			LabelTarget labelTarget;
			if (this._targetCache == null)
			{
				this._targetCache = new Dictionary<LabelTarget, LabelTarget>();
			}
			else if (this._targetCache.TryGetValue(node, out labelTarget))
			{
				return labelTarget;
			}
			Type type = node.Type;
			if (!typeof(IQueryable).IsAssignableFrom(type))
			{
				labelTarget = base.VisitLabelTarget(node);
			}
			else
			{
				labelTarget = Expression.Label(this.GetEquivalentType(type), node.Name);
			}
			this._targetCache.Add(node, labelTarget);
			return labelTarget;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000CAE8 File Offset: 0x0000ACE8
		public EnumerableRewriter()
		{
		}

		// Token: 0x0400044A RID: 1098
		private Dictionary<LabelTarget, LabelTarget> _targetCache;

		// Token: 0x0400044B RID: 1099
		private Dictionary<Type, Type> _equivalentTypeCache;

		// Token: 0x0400044C RID: 1100
		private static ILookup<string, MethodInfo> s_seqMethods;

		// Token: 0x02000091 RID: 145
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000434 RID: 1076 RVA: 0x0000CAF0 File Offset: 0x0000ACF0
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000435 RID: 1077 RVA: 0x00002162 File Offset: 0x00000362
			public <>c()
			{
			}

			// Token: 0x06000436 RID: 1078 RVA: 0x0000CAFC File Offset: 0x0000ACFC
			internal bool <GetEquivalentType>b__7_0(TypeInfo i)
			{
				return i.IsGenericType && i.GenericTypeArguments.Length == 1;
			}

			// Token: 0x06000437 RID: 1079 RVA: 0x0000CB13 File Offset: 0x0000AD13
			internal <>f__AnonymousType0<TypeInfo, Type> <GetEquivalentType>b__7_1(TypeInfo i)
			{
				return new
				{
					Info = i,
					GenType = i.GetGenericTypeDefinition()
				};
			}

			// Token: 0x06000438 RID: 1080 RVA: 0x0000CB21 File Offset: 0x0000AD21
			internal bool <GetEquivalentType>b__7_2(<>f__AnonymousType0<TypeInfo, Type> i)
			{
				return i.GenType == typeof(IOrderedQueryable<>) || i.GenType == typeof(IOrderedEnumerable<>);
			}

			// Token: 0x06000439 RID: 1081 RVA: 0x0000CB51 File Offset: 0x0000AD51
			internal Type <GetEquivalentType>b__7_3(<>f__AnonymousType0<TypeInfo, Type> i)
			{
				return i.Info.GenericTypeArguments[0];
			}

			// Token: 0x0600043A RID: 1082 RVA: 0x0000CB60 File Offset: 0x0000AD60
			internal bool <GetEquivalentType>b__7_4(<>f__AnonymousType0<TypeInfo, Type> i)
			{
				return i.GenType == typeof(IQueryable<>) || i.GenType == typeof(IEnumerable<>);
			}

			// Token: 0x0600043B RID: 1083 RVA: 0x0000CB51 File Offset: 0x0000AD51
			internal Type <GetEquivalentType>b__7_5(<>f__AnonymousType0<TypeInfo, Type> i)
			{
				return i.Info.GenericTypeArguments[0];
			}

			// Token: 0x0600043C RID: 1084 RVA: 0x0000CB90 File Offset: 0x0000AD90
			internal string <FindEnumerableMethod>b__10_0(MethodInfo m)
			{
				return m.Name;
			}

			// Token: 0x0400044D RID: 1101
			public static readonly EnumerableRewriter.<>c <>9 = new EnumerableRewriter.<>c();

			// Token: 0x0400044E RID: 1102
			public static Func<TypeInfo, bool> <>9__7_0;

			// Token: 0x0400044F RID: 1103
			public static Func<TypeInfo, <>f__AnonymousType0<TypeInfo, Type>> <>9__7_1;

			// Token: 0x04000450 RID: 1104
			public static Func<<>f__AnonymousType0<TypeInfo, Type>, bool> <>9__7_2;

			// Token: 0x04000451 RID: 1105
			public static Func<<>f__AnonymousType0<TypeInfo, Type>, Type> <>9__7_3;

			// Token: 0x04000452 RID: 1106
			public static Func<<>f__AnonymousType0<TypeInfo, Type>, bool> <>9__7_4;

			// Token: 0x04000453 RID: 1107
			public static Func<<>f__AnonymousType0<TypeInfo, Type>, Type> <>9__7_5;

			// Token: 0x04000454 RID: 1108
			public static Func<MethodInfo, string> <>9__10_0;
		}

		// Token: 0x02000092 RID: 146
		[CompilerGenerated]
		private sealed class <>c__DisplayClass10_0
		{
			// Token: 0x0600043D RID: 1085 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__DisplayClass10_0()
			{
			}

			// Token: 0x0600043E RID: 1086 RVA: 0x0000CB98 File Offset: 0x0000AD98
			internal bool <FindEnumerableMethod>b__1(MethodInfo m)
			{
				return EnumerableRewriter.ArgsMatch(m, this.args, this.typeArgs);
			}

			// Token: 0x04000455 RID: 1109
			public ReadOnlyCollection<Expression> args;

			// Token: 0x04000456 RID: 1110
			public Type[] typeArgs;
		}

		// Token: 0x02000093 RID: 147
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_0
		{
			// Token: 0x0600043F RID: 1087 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__DisplayClass11_0()
			{
			}

			// Token: 0x06000440 RID: 1088 RVA: 0x0000CBAC File Offset: 0x0000ADAC
			internal bool <FindMethod>b__0(MethodInfo m)
			{
				return m.Name == this.name;
			}

			// Token: 0x04000457 RID: 1111
			public string name;
		}
	}
}
