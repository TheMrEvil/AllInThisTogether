using System;
using System.Globalization;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x02000184 RID: 388
	internal class JsonObjectDataContract : JsonDataContract
	{
		// Token: 0x06001390 RID: 5008 RVA: 0x00049E98 File Offset: 0x00048098
		public JsonObjectDataContract(DataContract traditionalDataContract) : base(traditionalDataContract)
		{
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0004B84C File Offset: 0x00049A4C
		public override object ReadJsonValueCore(XmlReaderDelegator jsonReader, XmlObjectSerializerReadContextComplexJson context)
		{
			string attribute = jsonReader.GetAttribute("type");
			object obj;
			if (!(attribute == "null"))
			{
				if (!(attribute == "boolean"))
				{
					if (!(attribute == "string") && attribute != null)
					{
						if (!(attribute == "number"))
						{
							if (!(attribute == "object"))
							{
								if (!(attribute == "array"))
								{
									throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer.CreateSerializationException(SR.GetString("Unexpected attribute value '{0}'.", new object[]
									{
										attribute
									})));
								}
								return DataContractJsonSerializer.ReadJsonValue(DataContract.GetDataContract(Globals.TypeOfObjectArray), jsonReader, context);
							}
							else
							{
								jsonReader.Skip();
								obj = new object();
							}
						}
						else
						{
							obj = JsonObjectDataContract.ParseJsonNumber(jsonReader.ReadElementContentAsString());
						}
					}
					else
					{
						obj = jsonReader.ReadElementContentAsString();
					}
				}
				else
				{
					obj = jsonReader.ReadElementContentAsBoolean();
				}
			}
			else
			{
				jsonReader.Skip();
				obj = null;
			}
			if (context != null)
			{
				context.AddNewObject(obj);
			}
			return obj;
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0004B931 File Offset: 0x00049B31
		public override void WriteJsonValueCore(XmlWriterDelegator jsonWriter, object obj, XmlObjectSerializerWriteContextComplexJson context, RuntimeTypeHandle declaredTypeHandle)
		{
			jsonWriter.WriteAttributeString(null, "type", null, "object");
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0004B948 File Offset: 0x00049B48
		internal static object ParseJsonNumber(string value, out TypeCode objectTypeCode)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("The value '{0}' cannot be parsed as the type '{1}'.", new object[]
				{
					value,
					Globals.TypeOfInt
				})));
			}
			if (value.IndexOfAny(JsonGlobals.floatingPointCharacters) == -1)
			{
				int num;
				if (int.TryParse(value, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out num))
				{
					objectTypeCode = TypeCode.Int32;
					return num;
				}
				long num2;
				if (long.TryParse(value, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out num2))
				{
					objectTypeCode = TypeCode.Int64;
					return num2;
				}
			}
			decimal num3;
			if (decimal.TryParse(value, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out num3))
			{
				objectTypeCode = TypeCode.Decimal;
				if (num3 == 0m)
				{
					double num4 = XmlConverter.ToDouble(value);
					if (num4 != 0.0)
					{
						objectTypeCode = TypeCode.Double;
						return num4;
					}
				}
				return num3;
			}
			objectTypeCode = TypeCode.Double;
			return XmlConverter.ToDouble(value);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0004BA24 File Offset: 0x00049C24
		private static object ParseJsonNumber(string value)
		{
			TypeCode typeCode;
			return JsonObjectDataContract.ParseJsonNumber(value, out typeCode);
		}
	}
}
