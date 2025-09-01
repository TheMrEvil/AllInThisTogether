using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Wraps a <see cref="T:System.Type" /> object and delegates methods to that <see langword="Type" />.</summary>
	// Token: 0x020008CE RID: 2254
	public class TypeDelegator : TypeInfo
	{
		/// <summary>Returns a value that indicates whether the specified type can be assigned to this type.</summary>
		/// <param name="typeInfo">The type to check.</param>
		/// <returns>
		///   <see langword="true" /> if the specified type can be assigned to this type; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004AF0 RID: 19184 RVA: 0x00057EED File Offset: 0x000560ED
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TypeDelegator" /> class with default properties.</summary>
		// Token: 0x06004AF1 RID: 19185 RVA: 0x000EFDF7 File Offset: 0x000EDFF7
		protected TypeDelegator()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TypeDelegator" /> class specifying the encapsulating instance.</summary>
		/// <param name="delegatingType">The instance of the class <see cref="T:System.Type" /> that encapsulates the call to the method of an object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="delegatingType" /> is <see langword="null" />.</exception>
		// Token: 0x06004AF2 RID: 19186 RVA: 0x000EFDFF File Offset: 0x000EDFFF
		public TypeDelegator(Type delegatingType)
		{
			if (delegatingType == null)
			{
				throw new ArgumentNullException("delegatingType");
			}
			this.typeImpl = delegatingType;
		}

		/// <summary>Gets the GUID (globally unique identifier) of the implemented type.</summary>
		/// <returns>A GUID.</returns>
		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06004AF3 RID: 19187 RVA: 0x000EFE22 File Offset: 0x000EE022
		public override Guid GUID
		{
			get
			{
				return this.typeImpl.GUID;
			}
		}

		/// <summary>Gets a value that identifies this entity in metadata.</summary>
		/// <returns>A value which, in combination with the module, uniquely identifies this entity in metadata.</returns>
		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06004AF4 RID: 19188 RVA: 0x000EFE2F File Offset: 0x000EE02F
		public override int MetadataToken
		{
			get
			{
				return this.typeImpl.MetadataToken;
			}
		}

		/// <summary>Invokes the specified member. The method that is to be invoked must be accessible and provide the most specific match with the specified argument list, under the constraints of the specified binder and invocation attributes.</summary>
		/// <param name="name">The name of the member to invoke. This may be a constructor, method, property, or field. If an empty string ("") is passed, the default member is invoked.</param>
		/// <param name="invokeAttr">The invocation attribute. This must be one of the following <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="Static" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, or <see langword="SetProperty" />. A suitable invocation attribute must be specified. If a static member is to be invoked, the <see langword="Static" /> flag must be set.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects via reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used. See <see cref="T:System.Reflection.Binder" />.</param>
		/// <param name="target">The object on which to invoke the specified member.</param>
		/// <param name="args">An array of type <see langword="Object" /> that contains the number, order, and type of the parameters of the member to be invoked. If <paramref name="args" /> contains an uninitialized <see langword="Object" />, it is treated as empty, which, with the default binder, can be widened to 0, 0.0 or a string.</param>
		/// <param name="modifiers">An array of type <see langword="ParameterModifer" /> that is the same length as <paramref name="args" />, with elements that represent the attributes associated with the arguments of the member to be invoked. A parameter has attributes associated with it in the member's signature. For ByRef, use <see langword="ParameterModifer.ByRef" />, and for none, use <see langword="ParameterModifer.None" />. The default binder does exact matching on these. Attributes such as <see langword="In" /> and <see langword="InOut" /> are not used in binding, and can be viewed using <see langword="ParameterInfo" />.</param>
		/// <param name="culture">An instance of <see langword="CultureInfo" /> used to govern the coercion of types. This is necessary, for example, to convert a string that represents 1000 to a <see langword="Double" /> value, since 1000 is represented differently by different cultures. If <paramref name="culture" /> is <see langword="null" />, the <see langword="CultureInfo" /> for the current thread's <see langword="CultureInfo" /> is used.</param>
		/// <param name="namedParameters">An array of type <see langword="String" /> containing parameter names that match up, starting at element zero, with the <paramref name="args" /> array. There must be no holes in the array. If <paramref name="args" />. <see langword="Length" /> is greater than <paramref name="namedParameters" />. <see langword="Length" />, the remaining parameters are filled in order.</param>
		/// <returns>An <see langword="Object" /> representing the return value of the invoked member.</returns>
		// Token: 0x06004AF5 RID: 19189 RVA: 0x000EFE3C File Offset: 0x000EE03C
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			return this.typeImpl.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		/// <summary>Gets the module that contains the implemented type.</summary>
		/// <returns>A <see cref="T:System.Reflection.Module" /> object representing the module of the implemented type.</returns>
		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06004AF6 RID: 19190 RVA: 0x000EFE61 File Offset: 0x000EE061
		public override Module Module
		{
			get
			{
				return this.typeImpl.Module;
			}
		}

		/// <summary>Gets the assembly of the implemented type.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> object representing the assembly of the implemented type.</returns>
		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06004AF7 RID: 19191 RVA: 0x000EFE6E File Offset: 0x000EE06E
		public override Assembly Assembly
		{
			get
			{
				return this.typeImpl.Assembly;
			}
		}

		/// <summary>Gets a handle to the internal metadata representation of an implemented type.</summary>
		/// <returns>A <see langword="RuntimeTypeHandle" /> object.</returns>
		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06004AF8 RID: 19192 RVA: 0x000EFE7B File Offset: 0x000EE07B
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return this.typeImpl.TypeHandle;
			}
		}

		/// <summary>Gets the name of the implemented type, with the path removed.</summary>
		/// <returns>A <see langword="String" /> containing the type's non-qualified name.</returns>
		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06004AF9 RID: 19193 RVA: 0x000EFE88 File Offset: 0x000EE088
		public override string Name
		{
			get
			{
				return this.typeImpl.Name;
			}
		}

		/// <summary>Gets the fully qualified name of the implemented type.</summary>
		/// <returns>A <see langword="String" /> containing the type's fully qualified name.</returns>
		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06004AFA RID: 19194 RVA: 0x000EFE95 File Offset: 0x000EE095
		public override string FullName
		{
			get
			{
				return this.typeImpl.FullName;
			}
		}

		/// <summary>Gets the namespace of the implemented type.</summary>
		/// <returns>A <see langword="String" /> containing the type's namespace.</returns>
		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06004AFB RID: 19195 RVA: 0x000EFEA2 File Offset: 0x000EE0A2
		public override string Namespace
		{
			get
			{
				return this.typeImpl.Namespace;
			}
		}

		/// <summary>Gets the assembly's fully qualified name.</summary>
		/// <returns>A <see langword="String" /> containing the assembly's fully qualified name.</returns>
		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06004AFC RID: 19196 RVA: 0x000EFEAF File Offset: 0x000EE0AF
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.typeImpl.AssemblyQualifiedName;
			}
		}

		/// <summary>Gets the base type for the current type.</summary>
		/// <returns>The base type for a type.</returns>
		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06004AFD RID: 19197 RVA: 0x000EFEBC File Offset: 0x000EE0BC
		public override Type BaseType
		{
			get
			{
				return this.typeImpl.BaseType;
			}
		}

		/// <summary>Gets the constructor that implemented the <see langword="TypeDelegator" />.</summary>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects using reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="callConvention">The calling conventions.</param>
		/// <param name="types">An array of type <see langword="Type" /> containing a list of the parameter number, order, and types. Types cannot be <see langword="null" />; use an appropriate <see langword="GetMethod" /> method or an empty array to search for a method without parameters.</param>
		/// <param name="modifiers">An array of type <see langword="ParameterModifier" /> having the same length as the <paramref name="types" /> array, whose elements represent the attributes associated with the parameters of the method to get.</param>
		/// <returns>A <see langword="ConstructorInfo" /> object for the method that matches the specified criteria, or <see langword="null" /> if a match cannot be found.</returns>
		// Token: 0x06004AFE RID: 19198 RVA: 0x000EFEC9 File Offset: 0x000EE0C9
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this.typeImpl.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing constructors defined for the type wrapped by the current <see cref="T:System.Reflection.TypeDelegator" />.</summary>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <returns>An array of type <see langword="ConstructorInfo" /> containing the specified constructors defined for this class. If no constructors are defined, an empty array is returned. Depending on the value of a specified parameter, only public constructors or both public and non-public constructors will be returned.</returns>
		// Token: 0x06004AFF RID: 19199 RVA: 0x000EFEDD File Offset: 0x000EE0DD
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetConstructors(bindingAttr);
		}

		/// <summary>Searches for the specified method whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.</summary>
		/// <param name="name">The method name.</param>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects using reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="callConvention">The calling conventions.</param>
		/// <param name="types">An array of type <see langword="Type" /> containing a list of the parameter number, order, and types. Types cannot be <see langword="null" />; use an appropriate <see langword="GetMethod" /> method or an empty array to search for a method without parameters.</param>
		/// <param name="modifiers">An array of type <see langword="ParameterModifier" /> having the same length as the <paramref name="types" /> array, whose elements represent the attributes associated with the parameters of the method to get.</param>
		/// <returns>A <see langword="MethodInfoInfo" /> object for the implementation method that matches the specified criteria, or <see langword="null" /> if a match cannot be found.</returns>
		// Token: 0x06004B00 RID: 19200 RVA: 0x000EFEEB File Offset: 0x000EE0EB
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				return this.typeImpl.GetMethod(name, bindingAttr);
			}
			return this.typeImpl.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.MethodInfo" /> objects representing specified methods of the type wrapped by the current <see cref="T:System.Reflection.TypeDelegator" />.</summary>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <returns>An array of <see langword="MethodInfo" /> objects representing the methods defined on this <see langword="TypeDelegator" />.</returns>
		// Token: 0x06004B01 RID: 19201 RVA: 0x000EFF13 File Offset: 0x000EE113
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMethods(bindingAttr);
		}

		/// <summary>Returns a <see cref="T:System.Reflection.FieldInfo" /> object representing the field with the specified name.</summary>
		/// <param name="name">The name of the field to find.</param>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <returns>A <see langword="FieldInfo" /> object representing the field declared or inherited by this <see langword="TypeDelegator" /> with the specified name. Returns <see langword="null" /> if no such field is found.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06004B02 RID: 19202 RVA: 0x000EFF21 File Offset: 0x000EE121
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetField(name, bindingAttr);
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.FieldInfo" /> objects representing the data fields defined for the type wrapped by the current <see cref="T:System.Reflection.TypeDelegator" />.</summary>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <returns>An array of type <see langword="FieldInfo" /> containing the fields declared or inherited by the current <see langword="TypeDelegator" />. An empty array is returned if there are no matched fields.</returns>
		// Token: 0x06004B03 RID: 19203 RVA: 0x000EFF30 File Offset: 0x000EE130
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetFields(bindingAttr);
		}

		/// <summary>Returns the specified interface implemented by the type wrapped by the current <see cref="T:System.Reflection.TypeDelegator" />.</summary>
		/// <param name="name">The fully qualified name of the interface implemented by the current class.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> if the case is to be ignored; otherwise, <see langword="false" />.</param>
		/// <returns>A <see langword="Type" /> object representing the interface implemented (directly or indirectly) by the current class with the fully qualified name matching the specified name. If no interface that matches name is found, null is returned.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06004B04 RID: 19204 RVA: 0x000EFF3E File Offset: 0x000EE13E
		public override Type GetInterface(string name, bool ignoreCase)
		{
			return this.typeImpl.GetInterface(name, ignoreCase);
		}

		/// <summary>Returns all the interfaces implemented on the current class and its base classes.</summary>
		/// <returns>An array of type <see langword="Type" /> containing all the interfaces implemented on the current class and its base classes. If none are defined, an empty array is returned.</returns>
		// Token: 0x06004B05 RID: 19205 RVA: 0x000EFF4D File Offset: 0x000EE14D
		public override Type[] GetInterfaces()
		{
			return this.typeImpl.GetInterfaces();
		}

		/// <summary>Returns the specified event.</summary>
		/// <param name="name">The name of the event to get.</param>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <returns>An <see cref="T:System.Reflection.EventInfo" /> object representing the event declared or inherited by this type with the specified name. This method returns <see langword="null" /> if no such event is found.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06004B06 RID: 19206 RVA: 0x000EFF5A File Offset: 0x000EE15A
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetEvent(name, bindingAttr);
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.EventInfo" /> objects representing all the public events declared or inherited by the current <see langword="TypeDelegator" />.</summary>
		/// <returns>An array that contains all the events declared or inherited by the current type. If there are no events, an empty array is returned.</returns>
		// Token: 0x06004B07 RID: 19207 RVA: 0x000EFF69 File Offset: 0x000EE169
		public override EventInfo[] GetEvents()
		{
			return this.typeImpl.GetEvents();
		}

		/// <summary>When overridden in a derived class, searches for the specified property whose parameters match the specified argument types and modifiers, using the specified binding constraints.</summary>
		/// <param name="name">The property to get.</param>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects via reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used. See <see cref="T:System.Reflection.Binder" />.</param>
		/// <param name="returnType">The return type of the property.</param>
		/// <param name="types">A list of parameter types. The list represents the number, order, and types of the parameters. Types cannot be null; use an appropriate <see langword="GetMethod" /> method or an empty array to search for a method without parameters.</param>
		/// <param name="modifiers">An array of the same length as types with elements that represent the attributes associated with the parameters of the method to get.</param>
		/// <returns>A <see cref="T:System.Reflection.PropertyInfo" /> object for the property that matches the specified criteria, or null if a match cannot be found.</returns>
		// Token: 0x06004B08 RID: 19208 RVA: 0x000EFF76 File Offset: 0x000EE176
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			if (returnType == null && types == null)
			{
				return this.typeImpl.GetProperty(name, bindingAttr);
			}
			return this.typeImpl.GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.PropertyInfo" /> objects representing properties of the type wrapped by the current <see cref="T:System.Reflection.TypeDelegator" />.</summary>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <returns>An array of <see langword="PropertyInfo" /> objects representing properties defined on this <see langword="TypeDelegator" />.</returns>
		// Token: 0x06004B09 RID: 19209 RVA: 0x000EFFA8 File Offset: 0x000EE1A8
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetProperties(bindingAttr);
		}

		/// <summary>Returns the events specified in <paramref name="bindingAttr" /> that are declared or inherited by the current <see langword="TypeDelegator" />.</summary>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <returns>An array of type <see langword="EventInfo" /> containing the events specified in <paramref name="bindingAttr" />. If there are no events, an empty array is returned.</returns>
		// Token: 0x06004B0A RID: 19210 RVA: 0x000EFFB6 File Offset: 0x000EE1B6
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetEvents(bindingAttr);
		}

		/// <summary>Returns the nested types specified in <paramref name="bindingAttr" /> that are declared or inherited by the type wrapped by the current <see cref="T:System.Reflection.TypeDelegator" />.</summary>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <returns>An array of type <see langword="Type" /> containing the nested types.</returns>
		// Token: 0x06004B0B RID: 19211 RVA: 0x000EFFC4 File Offset: 0x000EE1C4
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetNestedTypes(bindingAttr);
		}

		/// <summary>Returns a nested type specified by <paramref name="name" /> and in <paramref name="bindingAttr" /> that are declared or inherited by the type represented by the current <see cref="T:System.Reflection.TypeDelegator" />.</summary>
		/// <param name="name">The nested type's name.</param>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <returns>A <see langword="Type" /> object representing the nested type.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06004B0C RID: 19212 RVA: 0x000EFFD2 File Offset: 0x000EE1D2
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetNestedType(name, bindingAttr);
		}

		/// <summary>Returns members (properties, methods, constructors, fields, events, and nested types) specified by the given <paramref name="name" />, <paramref name="type" />, and <paramref name="bindingAttr" />.</summary>
		/// <param name="name">The name of the member to get.</param>
		/// <param name="type">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <param name="bindingAttr">The type of members to get.</param>
		/// <returns>An array of type <see langword="MemberInfo" /> containing all the members of the current class and its base class meeting the specified criteria.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06004B0D RID: 19213 RVA: 0x000EFFE1 File Offset: 0x000EE1E1
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMember(name, type, bindingAttr);
		}

		/// <summary>Returns members specified by <paramref name="bindingAttr" />.</summary>
		/// <param name="bindingAttr">A bitmask that affects the way in which the search is conducted. The value is a combination of zero or more bit flags from <see cref="T:System.Reflection.BindingFlags" />.</param>
		/// <returns>An array of type <see langword="MemberInfo" /> containing all the members of the current class and its base classes that meet the <paramref name="bindingAttr" /> filter.</returns>
		// Token: 0x06004B0E RID: 19214 RVA: 0x000EFFF1 File Offset: 0x000EE1F1
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			return this.typeImpl.GetMembers(bindingAttr);
		}

		/// <summary>Gets the attributes assigned to the <see langword="TypeDelegator" />.</summary>
		/// <returns>A <see langword="TypeAttributes" /> object representing the implementation attribute flags.</returns>
		// Token: 0x06004B0F RID: 19215 RVA: 0x000EFFFF File Offset: 0x000EE1FF
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.typeImpl.Attributes;
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06004B10 RID: 19216 RVA: 0x000F000C File Offset: 0x000EE20C
		public override bool IsTypeDefinition
		{
			get
			{
				return this.typeImpl.IsTypeDefinition;
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06004B11 RID: 19217 RVA: 0x000F0019 File Offset: 0x000EE219
		public override bool IsSZArray
		{
			get
			{
				return this.typeImpl.IsSZArray;
			}
		}

		/// <summary>Returns a value that indicates whether the <see cref="T:System.Type" /> is an array.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is an array; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004B12 RID: 19218 RVA: 0x000F0026 File Offset: 0x000EE226
		protected override bool IsArrayImpl()
		{
			return this.typeImpl.IsArray;
		}

		/// <summary>Returns a value that indicates whether the <see cref="T:System.Type" /> is one of the primitive types.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is one of the primitive types; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004B13 RID: 19219 RVA: 0x000F0033 File Offset: 0x000EE233
		protected override bool IsPrimitiveImpl()
		{
			return this.typeImpl.IsPrimitive;
		}

		/// <summary>Returns a value that indicates whether the <see cref="T:System.Type" /> is passed by reference.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is passed by reference; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004B14 RID: 19220 RVA: 0x000F0040 File Offset: 0x000EE240
		protected override bool IsByRefImpl()
		{
			return this.typeImpl.IsByRef;
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06004B15 RID: 19221 RVA: 0x000F004D File Offset: 0x000EE24D
		public override bool IsGenericTypeParameter
		{
			get
			{
				return this.typeImpl.IsGenericTypeParameter;
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06004B16 RID: 19222 RVA: 0x000F005A File Offset: 0x000EE25A
		public override bool IsGenericMethodParameter
		{
			get
			{
				return this.typeImpl.IsGenericMethodParameter;
			}
		}

		/// <summary>Returns a value that indicates whether the <see cref="T:System.Type" /> is a pointer.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is a pointer; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004B17 RID: 19223 RVA: 0x000F0067 File Offset: 0x000EE267
		protected override bool IsPointerImpl()
		{
			return this.typeImpl.IsPointer;
		}

		/// <summary>Returns a value that indicates whether the type is a value type; that is, not a class or an interface.</summary>
		/// <returns>
		///   <see langword="true" /> if the type is a value type; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004B18 RID: 19224 RVA: 0x000F0074 File Offset: 0x000EE274
		protected override bool IsValueTypeImpl()
		{
			return this.typeImpl.IsValueType;
		}

		/// <summary>Returns a value that indicates whether the <see cref="T:System.Type" /> is a COM object.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is a COM object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004B19 RID: 19225 RVA: 0x000F0081 File Offset: 0x000EE281
		protected override bool IsCOMObjectImpl()
		{
			return this.typeImpl.IsCOMObject;
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06004B1A RID: 19226 RVA: 0x000F008E File Offset: 0x000EE28E
		public override bool IsByRefLike
		{
			get
			{
				return this.typeImpl.IsByRefLike;
			}
		}

		/// <summary>Gets a value that indicates whether this object represents a constructed generic type.</summary>
		/// <returns>
		///   <see langword="true" /> if this object represents a constructed generic type; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06004B1B RID: 19227 RVA: 0x000F009B File Offset: 0x000EE29B
		public override bool IsConstructedGenericType
		{
			get
			{
				return this.typeImpl.IsConstructedGenericType;
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06004B1C RID: 19228 RVA: 0x000F00A8 File Offset: 0x000EE2A8
		public override bool IsCollectible
		{
			get
			{
				return this.typeImpl.IsCollectible;
			}
		}

		/// <summary>Returns the <see cref="T:System.Type" /> of the object encompassed or referred to by the current array, pointer or ByRef.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object encompassed or referred to by the current array, pointer or <see langword="ByRef" />, or <see langword="null" /> if the current <see cref="T:System.Type" /> is not an array, a pointer or a <see langword="ByRef" />.</returns>
		// Token: 0x06004B1D RID: 19229 RVA: 0x000F00B5 File Offset: 0x000EE2B5
		public override Type GetElementType()
		{
			return this.typeImpl.GetElementType();
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> encompasses or refers to another type; that is, whether the current <see cref="T:System.Type" /> is an array, a pointer or a ByRef.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Type" /> is an array, a pointer or a ByRef; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004B1E RID: 19230 RVA: 0x000F00C2 File Offset: 0x000EE2C2
		protected override bool HasElementTypeImpl()
		{
			return this.typeImpl.HasElementType;
		}

		/// <summary>Gets the underlying <see cref="T:System.Type" /> that represents the implemented type.</summary>
		/// <returns>The underlying type.</returns>
		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06004B1F RID: 19231 RVA: 0x000F00CF File Offset: 0x000EE2CF
		public override Type UnderlyingSystemType
		{
			get
			{
				return this.typeImpl.UnderlyingSystemType;
			}
		}

		/// <summary>Returns all the custom attributes defined for this type, specifying whether to search the type's inheritance chain.</summary>
		/// <param name="inherit">Specifies whether to search this type's inheritance chain to find the attributes.</param>
		/// <returns>An array of objects containing all the custom attributes defined for this type.</returns>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded.</exception>
		// Token: 0x06004B20 RID: 19232 RVA: 0x000F00DC File Offset: 0x000EE2DC
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.typeImpl.GetCustomAttributes(inherit);
		}

		/// <summary>Returns an array of custom attributes identified by type.</summary>
		/// <param name="attributeType">An array of custom attributes identified by type.</param>
		/// <param name="inherit">Specifies whether to search this type's inheritance chain to find the attributes.</param>
		/// <returns>An array of objects containing the custom attributes defined in this type that match the <paramref name="attributeType" /> parameter, specifying whether to search the type's inheritance chain, or <see langword="null" /> if no custom attributes are defined on this type.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type cannot be loaded.</exception>
		// Token: 0x06004B21 RID: 19233 RVA: 0x000F00EA File Offset: 0x000EE2EA
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.typeImpl.GetCustomAttributes(attributeType, inherit);
		}

		/// <summary>Indicates whether a custom attribute identified by <paramref name="attributeType" /> is defined.</summary>
		/// <param name="attributeType">Specifies whether to search this type's inheritance chain to find the attributes.</param>
		/// <param name="inherit">An array of custom attributes identified by type.</param>
		/// <returns>
		///   <see langword="true" /> if a custom attribute identified by <paramref name="attributeType" /> is defined; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.ReflectionTypeLoadException">The custom attribute type cannot be loaded.</exception>
		// Token: 0x06004B22 RID: 19234 RVA: 0x000F00F9 File Offset: 0x000EE2F9
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.typeImpl.IsDefined(attributeType, inherit);
		}

		/// <summary>Returns an interface mapping for the specified interface type.</summary>
		/// <param name="interfaceType">The <see cref="T:System.Type" /> of the interface to retrieve a mapping of.</param>
		/// <returns>An <see cref="T:System.Reflection.InterfaceMapping" /> object representing the interface mapping for <paramref name="interfaceType" />.</returns>
		// Token: 0x06004B23 RID: 19235 RVA: 0x000F0108 File Offset: 0x000EE308
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			return this.typeImpl.GetInterfaceMap(interfaceType);
		}

		/// <summary>A value indicating type information.</summary>
		// Token: 0x04002F56 RID: 12118
		protected Type typeImpl;
	}
}
