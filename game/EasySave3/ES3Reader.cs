using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using ES3Internal;
using ES3Types;
using UnityEngine;

// Token: 0x0200000E RID: 14
public abstract class ES3Reader : IDisposable
{
	// Token: 0x06000103 RID: 259
	internal abstract int Read_int();

	// Token: 0x06000104 RID: 260
	internal abstract float Read_float();

	// Token: 0x06000105 RID: 261
	internal abstract bool Read_bool();

	// Token: 0x06000106 RID: 262
	internal abstract char Read_char();

	// Token: 0x06000107 RID: 263
	internal abstract decimal Read_decimal();

	// Token: 0x06000108 RID: 264
	internal abstract double Read_double();

	// Token: 0x06000109 RID: 265
	internal abstract long Read_long();

	// Token: 0x0600010A RID: 266
	internal abstract ulong Read_ulong();

	// Token: 0x0600010B RID: 267
	internal abstract byte Read_byte();

	// Token: 0x0600010C RID: 268
	internal abstract sbyte Read_sbyte();

	// Token: 0x0600010D RID: 269
	internal abstract short Read_short();

	// Token: 0x0600010E RID: 270
	internal abstract ushort Read_ushort();

	// Token: 0x0600010F RID: 271
	internal abstract uint Read_uint();

	// Token: 0x06000110 RID: 272
	internal abstract string Read_string();

	// Token: 0x06000111 RID: 273
	internal abstract byte[] Read_byteArray();

	// Token: 0x06000112 RID: 274
	internal abstract long Read_ref();

	// Token: 0x06000113 RID: 275
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract string ReadPropertyName();

	// Token: 0x06000114 RID: 276
	protected abstract Type ReadKeyPrefix(bool ignore = false);

	// Token: 0x06000115 RID: 277
	protected abstract void ReadKeySuffix();

	// Token: 0x06000116 RID: 278
	internal abstract byte[] ReadElement(bool skip = false);

	// Token: 0x06000117 RID: 279
	public abstract void Dispose();

	// Token: 0x06000118 RID: 280 RVA: 0x000052A0 File Offset: 0x000034A0
	internal virtual bool Goto(string key)
	{
		if (key == null)
		{
			throw new ArgumentNullException("Key cannot be NULL when loading data.");
		}
		string text;
		while ((text = this.ReadPropertyName()) != key)
		{
			if (text == null)
			{
				return false;
			}
			this.Skip();
		}
		return true;
	}

	// Token: 0x06000119 RID: 281 RVA: 0x000052D7 File Offset: 0x000034D7
	internal virtual bool StartReadObject()
	{
		this.serializationDepth++;
		return false;
	}

	// Token: 0x0600011A RID: 282 RVA: 0x000052E8 File Offset: 0x000034E8
	internal virtual void EndReadObject()
	{
		this.serializationDepth--;
	}

	// Token: 0x0600011B RID: 283
	internal abstract bool StartReadDictionary();

	// Token: 0x0600011C RID: 284
	internal abstract void EndReadDictionary();

	// Token: 0x0600011D RID: 285
	internal abstract bool StartReadDictionaryKey();

	// Token: 0x0600011E RID: 286
	internal abstract void EndReadDictionaryKey();

	// Token: 0x0600011F RID: 287
	internal abstract void StartReadDictionaryValue();

	// Token: 0x06000120 RID: 288
	internal abstract bool EndReadDictionaryValue();

	// Token: 0x06000121 RID: 289
	internal abstract bool StartReadCollection();

	// Token: 0x06000122 RID: 290
	internal abstract void EndReadCollection();

	// Token: 0x06000123 RID: 291
	internal abstract bool StartReadCollectionItem();

	// Token: 0x06000124 RID: 292
	internal abstract bool EndReadCollectionItem();

