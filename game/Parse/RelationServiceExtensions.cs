using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;
using Parse.Abstractions.Platform.Objects;

namespace Parse
{
	// Token: 0x02000012 RID: 18
	public static class RelationServiceExtensions
	{
		// Token: 0x06000124 RID: 292 RVA: 0x000066EE File Offset: 0x000048EE
		internal static ParseRelationBase CreateRelation(this IServiceHub serviceHub, ParseObject parent, string key, string targetClassName)
		{
			return serviceHub.ClassController.CreateRelation(parent, key, targetClassName);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006700 File Offset: 0x00004900
		internal static ParseRelationBase CreateRelation(this IParseObjectClassController classController, ParseObject parent, string key, string targetClassName)
		{
			RelationServiceExtensions.<>c__DisplayClass1_0 CS$<>8__locals1 = new RelationServiceExtensions.<>c__DisplayClass1_0();
			CS$<>8__locals1.parent = parent;
			CS$<>8__locals1.key = key;
			CS$<>8__locals1.targetClassName = targetClassName;
			return (Expression.Lambda<Func<ParseRelation<ParseObject>>>(Expression.Call(null, methodof(RelationServiceExtensions.CreateRelation(ParseObject, string, string)), new Expression[]
			{
				Expression.Field(Expression.Constant(CS$<>8__locals1, typeof(RelationServiceExtensions.<>c__DisplayClass1_0)), fieldof(RelationServiceExtensions.<>c__DisplayClass1_0.parent)),
				Expression.Field(Expression.Constant(CS$<>8__locals1, typeof(RelationServiceExtensions.<>c__DisplayClass1_0)), fieldof(RelationServiceExtensions.<>c__DisplayClass1_0.key)),
				Expression.Field(Expression.Constant(CS$<>8__locals1, typeof(RelationServiceExtensions.<>c__DisplayClass1_0)), fieldof(RelationServiceExtensions.<>c__DisplayClass1_0.targetClassName))
			}), Array.Empty<ParameterExpression>()).Body as MethodCallExpression).Method.GetGenericMethodDefinition().MakeGenericMethod(new Type[]
			{
				classController.GetType(CS$<>8__locals1.targetClassName) ?? typeof(ParseObject)
			}).Invoke(null, new object[]
			{
				CS$<>8__locals1.parent,
				CS$<>8__locals1.key,
				CS$<>8__locals1.targetClassName
			}) as ParseRelationBase;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000681B File Offset: 0x00004A1B
		private static ParseRelation<T> CreateRelation<T>(ParseObject parent, string key, string targetClassName) where T : ParseObject
		{
			return new ParseRelation<T>(parent, key, targetClassName);
		}

		// Token: 0x020000B9 RID: 185
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0
		{
			// Token: 0x06000606 RID: 1542 RVA: 0x000133AB File Offset: 0x000115AB
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x04000146 RID: 326
			public ParseObject parent;

			// Token: 0x04000147 RID: 327
			public string key;

			// Token: 0x04000148 RID: 328
			public string targetClassName;
		}
	}
}
