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
	/// <summary>Defines and represents a method (or constructor) on a dynamic class.</summary>
	// Token: 0x02000934 RID: 2356
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_MethodBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class MethodBuilder : MethodInfo, _MethodBuilder
	{
		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array that receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06005100 RID: 20736 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _MethodBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06005101 RID: 20737 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _MethodBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06005102 RID: 20738 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _MethodBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DispIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06005103 RID: 20739 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _MethodBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005104 RID: 20740 RVA: 0x000FD818 File Offset: 0x000FBA18
		internal MethodBuilder(TypeBuilder tb, string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnModReq, Type[] returnModOpt, Type[] parameterTypes, Type[][] paramModReq, Type[][] paramModOpt)
		{
			this.init_locals = true;
			base..ctor();
			this.name = name;
			this.attrs = attributes;
			this.call_conv = callingConvention;
			this.rtype = returnType;
			this.returnModReq = returnModReq;
			this.returnModOpt = returnModOpt;
			this.paramModReq = paramModReq;
			this.paramModOpt = paramModOpt;
			if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				this.call_conv |= CallingConventions.HasThis;
			}
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
			this.table_idx = this.get_next_table_index(this, 6, 1);
			((ModuleBuilder)tb.Module).RegisterToken(this, this.GetToken().Token);
		}

		// Token: 0x06005105 RID: 20741 RVA: 0x000FD90C File Offset: 0x000FBB0C
		internal MethodBuilder(TypeBuilder tb, string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnModReq, Type[] returnModOpt, Type[] parameterTypes, Type[][] paramModReq, Type[][] paramModOpt, string dllName, string entryName, CallingConvention nativeCConv, CharSet nativeCharset) : this(tb, name, attributes, callingConvention, returnType, returnModReq, returnModOpt, parameterTypes, paramModReq, paramModOpt)
		{
			this.pi_dll = dllName;
			this.pi_entry = entryName;
			this.native_cc = nativeCConv;
			this.charset = nativeCharset;
		}

		/// <summary>Not supported for this type.</summary>
		/// <returns>Not supported.</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06005106 RID: 20742 RVA: 0x000472C8 File Offset: 0x000454C8
		public override bool ContainsGenericParameters
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether the local variables in this method are zero initialized. The default value of this property is <see langword="true" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the local variables in this method should be zero initialized; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is <see langword="false" />. (Get or set.)</exception>
		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06005107 RID: 20743 RVA: 0x000FD950 File Offset: 0x000FBB50
		// (set) Token: 0x06005108 RID: 20744 RVA: 0x000FD958 File Offset: 0x000FBB58
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

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06005109 RID: 20745 RVA: 0x000FD961 File Offset: 0x000FBB61
		internal TypeBuilder TypeBuilder
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Retrieves the internal handle for the method. Use this handle to access the underlying metadata handle.</summary>
		/// <returns>Read-only. The internal handle for the method. Use this handle to access the underlying metadata handle.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="P:System.Reflection.MethodBase.MethodHandle" /> on the returned <see cref="T:System.Reflection.MethodInfo" />.</exception>
		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x0600510A RID: 20746 RVA: 0x000FD969 File Offset: 0x000FBB69
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw this.NotSupported();
			}
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x0600510B RID: 20747 RVA: 0x000FD971 File Offset: 0x000FBB71
		internal RuntimeMethodHandle MethodHandleInternal
		{
			get
			{
				return this.mhandle;
			}
		}

		/// <summary>Gets the return type of the method represented by this <see cref="T:System.Reflection.Emit.MethodBuilder" />.</summary>
		/// <returns>The return type of the method.</returns>
		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x0600510C RID: 20748 RVA: 0x000FD979 File Offset: 0x000FBB79
		public override Type ReturnType
		{
			get
			{
				return this.rtype;
			}
		}

		/// <summary>Retrieves the class that was used in reflection to obtain this object.</summary>
		/// <returns>Read-only. The type used to obtain this method.</returns>
		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x0600510D RID: 20749 RVA: 0x000FD961 File Offset: 0x000FBB61
		public override Type ReflectedType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Returns the type that declares this method.</summary>
		/// <returns>Read-only. The type that declares this method.</returns>
		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x0600510E RID: 20750 RVA: 0x000FD961 File Offset: 0x000FBB61
		public override Type DeclaringType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Retrieves the name of this method.</summary>
		/// <returns>Read-only. Retrieves a string containing the simple name of this method.</returns>
		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x0600510F RID: 20751 RVA: 0x000FD981 File Offset: 0x000FBB81
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Retrieves the attributes for this method.</summary>
		/// <returns>Read-only. Retrieves the <see langword="MethodAttributes" /> for this method.</returns>
		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06005110 RID: 20752 RVA: 0x000FD989 File Offset: 0x000FBB89
		public override MethodAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		/// <summary>Returns the custom attributes of the method's return type.</summary>
		/// <returns>Read-only. The custom attributes of the method's return type.</returns>
		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06005111 RID: 20753 RVA: 0x0000AF5E File Offset: 0x0000915E
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return null;
			}
		}

		/// <summary>Returns the calling convention of the method.</summary>
		/// <returns>Read-only. The calling convention of the method.</returns>
		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06005112 RID: 20754 RVA: 0x000FD991 File Offset: 0x000FBB91
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.call_conv;
			}
		}

		/// <summary>Retrieves the signature of the method.</summary>
		/// <returns>Read-only. A String containing the signature of the method reflected by this <see langword="MethodBase" /> instance.</returns>
		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06005113 RID: 20755 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO("Not implemented")]
		public string Signature
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (set) Token: 0x06005114 RID: 20756 RVA: 0x000FD999 File Offset: 0x000FBB99
		internal bool BestFitMapping
		{
			set
			{
				this.extra_flags = (uint)(((ulong)this.extra_flags & 18446744073709551567UL) | (value ? 16UL : 32UL));
			}
		}

		// Token: 0x17000D66 RID: 3430
		// (set) Token: 0x06005115 RID: 20757 RVA: 0x000FD9B8 File Offset: 0x000FBBB8
		internal bool ThrowOnUnmappableChar
		{
			set
			{
				this.extra_flags = (uint)(((ulong)this.extra_flags & 18446744073709539327UL) | (value ? 4096UL : 8192UL));
			}
		}

		// Token: 0x17000D67 RID: 3431
		// (set) Token: 0x06005116 RID: 20758 RVA: 0x000FD9E0 File Offset: 0x000FBBE0
		internal bool ExactSpelling
		{
			set
			{
				this.extra_flags = (uint)(((ulong)this.extra_flags & 18446744073709551614UL) | (value ? 1UL : 0UL));
			}
		}

		// Token: 0x17000D68 RID: 3432
		// (set) Token: 0x06005117 RID: 20759 RVA: 0x000FD9FD File Offset: 0x000FBBFD
		internal bool SetLastError
		{
			set
			{
				this.extra_flags = (uint)(((ulong)this.extra_flags & 18446744073709551551UL) | (value ? 64UL : 0UL));
			}
		}

		/// <summary>Returns the <see langword="MethodToken" /> that represents the token for this method.</summary>
		/// <returns>Returns the <see langword="MethodToken" /> of this method.</returns>
		// Token: 0x06005118 RID: 20760 RVA: 0x000FDA1B File Offset: 0x000FBC1B
		public MethodToken GetToken()
		{
			return new MethodToken(100663296 | this.table_idx);
		}

		/// <summary>Return the base implementation for a method.</summary>
		/// <returns>The base implementation of this method.</returns>
		// Token: 0x06005119 RID: 20761 RVA: 0x0000270D File Offset: 0x0000090D
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		/// <summary>Returns the implementation flags for the method.</summary>
		/// <returns>Returns the implementation flags for the method.</returns>
		// Token: 0x0600511A RID: 20762 RVA: 0x000FDA2E File Offset: 0x000FBC2E
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.iattrs;
		}

		/// <summary>Returns the parameters of this method.</summary>
		/// <returns>An array of <see langword="ParameterInfo" /> objects that represent the parameters of the method.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see langword="GetParameters" /> on the returned <see cref="T:System.Reflection.MethodInfo" />.</exception>
		// Token: 0x0600511B RID: 20763 RVA: 0x000FDA36 File Offset: 0x000FBC36
		public override ParameterInfo[] GetParameters()
		{
			if (!this.type.is_created)
			{
				throw this.NotSupported();
			}
			return this.GetParametersInternal();
		}

		// Token: 0x0600511C RID: 20764 RVA: 0x000FDA54 File Offset: 0x000FBC54
		internal override ParameterInfo[] GetParametersInternal()
		{
			if (this.parameters == null)
			{
				return null;
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

		// Token: 0x0600511D RID: 20765 RVA: 0x000FDAB2 File Offset: 0x000FBCB2
		internal override int GetParametersCount()
		{
			if (this.parameters == null)
			{
				return 0;
			}
			return this.parameters.Length;
		}

		// Token: 0x0600511E RID: 20766 RVA: 0x000FDAC6 File Offset: 0x000FBCC6
		internal override Type GetParameterType(int pos)
		{
			return this.parameters[pos];
		}

		// Token: 0x0600511F RID: 20767 RVA: 0x000FDAD0 File Offset: 0x000FBCD0
		internal MethodBase RuntimeResolve()
		{
			return this.type.RuntimeResolve().GetMethod(this);
		}

		/// <summary>Returns a reference to the module that contains this method.</summary>
		/// <returns>Returns a reference to the module that contains this method.</returns>
		// Token: 0x06005120 RID: 20768 RVA: 0x000FDAE3 File Offset: 0x000FBCE3
		public Module GetModule()
		{
			return this.type.Module;
		}

		/// <summary>Creates the body of the method using a supplied byte array of Microsoft intermediate language (MSIL) instructions.</summary>
		/// <param name="il">An array containing valid MSIL instructions. If this parameter is <see langword="null" />, the method's body is cleared.</param>
		/// <param name="count">The number of valid bytes in the MSIL array. This value is ignored if MSIL is <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="count" /> is not within the range of indexes of the supplied MSIL instruction array and <paramref name="il" /> is not <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  This method was called previously on this <see langword="MethodBuilder" /> with an <paramref name="il" /> argument that was not <see langword="null" />.  
		///  -or-  
		///  For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06005121 RID: 20769 RVA: 0x000FDAF0 File Offset: 0x000FBCF0
		public void CreateMethodBody(byte[] il, int count)
		{
			if (il != null && (count < 0 || count > il.Length))
			{
				throw new ArgumentOutOfRangeException("Index was out of range.  Must be non-negative and less than the size of the collection.");
			}
			if (this.code != null || this.type.is_created)
			{
				throw new InvalidOperationException("Type definition of the method is complete.");
			}
			if (il == null)
			{
				this.code = null;
				return;
			}
			this.code = new byte[count];
			Array.Copy(il, this.code, count);
		}

		/// <summary>Creates the body of the method by using a specified byte array of Microsoft intermediate language (MSIL) instructions.</summary>
		/// <param name="il">An array that contains valid MSIL instructions.</param>
		/// <param name="maxStack">The maximum stack evaluation depth.</param>
		/// <param name="localSignature">An array of bytes that contain the serialized local variable structure. Specify <see langword="null" /> if the method has no local variables.</param>
		/// <param name="exceptionHandlers">A collection that contains the exception handlers for the method. Specify <see langword="null" /> if the method has no exception handlers.</param>
		/// <param name="tokenFixups">A collection of values that represent offsets in <paramref name="il" />, each of which specifies the beginning of a token that may be modified. Specify <see langword="null" /> if the method has no tokens that have to be modified.</param>
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
		///  This method was called previously on this <see cref="T:System.Reflection.Emit.MethodBuilder" /> object.</exception>
		// Token: 0x06005122 RID: 20770 RVA: 0x000FDB59 File Offset: 0x000FBD59
		public void SetMethodBody(byte[] il, int maxStack, byte[] localSignature, IEnumerable<ExceptionHandler> exceptionHandlers, IEnumerable<int> tokenFixups)
		{
			this.GetILGenerator().Init(il, maxStack, localSignature, exceptionHandlers, tokenFixups);
		}

		/// <summary>Dynamically invokes the method reflected by this instance on the given object, passing along the specified parameters, and under the constraints of the given binder.</summary>
		/// <param name="obj">The object on which to invoke the specified method. If the method is static, this parameter is ignored.</param>
		/// <param name="invokeAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects via reflection. If binder is <see langword="null" />, the default binder is used. For more details, see <see cref="T:System.Reflection.Binder" />.</param>
		/// <param name="parameters">An argument list. This is an array of arguments with the same number, order, and type as the parameters of the method to be invoked. If there are no parameters this should be <see langword="null" />.</param>
		/// <param name="culture">An instance of <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. If this is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. (Note that this is necessary to, for example, convert a <see cref="T:System.String" /> that represents 1000 to a <see cref="T:System.Double" /> value, since 1000 is represented differently by different cultures.)</param>
		/// <returns>Returns an object containing the return value of the invoked method.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" /> on the returned <see cref="T:System.Reflection.MethodInfo" />.</exception>
		// Token: 0x06005123 RID: 20771 RVA: 0x000FD969 File Offset: 0x000FBB69
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw this.NotSupported();
		}

		/// <summary>Checks if the specified custom attribute type is defined.</summary>
		/// <param name="attributeType">The custom attribute type.</param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the custom attributes.</param>
		/// <returns>
		///   <see langword="true" /> if the specified custom attribute type is defined; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> on the returned <see cref="T:System.Reflection.MethodInfo" />.</exception>
		// Token: 0x06005124 RID: 20772 RVA: 0x000FD969 File Offset: 0x000FBB69
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.NotSupported();
		}

		/// <summary>Returns all the custom attributes defined for this method.</summary>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the custom attributes.</param>
		/// <returns>Returns an array of objects representing all the custom attributes of this method.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> on the returned <see cref="T:System.Reflection.MethodInfo" />.</exception>
		// Token: 0x06005125 RID: 20773 RVA: 0x000FDB6D File Offset: 0x000FBD6D
		public override object[] GetCustomAttributes(bool inherit)
		{
			if (this.type.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, inherit);
			}
			throw this.NotSupported();
		}

		/// <summary>Returns the custom attributes identified by the given type.</summary>
		/// <param name="attributeType">The custom attribute type.</param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the custom attributes.</param>
		/// <returns>Returns an array of objects representing the attributes of this method that are of type <paramref name="attributeType" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> on the returned <see cref="T:System.Reflection.MethodInfo" />.</exception>
		// Token: 0x06005126 RID: 20774 RVA: 0x000FDB8A File Offset: 0x000FBD8A
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (this.type.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
			}
			throw this.NotSupported();
		}

		/// <summary>Returns an <see langword="ILGenerator" /> for this method with a default Microsoft intermediate language (MSIL) stream size of 64 bytes.</summary>
		/// <returns>Returns an <see langword="ILGenerator" /> object for this method.</returns>
		/// <exception cref="T:System.InvalidOperationException">The method should not have a body because of its <see cref="T:System.Reflection.MethodAttributes" /> or <see cref="T:System.Reflection.MethodImplAttributes" /> flags, for example because it has the <see cref="F:System.Reflection.MethodAttributes.PinvokeImpl" /> flag.  
		///  -or-  
		///  The method is a generic method, but not a generic method definition. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06005127 RID: 20775 RVA: 0x000FDBA8 File Offset: 0x000FBDA8
		public ILGenerator GetILGenerator()
		{
			return this.GetILGenerator(64);
		}

		/// <summary>Returns an <see langword="ILGenerator" /> for this method with the specified Microsoft intermediate language (MSIL) stream size.</summary>
		/// <param name="size">The size of the MSIL stream, in bytes.</param>
		/// <returns>Returns an <see langword="ILGenerator" /> object for this method.</returns>
		/// <exception cref="T:System.InvalidOperationException">The method should not have a body because of its <see cref="T:System.Reflection.MethodAttributes" /> or <see cref="T:System.Reflection.MethodImplAttributes" /> flags, for example because it has the <see cref="F:System.Reflection.MethodAttributes.PinvokeImpl" /> flag.  
		///  -or-  
		///  The method is a generic method, but not a generic method definition. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06005128 RID: 20776 RVA: 0x000FDBB4 File Offset: 0x000FBDB4
		public ILGenerator GetILGenerator(int size)
		{
			if ((this.iattrs & MethodImplAttributes.CodeTypeMask) != MethodImplAttributes.IL || (this.iattrs & MethodImplAttributes.ManagedMask) != MethodImplAttributes.IL)
			{
				throw new InvalidOperationException("Method body should not exist.");
			}
			if (this.ilgen != null)
			{
				return this.ilgen;
			}
			this.ilgen = new ILGenerator(this.type.Module, ((ModuleBuilder)this.type.Module).GetTokenGenerator(), size);
			return this.ilgen;
		}

		/// <summary>Sets the parameter attributes and the name of a parameter of this method, or of the return value of this method. Returns a ParameterBuilder that can be used to apply custom attributes.</summary>
		/// <param name="position">The position of the parameter in the parameter list. Parameters are indexed beginning with the number 1 for the first parameter; the number 0 represents the return value of the method.</param>
		/// <param name="attributes">The parameter attributes of the parameter.</param>
		/// <param name="strParamName">The name of the parameter. The name can be the null string.</param>
		/// <returns>Returns a <see langword="ParameterBuilder" /> object that represents a parameter of this method or the return value of this method.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The method has no parameters.  
		///  -or-  
		///  <paramref name="position" /> is less than zero.  
		///  -or-  
		///  <paramref name="position" /> is greater than the number of the method's parameters.</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06005129 RID: 20777 RVA: 0x000FDC24 File Offset: 0x000FBE24
		public ParameterBuilder DefineParameter(int position, ParameterAttributes attributes, string strParamName)
		{
			this.RejectIfCreated();
			if (position < 0 || this.parameters == null || position > this.parameters.Length)
			{
				throw new ArgumentOutOfRangeException("position");
			}
			ParameterBuilder parameterBuilder = new ParameterBuilder(this, position, attributes, strParamName);
			if (this.pinfo == null)
			{
				this.pinfo = new ParameterBuilder[this.parameters.Length + 1];
			}
			this.pinfo[position] = parameterBuilder;
			return parameterBuilder;
		}

		// Token: 0x0600512A RID: 20778 RVA: 0x000FDC8C File Offset: 0x000FBE8C
		internal void check_override()
		{
			if (this.override_methods != null)
			{
				foreach (MethodInfo methodInfo in this.override_methods)
				{
					if (methodInfo.IsVirtual && !base.IsVirtual)
					{
						throw new TypeLoadException(string.Format("Method '{0}' override '{1}' but it is not virtual", this.name, methodInfo));
					}
				}
			}
		}

		// Token: 0x0600512B RID: 20779 RVA: 0x000FDCE4 File Offset: 0x000FBEE4
		internal void fixup()
		{
			if ((this.attrs & (MethodAttributes.Abstract | MethodAttributes.PinvokeImpl)) == MethodAttributes.PrivateScope && (this.iattrs & (MethodImplAttributes)4099) == MethodImplAttributes.IL && (this.ilgen == null || this.ilgen.ILOffset == 0) && (this.code == null || this.code.Length == 0))
			{
				throw new InvalidOperationException(string.Format("Method '{0}.{1}' does not have a method body.", this.DeclaringType.FullName, this.Name));
			}
			if (this.ilgen != null)
			{
				this.ilgen.label_fixup(this);
			}
		}

		// Token: 0x0600512C RID: 20780 RVA: 0x000FDD68 File Offset: 0x000FBF68
		internal void ResolveUserTypes()
		{
			this.rtype = TypeBuilder.ResolveUserType(this.rtype);
			TypeBuilder.ResolveUserTypes(this.parameters);
			TypeBuilder.ResolveUserTypes(this.returnModReq);
			TypeBuilder.ResolveUserTypes(this.returnModOpt);
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

		// Token: 0x0600512D RID: 20781 RVA: 0x000FDDF1 File Offset: 0x000FBFF1
		internal void FixupTokens(Dictionary<int, int> token_map, Dictionary<int, MemberInfo> member_map)
		{
			if (this.ilgen != null)
			{
				this.ilgen.FixupTokens(token_map, member_map);
			}
		}

		// Token: 0x0600512E RID: 20782 RVA: 0x000FDE08 File Offset: 0x000FC008
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

		/// <summary>Sets a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to describe the custom attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customBuilder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x0600512F RID: 20783 RVA: 0x000FDE74 File Offset: 0x000FC074
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			string fullName = customBuilder.Ctor.ReflectedType.FullName;
			if (fullName == "System.Runtime.CompilerServices.MethodImplAttribute")
			{
				byte[] data = customBuilder.Data;
				int num = (int)data[2];
				num |= (int)data[3] << 8;
				this.iattrs |= (MethodImplAttributes)num;
				return;
			}
			if (!(fullName == "System.Runtime.InteropServices.DllImportAttribute"))
			{
				if (fullName == "System.Runtime.InteropServices.PreserveSigAttribute")
				{
					this.iattrs |= MethodImplAttributes.PreserveSig;
					return;
				}
				if (fullName == "System.Runtime.CompilerServices.SpecialNameAttribute")
				{
					this.attrs |= MethodAttributes.SpecialName;
					return;
				}
				if (fullName == "System.Security.SuppressUnmanagedCodeSecurityAttribute")
				{
					this.attrs |= MethodAttributes.HasSecurity;
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
				return;
			}
			else
			{
				CustomAttributeBuilder.CustomAttributeInfo customAttributeInfo = CustomAttributeBuilder.decode_cattr(customBuilder);
				bool flag = true;
				this.pi_dll = (string)customAttributeInfo.ctorArgs[0];
				if (this.pi_dll == null || this.pi_dll.Length == 0)
				{
					throw new ArgumentException("DllName cannot be empty");
				}
				this.native_cc = System.Runtime.InteropServices.CallingConvention.Winapi;
				for (int i = 0; i < customAttributeInfo.namedParamNames.Length; i++)
				{
					string a = customAttributeInfo.namedParamNames[i];
					object obj = customAttributeInfo.namedParamValues[i];
					if (a == "CallingConvention")
					{
						this.native_cc = (CallingConvention)obj;
					}
					else if (a == "CharSet")
					{
						this.charset = (CharSet)obj;
					}
					else if (a == "EntryPoint")
					{
						this.pi_entry = (string)obj;
					}
					else if (a == "ExactSpelling")
					{
						this.ExactSpelling = (bool)obj;
					}
					else if (a == "SetLastError")
					{
						this.SetLastError = (bool)obj;
					}
					else if (a == "PreserveSig")
					{
						flag = (bool)obj;
					}
					else if (a == "BestFitMapping")
					{
						this.BestFitMapping = (bool)obj;
					}
					else if (a == "ThrowOnUnmappableChar")
					{
						this.ThrowOnUnmappableChar = (bool)obj;
					}
				}
				this.attrs |= MethodAttributes.PinvokeImpl;
				if (flag)
				{
					this.iattrs |= MethodImplAttributes.PreserveSig;
				}
				return;
			}
		}

		/// <summary>Sets a custom attribute using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute.</param>
		/// <param name="binaryAttribute">A byte blob representing the attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06005130 RID: 20784 RVA: 0x000FE128 File Offset: 0x000FC328
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

		/// <summary>Sets the implementation flags for this method.</summary>
		/// <param name="attributes">The implementation flags to set.</param>
		/// <exception cref="T:System.InvalidOperationException">The containing type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06005131 RID: 20785 RVA: 0x000FE159 File Offset: 0x000FC359
		public void SetImplementationFlags(MethodImplAttributes attributes)
		{
			this.RejectIfCreated();
			this.iattrs = attributes;
		}

		/// <summary>Adds declarative security to this method.</summary>
		/// <param name="action">The security action to be taken (Demand, Assert, and so on).</param>
		/// <param name="pset">The set of permissions the action applies to.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="action" /> is invalid (<see langword="RequestMinimum" />, <see langword="RequestOptional" />, and <see langword="RequestRefuse" /> are invalid).</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  The permission set <paramref name="pset" /> contains an action that was added earlier by <see cref="M:System.Reflection.Emit.MethodBuilder.AddDeclarativeSecurity(System.Security.Permissions.SecurityAction,System.Security.PermissionSet)" />.  
		///  -or-  
		///  For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is <see langword="false" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pset" /> is <see langword="null" />.</exception>
		// Token: 0x06005132 RID: 20786 RVA: 0x000FE168 File Offset: 0x000FC368
		public void AddDeclarativeSecurity(SecurityAction action, PermissionSet pset)
		{
			if (pset == null)
			{
				throw new ArgumentNullException("pset");
			}
			if (action == SecurityAction.RequestMinimum || action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse)
			{
				throw new ArgumentOutOfRangeException("Request* values are not permitted", "action");
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

		/// <summary>Sets marshaling information for the return type of this method.</summary>
		/// <param name="unmanagedMarshal">Marshaling information for the return type of this method.</param>
		/// <exception cref="T:System.InvalidOperationException">The containing type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06005133 RID: 20787 RVA: 0x000FE248 File Offset: 0x000FC448
		[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead.")]
		public void SetMarshal(UnmanagedMarshal unmanagedMarshal)
		{
			this.RejectIfCreated();
			throw new NotImplementedException();
		}

		/// <summary>Set a symbolic custom attribute using a blob.</summary>
		/// <param name="name">The name of the symbolic custom attribute.</param>
		/// <param name="data">The byte blob that represents the value of the symbolic custom attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">The containing type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  The module that contains this method is not a debug module.  
		///  -or-  
		///  For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06005134 RID: 20788 RVA: 0x000FE248 File Offset: 0x000FC448
		[MonoTODO]
		public void SetSymCustomAttribute(string name, byte[] data)
		{
			this.RejectIfCreated();
			throw new NotImplementedException();
		}

		/// <summary>Returns this <see langword="MethodBuilder" /> instance as a string.</summary>
		/// <returns>Returns a string containing the name, attributes, method signature, exceptions, and local signature of this method followed by the current Microsoft intermediate language (MSIL) stream.</returns>
		// Token: 0x06005135 RID: 20789 RVA: 0x000FE255 File Offset: 0x000FC455
		[SecuritySafeCritical]
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"MethodBuilder [",
				this.type.Name,
				"::",
				this.name,
				"]"
			});
		}

		/// <summary>Determines whether the given object is equal to this instance.</summary>
		/// <param name="obj">The object to compare with this <see langword="MethodBuilder" /> instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see langword="MethodBuilder" /> and is equal to this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005136 RID: 20790 RVA: 0x000FE291 File Offset: 0x000FC491
		[MonoTODO]
		[SecuritySafeCritical]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Gets the hash code for this method.</summary>
		/// <returns>The hash code for this method.</returns>
		// Token: 0x06005137 RID: 20791 RVA: 0x000FE29A File Offset: 0x000FC49A
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x06005138 RID: 20792 RVA: 0x000FE2A7 File Offset: 0x000FC4A7
		internal override int get_next_table_index(object obj, int table, int count)
		{
			return this.type.get_next_table_index(obj, table, count);
		}

		// Token: 0x06005139 RID: 20793 RVA: 0x000FE2B8 File Offset: 0x000FC4B8
		private void ExtendArray<T>(ref T[] array, T elem)
		{
			if (array == null)
			{
				array = new T[1];
			}
			else
			{
				T[] array2 = new T[array.Length + 1];
				Array.Copy(array, array2, array.Length);
				array = array2;
			}
			array[array.Length - 1] = elem;
		}

		// Token: 0x0600513A RID: 20794 RVA: 0x000FE2FC File Offset: 0x000FC4FC
		internal void set_override(MethodInfo mdecl)
		{
			this.ExtendArray<MethodInfo>(ref this.override_methods, mdecl);
		}

		// Token: 0x0600513B RID: 20795 RVA: 0x000FE30B File Offset: 0x000FC50B
		private void RejectIfCreated()
		{
			if (this.type.is_created)
			{
				throw new InvalidOperationException("Type definition of the method is complete.");
			}
		}

		// Token: 0x0600513C RID: 20796 RVA: 0x000F79BE File Offset: 0x000F5BBE
		private Exception NotSupported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		/// <summary>Returns a generic method constructed from the current generic method definition using the specified generic type arguments.</summary>
		/// <param name="typeArguments">An array of <see cref="T:System.Type" /> objects that represent the type arguments for the generic method.</param>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> representing the generic method constructed from the current generic method definition using the specified generic type arguments.</returns>
		// Token: 0x0600513D RID: 20797 RVA: 0x000FE328 File Offset: 0x000FC528
		public override MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException("Method is not a generic method definition");
			}
			if (typeArguments == null)
			{
				throw new ArgumentNullException("typeArguments");
			}
			if (this.generic_params.Length != typeArguments.Length)
			{
				throw new ArgumentException("Incorrect length", "typeArguments");
			}
			for (int i = 0; i < typeArguments.Length; i++)
			{
				if (typeArguments[i] == null)
				{
					throw new ArgumentNullException("typeArguments");
				}
			}
			return new MethodOnTypeBuilderInst(this, typeArguments);
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Reflection.Emit.MethodBuilder" /> object represents the definition of a generic method.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Reflection.Emit.MethodBuilder" /> object represents the definition of a generic method; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x0600513E RID: 20798 RVA: 0x000FE3A0 File Offset: 0x000FC5A0
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.generic_params != null;
			}
		}

		/// <summary>Gets a value indicating whether the method is a generic method.</summary>
		/// <returns>
		///   <see langword="true" /> if the method is generic; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x0600513F RID: 20799 RVA: 0x000FE3A0 File Offset: 0x000FC5A0
		public override bool IsGenericMethod
		{
			get
			{
				return this.generic_params != null;
			}
		}

		/// <summary>Returns this method.</summary>
		/// <returns>The current instance of <see cref="T:System.Reflection.Emit.MethodBuilder" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current method is not generic. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property returns <see langword="false" />.</exception>
		// Token: 0x06005140 RID: 20800 RVA: 0x000FE3AB File Offset: 0x000FC5AB
		public override MethodInfo GetGenericMethodDefinition()
		{
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException();
			}
			return this;
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> objects that represent the type parameters of the method, if it is generic.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> objects representing the type parameters, if the method is generic, or <see langword="null" /> if the method is not generic.</returns>
		// Token: 0x06005141 RID: 20801 RVA: 0x000FE3BC File Offset: 0x000FC5BC
		public override Type[] GetGenericArguments()
		{
			if (this.generic_params == null)
			{
				return null;
			}
			Type[] array = new Type[this.generic_params.Length];
			for (int i = 0; i < this.generic_params.Length; i++)
			{
				array[i] = this.generic_params[i];
			}
			return array;
		}

		/// <summary>Sets the number of generic type parameters for the current method, specifies their names, and returns an array of <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> objects that can be used to define their constraints.</summary>
		/// <param name="names">An array of strings that represent the names of the generic type parameters.</param>
		/// <returns>An array of <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> objects representing the type parameters of the generic method.</returns>
		/// <exception cref="T:System.InvalidOperationException">Generic type parameters have already been defined for this method.  
		///  -or-  
		///  The method has been completed already.  
		///  -or-  
		///  The <see cref="M:System.Reflection.Emit.MethodBuilder.SetImplementationFlags(System.Reflection.MethodImplAttributes)" /> method has been called for the current method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="names" /> is <see langword="null" />.  
		/// -or-  
		/// An element of <paramref name="names" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="names" /> is an empty array.</exception>
		// Token: 0x06005142 RID: 20802 RVA: 0x000FE400 File Offset: 0x000FC600
		public GenericTypeParameterBuilder[] DefineGenericParameters(params string[] names)
		{
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			if (names.Length == 0)
			{
				throw new ArgumentException("names");
			}
			this.generic_params = new GenericTypeParameterBuilder[names.Length];
			for (int i = 0; i < names.Length; i++)
			{
				string text = names[i];
				if (text == null)
				{
					throw new ArgumentNullException("names");
				}
				this.generic_params[i] = new GenericTypeParameterBuilder(this.type, this, text, i);
			}
			return this.generic_params;
		}

		/// <summary>Sets the return type of the method.</summary>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that represents the return type of the method.</param>
		/// <exception cref="T:System.InvalidOperationException">The current method is generic, but is not a generic method definition. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06005143 RID: 20803 RVA: 0x000FE474 File Offset: 0x000FC674
		public void SetReturnType(Type returnType)
		{
			this.rtype = returnType;
		}

		/// <summary>Sets the number and types of parameters for a method.</summary>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects representing the parameter types.</param>
		/// <exception cref="T:System.InvalidOperationException">The current method is generic, but is not a generic method definition. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06005144 RID: 20804 RVA: 0x000FE480 File Offset: 0x000FC680
		public void SetParameters(params Type[] parameterTypes)
		{
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
		}

		/// <summary>Sets the method signature, including the return type, the parameter types, and the required and optional custom modifiers of the return type and parameter types.</summary>
		/// <param name="returnType">The return type of the method.</param>
		/// <param name="returnTypeRequiredCustomModifiers">An array of types representing the required custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no required custom modifiers, specify <see langword="null" />.</param>
		/// <param name="returnTypeOptionalCustomModifiers">An array of types representing the optional custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no optional custom modifiers, specify <see langword="null" />.</param>
		/// <param name="parameterTypes">The types of the parameters of the method.</param>
		/// <param name="parameterTypeRequiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have required custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <param name="parameterTypeOptionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have optional custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <exception cref="T:System.InvalidOperationException">The current method is generic, but is not a generic method definition. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x06005145 RID: 20805 RVA: 0x000FE4D6 File Offset: 0x000FC6D6
		public void SetSignature(Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			this.SetReturnType(returnType);
			this.SetParameters(parameterTypes);
			this.returnModReq = returnTypeRequiredCustomModifiers;
			this.returnModOpt = returnTypeOptionalCustomModifiers;
			this.paramModReq = parameterTypeRequiredCustomModifiers;
			this.paramModOpt = parameterTypeOptionalCustomModifiers;
		}

		/// <summary>Gets the module in which the current method is being defined.</summary>
		/// <returns>The <see cref="T:System.Reflection.Module" /> in which the member represented by the current <see cref="T:System.Reflection.MemberInfo" /> is being defined.</returns>
		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06005146 RID: 20806 RVA: 0x000FE505 File Offset: 0x000FC705
		public override Module Module
		{
			get
			{
				return this.GetModule();
			}
		}

		/// <summary>Gets a <see cref="T:System.Reflection.ParameterInfo" /> object that contains information about the return type of the method, such as whether the return type has custom modifiers.</summary>
		/// <returns>A <see cref="T:System.Reflection.ParameterInfo" /> object that contains information about the return type.</returns>
		/// <exception cref="T:System.InvalidOperationException">The declaring type has not been created.</exception>
		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06005147 RID: 20807 RVA: 0x000FE50D File Offset: 0x000FC70D
		public override ParameterInfo ReturnParameter
		{
			get
			{
				return base.ReturnParameter;
			}
		}

		// Token: 0x06005148 RID: 20808 RVA: 0x000173AD File Offset: 0x000155AD
		internal MethodBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040031B7 RID: 12727
		private RuntimeMethodHandle mhandle;

		// Token: 0x040031B8 RID: 12728
		private Type rtype;

		// Token: 0x040031B9 RID: 12729
		internal Type[] parameters;

		// Token: 0x040031BA RID: 12730
		private MethodAttributes attrs;

		// Token: 0x040031BB RID: 12731
		private MethodImplAttributes iattrs;

		// Token: 0x040031BC RID: 12732
		private string name;

		// Token: 0x040031BD RID: 12733
		private int table_idx;

		// Token: 0x040031BE RID: 12734
		private byte[] code;

		// Token: 0x040031BF RID: 12735
		private ILGenerator ilgen;

		// Token: 0x040031C0 RID: 12736
		private TypeBuilder type;

		// Token: 0x040031C1 RID: 12737
		internal ParameterBuilder[] pinfo;

		// Token: 0x040031C2 RID: 12738
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x040031C3 RID: 12739
		private MethodInfo[] override_methods;

		// Token: 0x040031C4 RID: 12740
		private string pi_dll;

		// Token: 0x040031C5 RID: 12741
		private string pi_entry;

		// Token: 0x040031C6 RID: 12742
		private CharSet charset;

		// Token: 0x040031C7 RID: 12743
		private uint extra_flags;

		// Token: 0x040031C8 RID: 12744
		private CallingConvention native_cc;

		// Token: 0x040031C9 RID: 12745
		private CallingConventions call_conv;

		// Token: 0x040031CA RID: 12746
		private bool init_locals;

		// Token: 0x040031CB RID: 12747
		private IntPtr generic_container;

		// Token: 0x040031CC RID: 12748
		internal GenericTypeParameterBuilder[] generic_params;

		// Token: 0x040031CD RID: 12749
		private Type[] returnModReq;

		// Token: 0x040031CE RID: 12750
		private Type[] returnModOpt;

		// Token: 0x040031CF RID: 12751
		private Type[][] paramModReq;

		// Token: 0x040031D0 RID: 12752
		private Type[][] paramModOpt;

		// Token: 0x040031D1 RID: 12753
		private RefEmitPermissionSet[] permissions;
	}
}
