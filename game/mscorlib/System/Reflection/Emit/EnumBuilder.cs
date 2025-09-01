using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Describes and represents an enumeration type.</summary>
	// Token: 0x02000920 RID: 2336
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_EnumBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	public sealed class EnumBuilder : TypeInfo, _EnumBuilder
	{
		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004FA5 RID: 20389 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _EnumBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004FA6 RID: 20390 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _EnumBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004FA7 RID: 20391 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _EnumBuilder.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x06004FA8 RID: 20392 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _EnumBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004FA9 RID: 20393 RVA: 0x000FA2F8 File Offset: 0x000F84F8
		internal EnumBuilder(ModuleBuilder mb, string name, TypeAttributes visibility, Type underlyingType)
		{
			this._tb = new TypeBuilder(mb, name, visibility | TypeAttributes.Sealed, typeof(Enum), null, PackingSize.Unspecified, 0, null);
			this._underlyingType = underlyingType;
			this._underlyingField = this._tb.DefineField("value__", underlyingType, FieldAttributes.Private | FieldAttributes.SpecialName | FieldAttributes.RTSpecialName);
			this.setup_enum_type(this._tb);
		}

		// Token: 0x06004FAA RID: 20394 RVA: 0x000FA35E File Offset: 0x000F855E
		internal TypeBuilder GetTypeBuilder()
		{
			return this._tb;
		}

		// Token: 0x06004FAB RID: 20395 RVA: 0x000FA366 File Offset: 0x000F8566
		internal override Type InternalResolve()
		{
			return this._tb.InternalResolve();
		}

		// Token: 0x06004FAC RID: 20396 RVA: 0x000FA373 File Offset: 0x000F8573
		internal override Type RuntimeResolve()
		{
			return this._tb.RuntimeResolve();
		}

		/// <summary>Retrieves the dynamic assembly that contains this enum definition.</summary>
		/// <returns>Read-only. The dynamic assembly that contains this enum definition.</returns>
		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06004FAD RID: 20397 RVA: 0x000FA380 File Offset: 0x000F8580
		public override Assembly Assembly
		{
			get
			{
				return this._tb.Assembly;
			}
		}

		/// <summary>Returns the full path of this enum qualified by the display name of the parent assembly.</summary>
		/// <returns>Read-only. The full path of this enum qualified by the display name of the parent assembly.</returns>
		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06004FAE RID: 20398 RVA: 0x000FA38D File Offset: 0x000F858D
		public override string AssemblyQualifiedName
		{
			get
			{
				return this._tb.AssemblyQualifiedName;
			}
		}

		/// <summary>Returns the parent <see cref="T:System.Type" /> of this type which is always <see cref="T:System.Enum" />.</summary>
		/// <returns>Read-only. The parent <see cref="T:System.Type" /> of this type.</returns>
		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x06004FAF RID: 20399 RVA: 0x000FA39A File Offset: 0x000F859A
		public override Type BaseType
		{
			get
			{
				return this._tb.BaseType;
			}
		}

		/// <summary>Returns the type that declared this <see cref="T:System.Reflection.Emit.EnumBuilder" />.</summary>
		/// <returns>Read-only. The type that declared this <see cref="T:System.Reflection.Emit.EnumBuilder" />.</returns>
		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x06004FB0 RID: 20400 RVA: 0x000FA3A7 File Offset: 0x000F85A7
		public override Type DeclaringType
		{
			get
			{
				return this._tb.DeclaringType;
			}
		}

		/// <summary>Returns the full path of this enum.</summary>
		/// <returns>Read-only. The full path of this enum.</returns>
		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x06004FB1 RID: 20401 RVA: 0x000FA3B4 File Offset: 0x000F85B4
		public override string FullName
		{
			get
			{
				return this._tb.FullName;
			}
		}

		/// <summary>Returns the GUID of this enum.</summary>
		/// <returns>Read-only. The GUID of this enum.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06004FB2 RID: 20402 RVA: 0x000FA3C1 File Offset: 0x000F85C1
		public override Guid GUID
		{
			get
			{
				return this._tb.GUID;
			}
		}

		/// <summary>Retrieves the dynamic module that contains this <see cref="T:System.Reflection.Emit.EnumBuilder" /> definition.</summary>
		/// <returns>Read-only. The dynamic module that contains this <see cref="T:System.Reflection.Emit.EnumBuilder" /> definition.</returns>
		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06004FB3 RID: 20403 RVA: 0x000FA3CE File Offset: 0x000F85CE
		public override Module Module
		{
			get
			{
				return this._tb.Module;
			}
		}

		/// <summary>Returns the name of this enum.</summary>
		/// <returns>Read-only. The name of this enum.</returns>
		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06004FB4 RID: 20404 RVA: 0x000FA3DB File Offset: 0x000F85DB
		public override string Name
		{
			get
			{
				return this._tb.Name;
			}
		}

		/// <summary>Returns the namespace of this enum.</summary>
		/// <returns>Read-only. The namespace of this enum.</returns>
		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06004FB5 RID: 20405 RVA: 0x000FA3E8 File Offset: 0x000F85E8
		public override string Namespace
		{
			get
			{
				return this._tb.Namespace;
			}
		}

		/// <summary>Returns the type that was used to obtain this <see cref="T:System.Reflection.Emit.EnumBuilder" />.</summary>
		/// <returns>Read-only. The type that was used to obtain this <see cref="T:System.Reflection.Emit.EnumBuilder" />.</returns>
		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06004FB6 RID: 20406 RVA: 0x000FA3F5 File Offset: 0x000F85F5
		public override Type ReflectedType
		{
			get
			{
				return this._tb.ReflectedType;
			}
		}

		/// <summary>Retrieves the internal handle for this enum.</summary>
		/// <returns>Read-only. The internal handle for this enum.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not currently supported.</exception>
		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06004FB7 RID: 20407 RVA: 0x000FA402 File Offset: 0x000F8602
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return this._tb.TypeHandle;
			}
		}

		/// <summary>Returns the internal metadata type token of this enum.</summary>
		/// <returns>Read-only. The type token of this enum.</returns>
		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06004FB8 RID: 20408 RVA: 0x000FA40F File Offset: 0x000F860F
		public TypeToken TypeToken
		{
			get
			{
				return this._tb.TypeToken;
			}
		}

		/// <summary>Returns the underlying field for this enum.</summary>
		/// <returns>Read-only. The underlying field for this enum.</returns>
		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06004FB9 RID: 20409 RVA: 0x000FA41C File Offset: 0x000F861C
		public FieldBuilder UnderlyingField
		{
			get
			{
				return this._underlyingField;
			}
		}

		/// <summary>Returns the underlying system type for this enum.</summary>
		/// <returns>Read-only. Returns the underlying system type.</returns>
		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06004FBA RID: 20410 RVA: 0x000FA424 File Offset: 0x000F8624
		public override Type UnderlyingSystemType
		{
			get
			{
				return this._underlyingType;
			}
		}

		/// <summary>Creates a <see cref="T:System.Type" /> object for this enum.</summary>
		/// <returns>A <see cref="T:System.Type" /> object for this enum.</returns>
		/// <exception cref="T:System.InvalidOperationException">This type has been previously created.  
		///  -or-  
		///  The enclosing type has not been created.</exception>
		// Token: 0x06004FBB RID: 20411 RVA: 0x000FA42C File Offset: 0x000F862C
		public Type CreateType()
		{
			return this._tb.CreateType();
		}

		/// <summary>Gets a <see cref="T:System.Reflection.TypeInfo" /> object that represents this enumeration.</summary>
		/// <returns>An object that represents this enumeration.</returns>
		// Token: 0x06004FBC RID: 20412 RVA: 0x000FA439 File Offset: 0x000F8639
		public TypeInfo CreateTypeInfo()
		{
			return this._tb.CreateTypeInfo();
		}

		/// <summary>Returns the underlying integer type of the current enumeration, which is set when the enumeration builder is defined.</summary>
		/// <returns>The underlying type.</returns>
		// Token: 0x06004FBD RID: 20413 RVA: 0x000FA424 File Offset: 0x000F8624
		public override Type GetEnumUnderlyingType()
		{
			return this._underlyingType;
		}

		// Token: 0x06004FBE RID: 20414
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void setup_enum_type(Type t);

		/// <summary>Defines the named static field in an enumeration type with the specified constant value.</summary>
		/// <param name="literalName">The name of the static field.</param>
		/// <param name="literalValue">The constant value of the literal.</param>
		/// <returns>The defined field.</returns>
		// Token: 0x06004FBF RID: 20415 RVA: 0x000FA448 File Offset: 0x000F8648
		public FieldBuilder DefineLiteral(string literalName, object literalValue)
		{
			FieldBuilder fieldBuilder = this._tb.DefineField(literalName, this, FieldAttributes.FamANDAssem | FieldAttributes.Family | FieldAttributes.Static | FieldAttributes.Literal);
			fieldBuilder.SetConstant(literalValue);
			return fieldBuilder;
		}

		// Token: 0x06004FC0 RID: 20416 RVA: 0x000FA46D File Offset: 0x000F866D
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this._tb.attrs;
		}

		// Token: 0x06004FC1 RID: 20417 RVA: 0x000FA47A File Offset: 0x000F867A
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this._tb.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing the public and non-public constructors defined for this class, as specified.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing the specified constructors defined for this class. If no constructors are defined, an empty array is returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004FC2 RID: 20418 RVA: 0x000FA48E File Offset: 0x000F868E
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this._tb.GetConstructors(bindingAttr);
		}

		/// <summary>Returns all the custom attributes defined for this constructor.</summary>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <returns>Returns an array of objects representing all the custom attributes of the constructor represented by this <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> instance.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004FC3 RID: 20419 RVA: 0x000FA49C File Offset: 0x000F869C
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this._tb.GetCustomAttributes(inherit);
		}

		/// <summary>Returns the custom attributes identified by the given type.</summary>
		/// <param name="attributeType">The <see langword="Type" /> object to which the custom attributes are applied.</param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <returns>Returns an array of objects representing the attributes of this constructor that are of <see cref="T:System.Type" /><paramref name="attributeType" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004FC4 RID: 20420 RVA: 0x000FA4AA File Offset: 0x000F86AA
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this._tb.GetCustomAttributes(attributeType, inherit);
		}

		/// <summary>Calling this method always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>This method is not supported. No value is returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported.</exception>
		// Token: 0x06004FC5 RID: 20421 RVA: 0x000FA4B9 File Offset: 0x000F86B9
		public override Type GetElementType()
		{
			return this._tb.GetElementType();
		}

		/// <summary>Returns the event with the specified name.</summary>
		/// <param name="name">The name of the event to get.</param>
		/// <param name="bindingAttr">This invocation attribute. This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an <see cref="T:System.Reflection.EventInfo" /> object representing the event declared or inherited by this type with the specified name. If there are no matches, <see langword="null" /> is returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004FC6 RID: 20422 RVA: 0x000FA4C6 File Offset: 0x000F86C6
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			return this._tb.GetEvent(name, bindingAttr);
		}

		/// <summary>Returns the events for the public events declared or inherited by this type.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.EventInfo" /> objects representing the public events declared or inherited by this type. An empty array is returned if there are no public events.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004FC7 RID: 20423 RVA: 0x000FA4D5 File Offset: 0x000F86D5
		public override EventInfo[] GetEvents()
		{
			return this._tb.GetEvents();
		}

		/// <summary>Returns the public and non-public events that are declared by this type.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.EventInfo" /> objects representing the public and non-public events declared or inherited by this type. An empty array is returned if there are no events, as specified.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004FC8 RID: 20424 RVA: 0x000FA4E2 File Offset: 0x000F86E2
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this._tb.GetEvents(bindingAttr);
		}

		/// <summary>Returns the field specified by the given name.</summary>
		/// <param name="name">The name of the field to get.</param>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns the <see cref="T:System.Reflection.FieldInfo" /> object representing the field declared or inherited by this type with the specified name and public or non-public modifier. If there are no matches, then null is returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004FC9 RID: 20425 RVA: 0x000FA4F0 File Offset: 0x000F86F0
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this._tb.GetField(name, bindingAttr);
		}

		/// <summary>Returns the public and non-public fields that are declared by this type.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as InvokeMethod, NonPublic, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.FieldInfo" /> objects representing the public and non-public fields declared or inherited by this type. An empty array is returned if there are no fields, as specified.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004FCA RID: 20426 RVA: 0x000FA4FF File Offset: 0x000F86FF
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this._tb.GetFields(bindingAttr);
		}

		/// <summary>Returns the interface implemented (directly or indirectly) by this type, with the specified fully-qualified name.</summary>
		/// <param name="name">The name of the interface.</param>
		/// <param name="ignoreCase">If <see langword="true" />, the search is case-insensitive. If <see langword="false" />, the search is case-sensitive.</param>
		/// <returns>Returns a <see cref="T:System.Type" /> object representing the implemented interface. Returns null if no interface matching name is found.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004FCB RID: 20427 RVA: 0x000FA50D File Offset: 0x000F870D
		public override Type GetInterface(string name, bool ignoreCase)
		{
			return this._tb.GetInterface(name, ignoreCase);
		}

		/// <summary>Returns an interface mapping for the interface requested.</summary>
		/// <param name="interfaceType">The type of the interface for which the interface mapping is to be retrieved.</param>
		/// <returns>The requested interface mapping.</returns>
		/// <exception cref="T:System.ArgumentException">The type does not implement the interface.</exception>
		// Token: 0x06004FCC RID: 20428 RVA: 0x000FA51C File Offset: 0x000F871C
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			return this._tb.GetInterfaceMap(interfaceType);
		}

		/// <summary>Returns an array of all the interfaces implemented on this a class and its base classes.</summary>
		/// <returns>Returns an array of <see cref="T:System.Type" /> objects representing the implemented interfaces. If none are defined, an empty array is returned.</returns>
		// Token: 0x06004FCD RID: 20429 RVA: 0x000FA52A File Offset: 0x000F872A
		public override Type[] GetInterfaces()
		{
			return this._tb.GetInterfaces();
		}

		/// <summary>Returns all members with the specified name, type, and binding that are declared or inherited by this type.</summary>
		/// <param name="name">The name of the member.</param>
		/// <param name="type">The type of member that is to be returned.</param>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public and non-public members defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public members are returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004FCE RID: 20430 RVA: 0x000FA537 File Offset: 0x000F8737
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			return this._tb.GetMember(name, type, bindingAttr);
		}

		/// <summary>Returns the specified members declared or inherited by this type,.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public and non-public members declared or inherited by this type. An empty array is returned if there are no matching members.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004FCF RID: 20431 RVA: 0x000FA547 File Offset: 0x000F8747
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			return this._tb.GetMembers(bindingAttr);
		}

		// Token: 0x06004FD0 RID: 20432 RVA: 0x000FA555 File Offset: 0x000F8755
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				return this._tb.GetMethod(name, bindingAttr);
			}
			return this._tb.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns all the public and non-public methods declared or inherited by this type, as specified.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MethodInfo" /> objects representing the public and non-public methods defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public methods are returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004FD1 RID: 20433 RVA: 0x000FA57D File Offset: 0x000F877D
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this._tb.GetMethods(bindingAttr);
		}

		/// <summary>Returns the specified nested type that is declared by this type.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the nested type to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to conduct a case-sensitive search for public methods.</param>
		/// <returns>A <see cref="T:System.Type" /> object representing the nested type that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004FD2 RID: 20434 RVA: 0x000FA58B File Offset: 0x000F878B
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			return this._tb.GetNestedType(name, bindingAttr);
		}

		/// <summary>Returns the public and non-public nested types that are declared or inherited by this type.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing all the types nested within the current <see cref="T:System.Type" /> that match the specified binding constraints.  
		///  An empty array of type <see cref="T:System.Type" />, if no types are nested within the current <see cref="T:System.Type" />, or if none of the nested types match the binding constraints.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004FD3 RID: 20435 RVA: 0x000FA59A File Offset: 0x000F879A
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			return this._tb.GetNestedTypes(bindingAttr);
		}

		/// <summary>Returns all the public and non-public properties declared or inherited by this type, as specified.</summary>
		/// <param name="bindingAttr">This invocation attribute. This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.PropertyInfo" /> objects representing the public and non-public properties defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public properties are returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004FD4 RID: 20436 RVA: 0x000FA5A8 File Offset: 0x000F87A8
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this._tb.GetProperties(bindingAttr);
		}

		// Token: 0x06004FD5 RID: 20437 RVA: 0x000FA5B6 File Offset: 0x000F87B6
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.CreateNotSupportedException();
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x000FA5BE File Offset: 0x000F87BE
		protected override bool HasElementTypeImpl()
		{
			return this._tb.HasElementType;
		}

		/// <summary>Invokes the specified member. The method that is to be invoked must be accessible and provide the most specific match with the specified argument list, under the contraints of the specified binder and invocation attributes.</summary>
		/// <param name="name">The name of the member to invoke. This can be a constructor, method, property, or field. A suitable invocation attribute must be specified. Note that it is possible to invoke the default member of a class by passing an empty string as the name of the member.</param>
		/// <param name="invokeAttr">The invocation attribute. This must be a bit flag from <see langword="BindingFlags" />.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects using reflection. If binder is <see langword="null" />, the default binder is used. See <see cref="T:System.Reflection.Binder" />.</param>
		/// <param name="target">The object on which to invoke the specified member. If the member is static, this parameter is ignored.</param>
		/// <param name="args">An argument list. This is an array of objects that contains the number, order, and type of the parameters of the member to be invoked. If there are no parameters this should be null.</param>
		/// <param name="modifiers">An array of the same length as <paramref name="args" /> with elements that represent the attributes associated with the arguments of the member to be invoked. A parameter has attributes associated with it in the metadata. They are used by various interoperability services. See the metadata specs for details such as this.</param>
		/// <param name="culture">An instance of <see langword="CultureInfo" /> used to govern the coercion of types. If this is null, the <see langword="CultureInfo" /> for the current thread is used. (Note that this is necessary to, for example, convert a string that represents 1000 to a double value, since 1000 is represented differently by different cultures.)</param>
		/// <param name="namedParameters">Each parameter in the <paramref name="namedParameters" /> array gets the value in the corresponding element in the <paramref name="args" /> array. If the length of <paramref name="args" /> is greater than the length of <paramref name="namedParameters" />, the remaining argument values are passed in order.</param>
		/// <returns>Returns the return value of the invoked member.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004FD7 RID: 20439 RVA: 0x000FA5CC File Offset: 0x000F87CC
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			return this._tb.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06004FDC RID: 20444 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06004FDD RID: 20445 RVA: 0x000040F7 File Offset: 0x000022F7
		protected override bool IsValueTypeImpl()
		{
			return true;
		}

		/// <summary>Checks if the specified custom attribute type is defined.</summary>
		/// <param name="attributeType">The <see langword="Type" /> object to which the custom attributes are applied.</param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <returns>
		///   <see langword="true" /> if one or more instance of <paramref name="attributeType" /> is defined on this member; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete.</exception>
		// Token: 0x06004FDE RID: 20446 RVA: 0x000FA5F1 File Offset: 0x000F87F1
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this._tb.IsDefined(attributeType, inherit);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object representing a one-dimensional array of the current type, with a lower bound of zero.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing a one-dimensional array of the current type, with a lower bound of zero.</returns>
		// Token: 0x06004FDF RID: 20447 RVA: 0x000F6525 File Offset: 0x000F4725
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object representing an array of the current type, with the specified number of dimensions.</summary>
		/// <param name="rank">The number of dimensions for the array. This number must be less than or equal to 32.</param>
		/// <returns>An object representing an array of the current type, with the specified number of dimensions.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="rank" /> is less than 1.</exception>
		// Token: 0x06004FE0 RID: 20448 RVA: 0x000F652E File Offset: 0x000F472E
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents the current type when passed as a ref parameter (ByRef parameter in Visual Basic).</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the current type when passed as a ref parameter (ByRef parameter in Visual Basic).</returns>
		// Token: 0x06004FE1 RID: 20449 RVA: 0x000F6541 File Offset: 0x000F4741
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a pointer to the current type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents a pointer to the current type.</returns>
		// Token: 0x06004FE2 RID: 20450 RVA: 0x000F6549 File Offset: 0x000F4749
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		/// <summary>Sets a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is <see langword="null" />.</exception>
		// Token: 0x06004FE3 RID: 20451 RVA: 0x000FA600 File Offset: 0x000F8800
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this._tb.SetCustomAttribute(customBuilder);
		}

		/// <summary>Sets a custom attribute using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute.</param>
		/// <param name="binaryAttribute">A byte blob representing the attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is <see langword="null" />.</exception>
		// Token: 0x06004FE4 RID: 20452 RVA: 0x000FA60E File Offset: 0x000F880E
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x06004FE5 RID: 20453 RVA: 0x000F79BE File Offset: 0x000F5BBE
		private Exception CreateNotSupportedException()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06004FE6 RID: 20454 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		internal override bool IsUserType
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether this object represents a constructed generic type.</summary>
		/// <returns>
		///   <see langword="true" /> if this object represents a constructed generic type; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06004FE7 RID: 20455 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether a specified <see cref="T:System.Reflection.TypeInfo" /> object can be assigned to this object.</summary>
		/// <param name="typeInfo">The object to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="typeInfo" /> can be assigned to this object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004FE8 RID: 20456 RVA: 0x000FA61D File Offset: 0x000F881D
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return base.IsAssignableFrom(typeInfo);
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06004FE9 RID: 20457 RVA: 0x000040F7 File Offset: 0x000022F7
		public override bool IsTypeDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004FEA RID: 20458 RVA: 0x000173AD File Offset: 0x000155AD
		internal EnumBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400314B RID: 12619
		private TypeBuilder _tb;

		// Token: 0x0400314C RID: 12620
		private FieldBuilder _underlyingField;

		// Token: 0x0400314D RID: 12621
		private Type _underlyingType;
	}
}
