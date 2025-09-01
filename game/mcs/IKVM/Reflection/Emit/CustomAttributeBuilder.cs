using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000D7 RID: 215
	public sealed class CustomAttributeBuilder
	{
		// Token: 0x060009ED RID: 2541 RVA: 0x00022B83 File Offset: 0x00020D83
		internal CustomAttributeBuilder(ConstructorInfo con, byte[] blob)
		{
			this.con = con;
			this.blob = blob;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00022B99 File Offset: 0x00020D99
		private CustomAttributeBuilder(ConstructorInfo con, int securityAction, byte[] blob)
		{
			this.con = con;
			this.blob = blob;
			this.constructorArgs = new object[]
			{
				securityAction
			};
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00022BC4 File Offset: 0x00020DC4
		public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs) : this(con, constructorArgs, null, null, null, null)
		{
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00022BD2 File Offset: 0x00020DD2
		public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, FieldInfo[] namedFields, object[] fieldValues) : this(con, constructorArgs, null, null, namedFields, fieldValues)
		{
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00022BE1 File Offset: 0x00020DE1
		public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues) : this(con, constructorArgs, namedProperties, propertyValues, null, null)
		{
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00022BF0 File Offset: 0x00020DF0
		public CustomAttributeBuilder(ConstructorInfo con, object[] constructorArgs, PropertyInfo[] namedProperties, object[] propertyValues, FieldInfo[] namedFields, object[] fieldValues)
		{
			this.con = con;
			this.constructorArgs = constructorArgs;
			this.namedProperties = namedProperties;
			this.propertyValues = propertyValues;
			this.namedFields = namedFields;
			this.fieldValues = fieldValues;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00022C25 File Offset: 0x00020E25
		public static CustomAttributeBuilder __FromBlob(ConstructorInfo con, byte[] blob)
		{
			return new CustomAttributeBuilder(con, blob);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00022C2E File Offset: 0x00020E2E
		public static CustomAttributeBuilder __FromBlob(ConstructorInfo con, int securityAction, byte[] blob)
		{
			return new CustomAttributeBuilder(con, securityAction, blob);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00022C38 File Offset: 0x00020E38
		public static CustomAttributeTypedArgument __MakeTypedArgument(Type type, object value)
		{
			return new CustomAttributeTypedArgument(type, value);
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x00022C41 File Offset: 0x00020E41
		internal ConstructorInfo Constructor
		{
			get
			{
				return this.con;
			}
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00022C4C File Offset: 0x00020E4C
		internal int WriteBlob(ModuleBuilder moduleBuilder)
		{
			ByteBuffer bb;
			if (this.blob != null)
			{
				bb = ByteBuffer.Wrap(this.blob);
			}
			else
			{
				bb = new ByteBuffer(100);
				new CustomAttributeBuilder.BlobWriter(moduleBuilder.Assembly, this, bb).WriteCustomAttributeBlob();
			}
			return moduleBuilder.Blobs.Add(bb);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00022C95 File Offset: 0x00020E95
		internal object GetConstructorArgument(int pos)
		{
			return this.constructorArgs[pos];
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x00022C9F File Offset: 0x00020E9F
		internal int ConstructorArgumentCount
		{
			get
			{
				if (this.constructorArgs != null)
				{
					return this.constructorArgs.Length;
				}
				return 0;
			}
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00022CB4 File Offset: 0x00020EB4
		internal T? GetFieldValue<T>(string name) where T : struct
		{
			object fieldValue = this.GetFieldValue(name);
			if (fieldValue is T)
			{
				return new T?((T)((object)fieldValue));
			}
			if (fieldValue == null)
			{
				return null;
			}
			if (typeof(T).IsEnum)
			{
				return new T?((T)((object)Enum.ToObject(typeof(T), fieldValue)));
			}
			return new T?((T)((object)Convert.ChangeType(fieldValue, typeof(T))));
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00022D30 File Offset: 0x00020F30
		internal object GetFieldValue(string name)
		{
			if (this.namedFields != null)
			{
				for (int i = 0; i < this.namedFields.Length; i++)
				{
					if (this.namedFields[i].Name == name)
					{
						return this.fieldValues[i];
					}
				}
			}
			return null;
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x00022D78 File Offset: 0x00020F78
		internal bool IsLegacyDeclSecurity
		{
			get
			{
				return this.con == CustomAttributeBuilder.LegacyPermissionSet || (this.con.DeclaringType == this.con.Module.universe.System_Security_Permissions_PermissionSetAttribute && this.blob == null && (this.namedFields == null || this.namedFields.Length == 0) && this.namedProperties != null && this.namedProperties.Length == 1 && this.namedProperties[0].Name == "XML" && this.propertyValues[0] is string);
			}
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00022E14 File Offset: 0x00021014
		internal int WriteLegacyDeclSecurityBlob(ModuleBuilder moduleBuilder)
		{
			if (this.blob != null)
			{
				return moduleBuilder.Blobs.Add(ByteBuffer.Wrap(this.blob));
			}
			return moduleBuilder.Blobs.Add(ByteBuffer.Wrap(Encoding.Unicode.GetBytes((string)this.propertyValues[0])));
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00022E67 File Offset: 0x00021067
		internal void WriteNamedArgumentsForDeclSecurity(ModuleBuilder moduleBuilder, ByteBuffer bb)
		{
			if (this.blob != null)
			{
				bb.Write(this.blob);
				return;
			}
			new CustomAttributeBuilder.BlobWriter(moduleBuilder.Assembly, this, bb).WriteNamedArguments(true);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00022E94 File Offset: 0x00021094
		internal CustomAttributeData ToData(Assembly asm)
		{
			if (this.blob == null)
			{
				List<CustomAttributeNamedArgument> list = new List<CustomAttributeNamedArgument>();
				if (this.namedProperties != null)
				{
					for (int i = 0; i < this.namedProperties.Length; i++)
					{
						list.Add(new CustomAttributeNamedArgument(this.namedProperties[i], CustomAttributeBuilder.RewrapValue(this.namedProperties[i].PropertyType, this.propertyValues[i])));
					}
				}
				if (this.namedFields != null)
				{
					for (int j = 0; j < this.namedFields.Length; j++)
					{
						list.Add(new CustomAttributeNamedArgument(this.namedFields[j], CustomAttributeBuilder.RewrapValue(this.namedFields[j].FieldType, this.fieldValues[j])));
					}
				}
				List<CustomAttributeTypedArgument> list2 = new List<CustomAttributeTypedArgument>(this.constructorArgs.Length);
				ParameterInfo[] parameters = this.Constructor.GetParameters();
				for (int k = 0; k < this.constructorArgs.Length; k++)
				{
					list2.Add(CustomAttributeBuilder.RewrapValue(parameters[k].ParameterType, this.constructorArgs[k]));
				}
				return new CustomAttributeData(asm.ManifestModule, this.con, list2, list);
			}
			if (this.constructorArgs != null)
			{
				return new CustomAttributeData(asm, this.con, (int)this.constructorArgs[0], this.blob, -1);
			}
			return new CustomAttributeData(asm, this.con, new ByteReader(this.blob, 0, this.blob.Length));
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00022FF4 File Offset: 0x000211F4
		private static CustomAttributeTypedArgument RewrapValue(Type type, object value)
		{
			if (value is Array)
			{
				Array array = (Array)value;
				return CustomAttributeBuilder.RewrapArray(type.Module.universe.Import(array.GetType()), array);
			}
			if (!(value is CustomAttributeTypedArgument))
			{
				return new CustomAttributeTypedArgument(type, value);
			}
			CustomAttributeTypedArgument result = (CustomAttributeTypedArgument)value;
			if (result.Value is Array)
			{
				return CustomAttributeBuilder.RewrapArray(result.ArgumentType, (Array)result.Value);
			}
			return result;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002306C File Offset: 0x0002126C
		private static CustomAttributeTypedArgument RewrapArray(Type arrayType, Array array)
		{
			Type elementType = arrayType.GetElementType();
			CustomAttributeTypedArgument[] array2 = new CustomAttributeTypedArgument[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = CustomAttributeBuilder.RewrapValue(elementType, array.GetValue(i));
			}
			return new CustomAttributeTypedArgument(arrayType, array2);
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x000230B5 File Offset: 0x000212B5
		internal bool HasBlob
		{
			get
			{
				return this.blob != null;
			}
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x000230C0 File Offset: 0x000212C0
		internal CustomAttributeBuilder DecodeBlob(Assembly asm)
		{
			if (this.blob == null)
			{
				return this;
			}
			return this.ToData(asm).__ToBuilder();
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x000230D8 File Offset: 0x000212D8
		internal byte[] GetBlob(Assembly asm)
		{
			ByteBuffer byteBuffer = new ByteBuffer(100);
			new CustomAttributeBuilder.BlobWriter(asm, this, byteBuffer).WriteCustomAttributeBlob();
			return byteBuffer.ToArray();
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00023100 File Offset: 0x00021300
		internal KnownCA KnownCA
		{
			get
			{
				TypeName typeName = this.con.DeclaringType.TypeName;
				string text = typeName.Namespace;
				if (!(text == "System"))
				{
					if (!(text == "System.Runtime.CompilerServices"))
					{
						if (text == "System.Runtime.InteropServices")
						{
							text = typeName.Name;
							uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
							if (num <= 2546496714U)
							{
								if (num <= 2042804821U)
								{
									if (num != 545503314U)
									{
										if (num == 2042804821U)
										{
											if (text == "OutAttribute")
											{
												return KnownCA.OutAttribute;
											}
										}
									}
									else if (text == "PreserveSigAttribute")
									{
										return KnownCA.PreserveSigAttribute;
									}
								}
								else if (num != 2199891167U)
								{
									if (num == 2546496714U)
									{
										if (text == "InAttribute")
										{
											return KnownCA.InAttribute;
										}
									}
								}
								else if (text == "MarshalAsAttribute")
								{
									return KnownCA.MarshalAsAttribute;
								}
							}
							else if (num <= 3696509043U)
							{
								if (num != 3304475762U)
								{
									if (num == 3696509043U)
									{
										if (text == "OptionalAttribute")
										{
											return KnownCA.OptionalAttribute;
										}
									}
								}
								else if (text == "StructLayoutAttribute")
								{
									return KnownCA.StructLayoutAttribute;
								}
							}
							else if (num != 3744513404U)
							{
								if (num != 3951491190U)
								{
									if (num == 3954382425U)
									{
										if (text == "ComImportAttribute")
										{
											return KnownCA.ComImportAttribute;
										}
									}
								}
								else if (text == "DllImportAttribute")
								{
									return KnownCA.DllImportAttribute;
								}
							}
							else if (text == "FieldOffsetAttribute")
							{
								return KnownCA.FieldOffsetAttribute;
							}
						}
					}
					else
					{
						text = typeName.Name;
						if (text == "MethodImplAttribute")
						{
							return KnownCA.MethodImplAttribute;
						}
						if (text == "SpecialNameAttribute")
						{
							return KnownCA.SpecialNameAttribute;
						}
					}
				}
				else
				{
					text = typeName.Name;
					if (text == "SerializableAttribute")
					{
						return KnownCA.SerializableAttribute;
					}
					if (text == "NonSerializedAttribute")
					{
						return KnownCA.NonSerializedAttribute;
					}
				}
				if (typeName.Matches("System.Security.SuppressUnmanagedCodeSecurityAttribute"))
				{
					return KnownCA.SuppressUnmanagedCodeSecurityAttribute;
				}
				return KnownCA.Unknown;
			}
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x000232F0 File Offset: 0x000214F0
		// Note: this type is marked as 'beforefieldinit'.
		static CustomAttributeBuilder()
		{
		}

		// Token: 0x04000416 RID: 1046
		internal static readonly ConstructorInfo LegacyPermissionSet = new ConstructorBuilder(null);

		// Token: 0x04000417 RID: 1047
		private readonly ConstructorInfo con;

		// Token: 0x04000418 RID: 1048
		private readonly byte[] blob;

		// Token: 0x04000419 RID: 1049
		private readonly object[] constructorArgs;

		// Token: 0x0400041A RID: 1050
		private readonly PropertyInfo[] namedProperties;

		// Token: 0x0400041B RID: 1051
		private readonly object[] propertyValues;

		// Token: 0x0400041C RID: 1052
		private readonly FieldInfo[] namedFields;

		// Token: 0x0400041D RID: 1053
		private readonly object[] fieldValues;

		// Token: 0x02000365 RID: 869
		private sealed class BlobWriter
		{
			// Token: 0x0600263D RID: 9789 RVA: 0x000B56E7 File Offset: 0x000B38E7
			internal BlobWriter(Assembly assembly, CustomAttributeBuilder cab, ByteBuffer bb)
			{
				this.assembly = assembly;
				this.cab = cab;
				this.bb = bb;
			}

			// Token: 0x0600263E RID: 9790 RVA: 0x000B5704 File Offset: 0x000B3904
			internal void WriteCustomAttributeBlob()
			{
				this.WriteUInt16(1);
				ParameterInfo[] parameters = this.cab.con.GetParameters();
				for (int i = 0; i < parameters.Length; i++)
				{
					this.WriteFixedArg(parameters[i].ParameterType, this.cab.constructorArgs[i]);
				}
				this.WriteNamedArguments(false);
			}

			// Token: 0x0600263F RID: 9791 RVA: 0x000B575C File Offset: 0x000B395C
			internal void WriteNamedArguments(bool forDeclSecurity)
			{
				int num = 0;
				if (this.cab.namedFields != null)
				{
					num += this.cab.namedFields.Length;
				}
				if (this.cab.namedProperties != null)
				{
					num += this.cab.namedProperties.Length;
				}
				if (forDeclSecurity)
				{
					this.WritePackedLen(num);
				}
				else
				{
					this.WriteUInt16((ushort)num);
				}
				if (this.cab.namedFields != null)
				{
					for (int i = 0; i < this.cab.namedFields.Length; i++)
					{
						this.WriteNamedArg(83, this.cab.namedFields[i].FieldType, this.cab.namedFields[i].Name, this.cab.fieldValues[i]);
					}
				}
				if (this.cab.namedProperties != null)
				{
					for (int j = 0; j < this.cab.namedProperties.Length; j++)
					{
						this.WriteNamedArg(84, this.cab.namedProperties[j].PropertyType, this.cab.namedProperties[j].Name, this.cab.propertyValues[j]);
					}
				}
			}

			// Token: 0x06002640 RID: 9792 RVA: 0x000B5875 File Offset: 0x000B3A75
			private void WriteNamedArg(byte fieldOrProperty, Type type, string name, object value)
			{
				this.WriteByte(fieldOrProperty);
				this.WriteFieldOrPropType(type);
				this.WriteString(name);
				this.WriteFixedArg(type, value);
			}

			// Token: 0x06002641 RID: 9793 RVA: 0x000B5895 File Offset: 0x000B3A95
			private void WriteByte(byte value)
			{
				this.bb.Write(value);
			}

			// Token: 0x06002642 RID: 9794 RVA: 0x000B58A3 File Offset: 0x000B3AA3
			private void WriteUInt16(ushort value)
			{
				this.bb.Write(value);
			}

			// Token: 0x06002643 RID: 9795 RVA: 0x000B58B1 File Offset: 0x000B3AB1
			private void WriteInt32(int value)
			{
				this.bb.Write(value);
			}

			// Token: 0x06002644 RID: 9796 RVA: 0x000B58C0 File Offset: 0x000B3AC0
			private void WriteFixedArg(Type type, object value)
			{
				Universe universe = this.assembly.universe;
				if (type == universe.System_String)
				{
					this.WriteString((string)value);
					return;
				}
				if (type == universe.System_Boolean)
				{
					this.WriteByte(((bool)value) ? 1 : 0);
					return;
				}
				if (type == universe.System_Char)
				{
					this.WriteUInt16((ushort)((char)value));
					return;
				}
				if (type == universe.System_SByte)
				{
					this.WriteByte((byte)((sbyte)value));
					return;
				}
				if (type == universe.System_Byte)
				{
					this.WriteByte((byte)value);
					return;
				}
				if (type == universe.System_Int16)
				{
					this.WriteUInt16((ushort)((short)value));
					return;
				}
				if (type == universe.System_UInt16)
				{
					this.WriteUInt16((ushort)value);
					return;
				}
				if (type == universe.System_Int32)
				{
					this.WriteInt32((int)value);
					return;
				}
				if (type == universe.System_UInt32)
				{
					this.WriteInt32((int)((uint)value));
					return;
				}
				if (type == universe.System_Int64)
				{
					this.WriteInt64((long)value);
					return;
				}
				if (type == universe.System_UInt64)
				{
					this.WriteInt64((long)((ulong)value));
					return;
				}
				if (type == universe.System_Single)
				{
					this.WriteSingle((float)value);
					return;
				}
				if (type == universe.System_Double)
				{
					this.WriteDouble((double)value);
					return;
				}
				if (type == universe.System_Type)
				{
					this.WriteTypeName((Type)value);
					return;
				}
				if (type == universe.System_Object)
				{
					if (value == null)
					{
						type = universe.System_String;
					}
					else if (value is Type)
					{
						type = universe.System_Type;
					}
					else if (value is CustomAttributeTypedArgument)
					{
						CustomAttributeTypedArgument customAttributeTypedArgument = (CustomAttributeTypedArgument)value;
						value = customAttributeTypedArgument.Value;
						type = customAttributeTypedArgument.ArgumentType;
					}
					else
					{
						type = universe.Import(value.GetType());
					}
					this.WriteFieldOrPropType(type);
					this.WriteFixedArg(type, value);
					return;
				}
				if (type.IsArray)
				{
					if (value == null)
					{
						this.WriteInt32(-1);
						return;
					}
					Array array = (Array)value;
					Type elementType = type.GetElementType();
					this.WriteInt32(array.Length);
					using (IEnumerator enumerator = array.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object value2 = enumerator.Current;
							this.WriteFixedArg(elementType, value2);
						}
						return;
					}
				}
				if (type.IsEnum)
				{
					this.WriteFixedArg(type.GetEnumUnderlyingTypeImpl(), value);
					return;
				}
				throw new ArgumentException();
			}

			// Token: 0x06002645 RID: 9797 RVA: 0x000B5B60 File Offset: 0x000B3D60
			private void WriteInt64(long value)
			{
				this.bb.Write(value);
			}

			// Token: 0x06002646 RID: 9798 RVA: 0x000B5B6E File Offset: 0x000B3D6E
			private void WriteSingle(float value)
			{
				this.bb.Write(value);
			}

			// Token: 0x06002647 RID: 9799 RVA: 0x000B5B7C File Offset: 0x000B3D7C
			private void WriteDouble(double value)
			{
				this.bb.Write(value);
			}

			// Token: 0x06002648 RID: 9800 RVA: 0x000B5B8C File Offset: 0x000B3D8C
			private void WriteTypeName(Type type)
			{
				string val = null;
				if (type != null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					this.GetTypeName(stringBuilder, type, false);
					val = stringBuilder.ToString();
				}
				this.WriteString(val);
			}

			// Token: 0x06002649 RID: 9801 RVA: 0x000B5BC4 File Offset: 0x000B3DC4
			private void GetTypeName(StringBuilder sb, Type type, bool isTypeParam)
			{
				bool flag = !this.assembly.ManifestModule.__IsMissing && this.assembly.ManifestModule.MDStreamVersion < 131072;
				bool flag2 = type.Assembly != this.assembly && (!flag || type.Assembly != type.Module.universe.Mscorlib);
				if (isTypeParam && flag2)
				{
					sb.Append('[');
				}
				this.GetTypeNameImpl(sb, type);
				if (flag2)
				{
					if (flag)
					{
						sb.Append(',');
					}
					else
					{
						sb.Append(", ");
					}
					if (isTypeParam)
					{
						sb.Append(type.Assembly.FullName.Replace("]", "\\]")).Append(']');
						return;
					}
					sb.Append(type.Assembly.FullName);
				}
			}

			// Token: 0x0600264A RID: 9802 RVA: 0x000B5CA4 File Offset: 0x000B3EA4
			private void GetTypeNameImpl(StringBuilder sb, Type type)
			{
				if (type.HasElementType)
				{
					this.GetTypeNameImpl(sb, type.GetElementType());
					sb.Append(((ElementHolderType)type).GetSuffix());
					return;
				}
				if (type.IsConstructedGenericType)
				{
					sb.Append(type.GetGenericTypeDefinition().FullName);
					sb.Append('[');
					string value = "";
					foreach (Type type2 in type.GetGenericArguments())
					{
						sb.Append(value);
						this.GetTypeName(sb, type2, true);
						value = ",";
					}
					sb.Append(']');
					return;
				}
				sb.Append(type.FullName);
			}

			// Token: 0x0600264B RID: 9803 RVA: 0x000B5D49 File Offset: 0x000B3F49
			private void WriteString(string val)
			{
				this.bb.Write(val);
			}

			// Token: 0x0600264C RID: 9804 RVA: 0x000B5D57 File Offset: 0x000B3F57
			private void WritePackedLen(int len)
			{
				this.bb.WriteCompressedUInt(len);
			}

			// Token: 0x0600264D RID: 9805 RVA: 0x000B5D68 File Offset: 0x000B3F68
			private void WriteFieldOrPropType(Type type)
			{
				Universe universe = type.Module.universe;
				if (type == universe.System_Type)
				{
					this.WriteByte(80);
					return;
				}
				if (type == universe.System_Object)
				{
					this.WriteByte(81);
					return;
				}
				if (type == universe.System_Boolean)
				{
					this.WriteByte(2);
					return;
				}
				if (type == universe.System_Char)
				{
					this.WriteByte(3);
					return;
				}
				if (type == universe.System_SByte)
				{
					this.WriteByte(4);
					return;
				}
				if (type == universe.System_Byte)
				{
					this.WriteByte(5);
					return;
				}
				if (type == universe.System_Int16)
				{
					this.WriteByte(6);
					return;
				}
				if (type == universe.System_UInt16)
				{
					this.WriteByte(7);
					return;
				}
				if (type == universe.System_Int32)
				{
					this.WriteByte(8);
					return;
				}
				if (type == universe.System_UInt32)
				{
					this.WriteByte(9);
					return;
				}
				if (type == universe.System_Int64)
				{
					this.WriteByte(10);
					return;
				}
				if (type == universe.System_UInt64)
				{
					this.WriteByte(11);
					return;
				}
				if (type == universe.System_Single)
				{
					this.WriteByte(12);
					return;
				}
				if (type == universe.System_Double)
				{
					this.WriteByte(13);
					return;
				}
				if (type == universe.System_String)
				{
					this.WriteByte(14);
					return;
				}
				if (type.IsArray)
				{
					this.WriteByte(29);
					this.WriteFieldOrPropType(type.GetElementType());
					return;
				}
				if (type.IsEnum)
				{
					this.WriteByte(85);
					this.WriteTypeName(type);
					return;
				}
				throw new ArgumentException();
			}

			// Token: 0x04000F09 RID: 3849
			private readonly Assembly assembly;

			// Token: 0x04000F0A RID: 3850
			private readonly CustomAttributeBuilder cab;

			// Token: 0x04000F0B RID: 3851
			private readonly ByteBuffer bb;
		}
	}
}
