using System;
using System.Collections;
using System.Collections.Generic;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200001F RID: 31
	[Preserve]
	public class ES3DictionaryType : ES3Type
	{
		// Token: 0x0600020E RID: 526 RVA: 0x00007C0C File Offset: 0x00005E0C
		public ES3DictionaryType(Type type) : base(type)
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

		// Token: 0x0600020F RID: 527 RVA: 0x00007C63 File Offset: 0x00005E63
		public ES3DictionaryType(Type type, ES3Type keyType, ES3Type valueType) : base(type)
		{
			this.keyType = keyType;
			this.valueType = valueType;
			if (keyType == null || valueType == null)
			{
				this.isUnsupported = true;
			}
			this.isDictionary = true;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00007C8E File Offset: 0x00005E8E
		public override void Write(object obj, ES3Writer writer)
		{
			this.Write(obj, writer, writer.settings.memberReferenceMode);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007CA4 File Offset: 0x00005EA4
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

		// Token: 0x06000212 RID: 530 RVA: 0x00007D40 File Offset: 0x00005F40
		public override object Read<T>(ES3Reader reader)
		{
			return this.Read(reader);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00007D49 File Offset: 0x00005F49
		public override void ReadInto<T>(ES3Reader reader, object obj)
		{
			this.ReadInto(reader, obj);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007D54 File Offset: 0x00005F54
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

		// Token: 0x06000215 RID: 533 RVA: 0x00007DC4 File Offset: 0x00005FC4
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

		// Token: 0x04000061 RID: 97
		public ES3Type keyType;

		// Token: 0x04000062 RID: 98
		public ES3Type valueType;

		// Token: 0x04000063 RID: 99
		protected ES3Reflection.ES3ReflectedMethod readMethod;

		// Token: 0x04000064 RID: 100
		protected ES3Reflection.ES3ReflectedMethod readIntoMethod;
	}
}
