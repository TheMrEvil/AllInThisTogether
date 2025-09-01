using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using QFSW.QC.Comparators;
using QFSW.QC.Utilities;
using UnityEngine;

namespace QFSW.QC
{
	// Token: 0x02000011 RID: 17
	public static class InvocationTargetFactory
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002AAB File Offset: 0x00000CAB
		public static IEnumerable<T> FindTargets<T>(MonoTargetType method) where T : MonoBehaviour
		{
			foreach (object obj in InvocationTargetFactory.FindTargets(typeof(T), method))
			{
				yield return obj as T;
			}
			IEnumerator<object> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002ABC File Offset: 0x00000CBC
		public static IEnumerable<object> FindTargets(Type classType, MonoTargetType method)
		{
			switch (method)
			{
			case MonoTargetType.Single:
			{
				UnityEngine.Object @object = UnityEngine.Object.FindObjectOfType(classType);
				if (!(@object == null))
				{
					return @object.Yield<UnityEngine.Object>();
				}
				return Enumerable.Empty<object>();
			}
			case MonoTargetType.All:
				return UnityEngine.Object.FindObjectsOfType(classType).OrderBy((UnityEngine.Object x) => x.name, new AlphanumComparator());
			case MonoTargetType.Registry:
				return QuantumRegistry.GetRegistryContents(classType);
			case MonoTargetType.Singleton:
				return InvocationTargetFactory.GetSingletonInstance(classType).Yield<object>();
			case MonoTargetType.SingleInactive:
				return InvocationTargetFactory.WrapSingleCached(classType, method, (Type type) => Resources.FindObjectsOfTypeAll(type).FirstOrDefault((UnityEngine.Object x) => !x.hideFlags.HasFlag(HideFlags.HideInHierarchy)));
			case MonoTargetType.AllInactive:
				return (from x in Resources.FindObjectsOfTypeAll(classType)
				where !x.hideFlags.HasFlag(HideFlags.HideInHierarchy)
				select x).OrderBy((UnityEngine.Object x) => x.name, new AlphanumComparator());
			default:
				throw new ArgumentException(string.Format("Unsupported MonoTargetType {0}", method));
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002BE0 File Offset: 0x00000DE0
		private static IEnumerable<object> WrapSingleCached(Type classType, MonoTargetType method, Func<Type, object> targetFinder)
		{
			object obj;
			if (!InvocationTargetFactory.TargetCache.TryGetValue(new ValueTuple<MonoTargetType, Type>(method, classType), out obj) || obj as UnityEngine.Object == null)
			{
				obj = targetFinder(classType);
				InvocationTargetFactory.TargetCache[new ValueTuple<MonoTargetType, Type>(method, classType)] = obj;
			}
			if (obj != null)
			{
				return obj.Yield<object>();
			}
			return Enumerable.Empty<object>();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002C3C File Offset: 0x00000E3C
		public static object InvokeOnTargets(MethodInfo invokingMethod, IEnumerable<object> targets, object[] data)
		{
			int num = 0;
			int num2 = 0;
			Dictionary<object, object> dictionary = new Dictionary<object, object>();
			foreach (object obj in targets)
			{
				num2++;
				object obj2 = invokingMethod.Invoke(obj, data);
				if (obj2 != null)
				{
					dictionary.Add(obj, obj2);
					num++;
				}
			}
			if (num > 1)
			{
				return dictionary;
			}
			if (num == 1)
			{
				return dictionary.Values.First<object>();
			}
			if (num2 == 0)
			{
				string displayName = invokingMethod.DeclaringType.GetDisplayName(false);
				throw new Exception("Could not invoke the command because no objects of type " + displayName + " could be found.");
			}
			return null;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002CE8 File Offset: 0x00000EE8
		private static string FormatInvocationMessage(int invocationCount, object lastTarget = null)
		{
			if (invocationCount == 0)
			{
				throw new Exception("No targets could be found");
			}
			if (invocationCount != 1)
			{
				return string.Format("> Invoked on {0} targets", invocationCount);
			}
			UnityEngine.Object @object = lastTarget as UnityEngine.Object;
			string str;
			if (@object != null)
			{
				str = @object.name;
			}
			else
			{
				str = ((lastTarget != null) ? lastTarget.ToString() : null);
			}
			return "> Invoked on " + str;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002D48 File Offset: 0x00000F48
		private static object GetSingletonInstance(Type classType)
		{
			if (QuantumRegistry.GetRegistrySize(classType) > 0)
			{
				return QuantumRegistry.GetRegistryContents(classType).First<object>();
			}
			object obj = InvocationTargetFactory.CreateCommandSingletonInstance(classType);
			QuantumRegistry.RegisterObject(classType, obj);
			return obj;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002D79 File Offset: 0x00000F79
		private static Component CreateCommandSingletonInstance(Type classType)
		{
			GameObject gameObject = new GameObject(string.Format("{0}Singleton", classType));
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			return gameObject.AddComponent(classType);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002D97 File Offset: 0x00000F97
		// Note: this type is marked as 'beforefieldinit'.
		static InvocationTargetFactory()
		{
		}

		// Token: 0x04000024 RID: 36
		private static readonly Dictionary<ValueTuple<MonoTargetType, Type>, object> TargetCache = new Dictionary<ValueTuple<MonoTargetType, Type>, object>();

		// Token: 0x02000087 RID: 135
		[CompilerGenerated]
		private sealed class <FindTargets>d__1<T> : IEnumerable<!0>, IEnumerable, IEnumerator<!0>, IEnumerator, IDisposable where T : MonoBehaviour
		{
			// Token: 0x060002A8 RID: 680 RVA: 0x0000AD1F File Offset: 0x00008F1F
			[DebuggerHidden]
			public <FindTargets>d__1(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060002A9 RID: 681 RVA: 0x0000AD3C File Offset: 0x00008F3C
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

			// Token: 0x060002AA RID: 682 RVA: 0x0000AD74 File Offset: 0x00008F74
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
						enumerator = InvocationTargetFactory.FindTargets(typeof(T), method).GetEnumerator();
						this.<>1__state = -3;
					}
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						result = false;
					}
					else
					{
						object obj = enumerator.Current;
						this.<>2__current = (obj as T);
						this.<>1__state = 1;
						result = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x060002AB RID: 683 RVA: 0x0000AE30 File Offset: 0x00009030
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x060002AC RID: 684 RVA: 0x0000AE4C File Offset: 0x0000904C
			T IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002AD RID: 685 RVA: 0x0000AE54 File Offset: 0x00009054
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x060002AE RID: 686 RVA: 0x0000AE5B File Offset: 0x0000905B
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002AF RID: 687 RVA: 0x0000AE68 File Offset: 0x00009068
			[DebuggerHidden]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				InvocationTargetFactory.<FindTargets>d__1<T> <FindTargets>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<FindTargets>d__ = this;
				}
				else
				{
					<FindTargets>d__ = new InvocationTargetFactory.<FindTargets>d__1<T>(0);
				}
				<FindTargets>d__.method = method;
				return <FindTargets>d__;
			}

			// Token: 0x060002B0 RID: 688 RVA: 0x0000AEAB File Offset: 0x000090AB
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();
			}

			// Token: 0x04000183 RID: 387
			private int <>1__state;

			// Token: 0x04000184 RID: 388
			private T <>2__current;

			// Token: 0x04000185 RID: 389
			private int <>l__initialThreadId;

			// Token: 0x04000186 RID: 390
			private MonoTargetType method;

			// Token: 0x04000187 RID: 391
			public MonoTargetType <>3__method;

			// Token: 0x04000188 RID: 392
			private IEnumerator<object> <>7__wrap1;
		}

		// Token: 0x02000088 RID: 136
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060002B1 RID: 689 RVA: 0x0000AEB3 File Offset: 0x000090B3
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060002B2 RID: 690 RVA: 0x0000AEBF File Offset: 0x000090BF
			public <>c()
			{
			}

			// Token: 0x060002B3 RID: 691 RVA: 0x0000AEC7 File Offset: 0x000090C7
			internal object <FindTargets>b__2_0(Type type)
			{
				return Resources.FindObjectsOfTypeAll(type).FirstOrDefault((UnityEngine.Object x) => !x.hideFlags.HasFlag(HideFlags.HideInHierarchy));
			}

			// Token: 0x060002B4 RID: 692 RVA: 0x0000AEF3 File Offset: 0x000090F3
			internal bool <FindTargets>b__2_4(UnityEngine.Object x)
			{
				return !x.hideFlags.HasFlag(HideFlags.HideInHierarchy);
			}

			// Token: 0x060002B5 RID: 693 RVA: 0x0000AF0E File Offset: 0x0000910E
			internal string <FindTargets>b__2_1(UnityEngine.Object x)
			{
				return x.name;
			}

			// Token: 0x060002B6 RID: 694 RVA: 0x0000AF16 File Offset: 0x00009116
			internal bool <FindTargets>b__2_2(UnityEngine.Object x)
			{
				return !x.hideFlags.HasFlag(HideFlags.HideInHierarchy);
			}

			// Token: 0x060002B7 RID: 695 RVA: 0x0000AF31 File Offset: 0x00009131
			internal string <FindTargets>b__2_3(UnityEngine.Object x)
			{
				return x.name;
			}

			// Token: 0x04000189 RID: 393
			public static readonly InvocationTargetFactory.<>c <>9 = new InvocationTargetFactory.<>c();

			// Token: 0x0400018A RID: 394
			public static Func<UnityEngine.Object, bool> <>9__2_4;

			// Token: 0x0400018B RID: 395
			public static Func<Type, object> <>9__2_0;

			// Token: 0x0400018C RID: 396
			public static Func<UnityEngine.Object, string> <>9__2_1;

			// Token: 0x0400018D RID: 397
			public static Func<UnityEngine.Object, bool> <>9__2_2;

			// Token: 0x0400018E RID: 398
			public static Func<UnityEngine.Object, string> <>9__2_3;
		}
	}
}
