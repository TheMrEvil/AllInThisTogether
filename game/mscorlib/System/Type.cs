using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System
{
	/// <summary>Represents type declarations: class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.</summary>
	// Token: 0x020001A1 RID: 417
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_Type))]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	public abstract class Type : MemberInfo, IReflect, _Type
	{
		/// <summary>Returns a value that indicates whether the specified value exists in the current enumeration type.</summary>
		/// <param name="value">The value to be tested.</param>
		/// <returns>
		///   <see langword="true" /> if the specified value is a member of the current enumeration type; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The current type is not an enumeration.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> is of a type that cannot be the underlying type of an enumeration.</exception>
		// Token: 0x06001109 RID: 4361 RVA: 0x000465BC File Offset: 0x000447BC
		public virtual bool IsEnumDefined(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!this.IsEnum)
			{
				throw new ArgumentException("Type provided must be an Enum.", "enumType");
			}
			Type type = value.GetType();
			if (type.IsEnum)
			{
				if (!type.IsEquivalentTo(this))
				{
					throw new ArgumentException(SR.Format("Object must be the same type as the enum. The type passed in was '{0}'; the enum type was '{1}'.", type.ToString(), this.ToString()));
				}
				type = type.GetEnumUnderlyingType();
			}
			if (type == typeof(string))
			{
				object[] enumNames = this.GetEnumNames();
				return Array.IndexOf<object>(enumNames, value) >= 0;
			}
			if (!Type.IsIntegerType(type))
			{
				throw new InvalidOperationException("Unknown enum type.");
			}
			Type enumUnderlyingType = this.GetEnumUnderlyingType();
			if (enumUnderlyingType.GetTypeCodeImpl() != type.GetTypeCodeImpl())
			{
				throw new ArgumentException(SR.Format("Enum underlying type and the object must be same type or object must be a String. Type passed in was '{0}'; the enum underlying type was '{1}'.", type.ToString(), enumUnderlyingType.ToString()));
			}
			return Type.BinarySearch(this.GetEnumRawConstantValues(), value) >= 0;
		}

		/// <summary>Returns the name of the constant that has the specified value, for the current enumeration type.</summary>
		/// <param name="value">The value whose name is to be retrieved.</param>
		/// <returns>The name of the member of the current enumeration type that has the specified value, or <see langword="null" /> if no such constant is found.</returns>
		/// <exception cref="T:System.ArgumentException">The current type is not an enumeration.  
		///  -or-  
		///  <paramref name="value" /> is neither of the current type nor does it have the same underlying type as the current type.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600110A RID: 4362 RVA: 0x000466A8 File Offset: 0x000448A8
		public virtual string GetEnumName(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!this.IsEnum)
			{
				throw new ArgumentException("Type provided must be an Enum.", "enumType");
			}
			Type type = value.GetType();
			if (!type.IsEnum && !Type.IsIntegerType(type))
			{
				throw new ArgumentException("The value passed in must be an enum base or an underlying type for an enum, such as an Int32.", "value");
			}
			int num = Type.BinarySearch(this.GetEnumRawConstantValues(), value);
			if (num >= 0)
			{
				return this.GetEnumNames()[num];
			}
			return null;
		}

		/// <summary>Returns the names of the members of the current enumeration type.</summary>
		/// <returns>An array that contains the names of the members of the enumeration.</returns>
		/// <exception cref="T:System.ArgumentException">The current type is not an enumeration.</exception>
		// Token: 0x0600110B RID: 4363 RVA: 0x00046720 File Offset: 0x00044920
		public virtual string[] GetEnumNames()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException("Type provided must be an Enum.", "enumType");
			}
			string[] result;
			Array array;
			this.GetEnumData(out result, out array);
			return result;
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00046750 File Offset: 0x00044950
		private Array GetEnumRawConstantValues()
		{
			string[] array;
			Array result;
			this.GetEnumData(out array, out result);
			return result;
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00046768 File Offset: 0x00044968
		private void GetEnumData(out string[] enumNames, out Array enumValues)
		{
			FieldInfo[] fields = this.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			object[] array = new object[fields.Length];
			string[] array2 = new string[fields.Length];
			for (int i = 0; i < fields.Length; i++)
			{
				array2[i] = fields[i].Name;
				array[i] = fields[i].GetRawConstantValue();
			}
			IComparer @default = Comparer<object>.Default;
			for (int j = 1; j < array.Length; j++)
			{
				int num = j;
				string text = array2[j];
				object obj = array[j];
				bool flag = false;
				while (@default.Compare(array[num - 1], obj) > 0)
				{
					array2[num] = array2[num - 1];
					array[num] = array[num - 1];
					num--;
					flag = true;
					if (num == 0)
					{
						break;
					}
				}
				if (flag)
				{
					array2[num] = text;
					array[num] = obj;
				}
			}
			enumNames = array2;
			enumValues = array;
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x00046834 File Offset: 0x00044A34
		private static int BinarySearch(Array array, object value)
		{
			ulong[] array2 = new ulong[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = Enum.ToUInt64(array.GetValue(i));
			}
			ulong value2 = Enum.ToUInt64(value);
			return Array.BinarySearch<ulong>(array2, value2);
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0004687C File Offset: 0x00044A7C
		internal static bool IsIntegerType(Type t)
		{
			return t == typeof(int) || t == typeof(short) || t == typeof(ushort) || t == typeof(byte) || t == typeof(sbyte) || t == typeof(uint) || t == typeof(long) || t == typeof(ulong) || t == typeof(char) || t == typeof(bool);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is serializable.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is serializable; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x00046944 File Offset: 0x00044B44
		public virtual bool IsSerializable
		{
			get
			{
				if ((this.GetAttributeFlagsImpl() & TypeAttributes.Serializable) != TypeAttributes.NotPublic)
				{
					return true;
				}
				Type type = this.UnderlyingSystemType;
				if (type.IsRuntimeImplemented())
				{
					while (!(type == typeof(Delegate)) && !(type == typeof(Enum)))
					{
						type = type.BaseType;
						if (!(type != null))
						{
							return false;
						}
					}
					return true;
				}
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> object has type parameters that have not been replaced by specific types.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> object is itself a generic type parameter or has type parameters for which specific types have not been supplied; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x000469A8 File Offset: 0x00044BA8
		public virtual bool ContainsGenericParameters
		{
			get
			{
				if (this.HasElementType)
				{
					return this.GetRootElementType().ContainsGenericParameters;
				}
				if (this.IsGenericParameter)
				{
					return true;
				}
				if (!this.IsGenericType)
				{
					return false;
				}
				Type[] genericArguments = this.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					if (genericArguments[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00046A00 File Offset: 0x00044C00
		internal Type GetRootElementType()
		{
			Type type = this;
			while (type.HasElementType)
			{
				type = type.GetElementType();
			}
			return type;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> can be accessed by code outside the assembly.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Type" /> is a public type or a public nested type such that all the enclosing types are public; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x00046A24 File Offset: 0x00044C24
		public bool IsVisible
		{
			get
			{
				if (this.IsGenericParameter)
				{
					return true;
				}
				if (this.HasElementType)
				{
					return this.GetElementType().IsVisible;
				}
				Type type = this;
				while (type.IsNested)
				{
					if (!type.IsNestedPublic)
					{
						return false;
					}
					type = type.DeclaringType;
				}
				if (!type.IsPublic)
				{
					return false;
				}
				if (this.IsGenericType && !this.IsGenericTypeDefinition)
				{
					Type[] genericArguments = this.GetGenericArguments();
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
		}

		/// <summary>Returns an array of <see cref="T:System.Type" /> objects representing a filtered list of interfaces implemented or inherited by the current <see cref="T:System.Type" />.</summary>
		/// <param name="filter">The delegate that compares the interfaces against <paramref name="filterCriteria" />.</param>
		/// <param name="filterCriteria">The search criteria that determines whether an interface should be included in the returned array.</param>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing a filtered list of the interfaces implemented or inherited by the current <see cref="T:System.Type" />, or an empty array of type <see cref="T:System.Type" /> if no interfaces matching the filter are implemented or inherited by the current <see cref="T:System.Type" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filter" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A static initializer is invoked and throws an exception.</exception>
		// Token: 0x06001114 RID: 4372 RVA: 0x00046AA8 File Offset: 0x00044CA8
		public virtual Type[] FindInterfaces(TypeFilter filter, object filterCriteria)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			Type[] interfaces = this.GetInterfaces();
			int num = 0;
			for (int i = 0; i < interfaces.Length; i++)
			{
				if (!filter(interfaces[i], filterCriteria))
				{
					interfaces[i] = null;
				}
				else
				{
					num++;
				}
			}
			if (num == interfaces.Length)
			{
				return interfaces;
			}
			Type[] array = new Type[num];
			num = 0;
			for (int j = 0; j < interfaces.Length; j++)
			{
				if (interfaces[j] != null)
				{
					array[num++] = interfaces[j];
				}
			}
			return array;
		}

		/// <summary>Returns a filtered array of <see cref="T:System.Reflection.MemberInfo" /> objects of the specified member type.</summary>
		/// <param name="memberType">An object that indicates the type of member to search for.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <param name="filter">The delegate that does the comparisons, returning <see langword="true" /> if the member currently being inspected matches the <paramref name="filterCriteria" /> and <see langword="false" /> otherwise. You can use the <see langword="FilterAttribute" />, <see langword="FilterName" />, and <see langword="FilterNameIgnoreCase" /> delegates supplied by this class. The first uses the fields of <see langword="FieldAttributes" />, <see langword="MethodAttributes" />, and <see langword="MethodImplAttributes" /> as search criteria, and the other two delegates use <see langword="String" /> objects as the search criteria.</param>
		/// <param name="filterCriteria">The search criteria that determines whether a member is returned in the array of <see langword="MemberInfo" /> objects.  
		///  The fields of <see langword="FieldAttributes" />, <see langword="MethodAttributes" />, and <see langword="MethodImplAttributes" /> can be used in conjunction with the <see langword="FilterAttribute" /> delegate supplied by this class.</param>
		/// <returns>A filtered array of <see cref="T:System.Reflection.MemberInfo" /> objects of the specified member type.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.MemberInfo" />, if the current <see cref="T:System.Type" /> does not have members of type <paramref name="memberType" /> that match the filter criteria.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filter" /> is <see langword="null" />.</exception>
		// Token: 0x06001115 RID: 4373 RVA: 0x00046B2C File Offset: 0x00044D2C
		public virtual MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria)
		{
			MethodInfo[] array = null;
			ConstructorInfo[] array2 = null;
			FieldInfo[] array3 = null;
			PropertyInfo[] array4 = null;
			EventInfo[] array5 = null;
			Type[] array6 = null;
			int num = 0;
			if ((memberType & MemberTypes.Method) != (MemberTypes)0)
			{
				array = this.GetMethods(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (!filter(array[i], filterCriteria))
						{
							array[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array.Length;
				}
			}
			if ((memberType & MemberTypes.Constructor) != (MemberTypes)0)
			{
				array2 = this.GetConstructors(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array2.Length; i++)
					{
						if (!filter(array2[i], filterCriteria))
						{
							array2[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array2.Length;
				}
			}
			if ((memberType & MemberTypes.Field) != (MemberTypes)0)
			{
				array3 = this.GetFields(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array3.Length; i++)
					{
						if (!filter(array3[i], filterCriteria))
						{
							array3[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array3.Length;
				}
			}
			if ((memberType & MemberTypes.Property) != (MemberTypes)0)
			{
				array4 = this.GetProperties(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array4.Length; i++)
					{
						if (!filter(array4[i], filterCriteria))
						{
							array4[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array4.Length;
				}
			}
			if ((memberType & MemberTypes.Event) != (MemberTypes)0)
			{
				array5 = this.GetEvents(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array5.Length; i++)
					{
						if (!filter(array5[i], filterCriteria))
						{
							array5[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array5.Length;
				}
			}
			if ((memberType & MemberTypes.NestedType) != (MemberTypes)0)
			{
				array6 = this.GetNestedTypes(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array6.Length; i++)
					{
						if (!filter(array6[i], filterCriteria))
						{
							array6[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array6.Length;
				}
			}
			MemberInfo[] array7 = new MemberInfo[num];
			num = 0;
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != null)
					{
						array7[num++] = array[i];
					}
				}
			}
			if (array2 != null)
			{
				for (int i = 0; i < array2.Length; i++)
				{
					if (array2[i] != null)
					{
						array7[num++] = array2[i];
					}
				}
			}
			if (array3 != null)
			{
				for (int i = 0; i < array3.Length; i++)
				{
					if (array3[i] != null)
					{
						array7[num++] = array3[i];
					}
				}
			}
			if (array4 != null)
			{
				for (int i = 0; i < array4.Length; i++)
				{
					if (array4[i] != null)
					{
						array7[num++] = array4[i];
					}
				}
			}
			if (array5 != null)
			{
				for (int i = 0; i < array5.Length; i++)
				{
					if (array5[i] != null)
					{
						array7[num++] = array5[i];
					}
				}
			}
			if (array6 != null)
			{
				for (int i = 0; i < array6.Length; i++)
				{
					if (array6[i] != null)
					{
						array7[num++] = array6[i];
					}
				}
			}
			return array7;
		}

		/// <summary>Determines whether the current <see cref="T:System.Type" /> derives from the specified <see cref="T:System.Type" />.</summary>
		/// <param name="c">The type to compare with the current type.</param>
		/// <returns>
		///   <see langword="true" /> if the current <see langword="Type" /> derives from <paramref name="c" />; otherwise, <see langword="false" />. This method also returns <see langword="false" /> if <paramref name="c" /> and the current <see langword="Type" /> are equal.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is <see langword="null" />.</exception>
		// Token: 0x06001116 RID: 4374 RVA: 0x00046E38 File Offset: 0x00045038
		[ComVisible(true)]
		public virtual bool IsSubclassOf(Type c)
		{
			Type type = this;
			if (type == c)
			{
				return false;
			}
			while (type != null)
			{
				if (type == c)
				{
					return true;
				}
				type = type.BaseType;
			}
			return false;
		}

		/// <summary>Determines whether an instance of a specified type can be assigned to an instance of the current type.</summary>
		/// <param name="c">The type to compare with the current type.</param>
		/// <returns>
		///   <see langword="true" /> if any of the following conditions is true:  
		///
		/// <paramref name="c" /> and the current instance represent the same type.  
		///
		/// <paramref name="c" /> is derived either directly or indirectly from the current instance. <paramref name="c" /> is derived directly from the current instance if it inherits from the current instance; <paramref name="c" /> is derived indirectly from the current instance if it inherits from a succession of one or more classes that inherit from the current instance.  
		///
		/// The current instance is an interface that <paramref name="c" /> implements.  
		///
		/// <paramref name="c" /> is a generic type parameter, and the current instance represents one of the constraints of <paramref name="c" />.  
		///  In the following example, the current instance is a <see cref="T:System.Type" /> object that represents the <see cref="T:System.IO.Stream" /> class. GenericWithConstraint is a generic type whose generic type parameter must be of type    <see cref="T:System.IO.Stream" />. Passing its generic type parameter to the <see cref="M:System.Type.IsAssignableFrom(System.Type)" /> indicates that  an instance of the generic type parameter can be assigned to an <see cref="T:System.IO.Stream" /> object.  
		/// using System;
		/// using System.IO;
		///
		/// public class Example
		/// {
		///    public static void Main()
		///    {
		/// Type t = typeof(Stream);
		/// Type genericT = typeof(GenericWithConstraint&lt;&gt;);
		/// Type genericParam = genericT.GetGenericArguments()[0];
		/// Console.WriteLine(t.IsAssignableFrom(genericParam));  
		/// // Displays True.
		///    }
		/// }
		///
		/// public class GenericWithConstraint&lt;T&gt; where T : Stream
		/// {}
		/// Imports System.IO
		///
		/// Module Example
		///    Public Sub Main()
		/// Dim t As Type = GetType(Stream)
		/// Dim genericT As Type = GetType(GenericWithConstraint(Of ))
		/// Dim genericParam As Type = genericT.GetGenericArguments()(0)
		/// Console.WriteLine(t.IsAssignableFrom(genericParam))  
		/// ' Displays True.
		///    End Sub
		/// End Module
		///
		/// Public Class GenericWithConstraint(Of T As Stream)
		/// End Class
		///
		/// <paramref name="c" /> represents a value type, and the current instance represents Nullable&lt;c&gt; (Nullable(Of c) in Visual Basic).  
		///
		///
		///  <see langword="false" /> if none of these conditions are true, or if <paramref name="c" /> is <see langword="null" />.</returns>
		// Token: 0x06001117 RID: 4375 RVA: 0x00046E70 File Offset: 0x00045070
		public virtual bool IsAssignableFrom(Type c)
		{
			if (c == null)
			{
				return false;
			}
			if (this == c)
			{
				return true;
			}
			Type underlyingSystemType = this.UnderlyingSystemType;
			if (underlyingSystemType.IsRuntimeImplemented())
			{
				return underlyingSystemType.IsAssignableFrom(c);
			}
			if (c.IsSubclassOf(this))
			{
				return true;
			}
			if (this.IsInterface)
			{
				return c.ImplementInterface(this);
			}
			if (this.IsGenericParameter)
			{
				Type[] genericParameterConstraints = this.GetGenericParameterConstraints();
				for (int i = 0; i < genericParameterConstraints.Length; i++)
				{
					if (!genericParameterConstraints[i].IsAssignableFrom(c))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00046EF4 File Offset: 0x000450F4
		internal bool ImplementInterface(Type ifaceType)
		{
			Type type = this;
			while (type != null)
			{
				Type[] interfaces = type.GetInterfaces();
				if (interfaces != null)
				{
					for (int i = 0; i < interfaces.Length; i++)
					{
						if (interfaces[i] == ifaceType || (interfaces[i] != null && interfaces[i].ImplementInterface(ifaceType)))
						{
							return true;
						}
					}
				}
				type = type.BaseType;
			}
			return false;
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00046F54 File Offset: 0x00045154
		private static bool FilterAttributeImpl(MemberInfo m, object filterCriteria)
		{
			if (filterCriteria == null)
			{
				throw new InvalidFilterCriteriaException("An Int32 must be provided for the filter criteria.");
			}
			MemberTypes memberType = m.MemberType;
			if (memberType != MemberTypes.Constructor)
			{
				if (memberType == MemberTypes.Field)
				{
					FieldAttributes fieldAttributes = FieldAttributes.PrivateScope;
					try
					{
						fieldAttributes = (FieldAttributes)((int)filterCriteria);
					}
					catch
					{
						throw new InvalidFilterCriteriaException("An Int32 must be provided for the filter criteria.");
					}
					FieldAttributes attributes = ((FieldInfo)m).Attributes;
					return ((fieldAttributes & FieldAttributes.FieldAccessMask) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.FieldAccessMask) == (fieldAttributes & FieldAttributes.FieldAccessMask)) && ((fieldAttributes & FieldAttributes.Static) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.InitOnly) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.InitOnly) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.Literal) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.Literal) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.NotSerialized) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.NotSerialized) != FieldAttributes.PrivateScope) && ((fieldAttributes & FieldAttributes.PinvokeImpl) == FieldAttributes.PrivateScope || (attributes & FieldAttributes.PinvokeImpl) != FieldAttributes.PrivateScope);
				}
				if (memberType != MemberTypes.Method)
				{
					return false;
				}
			}
			MethodAttributes methodAttributes = MethodAttributes.PrivateScope;
			try
			{
				methodAttributes = (MethodAttributes)((int)filterCriteria);
			}
			catch
			{
				throw new InvalidFilterCriteriaException("An Int32 must be provided for the filter criteria.");
			}
			MethodAttributes attributes2;
			if (m.MemberType == MemberTypes.Method)
			{
				attributes2 = ((MethodInfo)m).Attributes;
			}
			else
			{
				attributes2 = ((ConstructorInfo)m).Attributes;
			}
			return ((methodAttributes & MethodAttributes.MemberAccessMask) == MethodAttributes.PrivateScope || (attributes2 & MethodAttributes.MemberAccessMask) == (methodAttributes & MethodAttributes.MemberAccessMask)) && ((methodAttributes & MethodAttributes.Static) == MethodAttributes.PrivateScope || (attributes2 & MethodAttributes.Static) != MethodAttributes.PrivateScope) && ((methodAttributes & MethodAttributes.Final) == MethodAttributes.PrivateScope || (attributes2 & MethodAttributes.Final) != MethodAttributes.PrivateScope) && ((methodAttributes & MethodAttributes.Virtual) == MethodAttributes.PrivateScope || (attributes2 & MethodAttributes.Virtual) != MethodAttributes.PrivateScope) && ((methodAttributes & MethodAttributes.Abstract) == MethodAttributes.PrivateScope || (attributes2 & MethodAttributes.Abstract) != MethodAttributes.PrivateScope) && ((methodAttributes & MethodAttributes.SpecialName) == MethodAttributes.PrivateScope || (attributes2 & MethodAttributes.SpecialName) != MethodAttributes.PrivateScope);
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x000470D0 File Offset: 0x000452D0
		private static bool FilterNameImpl(MemberInfo m, object filterCriteria)
		{
			if (filterCriteria == null || !(filterCriteria is string))
			{
				throw new InvalidFilterCriteriaException("A String must be provided for the filter criteria.");
			}
			string text = (string)filterCriteria;
			text = text.Trim();
			string text2 = m.Name;
			if (m.MemberType == MemberTypes.NestedType)
			{
				text2 = text2.Substring(text2.LastIndexOf('+') + 1);
			}
			if (text.Length > 0 && text[text.Length - 1] == '*')
			{
				text = text.Substring(0, text.Length - 1);
				return text2.StartsWith(text, StringComparison.Ordinal);
			}
			return text2.Equals(text);
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00047164 File Offset: 0x00045364
		private static bool FilterNameIgnoreCaseImpl(MemberInfo m, object filterCriteria)
		{
			if (filterCriteria == null || !(filterCriteria is string))
			{
				throw new InvalidFilterCriteriaException("A String must be provided for the filter criteria.");
			}
			string text = (string)filterCriteria;
			text = text.Trim();
			string text2 = m.Name;
			if (m.MemberType == MemberTypes.NestedType)
			{
				text2 = text2.Substring(text2.LastIndexOf('+') + 1);
			}
			if (text.Length > 0 && text[text.Length - 1] == '*')
			{
				text = text.Substring(0, text.Length - 1);
				return string.Compare(text2, 0, text, 0, text.Length, StringComparison.OrdinalIgnoreCase) == 0;
			}
			return string.Compare(text, text2, StringComparison.OrdinalIgnoreCase) == 0;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Type" /> class.</summary>
		// Token: 0x0600111C RID: 4380 RVA: 0x00047204 File Offset: 0x00045404
		protected Type()
		{
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a type or a nested type.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a type or a nested type.</returns>
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x0004720C File Offset: 0x0004540C
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.TypeInfo;
			}
		}

		/// <summary>Gets the current <see cref="T:System.Type" />.</summary>
		/// <returns>The current <see cref="T:System.Type" />.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
		// Token: 0x0600111E RID: 4382 RVA: 0x00047210 File Offset: 0x00045410
		public new Type GetType()
		{
			return base.GetType();
		}

		/// <summary>Gets the namespace of the <see cref="T:System.Type" />.</summary>
		/// <returns>The namespace of the <see cref="T:System.Type" />; <see langword="null" /> if the current instance has no namespace or represents a generic parameter.</returns>
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600111F RID: 4383
		public abstract string Namespace { get; }

		/// <summary>Gets the assembly-qualified name of the type, which includes the name of the assembly from which this <see cref="T:System.Type" /> object was loaded.</summary>
		/// <returns>The assembly-qualified name of the <see cref="T:System.Type" />, which includes the name of the assembly from which the <see cref="T:System.Type" /> was loaded, or <see langword="null" /> if the current instance represents a generic type parameter.</returns>
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06001120 RID: 4384
		public abstract string AssemblyQualifiedName { get; }

		/// <summary>Gets the fully qualified name of the type, including its namespace but not its assembly.</summary>
		/// <returns>The fully qualified name of the type, including its namespace but not its assembly; or <see langword="null" /> if the current instance represents a generic type parameter, an array type, pointer type, or <see langword="byref" /> type based on a type parameter, or a generic type that is not a generic type definition but contains unresolved type parameters.</returns>
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06001121 RID: 4385
		public abstract string FullName { get; }

		/// <summary>Gets the <see cref="T:System.Reflection.Assembly" /> in which the type is declared. For generic types, gets the <see cref="T:System.Reflection.Assembly" /> in which the generic type is defined.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> instance that describes the assembly containing the current type. For generic types, the instance describes the assembly that contains the generic type definition, not the assembly that creates and uses a particular constructed type.</returns>
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06001122 RID: 4386
		public abstract Assembly Assembly { get; }

		/// <summary>Gets the module (the DLL) in which the current <see cref="T:System.Type" /> is defined.</summary>
		/// <returns>The module in which the current <see cref="T:System.Type" /> is defined.</returns>
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06001123 RID: 4387
		public new abstract Module Module { get; }

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> object represents a type whose definition is nested inside the definition of another type.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is nested inside another type; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x00047218 File Offset: 0x00045418
		public bool IsNested
		{
			get
			{
				return this.DeclaringType != null;
			}
		}

		/// <summary>Gets the type that declares the current nested type or generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the enclosing type, if the current type is a nested type; or the generic type definition, if the current type is a type parameter of a generic type; or the type that declares the generic method, if the current type is a type parameter of a generic method; otherwise, <see langword="null" />.</returns>
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x0000AF5E File Offset: 0x0000915E
		public override Type DeclaringType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MethodBase" /> that represents the declaring method, if the current <see cref="T:System.Type" /> represents a type parameter of a generic method.</summary>
		/// <returns>If the current <see cref="T:System.Type" /> represents a type parameter of a generic method, a <see cref="T:System.Reflection.MethodBase" /> that represents declaring method; otherwise, <see langword="null" />.</returns>
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x0000AF5E File Offset: 0x0000915E
		public virtual MethodBase DeclaringMethod
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the class object that was used to obtain this member.</summary>
		/// <returns>The <see langword="Type" /> object through which this <see cref="T:System.Type" /> object was obtained.</returns>
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x0000AF5E File Offset: 0x0000915E
		public override Type ReflectedType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Indicates the type provided by the common language runtime that represents this type.</summary>
		/// <returns>The underlying system type for the <see cref="T:System.Type" />.</returns>
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06001128 RID: 4392
		public abstract Type UnderlyingSystemType { get; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x00047226 File Offset: 0x00045426
		public virtual bool IsTypeDefinition
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Gets a value that indicates whether the type is an array.</summary>
		/// <returns>
		///   <see langword="true" /> if the current type is an array; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x0004722D File Offset: 0x0004542D
		public bool IsArray
		{
			get
			{
				return this.IsArrayImpl();
			}
		}

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.IsArray" /> property and determines whether the <see cref="T:System.Type" /> is an array.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is an array; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600112B RID: 4395
		protected abstract bool IsArrayImpl();

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is passed by reference.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is passed by reference; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x00047235 File Offset: 0x00045435
		public bool IsByRef
		{
			get
			{
				return this.IsByRefImpl();
			}
		}

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.IsByRef" /> property and determines whether the <see cref="T:System.Type" /> is passed by reference.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is passed by reference; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600112D RID: 4397
		protected abstract bool IsByRefImpl();

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is a pointer.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is a pointer; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x0004723D File Offset: 0x0004543D
		public bool IsPointer
		{
			get
			{
				return this.IsPointerImpl();
			}
		}

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.IsPointer" /> property and determines whether the <see cref="T:System.Type" /> is a pointer.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is a pointer; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600112F RID: 4399
		protected abstract bool IsPointerImpl();

		/// <summary>Gets a value that indicates whether this object represents a constructed generic type. You can create instances of a constructed generic type.</summary>
		/// <returns>
		///   <see langword="true" /> if this object represents a constructed generic type; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x00047226 File Offset: 0x00045426
		public virtual bool IsConstructedGenericType
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> represents a type parameter in the definition of a generic type or method.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> object represents a type parameter of a generic type definition or generic method definition; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x00047245 File Offset: 0x00045445
		public virtual bool IsGenericTypeParameter
		{
			get
			{
				return this.IsGenericParameter && this.DeclaringMethod == null;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x0004725D File Offset: 0x0004545D
		public virtual bool IsGenericMethodParameter
		{
			get
			{
				return this.IsGenericParameter && this.DeclaringMethod != null;
			}
		}

		/// <summary>Gets a value indicating whether the current type is a generic type.</summary>
		/// <returns>
		///   <see langword="true" /> if the current type is a generic type; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsGenericType
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> represents a generic type definition, from which other generic types can be constructed.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> object represents a generic type definition; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x00047226 File Offset: 0x00045426
		public virtual bool IsSZArray
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x00047275 File Offset: 0x00045475
		public virtual bool IsVariableBoundArray
		{
			get
			{
				return this.IsArray && !this.IsSZArray;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x0004728A File Offset: 0x0004548A
		public virtual bool IsByRefLike
		{
			get
			{
				throw new NotSupportedException("Derived classes must provide an implementation.");
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> encompasses or refers to another type; that is, whether the current <see cref="T:System.Type" /> is an array, a pointer, or is passed by reference.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is an array, a pointer, or is passed by reference; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x00047296 File Offset: 0x00045496
		public bool HasElementType
		{
			get
			{
				return this.HasElementTypeImpl();
			}
		}

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.HasElementType" /> property and determines whether the current <see cref="T:System.Type" /> encompasses or refers to another type; that is, whether the current <see cref="T:System.Type" /> is an array, a pointer, or is passed by reference.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is an array, a pointer, or is passed by reference; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600113A RID: 4410
		protected abstract bool HasElementTypeImpl();

		/// <summary>When overridden in a derived class, returns the <see cref="T:System.Type" /> of the object encompassed or referred to by the current array, pointer or reference type.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object encompassed or referred to by the current array, pointer, or reference type, or <see langword="null" /> if the current <see cref="T:System.Type" /> is not an array or a pointer, or is not passed by reference, or represents a generic type or a type parameter in the definition of a generic type or generic method.</returns>
		// Token: 0x0600113B RID: 4411
		public abstract Type GetElementType();

		/// <summary>Gets the number of dimensions in an array.</summary>
		/// <returns>An integer that contains the number of dimensions in the current type.</returns>
		/// <exception cref="T:System.NotSupportedException">The functionality of this method is unsupported in the base class and must be implemented in a derived class instead.</exception>
		/// <exception cref="T:System.ArgumentException">The current type is not an array.</exception>
		// Token: 0x0600113C RID: 4412 RVA: 0x0004728A File Offset: 0x0004548A
		public virtual int GetArrayRank()
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a generic type definition from which the current generic type can be constructed.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing a generic type from which the current type can be constructed.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current type is not a generic type.  That is, <see cref="P:System.Type.IsGenericType" /> returns <see langword="false" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
		// Token: 0x0600113D RID: 4413 RVA: 0x0004728A File Offset: 0x0004548A
		public virtual Type GetGenericTypeDefinition()
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		/// <summary>Gets an array of the generic type arguments for this type.</summary>
		/// <returns>An array of the generic type arguments for this type.</returns>
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x0004729E File Offset: 0x0004549E
		public virtual Type[] GenericTypeArguments
		{
			get
			{
				if (!this.IsGenericType || this.IsGenericTypeDefinition)
				{
					return Array.Empty<Type>();
				}
				return this.GetGenericArguments();
			}
		}

		/// <summary>Returns an array of <see cref="T:System.Type" /> objects that represent the type arguments of a closed generic type or the type parameters of a generic type definition.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that represent the type arguments of a generic type. Returns an empty array if the current type is not a generic type.</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
		// Token: 0x0600113F RID: 4415 RVA: 0x0004728A File Offset: 0x0004548A
		public virtual Type[] GetGenericArguments()
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		/// <summary>Gets the position of the type parameter in the type parameter list of the generic type or method that declared the parameter, when the <see cref="T:System.Type" /> object represents a type parameter of a generic type or a generic method.</summary>
		/// <returns>The position of a type parameter in the type parameter list of the generic type or method that defines the parameter. Position numbers begin at 0.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current type does not represent a type parameter. That is, <see cref="P:System.Type.IsGenericParameter" /> returns <see langword="false" />.</exception>
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x000472BC File Offset: 0x000454BC
		public virtual int GenericParameterPosition
		{
			get
			{
				throw new InvalidOperationException("Method may only be called on a Type for which Type.IsGenericParameter is true.");
			}
		}

		/// <summary>Gets a combination of <see cref="T:System.Reflection.GenericParameterAttributes" /> flags that describe the covariance and special constraints of the current generic type parameter.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Reflection.GenericParameterAttributes" /> values that describes the covariance and special constraints of the current generic type parameter.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Type" /> object is not a generic type parameter. That is, the <see cref="P:System.Type.IsGenericParameter" /> property returns <see langword="false" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x000472C8 File Offset: 0x000454C8
		public virtual GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Returns an array of <see cref="T:System.Type" /> objects that represent the constraints on the current generic type parameter.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that represent the constraints on the current generic type parameter.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Type" /> object is not a generic type parameter. That is, the <see cref="P:System.Type.IsGenericParameter" /> property returns <see langword="false" />.</exception>
		// Token: 0x06001142 RID: 4418 RVA: 0x000472CF File Offset: 0x000454CF
		public virtual Type[] GetGenericParameterConstraints()
		{
			if (!this.IsGenericParameter)
			{
				throw new InvalidOperationException("Method may only be called on a Type for which Type.IsGenericParameter is true.");
			}
			throw new InvalidOperationException();
		}

		/// <summary>Gets the attributes associated with the <see cref="T:System.Type" />.</summary>
		/// <returns>A <see cref="T:System.Reflection.TypeAttributes" /> object representing the attribute set of the <see cref="T:System.Type" />, unless the <see cref="T:System.Type" /> represents a generic type parameter, in which case the value is unspecified.</returns>
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x000472E9 File Offset: 0x000454E9
		public TypeAttributes Attributes
		{
			get
			{
				return this.GetAttributeFlagsImpl();
			}
		}

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.Attributes" /> property and gets a bitmask indicating the attributes associated with the <see cref="T:System.Type" />.</summary>
		/// <returns>A <see cref="T:System.Reflection.TypeAttributes" /> object representing the attribute set of the <see cref="T:System.Type" />.</returns>
		// Token: 0x06001144 RID: 4420
		protected abstract TypeAttributes GetAttributeFlagsImpl();

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is abstract and must be overridden.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is abstract; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x000472F1 File Offset: 0x000454F1
		public bool IsAbstract
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.Abstract) > TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> has a <see cref="T:System.Runtime.InteropServices.ComImportAttribute" /> attribute applied, indicating that it was imported from a COM type library.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> has a <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x00047302 File Offset: 0x00045502
		public bool IsImport
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.Import) > TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is declared sealed.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is declared sealed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x00047313 File Offset: 0x00045513
		public bool IsSealed
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.Sealed) > TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the type has a name that requires special handling.</summary>
		/// <returns>
		///   <see langword="true" /> if the type has a name that requires special handling; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x00047324 File Offset: 0x00045524
		public bool IsSpecialName
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.SpecialName) > TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is a class or a delegate; that is, not a value type or interface.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is a class; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x00047335 File Offset: 0x00045535
		public bool IsClass
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic && !this.IsValueType;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is nested and visible only within its own assembly.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is nested and visible only within its own assembly; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x0004734D File Offset: 0x0004554D
		public bool IsNestedAssembly
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedAssembly;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is nested and visible only to classes that belong to both its own family and its own assembly.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is nested and visible only to classes that belong to both its own family and its own assembly; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x0004735A File Offset: 0x0004555A
		public bool IsNestedFamANDAssem
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedFamANDAssem;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is nested and visible only within its own family.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is nested and visible only within its own family; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x00047367 File Offset: 0x00045567
		public bool IsNestedFamily
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedFamily;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is nested and visible only to classes that belong to either its own family or to its own assembly.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is nested and visible only to classes that belong to its own family or to its own assembly; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x00047374 File Offset: 0x00045574
		public bool IsNestedFamORAssem
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.VisibilityMask;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is nested and declared private.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is nested and declared private; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x00047381 File Offset: 0x00045581
		public bool IsNestedPrivate
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPrivate;
			}
		}

		/// <summary>Gets a value indicating whether a class is nested and declared public.</summary>
		/// <returns>
		///   <see langword="true" /> if the class is nested and declared public; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x0004738E File Offset: 0x0004558E
		public bool IsNestedPublic
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPublic;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is not declared public.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is not declared public and is not a nested type; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x0004739B File Offset: 0x0004559B
		public bool IsNotPublic
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is declared public.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is declared public and is not a nested type; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x000473A8 File Offset: 0x000455A8
		public bool IsPublic
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.Public;
			}
		}

		/// <summary>Gets a value indicating whether the fields of the current type are laid out automatically by the common language runtime.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Type.Attributes" /> property of the current type includes <see cref="F:System.Reflection.TypeAttributes.AutoLayout" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x000473B5 File Offset: 0x000455B5
		public bool IsAutoLayout
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.LayoutMask) == TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the fields of the current type are laid out at explicitly specified offsets.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Type.Attributes" /> property of the current type includes <see cref="F:System.Reflection.TypeAttributes.ExplicitLayout" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x000473C3 File Offset: 0x000455C3
		public bool IsExplicitLayout
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.LayoutMask) == TypeAttributes.ExplicitLayout;
			}
		}

		/// <summary>Gets a value indicating whether the fields of the current type are laid out sequentially, in the order that they were defined or emitted to the metadata.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Type.Attributes" /> property of the current type includes <see cref="F:System.Reflection.TypeAttributes.SequentialLayout" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x000473D2 File Offset: 0x000455D2
		public bool IsLayoutSequential
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.LayoutMask) == TypeAttributes.SequentialLayout;
			}
		}

		/// <summary>Gets a value indicating whether the string format attribute <see langword="AnsiClass" /> is selected for the <see cref="T:System.Type" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the string format attribute <see langword="AnsiClass" /> is selected for the <see cref="T:System.Type" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x000473E0 File Offset: 0x000455E0
		public bool IsAnsiClass
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.StringFormatMask) == TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the string format attribute <see langword="AutoClass" /> is selected for the <see cref="T:System.Type" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the string format attribute <see langword="AutoClass" /> is selected for the <see cref="T:System.Type" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x000473F1 File Offset: 0x000455F1
		public bool IsAutoClass
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.StringFormatMask) == TypeAttributes.AutoClass;
			}
		}

		/// <summary>Gets a value indicating whether the string format attribute <see langword="UnicodeClass" /> is selected for the <see cref="T:System.Type" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the string format attribute <see langword="UnicodeClass" /> is selected for the <see cref="T:System.Type" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x00047406 File Offset: 0x00045606
		public bool IsUnicodeClass
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.StringFormatMask) == TypeAttributes.UnicodeClass;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is a COM object.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is a COM object; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x0004741B File Offset: 0x0004561B
		public bool IsCOMObject
		{
			get
			{
				return this.IsCOMObjectImpl();
			}
		}

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.IsCOMObject" /> property and determines whether the <see cref="T:System.Type" /> is a COM object.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is a COM object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001159 RID: 4441
		protected abstract bool IsCOMObjectImpl();

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> can be hosted in a context.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> can be hosted in a context; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x00047423 File Offset: 0x00045623
		public bool IsContextful
		{
			get
			{
				return this.IsContextfulImpl();
			}
		}

		/// <summary>Implements the <see cref="P:System.Type.IsContextful" /> property and determines whether the <see cref="T:System.Type" /> can be hosted in a context.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> can be hosted in a context; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600115B RID: 4443 RVA: 0x0004742B File Offset: 0x0004562B
		protected virtual bool IsContextfulImpl()
		{
			return typeof(ContextBoundObject).IsAssignableFrom(this);
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x000040F7 File Offset: 0x000022F7
		public virtual bool IsCollectible
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> represents an enumeration.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Type" /> represents an enumeration; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x0004743D File Offset: 0x0004563D
		public virtual bool IsEnum
		{
			get
			{
				return this.IsSubclassOf(typeof(Enum));
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is marshaled by reference.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is marshaled by reference; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x0004744F File Offset: 0x0004564F
		public bool IsMarshalByRef
		{
			get
			{
				return this.IsMarshalByRefImpl();
			}
		}

		/// <summary>Implements the <see cref="P:System.Type.IsMarshalByRef" /> property and determines whether the <see cref="T:System.Type" /> is marshaled by reference.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is marshaled by reference; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600115F RID: 4447 RVA: 0x00047457 File Offset: 0x00045657
		protected virtual bool IsMarshalByRefImpl()
		{
			return typeof(MarshalByRefObject).IsAssignableFrom(this);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is one of the primitive types.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is one of the primitive types; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x00047469 File Offset: 0x00045669
		public bool IsPrimitive
		{
			get
			{
				return this.IsPrimitiveImpl();
			}
		}

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.IsPrimitive" /> property and determines whether the <see cref="T:System.Type" /> is one of the primitive types.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is one of the primitive types; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001161 RID: 4449
		protected abstract bool IsPrimitiveImpl();

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is a value type.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is a value type; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x00047471 File Offset: 0x00045671
		public bool IsValueType
		{
			get
			{
				return this.IsValueTypeImpl();
			}
		}

		/// <summary>Implements the <see cref="P:System.Type.IsValueType" /> property and determines whether the <see cref="T:System.Type" /> is a value type; that is, not a class or an interface.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is a value type; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001163 RID: 4451 RVA: 0x00047479 File Offset: 0x00045679
		protected virtual bool IsValueTypeImpl()
		{
			return this.IsSubclassOf(typeof(ValueType));
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsSignatureType
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the current type is security-critical or security-safe-critical at the current trust level, and therefore can perform critical operations.</summary>
		/// <returns>
		///   <see langword="true" /> if the current type is security-critical or security-safe-critical at the current trust level; <see langword="false" /> if it is transparent.</returns>
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x00047226 File Offset: 0x00045426
		public virtual bool IsSecurityCritical
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Gets a value that indicates whether the current type is security-safe-critical at the current trust level; that is, whether it can perform critical operations and can be accessed by transparent code.</summary>
		/// <returns>
		///   <see langword="true" /> if the current type is security-safe-critical at the current trust level; <see langword="false" /> if it is security-critical or transparent.</returns>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x00047226 File Offset: 0x00045426
		public virtual bool IsSecuritySafeCritical
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Gets a value that indicates whether the current type is transparent at the current trust level, and therefore cannot perform critical operations.</summary>
		/// <returns>
		///   <see langword="true" /> if the type is security-transparent at the current trust level; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x00047226 File Offset: 0x00045426
		public virtual bool IsSecurityTransparent
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Gets a <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> that describes the layout of the current type.</summary>
		/// <returns>Gets a <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> that describes the gross layout features of the current type.</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x000472C8 File Offset: 0x000454C8
		public virtual StructLayoutAttribute StructLayoutAttribute
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets the initializer for the type.</summary>
		/// <returns>An object that contains the name of the class constructor for the <see cref="T:System.Type" />.</returns>
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x0004748B File Offset: 0x0004568B
		public ConstructorInfo TypeInitializer
		{
			get
			{
				return this.GetConstructorImpl(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.Any, Type.EmptyTypes, null);
			}
		}

		/// <summary>Searches for a public instance constructor whose parameters match the types in the specified array.</summary>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the desired constructor.  
		///  -or-  
		///  An empty array of <see cref="T:System.Type" /> objects, to get a constructor that takes no parameters. Such an empty array is provided by the <see langword="static" /> field <see cref="F:System.Type.EmptyTypes" />.</param>
		/// <returns>An object representing the public instance constructor whose parameters match the types in the parameter type array, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="types" /> is <see langword="null" />.  
		/// -or-  
		/// One of the elements in <paramref name="types" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.</exception>
		// Token: 0x0600116A RID: 4458 RVA: 0x0004749D File Offset: 0x0004569D
		[ComVisible(true)]
		public ConstructorInfo GetConstructor(Type[] types)
		{
			return this.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, types, null);
		}

		/// <summary>Searches for a constructor whose parameters match the specified argument types and modifiers, using the specified binding constraints.</summary>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.  
		///  -or-  
		///  A null reference (<see langword="Nothing" /> in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the constructor to get.  
		///  -or-  
		///  An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a constructor that takes no parameters.  
		///  -or-  
		///  <see cref="F:System.Type.EmptyTypes" />.</param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the parameter type array. The default binder does not process this parameter.</param>
		/// <returns>A <see cref="T:System.Reflection.ConstructorInfo" /> object representing the constructor that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="types" /> is <see langword="null" />.  
		/// -or-  
		/// One of the elements in <paramref name="types" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.  
		/// -or-  
		/// <paramref name="modifiers" /> is multidimensional.  
		/// -or-  
		/// <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length.</exception>
		// Token: 0x0600116B RID: 4459 RVA: 0x000474AA File Offset: 0x000456AA
		[ComVisible(true)]
		public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetConstructor(bindingAttr, binder, CallingConventions.Any, types, modifiers);
		}

		/// <summary>Searches for a constructor whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.</summary>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.  
		///  -or-  
		///  A null reference (<see langword="Nothing" /> in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />.</param>
		/// <param name="callConvention">The object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and the stack is cleaned up.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the constructor to get.  
		///  -or-  
		///  An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a constructor that takes no parameters.</param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter.</param>
		/// <returns>An object representing the constructor that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="types" /> is <see langword="null" />.  
		/// -or-  
		/// One of the elements in <paramref name="types" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.  
		/// -or-  
		/// <paramref name="modifiers" /> is multidimensional.  
		/// -or-  
		/// <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length.</exception>
		// Token: 0x0600116C RID: 4460 RVA: 0x000474B8 File Offset: 0x000456B8
		[ComVisible(true)]
		public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetConstructorImpl(bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>When overridden in a derived class, searches for a constructor whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.</summary>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.  
		///  -or-  
		///  A null reference (<see langword="Nothing" /> in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />.</param>
		/// <param name="callConvention">The object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and the stack is cleaned up.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the constructor to get.  
		///  -or-  
		///  An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a constructor that takes no parameters.</param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter.</param>
		/// <returns>A <see cref="T:System.Reflection.ConstructorInfo" /> object representing the constructor that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="types" /> is <see langword="null" />.  
		/// -or-  
		/// One of the elements in <paramref name="types" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.  
		/// -or-  
		/// <paramref name="modifiers" /> is multidimensional.  
		/// -or-  
		/// <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length.</exception>
		/// <exception cref="T:System.NotSupportedException">The current type is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> or <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.</exception>
		// Token: 0x0600116D RID: 4461
		protected abstract ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

		/// <summary>Returns all the public constructors defined for the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing all the public instance constructors defined for the current <see cref="T:System.Type" />, but not including the type initializer (static constructor). If no public instance constructors are defined for the current <see cref="T:System.Type" />, or if the current <see cref="T:System.Type" /> represents a type parameter in the definition of a generic type or generic method, an empty array of type <see cref="T:System.Reflection.ConstructorInfo" /> is returned.</returns>
		// Token: 0x0600116E RID: 4462 RVA: 0x00047507 File Offset: 0x00045707
		[ComVisible(true)]
		public ConstructorInfo[] GetConstructors()
		{
			return this.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the constructors defined for the current <see cref="T:System.Type" />, using the specified <see langword="BindingFlags" />.</summary>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>An array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing all constructors defined for the current <see cref="T:System.Type" /> that match the specified binding constraints, including the type initializer if it is defined. Returns an empty array of type <see cref="T:System.Reflection.ConstructorInfo" /> if no constructors are defined for the current <see cref="T:System.Type" />, if none of the defined constructors match the binding constraints, or if the current <see cref="T:System.Type" /> represents a type parameter in the definition of a generic type or generic method.</returns>
		// Token: 0x0600116F RID: 4463
		[ComVisible(true)]
		public abstract ConstructorInfo[] GetConstructors(BindingFlags bindingAttr);

		/// <summary>Returns the <see cref="T:System.Reflection.EventInfo" /> object representing the specified public event.</summary>
		/// <param name="name">The string containing the name of an event that is declared or inherited by the current <see cref="T:System.Type" />.</param>
		/// <returns>The object representing the specified public event that is declared or inherited by the current <see cref="T:System.Type" />, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06001170 RID: 4464 RVA: 0x00047511 File Offset: 0x00045711
		public EventInfo GetEvent(string name)
		{
			return this.GetEvent(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, returns the <see cref="T:System.Reflection.EventInfo" /> object representing the specified event, using the specified binding constraints.</summary>
		/// <param name="name">The string containing the name of an event which is declared or inherited by the current <see cref="T:System.Type" />.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>The object representing the specified event that is declared or inherited by the current <see cref="T:System.Type" />, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06001171 RID: 4465
		public abstract EventInfo GetEvent(string name, BindingFlags bindingAttr);

		/// <summary>Returns all the public events that are declared or inherited by the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.EventInfo" /> objects representing all the public events which are declared or inherited by the current <see cref="T:System.Type" />.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.EventInfo" />, if the current <see cref="T:System.Type" /> does not have public events.</returns>
		// Token: 0x06001172 RID: 4466 RVA: 0x0004751C File Offset: 0x0004571C
		public virtual EventInfo[] GetEvents()
		{
			return this.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for events that are declared or inherited by the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>An array of <see cref="T:System.Reflection.EventInfo" /> objects representing all events that are declared or inherited by the current <see cref="T:System.Type" /> that match the specified binding constraints.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.EventInfo" />, if the current <see cref="T:System.Type" /> does not have events, or if none of the events match the binding constraints.</returns>
		// Token: 0x06001173 RID: 4467
		public abstract EventInfo[] GetEvents(BindingFlags bindingAttr);

		/// <summary>Searches for the public field with the specified name.</summary>
		/// <param name="name">The string containing the name of the data field to get.</param>
		/// <returns>An object representing the public field with the specified name, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">This <see cref="T:System.Type" /> object is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> whose <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> method has not yet been called.</exception>
		// Token: 0x06001174 RID: 4468 RVA: 0x00047526 File Offset: 0x00045726
		public FieldInfo GetField(string name)
		{
			return this.GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Searches for the specified field, using the specified binding constraints.</summary>
		/// <param name="name">The string containing the name of the data field to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>An object representing the field that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06001175 RID: 4469
		public abstract FieldInfo GetField(string name, BindingFlags bindingAttr);

		/// <summary>Returns all the public fields of the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.FieldInfo" /> objects representing all the public fields defined for the current <see cref="T:System.Type" />.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.FieldInfo" />, if no public fields are defined for the current <see cref="T:System.Type" />.</returns>
		// Token: 0x06001176 RID: 4470 RVA: 0x00047531 File Offset: 0x00045731
		public FieldInfo[] GetFields()
		{
			return this.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the fields defined for the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>An array of <see cref="T:System.Reflection.FieldInfo" /> objects representing all fields defined for the current <see cref="T:System.Type" /> that match the specified binding constraints.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.FieldInfo" />, if no fields are defined for the current <see cref="T:System.Type" />, or if none of the defined fields match the binding constraints.</returns>
		// Token: 0x06001177 RID: 4471
		public abstract FieldInfo[] GetFields(BindingFlags bindingAttr);

		/// <summary>Searches for the public members with the specified name.</summary>
		/// <param name="name">The string containing the name of the public members to get.</param>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public members with the specified name, if found; otherwise, an empty array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06001178 RID: 4472 RVA: 0x0004753B File Offset: 0x0004573B
		public MemberInfo[] GetMember(string name)
		{
			return this.GetMember(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Searches for the specified members, using the specified binding constraints.</summary>
		/// <param name="name">The string containing the name of the members to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return an empty array.</param>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public members with the specified name, if found; otherwise, an empty array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06001179 RID: 4473 RVA: 0x00047546 File Offset: 0x00045746
		public virtual MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
		{
			return this.GetMember(name, MemberTypes.All, bindingAttr);
		}

		/// <summary>Searches for the specified members of the specified member type, using the specified binding constraints.</summary>
		/// <param name="name">The string containing the name of the members to get.</param>
		/// <param name="type">The value to search for.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return an empty array.</param>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public members with the specified name, if found; otherwise, an empty array.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">A derived class must provide an implementation.</exception>
		// Token: 0x0600117A RID: 4474 RVA: 0x0004728A File Offset: 0x0004548A
		public virtual MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		/// <summary>Returns all the public members of the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing all the public members of the current <see cref="T:System.Type" />.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.MemberInfo" />, if the current <see cref="T:System.Type" /> does not have public members.</returns>
		// Token: 0x0600117B RID: 4475 RVA: 0x00047555 File Offset: 0x00045755
		public MemberInfo[] GetMembers()
		{
			return this.GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the members defined for the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero (<see cref="F:System.Reflection.BindingFlags.Default" />), to return an empty array.</param>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing all members defined for the current <see cref="T:System.Type" /> that match the specified binding constraints.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.MemberInfo" />, if no members are defined for the current <see cref="T:System.Type" />, or if none of the defined members match the binding constraints.</returns>
		// Token: 0x0600117C RID: 4476
		public abstract MemberInfo[] GetMembers(BindingFlags bindingAttr);

		/// <summary>Searches for the public method with the specified name.</summary>
		/// <param name="name">The string containing the name of the public method to get.</param>
		/// <returns>An object that represents the public method with the specified name, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x0600117D RID: 4477 RVA: 0x0004755F File Offset: 0x0004575F
		public MethodInfo GetMethod(string name)
		{
			return this.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Searches for the specified method, using the specified binding constraints.</summary>
		/// <param name="name">The string containing the name of the method to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>An object representing the method that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x0600117E RID: 4478 RVA: 0x0004756A File Offset: 0x0004576A
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetMethodImpl(name, bindingAttr, null, CallingConventions.Any, null, null);
		}

		/// <summary>Searches for the specified public method whose parameters match the specified argument types.</summary>
		/// <param name="name">The string containing the name of the public method to get.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.  
		///  -or-  
		///  An empty array of <see cref="T:System.Type" /> objects (as provided by the <see cref="F:System.Type.EmptyTypes" /> field) to get a method that takes no parameters.</param>
		/// <returns>An object representing the public method whose parameters match the specified argument types, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and specified parameters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="types" /> is <see langword="null" />.  
		/// -or-  
		/// One of the elements in <paramref name="types" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.</exception>
		// Token: 0x0600117F RID: 4479 RVA: 0x00047586 File Offset: 0x00045786
		public MethodInfo GetMethod(string name, Type[] types)
		{
			return this.GetMethod(name, types, null);
		}

		/// <summary>Searches for the specified public method whose parameters match the specified argument types and modifiers.</summary>
		/// <param name="name">The string containing the name of the public method to get.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.  
		///  -or-  
		///  An empty array of <see cref="T:System.Type" /> objects (as provided by the <see cref="F:System.Type.EmptyTypes" /> field) to get a method that takes no parameters.</param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter.</param>
		/// <returns>An object representing the public method that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and specified parameters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="types" /> is <see langword="null" />.  
		/// -or-  
		/// One of the elements in <paramref name="types" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.  
		/// -or-  
		/// <paramref name="modifiers" /> is multidimensional.</exception>
		// Token: 0x06001180 RID: 4480 RVA: 0x00047591 File Offset: 0x00045791
		public MethodInfo GetMethod(string name, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethod(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, types, modifiers);
		}

		/// <summary>Searches for the specified method whose parameters match the specified argument types and modifiers, using the specified binding constraints.</summary>
		/// <param name="name">The string containing the name of the method to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.  
		///  -or-  
		///  A null reference (<see langword="Nothing" /> in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.  
		///  -or-  
		///  An empty array of <see cref="T:System.Type" /> objects (as provided by the <see cref="F:System.Type.EmptyTypes" /> field) to get a method that takes no parameters.</param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter.</param>
		/// <returns>An object representing the method that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="types" /> is <see langword="null" />.  
		/// -or-  
		/// One of the elements in <paramref name="types" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.  
		/// -or-  
		/// <paramref name="modifiers" /> is multidimensional.</exception>
		// Token: 0x06001181 RID: 4481 RVA: 0x0004759F File Offset: 0x0004579F
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethod(name, bindingAttr, binder, CallingConventions.Any, types, modifiers);
		}

		/// <summary>Searches for the specified method whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.</summary>
		/// <param name="name">The string containing the name of the method to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.  
		///  -or-  
		///  A null reference (<see langword="Nothing" /> in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />.</param>
		/// <param name="callConvention">The object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and how the stack is cleaned up.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.  
		///  -or-  
		///  An empty array of <see cref="T:System.Type" /> objects (as provided by the <see cref="F:System.Type.EmptyTypes" /> field) to get a method that takes no parameters.</param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter.</param>
		/// <returns>An object representing the method that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="types" /> is <see langword="null" />.  
		/// -or-  
		/// One of the elements in <paramref name="types" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.  
		/// -or-  
		/// <paramref name="modifiers" /> is multidimensional.</exception>
		// Token: 0x06001182 RID: 4482 RVA: 0x000475B0 File Offset: 0x000457B0
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetMethodImpl(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>When overridden in a derived class, searches for the specified method whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.</summary>
		/// <param name="name">The string containing the name of the method to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.  
		///  -or-  
		///  A null reference (<see langword="Nothing" /> in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />.</param>
		/// <param name="callConvention">The object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and what process cleans up the stack.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.  
		///  -or-  
		///  An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a method that takes no parameters.  
		///  -or-  
		///  <see langword="null" />. If <paramref name="types" /> is <see langword="null" />, arguments are not matched.</param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter.</param>
		/// <returns>An object representing the method that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.  
		/// -or-  
		/// <paramref name="modifiers" /> is multidimensional.  
		/// -or-  
		/// <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length.</exception>
		/// <exception cref="T:System.NotSupportedException">The current type is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> or <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.</exception>
		// Token: 0x06001183 RID: 4483
		protected abstract MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

		// Token: 0x06001184 RID: 4484 RVA: 0x0004760F File Offset: 0x0004580F
		public MethodInfo GetMethod(string name, int genericParameterCount, Type[] types)
		{
			return this.GetMethod(name, genericParameterCount, types, null);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0004761B File Offset: 0x0004581B
		public MethodInfo GetMethod(string name, int genericParameterCount, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethod(name, genericParameterCount, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, types, modifiers);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0004762B File Offset: 0x0004582B
		public MethodInfo GetMethod(string name, int genericParameterCount, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetMethod(name, genericParameterCount, bindingAttr, binder, CallingConventions.Any, types, modifiers);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00047640 File Offset: 0x00045840
		public MethodInfo GetMethod(string name, int genericParameterCount, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (genericParameterCount < 0)
			{
				throw new ArgumentException("Non-negative number required.", "genericParameterCount");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetMethodImpl(name, genericParameterCount, bindingAttr, binder, callConvention, types, modifiers);
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x000472C8 File Offset: 0x000454C8
		protected virtual MethodInfo GetMethodImpl(string name, int genericParameterCount, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns all the public methods of the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MethodInfo" /> objects representing all the public methods defined for the current <see cref="T:System.Type" />.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.MethodInfo" />, if no public methods are defined for the current <see cref="T:System.Type" />.</returns>
		// Token: 0x06001189 RID: 4489 RVA: 0x000476B5 File Offset: 0x000458B5
		public MethodInfo[] GetMethods()
		{
			return this.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the methods defined for the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>An array of <see cref="T:System.Reflection.MethodInfo" /> objects representing all methods defined for the current <see cref="T:System.Type" /> that match the specified binding constraints.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.MethodInfo" />, if no methods are defined for the current <see cref="T:System.Type" />, or if none of the defined methods match the binding constraints.</returns>
		// Token: 0x0600118A RID: 4490
		public abstract MethodInfo[] GetMethods(BindingFlags bindingAttr);

		/// <summary>Searches for the public nested type with the specified name.</summary>
		/// <param name="name">The string containing the name of the nested type to get.</param>
		/// <returns>An object representing the public nested type with the specified name, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x0600118B RID: 4491 RVA: 0x000476BF File Offset: 0x000458BF
		public Type GetNestedType(string name)
		{
			return this.GetNestedType(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the specified nested type, using the specified binding constraints.</summary>
		/// <param name="name">The string containing the name of the nested type to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>An object representing the nested type that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x0600118C RID: 4492
		public abstract Type GetNestedType(string name, BindingFlags bindingAttr);

		/// <summary>Returns the public types nested in the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing the public types nested in the current <see cref="T:System.Type" /> (the search is not recursive), or an empty array of type <see cref="T:System.Type" /> if no public types are nested in the current <see cref="T:System.Type" />.</returns>
		// Token: 0x0600118D RID: 4493 RVA: 0x000476CA File Offset: 0x000458CA
		public Type[] GetNestedTypes()
		{
			return this.GetNestedTypes(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the types nested in the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing all the types nested in the current <see cref="T:System.Type" /> that match the specified binding constraints (the search is not recursive), or an empty array of type <see cref="T:System.Type" />, if no nested types are found that match the binding constraints.</returns>
		// Token: 0x0600118E RID: 4494
		public abstract Type[] GetNestedTypes(BindingFlags bindingAttr);

		/// <summary>Searches for the public property with the specified name.</summary>
		/// <param name="name">The string containing the name of the public property to get.</param>
		/// <returns>An object representing the public property with the specified name, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x0600118F RID: 4495 RVA: 0x000476D4 File Offset: 0x000458D4
		public PropertyInfo GetProperty(string name)
		{
			return this.GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Searches for the specified property, using the specified binding constraints.</summary>
		/// <param name="name">The string containing the name of the property to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>An object representing the property that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified binding constraints.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x06001190 RID: 4496 RVA: 0x000476DF File Offset: 0x000458DF
		public PropertyInfo GetProperty(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetPropertyImpl(name, bindingAttr, null, null, null, null);
		}

		/// <summary>Searches for the public property with the specified name and return type.</summary>
		/// <param name="name">The string containing the name of the public property to get.</param>
		/// <param name="returnType">The return type of the property.</param>
		/// <returns>An object representing the public property with the specified name, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />, or <paramref name="returnType" /> is <see langword="null" />.</exception>
		// Token: 0x06001191 RID: 4497 RVA: 0x000476FB File Offset: 0x000458FB
		public PropertyInfo GetProperty(string name, Type returnType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (returnType == null)
			{
				throw new ArgumentNullException("returnType");
			}
			return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, returnType, null, null);
		}

		/// <summary>Searches for the specified public property whose parameters match the specified argument types.</summary>
		/// <param name="name">The string containing the name of the public property to get.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.  
		///  -or-  
		///  An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed.</param>
		/// <returns>An object representing the public property whose parameters match the specified argument types, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified argument types.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="types" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.</exception>
		/// <exception cref="T:System.NullReferenceException">An element of <paramref name="types" /> is <see langword="null" />.</exception>
		// Token: 0x06001192 RID: 4498 RVA: 0x0004772C File Offset: 0x0004592C
		public PropertyInfo GetProperty(string name, Type[] types)
		{
			return this.GetProperty(name, null, types);
		}

		/// <summary>Searches for the specified public property whose parameters match the specified argument types.</summary>
		/// <param name="name">The string containing the name of the public property to get.</param>
		/// <param name="returnType">The return type of the property.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.  
		///  -or-  
		///  An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed.</param>
		/// <returns>An object representing the public property whose parameters match the specified argument types, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified argument types.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="types" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.</exception>
		/// <exception cref="T:System.NullReferenceException">An element of <paramref name="types" /> is <see langword="null" />.</exception>
		// Token: 0x06001193 RID: 4499 RVA: 0x00047737 File Offset: 0x00045937
		public PropertyInfo GetProperty(string name, Type returnType, Type[] types)
		{
			return this.GetProperty(name, returnType, types, null);
		}

		/// <summary>Searches for the specified public property whose parameters match the specified argument types and modifiers.</summary>
		/// <param name="name">The string containing the name of the public property to get.</param>
		/// <param name="returnType">The return type of the property.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.  
		///  -or-  
		///  An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed.</param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter.</param>
		/// <returns>An object representing the public property that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified argument types and modifiers.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="types" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.  
		/// -or-  
		/// <paramref name="modifiers" /> is multidimensional.  
		/// -or-  
		/// <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length.</exception>
		/// <exception cref="T:System.NullReferenceException">An element of <paramref name="types" /> is <see langword="null" />.</exception>
		// Token: 0x06001194 RID: 4500 RVA: 0x00047743 File Offset: 0x00045943
		public PropertyInfo GetProperty(string name, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			return this.GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, returnType, types, modifiers);
		}

		/// <summary>Searches for the specified property whose parameters match the specified argument types and modifiers, using the specified binding constraints.</summary>
		/// <param name="name">The string containing the name of the property to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.  
		///  -or-  
		///  A null reference (<see langword="Nothing" /> in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />.</param>
		/// <param name="returnType">The return type of the property.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.  
		///  -or-  
		///  An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed.</param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter.</param>
		/// <returns>An object representing the property that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified binding constraints.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="types" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.  
		/// -or-  
		/// <paramref name="modifiers" /> is multidimensional.  
		/// -or-  
		/// <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length.</exception>
		/// <exception cref="T:System.NullReferenceException">An element of <paramref name="types" /> is <see langword="null" />.</exception>
		// Token: 0x06001195 RID: 4501 RVA: 0x00047753 File Offset: 0x00045953
		public PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			return this.GetPropertyImpl(name, bindingAttr, binder, returnType, types, modifiers);
		}

		/// <summary>When overridden in a derived class, searches for the specified property whose parameters match the specified argument types and modifiers, using the specified binding constraints.</summary>
		/// <param name="name">The string containing the name of the property to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded member, coercion of argument types, and invocation of a member through reflection.  
		///  -or-  
		///  A null reference (<see langword="Nothing" /> in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />.</param>
		/// <param name="returnType">The return type of the property.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.  
		///  -or-  
		///  An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed.</param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter.</param>
		/// <returns>An object representing the property that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified binding constraints.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="types" /> is <see langword="null" />.  
		/// -or-  
		/// One of the elements in <paramref name="types" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.  
		/// -or-  
		/// <paramref name="modifiers" /> is multidimensional.  
		/// -or-  
		/// <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length.</exception>
		/// <exception cref="T:System.NotSupportedException">The current type is a <see cref="T:System.Reflection.Emit.TypeBuilder" />, <see cref="T:System.Reflection.Emit.EnumBuilder" />, or <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.</exception>
		// Token: 0x06001196 RID: 4502
		protected abstract PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		/// <summary>Returns all the public properties of the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.PropertyInfo" /> objects representing all public properties of the current <see cref="T:System.Type" />.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.PropertyInfo" />, if the current <see cref="T:System.Type" /> does not have public properties.</returns>
		// Token: 0x06001197 RID: 4503 RVA: 0x00047781 File Offset: 0x00045981
		public PropertyInfo[] GetProperties()
		{
			return this.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the properties of the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>An array of <see cref="T:System.Reflection.PropertyInfo" /> objects representing all properties of the current <see cref="T:System.Type" /> that match the specified binding constraints.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.PropertyInfo" />, if the current <see cref="T:System.Type" /> does not have properties, or if none of the properties match the binding constraints.</returns>
		// Token: 0x06001198 RID: 4504
		public abstract PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		/// <summary>Searches for the members defined for the current <see cref="T:System.Type" /> whose <see cref="T:System.Reflection.DefaultMemberAttribute" /> is set.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing all default members of the current <see cref="T:System.Type" />.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.MemberInfo" />, if the current <see cref="T:System.Type" /> does not have default members.</returns>
		// Token: 0x06001199 RID: 4505 RVA: 0x00047226 File Offset: 0x00045426
		public virtual MemberInfo[] GetDefaultMembers()
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Gets the handle for the current <see cref="T:System.Type" />.</summary>
		/// <returns>The handle for the current <see cref="T:System.Type" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The .NET Compact Framework does not currently support this property.</exception>
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x000472C8 File Offset: 0x000454C8
		public virtual RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets the handle for the <see cref="T:System.Type" /> of a specified object.</summary>
		/// <param name="o">The object for which to get the type handle.</param>
		/// <returns>The handle for the <see cref="T:System.Type" /> of the specified <see cref="T:System.Object" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="o" /> is <see langword="null" />.</exception>
		// Token: 0x0600119B RID: 4507 RVA: 0x0004778B File Offset: 0x0004598B
		public static RuntimeTypeHandle GetTypeHandle(object o)
		{
			if (o == null)
			{
				throw new ArgumentNullException(null, "Invalid handle.");
			}
			return o.GetType().TypeHandle;
		}

		/// <summary>Gets the types of the objects in the specified array.</summary>
		/// <param name="args">An array of objects whose types to determine.</param>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing the types of the corresponding elements in <paramref name="args" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="args" /> is <see langword="null" />.  
		/// -or-  
		/// One or more of the elements in <paramref name="args" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The class initializers are invoked and at least one throws an exception.</exception>
		// Token: 0x0600119C RID: 4508 RVA: 0x000477A8 File Offset: 0x000459A8
		public static Type[] GetTypeArray(object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			Type[] array = new Type[args.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (args[i] == null)
				{
					throw new ArgumentNullException();
				}
				array[i] = args[i].GetType();
			}
			return array;
		}

		/// <summary>Gets the underlying type code of the specified <see cref="T:System.Type" />.</summary>
		/// <param name="type">The type whose underlying type code to get.</param>
		/// <returns>The code of the underlying type, or <see cref="F:System.TypeCode.Empty" /> if <paramref name="type" /> is <see langword="null" />.</returns>
		// Token: 0x0600119D RID: 4509 RVA: 0x000477F1 File Offset: 0x000459F1
		public static TypeCode GetTypeCode(Type type)
		{
			if (type == null)
			{
				return TypeCode.Empty;
			}
			return type.GetTypeCodeImpl();
		}

		/// <summary>Returns the underlying type code of this <see cref="T:System.Type" /> instance.</summary>
		/// <returns>The type code of the underlying type.</returns>
		// Token: 0x0600119E RID: 4510 RVA: 0x00047804 File Offset: 0x00045A04
		protected virtual TypeCode GetTypeCodeImpl()
		{
			if (this != this.UnderlyingSystemType && this.UnderlyingSystemType != null)
			{
				return Type.GetTypeCode(this.UnderlyingSystemType);
			}
			return TypeCode.Object;
		}

		/// <summary>Gets the GUID associated with the <see cref="T:System.Type" />.</summary>
		/// <returns>The GUID associated with the <see cref="T:System.Type" />.</returns>
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600119F RID: 4511
		public abstract Guid GUID { get; }

		/// <summary>Gets the type associated with the specified class identifier (CLSID).</summary>
		/// <param name="clsid">The CLSID of the type to get.</param>
		/// <returns>
		///   <see langword="System.__ComObject" /> regardless of whether the CLSID is valid.</returns>
		// Token: 0x060011A0 RID: 4512 RVA: 0x0004782F File Offset: 0x00045A2F
		public static Type GetTypeFromCLSID(Guid clsid)
		{
			return Type.GetTypeFromCLSID(clsid, null, false);
		}

		/// <summary>Gets the type associated with the specified class identifier (CLSID), specifying whether to throw an exception if an error occurs while loading the type.</summary>
		/// <param name="clsid">The CLSID of the type to get.</param>
		/// <param name="throwOnError">
		///   <see langword="true" /> to throw any exception that occurs.  
		/// -or-  
		/// <see langword="false" /> to ignore any exception that occurs.</param>
		/// <returns>
		///   <see langword="System.__ComObject" /> regardless of whether the CLSID is valid.</returns>
		// Token: 0x060011A1 RID: 4513 RVA: 0x00047839 File Offset: 0x00045A39
		public static Type GetTypeFromCLSID(Guid clsid, bool throwOnError)
		{
			return Type.GetTypeFromCLSID(clsid, null, throwOnError);
		}

		/// <summary>Gets the type associated with the specified class identifier (CLSID) from the specified server.</summary>
		/// <param name="clsid">The CLSID of the type to get.</param>
		/// <param name="server">The server from which to load the type. If the server name is <see langword="null" />, this method automatically reverts to the local machine.</param>
		/// <returns>
		///   <see langword="System.__ComObject" /> regardless of whether the CLSID is valid.</returns>
		// Token: 0x060011A2 RID: 4514 RVA: 0x00047843 File Offset: 0x00045A43
		public static Type GetTypeFromCLSID(Guid clsid, string server)
		{
			return Type.GetTypeFromCLSID(clsid, server, false);
		}

		/// <summary>Gets the type associated with the specified program identifier (ProgID), returning null if an error is encountered while loading the <see cref="T:System.Type" />.</summary>
		/// <param name="progID">The ProgID of the type to get.</param>
		/// <returns>The type associated with the specified ProgID, if <paramref name="progID" /> is a valid entry in the registry and a type is associated with it; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="progID" /> is <see langword="null" />.</exception>
		// Token: 0x060011A3 RID: 4515 RVA: 0x0004784D File Offset: 0x00045A4D
		public static Type GetTypeFromProgID(string progID)
		{
			return Type.GetTypeFromProgID(progID, null, false);
		}

		/// <summary>Gets the type associated with the specified program identifier (ProgID), specifying whether to throw an exception if an error occurs while loading the type.</summary>
		/// <param name="progID">The ProgID of the type to get.</param>
		/// <param name="throwOnError">
		///   <see langword="true" /> to throw any exception that occurs.  
		/// -or-  
		/// <see langword="false" /> to ignore any exception that occurs.</param>
		/// <returns>The type associated with the specified program identifier (ProgID), if <paramref name="progID" /> is a valid entry in the registry and a type is associated with it; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="progID" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The specified ProgID is not registered.</exception>
		// Token: 0x060011A4 RID: 4516 RVA: 0x00047857 File Offset: 0x00045A57
		public static Type GetTypeFromProgID(string progID, bool throwOnError)
		{
			return Type.GetTypeFromProgID(progID, null, throwOnError);
		}

		/// <summary>Gets the type associated with the specified program identifier (progID) from the specified server, returning null if an error is encountered while loading the type.</summary>
		/// <param name="progID">The progID of the type to get.</param>
		/// <param name="server">The server from which to load the type. If the server name is <see langword="null" />, this method automatically reverts to the local machine.</param>
		/// <returns>The type associated with the specified program identifier (progID), if <paramref name="progID" /> is a valid entry in the registry and a type is associated with it; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="prodID" /> is <see langword="null" />.</exception>
		// Token: 0x060011A5 RID: 4517 RVA: 0x00047861 File Offset: 0x00045A61
		public static Type GetTypeFromProgID(string progID, string server)
		{
			return Type.GetTypeFromProgID(progID, server, false);
		}

		/// <summary>Gets the type from which the current <see cref="T:System.Type" /> directly inherits.</summary>
		/// <returns>The <see cref="T:System.Type" /> from which the current <see cref="T:System.Type" /> directly inherits, or <see langword="null" /> if the current <see langword="Type" /> represents the <see cref="T:System.Object" /> class or an interface.</returns>
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060011A6 RID: 4518
		public abstract Type BaseType { get; }

		/// <summary>Invokes the specified member, using the specified binding constraints and matching the specified argument list.</summary>
		/// <param name="name">The string containing the name of the constructor, method, property, or field member to invoke.  
		///  -or-  
		///  An empty string ("") to invoke the default member.  
		///  -or-  
		///  For <see langword="IDispatch" /> members, a string representing the DispID, for example "[DispID=3]".</param>
		/// <param name="invokeAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted. The access can be one of the <see langword="BindingFlags" /> such as <see langword="Public" />, <see langword="NonPublic" />, <see langword="Private" />, <see langword="InvokeMethod" />, <see langword="GetField" />, and so on. The type of lookup need not be specified. If the type of lookup is omitted, <see langword="BindingFlags.Public" /> | <see langword="BindingFlags.Instance" /> | <see langword="BindingFlags.Static" /> are used.</param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.  
		///  -or-  
		///  A null reference (<see langword="Nothing" /> in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. Note that explicitly defining a <see cref="T:System.Reflection.Binder" /> object may be required for successfully invoking method overloads with variable arguments.</param>
		/// <param name="target">The object on which to invoke the specified member.</param>
		/// <param name="args">An array containing the arguments to pass to the member to invoke.</param>
		/// <returns>An object representing the return value of the invoked member.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="invokeAttr" /> does not contain <see langword="CreateInstance" /> and <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="invokeAttr" /> is not a valid <see cref="T:System.Reflection.BindingFlags" /> attribute.  
		/// -or-  
		/// <paramref name="invokeAttr" /> does not contain one of the following binding flags: <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, or <see langword="SetProperty" />.  
		/// -or-  
		/// <paramref name="invokeAttr" /> contains <see langword="CreateInstance" /> combined with <see langword="InvokeMethod" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, or <see langword="SetProperty" />.  
		/// -or-  
		/// <paramref name="invokeAttr" /> contains both <see langword="GetField" /> and <see langword="SetField" />.  
		/// -or-  
		/// <paramref name="invokeAttr" /> contains both <see langword="GetProperty" /> and <see langword="SetProperty" />.  
		/// -or-  
		/// <paramref name="invokeAttr" /> contains <see langword="InvokeMethod" /> combined with <see langword="SetField" /> or <see langword="SetProperty" />.  
		/// -or-  
		/// <paramref name="invokeAttr" /> contains <see langword="SetField" /> and <paramref name="args" /> has more than one element.  
		/// -or-  
		/// This method is called on a COM object and one of the following binding flags was not passed in: <see langword="BindingFlags.InvokeMethod" />, <see langword="BindingFlags.GetProperty" />, <see langword="BindingFlags.SetProperty" />, <see langword="BindingFlags.PutDispProperty" />, or <see langword="BindingFlags.PutRefDispProperty" />.  
		/// -or-  
		/// One of the named parameter arrays contains a string that is <see langword="null" />.</exception>
		/// <exception cref="T:System.MethodAccessException">The specified member is a class initializer.</exception>
		/// <exception cref="T:System.MissingFieldException">The field or property cannot be found.</exception>
		/// <exception cref="T:System.MissingMethodException">No method can be found that matches the arguments in <paramref name="args" />.  
		///  -or-  
		///  The current <see cref="T:System.Type" /> object represents a type that contains open type parameters, that is, <see cref="P:System.Type.ContainsGenericParameters" /> returns <see langword="true" />.</exception>
		/// <exception cref="T:System.Reflection.TargetException">The specified member cannot be invoked on <paramref name="target" />.</exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method matches the binding criteria.</exception>
		/// <exception cref="T:System.NotSupportedException">The .NET Compact Framework does not currently support this method.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method represented by <paramref name="name" /> has one or more unspecified generic type parameters. That is, the method's <see cref="P:System.Reflection.MethodInfo.ContainsGenericParameters" /> property returns <see langword="true" />.</exception>
		// Token: 0x060011A7 RID: 4519 RVA: 0x0004786C File Offset: 0x00045A6C
		[DebuggerHidden]
		[DebuggerStepThrough]
		public object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args)
		{
			return this.InvokeMember(name, invokeAttr, binder, target, args, null, null, null);
		}

		/// <summary>Invokes the specified member, using the specified binding constraints and matching the specified argument list and culture.</summary>
		/// <param name="name">The string containing the name of the constructor, method, property, or field member to invoke.  
		///  -or-  
		///  An empty string ("") to invoke the default member.  
		///  -or-  
		///  For <see langword="IDispatch" /> members, a string representing the DispID, for example "[DispID=3]".</param>
		/// <param name="invokeAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted. The access can be one of the <see langword="BindingFlags" /> such as <see langword="Public" />, <see langword="NonPublic" />, <see langword="Private" />, <see langword="InvokeMethod" />, <see langword="GetField" />, and so on. The type of lookup need not be specified. If the type of lookup is omitted, <see langword="BindingFlags.Public" /> | <see langword="BindingFlags.Instance" /> | <see langword="BindingFlags.Static" /> are used.</param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.  
		///  -or-  
		///  A null reference (<see langword="Nothing" /> in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. Note that explicitly defining a <see cref="T:System.Reflection.Binder" /> object may be required for successfully invoking method overloads with variable arguments.</param>
		/// <param name="target">The object on which to invoke the specified member.</param>
		/// <param name="args">An array containing the arguments to pass to the member to invoke.</param>
		/// <param name="culture">The object representing the globalization locale to use, which may be necessary for locale-specific conversions, such as converting a numeric <see cref="T:System.String" /> to a <see cref="T:System.Double" />.  
		///  -or-  
		///  A null reference (<see langword="Nothing" /> in Visual Basic) to use the current thread's <see cref="T:System.Globalization.CultureInfo" />.</param>
		/// <returns>An object representing the return value of the invoked member.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="invokeAttr" /> does not contain <see langword="CreateInstance" /> and <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="invokeAttr" /> is not a valid <see cref="T:System.Reflection.BindingFlags" /> attribute.  
		/// -or-  
		/// <paramref name="invokeAttr" /> does not contain one of the following binding flags: <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, or <see langword="SetProperty" />.  
		/// -or-  
		/// <paramref name="invokeAttr" /> contains <see langword="CreateInstance" /> combined with <see langword="InvokeMethod" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, or <see langword="SetProperty" />.  
		/// -or-  
		/// <paramref name="invokeAttr" /> contains both <see langword="GetField" /> and <see langword="SetField" />.  
		/// -or-  
		/// <paramref name="invokeAttr" /> contains both <see langword="GetProperty" /> and <see langword="SetProperty" />.  
		/// -or-  
		/// <paramref name="invokeAttr" /> contains <see langword="InvokeMethod" /> combined with <see langword="SetField" /> or <see langword="SetProperty" />.  
		/// -or-  
		/// <paramref name="invokeAttr" /> contains <see langword="SetField" /> and <paramref name="args" /> has more than one element.  
		/// -or-  
		/// This method is called on a COM object and one of the following binding flags was not passed in: <see langword="BindingFlags.InvokeMethod" />, <see langword="BindingFlags.GetProperty" />, <see langword="BindingFlags.SetProperty" />, <see langword="BindingFlags.PutDispProperty" />, or <see langword="BindingFlags.PutRefDispProperty" />.  
		/// -or-  
		/// One of the named parameter arrays contains a string that is <see langword="null" />.</exception>
		/// <exception cref="T:System.MethodAccessException">The specified member is a class initializer.</exception>
		/// <exception cref="T:System.MissingFieldException">The field or property cannot be found.</exception>
		/// <exception cref="T:System.MissingMethodException">No method can be found that matches the arguments in <paramref name="args" />.  
		///  -or-  
		///  The current <see cref="T:System.Type" /> object represents a type that contains open type parameters, that is, <see cref="P:System.Type.ContainsGenericParameters" /> returns <see langword="true" />.</exception>
		/// <exception cref="T:System.Reflection.TargetException">The specified member cannot be invoked on <paramref name="target" />.</exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method matches the binding criteria.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method represented by <paramref name="name" /> has one or more unspecified generic type parameters. That is, the method's <see cref="P:System.Reflection.MethodInfo.ContainsGenericParameters" /> property returns <see langword="true" />.</exception>
		// Token: 0x060011A8 RID: 4520 RVA: 0x0004788C File Offset: 0x00045A8C
		[DebuggerHidden]
		[DebuggerStepThrough]
		public object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, CultureInfo culture)
		{
			return this.InvokeMember(name, invokeAttr, binder, target, args, null, culture, null);
		}

		/// <summary>When overridden in a derived class, invokes the specified member, using the specified binding constraints and matching the specified argument list, modifiers and culture.</summary>
		/// <param name="name">The string containing the name of the constructor, method, property, or field member to invoke.  
		///  -or-  
		///  An empty string ("") to invoke the default member.  
		///  -or-  
		///  For <see langword="IDispatch" /> members, a string representing the DispID, for example "[DispID=3]".</param>
		/// <param name="invokeAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted. The access can be one of the <see langword="BindingFlags" /> such as <see langword="Public" />, <see langword="NonPublic" />, <see langword="Private" />, <see langword="InvokeMethod" />, <see langword="GetField" />, and so on. The type of lookup need not be specified. If the type of lookup is omitted, <see langword="BindingFlags.Public" /> | <see langword="BindingFlags.Instance" /> | <see langword="BindingFlags.Static" /> are used.</param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.  
		///  -or-  
		///  A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. Note that explicitly defining a <see cref="T:System.Reflection.Binder" /> object may be required for successfully invoking method overloads with variable arguments.</param>
		/// <param name="target">The object on which to invoke the specified member.</param>
		/// <param name="args">An array containing the arguments to pass to the member to invoke.</param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="args" /> array. A parameter's associated attributes are stored in the member's signature.  
		///  The default binder processes this parameter only when calling a COM component.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> object representing the globalization locale to use, which may be necessary for locale-specific conversions, such as converting a numeric String to a Double.  
		///  -or-  
		///  A null reference (<see langword="Nothing" /> in Visual Basic) to use the current thread's <see cref="T:System.Globalization.CultureInfo" />.</param>
		/// <param name="namedParameters">An array containing the names of the parameters to which the values in the <paramref name="args" /> array are passed.</param>
		/// <returns>An object representing the return value of the invoked member.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="invokeAttr" /> does not contain <see langword="CreateInstance" /> and <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="args" /> and <paramref name="modifiers" /> do not have the same length.  
		/// -or-  
		/// <paramref name="invokeAttr" /> is not a valid <see cref="T:System.Reflection.BindingFlags" /> attribute.  
		/// -or-  
		/// <paramref name="invokeAttr" /> does not contain one of the following binding flags: <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, or <see langword="SetProperty" />.  
		/// -or-  
		/// <paramref name="invokeAttr" /> contains <see langword="CreateInstance" /> combined with <see langword="InvokeMethod" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, or <see langword="SetProperty" />.  
		/// -or-  
		/// <paramref name="invokeAttr" /> contains both <see langword="GetField" /> and <see langword="SetField" />.  
		/// -or-  
		/// <paramref name="invokeAttr" /> contains both <see langword="GetProperty" /> and <see langword="SetProperty" />.  
		/// -or-  
		/// <paramref name="invokeAttr" /> contains <see langword="InvokeMethod" /> combined with <see langword="SetField" /> or <see langword="SetProperty" />.  
		/// -or-  
		/// <paramref name="invokeAttr" /> contains <see langword="SetField" /> and <paramref name="args" /> has more than one element.  
		/// -or-  
		/// The named parameter array is larger than the argument array.  
		/// -or-  
		/// This method is called on a COM object and one of the following binding flags was not passed in: <see langword="BindingFlags.InvokeMethod" />, <see langword="BindingFlags.GetProperty" />, <see langword="BindingFlags.SetProperty" />, <see langword="BindingFlags.PutDispProperty" />, or <see langword="BindingFlags.PutRefDispProperty" />.  
		/// -or-  
		/// One of the named parameter arrays contains a string that is <see langword="null" />.</exception>
		/// <exception cref="T:System.MethodAccessException">The specified member is a class initializer.</exception>
		/// <exception cref="T:System.MissingFieldException">The field or property cannot be found.</exception>
		/// <exception cref="T:System.MissingMethodException">No method can be found that matches the arguments in <paramref name="args" />.  
		///  -or-  
		///  No member can be found that has the argument names supplied in <paramref name="namedParameters" />.  
		///  -or-  
		///  The current <see cref="T:System.Type" /> object represents a type that contains open type parameters, that is, <see cref="P:System.Type.ContainsGenericParameters" /> returns <see langword="true" />.</exception>
		/// <exception cref="T:System.Reflection.TargetException">The specified member cannot be invoked on <paramref name="target" />.</exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method matches the binding criteria.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method represented by <paramref name="name" /> has one or more unspecified generic type parameters. That is, the method's <see cref="P:System.Reflection.MethodInfo.ContainsGenericParameters" /> property returns <see langword="true" />.</exception>
		// Token: 0x060011A9 RID: 4521
		public abstract object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		/// <summary>Searches for the interface with the specified name.</summary>
		/// <param name="name">The string containing the name of the interface to get. For generic interfaces, this is the mangled name.</param>
		/// <returns>An object representing the interface with the specified name, implemented or inherited by the current <see cref="T:System.Type" />, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">The current <see cref="T:System.Type" /> represents a type that implements the same generic interface with different type arguments.</exception>
		// Token: 0x060011AA RID: 4522 RVA: 0x000478AA File Offset: 0x00045AAA
		public Type GetInterface(string name)
		{
			return this.GetInterface(name, false);
		}

		/// <summary>When overridden in a derived class, searches for the specified interface, specifying whether to do a case-insensitive search for the interface name.</summary>
		/// <param name="name">The string containing the name of the interface to get. For generic interfaces, this is the mangled name.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to ignore the case of that part of <paramref name="name" /> that specifies the simple interface name (the part that specifies the namespace must be correctly cased).  
		/// -or-  
		/// <see langword="false" /> to perform a case-sensitive search for all parts of <paramref name="name" />.</param>
		/// <returns>An object representing the interface with the specified name, implemented or inherited by the current <see cref="T:System.Type" />, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">The current <see cref="T:System.Type" /> represents a type that implements the same generic interface with different type arguments.</exception>
		// Token: 0x060011AB RID: 4523
		public abstract Type GetInterface(string name, bool ignoreCase);

		/// <summary>When overridden in a derived class, gets all the interfaces implemented or inherited by the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing all the interfaces implemented or inherited by the current <see cref="T:System.Type" />.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Type" />, if no interfaces are implemented or inherited by the current <see cref="T:System.Type" />.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A static initializer is invoked and throws an exception.</exception>
		// Token: 0x060011AC RID: 4524
		public abstract Type[] GetInterfaces();

		/// <summary>Returns an interface mapping for the specified interface type.</summary>
		/// <param name="interfaceType">The interface type to retrieve a mapping for.</param>
		/// <returns>An object that represents the interface mapping for <paramref name="interfaceType" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="interfaceType" /> is not implemented by the current type.  
		/// -or-  
		/// The <paramref name="interfaceType" /> argument does not refer to an interface.  
		/// -or-  
		/// The current instance or <paramref name="interfaceType" /> argument is an open generic type; that is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns <see langword="true" />.
		/// -or-
		///  <paramref name="interfaceType" /> is a generic interface, and the current type is an array type.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="interfaceType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Type" /> represents a generic type parameter; that is, <see cref="P:System.Type.IsGenericParameter" /> is <see langword="true" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
		// Token: 0x060011AD RID: 4525 RVA: 0x0004728A File Offset: 0x0004548A
		[ComVisible(true)]
		public virtual InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		/// <summary>Determines whether the specified object is an instance of the current <see cref="T:System.Type" />.</summary>
		/// <param name="o">The object to compare with the current type.</param>
		/// <returns>
		///   <see langword="true" /> if the current <see langword="Type" /> is in the inheritance hierarchy of the object represented by <paramref name="o" />, or if the current <see langword="Type" /> is an interface that <paramref name="o" /> implements. <see langword="false" /> if neither of these conditions is the case, if <paramref name="o" /> is <see langword="null" />, or if the current <see langword="Type" /> is an open generic type (that is, <see cref="P:System.Type.ContainsGenericParameters" /> returns <see langword="true" />).</returns>
		// Token: 0x060011AE RID: 4526 RVA: 0x000478B4 File Offset: 0x00045AB4
		public virtual bool IsInstanceOfType(object o)
		{
			return o != null && this.IsAssignableFrom(o.GetType());
		}

		/// <summary>Determines whether two COM types have the same identity and are eligible for type equivalence.</summary>
		/// <param name="other">The COM type that is tested for equivalence with the current type.</param>
		/// <returns>
		///   <see langword="true" /> if the COM types are equivalent; otherwise, <see langword="false" />. This method also returns <see langword="false" /> if one type is in an assembly that is loaded for execution, and the other is in an assembly that is loaded into the reflection-only context.</returns>
		// Token: 0x060011AF RID: 4527 RVA: 0x000478C7 File Offset: 0x00045AC7
		public virtual bool IsEquivalentTo(Type other)
		{
			return this == other;
		}

		/// <summary>Returns the underlying type of the current enumeration type.</summary>
		/// <returns>The underlying type of the current enumeration.</returns>
		/// <exception cref="T:System.ArgumentException">The current type is not an enumeration.  
		///  -or-  
		///  The enumeration type is not valid, because it contains more than one instance field.</exception>
		// Token: 0x060011B0 RID: 4528 RVA: 0x000478D0 File Offset: 0x00045AD0
		public virtual Type GetEnumUnderlyingType()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException("Type provided must be an Enum.", "enumType");
			}
			FieldInfo[] fields = this.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (fields == null || fields.Length != 1)
			{
				throw new ArgumentException("The Enum type should contain one and only one instance field.", "enumType");
			}
			return fields[0].FieldType;
		}

		/// <summary>Returns an array of the values of the constants in the current enumeration type.</summary>
		/// <returns>An array that contains the values. The elements of the array are sorted by the binary values (that is, the unsigned values) of the enumeration constants.</returns>
		/// <exception cref="T:System.ArgumentException">The current type is not an enumeration.</exception>
		// Token: 0x060011B1 RID: 4529 RVA: 0x0004791F File Offset: 0x00045B1F
		public virtual Array GetEnumValues()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException("Type provided must be an Enum.", "enumType");
			}
			throw NotImplemented.ByDesign;
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object representing a one-dimensional array of the current type, with a lower bound of zero.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing a one-dimensional array of the current type, with a lower bound of zero.</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
		/// <exception cref="T:System.TypeLoadException">The current type is <see cref="T:System.TypedReference" />.  
		///  -or-  
		///  The current type is a <see langword="ByRef" /> type. That is, <see cref="P:System.Type.IsByRef" /> returns <see langword="true" />.</exception>
		// Token: 0x060011B2 RID: 4530 RVA: 0x000472C8 File Offset: 0x000454C8
		public virtual Type MakeArrayType()
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object representing an array of the current type, with the specified number of dimensions.</summary>
		/// <param name="rank">The number of dimensions for the array. This number must be less than or equal to 32.</param>
		/// <returns>An object representing an array of the current type, with the specified number of dimensions.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="rank" /> is invalid. For example, 0 or negative.</exception>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		/// <exception cref="T:System.TypeLoadException">The current type is <see cref="T:System.TypedReference" />.  
		///  -or-  
		///  The current type is a <see langword="ByRef" /> type. That is, <see cref="P:System.Type.IsByRef" /> returns <see langword="true" />.  
		///  -or-  
		///  <paramref name="rank" /> is greater than 32.</exception>
		// Token: 0x060011B3 RID: 4531 RVA: 0x000472C8 File Offset: 0x000454C8
		public virtual Type MakeArrayType(int rank)
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents the current type when passed as a <see langword="ref" /> parameter (<see langword="ByRef" /> parameter in Visual Basic).</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the current type when passed as a <see langword="ref" /> parameter (<see langword="ByRef" /> parameter in Visual Basic).</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		/// <exception cref="T:System.TypeLoadException">The current type is <see cref="T:System.TypedReference" />.  
		///  -or-  
		///  The current type is a <see langword="ByRef" /> type. That is, <see cref="P:System.Type.IsByRef" /> returns <see langword="true" />.</exception>
		// Token: 0x060011B4 RID: 4532 RVA: 0x000472C8 File Offset: 0x000454C8
		public virtual Type MakeByRefType()
		{
			throw new NotSupportedException();
		}

		/// <summary>Substitutes the elements of an array of types for the type parameters of the current generic type definition and returns a <see cref="T:System.Type" /> object representing the resulting constructed type.</summary>
		/// <param name="typeArguments">An array of types to be substituted for the type parameters of the current generic type.</param>
		/// <returns>A <see cref="T:System.Type" /> representing the constructed type formed by substituting the elements of <paramref name="typeArguments" /> for the type parameters of the current generic type.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current type does not represent a generic type definition. That is, <see cref="P:System.Type.IsGenericTypeDefinition" /> returns <see langword="false" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeArguments" /> is <see langword="null" />.  
		/// -or-  
		/// Any element of <paramref name="typeArguments" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in <paramref name="typeArguments" /> is not the same as the number of type parameters in the current generic type definition.  
		///  -or-  
		///  Any element of <paramref name="typeArguments" /> does not satisfy the constraints specified for the corresponding type parameter of the current generic type.  
		///  -or-  
		///  <paramref name="typeArguments" /> contains an element that is a pointer type (<see cref="P:System.Type.IsPointer" /> returns <see langword="true" />), a by-ref type (<see cref="P:System.Type.IsByRef" /> returns <see langword="true" />), or <see cref="T:System.Void" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
		// Token: 0x060011B5 RID: 4533 RVA: 0x0004728A File Offset: 0x0004548A
		public virtual Type MakeGenericType(params Type[] typeArguments)
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a pointer to the current type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents a pointer to the current type.</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		/// <exception cref="T:System.TypeLoadException">The current type is <see cref="T:System.TypedReference" />.  
		///  -or-  
		///  The current type is a <see langword="ByRef" /> type. That is, <see cref="P:System.Type.IsByRef" /> returns <see langword="true" />.</exception>
		// Token: 0x060011B6 RID: 4534 RVA: 0x000472C8 File Offset: 0x000454C8
		public virtual Type MakePointerType()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0004793E File Offset: 0x00045B3E
		public static Type MakeGenericSignatureType(Type genericTypeDefinition, params Type[] typeArguments)
		{
			return new SignatureConstructedGenericType(genericTypeDefinition, typeArguments);
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00047947 File Offset: 0x00045B47
		public static Type MakeGenericMethodParameter(int position)
		{
			if (position < 0)
			{
				throw new ArgumentException("Non-negative number required.", "position");
			}
			return new SignatureGenericMethodParameterType(position);
		}

		/// <summary>Returns a <see langword="String" /> representing the name of the current <see langword="Type" />.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the name of the current <see cref="T:System.Type" />.</returns>
		// Token: 0x060011B9 RID: 4537 RVA: 0x00047963 File Offset: 0x00045B63
		public override string ToString()
		{
			return "Type: " + this.Name;
		}

		/// <summary>Determines if the underlying system type of the current <see cref="T:System.Type" /> object is the same as the underlying system type of the specified <see cref="T:System.Object" />.</summary>
		/// <param name="o">The object whose underlying system type is to be compared with the underlying system type of the current <see cref="T:System.Type" />. For the comparison to succeed, <paramref name="o" /> must be able to be cast or converted to an object of type   <see cref="T:System.Type" />.</param>
		/// <returns>
		///   <see langword="true" /> if the underlying system type of <paramref name="o" /> is the same as the underlying system type of the current <see cref="T:System.Type" />; otherwise, <see langword="false" />. This method also returns <see langword="false" /> if: .  
		///
		/// <paramref name="o" /> is <see langword="null" />.  
		///
		/// <paramref name="o" /> cannot be cast or converted to a <see cref="T:System.Type" /> object.</returns>
		// Token: 0x060011BA RID: 4538 RVA: 0x00047975 File Offset: 0x00045B75
		public override bool Equals(object o)
		{
			return o != null && this.Equals(o as Type);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code for this instance.</returns>
		// Token: 0x060011BB RID: 4539 RVA: 0x00047988 File Offset: 0x00045B88
		public override int GetHashCode()
		{
			Type underlyingSystemType = this.UnderlyingSystemType;
			if (underlyingSystemType != this)
			{
				return underlyingSystemType.GetHashCode();
			}
			return base.GetHashCode();
		}

		/// <summary>Determines if the underlying system type of the current <see cref="T:System.Type" /> is the same as the underlying system type of the specified <see cref="T:System.Type" />.</summary>
		/// <param name="o">The object whose underlying system type is to be compared with the underlying system type of the current <see cref="T:System.Type" />.</param>
		/// <returns>
		///   <see langword="true" /> if the underlying system type of <paramref name="o" /> is the same as the underlying system type of the current <see cref="T:System.Type" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060011BC RID: 4540 RVA: 0x000479AD File Offset: 0x00045BAD
		public virtual bool Equals(Type o)
		{
			return !(o == null) && this.UnderlyingSystemType == o.UnderlyingSystemType;
		}

		/// <summary>Gets a reference to the default binder, which implements internal rules for selecting the appropriate members to be called by <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" />.</summary>
		/// <returns>A reference to the default binder used by the system.</returns>
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x000479C8 File Offset: 0x00045BC8
		public static Binder DefaultBinder
		{
			get
			{
				if (Type.s_defaultBinder == null)
				{
					DefaultBinder value = new DefaultBinder();
					Interlocked.CompareExchange<Binder>(ref Type.s_defaultBinder, value, null);
				}
				return Type.s_defaultBinder;
			}
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060011BE RID: 4542 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _Type.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">A pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060011BF RID: 4543 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _Type.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060011C0 RID: 4544 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _Type.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060011C1 RID: 4545 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _Type.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x000479FF File Offset: 0x00045BFF
		internal virtual Type InternalResolve()
		{
			return this.UnderlyingSystemType;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x000479F8 File Offset: 0x00045BF8
		internal virtual Type RuntimeResolve()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x000040F7 File Offset: 0x000022F7
		internal virtual bool IsUserType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00047A07 File Offset: 0x00045C07
		internal virtual MethodInfo GetMethod(MethodInfo fromNoninstanciated)
		{
			throw new InvalidOperationException("can only be called in generic type");
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00047A07 File Offset: 0x00045C07
		internal virtual ConstructorInfo GetConstructor(ConstructorInfo fromNoninstanciated)
		{
			throw new InvalidOperationException("can only be called in generic type");
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00047A07 File Offset: 0x00045C07
		internal virtual FieldInfo GetField(FieldInfo fromNoninstanciated)
		{
			throw new InvalidOperationException("can only be called in generic type");
		}

		/// <summary>Gets the type referenced by the specified type handle.</summary>
		/// <param name="handle">The object that refers to the type.</param>
		/// <returns>The type referenced by the specified <see cref="T:System.RuntimeTypeHandle" />, or <see langword="null" /> if the <see cref="P:System.RuntimeTypeHandle.Value" /> property of <paramref name="handle" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
		// Token: 0x060011C8 RID: 4552 RVA: 0x00047A13 File Offset: 0x00045C13
		public static Type GetTypeFromHandle(RuntimeTypeHandle handle)
		{
			if (handle.Value == IntPtr.Zero)
			{
				return null;
			}
			return Type.internal_from_handle(handle.Value);
		}

		// Token: 0x060011C9 RID: 4553
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type internal_from_handle(IntPtr handle);

		// Token: 0x060011CA RID: 4554 RVA: 0x00047A36 File Offset: 0x00045C36
		internal virtual RuntimeTypeHandle GetTypeHandleInternal()
		{
			return this.TypeHandle;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x000479F8 File Offset: 0x00045BF8
		internal virtual bool IsWindowsRuntimeObjectImpl()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x000479F8 File Offset: 0x00045BF8
		internal virtual bool IsExportedToWindowsRuntimeImpl()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060011CD RID: 4557 RVA: 0x00047A3E File Offset: 0x00045C3E
		internal bool IsWindowsRuntimeObject
		{
			get
			{
				return this.IsWindowsRuntimeObjectImpl();
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x00047A46 File Offset: 0x00045C46
		internal bool IsExportedToWindowsRuntime
		{
			get
			{
				return this.IsExportedToWindowsRuntimeImpl();
			}
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		internal virtual bool HasProxyAttributeImpl()
		{
			return false;
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		internal virtual bool IsSzArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00047A4E File Offset: 0x00045C4E
		internal string FormatTypeName()
		{
			return this.FormatTypeName(false);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x000479F8 File Offset: 0x00045BF8
		internal virtual string FormatTypeName(bool serialization)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is an interface; that is, not a class or a value type.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is an interface; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x00047A58 File Offset: 0x00045C58
		public bool IsInterface
		{
			[SecuritySafeCritical]
			get
			{
				RuntimeType runtimeType = this as RuntimeType;
				if (runtimeType != null)
				{
					return RuntimeTypeHandle.IsInterface(runtimeType);
				}
				return (this.GetAttributeFlagsImpl() & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> with the specified name, specifying whether to throw an exception if the type is not found and whether to perform a case-sensitive search.</summary>
		/// <param name="typeName">The assembly-qualified name of the type to get. See <see cref="P:System.Type.AssemblyQualifiedName" />. If the type is in the currently executing assembly or in Mscorlib.dll, it is sufficient to supply the type name qualified by its namespace.</param>
		/// <param name="throwOnError">
		///   <see langword="true" /> to throw an exception if the type cannot be found; <see langword="false" /> to return <see langword="null" />. Specifying <see langword="false" /> also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to perform a case-insensitive search for <paramref name="typeName" />, <see langword="false" /> to perform a case-sensitive search for <paramref name="typeName" />.</param>
		/// <returns>The type with the specified name. If the type is not found, the <paramref name="throwOnError" /> parameter specifies whether <see langword="null" /> is returned or an exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwOnError" />. See the Exceptions section.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is <see langword="true" /> and the type is not found.  
		/// -or-  
		/// <paramref name="throwOnError" /> is <see langword="true" /> and <paramref name="typeName" /> contains invalid characters, such as an embedded tab.  
		/// -or-  
		/// <paramref name="throwOnError" /> is <see langword="true" /> and <paramref name="typeName" /> is an empty string.  
		/// -or-  
		/// <paramref name="throwOnError" /> is <see langword="true" /> and <paramref name="typeName" /> represents an array type with an invalid size.  
		/// -or-  
		/// <paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="throwOnError" /> is <see langword="true" /> and <paramref name="typeName" /> contains invalid syntax. For example, "MyType[,*,]".  
		/// -or-  
		/// <paramref name="typeName" /> represents a generic type that has a pointer type, a <see langword="ByRef" /> type, or <see cref="T:System.Void" /> as one of its type arguments.  
		/// -or-  
		/// <paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.  
		/// -or-  
		/// <paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="throwOnError" /> is <see langword="true" /> and the assembly or one of its dependencies was not found.</exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly or one of its dependencies was found, but could not be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid.  
		///  -or-  
		///  Version 2.0 or later of the common language runtime is currently loaded, and the assembly was compiled with a later version.</exception>
		// Token: 0x060011D4 RID: 4564 RVA: 0x00047A8C File Offset: 0x00045C8C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Type GetType(string typeName, bool throwOnError, bool ignoreCase)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeType.GetType(typeName, throwOnError, ignoreCase, false, ref stackCrawlMark);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> with the specified name, performing a case-sensitive search and specifying whether to throw an exception if the type is not found.</summary>
		/// <param name="typeName">The assembly-qualified name of the type to get. See <see cref="P:System.Type.AssemblyQualifiedName" />. If the type is in the currently executing assembly or in Mscorlib.dll, it is sufficient to supply the type name qualified by its namespace.</param>
		/// <param name="throwOnError">
		///   <see langword="true" /> to throw an exception if the type cannot be found; <see langword="false" /> to return <see langword="null" />. Specifying <see langword="false" /> also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
		/// <returns>The type with the specified name. If the type is not found, the <paramref name="throwOnError" /> parameter specifies whether <see langword="null" /> is returned or an exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwOnError" />. See the Exceptions section.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is <see langword="true" /> and the type is not found.  
		/// -or-  
		/// <paramref name="throwOnError" /> is <see langword="true" /> and <paramref name="typeName" /> contains invalid characters, such as an embedded tab.  
		/// -or-  
		/// <paramref name="throwOnError" /> is <see langword="true" /> and <paramref name="typeName" /> is an empty string.  
		/// -or-  
		/// <paramref name="throwOnError" /> is <see langword="true" /> and <paramref name="typeName" /> represents an array type with an invalid size.  
		/// -or-  
		/// <paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="throwOnError" /> is <see langword="true" /> and <paramref name="typeName" /> contains invalid syntax. For example, "MyType[,*,]".  
		/// -or-  
		/// <paramref name="typeName" /> represents a generic type that has a pointer type, a <see langword="ByRef" /> type, or <see cref="T:System.Void" /> as one of its type arguments.  
		/// -or-  
		/// <paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.  
		/// -or-  
		/// <paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="throwOnError" /> is <see langword="true" /> and the assembly or one of its dependencies was not found.</exception>
		/// <exception cref="T:System.IO.FileLoadException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.  
		///
		///
		///
		///
		///  The assembly or one of its dependencies was found, but could not be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid.  
		///  -or-  
		///  Version 2.0 or later of the common language runtime is currently loaded, and the assembly was compiled with a later version.</exception>
		// Token: 0x060011D5 RID: 4565 RVA: 0x00047AA8 File Offset: 0x00045CA8
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Type GetType(string typeName, bool throwOnError)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeType.GetType(typeName, throwOnError, false, false, ref stackCrawlMark);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> with the specified name, performing a case-sensitive search.</summary>
		/// <param name="typeName">The assembly-qualified name of the type to get. See <see cref="P:System.Type.AssemblyQualifiedName" />. If the type is in the currently executing assembly or in Mscorlib.dll, it is sufficient to supply the type name qualified by its namespace.</param>
		/// <returns>The type with the specified name, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeName" /> represents a generic type that has a pointer type, a <see langword="ByRef" /> type, or <see cref="T:System.Void" /> as one of its type arguments.  
		/// -or-  
		/// <paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.  
		/// -or-  
		/// <paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" />.</exception>
		/// <exception cref="T:System.IO.FileLoadException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.  
		///
		///
		///
		///
		///  The assembly or one of its dependencies was found, but could not be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid.  
		///  -or-  
		///  Version 2.0 or later of the common language runtime is currently loaded, and the assembly was compiled with a later version.</exception>
		// Token: 0x060011D6 RID: 4566 RVA: 0x00047AC4 File Offset: 0x00045CC4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Type GetType(string typeName)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeType.GetType(typeName, false, false, false, ref stackCrawlMark);
		}

		/// <summary>Gets the type with the specified name, optionally providing custom methods to resolve the assembly and the type.</summary>
		/// <param name="typeName">The name of the type to get. If the <paramref name="typeResolver" /> parameter is provided, the type name can be any string that <paramref name="typeResolver" /> is capable of resolving. If the <paramref name="assemblyResolver" /> parameter is provided or if standard type resolution is used, <paramref name="typeName" /> must be an assembly-qualified name (see <see cref="P:System.Type.AssemblyQualifiedName" />), unless the type is in the currently executing assembly or in Mscorlib.dll, in which case it is sufficient to supply the type name qualified by its namespace.</param>
		/// <param name="assemblyResolver">A method that locates and returns the assembly that is specified in <paramref name="typeName" />. The assembly name is passed to <paramref name="assemblyResolver" /> as an <see cref="T:System.Reflection.AssemblyName" /> object. If <paramref name="typeName" /> does not contain the name of an assembly, <paramref name="assemblyResolver" /> is not called. If <paramref name="assemblyResolver" /> is not supplied, standard assembly resolution is performed.  
		///  Caution   Do not pass methods from unknown or untrusted callers. Doing so could result in elevation of privilege for malicious code. Use only methods that you provide or that you are familiar with.</param>
		/// <param name="typeResolver">A method that locates and returns the type that is specified by <paramref name="typeName" /> from the assembly that is returned by <paramref name="assemblyResolver" /> or by standard assembly resolution. If no assembly is provided, the <paramref name="typeResolver" /> method can provide one. The method also takes a parameter that specifies whether to perform a case-insensitive search; <see langword="false" /> is passed to that parameter.  
		///  Caution   Do not pass methods from unknown or untrusted callers.</param>
		/// <returns>The type with the specified name, or <see langword="null" /> if the type is not found.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
		/// <exception cref="T:System.ArgumentException">An error occurs when <paramref name="typeName" /> is parsed into a type name and an assembly name (for example, when the simple type name includes an unescaped special character).  
		///  -or-  
		///  <paramref name="typeName" /> represents a generic type that has a pointer type, a <see langword="ByRef" /> type, or <see cref="T:System.Void" /> as one of its type arguments.  
		///  -or-  
		///  <paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.  
		///  -or-  
		///  <paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" />.</exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly or one of its dependencies was found, but could not be loaded.  
		///  -or-  
		///  <paramref name="typeName" /> contains an invalid assembly name.  
		///  -or-  
		///  <paramref name="typeName" /> is a valid assembly name without a type name.</exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid.  
		///  -or-  
		///  The assembly was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		// Token: 0x060011D7 RID: 4567 RVA: 0x00047AE0 File Offset: 0x00045CE0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TypeNameParser.GetType(typeName, assemblyResolver, typeResolver, false, false, ref stackCrawlMark);
		}

		/// <summary>Gets the type with the specified name, specifying whether to throw an exception if the type is not found, and optionally providing custom methods to resolve the assembly and the type.</summary>
		/// <param name="typeName">The name of the type to get. If the <paramref name="typeResolver" /> parameter is provided, the type name can be any string that <paramref name="typeResolver" /> is capable of resolving. If the <paramref name="assemblyResolver" /> parameter is provided or if standard type resolution is used, <paramref name="typeName" /> must be an assembly-qualified name (see <see cref="P:System.Type.AssemblyQualifiedName" />), unless the type is in the currently executing assembly or in Mscorlib.dll, in which case it is sufficient to supply the type name qualified by its namespace.</param>
		/// <param name="assemblyResolver">A method that locates and returns the assembly that is specified in <paramref name="typeName" />. The assembly name is passed to <paramref name="assemblyResolver" /> as an <see cref="T:System.Reflection.AssemblyName" /> object. If <paramref name="typeName" /> does not contain the name of an assembly, <paramref name="assemblyResolver" /> is not called. If <paramref name="assemblyResolver" /> is not supplied, standard assembly resolution is performed.  
		///  Caution   Do not pass methods from unknown or untrusted callers. Doing so could result in elevation of privilege for malicious code. Use only methods that you provide or that you are familiar with.</param>
		/// <param name="typeResolver">A method that locates and returns the type that is specified by <paramref name="typeName" /> from the assembly that is returned by <paramref name="assemblyResolver" /> or by standard assembly resolution. If no assembly is provided, the method can provide one. The method also takes a parameter that specifies whether to perform a case-insensitive search; <see langword="false" /> is passed to that parameter.  
		///  Caution   Do not pass methods from unknown or untrusted callers.</param>
		/// <param name="throwOnError">
		///   <see langword="true" /> to throw an exception if the type cannot be found; <see langword="false" /> to return <see langword="null" />. Specifying <see langword="false" /> also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
		/// <returns>The type with the specified name. If the type is not found, the <paramref name="throwOnError" /> parameter specifies whether <see langword="null" /> is returned or an exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwOnError" />. See the Exceptions section.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is <see langword="true" /> and the type is not found.  
		/// -or-  
		/// <paramref name="throwOnError" /> is <see langword="true" /> and <paramref name="typeName" /> contains invalid characters, such as an embedded tab.  
		/// -or-  
		/// <paramref name="throwOnError" /> is <see langword="true" /> and <paramref name="typeName" /> is an empty string.  
		/// -or-  
		/// <paramref name="throwOnError" /> is <see langword="true" /> and <paramref name="typeName" /> represents an array type with an invalid size.  
		/// -or-  
		/// <paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" />.</exception>
		/// <exception cref="T:System.ArgumentException">An error occurs when <paramref name="typeName" /> is parsed into a type name and an assembly name (for example, when the simple type name includes an unescaped special character).  
		///  -or-  
		///  <paramref name="throwOnError" /> is <see langword="true" /> and <paramref name="typeName" /> contains invalid syntax (for example, "MyType[,*,]").  
		///  -or-  
		///  <paramref name="typeName" /> represents a generic type that has a pointer type, a <see langword="ByRef" /> type, or <see cref="T:System.Void" /> as one of its type arguments.  
		///  -or-  
		///  <paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.  
		///  -or-  
		///  <paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="throwOnError" /> is <see langword="true" /> and the assembly or one of its dependencies was not found.  
		/// -or-  
		/// <paramref name="typeName" /> contains an invalid assembly name.  
		/// -or-  
		/// <paramref name="typeName" /> is a valid assembly name without a type name.</exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly or one of its dependencies was found, but could not be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid.  
		///  -or-  
		///  The assembly was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		// Token: 0x060011D8 RID: 4568 RVA: 0x00047AFC File Offset: 0x00045CFC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TypeNameParser.GetType(typeName, assemblyResolver, typeResolver, throwOnError, false, ref stackCrawlMark);
		}

		/// <summary>Gets the type with the specified name, specifying whether to perform a case-sensitive search and whether to throw an exception if the type is not found, and optionally providing custom methods to resolve the assembly and the type.</summary>
		/// <param name="typeName">The name of the type to get. If the <paramref name="typeResolver" /> parameter is provided, the type name can be any string that <paramref name="typeResolver" /> is capable of resolving. If the <paramref name="assemblyResolver" /> parameter is provided or if standard type resolution is used, <paramref name="typeName" /> must be an assembly-qualified name (see <see cref="P:System.Type.AssemblyQualifiedName" />), unless the type is in the currently executing assembly or in Mscorlib.dll, in which case it is sufficient to supply the type name qualified by its namespace.</param>
		/// <param name="assemblyResolver">A method that locates and returns the assembly that is specified in <paramref name="typeName" />. The assembly name is passed to <paramref name="assemblyResolver" /> as an <see cref="T:System.Reflection.AssemblyName" /> object. If <paramref name="typeName" /> does not contain the name of an assembly, <paramref name="assemblyResolver" /> is not called. If <paramref name="assemblyResolver" /> is not supplied, standard assembly resolution is performed.  
		///  Caution   Do not pass methods from unknown or untrusted callers. Doing so could result in elevation of privilege for malicious code. Use only methods that you provide or that you are familiar with.</param>
		/// <param name="typeResolver">A method that locates and returns the type that is specified by <paramref name="typeName" /> from the assembly that is returned by <paramref name="assemblyResolver" /> or by standard assembly resolution. If no assembly is provided, the method can provide one. The method also takes a parameter that specifies whether to perform a case-insensitive search; the value of <paramref name="ignoreCase" /> is passed to that parameter.  
		///  Caution   Do not pass methods from unknown or untrusted callers.</param>
		/// <param name="throwOnError">
		///   <see langword="true" /> to throw an exception if the type cannot be found; <see langword="false" /> to return <see langword="null" />. Specifying <see langword="false" /> also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to perform a case-insensitive search for <paramref name="typeName" />, <see langword="false" /> to perform a case-sensitive search for <paramref name="typeName" />.</param>
		/// <returns>The type with the specified name. If the type is not found, the <paramref name="throwOnError" /> parameter specifies whether <see langword="null" /> is returned or an exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwOnError" />. See the Exceptions section.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is <see langword="true" /> and the type is not found.  
		/// -or-  
		/// <paramref name="throwOnError" /> is <see langword="true" /> and <paramref name="typeName" /> contains invalid characters, such as an embedded tab.  
		/// -or-  
		/// <paramref name="throwOnError" /> is <see langword="true" /> and <paramref name="typeName" /> is an empty string.  
		/// -or-  
		/// <paramref name="throwOnError" /> is <see langword="true" /> and <paramref name="typeName" /> represents an array type with an invalid size.  
		/// -or-  
		/// <paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" />.</exception>
		/// <exception cref="T:System.ArgumentException">An error occurs when <paramref name="typeName" /> is parsed into a type name and an assembly name (for example, when the simple type name includes an unescaped special character).  
		///  -or-  
		///  <paramref name="throwOnError" /> is <see langword="true" /> and <paramref name="typeName" /> contains invalid syntax (for example, "MyType[,*,]").  
		///  -or-  
		///  <paramref name="typeName" /> represents a generic type that has a pointer type, a <see langword="ByRef" /> type, or <see cref="T:System.Void" /> as one of its type arguments.  
		///  -or-  
		///  <paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.  
		///  -or-  
		///  <paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="throwOnError" /> is <see langword="true" /> and the assembly or one of its dependencies was not found.</exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly or one of its dependencies was found, but could not be loaded.  
		///  -or-  
		///  <paramref name="typeName" /> contains an invalid assembly name.  
		///  -or-  
		///  <paramref name="typeName" /> is a valid assembly name without a type name.</exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid.  
		///  -or-  
		///  The assembly was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		// Token: 0x060011D9 RID: 4569 RVA: 0x00047B18 File Offset: 0x00045D18
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TypeNameParser.GetType(typeName, assemblyResolver, typeResolver, throwOnError, ignoreCase, ref stackCrawlMark);
		}

		/// <summary>Indicates whether two <see cref="T:System.Type" /> objects are equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060011DA RID: 4570 RVA: 0x00028426 File Offset: 0x00026626
		public static bool operator ==(Type left, Type right)
		{
			return left == right;
		}

		/// <summary>Indicates whether two <see cref="T:System.Type" /> objects are not equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060011DB RID: 4571 RVA: 0x0002842C File Offset: 0x0002662C
		public static bool operator !=(Type left, Type right)
		{
			return left != right;
		}

		/// <summary>Gets the <see cref="T:System.Type" /> with the specified name, specifying whether to perform a case-sensitive search and whether to throw an exception if the type is not found. The type is loaded for reflection only, not for execution.</summary>
		/// <param name="typeName">The assembly-qualified name of the <see cref="T:System.Type" /> to get.</param>
		/// <param name="throwIfNotFound">
		///   <see langword="true" /> to throw a <see cref="T:System.TypeLoadException" /> if the type cannot be found; <see langword="false" /> to return <see langword="null" /> if the type cannot be found. Specifying <see langword="false" /> also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to perform a case-insensitive search for <paramref name="typeName" />; <see langword="false" /> to perform a case-sensitive search for <paramref name="typeName" />.</param>
		/// <returns>The type with the specified name, if found; otherwise, <see langword="null" />. If the type is not found, the <paramref name="throwIfNotFound" /> parameter specifies whether <see langword="null" /> is returned or an exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwIfNotFound" />. See the Exceptions section.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwIfNotFound" /> is <see langword="true" /> and the type is not found.  
		/// -or-  
		/// <paramref name="throwIfNotFound" /> is <see langword="true" /> and <paramref name="typeName" /> contains invalid characters, such as an embedded tab.  
		/// -or-  
		/// <paramref name="throwIfNotFound" /> is <see langword="true" /> and <paramref name="typeName" /> is an empty string.  
		/// -or-  
		/// <paramref name="throwIfNotFound" /> is <see langword="true" /> and <paramref name="typeName" /> represents an array type with an invalid size.  
		/// -or-  
		/// <paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" /> objects.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeName" /> does not include the assembly name.  
		/// -or-  
		/// <paramref name="throwIfNotFound" /> is <see langword="true" /> and <paramref name="typeName" /> contains invalid syntax; for example, "MyType[,*,]".  
		/// -or-  
		/// <paramref name="typeName" /> represents a generic type that has a pointer type, a <see langword="ByRef" /> type, or <see cref="T:System.Void" /> as one of its type arguments.  
		/// -or-  
		/// <paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.  
		/// -or-  
		/// <paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="throwIfNotFound" /> is <see langword="true" /> and the assembly or one of its dependencies was not found.</exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly or one of its dependencies was found, but could not be loaded.</exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid.  
		///  -or-  
		///  The assembly was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		// Token: 0x060011DC RID: 4572 RVA: 0x00047B34 File Offset: 0x00045D34
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Type ReflectionOnlyGetType(string typeName, bool throwIfNotFound, bool ignoreCase)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeType.GetType(typeName, throwIfNotFound, ignoreCase, true, ref stackCrawlMark);
		}

		/// <summary>Gets the type associated with the specified class identifier (CLSID) from the specified server, specifying whether to throw an exception if an error occurs while loading the type.</summary>
		/// <param name="clsid">The CLSID of the type to get.</param>
		/// <param name="server">The server from which to load the type. If the server name is <see langword="null" />, this method automatically reverts to the local machine.</param>
		/// <param name="throwOnError">
		///   <see langword="true" /> to throw any exception that occurs.  
		/// -or-  
		/// <see langword="false" /> to ignore any exception that occurs.</param>
		/// <returns>
		///   <see langword="System.__ComObject" /> regardless of whether the CLSID is valid.</returns>
		// Token: 0x060011DD RID: 4573 RVA: 0x00047B4E File Offset: 0x00045D4E
		public static Type GetTypeFromCLSID(Guid clsid, string server, bool throwOnError)
		{
			return RuntimeType.GetTypeFromCLSIDImpl(clsid, server, throwOnError);
		}

		/// <summary>Gets the type associated with the specified program identifier (progID) from the specified server, specifying whether to throw an exception if an error occurs while loading the type.</summary>
		/// <param name="progID">The progID of the <see cref="T:System.Type" /> to get.</param>
		/// <param name="server">The server from which to load the type. If the server name is <see langword="null" />, this method automatically reverts to the local machine.</param>
		/// <param name="throwOnError">
		///   <see langword="true" /> to throw any exception that occurs.  
		/// -or-  
		/// <see langword="false" /> to ignore any exception that occurs.</param>
		/// <returns>The type associated with the specified program identifier (progID), if <paramref name="progID" /> is a valid entry in the registry and a type is associated with it; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="progID" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The specified progID is not registered.</exception>
		// Token: 0x060011DE RID: 4574 RVA: 0x00047B58 File Offset: 0x00045D58
		public static Type GetTypeFromProgID(string progID, string server, bool throwOnError)
		{
			return RuntimeType.GetTypeFromProgIDImpl(progID, server, throwOnError);
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x00047B64 File Offset: 0x00045D64
		internal string FullNameOrDefault
		{
			get
			{
				if (this.InternalNameIfAvailable == null)
				{
					return "UnknownType";
				}
				string result;
				try
				{
					result = this.FullName;
				}
				catch (MissingMetadataException)
				{
					result = "UnknownType";
				}
				return result;
			}
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00047BA4 File Offset: 0x00045DA4
		internal bool IsRuntimeImplemented()
		{
			return this.UnderlyingSystemType is RuntimeType;
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x00047BB4 File Offset: 0x00045DB4
		internal virtual string InternalGetNameIfAvailable(ref Type rootCauseForFailure)
		{
			return this.Name;
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x00047BBC File Offset: 0x00045DBC
		internal string InternalNameIfAvailable
		{
			get
			{
				Type type = null;
				return this.InternalGetNameIfAvailable(ref type);
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x00047BD3 File Offset: 0x00045DD3
		internal string NameOrDefault
		{
			get
			{
				return this.InternalNameIfAvailable ?? "UnknownType";
			}
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00047BE4 File Offset: 0x00045DE4
		// Note: this type is marked as 'beforefieldinit'.
		static Type()
		{
		}

		// Token: 0x0400133D RID: 4925
		private static volatile Binder s_defaultBinder;

		/// <summary>Separates names in the namespace of the <see cref="T:System.Type" />. This field is read-only.</summary>
		// Token: 0x0400133E RID: 4926
		public static readonly char Delimiter = '.';

		/// <summary>Represents an empty array of type <see cref="T:System.Type" />. This field is read-only.</summary>
		// Token: 0x0400133F RID: 4927
		public static readonly Type[] EmptyTypes = Array.Empty<Type>();

		/// <summary>Represents a missing value in the <see cref="T:System.Type" /> information. This field is read-only.</summary>
		// Token: 0x04001340 RID: 4928
		public static readonly object Missing = System.Reflection.Missing.Value;

		/// <summary>Represents the member filter used on attributes. This field is read-only.</summary>
		// Token: 0x04001341 RID: 4929
		public static readonly MemberFilter FilterAttribute = new MemberFilter(Type.FilterAttributeImpl);

		/// <summary>Represents the case-sensitive member filter used on names. This field is read-only.</summary>
		// Token: 0x04001342 RID: 4930
		public static readonly MemberFilter FilterName = new MemberFilter(Type.FilterNameImpl);

		/// <summary>Represents the case-insensitive member filter used on names. This field is read-only.</summary>
		// Token: 0x04001343 RID: 4931
		public static readonly MemberFilter FilterNameIgnoreCase = new MemberFilter(Type.FilterNameIgnoreCaseImpl);

		// Token: 0x04001344 RID: 4932
		private const BindingFlags DefaultLookup = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

		// Token: 0x04001345 RID: 4933
		internal RuntimeTypeHandle _impl;

		// Token: 0x04001346 RID: 4934
		internal const string DefaultTypeNameWhenMissingMetadata = "UnknownType";
	}
}
