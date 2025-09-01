using System;
using System.Globalization;
using System.Runtime.InteropServices;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Defines and represents a field. This class cannot be inherited.</summary>
	// Token: 0x02000924 RID: 2340
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_FieldBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class FieldBuilder : FieldInfo, _FieldBuilder
	{
		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x0600500F RID: 20495 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _FieldBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06005010 RID: 20496 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _FieldBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06005011 RID: 20497 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _FieldBuilder.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x06005012 RID: 20498 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _FieldBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005013 RID: 20499 RVA: 0x000FAAC8 File Offset: 0x000F8CC8
		internal FieldBuilder(TypeBuilder tb, string fieldName, Type type, FieldAttributes attributes, Type[] modReq, Type[] modOpt)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.attrs = attributes;
			this.name = fieldName;
			this.type = type;
			this.modReq = modReq;
			this.modOpt = modOpt;
			this.offset = -1;
			this.typeb = tb;
			((ModuleBuilder)tb.Module).RegisterToken(this, this.GetToken().Token);
		}

		/// <summary>Indicates the attributes of this field. This property is read-only.</summary>
		/// <returns>The attributes of this field.</returns>
		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06005014 RID: 20500 RVA: 0x000FAB42 File Offset: 0x000F8D42
		public override FieldAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		/// <summary>Indicates a reference to the <see cref="T:System.Type" /> object for the type that declares this field. This property is read-only.</summary>
		/// <returns>A reference to the <see cref="T:System.Type" /> object for the type that declares this field.</returns>
		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06005015 RID: 20501 RVA: 0x000FAB4A File Offset: 0x000F8D4A
		public override Type DeclaringType
		{
			get
			{
				return this.typeb;
			}
		}

		/// <summary>Indicates the internal metadata handle for this field. This property is read-only.</summary>
		/// <returns>The internal metadata handle for this field.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06005016 RID: 20502 RVA: 0x000FAB52 File Offset: 0x000F8D52
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				throw this.CreateNotSupportedException();
			}
		}

		/// <summary>Indicates the <see cref="T:System.Type" /> object that represents the type of this field. This property is read-only.</summary>
		/// <returns>The <see cref="T:System.Type" /> object that represents the type of this field.</returns>
		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06005017 RID: 20503 RVA: 0x000FAB5A File Offset: 0x000F8D5A
		public override Type FieldType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Indicates the name of this field. This property is read-only.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of this field.</returns>
		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06005018 RID: 20504 RVA: 0x000FAB62 File Offset: 0x000F8D62
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Indicates the reference to the <see cref="T:System.Type" /> object from which this object was obtained. This property is read-only.</summary>
		/// <returns>A reference to the <see cref="T:System.Type" /> object from which this instance was obtained.</returns>
		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06005019 RID: 20505 RVA: 0x000FAB4A File Offset: 0x000F8D4A
		public override Type ReflectedType
		{
			get
			{
				return this.typeb;
			}
		}

		/// <summary>Returns all the custom attributes defined for this field.</summary>
		/// <param name="inherit">Controls inheritance of custom attributes from base classes.</param>
		/// <returns>An array of type <see cref="T:System.Object" /> representing all the custom attributes of the constructor represented by this <see cref="T:System.Reflection.Emit.FieldBuilder" /> instance.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x0600501A RID: 20506 RVA: 0x000FAB6A File Offset: 0x000F8D6A
		public override object[] GetCustomAttributes(bool inherit)
		{
			if (this.typeb.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, inherit);
			}
			throw this.CreateNotSupportedException();
		}

		/// <summary>Returns all the custom attributes defined for this field identified by the given type.</summary>
		/// <param name="attributeType">The custom attribute type.</param>
		/// <param name="inherit">Controls inheritance of custom attributes from base classes.</param>
		/// <returns>An array of type <see cref="T:System.Object" /> representing all the custom attributes of the constructor represented by this <see cref="T:System.Reflection.Emit.FieldBuilder" /> instance.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x0600501B RID: 20507 RVA: 0x000FAB87 File Offset: 0x000F8D87
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (this.typeb.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
			}
			throw this.CreateNotSupportedException();
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x0600501C RID: 20508 RVA: 0x000FABA5 File Offset: 0x000F8DA5
		public override int MetadataToken
		{
			get
			{
				return ((ModuleBuilder)this.typeb.Module).GetToken(this);
			}
		}

		/// <summary>Returns the token representing this field.</summary>
		/// <returns>The <see cref="T:System.Reflection.Emit.FieldToken" /> object that represents the token for this field.</returns>
		// Token: 0x0600501D RID: 20509 RVA: 0x000FABBD File Offset: 0x000F8DBD
		public FieldToken GetToken()
		{
			return new FieldToken(this.MetadataToken);
		}

		/// <summary>Retrieves the value of the field supported by the given object.</summary>
		/// <param name="obj">The object on which to access the field.</param>
		/// <returns>An <see cref="T:System.Object" /> containing the value of the field reflected by this instance.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x0600501E RID: 20510 RVA: 0x000FAB52 File Offset: 0x000F8D52
		public override object GetValue(object obj)
		{
			throw this.CreateNotSupportedException();
		}

		/// <summary>Indicates whether an attribute having the specified type is defined on a field.</summary>
		/// <param name="attributeType">The type of the attribute.</param>
		/// <param name="inherit">Controls inheritance of custom attributes from base classes.</param>
		/// <returns>
		///   <see langword="true" /> if one or more instance of <paramref name="attributeType" /> is defined on this field; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the field using <see cref="M:System.Type.GetField(System.String,System.Reflection.BindingFlags)" /> and call <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> on the returned <see cref="T:System.Reflection.FieldInfo" />.</exception>
		// Token: 0x0600501F RID: 20511 RVA: 0x000FAB52 File Offset: 0x000F8D52
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.CreateNotSupportedException();
		}

		// Token: 0x06005020 RID: 20512 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		internal override int GetFieldOffset()
		{
			return 0;
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x000FABCA File Offset: 0x000F8DCA
		internal void SetRVAData(byte[] data)
		{
			this.rva_data = (byte[])data.Clone();
		}

		/// <summary>Sets the default value of this field.</summary>
		/// <param name="defaultValue">The new default value for this field.</param>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		/// <exception cref="T:System.ArgumentException">The field is not one of the supported types.  
		///  -or-  
		///  The type of <paramref name="defaultValue" /> does not match the type of the field.  
		///  -or-  
		///  The field is of type <see cref="T:System.Object" /> or other reference type, <paramref name="defaultValue" /> is not <see langword="null" />, and the value cannot be assigned to the reference type.</exception>
		// Token: 0x06005022 RID: 20514 RVA: 0x000FABDD File Offset: 0x000F8DDD
		public void SetConstant(object defaultValue)
		{
			this.RejectIfCreated();
			this.def_value = defaultValue;
		}

		/// <summary>Sets a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The parent type of this field is complete.</exception>
		// Token: 0x06005023 RID: 20515 RVA: 0x000FABEC File Offset: 0x000F8DEC
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this.RejectIfCreated();
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			string fullName = customBuilder.Ctor.ReflectedType.FullName;
			if (fullName == "System.Runtime.InteropServices.FieldOffsetAttribute")
			{
				byte[] data = customBuilder.Data;
				this.offset = (int)data[2];
				this.offset |= (int)data[3] << 8;
				this.offset |= (int)data[4] << 16;
				this.offset |= (int)data[5] << 24;
				return;
			}
			if (fullName == "System.NonSerializedAttribute")
			{
				this.attrs |= FieldAttributes.NotSerialized;
				return;
			}
			if (fullName == "System.Runtime.CompilerServices.SpecialNameAttribute")
			{
				this.attrs |= FieldAttributes.SpecialName;
				return;
			}
			if (fullName == "System.Runtime.InteropServices.MarshalAsAttribute")
			{
				this.attrs |= FieldAttributes.HasFieldMarshal;
				this.marshal_info = CustomAttributeBuilder.get_umarshal(customBuilder, true);
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

		/// <summary>Sets a custom attribute using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute.</param>
		/// <param name="binaryAttribute">A byte blob representing the attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The parent type of this field is complete.</exception>
		// Token: 0x06005024 RID: 20516 RVA: 0x000FAD2E File Offset: 0x000F8F2E
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.RejectIfCreated();
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		/// <summary>Describes the native marshaling of the field.</summary>
		/// <param name="unmanagedMarshal">A descriptor specifying the native marshalling of this field.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="unmanagedMarshal" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x06005025 RID: 20517 RVA: 0x000FAD43 File Offset: 0x000F8F43
		[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead.")]
		public void SetMarshal(UnmanagedMarshal unmanagedMarshal)
		{
			this.RejectIfCreated();
			this.marshal_info = unmanagedMarshal;
			this.attrs |= FieldAttributes.HasFieldMarshal;
		}

		/// <summary>Specifies the field layout.</summary>
		/// <param name="iOffset">The offset of the field within the type containing this field.</param>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="iOffset" /> is less than zero.</exception>
		// Token: 0x06005026 RID: 20518 RVA: 0x000FAD64 File Offset: 0x000F8F64
		public void SetOffset(int iOffset)
		{
			this.RejectIfCreated();
			if (iOffset < 0)
			{
				throw new ArgumentException("Negative field offset is not allowed");
			}
			this.offset = iOffset;
		}

		/// <summary>Sets the value of the field supported by the given object.</summary>
		/// <param name="obj">The object on which to access the field.</param>
		/// <param name="val">The value to assign to the field.</param>
		/// <param name="invokeAttr">A member of <see langword="IBinder" /> that specifies the type of binding that is desired (for example, IBinder.CreateInstance, IBinder.ExactBinding).</param>
		/// <param name="binder">A set of properties and enabling for binding, coercion of argument types, and invocation of members using reflection. If binder is null, then IBinder.DefaultBinding is used.</param>
		/// <param name="culture">The software preferences of a particular culture.</param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06005027 RID: 20519 RVA: 0x000FAB52 File Offset: 0x000F8D52
		public override void SetValue(object obj, object val, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			throw this.CreateNotSupportedException();
		}

		// Token: 0x06005028 RID: 20520 RVA: 0x000F79BE File Offset: 0x000F5BBE
		private Exception CreateNotSupportedException()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x06005029 RID: 20521 RVA: 0x000FAD82 File Offset: 0x000F8F82
		private void RejectIfCreated()
		{
			if (this.typeb.is_created)
			{
				throw new InvalidOperationException("Unable to change after type has been created.");
			}
		}

		// Token: 0x0600502A RID: 20522 RVA: 0x000FAD9C File Offset: 0x000F8F9C
		internal void ResolveUserTypes()
		{
			this.type = TypeBuilder.ResolveUserType(this.type);
			TypeBuilder.ResolveUserTypes(this.modReq);
			TypeBuilder.ResolveUserTypes(this.modOpt);
			if (this.marshal_info != null)
			{
				this.marshal_info.marshaltyperef = TypeBuilder.ResolveUserType(this.marshal_info.marshaltyperef);
			}
		}

		// Token: 0x0600502B RID: 20523 RVA: 0x000FADF4 File Offset: 0x000F8FF4
		internal FieldInfo RuntimeResolve()
		{
			RuntimeTypeHandle declaringType = new RuntimeTypeHandle(this.typeb.CreateType() as RuntimeType);
			return FieldInfo.GetFieldFromHandle(this.handle, declaringType);
		}

		/// <summary>Gets the module in which the type that contains this field is being defined.</summary>
		/// <returns>A <see cref="T:System.Reflection.Module" /> that represents the dynamic module in which this field is being defined.</returns>
		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x0600502C RID: 20524 RVA: 0x000FAE24 File Offset: 0x000F9024
		public override Module Module
		{
			get
			{
				return base.Module;
			}
		}

		// Token: 0x0600502D RID: 20525 RVA: 0x000173AD File Offset: 0x000155AD
		internal FieldBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400315D RID: 12637
		private FieldAttributes attrs;

		// Token: 0x0400315E RID: 12638
		private Type type;

		// Token: 0x0400315F RID: 12639
		private string name;

		// Token: 0x04003160 RID: 12640
		private object def_value;

		// Token: 0x04003161 RID: 12641
		private int offset;

		// Token: 0x04003162 RID: 12642
		internal TypeBuilder typeb;

		// Token: 0x04003163 RID: 12643
		private byte[] rva_data;

		// Token: 0x04003164 RID: 12644
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04003165 RID: 12645
		private UnmanagedMarshal marshal_info;

		// Token: 0x04003166 RID: 12646
		private RuntimeFieldHandle handle;

		// Token: 0x04003167 RID: 12647
		private Type[] modReq;

		// Token: 0x04003168 RID: 12648
		private Type[] modOpt;
	}
}
