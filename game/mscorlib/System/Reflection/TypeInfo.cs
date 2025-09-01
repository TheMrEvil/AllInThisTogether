using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	/// <summary>Represents type declarations for class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.</summary>
	// Token: 0x020008D0 RID: 2256
	public abstract class TypeInfo : Type, IReflectableType
	{
		// Token: 0x06004B28 RID: 19240 RVA: 0x000EF9D3 File Offset: 0x000EDBD3
		protected TypeInfo()
		{
		}

		/// <summary>Returns a representation of the current type as a <see cref="T:System.Reflection.TypeInfo" /> object.</summary>
		/// <returns>A reference to the current type.</returns>
		// Token: 0x06004B29 RID: 19241 RVA: 0x0000270D File Offset: 0x0000090D
		TypeInfo IReflectableType.GetTypeInfo()
		{
			return this;
		}

		/// <summary>Returns the current type as a <see cref="T:System.Type" /> object.</summary>
		/// <returns>The current type.</returns>
		// Token: 0x06004B2A RID: 19242 RVA: 0x0000270D File Offset: 0x0000090D
		public virtual Type AsType()
		{
			return this;
		}

		/// <summary>Gets an array of the generic type parameters of the current instance.</summary>
		/// <returns>An array that contains the current instance's generic type parameters, or an array of <see cref="P:System.Array.Length" /> zero if the current instance has no generic type parameters.</returns>
		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06004B2B RID: 19243 RVA: 0x000F0116 File Offset: 0x000EE316
		public virtual Type[] GenericTypeParameters
		{
			get
			{
				if (!this.IsGenericTypeDefinition)
				{
					return Type.EmptyTypes;
				}
				return this.GetGenericArguments();
			}
		}

		/// <summary>Returns an object that represents the specified public event declared by the current type.</summary>
		/// <param name="name">The name of the event.</param>
		/// <returns>An object that represents the specified event, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06004B2C RID: 19244 RVA: 0x000F012C File Offset: 0x000EE32C
		public virtual EventInfo GetDeclaredEvent(string name)
		{
			return this.GetEvent(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Returns an object that represents the specified public field declared by the current type.</summary>
		/// <param name="name">The name of the field.</param>
		/// <returns>An object that represents the specified field, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06004B2D RID: 19245 RVA: 0x000F0137 File Offset: 0x000EE337
		public virtual FieldInfo GetDeclaredField(string name)
		{
			return this.GetField(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Returns an object that represents the specified public method declared by the current type.</summary>
		/// <param name="name">The name of the method.</param>
		/// <returns>An object that represents the specified method, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06004B2E RID: 19246 RVA: 0x000F0142 File Offset: 0x000EE342
		public virtual MethodInfo GetDeclaredMethod(string name)
		{
			return base.GetMethod(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Returns an object that represents the specified public nested type declared by the current type.</summary>
		/// <param name="name">The name of the nested type.</param>
		/// <returns>An object that represents the specified nested type, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06004B2F RID: 19247 RVA: 0x000F014D File Offset: 0x000EE34D
		public virtual TypeInfo GetDeclaredNestedType(string name)
		{
			Type nestedType = this.GetNestedType(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (nestedType == null)
			{
				return null;
			}
			return nestedType.GetTypeInfo();
		}

		/// <summary>Returns an object that represents the specified public property declared by the current type.</summary>
		/// <param name="name">The name of the property.</param>
		/// <returns>An object that represents the specified property, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06004B30 RID: 19248 RVA: 0x000F0163 File Offset: 0x000EE363
		public virtual PropertyInfo GetDeclaredProperty(string name)
		{
			return base.GetProperty(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		/// <summary>Returns a collection that contains all public methods declared on the current type that match the specified name.</summary>
		/// <param name="name">The method name to search for.</param>
		/// <returns>A collection that contains methods that match <paramref name="name" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06004B31 RID: 19249 RVA: 0x000F016E File Offset: 0x000EE36E
		public virtual IEnumerable<MethodInfo> GetDeclaredMethods(string name)
		{
			foreach (MethodInfo methodInfo in this.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (methodInfo.Name == name)
				{
					yield return methodInfo;
				}
			}
			MethodInfo[] array = null;
			yield break;
		}

		/// <summary>Gets a collection of the constructors declared by the current type.</summary>
		/// <returns>A collection of the constructors declared by the current type.</returns>
		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06004B32 RID: 19250 RVA: 0x000F0185 File Offset: 0x000EE385
		public virtual IEnumerable<ConstructorInfo> DeclaredConstructors
		{
			get
			{
				return this.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>Gets a collection of the events defined by the current type.</summary>
		/// <returns>A collection of the events defined by the current type.</returns>
		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06004B33 RID: 19251 RVA: 0x000F018F File Offset: 0x000EE38F
		public virtual IEnumerable<EventInfo> DeclaredEvents
		{
			get
			{
				return this.GetEvents(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>Gets a collection of the fields defined by the current type.</summary>
		/// <returns>A collection of the fields defined by the current type.</returns>
		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06004B34 RID: 19252 RVA: 0x000F0199 File Offset: 0x000EE399
		public virtual IEnumerable<FieldInfo> DeclaredFields
		{
			get
			{
				return this.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>Gets a collection of the members defined by the current type.</summary>
		/// <returns>A collection of the members defined by the current type.</returns>
		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06004B35 RID: 19253 RVA: 0x000F01A3 File Offset: 0x000EE3A3
		public virtual IEnumerable<MemberInfo> DeclaredMembers
		{
			get
			{
				return this.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>Gets a collection of the methods defined by the current type.</summary>
		/// <returns>A collection of the methods defined by the current type.</returns>
		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06004B36 RID: 19254 RVA: 0x000F01AD File Offset: 0x000EE3AD
		public virtual IEnumerable<MethodInfo> DeclaredMethods
		{
			get
			{
				return this.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>Gets a collection of the nested types defined by the current type.</summary>
		/// <returns>A collection of nested types defined by the current type.</returns>
		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06004B37 RID: 19255 RVA: 0x000F01B7 File Offset: 0x000EE3B7
		public virtual IEnumerable<TypeInfo> DeclaredNestedTypes
		{
			get
			{
				foreach (Type type in this.GetNestedTypes(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					yield return type.GetTypeInfo();
				}
				Type[] array = null;
				yield break;
			}
		}

		/// <summary>Gets a collection of the properties defined by the current type.</summary>
		/// <returns>A collection of the properties defined by the current type.</returns>
		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06004B38 RID: 19256 RVA: 0x000F01C7 File Offset: 0x000EE3C7
		public virtual IEnumerable<PropertyInfo> DeclaredProperties
		{
			get
			{
				return this.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		/// <summary>Gets a collection of the interfaces implemented by the current type.</summary>
		/// <returns>A collection of the interfaces implemented by the current type.</returns>
		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06004B39 RID: 19257 RVA: 0x000F01D1 File Offset: 0x000EE3D1
		public virtual IEnumerable<Type> ImplementedInterfaces
		{
			get
			{
				return this.GetInterfaces();
			}
		}

		/// <summary>Returns a value that indicates whether the specified type can be assigned to the current type.</summary>
		/// <param name="typeInfo">The type to check.</param>
		/// <returns>
		///   <see langword="true" /> if the specified type can be assigned to this type; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004B3A RID: 19258 RVA: 0x000F01DC File Offset: 0x000EE3DC
		public virtual bool IsAssignableFrom(TypeInfo typeInfo)
		{
			if (typeInfo == null)
			{
				return false;
			}
			if (this == typeInfo)
			{
				return true;
			}
			if (typeInfo.IsSubclassOf(this))
			{
				return true;
			}
			if (base.IsInterface)
			{
				return typeInfo.ImplementInterface(this);
			}
			if (this.IsGenericParameter)
			{
				Type[] genericParameterConstraints = this.GetGenericParameterConstraints();
				for (int i = 0; i < genericParameterConstraints.Length; i++)
				{
					if (!genericParameterConstraints[i].IsAssignableFrom(typeInfo))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x04002F57 RID: 12119
		private const BindingFlags DeclaredOnlyLookup = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x020008D1 RID: 2257
		[CompilerGenerated]
		private sealed class <GetDeclaredMethods>d__10 : IEnumerable<MethodInfo>, IEnumerable, IEnumerator<MethodInfo>, IDisposable, IEnumerator
		{
			// Token: 0x06004B3B RID: 19259 RVA: 0x000F0247 File Offset: 0x000EE447
			[DebuggerHidden]
			public <GetDeclaredMethods>d__10(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06004B3C RID: 19260 RVA: 0x00004BF9 File Offset: 0x00002DF9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06004B3D RID: 19261 RVA: 0x000F0264 File Offset: 0x000EE464
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				TypeInfo typeInfo = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					array = typeInfo.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					i = 0;
					goto IL_7B;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				IL_6D:
				i++;
				IL_7B:
				if (i >= array.Length)
				{
					array = null;
					return false;
				}
				MethodInfo methodInfo = array[i];
				if (methodInfo.Name == name)
				{
					this.<>2__current = methodInfo;
					this.<>1__state = 1;
					return true;
				}
				goto IL_6D;
			}

			// Token: 0x17000C20 RID: 3104
			// (get) Token: 0x06004B3E RID: 19262 RVA: 0x000F0304 File Offset: 0x000EE504
			MethodInfo IEnumerator<MethodInfo>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004B3F RID: 19263 RVA: 0x000472C8 File Offset: 0x000454C8
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000C21 RID: 3105
			// (get) Token: 0x06004B40 RID: 19264 RVA: 0x000F0304 File Offset: 0x000EE504
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004B41 RID: 19265 RVA: 0x000F030C File Offset: 0x000EE50C
			[DebuggerHidden]
			IEnumerator<MethodInfo> IEnumerable<MethodInfo>.GetEnumerator()
			{
				TypeInfo.<GetDeclaredMethods>d__10 <GetDeclaredMethods>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetDeclaredMethods>d__ = this;
				}
				else
				{
					<GetDeclaredMethods>d__ = new TypeInfo.<GetDeclaredMethods>d__10(0);
					<GetDeclaredMethods>d__.<>4__this = this;
				}
				<GetDeclaredMethods>d__.name = name;
				return <GetDeclaredMethods>d__;
			}

			// Token: 0x06004B42 RID: 19266 RVA: 0x000F035B File Offset: 0x000EE55B
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Reflection.MethodInfo>.GetEnumerator();
			}

			// Token: 0x04002F58 RID: 12120
			private int <>1__state;

			// Token: 0x04002F59 RID: 12121
			private MethodInfo <>2__current;

			// Token: 0x04002F5A RID: 12122
			private int <>l__initialThreadId;

			// Token: 0x04002F5B RID: 12123
			public TypeInfo <>4__this;

			// Token: 0x04002F5C RID: 12124
			private string name;

			// Token: 0x04002F5D RID: 12125
			public string <>3__name;

			// Token: 0x04002F5E RID: 12126
			private MethodInfo[] <>7__wrap1;

			// Token: 0x04002F5F RID: 12127
			private int <>7__wrap2;
		}

		// Token: 0x020008D2 RID: 2258
		[CompilerGenerated]
		private sealed class <get_DeclaredNestedTypes>d__22 : IEnumerable<TypeInfo>, IEnumerable, IEnumerator<TypeInfo>, IDisposable, IEnumerator
		{
			// Token: 0x06004B43 RID: 19267 RVA: 0x000F0363 File Offset: 0x000EE563
			[DebuggerHidden]
			public <get_DeclaredNestedTypes>d__22(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06004B44 RID: 19268 RVA: 0x00004BF9 File Offset: 0x00002DF9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06004B45 RID: 19269 RVA: 0x000F0380 File Offset: 0x000EE580
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				TypeInfo typeInfo = this;
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
					array = typeInfo.GetNestedTypes(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					i = 0;
				}
				if (i >= array.Length)
				{
					array = null;
					return false;
				}
				Type type = array[i];
				this.<>2__current = type.GetTypeInfo();
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000C22 RID: 3106
			// (get) Token: 0x06004B46 RID: 19270 RVA: 0x000F0412 File Offset: 0x000EE612
			TypeInfo IEnumerator<TypeInfo>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004B47 RID: 19271 RVA: 0x000472C8 File Offset: 0x000454C8
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000C23 RID: 3107
			// (get) Token: 0x06004B48 RID: 19272 RVA: 0x000F0412 File Offset: 0x000EE612
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004B49 RID: 19273 RVA: 0x000F041C File Offset: 0x000EE61C
			[DebuggerHidden]
			IEnumerator<TypeInfo> IEnumerable<TypeInfo>.GetEnumerator()
			{
				TypeInfo.<get_DeclaredNestedTypes>d__22 <get_DeclaredNestedTypes>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_DeclaredNestedTypes>d__ = this;
				}
				else
				{
					<get_DeclaredNestedTypes>d__ = new TypeInfo.<get_DeclaredNestedTypes>d__22(0);
					<get_DeclaredNestedTypes>d__.<>4__this = this;
				}
				return <get_DeclaredNestedTypes>d__;
			}

			// Token: 0x06004B4A RID: 19274 RVA: 0x000F045F File Offset: 0x000EE65F
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Reflection.TypeInfo>.GetEnumerator();
			}

			// Token: 0x04002F60 RID: 12128
			private int <>1__state;

			// Token: 0x04002F61 RID: 12129
			private TypeInfo <>2__current;

			// Token: 0x04002F62 RID: 12130
			private int <>l__initialThreadId;

			// Token: 0x04002F63 RID: 12131
			public TypeInfo <>4__this;

			// Token: 0x04002F64 RID: 12132
			private Type[] <>7__wrap1;

			// Token: 0x04002F65 RID: 12133
			private int <>7__wrap2;
		}
	}
}
