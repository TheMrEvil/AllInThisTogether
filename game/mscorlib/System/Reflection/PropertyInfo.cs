using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using Unity;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a property and provides access to property metadata.</summary>
	// Token: 0x020008BC RID: 2236
	[Serializable]
	public abstract class PropertyInfo : MemberInfo, _PropertyInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.PropertyInfo" /> class.</summary>
		// Token: 0x060049E5 RID: 18917 RVA: 0x00047204 File Offset: 0x00045404
		protected PropertyInfo()
		{
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a property.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a property.</returns>
		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x060049E6 RID: 18918 RVA: 0x00026EE1 File Offset: 0x000250E1
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Property;
			}
		}

		/// <summary>Gets the type of this property.</summary>
		/// <returns>The type of this property.</returns>
		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x060049E7 RID: 18919
		public abstract Type PropertyType { get; }

		/// <summary>When overridden in a derived class, returns an array of all the index parameters for the property.</summary>
		/// <returns>An array of type <see langword="ParameterInfo" /> containing the parameters for the indexes. If the property is not indexed, the array has 0 (zero) elements.</returns>
		// Token: 0x060049E8 RID: 18920
		public abstract ParameterInfo[] GetIndexParameters();

		/// <summary>Gets the attributes for this property.</summary>
		/// <returns>The attributes of this property.</returns>
		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x060049E9 RID: 18921
		public abstract PropertyAttributes Attributes { get; }

		/// <summary>Gets a value indicating whether the property is the special name.</summary>
		/// <returns>
		///   <see langword="true" /> if this property is the special name; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x060049EA RID: 18922 RVA: 0x000EF4AA File Offset: 0x000ED6AA
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & PropertyAttributes.SpecialName) > PropertyAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether the property can be read.</summary>
		/// <returns>
		///   <see langword="true" /> if this property can be read; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x060049EB RID: 18923
		public abstract bool CanRead { get; }

		/// <summary>Gets a value indicating whether the property can be written to.</summary>
		/// <returns>
		///   <see langword="true" /> if this property can be written to; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x060049EC RID: 18924
		public abstract bool CanWrite { get; }

		/// <summary>Returns an array whose elements reflect the public <see langword="get" /> and <see langword="set" /> accessors of the property reflected by the current instance.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MethodInfo" /> objects that reflect the public <see langword="get" /> and <see langword="set" /> accessors of the property reflected by the current instance, if found; otherwise, this method returns an array with zero (0) elements.</returns>
		// Token: 0x060049ED RID: 18925 RVA: 0x000EF4BB File Offset: 0x000ED6BB
		public MethodInfo[] GetAccessors()
		{
			return this.GetAccessors(false);
		}

		/// <summary>Returns an array whose elements reflect the public and, if specified, non-public <see langword="get" /> and <see langword="set" /> accessors of the property reflected by the current instance.</summary>
		/// <param name="nonPublic">Indicates whether non-public methods should be returned in the returned array. <see langword="true" /> if non-public methods are to be included; otherwise, <see langword="false" />.</param>
		/// <returns>An array whose elements reflect the <see langword="get" /> and <see langword="set" /> accessors of the property reflected by the current instance. If <paramref name="nonPublic" /> is <see langword="true" />, this array contains public and non-public <see langword="get" /> and <see langword="set" /> accessors. If <paramref name="nonPublic" /> is <see langword="false" />, this array contains only public <see langword="get" /> and <see langword="set" /> accessors. If no accessors with the specified visibility are found, this method returns an array with zero (0) elements.</returns>
		// Token: 0x060049EE RID: 18926
		public abstract MethodInfo[] GetAccessors(bool nonPublic);

		/// <summary>Gets the <see langword="get" /> accessor for this property.</summary>
		/// <returns>The <see langword="get" /> accessor for this property.</returns>
		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x060049EF RID: 18927 RVA: 0x000EF4C4 File Offset: 0x000ED6C4
		public virtual MethodInfo GetMethod
		{
			get
			{
				return this.GetGetMethod(true);
			}
		}

		/// <summary>Returns the public <see langword="get" /> accessor for this property.</summary>
		/// <returns>A <see langword="MethodInfo" /> object representing the public <see langword="get" /> accessor for this property, or <see langword="null" /> if the <see langword="get" /> accessor is non-public or does not exist.</returns>
		// Token: 0x060049F0 RID: 18928 RVA: 0x000EF4CD File Offset: 0x000ED6CD
		public MethodInfo GetGetMethod()
		{
			return this.GetGetMethod(false);
		}

		/// <summary>When overridden in a derived class, returns the public or non-public <see langword="get" /> accessor for this property.</summary>
		/// <param name="nonPublic">Indicates whether a non-public <see langword="get" /> accessor should be returned. <see langword="true" /> if a non-public accessor is to be returned; otherwise, <see langword="false" />.</param>
		/// <returns>A <see langword="MethodInfo" /> object representing the <see langword="get" /> accessor for this property, if <paramref name="nonPublic" /> is <see langword="true" />. Returns <see langword="null" /> if <paramref name="nonPublic" /> is <see langword="false" /> and the <see langword="get" /> accessor is non-public, or if <paramref name="nonPublic" /> is <see langword="true" /> but no <see langword="get" /> accessors exist.</returns>
		/// <exception cref="T:System.Security.SecurityException">The requested method is non-public and the caller does not have <see cref="T:System.Security.Permissions.ReflectionPermission" /> to reflect on this non-public method.</exception>
		// Token: 0x060049F1 RID: 18929
		public abstract MethodInfo GetGetMethod(bool nonPublic);

		/// <summary>Gets the <see langword="set" /> accessor for this property.</summary>
		/// <returns>The <see langword="set" /> accessor for this property, or <see langword="null" /> if the property is read-only.</returns>
		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x060049F2 RID: 18930 RVA: 0x000EF4D6 File Offset: 0x000ED6D6
		public virtual MethodInfo SetMethod
		{
			get
			{
				return this.GetSetMethod(true);
			}
		}

		/// <summary>Returns the public <see langword="set" /> accessor for this property.</summary>
		/// <returns>The <see langword="MethodInfo" /> object representing the <see langword="Set" /> method for this property if the <see langword="set" /> accessor is public, or <see langword="null" /> if the <see langword="set" /> accessor is not public.</returns>
		// Token: 0x060049F3 RID: 18931 RVA: 0x000EF4DF File Offset: 0x000ED6DF
		public MethodInfo GetSetMethod()
		{
			return this.GetSetMethod(false);
		}

		/// <summary>When overridden in a derived class, returns the <see langword="set" /> accessor for this property.</summary>
		/// <param name="nonPublic">Indicates whether the accessor should be returned if it is non-public. <see langword="true" /> if a non-public accessor is to be returned; otherwise, <see langword="false" />.</param>
		/// <returns>This property's <see langword="Set" /> method, or <see langword="null" />, as shown in the following table.  
		///   Value  
		///
		///   Condition  
		///
		///   The <see langword="Set" /> method for this property.  
		///
		///   The <see langword="set" /> accessor is public.  
		///
		///  -or-  
		///
		///  <paramref name="nonPublic" /> is <see langword="true" /> and the <see langword="set" /> accessor is non-public.  
		///
		///  <see langword="null" /><paramref name="nonPublic" /> is <see langword="true" />, but the property is read-only.  
		///
		///  -or-  
		///
		///  <paramref name="nonPublic" /> is <see langword="false" /> and the <see langword="set" /> accessor is non-public.  
		///
		///  -or-  
		///
		///  There is no <see langword="set" /> accessor.</returns>
		/// <exception cref="T:System.Security.SecurityException">The requested method is non-public and the caller does not have <see cref="T:System.Security.Permissions.ReflectionPermission" /> to reflect on this non-public method.</exception>
		// Token: 0x060049F4 RID: 18932
		public abstract MethodInfo GetSetMethod(bool nonPublic);

		/// <summary>Returns an array of types representing the optional custom modifiers of the property.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that identify the optional custom modifiers of the current property, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.</returns>
		// Token: 0x060049F5 RID: 18933 RVA: 0x000EF2B2 File Offset: 0x000ED4B2
		public virtual Type[] GetOptionalCustomModifiers()
		{
			return Array.Empty<Type>();
		}

		/// <summary>Returns an array of types representing the required custom modifiers of the property.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that identify the required custom modifiers of the current property, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.</returns>
		// Token: 0x060049F6 RID: 18934 RVA: 0x000EF2B2 File Offset: 0x000ED4B2
		public virtual Type[] GetRequiredCustomModifiers()
		{
			return Array.Empty<Type>();
		}

		/// <summary>Returns the property value of a specified object.</summary>
		/// <param name="obj">The object whose property value will be returned.</param>
		/// <returns>The property value of the specified object.</returns>
		// Token: 0x060049F7 RID: 18935 RVA: 0x000EF4E8 File Offset: 0x000ED6E8
		[DebuggerStepThrough]
		[DebuggerHidden]
		public object GetValue(object obj)
		{
			return this.GetValue(obj, null);
		}

		/// <summary>Returns the property value of a specified object with optional index values for indexed properties.</summary>
		/// <param name="obj">The object whose property value will be returned.</param>
		/// <param name="index">Optional index values for indexed properties. The indexes of indexed properties are zero-based. This value should be <see langword="null" /> for non-indexed properties.</param>
		/// <returns>The property value of the specified object.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> array does not contain the type of arguments needed.  
		///  -or-  
		///  The property's <see langword="get" /> accessor is not found.</exception>
		/// <exception cref="T:System.Reflection.TargetException">In the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.  
		///
		///
		///
		///
		///  The object does not match the target type, or a property is an instance property but <paramref name="obj" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index" /> does not match the number of parameters the indexed property takes.</exception>
		/// <exception cref="T:System.MethodAccessException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.  
		///
		///
		///
		///
		///  There was an illegal attempt to access a private or protected method inside a class.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while retrieving the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
		// Token: 0x060049F8 RID: 18936 RVA: 0x000EF4F2 File Offset: 0x000ED6F2
		[DebuggerHidden]
		[DebuggerStepThrough]
		public virtual object GetValue(object obj, object[] index)
		{
			return this.GetValue(obj, BindingFlags.Default, null, index, null);
		}

		/// <summary>When overridden in a derived class, returns the property value of a specified object that has the specified binding, index, and culture-specific information.</summary>
		/// <param name="obj">The object whose property value will be returned.</param>
		/// <param name="invokeAttr">A bitwise combination of the following enumeration members that specify the invocation attribute: <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="Static" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, and <see langword="SetProperty" />. You must specify a suitable invocation attribute. For example, to invoke a static member, set the <see langword="Static" /> flag.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="index">Optional index values for indexed properties. This value should be <see langword="null" /> for non-indexed properties.</param>
		/// <param name="culture">The culture for which the resource is to be localized. If the resource is not localized for this culture, the <see cref="P:System.Globalization.CultureInfo.Parent" /> property will be called successively in search of a match. If this value is <see langword="null" />, the culture-specific information is obtained from the <see cref="P:System.Globalization.CultureInfo.CurrentUICulture" /> property.</param>
		/// <returns>The property value of the specified object.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> array does not contain the type of arguments needed.  
		///  -or-  
		///  The property's <see langword="get" /> accessor is not found.</exception>
		/// <exception cref="T:System.Reflection.TargetException">The object does not match the target type, or a property is an instance property but <paramref name="obj" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index" /> does not match the number of parameters the indexed property takes.</exception>
		/// <exception cref="T:System.MethodAccessException">There was an illegal attempt to access a private or protected method inside a class.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while retrieving the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
		// Token: 0x060049F9 RID: 18937
		public abstract object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		/// <summary>Returns a literal value associated with the property by a compiler.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the literal value associated with the property. If the literal value is a class type with an element value of zero, the return value is <see langword="null" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The Constant table in unmanaged metadata does not contain a constant value for the current property.</exception>
		/// <exception cref="T:System.FormatException">The type of the value is not one of the types permitted by the Common Language Specification (CLS). See the ECMA Partition II specification, Metadata.</exception>
		// Token: 0x060049FA RID: 18938 RVA: 0x00047226 File Offset: 0x00045426
		public virtual object GetConstantValue()
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Returns a literal value associated with the property by a compiler.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the literal value associated with the property. If the literal value is a class type with an element value of zero, the return value is <see langword="null" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The Constant table in unmanaged metadata does not contain a constant value for the current property.</exception>
		/// <exception cref="T:System.FormatException">The type of the value is not one of the types permitted by the Common Language Specification (CLS). See the ECMA Partition II specification, Metadata Logical Format: Other Structures, Element Types used in Signatures.</exception>
		// Token: 0x060049FB RID: 18939 RVA: 0x00047226 File Offset: 0x00045426
		public virtual object GetRawConstantValue()
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Sets the property value of a specified object.</summary>
		/// <param name="obj">The object whose property value will be set.</param>
		/// <param name="value">The new property value.</param>
		/// <exception cref="T:System.ArgumentException">The property's <see langword="set" /> accessor is not found.  
		///  -or-  
		///  <paramref name="value" /> cannot be converted to the type of <see cref="P:System.Reflection.PropertyInfo.PropertyType" />.</exception>
		/// <exception cref="T:System.Reflection.TargetException">In the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.  
		///
		///
		///
		///
		///  The type of <paramref name="obj" /> does not match the target type, or a property is an instance property but <paramref name="obj" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MethodAccessException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.  
		///
		///
		///
		///
		///  There was an illegal attempt to access a private or protected method inside a class.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while setting the property value. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
		// Token: 0x060049FC RID: 18940 RVA: 0x000EF4FF File Offset: 0x000ED6FF
		[DebuggerHidden]
		[DebuggerStepThrough]
		public void SetValue(object obj, object value)
		{
			this.SetValue(obj, value, null);
		}

		/// <summary>Sets the property value of a specified object with optional index values for index properties.</summary>
		/// <param name="obj">The object whose property value will be set.</param>
		/// <param name="value">The new property value.</param>
		/// <param name="index">Optional index values for indexed properties. This value should be <see langword="null" /> for non-indexed properties.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> array does not contain the type of arguments needed.  
		///  -or-  
		///  The property's <see langword="set" /> accessor is not found.  
		///  -or-  
		///  <paramref name="value" /> cannot be converted to the type of <see cref="P:System.Reflection.PropertyInfo.PropertyType" />.</exception>
		/// <exception cref="T:System.Reflection.TargetException">In the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.  
		///
		///
		///
		///
		///  The object does not match the target type, or a property is an instance property but <paramref name="obj" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index" /> does not match the number of parameters the indexed property takes.</exception>
		/// <exception cref="T:System.MethodAccessException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.  
		///
		///
		///
		///
		///  There was an illegal attempt to access a private or protected method inside a class.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while setting the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
		// Token: 0x060049FD RID: 18941 RVA: 0x000EF50A File Offset: 0x000ED70A
		[DebuggerHidden]
		[DebuggerStepThrough]
		public virtual void SetValue(object obj, object value, object[] index)
		{
			this.SetValue(obj, value, BindingFlags.Default, null, index, null);
		}

		/// <summary>When overridden in a derived class, sets the property value for a specified object that has the specified binding, index, and culture-specific information.</summary>
		/// <param name="obj">The object whose property value will be set.</param>
		/// <param name="value">The new property value.</param>
		/// <param name="invokeAttr">A bitwise combination of the following enumeration members that specify the invocation attribute: <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="Static" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, or <see langword="SetProperty" />. You must specify a suitable invocation attribute. For example, to invoke a static member, set the <see langword="Static" /> flag.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="index">Optional index values for indexed properties. This value should be <see langword="null" /> for non-indexed properties.</param>
		/// <param name="culture">The culture for which the resource is to be localized. If the resource is not localized for this culture, the <see cref="P:System.Globalization.CultureInfo.Parent" /> property will be called successively in search of a match. If this value is <see langword="null" />, the culture-specific information is obtained from the <see cref="P:System.Globalization.CultureInfo.CurrentUICulture" /> property.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> array does not contain the type of arguments needed.  
		///  -or-  
		///  The property's <see langword="set" /> accessor is not found.  
		///  -or-  
		///  <paramref name="value" /> cannot be converted to the type of <see cref="P:System.Reflection.PropertyInfo.PropertyType" />.</exception>
		/// <exception cref="T:System.Reflection.TargetException">The object does not match the target type, or a property is an instance property but <paramref name="obj" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index" /> does not match the number of parameters the indexed property takes.</exception>
		/// <exception cref="T:System.MethodAccessException">There was an illegal attempt to access a private or protected method inside a class.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while setting the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
		// Token: 0x060049FE RID: 18942
		public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060049FF RID: 18943 RVA: 0x000EE36E File Offset: 0x000EC56E
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06004A00 RID: 18944 RVA: 0x000EE377 File Offset: 0x000EC577
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.PropertyInfo" /> objects are equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004A01 RID: 18945 RVA: 0x00064560 File Offset: 0x00062760
		public static bool operator ==(PropertyInfo left, PropertyInfo right)
		{
			return left == right || (left != null && right != null && left.Equals(right));
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.PropertyInfo" /> objects are not equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004A02 RID: 18946 RVA: 0x000EF518 File Offset: 0x000ED718
		public static bool operator !=(PropertyInfo left, PropertyInfo right)
		{
			return !(left == right);
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004A03 RID: 18947 RVA: 0x000173AD File Offset: 0x000155AD
		void _PropertyInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a <see cref="T:System.Type" /> object representing the <see cref="T:System.Reflection.PropertyInfo" /> type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Reflection.PropertyInfo" /> type.</returns>
		// Token: 0x06004A04 RID: 18948 RVA: 0x00052955 File Offset: 0x00050B55
		Type _PropertyInfo.GetType()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004A05 RID: 18949 RVA: 0x000173AD File Offset: 0x000155AD
		void _PropertyInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004A06 RID: 18950 RVA: 0x000173AD File Offset: 0x000155AD
		void _PropertyInfo.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x06004A07 RID: 18951 RVA: 0x000173AD File Offset: 0x000155AD
		void _PropertyInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
