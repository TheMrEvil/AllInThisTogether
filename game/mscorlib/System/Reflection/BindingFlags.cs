using System;

namespace System.Reflection
{
	/// <summary>Specifies flags that control binding and the way in which the search for members and types is conducted by reflection.</summary>
	// Token: 0x02000893 RID: 2195
	[Flags]
	public enum BindingFlags
	{
		/// <summary>Specifies that no binding flags are defined.</summary>
		// Token: 0x04002E68 RID: 11880
		Default = 0,
		/// <summary>Specifies that the case of the member name should not be considered when binding.</summary>
		// Token: 0x04002E69 RID: 11881
		IgnoreCase = 1,
		/// <summary>Specifies that only members declared at the level of the supplied type's hierarchy should be considered. Inherited members are not considered.</summary>
		// Token: 0x04002E6A RID: 11882
		DeclaredOnly = 2,
		/// <summary>Specifies that instance members are to be included in the search.</summary>
		// Token: 0x04002E6B RID: 11883
		Instance = 4,
		/// <summary>Specifies that static members are to be included in the search.</summary>
		// Token: 0x04002E6C RID: 11884
		Static = 8,
		/// <summary>Specifies that public members are to be included in the search.</summary>
		// Token: 0x04002E6D RID: 11885
		Public = 16,
		/// <summary>Specifies that non-public members are to be included in the search.</summary>
		// Token: 0x04002E6E RID: 11886
		NonPublic = 32,
		/// <summary>Specifies that public and protected static members up the hierarchy should be returned. Private static members in inherited classes are not returned. Static members include fields, methods, events, and properties. Nested types are not returned.</summary>
		// Token: 0x04002E6F RID: 11887
		FlattenHierarchy = 64,
		/// <summary>Specifies that a method is to be invoked. This must not be a constructor or a type initializer.  
		///  This flag is passed to an <see langword="InvokeMember" /> method to invoke a method.</summary>
		// Token: 0x04002E70 RID: 11888
		InvokeMethod = 256,
		/// <summary>Specifies that reflection should create an instance of the specified type. Calls the constructor that matches the given arguments. The supplied member name is ignored. If the type of lookup is not specified, (Instance | Public) will apply. It is not possible to call a type initializer.  
		///  This flag is passed to an <see langword="InvokeMember" /> method to invoke a constructor.</summary>
		// Token: 0x04002E71 RID: 11889
		CreateInstance = 512,
		/// <summary>Specifies that the value of the specified field should be returned.  
		///  This flag is passed to an <see langword="InvokeMember" /> method to get a field value.</summary>
		// Token: 0x04002E72 RID: 11890
		GetField = 1024,
		/// <summary>Specifies that the value of the specified field should be set.  
		///  This flag is passed to an <see langword="InvokeMember" /> method to set a field value.</summary>
		// Token: 0x04002E73 RID: 11891
		SetField = 2048,
		/// <summary>Specifies that the value of the specified property should be returned.  
		///  This flag is passed to an <see langword="InvokeMember" /> method to invoke a property getter.</summary>
		// Token: 0x04002E74 RID: 11892
		GetProperty = 4096,
		/// <summary>Specifies that the value of the specified property should be set. For COM properties, specifying this binding flag is equivalent to specifying <see langword="PutDispProperty" /> and <see langword="PutRefDispProperty" />.  
		///  This flag is passed to an <see langword="InvokeMember" /> method to invoke a property setter.</summary>
		// Token: 0x04002E75 RID: 11893
		SetProperty = 8192,
		/// <summary>Specifies that the <see langword="PROPPUT" /> member on a COM object should be invoked. <see langword="PROPPUT" /> specifies a property-setting function that uses a value. Use <see langword="PutDispProperty" /> if a property has both <see langword="PROPPUT" /> and <see langword="PROPPUTREF" /> and you need to distinguish which one is called.</summary>
		// Token: 0x04002E76 RID: 11894
		PutDispProperty = 16384,
		/// <summary>Specifies that the <see langword="PROPPUTREF" /> member on a COM object should be invoked. <see langword="PROPPUTREF" /> specifies a property-setting function that uses a reference instead of a value. Use <see langword="PutRefDispProperty" /> if a property has both <see langword="PROPPUT" /> and <see langword="PROPPUTREF" /> and you need to distinguish which one is called.</summary>
		// Token: 0x04002E77 RID: 11895
		PutRefDispProperty = 32768,
		/// <summary>Specifies that types of the supplied arguments must exactly match the types of the corresponding formal parameters. Reflection throws an exception if the caller supplies a non-null <see langword="Binder" /> object, since that implies that the caller is supplying <see langword="BindToXXX" /> implementations that will pick the appropriate method.</summary>
		// Token: 0x04002E78 RID: 11896
		ExactBinding = 65536,
		/// <summary>Not implemented.</summary>
		// Token: 0x04002E79 RID: 11897
		SuppressChangeType = 131072,
		/// <summary>Returns the set of members whose parameter count matches the number of supplied arguments. This binding flag is used for methods with parameters that have default values and methods with variable arguments (varargs). This flag should only be used with <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" />.</summary>
		// Token: 0x04002E7A RID: 11898
		OptionalParamBinding = 262144,
		/// <summary>Used in COM interop to specify that the return value of the member can be ignored.</summary>
		// Token: 0x04002E7B RID: 11899
		IgnoreReturn = 16777216,
		// Token: 0x04002E7C RID: 11900
		DoNotWrapExceptions = 33554432
	}
}
