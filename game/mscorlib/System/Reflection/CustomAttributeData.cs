using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection
{
	/// <summary>Provides access to custom attribute data for assemblies, modules, types, members and parameters that are loaded into the reflection-only context.</summary>
	// Token: 0x020008EC RID: 2284
	[ComVisible(true)]
	[Serializable]
	public class CustomAttributeData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.CustomAttributeData" /> class.</summary>
		// Token: 0x06004C8E RID: 19598 RVA: 0x0000259F File Offset: 0x0000079F
		protected CustomAttributeData()
		{
		}

		// Token: 0x06004C8F RID: 19599 RVA: 0x000F2BD1 File Offset: 0x000F0DD1
		internal CustomAttributeData(ConstructorInfo ctorInfo, Assembly assembly, IntPtr data, uint data_length)
		{
			this.ctorInfo = ctorInfo;
			this.lazyData = new CustomAttributeData.LazyCAttrData();
			this.lazyData.assembly = assembly;
			this.lazyData.data = data;
			this.lazyData.data_length = data_length;
		}

		// Token: 0x06004C90 RID: 19600 RVA: 0x000F2C10 File Offset: 0x000F0E10
		internal CustomAttributeData(ConstructorInfo ctorInfo) : this(ctorInfo, Array.Empty<CustomAttributeTypedArgument>(), Array.Empty<CustomAttributeNamedArgument>())
		{
		}

		// Token: 0x06004C91 RID: 19601 RVA: 0x000F2C23 File Offset: 0x000F0E23
		internal CustomAttributeData(ConstructorInfo ctorInfo, IList<CustomAttributeTypedArgument> ctorArgs, IList<CustomAttributeNamedArgument> namedArgs)
		{
			this.ctorInfo = ctorInfo;
			this.ctorArgs = ctorArgs;
			this.namedArgs = namedArgs;
		}

		// Token: 0x06004C92 RID: 19602
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResolveArgumentsInternal(ConstructorInfo ctor, Assembly assembly, IntPtr data, uint data_length, out object[] ctorArgs, out object[] namedArgs);

		// Token: 0x06004C93 RID: 19603 RVA: 0x000F2C40 File Offset: 0x000F0E40
		private void ResolveArguments()
		{
			if (this.lazyData == null)
			{
				return;
			}
			object[] array;
			object[] array2;
			CustomAttributeData.ResolveArgumentsInternal(this.ctorInfo, this.lazyData.assembly, this.lazyData.data, this.lazyData.data_length, out array, out array2);
			this.ctorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>((array != null) ? CustomAttributeData.UnboxValues<CustomAttributeTypedArgument>(array) : Array.Empty<CustomAttributeTypedArgument>());
			this.namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>((array2 != null) ? CustomAttributeData.UnboxValues<CustomAttributeNamedArgument>(array2) : Array.Empty<CustomAttributeNamedArgument>());
			this.lazyData = null;
		}

		/// <summary>Gets a <see cref="T:System.Reflection.ConstructorInfo" /> object that represents the constructor that would have initialized the custom attribute.</summary>
		/// <returns>An object that represents the constructor that would have initialized the custom attribute represented by the current instance of the <see cref="T:System.Reflection.CustomAttributeData" /> class.</returns>
		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06004C94 RID: 19604 RVA: 0x000F2CC3 File Offset: 0x000F0EC3
		[ComVisible(true)]
		public virtual ConstructorInfo Constructor
		{
			get
			{
				return this.ctorInfo;
			}
		}

		/// <summary>Gets the list of positional arguments specified for the attribute instance represented by the <see cref="T:System.Reflection.CustomAttributeData" /> object.</summary>
		/// <returns>A collection of structures that represent the positional arguments specified for the custom attribute instance.</returns>
		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06004C95 RID: 19605 RVA: 0x000F2CCB File Offset: 0x000F0ECB
		[ComVisible(true)]
		public virtual IList<CustomAttributeTypedArgument> ConstructorArguments
		{
			get
			{
				this.ResolveArguments();
				return this.ctorArgs;
			}
		}

		/// <summary>Gets the list of named arguments specified for the attribute instance represented by the <see cref="T:System.Reflection.CustomAttributeData" /> object.</summary>
		/// <returns>A collection of structures that represent the named arguments specified for the custom attribute instance.</returns>
		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06004C96 RID: 19606 RVA: 0x000F2CD9 File Offset: 0x000F0ED9
		public virtual IList<CustomAttributeNamedArgument> NamedArguments
		{
			get
			{
				this.ResolveArguments();
				return this.namedArgs;
			}
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target assembly.</summary>
		/// <param name="target">The assembly whose custom attribute data is to be retrieved.</param>
		/// <returns>A list of objects that represent data about the attributes that have been applied to the target assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is <see langword="null" />.</exception>
		// Token: 0x06004C97 RID: 19607 RVA: 0x000F2CE7 File Offset: 0x000F0EE7
		public static IList<CustomAttributeData> GetCustomAttributes(Assembly target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target, false);
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target member.</summary>
		/// <param name="target">The member whose attribute data is to be retrieved.</param>
		/// <returns>A list of objects that represent data about the attributes that have been applied to the target member.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is <see langword="null" />.</exception>
		// Token: 0x06004C98 RID: 19608 RVA: 0x000F2CE7 File Offset: 0x000F0EE7
		public static IList<CustomAttributeData> GetCustomAttributes(MemberInfo target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target, false);
		}

		// Token: 0x06004C99 RID: 19609 RVA: 0x000F2CE7 File Offset: 0x000F0EE7
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeType target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target, false);
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target module.</summary>
		/// <param name="target">The module whose custom attribute data is to be retrieved.</param>
		/// <returns>A list of objects that represent data about the attributes that have been applied to the target module.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is <see langword="null" />.</exception>
		// Token: 0x06004C9A RID: 19610 RVA: 0x000F2CE7 File Offset: 0x000F0EE7
		public static IList<CustomAttributeData> GetCustomAttributes(Module target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target, false);
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the target parameter.</summary>
		/// <param name="target">The parameter whose attribute data is to be retrieved.</param>
		/// <returns>A list of objects that represent data about the attributes that have been applied to the target parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="target" /> is <see langword="null" />.</exception>
		// Token: 0x06004C9B RID: 19611 RVA: 0x000F2CE7 File Offset: 0x000F0EE7
		public static IList<CustomAttributeData> GetCustomAttributes(ParameterInfo target)
		{
			return MonoCustomAttrs.GetCustomAttributesData(target, false);
		}

		/// <summary>Gets the type of the attribute.</summary>
		/// <returns>The type of the attribute.</returns>
		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06004C9C RID: 19612 RVA: 0x000F2CF0 File Offset: 0x000F0EF0
		public Type AttributeType
		{
			get
			{
				return this.ctorInfo.DeclaringType;
			}
		}

		/// <summary>Returns a string representation of the custom attribute.</summary>
		/// <returns>A string value that represents the custom attribute.</returns>
		// Token: 0x06004C9D RID: 19613 RVA: 0x000F2D00 File Offset: 0x000F0F00
		public override string ToString()
		{
			this.ResolveArguments();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[" + this.ctorInfo.DeclaringType.FullName + "(");
			for (int i = 0; i < this.ctorArgs.Count; i++)
			{
				stringBuilder.Append(this.ctorArgs[i].ToString());
				if (i + 1 < this.ctorArgs.Count)
				{
					stringBuilder.Append(", ");
				}
			}
			if (this.namedArgs.Count > 0)
			{
				stringBuilder.Append(", ");
			}
			for (int j = 0; j < this.namedArgs.Count; j++)
			{
				stringBuilder.Append(this.namedArgs[j].ToString());
				if (j + 1 < this.namedArgs.Count)
				{
					stringBuilder.Append(", ");
				}
			}
			stringBuilder.AppendFormat(")]", Array.Empty<object>());
			return stringBuilder.ToString();
		}

		// Token: 0x06004C9E RID: 19614 RVA: 0x000F2E18 File Offset: 0x000F1018
		private static T[] UnboxValues<T>(object[] values)
		{
			T[] array = new T[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				array[i] = (T)((object)values[i]);
			}
			return array;
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is equal to the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004C9F RID: 19615 RVA: 0x000F2E4C File Offset: 0x000F104C
		public override bool Equals(object obj)
		{
			CustomAttributeData customAttributeData = obj as CustomAttributeData;
			if (customAttributeData == null || customAttributeData.ctorInfo != this.ctorInfo || customAttributeData.ctorArgs.Count != this.ctorArgs.Count || customAttributeData.namedArgs.Count != this.namedArgs.Count)
			{
				return false;
			}
			for (int i = 0; i < this.ctorArgs.Count; i++)
			{
				if (this.ctorArgs[i].Equals(customAttributeData.ctorArgs[i]))
				{
					return false;
				}
			}
			for (int j = 0; j < this.namedArgs.Count; j++)
			{
				bool flag = false;
				for (int k = 0; k < customAttributeData.namedArgs.Count; k++)
				{
					if (this.namedArgs[j].Equals(customAttributeData.namedArgs[k]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Serves as a hash function for a particular type.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x06004CA0 RID: 19616 RVA: 0x000F2F5C File Offset: 0x000F115C
		public override int GetHashCode()
		{
			int num = (this.ctorInfo == null) ? 13 : (this.ctorInfo.GetHashCode() << 16);
			if (this.ctorArgs != null)
			{
				for (int i = 0; i < this.ctorArgs.Count; i++)
				{
					num += (num ^ 7 + this.ctorArgs[i].GetHashCode() << i * 4);
				}
			}
			if (this.namedArgs != null)
			{
				for (int j = 0; j < this.namedArgs.Count; j++)
				{
					num += this.namedArgs[j].GetHashCode() << 5;
				}
			}
			return num;
		}

		// Token: 0x04003024 RID: 12324
		private ConstructorInfo ctorInfo;

		// Token: 0x04003025 RID: 12325
		private IList<CustomAttributeTypedArgument> ctorArgs;

		// Token: 0x04003026 RID: 12326
		private IList<CustomAttributeNamedArgument> namedArgs;

		// Token: 0x04003027 RID: 12327
		private CustomAttributeData.LazyCAttrData lazyData;

		// Token: 0x020008ED RID: 2285
		private class LazyCAttrData
		{
			// Token: 0x06004CA1 RID: 19617 RVA: 0x0000259F File Offset: 0x0000079F
			public LazyCAttrData()
			{
			}

			// Token: 0x04003028 RID: 12328
			internal Assembly assembly;

			// Token: 0x04003029 RID: 12329
			internal IntPtr data;

			// Token: 0x0400302A RID: 12330
			internal uint data_length;
		}
	}
}
