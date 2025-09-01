using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Policy;
using System.Security.Principal;

namespace System
{
	/// <summary>Exposes the public members of the <see cref="T:System.AppDomain" /> class to unmanaged code.</summary>
	// Token: 0x02000202 RID: 514
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Guid("05F696DC-2B29-3663-AD8B-C4389CF2A713")]
	public interface _AppDomain
	{
		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06001618 RID: 5656
		void GetTypeInfoCount(out uint pcTInfo);

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06001619 RID: 5657
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x0600161A RID: 5658
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
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x0600161B RID: 5659
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.ToString" /> method.</summary>
		/// <returns>A string formed by concatenating the literal string "Name:", the friendly name of the application domain, and either string representations of the context policies or the string "There are no context policies."</returns>
		// Token: 0x0600161C RID: 5660
		string ToString();

		/// <summary>Provides COM objects with version-independent access to the inherited <see cref="M:System.Object.Equals(System.Object)" /> method.</summary>
		/// <param name="other">The <see cref="T:System.Object" /> to compare to the current <see cref="T:System.Object" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600161D RID: 5661
		bool Equals(object other);

		/// <summary>Provides COM objects with version-independent access to the inherited <see cref="M:System.Object.GetHashCode" /> method.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x0600161E RID: 5662
		int GetHashCode();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.GetType" /> method.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the type of the current instance.</returns>
		// Token: 0x0600161F RID: 5663
		Type GetType();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.InitializeLifetimeService" /> method.</summary>
		/// <returns>Always <see langword="null" />.</returns>
		// Token: 0x06001620 RID: 5664
		[SecurityCritical]
		object InitializeLifetimeService();

		/// <summary>Provides COM objects with version-independent access to the inherited <see cref="M:System.MarshalByRefObject.GetLifetimeService" /> method.</summary>
		/// <returns>An object of type <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> used to control the lifetime policy for this instance.</returns>
		// Token: 0x06001621 RID: 5665
		[SecurityCritical]
		object GetLifetimeService();

		/// <summary>Provides COM objects with version-independent access to the <see cref="E:System.AppDomain.DomainUnload" /> event.</summary>
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06001622 RID: 5666
		// (remove) Token: 0x06001623 RID: 5667
		event EventHandler DomainUnload;

		/// <summary>Provides COM objects with version-independent access to the <see cref="E:System.AppDomain.AssemblyLoad" /> event.</summary>
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06001624 RID: 5668
		// (remove) Token: 0x06001625 RID: 5669
		event AssemblyLoadEventHandler AssemblyLoad;

		/// <summary>Provides COM objects with version-independent access to the <see cref="E:System.AppDomain.ProcessExit" /> event.</summary>
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06001626 RID: 5670
		// (remove) Token: 0x06001627 RID: 5671
		event EventHandler ProcessExit;

		/// <summary>Provides COM objects with version-independent access to the <see cref="E:System.AppDomain.TypeResolve" /> event.</summary>
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06001628 RID: 5672
		// (remove) Token: 0x06001629 RID: 5673
		event ResolveEventHandler TypeResolve;

		/// <summary>Provides COM objects with version-independent access to the <see cref="E:System.AppDomain.ResourceResolve" /> event.</summary>
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600162A RID: 5674
		// (remove) Token: 0x0600162B RID: 5675
		event ResolveEventHandler ResourceResolve;

		/// <summary>Provides COM objects with version-independent access to the <see cref="E:System.AppDomain.AssemblyResolve" /> event.</summary>
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600162C RID: 5676
		// (remove) Token: 0x0600162D RID: 5677
		event ResolveEventHandler AssemblyResolve;

		/// <summary>Provides COM objects with version-independent access to the <see cref="E:System.AppDomain.UnhandledException" /> event.</summary>
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600162E RID: 5678
		// (remove) Token: 0x0600162F RID: 5679
		event UnhandledExceptionEventHandler UnhandledException;

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess)" /> method overload.</summary>
		/// <param name="name">The unique identity of the dynamic assembly.</param>
		/// <param name="access">The access mode for the dynamic assembly.</param>
		/// <returns>Represents the dynamic assembly created.</returns>
		// Token: 0x06001630 RID: 5680
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.String)" /> method overload.</summary>
		/// <param name="name">The unique identity of the dynamic assembly.</param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed.</param>
		/// <param name="dir">The name of the directory where the assembly will be saved. If <paramref name="dir" /> is <see langword="null" />, the directory defaults to the current directory.</param>
		/// <returns>Represents the dynamic assembly created.</returns>
		// Token: 0x06001631 RID: 5681
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.Security.Policy.Evidence)" /> method overload.</summary>
		/// <param name="name">The unique identity of the dynamic assembly.</param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed.</param>
		/// <param name="evidence">The evidence supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution.</param>
		/// <returns>Represents the dynamic assembly created.</returns>
		// Token: 0x06001632 RID: 5682
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.Security.PermissionSet,System.Security.PermissionSet,System.Security.PermissionSet)" /> method overload.</summary>
		/// <param name="name">The unique identity of the dynamic assembly.</param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed.</param>
		/// <param name="requiredPermissions">The required permissions request.</param>
		/// <param name="optionalPermissions">The optional permissions request.</param>
		/// <param name="refusedPermissions">The refused permissions request.</param>
		/// <returns>Represents the dynamic assembly created.</returns>
		// Token: 0x06001633 RID: 5683
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.String,System.Security.Policy.Evidence)" /> method overload.</summary>
		/// <param name="name">The unique identity of the dynamic assembly.</param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed.</param>
		/// <param name="dir">The name of the directory where the assembly will be saved. If <paramref name="dir" /> is <see langword="null" />, the directory defaults to the current directory.</param>
		/// <param name="evidence">The evidence supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution.</param>
		/// <returns>Represents the dynamic assembly created.</returns>
		// Token: 0x06001634 RID: 5684
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.String,System.Security.PermissionSet,System.Security.PermissionSet,System.Security.PermissionSet)" /> method overload.</summary>
		/// <param name="name">The unique identity of the dynamic assembly.</param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed.</param>
		/// <param name="dir">The name of the directory where the assembly will be saved. If <paramref name="dir" /> is <see langword="null" />, the directory defaults to the current directory.</param>
		/// <param name="requiredPermissions">The required permissions request.</param>
		/// <param name="optionalPermissions">The optional permissions request.</param>
		/// <param name="refusedPermissions">The refused permissions request.</param>
		/// <returns>Represents the dynamic assembly created.</returns>
		// Token: 0x06001635 RID: 5685
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.Security.Policy.Evidence,System.Security.PermissionSet,System.Security.PermissionSet,System.Security.PermissionSet)" /> method overload.</summary>
		/// <param name="name">The unique identity of the dynamic assembly.</param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed.</param>
		/// <param name="evidence">The evidence supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution.</param>
		/// <param name="requiredPermissions">The required permissions request.</param>
		/// <param name="optionalPermissions">The optional permissions request.</param>
		/// <param name="refusedPermissions">The refused permissions request.</param>
		/// <returns>Represents the dynamic assembly created.</returns>
		// Token: 0x06001636 RID: 5686
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.String,System.Security.Policy.Evidence,System.Security.PermissionSet,System.Security.PermissionSet,System.Security.PermissionSet)" /> method overload.</summary>
		/// <param name="name">The unique identity of the dynamic assembly.</param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed.</param>
		/// <param name="dir">The name of the directory where the assembly will be saved. If <paramref name="dir" /> is <see langword="null" />, the directory defaults to the current directory.</param>
		/// <param name="evidence">The evidence supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution.</param>
		/// <param name="requiredPermissions">The required permissions request.</param>
		/// <param name="optionalPermissions">The optional permissions request.</param>
		/// <param name="refusedPermissions">The refused permissions request.</param>
		/// <returns>Represents the dynamic assembly created.</returns>
		// Token: 0x06001637 RID: 5687
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.DefineDynamicAssembly(System.Reflection.AssemblyName,System.Reflection.Emit.AssemblyBuilderAccess,System.String,System.Security.Policy.Evidence,System.Security.PermissionSet,System.Security.PermissionSet,System.Security.PermissionSet,System.Boolean)" /> method overload.</summary>
		/// <param name="name">The unique identity of the dynamic assembly.</param>
		/// <param name="access">The mode in which the dynamic assembly will be accessed.</param>
		/// <param name="dir">The name of the directory where the dynamic assembly will be saved. If <paramref name="dir" /> is <see langword="null" />, the directory defaults to the current directory.</param>
		/// <param name="evidence">The evidence supplied for the dynamic assembly. The evidence is used unaltered as the final set of evidence used for policy resolution.</param>
		/// <param name="requiredPermissions">The required permissions request.</param>
		/// <param name="optionalPermissions">The optional permissions request.</param>
		/// <param name="refusedPermissions">The refused permissions request.</param>
		/// <param name="isSynchronized">
		///   <see langword="true" /> to synchronize the creation of modules, types, and members in the dynamic assembly; otherwise, <see langword="false" />.</param>
		/// <returns>Represents the dynamic assembly created.</returns>
		// Token: 0x06001638 RID: 5688
		AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, string dir, Evidence evidence, PermissionSet requiredPermissions, PermissionSet optionalPermissions, PermissionSet refusedPermissions, bool isSynchronized);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.CreateInstance(System.String,System.String)" /> method.</summary>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property.</param>
		/// <returns>An object that is a wrapper for the new instance specified by <paramref name="typeName" />. The return value needs to be unwrapped to access the real object.</returns>
		// Token: 0x06001639 RID: 5689
		ObjectHandle CreateInstance(string assemblyName, string typeName);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.CreateInstanceFrom(System.String,System.String)" /> method overload.</summary>
		/// <param name="assemblyFile">The name, including the path, of a file that contains an assembly that defines the requested type. The assembly is loaded using the <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" /> method.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property.</param>
		/// <returns>An object that is a wrapper for the new instance, or <see langword="null" /> if <paramref name="typeName" /> is not found. The return value needs to be unwrapped to access the real object.</returns>
		// Token: 0x0600163A RID: 5690
		ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.CreateInstance(System.String,System.String,System.Object[])" /> method overload.</summary>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property.</param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.</param>
		/// <returns>An object that is a wrapper for the new instance specified by <paramref name="typeName" />. The return value needs to be unwrapped to access the real object.</returns>
		// Token: 0x0600163B RID: 5691
		ObjectHandle CreateInstance(string assemblyName, string typeName, object[] activationAttributes);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.CreateInstanceFrom(System.String,System.String,System.Object[])" /> method overload.</summary>
		/// <param name="assemblyFile">The name, including the path, of a file that contains an assembly that defines the requested type. The assembly is loaded using the <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" /> method.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property.</param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.</param>
		/// <returns>An object that is a wrapper for the new instance, or <see langword="null" /> if <paramref name="typeName" /> is not found. The return value needs to be unwrapped to access the real object.</returns>
		// Token: 0x0600163C RID: 5692
		ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, object[] activationAttributes);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.CreateInstance(System.String,System.String,System.Boolean,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo,System.Object[],System.Security.Policy.Evidence)" /> method overload.</summary>
		/// <param name="assemblyName">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property.</param>
		/// <param name="ignoreCase">A Boolean value specifying whether to perform a case-sensitive search or not.</param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects using reflection. If <paramref name="binder" /> is null, the default binder is used.</param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to invoke. If the default constructor is preferred, <paramref name="args" /> must be an empty array or null.</param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is <see langword="null" />, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used.</param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.</param>
		/// <param name="securityAttributes">Information used to authorize creation of <paramref name="typeName" />.</param>
		/// <returns>An object that is a wrapper for the new instance specified by <paramref name="typeName" />. The return value needs to be unwrapped to access the real object.</returns>
		// Token: 0x0600163D RID: 5693
		ObjectHandle CreateInstance(string assemblyName, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.CreateInstanceFrom(System.String,System.String,System.Boolean,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo,System.Object[],System.Security.Policy.Evidence)" /> method overload.</summary>
		/// <param name="assemblyFile">The name, including the path, of a file that contains an assembly that defines the requested type. The assembly is loaded using the <see cref="M:System.Reflection.Assembly.LoadFrom(System.String)" /> method.</param>
		/// <param name="typeName">The fully qualified name of the requested type, including the namespace but not the assembly, as returned by the <see cref="P:System.Type.FullName" /> property.</param>
		/// <param name="ignoreCase">A Boolean value specifying whether to perform a case-sensitive search or not.</param>
		/// <param name="bindingAttr">A combination of zero or more bit flags that affect the search for the <paramref name="typeName" /> constructor. If <paramref name="bindingAttr" /> is zero, a case-sensitive search for public constructors is conducted.</param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is null, the default binder is used.</param>
		/// <param name="args">The arguments to pass to the constructor. This array of arguments must match in number, order, and type the parameters of the constructor to invoke. If the default constructor is preferred, <paramref name="args" /> must be an empty array or null.</param>
		/// <param name="culture">Culture-specific information that governs the coercion of <paramref name="args" /> to the formal types declared for the <paramref name="typeName" /> constructor. If <paramref name="culture" /> is <see langword="null" />, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used.</param>
		/// <param name="activationAttributes">An array of one or more attributes that can participate in activation. Typically, an array that contains a single <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> object. The <see cref="T:System.Runtime.Remoting.Activation.UrlAttribute" /> specifies the URL that is required to activate a remote object.</param>
		/// <param name="securityAttributes">Information used to authorize creation of <paramref name="typeName" />.</param>
		/// <returns>An object that is a wrapper for the new instance, or <see langword="null" /> if <paramref name="typeName" /> is not found. The return value needs to be unwrapped to access the real object.</returns>
		// Token: 0x0600163E RID: 5694
		ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes, Evidence securityAttributes);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.Load(System.Reflection.AssemblyName)" /> method overload.</summary>
		/// <param name="assemblyRef">An object that describes the assembly to load.</param>
		/// <returns>The loaded assembly.</returns>
		// Token: 0x0600163F RID: 5695
		Assembly Load(AssemblyName assemblyRef);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.Load(System.String)" /> method overload.</summary>
		/// <param name="assemblyString">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <returns>The loaded assembly.</returns>
		// Token: 0x06001640 RID: 5696
		Assembly Load(string assemblyString);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.Load(System.Byte[])" /> method overload.</summary>
		/// <param name="rawAssembly">An array of type <see langword="byte" /> that is a COFF-based image containing an emitted assembly.</param>
		/// <returns>The loaded assembly.</returns>
		// Token: 0x06001641 RID: 5697
		Assembly Load(byte[] rawAssembly);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.Load(System.Byte[],System.Byte[])" /> method overload.</summary>
		/// <param name="rawAssembly">An array of type <see langword="byte" /> that is a COFF-based image containing an emitted assembly.</param>
		/// <param name="rawSymbolStore">An array of type <see langword="byte" /> containing the raw bytes representing the symbols for the assembly.</param>
		/// <returns>The loaded assembly.</returns>
		// Token: 0x06001642 RID: 5698
		Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.Load(System.Byte[],System.Byte[],System.Security.Policy.Evidence)" /> method overload.</summary>
		/// <param name="rawAssembly">An array of type <see langword="byte" /> that is a COFF-based image containing an emitted assembly.</param>
		/// <param name="rawSymbolStore">An array of type <see langword="byte" /> containing the raw bytes representing the symbols for the assembly.</param>
		/// <param name="securityEvidence">Evidence for loading the assembly.</param>
		/// <returns>The loaded assembly.</returns>
		// Token: 0x06001643 RID: 5699
		Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore, Evidence securityEvidence);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.Load(System.Reflection.AssemblyName,System.Security.Policy.Evidence)" /> method overload.</summary>
		/// <param name="assemblyRef">An object that describes the assembly to load.</param>
		/// <param name="assemblySecurity">Evidence for loading the assembly.</param>
		/// <returns>The loaded assembly.</returns>
		// Token: 0x06001644 RID: 5700
		Assembly Load(AssemblyName assemblyRef, Evidence assemblySecurity);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.Load(System.String,System.Security.Policy.Evidence)" /> method overload.</summary>
		/// <param name="assemblyString">The display name of the assembly. See <see cref="P:System.Reflection.Assembly.FullName" />.</param>
		/// <param name="assemblySecurity">Evidence for loading the assembly.</param>
		/// <returns>The loaded assembly.</returns>
		// Token: 0x06001645 RID: 5701
		Assembly Load(string assemblyString, Evidence assemblySecurity);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.ExecuteAssembly(System.String,System.Security.Policy.Evidence)" /> method overload.</summary>
		/// <param name="assemblyFile">The name of the file that contains the assembly to execute.</param>
		/// <param name="assemblySecurity">Evidence for loading the assembly.</param>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		// Token: 0x06001646 RID: 5702
		int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.ExecuteAssembly(System.String)" /> method overload.</summary>
		/// <param name="assemblyFile">The name of the file that contains the assembly to execute.</param>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		// Token: 0x06001647 RID: 5703
		int ExecuteAssembly(string assemblyFile);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.ExecuteAssembly(System.String,System.Security.Policy.Evidence,System.String[])" /> method overload.</summary>
		/// <param name="assemblyFile">The name of the file that contains the assembly to execute.</param>
		/// <param name="assemblySecurity">The supplied evidence for the assembly.</param>
		/// <param name="args">The arguments to the entry point of the assembly.</param>
		/// <returns>The value returned by the entry point of the assembly.</returns>
		// Token: 0x06001648 RID: 5704
		int ExecuteAssembly(string assemblyFile, Evidence assemblySecurity, string[] args);

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.AppDomain.FriendlyName" /> property.</summary>
		/// <returns>The friendly name of this application domain.</returns>
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06001649 RID: 5705
		string FriendlyName { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.AppDomain.BaseDirectory" /> property.</summary>
		/// <returns>The base directory that the assembly resolver uses to probe for assemblies.</returns>
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600164A RID: 5706
		string BaseDirectory { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.AppDomain.RelativeSearchPath" /> property.</summary>
		/// <returns>The path under the base directory where the assembly resolver should probe for private assemblies.</returns>
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600164B RID: 5707
		string RelativeSearchPath { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.AppDomain.ShadowCopyFiles" /> property.</summary>
		/// <returns>
		///   <see langword="true" /> if the application domain is configured to shadow copy files; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600164C RID: 5708
		bool ShadowCopyFiles { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.GetAssemblies" /> method.</summary>
		/// <returns>An array of assemblies in this application domain.</returns>
		// Token: 0x0600164D RID: 5709
		Assembly[] GetAssemblies();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.AppendPrivatePath(System.String)" /> method.</summary>
		/// <param name="path">The name of the directory to be appended to the private path.</param>
		// Token: 0x0600164E RID: 5710
		[SecurityCritical]
		void AppendPrivatePath(string path);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.ClearPrivatePath" /> method.</summary>
		// Token: 0x0600164F RID: 5711
		[SecurityCritical]
		void ClearPrivatePath();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.SetShadowCopyPath(System.String)" /> method.</summary>
		/// <param name="s">A list of directory names, where each name is separated by a semicolon.</param>
		// Token: 0x06001650 RID: 5712
		[SecurityCritical]
		void SetShadowCopyPath(string s);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.ClearShadowCopyPath" /> method.</summary>
		// Token: 0x06001651 RID: 5713
		[SecurityCritical]
		void ClearShadowCopyPath();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.SetCachePath(System.String)" /> method.</summary>
		/// <param name="s">The fully qualified path to the shadow copy location.</param>
		// Token: 0x06001652 RID: 5714
		[SecurityCritical]
		void SetCachePath(string s);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.SetData(System.String,System.Object)" /> method.</summary>
		/// <param name="name">The name of a user-defined application domain property to create or change.</param>
		/// <param name="data">The value of the property.</param>
		// Token: 0x06001653 RID: 5715
		[SecurityCritical]
		void SetData(string name, object data);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.GetData(System.String)" /> method.</summary>
		/// <param name="name">The name of a predefined application domain property, or the name of an application domain property you have defined.</param>
		/// <returns>The value of the <paramref name="name" /> property.</returns>
		// Token: 0x06001654 RID: 5716
		object GetData(string name);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.DoCallBack(System.CrossAppDomainDelegate)" /> method.</summary>
		/// <param name="theDelegate">A delegate that specifies a method to call.</param>
		// Token: 0x06001655 RID: 5717
		void DoCallBack(CrossAppDomainDelegate theDelegate);

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.AppDomain.DynamicDirectory" /> property.</summary>
		/// <returns>Get the directory that the assembly resolver uses to probe for dynamically-created assemblies.</returns>
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06001656 RID: 5718
		string DynamicDirectory { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.AppDomain.Evidence" /> property.</summary>
		/// <returns>Gets the <see cref="T:System.Security.Policy.Evidence" /> associated with this application domain that is used as input to the security policy.</returns>
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06001657 RID: 5719
		Evidence Evidence { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.SetAppDomainPolicy(System.Security.Policy.PolicyLevel)" /> method.</summary>
		/// <param name="domainPolicy">The security policy level.</param>
		// Token: 0x06001658 RID: 5720
		[SecurityCritical]
		void SetAppDomainPolicy(PolicyLevel domainPolicy);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.SetPrincipalPolicy(System.Security.Principal.PrincipalPolicy)" /> method.</summary>
		/// <param name="policy">One of the <see cref="T:System.Security.Principal.PrincipalPolicy" /> values that specifies the type of the principal object to attach to threads.</param>
		// Token: 0x06001659 RID: 5721
		void SetPrincipalPolicy(PrincipalPolicy policy);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.AppDomain.SetThreadPrincipal(System.Security.Principal.IPrincipal)" /> method.</summary>
		/// <param name="principal">The principal object to attach to threads.</param>
		// Token: 0x0600165A RID: 5722
		void SetThreadPrincipal(IPrincipal principal);
	}
}
