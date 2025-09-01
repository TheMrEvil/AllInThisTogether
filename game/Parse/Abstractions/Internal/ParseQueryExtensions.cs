using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Parse.Infrastructure.Data;

namespace Parse.Abstractions.Internal
{
	// Token: 0x02000073 RID: 115
	public static class ParseQueryExtensions
	{
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00011C8B File Offset: 0x0000FE8B
		private static MethodInfo ParseObjectGetMethod
		{
			[CompilerGenerated]
			get
			{
				return ParseQueryExtensions.<ParseObjectGetMethod>k__BackingField;
			}
		} = ParseQueryExtensions.GetMethod<ParseObject>((ParseObject target) => target.Get<int>(null)).GetGenericMethodDefinition();

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00011C92 File Offset: 0x0000FE92
		private static MethodInfo StringContainsMethod
		{
			[CompilerGenerated]
			get
			{
				return ParseQueryExtensions.<StringContainsMethod>k__BackingField;
			}
		} = ParseQueryExtensions.GetMethod<string>((string text) => text.Contains(null));

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00011C99 File Offset: 0x0000FE99
		private static MethodInfo StringStartsWithMethod
		{
			[CompilerGenerated]
			get
			{
				return ParseQueryExtensions.<StringStartsWithMethod>k__BackingField;
			}
		} = ParseQueryExtensions.GetMethod<string>((string text) => text.StartsWith(null));

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x00011CA0 File Offset: 0x0000FEA0
		private static MethodInfo StringEndsWithMethod
		{
			[CompilerGenerated]
			get
			{
				return ParseQueryExtensions.<StringEndsWithMethod>k__BackingField;
			}
		} = ParseQueryExtensions.GetMethod<string>((string text) => text.EndsWith(null));

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00011CA7 File Offset: 0x0000FEA7
		private static MethodInfo ContainsMethod
		{
			[CompilerGenerated]
			get
			{
				return ParseQueryExtensions.<ContainsMethod>k__BackingField;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00011CAE File Offset: 0x0000FEAE
		private static MethodInfo NotContainsMethod
		{
			[CompilerGenerated]
			get
			{
				return ParseQueryExtensions.<NotContainsMethod>k__BackingField;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00011CB5 File Offset: 0x0000FEB5
		private static MethodInfo ContainsKeyMethod
		{
			[CompilerGenerated]
			get
			{
				return ParseQueryExtensions.<ContainsKeyMethod>k__BackingField;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00011CBC File Offset: 0x0000FEBC
		private static MethodInfo NotContainsKeyMethod
		{
			[CompilerGenerated]
			get
			{
				return ParseQueryExtensions.<NotContainsKeyMethod>k__BackingField;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00011CC3 File Offset: 0x0000FEC3
		private static Dictionary<MethodInfo, MethodInfo> Mappings
		{
			[CompilerGenerated]
			get
			{
				return ParseQueryExtensions.<Mappings>k__BackingField;
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00011CCC File Offset: 0x0000FECC
		static ParseQueryExtensions()
		{
			Dictionary<MethodInfo, MethodInfo> dictionary = new Dictionary<MethodInfo, MethodInfo>();
			MethodInfo stringContainsMethod = ParseQueryExtensions.StringContainsMethod;
			dictionary[stringContainsMethod] = ParseQueryExtensions.GetMethod<ParseQuery<ParseObject>>((ParseQuery<ParseObject> query) => query.WhereContains(null, null));
			MethodInfo stringStartsWithMethod = ParseQueryExtensions.StringStartsWithMethod;
			dictionary[stringStartsWithMethod] = ParseQueryExtensions.GetMethod<ParseQuery<ParseObject>>((ParseQuery<ParseObject> query) => query.WhereStartsWith(null, null));
			MethodInfo stringEndsWithMethod = ParseQueryExtensions.StringEndsWithMethod;
			dictionary[stringEndsWithMethod] = ParseQueryExtensions.GetMethod<ParseQuery<ParseObject>>((ParseQuery<ParseObject> query) => query.WhereEndsWith(null, null));
			ParseQueryExtensions.Mappings = dictionary;
			ParseQueryExtensions.ContainsMethod = ParseQueryExtensions.GetMethod<object>((object o) => ParseQueryExtensions.ContainsStub<object>(null, null)).GetGenericMethodDefinition();
			ParseQueryExtensions.NotContainsMethod = ParseQueryExtensions.GetMethod<object>((object o) => ParseQueryExtensions.NotContainsStub<object>(null, null)).GetGenericMethodDefinition();
			ParseQueryExtensions.ContainsKeyMethod = ParseQueryExtensions.GetMethod<object>((object o) => ParseQueryExtensions.ContainsKeyStub(null, null));
			ParseQueryExtensions.NotContainsKeyMethod = ParseQueryExtensions.GetMethod<object>((object o) => ParseQueryExtensions.NotContainsKeyStub(null, null));
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00012196 File Offset: 0x00010396
		private static MethodInfo GetMethod<T>(Expression<Action<T>> expression)
		{
			return (expression.Body as MethodCallExpression).Method;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000121A8 File Offset: 0x000103A8
		private static bool ContainsStub<T>(object collection, T value)
		{
			throw new NotImplementedException("Exists only for expression translation as a placeholder.");
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x000121B4 File Offset: 0x000103B4
		private static bool NotContainsStub<T>(object collection, T value)
		{
			throw new NotImplementedException("Exists only for expression translation as a placeholder.");
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000121C0 File Offset: 0x000103C0
		private static bool ContainsKeyStub(ParseObject obj, string key)
		{
			throw new NotImplementedException("Exists only for expression translation as a placeholder.");
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000121CC File Offset: 0x000103CC
		private static bool NotContainsKeyStub(ParseObject obj, string key)
		{
			throw new NotImplementedException("Exists only for expression translation as a placeholder.");
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x000121D8 File Offset: 0x000103D8
		private static object GetValue(Expression exp)
		{
			object result;
			try
			{
				result = Expression.Lambda(typeof(Func<>).MakeGenericType(new Type[]
				{
					exp.Type
				}), exp, Array.Empty<ParameterExpression>()).Compile().DynamicInvoke(Array.Empty<object>());
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException("Unable to evaluate expression: " + ((exp != null) ? exp.ToString() : null), innerException);
			}
			return result;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00012250 File Offset: 0x00010450
		private static bool IsParseObjectGet(MethodCallExpression node)
		{
			return node != null && node.Object != null && typeof(ParseObject).GetTypeInfo().IsAssignableFrom(node.Object.Type.GetTypeInfo()) && node.Method.IsGenericMethod && node.Method.GetGenericMethodDefinition() == ParseQueryExtensions.ParseObjectGetMethod;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000122B4 File Offset: 0x000104B4
		private static ParseQuery<T> WhereMethodCall<T>(this ParseQuery<T> source, Expression<Func<T, bool>> expression, MethodCallExpression node) where T : ParseObject
		{
			if (ParseQueryExtensions.IsParseObjectGet(node) && (node.Type == typeof(bool) || node.Type == typeof(bool?)))
			{
				return source.WhereEqualTo(ParseQueryExtensions.GetValue(node.Arguments[0]) as string, true);
			}
			MethodInfo methodInfo;
			if (!ParseQueryExtensions.Mappings.TryGetValue(node.Method, out methodInfo))
			{
				if (node.Arguments[0] == expression.Parameters[0])
				{
					if (node.Method == ParseQueryExtensions.ContainsKeyMethod)
					{
						return source.WhereExists(ParseQueryExtensions.GetValue(node.Arguments[1]) as string);
					}
					if (node.Method == ParseQueryExtensions.NotContainsKeyMethod)
					{
						return source.WhereDoesNotExist(ParseQueryExtensions.GetValue(node.Arguments[1]) as string);
					}
				}
				if (node.Method.IsGenericMethod)
				{
					if (node.Method.GetGenericMethodDefinition() == ParseQueryExtensions.ContainsMethod)
					{
						if (ParseQueryExtensions.IsParseObjectGet(node.Arguments[0] as MethodCallExpression))
						{
							return source.WhereEqualTo(ParseQueryExtensions.GetValue(((MethodCallExpression)node.Arguments[0]).Arguments[0]) as string, ParseQueryExtensions.GetValue(node.Arguments[1]));
						}
						if (ParseQueryExtensions.IsParseObjectGet(node.Arguments[1] as MethodCallExpression))
						{
							return source.WhereContainedIn<object>(ParseQueryExtensions.GetValue(((MethodCallExpression)node.Arguments[1]).Arguments[0]) as string, (ParseQueryExtensions.GetValue(node.Arguments[0]) as IEnumerable).Cast<object>());
						}
					}
					if (node.Method.GetGenericMethodDefinition() == ParseQueryExtensions.NotContainsMethod)
					{
						if (ParseQueryExtensions.IsParseObjectGet(node.Arguments[0] as MethodCallExpression))
						{
							return source.WhereNotEqualTo(ParseQueryExtensions.GetValue(((MethodCallExpression)node.Arguments[0]).Arguments[0]) as string, ParseQueryExtensions.GetValue(node.Arguments[1]));
						}
						if (ParseQueryExtensions.IsParseObjectGet(node.Arguments[1] as MethodCallExpression))
						{
							return source.WhereNotContainedIn<object>(ParseQueryExtensions.GetValue(((MethodCallExpression)node.Arguments[1]).Arguments[0]) as string, (ParseQueryExtensions.GetValue(node.Arguments[0]) as IEnumerable).Cast<object>());
						}
					}
				}
				MethodInfo method = node.Method;
				throw new InvalidOperationException(((method != null) ? method.ToString() : null) + " is not a supported method call in a where expression.");
			}
			MethodCallExpression methodCallExpression = new ParseQueryExtensions.ObjectNormalizer().Visit(node.Object) as MethodCallExpression;
			if (!ParseQueryExtensions.IsParseObjectGet(methodCallExpression) || methodCallExpression.Object != expression.Parameters[0])
			{
				throw new InvalidOperationException("The left-hand side of a supported function call must be a ParseObject field access.");
			}
			return methodInfo.DeclaringType.GetGenericTypeDefinition().MakeGenericType(new Type[]
			{
				typeof(T)
			}).GetRuntimeMethod(methodInfo.Name, (from parameter in methodInfo.GetParameters()
			select parameter.ParameterType).ToArray<Type>()).Invoke(source, new object[]
			{
				ParseQueryExtensions.GetValue(methodCallExpression.Arguments[0]),
				ParseQueryExtensions.GetValue(node.Arguments[0])
			}) as ParseQuery<T>;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00012650 File Offset: 0x00010850
		private static ParseQuery<T> WhereBinaryExpression<T>(this ParseQuery<T> source, Expression<Func<T, bool>> expression, BinaryExpression node) where T : ParseObject
		{
			MethodCallExpression methodCallExpression = new ParseQueryExtensions.ObjectNormalizer().Visit(node.Left) as MethodCallExpression;
			if (!ParseQueryExtensions.IsParseObjectGet(methodCallExpression) || methodCallExpression.Object != expression.Parameters[0])
			{
				throw new InvalidOperationException("Where expressions must have one side be a field operation on a ParseObject.");
			}
			string key = ParseQueryExtensions.GetValue(methodCallExpression.Arguments[0]) as string;
			object value = ParseQueryExtensions.GetValue(node.Right);
			if (value != null && !ParseDataEncoder.Validate(value))
			{
				throw new InvalidOperationException("Where clauses must use types compatible with ParseObjects.");
			}
			ExpressionType nodeType = node.NodeType;
			switch (nodeType)
			{
			case ExpressionType.Equal:
				return source.WhereEqualTo(key, value);
			case ExpressionType.ExclusiveOr:
			case ExpressionType.Invoke:
			case ExpressionType.Lambda:
			case ExpressionType.LeftShift:
				break;
			case ExpressionType.GreaterThan:
				return source.WhereGreaterThan(key, value);
			case ExpressionType.GreaterThanOrEqual:
				return source.WhereGreaterThanOrEqualTo(key, value);
			case ExpressionType.LessThan:
				return source.WhereLessThan(key, value);
			case ExpressionType.LessThanOrEqual:
				return source.WhereLessThanOrEqualTo(key, value);
			default:
				if (nodeType == ExpressionType.NotEqual)
				{
					return source.WhereNotEqualTo(key, value);
				}
				break;
			}
			throw new InvalidOperationException("Where expressions do not support this operator.");
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00012760 File Offset: 0x00010960
		public static ParseQuery<TSource> Where<TSource>(this ParseQuery<TSource> source, Expression<Func<TSource, bool>> predicate) where TSource : ParseObject
		{
			BinaryExpression binaryExpression = predicate.Body as BinaryExpression;
			if (binaryExpression != null)
			{
				if (binaryExpression.NodeType == ExpressionType.AndAlso)
				{
					return source.Where(Expression.Lambda<Func<TSource, bool>>(binaryExpression.Left, predicate.Parameters)).Where(Expression.Lambda<Func<TSource, bool>>(binaryExpression.Right, predicate.Parameters));
				}
				if (binaryExpression.NodeType == ExpressionType.OrElse)
				{
					return source.Services.ConstructOrQuery(source.Where(Expression.Lambda<Func<TSource, bool>>(binaryExpression.Left, predicate.Parameters)), new ParseQuery<TSource>[]
					{
						source.Where(Expression.Lambda<Func<TSource, bool>>(binaryExpression.Right, predicate.Parameters))
					});
				}
			}
			Expression expression = new ParseQueryExtensions.WhereNormalizer().Visit(predicate.Body);
			MethodCallExpression methodCallExpression = expression as MethodCallExpression;
			if (methodCallExpression != null)
			{
				return source.WhereMethodCall(predicate, methodCallExpression);
			}
			BinaryExpression binaryExpression2 = expression as BinaryExpression;
			if (binaryExpression2 != null)
			{
				return source.WhereBinaryExpression(predicate, binaryExpression2);
			}
			UnaryExpression unaryExpression = expression as UnaryExpression;
			if (unaryExpression != null && unaryExpression.NodeType == ExpressionType.Not)
			{
				MethodCallExpression methodCallExpression2 = unaryExpression.Operand as MethodCallExpression;
				if (methodCallExpression2 != null)
				{
					Type type = unaryExpression.Type;
					if (ParseQueryExtensions.IsParseObjectGet(methodCallExpression2) && (type == typeof(bool) || type == typeof(bool?)))
					{
						return source.WhereNotEqualTo(ParseQueryExtensions.GetValue(methodCallExpression2.Arguments[0]) as string, true);
					}
				}
			}
			throw new InvalidOperationException("Encountered an unsupported expression for ParseQueries.");
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000128CC File Offset: 0x00010ACC
		private static string GetOrderByPath<TSource, TSelector>(Expression<Func<TSource, TSelector>> keySelector)
		{
			string text = null;
			MethodCallExpression methodCallExpression = new ParseQueryExtensions.ObjectNormalizer().Visit(keySelector.Body) as MethodCallExpression;
			if (ParseQueryExtensions.IsParseObjectGet(methodCallExpression) && methodCallExpression.Object == keySelector.Parameters[0])
			{
				text = (ParseQueryExtensions.GetValue(methodCallExpression.Arguments[0]) as string);
			}
			if (text == null)
			{
				throw new InvalidOperationException("OrderBy expression must be a field access on a ParseObject.");
			}
			return text;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00012933 File Offset: 0x00010B33
		public static ParseQuery<TSource> OrderBy<TSource, TSelector>(this ParseQuery<TSource> source, Expression<Func<TSource, TSelector>> keySelector) where TSource : ParseObject
		{
			return source.OrderBy(ParseQueryExtensions.GetOrderByPath<TSource, TSelector>(keySelector));
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00012941 File Offset: 0x00010B41
		public static ParseQuery<TSource> OrderByDescending<TSource, TSelector>(this ParseQuery<TSource> source, Expression<Func<TSource, TSelector>> keySelector) where TSource : ParseObject
		{
			return source.OrderByDescending(ParseQueryExtensions.GetOrderByPath<TSource, TSelector>(keySelector));
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001294F File Offset: 0x00010B4F
		public static ParseQuery<TSource> ThenBy<TSource, TSelector>(this ParseQuery<TSource> source, Expression<Func<TSource, TSelector>> keySelector) where TSource : ParseObject
		{
			return source.ThenBy(ParseQueryExtensions.GetOrderByPath<TSource, TSelector>(keySelector));
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001295D File Offset: 0x00010B5D
		public static ParseQuery<TSource> ThenByDescending<TSource, TSelector>(this ParseQuery<TSource> source, Expression<Func<TSource, TSelector>> keySelector) where TSource : ParseObject
		{
			return source.ThenByDescending(ParseQueryExtensions.GetOrderByPath<TSource, TSelector>(keySelector));
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001296C File Offset: 0x00010B6C
		public static ParseQuery<TResult> Join<TOuter, TInner, TKey, TResult>(this ParseQuery<TOuter> outer, ParseQuery<TInner> inner, Expression<Func<TOuter, TKey>> outerKeySelector, Expression<Func<TInner, TKey>> innerKeySelector, Expression<Func<TOuter, TInner, TResult>> resultSelector) where TOuter : ParseObject where TInner : ParseObject where TResult : ParseObject
		{
			if (resultSelector.Body == resultSelector.Parameters[1])
			{
				return inner.Join(outer, innerKeySelector, outerKeySelector, (TInner i, TOuter o) => i) as ParseQuery<TResult>;
			}
			if (resultSelector.Body != resultSelector.Parameters[0])
			{
				throw new InvalidOperationException("Joins must select either the outer or inner object.");
			}
			Expression expression = new ParseQueryExtensions.ObjectNormalizer().Visit(outerKeySelector.Body);
			Expression expression2 = new ParseQueryExtensions.ObjectNormalizer().Visit(innerKeySelector.Body);
			MethodCallExpression methodCallExpression = expression as MethodCallExpression;
			MethodCallExpression methodCallExpression2 = expression2 as MethodCallExpression;
			if (!ParseQueryExtensions.IsParseObjectGet(methodCallExpression) || methodCallExpression.Object != outerKeySelector.Parameters[0])
			{
				throw new InvalidOperationException("The key for the selected object must be a field access on the ParseObject.");
			}
			string key = ParseQueryExtensions.GetValue(methodCallExpression.Arguments[0]) as string;
			if (ParseQueryExtensions.IsParseObjectGet(methodCallExpression2) && methodCallExpression2.Object == innerKeySelector.Parameters[0])
			{
				return outer.WhereMatchesKeyInQuery<TInner>(key, ParseQueryExtensions.GetValue(methodCallExpression2.Arguments[0]) as string, inner) as ParseQuery<TResult>;
			}
			if (innerKeySelector.Body == innerKeySelector.Parameters[0])
			{
				return outer.WhereMatchesQuery<TInner>(key, inner) as ParseQuery<TResult>;
			}
			throw new InvalidOperationException("The key for the joined object must be a ParseObject or a field access on the ParseObject.");
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00012AE0 File Offset: 0x00010CE0
		public static string GetClassName<T>(this ParseQuery<T> query) where T : ParseObject
		{
			return query.ClassName;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00012AE8 File Offset: 0x00010CE8
		public static IDictionary<string, object> BuildParameters<T>(this ParseQuery<T> query) where T : ParseObject
		{
			return query.BuildParameters(false);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00012AF1 File Offset: 0x00010CF1
		public static object GetConstraint<T>(this ParseQuery<T> query, string key) where T : ParseObject
		{
			return query.GetConstraint(key);
		}

		// Token: 0x040000FF RID: 255
		[CompilerGenerated]
		private static readonly MethodInfo <ParseObjectGetMethod>k__BackingField;

		// Token: 0x04000100 RID: 256
		[CompilerGenerated]
		private static readonly MethodInfo <StringContainsMethod>k__BackingField;

		// Token: 0x04000101 RID: 257
		[CompilerGenerated]
		private static readonly MethodInfo <StringStartsWithMethod>k__BackingField;

		// Token: 0x04000102 RID: 258
		[CompilerGenerated]
		private static readonly MethodInfo <StringEndsWithMethod>k__BackingField;

		// Token: 0x04000103 RID: 259
		[CompilerGenerated]
		private static readonly MethodInfo <ContainsMethod>k__BackingField;

		// Token: 0x04000104 RID: 260
		[CompilerGenerated]
		private static readonly MethodInfo <NotContainsMethod>k__BackingField;

		// Token: 0x04000105 RID: 261
		[CompilerGenerated]
		private static readonly MethodInfo <ContainsKeyMethod>k__BackingField;

		// Token: 0x04000106 RID: 262
		[CompilerGenerated]
		private static readonly MethodInfo <NotContainsKeyMethod>k__BackingField;

		// Token: 0x04000107 RID: 263
		[CompilerGenerated]
		private static readonly Dictionary<MethodInfo, MethodInfo> <Mappings>k__BackingField;

		// Token: 0x0200013F RID: 319
		private class ObjectNormalizer : ExpressionVisitor
		{
			// Token: 0x0600080C RID: 2060 RVA: 0x00017DA0 File Offset: 0x00015FA0
			protected override Expression VisitIndex(IndexExpression node)
			{
				MethodCallExpression methodCallExpression = this.Visit(node.Object) as MethodCallExpression;
				if (!ParseQueryExtensions.IsParseObjectGet(methodCallExpression))
				{
					return base.VisitIndex(node);
				}
				string text = ParseQueryExtensions.GetValue(node.Arguments[0]) as string;
				if (text == null)
				{
					throw new InvalidOperationException("Index must be a string");
				}
				return Expression.Call(methodCallExpression.Object, ParseQueryExtensions.ParseObjectGetMethod.MakeGenericMethod(new Type[]
				{
					node.Type
				}), new Expression[]
				{
					Expression.Constant(string.Format("{0}.{1}", ParseQueryExtensions.GetValue(methodCallExpression.Arguments[0]), text), typeof(string))
				});
			}

			// Token: 0x0600080D RID: 2061 RVA: 0x00017E4C File Offset: 0x0001604C
			protected override Expression VisitMember(MemberExpression node)
			{
				ParseFieldNameAttribute customAttribute = node.Member.GetCustomAttribute<ParseFieldNameAttribute>();
				if (customAttribute == null || !typeof(ParseObject).GetTypeInfo().IsAssignableFrom(node.Expression.Type.GetTypeInfo()))
				{
					return base.VisitMember(node);
				}
				return Expression.Call(node.Expression, ParseQueryExtensions.ParseObjectGetMethod.MakeGenericMethod(new Type[]
				{
					node.Type
				}), new Expression[]
				{
					Expression.Constant(customAttribute.FieldName, typeof(string))
				});
			}

			// Token: 0x0600080E RID: 2062 RVA: 0x00017ED8 File Offset: 0x000160D8
			protected override Expression VisitUnary(UnaryExpression node)
			{
				MethodCallExpression methodCallExpression = this.Visit(node.Operand) as MethodCallExpression;
				if ((node.NodeType != ExpressionType.Convert && node.NodeType != ExpressionType.ConvertChecked) || !ParseQueryExtensions.IsParseObjectGet(methodCallExpression))
				{
					return base.VisitUnary(node);
				}
				return Expression.Call(methodCallExpression.Object, ParseQueryExtensions.ParseObjectGetMethod.MakeGenericMethod(new Type[]
				{
					node.Type
				}), methodCallExpression.Arguments);
			}

			// Token: 0x0600080F RID: 2063 RVA: 0x00017F48 File Offset: 0x00016148
			protected override Expression VisitMethodCall(MethodCallExpression node)
			{
				if (node.Method.Name == "get_Item" && node.Object is ParameterExpression)
				{
					return Expression.Call(node.Object, ParseQueryExtensions.ParseObjectGetMethod.MakeGenericMethod(new Type[]
					{
						typeof(object)
					}), new Expression[]
					{
						Expression.Constant(ParseQueryExtensions.GetValue(node.Arguments[0]) as string, typeof(string))
					});
				}
				if (node.Method.Name == "get_Item" || ParseQueryExtensions.IsParseObjectGet(node))
				{
					MethodCallExpression methodCallExpression = this.Visit(node.Object) as MethodCallExpression;
					if (ParseQueryExtensions.IsParseObjectGet(methodCallExpression))
					{
						string text = ParseQueryExtensions.GetValue(node.Arguments[0]) as string;
						if (text == null)
						{
							throw new InvalidOperationException("Index must be a string");
						}
						return Expression.Call(methodCallExpression.Object, ParseQueryExtensions.ParseObjectGetMethod.MakeGenericMethod(new Type[]
						{
							node.Type
						}), new Expression[]
						{
							Expression.Constant(string.Format("{0}.{1}", ParseQueryExtensions.GetValue(methodCallExpression.Arguments[0]), text), typeof(string))
						});
					}
				}
				return base.VisitMethodCall(node);
			}

			// Token: 0x06000810 RID: 2064 RVA: 0x00018091 File Offset: 0x00016291
			public ObjectNormalizer()
			{
			}
		}

		// Token: 0x02000140 RID: 320
		private class WhereNormalizer : ExpressionVisitor
		{
			// Token: 0x06000811 RID: 2065 RVA: 0x0001809C File Offset: 0x0001629C
			protected override Expression VisitBinary(BinaryExpression node)
			{
				MethodCallExpression methodCallExpression = new ParseQueryExtensions.ObjectNormalizer().Visit(node.Right) as MethodCallExpression;
				MethodCallExpression methodCallExpression2 = new ParseQueryExtensions.ObjectNormalizer().Visit(node.Left) as MethodCallExpression;
				MethodCallExpression left;
				Expression right;
				bool flag;
				if (methodCallExpression2 != null)
				{
					left = methodCallExpression2;
					right = node.Right;
					flag = false;
				}
				else
				{
					left = methodCallExpression;
					right = node.Left;
					flag = true;
				}
				try
				{
					ExpressionType nodeType = node.NodeType;
					switch (nodeType)
					{
					case ExpressionType.Equal:
						return Expression.Equal(left, right);
					case ExpressionType.ExclusiveOr:
					case ExpressionType.Invoke:
					case ExpressionType.Lambda:
					case ExpressionType.LeftShift:
						break;
					case ExpressionType.GreaterThan:
						return flag ? Expression.LessThan(left, right) : Expression.GreaterThan(left, right);
					case ExpressionType.GreaterThanOrEqual:
						return flag ? Expression.LessThanOrEqual(left, right) : Expression.GreaterThanOrEqual(left, right);
					case ExpressionType.LessThan:
						return flag ? Expression.GreaterThan(left, right) : Expression.LessThan(left, right);
					case ExpressionType.LessThanOrEqual:
						return flag ? Expression.GreaterThanOrEqual(left, right) : Expression.LessThanOrEqual(left, right);
					default:
						if (nodeType == ExpressionType.NotEqual)
						{
							return Expression.NotEqual(left, right);
						}
						break;
					}
				}
				catch (ArgumentException)
				{
					throw new InvalidOperationException("Operation not supported: " + ((node != null) ? node.ToString() : null));
				}
				return base.VisitBinary(node);
			}

			// Token: 0x06000812 RID: 2066 RVA: 0x000181E4 File Offset: 0x000163E4
			protected override Expression VisitUnary(UnaryExpression node)
			{
				if (node.NodeType == ExpressionType.Not)
				{
					Expression expression = this.Visit(node.Operand);
					BinaryExpression binaryExpression = expression as BinaryExpression;
					if (binaryExpression != null)
					{
						ExpressionType nodeType = binaryExpression.NodeType;
						switch (nodeType)
						{
						case ExpressionType.Equal:
							return Expression.NotEqual(binaryExpression.Left, binaryExpression.Right);
						case ExpressionType.ExclusiveOr:
						case ExpressionType.Invoke:
						case ExpressionType.Lambda:
						case ExpressionType.LeftShift:
							break;
						case ExpressionType.GreaterThan:
							return Expression.LessThanOrEqual(binaryExpression.Left, binaryExpression.Right);
						case ExpressionType.GreaterThanOrEqual:
							return Expression.LessThan(binaryExpression.Left, binaryExpression.Right);
						case ExpressionType.LessThan:
							return Expression.GreaterThanOrEqual(binaryExpression.Left, binaryExpression.Right);
						case ExpressionType.LessThanOrEqual:
							return Expression.GreaterThan(binaryExpression.Left, binaryExpression.Right);
						default:
							if (nodeType == ExpressionType.NotEqual)
							{
								return Expression.Equal(binaryExpression.Left, binaryExpression.Right);
							}
							break;
						}
					}
					MethodCallExpression methodCallExpression = expression as MethodCallExpression;
					if (methodCallExpression != null)
					{
						if (methodCallExpression.Method.IsGenericMethod)
						{
							if (methodCallExpression.Method.GetGenericMethodDefinition() == ParseQueryExtensions.ContainsMethod)
							{
								return Expression.Call(ParseQueryExtensions.NotContainsMethod.MakeGenericMethod(methodCallExpression.Method.GetGenericArguments()), methodCallExpression.Arguments.ToArray<Expression>());
							}
							if (methodCallExpression.Method.GetGenericMethodDefinition() == ParseQueryExtensions.NotContainsMethod)
							{
								return Expression.Call(ParseQueryExtensions.ContainsMethod.MakeGenericMethod(methodCallExpression.Method.GetGenericArguments()), methodCallExpression.Arguments.ToArray<Expression>());
							}
						}
						if (methodCallExpression.Method == ParseQueryExtensions.ContainsKeyMethod)
						{
							return Expression.Call(ParseQueryExtensions.NotContainsKeyMethod, methodCallExpression.Arguments.ToArray<Expression>());
						}
						if (methodCallExpression.Method == ParseQueryExtensions.NotContainsKeyMethod)
						{
							return Expression.Call(ParseQueryExtensions.ContainsKeyMethod, methodCallExpression.Arguments.ToArray<Expression>());
						}
					}
				}
				return base.VisitUnary(node);
			}

			// Token: 0x06000813 RID: 2067 RVA: 0x000183AC File Offset: 0x000165AC
			protected override Expression VisitMethodCall(MethodCallExpression node)
			{
				if (node.Method.Name == "Equals" && node.Method.ReturnType == typeof(bool) && node.Method.GetParameters().Length == 1)
				{
					MethodCallExpression methodCallExpression = new ParseQueryExtensions.ObjectNormalizer().Visit(node.Object) as MethodCallExpression;
					MethodCallExpression methodCallExpression2 = new ParseQueryExtensions.ObjectNormalizer().Visit(node.Arguments[0]) as MethodCallExpression;
					if ((ParseQueryExtensions.IsParseObjectGet(methodCallExpression) && methodCallExpression.Object is ParameterExpression) || (ParseQueryExtensions.IsParseObjectGet(methodCallExpression2) && methodCallExpression2.Object is ParameterExpression))
					{
						return Expression.Equal(node.Object, node.Arguments[0]);
					}
				}
				if (node.Method != ParseQueryExtensions.StringContainsMethod && node.Method.Name == "Contains" && node.Method.ReturnType == typeof(bool) && node.Method.GetParameters().Length <= 2)
				{
					Expression expression = (node.Method.GetParameters().Length == 1) ? node.Object : node.Arguments[0];
					int index = node.Method.GetParameters().Length - 1;
					MethodCallExpression methodCallExpression3 = new ParseQueryExtensions.ObjectNormalizer().Visit(node.Arguments[index]) as MethodCallExpression;
					if (methodCallExpression3 != null && ParseQueryExtensions.IsParseObjectGet(methodCallExpression3) && methodCallExpression3.Object is ParameterExpression)
					{
						return Expression.Call(ParseQueryExtensions.ContainsMethod.MakeGenericMethod(new Type[]
						{
							methodCallExpression3.Type
						}), expression, methodCallExpression3);
					}
					MethodCallExpression methodCallExpression4 = new ParseQueryExtensions.ObjectNormalizer().Visit(expression) as MethodCallExpression;
					if (methodCallExpression4 != null && ParseQueryExtensions.IsParseObjectGet(methodCallExpression4) && methodCallExpression4.Object is ParameterExpression)
					{
						Expression expression2 = node.Arguments[index];
						return Expression.Call(ParseQueryExtensions.ContainsMethod.MakeGenericMethod(new Type[]
						{
							expression2.Type
						}), methodCallExpression4, expression2);
					}
				}
				if (node.Method.Name == "ContainsKey" && node.Method.ReturnType == typeof(bool) && node.Method.GetParameters().Length == 1)
				{
					Expression expression3 = null;
					string text = null;
					MethodCallExpression methodCallExpression5 = new ParseQueryExtensions.ObjectNormalizer().Visit(node.Object) as MethodCallExpression;
					if (methodCallExpression5 != null && ParseQueryExtensions.IsParseObjectGet(methodCallExpression5) && methodCallExpression5.Object is ParameterExpression)
					{
						return Expression.Call(ParseQueryExtensions.ContainsKeyMethod, methodCallExpression5.Object, Expression.Constant(string.Format("{0}.{1}", ParseQueryExtensions.GetValue(methodCallExpression5.Arguments[0]), ParseQueryExtensions.GetValue(node.Arguments[0]))));
					}
					if (node.Object is ParameterExpression)
					{
						expression3 = node.Object;
						text = (ParseQueryExtensions.GetValue(node.Arguments[0]) as string);
					}
					if (expression3 != null && text != null)
					{
						return Expression.Call(ParseQueryExtensions.ContainsKeyMethod, expression3, Expression.Constant(text));
					}
				}
				return base.VisitMethodCall(node);
			}

			// Token: 0x06000814 RID: 2068 RVA: 0x000186DE File Offset: 0x000168DE
			public WhereNormalizer()
			{
			}
		}

		// Token: 0x02000141 RID: 321
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000815 RID: 2069 RVA: 0x000186E6 File Offset: 0x000168E6
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000816 RID: 2070 RVA: 0x000186F2 File Offset: 0x000168F2
			public <>c()
			{
			}

			// Token: 0x040002EC RID: 748
			public static readonly ParseQueryExtensions.<>c <>9 = new ParseQueryExtensions.<>c();
		}

		// Token: 0x02000142 RID: 322
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__37<T> where T : ParseObject
		{
			// Token: 0x06000817 RID: 2071 RVA: 0x000186FA File Offset: 0x000168FA
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__37()
			{
			}

			// Token: 0x06000818 RID: 2072 RVA: 0x00018706 File Offset: 0x00016906
			public <>c__37()
			{
			}

			// Token: 0x06000819 RID: 2073 RVA: 0x0001870E File Offset: 0x0001690E
			internal Type <WhereMethodCall>b__37_0(ParameterInfo parameter)
			{
				return parameter.ParameterType;
			}

			// Token: 0x040002ED RID: 749
			public static readonly ParseQueryExtensions.<>c__37<T> <>9 = new ParseQueryExtensions.<>c__37<T>();

			// Token: 0x040002EE RID: 750
			public static Func<ParameterInfo, Type> <>9__37_0;
		}

		// Token: 0x02000143 RID: 323
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__45<TOuter, TInner, TKey, TResult> where TOuter : ParseObject where TInner : ParseObject where TResult : ParseObject
		{
			// Token: 0x0600081A RID: 2074 RVA: 0x00018716 File Offset: 0x00016916
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__45()
			{
			}

			// Token: 0x0600081B RID: 2075 RVA: 0x00018722 File Offset: 0x00016922
			public <>c__45()
			{
			}

			// Token: 0x040002EF RID: 751
			public static readonly ParseQueryExtensions.<>c__45<TOuter, TInner, TKey, TResult> <>9 = new ParseQueryExtensions.<>c__45<TOuter, TInner, TKey, TResult>();
		}
	}
}