	// Token: 0x06000125 RID: 293 RVA: 0x000052F8 File Offset: 0x000034F8
	internal ES3Reader(ES3Settings settings, bool readHeaderAndFooter = true)
	{
		this.settings = settings;
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x06000126 RID: 294 RVA: 0x00005307 File Offset: 0x00003507
	public virtual ES3Reader.ES3ReaderPropertyEnumerator Properties
	{
		get
		{
			return new ES3Reader.ES3ReaderPropertyEnumerator(this);
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000127 RID: 295 RVA: 0x0000530F File Offset: 0x0000350F
	internal virtual ES3Reader.ES3ReaderRawEnumerator RawEnumerator
	{
		get
		{
			return new ES3Reader.ES3ReaderRawEnumerator(this);
		}
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00005317 File Offset: 0x00003517
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void Skip()
	{
		this.ReadElement(true);
	}

	// Token: 0x06000129 RID: 297 RVA: 0x00005321 File Offset: 0x00003521
	public virtual T Read<T>()
	{
		return this.Read<T>(ES3TypeMgr.GetOrCreateES3Type(typeof(T), true));
	}

	// Token: 0x0600012A RID: 298 RVA: 0x00005339 File Offset: 0x00003539
	public virtual void ReadInto<T>(object obj)
	{
		this.ReadInto<T>(obj, ES3TypeMgr.GetOrCreateES3Type(typeof(T), true));
	}

	// Token: 0x0600012B RID: 299 RVA: 0x00005352 File Offset: 0x00003552
	[EditorBrowsable(EditorBrowsableState.Never)]
	public T ReadProperty<T>()
	{
		return this.ReadProperty<T>(ES3TypeMgr.GetOrCreateES3Type(typeof(T), true));
	}

	// Token: 0x0600012C RID: 300 RVA: 0x0000536A File Offset: 0x0000356A
	[EditorBrowsable(EditorBrowsableState.Never)]
	public T ReadProperty<T>(ES3Type type)
	{
		this.ReadPropertyName();
		return this.Read<T>(type);
	}

	// Token: 0x0600012D RID: 301 RVA: 0x0000537A File Offset: 0x0000357A
	[EditorBrowsable(EditorBrowsableState.Never)]
	public long ReadRefProperty()
	{
		this.ReadPropertyName();
		return this.Read_ref();
	}

	// Token: 0x0600012E RID: 302 RVA: 0x00005389 File Offset: 0x00003589
	internal Type ReadType()
	{
		return ES3Reflection.GetType(this.Read<string>(ES3Type_string.Instance));
	}

	// Token: 0x0600012F RID: 303 RVA: 0x0000539C File Offset: 0x0000359C
	public object SetPrivateProperty(string name, object value, object objectContainingProperty)
	{
		ES3Reflection.ES3ReflectedMember es3ReflectedProperty = ES3Reflection.GetES3ReflectedProperty(objectContainingProperty.GetType(), name);
		if (es3ReflectedProperty.IsNull)
		{
			string str = "A private property named ";
			string str2 = " does not exist in the type ";
			Type type = objectContainingProperty.GetType();
			throw new MissingMemberException(str + name + str2 + ((type != null) ? type.ToString() : null));
		}
		es3ReflectedProperty.SetValue(objectContainingProperty, value);
		return objectContainingProperty;
	}

	// Token: 0x06000130 RID: 304 RVA: 0x000053F4 File Offset: 0x000035F4
	public object SetPrivateField(string name, object value, object objectContainingField)
	{
		ES3Reflection.ES3ReflectedMember es3ReflectedMember = ES3Reflection.GetES3ReflectedMember(objectContainingField.GetType(), name);
		if (es3ReflectedMember.IsNull)
		{
			string str = "A private field named ";
			string str2 = " does not exist in the type ";
			Type type = objectContainingField.GetType();
			throw new MissingMemberException(str + name + str2 + ((type != null) ? type.ToString() : null));
		}
		es3ReflectedMember.SetValue(objectContainingField, value);
		return objectContainingField;
	}

	// Token: 0x06000131 RID: 305 RVA: 0x0000544C File Offset: 0x0000364C
	public virtual T Read<T>(string key)
	{
		if (!this.Goto(key))
		{
			throw new KeyNotFoundException(string.Concat(new string[]
			{
				"Key \"",
				key,
				"\" was not found in file \"",
				this.settings.FullPath,
				"\". Use Load<T>(key, defaultValue) if you want to return a default value if the key does not exist."
			}));
		}
		Type type = this.ReadTypeFromHeader<T>();
		return this.Read<T>(ES3TypeMgr.GetOrCreateES3Type(type, true));
	}

	// Token: 0x06000132 RID: 306 RVA: 0x000054B4 File Offset: 0x000036B4
	public virtual T Read<T>(string key, T defaultValue)
	{
		if (!this.Goto(key))
		{
			return defaultValue;
		}
		Type type = this.ReadTypeFromHeader<T>();
		return this.Read<T>(ES3TypeMgr.GetOrCreateES3Type(type, true));
	}

	// Token: 0x06000133 RID: 307 RVA: 0x000054E0 File Offset: 0x000036E0
	public virtual void ReadInto<T>(string key, T obj) where T : class
	{
		if (!this.Goto(key))
		{
			throw new KeyNotFoundException(string.Concat(new string[]
			{
				"Key \"",
				key,
				"\" was not found in file \"",
				this.settings.FullPath,
				"\""
			}));
		}
		Type type = this.ReadTypeFromHeader<T>();
		this.ReadInto<T>(obj, ES3TypeMgr.GetOrCreateES3Type(type, true));
	}

	// Token: 0x06000134 RID: 308 RVA: 0x0000554B File Offset: 0x0000374B
	protected virtual void ReadObject<T>(object obj, ES3Type type)
	{
		if (this.StartReadObject())
		{
			return;
		}
		type.ReadInto<T>(this, obj);
		this.EndReadObject();
		ES3Reader.TryOnAfterDeserialize(obj);
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0000556C File Offset: 0x0000376C
	protected virtual T ReadObject<T>(ES3Type type)
	{
		if (this.StartReadObject())
		{
			return default(T);
		}
		object obj = type.Read<T>(this);
		this.EndReadObject();
		ES3Reader.TryOnAfterDeserialize(obj);
		return (T)((object)obj);
	}

	// Token: 0x06000136 RID: 310 RVA: 0x000055A4 File Offset: 0x000037A4
	internal static void TryOnAfterDeserialize(object obj)
	{
		ISerializationCallbackReceiver serializationCallbackReceiver = obj as ISerializationCallbackReceiver;
		if (serializationCallbackReceiver != null)
		{
			serializationCallbackReceiver.OnAfterDeserialize();
		}
	}

	// Token: 0x06000137 RID: 311 RVA: 0x000055C4 File Offset: 0x000037C4
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual T Read<T>(ES3Type type)
	{
		if (type == null || type.isUnsupported)
		{
			throw new NotSupportedException("Type of " + ((type != null) ? type.ToString() : null) + " is not currently supported, and could not be loaded using reflection.");
		}
		if (type.isPrimitive)
		{
			return (T)((object)type.Read<T>(this));
		}
		if (type.isCollection)
		{
			return (T)((object)((ES3CollectionType)type).Read(this));
		}
		if (type.isDictionary)
		{
			return (T)((object)((ES3DictionaryType)type).Read(this));
		}
		return this.ReadObject<T>(type);
	}

	// Token: 0x06000138 RID: 312 RVA: 0x00005650 File Offset: 0x00003850
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void ReadInto<T>(object obj, ES3Type type)
	{
		if (type == null || type.isUnsupported)
		{
			string str = "Type of ";
			Type type2 = obj.GetType();
			throw new NotSupportedException(str + ((type2 != null) ? type2.ToString() : null) + " is not currently supported, and could not be loaded using reflection.");
		}
		if (type.isCollection)
		{
			((ES3CollectionType)type).ReadInto(this, obj);
			return;
		}
		if (type.isDictionary)
		{
			((ES3DictionaryType)type).ReadInto(this, obj);
			return;
		}
		this.ReadObject<T>(obj, type);
	}

	// Token: 0x06000139 RID: 313 RVA: 0x000056C4 File Offset: 0x000038C4
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal Type ReadTypeFromHeader<T>()
	{
		if (typeof(T) == typeof(object))
		{
			return this.ReadKeyPrefix(false);
		}
		if (!this.settings.typeChecking)
		{
			this.ReadKeyPrefix(true);
			return typeof(T);
		}
		Type type = this.ReadKeyPrefix(false);
		if (type == null)
		{
			string str = "Trying to load data of type ";
			Type typeFromHandle = typeof(T);
			throw new TypeLoadException(str + ((typeFromHandle != null) ? typeFromHandle.ToString() : null) + ", but the type of data contained in file no longer exists. This may be because the type has been removed from your project or renamed.");
		}
		if (type != typeof(T))
		{
			string[] array = new string[5];
			array[0] = "Trying to load data of type ";
			int num = 1;
			Type typeFromHandle2 = typeof(T);
			array[num] = ((typeFromHandle2 != null) ? typeFromHandle2.ToString() : null);
			array[2] = ", but data contained in file is type of ";
			int num2 = 3;
			Type type2 = type;
			array[num2] = ((type2 != null) ? type2.ToString() : null);
			array[4] = ".";
			throw new InvalidOperationException(string.Concat(array));
		}
		return type;
	}

	// Token: 0x0600013A RID: 314 RVA: 0x000057B8 File Offset: 0x000039B8
	public static ES3Reader Create()
	{
		return ES3Reader.Create(new ES3Settings(null, null));
	}

	// Token: 0x0600013B RID: 315 RVA: 0x000057C6 File Offset: 0x000039C6
	public static ES3Reader Create(string filePath)
	{
		return ES3Reader.Create(new ES3Settings(filePath, null));
	}

	// Token: 0x0600013C RID: 316 RVA: 0x000057D4 File Offset: 0x000039D4
	public static ES3Reader Create(string filePath, ES3Settings settings)
	{
		return ES3Reader.Create(new ES3Settings(filePath, settings));
	}

	// Token: 0x0600013D RID: 317 RVA: 0x000057E4 File Offset: 0x000039E4
	public static ES3Reader Create(ES3Settings settings)
	{
		Stream stream = ES3Stream.CreateStream(settings, ES3FileMode.Read);
		if (stream == null)
		{
			return null;
		}
		if (settings.format == ES3.Format.JSON)
		{
			return new ES3JSONReader(stream, settings, true);
		}
		return null;
	}

	// Token: 0x0600013E RID: 318 RVA: 0x00005810 File Offset: 0x00003A10
	public static ES3Reader Create(byte[] bytes)
	{
		return ES3Reader.Create(bytes, new ES3Settings(null, null));
	}

	// Token: 0x0600013F RID: 319 RVA: 0x00005820 File Offset: 0x00003A20
	public static ES3Reader Create(byte[] bytes, ES3Settings settings)
	{
		Stream stream = ES3Stream.CreateStream(new MemoryStream(bytes), settings, ES3FileMode.Read);
		if (stream == null)
		{
			return null;
		}
		if (settings.format == ES3.Format.JSON)
		{
			return new ES3JSONReader(stream, settings, true);
		}
		return null;
	}

	// Token: 0x06000140 RID: 320 RVA: 0x00005852 File Offset: 0x00003A52
	internal static ES3Reader Create(Stream stream, ES3Settings settings)
	{
		stream = ES3Stream.CreateStream(stream, settings, ES3FileMode.Read);
		if (settings.format == ES3.Format.JSON)
		{
			return new ES3JSONReader(stream, settings, true);
		}
		return null;
	}

	// Token: 0x06000141 RID: 321 RVA: 0x00005870 File Offset: 0x00003A70
	internal static ES3Reader Create(Stream stream, ES3Settings settings, bool readHeaderAndFooter)
	{
		if (settings.format == ES3.Format.JSON)
		{
			return new ES3JSONReader(stream, settings, readHeaderAndFooter);
		}
		return null;
	}

	// Token: 0x04000026 RID: 38
	public ES3Settings settings;

	// Token: 0x04000027 RID: 39
	protected int serializationDepth;

	// Token: 0x04000028 RID: 40
	internal string overridePropertiesName;

	// Token: 0x020000F9 RID: 249
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ES3ReaderPropertyEnumerator
	{
		// Token: 0x0600055C RID: 1372 RVA: 0x0001F5A3 File Offset: 0x0001D7A3
		public ES3ReaderPropertyEnumerator(ES3Reader reader)
		{
			this.reader = reader;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0001F5B2 File Offset: 0x0001D7B2
		public IEnumerator GetEnumerator()
		{
			for (;;)
			{
				if (this.reader.overridePropertiesName != null)
				{
					string overridePropertiesName = this.reader.overridePropertiesName;
					this.reader.overridePropertiesName = null;
					yield return overridePropertiesName;
				}
				else
				{
					string text;
					if ((text = this.reader.ReadPropertyName()) == null)
					{
						break;
					}
					yield return text;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x040001B1 RID: 433
		public ES3Reader reader;

		// Token: 0x02000114 RID: 276
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__2 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060005D2 RID: 1490 RVA: 0x00020944 File Offset: 0x0001EB44
			[DebuggerHidden]
			public <GetEnumerator>d__2(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060005D3 RID: 1491 RVA: 0x00020953 File Offset: 0x0001EB53
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060005D4 RID: 1492 RVA: 0x00020958 File Offset: 0x0001EB58
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				ES3Reader.ES3ReaderPropertyEnumerator es3ReaderPropertyEnumerator = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					break;
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					break;
				default:
					return false;
				}
				if (es3ReaderPropertyEnumerator.reader.overridePropertiesName != null)
				{
					string overridePropertiesName = es3ReaderPropertyEnumerator.reader.overridePropertiesName;
					es3ReaderPropertyEnumerator.reader.overridePropertiesName = null;
					this.<>2__current = overridePropertiesName;
					this.<>1__state = 1;
					return true;
				}
				string text;
				if ((text = es3ReaderPropertyEnumerator.reader.ReadPropertyName()) == null)
				{
					return false;
				}
				this.<>2__current = text;
				this.<>1__state = 2;
				return true;
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x060005D5 RID: 1493 RVA: 0x000209F5 File Offset: 0x0001EBF5
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060005D6 RID: 1494 RVA: 0x000209FD File Offset: 0x0001EBFD
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00020A04 File Offset: 0x0001EC04
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400021B RID: 539
			private int <>1__state;

			// Token: 0x0400021C RID: 540
			private object <>2__current;

			// Token: 0x0400021D RID: 541
			public ES3Reader.ES3ReaderPropertyEnumerator <>4__this;
		}
	}

	// Token: 0x020000FA RID: 250
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class ES3ReaderRawEnumerator
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x0001F5C1 File Offset: 0x0001D7C1
		public ES3ReaderRawEnumerator(ES3Reader reader)
		{
			this.reader = reader;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001F5D0 File Offset: 0x0001D7D0
		public IEnumerator GetEnumerator()
		{
			for (;;)
			{
				string text = this.reader.ReadPropertyName();
				if (text == null)
				{
					break;
				}
				Type type = this.reader.ReadTypeFromHeader<object>();
				byte[] bytes = this.reader.ReadElement(false);
				this.reader.ReadKeySuffix();
				if (type != null)
				{
					yield return new KeyValuePair<string, ES3Data>(text, new ES3Data(type, bytes));
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x040001B2 RID: 434
		public ES3Reader reader;

		// Token: 0x02000115 RID: 277
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__2 : IEnumerator<object>, IEnumerator, IDisposable
		{
			// Token: 0x060005D8 RID: 1496 RVA: 0x00020A0C File Offset: 0x0001EC0C
			[DebuggerHidden]
			public <GetEnumerator>d__2(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x060005D9 RID: 1497 RVA: 0x00020A1B File Offset: 0x0001EC1B
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060005DA RID: 1498 RVA: 0x00020A20 File Offset: 0x0001EC20
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				ES3Reader.ES3ReaderRawEnumerator es3ReaderRawEnumerator = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
				}
				else
				{
					this.<>1__state = -1;
				}
				string text;
				Type type;
				byte[] bytes;
				for (;;)
				{
					text = es3ReaderRawEnumerator.reader.ReadPropertyName();
					if (text == null)
					{
						break;
					}
					type = es3ReaderRawEnumerator.reader.ReadTypeFromHeader<object>();
					bytes = es3ReaderRawEnumerator.reader.ReadElement(false);
					es3ReaderRawEnumerator.reader.ReadKeySuffix();
					if (type != null)
					{
						goto Block_4;
					}
				}
				return false;
				Block_4:
				this.<>2__current = new KeyValuePair<string, ES3Data>(text, new ES3Data(type, bytes));
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060005DB RID: 1499 RVA: 0x00020AB4 File Offset: 0x0001ECB4
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060005DC RID: 1500 RVA: 0x00020ABC File Offset: 0x0001ECBC
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060005DD RID: 1501 RVA: 0x00020AC3 File Offset: 0x0001ECC3
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400021E RID: 542
			private int <>1__state;

			// Token: 0x0400021F RID: 543
			private object <>2__current;

			// Token: 0x04000220 RID: 544
			public ES3Reader.ES3ReaderRawEnumerator <>4__this;
		}
	}
}
