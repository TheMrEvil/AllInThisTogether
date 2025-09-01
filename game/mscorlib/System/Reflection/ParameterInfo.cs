using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using Unity;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a parameter and provides access to parameter metadata.</summary>
	// Token: 0x020008B6 RID: 2230
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class ParameterInfo : ICustomAttributeProvider, IObjectReference, _ParameterInfo
	{
		/// <summary>Initializes a new instance of the <see langword="ParameterInfo" /> class.</summary>
		// Token: 0x060049BF RID: 18879 RVA: 0x0000259F File Offset: 0x0000079F
		protected ParameterInfo()
		{
		}

		/// <summary>Gets the attributes for this parameter.</summary>
		/// <returns>A <see langword="ParameterAttributes" /> object representing the attributes for this parameter.</returns>
		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x060049C0 RID: 18880 RVA: 0x000EF207 File Offset: 0x000ED407
		public virtual ParameterAttributes Attributes
		{
			get
			{
				return this.AttrsImpl;
			}
		}

		/// <summary>Gets a value indicating the member in which the parameter is implemented.</summary>
		/// <returns>The member which implanted the parameter represented by this <see cref="T:System.Reflection.ParameterInfo" />.</returns>
		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x060049C1 RID: 18881 RVA: 0x000EF20F File Offset: 0x000ED40F
		public virtual MemberInfo Member
		{
			get
			{
				return this.MemberImpl;
			}
		}

		/// <summary>Gets the name of the parameter.</summary>
		/// <returns>The simple name of this parameter.</returns>
		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x060049C2 RID: 18882 RVA: 0x000EF217 File Offset: 0x000ED417
		public virtual string Name
		{
			get
			{
				return this.NameImpl;
			}
		}

		/// <summary>Gets the <see langword="Type" /> of this parameter.</summary>
		/// <returns>The <see langword="Type" /> object that represents the <see langword="Type" /> of this parameter.</returns>
		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x060049C3 RID: 18883 RVA: 0x000EF21F File Offset: 0x000ED41F
		public virtual Type ParameterType
		{
			get
			{
				return this.ClassImpl;
			}
		}

		/// <summary>Gets the zero-based position of the parameter in the formal parameter list.</summary>
		/// <returns>An integer representing the position this parameter occupies in the parameter list.</returns>
		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x060049C4 RID: 18884 RVA: 0x000EF227 File Offset: 0x000ED427
		public virtual int Position
		{
			get
			{
				return this.PositionImpl;
			}
		}

		/// <summary>Gets a value indicating whether this is an input parameter.</summary>
		/// <returns>
		///   <see langword="true" /> if the parameter is an input parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x060049C5 RID: 18885 RVA: 0x000EF22F File Offset: 0x000ED42F
		public bool IsIn
		{
			get
			{
				return (this.Attributes & ParameterAttributes.In) > ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether this parameter is a locale identifier (lcid).</summary>
		/// <returns>
		///   <see langword="true" /> if the parameter is a locale identifier; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x060049C6 RID: 18886 RVA: 0x000EF23C File Offset: 0x000ED43C
		public bool IsLcid
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Lcid) > ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether this parameter is optional.</summary>
		/// <returns>
		///   <see langword="true" /> if the parameter is optional; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x060049C7 RID: 18887 RVA: 0x000EF249 File Offset: 0x000ED449
		public bool IsOptional
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Optional) > ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether this is an output parameter.</summary>
		/// <returns>
		///   <see langword="true" /> if the parameter is an output parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x060049C8 RID: 18888 RVA: 0x000EF257 File Offset: 0x000ED457
		public bool IsOut
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Out) > ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether this is a <see langword="Retval" /> parameter.</summary>
		/// <returns>
		///   <see langword="true" /> if the parameter is a <see langword="Retval" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x060049C9 RID: 18889 RVA: 0x000EF264 File Offset: 0x000ED464
		public bool IsRetval
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Retval) > ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating the default value if the parameter has a default value.</summary>
		/// <returns>The default value of the parameter, or <see cref="F:System.DBNull.Value" /> if the parameter has no default value.</returns>
		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x060049CA RID: 18890 RVA: 0x00047226 File Offset: 0x00045426
		public virtual object DefaultValue
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Gets a value indicating the default value if the parameter has a default value.</summary>
		/// <returns>The default value of the parameter, or <see cref="F:System.DBNull.Value" /> if the parameter has no default value.</returns>
		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x060049CB RID: 18891 RVA: 0x00047226 File Offset: 0x00045426
		public virtual object RawDefaultValue
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Gets a value that indicates whether this parameter has a default value.</summary>
		/// <returns>
		///   <see langword="true" /> if this parameter has a default value; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x060049CC RID: 18892 RVA: 0x00047226 File Offset: 0x00045426
		public virtual bool HasDefaultValue
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		/// <summary>Determines whether the custom attribute of the specified type or its derived types is applied to this parameter.</summary>
		/// <param name="attributeType">The <see langword="Type" /> object to search for.</param>
		/// <param name="inherit">This argument is ignored for objects of this type.</param>
		/// <returns>
		///   <see langword="true" /> if one or more instances of <paramref name="attributeType" /> or its derived types are applied to this parameter; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not a <see cref="T:System.Type" /> object supplied by the common language runtime.</exception>
		// Token: 0x060049CD RID: 18893 RVA: 0x000EF271 File Offset: 0x000ED471
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return false;
		}

		/// <summary>Gets a collection that contains this parameter's custom attributes.</summary>
		/// <returns>A collection that contains this parameter's custom attributes.</returns>
		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x060049CE RID: 18894 RVA: 0x000EF288 File Offset: 0x000ED488
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects for the current parameter, which can be used in the reflection-only context.</summary>
		/// <returns>A generic list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the current parameter.</returns>
		// Token: 0x060049CF RID: 18895 RVA: 0x00047226 File Offset: 0x00045426
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw NotImplemented.ByDesign;
		}

		/// <summary>Gets all the custom attributes defined on this parameter.</summary>
		/// <param name="inherit">This argument is ignored for objects of this type.</param>
		/// <returns>An array that contains all the custom attributes applied to this parameter.</returns>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type could not be loaded.</exception>
		// Token: 0x060049D0 RID: 18896 RVA: 0x000EF290 File Offset: 0x000ED490
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			return Array.Empty<object>();
		}

		/// <summary>Gets the custom attributes of the specified type or its derived types that are applied to this parameter.</summary>
		/// <param name="attributeType">The custom attributes identified by type.</param>
		/// <param name="inherit">This argument is ignored for objects of this type.</param>
		/// <returns>An array that contains the custom attributes of the specified type or its derived types.</returns>
		/// <exception cref="T:System.ArgumentException">The type must be a type provided by the underlying runtime system.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type could not be loaded.</exception>
		// Token: 0x060049D1 RID: 18897 RVA: 0x000EF297 File Offset: 0x000ED497
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return Array.Empty<object>();
		}

		/// <summary>Gets the optional custom modifiers of the parameter.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that identify the optional custom modifiers of the current parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.</returns>
		// Token: 0x060049D2 RID: 18898 RVA: 0x000EF2B2 File Offset: 0x000ED4B2
		public virtual Type[] GetOptionalCustomModifiers()
		{
			return Array.Empty<Type>();
		}

		/// <summary>Gets the required custom modifiers of the parameter.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that identify the required custom modifiers of the current parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.</returns>
		// Token: 0x060049D3 RID: 18899 RVA: 0x000EF2B2 File Offset: 0x000ED4B2
		public virtual Type[] GetRequiredCustomModifiers()
		{
			return Array.Empty<Type>();
		}

		/// <summary>Gets a value that identifies this parameter in metadata.</summary>
		/// <returns>A value which, in combination with the module, uniquely identifies this parameter in metadata.</returns>
		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x060049D4 RID: 18900 RVA: 0x000EF2B9 File Offset: 0x000ED4B9
		public virtual int MetadataToken
		{
			get
			{
				return 134217728;
			}
		}

		/// <summary>Returns the real object that should be deserialized instead of the object that the serialized stream specifies.</summary>
		/// <param name="context">The serialized stream from which the current object is deserialized.</param>
		/// <returns>The actual object that is put into the graph.</returns>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The parameter's position in the parameter list of its associated member is not valid for that member's type.</exception>
		// Token: 0x060049D5 RID: 18901 RVA: 0x000EF2C0 File Offset: 0x000ED4C0
		[SecurityCritical]
		public object GetRealObject(StreamingContext context)
		{
			if (this.MemberImpl == null)
			{
				throw new SerializationException("Insufficient state to return the real object.");
			}
			MemberTypes memberType = this.MemberImpl.MemberType;
			if (memberType != MemberTypes.Constructor && memberType != MemberTypes.Method)
			{
				if (memberType != MemberTypes.Property)
				{
					throw new SerializationException("Serialized member does not have a ParameterInfo.");
				}
				ParameterInfo[] array = ((PropertyInfo)this.MemberImpl).GetIndexParameters();
				if (array != null && this.PositionImpl > -1 && this.PositionImpl < array.Length)
				{
					return array[this.PositionImpl];
				}
				throw new SerializationException("Non existent ParameterInfo. Position bigger than member's parameters length.");
			}
			else if (this.PositionImpl == -1)
			{
				if (this.MemberImpl.MemberType == MemberTypes.Method)
				{
					return ((MethodInfo)this.MemberImpl).ReturnParameter;
				}
				throw new SerializationException("Non existent ParameterInfo. Position bigger than member's parameters length.");
			}
			else
			{
				ParameterInfo[] array = ((MethodBase)this.MemberImpl).GetParametersNoCopy();
				if (array != null && this.PositionImpl < array.Length)
				{
					return array[this.PositionImpl];
				}
				throw new SerializationException("Non existent ParameterInfo. Position bigger than member's parameters length.");
			}
		}

		/// <summary>Gets the parameter type and name represented as a string.</summary>
		/// <returns>A string containing the type and the name of the parameter.</returns>
		// Token: 0x060049D6 RID: 18902 RVA: 0x000EF3B2 File Offset: 0x000ED5B2
		public override string ToString()
		{
			return this.ParameterType.FormatTypeName() + " " + this.Name;
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060049D7 RID: 18903 RVA: 0x000173AD File Offset: 0x000155AD
		void _ParameterInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060049D8 RID: 18904 RVA: 0x000173AD File Offset: 0x000155AD
		void _ParameterInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060049D9 RID: 18905 RVA: 0x000173AD File Offset: 0x000155AD
		void _ParameterInfo.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x060049DA RID: 18906 RVA: 0x000173AD File Offset: 0x000155AD
		void _ParameterInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>The attributes of the parameter.</summary>
		// Token: 0x04002F05 RID: 12037
		protected ParameterAttributes AttrsImpl;

		/// <summary>The <see langword="Type" /> of the parameter.</summary>
		// Token: 0x04002F06 RID: 12038
		protected Type ClassImpl;

		/// <summary>The default value of the parameter.</summary>
		// Token: 0x04002F07 RID: 12039
		protected object DefaultValueImpl;

		/// <summary>The member in which the field is implemented.</summary>
		// Token: 0x04002F08 RID: 12040
		protected MemberInfo MemberImpl;

		/// <summary>The name of the parameter.</summary>
		// Token: 0x04002F09 RID: 12041
		protected string NameImpl;

		/// <summary>The zero-based position of the parameter in the parameter list.</summary>
		// Token: 0x04002F0A RID: 12042
		protected int PositionImpl;

		// Token: 0x04002F0B RID: 12043
		private const int MetadataToken_ParamDef = 134217728;
	}
}
