using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Exposes the public members of the <see cref="T:System.Type" /> class to the unmanaged code.</summary>
	// Token: 0x0200077F RID: 1919
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("BCA8B44D-AAD6-3A86-8AB7-03349F4F2DA2")]
	[TypeLibImportClass(typeof(Type))]
	public interface _Type
	{
		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		// Token: 0x06004406 RID: 17414
		void GetTypeInfoCount(out uint pcTInfo);

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		// Token: 0x06004407 RID: 17415
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array that receives the IDs corresponding to the names.</param>
		// Token: 0x06004408 RID: 17416
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		// Token: 0x06004409 RID: 17417
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.ToString" /> method.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the name of the current <see cref="T:System.Type" />.</returns>
		// Token: 0x0600440A RID: 17418
		string ToString();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.Equals(System.Object)" /> method.</summary>
		/// <param name="other">The <see cref="T:System.Object" /> whose underlying system type is to be compared with the underlying system type of the current <see cref="T:System.Type" />.</param>
		/// <returns>
		///   <see langword="true" /> if the underlying system type of <paramref name="o" /> is the same as the underlying system type of the current <see cref="T:System.Type" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600440B RID: 17419
		bool Equals(object other);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetHashCode" /> method.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the hash code for this instance.</returns>
		// Token: 0x0600440C RID: 17420
		int GetHashCode();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetType" /> method.</summary>
		/// <returns>The current <see cref="T:System.Type" />.</returns>
		// Token: 0x0600440D RID: 17421
		Type GetType();

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.MemberType" /> property.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a type or a nested type.</returns>
		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x0600440E RID: 17422
		MemberTypes MemberType { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MemberInfo.Name" /> property.</summary>
		/// <returns>The name of the <see cref="T:System.Type" />.</returns>
		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x0600440F RID: 17423
		string Name { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.DeclaringType" /> property.</summary>
		/// <returns>The <see cref="T:System.Type" /> object for the class that declares this member. If the type is a nested type, this property returns the enclosing type.</returns>
		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06004410 RID: 17424
		Type DeclaringType { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.ReflectedType" /> property.</summary>
		/// <returns>The <see cref="T:System.Type" /> object through which this <see cref="T:System.Reflection.MemberInfo" /> object was obtained.</returns>
		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06004411 RID: 17425
		Type ReflectedType { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Type,System.Boolean)" /> method.</summary>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned.</param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <returns>An array of custom attributes applied to this member, or an array with zero (0) elements if no attributes have been applied.</returns>
		// Token: 0x06004412 RID: 17426
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetCustomAttributes(System.Boolean)" /> method.</summary>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <returns>An array of custom attributes applied to this member, or an array with zero (0) elements if no attributes have been applied.</returns>
		// Token: 0x06004413 RID: 17427
		object[] GetCustomAttributes(bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> method.</summary>
		/// <param name="attributeType">The <see langword="Type" /> object to which the custom attributes are applied.</param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <returns>
		///   <see langword="true" /> if one or more instance of <paramref name="attributeType" /> is applied to this member; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004414 RID: 17428
		bool IsDefined(Type attributeType, bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.GUID" /> property.</summary>
		/// <returns>The GUID associated with the <see cref="T:System.Type" />.</returns>
		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06004415 RID: 17429
		Guid GUID { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.Module" /> property.</summary>
		/// <returns>The name of the module in which the current <see cref="T:System.Type" /> is defined.</returns>
		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06004416 RID: 17430
		Module Module { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.Assembly" /> property.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> instance that describes the assembly containing the current type.</returns>
		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06004417 RID: 17431
		Assembly Assembly { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.TypeHandle" /> property.</summary>
		/// <returns>The handle for the current <see cref="T:System.Type" />.</returns>
		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06004418 RID: 17432
		RuntimeTypeHandle TypeHandle { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.FullName" /> property.</summary>
		/// <returns>A string containing the fully qualified name of the <see cref="T:System.Type" />, including the namespace of the <see cref="T:System.Type" /> but not the assembly.</returns>
		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06004419 RID: 17433
		string FullName { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.Namespace" /> property.</summary>
		/// <returns>The namespace of the <see cref="T:System.Type" />.</returns>
		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x0600441A RID: 17434
		string Namespace { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.AssemblyQualifiedName" /> property.</summary>
		/// <returns>The assembly-qualified name of the <see cref="T:System.Type" />, including the name of the assembly from which the <see cref="T:System.Type" /> was loaded.</returns>
		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x0600441B RID: 17435
		string AssemblyQualifiedName { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetArrayRank" /> method.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the number of dimensions in the current <see cref="T:System.Type" />.</returns>
		// Token: 0x0600441C RID: 17436
		int GetArrayRank();

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.BaseType" /> property.</summary>
		/// <returns>The <see cref="T:System.Type" /> from which the current <see cref="T:System.Type" /> directly inherits, or <see langword="null" /> if the current <see langword="Type" /> represents the <see cref="T:System.Object" /> class.</returns>
		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x0600441D RID: 17437
		Type BaseType { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetConstructors(System.Reflection.BindingFlags)" /> method.</summary>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>An array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing all constructors defined for the current <see cref="T:System.Type" /> that match the specified binding constraints, including the type initializer if it is defined. Returns an empty array of type <see cref="T:System.Reflection.ConstructorInfo" /> if no constructors are defined for the current <see cref="T:System.Type" />, if none of the defined constructors match the binding constraints, or if the current <see cref="T:System.Type" /> represents a type parameter of a generic type or method definition.</returns>
		// Token: 0x0600441E RID: 17438
		ConstructorInfo[] GetConstructors(BindingFlags bindingAttr);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetInterface(System.String,System.Boolean)" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the interface to get. For generic interfaces, this is the mangled name.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to perform a case-insensitive search for <paramref name="name" />.  
		/// -or-  
		/// <see langword="false" /> to perform a case-sensitive search for <paramref name="name" />.</param>
		/// <returns>A <see cref="T:System.Type" /> object representing the interface with the specified name, implemented or inherited by the current <see cref="T:System.Type" />, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x0600441F RID: 17439
		Type GetInterface(string name, bool ignoreCase);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetInterfaces" /> method.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing all the interfaces implemented or inherited by the current <see cref="T:System.Type" />.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Type" />, if no interfaces are implemented or inherited by the current <see cref="T:System.Type" />.</returns>
		// Token: 0x06004420 RID: 17440
		Type[] GetInterfaces();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.FindInterfaces(System.Reflection.TypeFilter,System.Object)" /> method.</summary>
		/// <param name="filter">The <see cref="T:System.Reflection.TypeFilter" /> delegate that compares the interfaces against <paramref name="filterCriteria" />.</param>
		/// <param name="filterCriteria">The search criteria that determines whether an interface should be included in the returned array.</param>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing a filtered list of the interfaces implemented or inherited by the current <see cref="T:System.Type" />.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Type" />, if no interfaces matching the filter are implemented or inherited by the current <see cref="T:System.Type" />.</returns>
		// Token: 0x06004421 RID: 17441
		Type[] FindInterfaces(TypeFilter filter, object filterCriteria);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetEvent(System.String,System.Reflection.BindingFlags)" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of an event that is declared or inherited by the current <see cref="T:System.Type" />.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>The <see cref="T:System.Reflection.EventInfo" /> object representing the specified event that is declared or inherited by the current <see cref="T:System.Type" />, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x06004422 RID: 17442
		EventInfo GetEvent(string name, BindingFlags bindingAttr);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetEvents" /> method.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.EventInfo" /> objects representing all the public events that are declared or inherited by the current <see cref="T:System.Type" />.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.EventInfo" />, if the current <see cref="T:System.Type" /> does not have public events.</returns>
		// Token: 0x06004423 RID: 17443
		EventInfo[] GetEvents();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetEvents(System.Reflection.BindingFlags)" /> method.</summary>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>An array of <see cref="T:System.Reflection.EventInfo" /> objects representing all events that are declared or inherited by the current <see cref="T:System.Type" /> that match the specified binding constraints.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.EventInfo" />, if the current <see cref="T:System.Type" /> does not have events, or if none of the events match the binding constraints.</returns>
		// Token: 0x06004424 RID: 17444
		EventInfo[] GetEvents(BindingFlags bindingAttr);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetNestedTypes(System.Reflection.BindingFlags)" /> method, and searches for the types nested within the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing all the types nested within the current <see cref="T:System.Type" /> that match the specified binding constraints.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Type" />, if no types are nested within the current <see cref="T:System.Type" />, or if none of the nested types match the binding constraints.</returns>
		// Token: 0x06004425 RID: 17445
		Type[] GetNestedTypes(BindingFlags bindingAttr);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetNestedType(System.String,System.Reflection.BindingFlags)" /> method.</summary>
		/// <param name="name">The string containing the name of the nested type to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>A <see cref="T:System.Type" /> object representing the nested type that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x06004426 RID: 17446
		Type GetNestedType(string name, BindingFlags bindingAttr);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetMember(System.String,System.Reflection.MemberTypes,System.Reflection.BindingFlags)" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the members to get.</param>
		/// <param name="type">The <see cref="T:System.Reflection.MemberTypes" /> value to search for.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return an empty array.</param>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public members with the specified name, if found; otherwise, an empty array.</returns>
		// Token: 0x06004427 RID: 17447
		MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetDefaultMembers" /> method.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing all default members of the current <see cref="T:System.Type" />.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.MemberInfo" />, if the current <see cref="T:System.Type" /> does not have default members.</returns>
		// Token: 0x06004428 RID: 17448
		MemberInfo[] GetDefaultMembers();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.FindMembers(System.Reflection.MemberTypes,System.Reflection.BindingFlags,System.Reflection.MemberFilter,System.Object)" /> method.</summary>
		/// <param name="memberType">A <see langword="MemberTypes" /> object indicating the type of member to search for.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <param name="filter">The delegate that does the comparisons, returning <see langword="true" /> if the member currently being inspected matches the <paramref name="filterCriteria" /> and <see langword="false" /> otherwise. You can use the <see langword="FilterAttribute" />, <see langword="FilterName" />, and <see langword="FilterNameIgnoreCase" /> delegates supplied by this class. The first uses the fields of <see langword="FieldAttributes" />, <see langword="MethodAttributes" />, and <see langword="MethodImplAttributes" /> as search criteria, and the other two delegates use <see langword="String" /> objects as the search criteria.</param>
		/// <param name="filterCriteria">The search criteria that determines whether a member is returned in the array of <see langword="MemberInfo" /> objects.  
		///  The fields of <see langword="FieldAttributes" />, <see langword="MethodAttributes" />, and <see langword="MethodImplAttributes" /> can be used in conjunction with the <see langword="FilterAttribute" /> delegate supplied by this class.</param>
		/// <returns>A filtered array of <see cref="T:System.Reflection.MemberInfo" /> objects of the specified member type.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.MemberInfo" />, if the current <see cref="T:System.Type" /> does not have members of type <paramref name="memberType" /> that match the filter criteria.</returns>
		// Token: 0x06004429 RID: 17449
		MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetElementType" /> method.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object encompassed or referred to by the current array, pointer or reference type.  
		///  -or-  
		///  <see langword="null" /> if the current <see cref="T:System.Type" /> is not an array or a pointer, or is not passed by reference, or represents a generic type or a type parameter of a generic type or method definition.</returns>
		// Token: 0x0600442A RID: 17450
		Type GetElementType();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.IsSubclassOf(System.Type)" /> method.</summary>
		/// <param name="c">The <see cref="T:System.Type" /> to compare with the current <see cref="T:System.Type" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> represented by the <paramref name="c" /> parameter and the current <see cref="T:System.Type" /> represent classes, and the class represented by the current <see cref="T:System.Type" /> derives from the class represented by <paramref name="c" />; otherwise, <see langword="false" />. This method also returns <see langword="false" /> if <paramref name="c" /> and the current <see cref="T:System.Type" /> represent the same class.</returns>
		// Token: 0x0600442B RID: 17451
		bool IsSubclassOf(Type c);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.IsInstanceOfType(System.Object)" /> method.</summary>
		/// <param name="o">The object to compare with the current <see cref="T:System.Type" />.</param>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Type" /> is in the inheritance hierarchy of the object represented by <paramref name="o" />, or if the current <see cref="T:System.Type" /> is an interface that <paramref name="o" /> supports. <see langword="false" /> if neither of these conditions is the case, or if <paramref name="o" /> is <see langword="null" />, or if the current <see cref="T:System.Type" /> is an open generic type (that is, <see cref="P:System.Type.ContainsGenericParameters" /> returns <see langword="true" />).</returns>
		// Token: 0x0600442C RID: 17452
		bool IsInstanceOfType(object o);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.IsAssignableFrom(System.Type)" /> method.</summary>
		/// <param name="c">The <see cref="T:System.Type" /> to compare with the current <see cref="T:System.Type" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> and the current <see cref="T:System.Type" /> represent the same type, or if the current <see cref="T:System.Type" /> is in the inheritance hierarchy of <paramref name="c" />, or if the current <see cref="T:System.Type" /> is an interface that <paramref name="c" /> implements, or if <paramref name="c" /> is a generic type parameter and the current <see cref="T:System.Type" /> represents one of the constraints of <paramref name="c" />. <see langword="false" /> if none of these conditions are the case, or if <paramref name="c" /> is <see langword="null" />.</returns>
		// Token: 0x0600442D RID: 17453
		bool IsAssignableFrom(Type c);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetInterfaceMap(System.Type)" /> method.</summary>
		/// <param name="interfaceType">The <see cref="T:System.Type" /> of the interface of which to retrieve a mapping.</param>
		/// <returns>An <see cref="T:System.Reflection.InterfaceMapping" /> object representing the interface mapping for <paramref name="interfaceType" />.</returns>
		// Token: 0x0600442E RID: 17454
		InterfaceMapping GetInterfaceMap(Type interfaceType);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Type[],System.Reflection.ParameterModifier[])" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the method to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.  
		///  -or-  
		///  <see langword="null" />, to use the <see cref="P:System.Type.DefaultBinder" />.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.  
		///  -or-  
		///  An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a method that takes no parameters.</param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter.</param>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x0600442F RID: 17455
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags)" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the method to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x06004430 RID: 17456
		MethodInfo GetMethod(string name, BindingFlags bindingAttr);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetMethods(System.Reflection.BindingFlags)" /> method.</summary>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>An array of <see cref="T:System.Reflection.MethodInfo" /> objects representing all methods defined for the current <see cref="T:System.Type" /> that match the specified binding constraints.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.MethodInfo" />, if no methods are defined for the current <see cref="T:System.Type" />, or if none of the defined methods match the binding constraints.</returns>
		// Token: 0x06004431 RID: 17457
		MethodInfo[] GetMethods(BindingFlags bindingAttr);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetField(System.String,System.Reflection.BindingFlags)" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the data field to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object representing the field that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x06004432 RID: 17458
		FieldInfo GetField(string name, BindingFlags bindingAttr);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetFields(System.Reflection.BindingFlags)" /> method.</summary>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>An array of <see cref="T:System.Reflection.FieldInfo" /> objects representing all fields defined for the current <see cref="T:System.Type" /> that match the specified binding constraints.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.FieldInfo" />, if no fields are defined for the current <see cref="T:System.Type" />, or if none of the defined fields match the binding constraints.</returns>
		// Token: 0x06004433 RID: 17459
		FieldInfo[] GetFields(BindingFlags bindingAttr);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetProperty(System.String,System.Reflection.BindingFlags)" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the property to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object representing the property that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x06004434 RID: 17460
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetProperty(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Type,System.Type[],System.Reflection.ParameterModifier[])" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the property to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.  
		///  -or-  
		///  <see langword="null" />, to use the <see cref="P:System.Type.DefaultBinder" />.</param>
		/// <param name="returnType">The return type of the property.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.  
		///  -or-  
		///  An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed.</param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter.</param>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object representing the property that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x06004435 RID: 17461
		PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetProperties(System.Reflection.BindingFlags)" /> method.</summary>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>An array of <see cref="T:System.Reflection.PropertyInfo" /> objects representing all properties of the current <see cref="T:System.Type" /> that match the specified binding constraints.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.PropertyInfo" />, if the current <see cref="T:System.Type" /> does not have properties, or if none of the properties match the binding constraints.</returns>
		// Token: 0x06004436 RID: 17462
		PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetMember(System.String,System.Reflection.BindingFlags)" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the members to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return an empty array.</param>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public members with the specified name, if found; otherwise, an empty array.</returns>
		// Token: 0x06004437 RID: 17463
		MemberInfo[] GetMember(string name, BindingFlags bindingAttr);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetMembers(System.Reflection.BindingFlags)" /> method.</summary>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing all members defined for the current <see cref="T:System.Type" /> that match the specified binding constraints.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.MemberInfo" />, if no members are defined for the current <see cref="T:System.Type" />, or if none of the defined members match the binding constraints.</returns>
		// Token: 0x06004438 RID: 17464
		MemberInfo[] GetMembers(BindingFlags bindingAttr);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the constructor, method, property, or field member to invoke.  
		///  -or-  
		///  An empty string ("") to invoke the default member.  
		///  -or-  
		///  For IDispatch members, a string representing the DispID, for example "[DispID=3]".</param>
		/// <param name="invokeAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted. The access can be one of the <see langword="BindingFlags" /> such as <see langword="Public" />, <see langword="NonPublic" />, <see langword="Private" />, <see langword="InvokeMethod" />, <see langword="GetField" />, and so on. The type of lookup need not be specified. If the type of lookup is omitted, <see langword="BindingFlags.Public" /> | <see langword="BindingFlags.Instance" /> will apply.</param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.  
		///  -or-  
		///  <see langword="null" />, to use the <see cref="P:System.Type.DefaultBinder" />.</param>
		/// <param name="target">The <see cref="T:System.Object" /> on which to invoke the specified member.</param>
		/// <param name="args">An array containing the arguments to pass to the member to invoke.</param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="args" /> array. A parameter's associated attributes are stored in the member's signature. The default binder does not process this parameter.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> object representing the globalization locale to use, which may be necessary for locale-specific conversions, such as converting a numeric String to a Double.  
		///  -or-  
		///  <see langword="null" /> to use the current thread's <see cref="T:System.Globalization.CultureInfo" />.</param>
		/// <param name="namedParameters">An array containing the names of the parameters to which the values in the <paramref name="args" /> array are passed.</param>
		/// <returns>An <see cref="T:System.Object" /> representing the return value of the invoked member.</returns>
		// Token: 0x06004439 RID: 17465
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.UnderlyingSystemType" /> property.</summary>
		/// <returns>The underlying system type for the <see cref="T:System.Type" />.</returns>
		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x0600443A RID: 17466
		Type UnderlyingSystemType { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Globalization.CultureInfo)" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the constructor, method, property, or field member to invoke.  
		///  -or-  
		///  An empty string ("") to invoke the default member.  
		///  -or-  
		///  For IDispatch members, a string representing the DispID, for example "[DispID=3]".</param>
		/// <param name="invokeAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted. The access can be one of the <see langword="BindingFlags" /> such as <see langword="Public" />, <see langword="NonPublic" />, <see langword="Private" />, <see langword="InvokeMethod" />, <see langword="GetField" />, and so on. The type of lookup need not be specified. If the type of lookup is omitted, <see langword="BindingFlags.Public" /> | <see langword="BindingFlags.Instance" /> will apply.</param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.  
		///  -or-  
		///  <see langword="null" />, to use the <see cref="P:System.Type.DefaultBinder" />.</param>
		/// <param name="target">The <see cref="T:System.Object" /> on which to invoke the specified member.</param>
		/// <param name="args">An array containing the arguments to pass to the member to invoke.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> object representing the globalization locale to use, which may be necessary for locale-specific conversions, such as converting a numeric String to a Double.  
		///  -or-  
		///  <see langword="null" /> to use the current thread's <see cref="T:System.Globalization.CultureInfo" />.</param>
		/// <returns>An <see cref="T:System.Object" /> representing the return value of the invoked member.</returns>
		// Token: 0x0600443B RID: 17467
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, CultureInfo culture);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[])" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the constructor, method, property, or field member to invoke.  
		///  -or-  
		///  An empty string ("") to invoke the default member.  
		///  -or-  
		///  For IDispatch members, a string representing the DispID, for example "[DispID=3]".</param>
		/// <param name="invokeAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted. The access can be one of the <see langword="BindingFlags" /> such as <see langword="Public" />, <see langword="NonPublic" />, <see langword="Private" />, <see langword="InvokeMethod" />, <see langword="GetField" />, and so on. The type of lookup need not be specified. If the type of lookup is omitted, <see langword="BindingFlags.Public" /> | <see langword="BindingFlags.Instance" /> will apply.</param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.  
		///  -or-  
		///  <see langword="null" />, to use the <see cref="P:System.Type.DefaultBinder" />.</param>
		/// <param name="target">The <see cref="T:System.Object" /> on which to invoke the specified member.</param>
		/// <param name="args">An array containing the arguments to pass to the member to invoke.</param>
		/// <returns>An <see cref="T:System.Object" /> representing the return value of the invoked member.</returns>
		// Token: 0x0600443C RID: 17468
		object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> method.</summary>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.  
		///  -or-  
		///  <see langword="null" />, to use the <see cref="P:System.Type.DefaultBinder" />.</param>
		/// <param name="callConvention">The <see cref="T:System.Reflection.CallingConventions" /> object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and the stack is cleaned up.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the constructor to get.  
		///  -or-  
		///  An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a constructor that takes no parameters.</param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter.</param>
		/// <returns>A <see cref="T:System.Reflection.ConstructorInfo" /> object representing the constructor that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x0600443D RID: 17469
		ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Type[],System.Reflection.ParameterModifier[])" /> method.</summary>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.  
		///  -or-  
		///  <see langword="null" />, to use the <see cref="P:System.Type.DefaultBinder" />.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the constructor to get.  
		///  -or-  
		///  An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a constructor that takes no parameters.  
		///  -or-  
		///  <see cref="F:System.Type.EmptyTypes" />.</param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the parameter type array. The default binder does not process this parameter.</param>
		/// <returns>A <see cref="T:System.Reflection.ConstructorInfo" /> object representing the constructor that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x0600443E RID: 17470
		ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetConstructor(System.Type[])" /> method.</summary>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the desired constructor.  
		///  -or-  
		///  An empty array of <see cref="T:System.Type" /> objects, to get a constructor that takes no parameters. Such an empty array is provided by the <see langword="static" /> field <see cref="F:System.Type.EmptyTypes" />.</param>
		/// <returns>A <see cref="T:System.Reflection.ConstructorInfo" /> object representing the public instance constructor whose parameters match the types in the parameter type array, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x0600443F RID: 17471
		ConstructorInfo GetConstructor(Type[] types);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetConstructors" /> method.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing all the public instance constructors defined for the current <see cref="T:System.Type" />, but not including the type initializer (static constructor). If no public instance constructors are defined for the current <see cref="T:System.Type" />, or if the current <see cref="T:System.Type" /> represents a type parameter of a generic type or method definition, an empty array of type <see cref="T:System.Reflection.ConstructorInfo" /> is returned.</returns>
		// Token: 0x06004440 RID: 17472
		ConstructorInfo[] GetConstructors();

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.TypeInitializer" /> property.</summary>
		/// <returns>A <see cref="T:System.Reflection.ConstructorInfo" /> containing the name of the class constructor for the <see cref="T:System.Type" />.</returns>
		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06004441 RID: 17473
		ConstructorInfo TypeInitializer { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the method to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.  
		///  -or-  
		///  <see langword="null" />, to use the <see cref="P:System.Type.DefaultBinder" />.</param>
		/// <param name="callConvention">The <see cref="T:System.Reflection.CallingConventions" /> object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and how the stack is cleaned up.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.  
		///  -or-  
		///  An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a method that takes no parameters.</param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter.</param>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the method that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x06004442 RID: 17474
		MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetMethod(System.String,System.Type[],System.Reflection.ParameterModifier[])" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the public method to get.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.  
		///  -or-  
		///  An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a method that takes no parameters.</param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter.</param>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the public method that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x06004443 RID: 17475
		MethodInfo GetMethod(string name, Type[] types, ParameterModifier[] modifiers);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetMethod(System.String,System.Type[])" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the public method to get.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.  
		///  -or-  
		///  An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a method that takes no parameters.</param>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the public method whose parameters match the specified argument types, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x06004444 RID: 17476
		MethodInfo GetMethod(string name, Type[] types);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetMethod(System.String)" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the public method to get.</param>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object representing the public method with the specified name, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x06004445 RID: 17477
		MethodInfo GetMethod(string name);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetMethods" /> method.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MethodInfo" /> objects representing all the public methods defined for the current <see cref="T:System.Type" />.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.MethodInfo" />, if no public methods are defined for the current <see cref="T:System.Type" />.</returns>
		// Token: 0x06004446 RID: 17478
		MethodInfo[] GetMethods();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetField(System.String)" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the data field to get.</param>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object representing the public field with the specified name, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x06004447 RID: 17479
		FieldInfo GetField(string name);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetFields" /> method.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.FieldInfo" /> objects representing all the public fields defined for the current <see cref="T:System.Type" />.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.FieldInfo" />, if no public fields are defined for the current <see cref="T:System.Type" />.</returns>
		// Token: 0x06004448 RID: 17480
		FieldInfo[] GetFields();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetInterface(System.String)" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the interface to get. For generic interfaces, this is the mangled name.</param>
		/// <returns>A <see cref="T:System.Type" /> object representing the interface with the specified name, implemented or inherited by the current <see cref="T:System.Type" />, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x06004449 RID: 17481
		Type GetInterface(string name);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetEvent(System.String)" /> method.</summary>
		/// <param name="name">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to return <see langword="null" />.</param>
		/// <returns>An array of <see cref="T:System.Reflection.EventInfo" /> objects representing all events that are declared or inherited by the current <see cref="T:System.Type" /> that match the specified binding constraints.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.EventInfo" />, if the current <see cref="T:System.Type" /> does not have events, or if none of the events match the binding constraints.</returns>
		// Token: 0x0600444A RID: 17482
		EventInfo GetEvent(string name);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetProperty(System.String,System.Type,System.Type[],System.Reflection.ParameterModifier[])" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the public property to get.</param>
		/// <param name="returnType">The return type of the property.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.  
		///  -or-  
		///  An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed.</param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter.</param>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object representing the public property that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x0600444B RID: 17483
		PropertyInfo GetProperty(string name, Type returnType, Type[] types, ParameterModifier[] modifiers);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetProperty(System.String,System.Type,System.Type[])" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the public property to get.</param>
		/// <param name="returnType">The return type of the property.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.  
		///  -or-  
		///  An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed.</param>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object representing the public property whose parameters match the specified argument types, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x0600444C RID: 17484
		PropertyInfo GetProperty(string name, Type returnType, Type[] types);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetProperty(System.String,System.Type[])" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the public property to get.</param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.  
		///  -or-  
		///  An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed.</param>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object representing the public property whose parameters match the specified argument types, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x0600444D RID: 17485
		PropertyInfo GetProperty(string name, Type[] types);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetProperty(System.String,System.Type)" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the public property to get.</param>
		/// <param name="returnType">The return type of the property.</param>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object representing the public property with the specified name, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x0600444E RID: 17486
		PropertyInfo GetProperty(string name, Type returnType);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetProperty(System.String)" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the public property to get.</param>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object representing the public property with the specified name, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x0600444F RID: 17487
		PropertyInfo GetProperty(string name);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetProperties" /> method.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.PropertyInfo" /> objects representing all public properties of the current <see cref="T:System.Type" />.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.PropertyInfo" />, if the current <see cref="T:System.Type" /> does not have public properties.</returns>
		// Token: 0x06004450 RID: 17488
		PropertyInfo[] GetProperties();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetNestedTypes" /> method.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing all the types nested within the current <see cref="T:System.Type" />.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Type" />, if no types are nested within the current <see cref="T:System.Type" />.</returns>
		// Token: 0x06004451 RID: 17489
		Type[] GetNestedTypes();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetNestedType(System.String)" /> method.</summary>
		/// <param name="name">The string containing the name of the nested type to get.</param>
		/// <returns>A <see cref="T:System.Type" /> object representing the public nested type with the specified name, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x06004452 RID: 17490
		Type GetNestedType(string name);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetMember(System.String)" /> method.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the public members to get.</param>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public members with the specified name, if found; otherwise, an empty array.</returns>
		// Token: 0x06004453 RID: 17491
		MemberInfo[] GetMember(string name);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetMembers" /> method.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing all the public members of the current <see cref="T:System.Type" />.  
		///  -or-  
		///  An empty array of type <see cref="T:System.Reflection.MemberInfo" />, if the current <see cref="T:System.Type" /> does not have public members.</returns>
		// Token: 0x06004454 RID: 17492
		MemberInfo[] GetMembers();

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.Attributes" /> property.</summary>
		/// <returns>A <see cref="T:System.Reflection.TypeAttributes" /> object representing the attribute set of the <see cref="T:System.Type" />, unless the <see cref="T:System.Type" /> represents a generic type parameter, in which case the value is unspecified.</returns>
		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06004455 RID: 17493
		TypeAttributes Attributes { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsNotPublic" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the top-level <see cref="T:System.Type" /> is not declared public; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06004456 RID: 17494
		bool IsNotPublic { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsPublic" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the top-level <see cref="T:System.Type" /> is declared public; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06004457 RID: 17495
		bool IsPublic { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsNestedPublic" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the class is nested and declared public; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06004458 RID: 17496
		bool IsNestedPublic { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsNestedPrivate" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is nested and declared private; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06004459 RID: 17497
		bool IsNestedPrivate { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsNestedFamily" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is nested and visible only within its own family; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x0600445A RID: 17498
		bool IsNestedFamily { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsNestedAssembly" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is nested and visible only within its own assembly; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x0600445B RID: 17499
		bool IsNestedAssembly { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsNestedFamANDAssem" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is nested and visible only to classes that belong to both its own family and its own assembly; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x0600445C RID: 17500
		bool IsNestedFamANDAssem { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsNestedFamORAssem" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is nested and visible only to classes that belong to its own family or to its own assembly; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x0600445D RID: 17501
		bool IsNestedFamORAssem { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsAutoLayout" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the class layout attribute <see langword="AutoLayout" /> is selected for the <see cref="T:System.Type" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x0600445E RID: 17502
		bool IsAutoLayout { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsLayoutSequential" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the class layout attribute <see langword="SequentialLayout" /> is selected for the <see cref="T:System.Type" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x0600445F RID: 17503
		bool IsLayoutSequential { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsExplicitLayout" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the class layout attribute <see langword="ExplicitLayout" /> is selected for the <see cref="T:System.Type" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06004460 RID: 17504
		bool IsExplicitLayout { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsClass" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is a class; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06004461 RID: 17505
		bool IsClass { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsInterface" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is an interface; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06004462 RID: 17506
		bool IsInterface { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsValueType" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is a value type; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06004463 RID: 17507
		bool IsValueType { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsAbstract" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is abstract; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06004464 RID: 17508
		bool IsAbstract { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsSealed" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is declared sealed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06004465 RID: 17509
		bool IsSealed { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsEnum" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Type" /> represents an enumeration; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06004466 RID: 17510
		bool IsEnum { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsSpecialName" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> has a name that requires special handling; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06004467 RID: 17511
		bool IsSpecialName { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsImport" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> has <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06004468 RID: 17512
		bool IsImport { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsSerializable" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is serializable; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06004469 RID: 17513
		bool IsSerializable { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsAnsiClass" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the string format attribute <see langword="AnsiClass" /> is selected for the <see cref="T:System.Type" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x0600446A RID: 17514
		bool IsAnsiClass { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsUnicodeClass" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the string format attribute <see langword="UnicodeClass" /> is selected for the <see cref="T:System.Type" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x0600446B RID: 17515
		bool IsUnicodeClass { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsAutoClass" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the string format attribute <see langword="AutoClass" /> is selected for the <see cref="T:System.Type" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x0600446C RID: 17516
		bool IsAutoClass { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsArray" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is an array; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x0600446D RID: 17517
		bool IsArray { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsByRef" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is passed by reference; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x0600446E RID: 17518
		bool IsByRef { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsPointer" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is a pointer; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x0600446F RID: 17519
		bool IsPointer { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsPrimitive" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is one of the primitive types; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06004470 RID: 17520
		bool IsPrimitive { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsCOMObject" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is a COM object; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06004471 RID: 17521
		bool IsCOMObject { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.HasElementType" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is an array, a pointer, or is passed by reference; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06004472 RID: 17522
		bool HasElementType { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsContextful" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> can be hosted in a context; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06004473 RID: 17523
		bool IsContextful { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Type.IsMarshalByRef" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is marshaled by reference; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06004474 RID: 17524
		bool IsMarshalByRef { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.Equals(System.Type)" /> method.</summary>
		/// <param name="o">The <see cref="T:System.Type" /> whose underlying system type is to be compared with the underlying system type of the current <see cref="T:System.Type" />.</param>
		/// <returns>
		///   <see langword="true" /> if the underlying system type of <paramref name="o" /> is the same as the underlying system type of the current <see cref="T:System.Type" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004475 RID: 17525
		bool Equals(Type o);
	}
}
