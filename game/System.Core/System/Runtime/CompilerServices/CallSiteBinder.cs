using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace System.Runtime.CompilerServices
{
	/// <summary>Class responsible for runtime binding of the dynamic operations on the dynamic call site.</summary>
	// Token: 0x020002DE RID: 734
	public abstract class CallSiteBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.CallSiteBinder" /> class.</summary>
		// Token: 0x06001655 RID: 5717 RVA: 0x00002162 File Offset: 0x00000362
		protected CallSiteBinder()
		{
		}

		/// <summary>Gets a label that can be used to cause the binding to be updated. It indicates that the expression's binding is no longer valid. This is typically used when the "version" of a dynamic object has changed.</summary>
		/// <returns>The <see cref="T:System.Linq.Expressions.LabelTarget" /> object representing a label that can be used to trigger the binding update.</returns>
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x0004B9AE File Offset: 0x00049BAE
		public static LabelTarget UpdateLabel
		{
			[CompilerGenerated]
			get
			{
				return CallSiteBinder.<UpdateLabel>k__BackingField;
			}
		} = Expression.Label("CallSiteBinder.UpdateLabel");

		/// <summary>Performs the runtime binding of the dynamic operation on a set of arguments.</summary>
		/// <param name="args">An array of arguments to the dynamic operation.</param>
		/// <param name="parameters">The array of <see cref="T:System.Linq.Expressions.ParameterExpression" /> instances that represent the parameters of the call site in the binding process.</param>
		/// <param name="returnLabel">A LabelTarget used to return the result of the dynamic binding.</param>
		/// <returns>An Expression that performs tests on the dynamic operation arguments, and performs the dynamic operation if the tests are valid. If the tests fail on subsequent occurrences of the dynamic operation, Bind will be called again to produce a new <see cref="T:System.Linq.Expressions.Expression" /> for the new argument types.</returns>
		// Token: 0x06001657 RID: 5719
		public abstract Expression Bind(object[] args, ReadOnlyCollection<ParameterExpression> parameters, LabelTarget returnLabel);

		/// <summary>Provides low-level runtime binding support. Classes can override this and provide a direct delegate for the implementation of rule. This can enable saving rules to disk, having specialized rules available at runtime, or providing a different caching policy.</summary>
		/// <param name="site">The CallSite the bind is being performed for.</param>
		/// <param name="args">The arguments for the binder.</param>
		/// <typeparam name="T">The target type of the CallSite.</typeparam>
		/// <returns>A new delegate which replaces the CallSite Target.</returns>
		// Token: 0x06001658 RID: 5720 RVA: 0x0004B9B8 File Offset: 0x00049BB8
		public virtual T BindDelegate<T>(CallSite<T> site, object[] args) where T : class
		{
			return default(T);
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x0004B9D0 File Offset: 0x00049BD0
		internal T BindCore<T>(CallSite<T> site, object[] args) where T : class
		{
			T t = this.BindDelegate<T>(site, args);
			if (t != null)
			{
				return t;
			}
			CallSiteBinder.LambdaSignature<T> instance = CallSiteBinder.LambdaSignature<T>.Instance;
			Expression expression = this.Bind(args, instance.Parameters, instance.ReturnLabel);
			if (expression == null)
			{
				throw Error.NoOrInvalidRuleProduced();
			}
			T t2 = CallSiteBinder.Stitch<T>(expression, instance).Compile();
			this.CacheTarget<T>(t2);
			return t2;
		}

		/// <summary>Adds a target to the cache of known targets. The cached targets will be scanned before calling BindDelegate to produce the new rule.</summary>
		/// <param name="target">The target delegate to be added to the cache.</param>
		/// <typeparam name="T">The type of target being added.</typeparam>
		// Token: 0x0600165A RID: 5722 RVA: 0x0004BA26 File Offset: 0x00049C26
		protected void CacheTarget<T>(T target) where T : class
		{
			this.GetRuleCache<T>().AddRule(target);
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x0004BA34 File Offset: 0x00049C34
		private static Expression<T> Stitch<T>(Expression binding, CallSiteBinder.LambdaSignature<T> signature) where T : class
		{
			Type typeFromHandle = typeof(CallSite<T>);
			ReadOnlyCollectionBuilder<Expression> readOnlyCollectionBuilder = new ReadOnlyCollectionBuilder<Expression>(3);
			readOnlyCollectionBuilder.Add(binding);
			ParameterExpression parameterExpression = Expression.Parameter(typeof(CallSite), "$site");
			TrueReadOnlyCollection<ParameterExpression> trueReadOnlyCollection = signature.Parameters.AddFirst(parameterExpression);
			Expression item = Expression.Label(CallSiteBinder.UpdateLabel);
			readOnlyCollectionBuilder.Add(item);
			readOnlyCollectionBuilder.Add(Expression.Label(signature.ReturnLabel, Expression.Condition(Expression.Call(CachedReflectionInfo.CallSiteOps_SetNotMatched, parameterExpression), Expression.Default(signature.ReturnLabel.Type), Expression.Invoke(Expression.Property(Expression.Convert(parameterExpression, typeFromHandle), typeof(CallSite<T>).GetProperty("Update")), trueReadOnlyCollection))));
			return Expression.Lambda<T>(Expression.Block(readOnlyCollectionBuilder), "CallSite.Target", true, trueReadOnlyCollection);
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x0004BAF8 File Offset: 0x00049CF8
		internal RuleCache<T> GetRuleCache<T>() where T : class
		{
			if (this.Cache == null)
			{
				Interlocked.CompareExchange<Dictionary<Type, object>>(ref this.Cache, new Dictionary<Type, object>(), null);
			}
			Dictionary<Type, object> cache = this.Cache;
			Dictionary<Type, object> obj = cache;
			object obj2;
			lock (obj)
			{
				if (!cache.TryGetValue(typeof(T), out obj2))
				{
					obj2 = (cache[typeof(T)] = new RuleCache<T>());
				}
			}
			return obj2 as RuleCache<T>;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x0004BB80 File Offset: 0x00049D80
		// Note: this type is marked as 'beforefieldinit'.
		static CallSiteBinder()
		{
		}

		// Token: 0x04000B4E RID: 2894
		internal Dictionary<Type, object> Cache;

		// Token: 0x04000B4F RID: 2895
		[CompilerGenerated]
		private static readonly LabelTarget <UpdateLabel>k__BackingField;

		// Token: 0x020002DF RID: 735
		private sealed class LambdaSignature<T> where T : class
		{
			// Token: 0x170003D6 RID: 982
			// (get) Token: 0x0600165E RID: 5726 RVA: 0x0004BB91 File Offset: 0x00049D91
			internal static CallSiteBinder.LambdaSignature<T> Instance
			{
				get
				{
					if (CallSiteBinder.LambdaSignature<T>.s_instance == null)
					{
						CallSiteBinder.LambdaSignature<T>.s_instance = new CallSiteBinder.LambdaSignature<T>();
					}
					return CallSiteBinder.LambdaSignature<T>.s_instance;
				}
			}

			// Token: 0x0600165F RID: 5727 RVA: 0x0004BBAC File Offset: 0x00049DAC
			private LambdaSignature()
			{
				Type typeFromHandle = typeof(T);
				if (!typeFromHandle.IsSubclassOf(typeof(MulticastDelegate)))
				{
					throw Error.TypeParameterIsNotDelegate(typeFromHandle);
				}
				MethodInfo invokeMethod = typeFromHandle.GetInvokeMethod();
				ParameterInfo[] parametersCached = invokeMethod.GetParametersCached();
				if (parametersCached[0].ParameterType != typeof(CallSite))
				{
					throw Error.FirstArgumentMustBeCallSite();
				}
				ParameterExpression[] array = new ParameterExpression[parametersCached.Length - 1];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Expression.Parameter(parametersCached[i + 1].ParameterType, "$arg" + i.ToString());
				}
				this.Parameters = new TrueReadOnlyCollection<ParameterExpression>(array);
				this.ReturnLabel = Expression.Label(invokeMethod.GetReturnType());
			}

			// Token: 0x04000B50 RID: 2896
			private static CallSiteBinder.LambdaSignature<T> s_instance;

			// Token: 0x04000B51 RID: 2897
			internal readonly ReadOnlyCollection<ParameterExpression> Parameters;

			// Token: 0x04000B52 RID: 2898
			internal readonly LabelTarget ReturnLabel;
		}
	}
}
