using System;
using System.Collections;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200001E RID: 30
	[Preserve]
	public class ES3ConcurrentDictionaryType : ES3Type
	{
		// Token: 0x06000206 RID: 518 RVA: 0x000079BC File Offset: 0x00005BBC
		public ES3ConcurrentDictionaryType(Type type) : base(type)
		{
			Type[] elementTypes = ES3Reflection.GetElementTypes(type);
			this.keyType = ES3TypeMgr.GetOrCreateES3Type(elementTypes[0], false);
			this.valueType = ES3TypeMgr.GetOrCreateES3Type(elementTypes[1], false);
			if (this.keyType == null || this.valueType == null)
			{
				this.isUnsupported = true;
			}
			this.isDictionary = true;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00007A13 File Offset: 0x00005C13
		public ES3ConcurrentDictionaryType(Type type, ES3Type keyType, ES3Type valueType) : base(type)
		{
			this.keyType = keyType;
			this.valueType = valueType;
			if (keyType == null || valueType == null)
			{
				this.isUnsupported = true;
			}
			this.isDictionary = true;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00007A3E File Offset: 0x00005C3E
		public override void Write(object obj, ES3Writer writer)
		{
			this.Write(obj, writer, writer.settings.memberReferenceMode);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00007A54 File Offset: 0x00005C54
		public void Write(object obj, ES3Writer writer, ES3.ReferenceMode memberReferenceMode)
		{
			IDictionary dictionary = (IDictionary)obj;
			int num = 0;
			foreach (object obj2 in dictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
				writer.StartWriteDictionaryKey(num);
				writer.Write(dictionaryEntry.Key, this.keyType, memberReferenceMode);
				writer.EndWriteDictionaryKey(num);
				writer.StartWriteDictionaryValue(num);
				writer.Write(dictionaryEntry.Value, this.valueType, memberReferenceMode);
				writer.EndWriteDictionaryValue(num);
				num++;
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00007AF0 File Offset: 0x00005CF0
		public override object Read<T>(ES3Reader reader)
		{
			return this.Read(reader);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007AF9 File Offset: 0x00005CF9
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			this.ReadInto(reader, obj);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007B04 File Offset: 0x00005D04
		public object Read(ES3Reader reader)
		{
			if (reader.StartReadDictionary())
			{
				return null;
			}
			IDictionary dictionary = (IDictionary)ES3Reflection.CreateInstance(this.type);
			while (reader.StartReadDictionaryKey())
			{
				object key = reader.Read<object>(this.keyType);
				reader.EndReadDictionaryKey();
				reader.StartReadDictionaryValue();
				object value = reader.Read<object>(this.valueType);
				dictionary.Add(key, value);
				if (reader.EndReadDictionaryValue())
				{
					reader.EndReadDictionary();
					return dictionary;
				}
			}
			return dictionary;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00007B74 File Offset: 0x00005D74
		public void ReadInto(ES3Reader reader, object obj)
		{
			if (reader.StartReadDictionary())
			{
				throw new NullReferenceException("The Dictionary we are trying to load is stored as null, which is not allowed when using ReadInto methods.");
			}
			IDictionary dictionary = (IDictionary)obj;
			while (reader.StartReadDictionaryKey())
			{
				object obj2 = reader.Read<object>(this.keyType);
				if (!dictionary.Contains(obj2))
				{
					throw new KeyNotFoundException("The key \"" + ((obj2 != null) ? obj2.ToString() : null) + "\" in the Dictionary we are loading does not exist in the Dictionary we are loading into");
				}
				object obj3 = dictionary[obj2];
				reader.EndReadDictionaryKey();
				reader.StartReadDictionaryValue();
				reader.ReadInto<object>(obj3, this.valueType);
				if (reader.EndReadDictionaryValue())
				{
					reader.EndReadDictionary();
					return;
				}
			}
		}

		// Token: 0x0400005D RID: 93
		public ES3Type keyType;

		// Token: 0x0400005E RID: 94
		public ES3Type valueType;

		// Token: 0x0400005F RID: 95
		protected ES3Reflection.ES3ReflectedMethod readMethod;

		// Token: 0x04000060 RID: 96
		protected ES3Reflection.ES3ReflectedMethod readIntoMethod;
	}
}
