using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Unity;

namespace System.Reflection
{
	/// <summary>Provides information about methods and constructors.</summary>
	// Token: 0x020008AD RID: 2221
	[Serializable]
	public abstract class MethodBase : MemberInfo, _MethodBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.MethodBase" /> class.</summary>
		// Token: 0x06004928 RID: 18728 RVA: 0x00047204 File Offset: 0x00045404
		protected MethodBase()
		{
		}

		/// <summary>When overridden in a derived class, gets the parameters of the specified method or constructor.</summary>
		/// <returns>An array of type <see langword="ParameterInfo" /> containing information that matches the signature of the method (or constructor) reflected by this <see langword="MethodBase" /> instance.</returns>
		// Token: 0x06004929 RID: 18729
		public abstract ParameterInfo[] GetParameters();

		/// <summary>Gets the attributes associated with this method.</summary>
		/// <returns>One of the <see cref="T:System.Reflection.MethodAttributes" /> values.</returns>
		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x0600492A RID: 18730
		public abstract MethodAttributes Attributes { get; }

		/// <summary>Gets the <see cref="T:System.Reflection.MethodImplAttributes" /> flags that specify the attributes of a method implementation.</summary>
		/// <returns>The method implementation flags.</returns>
		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x0600492B RID: 18731 RVA: 0x000EEAD1 File Offset: 0x000ECCD1
		public virtual MethodImplAttributes MethodImplementationFlags
		{
			get
			{
				return this.GetMethodImplementationFlags();
			}
		}

		/// <summary>When overridden in a derived class, returns the <see cref="T:System.Reflection.MethodImplAttributes" /> flags.</summary>
		/// <returns>The <see langword="MethodImplAttributes" /> flags.</returns>
		// Token: 0x0600492C RID: 18732
		public abstract MethodImplAttributes GetMethodImplementationFlags();

		/// <summary>When overridden in a derived class, gets a <see cref="T:System.Reflection.MethodBody" /> object that provides access to the MSIL stream, local variables, and exceptions for the current method.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodBody" /> object that provides access to the MSIL stream, local variables, and exceptions for the current method.</returns>
		/// <exception cref="T:System.InvalidOperationException">This method is invalid unless overridden in a derived class.</exception>
		// Token: 0x0600492D RID: 18733 RVA: 0x00084B75 File Offset: 0x00082D75
		public virtual MethodBody GetMethodBody()
		{
			throw new InvalidOperationException();
		}

		/// <summary>Gets a value indicating the calling conventions for this method.</summary>
		/// <returns>The <see cref="T:System.Reflection.CallingConventions" /> for this method.</returns>
		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x0600492E RID: 18734 RVA: 0x000040F7 File Offset: 0x000022F7
		public virtual CallingConventions CallingConvention
		{
			get
			{
				return CallingConventions.Standard;
			}
		}

