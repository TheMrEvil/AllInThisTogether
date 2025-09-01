using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200000D RID: 13
	[StaticAccessor("AndroidJNIBindingsHelpers", StaticAccessorType.DoubleColon)]
	[NativeConditional("PLATFORM_ANDROID")]
	[NativeHeader("Modules/AndroidJNI/Public/AndroidJNIBindingsHelpers.h")]
	public static class AndroidJNI
	{
		// Token: 0x0600007F RID: 127
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int AttachCurrentThread();

		// Token: 0x06000080 RID: 128
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int DetachCurrentThread();

		// Token: 0x06000081 RID: 129
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetVersion();

		// Token: 0x06000082 RID: 130
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr FindClass(string name);

		// Token: 0x06000083 RID: 131
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr FromReflectedMethod(IntPtr refMethod);

		// Token: 0x06000084 RID: 132
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr FromReflectedField(IntPtr refField);

		// Token: 0x06000085 RID: 133
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToReflectedMethod(IntPtr clazz, IntPtr methodID, bool isStatic);

		// Token: 0x06000086 RID: 134
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToReflectedField(IntPtr clazz, IntPtr fieldID, bool isStatic);

		// Token: 0x06000087 RID: 135
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetSuperclass(IntPtr clazz);

		// Token: 0x06000088 RID: 136
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsAssignableFrom(IntPtr clazz1, IntPtr clazz2);

		// Token: 0x06000089 RID: 137
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Throw(IntPtr obj);

		// Token: 0x0600008A RID: 138
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int ThrowNew(IntPtr clazz, string message);

		// Token: 0x0600008B RID: 139
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ExceptionOccurred();

		// Token: 0x0600008C RID: 140
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ExceptionDescribe();

		// Token: 0x0600008D RID: 141
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ExceptionClear();

		// Token: 0x0600008E RID: 142
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FatalError(string message);

		// Token: 0x0600008F RID: 143
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int PushLocalFrame(int capacity);

		// Token: 0x06000090 RID: 144
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr PopLocalFrame(IntPtr ptr);

		// Token: 0x06000091 RID: 145
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewGlobalRef(IntPtr obj);

		// Token: 0x06000092 RID: 146
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteGlobalRef(IntPtr obj);

		// Token: 0x06000093 RID: 147
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewWeakGlobalRef(IntPtr obj);

		// Token: 0x06000094 RID: 148
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteWeakGlobalRef(IntPtr obj);

		// Token: 0x06000095 RID: 149
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewLocalRef(IntPtr obj);

		// Token: 0x06000096 RID: 150
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DeleteLocalRef(IntPtr obj);

		// Token: 0x06000097 RID: 151
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsSameObject(IntPtr obj1, IntPtr obj2);

		// Token: 0x06000098 RID: 152
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int EnsureLocalCapacity(int capacity);

		// Token: 0x06000099 RID: 153
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr AllocObject(IntPtr clazz);

		// Token: 0x0600009A RID: 154
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewObject(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x0600009B RID: 155
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetObjectClass(IntPtr obj);

		// Token: 0x0600009C RID: 156
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsInstanceOf(IntPtr obj, IntPtr clazz);

		// Token: 0x0600009D RID: 157
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetMethodID(IntPtr clazz, string name, string sig);

		// Token: 0x0600009E RID: 158
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetFieldID(IntPtr clazz, string name, string sig);

		// Token: 0x0600009F RID: 159
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetStaticMethodID(IntPtr clazz, string name, string sig);

		// Token: 0x060000A0 RID: 160
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetStaticFieldID(IntPtr clazz, string name, string sig);

		// Token: 0x060000A1 RID: 161 RVA: 0x0000670C File Offset: 0x0000490C
		public static IntPtr NewString(string chars)
		{
			return AndroidJNI.NewStringFromStr(chars);
		}

		// Token: 0x060000A2 RID: 162
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr NewStringFromStr(string chars);

		// Token: 0x060000A3 RID: 163
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewString(char[] chars);

		// Token: 0x060000A4 RID: 164
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewStringUTF(string bytes);

		// Token: 0x060000A5 RID: 165
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetStringChars(IntPtr str);

		// Token: 0x060000A6 RID: 166
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetStringLength(IntPtr str);

		// Token: 0x060000A7 RID: 167
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetStringUTFLength(IntPtr str);

		// Token: 0x060000A8 RID: 168
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetStringUTFChars(IntPtr str);

		// Token: 0x060000A9 RID: 169
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string CallStringMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x060000AA RID: 170
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr CallObjectMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x060000AB RID: 171
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int CallIntMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x060000AC RID: 172
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CallBooleanMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x060000AD RID: 173
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short CallShortMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x060000AE RID: 174 RVA: 0x00006724 File Offset: 0x00004924
		[Obsolete("AndroidJNI.CallByteMethod is obsolete. Use AndroidJNI.CallSByteMethod method instead")]
		public static byte CallByteMethod(IntPtr obj, IntPtr methodID, jvalue[] args)
		{
			return (byte)AndroidJNI.CallSByteMethod(obj, methodID, args);
		}

		// Token: 0x060000AF RID: 175
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern sbyte CallSByteMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x060000B0 RID: 176
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char CallCharMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x060000B1 RID: 177
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float CallFloatMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x060000B2 RID: 178
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double CallDoubleMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x060000B3 RID: 179
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long CallLongMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x060000B4 RID: 180
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CallVoidMethod(IntPtr obj, IntPtr methodID, jvalue[] args);

		// Token: 0x060000B5 RID: 181
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetStringField(IntPtr obj, IntPtr fieldID);

		// Token: 0x060000B6 RID: 182
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetObjectField(IntPtr obj, IntPtr fieldID);

		// Token: 0x060000B7 RID: 183
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetBooleanField(IntPtr obj, IntPtr fieldID);

		// Token: 0x060000B8 RID: 184 RVA: 0x00006740 File Offset: 0x00004940
		[Obsolete("AndroidJNI.GetByteField is obsolete. Use AndroidJNI.GetSByteField method instead")]
		public static byte GetByteField(IntPtr obj, IntPtr fieldID)
		{
			return (byte)AndroidJNI.GetSByteField(obj, fieldID);
		}

		// Token: 0x060000B9 RID: 185
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern sbyte GetSByteField(IntPtr obj, IntPtr fieldID);

		// Token: 0x060000BA RID: 186
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char GetCharField(IntPtr obj, IntPtr fieldID);

		// Token: 0x060000BB RID: 187
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short GetShortField(IntPtr obj, IntPtr fieldID);

		// Token: 0x060000BC RID: 188
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetIntField(IntPtr obj, IntPtr fieldID);

		// Token: 0x060000BD RID: 189
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetLongField(IntPtr obj, IntPtr fieldID);

		// Token: 0x060000BE RID: 190
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetFloatField(IntPtr obj, IntPtr fieldID);

		// Token: 0x060000BF RID: 191
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double GetDoubleField(IntPtr obj, IntPtr fieldID);

		// Token: 0x060000C0 RID: 192
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStringField(IntPtr obj, IntPtr fieldID, string val);

		// Token: 0x060000C1 RID: 193
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetObjectField(IntPtr obj, IntPtr fieldID, IntPtr val);

		// Token: 0x060000C2 RID: 194
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetBooleanField(IntPtr obj, IntPtr fieldID, bool val);

		// Token: 0x060000C3 RID: 195 RVA: 0x0000675A File Offset: 0x0000495A
		[Obsolete("AndroidJNI.SetByteField is obsolete. Use AndroidJNI.SetSByteField method instead")]
		public static void SetByteField(IntPtr obj, IntPtr fieldID, byte val)
		{
			AndroidJNI.SetSByteField(obj, fieldID, (sbyte)val);
		}

		// Token: 0x060000C4 RID: 196
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetSByteField(IntPtr obj, IntPtr fieldID, sbyte val);

		// Token: 0x060000C5 RID: 197
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCharField(IntPtr obj, IntPtr fieldID, char val);

		// Token: 0x060000C6 RID: 198
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetShortField(IntPtr obj, IntPtr fieldID, short val);

		// Token: 0x060000C7 RID: 199
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetIntField(IntPtr obj, IntPtr fieldID, int val);

		// Token: 0x060000C8 RID: 200
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLongField(IntPtr obj, IntPtr fieldID, long val);

		// Token: 0x060000C9 RID: 201
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetFloatField(IntPtr obj, IntPtr fieldID, float val);

		// Token: 0x060000CA RID: 202
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetDoubleField(IntPtr obj, IntPtr fieldID, double val);

		// Token: 0x060000CB RID: 203
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string CallStaticStringMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x060000CC RID: 204
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr CallStaticObjectMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x060000CD RID: 205
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int CallStaticIntMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x060000CE RID: 206
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CallStaticBooleanMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x060000CF RID: 207
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short CallStaticShortMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x060000D0 RID: 208 RVA: 0x00006768 File Offset: 0x00004968
		[Obsolete("AndroidJNI.CallStaticByteMethod is obsolete. Use AndroidJNI.CallStaticSByteMethod method instead")]
		public static byte CallStaticByteMethod(IntPtr clazz, IntPtr methodID, jvalue[] args)
		{
			return (byte)AndroidJNI.CallStaticSByteMethod(clazz, methodID, args);
		}

		// Token: 0x060000D1 RID: 209
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern sbyte CallStaticSByteMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x060000D2 RID: 210
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char CallStaticCharMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x060000D3 RID: 211
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float CallStaticFloatMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x060000D4 RID: 212
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double CallStaticDoubleMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x060000D5 RID: 213
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long CallStaticLongMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x060000D6 RID: 214
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CallStaticVoidMethod(IntPtr clazz, IntPtr methodID, jvalue[] args);

		// Token: 0x060000D7 RID: 215
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetStaticStringField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x060000D8 RID: 216
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetStaticObjectField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x060000D9 RID: 217
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetStaticBooleanField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x060000DA RID: 218 RVA: 0x00006784 File Offset: 0x00004984
		[Obsolete("AndroidJNI.GetStaticByteField is obsolete. Use AndroidJNI.GetStaticSByteField method instead")]
		public static byte GetStaticByteField(IntPtr clazz, IntPtr fieldID)
		{
			return (byte)AndroidJNI.GetStaticSByteField(clazz, fieldID);
		}

		// Token: 0x060000DB RID: 219
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern sbyte GetStaticSByteField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x060000DC RID: 220
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char GetStaticCharField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x060000DD RID: 221
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short GetStaticShortField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x060000DE RID: 222
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetStaticIntField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x060000DF RID: 223
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetStaticLongField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x060000E0 RID: 224
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetStaticFloatField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x060000E1 RID: 225
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double GetStaticDoubleField(IntPtr clazz, IntPtr fieldID);

		// Token: 0x060000E2 RID: 226
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticStringField(IntPtr clazz, IntPtr fieldID, string val);

		// Token: 0x060000E3 RID: 227
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticObjectField(IntPtr clazz, IntPtr fieldID, IntPtr val);

		// Token: 0x060000E4 RID: 228
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticBooleanField(IntPtr clazz, IntPtr fieldID, bool val);

		// Token: 0x060000E5 RID: 229 RVA: 0x0000679E File Offset: 0x0000499E
		[Obsolete("AndroidJNI.SetStaticByteField is obsolete. Use AndroidJNI.SetStaticSByteField method instead")]
		public static void SetStaticByteField(IntPtr clazz, IntPtr fieldID, byte val)
		{
			AndroidJNI.SetStaticSByteField(clazz, fieldID, (sbyte)val);
		}

		// Token: 0x060000E6 RID: 230
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticSByteField(IntPtr clazz, IntPtr fieldID, sbyte val);

		// Token: 0x060000E7 RID: 231
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticCharField(IntPtr clazz, IntPtr fieldID, char val);

		// Token: 0x060000E8 RID: 232
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticShortField(IntPtr clazz, IntPtr fieldID, short val);

		// Token: 0x060000E9 RID: 233
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticIntField(IntPtr clazz, IntPtr fieldID, int val);

		// Token: 0x060000EA RID: 234
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticLongField(IntPtr clazz, IntPtr fieldID, long val);

		// Token: 0x060000EB RID: 235
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticFloatField(IntPtr clazz, IntPtr fieldID, float val);

		// Token: 0x060000EC RID: 236
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStaticDoubleField(IntPtr clazz, IntPtr fieldID, double val);

		// Token: 0x060000ED RID: 237
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToBooleanArray(bool[] array);

		// Token: 0x060000EE RID: 238
		[ThreadSafe]
		[Obsolete("AndroidJNI.ToByteArray is obsolete. Use AndroidJNI.ToSByteArray method instead")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToByteArray(byte[] array);

		// Token: 0x060000EF RID: 239
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToSByteArray([Unmarshalled] sbyte[] array);

		// Token: 0x060000F0 RID: 240
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToCharArray([Unmarshalled] char[] array);

		// Token: 0x060000F1 RID: 241
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToShortArray([Unmarshalled] short[] array);

		// Token: 0x060000F2 RID: 242
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToIntArray([Unmarshalled] int[] array);

		// Token: 0x060000F3 RID: 243
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToLongArray([Unmarshalled] long[] array);

		// Token: 0x060000F4 RID: 244
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToFloatArray([Unmarshalled] float[] array);

		// Token: 0x060000F5 RID: 245
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToDoubleArray([Unmarshalled] double[] array);

		// Token: 0x060000F6 RID: 246
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ToObjectArray(IntPtr[] array, IntPtr arrayClass);

		// Token: 0x060000F7 RID: 247 RVA: 0x000067AC File Offset: 0x000049AC
		public static IntPtr ToObjectArray(IntPtr[] array)
		{
			return AndroidJNI.ToObjectArray(array, IntPtr.Zero);
		}

		// Token: 0x060000F8 RID: 248
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool[] FromBooleanArray(IntPtr array);

		// Token: 0x060000F9 RID: 249
		[ThreadSafe]
		[Obsolete("AndroidJNI.FromByteArray is obsolete. Use AndroidJNI.FromSByteArray method instead")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte[] FromByteArray(IntPtr array);

		// Token: 0x060000FA RID: 250
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern sbyte[] FromSByteArray(IntPtr array);

		// Token: 0x060000FB RID: 251
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char[] FromCharArray(IntPtr array);

		// Token: 0x060000FC RID: 252
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short[] FromShortArray(IntPtr array);

		// Token: 0x060000FD RID: 253
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int[] FromIntArray(IntPtr array);

		// Token: 0x060000FE RID: 254
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long[] FromLongArray(IntPtr array);

		// Token: 0x060000FF RID: 255
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float[] FromFloatArray(IntPtr array);

		// Token: 0x06000100 RID: 256
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double[] FromDoubleArray(IntPtr array);

		// Token: 0x06000101 RID: 257
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr[] FromObjectArray(IntPtr array);

		// Token: 0x06000102 RID: 258
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetArrayLength(IntPtr array);

		// Token: 0x06000103 RID: 259
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewBooleanArray(int size);

		// Token: 0x06000104 RID: 260 RVA: 0x000067CC File Offset: 0x000049CC
		[Obsolete("AndroidJNI.NewByteArray is obsolete. Use AndroidJNI.NewSByteArray method instead")]
		public static IntPtr NewByteArray(int size)
		{
			return AndroidJNI.NewSByteArray(size);
		}

		// Token: 0x06000105 RID: 261
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewSByteArray(int size);

		// Token: 0x06000106 RID: 262
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewCharArray(int size);

		// Token: 0x06000107 RID: 263
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewShortArray(int size);

		// Token: 0x06000108 RID: 264
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewIntArray(int size);

		// Token: 0x06000109 RID: 265
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewLongArray(int size);

		// Token: 0x0600010A RID: 266
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewFloatArray(int size);

		// Token: 0x0600010B RID: 267
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewDoubleArray(int size);

		// Token: 0x0600010C RID: 268
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr NewObjectArray(int size, IntPtr clazz, IntPtr obj);

		// Token: 0x0600010D RID: 269
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetBooleanArrayElement(IntPtr array, int index);

		// Token: 0x0600010E RID: 270 RVA: 0x000067E4 File Offset: 0x000049E4
		[Obsolete("AndroidJNI.GetByteArrayElement is obsolete. Use AndroidJNI.GetSByteArrayElement method instead")]
		public static byte GetByteArrayElement(IntPtr array, int index)
		{
			return (byte)AndroidJNI.GetSByteArrayElement(array, index);
		}

		// Token: 0x0600010F RID: 271
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern sbyte GetSByteArrayElement(IntPtr array, int index);

		// Token: 0x06000110 RID: 272
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char GetCharArrayElement(IntPtr array, int index);

		// Token: 0x06000111 RID: 273
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short GetShortArrayElement(IntPtr array, int index);

		// Token: 0x06000112 RID: 274
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetIntArrayElement(IntPtr array, int index);

		// Token: 0x06000113 RID: 275
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetLongArrayElement(IntPtr array, int index);

		// Token: 0x06000114 RID: 276
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetFloatArrayElement(IntPtr array, int index);

		// Token: 0x06000115 RID: 277
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double GetDoubleArrayElement(IntPtr array, int index);

		// Token: 0x06000116 RID: 278
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetObjectArrayElement(IntPtr array, int index);

		// Token: 0x06000117 RID: 279 RVA: 0x000067FE File Offset: 0x000049FE
		[Obsolete("AndroidJNI.SetBooleanArrayElement(IntPtr, int, byte) is obsolete. Use AndroidJNI.SetBooleanArrayElement(IntPtr, int, bool) method instead")]
		public static void SetBooleanArrayElement(IntPtr array, int index, byte val)
		{
			AndroidJNI.SetBooleanArrayElement(array, index, val > 0);
		}

		// Token: 0x06000118 RID: 280
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetBooleanArrayElement(IntPtr array, int index, bool val);

		// Token: 0x06000119 RID: 281 RVA: 0x0000680D File Offset: 0x00004A0D
		[Obsolete("AndroidJNI.SetByteArrayElement is obsolete. Use AndroidJNI.SetSByteArrayElement method instead")]
		public static void SetByteArrayElement(IntPtr array, int index, sbyte val)
		{
			AndroidJNI.SetSByteArrayElement(array, index, val);
		}

		// Token: 0x0600011A RID: 282
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetSByteArrayElement(IntPtr array, int index, sbyte val);

		// Token: 0x0600011B RID: 283
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCharArrayElement(IntPtr array, int index, char val);

		// Token: 0x0600011C RID: 284
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetShortArrayElement(IntPtr array, int index, short val);

		// Token: 0x0600011D RID: 285
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetIntArrayElement(IntPtr array, int index, int val);

		// Token: 0x0600011E RID: 286
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLongArrayElement(IntPtr array, int index, long val);

		// Token: 0x0600011F RID: 287
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetFloatArrayElement(IntPtr array, int index, float val);

		// Token: 0x06000120 RID: 288
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetDoubleArrayElement(IntPtr array, int index, double val);

		// Token: 0x06000121 RID: 289
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetObjectArrayElement(IntPtr array, int index, IntPtr obj);
	}
}
