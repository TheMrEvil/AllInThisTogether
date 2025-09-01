using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020006B3 RID: 1715
	internal sealed class ObjectReader
	{
		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06003F2A RID: 16170 RVA: 0x000DA8F3 File Offset: 0x000D8AF3
		private SerStack ValueFixupStack
		{
			get
			{
				if (this.valueFixupStack == null)
				{
					this.valueFixupStack = new SerStack("ValueType Fixup Stack");
				}
				return this.valueFixupStack;
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06003F2B RID: 16171 RVA: 0x000DA913 File Offset: 0x000D8B13
		// (set) Token: 0x06003F2C RID: 16172 RVA: 0x000DA91B File Offset: 0x000D8B1B
		internal object TopObject
		{
			get
			{
				return this.m_topObject;
			}
			set
			{
				this.m_topObject = value;
				if (this.m_objectManager != null)
				{
					this.m_objectManager.TopObject = value;
				}
			}
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x000DA938 File Offset: 0x000D8B38
		internal void SetMethodCall(BinaryMethodCall binaryMethodCall)
		{
			this.bMethodCall = true;
			this.binaryMethodCall = binaryMethodCall;
		}

		// Token: 0x06003F2E RID: 16174 RVA: 0x000DA948 File Offset: 0x000D8B48
		internal void SetMethodReturn(BinaryMethodReturn binaryMethodReturn)
		{
			this.bMethodReturn = true;
			this.binaryMethodReturn = binaryMethodReturn;
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x000DA958 File Offset: 0x000D8B58
		internal ObjectReader(Stream stream, ISurrogateSelector selector, StreamingContext context, InternalFE formatterEnums, SerializationBinder binder)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream", Environment.GetResourceString("Stream cannot be null."));
			}
			this.m_stream = stream;
			this.m_surrogates = selector;
			this.m_context = context;
			this.m_binder = binder;
			this.formatterEnums = formatterEnums;
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x000DA9B4 File Offset: 0x000D8BB4
		[SecurityCritical]
		internal object Deserialize(HeaderHandler handler, __BinaryParser serParser, bool fCheck, bool isCrossAppDomain, IMethodCallMessage methodCallMessage)
		{
			if (serParser == null)
			{
				throw new ArgumentNullException("serParser", Environment.GetResourceString("Parameter '{0}' cannot be null.", new object[]
				{
					serParser
				}));
			}
			this.bFullDeserialization = false;
			this.TopObject = null;
			this.topId = 0L;
			this.bMethodCall = false;
			this.bMethodReturn = false;
			this.bIsCrossAppDomain = isCrossAppDomain;
			this.bSimpleAssembly = (this.formatterEnums.FEassemblyFormat == FormatterAssemblyStyle.Simple);
			this.handler = handler;
			serParser.Run();
			if (this.bFullDeserialization)
			{
				this.m_objectManager.DoFixups();
			}
			if (!this.bMethodCall && !this.bMethodReturn)
			{
				if (this.TopObject == null)
				{
					throw new SerializationException(Environment.GetResourceString("No top object."));
				}
				if (this.HasSurrogate(this.TopObject.GetType()) && this.topId != 0L)
				{
					this.TopObject = this.m_objectManager.GetObject(this.topId);
				}
				if (this.TopObject is IObjectReference)
				{
					this.TopObject = ((IObjectReference)this.TopObject).GetRealObject(this.m_context);
				}
			}
			if (this.bFullDeserialization)
			{
				this.m_objectManager.RaiseDeserializationEvent();
			}
			if (handler != null)
			{
				this.handlerObject = handler(this.headers);
			}
			if (this.bMethodCall)
			{
				object[] callA = this.TopObject as object[];
				this.TopObject = this.binaryMethodCall.ReadArray(callA, this.handlerObject);
			}
			else if (this.bMethodReturn)
			{
				object[] returnA = this.TopObject as object[];
				this.TopObject = this.binaryMethodReturn.ReadArray(returnA, methodCallMessage, this.handlerObject);
			}
			return this.TopObject;
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x000DAB50 File Offset: 0x000D8D50
		[SecurityCritical]
		private bool HasSurrogate(Type t)
		{
			ISurrogateSelector surrogateSelector;
			return this.m_surrogates != null && this.m_surrogates.GetSurrogate(t, this.m_context, out surrogateSelector) != null;
		}

		// Token: 0x06003F32 RID: 16178 RVA: 0x000DAB7E File Offset: 0x000D8D7E
		[SecurityCritical]
		private void CheckSerializable(Type t)
		{
			if (!t.IsSerializable && !this.HasSurrogate(t))
			{
				throw new SerializationException(string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Type '{0}' in Assembly '{1}' is not marked as serializable."), t.FullName, t.Assembly.FullName));
			}
		}

		// Token: 0x06003F33 RID: 16179 RVA: 0x000DABBC File Offset: 0x000D8DBC
		[SecurityCritical]
		private void InitFullDeserialization()
		{
			this.bFullDeserialization = true;
			this.stack = new SerStack("ObjectReader Object Stack");
			this.m_objectManager = new ObjectManager(this.m_surrogates, this.m_context, false, this.bIsCrossAppDomain);
			if (this.m_formatterConverter == null)
			{
				this.m_formatterConverter = new FormatterConverter();
			}
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x000DAC11 File Offset: 0x000D8E11
		internal object CrossAppDomainArray(int index)
		{
			return this.crossAppDomainArray[index];
		}

		// Token: 0x06003F35 RID: 16181 RVA: 0x000DAC1B File Offset: 0x000D8E1B
		[SecurityCritical]
		internal ReadObjectInfo CreateReadObjectInfo(Type objectType)
		{
			return ReadObjectInfo.Create(objectType, this.m_surrogates, this.m_context, this.m_objectManager, this.serObjectInfoInit, this.m_formatterConverter, this.bSimpleAssembly);
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x000DAC48 File Offset: 0x000D8E48
		[SecurityCritical]
		internal ReadObjectInfo CreateReadObjectInfo(Type objectType, string[] memberNames, Type[] memberTypes)
		{
			return ReadObjectInfo.Create(objectType, memberNames, memberTypes, this.m_surrogates, this.m_context, this.m_objectManager, this.serObjectInfoInit, this.m_formatterConverter, this.bSimpleAssembly);
		}

		// Token: 0x06003F37 RID: 16183 RVA: 0x000DAC84 File Offset: 0x000D8E84
		[SecurityCritical]
		internal void Parse(ParseRecord pr)
		{
			switch (pr.PRparseTypeEnum)
			{
			case InternalParseTypeE.SerializedStreamHeader:
				this.ParseSerializedStreamHeader(pr);
				return;
			case InternalParseTypeE.Object:
				this.ParseObject(pr);
				return;
			case InternalParseTypeE.Member:
				this.ParseMember(pr);
				return;
			case InternalParseTypeE.ObjectEnd:
				this.ParseObjectEnd(pr);
				return;
			case InternalParseTypeE.MemberEnd:
				this.ParseMemberEnd(pr);
				return;
			case InternalParseTypeE.SerializedStreamHeaderEnd:
				this.ParseSerializedStreamHeaderEnd(pr);
				return;
			case InternalParseTypeE.Envelope:
			case InternalParseTypeE.EnvelopeEnd:
			case InternalParseTypeE.Body:
			case InternalParseTypeE.BodyEnd:
				return;
			}
			throw new SerializationException(Environment.GetResourceString("Invalid element '{0}'.", new object[]
			{
				pr.PRname
			}));
		}

		// Token: 0x06003F38 RID: 16184 RVA: 0x000DAD24 File Offset: 0x000D8F24
		private void ParseError(ParseRecord processing, ParseRecord onStack)
		{
			string key = "Parse error. Current element is not compatible with the next element, {0}.";
			object[] array = new object[1];
			int num = 0;
			string[] array2 = new string[7];
			array2[0] = onStack.PRname;
			array2[1] = " ";
			int num2 = 2;
			object obj = onStack.PRparseTypeEnum;
			array2[num2] = ((obj != null) ? obj.ToString() : null);
			array2[3] = " ";
			array2[4] = processing.PRname;
			array2[5] = " ";
			int num3 = 6;
			object obj2 = processing.PRparseTypeEnum;
			array2[num3] = ((obj2 != null) ? obj2.ToString() : null);
			array[num] = string.Concat(array2);
			throw new SerializationException(Environment.GetResourceString(key, array));
		}

		// Token: 0x06003F39 RID: 16185 RVA: 0x000DADB2 File Offset: 0x000D8FB2
		private void ParseSerializedStreamHeader(ParseRecord pr)
		{
			this.stack.Push(pr);
		}

		// Token: 0x06003F3A RID: 16186 RVA: 0x000DADC0 File Offset: 0x000D8FC0
		private void ParseSerializedStreamHeaderEnd(ParseRecord pr)
		{
			this.stack.Pop();
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06003F3B RID: 16187 RVA: 0x000DADCE File Offset: 0x000D8FCE
		private bool IsRemoting
		{
			get
			{
				return this.bMethodCall || this.bMethodReturn;
			}
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x000DADE0 File Offset: 0x000D8FE0
		[SecurityCritical]
		internal void CheckSecurity(ParseRecord pr)
		{
			Type prdtType = pr.PRdtType;
			if (prdtType != null && this.IsRemoting)
			{
				if (typeof(MarshalByRefObject).IsAssignableFrom(prdtType))
				{
					throw new ArgumentException(Environment.GetResourceString("Type {0} must be marshaled by reference in this context.", new object[]
					{
						prdtType.FullName
					}));
				}
				FormatterServices.CheckTypeSecurity(prdtType, this.formatterEnums.FEsecurityLevel);
			}
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x000DAE44 File Offset: 0x000D9044
		[SecurityCritical]
		private void ParseObject(ParseRecord pr)
		{
			if (!this.bFullDeserialization)
			{
				this.InitFullDeserialization();
			}
			if (pr.PRobjectPositionEnum == InternalObjectPositionE.Top)
			{
				this.topId = pr.PRobjectId;
			}
			if (pr.PRparseTypeEnum == InternalParseTypeE.Object)
			{
				this.stack.Push(pr);
			}
			if (pr.PRobjectTypeEnum == InternalObjectTypeE.Array)
			{
				this.ParseArray(pr);
				return;
			}
			if (pr.PRdtType == null)
			{
				pr.PRnewObj = new TypeLoadExceptionHolder(pr.PRkeyDt);
				return;
			}
			if (pr.PRdtType == Converter.typeofString)
			{
				if (pr.PRvalue == null)
				{
					return;
				}
				pr.PRnewObj = pr.PRvalue;
				if (pr.PRobjectPositionEnum == InternalObjectPositionE.Top)
				{
					this.TopObject = pr.PRnewObj;
					return;
				}
				this.stack.Pop();
				this.RegisterObject(pr.PRnewObj, pr, (ParseRecord)this.stack.Peek());
				return;
			}
			else
			{
				this.CheckSerializable(pr.PRdtType);
				if (this.IsRemoting && this.formatterEnums.FEsecurityLevel != TypeFilterLevel.Full)
				{
					pr.PRnewObj = FormatterServices.GetSafeUninitializedObject(pr.PRdtType);
				}
				else
				{
					pr.PRnewObj = FormatterServices.GetUninitializedObject(pr.PRdtType);
				}
				this.m_objectManager.RaiseOnDeserializingEvent(pr.PRnewObj);
				if (pr.PRnewObj == null)
				{
					throw new SerializationException(Environment.GetResourceString("Top object cannot be instantiated for element '{0}'.", new object[]
					{
						pr.PRdtType
					}));
				}
				if (pr.PRobjectPositionEnum == InternalObjectPositionE.Top)
				{
					this.TopObject = pr.PRnewObj;
				}
				if (pr.PRobjectInfo == null)
				{
					pr.PRobjectInfo = ReadObjectInfo.Create(pr.PRdtType, this.m_surrogates, this.m_context, this.m_objectManager, this.serObjectInfoInit, this.m_formatterConverter, this.bSimpleAssembly);
				}
				this.CheckSecurity(pr);
				return;
			}
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x000DAFF0 File Offset: 0x000D91F0
		[SecurityCritical]
		private void ParseObjectEnd(ParseRecord pr)
		{
			ParseRecord parseRecord = (ParseRecord)this.stack.Peek();
			if (parseRecord == null)
			{
				parseRecord = pr;
			}
			if (parseRecord.PRobjectPositionEnum == InternalObjectPositionE.Top && parseRecord.PRdtType == Converter.typeofString)
			{
				parseRecord.PRnewObj = parseRecord.PRvalue;
				this.TopObject = parseRecord.PRnewObj;
				return;
			}
			this.stack.Pop();
			ParseRecord parseRecord2 = (ParseRecord)this.stack.Peek();
			if (parseRecord.PRnewObj == null)
			{
				return;
			}
			if (parseRecord.PRobjectTypeEnum == InternalObjectTypeE.Array)
			{
				if (parseRecord.PRobjectPositionEnum == InternalObjectPositionE.Top)
				{
					this.TopObject = parseRecord.PRnewObj;
				}
				this.RegisterObject(parseRecord.PRnewObj, parseRecord, parseRecord2);
				return;
			}
			parseRecord.PRobjectInfo.PopulateObjectMembers(parseRecord.PRnewObj, parseRecord.PRmemberData);
			if (!parseRecord.PRisRegistered && parseRecord.PRobjectId > 0L)
			{
				this.RegisterObject(parseRecord.PRnewObj, parseRecord, parseRecord2);
			}
			if (parseRecord.PRisValueTypeFixup)
			{
				((ValueFixup)this.ValueFixupStack.Pop()).Fixup(parseRecord, parseRecord2);
			}
			if (parseRecord.PRobjectPositionEnum == InternalObjectPositionE.Top)
			{
				this.TopObject = parseRecord.PRnewObj;
			}
			parseRecord.PRobjectInfo.ObjectEnd();
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x000DB10C File Offset: 0x000D930C
		[SecurityCritical]
		private void ParseArray(ParseRecord pr)
		{
			if (pr.PRarrayTypeEnum == InternalArrayTypeE.Base64)
			{
				if (pr.PRvalue.Length > 0)
				{
					pr.PRnewObj = Convert.FromBase64String(pr.PRvalue);
				}
				else
				{
					pr.PRnewObj = new byte[0];
				}
				if (this.stack.Peek() == pr)
				{
					this.stack.Pop();
				}
				if (pr.PRobjectPositionEnum == InternalObjectPositionE.Top)
				{
					this.TopObject = pr.PRnewObj;
				}
				ParseRecord objectPr = (ParseRecord)this.stack.Peek();
				this.RegisterObject(pr.PRnewObj, pr, objectPr);
			}
			else if (pr.PRnewObj != null && Converter.IsWriteAsByteArray(pr.PRarrayElementTypeCode))
			{
				if (pr.PRobjectPositionEnum == InternalObjectPositionE.Top)
				{
					this.TopObject = pr.PRnewObj;
				}
				ParseRecord objectPr2 = (ParseRecord)this.stack.Peek();
				this.RegisterObject(pr.PRnewObj, pr, objectPr2);
			}
			else if (pr.PRarrayTypeEnum == InternalArrayTypeE.Jagged || pr.PRarrayTypeEnum == InternalArrayTypeE.Single)
			{
				bool flag = true;
				if (pr.PRlowerBoundA == null || pr.PRlowerBoundA[0] == 0)
				{
					if (pr.PRarrayElementType == Converter.typeofString)
					{
						object[] probjectA = new string[pr.PRlengthA[0]];
						pr.PRobjectA = probjectA;
						pr.PRnewObj = pr.PRobjectA;
						flag = false;
					}
					else if (pr.PRarrayElementType == Converter.typeofObject)
					{
						pr.PRobjectA = new object[pr.PRlengthA[0]];
						pr.PRnewObj = pr.PRobjectA;
						flag = false;
					}
					else if (pr.PRarrayElementType != null)
					{
						pr.PRnewObj = Array.UnsafeCreateInstance(pr.PRarrayElementType, new int[]
						{
							pr.PRlengthA[0]
						});
					}
					pr.PRisLowerBound = false;
				}
				else
				{
					if (pr.PRarrayElementType != null)
					{
						pr.PRnewObj = Array.UnsafeCreateInstance(pr.PRarrayElementType, pr.PRlengthA, pr.PRlowerBoundA);
					}
					pr.PRisLowerBound = true;
				}
				if (pr.PRarrayTypeEnum == InternalArrayTypeE.Single)
				{
					if (!pr.PRisLowerBound && Converter.IsWriteAsByteArray(pr.PRarrayElementTypeCode))
					{
						pr.PRprimitiveArray = new PrimitiveArray(pr.PRarrayElementTypeCode, (Array)pr.PRnewObj);
					}
					else if (flag && pr.PRarrayElementType != null && !pr.PRarrayElementType.IsValueType && !pr.PRisLowerBound)
					{
						pr.PRobjectA = (object[])pr.PRnewObj;
					}
				}
				if (pr.PRobjectPositionEnum == InternalObjectPositionE.Headers)
				{
					this.headers = (Header[])pr.PRnewObj;
				}
				pr.PRindexMap = new int[1];
			}
			else
			{
				if (pr.PRarrayTypeEnum != InternalArrayTypeE.Rectangular)
				{
					throw new SerializationException(Environment.GetResourceString("Invalid array type '{0}'.", new object[]
					{
						pr.PRarrayTypeEnum
					}));
				}
				pr.PRisLowerBound = false;
				if (pr.PRlowerBoundA != null)
				{
					for (int i = 0; i < pr.PRrank; i++)
					{
						if (pr.PRlowerBoundA[i] != 0)
						{
							pr.PRisLowerBound = true;
						}
					}
				}
				if (pr.PRarrayElementType != null)
				{
					if (!pr.PRisLowerBound)
					{
						pr.PRnewObj = Array.UnsafeCreateInstance(pr.PRarrayElementType, pr.PRlengthA);
					}
					else
					{
						pr.PRnewObj = Array.UnsafeCreateInstance(pr.PRarrayElementType, pr.PRlengthA, pr.PRlowerBoundA);
					}
				}
				int num = 1;
				for (int j = 0; j < pr.PRrank; j++)
				{
					num *= pr.PRlengthA[j];
				}
				pr.PRindexMap = new int[pr.PRrank];
				pr.PRrectangularMap = new int[pr.PRrank];
				pr.PRlinearlength = num;
			}
			this.CheckSecurity(pr);
		}

		// Token: 0x06003F40 RID: 16192 RVA: 0x000DB480 File Offset: 0x000D9680
		private void NextRectangleMap(ParseRecord pr)
		{
			for (int i = pr.PRrank - 1; i > -1; i--)
			{
				if (pr.PRrectangularMap[i] < pr.PRlengthA[i] - 1)
				{
					pr.PRrectangularMap[i]++;
					if (i < pr.PRrank - 1)
					{
						for (int j = i + 1; j < pr.PRrank; j++)
						{
							pr.PRrectangularMap[j] = 0;
						}
					}
					Array.Copy(pr.PRrectangularMap, pr.PRindexMap, pr.PRrank);
					return;
				}
			}
		}

		// Token: 0x06003F41 RID: 16193 RVA: 0x000DB504 File Offset: 0x000D9704
		[SecurityCritical]
		private void ParseArrayMember(ParseRecord pr)
		{
			ParseRecord parseRecord = (ParseRecord)this.stack.Peek();
			if (parseRecord.PRarrayTypeEnum == InternalArrayTypeE.Rectangular)
			{
				if (parseRecord.PRmemberIndex > 0)
				{
					this.NextRectangleMap(parseRecord);
				}
				if (parseRecord.PRisLowerBound)
				{
					for (int i = 0; i < parseRecord.PRrank; i++)
					{
						parseRecord.PRindexMap[i] = parseRecord.PRrectangularMap[i] + parseRecord.PRlowerBoundA[i];
					}
				}
			}
			else if (!parseRecord.PRisLowerBound)
			{
				parseRecord.PRindexMap[0] = parseRecord.PRmemberIndex;
			}
			else
			{
				parseRecord.PRindexMap[0] = parseRecord.PRlowerBoundA[0] + parseRecord.PRmemberIndex;
			}
			if (pr.PRmemberValueEnum == InternalMemberValueE.Reference)
			{
				object @object = this.m_objectManager.GetObject(pr.PRidRef);
				if (@object == null)
				{
					int[] array = new int[parseRecord.PRrank];
					Array.Copy(parseRecord.PRindexMap, 0, array, 0, parseRecord.PRrank);
					this.m_objectManager.RecordArrayElementFixup(parseRecord.PRobjectId, array, pr.PRidRef);
				}
				else if (parseRecord.PRobjectA != null)
				{
					parseRecord.PRobjectA[parseRecord.PRindexMap[0]] = @object;
				}
				else
				{
					((Array)parseRecord.PRnewObj).SetValue(@object, parseRecord.PRindexMap);
				}
			}
			else if (pr.PRmemberValueEnum == InternalMemberValueE.Nested)
			{
				if (pr.PRdtType == null)
				{
					pr.PRdtType = parseRecord.PRarrayElementType;
				}
				this.ParseObject(pr);
				this.stack.Push(pr);
				if (parseRecord.PRarrayElementType != null)
				{
					if (parseRecord.PRarrayElementType.IsValueType && pr.PRarrayElementTypeCode == InternalPrimitiveTypeE.Invalid)
					{
						pr.PRisValueTypeFixup = true;
						this.ValueFixupStack.Push(new ValueFixup((Array)parseRecord.PRnewObj, parseRecord.PRindexMap));
					}
					else if (parseRecord.PRobjectA != null)
					{
						parseRecord.PRobjectA[parseRecord.PRindexMap[0]] = pr.PRnewObj;
					}
					else
					{
						((Array)parseRecord.PRnewObj).SetValue(pr.PRnewObj, parseRecord.PRindexMap);
					}
				}
			}
			else if (pr.PRmemberValueEnum == InternalMemberValueE.InlineValue)
			{
				if (parseRecord.PRarrayElementType == Converter.typeofString || pr.PRdtType == Converter.typeofString)
				{
					this.ParseString(pr, parseRecord);
					if (parseRecord.PRobjectA != null)
					{
						parseRecord.PRobjectA[parseRecord.PRindexMap[0]] = pr.PRvalue;
					}
					else
					{
						((Array)parseRecord.PRnewObj).SetValue(pr.PRvalue, parseRecord.PRindexMap);
					}
				}
				else if (parseRecord.PRisArrayVariant)
				{
					if (pr.PRkeyDt == null)
					{
						throw new SerializationException(Environment.GetResourceString("Array element type is Object, 'dt' attribute is null."));
					}
					object obj;
					if (pr.PRdtType == Converter.typeofString)
					{
						this.ParseString(pr, parseRecord);
						obj = pr.PRvalue;
					}
					else if (pr.PRdtTypeCode == InternalPrimitiveTypeE.Invalid)
					{
						this.CheckSerializable(pr.PRdtType);
						if (this.IsRemoting && this.formatterEnums.FEsecurityLevel != TypeFilterLevel.Full)
						{
							obj = FormatterServices.GetSafeUninitializedObject(pr.PRdtType);
						}
						else
						{
							obj = FormatterServices.GetUninitializedObject(pr.PRdtType);
						}
					}
					else if (pr.PRvarValue != null)
					{
						obj = pr.PRvarValue;
					}
					else
					{
						obj = Converter.FromString(pr.PRvalue, pr.PRdtTypeCode);
					}
					if (parseRecord.PRobjectA != null)
					{
						parseRecord.PRobjectA[parseRecord.PRindexMap[0]] = obj;
					}
					else
					{
						((Array)parseRecord.PRnewObj).SetValue(obj, parseRecord.PRindexMap);
					}
				}
				else if (parseRecord.PRprimitiveArray != null)
				{
					parseRecord.PRprimitiveArray.SetValue(pr.PRvalue, parseRecord.PRindexMap[0]);
				}
				else
				{
					object obj2;
					if (pr.PRvarValue != null)
					{
						obj2 = pr.PRvarValue;
					}
					else
					{
						obj2 = Converter.FromString(pr.PRvalue, parseRecord.PRarrayElementTypeCode);
					}
					if (parseRecord.PRobjectA != null)
					{
						parseRecord.PRobjectA[parseRecord.PRindexMap[0]] = obj2;
					}
					else
					{
						((Array)parseRecord.PRnewObj).SetValue(obj2, parseRecord.PRindexMap);
					}
				}
			}
			else if (pr.PRmemberValueEnum == InternalMemberValueE.Null)
			{
				parseRecord.PRmemberIndex += pr.PRnullCount - 1;
			}
			else
			{
				this.ParseError(pr, parseRecord);
			}
			parseRecord.PRmemberIndex++;
		}

		// Token: 0x06003F42 RID: 16194 RVA: 0x000DB91A File Offset: 0x000D9B1A
		[SecurityCritical]
		private void ParseArrayMemberEnd(ParseRecord pr)
		{
			if (pr.PRmemberValueEnum == InternalMemberValueE.Nested)
			{
				this.ParseObjectEnd(pr);
			}
		}

		// Token: 0x06003F43 RID: 16195 RVA: 0x000DB92C File Offset: 0x000D9B2C
		[SecurityCritical]
		private void ParseMember(ParseRecord pr)
		{
			ParseRecord parseRecord = (ParseRecord)this.stack.Peek();
			InternalMemberTypeE prmemberTypeEnum = pr.PRmemberTypeEnum;
			if (prmemberTypeEnum != InternalMemberTypeE.Field && prmemberTypeEnum == InternalMemberTypeE.Item)
			{
				this.ParseArrayMember(pr);
				return;
			}
			if (pr.PRdtType == null && parseRecord.PRobjectInfo.isTyped)
			{
				pr.PRdtType = parseRecord.PRobjectInfo.GetType(pr.PRname);
				if (pr.PRdtType != null)
				{
					pr.PRdtTypeCode = Converter.ToCode(pr.PRdtType);
				}
			}
			if (pr.PRmemberValueEnum == InternalMemberValueE.Null)
			{
				parseRecord.PRobjectInfo.AddValue(pr.PRname, null, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
				return;
			}
			if (pr.PRmemberValueEnum == InternalMemberValueE.Nested)
			{
				this.ParseObject(pr);
				this.stack.Push(pr);
				if (pr.PRobjectInfo != null && pr.PRobjectInfo.objectType != null && pr.PRobjectInfo.objectType.IsValueType)
				{
					pr.PRisValueTypeFixup = true;
					this.ValueFixupStack.Push(new ValueFixup(parseRecord.PRnewObj, pr.PRname, parseRecord.PRobjectInfo));
					return;
				}
				parseRecord.PRobjectInfo.AddValue(pr.PRname, pr.PRnewObj, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
				return;
			}
			else
			{
				if (pr.PRmemberValueEnum != InternalMemberValueE.Reference)
				{
					if (pr.PRmemberValueEnum == InternalMemberValueE.InlineValue)
					{
						if (pr.PRdtType == Converter.typeofString)
						{
							this.ParseString(pr, parseRecord);
							parseRecord.PRobjectInfo.AddValue(pr.PRname, pr.PRvalue, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
							return;
						}
						if (pr.PRdtTypeCode != InternalPrimitiveTypeE.Invalid)
						{
							object value;
							if (pr.PRvarValue != null)
							{
								value = pr.PRvarValue;
							}
							else
							{
								value = Converter.FromString(pr.PRvalue, pr.PRdtTypeCode);
							}
							parseRecord.PRobjectInfo.AddValue(pr.PRname, value, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
							return;
						}
						if (pr.PRarrayTypeEnum == InternalArrayTypeE.Base64)
						{
							parseRecord.PRobjectInfo.AddValue(pr.PRname, Convert.FromBase64String(pr.PRvalue), ref parseRecord.PRsi, ref parseRecord.PRmemberData);
							return;
						}
						if (pr.PRdtType == Converter.typeofObject)
						{
							throw new SerializationException(Environment.GetResourceString("Type is missing for member of type Object '{0}'.", new object[]
							{
								pr.PRname
							}));
						}
						this.ParseString(pr, parseRecord);
						if (pr.PRdtType == Converter.typeofSystemVoid)
						{
							parseRecord.PRobjectInfo.AddValue(pr.PRname, pr.PRdtType, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
							return;
						}
						if (parseRecord.PRobjectInfo.isSi)
						{
							parseRecord.PRobjectInfo.AddValue(pr.PRname, pr.PRvalue, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
							return;
						}
					}
					else
					{
						this.ParseError(pr, parseRecord);
					}
					return;
				}
				object @object = this.m_objectManager.GetObject(pr.PRidRef);
				if (@object == null)
				{
					parseRecord.PRobjectInfo.AddValue(pr.PRname, null, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
					parseRecord.PRobjectInfo.RecordFixup(parseRecord.PRobjectId, pr.PRname, pr.PRidRef);
					return;
				}
				parseRecord.PRobjectInfo.AddValue(pr.PRname, @object, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
				return;
			}
		}

		// Token: 0x06003F44 RID: 16196 RVA: 0x000DBC40 File Offset: 0x000D9E40
		[SecurityCritical]
		private void ParseMemberEnd(ParseRecord pr)
		{
			InternalMemberTypeE prmemberTypeEnum = pr.PRmemberTypeEnum;
			if (prmemberTypeEnum != InternalMemberTypeE.Field)
			{
				if (prmemberTypeEnum == InternalMemberTypeE.Item)
				{
					this.ParseArrayMemberEnd(pr);
					return;
				}
				this.ParseError(pr, (ParseRecord)this.stack.Peek());
			}
			else if (pr.PRmemberValueEnum == InternalMemberValueE.Nested)
			{
				this.ParseObjectEnd(pr);
				return;
			}
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x000DBC8C File Offset: 0x000D9E8C
		[SecurityCritical]
		private void ParseString(ParseRecord pr, ParseRecord parentPr)
		{
			if (!pr.PRisRegistered && pr.PRobjectId > 0L)
			{
				this.RegisterObject(pr.PRvalue, pr, parentPr, true);
			}
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x000DBCAF File Offset: 0x000D9EAF
		[SecurityCritical]
		private void RegisterObject(object obj, ParseRecord pr, ParseRecord objectPr)
		{
			this.RegisterObject(obj, pr, objectPr, false);
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x000DBCBC File Offset: 0x000D9EBC
		[SecurityCritical]
		private void RegisterObject(object obj, ParseRecord pr, ParseRecord objectPr, bool bIsString)
		{
			if (!pr.PRisRegistered)
			{
				pr.PRisRegistered = true;
				long idOfContainingObj = 0L;
				MemberInfo member = null;
				int[] arrayIndex = null;
				if (objectPr != null)
				{
					arrayIndex = objectPr.PRindexMap;
					idOfContainingObj = objectPr.PRobjectId;
					if (objectPr.PRobjectInfo != null && !objectPr.PRobjectInfo.isSi)
					{
						member = objectPr.PRobjectInfo.GetMemberInfo(pr.PRname);
					}
				}
				SerializationInfo prsi = pr.PRsi;
				if (bIsString)
				{
					this.m_objectManager.RegisterString((string)obj, pr.PRobjectId, prsi, idOfContainingObj, member);
					return;
				}
				this.m_objectManager.RegisterObject(obj, pr.PRobjectId, prsi, idOfContainingObj, member, arrayIndex);
			}
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x000DBD58 File Offset: 0x000D9F58
		[SecurityCritical]
		internal long GetId(long objectId)
		{
			if (!this.bFullDeserialization)
			{
				this.InitFullDeserialization();
			}
			if (objectId > 0L)
			{
				return objectId;
			}
			if (this.bOldFormatDetected || objectId == -1L)
			{
				this.bOldFormatDetected = true;
				if (this.valTypeObjectIdTable == null)
				{
					this.valTypeObjectIdTable = new IntSizedArray();
				}
				long num;
				if ((num = (long)this.valTypeObjectIdTable[(int)objectId]) == 0L)
				{
					num = 2147483647L + objectId;
					this.valTypeObjectIdTable[(int)objectId] = (int)num;
				}
				return num;
			}
			return -1L * objectId;
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x000DBDD4 File Offset: 0x000D9FD4
		[Conditional("SER_LOGGING")]
		private void IndexTraceMessage(string message, int[] index)
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(10);
			stringBuilder.Append("[");
			for (int i = 0; i < index.Length; i++)
			{
				stringBuilder.Append(index[i]);
				if (i != index.Length - 1)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append("]");
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x000DBE30 File Offset: 0x000DA030
		[SecurityCritical]
		internal Type Bind(string assemblyString, string typeString)
		{
			Type type = null;
			if (this.m_binder != null)
			{
				type = this.m_binder.BindToType(assemblyString, typeString);
			}
			if (type == null)
			{
				type = this.FastBindToType(assemblyString, typeString);
			}
			return type;
		}

		// Token: 0x06003F4B RID: 16203 RVA: 0x000DBE64 File Offset: 0x000DA064
		[SecurityCritical]
		internal Type FastBindToType(string assemblyName, string typeName)
		{
			Type type = null;
			ObjectReader.TypeNAssembly typeNAssembly = (ObjectReader.TypeNAssembly)this.typeCache.GetCachedValue(typeName);
			if (typeNAssembly == null || typeNAssembly.assemblyName != assemblyName)
			{
				Assembly assembly = null;
				if (this.bSimpleAssembly)
				{
					try
					{
						assembly = ObjectReader.ResolveSimpleAssemblyName(new AssemblyName(assemblyName));
					}
					catch (Exception)
					{
					}
					if (assembly == null)
					{
						return null;
					}
					ObjectReader.GetSimplyNamedTypeFromAssembly(assembly, typeName, ref type);
				}
				else
				{
					try
					{
						assembly = Assembly.Load(assemblyName);
					}
					catch (Exception)
					{
					}
					if (assembly == null)
					{
						return null;
					}
					type = FormatterServices.GetTypeFromAssembly(assembly, typeName);
				}
				if (type == null)
				{
					return null;
				}
				ObjectReader.CheckTypeForwardedTo(assembly, type.Assembly, type);
				typeNAssembly = new ObjectReader.TypeNAssembly();
				typeNAssembly.type = type;
				typeNAssembly.assemblyName = assemblyName;
				this.typeCache.SetCachedValue(typeNAssembly);
			}
			return typeNAssembly.type;
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x000DBF40 File Offset: 0x000DA140
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Assembly ResolveSimpleAssemblyName(AssemblyName assemblyName)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMe;
			Assembly assembly = RuntimeAssembly.LoadWithPartialNameInternal(assemblyName, null, ref stackCrawlMark);
			if (assembly == null && assemblyName != null)
			{
				assembly = RuntimeAssembly.LoadWithPartialNameInternal(assemblyName.Name, null, ref stackCrawlMark);
			}
			return assembly;
		}

		// Token: 0x06003F4D RID: 16205 RVA: 0x000DBF78 File Offset: 0x000DA178
		[SecurityCritical]
		private static void GetSimplyNamedTypeFromAssembly(Assembly assm, string typeName, ref Type type)
		{
			try
			{
				type = FormatterServices.GetTypeFromAssembly(assm, typeName);
			}
			catch (TypeLoadException)
			{
			}
			catch (FileNotFoundException)
			{
			}
			catch (FileLoadException)
			{
			}
			catch (BadImageFormatException)
			{
			}
			if (type == null)
			{
				type = Type.GetType(typeName, new Func<AssemblyName, Assembly>(ObjectReader.ResolveSimpleAssemblyName), new Func<Assembly, string, bool, Type>(new ObjectReader.TopLevelAssemblyTypeResolver(assm).ResolveType), false);
			}
		}

		// Token: 0x06003F4E RID: 16206 RVA: 0x000DBFFC File Offset: 0x000DA1FC
		[SecurityCritical]
		internal Type GetType(BinaryAssemblyInfo assemblyInfo, string name)
		{
			Type type = null;
			if (this.previousName != null && this.previousName.Length == name.Length && this.previousName.Equals(name) && this.previousAssemblyString != null && this.previousAssemblyString.Length == assemblyInfo.assemblyString.Length && this.previousAssemblyString.Equals(assemblyInfo.assemblyString))
			{
				type = this.previousType;
			}
			else
			{
				type = this.Bind(assemblyInfo.assemblyString, name);
				if (type == null)
				{
					Assembly assembly = assemblyInfo.GetAssembly();
					if (this.bSimpleAssembly)
					{
						ObjectReader.GetSimplyNamedTypeFromAssembly(assembly, name, ref type);
					}
					else
					{
						type = FormatterServices.GetTypeFromAssembly(assembly, name);
					}
					if (type != null)
					{
						ObjectReader.CheckTypeForwardedTo(assembly, type.Assembly, type);
					}
				}
				this.previousAssemblyString = assemblyInfo.assemblyString;
				this.previousName = name;
				this.previousType = type;
			}
			return type;
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x000DC0D4 File Offset: 0x000DA2D4
		[SecuritySafeCritical]
		private static void CheckTypeForwardedTo(Assembly sourceAssembly, Assembly destAssembly, Type resolvedType)
		{
			if (!FormatterServices.UnsafeTypeForwardersIsEnabled() && sourceAssembly != destAssembly)
			{
				TypeInformation typeInformation = BinaryFormatter.GetTypeInformation(resolvedType);
				if (typeInformation.HasTypeForwardedFrom)
				{
					try
					{
						Assembly.Load(typeInformation.AssemblyString);
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x0400292D RID: 10541
		internal Stream m_stream;

		// Token: 0x0400292E RID: 10542
		internal ISurrogateSelector m_surrogates;

		// Token: 0x0400292F RID: 10543
		internal StreamingContext m_context;

		// Token: 0x04002930 RID: 10544
		internal ObjectManager m_objectManager;

		// Token: 0x04002931 RID: 10545
		internal InternalFE formatterEnums;

		// Token: 0x04002932 RID: 10546
		internal SerializationBinder m_binder;

		// Token: 0x04002933 RID: 10547
		internal long topId;

		// Token: 0x04002934 RID: 10548
		internal bool bSimpleAssembly;

		// Token: 0x04002935 RID: 10549
		internal object handlerObject;

		// Token: 0x04002936 RID: 10550
		internal object m_topObject;

		// Token: 0x04002937 RID: 10551
		internal Header[] headers;

		// Token: 0x04002938 RID: 10552
		internal HeaderHandler handler;

		// Token: 0x04002939 RID: 10553
		internal SerObjectInfoInit serObjectInfoInit;

		// Token: 0x0400293A RID: 10554
		internal IFormatterConverter m_formatterConverter;

		// Token: 0x0400293B RID: 10555
		internal SerStack stack;

		// Token: 0x0400293C RID: 10556
		private SerStack valueFixupStack;

		// Token: 0x0400293D RID: 10557
		internal object[] crossAppDomainArray;

		// Token: 0x0400293E RID: 10558
		private bool bFullDeserialization;

		// Token: 0x0400293F RID: 10559
		private bool bMethodCall;

		// Token: 0x04002940 RID: 10560
		private bool bMethodReturn;

		// Token: 0x04002941 RID: 10561
		private BinaryMethodCall binaryMethodCall;

		// Token: 0x04002942 RID: 10562
		private BinaryMethodReturn binaryMethodReturn;

		// Token: 0x04002943 RID: 10563
		private bool bIsCrossAppDomain;

		// Token: 0x04002944 RID: 10564
		private const int THRESHOLD_FOR_VALUETYPE_IDS = 2147483647;

		// Token: 0x04002945 RID: 10565
		private bool bOldFormatDetected;

		// Token: 0x04002946 RID: 10566
		private IntSizedArray valTypeObjectIdTable;

		// Token: 0x04002947 RID: 10567
		private NameCache typeCache = new NameCache();

		// Token: 0x04002948 RID: 10568
		private string previousAssemblyString;

		// Token: 0x04002949 RID: 10569
		private string previousName;

		// Token: 0x0400294A RID: 10570
		private Type previousType;

		// Token: 0x020006B4 RID: 1716
		internal class TypeNAssembly
		{
			// Token: 0x06003F50 RID: 16208 RVA: 0x0000259F File Offset: 0x0000079F
			public TypeNAssembly()
			{
			}

			// Token: 0x0400294B RID: 10571
			public Type type;

			// Token: 0x0400294C RID: 10572
			public string assemblyName;
		}

		// Token: 0x020006B5 RID: 1717
		internal sealed class TopLevelAssemblyTypeResolver
		{
			// Token: 0x06003F51 RID: 16209 RVA: 0x000DC124 File Offset: 0x000DA324
			public TopLevelAssemblyTypeResolver(Assembly topLevelAssembly)
			{
				this.m_topLevelAssembly = topLevelAssembly;
			}

			// Token: 0x06003F52 RID: 16210 RVA: 0x000DC133 File Offset: 0x000DA333
			public Type ResolveType(Assembly assembly, string simpleTypeName, bool ignoreCase)
			{
				if (assembly == null)
				{
					assembly = this.m_topLevelAssembly;
				}
				return assembly.GetType(simpleTypeName, false, ignoreCase);
			}

			// Token: 0x0400294D RID: 10573
			private Assembly m_topLevelAssembly;
		}
	}
}