		/// <summary>Gets a value indicating whether the method is abstract.</summary>
		/// <returns>
		///   <see langword="true" /> if the method is abstract; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x0600492F RID: 18735 RVA: 0x000EEAD9 File Offset: 0x000ECCD9
		public bool IsAbstract
		{
			get
			{
				return (this.Attributes & MethodAttributes.Abstract) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the method is a constructor.</summary>
		/// <returns>
		///   <see langword="true" /> if this method is a constructor represented by a <see cref="T:System.Reflection.ConstructorInfo" /> object (see note in Remarks about <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> objects); otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06004930 RID: 18736 RVA: 0x000EEAEA File Offset: 0x000ECCEA
		public bool IsConstructor
		{
			get
			{
				return this is ConstructorInfo && !this.IsStatic && (this.Attributes & MethodAttributes.RTSpecialName) == MethodAttributes.RTSpecialName;
			}
		}

		/// <summary>Gets a value indicating whether this method is <see langword="final" />.</summary>
		/// <returns>
		///   <see langword="true" /> if this method is <see langword="final" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06004931 RID: 18737 RVA: 0x000EEB11 File Offset: 0x000ECD11
		public bool IsFinal
		{
			get
			{
				return (this.Attributes & MethodAttributes.Final) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether only a member of the same kind with exactly the same signature is hidden in the derived class.</summary>
		/// <returns>
		///   <see langword="true" /> if the member is hidden by signature; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06004932 RID: 18738 RVA: 0x000EEB1F File Offset: 0x000ECD1F
		public bool IsHideBySig
		{
			get
			{
				return (this.Attributes & MethodAttributes.HideBySig) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether this method has a special name.</summary>
		/// <returns>
		///   <see langword="true" /> if this method has a special name; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06004933 RID: 18739 RVA: 0x000EEB30 File Offset: 0x000ECD30
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & MethodAttributes.SpecialName) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the method is <see langword="static" />.</summary>
		/// <returns>
		///   <see langword="true" /> if this method is <see langword="static" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06004934 RID: 18740 RVA: 0x000EEB41 File Offset: 0x000ECD41
		public bool IsStatic
		{
			get
			{
				return (this.Attributes & MethodAttributes.Static) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the method is <see langword="virtual" />.</summary>
		/// <returns>
		///   <see langword="true" /> if this method is <see langword="virtual" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06004935 RID: 18741 RVA: 0x000EEB4F File Offset: 0x000ECD4F
		public bool IsVirtual
		{
			get
			{
				return (this.Attributes & MethodAttributes.Virtual) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the potential visibility of this method or constructor is described by <see cref="F:System.Reflection.MethodAttributes.Assembly" />; that is, the method or constructor is visible at most to other types in the same assembly, and is not visible to derived types outside the assembly.</summary>
		/// <returns>
		///   <see langword="true" /> if the visibility of this method or constructor is exactly described by <see cref="F:System.Reflection.MethodAttributes.Assembly" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06004936 RID: 18742 RVA: 0x000EEB5D File Offset: 0x000ECD5D
		public bool IsAssembly
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Assembly;
			}
		}

		/// <summary>Gets a value indicating whether the visibility of this method or constructor is described by <see cref="F:System.Reflection.MethodAttributes.Family" />; that is, the method or constructor is visible only within its class and derived classes.</summary>
		/// <returns>
		///   <see langword="true" /> if access to this method or constructor is exactly described by <see cref="F:System.Reflection.MethodAttributes.Family" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06004937 RID: 18743 RVA: 0x000EEB6A File Offset: 0x000ECD6A
		public bool IsFamily
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Family;
			}
		}

		/// <summary>Gets a value indicating whether the visibility of this method or constructor is described by <see cref="F:System.Reflection.MethodAttributes.FamANDAssem" />; that is, the method or constructor can be called by derived classes, but only if they are in the same assembly.</summary>
		/// <returns>
		///   <see langword="true" /> if access to this method or constructor is exactly described by <see cref="F:System.Reflection.MethodAttributes.FamANDAssem" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06004938 RID: 18744 RVA: 0x000EEB77 File Offset: 0x000ECD77
		public bool IsFamilyAndAssembly
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.FamANDAssem;
			}
		}

		/// <summary>Gets a value indicating whether the potential visibility of this method or constructor is described by <see cref="F:System.Reflection.MethodAttributes.FamORAssem" />; that is, the method or constructor can be called by derived classes wherever they are, and by classes in the same assembly.</summary>
		/// <returns>
		///   <see langword="true" /> if access to this method or constructor is exactly described by <see cref="F:System.Reflection.MethodAttributes.FamORAssem" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06004939 RID: 18745 RVA: 0x000EEB84 File Offset: 0x000ECD84
		public bool IsFamilyOrAssembly
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.FamORAssem;
			}
		}

		/// <summary>Gets a value indicating whether this member is private.</summary>
		/// <returns>
		///   <see langword="true" /> if access to this method is restricted to other members of the class itself; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x0600493A RID: 18746 RVA: 0x000EEB91 File Offset: 0x000ECD91
		public bool IsPrivate
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Private;
			}
		}

		/// <summary>Gets a value indicating whether this is a public method.</summary>
		/// <returns>
		///   <see langword="true" /> if this method is public; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x0600493B RID: 18747 RVA: 0x000EEB9E File Offset: 0x000ECD9E
		public bool IsPublic
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x0600493C RID: 18748 RVA: 0x000EEBAB File Offset: 0x000ECDAB
		public virtual bool IsConstructedGenericMethod
		{
			get
			{
				return this.IsGenericMethod && !this.IsGenericMethodDefinition;
			}
		}

		/// <summary>Gets a value indicating whether the method is generic.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Reflection.MethodBase" /> represents a generic method; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x0600493D RID: 18749 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the method is a generic method definition.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Reflection.MethodBase" /> object represents the definition of a generic method; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x0600493E RID: 18750 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		/// <summary>Returns an array of <see cref="T:System.Type" /> objects that represent the type arguments of a generic method or the type parameters of a generic method definition.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that represent the type arguments of a generic method or the type parameters of a generic method definition. Returns an empty array if the current method is not a generic method.</returns>
		/// <exception cref="T:System.NotSupportedException">The current object is a <see cref="T:System.Reflection.ConstructorInfo" />. Generic constructors are not supported in the .NET Framework version 2.0. This exception is the default behavior if this method is not overridden in a derived class.</exception>
		// Token: 0x0600493F RID: 18751 RVA: 0x0004728A File Offset: 0x0004548A
		public virtual Type[] GetGenericArguments()
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		/// <summary>Gets a value indicating whether the generic method contains unassigned generic type parameters.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Reflection.MethodBase" /> object represents a generic method that contains unassigned generic type parameters; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06004940 RID: 18752 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public virtual bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		/// <summary>Invokes the method or constructor represented by the current instance, using the specified parameters.</summary>
		/// <param name="obj">The object on which to invoke the method or constructor. If a method is static, this argument is ignored. If a constructor is static, this argument must be <see langword="null" /> or an instance of the class that defines the constructor.</param>
		/// <param name="parameters">An argument list for the invoked method or constructor. This is an array of objects with the same number, order, and type as the parameters of the method or constructor to be invoked. If there are no parameters, <paramref name="parameters" /> should be <see langword="null" />.  
		///  If the method or constructor represented by this instance takes a <see langword="ref" /> parameter (<see langword="ByRef" /> in Visual Basic), no special attribute is required for that parameter in order to invoke the method or constructor using this function. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is <see langword="null" />. For value-type elements, this value is 0, 0.0, or <see langword="false" />, depending on the specific element type.</param>
		/// <returns>An object containing the return value of the invoked method, or <see langword="null" /> in the case of a constructor.</returns>
		/// <exception cref="T:System.Reflection.TargetException">In the.NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.  
		///
		///
		///
		///
		///  The <paramref name="obj" /> parameter is <see langword="null" /> and the method is not static.  
		///  -or-  
		///  The method is not declared or inherited by the class of <paramref name="obj" />.  
		///  -or-  
		///  A static constructor is invoked, and <paramref name="obj" /> is neither <see langword="null" /> nor an instance of the class that declared the constructor.</exception>
		/// <exception cref="T:System.ArgumentException">The elements of the <paramref name="parameters" /> array do not match the signature of the method or constructor reflected by this instance.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The invoked method or constructor throws an exception.  
		///  -or-  
		///  The current instance is a <see cref="T:System.Reflection.Emit.DynamicMethod" /> that contains unverifiable code. See the "Verification" section in Remarks for <see cref="T:System.Reflection.Emit.DynamicMethod" />.</exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The <paramref name="parameters" /> array does not have the correct number of arguments.</exception>
		/// <exception cref="T:System.MethodAccessException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.  
		///
		///
		///
		///
		///  The caller does not have permission to execute the method or constructor that is represented by the current instance.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type that declares the method is an open generic type. That is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns <see langword="true" /> for the declaring type.</exception>
		/// <exception cref="T:System.NotSupportedException">The current instance is a <see cref="T:System.Reflection.Emit.MethodBuilder" />.</exception>
		// Token: 0x06004941 RID: 18753 RVA: 0x000EEBC0 File Offset: 0x000ECDC0
		[DebuggerStepThrough]
		[DebuggerHidden]
		public object Invoke(object obj, object[] parameters)
		{
			return this.Invoke(obj, BindingFlags.Default, null, parameters, null);
		}

		/// <summary>When overridden in a derived class, invokes the reflected method or constructor with the given parameters.</summary>
		/// <param name="obj">The object on which to invoke the method or constructor. If a method is static, this argument is ignored. If a constructor is static, this argument must be <see langword="null" /> or an instance of the class that defines the constructor.</param>
		/// <param name="invokeAttr">A bitmask that is a combination of 0 or more bit flags from <see cref="T:System.Reflection.BindingFlags" />. If <paramref name="binder" /> is <see langword="null" />, this parameter is assigned the value <see cref="F:System.Reflection.BindingFlags.Default" />; thus, whatever you pass in is ignored.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see langword="MemberInfo" /> objects via reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
		/// <param name="parameters">An argument list for the invoked method or constructor. This is an array of objects with the same number, order, and type as the parameters of the method or constructor to be invoked. If there are no parameters, this should be <see langword="null" />.  
		///  If the method or constructor represented by this instance takes a ByRef parameter, there is no special attribute required for that parameter in order to invoke the method or constructor using this function. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is <see langword="null" />. For value-type elements, this value is 0, 0.0, or <see langword="false" />, depending on the specific element type.</param>
		/// <param name="culture">An instance of <see langword="CultureInfo" /> used to govern the coercion of types. If this is <see langword="null" />, the <see langword="CultureInfo" /> for the current thread is used. (This is necessary to convert a <see langword="String" /> that represents 1000 to a <see langword="Double" /> value, for example, since 1000 is represented differently by different cultures.)</param>
		/// <returns>An <see langword="Object" /> containing the return value of the invoked method, or <see langword="null" /> in the case of a constructor, or <see langword="null" /> if the method's return type is <see langword="void" />. Before calling the method or constructor, <see langword="Invoke" /> checks to see if the user has access permission and verifies that the parameters are valid.</returns>
		/// <exception cref="T:System.Reflection.TargetException">The <paramref name="obj" /> parameter is <see langword="null" /> and the method is not static.  
		///  -or-  
		///  The method is not declared or inherited by the class of <paramref name="obj" />.  
		///  -or-  
		///  A static constructor is invoked, and <paramref name="obj" /> is neither <see langword="null" /> nor an instance of the class that declared the constructor.</exception>
		/// <exception cref="T:System.ArgumentException">The type of the <paramref name="parameters" /> parameter does not match the signature of the method or constructor reflected by this instance.</exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The <paramref name="parameters" /> array does not have the correct number of arguments.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The invoked method or constructor throws an exception.</exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to execute the method or constructor that is represented by the current instance.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type that declares the method is an open generic type. That is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns <see langword="true" /> for the declaring type.</exception>
		// Token: 0x06004942 RID: 18754
		public abstract object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		/// <summary>Gets a handle to the internal metadata representation of a method.</summary>
		/// <returns>A <see cref="T:System.RuntimeMethodHandle" /> object.</returns>
		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06004943 RID: 18755
		public abstract RuntimeMethodHandle MethodHandle { get; }

		/// <summary>Gets a value that indicates whether the current method or constructor is security-critical or security-safe-critical at the current trust level, and therefore can perform critical operations.</summary>
		/// <returns>
		///   <see langword="true" /> if the current method or constructor is security-critical or security-safe-critical at the current trust level; <see langword="false" /> if it is transparent.</returns>
		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06004944 RID: 18756 RVA: 0x00047226 File Offset: 0x00045426
		public virtual bool IsSecurityCritical
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Gets a value that indicates whether the current method or constructor is security-safe-critical at the current trust level; that is, whether it can perform critical operations and can be accessed by transparent code.</summary>
		/// <returns>
		///   <see langword="true" /> if the method or constructor is security-safe-critical at the current trust level; <see langword="false" /> if it is security-critical or transparent.</returns>
		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06004945 RID: 18757 RVA: 0x00047226 File Offset: 0x00045426
		public virtual bool IsSecuritySafeCritical
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Gets a value that indicates whether the current method or constructor is transparent at the current trust level, and therefore cannot perform critical operations.</summary>
		/// <returns>
		///   <see langword="true" /> if the method or constructor is security-transparent at the current trust level; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06004946 RID: 18758 RVA: 0x00047226 File Offset: 0x00045426
		public virtual bool IsSecurityTransparent
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004947 RID: 18759 RVA: 0x000EE36E File Offset: 0x000EC56E
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06004948 RID: 18760 RVA: 0x000EE377 File Offset: 0x000EC577
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.MethodBase" /> objects are equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004949 RID: 18761 RVA: 0x000EEBD0 File Offset: 0x000ECDD0
		public static bool operator ==(MethodBase left, MethodBase right)
		{
			if (left == right)
			{
				return true;
			}
			if (left == null || right == null)
			{
				return false;
			}
			MethodInfo left2;
			MethodInfo right2;
			if ((left2 = (left as MethodInfo)) != null && (right2 = (right as MethodInfo)) != null)
			{
				return left2 == right2;
			}
			ConstructorInfo left3;
			ConstructorInfo right3;
			return (left3 = (left as ConstructorInfo)) != null && (right3 = (right as ConstructorInfo)) != null && left3 == right3;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.MethodBase" /> objects are not equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600494A RID: 18762 RVA: 0x000EEC3C File Offset: 0x000ECE3C
		public static bool operator !=(MethodBase left, MethodBase right)
		{
			return !(left == right);
		}

		// Token: 0x0600494B RID: 18763 RVA: 0x000EEC48 File Offset: 0x000ECE48
		internal virtual ParameterInfo[] GetParametersInternal()
		{
			return this.GetParameters();
		}

		// Token: 0x0600494C RID: 18764 RVA: 0x000EEC50 File Offset: 0x000ECE50
		internal virtual int GetParametersCount()
		{
			return this.GetParametersInternal().Length;
		}

		// Token: 0x0600494D RID: 18765 RVA: 0x000479F8 File Offset: 0x00045BF8
		internal virtual Type GetParameterType(int pos)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600494E RID: 18766 RVA: 0x000EEC5A File Offset: 0x000ECE5A
		internal virtual int get_next_table_index(object obj, int table, int count)
		{
			if (this is MethodBuilder)
			{
				return ((MethodBuilder)this).get_next_table_index(obj, table, count);
			}
			if (this is ConstructorBuilder)
			{
				return ((ConstructorBuilder)this).get_next_table_index(obj, table, count);
			}
			throw new Exception("Method is not a builder method");
		}

		// Token: 0x0600494F RID: 18767 RVA: 0x000EEC94 File Offset: 0x000ECE94
		internal virtual string FormatNameAndSig(bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder(this.Name);
			stringBuilder.Append("(");
			stringBuilder.Append(MethodBase.ConstructParameters(this.GetParameterTypes(), this.CallingConvention, serialization));
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06004950 RID: 18768 RVA: 0x000EECE4 File Offset: 0x000ECEE4
		internal virtual Type[] GetParameterTypes()
		{
			ParameterInfo[] parametersNoCopy = this.GetParametersNoCopy();
			Type[] array = new Type[parametersNoCopy.Length];
			for (int i = 0; i < parametersNoCopy.Length; i++)
			{
				array[i] = parametersNoCopy[i].ParameterType;
			}
			return array;
		}

		// Token: 0x06004951 RID: 18769 RVA: 0x000EEC48 File Offset: 0x000ECE48
		internal virtual ParameterInfo[] GetParametersNoCopy()
		{
			return this.GetParameters();
		}

		/// <summary>Gets method information by using the method's internal metadata representation (handle).</summary>
		/// <param name="handle">The method's handle.</param>
		/// <returns>A <see langword="MethodBase" /> containing information about the method.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="handle" /> is invalid.</exception>
		// Token: 0x06004952 RID: 18770 RVA: 0x000EED1C File Offset: 0x000ECF1C
		public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle)
		{
			if (handle.IsNullHandle())
			{
				throw new ArgumentException(Environment.GetResourceString("The handle is invalid."));
			}
			MethodBase methodFromHandleInternalType = RuntimeMethodInfo.GetMethodFromHandleInternalType(handle.Value, IntPtr.Zero);
			if (methodFromHandleInternalType == null)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			Type declaringType = methodFromHandleInternalType.DeclaringType;
			if (declaringType != null && declaringType.IsGenericType)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Cannot resolve method {0} because the declaring type of the method handle {1} is generic. Explicitly provide the declaring type to GetMethodFromHandle."), methodFromHandleInternalType, declaringType.GetGenericTypeDefinition()));
			}
			return methodFromHandleInternalType;
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MethodBase" /> object for the constructor or method represented by the specified handle, for the specified generic type.</summary>
		/// <param name="handle">A handle to the internal metadata representation of a constructor or method.</param>
		/// <param name="declaringType">A handle to the generic type that defines the constructor or method.</param>
		/// <returns>A <see cref="T:System.Reflection.MethodBase" /> object representing the method or constructor specified by <paramref name="handle" />, in the generic type specified by <paramref name="declaringType" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="handle" /> is invalid.</exception>
		// Token: 0x06004953 RID: 18771 RVA: 0x000EEDA4 File Offset: 0x000ECFA4
		[ComVisible(false)]
		public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle, RuntimeTypeHandle declaringType)
		{
			if (handle.IsNullHandle())
			{
				throw new ArgumentException(Environment.GetResourceString("The handle is invalid."));
			}
			MethodBase methodFromHandleInternalType = RuntimeMethodInfo.GetMethodFromHandleInternalType(handle.Value, declaringType.Value);
			if (methodFromHandleInternalType == null)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			return methodFromHandleInternalType;
		}

		// Token: 0x06004954 RID: 18772 RVA: 0x000EEDF4 File Offset: 0x000ECFF4
		internal static string ConstructParameters(Type[] parameterTypes, CallingConventions callingConvention, bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string value = "";
			foreach (Type type in parameterTypes)
			{
				stringBuilder.Append(value);
				string text = type.FormatTypeName(serialization);
				if (type.IsByRef && !serialization)
				{
					stringBuilder.Append(text.TrimEnd(new char[]
					{
						'&'
					}));
					stringBuilder.Append(" ByRef");
				}
				else
				{
					stringBuilder.Append(text);
				}
				value = ", ";
			}
			if ((callingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
			{
				stringBuilder.Append(value);
				stringBuilder.Append("...");
			}
			return stringBuilder.ToString();
		}

		/// <summary>Returns a <see langword="MethodBase" /> object representing the currently executing method.</summary>
		/// <returns>
		///   <see cref="M:System.Reflection.MethodBase.GetCurrentMethod" /> is a static method that is called from within an executing method and that returns information about that method.  
		/// A <see langword="MethodBase" /> object representing the currently executing method.</returns>
		/// <exception cref="T:System.Reflection.TargetException">This member was invoked with a late-binding mechanism.</exception>
		// Token: 0x06004955 RID: 18773
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern MethodBase GetCurrentMethod();

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004956 RID: 18774 RVA: 0x000173AD File Offset: 0x000155AD
		void _MethodBase.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Runtime.InteropServices._MethodBase.GetType" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.Runtime.InteropServices._MethodBase.GetType" />.</returns>
		// Token: 0x06004957 RID: 18775 RVA: 0x00052955 File Offset: 0x00050B55
		Type _MethodBase.GetType()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004958 RID: 18776 RVA: 0x000173AD File Offset: 0x000155AD
		void _MethodBase.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06004959 RID: 18777 RVA: 0x000173AD File Offset: 0x000155AD
		void _MethodBase.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x0600495A RID: 18778 RVA: 0x000173AD File Offset: 0x000155AD
		void _MethodBase.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
