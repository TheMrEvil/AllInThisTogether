using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace QFSW.QC
{
	// Token: 0x0200000F RID: 15
	public class InjectionLoader<T>
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002988 File Offset: 0x00000B88
		public Type[] GetInjectableTypes(bool forceReload = false)
		{
			if (this._injectableTypes == null || forceReload)
			{
				this._injectableTypes = (from type in AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly assembly) => assembly.GetTypes())
				where typeof(T).IsAssignableFrom(type)
				where !type.IsAbstract
				where !type.IsDefined(typeof(NoInjectAttribute), false)
				select type).ToArray<Type>();
			}
			return this._injectableTypes;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002A50 File Offset: 0x00000C50
		public IEnumerable<T> GetInjectedInstances(bool forceReload = false)
		{
			IEnumerable<Type> injectableTypes = this.GetInjectableTypes(forceReload);
			return this.GetInjectedInstances(injectableTypes);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002A6C File Offset: 0x00000C6C
		public IEnumerable<T> GetInjectedInstances(IEnumerable<Type> injectableTypes)
		{
			foreach (Type type in injectableTypes)
			{
				T t = default(T);
				bool flag = false;
				try
				{
					t = (T)((object)Activator.CreateInstance(type));
					flag = true;
				}
				catch (MissingMethodException)
				{
					UnityEngine.Debug.LogError(string.Format("Could not load {0} {1} as it is missing a public parameterless constructor.", typeof(T), type));
				}
				catch (Exception exception)
				{
					UnityEngine.Debug.LogException(exception);
				}
				if (flag)
				{
					yield return t;
				}
			}
			IEnumerator<Type> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002A7C File Offset: 0x00000C7C
		public InjectionLoader()
		{
		}

		// Token: 0x04000023 RID: 35
		private Type[] _injectableTypes;

		// Token: 0x02000085 RID: 133
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000299 RID: 665 RVA: 0x0000AAFB File Offset: 0x00008CFB
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600029A RID: 666 RVA: 0x0000AB07 File Offset: 0x00008D07
			public <>c()
			{
			}

			// Token: 0x0600029B RID: 667 RVA: 0x0000AB0F File Offset: 0x00008D0F
			internal IEnumerable<Type> <GetInjectableTypes>b__1_0(Assembly assembly)
			{
				return assembly.GetTypes();
			}

			// Token: 0x0600029C RID: 668 RVA: 0x0000AB17 File Offset: 0x00008D17
			internal bool <GetInjectableTypes>b__1_1(Type type)
			{
				return typeof(T).IsAssignableFrom(type);
			}

			// Token: 0x0600029D RID: 669 RVA: 0x0000AB29 File Offset: 0x00008D29
			internal bool <GetInjectableTypes>b__1_2(Type type)
			{
				return !type.IsAbstract;
			}

			// Token: 0x0600029E RID: 670 RVA: 0x0000AB34 File Offset: 0x00008D34
			internal bool <GetInjectableTypes>b__1_3(Type type)
			{
				return !type.IsDefined(typeof(NoInjectAttribute), false);
			}

			// Token: 0x04000178 RID: 376
			public static readonly InjectionLoader<T>.<>c <>9 = new InjectionLoader<T>.<>c();

			// Token: 0x04000179 RID: 377
			public static Func<Assembly, IEnumerable<Type>> <>9__1_0;

			// Token: 0x0400017A RID: 378
			public static Func<Type, bool> <>9__1_1;

			// Token: 0x0400017B RID: 379
			public static Func<Type, bool> <>9__1_2;

			// Token: 0x0400017C RID: 380
			public static Func<Type, bool> <>9__1_3;
		}

		// Token: 0x02000086 RID: 134
		[CompilerGenerated]
		private sealed class <GetInjectedInstances>d__3 : IEnumerable<T>, IEnumerable, IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x0600029F RID: 671 RVA: 0x0000AB4A File Offset: 0x00008D4A
			[DebuggerHidden]
			public <GetInjectedInstances>d__3(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060002A0 RID: 672 RVA: 0x0000AB64 File Offset: 0x00008D64
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x060002A1 RID: 673 RVA: 0x0000AB9C File Offset: 0x00008D9C
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -3;
					}
					else
					{
						this.<>1__state = -1;
						enumerator = injectableTypes.GetEnumerator();
						this.<>1__state = -3;
					}
					while (enumerator.MoveNext())
					{
						Type type = enumerator.Current;
						T t = default(T);
						bool flag = false;
						try
						{
							t = (T)((object)Activator.CreateInstance(type));
							flag = true;
						}
						catch (MissingMethodException)
						{
							UnityEngine.Debug.LogError(string.Format("Could not load {0} {1} as it is missing a public parameterless constructor.", typeof(T), type));
						}
						catch (Exception exception)
						{
							UnityEngine.Debug.LogException(exception);
						}
						if (flag)
						{
							this.<>2__current = t;
							this.<>1__state = 1;
							return true;
						}
					}
					this.<>m__Finally1();
					enumerator = null;
					result = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x060002A2 RID: 674 RVA: 0x0000AC9C File Offset: 0x00008E9C
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000ACB8 File Offset: 0x00008EB8
			T IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002A4 RID: 676 RVA: 0x0000ACC0 File Offset: 0x00008EC0
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000ACC7 File Offset: 0x00008EC7
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002A6 RID: 678 RVA: 0x0000ACD4 File Offset: 0x00008ED4
			[DebuggerHidden]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				InjectionLoader<T>.<GetInjectedInstances>d__3 <GetInjectedInstances>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetInjectedInstances>d__ = this;
				}
				else
				{
					<GetInjectedInstances>d__ = new InjectionLoader<T>.<GetInjectedInstances>d__3(0);
				}
				<GetInjectedInstances>d__.injectableTypes = injectableTypes;
				return <GetInjectedInstances>d__;
			}

			// Token: 0x060002A7 RID: 679 RVA: 0x0000AD17 File Offset: 0x00008F17
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();
			}

			// Token: 0x0400017D RID: 381
			private int <>1__state;

			// Token: 0x0400017E RID: 382
			private T <>2__current;

			// Token: 0x0400017F RID: 383
			private int <>l__initialThreadId;

			// Token: 0x04000180 RID: 384
			private IEnumerable<Type> injectableTypes;

			// Token: 0x04000181 RID: 385
			public IEnumerable<Type> <>3__injectableTypes;

			// Token: 0x04000182 RID: 386
			private IEnumerator<Type> <>7__wrap1;
		}
	}
}
