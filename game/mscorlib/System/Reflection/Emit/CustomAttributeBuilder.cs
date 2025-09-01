using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection.Emit
{
	/// <summary>Helps build custom attributes.</summary>
	// Token: 0x02000917 RID: 2327
	[ComDefaultInterface(typeof(_CustomAttributeBuilder))]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public class CustomAttributeBuilder : _CustomAttributeBuilder
	{
		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004F36 RID: 20278 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _CustomAttributeBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004F37 RID: 20279 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _CustomAttributeBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06004F38 RID: 20280 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _CustomAttributeBuilder.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x06004F39 RID: 20281 RVA: 0x000479F8 File Offset: 0x00045BF8
		void _CustomAttributeBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06004F3A RID: 20282 RVA: 0x000F87DF File Offset: 0x000F69DF
		internal ConstructorInfo Ctor
		{
			get
			{
				return this.ctor;
			}
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x06004F3B RID: 20283 RVA: 0x000F87E7 File Offset: 0x000F69E7
		internal byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x06004F3C RID: 20284
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern byte[] GetBlob(Assembly asmb, ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues, FieldInfo[] namedFields, object[] fieldValues);

		// Token: 0x06004F3D RID: 20285 RVA: 0x000F87F0 File Offset: 0x000F69F0
		internal object Invoke()
		{
			object obj = this.ctor.Invoke(this.args);
			for (int i = 0; i < this.namedFields.Length; i++)
			{
				this.namedFields[i].SetValue(obj, this.fieldValues[i]);
			}
			for (int j = 0; j < this.namedProperties.Length; j++)
			{
				this.namedProperties[j].SetValue(obj, this.propertyValues[j]);
			}
			return obj;
		}

		// Token: 0x06004F3E RID: 20286 RVA: 0x000F8864 File Offset: 0x000F6A64
		internal CustomAttributeBuilder(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			this.ctor = con;
			this.data = (byte[])binaryAttribute.Clone();
		}

		/// <summary>Initializes an instance of the <see langword="CustomAttributeBuilder" /> class given the constructor for the custom attribute and the arguments to the constructor.</summary>
		/// <param name="con">The constructor for the custom attribute.</param>
		/// <param name="constructorArgs">The arguments to the constructor of the custom attribute.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="con" /> is static or private.  
		/// -or-  
		/// The number of supplied arguments does not match the number of parameters of the constructor as required by the calling convention of the constructor.  
		/// -or-  
		/// The type of supplied argument does not match the type of the parameter declared in the constructor.  
		/// -or-  
		/// A supplied argument is a reference type other than <see cref="T:System.String" /> or <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="constructorArgs" /> is <see langword="null" />.</exception>
		// Token: 0x06004F3F RID: 20287 RVA: 0x000F88B1 File Offset: 0x000F6AB1
		public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs)
		{
			this.Initialize(con, constructorArgs, new PropertyInfo[0], new object[0], new FieldInfo[0], new object[0]);
		}

		/// <summary>Initializes an instance of the <see langword="CustomAttributeBuilder" /> class given the constructor for the custom attribute, the arguments to the constructor, and a set of named field/value pairs.</summary>
		/// <param name="con">The constructor for the custom attribute.</param>
		/// <param name="constructorArgs">The arguments to the constructor of the custom attribute.</param>
		/// <param name="namedFields">Named fields of the custom attribute.</param>
		/// <param name="fieldValues">Values for the named fields of the custom attribute.</param>
		/// <exception cref="T:System.ArgumentException">The lengths of the <paramref name="namedFields" /> and <paramref name="fieldValues" /> arrays are different.  
		///  -or-  
		///  <paramref name="con" /> is static or private.  
		///  -or-  
		///  The number of supplied arguments does not match the number of parameters of the constructor as required by the calling convention of the constructor.  
		///  -or-  
		///  The type of supplied argument does not match the type of the parameter declared in the constructor.  
		///  -or-  
		///  The types of the field values do not match the types of the named fields.  
		///  -or-  
		///  The field does not belong to the same class or base class as the constructor.  
		///  -or-  
		///  A supplied argument or named field is a reference type other than <see cref="T:System.String" /> or <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">One of the parameters is <see langword="null" />.</exception>
		// Token: 0x06004F40 RID: 20288 RVA: 0x000F88D9 File Offset: 0x000F6AD9
		public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, FieldInfo[] namedFields, object[] fieldValues)
		{
			this.Initialize(con, constructorArgs, new PropertyInfo[0], new object[0], namedFields, fieldValues);
		}

		/// <summary>Initializes an instance of the <see langword="CustomAttributeBuilder" /> class given the constructor for the custom attribute, the arguments to the constructor, and a set of named property or value pairs.</summary>
		/// <param name="con">The constructor for the custom attribute.</param>
		/// <param name="constructorArgs">The arguments to the constructor of the custom attribute.</param>
		/// <param name="namedProperties">Named properties of the custom attribute.</param>
		/// <param name="propertyValues">Values for the named properties of the custom attribute.</param>
		/// <exception cref="T:System.ArgumentException">The lengths of the <paramref name="namedProperties" /> and <paramref name="propertyValues" /> arrays are different.  
		///  -or-  
		///  <paramref name="con" /> is static or private.  
		///  -or-  
		///  The number of supplied arguments does not match the number of parameters of the constructor as required by the calling convention of the constructor.  
		///  -or-  
		///  The type of supplied argument does not match the type of the parameter declared in the constructor.  
		///  -or-  
		///  The types of the property values do not match the types of the named properties.  
		///  -or-  
		///  A property has no setter method.  
		///  -or-  
		///  The property does not belong to the same class or base class as the constructor.  
		///  -or-  
		///  A supplied argument or named property is a reference type other than <see cref="T:System.String" /> or <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">One of the parameters is <see langword="null" />.</exception>
		// Token: 0x06004F41 RID: 20289 RVA: 0x000F88F8 File Offset: 0x000F6AF8
		public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues)
		{
			this.Initialize(con, constructorArgs, namedProperties, propertyValues, new FieldInfo[0], new object[0]);
		}

		/// <summary>Initializes an instance of the <see langword="CustomAttributeBuilder" /> class given the constructor for the custom attribute, the arguments to the constructor, a set of named property or value pairs, and a set of named field or value pairs.</summary>
		/// <param name="con">The constructor for the custom attribute.</param>
		/// <param name="constructorArgs">The arguments to the constructor of the custom attribute.</param>
		/// <param name="namedProperties">Named properties of the custom attribute.</param>
		/// <param name="propertyValues">Values for the named properties of the custom attribute.</param>
		/// <param name="namedFields">Named fields of the custom attribute.</param>
		/// <param name="fieldValues">Values for the named fields of the custom attribute.</param>
		/// <exception cref="T:System.ArgumentException">The lengths of the <paramref name="namedProperties" /> and <paramref name="propertyValues" /> arrays are different.  
		///  -or-  
		///  The lengths of the <paramref name="namedFields" /> and <paramref name="fieldValues" /> arrays are different.  
		///  -or-  
		///  <paramref name="con" /> is static or private.  
		///  -or-  
		///  The number of supplied arguments does not match the number of parameters of the constructor as required by the calling convention of the constructor.  
		///  -or-  
		///  The type of supplied argument does not match the type of the parameter declared in the constructor.  
		///  -or-  
		///  The types of the property values do not match the types of the named properties.  
		///  -or-  
		///  The types of the field values do not match the types of the corresponding field types.  
		///  -or-  
		///  A property has no setter.  
		///  -or-  
		///  The property or field does not belong to the same class or base class as the constructor.  
		///  -or-  
		///  A supplied argument, named property, or named field is a reference type other than <see cref="T:System.String" /> or <see cref="T:System.Type" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">One of the parameters is <see langword="null" />.</exception>
		// Token: 0x06004F42 RID: 20290 RVA: 0x000F8917 File Offset: 0x000F6B17
		public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues, FieldInfo[] namedFields, object[] fieldValues)
		{
			this.Initialize(con, constructorArgs, namedProperties, propertyValues, namedFields, fieldValues);
		}

		// Token: 0x06004F43 RID: 20291 RVA: 0x000F8930 File Offset: 0x000F6B30
		private bool IsValidType(Type t)
		{
			if (t.IsArray && t.GetArrayRank() > 1)
			{
				return false;
			}
			if (t is TypeBuilder && t.IsEnum)
			{
				Enum.GetUnderlyingType(t);
			}
			return (!t.IsClass || t.IsArray || t == typeof(object) || t == typeof(Type) || t == typeof(string) || t.Assembly.GetName().Name == "mscorlib") && (!t.IsValueType || t.IsPrimitive || t.IsEnum || (t.Assembly is AssemblyBuilder && t.Assembly.GetName().Name == "mscorlib"));
		}

		// Token: 0x06004F44 RID: 20292 RVA: 0x000F8A10 File Offset: 0x000F6C10
		private bool IsValidParam(object o, Type paramType)
		{
			Type type = o.GetType();
			if (!this.IsValidType(type))
			{
				return false;
			}
			if (paramType == typeof(object))
			{
				if (type.IsArray && type.GetArrayRank() == 1)
				{
					return this.IsValidType(type.GetElementType());
				}
				if (!type.IsPrimitive && !typeof(Type).IsAssignableFrom(type) && type != typeof(string) && !type.IsEnum)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004F45 RID: 20293 RVA: 0x000F8A98 File Offset: 0x000F6C98
		private static bool IsValidValue(Type type, object value)
		{
			if (type.IsValueType && value == null)
			{
				return false;
			}
			if (type.IsArray && type.GetElementType().IsValueType)
			{
				using (IEnumerator enumerator = ((Array)value).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == null)
						{
							return false;
						}
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06004F46 RID: 20294 RVA: 0x000F8B10 File Offset: 0x000F6D10
		private void Initialize(ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues, FieldInfo[] namedFields, object[] fieldValues)
		{
			this.ctor = con;
			this.args = constructorArgs;
			this.namedProperties = namedProperties;
			this.propertyValues = propertyValues;
			this.namedFields = namedFields;
			this.fieldValues = fieldValues;
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (constructorArgs == null)
			{
				throw new ArgumentNullException("constructorArgs");
			}
			if (namedProperties == null)
			{
				throw new ArgumentNullException("namedProperties");
			}
			if (propertyValues == null)
			{
				throw new ArgumentNullException("propertyValues");
			}
			if (namedFields == null)
			{
				throw new ArgumentNullException("namedFields");
			}
			if (fieldValues == null)
			{
				throw new ArgumentNullException("fieldValues");
			}
			if (con.GetParametersCount() != constructorArgs.Length)
			{
				throw new ArgumentException("Parameter count does not match passed in argument value count.");
			}
			if (namedProperties.Length != propertyValues.Length)
			{
				throw new ArgumentException("Array lengths must be the same.", "namedProperties, propertyValues");
			}
			if (namedFields.Length != fieldValues.Length)
			{
				throw new ArgumentException("Array lengths must be the same.", "namedFields, fieldValues");
			}
			if ((con.Attributes & MethodAttributes.Static) == MethodAttributes.Static || (con.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Private)
			{
				throw new ArgumentException("Cannot have private or static constructor.");
			}
			Type declaringType = this.ctor.DeclaringType;
			int num = 0;
			foreach (FieldInfo fieldInfo in namedFields)
			{
				Type declaringType2 = fieldInfo.DeclaringType;
				if (declaringType != declaringType2 && !declaringType2.IsSubclassOf(declaringType) && !declaringType.IsSubclassOf(declaringType2))
				{
					throw new ArgumentException("Field '" + fieldInfo.Name + "' does not belong to the same class as the constructor");
				}
				if (!this.IsValidType(fieldInfo.FieldType))
				{
					throw new ArgumentException("Field '" + fieldInfo.Name + "' does not have a valid type.");
				}
				if (!CustomAttributeBuilder.IsValidValue(fieldInfo.FieldType, fieldValues[num]))
				{
					throw new ArgumentException("Field " + fieldInfo.Name + " is not a valid value.");
				}
				if (fieldValues[num] != null && !(fieldInfo.FieldType is TypeBuilder) && !fieldInfo.FieldType.IsEnum && !fieldInfo.FieldType.IsInstanceOfType(fieldValues[num]) && !fieldInfo.FieldType.IsArray)
				{
					string str = "Value of field '";
					string name = fieldInfo.Name;
					string str2 = "' does not match field type: ";
					Type fieldType = fieldInfo.FieldType;
					throw new ArgumentException(str + name + str2 + ((fieldType != null) ? fieldType.ToString() : null));
				}
				num++;
			}
			num = 0;
			foreach (PropertyInfo propertyInfo in namedProperties)
			{
				if (!propertyInfo.CanWrite)
				{
					throw new ArgumentException("Property '" + propertyInfo.Name + "' does not have a setter.");
				}
				Type declaringType3 = propertyInfo.DeclaringType;
				if (declaringType != declaringType3 && !declaringType3.IsSubclassOf(declaringType) && !declaringType.IsSubclassOf(declaringType3))
				{
					throw new ArgumentException("Property '" + propertyInfo.Name + "' does not belong to the same class as the constructor");
				}
				if (!this.IsValidType(propertyInfo.PropertyType))
				{
					throw new ArgumentException("Property '" + propertyInfo.Name + "' does not have a valid type.");
				}
				if (!CustomAttributeBuilder.IsValidValue(propertyInfo.PropertyType, propertyValues[num]))
				{
					throw new ArgumentException("Property " + propertyInfo.Name + " is not a valid value.");
				}
				if (propertyValues[num] != null && !(propertyInfo.PropertyType is TypeBuilder) && !propertyInfo.PropertyType.IsEnum && !propertyInfo.PropertyType.IsInstanceOfType(propertyValues[num]) && !propertyInfo.PropertyType.IsArray)
				{
					string[] array = new string[6];
					array[0] = "Value of property '";
					array[1] = propertyInfo.Name;
					array[2] = "' does not match property type: ";
					int num2 = 3;
					Type propertyType = propertyInfo.PropertyType;
					array[num2] = ((propertyType != null) ? propertyType.ToString() : null);
					array[4] = " -> ";
					int num3 = 5;
					object obj = propertyValues[num];
					array[num3] = ((obj != null) ? obj.ToString() : null);
					throw new ArgumentException(string.Concat(array));
				}
				num++;
			}
			num = 0;
			foreach (ParameterInfo parameterInfo in CustomAttributeBuilder.GetParameters(con))
			{
				if (parameterInfo != null)
				{
					Type parameterType = parameterInfo.ParameterType;
					if (!this.IsValidType(parameterType))
					{
						throw new ArgumentException("Parameter " + num.ToString() + " does not have a valid type.");
					}
					if (!CustomAttributeBuilder.IsValidValue(parameterType, constructorArgs[num]))
					{
						throw new ArgumentException("Parameter " + num.ToString() + " is not a valid value.");
					}
					if (constructorArgs[num] != null)
					{
						if (!(parameterType is TypeBuilder) && !parameterType.IsEnum && !parameterType.IsInstanceOfType(constructorArgs[num]) && !parameterType.IsArray)
						{
							string[] array2 = new string[6];
							array2[0] = "Value of argument ";
							array2[1] = num.ToString();
							array2[2] = " does not match parameter type: ";
							int num4 = 3;
							Type type = parameterType;
							array2[num4] = ((type != null) ? type.ToString() : null);
							array2[4] = " -> ";
							int num5 = 5;
							object obj2 = constructorArgs[num];
							array2[num5] = ((obj2 != null) ? obj2.ToString() : null);
							throw new ArgumentException(string.Concat(array2));
						}
						if (!this.IsValidParam(constructorArgs[num], parameterType))
						{
							string str3 = "Cannot emit a CustomAttribute with argument of type ";
							Type type2 = constructorArgs[num].GetType();
							throw new ArgumentException(str3 + ((type2 != null) ? type2.ToString() : null) + ".");
						}
					}
				}
				num++;
			}
			this.data = CustomAttributeBuilder.GetBlob(declaringType.Assembly, con, constructorArgs, namedProperties, propertyValues, namedFields, fieldValues);
		}

		// Token: 0x06004F47 RID: 20295 RVA: 0x000F9034 File Offset: 0x000F7234
		internal static int decode_len(byte[] data, int pos, out int rpos)
		{
			int result;
			if ((data[pos] & 128) == 0)
			{
				result = (int)(data[pos++] & 127);
			}
			else if ((data[pos] & 64) == 0)
			{
				result = ((int)(data[pos] & 63) << 8) + (int)data[pos + 1];
				pos += 2;
			}
			else
			{
				result = ((int)(data[pos] & 31) << 24) + ((int)data[pos + 1] << 16) + ((int)data[pos + 2] << 8) + (int)data[pos + 3];
				pos += 4;
			}
			rpos = pos;
			return result;
		}

		// Token: 0x06004F48 RID: 20296 RVA: 0x000F90A4 File Offset: 0x000F72A4
		internal static string string_from_bytes(byte[] data, int pos, int len)
		{
			return Encoding.UTF8.GetString(data, pos, len);
		}

		// Token: 0x06004F49 RID: 20297 RVA: 0x000F90B4 File Offset: 0x000F72B4
		internal static string decode_string(byte[] data, int pos, out int rpos)
		{
			if (data[pos] == 255)
			{
				rpos = pos + 1;
				return null;
			}
			int num = CustomAttributeBuilder.decode_len(data, pos, out pos);
			string result = CustomAttributeBuilder.string_from_bytes(data, pos, num);
			pos += num;
			rpos = pos;
			return result;
		}

		// Token: 0x06004F4A RID: 20298 RVA: 0x000F90EC File Offset: 0x000F72EC
		internal string string_arg()
		{
			int pos = 2;
			return CustomAttributeBuilder.decode_string(this.data, pos, out pos);
		}

		// Token: 0x06004F4B RID: 20299 RVA: 0x000F910C File Offset: 0x000F730C
		internal static UnmanagedMarshal get_umarshal(CustomAttributeBuilder customBuilder, bool is_field)
		{
			byte[] array = customBuilder.Data;
			UnmanagedType elemType = (UnmanagedType)80;
			int num = -1;
			int sizeParamIndex = -1;
			bool flag = false;
			string text = null;
			Type typeref = null;
			string cookie = string.Empty;
			int num2 = (int)array[2];
			num2 |= (int)array[3] << 8;
			string fullName = CustomAttributeBuilder.GetParameters(customBuilder.Ctor)[0].ParameterType.FullName;
			int num3 = 6;
			if (fullName == "System.Int16")
			{
				num3 = 4;
			}
			int num4 = (int)array[num3++];
			num4 |= (int)array[num3++] << 8;
			int i = 0;
			while (i < num4)
			{
				byte b = array[num3++];
				if (array[num3++] == 85)
				{
					CustomAttributeBuilder.decode_string(array, num3, out num3);
				}
				string text2 = CustomAttributeBuilder.decode_string(array, num3, out num3);
				uint num5 = <PrivateImplementationDetails>.ComputeStringHash(text2);
				if (num5 <= 2523910760U)
				{
					if (num5 <= 1554623949U)
					{
						if (num5 != 67206855U)
						{
							if (num5 != 1554623949U)
							{
								goto IL_30E;
							}
							if (!(text2 == "SafeArraySubType"))
							{
								goto IL_30E;
							}
							elemType = (UnmanagedType)((int)array[num3++] | (int)array[num3++] << 8 | (int)array[num3++] << 16 | (int)array[num3++] << 24);
						}
						else
						{
							if (!(text2 == "MarshalCookie"))
							{
								goto IL_30E;
							}
							cookie = CustomAttributeBuilder.decode_string(array, num3, out num3);
						}
					}
					else if (num5 != 1823397059U)
					{
						if (num5 != 2523910760U)
						{
							goto IL_30E;
						}
						if (!(text2 == "IidParameterIndex"))
						{
							goto IL_30E;
						}
						num3 += 4;
					}
					else
					{
						if (!(text2 == "SizeParamIndex"))
						{
							goto IL_30E;
						}
						sizeParamIndex = ((int)array[num3++] | (int)array[num3++] << 8);
						flag = true;
					}
				}
				else if (num5 <= 2658176172U)
				{
					if (num5 != 2546868066U)
					{
						if (num5 != 2658176172U)
						{
							goto IL_30E;
						}
						if (!(text2 == "ArraySubType"))
						{
							goto IL_30E;
						}
						elemType = (UnmanagedType)((int)array[num3++] | (int)array[num3++] << 8 | (int)array[num3++] << 16 | (int)array[num3++] << 24);
					}
					else
					{
						if (!(text2 == "MarshalTypeRef"))
						{
							goto IL_30E;
						}
						text = CustomAttributeBuilder.decode_string(array, num3, out num3);
						if (text != null)
						{
							typeref = Type.GetType(text);
						}
					}
				}
				else if (num5 != 2784686469U)
				{
					if (num5 != 3888525279U)
					{
						if (num5 != 4141739223U)
						{
							goto IL_30E;
						}
						if (!(text2 == "SafeArrayUserDefinedSubType"))
						{
							goto IL_30E;
						}
						CustomAttributeBuilder.decode_string(array, num3, out num3);
					}
					else
					{
						if (!(text2 == "SizeConst"))
						{
							goto IL_30E;
						}
						num = ((int)array[num3++] | (int)array[num3++] << 8 | (int)array[num3++] << 16 | (int)array[num3++] << 24);
						flag = true;
					}
				}
				else
				{
					if (!(text2 == "MarshalType"))
					{
						goto IL_30E;
					}
					text = CustomAttributeBuilder.decode_string(array, num3, out num3);
				}
				i++;
				continue;
				IL_30E:
				throw new Exception("Unknown MarshalAsAttribute field: " + text2);
			}
			UnmanagedType unmanagedType = (UnmanagedType)num2;
			if (unmanagedType <= UnmanagedType.SafeArray)
			{
				if (unmanagedType == UnmanagedType.ByValTStr)
				{
					return UnmanagedMarshal.DefineByValTStr(num);
				}
				if (unmanagedType == UnmanagedType.SafeArray)
				{
					return UnmanagedMarshal.DefineSafeArray(elemType);
				}
			}
			else if (unmanagedType != UnmanagedType.ByValArray)
			{
				if (unmanagedType != UnmanagedType.LPArray)
				{
					if (unmanagedType == UnmanagedType.CustomMarshaler)
					{
						return UnmanagedMarshal.DefineCustom(typeref, cookie, text, Guid.Empty);
					}
				}
				else
				{
					if (flag)
					{
						return UnmanagedMarshal.DefineLPArrayInternal(elemType, num, sizeParamIndex);
					}
					return UnmanagedMarshal.DefineLPArray(elemType);
				}
			}
			else
			{
				if (!is_field)
				{
					throw new ArgumentException("Specified unmanaged type is only valid on fields");
				}
				return UnmanagedMarshal.DefineByValArray(num);
			}
			return UnmanagedMarshal.DefineUnmanagedMarshal((UnmanagedType)num2);
		}

		// Token: 0x06004F4C RID: 20300 RVA: 0x000F94C4 File Offset: 0x000F76C4
		private static Type elementTypeToType(int elementType)
		{
			switch (elementType)
			{
			case 2:
				return typeof(bool);
			case 3:
				return typeof(char);
			case 4:
				return typeof(sbyte);
			case 5:
				return typeof(byte);
			case 6:
				return typeof(short);
			case 7:
				return typeof(ushort);
			case 8:
				return typeof(int);
			case 9:
				return typeof(uint);
			case 10:
				return typeof(long);
			case 11:
				return typeof(ulong);
			case 12:
				return typeof(float);
			case 13:
				return typeof(double);
			case 14:
				return typeof(string);
			default:
				throw new Exception("Unknown element type '" + elementType.ToString() + "'");
			}
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x000F95BC File Offset: 0x000F77BC
		private static object decode_cattr_value(Type t, byte[] data, int pos, out int rpos)
		{
			TypeCode typeCode = Type.GetTypeCode(t);
			if (typeCode <= TypeCode.Boolean)
			{
				if (typeCode != TypeCode.Object)
				{
					if (typeCode == TypeCode.Boolean)
					{
						rpos = pos + 1;
						return data[pos] != 0;
					}
				}
				else
				{
					int num = (int)data[pos];
					pos++;
					if (num >= 2 && num <= 14)
					{
						return CustomAttributeBuilder.decode_cattr_value(CustomAttributeBuilder.elementTypeToType(num), data, pos, out rpos);
					}
					throw new Exception("Subtype '" + num.ToString() + "' of type object not yet handled in decode_cattr_value");
				}
			}
			else
			{
				if (typeCode == TypeCode.Int32)
				{
					rpos = pos + 4;
					return (int)data[pos] + ((int)data[pos + 1] << 8) + ((int)data[pos + 2] << 16) + ((int)data[pos + 3] << 24);
				}
				if (typeCode == TypeCode.String)
				{
					if (data[pos] == 255)
					{
						rpos = pos + 1;
						return null;
					}
					int num2 = CustomAttributeBuilder.decode_len(data, pos, out pos);
					rpos = pos + num2;
					return CustomAttributeBuilder.string_from_bytes(data, pos, num2);
				}
			}
			throw new Exception("FIXME: Type " + ((t != null) ? t.ToString() : null) + " not yet handled in decode_cattr_value.");
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x000F96B4 File Offset: 0x000F78B4
		internal static CustomAttributeBuilder.CustomAttributeInfo decode_cattr(CustomAttributeBuilder customBuilder)
		{
			byte[] array = customBuilder.Data;
			ConstructorInfo constructorInfo = customBuilder.Ctor;
			int num = 0;
			CustomAttributeBuilder.CustomAttributeInfo customAttributeInfo = default(CustomAttributeBuilder.CustomAttributeInfo);
			if (array.Length < 2)
			{
				throw new Exception("Custom attr length is only '" + array.Length.ToString() + "'");
			}
			if (array[0] != 1 || array[1] != 0)
			{
				throw new Exception("Prolog invalid");
			}
			num = 2;
			ParameterInfo[] parameters = CustomAttributeBuilder.GetParameters(constructorInfo);
			customAttributeInfo.ctor = constructorInfo;
			customAttributeInfo.ctorArgs = new object[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				customAttributeInfo.ctorArgs[i] = CustomAttributeBuilder.decode_cattr_value(parameters[i].ParameterType, array, num, out num);
			}
			int num2 = (int)array[num] + (int)array[num + 1] * 256;
			num += 2;
			customAttributeInfo.namedParamNames = new string[num2];
			customAttributeInfo.namedParamValues = new object[num2];
			for (int j = 0; j < num2; j++)
			{
				int num3 = (int)array[num++];
				int num4 = (int)array[num++];
				string text = null;
				if (num4 == 85)
				{
					int num5 = CustomAttributeBuilder.decode_len(array, num, out num);
					text = CustomAttributeBuilder.string_from_bytes(array, num, num5);
					num += num5;
				}
				int num6 = CustomAttributeBuilder.decode_len(array, num, out num);
				string text2 = CustomAttributeBuilder.string_from_bytes(array, num, num6);
				customAttributeInfo.namedParamNames[j] = text2;
				num += num6;
				if (num3 != 83)
				{
					throw new Exception("Unknown named type: " + num3.ToString());
				}
				FieldInfo field = constructorInfo.DeclaringType.GetField(text2, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (field == null)
				{
					string[] array2 = new string[5];
					array2[0] = "Custom attribute type '";
					int num7 = 1;
					Type declaringType = constructorInfo.DeclaringType;
					array2[num7] = ((declaringType != null) ? declaringType.ToString() : null);
					array2[2] = "' doesn't contain a field named '";
					array2[3] = text2;
					array2[4] = "'";
					throw new Exception(string.Concat(array2));
				}
				object obj = CustomAttributeBuilder.decode_cattr_value(field.FieldType, array, num, out num);
				if (text != null)
				{
					obj = Enum.ToObject(Type.GetType(text), obj);
				}
				customAttributeInfo.namedParamValues[j] = obj;
			}
			return customAttributeInfo;
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x000F98BC File Offset: 0x000F7ABC
		private static ParameterInfo[] GetParameters(ConstructorInfo ctor)
		{
			ConstructorBuilder constructorBuilder = ctor as ConstructorBuilder;
			if (constructorBuilder != null)
			{
				return constructorBuilder.GetParametersInternal();
			}
			return ctor.GetParametersInternal();
		}

		// Token: 0x04003129 RID: 12585
		private ConstructorInfo ctor;

		// Token: 0x0400312A RID: 12586
		private byte[] data;

		// Token: 0x0400312B RID: 12587
		private object[] args;

		// Token: 0x0400312C RID: 12588
		private PropertyInfo[] namedProperties;

		// Token: 0x0400312D RID: 12589
		private object[] propertyValues;

		// Token: 0x0400312E RID: 12590
		private FieldInfo[] namedFields;

		// Token: 0x0400312F RID: 12591
		private object[] fieldValues;

		// Token: 0x02000918 RID: 2328
		internal struct CustomAttributeInfo
		{
			// Token: 0x04003130 RID: 12592
			public ConstructorInfo ctor;

			// Token: 0x04003131 RID: 12593
			public object[] ctorArgs;

			// Token: 0x04003132 RID: 12594
			public string[] namedParamNames;

			// Token: 0x04003133 RID: 12595
			public object[] namedParamValues;
		}
	}
}
