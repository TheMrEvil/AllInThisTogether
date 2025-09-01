using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000207 RID: 519
	internal static class CachedReflectionInfo
	{
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0002BD98 File Offset: 0x00029F98
		public static MethodInfo String_Format_String_ObjectArray
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_String_Format_String_ObjectArray) == null)
				{
					result = (CachedReflectionInfo.s_String_Format_String_ObjectArray = typeof(string).GetMethod("Format", new Type[]
					{
						typeof(string),
						typeof(object[])
					}));
				}
				return result;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0002BDE8 File Offset: 0x00029FE8
		public static ConstructorInfo InvalidCastException_Ctor_String
		{
			get
			{
				ConstructorInfo result;
				if ((result = CachedReflectionInfo.s_InvalidCastException_Ctor_String) == null)
				{
					result = (CachedReflectionInfo.s_InvalidCastException_Ctor_String = typeof(InvalidCastException).GetConstructor(new Type[]
					{
						typeof(string)
					}));
				}
				return result;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0002BE1B File Offset: 0x0002A01B
		public static MethodInfo CallSiteOps_SetNotMatched
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_CallSiteOps_SetNotMatched) == null)
				{
					result = (CachedReflectionInfo.s_CallSiteOps_SetNotMatched = typeof(CallSiteOps).GetMethod("SetNotMatched"));
				}
				return result;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x0002BE40 File Offset: 0x0002A040
		public static MethodInfo CallSiteOps_CreateMatchmaker
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_CallSiteOps_CreateMatchmaker) == null)
				{
					result = (CachedReflectionInfo.s_CallSiteOps_CreateMatchmaker = typeof(CallSiteOps).GetMethod("CreateMatchmaker"));
				}
				return result;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0002BE65 File Offset: 0x0002A065
		public static MethodInfo CallSiteOps_GetMatch
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_CallSiteOps_GetMatch) == null)
				{
					result = (CachedReflectionInfo.s_CallSiteOps_GetMatch = typeof(CallSiteOps).GetMethod("GetMatch"));
				}
				return result;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0002BE8A File Offset: 0x0002A08A
		public static MethodInfo CallSiteOps_ClearMatch
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_CallSiteOps_ClearMatch) == null)
				{
					result = (CachedReflectionInfo.s_CallSiteOps_ClearMatch = typeof(CallSiteOps).GetMethod("ClearMatch"));
				}
				return result;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0002BEAF File Offset: 0x0002A0AF
		public static MethodInfo CallSiteOps_UpdateRules
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_CallSiteOps_UpdateRules) == null)
				{
					result = (CachedReflectionInfo.s_CallSiteOps_UpdateRules = typeof(CallSiteOps).GetMethod("UpdateRules"));
				}
				return result;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0002BED4 File Offset: 0x0002A0D4
		public static MethodInfo CallSiteOps_GetRules
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_CallSiteOps_GetRules) == null)
				{
					result = (CachedReflectionInfo.s_CallSiteOps_GetRules = typeof(CallSiteOps).GetMethod("GetRules"));
				}
				return result;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0002BEF9 File Offset: 0x0002A0F9
		public static MethodInfo CallSiteOps_GetRuleCache
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_CallSiteOps_GetRuleCache) == null)
				{
					result = (CachedReflectionInfo.s_CallSiteOps_GetRuleCache = typeof(CallSiteOps).GetMethod("GetRuleCache"));
				}
				return result;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0002BF1E File Offset: 0x0002A11E
		public static MethodInfo CallSiteOps_GetCachedRules
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_CallSiteOps_GetCachedRules) == null)
				{
					result = (CachedReflectionInfo.s_CallSiteOps_GetCachedRules = typeof(CallSiteOps).GetMethod("GetCachedRules"));
				}
				return result;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0002BF43 File Offset: 0x0002A143
		public static MethodInfo CallSiteOps_AddRule
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_CallSiteOps_AddRule) == null)
				{
					result = (CachedReflectionInfo.s_CallSiteOps_AddRule = typeof(CallSiteOps).GetMethod("AddRule"));
				}
				return result;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x0002BF68 File Offset: 0x0002A168
		public static MethodInfo CallSiteOps_MoveRule
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_CallSiteOps_MoveRule) == null)
				{
					result = (CachedReflectionInfo.s_CallSiteOps_MoveRule = typeof(CallSiteOps).GetMethod("MoveRule"));
				}
				return result;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0002BF8D File Offset: 0x0002A18D
		public static MethodInfo CallSiteOps_Bind
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_CallSiteOps_Bind) == null)
				{
					result = (CachedReflectionInfo.s_CallSiteOps_Bind = typeof(CallSiteOps).GetMethod("Bind"));
				}
				return result;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x0002BFB2 File Offset: 0x0002A1B2
		public static MethodInfo DynamicObject_TryGetMember
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_DynamicObject_TryGetMember) == null)
				{
					result = (CachedReflectionInfo.s_DynamicObject_TryGetMember = typeof(DynamicObject).GetMethod("TryGetMember"));
				}
				return result;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0002BFD7 File Offset: 0x0002A1D7
		public static MethodInfo DynamicObject_TrySetMember
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_DynamicObject_TrySetMember) == null)
				{
					result = (CachedReflectionInfo.s_DynamicObject_TrySetMember = typeof(DynamicObject).GetMethod("TrySetMember"));
				}
				return result;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000C93 RID: 3219 RVA: 0x0002BFFC File Offset: 0x0002A1FC
		public static MethodInfo DynamicObject_TryDeleteMember
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_DynamicObject_TryDeleteMember) == null)
				{
					result = (CachedReflectionInfo.s_DynamicObject_TryDeleteMember = typeof(DynamicObject).GetMethod("TryDeleteMember"));
				}
				return result;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0002C021 File Offset: 0x0002A221
		public static MethodInfo DynamicObject_TryGetIndex
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_DynamicObject_TryGetIndex) == null)
				{
					result = (CachedReflectionInfo.s_DynamicObject_TryGetIndex = typeof(DynamicObject).GetMethod("TryGetIndex"));
				}
				return result;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0002C046 File Offset: 0x0002A246
		public static MethodInfo DynamicObject_TrySetIndex
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_DynamicObject_TrySetIndex) == null)
				{
					result = (CachedReflectionInfo.s_DynamicObject_TrySetIndex = typeof(DynamicObject).GetMethod("TrySetIndex"));
				}
				return result;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0002C06B File Offset: 0x0002A26B
		public static MethodInfo DynamicObject_TryDeleteIndex
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_DynamicObject_TryDeleteIndex) == null)
				{
					result = (CachedReflectionInfo.s_DynamicObject_TryDeleteIndex = typeof(DynamicObject).GetMethod("TryDeleteIndex"));
				}
				return result;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x0002C090 File Offset: 0x0002A290
		public static MethodInfo DynamicObject_TryConvert
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_DynamicObject_TryConvert) == null)
				{
					result = (CachedReflectionInfo.s_DynamicObject_TryConvert = typeof(DynamicObject).GetMethod("TryConvert"));
				}
				return result;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x0002C0B5 File Offset: 0x0002A2B5
		public static MethodInfo DynamicObject_TryInvoke
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_DynamicObject_TryInvoke) == null)
				{
					result = (CachedReflectionInfo.s_DynamicObject_TryInvoke = typeof(DynamicObject).GetMethod("TryInvoke"));
				}
				return result;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x0002C0DA File Offset: 0x0002A2DA
		public static MethodInfo DynamicObject_TryInvokeMember
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_DynamicObject_TryInvokeMember) == null)
				{
					result = (CachedReflectionInfo.s_DynamicObject_TryInvokeMember = typeof(DynamicObject).GetMethod("TryInvokeMember"));
				}
				return result;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0002C0FF File Offset: 0x0002A2FF
		public static MethodInfo DynamicObject_TryBinaryOperation
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_DynamicObject_TryBinaryOperation) == null)
				{
					result = (CachedReflectionInfo.s_DynamicObject_TryBinaryOperation = typeof(DynamicObject).GetMethod("TryBinaryOperation"));
				}
				return result;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0002C124 File Offset: 0x0002A324
		public static MethodInfo DynamicObject_TryUnaryOperation
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_DynamicObject_TryUnaryOperation) == null)
				{
					result = (CachedReflectionInfo.s_DynamicObject_TryUnaryOperation = typeof(DynamicObject).GetMethod("TryUnaryOperation"));
				}
				return result;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0002C149 File Offset: 0x0002A349
		public static MethodInfo DynamicObject_TryCreateInstance
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_DynamicObject_TryCreateInstance) == null)
				{
					result = (CachedReflectionInfo.s_DynamicObject_TryCreateInstance = typeof(DynamicObject).GetMethod("TryCreateInstance"));
				}
				return result;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0002C16E File Offset: 0x0002A36E
		public static ConstructorInfo Nullable_Boolean_Ctor
		{
			get
			{
				ConstructorInfo result;
				if ((result = CachedReflectionInfo.s_Nullable_Boolean_Ctor) == null)
				{
					result = (CachedReflectionInfo.s_Nullable_Boolean_Ctor = typeof(bool?).GetConstructor(new Type[]
					{
						typeof(bool)
					}));
				}
				return result;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x0002C1A1 File Offset: 0x0002A3A1
		public static ConstructorInfo Decimal_Ctor_Int32
		{
			get
			{
				ConstructorInfo result;
				if ((result = CachedReflectionInfo.s_Decimal_Ctor_Int32) == null)
				{
					result = (CachedReflectionInfo.s_Decimal_Ctor_Int32 = typeof(decimal).GetConstructor(new Type[]
					{
						typeof(int)
					}));
				}
				return result;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x0002C1D4 File Offset: 0x0002A3D4
		public static ConstructorInfo Decimal_Ctor_UInt32
		{
			get
			{
				ConstructorInfo result;
				if ((result = CachedReflectionInfo.s_Decimal_Ctor_UInt32) == null)
				{
					result = (CachedReflectionInfo.s_Decimal_Ctor_UInt32 = typeof(decimal).GetConstructor(new Type[]
					{
						typeof(uint)
					}));
				}
				return result;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0002C207 File Offset: 0x0002A407
		public static ConstructorInfo Decimal_Ctor_Int64
		{
			get
			{
				ConstructorInfo result;
				if ((result = CachedReflectionInfo.s_Decimal_Ctor_Int64) == null)
				{
					result = (CachedReflectionInfo.s_Decimal_Ctor_Int64 = typeof(decimal).GetConstructor(new Type[]
					{
						typeof(long)
					}));
				}
				return result;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0002C23A File Offset: 0x0002A43A
		public static ConstructorInfo Decimal_Ctor_UInt64
		{
			get
			{
				ConstructorInfo result;
				if ((result = CachedReflectionInfo.s_Decimal_Ctor_UInt64) == null)
				{
					result = (CachedReflectionInfo.s_Decimal_Ctor_UInt64 = typeof(decimal).GetConstructor(new Type[]
					{
						typeof(ulong)
					}));
				}
				return result;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0002C270 File Offset: 0x0002A470
		public static ConstructorInfo Decimal_Ctor_Int32_Int32_Int32_Bool_Byte
		{
			get
			{
				ConstructorInfo result;
				if ((result = CachedReflectionInfo.s_Decimal_Ctor_Int32_Int32_Int32_Bool_Byte) == null)
				{
					result = (CachedReflectionInfo.s_Decimal_Ctor_Int32_Int32_Int32_Bool_Byte = typeof(decimal).GetConstructor(new Type[]
					{
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(bool),
						typeof(byte)
					}));
				}
				return result;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x0002C2E2 File Offset: 0x0002A4E2
		public static FieldInfo Decimal_One
		{
			get
			{
				FieldInfo result;
				if ((result = CachedReflectionInfo.s_Decimal_One) == null)
				{
					result = (CachedReflectionInfo.s_Decimal_One = typeof(decimal).GetField("One"));
				}
				return result;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0002C307 File Offset: 0x0002A507
		public static FieldInfo Decimal_MinusOne
		{
			get
			{
				FieldInfo result;
				if ((result = CachedReflectionInfo.s_Decimal_MinusOne) == null)
				{
					result = (CachedReflectionInfo.s_Decimal_MinusOne = typeof(decimal).GetField("MinusOne"));
				}
				return result;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0002C32C File Offset: 0x0002A52C
		public static FieldInfo Decimal_MinValue
		{
			get
			{
				FieldInfo result;
				if ((result = CachedReflectionInfo.s_Decimal_MinValue) == null)
				{
					result = (CachedReflectionInfo.s_Decimal_MinValue = typeof(decimal).GetField("MinValue"));
				}
				return result;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0002C351 File Offset: 0x0002A551
		public static FieldInfo Decimal_MaxValue
		{
			get
			{
				FieldInfo result;
				if ((result = CachedReflectionInfo.s_Decimal_MaxValue) == null)
				{
					result = (CachedReflectionInfo.s_Decimal_MaxValue = typeof(decimal).GetField("MaxValue"));
				}
				return result;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0002C376 File Offset: 0x0002A576
		public static FieldInfo Decimal_Zero
		{
			get
			{
				FieldInfo result;
				if ((result = CachedReflectionInfo.s_Decimal_Zero) == null)
				{
					result = (CachedReflectionInfo.s_Decimal_Zero = typeof(decimal).GetField("Zero"));
				}
				return result;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0002C39B File Offset: 0x0002A59B
		public static FieldInfo DateTime_MinValue
		{
			get
			{
				FieldInfo result;
				if ((result = CachedReflectionInfo.s_DateTime_MinValue) == null)
				{
					result = (CachedReflectionInfo.s_DateTime_MinValue = typeof(DateTime).GetField("MinValue"));
				}
				return result;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0002C3C0 File Offset: 0x0002A5C0
		public static MethodInfo MethodBase_GetMethodFromHandle_RuntimeMethodHandle
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_MethodBase_GetMethodFromHandle_RuntimeMethodHandle) == null)
				{
					result = (CachedReflectionInfo.s_MethodBase_GetMethodFromHandle_RuntimeMethodHandle = typeof(MethodBase).GetMethod("GetMethodFromHandle", new Type[]
					{
						typeof(RuntimeMethodHandle)
					}));
				}
				return result;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0002C3F8 File Offset: 0x0002A5F8
		public static MethodInfo MethodBase_GetMethodFromHandle_RuntimeMethodHandle_RuntimeTypeHandle
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_MethodBase_GetMethodFromHandle_RuntimeMethodHandle_RuntimeTypeHandle) == null)
				{
					result = (CachedReflectionInfo.s_MethodBase_GetMethodFromHandle_RuntimeMethodHandle_RuntimeTypeHandle = typeof(MethodBase).GetMethod("GetMethodFromHandle", new Type[]
					{
						typeof(RuntimeMethodHandle),
						typeof(RuntimeTypeHandle)
					}));
				}
				return result;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0002C448 File Offset: 0x0002A648
		public static MethodInfo MethodInfo_CreateDelegate_Type_Object
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_MethodInfo_CreateDelegate_Type_Object) == null)
				{
					result = (CachedReflectionInfo.s_MethodInfo_CreateDelegate_Type_Object = typeof(MethodInfo).GetMethod("CreateDelegate", new Type[]
					{
						typeof(Type),
						typeof(object)
					}));
				}
				return result;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000CAC RID: 3244 RVA: 0x0002C498 File Offset: 0x0002A698
		public static MethodInfo String_op_Equality_String_String
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_String_op_Equality_String_String) == null)
				{
					result = (CachedReflectionInfo.s_String_op_Equality_String_String = typeof(string).GetMethod("op_Equality", new Type[]
					{
						typeof(string),
						typeof(string)
					}));
				}
				return result;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x0002C4E8 File Offset: 0x0002A6E8
		public static MethodInfo String_Equals_String_String
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_String_Equals_String_String) == null)
				{
					result = (CachedReflectionInfo.s_String_Equals_String_String = typeof(string).GetMethod("Equals", new Type[]
					{
						typeof(string),
						typeof(string)
					}));
				}
				return result;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x0002C538 File Offset: 0x0002A738
		public static MethodInfo DictionaryOfStringInt32_Add_String_Int32
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_DictionaryOfStringInt32_Add_String_Int32) == null)
				{
					result = (CachedReflectionInfo.s_DictionaryOfStringInt32_Add_String_Int32 = typeof(Dictionary<string, int>).GetMethod("Add", new Type[]
					{
						typeof(string),
						typeof(int)
					}));
				}
				return result;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0002C588 File Offset: 0x0002A788
		public static ConstructorInfo DictionaryOfStringInt32_Ctor_Int32
		{
			get
			{
				ConstructorInfo result;
				if ((result = CachedReflectionInfo.s_DictionaryOfStringInt32_Ctor_Int32) == null)
				{
					result = (CachedReflectionInfo.s_DictionaryOfStringInt32_Ctor_Int32 = typeof(Dictionary<string, int>).GetConstructor(new Type[]
					{
						typeof(int)
					}));
				}
				return result;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0002C5BB File Offset: 0x0002A7BB
		public static MethodInfo Type_GetTypeFromHandle
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Type_GetTypeFromHandle) == null)
				{
					result = (CachedReflectionInfo.s_Type_GetTypeFromHandle = typeof(Type).GetMethod("GetTypeFromHandle"));
				}
				return result;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0002C5E0 File Offset: 0x0002A7E0
		public static MethodInfo Object_GetType
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Object_GetType) == null)
				{
					result = (CachedReflectionInfo.s_Object_GetType = typeof(object).GetMethod("GetType"));
				}
				return result;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0002C605 File Offset: 0x0002A805
		public static MethodInfo Decimal_op_Implicit_Byte
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Decimal_op_Implicit_Byte) == null)
				{
					result = (CachedReflectionInfo.s_Decimal_op_Implicit_Byte = typeof(decimal).GetMethod("op_Implicit", new Type[]
					{
						typeof(byte)
					}));
				}
				return result;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0002C63D File Offset: 0x0002A83D
		public static MethodInfo Decimal_op_Implicit_SByte
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Decimal_op_Implicit_SByte) == null)
				{
					result = (CachedReflectionInfo.s_Decimal_op_Implicit_SByte = typeof(decimal).GetMethod("op_Implicit", new Type[]
					{
						typeof(sbyte)
					}));
				}
				return result;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0002C675 File Offset: 0x0002A875
		public static MethodInfo Decimal_op_Implicit_Int16
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Decimal_op_Implicit_Int16) == null)
				{
					result = (CachedReflectionInfo.s_Decimal_op_Implicit_Int16 = typeof(decimal).GetMethod("op_Implicit", new Type[]
					{
						typeof(short)
					}));
				}
				return result;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x0002C6AD File Offset: 0x0002A8AD
		public static MethodInfo Decimal_op_Implicit_UInt16
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Decimal_op_Implicit_UInt16) == null)
				{
					result = (CachedReflectionInfo.s_Decimal_op_Implicit_UInt16 = typeof(decimal).GetMethod("op_Implicit", new Type[]
					{
						typeof(ushort)
					}));
				}
				return result;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x0002C6E5 File Offset: 0x0002A8E5
		public static MethodInfo Decimal_op_Implicit_Int32
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Decimal_op_Implicit_Int32) == null)
				{
					result = (CachedReflectionInfo.s_Decimal_op_Implicit_Int32 = typeof(decimal).GetMethod("op_Implicit", new Type[]
					{
						typeof(int)
					}));
				}
				return result;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0002C71D File Offset: 0x0002A91D
		public static MethodInfo Decimal_op_Implicit_UInt32
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Decimal_op_Implicit_UInt32) == null)
				{
					result = (CachedReflectionInfo.s_Decimal_op_Implicit_UInt32 = typeof(decimal).GetMethod("op_Implicit", new Type[]
					{
						typeof(uint)
					}));
				}
				return result;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x0002C755 File Offset: 0x0002A955
		public static MethodInfo Decimal_op_Implicit_Int64
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Decimal_op_Implicit_Int64) == null)
				{
					result = (CachedReflectionInfo.s_Decimal_op_Implicit_Int64 = typeof(decimal).GetMethod("op_Implicit", new Type[]
					{
						typeof(long)
					}));
				}
				return result;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0002C78D File Offset: 0x0002A98D
		public static MethodInfo Decimal_op_Implicit_UInt64
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Decimal_op_Implicit_UInt64) == null)
				{
					result = (CachedReflectionInfo.s_Decimal_op_Implicit_UInt64 = typeof(decimal).GetMethod("op_Implicit", new Type[]
					{
						typeof(ulong)
					}));
				}
				return result;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x0002C7C5 File Offset: 0x0002A9C5
		public static MethodInfo Decimal_op_Implicit_Char
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Decimal_op_Implicit_Char) == null)
				{
					result = (CachedReflectionInfo.s_Decimal_op_Implicit_Char = typeof(decimal).GetMethod("op_Implicit", new Type[]
					{
						typeof(char)
					}));
				}
				return result;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0002C800 File Offset: 0x0002AA00
		public static MethodInfo Math_Pow_Double_Double
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_Math_Pow_Double_Double) == null)
				{
					result = (CachedReflectionInfo.s_Math_Pow_Double_Double = typeof(Math).GetMethod("Pow", new Type[]
					{
						typeof(double),
						typeof(double)
					}));
				}
				return result;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0002C850 File Offset: 0x0002AA50
		public static ConstructorInfo Closure_ObjectArray_ObjectArray
		{
			get
			{
				ConstructorInfo result;
				if ((result = CachedReflectionInfo.s_Closure_ObjectArray_ObjectArray) == null)
				{
					result = (CachedReflectionInfo.s_Closure_ObjectArray_ObjectArray = typeof(Closure).GetConstructor(new Type[]
					{
						typeof(object[]),
						typeof(object[])
					}));
				}
				return result;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0002C890 File Offset: 0x0002AA90
		public static FieldInfo Closure_Constants
		{
			get
			{
				FieldInfo result;
				if ((result = CachedReflectionInfo.s_Closure_Constants) == null)
				{
					result = (CachedReflectionInfo.s_Closure_Constants = typeof(Closure).GetField("Constants"));
				}
				return result;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0002C8B5 File Offset: 0x0002AAB5
		public static FieldInfo Closure_Locals
		{
			get
			{
				FieldInfo result;
				if ((result = CachedReflectionInfo.s_Closure_Locals) == null)
				{
					result = (CachedReflectionInfo.s_Closure_Locals = typeof(Closure).GetField("Locals"));
				}
				return result;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0002C8DC File Offset: 0x0002AADC
		public static MethodInfo RuntimeOps_CreateRuntimeVariables_ObjectArray_Int64Array
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_RuntimeOps_CreateRuntimeVariables_ObjectArray_Int64Array) == null)
				{
					result = (CachedReflectionInfo.s_RuntimeOps_CreateRuntimeVariables_ObjectArray_Int64Array = typeof(RuntimeOps).GetMethod("CreateRuntimeVariables", new Type[]
					{
						typeof(object[]),
						typeof(long[])
					}));
				}
				return result;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0002C92C File Offset: 0x0002AB2C
		public static MethodInfo RuntimeOps_CreateRuntimeVariables
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_RuntimeOps_CreateRuntimeVariables) == null)
				{
					result = (CachedReflectionInfo.s_RuntimeOps_CreateRuntimeVariables = typeof(RuntimeOps).GetMethod("CreateRuntimeVariables", Type.EmptyTypes));
				}
				return result;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0002C956 File Offset: 0x0002AB56
		public static MethodInfo RuntimeOps_MergeRuntimeVariables
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_RuntimeOps_MergeRuntimeVariables) == null)
				{
					result = (CachedReflectionInfo.s_RuntimeOps_MergeRuntimeVariables = typeof(RuntimeOps).GetMethod("MergeRuntimeVariables"));
				}
				return result;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0002C97B File Offset: 0x0002AB7B
		public static MethodInfo RuntimeOps_Quote
		{
			get
			{
				MethodInfo result;
				if ((result = CachedReflectionInfo.s_RuntimeOps_Quote) == null)
				{
					result = (CachedReflectionInfo.s_RuntimeOps_Quote = typeof(RuntimeOps).GetMethod("Quote"));
				}
				return result;
			}
		}

		// Token: 0x040008D4 RID: 2260
		private static MethodInfo s_String_Format_String_ObjectArray;

		// Token: 0x040008D5 RID: 2261
		private static ConstructorInfo s_InvalidCastException_Ctor_String;

		// Token: 0x040008D6 RID: 2262
		private static MethodInfo s_CallSiteOps_SetNotMatched;

		// Token: 0x040008D7 RID: 2263
		private static MethodInfo s_CallSiteOps_CreateMatchmaker;

		// Token: 0x040008D8 RID: 2264
		private static MethodInfo s_CallSiteOps_GetMatch;

		// Token: 0x040008D9 RID: 2265
		private static MethodInfo s_CallSiteOps_ClearMatch;

		// Token: 0x040008DA RID: 2266
		private static MethodInfo s_CallSiteOps_UpdateRules;

		// Token: 0x040008DB RID: 2267
		private static MethodInfo s_CallSiteOps_GetRules;

		// Token: 0x040008DC RID: 2268
		private static MethodInfo s_CallSiteOps_GetRuleCache;

		// Token: 0x040008DD RID: 2269
		private static MethodInfo s_CallSiteOps_GetCachedRules;

		// Token: 0x040008DE RID: 2270
		private static MethodInfo s_CallSiteOps_AddRule;

		// Token: 0x040008DF RID: 2271
		private static MethodInfo s_CallSiteOps_MoveRule;

		// Token: 0x040008E0 RID: 2272
		private static MethodInfo s_CallSiteOps_Bind;

		// Token: 0x040008E1 RID: 2273
		private static MethodInfo s_DynamicObject_TryGetMember;

		// Token: 0x040008E2 RID: 2274
		private static MethodInfo s_DynamicObject_TrySetMember;

		// Token: 0x040008E3 RID: 2275
		private static MethodInfo s_DynamicObject_TryDeleteMember;

		// Token: 0x040008E4 RID: 2276
		private static MethodInfo s_DynamicObject_TryGetIndex;

		// Token: 0x040008E5 RID: 2277
		private static MethodInfo s_DynamicObject_TrySetIndex;

		// Token: 0x040008E6 RID: 2278
		private static MethodInfo s_DynamicObject_TryDeleteIndex;

		// Token: 0x040008E7 RID: 2279
		private static MethodInfo s_DynamicObject_TryConvert;

		// Token: 0x040008E8 RID: 2280
		private static MethodInfo s_DynamicObject_TryInvoke;

		// Token: 0x040008E9 RID: 2281
		private static MethodInfo s_DynamicObject_TryInvokeMember;

		// Token: 0x040008EA RID: 2282
		private static MethodInfo s_DynamicObject_TryBinaryOperation;

		// Token: 0x040008EB RID: 2283
		private static MethodInfo s_DynamicObject_TryUnaryOperation;

		// Token: 0x040008EC RID: 2284
		private static MethodInfo s_DynamicObject_TryCreateInstance;

		// Token: 0x040008ED RID: 2285
		private static ConstructorInfo s_Nullable_Boolean_Ctor;

		// Token: 0x040008EE RID: 2286
		private static ConstructorInfo s_Decimal_Ctor_Int32;

		// Token: 0x040008EF RID: 2287
		private static ConstructorInfo s_Decimal_Ctor_UInt32;

		// Token: 0x040008F0 RID: 2288
		private static ConstructorInfo s_Decimal_Ctor_Int64;

		// Token: 0x040008F1 RID: 2289
		private static ConstructorInfo s_Decimal_Ctor_UInt64;

		// Token: 0x040008F2 RID: 2290
		private static ConstructorInfo s_Decimal_Ctor_Int32_Int32_Int32_Bool_Byte;

		// Token: 0x040008F3 RID: 2291
		private static FieldInfo s_Decimal_One;

		// Token: 0x040008F4 RID: 2292
		private static FieldInfo s_Decimal_MinusOne;

		// Token: 0x040008F5 RID: 2293
		private static FieldInfo s_Decimal_MinValue;

		// Token: 0x040008F6 RID: 2294
		private static FieldInfo s_Decimal_MaxValue;

		// Token: 0x040008F7 RID: 2295
		private static FieldInfo s_Decimal_Zero;

		// Token: 0x040008F8 RID: 2296
		private static FieldInfo s_DateTime_MinValue;

		// Token: 0x040008F9 RID: 2297
		private static MethodInfo s_MethodBase_GetMethodFromHandle_RuntimeMethodHandle;

		// Token: 0x040008FA RID: 2298
		private static MethodInfo s_MethodBase_GetMethodFromHandle_RuntimeMethodHandle_RuntimeTypeHandle;

		// Token: 0x040008FB RID: 2299
		private static MethodInfo s_MethodInfo_CreateDelegate_Type_Object;

		// Token: 0x040008FC RID: 2300
		private static MethodInfo s_String_op_Equality_String_String;

		// Token: 0x040008FD RID: 2301
		private static MethodInfo s_String_Equals_String_String;

		// Token: 0x040008FE RID: 2302
		private static MethodInfo s_DictionaryOfStringInt32_Add_String_Int32;

		// Token: 0x040008FF RID: 2303
		private static ConstructorInfo s_DictionaryOfStringInt32_Ctor_Int32;

		// Token: 0x04000900 RID: 2304
		private static MethodInfo s_Type_GetTypeFromHandle;

		// Token: 0x04000901 RID: 2305
		private static MethodInfo s_Object_GetType;

		// Token: 0x04000902 RID: 2306
		private static MethodInfo s_Decimal_op_Implicit_Byte;

		// Token: 0x04000903 RID: 2307
		private static MethodInfo s_Decimal_op_Implicit_SByte;

		// Token: 0x04000904 RID: 2308
		private static MethodInfo s_Decimal_op_Implicit_Int16;

		// Token: 0x04000905 RID: 2309
		private static MethodInfo s_Decimal_op_Implicit_UInt16;

		// Token: 0x04000906 RID: 2310
		private static MethodInfo s_Decimal_op_Implicit_Int32;

		// Token: 0x04000907 RID: 2311
		private static MethodInfo s_Decimal_op_Implicit_UInt32;

		// Token: 0x04000908 RID: 2312
		private static MethodInfo s_Decimal_op_Implicit_Int64;

		// Token: 0x04000909 RID: 2313
		private static MethodInfo s_Decimal_op_Implicit_UInt64;

		// Token: 0x0400090A RID: 2314
		private static MethodInfo s_Decimal_op_Implicit_Char;

		// Token: 0x0400090B RID: 2315
		private static MethodInfo s_Math_Pow_Double_Double;

		// Token: 0x0400090C RID: 2316
		private static ConstructorInfo s_Closure_ObjectArray_ObjectArray;

		// Token: 0x0400090D RID: 2317
		private static FieldInfo s_Closure_Constants;

		// Token: 0x0400090E RID: 2318
		private static FieldInfo s_Closure_Locals;

		// Token: 0x0400090F RID: 2319
		private static MethodInfo s_RuntimeOps_CreateRuntimeVariables_ObjectArray_Int64Array;

		// Token: 0x04000910 RID: 2320
		private static MethodInfo s_RuntimeOps_CreateRuntimeVariables;

		// Token: 0x04000911 RID: 2321
		private static MethodInfo s_RuntimeOps_MergeRuntimeVariables;

		// Token: 0x04000912 RID: 2322
		private static MethodInfo s_RuntimeOps_Quote;
	}
}
