using System;

namespace UnityEngine
{
	// Token: 0x0200000E RID: 14
	internal class AndroidJNISafe
	{
		// Token: 0x06000122 RID: 290 RVA: 0x0000681C File Offset: 0x00004A1C
		public static void CheckException()
		{
			IntPtr intPtr = AndroidJNI.ExceptionOccurred();
			bool flag = intPtr != IntPtr.Zero;
			if (flag)
			{
				AndroidJNI.ExceptionClear();
				IntPtr intPtr2 = AndroidJNI.FindClass("java/lang/Throwable");
				IntPtr intPtr3 = AndroidJNI.FindClass("android/util/Log");
				try
				{
					IntPtr methodID = AndroidJNI.GetMethodID(intPtr2, "toString", "()Ljava/lang/String;");
					IntPtr staticMethodID = AndroidJNI.GetStaticMethodID(intPtr3, "getStackTraceString", "(Ljava/lang/Throwable;)Ljava/lang/String;");
					string message = AndroidJNI.CallStringMethod(intPtr, methodID, new jvalue[0]);
					jvalue[] array = new jvalue[1];
					array[0].l = intPtr;
					string javaStackTrace = AndroidJNI.CallStaticStringMethod(intPtr3, staticMethodID, array);
					throw new AndroidJavaException(message, javaStackTrace);
				}
				finally
				{
					AndroidJNISafe.DeleteLocalRef(intPtr);
					AndroidJNISafe.DeleteLocalRef(intPtr2);
					AndroidJNISafe.DeleteLocalRef(intPtr3);
				}
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000068E8 File Offset: 0x00004AE8
		public static void DeleteGlobalRef(IntPtr globalref)
		{
			bool flag = globalref != IntPtr.Zero;
			if (flag)
			{
				AndroidJNI.DeleteGlobalRef(globalref);
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000690C File Offset: 0x00004B0C
		public static void DeleteWeakGlobalRef(IntPtr globalref)
		{
			bool flag = globalref != IntPtr.Zero;
			if (flag)
			{
				AndroidJNI.DeleteWeakGlobalRef(globalref);
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006930 File Offset: 0x00004B30
		public static void DeleteLocalRef(IntPtr localref)
		{
			bool flag = localref != IntPtr.Zero;
			if (flag)
			{
				AndroidJNI.DeleteLocalRef(localref);
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006954 File Offset: 0x00004B54
		public static IntPtr NewString(string chars)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.NewString(chars);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006988 File Offset: 0x00004B88
		public static IntPtr NewStringUTF(string bytes)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.NewStringUTF(bytes);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000069BC File Offset: 0x00004BBC
		public static string GetStringChars(IntPtr str)
		{
			string stringChars;
			try
			{
				stringChars = AndroidJNI.GetStringChars(str);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return stringChars;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000069F0 File Offset: 0x00004BF0
		public static string GetStringUTFChars(IntPtr str)
		{
			string stringUTFChars;
			try
			{
				stringUTFChars = AndroidJNI.GetStringUTFChars(str);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return stringUTFChars;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006A24 File Offset: 0x00004C24
		public static IntPtr GetObjectClass(IntPtr ptr)
		{
			IntPtr objectClass;
			try
			{
				objectClass = AndroidJNI.GetObjectClass(ptr);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return objectClass;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006A58 File Offset: 0x00004C58
		public static IntPtr GetStaticMethodID(IntPtr clazz, string name, string sig)
		{
			IntPtr staticMethodID;
			try
			{
				staticMethodID = AndroidJNI.GetStaticMethodID(clazz, name, sig);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticMethodID;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006A8C File Offset: 0x00004C8C
		public static IntPtr GetMethodID(IntPtr obj, string name, string sig)
		{
			IntPtr methodID;
			try
			{
				methodID = AndroidJNI.GetMethodID(obj, name, sig);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return methodID;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006AC0 File Offset: 0x00004CC0
		public static IntPtr GetFieldID(IntPtr clazz, string name, string sig)
		{
			IntPtr fieldID;
			try
			{
				fieldID = AndroidJNI.GetFieldID(clazz, name, sig);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return fieldID;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006AF4 File Offset: 0x00004CF4
		public static IntPtr GetStaticFieldID(IntPtr clazz, string name, string sig)
		{
			IntPtr staticFieldID;
			try
			{
				staticFieldID = AndroidJNI.GetStaticFieldID(clazz, name, sig);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticFieldID;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006B28 File Offset: 0x00004D28
		public static IntPtr FromReflectedMethod(IntPtr refMethod)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.FromReflectedMethod(refMethod);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006B5C File Offset: 0x00004D5C
		public static IntPtr FromReflectedField(IntPtr refField)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.FromReflectedField(refField);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006B90 File Offset: 0x00004D90
		public static IntPtr FindClass(string name)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.FindClass(name);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006BC4 File Offset: 0x00004DC4
		public static IntPtr NewObject(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.NewObject(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006BF8 File Offset: 0x00004DF8
		public static void SetStaticObjectField(IntPtr clazz, IntPtr fieldID, IntPtr val)
		{
			try
			{
				AndroidJNI.SetStaticObjectField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006C2C File Offset: 0x00004E2C
		public static void SetStaticStringField(IntPtr clazz, IntPtr fieldID, string val)
		{
			try
			{
				AndroidJNI.SetStaticStringField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006C60 File Offset: 0x00004E60
		public static void SetStaticCharField(IntPtr clazz, IntPtr fieldID, char val)
		{
			try
			{
				AndroidJNI.SetStaticCharField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006C94 File Offset: 0x00004E94
		public static void SetStaticDoubleField(IntPtr clazz, IntPtr fieldID, double val)
		{
			try
			{
				AndroidJNI.SetStaticDoubleField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006CC8 File Offset: 0x00004EC8
		public static void SetStaticFloatField(IntPtr clazz, IntPtr fieldID, float val)
		{
			try
			{
				AndroidJNI.SetStaticFloatField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006CFC File Offset: 0x00004EFC
		public static void SetStaticLongField(IntPtr clazz, IntPtr fieldID, long val)
		{
			try
			{
				AndroidJNI.SetStaticLongField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006D30 File Offset: 0x00004F30
		public static void SetStaticShortField(IntPtr clazz, IntPtr fieldID, short val)
		{
			try
			{
				AndroidJNI.SetStaticShortField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006D64 File Offset: 0x00004F64
		public static void SetStaticSByteField(IntPtr clazz, IntPtr fieldID, sbyte val)
		{
			try
			{
				AndroidJNI.SetStaticSByteField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006D98 File Offset: 0x00004F98
		public static void SetStaticBooleanField(IntPtr clazz, IntPtr fieldID, bool val)
		{
			try
			{
				AndroidJNI.SetStaticBooleanField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00006DCC File Offset: 0x00004FCC
		public static void SetStaticIntField(IntPtr clazz, IntPtr fieldID, int val)
		{
			try
			{
				AndroidJNI.SetStaticIntField(clazz, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00006E00 File Offset: 0x00005000
		public static IntPtr GetStaticObjectField(IntPtr clazz, IntPtr fieldID)
		{
			IntPtr staticObjectField;
			try
			{
				staticObjectField = AndroidJNI.GetStaticObjectField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticObjectField;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006E34 File Offset: 0x00005034
		public static string GetStaticStringField(IntPtr clazz, IntPtr fieldID)
		{
			string staticStringField;
			try
			{
				staticStringField = AndroidJNI.GetStaticStringField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticStringField;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006E68 File Offset: 0x00005068
		public static char GetStaticCharField(IntPtr clazz, IntPtr fieldID)
		{
			char staticCharField;
			try
			{
				staticCharField = AndroidJNI.GetStaticCharField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticCharField;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006E9C File Offset: 0x0000509C
		public static double GetStaticDoubleField(IntPtr clazz, IntPtr fieldID)
		{
			double staticDoubleField;
			try
			{
				staticDoubleField = AndroidJNI.GetStaticDoubleField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticDoubleField;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006ED0 File Offset: 0x000050D0
		public static float GetStaticFloatField(IntPtr clazz, IntPtr fieldID)
		{
			float staticFloatField;
			try
			{
				staticFloatField = AndroidJNI.GetStaticFloatField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticFloatField;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006F04 File Offset: 0x00005104
		public static long GetStaticLongField(IntPtr clazz, IntPtr fieldID)
		{
			long staticLongField;
			try
			{
				staticLongField = AndroidJNI.GetStaticLongField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticLongField;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006F38 File Offset: 0x00005138
		public static short GetStaticShortField(IntPtr clazz, IntPtr fieldID)
		{
			short staticShortField;
			try
			{
				staticShortField = AndroidJNI.GetStaticShortField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticShortField;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006F6C File Offset: 0x0000516C
		public static sbyte GetStaticSByteField(IntPtr clazz, IntPtr fieldID)
		{
			sbyte staticSByteField;
			try
			{
				staticSByteField = AndroidJNI.GetStaticSByteField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticSByteField;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006FA0 File Offset: 0x000051A0
		public static bool GetStaticBooleanField(IntPtr clazz, IntPtr fieldID)
		{
			bool staticBooleanField;
			try
			{
				staticBooleanField = AndroidJNI.GetStaticBooleanField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticBooleanField;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006FD4 File Offset: 0x000051D4
		public static int GetStaticIntField(IntPtr clazz, IntPtr fieldID)
		{
			int staticIntField;
			try
			{
				staticIntField = AndroidJNI.GetStaticIntField(clazz, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return staticIntField;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007008 File Offset: 0x00005208
		public static void CallStaticVoidMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			try
			{
				AndroidJNI.CallStaticVoidMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000703C File Offset: 0x0000523C
		public static IntPtr CallStaticObjectMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.CallStaticObjectMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00007070 File Offset: 0x00005270
		public static string CallStaticStringMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			string result;
			try
			{
				result = AndroidJNI.CallStaticStringMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000070A4 File Offset: 0x000052A4
		public static char CallStaticCharMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			char result;
			try
			{
				result = AndroidJNI.CallStaticCharMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000070D8 File Offset: 0x000052D8
		public static double CallStaticDoubleMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			double result;
			try
			{
				result = AndroidJNI.CallStaticDoubleMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000710C File Offset: 0x0000530C
		public static float CallStaticFloatMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			float result;
			try
			{
				result = AndroidJNI.CallStaticFloatMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00007140 File Offset: 0x00005340
		public static long CallStaticLongMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			long result;
			try
			{
				result = AndroidJNI.CallStaticLongMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00007174 File Offset: 0x00005374
		public static short CallStaticShortMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			short result;
			try
			{
				result = AndroidJNI.CallStaticShortMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000071A8 File Offset: 0x000053A8
		public static sbyte CallStaticSByteMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			sbyte result;
			try
			{
				result = AndroidJNI.CallStaticSByteMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000071DC File Offset: 0x000053DC
		public static bool CallStaticBooleanMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			bool result;
			try
			{
				result = AndroidJNI.CallStaticBooleanMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00007210 File Offset: 0x00005410
		public static int CallStaticIntMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			int result;
			try
			{
				result = AndroidJNI.CallStaticIntMethod(clazz, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00007244 File Offset: 0x00005444
		public static void SetObjectField(IntPtr obj, IntPtr fieldID, IntPtr val)
		{
			try
			{
				AndroidJNI.SetObjectField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00007278 File Offset: 0x00005478
		public static void SetStringField(IntPtr obj, IntPtr fieldID, string val)
		{
			try
			{
				AndroidJNI.SetStringField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000072AC File Offset: 0x000054AC
		public static void SetCharField(IntPtr obj, IntPtr fieldID, char val)
		{
			try
			{
				AndroidJNI.SetCharField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000072E0 File Offset: 0x000054E0
		public static void SetDoubleField(IntPtr obj, IntPtr fieldID, double val)
		{
			try
			{
				AndroidJNI.SetDoubleField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00007314 File Offset: 0x00005514
		public static void SetFloatField(IntPtr obj, IntPtr fieldID, float val)
		{
			try
			{
				AndroidJNI.SetFloatField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00007348 File Offset: 0x00005548
		public static void SetLongField(IntPtr obj, IntPtr fieldID, long val)
		{
			try
			{
				AndroidJNI.SetLongField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000737C File Offset: 0x0000557C
		public static void SetShortField(IntPtr obj, IntPtr fieldID, short val)
		{
			try
			{
				AndroidJNI.SetShortField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000073B0 File Offset: 0x000055B0
		public static void SetSByteField(IntPtr obj, IntPtr fieldID, sbyte val)
		{
			try
			{
				AndroidJNI.SetSByteField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000073E4 File Offset: 0x000055E4
		public static void SetBooleanField(IntPtr obj, IntPtr fieldID, bool val)
		{
			try
			{
				AndroidJNI.SetBooleanField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00007418 File Offset: 0x00005618
		public static void SetIntField(IntPtr obj, IntPtr fieldID, int val)
		{
			try
			{
				AndroidJNI.SetIntField(obj, fieldID, val);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000744C File Offset: 0x0000564C
		public static IntPtr GetObjectField(IntPtr obj, IntPtr fieldID)
		{
			IntPtr objectField;
			try
			{
				objectField = AndroidJNI.GetObjectField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return objectField;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00007480 File Offset: 0x00005680
		public static string GetStringField(IntPtr obj, IntPtr fieldID)
		{
			string stringField;
			try
			{
				stringField = AndroidJNI.GetStringField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return stringField;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000074B4 File Offset: 0x000056B4
		public static char GetCharField(IntPtr obj, IntPtr fieldID)
		{
			char charField;
			try
			{
				charField = AndroidJNI.GetCharField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return charField;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000074E8 File Offset: 0x000056E8
		public static double GetDoubleField(IntPtr obj, IntPtr fieldID)
		{
			double doubleField;
			try
			{
				doubleField = AndroidJNI.GetDoubleField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return doubleField;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000751C File Offset: 0x0000571C
		public static float GetFloatField(IntPtr obj, IntPtr fieldID)
		{
			float floatField;
			try
			{
				floatField = AndroidJNI.GetFloatField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return floatField;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00007550 File Offset: 0x00005750
		public static long GetLongField(IntPtr obj, IntPtr fieldID)
		{
			long longField;
			try
			{
				longField = AndroidJNI.GetLongField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return longField;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00007584 File Offset: 0x00005784
		public static short GetShortField(IntPtr obj, IntPtr fieldID)
		{
			short shortField;
			try
			{
				shortField = AndroidJNI.GetShortField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return shortField;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000075B8 File Offset: 0x000057B8
		public static sbyte GetSByteField(IntPtr obj, IntPtr fieldID)
		{
			sbyte sbyteField;
			try
			{
				sbyteField = AndroidJNI.GetSByteField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return sbyteField;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000075EC File Offset: 0x000057EC
		public static bool GetBooleanField(IntPtr obj, IntPtr fieldID)
		{
			bool booleanField;
			try
			{
				booleanField = AndroidJNI.GetBooleanField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return booleanField;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00007620 File Offset: 0x00005820
		public static int GetIntField(IntPtr obj, IntPtr fieldID)
		{
			int intField;
			try
			{
				intField = AndroidJNI.GetIntField(obj, fieldID);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return intField;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00007654 File Offset: 0x00005854
		public static void CallVoidMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			try
			{
				AndroidJNI.CallVoidMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00007688 File Offset: 0x00005888
		public static IntPtr CallObjectMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.CallObjectMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000076BC File Offset: 0x000058BC
		public static string CallStringMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			string result;
			try
			{
				result = AndroidJNI.CallStringMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000076F0 File Offset: 0x000058F0
		public static char CallCharMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			char result;
			try
			{
				result = AndroidJNI.CallCharMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00007724 File Offset: 0x00005924
		public static double CallDoubleMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			double result;
			try
			{
				result = AndroidJNI.CallDoubleMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00007758 File Offset: 0x00005958
		public static float CallFloatMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			float result;
			try
			{
				result = AndroidJNI.CallFloatMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000778C File Offset: 0x0000598C
		public static long CallLongMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			long result;
			try
			{
				result = AndroidJNI.CallLongMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000077C0 File Offset: 0x000059C0
		public static short CallShortMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			short result;
			try
			{
				result = AndroidJNI.CallShortMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000077F4 File Offset: 0x000059F4
		public static sbyte CallSByteMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			sbyte result;
			try
			{
				result = AndroidJNI.CallSByteMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00007828 File Offset: 0x00005A28
		public static bool CallBooleanMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			bool result;
			try
			{
				result = AndroidJNI.CallBooleanMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000785C File Offset: 0x00005A5C
		public static int CallIntMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			int result;
			try
			{
				result = AndroidJNI.CallIntMethod(obj, methodID, args);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00007890 File Offset: 0x00005A90
		public static IntPtr[] FromObjectArray(IntPtr array)
		{
			IntPtr[] result;
			try
			{
				result = AndroidJNI.FromObjectArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000078C4 File Offset: 0x00005AC4
		public static char[] FromCharArray(IntPtr array)
		{
			char[] result;
			try
			{
				result = AndroidJNI.FromCharArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000078F8 File Offset: 0x00005AF8
		public static double[] FromDoubleArray(IntPtr array)
		{
			double[] result;
			try
			{
				result = AndroidJNI.FromDoubleArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000792C File Offset: 0x00005B2C
		public static float[] FromFloatArray(IntPtr array)
		{
			float[] result;
			try
			{
				result = AndroidJNI.FromFloatArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00007960 File Offset: 0x00005B60
		public static long[] FromLongArray(IntPtr array)
		{
			long[] result;
			try
			{
				result = AndroidJNI.FromLongArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007994 File Offset: 0x00005B94
		public static short[] FromShortArray(IntPtr array)
		{
			short[] result;
			try
			{
				result = AndroidJNI.FromShortArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000079C8 File Offset: 0x00005BC8
		public static byte[] FromByteArray(IntPtr array)
		{
			byte[] result;
			try
			{
				result = AndroidJNI.FromByteArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000079FC File Offset: 0x00005BFC
		public static sbyte[] FromSByteArray(IntPtr array)
		{
			sbyte[] result;
			try
			{
				result = AndroidJNI.FromSByteArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007A30 File Offset: 0x00005C30
		public static bool[] FromBooleanArray(IntPtr array)
		{
			bool[] result;
			try
			{
				result = AndroidJNI.FromBooleanArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007A64 File Offset: 0x00005C64
		public static int[] FromIntArray(IntPtr array)
		{
			int[] result;
			try
			{
				result = AndroidJNI.FromIntArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007A98 File Offset: 0x00005C98
		public static IntPtr ToObjectArray(IntPtr[] array)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.ToObjectArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007ACC File Offset: 0x00005CCC
		public static IntPtr ToObjectArray(IntPtr[] array, IntPtr type)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.ToObjectArray(array, type);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007B00 File Offset: 0x00005D00
		public static IntPtr ToCharArray(char[] array)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.ToCharArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007B34 File Offset: 0x00005D34
		public static IntPtr ToDoubleArray(double[] array)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.ToDoubleArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007B68 File Offset: 0x00005D68
		public static IntPtr ToFloatArray(float[] array)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.ToFloatArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007B9C File Offset: 0x00005D9C
		public static IntPtr ToLongArray(long[] array)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.ToLongArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007BD0 File Offset: 0x00005DD0
		public static IntPtr ToShortArray(short[] array)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.ToShortArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007C04 File Offset: 0x00005E04
		public static IntPtr ToByteArray(byte[] array)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.ToByteArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007C38 File Offset: 0x00005E38
		public static IntPtr ToSByteArray(sbyte[] array)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.ToSByteArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00007C6C File Offset: 0x00005E6C
		public static IntPtr ToBooleanArray(bool[] array)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.ToBooleanArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007CA0 File Offset: 0x00005EA0
		public static IntPtr ToIntArray(int[] array)
		{
			IntPtr result;
			try
			{
				result = AndroidJNI.ToIntArray(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return result;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007CD4 File Offset: 0x00005ED4
		public static IntPtr GetObjectArrayElement(IntPtr array, int index)
		{
			IntPtr objectArrayElement;
			try
			{
				objectArrayElement = AndroidJNI.GetObjectArrayElement(array, index);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return objectArrayElement;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007D08 File Offset: 0x00005F08
		public static int GetArrayLength(IntPtr array)
		{
			int arrayLength;
			try
			{
				arrayLength = AndroidJNI.GetArrayLength(array);
			}
			finally
			{
				AndroidJNISafe.CheckException();
			}
			return arrayLength;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000049C2 File Offset: 0x00002BC2
		public AndroidJNISafe()
		{
		}
	}
}
