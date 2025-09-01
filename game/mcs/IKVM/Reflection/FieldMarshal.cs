using System;
using System.Runtime.InteropServices;
using System.Text;
using IKVM.Reflection.Emit;
using IKVM.Reflection.Metadata;
using IKVM.Reflection.Reader;
using IKVM.Reflection.Writer;

namespace IKVM.Reflection
{
	// Token: 0x02000037 RID: 55
	public struct FieldMarshal
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x0000841C File Offset: 0x0000661C
		internal static bool ReadFieldMarshal(Module module, int token, out FieldMarshal fm)
		{
			fm = default(FieldMarshal);
			SortedTable<FieldMarshalTable.Record>.Enumerator enumerator = module.FieldMarshal.Filter(token).GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return false;
			}
			int num = enumerator.Current;
			ByteReader blob = module.GetBlob(module.FieldMarshal.records[num].NativeType);
			fm.UnmanagedType = (UnmanagedType)blob.ReadCompressedUInt();
			if (fm.UnmanagedType == UnmanagedType.LPArray)
			{
				fm.ArraySubType = new UnmanagedType?((UnmanagedType)blob.ReadCompressedUInt());
				if (fm.ArraySubType == (UnmanagedType)80)
				{
					fm.ArraySubType = null;
				}
				if (blob.Length != 0)
				{
					fm.SizeParamIndex = new short?((short)blob.ReadCompressedUInt());
					if (blob.Length != 0)
					{
						fm.SizeConst = new int?(blob.ReadCompressedUInt());
						if (blob.Length != 0 && blob.ReadCompressedUInt() == 0)
						{
							fm.SizeParamIndex = null;
						}
					}
				}
			}
			else if (fm.UnmanagedType == UnmanagedType.SafeArray)
			{
				if (blob.Length != 0)
				{
					fm.SafeArraySubType = new VarEnum?((VarEnum)blob.ReadCompressedUInt());
					if (blob.Length != 0)
					{
						fm.SafeArrayUserDefinedSubType = FieldMarshal.ReadType(module, blob);
					}
				}
			}
			else if (fm.UnmanagedType == UnmanagedType.ByValArray)
			{
				fm.SizeConst = new int?(blob.ReadCompressedUInt());
				if (blob.Length != 0)
				{
					fm.ArraySubType = new UnmanagedType?((UnmanagedType)blob.ReadCompressedUInt());
				}
			}
			else if (fm.UnmanagedType == UnmanagedType.ByValTStr)
			{
				fm.SizeConst = new int?(blob.ReadCompressedUInt());
			}
			else if (fm.UnmanagedType == UnmanagedType.Interface || fm.UnmanagedType == UnmanagedType.IDispatch || fm.UnmanagedType == UnmanagedType.IUnknown)
			{
				if (blob.Length != 0)
				{
					fm.IidParameterIndex = new int?(blob.ReadCompressedUInt());
				}
			}
			else if (fm.UnmanagedType == UnmanagedType.CustomMarshaler)
			{
				blob.ReadCompressedUInt();
				blob.ReadCompressedUInt();
				fm.MarshalType = FieldMarshal.ReadString(blob);
				fm.MarshalCookie = FieldMarshal.ReadString(blob);
				TypeNameParser typeNameParser = TypeNameParser.Parse(fm.MarshalType, false);
				if (!typeNameParser.Error)
				{
					fm.MarshalTypeRef = typeNameParser.GetType(module.universe, module, false, fm.MarshalType, false, false);
				}
			}
			return true;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00008674 File Offset: 0x00006874
		internal static void SetMarshalAsAttribute(ModuleBuilder module, int token, CustomAttributeBuilder attribute)
		{
			attribute = attribute.DecodeBlob(module.Assembly);
			FieldMarshalTable.Record newRecord = default(FieldMarshalTable.Record);
			newRecord.Parent = token;
			newRecord.NativeType = FieldMarshal.WriteMarshallingDescriptor(module, attribute);
			module.FieldMarshal.AddRecord(newRecord);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000086BC File Offset: 0x000068BC
		private static int WriteMarshallingDescriptor(ModuleBuilder module, CustomAttributeBuilder attribute)
		{
			object constructorArgument = attribute.GetConstructorArgument(0);
			UnmanagedType unmanagedType;
			if (constructorArgument is short)
			{
				unmanagedType = (UnmanagedType)((short)constructorArgument);
			}
			else if (constructorArgument is int)
			{
				unmanagedType = (UnmanagedType)((int)constructorArgument);
			}
			else
			{
				unmanagedType = (UnmanagedType)constructorArgument;
			}
			ByteBuffer byteBuffer = new ByteBuffer(5);
			byteBuffer.WriteCompressedUInt((int)unmanagedType);
			if (unmanagedType == UnmanagedType.LPArray)
			{
				UnmanagedType value = attribute.GetFieldValue<UnmanagedType>("ArraySubType") ?? ((UnmanagedType)80);
				byteBuffer.WriteCompressedUInt((int)value);
				short? fieldValue = attribute.GetFieldValue<short>("SizeParamIndex");
				int? num = (fieldValue != null) ? new int?((int)fieldValue.GetValueOrDefault()) : null;
				int? fieldValue2 = attribute.GetFieldValue<int>("SizeConst");
				if (num != null)
				{
					byteBuffer.WriteCompressedUInt(num.Value);
					if (fieldValue2 != null)
					{
						byteBuffer.WriteCompressedUInt(fieldValue2.Value);
						byteBuffer.WriteCompressedUInt(1);
					}
				}
				else if (fieldValue2 != null)
				{
					byteBuffer.WriteCompressedUInt(0);
					byteBuffer.WriteCompressedUInt(fieldValue2.Value);
					byteBuffer.WriteCompressedUInt(0);
				}
			}
			else if (unmanagedType == UnmanagedType.SafeArray)
			{
				VarEnum? fieldValue3 = attribute.GetFieldValue<VarEnum>("SafeArraySubType");
				if (fieldValue3 != null)
				{
					byteBuffer.WriteCompressedUInt((int)fieldValue3.Value);
					Type type = (Type)attribute.GetFieldValue("SafeArrayUserDefinedSubType");
					if (type != null)
					{
						FieldMarshal.WriteType(module, byteBuffer, type);
					}
				}
			}
			else if (unmanagedType == UnmanagedType.ByValArray)
			{
				byteBuffer.WriteCompressedUInt(attribute.GetFieldValue<int>("SizeConst") ?? 1);
				UnmanagedType? fieldValue4 = attribute.GetFieldValue<UnmanagedType>("ArraySubType");
				if (fieldValue4 != null)
				{
					byteBuffer.WriteCompressedUInt((int)fieldValue4.Value);
				}
			}
			else if (unmanagedType == UnmanagedType.ByValTStr)
			{
				byteBuffer.WriteCompressedUInt(attribute.GetFieldValue<int>("SizeConst").Value);
			}
			else if (unmanagedType == UnmanagedType.Interface || unmanagedType == UnmanagedType.IDispatch || unmanagedType == UnmanagedType.IUnknown)
			{
				int? fieldValue5 = attribute.GetFieldValue<int>("IidParameterIndex");
				if (fieldValue5 != null)
				{
					byteBuffer.WriteCompressedUInt(fieldValue5.Value);
				}
			}
			else if (unmanagedType == UnmanagedType.CustomMarshaler)
			{
				byteBuffer.WriteCompressedUInt(0);
				byteBuffer.WriteCompressedUInt(0);
				string text = (string)attribute.GetFieldValue("MarshalType");
				if (text != null)
				{
					FieldMarshal.WriteString(byteBuffer, text);
				}
				else
				{
					FieldMarshal.WriteType(module, byteBuffer, (Type)attribute.GetFieldValue("MarshalTypeRef"));
				}
				FieldMarshal.WriteString(byteBuffer, ((string)attribute.GetFieldValue("MarshalCookie")) ?? "");
			}
			return module.Blobs.Add(byteBuffer);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008958 File Offset: 0x00006B58
		private static Type ReadType(Module module, ByteReader br)
		{
			string text = FieldMarshal.ReadString(br);
			if (text == "")
			{
				return null;
			}
			return module.Assembly.GetType(text) ?? module.universe.GetType(text, true);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008998 File Offset: 0x00006B98
		private static void WriteType(Module module, ByteBuffer bb, Type type)
		{
			FieldMarshal.WriteString(bb, (type.Assembly == module.Assembly) ? type.FullName : type.AssemblyQualifiedName);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000089BC File Offset: 0x00006BBC
		private static string ReadString(ByteReader br)
		{
			return Encoding.UTF8.GetString(br.ReadBytes(br.ReadCompressedUInt()));
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000089D4 File Offset: 0x00006BD4
		private static void WriteString(ByteBuffer bb, string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			bb.WriteCompressedUInt(bytes.Length);
			bb.Write(bytes);
		}

		// Token: 0x04000150 RID: 336
		private const UnmanagedType NATIVE_TYPE_MAX = (UnmanagedType)80;

		// Token: 0x04000151 RID: 337
		public UnmanagedType UnmanagedType;

		// Token: 0x04000152 RID: 338
		public UnmanagedType? ArraySubType;

		// Token: 0x04000153 RID: 339
		public short? SizeParamIndex;

		// Token: 0x04000154 RID: 340
		public int? SizeConst;

		// Token: 0x04000155 RID: 341
		public VarEnum? SafeArraySubType;

		// Token: 0x04000156 RID: 342
		public Type SafeArrayUserDefinedSubType;

		// Token: 0x04000157 RID: 343
		public int? IidParameterIndex;

		// Token: 0x04000158 RID: 344
		public string MarshalType;

		// Token: 0x04000159 RID: 345
		public string MarshalCookie;

		// Token: 0x0400015A RID: 346
		public Type MarshalTypeRef;
	}
}
