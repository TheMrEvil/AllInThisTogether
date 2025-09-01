using System;
using System.Globalization;
using System.Runtime.InteropServices;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Defines and creates generic type parameters for dynamically defined generic types and methods. This class cannot be inherited.</summary>
	// Token: 0x02000928 RID: 2344
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class GenericTypeParameterBuilder : TypeInfo
	{
		/// <summary>Sets the base type that a type must inherit in order to be substituted for the type parameter.</summary>
		/// <param name="baseTypeConstraint">The <see cref="T:System.Type" /> that must be inherited by any type that is to be substituted for the type parameter.</param>
		// Token: 0x06005045 RID: 20549 RVA: 0x000FAF1A File Offset: 0x000F911A
		public void SetBaseTypeConstraint(Type baseTypeConstraint)
		{
			this.base_type = (baseTypeConstraint ?? typeof(object));
		}

		/// <summary>Sets the interfaces a type must implement in order to be substituted for the type parameter.</summary>
		/// <param name="interfaceConstraints">An array of <see cref="T:System.Type" /> objects that represent the interfaces a type must implement in order to be substituted for the type parameter.</param>
		// Token: 0x06005046 RID: 20550 RVA: 0x000FAF31 File Offset: 0x000F9131
		[ComVisible(true)]
		public void SetInterfaceConstraints(params Type[] interfaceConstraints)
		{
			this.iface_constraints = interfaceConstraints;
		}

		/// <summary>Sets the variance characteristics and special constraints of the generic parameter, such as the parameterless constructor constraint.</summary>
		/// <param name="genericParameterAttributes">A bitwise combination of <see cref="T:System.Reflection.GenericParameterAttributes" /> values that represent the variance characteristics and special constraints of the generic type parameter.</param>
		// Token: 0x06005047 RID: 20551 RVA: 0x000FAF3A File Offset: 0x000F913A
		public void SetGenericParameterAttributes(GenericParameterAttributes genericParameterAttributes)
		{
			this.attrs = genericParameterAttributes;
		}

		// Token: 0x06005048 RID: 20552 RVA: 0x000FAF43 File Offset: 0x000F9143
		internal GenericTypeParameterBuilder(TypeBuilder tbuilder, MethodBuilder mbuilder, string name, int index)
		{
			this.tbuilder = tbuilder;
			this.mbuilder = mbuilder;
			this.name = name;
			this.index = index;
		}

		// Token: 0x06005049 RID: 20553 RVA: 0x000FAF68 File Offset: 0x000F9168
		internal override Type InternalResolve()
		{
			if (this.mbuilder != null)
			{
				return MethodBase.GetMethodFromHandle(this.mbuilder.MethodHandleInternal, this.mbuilder.TypeBuilder.InternalResolve().TypeHandle).GetGenericArguments()[this.index];
			}
			return this.tbuilder.InternalResolve().GetGenericArguments()[this.index];
		}

		// Token: 0x0600504A RID: 20554 RVA: 0x000FAFCC File Offset: 0x000F91CC
		internal override Type RuntimeResolve()
		{
			if (this.mbuilder != null)
			{
				return MethodBase.GetMethodFromHandle(this.mbuilder.MethodHandleInternal, this.mbuilder.TypeBuilder.RuntimeResolve().TypeHandle).GetGenericArguments()[this.index];
			}
			return this.tbuilder.RuntimeResolve().GetGenericArguments()[this.index];
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="c">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600504B RID: 20555 RVA: 0x000FB030 File Offset: 0x000F9230
		[ComVisible(true)]
		public override bool IsSubclassOf(Type c)
		{
			throw this.not_supported();
		}

		// Token: 0x0600504C RID: 20556 RVA: 0x000040F7 File Offset: 0x000022F7
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return TypeAttributes.Public;
		}

		// Token: 0x0600504D RID: 20557 RVA: 0x000FB030 File Offset: 0x000F9230
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600504E RID: 20558 RVA: 0x000FB030 File Offset: 0x000F9230
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="name">Not supported.</param>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600504F RID: 20559 RVA: 0x000FB030 File Offset: 0x000F9230
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06005050 RID: 20560 RVA: 0x000FB030 File Offset: 0x000F9230
		public override EventInfo[] GetEvents()
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06005051 RID: 20561 RVA: 0x000FB030 File Offset: 0x000F9230
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="name">Not supported.</param>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06005052 RID: 20562 RVA: 0x000FB030 File Offset: 0x000F9230
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06005053 RID: 20563 RVA: 0x000FB030 File Offset: 0x000F9230
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="name">The name of the interface.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to search without regard for case; <see langword="false" /> to make a case-sensitive search.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06005054 RID: 20564 RVA: 0x000FB030 File Offset: 0x000F9230
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06005055 RID: 20565 RVA: 0x000FB030 File Offset: 0x000F9230
		public override Type[] GetInterfaces()
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06005056 RID: 20566 RVA: 0x000FB030 File Offset: 0x000F9230
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="name">Not supported.</param>
		/// <param name="type">Not supported.</param>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06005057 RID: 20567 RVA: 0x000FB030 File Offset: 0x000F9230
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06005058 RID: 20568 RVA: 0x000FB030 File Offset: 0x000F9230
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x06005059 RID: 20569 RVA: 0x000FB030 File Offset: 0x000F9230
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="name">Not supported.</param>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600505A RID: 20570 RVA: 0x000FB030 File Offset: 0x000F9230
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600505B RID: 20571 RVA: 0x000FB030 File Offset: 0x000F9230
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="bindingAttr">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600505C RID: 20572 RVA: 0x000FB030 File Offset: 0x000F9230
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x0600505D RID: 20573 RVA: 0x000FB030 File Offset: 0x000F9230
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		// Token: 0x0600505E RID: 20574 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected override bool HasElementTypeImpl()
		{
			return false;
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</summary>
		/// <param name="c">The object to test.</param>
		/// <returns>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x0600505F RID: 20575 RVA: 0x000FB030 File Offset: 0x000F9230
		public override bool IsAssignableFrom(Type c)
		{
			throw this.not_supported();
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</summary>
		/// <param name="typeInfo">The object to test.</param>
		/// <returns>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06005060 RID: 20576 RVA: 0x00057EED File Offset: 0x000560ED
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x06005061 RID: 20577 RVA: 0x000FB030 File Offset: 0x000F9230
		public override bool IsInstanceOfType(object o)
		{
			throw this.not_supported();
		}

		// Token: 0x06005062 RID: 20578 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06005063 RID: 20579 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06005064 RID: 20580 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x06005065 RID: 20581 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06005066 RID: 20582 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06005067 RID: 20583 RVA: 0x000FB038 File Offset: 0x000F9238
		protected override bool IsValueTypeImpl()
		{
			return this.base_type != null && this.base_type.IsValueType;
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="name">Not supported.</param>
		/// <param name="invokeAttr">Not supported.</param>
		/// <param name="binder">Not supported.</param>
		/// <param name="target">Not supported.</param>
		/// <param name="args">Not supported.</param>
		/// <param name="modifiers">Not supported.</param>
		/// <param name="culture">Not supported.</param>
		/// <param name="namedParameters">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06005068 RID: 20584 RVA: 0x000FB030 File Offset: 0x000F9230
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw this.not_supported();
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> in all cases.</summary>
		/// <returns>The type referred to by the current array type, pointer type, or <see langword="ByRef" /> type; or <see langword="null" /> if the current type is not an array type, is not a pointer type, and is not passed by reference.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06005069 RID: 20585 RVA: 0x000FB030 File Offset: 0x000F9230
		public override Type GetElementType()
		{
			throw this.not_supported();
		}

		/// <summary>Gets the current generic type parameter.</summary>
		/// <returns>The current <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> object.</returns>
		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x0600506A RID: 20586 RVA: 0x0000270D File Offset: 0x0000090D
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets an <see cref="T:System.Reflection.Assembly" /> object representing the dynamic assembly that contains the generic type definition the current type parameter belongs to.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> object representing the dynamic assembly that contains the generic type definition the current type parameter belongs to.</returns>
		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x0600506B RID: 20587 RVA: 0x000FB055 File Offset: 0x000F9255
		public override Assembly Assembly
		{
			get
			{
				return this.tbuilder.Assembly;
			}
		}

		/// <summary>Gets <see langword="null" /> in all cases.</summary>
		/// <returns>A null reference (<see langword="Nothing" /> in Visual Basic) in all cases.</returns>
		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x0600506C RID: 20588 RVA: 0x0000AF5E File Offset: 0x0000915E
		public override string AssemblyQualifiedName
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the base type constraint of the current generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the base type constraint of the generic type parameter, or <see langword="null" /> if the type parameter has no base type constraint.</returns>
		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x0600506D RID: 20589 RVA: 0x000FB062 File Offset: 0x000F9262
		public override Type BaseType
		{
			get
			{
				return this.base_type;
			}
		}

		/// <summary>Gets <see langword="null" /> in all cases.</summary>
		/// <returns>A null reference (<see langword="Nothing" /> in Visual Basic) in all cases.</returns>
		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x0600506E RID: 20590 RVA: 0x0000AF5E File Offset: 0x0000915E
		public override string FullName
		{
			get
			{
				return null;
			}
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x0600506F RID: 20591 RVA: 0x000FB030 File Offset: 0x000F9230
		public override Guid GUID
		{
			get
			{
				throw this.not_supported();
			}
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="attributeType">Not supported.</param>
		/// <param name="inherit">Not supported.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06005070 RID: 20592 RVA: 0x000FB030 File Offset: 0x000F9230
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06005071 RID: 20593 RVA: 0x000FB030 File Offset: 0x000F9230
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned.</param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06005072 RID: 20594 RVA: 0x000FB030 File Offset: 0x000F9230
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <param name="interfaceType">A <see cref="T:System.Type" /> object that represents the interface type for which the mapping is to be retrieved.</param>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06005073 RID: 20595 RVA: 0x000FB030 File Offset: 0x000F9230
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw this.not_supported();
		}

		/// <summary>Gets the name of the generic type parameter.</summary>
		/// <returns>The name of the generic type parameter.</returns>
		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06005074 RID: 20596 RVA: 0x000FB06A File Offset: 0x000F926A
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets <see langword="null" /> in all cases.</summary>
		/// <returns>A null reference (<see langword="Nothing" /> in Visual Basic) in all cases.</returns>
		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06005075 RID: 20597 RVA: 0x0000AF5E File Offset: 0x0000915E
		public override string Namespace
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the dynamic module that contains the generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Reflection.Module" /> object that represents the dynamic module that contains the generic type parameter.</returns>
		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06005076 RID: 20598 RVA: 0x000FB072 File Offset: 0x000F9272
		public override Module Module
		{
			get
			{
				return this.tbuilder.Module;
			}
		}

		/// <summary>Gets the generic type definition or generic method definition to which the generic type parameter belongs.</summary>
		/// <returns>If the type parameter belongs to a generic type, a <see cref="T:System.Type" /> object representing that generic type; if the type parameter belongs to a generic method, a <see cref="T:System.Type" /> object representing that type that declared that generic method.</returns>
		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06005077 RID: 20599 RVA: 0x000FB07F File Offset: 0x000F927F
		public override Type DeclaringType
		{
			get
			{
				if (!(this.mbuilder != null))
				{
					return this.tbuilder;
				}
				return this.mbuilder.DeclaringType;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object that was used to obtain the <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.</summary>
		/// <returns>The <see cref="T:System.Type" /> object that was used to obtain the <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.</returns>
		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06005078 RID: 20600 RVA: 0x00058E51 File Offset: 0x00057051
		public override Type ReflectedType
		{
			get
			{
				return this.DeclaringType;
			}
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06005079 RID: 20601 RVA: 0x000FB030 File Offset: 0x000F9230
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw this.not_supported();
			}
		}

		/// <summary>Not valid for generic type parameters.</summary>
		/// <returns>Not valid for generic type parameters.</returns>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x0600507A RID: 20602 RVA: 0x00084B75 File Offset: 0x00082D75
		public override Type[] GetGenericArguments()
		{
			throw new InvalidOperationException();
		}

		/// <summary>Not valid for generic type parameters.</summary>
		/// <returns>Not valid for generic type parameters.</returns>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x0600507B RID: 20603 RVA: 0x00084B75 File Offset: 0x00082D75
		public override Type GetGenericTypeDefinition()
		{
			throw new InvalidOperationException();
		}

		/// <summary>Gets <see langword="true" /> in all cases.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x0600507C RID: 20604 RVA: 0x000040F7 File Offset: 0x000022F7
		public override bool ContainsGenericParameters
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets <see langword="true" /> in all cases.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x0600507D RID: 20605 RVA: 0x000040F7 File Offset: 0x000022F7
		public override bool IsGenericParameter
		{
			get
			{
				return true;
			}
		}

		/// <summary>Returns <see langword="false" /> in all cases.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x0600507E RID: 20606 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override bool IsGenericType
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets <see langword="false" /> in all cases.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x0600507F RID: 20607 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a combination of <see cref="T:System.Reflection.GenericParameterAttributes" /> flags that describe the covariance and special constraints of the current generic type parameter.</summary>
		/// <returns>A bitwise combination of values that describes the covariance and special constraints of the current generic type parameter.</returns>
		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06005080 RID: 20608 RVA: 0x000472C8 File Offset: 0x000454C8
		public override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets the position of the type parameter in the type parameter list of the generic type or method that declared the parameter.</summary>
		/// <returns>The position of the type parameter in the type parameter list of the generic type or method that declared the parameter.</returns>
		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06005081 RID: 20609 RVA: 0x000FB0A1 File Offset: 0x000F92A1
		public override int GenericParameterPosition
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x06005082 RID: 20610 RVA: 0x00084B75 File Offset: 0x00082D75
		public override Type[] GetGenericParameterConstraints()
		{
			throw new InvalidOperationException();
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MethodInfo" /> that represents the declaring method, if the current <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> represents a type parameter of a generic method.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> that represents the declaring method, if the current <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> represents a type parameter of a generic method; otherwise, <see langword="null" />.</returns>
		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06005083 RID: 20611 RVA: 0x000FB0A9 File Offset: 0x000F92A9
		public override MethodBase DeclaringMethod
		{
			get
			{
				return this.mbuilder;
			}
		}

		/// <summary>Set a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class that defines the custom attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customBuilder" /> is <see langword="null" />.</exception>
		// Token: 0x06005084 RID: 20612 RVA: 0x000FB0B4 File Offset: 0x000F92B4
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
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
		/// <param name="binaryAttribute">A byte blob representing the attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="binaryAttribute" /> is a null reference.</exception>
		// Token: 0x06005085 RID: 20613 RVA: 0x000FB11C File Offset: 0x000F931C
		[MonoTODO("unverified implementation")]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x06005086 RID: 20614 RVA: 0x0004DD2C File Offset: 0x0004BF2C
		private Exception not_supported()
		{
			return new NotSupportedException();
		}

		/// <summary>Returns a string representation of the current generic type parameter.</summary>
		/// <returns>A string that contains the name of the generic type parameter.</returns>
		// Token: 0x06005087 RID: 20615 RVA: 0x000FB06A File Offset: 0x000F926A
		public override string ToString()
		{
			return this.name;
		}

		/// <summary>Tests whether the given object is an instance of <see langword="EventToken" /> and is equal to the current instance.</summary>
		/// <param name="o">The object to be compared with the current instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="o" /> is an instance of <see langword="EventToken" /> and equals the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005088 RID: 20616 RVA: 0x000FB12B File Offset: 0x000F932B
		[MonoTODO]
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		/// <summary>Returns a 32-bit integer hash code for the current instance.</summary>
		/// <returns>A 32-bit integer hash code.</returns>
		// Token: 0x06005089 RID: 20617 RVA: 0x000FB134 File Offset: 0x000F9334
		[MonoTODO]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns the type of a one-dimensional array whose element type is the generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the type of a one-dimensional array whose element type is the generic type parameter.</returns>
		// Token: 0x0600508A RID: 20618 RVA: 0x000F6525 File Offset: 0x000F4725
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		/// <summary>Returns the type of an array whose element type is the generic type parameter, with the specified number of dimensions.</summary>
		/// <param name="rank">The number of dimensions for the array.</param>
		/// <returns>A <see cref="T:System.Type" /> object that represents the type of an array whose element type is the generic type parameter, with the specified number of dimensions.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="rank" /> is not a valid number of dimensions. For example, its value is less than 1.</exception>
		// Token: 0x0600508B RID: 20619 RVA: 0x000F652E File Offset: 0x000F472E
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents the current generic type parameter when passed as a reference parameter.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the current generic type parameter when passed as a reference parameter.</returns>
		// Token: 0x0600508C RID: 20620 RVA: 0x000F6541 File Offset: 0x000F4741
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		/// <summary>Not valid for incomplete generic type parameters.</summary>
		/// <param name="typeArguments">An array of type arguments.</param>
		/// <returns>This method is invalid for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x0600508D RID: 20621 RVA: 0x000FB13C File Offset: 0x000F933C
		public override Type MakeGenericType(params Type[] typeArguments)
		{
			throw new InvalidOperationException(Environment.GetResourceString("{0} is not a GenericTypeDefinition. MakeGenericType may only be called on a type for which Type.IsGenericTypeDefinition is true."));
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a pointer to the current generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents a pointer to the current generic type parameter.</returns>
		// Token: 0x0600508E RID: 20622 RVA: 0x000F6549 File Offset: 0x000F4749
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x0600508F RID: 20623 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		internal override bool IsUserType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005090 RID: 20624 RVA: 0x000173AD File Offset: 0x000155AD
		internal GenericTypeParameterBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04003177 RID: 12663
		private TypeBuilder tbuilder;

		// Token: 0x04003178 RID: 12664
		private MethodBuilder mbuilder;

		// Token: 0x04003179 RID: 12665
		private string name;

		// Token: 0x0400317A RID: 12666
		private int index;

		// Token: 0x0400317B RID: 12667
		private Type base_type;

		// Token: 0x0400317C RID: 12668
		private Type[] iface_constraints;

		// Token: 0x0400317D RID: 12669
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x0400317E RID: 12670
		private GenericParameterAttributes attrs;
	}
}
