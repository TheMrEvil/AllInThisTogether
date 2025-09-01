using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.Internal
{
	// Token: 0x0200000D RID: 13
	internal static class ReflectionServices
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002904 File Offset: 0x00000B04
		public static Assembly Assembly(this MemberInfo member)
		{
			Type type = member as Type;
			if (type != null)
			{
				return type.Assembly;
			}
			return member.DeclaringType.Assembly;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002933 File Offset: 0x00000B33
		public static bool IsVisible(this ConstructorInfo constructor)
		{
			return constructor.DeclaringType.IsVisible && constructor.IsPublic;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000294A File Offset: 0x00000B4A
		public static bool IsVisible(this FieldInfo field)
		{
			return field.DeclaringType.IsVisible && field.IsPublic;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002964 File Offset: 0x00000B64
		public static bool IsVisible(this MethodInfo method)
		{
			if (!method.DeclaringType.IsVisible)
			{
				return false;
			}
			if (!method.IsPublic)
			{
				return false;
			}
			if (method.IsGenericMethod)
			{
				Type[] genericArguments = method.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					if (!genericArguments[i].IsVisible)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000029B4 File Offset: 0x00000BB4
		public static string GetDisplayName(Type declaringType, string name)
		{
			Assumes.NotNull<Type>(declaringType);
			return declaringType.GetDisplayName() + "." + name;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000029D0 File Offset: 0x00000BD0
		public static string GetDisplayName(this MemberInfo member)
		{
			Assumes.NotNull<MemberInfo>(member);
			MemberTypes memberType = member.MemberType;
			if (memberType == MemberTypes.TypeInfo || memberType == MemberTypes.NestedType)
			{
				return AttributedModelServices.GetTypeIdentity((Type)member);
			}
			return ReflectionServices.GetDisplayName(member.DeclaringType, member.Name);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A14 File Offset: 0x00000C14
		internal static bool TryGetGenericInterfaceType(Type instanceType, Type targetOpenInterfaceType, out Type targetClosedInterfaceType)
		{
			Assumes.IsTrue(targetOpenInterfaceType.IsInterface);
			Assumes.IsTrue(targetOpenInterfaceType.IsGenericTypeDefinition);
			Assumes.IsTrue(!instanceType.IsGenericTypeDefinition);
			if (instanceType.IsInterface && instanceType.IsGenericType && instanceType.UnderlyingSystemType.GetGenericTypeDefinition() == targetOpenInterfaceType.UnderlyingSystemType)
			{
				targetClosedInterfaceType = instanceType;
				return true;
			}
			try
			{
				Type @interface = instanceType.GetInterface(targetOpenInterfaceType.Name, false);
				if (@interface != null && @interface.UnderlyingSystemType.GetGenericTypeDefinition() == targetOpenInterfaceType.UnderlyingSystemType)
				{
					targetClosedInterfaceType = @interface;
					return true;
				}
			}
			catch (AmbiguousMatchException)
			{
			}
			targetClosedInterfaceType = null;
			return false;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002AC4 File Offset: 0x00000CC4
		internal static IEnumerable<PropertyInfo> GetAllProperties(this Type type)
		{
			return type.GetInterfaces().Concat(new Type[]
			{
				type
			}).SelectMany((Type itf) => itf.GetProperties());
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002B00 File Offset: 0x00000D00
		internal static IEnumerable<MethodInfo> GetAllMethods(this Type type)
		{
			IEnumerable<MethodInfo> declaredMethods = type.GetDeclaredMethods();
			Type baseType = type.BaseType;
			if (baseType.UnderlyingSystemType != typeof(object))
			{
				return declaredMethods.Concat(baseType.GetAllMethods());
			}
			return declaredMethods;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B40 File Offset: 0x00000D40
		private static IEnumerable<MethodInfo> GetDeclaredMethods(this Type type)
		{
			foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				yield return methodInfo;
			}
			MethodInfo[] array = null;
			yield break;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B50 File Offset: 0x00000D50
		public static IEnumerable<FieldInfo> GetAllFields(this Type type)
		{
			IEnumerable<FieldInfo> declaredFields = type.GetDeclaredFields();
			Type baseType = type.BaseType;
			if (baseType.UnderlyingSystemType != typeof(object))
			{
				return declaredFields.Concat(baseType.GetAllFields());
			}
			return declaredFields;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B90 File Offset: 0x00000D90
		private static IEnumerable<FieldInfo> GetDeclaredFields(this Type type)
		{
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				yield return fieldInfo;
			}
			FieldInfo[] array = null;
			yield break;
		}

		// Token: 0x0200000E RID: 14
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000041 RID: 65 RVA: 0x00002BA0 File Offset: 0x00000DA0
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000042 RID: 66 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c()
			{
			}

			// Token: 0x06000043 RID: 67 RVA: 0x00002BB4 File Offset: 0x00000DB4
			internal IEnumerable<PropertyInfo> <GetAllProperties>b__7_0(Type itf)
			{
				return itf.GetProperties();
			}

			// Token: 0x04000044 RID: 68
			public static readonly ReflectionServices.<>c <>9 = new ReflectionServices.<>c();

			// Token: 0x04000045 RID: 69
			public static Func<Type, IEnumerable<PropertyInfo>> <>9__7_0;
		}

		// Token: 0x0200000F RID: 15
		[CompilerGenerated]
		private sealed class <GetDeclaredMethods>d__9 : IEnumerable<MethodInfo>, IEnumerable, IEnumerator<MethodInfo>, IDisposable, IEnumerator
		{
			// Token: 0x06000044 RID: 68 RVA: 0x00002BBC File Offset: 0x00000DBC
			[DebuggerHidden]
			public <GetDeclaredMethods>d__9(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000045 RID: 69 RVA: 0x000028FF File Offset: 0x00000AFF
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000046 RID: 70 RVA: 0x00002BD8 File Offset: 0x00000DD8
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					i++;
				}
				else
				{
					this.<>1__state = -1;
					array = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					i = 0;
				}
				if (i >= array.Length)
				{
					array = null;
					return false;
				}
				MethodInfo methodInfo = array[i];
				this.<>2__current = methodInfo;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000047 RID: 71 RVA: 0x00002C63 File Offset: 0x00000E63
			MethodInfo IEnumerator<MethodInfo>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000048 RID: 72 RVA: 0x00002C6B File Offset: 0x00000E6B
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000049 RID: 73 RVA: 0x00002C63 File Offset: 0x00000E63
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600004A RID: 74 RVA: 0x00002C74 File Offset: 0x00000E74
			[DebuggerHidden]
			IEnumerator<MethodInfo> IEnumerable<MethodInfo>.GetEnumerator()
			{
				ReflectionServices.<GetDeclaredMethods>d__9 <GetDeclaredMethods>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetDeclaredMethods>d__ = this;
				}
				else
				{
					<GetDeclaredMethods>d__ = new ReflectionServices.<GetDeclaredMethods>d__9(0);
				}
				<GetDeclaredMethods>d__.type = type;
				return <GetDeclaredMethods>d__;
			}

			// Token: 0x0600004B RID: 75 RVA: 0x00002CB7 File Offset: 0x00000EB7
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Reflection.MethodInfo>.GetEnumerator();
			}

			// Token: 0x04000046 RID: 70
			private int <>1__state;

			// Token: 0x04000047 RID: 71
			private MethodInfo <>2__current;

			// Token: 0x04000048 RID: 72
			private int <>l__initialThreadId;

			// Token: 0x04000049 RID: 73
			private Type type;

			// Token: 0x0400004A RID: 74
			public Type <>3__type;

			// Token: 0x0400004B RID: 75
			private MethodInfo[] <>7__wrap1;

			// Token: 0x0400004C RID: 76
			private int <>7__wrap2;
		}

		// Token: 0x02000010 RID: 16
		[CompilerGenerated]
		private sealed class <GetDeclaredFields>d__11 : IEnumerable<FieldInfo>, IEnumerable, IEnumerator<FieldInfo>, IDisposable, IEnumerator
		{
			// Token: 0x0600004C RID: 76 RVA: 0x00002CBF File Offset: 0x00000EBF
			[DebuggerHidden]
			public <GetDeclaredFields>d__11(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600004D RID: 77 RVA: 0x000028FF File Offset: 0x00000AFF
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600004E RID: 78 RVA: 0x00002CDC File Offset: 0x00000EDC
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					i++;
				}
				else
				{
					this.<>1__state = -1;
					array = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					i = 0;
				}
				if (i >= array.Length)
				{
					array = null;
					return false;
				}
				FieldInfo fieldInfo = array[i];
				this.<>2__current = fieldInfo;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600004F RID: 79 RVA: 0x00002D67 File Offset: 0x00000F67
			FieldInfo IEnumerator<FieldInfo>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000050 RID: 80 RVA: 0x00002C6B File Offset: 0x00000E6B
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000051 RID: 81 RVA: 0x00002D67 File Offset: 0x00000F67
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000052 RID: 82 RVA: 0x00002D70 File Offset: 0x00000F70
			[DebuggerHidden]
			IEnumerator<FieldInfo> IEnumerable<FieldInfo>.GetEnumerator()
			{
				ReflectionServices.<GetDeclaredFields>d__11 <GetDeclaredFields>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetDeclaredFields>d__ = this;
				}
				else
				{
					<GetDeclaredFields>d__ = new ReflectionServices.<GetDeclaredFields>d__11(0);
				}
				<GetDeclaredFields>d__.type = type;
				return <GetDeclaredFields>d__;
			}

			// Token: 0x06000053 RID: 83 RVA: 0x00002DB3 File Offset: 0x00000FB3
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Reflection.FieldInfo>.GetEnumerator();
			}

			// Token: 0x0400004D RID: 77
			private int <>1__state;

			// Token: 0x0400004E RID: 78
			private FieldInfo <>2__current;

			// Token: 0x0400004F RID: 79
			private int <>l__initialThreadId;

			// Token: 0x04000050 RID: 80
			private Type type;

			// Token: 0x04000051 RID: 81
			public Type <>3__type;

			// Token: 0x04000052 RID: 82
			private FieldInfo[] <>7__wrap1;

			// Token: 0x04000053 RID: 83
			private int <>7__wrap2;
		}
	}
}
