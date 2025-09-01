using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	/// <summary>Defines and creates new instances of classes during run time.</summary>
	// Token: 0x02000947 RID: 2375
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_TypeBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class TypeBuilder : TypeInfo, _TypeBuilder
	{
		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06005284 RID: 21124 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _TypeBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06005285 RID: 21125 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _TypeBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06005286 RID: 21126 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _TypeBuilder.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x06005287 RID: 21127 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _TypeBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005288 RID: 21128 RVA: 0x00102EAE File Offset: 0x001010AE
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.attrs;
		}

		// Token: 0x06005289 RID: 21129 RVA: 0x00102EB6 File Offset: 0x001010B6
		private TypeBuilder()
		{
			if (RuntimeType.MakeTypeBuilderInstantiation == null)
			{
				RuntimeType.MakeTypeBuilderInstantiation = new Func<Type, Type[], Type>(TypeBuilderInstantiation.MakeGenericType);
			}
		}

		// Token: 0x0600528A RID: 21130 RVA: 0x00102ED8 File Offset: 0x001010D8
		[PreserveDependency("DoTypeBuilderResolve", "System.AppDomain")]
		internal TypeBuilder(ModuleBuilder mb, TypeAttributes attr, int table_idx) : this()
		{
			this.parent = null;
			this.attrs = attr;
			this.class_size = 0;
			this.table_idx = table_idx;
			this.tname = ((table_idx == 1) ? "<Module>" : ("type_" + table_idx.ToString()));
			this.nspace = string.Empty;
			this.fullname = TypeIdentifiers.WithoutEscape(this.tname);
			this.pmodule = mb;
		}

		// Token: 0x0600528B RID: 21131 RVA: 0x00102F4C File Offset: 0x0010114C
		internal TypeBuilder(ModuleBuilder mb, string name, TypeAttributes attr, Type parent, Type[] interfaces, PackingSize packing_size, int type_size, Type nesting_type) : this()
		{
			this.parent = TypeBuilder.ResolveUserType(parent);
			this.attrs = attr;
			this.class_size = type_size;
			this.packing_size = packing_size;
			this.nesting_type = nesting_type;
			this.check_name("fullname", name);
			if (parent == null && (attr & TypeAttributes.ClassSemanticsMask) != TypeAttributes.NotPublic && (attr & TypeAttributes.Abstract) == TypeAttributes.NotPublic)
			{
				throw new InvalidOperationException("Interface must be declared abstract.");
			}
			int num = name.LastIndexOf('.');
			if (num != -1)
			{
				this.tname = name.Substring(num + 1);
				this.nspace = name.Substring(0, num);
			}
			else
			{
				this.tname = name;
				this.nspace = string.Empty;
			}
			if (interfaces != null)
			{
				this.interfaces = new Type[interfaces.Length];
				Array.Copy(interfaces, this.interfaces, interfaces.Length);
			}
			this.pmodule = mb;
			if ((attr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic && parent == null)
			{
				this.parent = typeof(object);
			}
			this.table_idx = mb.get_next_table_index(this, 2, 1);
			this.fullname = this.GetFullName();
		}

		/// <summary>Retrieves the dynamic assembly that contains this type definition.</summary>
		/// <returns>Read-only. Retrieves the dynamic assembly that contains this type definition.</returns>
		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x0600528C RID: 21132 RVA: 0x0010305F File Offset: 0x0010125F
		public override Assembly Assembly
		{
			get
			{
				return this.pmodule.Assembly;
			}
		}

		/// <summary>Returns the full name of this type qualified by the display name of the assembly.</summary>
		/// <returns>Read-only. The full name of this type qualified by the display name of the assembly.</returns>
		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x0600528D RID: 21133 RVA: 0x0010306C File Offset: 0x0010126C
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.fullname.DisplayName + ", " + this.Assembly.FullName;
			}
		}

		/// <summary>Retrieves the base type of this type.</summary>
		/// <returns>Read-only. Retrieves the base type of this type.</returns>
		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x0600528E RID: 21134 RVA: 0x0010308E File Offset: 0x0010128E
		public override Type BaseType
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Returns the type that declared this type.</summary>
		/// <returns>Read-only. The type that declared this type.</returns>
		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x0600528F RID: 21135 RVA: 0x00103096 File Offset: 0x00101296
		public override Type DeclaringType
		{
			get
			{
				return this.nesting_type;
			}
		}

		/// <summary>Determines whether this type is derived from a specified type.</summary>
		/// <param name="c">A <see cref="T:System.Type" /> that is to be checked.</param>
		/// <returns>Read-only. Returns <see langword="true" /> if this type is the same as the type <paramref name="c" />, or is a subtype of type <paramref name="c" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005290 RID: 21136 RVA: 0x001030A0 File Offset: 0x001012A0
		[ComVisible(true)]
		public override bool IsSubclassOf(Type c)
		{
			if (c == null)
			{
				return false;
			}
			if (c == this)
			{
				return false;
			}
			Type baseType = this.parent;
			while (baseType != null)
			{
				if (c == baseType)
				{
					return true;
				}
				baseType = baseType.BaseType;
			}
			return false;
		}

		/// <summary>Returns the underlying system type for this <see langword="TypeBuilder" />.</summary>
		/// <returns>Read-only. Returns the underlying system type.</returns>
		/// <exception cref="T:System.InvalidOperationException">This type is an enumeration, but there is no underlying system type.</exception>
		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06005291 RID: 21137 RVA: 0x001030E8 File Offset: 0x001012E8
		public override Type UnderlyingSystemType
		{
			get
			{
				if (this.is_created)
				{
					return this.created.UnderlyingSystemType;
				}
				if (!this.IsEnum)
				{
					return this;
				}
				if (this.underlying_type != null)
				{
					return this.underlying_type;
				}
				throw new InvalidOperationException("Enumeration type is not defined.");
			}
		}

		// Token: 0x06005292 RID: 21138 RVA: 0x00103128 File Offset: 0x00101328
		private TypeName GetFullName()
		{
			TypeIdentifier typeIdentifier = TypeIdentifiers.FromInternal(this.tname);
			if (this.nesting_type != null)
			{
				return TypeNames.FromDisplay(this.nesting_type.FullName).NestedName(typeIdentifier);
			}
			if (this.nspace != null && this.nspace.Length > 0)
			{
				return TypeIdentifiers.FromInternal(this.nspace, typeIdentifier);
			}
			return typeIdentifier;
		}

		/// <summary>Retrieves the full path of this type.</summary>
		/// <returns>Read-only. Retrieves the full path of this type.</returns>
		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06005293 RID: 21139 RVA: 0x0010318A File Offset: 0x0010138A
		public override string FullName
		{
			get
			{
				return this.fullname.DisplayName;
			}
		}

		/// <summary>Retrieves the GUID of this type.</summary>
		/// <returns>Read-only. Retrieves the GUID of this type</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported for incomplete types.</exception>
		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06005294 RID: 21140 RVA: 0x00103197 File Offset: 0x00101397
		public override Guid GUID
		{
			get
			{
				this.check_created();
				return this.created.GUID;
			}
		}

		/// <summary>Retrieves the dynamic module that contains this type definition.</summary>
		/// <returns>Read-only. Retrieves the dynamic module that contains this type definition.</returns>
		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06005295 RID: 21141 RVA: 0x001031AA File Offset: 0x001013AA
		public override Module Module
		{
			get
			{
				return this.pmodule;
			}
		}

		/// <summary>Retrieves the name of this type.</summary>
		/// <returns>Read-only. Retrieves the <see cref="T:System.String" /> name of this type.</returns>
		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06005296 RID: 21142 RVA: 0x001031B2 File Offset: 0x001013B2
		public override string Name
		{
			get
			{
				return this.tname;
			}
		}

		/// <summary>Retrieves the namespace where this <see langword="TypeBuilder" /> is defined.</summary>
		/// <returns>Read-only. Retrieves the namespace where this <see langword="TypeBuilder" /> is defined.</returns>
		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06005297 RID: 21143 RVA: 0x001031BA File Offset: 0x001013BA
		public override string Namespace
		{
			get
			{
				return this.nspace;
			}
		}

		/// <summary>Retrieves the packing size of this type.</summary>
		/// <returns>Read-only. Retrieves the packing size of this type.</returns>
		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x06005298 RID: 21144 RVA: 0x001031C2 File Offset: 0x001013C2
		public PackingSize PackingSize
		{
			get
			{
				return this.packing_size;
			}
		}

		/// <summary>Retrieves the total size of a type.</summary>
		/// <returns>Read-only. Retrieves this type's total size.</returns>
		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x06005299 RID: 21145 RVA: 0x001031CA File Offset: 0x001013CA
		public int Size
		{
			get
			{
				return this.class_size;
			}
		}

		/// <summary>Returns the type that was used to obtain this type.</summary>
		/// <returns>Read-only. The type that was used to obtain this type.</returns>
		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x0600529A RID: 21146 RVA: 0x00103096 File Offset: 0x00101296
		public override Type ReflectedType
		{
			get
			{
				return this.nesting_type;
			}
		}

		/// <summary>Adds declarative security to this type.</summary>
		/// <param name="action">The security action to be taken such as Demand, Assert, and so on.</param>
		/// <param name="pset">The set of permissions the action applies to.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="action" /> is invalid (<see langword="RequestMinimum" />, <see langword="RequestOptional" />, and <see langword="RequestRefuse" /> are invalid).</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  The permission set <paramref name="pset" /> contains an action that was added earlier by <see langword="AddDeclarativeSecurity" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pset" /> is <see langword="null" />.</exception>
		// Token: 0x0600529B RID: 21147 RVA: 0x001031D4 File Offset: 0x001013D4
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
			this.check_not_created();
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
			this.attrs |= TypeAttributes.HasSecurity;
		}

		/// <summary>Adds an interface that this type implements.</summary>
		/// <param name="interfaceType">The interface that this type implements.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="interfaceType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x0600529C RID: 21148 RVA: 0x001032B4 File Offset: 0x001014B4
		[ComVisible(true)]
		public void AddInterfaceImplementation(Type interfaceType)
		{
			if (interfaceType == null)
			{
				throw new ArgumentNullException("interfaceType");
			}
			this.check_not_created();
			if (this.interfaces != null)
			{
				Type[] array = this.interfaces;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == interfaceType)
					{
						return;
					}
				}
				Type[] array2 = new Type[this.interfaces.Length + 1];
				this.interfaces.CopyTo(array2, 0);
				array2[this.interfaces.Length] = interfaceType;
				this.interfaces = array2;
				return;
			}
			this.interfaces = new Type[1];
			this.interfaces[0] = interfaceType;
		}

		// Token: 0x0600529D RID: 21149 RVA: 0x0010334C File Offset: 0x0010154C
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			this.check_created();
			if (!(this.created == typeof(object)))
			{
				return this.created.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
			}
			if (this.ctors == null)
			{
				return null;
			}
			ConstructorBuilder constructorBuilder = null;
			int num = 0;
			foreach (ConstructorBuilder constructorBuilder2 in this.ctors)
			{
				if (callConvention == CallingConventions.Any || constructorBuilder2.CallingConvention == callConvention)
				{
					constructorBuilder = constructorBuilder2;
					num++;
				}
			}
			if (num == 0)
			{
				return null;
			}
			if (types != null)
			{
				MethodBase[] array2 = new MethodBase[num];
				if (num == 1)
				{
					array2[0] = constructorBuilder;
				}
				else
				{
					num = 0;
					foreach (ConstructorBuilder constructorInfo in this.ctors)
					{
						if (callConvention == CallingConventions.Any || constructorInfo.CallingConvention == callConvention)
						{
							array2[num++] = constructorInfo;
						}
					}
				}
				if (binder == null)
				{
					binder = Type.DefaultBinder;
				}
				return (ConstructorInfo)binder.SelectMethod(bindingAttr, array2, types, modifiers);
			}
			if (num > 1)
			{
				throw new AmbiguousMatchException();
			}
			return constructorBuilder;
		}

		/// <summary>Determines whether a custom attribute is applied to the current type.</summary>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned.</param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <returns>
		///   <see langword="true" /> if one or more instances of <paramref name="attributeType" />, or an attribute derived from <paramref name="attributeType" />, is defined on this type; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported for incomplete types. Retrieve the type using <see cref="M:System.Type.GetType" /> and call <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> on the returned <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not defined.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is <see langword="null" />.</exception>
		// Token: 0x0600529E RID: 21150 RVA: 0x00103447 File Offset: 0x00101647
		[SecuritySafeCritical]
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (!this.is_created)
			{
				throw new NotSupportedException();
			}
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		/// <summary>Returns all the custom attributes defined for this type.</summary>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <returns>Returns an array of objects representing all the custom attributes of this type.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported for incomplete types. Retrieve the type using <see cref="M:System.Type.GetType" /> and call <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> on the returned <see cref="T:System.Type" />.</exception>
		// Token: 0x0600529F RID: 21151 RVA: 0x0010345F File Offset: 0x0010165F
		[SecuritySafeCritical]
		public override object[] GetCustomAttributes(bool inherit)
		{
			this.check_created();
			return this.created.GetCustomAttributes(inherit);
		}

		/// <summary>Returns all the custom attributes of the current type that are assignable to a specified type.</summary>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned.</param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <returns>An array of custom attributes defined on the current type.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported for incomplete types. Retrieve the type using <see cref="M:System.Type.GetType" /> and call <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> on the returned <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The type must be a type provided by the underlying runtime system.</exception>
		// Token: 0x060052A0 RID: 21152 RVA: 0x00103473 File Offset: 0x00101673
		[SecuritySafeCritical]
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			this.check_created();
			return this.created.GetCustomAttributes(attributeType, inherit);
		}

		/// <summary>Defines a nested type, given its name.</summary>
		/// <param name="name">The short name of the type. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <returns>The defined nested type.</returns>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="name" /> is zero or greater than 1023.  
		///  -or-  
		///  This operation would create a type with a duplicate <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> in the current assembly.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x060052A1 RID: 21153 RVA: 0x00103488 File Offset: 0x00101688
		public TypeBuilder DefineNestedType(string name)
		{
			return this.DefineNestedType(name, TypeAttributes.NestedPrivate, this.pmodule.assemblyb.corlib_object_type, null);
		}

		/// <summary>Defines a nested type, given its name and attributes.</summary>
		/// <param name="name">The short name of the type. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attr">The attributes of the type.</param>
		/// <returns>The defined nested type.</returns>
		/// <exception cref="T:System.ArgumentException">The nested attribute is not specified.  
		///  -or-  
		///  This type is sealed.  
		///  -or-  
		///  This type is an array.  
		///  -or-  
		///  This type is an interface, but the nested type is not an interface.  
		///  -or-  
		///  The length of <paramref name="name" /> is zero or greater than 1023.  
		///  -or-  
		///  This operation would create a type with a duplicate <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> in the current assembly.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x060052A2 RID: 21154 RVA: 0x001034A3 File Offset: 0x001016A3
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr)
		{
			return this.DefineNestedType(name, attr, this.pmodule.assemblyb.corlib_object_type, null);
		}

		/// <summary>Defines a nested type, given its name, attributes, and the type that it extends.</summary>
		/// <param name="name">The short name of the type. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attr">The attributes of the type.</param>
		/// <param name="parent">The type that the nested type extends.</param>
		/// <returns>The defined nested type.</returns>
		/// <exception cref="T:System.ArgumentException">The nested attribute is not specified.  
		///  -or-  
		///  This type is sealed.  
		///  -or-  
		///  This type is an array.  
		///  -or-  
		///  This type is an interface, but the nested type is not an interface.  
		///  -or-  
		///  The length of <paramref name="name" /> is zero or greater than 1023.  
		///  -or-  
		///  This operation would create a type with a duplicate <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> in the current assembly.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x060052A3 RID: 21155 RVA: 0x001034BE File Offset: 0x001016BE
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent)
		{
			return this.DefineNestedType(name, attr, parent, null);
		}

		// Token: 0x060052A4 RID: 21156 RVA: 0x001034CC File Offset: 0x001016CC
		private TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, Type[] interfaces, PackingSize packSize, int typeSize)
		{
			if (interfaces != null)
			{
				for (int i = 0; i < interfaces.Length; i++)
				{
					if (interfaces[i] == null)
					{
						throw new ArgumentNullException("interfaces");
					}
				}
			}
			TypeBuilder typeBuilder = new TypeBuilder(this.pmodule, name, attr, parent, interfaces, packSize, typeSize, this);
			typeBuilder.fullname = typeBuilder.GetFullName();
			this.pmodule.RegisterTypeName(typeBuilder, typeBuilder.fullname);
			if (this.subtypes != null)
			{
				TypeBuilder[] array = new TypeBuilder[this.subtypes.Length + 1];
				Array.Copy(this.subtypes, array, this.subtypes.Length);
				array[this.subtypes.Length] = typeBuilder;
				this.subtypes = array;
			}
			else
			{
				this.subtypes = new TypeBuilder[1];
				this.subtypes[0] = typeBuilder;
			}
			return typeBuilder;
		}

		/// <summary>Defines a nested type, given its name, attributes, the type that it extends, and the interfaces that it implements.</summary>
		/// <param name="name">The short name of the type. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attr">The attributes of the type.</param>
		/// <param name="parent">The type that the nested type extends.</param>
		/// <param name="interfaces">The interfaces that the nested type implements.</param>
		/// <returns>The defined nested type.</returns>
		/// <exception cref="T:System.ArgumentException">The nested attribute is not specified.  
		///  -or-  
		///  This type is sealed.  
		///  -or-  
		///  This type is an array.  
		///  -or-  
		///  This type is an interface, but the nested type is not an interface.  
		///  -or-  
		///  The length of <paramref name="name" /> is zero or greater than 1023.  
		///  -or-  
		///  This operation would create a type with a duplicate <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> in the current assembly.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// An element of the <paramref name="interfaces" /> array is <see langword="null" />.</exception>
		// Token: 0x060052A5 RID: 21157 RVA: 0x0010358E File Offset: 0x0010178E
		[ComVisible(true)]
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, Type[] interfaces)
		{
			return this.DefineNestedType(name, attr, parent, interfaces, PackingSize.Unspecified, 0);
		}

		/// <summary>Defines a nested type, given its name, attributes, the total size of the type, and the type that it extends.</summary>
		/// <param name="name">The short name of the type. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attr">The attributes of the type.</param>
		/// <param name="parent">The type that the nested type extends.</param>
		/// <param name="typeSize">The total size of the type.</param>
		/// <returns>The defined nested type.</returns>
		/// <exception cref="T:System.ArgumentException">The nested attribute is not specified.  
		///  -or-  
		///  This type is sealed.  
		///  -or-  
		///  This type is an array.  
		///  -or-  
		///  This type is an interface, but the nested type is not an interface.  
		///  -or-  
		///  The length of <paramref name="name" /> is zero or greater than 1023.  
		///  -or-  
		///  This operation would create a type with a duplicate <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> in the current assembly.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x060052A6 RID: 21158 RVA: 0x0010359D File Offset: 0x0010179D
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, int typeSize)
		{
			return this.DefineNestedType(name, attr, parent, null, PackingSize.Unspecified, typeSize);
		}

		/// <summary>Defines a nested type, given its name, attributes, the type that it extends, and the packing size.</summary>
		/// <param name="name">The short name of the type. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attr">The attributes of the type.</param>
		/// <param name="parent">The type that the nested type extends.</param>
		/// <param name="packSize">The packing size of the type.</param>
		/// <returns>The defined nested type.</returns>
		/// <exception cref="T:System.ArgumentException">The nested attribute is not specified.  
		///  -or-  
		///  This type is sealed.  
		///  -or-  
		///  This type is an array.  
		///  -or-  
		///  This type is an interface, but the nested type is not an interface.  
		///  -or-  
		///  The length of <paramref name="name" /> is zero or greater than 1023.  
		///  -or-  
		///  This operation would create a type with a duplicate <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> in the current assembly.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		// Token: 0x060052A7 RID: 21159 RVA: 0x001035AC File Offset: 0x001017AC
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, PackingSize packSize)
		{
			return this.DefineNestedType(name, attr, parent, null, packSize, 0);
		}

		/// <summary>Defines a nested type, given its name, attributes, size, and the type that it extends.</summary>
		/// <param name="name">The short name of the type. <paramref name="name" /> cannot contain embedded null values.</param>
		/// <param name="attr">The attributes of the type.</param>
		/// <param name="parent">The type that the nested type extends.</param>
		/// <param name="packSize">The packing size of the type.</param>
		/// <param name="typeSize">The total size of the type.</param>
		/// <returns>The defined nested type.</returns>
		// Token: 0x060052A8 RID: 21160 RVA: 0x001035BB File Offset: 0x001017BB
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, PackingSize packSize, int typeSize)
		{
			return this.DefineNestedType(name, attr, parent, null, packSize, typeSize);
		}

		/// <summary>Adds a new constructor to the type, with the given attributes and signature.</summary>
		/// <param name="attributes">The attributes of the constructor.</param>
		/// <param name="callingConvention">The calling convention of the constructor.</param>
		/// <param name="parameterTypes">The parameter types of the constructor.</param>
		/// <returns>The defined constructor.</returns>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x060052A9 RID: 21161 RVA: 0x001035CB File Offset: 0x001017CB
		[ComVisible(true)]
		public ConstructorBuilder DefineConstructor(MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes)
		{
			return this.DefineConstructor(attributes, callingConvention, parameterTypes, null, null);
		}

		/// <summary>Adds a new constructor to the type, with the given attributes, signature, and custom modifiers.</summary>
		/// <param name="attributes">The attributes of the constructor.</param>
		/// <param name="callingConvention">The calling convention of the constructor.</param>
		/// <param name="parameterTypes">The parameter types of the constructor.</param>
		/// <param name="requiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have required custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <param name="optionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have optional custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <returns>The defined constructor.</returns>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="requiredCustomModifiers" /> or <paramref name="optionalCustomModifiers" /> does not equal the size of <paramref name="parameterTypes" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x060052AA RID: 21162 RVA: 0x001035D8 File Offset: 0x001017D8
		[ComVisible(true)]
		public ConstructorBuilder DefineConstructor(MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
		{
			this.check_not_created();
			ConstructorBuilder constructorBuilder = new ConstructorBuilder(this, attributes, callingConvention, parameterTypes, requiredCustomModifiers, optionalCustomModifiers);
			if (this.ctors != null)
			{
				ConstructorBuilder[] array = new ConstructorBuilder[this.ctors.Length + 1];
				Array.Copy(this.ctors, array, this.ctors.Length);
				array[this.ctors.Length] = constructorBuilder;
				this.ctors = array;
			}
			else
			{
				this.ctors = new ConstructorBuilder[1];
				this.ctors[0] = constructorBuilder;
			}
			return constructorBuilder;
		}

		/// <summary>Defines the default constructor. The constructor defined here will simply call the default constructor of the parent.</summary>
		/// <param name="attributes">A <see langword="MethodAttributes" /> object representing the attributes to be applied to the constructor.</param>
		/// <returns>Returns the constructor.</returns>
		/// <exception cref="T:System.NotSupportedException">The parent type (base type) does not have a default constructor.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x060052AB RID: 21163 RVA: 0x00103650 File Offset: 0x00101850
		[ComVisible(true)]
		public ConstructorBuilder DefineDefaultConstructor(MethodAttributes attributes)
		{
			Type type;
			if (this.parent != null)
			{
				type = this.parent;
			}
			else
			{
				type = this.pmodule.assemblyb.corlib_object_type;
			}
			Type type2 = type;
			type = type.InternalResolve();
			if (type == typeof(object) || type == typeof(ValueType))
			{
				type = type2;
			}
			ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
			if (constructor == null)
			{
				throw new NotSupportedException("Parent does not have a default constructor. The default constructor must be explicitly defined.");
			}
			ConstructorBuilder constructorBuilder = this.DefineConstructor(attributes, CallingConventions.Standard, Type.EmptyTypes);
			ILGenerator ilgenerator = constructorBuilder.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Call, constructor);
			ilgenerator.Emit(OpCodes.Ret);
			return constructorBuilder;
		}

		// Token: 0x060052AC RID: 21164 RVA: 0x0010370C File Offset: 0x0010190C
		private void append_method(MethodBuilder mb)
		{
			if (this.methods != null)
			{
				if (this.methods.Length == this.num_methods)
				{
					MethodBuilder[] destinationArray = new MethodBuilder[this.methods.Length * 2];
					Array.Copy(this.methods, destinationArray, this.num_methods);
					this.methods = destinationArray;
				}
			}
			else
			{
				this.methods = new MethodBuilder[1];
			}
			this.methods[this.num_methods] = mb;
			this.num_methods++;
		}

		/// <summary>Adds a new method to the type, with the specified name, method attributes, and method signature.</summary>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attributes">The attributes of the method.</param>
		/// <param name="returnType">The return type of the method.</param>
		/// <param name="parameterTypes">The types of the parameters of the method.</param>
		/// <returns>The defined method.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.  
		///  -or-  
		///  The type of the parent of this method is an interface, and this method is not virtual (<see langword="Overridable" /> in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x060052AD RID: 21165 RVA: 0x00103784 File Offset: 0x00101984
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, Type returnType, Type[] parameterTypes)
		{
			return this.DefineMethod(name, attributes, CallingConventions.Standard, returnType, parameterTypes);
		}

		/// <summary>Adds a new method to the type, with the specified name, method attributes, calling convention, and method signature.</summary>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attributes">The attributes of the method.</param>
		/// <param name="callingConvention">The calling convention of the method.</param>
		/// <param name="returnType">The return type of the method.</param>
		/// <param name="parameterTypes">The types of the parameters of the method.</param>
		/// <returns>A <see cref="T:System.Reflection.Emit.MethodBuilder" /> representing the newly defined method.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.  
		///  -or-  
		///  The type of the parent of this method is an interface, and this method is not virtual (<see langword="Overridable" /> in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x060052AE RID: 21166 RVA: 0x00103794 File Offset: 0x00101994
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return this.DefineMethod(name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		/// <summary>Adds a new method to the type, with the specified name, method attributes, calling convention, method signature, and custom modifiers.</summary>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attributes">The attributes of the method.</param>
		/// <param name="callingConvention">The calling convention of the method.</param>
		/// <param name="returnType">The return type of the method.</param>
		/// <param name="returnTypeRequiredCustomModifiers">An array of types representing the required custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no required custom modifiers, specify <see langword="null" />.</param>
		/// <param name="returnTypeOptionalCustomModifiers">An array of types representing the optional custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no optional custom modifiers, specify <see langword="null" />.</param>
		/// <param name="parameterTypes">The types of the parameters of the method.</param>
		/// <param name="parameterTypeRequiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have required custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <param name="parameterTypeOptionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have optional custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <returns>A <see cref="T:System.Reflection.Emit.MethodBuilder" /> object representing the newly added method.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.  
		///  -or-  
		///  The type of the parent of this method is an interface, and this method is not virtual (<see langword="Overridable" /> in Visual Basic).  
		///  -or-  
		///  The size of <paramref name="parameterTypeRequiredCustomModifiers" /> or <paramref name="parameterTypeOptionalCustomModifiers" /> does not equal the size of <paramref name="parameterTypes" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x060052AF RID: 21167 RVA: 0x001037B4 File Offset: 0x001019B4
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			this.check_name("name", name);
			this.check_not_created();
			if (base.IsInterface && ((attributes & MethodAttributes.Abstract) == MethodAttributes.PrivateScope || (attributes & MethodAttributes.Virtual) == MethodAttributes.PrivateScope) && (attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				throw new ArgumentException("Interface method must be abstract and virtual.");
			}
			if (returnType == null)
			{
				returnType = this.pmodule.assemblyb.corlib_void_type;
			}
			MethodBuilder methodBuilder = new MethodBuilder(this, name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
			this.append_method(methodBuilder);
			return methodBuilder;
		}

		/// <summary>Defines a <see langword="PInvoke" /> method given its name, the name of the DLL in which the method is defined, the name of the entry point, the attributes of the method, the calling convention of the method, the return type of the method, the types of the parameters of the method, and the <see langword="PInvoke" /> flags.</summary>
		/// <param name="name">The name of the <see langword="PInvoke" /> method. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="dllName">The name of the DLL in which the <see langword="PInvoke" /> method is defined.</param>
		/// <param name="entryName">The name of the entry point in the DLL.</param>
		/// <param name="attributes">The attributes of the method.</param>
		/// <param name="callingConvention">The method's calling convention.</param>
		/// <param name="returnType">The method's return type.</param>
		/// <param name="parameterTypes">The types of the method's parameters.</param>
		/// <param name="nativeCallConv">The native calling convention.</param>
		/// <param name="nativeCharSet">The method's native character set.</param>
		/// <returns>The defined <see langword="PInvoke" /> method.</returns>
		/// <exception cref="T:System.ArgumentException">The method is not static.  
		///  -or-  
		///  The parent type is an interface.  
		///  -or-  
		///  The method is abstract.  
		///  -or-  
		///  The method was previously defined.  
		///  -or-  
		///  The length of <paramref name="name" />, <paramref name="dllName" />, or <paramref name="entryName" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" />, <paramref name="dllName" />, or <paramref name="entryName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x060052B0 RID: 21168 RVA: 0x00103838 File Offset: 0x00101A38
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			return this.DefinePInvokeMethod(name, dllName, entryName, attributes, callingConvention, returnType, null, null, parameterTypes, null, null, nativeCallConv, nativeCharSet);
		}

		/// <summary>Defines a <see langword="PInvoke" /> method given its name, the name of the DLL in which the method is defined, the name of the entry point, the attributes of the method, the calling convention of the method, the return type of the method, the types of the parameters of the method, the <see langword="PInvoke" /> flags, and custom modifiers for the parameters and return type.</summary>
		/// <param name="name">The name of the <see langword="PInvoke" /> method. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="dllName">The name of the DLL in which the <see langword="PInvoke" /> method is defined.</param>
		/// <param name="entryName">The name of the entry point in the DLL.</param>
		/// <param name="attributes">The attributes of the method.</param>
		/// <param name="callingConvention">The method's calling convention.</param>
		/// <param name="returnType">The method's return type.</param>
		/// <param name="returnTypeRequiredCustomModifiers">An array of types representing the required custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no required custom modifiers, specify <see langword="null" />.</param>
		/// <param name="returnTypeOptionalCustomModifiers">An array of types representing the optional custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no optional custom modifiers, specify <see langword="null" />.</param>
		/// <param name="parameterTypes">The types of the method's parameters.</param>
		/// <param name="parameterTypeRequiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have required custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <param name="parameterTypeOptionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have optional custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <param name="nativeCallConv">The native calling convention.</param>
		/// <param name="nativeCharSet">The method's native character set.</param>
		/// <returns>A <see cref="T:System.Reflection.Emit.MethodBuilder" /> representing the defined <see langword="PInvoke" /> method.</returns>
		/// <exception cref="T:System.ArgumentException">The method is not static.  
		///  -or-  
		///  The parent type is an interface.  
		///  -or-  
		///  The method is abstract.  
		///  -or-  
		///  The method was previously defined.  
		///  -or-  
		///  The length of <paramref name="name" />, <paramref name="dllName" />, or <paramref name="entryName" /> is zero.  
		///  -or-  
		///  The size of <paramref name="parameterTypeRequiredCustomModifiers" /> or <paramref name="parameterTypeOptionalCustomModifiers" /> does not equal the size of <paramref name="parameterTypes" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" />, <paramref name="dllName" />, or <paramref name="entryName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x060052B1 RID: 21169 RVA: 0x00103860 File Offset: 0x00101A60
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			this.check_name("name", name);
			this.check_name("dllName", dllName);
			this.check_name("entryName", entryName);
			if ((attributes & MethodAttributes.Abstract) != MethodAttributes.PrivateScope)
			{
				throw new ArgumentException("PInvoke methods must be static and native and cannot be abstract.");
			}
			if (base.IsInterface)
			{
				throw new ArgumentException("PInvoke methods cannot exist on interfaces.");
			}
			this.check_not_created();
			MethodBuilder methodBuilder = new MethodBuilder(this, name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, dllName, entryName, nativeCallConv, nativeCharSet);
			this.append_method(methodBuilder);
			return methodBuilder;
		}

		/// <summary>Defines a <see langword="PInvoke" /> method given its name, the name of the DLL in which the method is defined, the attributes of the method, the calling convention of the method, the return type of the method, the types of the parameters of the method, and the <see langword="PInvoke" /> flags.</summary>
		/// <param name="name">The name of the <see langword="PInvoke" /> method. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="dllName">The name of the DLL in which the <see langword="PInvoke" /> method is defined.</param>
		/// <param name="attributes">The attributes of the method.</param>
		/// <param name="callingConvention">The method's calling convention.</param>
		/// <param name="returnType">The method's return type.</param>
		/// <param name="parameterTypes">The types of the method's parameters.</param>
		/// <param name="nativeCallConv">The native calling convention.</param>
		/// <param name="nativeCharSet">The method's native character set.</param>
		/// <returns>The defined <see langword="PInvoke" /> method.</returns>
		/// <exception cref="T:System.ArgumentException">The method is not static.  
		///  -or-  
		///  The parent type is an interface.  
		///  -or-  
		///  The method is abstract.  
		///  -or-  
		///  The method was previously defined.  
		///  -or-  
		///  The length of <paramref name="name" /> or <paramref name="dllName" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="dllName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x060052B2 RID: 21170 RVA: 0x001038E8 File Offset: 0x00101AE8
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			return this.DefinePInvokeMethod(name, dllName, name, attributes, callingConvention, returnType, parameterTypes, nativeCallConv, nativeCharSet);
		}

		/// <summary>Adds a new method to the type, with the specified name and method attributes.</summary>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attributes">The attributes of the method.</param>
		/// <returns>A <see cref="T:System.Reflection.Emit.MethodBuilder" /> representing the newly defined method.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.  
		///  -or-  
		///  The type of the parent of this method is an interface, and this method is not virtual (<see langword="Overridable" /> in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x060052B3 RID: 21171 RVA: 0x00103909 File Offset: 0x00101B09
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes)
		{
			return this.DefineMethod(name, attributes, CallingConventions.Standard);
		}

		/// <summary>Adds a new method to the type, with the specified name, method attributes, and calling convention.</summary>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attributes">The attributes of the method.</param>
		/// <param name="callingConvention">The calling convention of the method.</param>
		/// <returns>A <see cref="T:System.Reflection.Emit.MethodBuilder" /> representing the newly defined method.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.  
		///  -or-  
		///  The type of the parent of this method is an interface and this method is not virtual (<see langword="Overridable" /> in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x060052B4 RID: 21172 RVA: 0x00103914 File Offset: 0x00101B14
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention)
		{
			return this.DefineMethod(name, attributes, callingConvention, null, null);
		}

		/// <summary>Specifies a given method body that implements a given method declaration, potentially with a different name.</summary>
		/// <param name="methodInfoBody">The method body to be used. This should be a <see langword="MethodBuilder" /> object.</param>
		/// <param name="methodInfoDeclaration">The method whose declaration is to be used.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="methodInfoBody" /> does not belong to this class.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="methodInfoBody" /> or <paramref name="methodInfoDeclaration" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  The declaring type of <paramref name="methodInfoBody" /> is not the type represented by this <see cref="T:System.Reflection.Emit.TypeBuilder" />.</exception>
		// Token: 0x060052B5 RID: 21173 RVA: 0x00103924 File Offset: 0x00101B24
		public void DefineMethodOverride(MethodInfo methodInfoBody, MethodInfo methodInfoDeclaration)
		{
			if (methodInfoBody == null)
			{
				throw new ArgumentNullException("methodInfoBody");
			}
			if (methodInfoDeclaration == null)
			{
				throw new ArgumentNullException("methodInfoDeclaration");
			}
			this.check_not_created();
			if (methodInfoBody.DeclaringType != this)
			{
				throw new ArgumentException("method body must belong to this type");
			}
			if (methodInfoBody is MethodBuilder)
			{
				((MethodBuilder)methodInfoBody).set_override(methodInfoDeclaration);
			}
		}

		/// <summary>Adds a new field to the type, with the given name, attributes, and field type.</summary>
		/// <param name="fieldName">The name of the field. <paramref name="fieldName" /> cannot contain embedded nulls.</param>
		/// <param name="type">The type of the field</param>
		/// <param name="attributes">The attributes of the field.</param>
		/// <returns>The defined field.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="fieldName" /> is zero.  
		///  -or-  
		///  <paramref name="type" /> is System.Void.  
		///  -or-  
		///  A total size was specified for the parent class of this field.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fieldName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x060052B6 RID: 21174 RVA: 0x0010398C File Offset: 0x00101B8C
		public FieldBuilder DefineField(string fieldName, Type type, FieldAttributes attributes)
		{
			return this.DefineField(fieldName, type, null, null, attributes);
		}

		/// <summary>Adds a new field to the type, with the given name, attributes, field type, and custom modifiers.</summary>
		/// <param name="fieldName">The name of the field. <paramref name="fieldName" /> cannot contain embedded nulls.</param>
		/// <param name="type">The type of the field</param>
		/// <param name="requiredCustomModifiers">An array of types representing the required custom modifiers for the field, such as <see cref="T:Microsoft.VisualC.IsConstModifier" />.</param>
		/// <param name="optionalCustomModifiers">An array of types representing the optional custom modifiers for the field, such as <see cref="T:Microsoft.VisualC.IsConstModifier" />.</param>
		/// <param name="attributes">The attributes of the field.</param>
		/// <returns>The defined field.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="fieldName" /> is zero.  
		///  -or-  
		///  <paramref name="type" /> is System.Void.  
		///  -or-  
		///  A total size was specified for the parent class of this field.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fieldName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x060052B7 RID: 21175 RVA: 0x0010399C File Offset: 0x00101B9C
		public FieldBuilder DefineField(string fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
		{
			this.check_name("fieldName", fieldName);
			if (type == typeof(void))
			{
				throw new ArgumentException("Bad field type in defining field.");
			}
			this.check_not_created();
			FieldBuilder fieldBuilder = new FieldBuilder(this, fieldName, type, attributes, requiredCustomModifiers, optionalCustomModifiers);
			if (this.fields != null)
			{
				if (this.fields.Length == this.num_fields)
				{
					FieldBuilder[] destinationArray = new FieldBuilder[this.fields.Length * 2];
					Array.Copy(this.fields, destinationArray, this.num_fields);
					this.fields = destinationArray;
				}
				this.fields[this.num_fields] = fieldBuilder;
				this.num_fields++;
			}
			else
			{
				this.fields = new FieldBuilder[1];
				this.fields[0] = fieldBuilder;
				this.num_fields++;
			}
			if (this.IsEnum && this.underlying_type == null && (attributes & FieldAttributes.Static) == FieldAttributes.PrivateScope)
			{
				this.underlying_type = type;
			}
			return fieldBuilder;
		}

		/// <summary>Adds a new property to the type, with the given name and property signature.</summary>
		/// <param name="name">The name of the property. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attributes">The attributes of the property.</param>
		/// <param name="returnType">The return type of the property.</param>
		/// <param name="parameterTypes">The types of the parameters of the property.</param>
		/// <returns>The defined property.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// Any of the elements of the <paramref name="parameterTypes" /> array is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x060052B8 RID: 21176 RVA: 0x00103A90 File Offset: 0x00101C90
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, Type returnType, Type[] parameterTypes)
		{
			return this.DefineProperty(name, attributes, (CallingConventions)0, returnType, null, null, parameterTypes, null, null);
		}

		/// <summary>Adds a new property to the type, with the given name, attributes, calling convention, and property signature.</summary>
		/// <param name="name">The name of the property. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attributes">The attributes of the property.</param>
		/// <param name="callingConvention">The calling convention of the property accessors.</param>
		/// <param name="returnType">The return type of the property.</param>
		/// <param name="parameterTypes">The types of the parameters of the property.</param>
		/// <returns>The defined property.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// Any of the elements of the <paramref name="parameterTypes" /> array is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x060052B9 RID: 21177 RVA: 0x00103AB0 File Offset: 0x00101CB0
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return this.DefineProperty(name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		/// <summary>Adds a new property to the type, with the given name, property signature, and custom modifiers.</summary>
		/// <param name="name">The name of the property. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attributes">The attributes of the property.</param>
		/// <param name="returnType">The return type of the property.</param>
		/// <param name="returnTypeRequiredCustomModifiers">An array of types representing the required custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the property. If the return type has no required custom modifiers, specify <see langword="null" />.</param>
		/// <param name="returnTypeOptionalCustomModifiers">An array of types representing the optional custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the property. If the return type has no optional custom modifiers, specify <see langword="null" />.</param>
		/// <param name="parameterTypes">The types of the parameters of the property.</param>
		/// <param name="parameterTypeRequiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have required custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <param name="parameterTypeOptionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have optional custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <returns>The defined property.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />  
		/// -or-  
		/// Any of the elements of the <paramref name="parameterTypes" /> array is <see langword="null" /></exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x060052BA RID: 21178 RVA: 0x00103AD0 File Offset: 0x00101CD0
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			return this.DefineProperty(name, attributes, (CallingConventions)0, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
		}

		/// <summary>Adds a new property to the type, with the given name, calling convention, property signature, and custom modifiers.</summary>
		/// <param name="name">The name of the property. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attributes">The attributes of the property.</param>
		/// <param name="callingConvention">The calling convention of the property accessors.</param>
		/// <param name="returnType">The return type of the property.</param>
		/// <param name="returnTypeRequiredCustomModifiers">An array of types representing the required custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the property. If the return type has no required custom modifiers, specify <see langword="null" />.</param>
		/// <param name="returnTypeOptionalCustomModifiers">An array of types representing the optional custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the property. If the return type has no optional custom modifiers, specify <see langword="null" />.</param>
		/// <param name="parameterTypes">The types of the parameters of the property.</param>
		/// <param name="parameterTypeRequiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have required custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <param name="parameterTypeOptionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify <see langword="null" /> instead of an array of types. If none of the parameters have optional custom modifiers, specify <see langword="null" /> instead of an array of arrays.</param>
		/// <returns>The defined property.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// Any of the elements of the <paramref name="parameterTypes" /> array is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x060052BB RID: 21179 RVA: 0x00103AF4 File Offset: 0x00101CF4
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			this.check_name("name", name);
			if (parameterTypes != null)
			{
				for (int i = 0; i < parameterTypes.Length; i++)
				{
					if (parameterTypes[i] == null)
					{
						throw new ArgumentNullException("parameterTypes");
					}
				}
			}
			this.check_not_created();
			PropertyBuilder propertyBuilder = new PropertyBuilder(this, name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
			if (this.properties != null)
			{
				Array.Resize<PropertyBuilder>(ref this.properties, this.properties.Length + 1);
				this.properties[this.properties.Length - 1] = propertyBuilder;
			}
			else
			{
				this.properties = new PropertyBuilder[]
				{
					propertyBuilder
				};
			}
			return propertyBuilder;
		}

		/// <summary>Defines the initializer for this type.</summary>
		/// <returns>Returns a type initializer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x060052BC RID: 21180 RVA: 0x00103B96 File Offset: 0x00101D96
		[ComVisible(true)]
		public ConstructorBuilder DefineTypeInitializer()
		{
			return this.DefineConstructor(MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, CallingConventions.Standard, null);
		}

		// Token: 0x060052BD RID: 21181
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern TypeInfo create_runtime_class();

		// Token: 0x060052BE RID: 21182 RVA: 0x00103BA5 File Offset: 0x00101DA5
		private bool is_nested_in(Type t)
		{
			while (t != null)
			{
				if (t == this)
				{
					return true;
				}
				t = t.DeclaringType;
			}
			return false;
		}

		// Token: 0x060052BF RID: 21183 RVA: 0x00103BC8 File Offset: 0x00101DC8
		private bool has_ctor_method()
		{
			MethodAttributes methodAttributes = MethodAttributes.SpecialName | MethodAttributes.RTSpecialName;
			for (int i = 0; i < this.num_methods; i++)
			{
				MethodBuilder methodBuilder = this.methods[i];
				if (methodBuilder.Name == ConstructorInfo.ConstructorName && (methodBuilder.Attributes & methodAttributes) == methodAttributes)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Creates a <see cref="T:System.Type" /> object for the class. After defining fields and methods on the class, <see langword="CreateType" /> is called in order to load its <see langword="Type" /> object.</summary>
		/// <returns>Returns the new <see cref="T:System.Type" /> object for this class.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enclosing type has not been created.  
		///  -or-  
		///  This type is non-abstract and contains an abstract method.  
		///  -or-  
		///  This type is not an abstract class or an interface and has a method without a method body.</exception>
		/// <exception cref="T:System.NotSupportedException">The type contains invalid Microsoft intermediate language (MSIL) code.  
		///  -or-  
		///  The branch target is specified using a 1-byte offset, but the target is at a distance greater than 127 bytes from the branch.</exception>
		/// <exception cref="T:System.TypeLoadException">The type cannot be loaded. For example, it contains a <see langword="static" /> method that has the calling convention <see cref="F:System.Reflection.CallingConventions.HasThis" />.</exception>
		// Token: 0x060052C0 RID: 21184 RVA: 0x00103C15 File Offset: 0x00101E15
		public Type CreateType()
		{
			return this.CreateTypeInfo();
		}

		/// <summary>Gets a <see cref="T:System.Reflection.TypeInfo" /> object that represents this type.</summary>
		/// <returns>An object that represents this type.</returns>
		// Token: 0x060052C1 RID: 21185 RVA: 0x00103C20 File Offset: 0x00101E20
		public TypeInfo CreateTypeInfo()
		{
			if (this.createTypeCalled)
			{
				return this.created;
			}
			if (!base.IsInterface && this.parent == null && this != this.pmodule.assemblyb.corlib_object_type && this.FullName != "<Module>")
			{
				this.SetParent(this.pmodule.assemblyb.corlib_object_type);
			}
			if (this.fields != null)
			{
				foreach (FieldBuilder fieldBuilder in this.fields)
				{
					if (!(fieldBuilder == null))
					{
						Type fieldType = fieldBuilder.FieldType;
						if (!fieldBuilder.IsStatic && fieldType is TypeBuilder && fieldType.IsValueType && fieldType != this && this.is_nested_in(fieldType))
						{
							TypeBuilder typeBuilder = (TypeBuilder)fieldType;
							if (!typeBuilder.is_created)
							{
								AppDomain.CurrentDomain.DoTypeBuilderResolve(typeBuilder);
								bool is_created = typeBuilder.is_created;
							}
						}
					}
				}
			}
			if (!base.IsInterface && !base.IsValueType && this.ctors == null && this.tname != "<Module>" && ((this.GetAttributeFlagsImpl() & TypeAttributes.Abstract) | TypeAttributes.Sealed) != (TypeAttributes.Abstract | TypeAttributes.Sealed) && !this.has_ctor_method())
			{
				this.DefineDefaultConstructor(MethodAttributes.Public);
			}
			this.createTypeCalled = true;
			if (this.parent != null && this.parent.IsSealed)
			{
				string[] array2 = new string[5];
				array2[0] = "Could not load type '";
				array2[1] = this.FullName;
				array2[2] = "' from assembly '";
				int num = 3;
				Assembly assembly = this.Assembly;
				array2[num] = ((assembly != null) ? assembly.ToString() : null);
				array2[4] = "' because the parent type is sealed.";
				throw new TypeLoadException(string.Concat(array2));
			}
			if (this.parent == this.pmodule.assemblyb.corlib_enum_type && this.methods != null)
			{
				string[] array3 = new string[5];
				array3[0] = "Could not load type '";
				array3[1] = this.FullName;
				array3[2] = "' from assembly '";
				int num2 = 3;
				Assembly assembly2 = this.Assembly;
				array3[num2] = ((assembly2 != null) ? assembly2.ToString() : null);
				array3[4] = "' because it is an enum with methods.";
				throw new TypeLoadException(string.Concat(array3));
			}
			if (this.interfaces != null)
			{
				foreach (Type type in this.interfaces)
				{
					if (type.IsNestedPrivate && type.Assembly != this.Assembly)
					{
						string[] array5 = new string[7];
						array5[0] = "Could not load type '";
						array5[1] = this.FullName;
						array5[2] = "' from assembly '";
						int num3 = 3;
						Assembly assembly3 = this.Assembly;
						array5[num3] = ((assembly3 != null) ? assembly3.ToString() : null);
						array5[4] = "' because it is implements the inaccessible interface '";
						array5[5] = type.FullName;
						array5[6] = "'.";
						throw new TypeLoadException(string.Concat(array5));
					}
				}
			}
			if (this.methods != null)
			{
				bool flag = !base.IsAbstract;
				for (int j = 0; j < this.num_methods; j++)
				{
					MethodBuilder methodBuilder = this.methods[j];
					if (flag && methodBuilder.IsAbstract)
					{
						string str = "Type is concrete but has abstract method ";
						MethodBuilder methodBuilder2 = methodBuilder;
						throw new InvalidOperationException(str + ((methodBuilder2 != null) ? methodBuilder2.ToString() : null));
					}
					methodBuilder.check_override();
					methodBuilder.fixup();
				}
			}
			if (this.ctors != null)
			{
				ConstructorBuilder[] array6 = this.ctors;
				for (int i = 0; i < array6.Length; i++)
				{
					array6[i].fixup();
				}
			}
			this.ResolveUserTypes();
			this.created = this.create_runtime_class();
			if (this.created != null)
			{
				return this.created;
			}
			return this;
		}

		// Token: 0x060052C2 RID: 21186 RVA: 0x00103F9C File Offset: 0x0010219C
		private void ResolveUserTypes()
		{
			this.parent = TypeBuilder.ResolveUserType(this.parent);
			TypeBuilder.ResolveUserTypes(this.interfaces);
			if (this.fields != null)
			{
				foreach (FieldBuilder fieldBuilder in this.fields)
				{
					if (fieldBuilder != null)
					{
						fieldBuilder.ResolveUserTypes();
					}
				}
			}
			if (this.methods != null)
			{
				foreach (MethodBuilder methodBuilder in this.methods)
				{
					if (methodBuilder != null)
					{
						methodBuilder.ResolveUserTypes();
					}
				}
			}
			if (this.ctors != null)
			{
				foreach (ConstructorBuilder constructorBuilder in this.ctors)
				{
					if (constructorBuilder != null)
					{
						constructorBuilder.ResolveUserTypes();
					}
				}
			}
		}

		// Token: 0x060052C3 RID: 21187 RVA: 0x00104060 File Offset: 0x00102260
		internal static void ResolveUserTypes(Type[] types)
		{
			if (types != null)
			{
				for (int i = 0; i < types.Length; i++)
				{
					types[i] = TypeBuilder.ResolveUserType(types[i]);
				}
			}
		}

		// Token: 0x060052C4 RID: 21188 RVA: 0x0010408C File Offset: 0x0010228C
		internal static Type ResolveUserType(Type t)
		{
			if (!(t != null) || (!(t.GetType().Assembly != typeof(int).Assembly) && !(t is TypeDelegator)))
			{
				return t;
			}
			t = t.UnderlyingSystemType;
			if (t != null && (t.GetType().Assembly != typeof(int).Assembly || t is TypeDelegator))
			{
				throw new NotSupportedException("User defined subclasses of System.Type are not yet supported.");
			}
			return t;
		}

		// Token: 0x060052C5 RID: 21189 RVA: 0x00104114 File Offset: 0x00102314
		internal void FixupTokens(Dictionary<int, int> token_map, Dictionary<int, MemberInfo> member_map)
		{
			if (this.methods != null)
			{
				for (int i = 0; i < this.num_methods; i++)
				{
					this.methods[i].FixupTokens(token_map, member_map);
				}
			}
			if (this.ctors != null)
			{
				ConstructorBuilder[] array = this.ctors;
				for (int j = 0; j < array.Length; j++)
				{
					array[j].FixupTokens(token_map, member_map);
				}
			}
			if (this.subtypes != null)
			{
				TypeBuilder[] array2 = this.subtypes;
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j].FixupTokens(token_map, member_map);
				}
			}
		}

		// Token: 0x060052C6 RID: 21190 RVA: 0x00104198 File Offset: 0x00102398
		internal void GenerateDebugInfo(ISymbolWriter symbolWriter)
		{
			symbolWriter.OpenNamespace(this.Namespace);
			if (this.methods != null)
			{
				for (int i = 0; i < this.num_methods; i++)
				{
					this.methods[i].GenerateDebugInfo(symbolWriter);
				}
			}
			if (this.ctors != null)
			{
				ConstructorBuilder[] array = this.ctors;
				for (int j = 0; j < array.Length; j++)
				{
					array[j].GenerateDebugInfo(symbolWriter);
				}
			}
			symbolWriter.CloseNamespace();
			if (this.subtypes != null)
			{
				for (int k = 0; k < this.subtypes.Length; k++)
				{
					this.subtypes[k].GenerateDebugInfo(symbolWriter);
				}
			}
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing the public and non-public constructors defined for this class, as specified.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> as in <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing the specified constructors defined for this class. If no constructors are defined, an empty array is returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x060052C7 RID: 21191 RVA: 0x0010422D File Offset: 0x0010242D
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			if (this.is_created)
			{
				return this.created.GetConstructors(bindingAttr);
			}
			throw new NotSupportedException();
		}

		// Token: 0x060052C8 RID: 21192 RVA: 0x0010424C File Offset: 0x0010244C
		internal ConstructorInfo[] GetConstructorsInternal(BindingFlags bindingAttr)
		{
			if (this.ctors == null)
			{
				return new ConstructorInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			foreach (ConstructorBuilder constructorBuilder in this.ctors)
			{
				bool flag = false;
				MethodAttributes attributes = constructorBuilder.Attributes;
				if ((attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
				{
					if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
					{
						flag = true;
					}
				}
				else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
				{
					flag = true;
				}
				if (flag)
				{
					flag = false;
					if ((attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
					{
						if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						arrayList.Add(constructorBuilder);
					}
				}
			}
			ConstructorInfo[] array2 = new ConstructorInfo[arrayList.Count];
			arrayList.CopyTo(array2);
			return array2;
		}

		/// <summary>Calling this method always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>This method is not supported. No value is returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x060052C9 RID: 21193 RVA: 0x000472C8 File Offset: 0x000454C8
		public override Type GetElementType()
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns the event with the specified name.</summary>
		/// <param name="name">The name of the event to search for.</param>
		/// <param name="bindingAttr">A bitwise combination of <see cref="T:System.Reflection.BindingFlags" /> values that limits the search.</param>
		/// <returns>An <see cref="T:System.Reflection.EventInfo" /> object representing the event declared or inherited by this type with the specified name, or <see langword="null" /> if there are no matches.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x060052CA RID: 21194 RVA: 0x001042ED File Offset: 0x001024ED
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			this.check_created();
			return this.created.GetEvent(name, bindingAttr);
		}

		/// <summary>Returns the public events declared or inherited by this type.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.EventInfo" /> objects representing the public events declared or inherited by this type. An empty array is returned if there are no public events.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x060052CB RID: 21195 RVA: 0x0004751C File Offset: 0x0004571C
		public override EventInfo[] GetEvents()
		{
			return this.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Returns the public and non-public events that are declared by this type.</summary>
		/// <param name="bindingAttr">A bitwise combination of <see cref="T:System.Reflection.BindingFlags" /> values that limits the search.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.EventInfo" /> objects representing the events declared or inherited by this type that match the specified binding flags. An empty array is returned if there are no matching events.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x060052CC RID: 21196 RVA: 0x00104302 File Offset: 0x00102502
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			if (this.is_created)
			{
				return this.created.GetEvents(bindingAttr);
			}
			throw new NotSupportedException();
		}

		/// <summary>Returns the field specified by the given name.</summary>
		/// <param name="name">The name of the field to get.</param>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> as in <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns the <see cref="T:System.Reflection.FieldInfo" /> object representing the field declared or inherited by this type with the specified name and public or non-public modifier. If there are no matches then <see langword="null" /> is returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x060052CD RID: 21197 RVA: 0x00104320 File Offset: 0x00102520
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			if (this.created != null)
			{
				return this.created.GetField(name, bindingAttr);
			}
			if (this.fields == null)
			{
				return null;
			}
			foreach (FieldBuilder fieldInfo in this.fields)
			{
				if (!(fieldInfo == null) && !(fieldInfo.Name != name))
				{
					bool flag = false;
					FieldAttributes attributes = fieldInfo.Attributes;
					if ((attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public)
					{
						if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						flag = false;
						if ((attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope)
						{
							if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
							{
								flag = true;
							}
						}
						else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag = true;
						}
						if (flag)
						{
							return fieldInfo;
						}
					}
				}
			}
			return null;
		}

		/// <summary>Returns the public and non-public fields that are declared by this type.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.FieldInfo" /> objects representing the public and non-public fields declared or inherited by this type. An empty array is returned if there are no fields, as specified.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x060052CE RID: 21198 RVA: 0x001043CC File Offset: 0x001025CC
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			if (this.created != null)
			{
				return this.created.GetFields(bindingAttr);
			}
			if (this.fields == null)
			{
				return new FieldInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			foreach (FieldBuilder fieldInfo in this.fields)
			{
				if (!(fieldInfo == null))
				{
					bool flag = false;
					FieldAttributes attributes = fieldInfo.Attributes;
					if ((attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public)
					{
						if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						flag = false;
						if ((attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope)
						{
							if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
							{
								flag = true;
							}
						}
						else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag = true;
						}
						if (flag)
						{
							arrayList.Add(fieldInfo);
						}
					}
				}
			}
			FieldInfo[] array2 = new FieldInfo[arrayList.Count];
			arrayList.CopyTo(array2);
			return array2;
		}

		/// <summary>Returns the interface implemented (directly or indirectly) by this class with the fully qualified name matching the given interface name.</summary>
		/// <param name="name">The name of the interface.</param>
		/// <param name="ignoreCase">If <see langword="true" />, the search is case-insensitive. If <see langword="false" />, the search is case-sensitive.</param>
		/// <returns>Returns a <see cref="T:System.Type" /> object representing the implemented interface. Returns null if no interface matching name is found.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x060052CF RID: 21199 RVA: 0x00104492 File Offset: 0x00102692
		public override Type GetInterface(string name, bool ignoreCase)
		{
			this.check_created();
			return this.created.GetInterface(name, ignoreCase);
		}

		/// <summary>Returns an array of all the interfaces implemented on this type and its base types.</summary>
		/// <returns>Returns an array of <see cref="T:System.Type" /> objects representing the implemented interfaces. If none are defined, an empty array is returned.</returns>
		// Token: 0x060052D0 RID: 21200 RVA: 0x001044A8 File Offset: 0x001026A8
		public override Type[] GetInterfaces()
		{
			if (this.is_created)
			{
				return this.created.GetInterfaces();
			}
			if (this.interfaces != null)
			{
				Type[] array = new Type[this.interfaces.Length];
				this.interfaces.CopyTo(array, 0);
				return array;
			}
			return Type.EmptyTypes;
		}

		/// <summary>Returns all the public and non-public members declared or inherited by this type, as specified.</summary>
		/// <param name="name">The name of the member.</param>
		/// <param name="type">The type of the member to return.</param>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, as in <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public and non-public members defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public members are returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x060052D1 RID: 21201 RVA: 0x001044F3 File Offset: 0x001026F3
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			this.check_created();
			return this.created.GetMember(name, type, bindingAttr);
		}

		/// <summary>Returns the members for the public and non-public members declared or inherited by this type.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public and non-public members declared or inherited by this type. An empty array is returned if there are no matching members.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x060052D2 RID: 21202 RVA: 0x00104509 File Offset: 0x00102709
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			this.check_created();
			return this.created.GetMembers(bindingAttr);
		}

		// Token: 0x060052D3 RID: 21203 RVA: 0x00104520 File Offset: 0x00102720
		private MethodInfo[] GetMethodsByName(string name, BindingFlags bindingAttr, bool ignoreCase, Type reflected_type)
		{
			MethodInfo[] array2;
			if ((bindingAttr & BindingFlags.DeclaredOnly) == BindingFlags.Default && this.parent != null)
			{
				MethodInfo[] array = this.parent.GetMethods(bindingAttr);
				ArrayList arrayList = new ArrayList(array.Length);
				bool flag = (bindingAttr & BindingFlags.FlattenHierarchy) > BindingFlags.Default;
				foreach (MethodInfo methodInfo in array)
				{
					MethodAttributes attributes = methodInfo.Attributes;
					if (!methodInfo.IsStatic || flag)
					{
						MethodAttributes methodAttributes = attributes & MethodAttributes.MemberAccessMask;
						bool flag2;
						if (methodAttributes != MethodAttributes.Private)
						{
							if (methodAttributes != MethodAttributes.Assembly)
							{
								if (methodAttributes == MethodAttributes.Public)
								{
									flag2 = ((bindingAttr & BindingFlags.Public) > BindingFlags.Default);
								}
								else
								{
									flag2 = ((bindingAttr & BindingFlags.NonPublic) > BindingFlags.Default);
								}
							}
							else
							{
								flag2 = ((bindingAttr & BindingFlags.NonPublic) > BindingFlags.Default);
							}
						}
						else
						{
							flag2 = false;
						}
						if (flag2)
						{
							arrayList.Add(methodInfo);
						}
					}
				}
				if (this.methods == null)
				{
					array2 = new MethodInfo[arrayList.Count];
					arrayList.CopyTo(array2);
				}
				else
				{
					array2 = new MethodInfo[this.methods.Length + arrayList.Count];
					arrayList.CopyTo(array2, 0);
					this.methods.CopyTo(array2, arrayList.Count);
				}
			}
			else
			{
				MethodInfo[] array3 = this.methods;
				array2 = array3;
			}
			if (array2 == null)
			{
				return new MethodInfo[0];
			}
			ArrayList arrayList2 = new ArrayList();
			foreach (MethodInfo methodInfo2 in array2)
			{
				if (!(methodInfo2 == null) && (name == null || string.Compare(methodInfo2.Name, name, ignoreCase) == 0))
				{
					bool flag2 = false;
					MethodAttributes attributes = methodInfo2.Attributes;
					if ((attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
					{
						if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
						{
							flag2 = true;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag2 = true;
					}
					if (flag2)
					{
						flag2 = false;
						if ((attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
						{
							if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
							{
								flag2 = true;
							}
						}
						else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag2 = true;
						}
						if (flag2)
						{
							arrayList2.Add(methodInfo2);
						}
					}
				}
			}
			MethodInfo[] array4 = new MethodInfo[arrayList2.Count];
			arrayList2.CopyTo(array4);
			return array4;
		}

		/// <summary>Returns all the public and non-public methods declared or inherited by this type, as specified.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> as in <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MethodInfo" /> objects representing the public and non-public methods defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public methods are returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x060052D4 RID: 21204 RVA: 0x001046E0 File Offset: 0x001028E0
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.GetMethodsByName(null, bindingAttr, false, this);
		}

		// Token: 0x060052D5 RID: 21205 RVA: 0x001046EC File Offset: 0x001028EC
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			this.check_created();
			if (types == null)
			{
				return this.created.GetMethod(name, bindingAttr);
			}
			return this.created.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns the public and non-public nested types that are declared by this type.</summary>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the nested type to get.</param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.  
		///  -or-  
		///  Zero, to conduct a case-sensitive search for public methods.</param>
		/// <returns>A <see cref="T:System.Type" /> object representing the nested type that matches the specified requirements, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x060052D6 RID: 21206 RVA: 0x0010471C File Offset: 0x0010291C
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			this.check_created();
			if (this.subtypes == null)
			{
				return null;
			}
			foreach (TypeBuilder typeBuilder in this.subtypes)
			{
				if (typeBuilder.is_created)
				{
					if ((typeBuilder.attrs & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPublic)
					{
						if ((bindingAttr & BindingFlags.Public) == BindingFlags.Default)
						{
							goto IL_55;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) == BindingFlags.Default)
					{
						goto IL_55;
					}
					if (typeBuilder.Name == name)
					{
						return typeBuilder.created;
					}
				}
				IL_55:;
			}
			return null;
		}

		/// <summary>Returns the public and non-public nested types that are declared or inherited by this type.</summary>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, as in <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing all the types nested within the current <see cref="T:System.Type" /> that match the specified binding constraints.  
		///  An empty array of type <see cref="T:System.Type" />, if no types are nested within the current <see cref="T:System.Type" />, or if none of the nested types match the binding constraints.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x060052D7 RID: 21207 RVA: 0x0010478C File Offset: 0x0010298C
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			if (!this.is_created)
			{
				throw new NotSupportedException();
			}
			ArrayList arrayList = new ArrayList();
			if (this.subtypes == null)
			{
				return Type.EmptyTypes;
			}
			foreach (TypeBuilder typeBuilder in this.subtypes)
			{
				bool flag = false;
				if ((typeBuilder.attrs & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPublic)
				{
					if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
					{
						flag = true;
					}
				}
				else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
				{
					flag = true;
				}
				if (flag)
				{
					arrayList.Add(typeBuilder);
				}
			}
			Type[] array2 = new Type[arrayList.Count];
			arrayList.CopyTo(array2);
			return array2;
		}

		/// <summary>Returns all the public and non-public properties declared or inherited by this type, as specified.</summary>
		/// <param name="bindingAttr">This invocation attribute. This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="NonPublic" />, and so on.</param>
		/// <returns>Returns an array of <see langword="PropertyInfo" /> objects representing the public and non-public properties defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public properties are returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x060052D8 RID: 21208 RVA: 0x0010481C File Offset: 0x00102A1C
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			if (this.is_created)
			{
				return this.created.GetProperties(bindingAttr);
			}
			if (this.properties == null)
			{
				return new PropertyInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			foreach (PropertyBuilder propertyInfo in this.properties)
			{
				bool flag = false;
				MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
				if (methodInfo == null)
				{
					methodInfo = propertyInfo.GetSetMethod(true);
				}
				if (!(methodInfo == null))
				{
					MethodAttributes attributes = methodInfo.Attributes;
					if ((attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
					{
						if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						flag = false;
						if ((attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
						{
							if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
							{
								flag = true;
							}
						}
						else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag = true;
						}
						if (flag)
						{
							arrayList.Add(propertyInfo);
						}
					}
				}
			}
			PropertyInfo[] array2 = new PropertyInfo[arrayList.Count];
			arrayList.CopyTo(array2);
			return array2;
		}

		// Token: 0x060052D9 RID: 21209 RVA: 0x001048FB File Offset: 0x00102AFB
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		// Token: 0x060052DA RID: 21210 RVA: 0x00104903 File Offset: 0x00102B03
		protected override bool HasElementTypeImpl()
		{
			return this.is_created && this.created.HasElementType;
		}

		/// <summary>Invokes the specified member. The method that is to be invoked must be accessible and provide the most specific match with the specified argument list, under the constraints of the specified binder and invocation attributes.</summary>
		/// <param name="name">The name of the member to invoke. This can be a constructor, method, property, or field. A suitable invocation attribute must be specified. Note that it is possible to invoke the default member of a class by passing an empty string as the name of the member.</param>
		/// <param name="invokeAttr">The invocation attribute. This must be a bit flag from <see langword="BindingFlags" />.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects using reflection. If binder is <see langword="null" />, the default binder is used. See <see cref="T:System.Reflection.Binder" />.</param>
		/// <param name="target">The object on which to invoke the specified member. If the member is static, this parameter is ignored.</param>
		/// <param name="args">An argument list. This is an array of Objects that contains the number, order, and type of the parameters of the member to be invoked. If there are no parameters this should be null.</param>
		/// <param name="modifiers">An array of the same length as <paramref name="args" /> with elements that represent the attributes associated with the arguments of the member to be invoked. A parameter has attributes associated with it in the metadata. They are used by various interoperability services. See the metadata specs for more details.</param>
		/// <param name="culture">An instance of <see langword="CultureInfo" /> used to govern the coercion of types. If this is null, the <see langword="CultureInfo" /> for the current thread is used. (Note that this is necessary to, for example, convert a String that represents 1000 to a Double value, since 1000 is represented differently by different cultures.)</param>
		/// <param name="namedParameters">Each parameter in the <paramref name="namedParameters" /> array gets the value in the corresponding element in the <paramref name="args" /> array. If the length of <paramref name="args" /> is greater than the length of <paramref name="namedParameters" />, the remaining argument values are passed in order.</param>
		/// <returns>Returns the return value of the invoked member.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported for incomplete types.</exception>
		// Token: 0x060052DB RID: 21211 RVA: 0x0010491C File Offset: 0x00102B1C
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			this.check_created();
			return this.created.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x060052DD RID: 21213 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x060052DE RID: 21214 RVA: 0x00047302 File Offset: 0x00045502
		protected override bool IsCOMObjectImpl()
		{
			return (this.GetAttributeFlagsImpl() & TypeAttributes.Import) > TypeAttributes.NotPublic;
		}

		// Token: 0x060052DF RID: 21215 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x060052E0 RID: 21216 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x060052E1 RID: 21217 RVA: 0x00104948 File Offset: 0x00102B48
		protected override bool IsValueTypeImpl()
		{
			if (this == this.pmodule.assemblyb.corlib_value_type || this == this.pmodule.assemblyb.corlib_enum_type)
			{
				return false;
			}
			Type baseType = this.parent;
			while (baseType != null)
			{
				if (baseType == this.pmodule.assemblyb.corlib_value_type)
				{
					return true;
				}
				baseType = baseType.BaseType;
			}
			return false;
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a one-dimensional array of the current type, with a lower bound of zero.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing a one-dimensional array type whose element type is the current type, with a lower bound of zero.</returns>
		// Token: 0x060052E2 RID: 21218 RVA: 0x000F6525 File Offset: 0x000F4725
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents an array of the current type, with the specified number of dimensions.</summary>
		/// <param name="rank">The number of dimensions for the array.</param>
		/// <returns>A <see cref="T:System.Type" /> object that represents a one-dimensional array of the current type.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="rank" /> is not a valid array dimension.</exception>
		// Token: 0x060052E3 RID: 21219 RVA: 0x000F652E File Offset: 0x000F472E
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents the current type when passed as a <see langword="ref" /> parameter (<see langword="ByRef" /> in Visual Basic).</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the current type when passed as a <see langword="ref" /> parameter (<see langword="ByRef" /> in Visual Basic).</returns>
		// Token: 0x060052E4 RID: 21220 RVA: 0x000F6541 File Offset: 0x000F4741
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		/// <summary>Substitutes the elements of an array of types for the type parameters of the current generic type definition, and returns the resulting constructed type.</summary>
		/// <param name="typeArguments">An array of types to be substituted for the type parameters of the current generic type definition.</param>
		/// <returns>A <see cref="T:System.Type" /> representing the constructed type formed by substituting the elements of <paramref name="typeArguments" /> for the type parameters of the current generic type.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current type does not represent the definition of a generic type. That is, <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> returns <see langword="false" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeArguments" /> is <see langword="null" />.  
		/// -or-  
		/// Any element of <paramref name="typeArguments" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Type.Module" /> property of any element of <paramref name="typeArguments" /> is <see langword="null" />.  
		///  -or-  
		///  The <see cref="P:System.Reflection.Module.Assembly" /> property of the module of any element of <paramref name="typeArguments" /> is <see langword="null" />.</exception>
		// Token: 0x060052E5 RID: 21221 RVA: 0x001049BC File Offset: 0x00102BBC
		public override Type MakeGenericType(params Type[] typeArguments)
		{
			if (!this.IsGenericTypeDefinition)
			{
				throw new InvalidOperationException("not a generic type definition");
			}
			if (typeArguments == null)
			{
				throw new ArgumentNullException("typeArguments");
			}
			if (this.generic_params.Length != typeArguments.Length)
			{
				throw new ArgumentException(string.Format("The type or method has {0} generic parameter(s) but {1} generic argument(s) where provided. A generic argument must be provided for each generic parameter.", this.generic_params.Length, typeArguments.Length), "typeArguments");
			}
			for (int i = 0; i < typeArguments.Length; i++)
			{
				if (typeArguments[i] == null)
				{
					throw new ArgumentNullException("typeArguments");
				}
			}
			Type[] array = new Type[typeArguments.Length];
			typeArguments.CopyTo(array, 0);
			return this.pmodule.assemblyb.MakeGenericType(this, array);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents the type of an unmanaged pointer to the current type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the type of an unmanaged pointer to the current type.</returns>
		// Token: 0x060052E6 RID: 21222 RVA: 0x000F6549 File Offset: 0x000F4749
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		/// <summary>Not supported in dynamic modules.</summary>
		/// <returns>Read-only.</returns>
		/// <exception cref="T:System.NotSupportedException">Not supported in dynamic modules.</exception>
		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x060052E7 RID: 21223 RVA: 0x00104A6A File Offset: 0x00102C6A
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				this.check_created();
				return this.created.TypeHandle;
			}
		}

		/// <summary>Set a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customBuilder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x060052E8 RID: 21224 RVA: 0x00104A80 File Offset: 0x00102C80
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			string fullName = customBuilder.Ctor.ReflectedType.FullName;
			if (fullName == "System.Runtime.InteropServices.StructLayoutAttribute")
			{
				byte[] data = customBuilder.Data;
				int num = (int)data[2] | (int)data[3] << 8;
				this.attrs &= ~TypeAttributes.LayoutMask;
				switch (num)
				{
				case 0:
					this.attrs |= TypeAttributes.SequentialLayout;
					goto IL_A5;
				case 2:
					this.attrs |= TypeAttributes.ExplicitLayout;
					goto IL_A5;
				case 3:
					this.attrs |= TypeAttributes.NotPublic;
					goto IL_A5;
				}
				throw new Exception("Error in customattr");
				IL_A5:
				Type type = (customBuilder.Ctor is ConstructorBuilder) ? ((ConstructorBuilder)customBuilder.Ctor).parameters[0] : customBuilder.Ctor.GetParametersInternal()[0].ParameterType;
				int num2 = 6;
				if (type.FullName == "System.Int16")
				{
					num2 = 4;
				}
				int num3 = (int)data[num2++];
				num3 |= (int)data[num2++] << 8;
				for (int i = 0; i < num3; i++)
				{
					num2++;
					int num4;
					if (data[num2++] == 85)
					{
						num4 = CustomAttributeBuilder.decode_len(data, num2, out num2);
						CustomAttributeBuilder.string_from_bytes(data, num2, num4);
						num2 += num4;
					}
					num4 = CustomAttributeBuilder.decode_len(data, num2, out num2);
					string a = CustomAttributeBuilder.string_from_bytes(data, num2, num4);
					num2 += num4;
					int num5 = (int)data[num2++];
					num5 |= (int)data[num2++] << 8;
					num5 |= (int)data[num2++] << 16;
					num5 |= (int)data[num2++] << 24;
					if (!(a == "CharSet"))
					{
						if (!(a == "Pack"))
						{
							if (a == "Size")
							{
								this.class_size = num5;
							}
						}
						else
						{
							this.packing_size = (PackingSize)num5;
						}
					}
					else
					{
						switch (num5)
						{
						case 1:
						case 2:
							this.attrs &= ~TypeAttributes.StringFormatMask;
							break;
						case 3:
							this.attrs &= ~TypeAttributes.AutoClass;
							this.attrs |= TypeAttributes.UnicodeClass;
							break;
						case 4:
							this.attrs &= ~TypeAttributes.UnicodeClass;
							this.attrs |= TypeAttributes.AutoClass;
							break;
						}
					}
				}
				return;
			}
			if (fullName == "System.Runtime.CompilerServices.SpecialNameAttribute")
			{
				this.attrs |= TypeAttributes.SpecialName;
				return;
			}
			if (fullName == "System.SerializableAttribute")
			{
				this.attrs |= TypeAttributes.Serializable;
				return;
			}
			if (fullName == "System.Runtime.InteropServices.ComImportAttribute")
			{
				this.attrs |= TypeAttributes.Import;
				return;
			}
			if (fullName == "System.Security.SuppressUnmanagedCodeSecurityAttribute")
			{
				this.attrs |= TypeAttributes.HasSecurity;
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
		/// <exception cref="T:System.InvalidOperationException">For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		// Token: 0x060052E9 RID: 21225 RVA: 0x00104DB4 File Offset: 0x00102FB4
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		/// <summary>Adds a new event to the type, with the given name, attributes and event type.</summary>
		/// <param name="name">The name of the event. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="attributes">The attributes of the event.</param>
		/// <param name="eventtype">The type of the event.</param>
		/// <returns>The defined event.</returns>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="eventtype" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x060052EA RID: 21226 RVA: 0x00104DC4 File Offset: 0x00102FC4
		public EventBuilder DefineEvent(string name, EventAttributes attributes, Type eventtype)
		{
			this.check_name("name", name);
			if (eventtype == null)
			{
				throw new ArgumentNullException("type");
			}
			this.check_not_created();
			EventBuilder eventBuilder = new EventBuilder(this, name, attributes, eventtype);
			if (this.events != null)
			{
				EventBuilder[] array = new EventBuilder[this.events.Length + 1];
				Array.Copy(this.events, array, this.events.Length);
				array[this.events.Length] = eventBuilder;
				this.events = array;
			}
			else
			{
				this.events = new EventBuilder[1];
				this.events[0] = eventBuilder;
			}
			return eventBuilder;
		}

		/// <summary>Defines initialized data field in the .sdata section of the portable executable (PE) file.</summary>
		/// <param name="name">The name used to refer to the data. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="data">The blob of data.</param>
		/// <param name="attributes">The attributes for the field.</param>
		/// <returns>A field to reference the data.</returns>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="name" /> is zero.  
		///  -or-  
		///  The size of the data is less than or equal to zero, or greater than or equal to 0x3f0000.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="data" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been previously called.</exception>
		// Token: 0x060052EB RID: 21227 RVA: 0x00104E57 File Offset: 0x00103057
		public FieldBuilder DefineInitializedData(string name, byte[] data, FieldAttributes attributes)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			FieldBuilder fieldBuilder = this.DefineUninitializedData(name, data.Length, attributes);
			fieldBuilder.SetRVAData(data);
			return fieldBuilder;
		}

		/// <summary>Defines an uninitialized data field in the <see langword=".sdata" /> section of the portable executable (PE) file.</summary>
		/// <param name="name">The name used to refer to the data. <paramref name="name" /> cannot contain embedded nulls.</param>
		/// <param name="size">The size of the data field.</param>
		/// <param name="attributes">The attributes for the field.</param>
		/// <returns>A field to reference the data.</returns>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="name" /> is zero.  
		///  -or-  
		///  <paramref name="size" /> is less than or equal to zero, or greater than or equal to 0x003f0000.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x060052EC RID: 21228 RVA: 0x00104E7C File Offset: 0x0010307C
		public FieldBuilder DefineUninitializedData(string name, int size, FieldAttributes attributes)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal", "name");
			}
			if (size <= 0 || size > 4128768)
			{
				throw new ArgumentException("Data size must be > 0 and < 0x3f0000");
			}
			this.check_not_created();
			string text = "$ArrayType$" + size.ToString();
			TypeIdentifier innerName = TypeIdentifiers.WithoutEscape(text);
			Type type = this.pmodule.GetRegisteredType(this.fullname.NestedName(innerName));
			if (type == null)
			{
				TypeBuilder typeBuilder = this.DefineNestedType(text, TypeAttributes.Public | TypeAttributes.NestedPublic | TypeAttributes.ExplicitLayout | TypeAttributes.Sealed, this.pmodule.assemblyb.corlib_value_type, null, PackingSize.Size1, size);
				typeBuilder.CreateType();
				type = typeBuilder;
			}
			return this.DefineField(name, type, attributes | FieldAttributes.Static | FieldAttributes.HasFieldRVA);
		}

		/// <summary>Returns the type token of this type.</summary>
		/// <returns>Read-only. Returns the <see langword="TypeToken" /> of this type.</returns>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x060052ED RID: 21229 RVA: 0x00104F3F File Offset: 0x0010313F
		public TypeToken TypeToken
		{
			get
			{
				return new TypeToken(33554432 | this.table_idx);
			}
		}

		/// <summary>Sets the base type of the type currently under construction.</summary>
		/// <param name="parent">The new base type.</param>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.  
		///  -or-  
		///  <paramref name="parent" /> is <see langword="null" />, and the current instance represents an interface whose attributes do not include <see cref="F:System.Reflection.TypeAttributes.Abstract" />.  
		///  -or-  
		///  For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is <see langword="true" />, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is <see langword="false" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="parent" /> is an interface. This exception condition is new in the .NET Framework version 2.0.</exception>
		// Token: 0x060052EE RID: 21230 RVA: 0x00104F54 File Offset: 0x00103154
		public void SetParent(Type parent)
		{
			this.check_not_created();
			if (parent == null)
			{
				if ((this.attrs & TypeAttributes.ClassSemanticsMask) != TypeAttributes.NotPublic)
				{
					if ((this.attrs & TypeAttributes.Abstract) == TypeAttributes.NotPublic)
					{
						throw new InvalidOperationException("Interface must be declared abstract.");
					}
					this.parent = null;
				}
				else
				{
					this.parent = typeof(object);
				}
			}
			else
			{
				this.parent = parent;
			}
			this.parent = TypeBuilder.ResolveUserType(this.parent);
		}

		// Token: 0x060052EF RID: 21231 RVA: 0x00104FC7 File Offset: 0x001031C7
		internal int get_next_table_index(object obj, int table, int count)
		{
			return this.pmodule.get_next_table_index(obj, table, count);
		}

		/// <summary>Returns an interface mapping for the requested interface.</summary>
		/// <param name="interfaceType">The <see cref="T:System.Type" /> of the interface for which the mapping is to be retrieved.</param>
		/// <returns>Returns the requested interface mapping.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types.</exception>
		// Token: 0x060052F0 RID: 21232 RVA: 0x00104FD7 File Offset: 0x001031D7
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			if (this.created == null)
			{
				throw new NotSupportedException("This method is not implemented for incomplete types.");
			}
			return this.created.GetInterfaceMap(interfaceType);
		}

		// Token: 0x060052F1 RID: 21233 RVA: 0x00104FFE File Offset: 0x001031FE
		internal override Type InternalResolve()
		{
			this.check_created();
			return this.created;
		}

		// Token: 0x060052F2 RID: 21234 RVA: 0x00104FFE File Offset: 0x001031FE
		internal override Type RuntimeResolve()
		{
			this.check_created();
			return this.created;
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x060052F3 RID: 21235 RVA: 0x0010500C File Offset: 0x0010320C
		internal bool is_created
		{
			get
			{
				return this.createTypeCalled;
			}
		}

		// Token: 0x060052F4 RID: 21236 RVA: 0x000F79BE File Offset: 0x000F5BBE
		private Exception not_supported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x060052F5 RID: 21237 RVA: 0x00105014 File Offset: 0x00103214
		private void check_not_created()
		{
			if (this.is_created)
			{
				throw new InvalidOperationException("Unable to change after type has been created.");
			}
		}

		// Token: 0x060052F6 RID: 21238 RVA: 0x00105029 File Offset: 0x00103229
		private void check_created()
		{
			if (!this.is_created)
			{
				throw this.not_supported();
			}
		}

		// Token: 0x060052F7 RID: 21239 RVA: 0x0010503A File Offset: 0x0010323A
		private void check_name(string argName, string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(argName);
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal", argName);
			}
			if (name[0] == '\0')
			{
				throw new ArgumentException("Illegal name", argName);
			}
		}

		/// <summary>Returns the name of the type excluding the namespace.</summary>
		/// <returns>Read-only. The name of the type excluding the namespace.</returns>
		// Token: 0x060052F8 RID: 21240 RVA: 0x0010506F File Offset: 0x0010326F
		public override string ToString()
		{
			return this.FullName;
		}

		/// <summary>Gets a value that indicates whether a specified <see cref="T:System.Type" /> can be assigned to this object.</summary>
		/// <param name="c">The object to test.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="c" /> parameter and the current type represent the same type, or if the current type is in the inheritance hierarchy of <paramref name="c" />, or if the current type is an interface that <paramref name="c" /> supports. <see langword="false" /> if none of these conditions are valid, or if <paramref name="c" /> is <see langword="null" />.</returns>
		// Token: 0x060052F9 RID: 21241 RVA: 0x00105077 File Offset: 0x00103277
		[MonoTODO]
		public override bool IsAssignableFrom(Type c)
		{
			return base.IsAssignableFrom(c);
		}

		// Token: 0x060052FA RID: 21242 RVA: 0x00105080 File Offset: 0x00103280
		[MonoTODO("arrays")]
		internal bool IsAssignableTo(Type c)
		{
			if (c == this)
			{
				return true;
			}
			if (c.IsInterface)
			{
				if (this.parent != null && this.is_created && c.IsAssignableFrom(this.parent))
				{
					return true;
				}
				if (this.interfaces == null)
				{
					return false;
				}
				foreach (Type c2 in this.interfaces)
				{
					if (c.IsAssignableFrom(c2))
					{
						return true;
					}
				}
				if (!this.is_created)
				{
					return false;
				}
			}
			if (this.parent == null)
			{
				return c == typeof(object);
			}
			return c.IsAssignableFrom(this.parent);
		}

		/// <summary>Returns a value that indicates whether the current dynamic type has been created.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> method has been called; otherwise, <see langword="false" />.</returns>
		// Token: 0x060052FB RID: 21243 RVA: 0x00105129 File Offset: 0x00103329
		public bool IsCreated()
		{
			return this.is_created;
		}

		/// <summary>Returns an array of <see cref="T:System.Type" /> objects representing the type arguments of a generic type or the type parameters of a generic type definition.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects. The elements of the array represent the type arguments of a generic type or the type parameters of a generic type definition.</returns>
		// Token: 0x060052FC RID: 21244 RVA: 0x00105134 File Offset: 0x00103334
		public override Type[] GetGenericArguments()
		{
			if (this.generic_params == null)
			{
				return null;
			}
			Type[] array = new Type[this.generic_params.Length];
			this.generic_params.CopyTo(array, 0);
			return array;
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a generic type definition from which the current type can be obtained.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing a generic type definition from which the current type can be obtained.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current type is not generic. That is, <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> returns <see langword="false" />.</exception>
		// Token: 0x060052FD RID: 21245 RVA: 0x00105167 File Offset: 0x00103367
		public override Type GetGenericTypeDefinition()
		{
			if (this.generic_params == null)
			{
				throw new InvalidOperationException("Type is not generic");
			}
			return this;
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x060052FE RID: 21246 RVA: 0x0010517D File Offset: 0x0010337D
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.generic_params != null;
			}
		}

		/// <summary>Gets a value indicating whether the current type is a generic type parameter.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Reflection.Emit.TypeBuilder" /> object represents a generic type parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x060052FF RID: 21247 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates the covariance and special constraints of the current generic type parameter.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Reflection.GenericParameterAttributes" /> values that describes the covariance and special constraints of the current generic type parameter.</returns>
		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06005300 RID: 21248 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				return GenericParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Reflection.Emit.TypeBuilder" /> represents a generic type definition from which other generic types can be constructed.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Reflection.Emit.TypeBuilder" /> object represents a generic type definition; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06005301 RID: 21249 RVA: 0x0010517D File Offset: 0x0010337D
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return this.generic_params != null;
			}
		}

		/// <summary>Gets a value indicating whether the current type is a generic type.</summary>
		/// <returns>
		///   <see langword="true" /> if the type represented by the current <see cref="T:System.Reflection.Emit.TypeBuilder" /> object is generic; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06005302 RID: 21250 RVA: 0x00105188 File Offset: 0x00103388
		public override bool IsGenericType
		{
			get
			{
				return this.IsGenericTypeDefinition;
			}
		}

		/// <summary>Gets the position of a type parameter in the type parameter list of the generic type that declared the parameter.</summary>
		/// <returns>If the current <see cref="T:System.Reflection.Emit.TypeBuilder" /> object represents a generic type parameter, the position of the type parameter in the type parameter list of the generic type that declared the parameter; otherwise, undefined.</returns>
		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06005303 RID: 21251 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[MonoTODO]
		public override int GenericParameterPosition
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the method that declared the current generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodBase" /> that represents the method that declared the current type, if the current type is a generic type parameter; otherwise, <see langword="null" />.</returns>
		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06005304 RID: 21252 RVA: 0x0000AF5E File Offset: 0x0000915E
		public override MethodBase DeclaringMethod
		{
			get
			{
				return null;
			}
		}

		/// <summary>Defines the generic type parameters for the current type, specifying their number and their names, and returns an array of <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> objects that can be used to set their constraints.</summary>
		/// <param name="names">An array of names for the generic type parameters.</param>
		/// <returns>An array of <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> objects that can be used to define the constraints of the generic type parameters for the current type.</returns>
		/// <exception cref="T:System.InvalidOperationException">Generic type parameters have already been defined for this type.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="names" /> is <see langword="null" />.  
		/// -or-  
		/// An element of <paramref name="names" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="names" /> is an empty array.</exception>
		// Token: 0x06005305 RID: 21253 RVA: 0x00105190 File Offset: 0x00103390
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
				this.generic_params[i] = new GenericTypeParameterBuilder(this, null, text, i);
			}
			return this.generic_params;
		}

		/// <summary>Returns the constructor of the specified constructed generic type that corresponds to the specified constructor of the generic type definition.</summary>
		/// <param name="type">The constructed generic type whose constructor is returned.</param>
		/// <param name="constructor">A constructor on the generic type definition of <paramref name="type" />, which specifies which constructor of <paramref name="type" /> to return.</param>
		/// <returns>A <see cref="T:System.Reflection.ConstructorInfo" /> object that represents the constructor of <paramref name="type" /> corresponding to <paramref name="constructor" />, which specifies a constructor belonging to the generic type definition of <paramref name="type" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not represent a generic type.  
		/// -or-  
		/// <paramref name="type" /> is not of type <see cref="T:System.Reflection.Emit.TypeBuilder" />.  
		/// -or-  
		/// The declaring type of <paramref name="constructor" /> is not a generic type definition.  
		/// -or-  
		/// The declaring type of <paramref name="constructor" /> is not the generic type definition of <paramref name="type" />.</exception>
		// Token: 0x06005306 RID: 21254 RVA: 0x00105200 File Offset: 0x00103400
		public static ConstructorInfo GetConstructor(Type type, ConstructorInfo constructor)
		{
			if (type == null)
			{
				throw new ArgumentException("Type is not generic", "type");
			}
			if (!type.IsGenericType)
			{
				throw new ArgumentException("Type is not a generic type", "type");
			}
			if (type.IsGenericTypeDefinition)
			{
				throw new ArgumentException("Type cannot be a generic type definition", "type");
			}
			if (constructor == null)
			{
				throw new NullReferenceException();
			}
			if (!constructor.DeclaringType.IsGenericTypeDefinition)
			{
				throw new ArgumentException("constructor declaring type is not a generic type definition", "constructor");
			}
			if (constructor.DeclaringType != type.GetGenericTypeDefinition())
			{
				throw new ArgumentException("constructor declaring type is not the generic type definition of type", "constructor");
			}
			ConstructorInfo constructor2 = type.GetConstructor(constructor);
			if (constructor2 == null)
			{
				throw new ArgumentException("constructor not found");
			}
			return constructor2;
		}

		// Token: 0x06005307 RID: 21255 RVA: 0x001052C0 File Offset: 0x001034C0
		private static bool IsValidGetMethodType(Type type)
		{
			if (type is TypeBuilder || type is TypeBuilderInstantiation)
			{
				return true;
			}
			if (type.Module is ModuleBuilder)
			{
				return true;
			}
			if (type.IsGenericParameter)
			{
				return false;
			}
			Type[] genericArguments = type.GetGenericArguments();
			if (genericArguments == null)
			{
				return false;
			}
			for (int i = 0; i < genericArguments.Length; i++)
			{
				if (TypeBuilder.IsValidGetMethodType(genericArguments[i]))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Returns the method of the specified constructed generic type that corresponds to the specified method of the generic type definition.</summary>
		/// <param name="type">The constructed generic type whose method is returned.</param>
		/// <param name="method">A method on the generic type definition of <paramref name="type" />, which specifies which method of <paramref name="type" /> to return.</param>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object that represents the method of <paramref name="type" /> corresponding to <paramref name="method" />, which specifies a method belonging to the generic type definition of <paramref name="type" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is a generic method that is not a generic method definition.  
		/// -or-  
		/// <paramref name="type" /> does not represent a generic type.  
		/// -or-  
		/// <paramref name="type" /> is not of type <see cref="T:System.Reflection.Emit.TypeBuilder" />.  
		/// -or-  
		/// The declaring type of <paramref name="method" /> is not a generic type definition.  
		/// -or-  
		/// The declaring type of <paramref name="method" /> is not the generic type definition of <paramref name="type" />.</exception>
		// Token: 0x06005308 RID: 21256 RVA: 0x00105320 File Offset: 0x00103520
		public static MethodInfo GetMethod(Type type, MethodInfo method)
		{
			if (!TypeBuilder.IsValidGetMethodType(type))
			{
				string str = "type is not TypeBuilder but ";
				Type type2 = type.GetType();
				throw new ArgumentException(str + ((type2 != null) ? type2.ToString() : null), "type");
			}
			if (type is TypeBuilder && type.ContainsGenericParameters)
			{
				type = type.MakeGenericType(type.GetGenericArguments());
			}
			if (!type.IsGenericType)
			{
				throw new ArgumentException("type is not a generic type", "type");
			}
			if (!method.DeclaringType.IsGenericTypeDefinition)
			{
				throw new ArgumentException("method declaring type is not a generic type definition", "method");
			}
			if (method.DeclaringType != type.GetGenericTypeDefinition())
			{
				throw new ArgumentException("method declaring type is not the generic type definition of type", "method");
			}
			if (method == null)
			{
				throw new NullReferenceException();
			}
			MethodInfo method2 = type.GetMethod(method);
			if (method2 == null)
			{
				throw new ArgumentException(string.Format("method {0} not found in type {1}", method.Name, type));
			}
			return method2;
		}

		/// <summary>Returns the field of the specified constructed generic type that corresponds to the specified field of the generic type definition.</summary>
		/// <param name="type">The constructed generic type whose field is returned.</param>
		/// <param name="field">A field on the generic type definition of <paramref name="type" />, which specifies which field of <paramref name="type" /> to return.</param>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object that represents the field of <paramref name="type" /> corresponding to <paramref name="field" />, which specifies a field belonging to the generic type definition of <paramref name="type" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not represent a generic type.  
		/// -or-  
		/// <paramref name="type" /> is not of type <see cref="T:System.Reflection.Emit.TypeBuilder" />.  
		/// -or-  
		/// The declaring type of <paramref name="field" /> is not a generic type definition.  
		/// -or-  
		/// The declaring type of <paramref name="field" /> is not the generic type definition of <paramref name="type" />.</exception>
		// Token: 0x06005309 RID: 21257 RVA: 0x00105408 File Offset: 0x00103608
		public static FieldInfo GetField(Type type, FieldInfo field)
		{
			if (!type.IsGenericType)
			{
				throw new ArgumentException("Type is not a generic type", "type");
			}
			if (type.IsGenericTypeDefinition)
			{
				throw new ArgumentException("Type cannot be a generic type definition", "type");
			}
			if (field is FieldOnTypeBuilderInst)
			{
				throw new ArgumentException("The specified field must be declared on a generic type definition.", "field");
			}
			if (field.DeclaringType != type.GetGenericTypeDefinition())
			{
				throw new ArgumentException("field declaring type is not the generic type definition of type", "method");
			}
			FieldInfo field2 = type.GetField(field);
			if (field2 == null)
			{
				throw new Exception("field not found");
			}
			return field2;
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x0600530A RID: 21258 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
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
		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x0600530B RID: 21259 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
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
		// Token: 0x0600530C RID: 21260 RVA: 0x000FA61D File Offset: 0x000F881D
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return base.IsAssignableFrom(typeInfo);
		}

		// Token: 0x0600530D RID: 21261 RVA: 0x001054A0 File Offset: 0x001036A0
		internal static bool SetConstantValue(Type destType, object value, ref object destValue)
		{
			if (value != null)
			{
				Type type = value.GetType();
				if (destType.IsByRef)
				{
					destType = destType.GetElementType();
				}
				destType = (Nullable.GetUnderlyingType(destType) ?? destType);
				if (destType.IsEnum)
				{
					EnumBuilder enumBuilder;
					Type type2;
					TypeBuilder typeBuilder;
					if ((enumBuilder = (destType as EnumBuilder)) != null)
					{
						type2 = enumBuilder.GetEnumUnderlyingType();
						if ((!enumBuilder.GetTypeBuilder().is_created || !(type == enumBuilder.GetTypeBuilder().created)) && !(type == type2))
						{
							TypeBuilder.throw_argument_ConstantDoesntMatch();
						}
					}
					else if ((typeBuilder = (destType as TypeBuilder)) != null)
					{
						type2 = typeBuilder.underlying_type;
						if (type2 == null || (type != typeBuilder.UnderlyingSystemType && type != type2))
						{
							TypeBuilder.throw_argument_ConstantDoesntMatch();
						}
					}
					else
					{
						type2 = Enum.GetUnderlyingType(destType);
						if (type != destType && type != type2)
						{
							TypeBuilder.throw_argument_ConstantDoesntMatch();
						}
					}
					type = type2;
				}
				else if (!destType.IsAssignableFrom(type))
				{
					TypeBuilder.throw_argument_ConstantDoesntMatch();
				}
				switch (Type.GetTypeCode(type))
				{
				case TypeCode.Boolean:
				case TypeCode.Char:
				case TypeCode.SByte:
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
					destValue = value;
					return true;
				case TypeCode.DateTime:
				{
					long ticks = ((DateTime)value).Ticks;
					destValue = ticks;
					return true;
				}
				case TypeCode.String:
					destValue = value;
					return true;
				}
				throw new ArgumentException(type.ToString() + " is not a supported constant type.");
			}
			destValue = null;
			return true;
		}

		// Token: 0x0600530E RID: 21262 RVA: 0x0010562B File Offset: 0x0010382B
		private static void throw_argument_ConstantDoesntMatch()
		{
			throw new ArgumentException("Constant does not match the defined type.");
		}

		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x0600530F RID: 21263 RVA: 0x000040F7 File Offset: 0x000022F7
		public override bool IsTypeDefinition
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0400331B RID: 13083
		private string tname;

		// Token: 0x0400331C RID: 13084
		private string nspace;

		// Token: 0x0400331D RID: 13085
		private Type parent;

		// Token: 0x0400331E RID: 13086
		private Type nesting_type;

		// Token: 0x0400331F RID: 13087
		internal Type[] interfaces;

		// Token: 0x04003320 RID: 13088
		internal int num_methods;

		// Token: 0x04003321 RID: 13089
		internal MethodBuilder[] methods;

		// Token: 0x04003322 RID: 13090
		internal ConstructorBuilder[] ctors;

		// Token: 0x04003323 RID: 13091
		internal PropertyBuilder[] properties;

		// Token: 0x04003324 RID: 13092
		internal int num_fields;

		// Token: 0x04003325 RID: 13093
		internal FieldBuilder[] fields;

		// Token: 0x04003326 RID: 13094
		internal EventBuilder[] events;

		// Token: 0x04003327 RID: 13095
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04003328 RID: 13096
		internal TypeBuilder[] subtypes;

		// Token: 0x04003329 RID: 13097
		internal TypeAttributes attrs;

		// Token: 0x0400332A RID: 13098
		private int table_idx;

		// Token: 0x0400332B RID: 13099
		private ModuleBuilder pmodule;

		// Token: 0x0400332C RID: 13100
		private int class_size;

		// Token: 0x0400332D RID: 13101
		private PackingSize packing_size;

		// Token: 0x0400332E RID: 13102
		private IntPtr generic_container;

		// Token: 0x0400332F RID: 13103
		private GenericTypeParameterBuilder[] generic_params;

		// Token: 0x04003330 RID: 13104
		private RefEmitPermissionSet[] permissions;

		// Token: 0x04003331 RID: 13105
		private TypeInfo created;

		// Token: 0x04003332 RID: 13106
		private int state;

		// Token: 0x04003333 RID: 13107
		private TypeName fullname;

		// Token: 0x04003334 RID: 13108
		private bool createTypeCalled;

		// Token: 0x04003335 RID: 13109
		private Type underlying_type;

		/// <summary>Represents that total size for the type is not specified.</summary>
		// Token: 0x04003336 RID: 13110
		public const int UnspecifiedTypeSize = 0;
	}
}
