using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using ES3Internal;
using ES3Types;
using UnityEngine;

// Token: 0x02000016 RID: 22
public abstract class ES3Writer : IDisposable
{
	// Token: 0x0600019E RID: 414
	internal abstract void WriteNull();

	// Token: 0x0600019F RID: 415 RVA: 0x0000663C File Offset: 0x0000483C
	internal virtual void StartWriteFile()
	{
		this.serializationDepth++;
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x0000664C File Offset: 0x0000484C
	internal virtual void EndWriteFile()
	{
		this.serializationDepth--;
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x0000665C File Offset: 0x0000485C
	internal virtual void StartWriteObject(string name)
	{
		this.serializationDepth++;
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x0000666C File Offset: 0x0000486C
	internal virtual void EndWriteObject(string name)
	{
		this.serializationDepth--;
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x0000667C File Offset: 0x0000487C
	internal virtual void StartWriteProperty(string name)
	{
		if (name == null)
		{
			throw new ArgumentNullException("Key or field name cannot be NULL when saving data.");
		}
		ES3Debug.Log("<b>" + name + "</b> (writing property)", null, this.serializationDepth);
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x000066A8 File Offset: 0x000048A8
	internal virtual void EndWriteProperty(string name)
	{
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x000066AA File Offset: 0x000048AA
	internal virtual void StartWriteCollection()
	{
		this.serializationDepth++;
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x000066BA File Offset: 0x000048BA
	internal virtual void EndWriteCollection()
	{
		this.serializationDepth--;
	}

	// Token: 0x060001A7 RID: 423
	internal abstract void StartWriteCollectionItem(int index);

	// Token: 0x060001A8 RID: 424
	internal abstract void EndWriteCollectionItem(int index);

	// Token: 0x060001A9 RID: 425
	internal abstract void StartWriteDictionary();

	// Token: 0x060001AA RID: 426
	internal abstract void EndWriteDictionary();

	// Token: 0x060001AB RID: 427
	internal abstract void StartWriteDictionaryKey(int index);

	// Token: 0x060001AC RID: 428
	internal abstract void EndWriteDictionaryKey(int index);

	// Token: 0x060001AD RID: 429
	internal abstract void StartWriteDictionaryValue(int index);

	// Token: 0x060001AE RID: 430
	internal abstract void EndWriteDictionaryValue(int index);

	// Token: 0x060001AF RID: 431
	public abstract void Dispose();

	// Token: 0x060001B0 RID: 432
	internal abstract void WriteRawProperty(string name, byte[] bytes);

	// Token: 0x060001B1 RID: 433
	internal abstract void WritePrimitive(int value);

	// Token: 0x060001B2 RID: 434
	internal abstract void WritePrimitive(float value);

	// Token: 0x060001B3 RID: 435
	internal abstract void WritePrimitive(bool value);

	// Token: 0x060001B4 RID: 436
	internal abstract void WritePrimitive(decimal value);

	// Token: 0x060001B5 RID: 437
	internal abstract void WritePrimitive(double value);

	// Token: 0x060001B6 RID: 438
	internal abstract void WritePrimitive(long value);

	// Token: 0x060001B7 RID: 439
	internal abstract void WritePrimitive(ulong value);

	// Token: 0x060001B8 RID: 440
	internal abstract void WritePrimitive(uint value);

	// Token: 0x060001B9 RID: 441
	internal abstract void WritePrimitive(byte value);

	// Token: 0x060001BA RID: 442
	internal abstract void WritePrimitive(sbyte value);

	// Token: 0x060001BB RID: 443
	internal abstract void WritePrimitive(short value);

	// Token: 0x060001BC RID: 444
	internal abstract void WritePrimitive(ushort value);

	// Token: 0x060001BD RID: 445
	internal abstract void WritePrimitive(char value);

	// Token: 0x060001BE RID: 446
	internal abstract void WritePrimitive(string value);

	// Token: 0x060001BF RID: 447
	internal abstract void WritePrimitive(byte[] value);

	// Token: 0x060001C0 RID: 448 RVA: 0x000066CA File Offset: 0x000048CA
	protected ES3Writer(ES3Settings settings, bool writeHeaderAndFooter, bool overwriteKeys)
	{
		this.settings = settings;
		this.writeHeaderAndFooter = writeHeaderAndFooter;
		this.overwriteKeys = overwriteKeys;
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x00006700 File Offset: 0x00004900
	internal virtual void Write(string key, Type type, byte[] value)
	{
		this.StartWriteProperty(key);
		this.StartWriteObject(key);
		this.WriteType(type);
		this.WriteRawProperty("value", value);
		this.EndWriteObject(key);
		this.EndWriteProperty(key);
		this.MarkKeyForDeletion(key);
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x00006738 File Offset: 0x00004938
	public virtual void Write<T>(string key, object value)
	{
		if (!(typeof(T) == typeof(object)))
		{
			this.Write(typeof(T), key, value);
			return;
		}
		if (value == null)
		{
			this.Write(typeof(object), key, null);
			return;
		}
		this.Write(value.GetType(), key, value);
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x00006798 File Offset: 0x00004998
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void Write(Type type, string key, object value)
	{
		this.StartWriteProperty(key);
		this.StartWriteObject(key);
		this.WriteType(type);
		this.WriteProperty("value", value, ES3TypeMgr.GetOrCreateES3Type(type, true), this.settings.referenceMode);
		this.EndWriteObject(key);
		this.EndWriteProperty(key);
		this.MarkKeyForDeletion(key);
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x000067F0 File Offset: 0x000049F0
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void Write(object value, ES3.ReferenceMode memberReferenceMode = ES3.ReferenceMode.ByRef)
	{
		if (value == null)
		{
			this.WriteNull();
			return;
		}
		ES3Type orCreateES3Type = ES3TypeMgr.GetOrCreateES3Type(value.GetType(), true);
		this.Write(value, orCreateES3Type, memberReferenceMode);
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x00006820 File Offset: 0x00004A20
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void Write(object value, ES3Type type, ES3.ReferenceMode memberReferenceMode = ES3.ReferenceMode.ByRef)
	{
		if (value == null || (ES3Reflection.IsAssignableFrom(typeof(UnityEngine.Object), value.GetType()) && value as UnityEngine.Object == null))
		{
			this.WriteNull();
			return;
		}
		if (type == null || type.type == typeof(object))
		{
			Type type2 = value.GetType();
			type = ES3TypeMgr.GetOrCreateES3Type(type2, true);
			if (type == null)
			{
				string str = "Types of ";
				Type type3 = type2;
				throw new NotSupportedException(str + ((type3 != null) ? type3.ToString() : null) + " are not supported. Please see the Supported Types guide for more information: https://docs.moodkie.com/easy-save-3/es3-supported-types/");
			}
			if (!type.isCollection && !type.isDictionary)
			{
				ES3Writer.TryOnBeforeSerialize(value);
				this.StartWriteObject(null);
				this.WriteType(type2);
				type.Write(value, this);
				this.EndWriteObject(null);
				return;
			}
		}
		if (type == null)
		{
			throw new ArgumentNullException("ES3Type argument cannot be null.");
		}
		if (type.isUnsupported)
		{
			if (type.isCollection || type.isDictionary)
			{
				Type type4 = type.type;
				throw new NotSupportedException(((type4 != null) ? type4.ToString() : null) + " is not supported because it's element type is not supported. Please see the Supported Types guide for more information: https://docs.moodkie.com/easy-save-3/es3-supported-types/");
			}
			string str2 = "Types of ";
			Type type5 = type.type;
			throw new NotSupportedException(str2 + ((type5 != null) ? type5.ToString() : null) + " are not supported. Please see the Supported Types guide for more information: https://docs.moodkie.com/easy-save-3/es3-supported-types/");
		}
		else
		{
			if (type.isPrimitive || type.isEnum)
			{
				type.Write(value, this);
				return;
			}
			if (type.isCollection)
			{
				this.StartWriteCollection();
				((ES3CollectionType)type).Write(value, this, memberReferenceMode);
				this.EndWriteCollection();
				return;
			}
			if (type.isDictionary)
			{
				this.StartWriteDictionary();
				((ES3DictionaryType)type).Write(value, this, memberReferenceMode);
				this.EndWriteDictionary();
				return;
			}
			ES3Writer.TryOnBeforeSerialize(value);
			if (type.type == typeof(GameObject))
			{
				((ES3Type_GameObject)type).saveChildren = this.settings.saveChildren;
			}
			this.StartWriteObject(null);
			if (type.isES3TypeUnityObject)
			{
				((ES3UnityObjectType)type).WriteObject(value, this, memberReferenceMode);
			}
			else
			{
				type.Write(value, this);
			}
			this.EndWriteObject(null);
			return;
		}
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x00006A10 File Offset: 0x00004C10
	internal static void TryOnBeforeSerialize(object obj)
	{
		ISerializationCallbackReceiver serializationCallbackReceiver = obj as ISerializationCallbackReceiver;
		if (serializationCallbackReceiver != null)
		{
			serializationCallbackReceiver.OnBeforeSerialize();
		}
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x00006A2D File Offset: 0x00004C2D
	internal virtual void WriteRef(UnityEngine.Object obj)
	{
		this.WriteRef(obj, "_ES3Ref");
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x00006A3B File Offset: 0x00004C3B
	internal virtual void WriteRef(UnityEngine.Object obj, string propertyName)
	{
		this.WriteRef(obj, "_ES3Ref", ES3ReferenceMgrBase.Current);
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x00006A50 File Offset: 0x00004C50
	internal virtual void WriteRef(UnityEngine.Object obj, string propertyName, ES3ReferenceMgrBase refMgr)
	{
		if (refMgr == null)
		{
			throw new InvalidOperationException(string.Format("An Easy Save 3 Manager is required to save references. To add one to your scene, exit playmode and go to Tools > Easy Save 3 > Add Manager to Scene. Object being saved by reference is {0} with name {1}.", obj.GetType(), obj.name));
		}
		long num = refMgr.Get(obj);
		if (num == -1L)
		{
			num = refMgr.Add(obj);
		}
		this.WriteProperty(propertyName, num.ToString());
	}

	// Token: 0x060001CA RID: 458 RVA: 0x00006AA5 File Offset: 0x00004CA5
	public virtual void WriteProperty(string name, object value)
	{
		this.WriteProperty(name, value, this.settings.memberReferenceMode);
	}

	// Token: 0x060001CB RID: 459 RVA: 0x00006ABA File Offset: 0x00004CBA
	public virtual void WriteProperty(string name, object value, ES3.ReferenceMode memberReferenceMode)
	{
		if (this.SerializationDepthLimitExceeded())
		{
			return;
		}
		this.StartWriteProperty(name);
		this.Write(value, memberReferenceMode);
		this.EndWriteProperty(name);
	}

	// Token: 0x060001CC RID: 460 RVA: 0x00006ADB File Offset: 0x00004CDB
	public virtual void WriteProperty<T>(string name, object value)
	{
		this.WriteProperty(name, value, ES3TypeMgr.GetOrCreateES3Type(typeof(T), true), this.settings.memberReferenceMode);
	}

	// Token: 0x060001CD RID: 461 RVA: 0x00006B00 File Offset: 0x00004D00
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void WriteProperty(string name, object value, ES3Type type)
	{
		this.WriteProperty(name, value, type, this.settings.memberReferenceMode);
	}

	// Token: 0x060001CE RID: 462 RVA: 0x00006B16 File Offset: 0x00004D16
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void WriteProperty(string name, object value, ES3Type type, ES3.ReferenceMode memberReferenceMode)
	{
		if (this.SerializationDepthLimitExceeded())
		{
			return;
		}
		this.StartWriteProperty(name);
		this.Write(value, type, memberReferenceMode);
		this.EndWriteProperty(name);
	}

	// Token: 0x060001CF RID: 463 RVA: 0x00006B39 File Offset: 0x00004D39
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void WritePropertyByRef(string name, UnityEngine.Object value)
	{
		if (this.SerializationDepthLimitExceeded())
		{
			return;
		}
		this.StartWriteProperty(name);
		if (value == null)
		{
			this.WriteNull();
			return;
		}
		this.StartWriteObject(name);
		this.WriteRef(value);
		this.EndWriteObject(name);
		this.EndWriteProperty(name);
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x00006B78 File Offset: 0x00004D78
	public void WritePrivateProperty(string name, object objectContainingProperty)
	{
		ES3Reflection.ES3ReflectedMember es3ReflectedProperty = ES3Reflection.GetES3ReflectedProperty(objectContainingProperty.GetType(), name);
		if (es3ReflectedProperty.IsNull)
		{
			string str = "A private property named ";
			string str2 = " does not exist in the type ";
			Type type = objectContainingProperty.GetType();
			throw new MissingMemberException(str + name + str2 + ((type != null) ? type.ToString() : null));
		}
		this.WriteProperty(name, es3ReflectedProperty.GetValue(objectContainingProperty), ES3TypeMgr.GetOrCreateES3Type(es3ReflectedProperty.MemberType, true));
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x00006BE0 File Offset: 0x00004DE0
	public void WritePrivateField(string name, object objectContainingField)
	{
		ES3Reflection.ES3ReflectedMember es3ReflectedMember = ES3Reflection.GetES3ReflectedMember(objectContainingField.GetType(), name);
		if (es3ReflectedMember.IsNull)
		{
			string str = "A private field named ";
			string str2 = " does not exist in the type ";
			Type type = objectContainingField.GetType();
			throw new MissingMemberException(str + name + str2 + ((type != null) ? type.ToString() : null));
		}
		this.WriteProperty(name, es3ReflectedMember.GetValue(objectContainingField), ES3TypeMgr.GetOrCreateES3Type(es3ReflectedMember.MemberType, true));
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x00006C48 File Offset: 0x00004E48
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void WritePrivateProperty(string name, object objectContainingProperty, ES3Type type)
	{
		ES3Reflection.ES3ReflectedMember es3ReflectedProperty = ES3Reflection.GetES3ReflectedProperty(objectContainingProperty.GetType(), name);
		if (es3ReflectedProperty.IsNull)
		{
			string str = "A private property named ";
			string str2 = " does not exist in the type ";
			Type type2 = objectContainingProperty.GetType();
			throw new MissingMemberException(str + name + str2 + ((type2 != null) ? type2.ToString() : null));
		}
		this.WriteProperty(name, es3ReflectedProperty.GetValue(objectContainingProperty), type);
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x00006CA4 File Offset: 0x00004EA4
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void WritePrivateField(string name, object objectContainingField, ES3Type type)
	{
		ES3Reflection.ES3ReflectedMember es3ReflectedMember = ES3Reflection.GetES3ReflectedMember(objectContainingField.GetType(), name);
		if (es3ReflectedMember.IsNull)
		{
			string str = "A private field named ";
			string str2 = " does not exist in the type ";
			Type type2 = objectContainingField.GetType();
			throw new MissingMemberException(str + name + str2 + ((type2 != null) ? type2.ToString() : null));
		}
		this.WriteProperty(name, es3ReflectedMember.GetValue(objectContainingField), type);
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x00006D00 File Offset: 0x00004F00
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void WritePrivatePropertyByRef(string name, object objectContainingProperty)
	{
		ES3Reflection.ES3ReflectedMember es3ReflectedProperty = ES3Reflection.GetES3ReflectedProperty(objectContainingProperty.GetType(), name);
		if (es3ReflectedProperty.IsNull)
		{
			string str = "A private property named ";
			string str2 = " does not exist in the type ";
			Type type = objectContainingProperty.GetType();
			throw new MissingMemberException(str + name + str2 + ((type != null) ? type.ToString() : null));
		}
		this.WritePropertyByRef(name, (UnityEngine.Object)es3ReflectedProperty.GetValue(objectContainingProperty));
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x00006D60 File Offset: 0x00004F60
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void WritePrivateFieldByRef(string name, object objectContainingField)
	{
		ES3Reflection.ES3ReflectedMember es3ReflectedMember = ES3Reflection.GetES3ReflectedMember(objectContainingField.GetType(), name);
		if (es3ReflectedMember.IsNull)
		{
			string str = "A private field named ";
			string str2 = " does not exist in the type ";
			Type type = objectContainingField.GetType();
			throw new MissingMemberException(str + name + str2 + ((type != null) ? type.ToString() : null));
		}
		this.WritePropertyByRef(name, (UnityEngine.Object)es3ReflectedMember.GetValue(objectContainingField));
	}

	// Token: 0x060001D6 RID: 470 RVA: 0x00006DBF File Offset: 0x00004FBF
	[EditorBrowsable(EditorBrowsableState.Never)]
	public void WriteType(Type type)
	{
		this.WriteProperty("__type", ES3Reflection.GetTypeString(type));
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x00006DD2 File Offset: 0x00004FD2
	public static ES3Writer Create(string filePath, ES3Settings settings)
	{
		return ES3Writer.Create(new ES3Settings(filePath, settings));
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x00006DE0 File Offset: 0x00004FE0
	public static ES3Writer Create(ES3Settings settings)
	{
		return ES3Writer.Create(settings, true, true, false);
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x00006DEC File Offset: 0x00004FEC
	internal static ES3Writer Create(ES3Settings settings, bool writeHeaderAndFooter, bool overwriteKeys, bool append)
	{
		Stream stream = ES3Stream.CreateStream(settings, append ? ES3FileMode.Append : ES3FileMode.Write);
		if (stream == null)
		{
			return null;
		}
		return ES3Writer.Create(stream, settings, writeHeaderAndFooter, overwriteKeys);
	}

	// Token: 0x060001DA RID: 474 RVA: 0x00006E15 File Offset: 0x00005015
	internal static ES3Writer Create(Stream stream, ES3Settings settings, bool writeHeaderAndFooter, bool overwriteKeys)
	{
		if (stream.GetType() == typeof(MemoryStream))
		{
			settings = (ES3Settings)settings.Clone();
			settings.location = ES3.Location.InternalMS;
		}
		if (settings.format == ES3.Format.JSON)
		{
			return new ES3JSONWriter(stream, settings, writeHeaderAndFooter, overwriteKeys);
		}
		return null;
	}

	// Token: 0x060001DB RID: 475 RVA: 0x00006E55 File Offset: 0x00005055
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected bool SerializationDepthLimitExceeded()
	{
		if (this.serializationDepth > this.settings.serializationDepthLimit)
		{
			ES3Debug.LogWarning("Serialization depth limit of " + this.settings.serializationDepthLimit.ToString() + " has been exceeded, indicating that there may be a circular reference.\nIf this is not a circular reference, you can increase the depth by going to Window > Easy Save 3 > Settings > Advanced Settings > Serialization Depth Limit", null, 0);
			return true;
		}
		return false;
	}

	// Token: 0x060001DC RID: 476 RVA: 0x00006E93 File Offset: 0x00005093
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void MarkKeyForDeletion(string key)
	{
		this.keysToDelete.Add(key);
	}

	// Token: 0x060001DD RID: 477 RVA: 0x00006EA4 File Offset: 0x000050A4
	protected void Merge()
	{
		using (ES3Reader es3Reader = ES3Reader.Create(this.settings))
		{
			if (es3Reader != null)
			{
				this.Merge(es3Reader);
			}
		}
	}

	// Token: 0x060001DE RID: 478 RVA: 0x00006EE8 File Offset: 0x000050E8
	protected void Merge(ES3Reader reader)
	{
		foreach (object obj in reader.RawEnumerator)
		{
			KeyValuePair<string, ES3Data> keyValuePair = (KeyValuePair<string, ES3Data>)obj;
			if (!this.keysToDelete.Contains(keyValuePair.Key) || keyValuePair.Value.type == null)
			{
				this.Write(keyValuePair.Key, keyValuePair.Value.type.type, keyValuePair.Value.bytes);
			}
		}
	}

	// Token: 0x060001DF RID: 479 RVA: 0x00006F88 File Offset: 0x00005188
	public virtual void Save()
	{
		this.Save(this.overwriteKeys);
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x00006F96 File Offset: 0x00005196
	public virtual void Save(bool overwriteKeys)
	{
		if (overwriteKeys)
		{
			this.Merge();
		}
		this.EndWriteFile();
		this.Dispose();
		if (this.settings.location == ES3.Location.File || this.settings.location == ES3.Location.PlayerPrefs)
		{
			ES3IO.CommitBackup(this.settings);
		}
	}

	// Token: 0x04000055 RID: 85
	public ES3Settings settings;

	// Token: 0x04000056 RID: 86
	protected HashSet<string> keysToDelete = new HashSet<string>();

	// Token: 0x04000057 RID: 87
	internal bool writeHeaderAndFooter = true;

	// Token: 0x04000058 RID: 88
	internal bool overwriteKeys = true;

	// Token: 0x04000059 RID: 89
	protected int serializationDepth;
}
