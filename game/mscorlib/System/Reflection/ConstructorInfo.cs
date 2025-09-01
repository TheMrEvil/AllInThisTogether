using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using Unity;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a class constructor and provides access to constructor metadata.</summary>
	// Token: 0x02000895 RID: 2197
	[Serializable]
	public abstract class ConstructorInfo : MethodBase, _ConstructorInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ConstructorInfo" /> class.</summary>
		// Token: 0x06004882 RID: 18562 RVA: 0x000EE1B6 File Offset: 0x000EC3B6
		protected ConstructorInfo()
		{
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a constructor.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a constructor.</returns>
		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06004883 RID: 18563 RVA: 0x000040F7 File Offset: 0x000022F7
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Constructor;
			}
		}

		/// <summary>Invokes the constructor reflected by the instance that has the specified parameters, providing default values for the parameters not commonly used.</summary>
		/// <param name="parameters">An array of values that matches the number, order and type (under the constraints of the default binder) of the parameters for this constructor. If this constructor takes no parameters, then use either an array with zero elements or <see langword="null" />, as in Object[] parameters = new Object[0]. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is <see langword="null" />. For value-type elements, this value is 0, 0.0, or <see langword="false" />, depending on the specific element type.</param>
		/// <returns>An instance of the class associated with the constructor.</returns>
		/// <exception cref="T:System.MemberAccessException">The class is abstract.  
		///  -or-  
		///  The constructor is a class initializer.</exception>
		/// <exception cref="T:System.MethodAccessException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.  
		///
		///
		///
		///
		///  The constructor is private or protected, and the caller lacks <see cref="F:System.Security.Permissions.ReflectionPermissionFlag.MemberAccess" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="parameters" /> array does not contain values that match the types accepted by this constructor.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The invoked constructor throws an exception.</exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">An incorrect number of parameters was passed.</exception>
		/// <exception cref="T:System.NotSupportedException">Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, and <see cref="T:System.RuntimeArgumentHandle" /> types is not supported.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the necessary code access permission.</exception>
		// Token: 0x06004884 RID: 18564 RVA: 0x000EE1BE File Offset: 0x000EC3BE
		[DebuggerStepThrough]
		[DebuggerHidden]
		public object Invoke(object[] parameters)
		{
			return this.Invoke(BindingFlags.CreateInstance, null, parameters, null);
		}

		/// <summary>When implemented in a derived class, invokes the constructor reflected by this <see langword="ConstructorInfo" /> with the specified arguments, under the constraints of the specified <see langword="Binder" />.</summary>
		/// <param name="invokeAttr">One of the <see langword="BindingFlags" /> values that specifies the type of binding.</param>
		/// <param name="binder">A <see langword="Binder" /> that defines a set of properties and enables the binding, coercion of argument types, and invocation of members using reflection. If <paramref name="binder" /> is <see langword="null" />, then <see langword="Binder.DefaultBinding" /> is used.</param>
		/// <param name="parameters">An array of type <see langword="Object" /> used to match the number, order and type of the parameters for this constructor, under the constraints of <paramref name="binder" />. If this constructor does not require parameters, pass an array with zero elements, as in Object[] parameters = new Object[0]. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is <see langword="null" />. For value-type elements, this value is 0, 0.0, or <see langword="false" />, depending on the specific element type.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. If this is <see langword="null" />, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used.</param>
		/// <returns>An instance of the class associated with the constructor.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="parameters" /> array does not contain values that match the types accepted by this constructor, under the constraints of the <paramref name="binder" />.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The invoked constructor throws an exception.</exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">An incorrect number of parameters was passed.</exception>
		/// <exception cref="T:System.NotSupportedException">Creation of <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, and <see cref="T:System.RuntimeArgumentHandle" /> types is not supported.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the necessary code access permissions.</exception>
		/// <exception cref="T:System.MemberAccessException">The class is abstract.  
		///  -or-  
		///  The constructor is a class initializer.</exception>
		/// <exception cref="T:System.MethodAccessException">The constructor is private or protected, and the caller lacks <see cref="F:System.Security.Permissions.ReflectionPermissionFlag.MemberAccess" />.</exception>
		// Token: 0x06004885 RID: 18565
		public abstract object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004886 RID: 18566 RVA: 0x000EE1CE File Offset: 0x000EC3CE
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06004887 RID: 18567 RVA: 0x000EE1D7 File Offset: 0x000EC3D7
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.ConstructorInfo" /> objects are equal.</summary>
		/// <param name="left">The first <see cref="T:System.Reflection.ConstructorInfo" /> to compare.</param>
		/// <param name="right">The second <see cref="T:System.Reflection.ConstructorInfo" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is equal to <paramref name="right" />; otherwise <see langword="false" />.</returns>
		// Token: 0x06004888 RID: 18568 RVA: 0x00064560 File Offset: 0x00062760
		public static bool operator ==(ConstructorInfo left, ConstructorInfo right)
		{
			return left == right || (left != null && right != null && left.Equals(right));
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.ConstructorInfo" /> objects are not equal.</summary>
		/// <param name="left">The first <see cref="T:System.Reflection.ConstructorInfo" /> to compare.</param>
		/// <param name="right">The second <see cref="T:System.Reflection.ConstructorInfo" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise <see langword="false" />.</returns>
		// Token: 0x06004889 RID: 18569 RVA: 0x000EE1DF File Offset: 0x000EC3DF
		public static bool operator !=(ConstructorInfo left, ConstructorInfo right)
		{
			return !(left == right);
		}

		// Token: 0x0600488A RID: 18570 RVA: 0x000EE1EB File Offset: 0x000EC3EB
		// Note: this type is marked as 'beforefieldinit'.
		static ConstructorInfo()
		{
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600488B RID: 18571 RVA: 0x000173AD File Offset: 0x000155AD
		void _ConstructorInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a <see cref="T:System.Type" /> object representing the <see cref="T:System.Reflection.ConstructorInfo" /> type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Reflection.ConstructorInfo" /> type.</returns>
		// Token: 0x0600488C RID: 18572 RVA: 0x00052955 File Offset: 0x00050B55
		Type _ConstructorInfo.GetType()
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600488D RID: 18573 RVA: 0x000173AD File Offset: 0x000155AD
		void _ConstructorInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600488E RID: 18574 RVA: 0x000173AD File Offset: 0x000155AD
		void _ConstructorInfo.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x0600488F RID: 18575 RVA: 0x000173AD File Offset: 0x000155AD
		void _ConstructorInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MethodBase.Invoke(System.Object,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> method.</summary>
		/// <param name="obj">The instance that created this method.</param>
		/// <param name="invokeAttr">One of the <see cref="T:System.Reflection.BindingFlags" /> values that specifies the type of binding.</param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> that defines a set of properties and enables the binding, coercion of argument types, and invocation of members using reflection. If <paramref name="binder" /> is <see langword="null" />, then <see cref="P:System.Type.DefaultBinder" /> is used.</param>
		/// <param name="parameters">An array of objects used to match the number, order, and type of the parameters for this constructor, under the constraints of <paramref name="binder" />. If this constructor does not require parameters, pass an array with zero elements. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is <see langword="null" />. For value-type elements, this value is 0, 0.0, or <see langword="false" />, depending on the specific element type.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. If this is <see langword="null" />, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used.</param>
		/// <returns>An instance of the type.</returns>
		// Token: 0x06004890 RID: 18576 RVA: 0x00052955 File Offset: 0x00050B55
		object _ConstructorInfo.Invoke_2(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MethodBase.Invoke(System.Object,System.Object[])" /> method.</summary>
		/// <param name="obj">The instance that created this method.</param>
		/// <param name="parameters">An array of objects used to match the number, order, and type of the parameters for this constructor, under the constraints of <paramref name="binder" />. If this constructor does not require parameters, pass an array with zero elements. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is <see langword="null" />. For value-type elements, this value is 0, 0.0, or <see langword="false" />, depending on the specific element type.</param>
		/// <returns>An instance of the type.</returns>
		// Token: 0x06004891 RID: 18577 RVA: 0x00052955 File Offset: 0x00050B55
		object _ConstructorInfo.Invoke_3(object obj, object[] parameters)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.ConstructorInfo.Invoke(System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> method.</summary>
		/// <param name="invokeAttr">One of the <see cref="T:System.Reflection.BindingFlags" /> values that specifies the type of binding.</param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> that defines a set of properties and enables the binding, coercion of argument types, and invocation of members using reflection. If <paramref name="binder" /> is <see langword="null" />, then <see cref="P:System.Type.DefaultBinder" /> is used.</param>
		/// <param name="parameters">An array of objects used to match the number, order, and type of the parameters for this constructor, under the constraints of <paramref name="binder" />. If this constructor does not require parameters, pass an array with zero elements. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is <see langword="null" />. For value-type elements, this value is 0, 0.0, or <see langword="false" />, depending on the specific element type.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. If this is <see langword="null" />, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used.</param>
		/// <returns>An instance of the type.</returns>
		// Token: 0x06004892 RID: 18578 RVA: 0x00052955 File Offset: 0x00050B55
		object _ConstructorInfo.Invoke_4(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.ConstructorInfo.Invoke(System.Object[])" /> method.</summary>
		/// <param name="parameters">An array of objects used to match the number, order, and type of the parameters for this constructor, under the constraints of <paramref name="binder" />. If this constructor does not require parameters, pass an array with zero elements. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is <see langword="null" />. For value-type elements, this value is 0, 0.0, or <see langword="false" />, depending on the specific element type.</param>
		/// <returns>An instance of the type.</returns>
		// Token: 0x06004893 RID: 18579 RVA: 0x00052955 File Offset: 0x00050B55
		object _ConstructorInfo.Invoke_5(object[] parameters)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Represents the name of the class constructor method as it is stored in metadata. This name is always ".ctor". This field is read-only.</summary>
		// Token: 0x04002E83 RID: 11907
		public static readonly string ConstructorName = ".ctor";

		/// <summary>Represents the name of the type constructor method as it is stored in metadata. This name is always ".cctor". This property is read-only.</summary>
		// Token: 0x04002E84 RID: 11908
		public static readonly string TypeConstructorName = ".cctor";
	}
}
