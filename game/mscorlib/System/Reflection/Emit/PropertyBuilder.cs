using System;
using System.Globalization;
using System.Runtime.InteropServices;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Defines the properties for a type.</summary>
	// Token: 0x02000940 RID: 2368
	[ComDefaultInterface(typeof(_PropertyBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class PropertyBuilder : PropertyInfo, _PropertyBuilder
	{
		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06005217 RID: 21015 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _PropertyBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06005218 RID: 21016 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _PropertyBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06005219 RID: 21017 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _PropertyBuilder.GetTypeInfoCount(out uint pcTInfo)
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
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x0600521A RID: 21018 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _PropertyBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600521B RID: 21019 RVA: 0x00102460 File Offset: 0x00100660
		internal PropertyBuilder(TypeBuilder tb, string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnModReq, Type[] returnModOpt, Type[] parameterTypes, Type[][] paramModReq, Type[][] paramModOpt)
		{
			this.name = name;
			this.attrs = attributes;
			this.callingConvention = callingConvention;
			this.type = returnType;
			this.returnModReq = returnModReq;
			this.returnModOpt = returnModOpt;
			this.paramModReq = paramModReq;
			this.paramModOpt = paramModOpt;
			if (parameterTypes != null)
			{
				this.parameters = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.parameters, this.parameters.Length);
			}
			this.typeb = tb;
			this.table_idx = tb.get_next_table_index(this, 23, 1);
		}

		/// <summary>Gets the attributes for this property.</summary>
		/// <returns>Attributes of this property.</returns>
		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x0600521C RID: 21020 RVA: 0x001024F0 File Offset: 0x001006F0
		public override PropertyAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		/// <summary>Gets a value indicating whether the property can be read.</summary>
		/// <returns>
		///   <see langword="true" /> if this property can be read; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x0600521D RID: 21021 RVA: 0x001024F8 File Offset: 0x001006F8
		public override bool CanRead
		{
			get
			{
				return this.get_method != null;
			}
		}

		/// <summary>Gets a value indicating whether the property can be written to.</summary>
		/// <returns>
		///   <see langword="true" /> if this property can be written to; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x0600521E RID: 21022 RVA: 0x00102506 File Offset: 0x00100706
		public override bool CanWrite
		{
			get
			{
				return this.set_method != null;
			}
		}

		/// <summary>Gets the class that declares this member.</summary>
		/// <returns>The <see langword="Type" /> object for the class that declares this member.</returns>
		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x0600521F RID: 21023 RVA: 0x00102514 File Offset: 0x00100714
		public override Type DeclaringType
		{
			get
			{
				return this.typeb;
			}
		}

		/// <summary>Gets the name of this member.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of this member.</returns>
		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06005220 RID: 21024 RVA: 0x0010251C File Offset: 0x0010071C
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Retrieves the token for this property.</summary>
		/// <returns>Read-only. Retrieves the token for this property.</returns>
		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06005221 RID: 21025 RVA: 0x00102524 File Offset: 0x00100724
		public PropertyToken PropertyToken
		{
			get
			{
				return default(PropertyToken);
			}
		}

		/// <summary>Gets the type of the field of this property.</summary>
		/// <returns>The type of this property.</returns>
		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06005222 RID: 21026 RVA: 0x0010253A File Offset: 0x0010073A
		public override Type PropertyType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets the class object that was used to obtain this instance of <see langword="MemberInfo" />.</summary>
		/// <returns>The <see langword="Type" /> object through which this <see langword="MemberInfo" /> object was obtained.</returns>
		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06005223 RID: 21027 RVA: 0x00102514 File Offset: 0x00100714
		public override Type ReflectedType
		{
			get
			{
				return this.typeb;
			}
		}

		/// <summary>Adds one of the other methods associated with this property.</summary>
		/// <param name="mdBuilder">A <see langword="MethodBuilder" /> object that represents the other method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x06005224 RID: 21028 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void AddOtherMethod(MethodBuilder mdBuilder)
		{
		}

		/// <summary>Returns an array of the public and non-public <see langword="get" /> and <see langword="set" /> accessors on this property.</summary>
		/// <param name="nonPublic">Indicates whether non-public methods should be returned in the <see langword="MethodInfo" /> array. <see langword="true" /> if non-public methods are to be included; otherwise, <see langword="false" />.</param>
		/// <returns>An array of type <see langword="MethodInfo" /> containing the matching public or non-public accessors, or an empty array if matching accessors do not exist on this property.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06005225 RID: 21029 RVA: 0x0000AF5E File Offset: 0x0000915E
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			return null;
		}

		/// <summary>Returns an array of all the custom attributes for this property.</summary>
		/// <param name="inherit">If <see langword="true" />, walks up this property's inheritance chain to find the custom attributes</param>
		/// <returns>An array of all the custom attributes.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06005226 RID: 21030 RVA: 0x00102542 File Offset: 0x00100742
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Returns an array of custom attributes identified by <see cref="T:System.Type" />.</summary>
		/// <param name="attributeType">An array of custom attributes identified by type.</param>
		/// <param name="inherit">If <see langword="true" />, walks up this property's inheritance chain to find the custom attributes.</param>
		/// <returns>An array of custom attributes defined on this reflected member, or <see langword="null" /> if no attributes are defined on this member.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06005227 RID: 21031 RVA: 0x00102542 File Offset: 0x00100742
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Returns the public and non-public get accessor for this property.</summary>
		/// <param name="nonPublic">Indicates whether non-public get accessors should be returned. <see langword="true" /> if non-public methods are to be included; otherwise, <see langword="false" />.</param>
		/// <returns>A <see langword="MethodInfo" /> object representing the get accessor for this property, if <paramref name="nonPublic" /> is <see langword="true" />. Returns <see langword="null" /> if <paramref name="nonPublic" /> is <see langword="false" /> and the get accessor is non-public, or if <paramref name="nonPublic" /> is <see langword="true" /> but no get accessors exist.</returns>
		// Token: 0x06005228 RID: 21032 RVA: 0x0010254A File Offset: 0x0010074A
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			return this.get_method;
		}

		/// <summary>Returns an array of all the index parameters for the property.</summary>
		/// <returns>An array of type <see langword="ParameterInfo" /> containing the parameters for the indexes.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06005229 RID: 21033 RVA: 0x00102542 File Offset: 0x00100742
		public override ParameterInfo[] GetIndexParameters()
		{
			throw this.not_supported();
		}

		/// <summary>Returns the set accessor for this property.</summary>
		/// <param name="nonPublic">Indicates whether the accessor should be returned if it is non-public. <see langword="true" /> if non-public methods are to be included; otherwise, <see langword="false" />.</param>
		/// <returns>The property's <see langword="Set" /> method, or <see langword="null" />, as shown in the following table.  
		///   Value  
		///
		///   Condition  
		///
		///   A <see cref="T:System.Reflection.MethodInfo" /> object representing the Set method for this property.  
		///
		///   The set accessor is public.  
		///
		///  <paramref name="nonPublic" /> is true and non-public methods can be returned.  
		///
		///   null  
		///
		///  <paramref name="nonPublic" /> is true, but the property is read-only.  
		///
		///  <paramref name="nonPublic" /> is false and the set accessor is non-public.</returns>
		// Token: 0x0600522A RID: 21034 RVA: 0x00102552 File Offset: 0x00100752
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			return this.set_method;
		}

		/// <summary>Gets the value of the indexed property by calling the property's getter method.</summary>
		/// <param name="obj">The object whose property value will be returned.</param>
		/// <param name="index">Optional index values for indexed properties. This value should be <see langword="null" /> for non-indexed properties.</param>
		/// <returns>The value of the specified indexed property.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x0600522B RID: 21035 RVA: 0x0000AF5E File Offset: 0x0000915E
		public override object GetValue(object obj, object[] index)
		{
			return null;
		}

		/// <summary>Gets the value of a property having the specified binding, index, and <see langword="CultureInfo" />.</summary>
		/// <param name="obj">The object whose property value will be returned.</param>
		/// <param name="invokeAttr">The invocation attribute. This must be a bit flag from <see langword="BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="Static" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, or <see langword="SetProperty" />. A suitable invocation attribute must be specified. If a static member is to be invoked, the <see langword="Static" /> flag of <see langword="BindingFlags" /> must be set.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects using reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="index">Optional index values for indexed properties. This value should be <see langword="null" /> for non-indexed properties.</param>
		/// <param name="culture">The <see langword="CultureInfo" /> object that represents the culture for which the resource is to be localized. Note that if the resource is not localized for this culture, the <see langword="CultureInfo.Parent" /> method will be called successively in search of a match. If this value is <see langword="null" />, the <see langword="CultureInfo" /> is obtained from the <see langword="CultureInfo.CurrentUICulture" /> property.</param>
		/// <returns>The property value for <paramref name="obj" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x0600522C RID: 21036 RVA: 0x00102542 File Offset: 0x00100742
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			throw this.not_supported();
		}

		/// <summary>Indicates whether one or more instance of <paramref name="attributeType" /> is defined on this property.</summary>
		/// <param name="attributeType">The <see langword="Type" /> object to which the custom attributes are applied.</param>
		/// <param name="inherit">Specifies whether to walk up this property's inheritance chain to find the custom attributes.</param>
		/// <returns>
		///   <see langword="true" /> if one or more instance of <paramref name="attributeType" /> is defined on this property; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x0600522D RID: 21037 RVA: 0x00102542 File Offset: 0x00100742
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Sets the default value of this property.</summary>
		/// <param name="defaultValue">The default value of this property.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		/// <exception cref="T:System.ArgumentException">The property is not one of the supported types.  
		///  -or-  
		///  The type of <paramref name="defaultValue" /> does not match the type of the property.  
		///  -or-  
		///  The property is of type <see cref="T:System.Object" /> or other reference type, <paramref name="defaultValue" /> is not <see langword="null" />, and the value cannot be assigned to the reference type.</exception>
		// Token: 0x0600522E RID: 21038 RVA: 0x0010255A File Offset: 0x0010075A
		public void SetConstant(object defaultValue)
		{
			this.def_value = defaultValue;
		}

		/// <summary>Set a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customBuilder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">if <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x0600522F RID: 21039 RVA: 0x00102564 File Offset: 0x00100764
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder.Ctor.ReflectedType.FullName == "System.Runtime.CompilerServices.SpecialNameAttribute")
			{
				this.attrs |= PropertyAttributes.SpecialName;
				return;
			}
			if (this.cattrs != null)
			{
				CustomAttributeBuilder[] array = new CustomAttributeBuilder[this.cattrs.Length + 1];
				this.cattrs.CopyTo(array, 0);
				array[this.cattrs.Length] = customBuilder;
				this.cattrs = array;
				return;
			}
			this.cattrs = new CustomAttributeBuilder[1];
			this.cattrs[0] = customBuilder;
		}

		/// <summary>Set a custom attribute using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute.</param>
		/// <param name="binaryAttribute">A byte blob representing the attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x06005230 RID: 21040 RVA: 0x001025ED File Offset: 0x001007ED
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		/// <summary>Sets the method that gets the property value.</summary>
		/// <param name="mdBuilder">A <see langword="MethodBuilder" /> object that represents the method that gets the property value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x06005231 RID: 21041 RVA: 0x001025FC File Offset: 0x001007FC
		public void SetGetMethod(MethodBuilder mdBuilder)
		{
			this.get_method = mdBuilder;
		}

		/// <summary>Sets the method that sets the property value.</summary>
		/// <param name="mdBuilder">A <see langword="MethodBuilder" /> object that represents the method that sets the property value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="mdBuilder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been called on the enclosing type.</exception>
		// Token: 0x06005232 RID: 21042 RVA: 0x00102605 File Offset: 0x00100805
		public void SetSetMethod(MethodBuilder mdBuilder)
		{
			this.set_method = mdBuilder;
		}

		/// <summary>Sets the value of the property with optional index values for index properties.</summary>
		/// <param name="obj">The object whose property value will be set.</param>
		/// <param name="value">The new value for this property.</param>
		/// <param name="index">Optional index values for indexed properties. This value should be <see langword="null" /> for non-indexed properties.</param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06005233 RID: 21043 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public override void SetValue(object obj, object value, object[] index)
		{
		}

		/// <summary>Sets the property value for the given object to the given value.</summary>
		/// <param name="obj">The object whose property value will be returned.</param>
		/// <param name="value">The new value for this property.</param>
		/// <param name="invokeAttr">The invocation attribute. This must be a bit flag from <see langword="BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="Static" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, or <see langword="SetProperty" />. A suitable invocation attribute must be specified. If a static member is to be invoked, the <see langword="Static" /> flag of <see langword="BindingFlags" /> must be set.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects using reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="index">Optional index values for indexed properties. This value should be <see langword="null" /> for non-indexed properties.</param>
		/// <param name="culture">The <see langword="CultureInfo" /> object that represents the culture for which the resource is to be localized. Note that if the resource is not localized for this culture, the <see langword="CultureInfo.Parent" /> method will be called successively in search of a match. If this value is <see langword="null" />, the <see langword="CultureInfo" /> is obtained from the <see langword="CultureInfo.CurrentUICulture" /> property.</param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06005234 RID: 21044 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
		}

		/// <summary>Gets the module in which the type that declares the current property is being defined.</summary>
		/// <returns>The <see cref="T:System.Reflection.Module" /> in which the type that declares the current property is defined.</returns>
		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06005235 RID: 21045 RVA: 0x000FAE24 File Offset: 0x000F9024
		public override Module Module
		{
			get
			{
				return base.Module;
			}
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x000F79BE File Offset: 0x000F5BBE
		private Exception not_supported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x000173AD File Offset: 0x000155AD
		internal PropertyBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040032F8 RID: 13048
		private PropertyAttributes attrs;

		// Token: 0x040032F9 RID: 13049
		private string name;

		// Token: 0x040032FA RID: 13050
		private Type type;

		// Token: 0x040032FB RID: 13051
		private Type[] parameters;

		// Token: 0x040032FC RID: 13052
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x040032FD RID: 13053
		private object def_value;

		// Token: 0x040032FE RID: 13054
		private MethodBuilder set_method;

		// Token: 0x040032FF RID: 13055
		private MethodBuilder get_method;

		// Token: 0x04003300 RID: 13056
		private int table_idx;

		// Token: 0x04003301 RID: 13057
		internal TypeBuilder typeb;

		// Token: 0x04003302 RID: 13058
		private Type[] returnModReq;

		// Token: 0x04003303 RID: 13059
		private Type[] returnModOpt;

		// Token: 0x04003304 RID: 13060
		private Type[][] paramModReq;

		// Token: 0x04003305 RID: 13061
		private Type[][] paramModOpt;

		// Token: 0x04003306 RID: 13062
		private CallingConventions callingConvention;
	}
}
