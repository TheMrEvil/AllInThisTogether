using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a field and provides access to field metadata.</summary>
	// Token: 0x0200089F RID: 2207
	[Serializable]
	public abstract class FieldInfo : MemberInfo, _FieldInfo
	{
		/// <summary>Initializes a new instance of the <see langword="FieldInfo" /> class.</summary>
		// Token: 0x060048C7 RID: 18631 RVA: 0x00047204 File Offset: 0x00045404
		protected FieldInfo()
		{
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a field.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a field.</returns>
		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x060048C8 RID: 18632 RVA: 0x00022807 File Offset: 0x00020A07
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Field;
			}
		}

		/// <summary>Gets the attributes associated with this field.</summary>
		/// <returns>The <see langword="FieldAttributes" /> for this field.</returns>
		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x060048C9 RID: 18633
		public abstract FieldAttributes Attributes { get; }

		/// <summary>Gets the type of this field object.</summary>
		/// <returns>The type of this field object.</returns>
		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x060048CA RID: 18634
		public abstract Type FieldType { get; }

		/// <summary>Gets a value indicating whether the field can only be set in the body of the constructor.</summary>
		/// <returns>
		///   <see langword="true" /> if the field has the <see langword="InitOnly" /> attribute set; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x060048CB RID: 18635 RVA: 0x000EE58A File Offset: 0x000EC78A
		public bool IsInitOnly
		{
			get
			{
				return (this.Attributes & FieldAttributes.InitOnly) > FieldAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the value is written at compile time and cannot be changed.</summary>
		/// <returns>
		///   <see langword="true" /> if the field has the <see langword="Literal" /> attribute set; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x060048CC RID: 18636 RVA: 0x000EE598 File Offset: 0x000EC798
		public bool IsLiteral
		{
			get
			{
				return (this.Attributes & FieldAttributes.Literal) > FieldAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether this field has the <see langword="NotSerialized" /> attribute.</summary>
		/// <returns>
		///   <see langword="true" /> if the field has the <see langword="NotSerialized" /> attribute set; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x060048CD RID: 18637 RVA: 0x000EE5A6 File Offset: 0x000EC7A6
		public bool IsNotSerialized
		{
			get
			{
				return (this.Attributes & FieldAttributes.NotSerialized) > FieldAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the corresponding <see langword="PinvokeImpl" /> attribute is set in <see cref="T:System.Reflection.FieldAttributes" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="PinvokeImpl" /> attribute is set in <see cref="T:System.Reflection.FieldAttributes" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x060048CE RID: 18638 RVA: 0x000EE5B7 File Offset: 0x000EC7B7
		public bool IsPinvokeImpl
		{
			get
			{
				return (this.Attributes & FieldAttributes.PinvokeImpl) > FieldAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the corresponding <see langword="SpecialName" /> attribute is set in the <see cref="T:System.Reflection.FieldAttributes" /> enumerator.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="SpecialName" /> attribute is set in <see cref="T:System.Reflection.FieldAttributes" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x060048CF RID: 18639 RVA: 0x000EE5C8 File Offset: 0x000EC7C8
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & FieldAttributes.SpecialName) > FieldAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the field is static.</summary>
		/// <returns>
		///   <see langword="true" /> if this field is static; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x060048D0 RID: 18640 RVA: 0x000EE5D9 File Offset: 0x000EC7D9
		public bool IsStatic
		{
			get
			{
				return (this.Attributes & FieldAttributes.Static) > FieldAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the potential visibility of this field is described by <see cref="F:System.Reflection.FieldAttributes.Assembly" />; that is, the field is visible at most to other types in the same assembly, and is not visible to derived types outside the assembly.</summary>
		/// <returns>
		///   <see langword="true" /> if the visibility of this field is exactly described by <see cref="F:System.Reflection.FieldAttributes.Assembly" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x060048D1 RID: 18641 RVA: 0x000EE5E7 File Offset: 0x000EC7E7
		public bool IsAssembly
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Assembly;
			}
		}

		/// <summary>Gets a value indicating whether the visibility of this field is described by <see cref="F:System.Reflection.FieldAttributes.Family" />; that is, the field is visible only within its class and derived classes.</summary>
		/// <returns>
		///   <see langword="true" /> if access to this field is exactly described by <see cref="F:System.Reflection.FieldAttributes.Family" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x060048D2 RID: 18642 RVA: 0x000EE5F4 File Offset: 0x000EC7F4
		public bool IsFamily
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Family;
			}
		}

		/// <summary>Gets a value indicating whether the visibility of this field is described by <see cref="F:System.Reflection.FieldAttributes.FamANDAssem" />; that is, the field can be accessed from derived classes, but only if they are in the same assembly.</summary>
		/// <returns>
		///   <see langword="true" /> if access to this field is exactly described by <see cref="F:System.Reflection.FieldAttributes.FamANDAssem" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x060048D3 RID: 18643 RVA: 0x000EE601 File Offset: 0x000EC801
		public bool IsFamilyAndAssembly
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamANDAssem;
			}
		}

		/// <summary>Gets a value indicating whether the potential visibility of this field is described by <see cref="F:System.Reflection.FieldAttributes.FamORAssem" />; that is, the field can be accessed by derived classes wherever they are, and by classes in the same assembly.</summary>
		/// <returns>
		///   <see langword="true" /> if access to this field is exactly described by <see cref="F:System.Reflection.FieldAttributes.FamORAssem" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x060048D4 RID: 18644 RVA: 0x000EE60E File Offset: 0x000EC80E
		public bool IsFamilyOrAssembly
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamORAssem;
			}
		}

		/// <summary>Gets a value indicating whether the field is private.</summary>
		/// <returns>
		///   <see langword="true" /> if the field is private; otherwise; <see langword="false" />.</returns>
		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x060048D5 RID: 18645 RVA: 0x000EE61B File Offset: 0x000EC81B
		public bool IsPrivate
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Private;
			}
		}

		/// <summary>Gets a value indicating whether the field is public.</summary>
		/// <returns>
		///   <see langword="true" /> if this field is public; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x060048D6 RID: 18646 RVA: 0x000EE628 File Offset: 0x000EC828
		public bool IsPublic
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public;
			}
		}

		/// <summary>Gets a value that indicates whether the current field is security-critical or security-safe-critical at the current trust level.</summary>
		/// <returns>
		///   <see langword="true" /> if the current field is security-critical or security-safe-critical at the current trust level; <see langword="false" /> if it is transparent.</returns>
		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x060048D7 RID: 18647 RVA: 0x000040F7 File Offset: 0x000022F7
		public virtual bool IsSecurityCritical
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value that indicates whether the current field is security-safe-critical at the current trust level.</summary>
		/// <returns>
		///   <see langword="true" /> if the current field is security-safe-critical at the current trust level; <see langword="false" /> if it is security-critical or transparent.</returns>
		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x060048D8 RID: 18648 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsSecuritySafeCritical
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the current field is transparent at the current trust level.</summary>
		/// <returns>
		///   <see langword="true" /> if the field is security-transparent at the current trust level; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x060048D9 RID: 18649 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsSecurityTransparent
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a <see langword="RuntimeFieldHandle" />, which is a handle to the internal metadata representation of a field.</summary>
		/// <returns>A handle to the internal metadata representation of a field.</returns>
		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x060048DA RID: 18650
		public abstract RuntimeFieldHandle FieldHandle { get; }

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060048DB RID: 18651 RVA: 0x000EE36E File Offset: 0x000EC56E
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060048DC RID: 18652 RVA: 0x000EE377 File Offset: 0x000EC577
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.FieldInfo" /> objects are equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060048DD RID: 18653 RVA: 0x00064560 File Offset: 0x00062760
		public static bool operator ==(FieldInfo left, FieldInfo right)
		{
			return left == right || (left != null && right != null && left.Equals(right));
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.FieldInfo" /> objects are not equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060048DE RID: 18654 RVA: 0x000EE635 File Offset: 0x000EC835
		public static bool operator !=(FieldInfo left, FieldInfo right)
		{
			return !(left == right);
		}

		/// <summary>When overridden in a derived class, returns the value of a field supported by a given object.</summary>
		/// <param name="obj">The object whose field value will be returned.</param>
		/// <returns>An object containing the value of the field reflected by this instance.</returns>
		/// <exception cref="T:System.Reflection.TargetException">In the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.  
		///
		///
		///
		///
		///  The field is non-static and <paramref name="obj" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">A field is marked literal, but the field does not have one of the accepted literal types.</exception>
		/// <exception cref="T:System.FieldAccessException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.  
		///
		///
		///
		///
		///  The caller does not have permission to access this field.</exception>
		/// <exception cref="T:System.ArgumentException">The method is neither declared nor inherited by the class of <paramref name="obj" />.</exception>
		// Token: 0x060048DF RID: 18655
		public abstract object GetValue(object obj);

		/// <summary>Sets the value of the field supported by the given object.</summary>
		/// <param name="obj">The object whose field value will be set.</param>
		/// <param name="value">The value to assign to the field.</param>
		/// <exception cref="T:System.FieldAccessException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.  
		///
		///
		///
		///
		///  The caller does not have permission to access this field.</exception>
		/// <exception cref="T:System.Reflection.TargetException">In the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.  
		///
		///
		///
		///
		///  The <paramref name="obj" /> parameter is <see langword="null" /> and the field is an instance field.</exception>
		/// <exception cref="T:System.ArgumentException">The field does not exist on the object.  
		///  -or-  
		///  The <paramref name="value" /> parameter cannot be converted and stored in the field.</exception>
		// Token: 0x060048E0 RID: 18656 RVA: 0x000EE641 File Offset: 0x000EC841
		[DebuggerHidden]
		[DebuggerStepThrough]
		public void SetValue(object obj, object value)
		{
			this.SetValue(obj, value, BindingFlags.Default, Type.DefaultBinder, null);
		}

		/// <summary>When overridden in a derived class, sets the value of the field supported by the given object.</summary>
		/// <param name="obj">The object whose field value will be set.</param>
		/// <param name="value">The value to assign to the field.</param>
		/// <param name="invokeAttr">A field of <see langword="Binder" /> that specifies the type of binding that is desired (for example, <see langword="Binder.CreateInstance" /> or <see langword="Binder.ExactBinding" />).</param>
		/// <param name="binder">A set of properties that enables the binding, coercion of argument types, and invocation of members through reflection. If <paramref name="binder" /> is <see langword="null" />, then <see langword="Binder.DefaultBinding" /> is used.</param>
		/// <param name="culture">The software preferences of a particular culture.</param>
		/// <exception cref="T:System.FieldAccessException">The caller does not have permission to access this field.</exception>
		/// <exception cref="T:System.Reflection.TargetException">The <paramref name="obj" /> parameter is <see langword="null" /> and the field is an instance field.</exception>
		/// <exception cref="T:System.ArgumentException">The field does not exist on the object.  
		///  -or-  
		///  The <paramref name="value" /> parameter cannot be converted and stored in the field.</exception>
		// Token: 0x060048E1 RID: 18657
		public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture);

		/// <summary>Sets the value of the field supported by the given object.</summary>
		/// <param name="obj">A <see cref="T:System.TypedReference" /> structure that encapsulates a managed pointer to a location and a runtime representation of the type that can be stored at that location.</param>
		/// <param name="value">The value to assign to the field.</param>
		/// <exception cref="T:System.NotSupportedException">The caller requires the Common Language Specification (CLS) alternative, but called this method instead.</exception>
		// Token: 0x060048E2 RID: 18658 RVA: 0x000EE652 File Offset: 0x000EC852
		[CLSCompliant(false)]
		public virtual void SetValueDirect(TypedReference obj, object value)
		{
			throw new NotSupportedException("This non-CLS method is not implemented.");
		}

		/// <summary>Returns the value of a field supported by a given object.</summary>
		/// <param name="obj">A <see cref="T:System.TypedReference" /> structure that encapsulates a managed pointer to a location and a runtime representation of the type that might be stored at that location.</param>
		/// <returns>An <see langword="Object" /> containing a field value.</returns>
		/// <exception cref="T:System.NotSupportedException">The caller requires the Common Language Specification (CLS) alternative, but called this method instead.</exception>
		// Token: 0x060048E3 RID: 18659 RVA: 0x000EE652 File Offset: 0x000EC852
		[CLSCompliant(false)]
		public virtual object GetValueDirect(TypedReference obj)
		{
			throw new NotSupportedException("This non-CLS method is not implemented.");
		}

		/// <summary>Returns a literal value associated with the field by a compiler.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the literal value associated with the field. If the literal value is a class type with an element value of zero, the return value is <see langword="null" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The Constant table in unmanaged metadata does not contain a constant value for the current field.</exception>
		/// <exception cref="T:System.FormatException">The type of the value is not one of the types permitted by the Common Language Specification (CLS). See the ECMA Partition II specification Metadata Logical Format: Other Structures, Element Types used in Signatures.</exception>
		/// <exception cref="T:System.NotSupportedException">The constant value for the field is not set.</exception>
		// Token: 0x060048E4 RID: 18660 RVA: 0x000EE652 File Offset: 0x000EC852
		public virtual object GetRawConstantValue()
		{
			throw new NotSupportedException("This non-CLS method is not implemented.");
		}

		/// <summary>Gets an array of types that identify the optional custom modifiers of the field.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that identify the optional custom modifiers of the current field, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />.</returns>
		// Token: 0x060048E5 RID: 18661 RVA: 0x00047226 File Offset: 0x00045426
		public virtual Type[] GetOptionalCustomModifiers()
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Gets an array of types that identify the required custom modifiers of the property.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that identify the required custom modifiers of the current property, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.</returns>
		// Token: 0x060048E6 RID: 18662 RVA: 0x00047226 File Offset: 0x00045426
		public virtual Type[] GetRequiredCustomModifiers()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x060048E7 RID: 18663
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern FieldInfo internal_from_handle_type(IntPtr field_handle, IntPtr type_handle);

		/// <summary>Gets a <see cref="T:System.Reflection.FieldInfo" /> for the field represented by the specified handle.</summary>
		/// <param name="handle">A <see cref="T:System.RuntimeFieldHandle" /> structure that contains the handle to the internal metadata representation of a field.</param>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object representing the field specified by <paramref name="handle" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="handle" /> is invalid.</exception>
		// Token: 0x060048E8 RID: 18664 RVA: 0x000EE65E File Offset: 0x000EC85E
		public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle)
		{
			if (handle.Value == IntPtr.Zero)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			return FieldInfo.internal_from_handle_type(handle.Value, IntPtr.Zero);
		}

		/// <summary>Gets a <see cref="T:System.Reflection.FieldInfo" /> for the field represented by the specified handle, for the specified generic type.</summary>
		/// <param name="handle">A <see cref="T:System.RuntimeFieldHandle" /> structure that contains the handle to the internal metadata representation of a field.</param>
		/// <param name="declaringType">A <see cref="T:System.RuntimeTypeHandle" /> structure that contains the handle to the generic type that defines the field.</param>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object representing the field specified by <paramref name="handle" />, in the generic type specified by <paramref name="declaringType" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="handle" /> is invalid.  
		/// -or-  
		/// <paramref name="declaringType" /> is not compatible with <paramref name="handle" />. For example, <paramref name="declaringType" /> is the runtime type handle of the generic type definition, and <paramref name="handle" /> comes from a constructed type.</exception>
		// Token: 0x060048E9 RID: 18665 RVA: 0x000EE690 File Offset: 0x000EC890
		[ComVisible(false)]
		public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle, RuntimeTypeHandle declaringType)
		{
			if (handle.Value == IntPtr.Zero)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			FieldInfo fieldInfo = FieldInfo.internal_from_handle_type(handle.Value, declaringType.Value);
			if (fieldInfo == null)
			{
				throw new ArgumentException("The field handle and the type handle are incompatible.");
			}
			return fieldInfo;
		}

		// Token: 0x060048EA RID: 18666 RVA: 0x000EE6E2 File Offset: 0x000EC8E2
		internal virtual int GetFieldOffset()
		{
			throw new SystemException("This method should not be called");
		}

		// Token: 0x060048EB RID: 18667
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MarshalAsAttribute get_marshal_info();

		// Token: 0x060048EC RID: 18668 RVA: 0x000EE6F0 File Offset: 0x000EC8F0
		internal object[] GetPseudoCustomAttributes()
		{
			int num = 0;
			if (this.IsNotSerialized)
			{
				num++;
			}
			if (this.DeclaringType.IsExplicitLayout)
			{
				num++;
			}
			MarshalAsAttribute marshal_info = this.get_marshal_info();
			if (marshal_info != null)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			num = 0;
			if (this.IsNotSerialized)
			{
				array[num++] = new NonSerializedAttribute();
			}
			if (this.DeclaringType.IsExplicitLayout)
			{
				array[num++] = new FieldOffsetAttribute(this.GetFieldOffset());
			}
			if (marshal_info != null)
			{
				array[num++] = marshal_info;
			}
			return array;
		}

		// Token: 0x060048ED RID: 18669 RVA: 0x000EE778 File Offset: 0x000EC978
		internal CustomAttributeData[] GetPseudoCustomAttributesData()
		{
			int num = 0;
			if (this.IsNotSerialized)
			{
				num++;
			}
			if (this.DeclaringType.IsExplicitLayout)
			{
				num++;
			}
			MarshalAsAttribute marshal_info = this.get_marshal_info();
			if (marshal_info != null)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			CustomAttributeData[] array = new CustomAttributeData[num];
			num = 0;
			if (this.IsNotSerialized)
			{
				array[num++] = new CustomAttributeData(typeof(NonSerializedAttribute).GetConstructor(Type.EmptyTypes));
			}
			if (this.DeclaringType.IsExplicitLayout)
			{
				CustomAttributeTypedArgument[] ctorArgs = new CustomAttributeTypedArgument[]
				{
					new CustomAttributeTypedArgument(typeof(int), this.GetFieldOffset())
				};
				array[num++] = new CustomAttributeData(typeof(FieldOffsetAttribute).GetConstructor(new Type[]
				{
					typeof(int)
				}), ctorArgs, EmptyArray<CustomAttributeNamedArgument>.Value);
			}
			if (marshal_info != null)
			{
				CustomAttributeTypedArgument[] ctorArgs2 = new CustomAttributeTypedArgument[]
				{
					new CustomAttributeTypedArgument(typeof(UnmanagedType), marshal_info.Value)
				};
				array[num++] = new CustomAttributeData(typeof(MarshalAsAttribute).GetConstructor(new Type[]
				{
					typeof(UnmanagedType)
				}), ctorArgs2, EmptyArray<CustomAttributeNamedArgument>.Value);
			}
			return array;
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060048EE RID: 18670 RVA: 0x000173AD File Offset: 0x000155AD
		void _FieldInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a <see cref="T:System.Type" /> object representing the <see cref="T:System.Reflection.FieldInfo" /> type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Reflection.FieldInfo" /> type.</returns>
		// Token: 0x060048EF RID: 18671 RVA: 0x00052955 File Offset: 0x00050B55
		Type _FieldInfo.GetType()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060048F0 RID: 18672 RVA: 0x000173AD File Offset: 0x000155AD
		void _FieldInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060048F1 RID: 18673 RVA: 0x000173AD File Offset: 0x000155AD
		void _FieldInfo.GetTypeInfoCount(out uint pcTInfo)
		{
			ThrowStub.ThrowNotSupportedException();
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
		// Token: 0x060048F2 RID: 18674 RVA: 0x000173AD File Offset: 0x000155AD
		void _FieldInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
