using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Defines and represents a constructor of a dynamic class.</summary>
	// Token: 0x02000915 RID: 2325
	[ComDefaultInterface(typeof(_ConstructorBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ConstructorBuilder : ConstructorInfo, _ConstructorBuilder
	{
		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004EEC RID: 20204 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _ConstructorBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004EED RID: 20205 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _ConstructorBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004EEE RID: 20206 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _ConstructorBuilder.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x06004EEF RID: 20207 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _ConstructorBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004EF0 RID: 20208 RVA: 0x000F7F44 File Offset: 0x000F6144
		internal ConstructorBuilder(TypeBuilder tb, MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] paramModReq, Type[][] paramModOpt)
		{
			this.init_locals = true;
			base..ctor();
			this.attrs = (attributes | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
			this.call_conv = callingConvention;
			if (parameterTypes != null)
			{
				for (int i = 0; i < parameterTypes.Length; i++)
				{
					if (parameterTypes[i] == null)
					{
						throw new ArgumentException("Elements of the parameterTypes array cannot be null", "parameterTypes");
					}
				}
				this.parameters = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.parameters, parameterTypes.Length);
			}
			this.type = tb;
			this.paramModReq = paramModReq;
			this.paramModOpt = paramModOpt;
			this.table_idx = this.get_next_table_index(this, 6, 1);
			((ModuleBuilder)tb.Module).RegisterToken(this, this.GetToken().Token);
		}

		/// <summary>Gets a <see cref="T:System.Reflection.CallingConventions" /> value that depends on whether the declaring type is generic.</summary>
		/// <returns>
		///   <see cref="F:System.Reflection.CallingConventions.HasThis" /> if the declaring type is generic; otherwise, <see cref="F:System.Reflection.CallingConventions.Standard" />.</returns>
		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06004EF1 RID: 20209 RVA: 0x000F800C File Offset: 0x000F620C
		[MonoTODO]
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.call_conv;
			}
		}

		/// <summary>Gets or sets whether the local variables in this constructor should be zero-initialized.</summary>
		/// <returns>Read/write. Gets or sets whether the local variables in this constructor should be zero-initialized.</returns>
		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06004EF2 RID: 20210 RVA: 0x000F8014 File Offset: 0x000F6214
		// (set) Token: 0x06004EF3 RID: 20211 RVA: 0x000F801C File Offset: 0x000F621C
		public bool InitLocals
		{
			get
			{
				return this.init_locals;
			}
			set
			{
				this.init_locals = value;
			}
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06004EF4 RID: 20212 RVA: 0x000F8025 File Offset: 0x000F6225
		internal TypeBuilder TypeBuilder
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Returns the method implementation flags for this constructor.</summary>
		/// <returns>The method implementation flags for this constructor.</returns>
		// Token: 0x06004EF5 RID: 20213 RVA: 0x000F802D File Offset: 0x000F622D
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.iattrs;
		}

		/// <summary>Returns the parameters of this constructor.</summary>
		/// <returns>An array that represents the parameters of this constructor.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has not been called on this constructor's type, in the .NET Framework versions 1.0 and 1.1.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has not been called on this constructor's type, in the .NET Framework version 2.0.</exception>
		// Token: 0x06004EF6 RID: 20214 RVA: 0x000F8035 File Offset: 0x000F6235
		public override ParameterInfo[] GetParameters()
		{
			if (!this.type.is_created)
			{
				throw this.not_created();
			}
			return this.GetParametersInternal();
		}

		// Token: 0x06004EF7 RID: 20215 RVA: 0x000F8054 File Offset: 0x000F6254
		internal override ParameterInfo[] GetParametersInternal()
		{
			if (this.parameters == null)
			{
				return EmptyArray<ParameterInfo>.Value;
			}
			ParameterInfo[] array = new ParameterInfo[this.parameters.Length];
			for (int i = 0; i < this.parameters.Length; i++)
			{
				ParameterInfo[] array2 = array;
				int num = i;
				ParameterBuilder[] array3 = this.pinfo;
				array2[num] = RuntimeParameterInfo.New((array3 != null) ? array3[i + 1] : null, this.parameters[i], this, i + 1);
			}
			return array;
		}

		// Token: 0x06004EF8 RID: 20216 RVA: 0x000F80B6 File Offset: 0x000F62B6
		internal override int GetParametersCount()
		{
			if (this.parameters == null)
			{
				return 0;
			}
			return this.parameters.Length;
		}

		// Token: 0x06004EF9 RID: 20217 RVA: 0x000F80CA File Offset: 0x000F62CA
		internal override Type GetParameterType(int pos)
		{
			return this.parameters[pos];
		}

		// Token: 0x06004EFA RID: 20218 RVA: 0x000F80D4 File Offset: 0x000F62D4
		internal MethodBase RuntimeResolve()
		{
			return this.type.RuntimeResolve().GetConstructor(this);
		}

		/// <summary>Dynamically invokes the constructor reflected by this instance with the specified arguments, under the constraints of the specified <see langword="Binder" />.</summary>
		/// <param name="obj">The object that needs to be reinitialized.</param>
		/// <param name="invokeAttr">One of the <see langword="BindingFlags" /> values that specifies the type of binding that is desired.</param>
		/// <param name="binder">A <see langword="Binder" /> that defines a set of properties and enables the binding, coercion of argument types, and invocation of members using reflection. If <paramref name="binder" /> is <see langword="null" />, then Binder.DefaultBinding is used.</param>
		/// <param name="parameters">An argument list. This is an array of arguments with the same number, order, and type as the parameters of the constructor to be invoked. If there are no parameters, this should be a null reference (<see langword="Nothing" /> in Visual Basic).</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. If this is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used.</param>
		/// <returns>An instance of the class associated with the constructor.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. You can retrieve the constructor using <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.ConstructorInfo.Invoke(System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> on the returned <see cref="T:System.Reflection.ConstructorInfo" />.</exception>
		// Token: 0x06004EFB RID: 20219 RVA: 0x000F80E7 File Offset: 0x000F62E7
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw this.not_supported();
		}

		/// <summary>Dynamically invokes the constructor represented by this instance on the given object, passing along the specified parameters, and under the constraints of the given binder.</summary>
		/// <param name="invokeAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as InvokeMethod, NonPublic, and so on.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects using reflection. If binder is <see langword="null" />, the default binder is used. See <see cref="T:System.Reflection.Binder" />.</param>
		/// <param name="parameters">An argument list. This is an array of arguments with the same number, order, and type as the parameters of the constructor to be invoked. If there are no parameters this should be <see langword="null" />.</param>
		/// <param name="culture">An instance of <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. If this is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. (For example, this is necessary to convert a <see cref="T:System.String" /> that represents 1000 to a <see cref="T:System.Double" /> value, since 1000 is represented differently by different cultures.)</param>
		/// <returns>The value returned by the invoked constructor.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. You can retrieve the constructor using <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.ConstructorInfo.Invoke(System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> on the returned <see cref="T:System.Reflection.ConstructorInfo" />.</exception>
		// Token: 0x06004EFC RID: 20220 RVA: 0x000F80E7 File Offset: 0x000F62E7
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw this.not_supported();
		}

		/// <summary>Gets the internal handle for the method. Use this handle to access the underlying metadata handle.</summary>
		/// <returns>The internal handle for the method. Use this handle to access the underlying metadata handle.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this class.</exception>
		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06004EFD RID: 20221 RVA: 0x000F80E7 File Offset: 0x000F62E7
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw this.not_supported();
			}
		}

		/// <summary>Gets the attributes for this constructor.</summary>
		/// <returns>The attributes for this constructor.</returns>
		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06004EFE RID: 20222 RVA: 0x000F80EF File Offset: 0x000F62EF
		public override MethodAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		/// <summary>Holds a reference to the <see cref="T:System.Type" /> object from which this object was obtained.</summary>
		/// <returns>The <see langword="Type" /> object from which this object was obtained.</returns>
		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06004EFF RID: 20223 RVA: 0x000F8025 File Offset: 0x000F6225
		public override Type ReflectedType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Type" /> object for the type that declares this member.</summary>
		/// <returns>The type that declares this member.</returns>
		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06004F00 RID: 20224 RVA: 0x000F8025 File Offset: 0x000F6225
		public override Type DeclaringType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets <see langword="null" />.</summary>
		/// <returns>Returns <see langword="null" />.</returns>
		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06004F01 RID: 20225 RVA: 0x0000AF5E File Offset: 0x0000915E
		[Obsolete]
		public Type ReturnType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Retrieves the name of this constructor.</summary>
		/// <returns>The name of this constructor.</returns>
		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06004F02 RID: 20226 RVA: 0x000F80F7 File Offset: 0x000F62F7
		public override string Name
		{
			get
			{
				if ((this.attrs & MethodAttributes.Static) == MethodAttributes.PrivateScope)
				{
					return ConstructorInfo.ConstructorName;
				}
				return ConstructorInfo.TypeConstructorName;
			}
		}

		/// <summary>Retrieves the signature of the field in the form of a string.</summary>
		/// <returns>The signature of the field.</returns>
		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06004F03 RID: 20227 RVA: 0x000F810F File Offset: 0x000F630F
		public string Signature
		{
			get
			{
				return "constructor signature";
			}
		}

		/// <summary>Adds declarative security to this constructor.</summary>
		/// <param name="action">The security action to be taken, such as Demand, Assert, and so on.</param>
		/// <param name="pset">The set of permissions the action applies to.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="action" /> is invalid (RequestMinimum, RequestOptional, and RequestRefuse are invalid).</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  The permission set <paramref name="pset" /> contains an action that was added earlier by <see langword="AddDeclarativeSecurity" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pset" /> is <see langword="null" />.</exception>
		// Token: 0x06004F04 RID: 20228 RVA: 0x000F8118 File Offset: 0x000F6318
		public void AddDeclarativeSecurity(SecurityAction action, PermissionSet pset)
		{
			if (pset == null)
			{
				throw new ArgumentNullException("pset");
			}
			if (action == SecurityAction.RequestMinimum || action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse)
			{
				throw new ArgumentOutOfRangeException("action", "Request* values are not permitted");
			}
			this.RejectIfCreated();
			if (this.permissions != null)
			{
				RefEmitPermissionSet[] array = this.permissions;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].action == action)
					{
						throw new InvalidOperationException("Multiple permission sets specified with the same SecurityAction.");
					}
				}
				RefEmitPermissionSet[] array2 = new RefEmitPermissionSet[this.permissions.Length + 1];
				this.permissions.CopyTo(array2, 0);
				this.permissions = array2;
			}
			else
			{
				this.permissions = new RefEmitPermissionSet[1];
			}
			this.permissions[this.permissions.Length - 1] = new RefEmitPermissionSet(action, pset.ToXml().ToString());
			this.attrs |= MethodAttributes.HasSecurity;
		}

		/// <summary>Defines a parameter of this constructor.</summary>
		/// <param name="iSequence">The position of the parameter in the parameter list. Parameters are indexed beginning with the number 1 for the first parameter.</param>
		/// <param name="attributes">The attributes of the parameter.</param>
		/// <param name="strParamName">The name of the parameter. The name can be the null string.</param>
		/// <returns>An object that represents the new parameter of this constructor.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="iSequence" /> is less than 0 (zero), or it is greater than the number of parameters of the constructor.</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x06004F05 RID: 20229 RVA: 0x000F81F8 File Offset: 0x000F63F8
		public ParameterBuilder DefineParameter(int iSequence, ParameterAttributes attributes, string strParamName)
		{
			if (iSequence < 0 || iSequence > this.GetParametersCount())
			{
				throw new ArgumentOutOfRangeException("iSequence");
			}
			if (this.type.is_created)
			{
				throw this.not_after_created();
			}
			ParameterBuilder parameterBuilder = new ParameterBuilder(this, iSequence, attributes, strParamName);
			if (this.pinfo == null)
			{
				this.pinfo = new ParameterBuilder[this.parameters.Length + 1];
			}
			this.pinfo[iSequence] = parameterBuilder;
			return parameterBuilder;
		}

		/// <summary>Checks if the specified custom attribute type is defined.</summary>
		/// <param name="attributeType">A custom attribute type.</param>
		/// <param name="inherit">Controls inheritance of custom attributes from base classes. This parameter is ignored.</param>
		/// <returns>
		///   <see langword="true" /> if the specified custom attribute type is defined; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. You can retrieve the constructor using <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> on the returned <see cref="T:System.Reflection.ConstructorInfo" />.</exception>
		// Token: 0x06004F06 RID: 20230 RVA: 0x000F80E7 File Offset: 0x000F62E7
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Returns all the custom attributes defined for this constructor.</summary>
		/// <param name="inherit">Controls inheritance of custom attributes from base classes. This parameter is ignored.</param>
		/// <returns>An array of objects representing all the custom attributes of the constructor represented by this <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> instance.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported.</exception>
		// Token: 0x06004F07 RID: 20231 RVA: 0x000F80E7 File Offset: 0x000F62E7
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Returns the custom attributes identified by the given type.</summary>
		/// <param name="attributeType">The custom attribute type.</param>
		/// <param name="inherit">Controls inheritance of custom attributes from base classes. This parameter is ignored.</param>
		/// <returns>An object array that represents the attributes of this constructor.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported.</exception>
		// Token: 0x06004F08 RID: 20232 RVA: 0x000F80E7 File Offset: 0x000F62E7
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Gets an <see cref="T:System.Reflection.Emit.ILGenerator" /> for this constructor.</summary>
		/// <returns>An <see cref="T:System.Reflection.Emit.ILGenerator" /> object for this constructor.</returns>
		/// <exception cref="T:System.InvalidOperationException">The constructor is a default constructor.  
		///  -or-  
		///  The constructor has <see cref="T:System.Reflection.MethodAttributes" /> or <see cref="T:System.Reflection.MethodImplAttributes" /> flags indicating that it should not have a method body.</exception>
		// Token: 0x06004F09 RID: 20233 RVA: 0x000F8262 File Offset: 0x000F6462
		public ILGenerator GetILGenerator()
		{
			return this.GetILGenerator(64);
		}

		/// <summary>Gets an <see cref="T:System.Reflection.Emit.ILGenerator" /> object, with the specified MSIL stream size, that can be used to build a method body for this constructor.</summary>
		/// <param name="streamSize">The size of the MSIL stream, in bytes.</param>
		/// <returns>An <see cref="T:System.Reflection.Emit.ILGenerator" /> for this constructor.</returns>
		/// <exception cref="T:System.InvalidOperationException">The constructor is a default constructor.  
		///  -or-  
		///  The constructor has <see cref="T:System.Reflection.MethodAttributes" /> or <see cref="T:System.Reflection.MethodImplAttributes" /> flags indicating that it should not have a method body.</exception>
		// Token: 0x06004F0A RID: 20234 RVA: 0x000F826C File Offset: 0x000F646C
		public ILGenerator GetILGenerator(int streamSize)
		{
			if (this.ilgen != null)
			{
				return this.ilgen;
			}
			this.ilgen = new ILGenerator(this.type.Module, ((ModuleBuilder)this.type.Module).GetTokenGenerator(), streamSize);
			return this.ilgen;
		}

		/// <summary>Creates the body of the constructor by using a specified byte array of Microsoft intermediate language (MSIL) instructions.</summary>
		/// <param name="il">An array that contains valid MSIL instructions.</param>
		/// <param name="maxStack">The maximum stack evaluation depth.</param>
		/// <param name="localSignature">An array of bytes that contain the serialized local variable structure. Specify <see langword="null" /> if the constructor has no local variables.</param>
		/// <param name="exceptionHandlers">A collection that contains the exception handlers for the constructor. Specify <see langword="null" /> if the constructor has no exception handlers.</param>
		/// <param name="tokenFixups">A collection of values that represent offsets in <paramref name="il" />, each of which specifies the beginning of a token that may be modified. Specify <see langword="null" /> if the constructor has no tokens that have to be modified.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="il" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxStack" /> is negative.  
		/// -or-  
		/// One of <paramref name="exceptionHandlers" /> specifies an offset outside of <paramref name="il" />.  
		/// -or-  
		/// One of <paramref name="tokenFixups" /> specifies an offset that is outside the <paramref name="il" /> array.</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type was previously created using the <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> method.  
		///  -or-  
		///  This method was called previously on this <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> object.</exception>
		// Token: 0x06004F0B RID: 20235 RVA: 0x000F82BA File Offset: 0x000F64BA
		public void SetMethodBody(byte[] il, int maxStack, byte[] localSignature, IEnumerable<ExceptionHandler> exceptionHandlers, IEnumerable<int> tokenFixups)
		{
			this.GetILGenerator().Init(il, maxStack, localSignature, exceptionHandlers, tokenFixups);
		}

		/// <summary>Set a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customBuilder" /> is <see langword="null" />.</exception>
		// Token: 0x06004F0C RID: 20236 RVA: 0x000F82D0 File Offset: 0x000F64D0
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			if (customBuilder.Ctor.ReflectedType.FullName == "System.Runtime.CompilerServices.MethodImplAttribute")
			{
				byte[] data = customBuilder.Data;
				int num = (int)data[2];
				num |= (int)data[3] << 8;
				this.SetImplementationFlags((MethodImplAttributes)num);
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
		// Token: 0x06004F0D RID: 20237 RVA: 0x000F836F File Offset: 0x000F656F
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		/// <summary>Sets the method implementation flags for this constructor.</summary>
		/// <param name="attributes">The method implementation flags.</param>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x06004F0E RID: 20238 RVA: 0x000F83A0 File Offset: 0x000F65A0
		public void SetImplementationFlags(MethodImplAttributes attributes)
		{
			if (this.type.is_created)
			{
				throw this.not_after_created();
			}
			this.iattrs = attributes;
		}

		/// <summary>Returns a reference to the module that contains this constructor.</summary>
		/// <returns>The module that contains this constructor.</returns>
		// Token: 0x06004F0F RID: 20239 RVA: 0x000F83BD File Offset: 0x000F65BD
		public Module GetModule()
		{
			return this.type.Module;
		}

		/// <summary>Returns the <see cref="T:System.Reflection.Emit.MethodToken" /> that represents the token for this constructor.</summary>
		/// <returns>The <see cref="T:System.Reflection.Emit.MethodToken" /> of this constructor.</returns>
		// Token: 0x06004F10 RID: 20240 RVA: 0x000F83CA File Offset: 0x000F65CA
		public MethodToken GetToken()
		{
			return new MethodToken(100663296 | this.table_idx);
		}

		/// <summary>Sets this constructor's custom attribute associated with symbolic information.</summary>
		/// <param name="name">The name of the custom attribute.</param>
		/// <param name="data">The value of the custom attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  The module does not have a symbol writer defined. For example, the module is not a debug module.</exception>
		// Token: 0x06004F11 RID: 20241 RVA: 0x000F83DD File Offset: 0x000F65DD
		[MonoTODO]
		public void SetSymCustomAttribute(string name, byte[] data)
		{
			if (this.type.is_created)
			{
				throw this.not_after_created();
			}
		}

		/// <summary>Gets the dynamic module in which this constructor is defined.</summary>
		/// <returns>A <see cref="T:System.Reflection.Module" /> object that represents the dynamic module in which this constructor is defined.</returns>
		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06004F12 RID: 20242 RVA: 0x000F83F3 File Offset: 0x000F65F3
		public override Module Module
		{
			get
			{
				return this.GetModule();
			}
		}

		/// <summary>Returns this <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> instance as a <see cref="T:System.String" />.</summary>
		/// <returns>A string containing the name, attributes, and exceptions of this constructor, followed by the current Microsoft intermediate language (MSIL) stream.</returns>
		// Token: 0x06004F13 RID: 20243 RVA: 0x000F83FB File Offset: 0x000F65FB
		public override string ToString()
		{
			return "ConstructorBuilder ['" + this.type.Name + "']";
		}

		// Token: 0x06004F14 RID: 20244 RVA: 0x000F8418 File Offset: 0x000F6618
		internal void fixup()
		{
			if ((this.attrs & (MethodAttributes.Abstract | MethodAttributes.PinvokeImpl)) == MethodAttributes.PrivateScope && (this.iattrs & (MethodImplAttributes)4099) == MethodImplAttributes.IL && (this.ilgen == null || this.ilgen.ILOffset == 0))
			{
				throw new InvalidOperationException("Method '" + this.Name + "' does not have a method body.");
			}
			if (this.ilgen != null)
			{
				this.ilgen.label_fixup(this);
			}
		}

		// Token: 0x06004F15 RID: 20245 RVA: 0x000F8488 File Offset: 0x000F6688
		internal void ResolveUserTypes()
		{
			TypeBuilder.ResolveUserTypes(this.parameters);
			if (this.paramModReq != null)
			{
				Type[][] array = this.paramModReq;
				for (int i = 0; i < array.Length; i++)
				{
					TypeBuilder.ResolveUserTypes(array[i]);
				}
			}
			if (this.paramModOpt != null)
			{
				Type[][] array = this.paramModOpt;
				for (int i = 0; i < array.Length; i++)
				{
					TypeBuilder.ResolveUserTypes(array[i]);
				}
			}
		}

		// Token: 0x06004F16 RID: 20246 RVA: 0x000F84EA File Offset: 0x000F66EA
		internal void FixupTokens(Dictionary<int, int> token_map, Dictionary<int, MemberInfo> member_map)
		{
			if (this.ilgen != null)
			{
				this.ilgen.FixupTokens(token_map, member_map);
			}
		}

		// Token: 0x06004F17 RID: 20247 RVA: 0x000F8504 File Offset: 0x000F6704
		internal void GenerateDebugInfo(ISymbolWriter symbolWriter)
		{
			if (this.ilgen != null && this.ilgen.HasDebugInfo)
			{
				SymbolToken symbolToken = new SymbolToken(this.GetToken().Token);
				symbolWriter.OpenMethod(symbolToken);
				symbolWriter.SetSymAttribute(symbolToken, "__name", Encoding.UTF8.GetBytes(this.Name));
				this.ilgen.GenerateDebugInfo(symbolWriter);
				symbolWriter.CloseMethod();
			}
		}

		// Token: 0x06004F18 RID: 20248 RVA: 0x000F8570 File Offset: 0x000F6770
		internal override int get_next_table_index(object obj, int table, int count)
		{
			return this.type.get_next_table_index(obj, table, count);
		}

		// Token: 0x06004F19 RID: 20249 RVA: 0x000F8580 File Offset: 0x000F6780
		private void RejectIfCreated()
		{
			if (this.type.is_created)
			{
				throw new InvalidOperationException("Type definition of the method is complete.");
			}
		}

		// Token: 0x06004F1A RID: 20250 RVA: 0x000F79BE File Offset: 0x000F5BBE
		private Exception not_supported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x06004F1B RID: 20251 RVA: 0x000F859A File Offset: 0x000F679A
		private Exception not_after_created()
		{
			return new InvalidOperationException("Unable to change after type has been created.");
		}

		// Token: 0x06004F1C RID: 20252 RVA: 0x000F85A6 File Offset: 0x000F67A6
		private Exception not_created()
		{
			return new NotSupportedException("The type is not yet created.");
		}

		// Token: 0x06004F1D RID: 20253 RVA: 0x000173AD File Offset: 0x000155AD
		internal ConstructorBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04003119 RID: 12569
		private RuntimeMethodHandle mhandle;

		// Token: 0x0400311A RID: 12570
		private ILGenerator ilgen;

		// Token: 0x0400311B RID: 12571
		internal Type[] parameters;

		// Token: 0x0400311C RID: 12572
		private MethodAttributes attrs;

		// Token: 0x0400311D RID: 12573
		private MethodImplAttributes iattrs;

		// Token: 0x0400311E RID: 12574
		private int table_idx;

		// Token: 0x0400311F RID: 12575
		private CallingConventions call_conv;

		// Token: 0x04003120 RID: 12576
		private TypeBuilder type;

		// Token: 0x04003121 RID: 12577
		internal ParameterBuilder[] pinfo;

		// Token: 0x04003122 RID: 12578
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04003123 RID: 12579
		private bool init_locals;

		// Token: 0x04003124 RID: 12580
		private Type[][] paramModReq;

		// Token: 0x04003125 RID: 12581
		private Type[][] paramModOpt;

		// Token: 0x04003126 RID: 12582
		private RefEmitPermissionSet[] permissions;
	}
}
