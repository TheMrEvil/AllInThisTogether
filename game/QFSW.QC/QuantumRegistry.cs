using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using QFSW.QC.Utilities;
using UnityEngine;

namespace QFSW.QC
{
	// Token: 0x02000032 RID: 50
	public static class QuantumRegistry
	{
		// Token: 0x06000128 RID: 296 RVA: 0x00006D75 File Offset: 0x00004F75
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void ResetRegistry()
		{
			QuantumRegistry._objectRegistry.Clear();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006D84 File Offset: 0x00004F84
		private static bool IsNull(object x)
		{
			UnityEngine.Object @object = x as UnityEngine.Object;
			if (@object != null)
			{
				return !@object;
			}
			return x == null;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006DA9 File Offset: 0x00004FA9
		public static void RegisterObject<T>(T obj) where T : class
		{
			QuantumRegistry.RegisterObject(typeof(T), obj);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006DC0 File Offset: 0x00004FC0
		public static void RegisterObject(Type type, object obj)
		{
			if (!type.IsClass)
			{
				throw new Exception("Registry may only contain class types");
			}
			Dictionary<Type, List<object>> objectRegistry = QuantumRegistry._objectRegistry;
			lock (objectRegistry)
			{
				if (QuantumRegistry._objectRegistry.ContainsKey(type))
				{
					if (QuantumRegistry._objectRegistry[type].Contains(obj))
					{
						throw new ArgumentException(string.Format("Could not register object '{0}' of type {1} as it was already registered.", obj, type.GetDisplayName(false)));
					}
					QuantumRegistry._objectRegistry[type].Add(obj);
				}
				else
				{
					QuantumRegistry._objectRegistry.Add(type, new List<object>
					{
						obj
					});
				}
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006E70 File Offset: 0x00005070
		public static void DeregisterObject<T>(T obj) where T : class
		{
			QuantumRegistry.DeregisterObject(typeof(T), obj);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006E88 File Offset: 0x00005088
		public static void DeregisterObject(Type type, object obj)
		{
			if (!type.IsClass)
			{
				throw new Exception("Registry may only contain class types");
			}
			Dictionary<Type, List<object>> objectRegistry = QuantumRegistry._objectRegistry;
			lock (objectRegistry)
			{
				if (!QuantumRegistry._objectRegistry.ContainsKey(type) || !QuantumRegistry._objectRegistry[type].Contains(obj))
				{
					throw new ArgumentException(string.Format("Could not deregister object '{0}' of type {1} as it was not found in the registry.", obj, type.GetDisplayName(false)));
				}
				QuantumRegistry._objectRegistry[type].Remove(obj);
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006F20 File Offset: 0x00005120
		public static int GetRegistrySize<T>() where T : class
		{
			return QuantumRegistry.GetRegistrySize(typeof(T));
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006F31 File Offset: 0x00005131
		public static int GetRegistrySize(Type type)
		{
			return QuantumRegistry.GetRegistryContents(type).Count<object>();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006F3E File Offset: 0x0000513E
		public static IEnumerable<T> GetRegistryContents<T>() where T : class
		{
			foreach (object obj in QuantumRegistry.GetRegistryContents(typeof(T)))
			{
				yield return (T)((object)obj);
			}
			IEnumerator<object> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006F48 File Offset: 0x00005148
		public static IEnumerable<object> GetRegistryContents(Type type)
		{
			if (!type.IsClass)
			{
				throw new Exception("Registry may only contain class types");
			}
			Dictionary<Type, List<object>> objectRegistry = QuantumRegistry._objectRegistry;
			IEnumerable<object> result;
			lock (objectRegistry)
			{
				if (QuantumRegistry._objectRegistry.ContainsKey(type))
				{
					List<object> list = QuantumRegistry._objectRegistry[type];
					list.RemoveAll(new Predicate<object>(QuantumRegistry.IsNull));
					result = list;
				}
				else
				{
					result = Enumerable.Empty<object>();
				}
			}
			return result;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006FCC File Offset: 0x000051CC
		public static void ClearRegistryContents<T>() where T : class
		{
			QuantumRegistry.ClearRegistryContents(typeof(T));
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006FE0 File Offset: 0x000051E0
		public static void ClearRegistryContents(Type type)
		{
			if (!type.IsClass)
			{
				throw new Exception("Registry may only contain class types");
			}
			Dictionary<Type, List<object>> objectRegistry = QuantumRegistry._objectRegistry;
			lock (objectRegistry)
			{
				if (QuantumRegistry._objectRegistry.ContainsKey(type))
				{
					QuantumRegistry._objectRegistry[type].Clear();
				}
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000704C File Offset: 0x0000524C
		private static IEnumerable<object> DisplayRegistry<T>() where T : class
		{
			if (QuantumRegistry.GetRegistrySize<T>() <= 0)
			{
				return ("The registry '" + typeof(T).GetDisplayName(false) + "' is empty").Yield<string>();
			}
			return QuantumRegistry.GetRegistryContents<T>();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00007080 File Offset: 0x00005280
		// Note: this type is marked as 'beforefieldinit'.
		static QuantumRegistry()
		{
		}

		// Token: 0x040000EC RID: 236
		private static readonly Dictionary<Type, List<object>> _objectRegistry = new Dictionary<Type, List<object>>();

		// Token: 0x02000096 RID: 150
		[CompilerGenerated]
		private sealed class <GetRegistryContents>d__9<T> : IEnumerable<!0>, IEnumerable, IEnumerator<!0>, IEnumerator, IDisposable where T : class
		{
			// Token: 0x060002F0 RID: 752 RVA: 0x0000B96D File Offset: 0x00009B6D
			[DebuggerHidden]
			public <GetRegistryContents>d__9(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060002F1 RID: 753 RVA: 0x0000B988 File Offset: 0x00009B88
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

			// Token: 0x060002F2 RID: 754 RVA: 0x0000B9C0 File Offset: 0x00009BC0
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
						enumerator = QuantumRegistry.GetRegistryContents(typeof(T)).GetEnumerator();
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
						this.<>2__current = (T)((object)obj);
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

			// Token: 0x060002F3 RID: 755 RVA: 0x0000BA6C File Offset: 0x00009C6C
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000BA88 File Offset: 0x00009C88
			T IEnumerator<!0>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002F5 RID: 757 RVA: 0x0000BA90 File Offset: 0x00009C90
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000BA97 File Offset: 0x00009C97
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002F7 RID: 759 RVA: 0x0000BAA4 File Offset: 0x00009CA4
			[DebuggerHidden]
			IEnumerator<T> IEnumerable<!0>.GetEnumerator()
			{
				QuantumRegistry.<GetRegistryContents>d__9<T> result;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					result = this;
				}
				else
				{
					result = new QuantumRegistry.<GetRegistryContents>d__9<T>(0);
				}
				return result;
			}

			// Token: 0x060002F8 RID: 760 RVA: 0x0000BADB File Offset: 0x00009CDB
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<T>.GetEnumerator();
			}

			// Token: 0x040001CA RID: 458
			private int <>1__state;

			// Token: 0x040001CB RID: 459
			private T <>2__current;

			// Token: 0x040001CC RID: 460
			private int <>l__initialThreadId;

			// Token: 0x040001CD RID: 461
			private IEnumerator<object> <>7__wrap1;
		}
	}
}
