using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Reflectrify
{
	// Token: 0x02000005 RID: 5
	public class AttributeTypeCache : ICache
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000021F8 File Offset: 0x000003F8
		public void Clear()
		{
			this.cache.Clear();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002208 File Offset: 0x00000408
		public T GetSingleAttribute<T>(Type type, AttributeTargets targets = AttributeTargets.All, bool inherit = true) where T : Attribute
		{
			ValueTuple<Type, Type, AttributeTargets, bool> key = new ValueTuple<Type, Type, AttributeTargets, bool>(type, typeof(T), targets, inherit);
			Attribute[] array;
			if (!this.cache.TryGetValue(key, out array))
			{
				array = AttributeTypeCache.FilterAttributes<T>((Attribute[])type.GetCustomAttributes(typeof(T), inherit), type, targets).ToArray<Attribute>();
				this.cache.TryAdd(key, array);
			}
			return (T)((object)array.FirstOrDefault<Attribute>());
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002278 File Offset: 0x00000478
		public T[] GetMultiAttributes<T>(Type type, AttributeTargets targets = AttributeTargets.All, bool inherit = true) where T : Attribute
		{
			ValueTuple<Type, Type, AttributeTargets, bool> key = new ValueTuple<Type, Type, AttributeTargets, bool>(type, typeof(T), targets, inherit);
			Attribute[] array;
			if (!this.cache.TryGetValue(key, out array))
			{
				array = AttributeTypeCache.FilterAttributes<T>((Attribute[])type.GetCustomAttributes(typeof(T), inherit), type, targets).ToArray<Attribute>();
				this.cache.TryAdd(key, array);
			}
			return array.Cast<T>().ToArray<T>();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022E5 File Offset: 0x000004E5
		private static IEnumerable<Attribute> FilterAttributes<T>(IEnumerable<Attribute> attributes, Type type, AttributeTargets targets) where T : Attribute
		{
			MemberInfo[] members = type.GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (targets.HasFlag(AttributeTargets.Class))
			{
				foreach (Attribute attribute in attributes)
				{
					yield return attribute;
				}
				IEnumerator<Attribute> enumerator = null;
			}
			foreach (MemberInfo memberInfo in members)
			{
				if (targets.HasFlag(AttributeTypeCache.GetAttributeTarget(memberInfo)))
				{
					object[] customAttributes = memberInfo.GetCustomAttributes(typeof(T), false);
					foreach (Attribute attribute2 in customAttributes.Cast<Attribute>())
					{
						yield return attribute2;
					}
					IEnumerator<Attribute> enumerator = null;
				}
			}
			MemberInfo[] array = null;
			yield break;
			yield break;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002304 File Offset: 0x00000504
		private static AttributeTargets GetAttributeTarget(MemberInfo member)
		{
			Type type = member as Type;
			if (type == null)
			{
				if (member is FieldInfo)
				{
					return AttributeTargets.Field;
				}
				if (member is PropertyInfo)
				{
					return AttributeTargets.Property;
				}
				if (member is MethodInfo)
				{
					return AttributeTargets.Method;
				}
				if (member is ConstructorInfo)
				{
					return AttributeTargets.Constructor;
				}
				if (!(member is EventInfo))
				{
					return AttributeTargets.All;
				}
				return AttributeTargets.Event;
			}
			else
			{
				if (typeof(Delegate).IsAssignableFrom(type))
				{
					return AttributeTargets.Delegate;
				}
				if (type.IsEnum)
				{
					return AttributeTargets.Enum;
				}
				if (type.IsInterface)
				{
					return AttributeTargets.Interface;
				}
				if (!type.IsValueType)
				{
					return AttributeTargets.Class;
				}
				return AttributeTargets.Struct;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000239F File Offset: 0x0000059F
		public AttributeTypeCache()
		{
		}

		// Token: 0x04000004 RID: 4
		private ConcurrentDictionary<ValueTuple<Type, Type, AttributeTargets, bool>, Attribute[]> cache = new ConcurrentDictionary<ValueTuple<Type, Type, AttributeTargets, bool>, Attribute[]>();

		// Token: 0x02000014 RID: 20
		[CompilerGenerated]
		private sealed class <FilterAttributes>d__4<T> : IEnumerable<Attribute>, IEnumerable, IEnumerator<Attribute>, IEnumerator, IDisposable where T : Attribute
		{
			// Token: 0x0600004A RID: 74 RVA: 0x0000343C File Offset: 0x0000163C
			[DebuggerHidden]
			public <FilterAttributes>d__4(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600004B RID: 75 RVA: 0x00003458 File Offset: 0x00001658
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				switch (this.<>1__state)
				{
				case -4:
				case 2:
					break;
				case -3:
				case 1:
					try
					{
						return;
					}
					finally
					{
						this.<>m__Finally1();
					}
					break;
				case -2:
				case -1:
				case 0:
					return;
				default:
					return;
				}
				try
				{
				}
				finally
				{
					this.<>m__Finally2();
				}
			}

			// Token: 0x0600004C RID: 76 RVA: 0x000034C4 File Offset: 0x000016C4
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					switch (this.<>1__state)
					{
					case 0:
						this.<>1__state = -1;
						members = type.GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
						if (!targets.HasFlag(AttributeTargets.Class))
						{
							goto IL_B0;
						}
						enumerator = attributes.GetEnumerator();
						this.<>1__state = -3;
						break;
					case 1:
						this.<>1__state = -3;
						break;
					case 2:
						this.<>1__state = -4;
						goto IL_14A;
					default:
						return false;
					}
					if (enumerator.MoveNext())
					{
						Attribute attribute = enumerator.Current;
						this.<>2__current = attribute;
						this.<>1__state = 1;
						return true;
					}
					this.<>m__Finally1();
					enumerator = null;
					IL_B0:
					array = members;
					i = 0;
					goto IL_172;
					IL_14A:
					if (enumerator.MoveNext())
					{
						Attribute attribute2 = enumerator.Current;
						this.<>2__current = attribute2;
						this.<>1__state = 2;
						return true;
					}
					this.<>m__Finally2();
					enumerator = null;
					IL_164:
					i++;
					IL_172:
					if (i >= array.Length)
					{
						array = null;
						result = false;
					}
					else
					{
						MemberInfo memberInfo = array[i];
						if (targets.HasFlag(AttributeTypeCache.GetAttributeTarget(memberInfo)))
						{
							object[] customAttributes = memberInfo.GetCustomAttributes(typeof(T), false);
							enumerator = customAttributes.Cast<Attribute>().GetEnumerator();
							this.<>1__state = -4;
							goto IL_14A;
						}
						goto IL_164;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x0600004D RID: 77 RVA: 0x00003688 File Offset: 0x00001888
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x0600004E RID: 78 RVA: 0x000036A4 File Offset: 0x000018A4
			private void <>m__Finally2()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x0600004F RID: 79 RVA: 0x000036C0 File Offset: 0x000018C0
			Attribute IEnumerator<Attribute>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000050 RID: 80 RVA: 0x000036C8 File Offset: 0x000018C8
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000051 RID: 81 RVA: 0x000036CF File Offset: 0x000018CF
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000052 RID: 82 RVA: 0x000036D8 File Offset: 0x000018D8
			[DebuggerHidden]
			IEnumerator<Attribute> IEnumerable<Attribute>.GetEnumerator()
			{
				AttributeTypeCache.<FilterAttributes>d__4<T> <FilterAttributes>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<FilterAttributes>d__ = this;
				}
				else
				{
					<FilterAttributes>d__ = new AttributeTypeCache.<FilterAttributes>d__4<T>(0);
				}
				<FilterAttributes>d__.attributes = attributes;
				<FilterAttributes>d__.type = type;
				<FilterAttributes>d__.targets = targets;
				return <FilterAttributes>d__;
			}

			// Token: 0x06000053 RID: 83 RVA: 0x00003733 File Offset: 0x00001933
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Attribute>.GetEnumerator();
			}

			// Token: 0x04000014 RID: 20
			private int <>1__state;

			// Token: 0x04000015 RID: 21
			private Attribute <>2__current;

			// Token: 0x04000016 RID: 22
			private int <>l__initialThreadId;

			// Token: 0x04000017 RID: 23
			private Type type;

			// Token: 0x04000018 RID: 24
			public Type <>3__type;

			// Token: 0x04000019 RID: 25
			private AttributeTargets targets;

			// Token: 0x0400001A RID: 26
			public AttributeTargets <>3__targets;

			// Token: 0x0400001B RID: 27
			private IEnumerable<Attribute> attributes;

			// Token: 0x0400001C RID: 28
			public IEnumerable<Attribute> <>3__attributes;

			// Token: 0x0400001D RID: 29
			private MemberInfo[] <members>5__2;

			// Token: 0x0400001E RID: 30
			private IEnumerator<Attribute> <>7__wrap2;

			// Token: 0x0400001F RID: 31
			private MemberInfo[] <>7__wrap3;

			// Token: 0x04000020 RID: 32
			private int <>7__wrap4;
		}
	}
}
