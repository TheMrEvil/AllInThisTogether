using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using ES3Internal;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200002C RID: 44
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Preserve]
	public abstract class ES3Type
	{
		// Token: 0x0600025A RID: 602 RVA: 0x000091DC File Offset: 0x000073DC
		protected ES3Type(Type type)
		{
			ES3TypeMgr.Add(type, this);
			this.type = type;
			this.isValueType = ES3Reflection.IsValueType(type);
		}

		// Token: 0x0600025B RID: 603
		public abstract void Write(object obj, ES3Writer writer);

		// Token: 0x0600025C RID: 604
		public abstract object Read<T>(ES3Reader reader);

		// Token: 0x0600025D RID: 605 RVA: 0x000091FE File Offset: 0x000073FE
		public virtual void ReadInto<T>(ES3Reader reader, object obj)
		{
			throw new NotImplementedException("Self-assigning Read is not implemented or supported on this type.");
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000920C File Offset: 0x0000740C
		protected bool WriteUsingDerivedType(object obj, ES3Writer writer)
		{
			Type left = obj.GetType();
			if (left != this.type)
			{
				writer.WriteType(left);
				ES3TypeMgr.GetOrCreateES3Type(left, true).Write(obj, writer);
				return true;
			}
			return false;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00009246 File Offset: 0x00007446
		protected void ReadUsingDerivedType<T>(ES3Reader reader, object obj)
		{
			ES3TypeMgr.GetOrCreateES3Type(reader.ReadType(), true).ReadInto<T>(reader, obj);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000925B File Offset: 0x0000745B
		internal string ReadPropertyName(ES3Reader reader)
		{
			if (reader.overridePropertiesName != null)
			{
				string overridePropertiesName = reader.overridePropertiesName;
				reader.overridePropertiesName = null;
				return overridePropertiesName;
			}
			return reader.ReadPropertyName();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000927C File Offset: 0x0000747C
		protected void WriteProperties(object obj, ES3Writer writer)
		{
			if (this.members == null)
			{
				this.GetMembers(writer.settings.safeReflection);
			}
			for (int i = 0; i < this.members.Length; i++)
			{
				ES3Member es3Member = this.members[i];
				writer.WriteProperty(es3Member.name, es3Member.reflectedMember.GetValue(obj), ES3TypeMgr.GetOrCreateES3Type(es3Member.type, true), writer.settings.memberReferenceMode);
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000092F0 File Offset: 0x000074F0
		protected object ReadProperties(ES3Reader reader, object obj)
		{
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				ES3Member es3Member = null;
				for (int i = 0; i < this.members.Length; i++)
				{
					if (this.members[i].name == text)
					{
						es3Member = this.members[i];
						break;
					}
				}
				if (text == "_Values")
				{
					ES3Type orCreateES3Type = ES3TypeMgr.GetOrCreateES3Type(ES3Reflection.BaseType(obj.GetType()), true);
					if (orCreateES3Type.isDictionary)
					{
						IDictionary dictionary = (IDictionary)obj;
						using (IDictionaryEnumerator enumerator2 = ((IDictionary)orCreateES3Type.Read<IDictionary>(reader)).GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object obj3 = enumerator2.Current;
								DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
								dictionary[dictionaryEntry.Key] = dictionaryEntry.Value;
							}
							goto IL_2AE;
						}
					}
					if (orCreateES3Type.isCollection)
					{
						IEnumerable enumerable = (IEnumerable)orCreateES3Type.Read<IEnumerable>(reader);
						Type left = orCreateES3Type.GetType();
						if (left == typeof(ES3ListType))
						{
							using (IEnumerator enumerator3 = enumerable.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									object value = enumerator3.Current;
									((IList)obj).Add(value);
								}
								goto IL_2AE;
							}
						}
						if (left == typeof(ES3QueueType))
						{
							MethodInfo method = orCreateES3Type.type.GetMethod("Enqueue");
							using (IEnumerator enumerator3 = enumerable.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									object obj4 = enumerator3.Current;
									method.Invoke(obj, new object[]
									{
										obj4
									});
								}
								goto IL_2AE;
							}
						}
						if (left == typeof(ES3StackType))
						{
							MethodInfo method2 = orCreateES3Type.type.GetMethod("Push");
							using (IEnumerator enumerator3 = enumerable.GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									object obj5 = enumerator3.Current;
									method2.Invoke(obj, new object[]
									{
										obj5
									});
								}
								goto IL_2AE;
							}
						}
						if (left == typeof(ES3HashSetType))
						{
							MethodInfo method3 = orCreateES3Type.type.GetMethod("Add");
							foreach (object obj6 in enumerable)
							{
								method3.Invoke(obj, new object[]
								{
									obj6
								});
							}
						}
					}
				}
				IL_2AE:
				if (es3Member == null)
				{
					reader.Skip();
				}
				else
				{
					ES3Type orCreateES3Type2 = ES3TypeMgr.GetOrCreateES3Type(es3Member.type, true);
					if (ES3Reflection.IsAssignableFrom(typeof(ES3DictionaryType), orCreateES3Type2.GetType()))
					{
						es3Member.reflectedMember.SetValue(obj, ((ES3DictionaryType)orCreateES3Type2).Read(reader));
					}
					else if (ES3Reflection.IsAssignableFrom(typeof(ES3CollectionType), orCreateES3Type2.GetType()))
					{
						es3Member.reflectedMember.SetValue(obj, ((ES3CollectionType)orCreateES3Type2).Read(reader));
					}
					else
					{
						object value2 = reader.Read<object>(orCreateES3Type2);
						es3Member.reflectedMember.SetValue(obj, value2);
					}
				}
			}
			return obj;
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000096FC File Offset: 0x000078FC
		protected void GetMembers(bool safe)
		{
			this.GetMembers(safe, null);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00009708 File Offset: 0x00007908
		protected void GetMembers(bool safe, string[] memberNames)
		{
			ES3Reflection.ES3ReflectedMember[] serializableMembers = ES3Reflection.GetSerializableMembers(this.type, safe, memberNames);
			this.members = new ES3Member[serializableMembers.Length];
			for (int i = 0; i < serializableMembers.Length; i++)
			{
				this.members[i] = new ES3Member(serializableMembers[i]);
			}
		}

		// Token: 0x04000072 RID: 114
		public const string typeFieldName = "__type";

		// Token: 0x04000073 RID: 115
		public ES3Member[] members;

		// Token: 0x04000074 RID: 116
		public Type type;

		// Token: 0x04000075 RID: 117
		public bool isPrimitive;

		// Token: 0x04000076 RID: 118
		public bool isValueType;

		// Token: 0x04000077 RID: 119
		public bool isCollection;

		// Token: 0x04000078 RID: 120
		public bool isDictionary;

		// Token: 0x04000079 RID: 121
		public bool isTuple;

		// Token: 0x0400007A RID: 122
		public bool isEnum;

		// Token: 0x0400007B RID: 123
		public bool isES3TypeUnityObject;

		// Token: 0x0400007C RID: 124
		public bool isReflectedType;

		// Token: 0x0400007D RID: 125
		public bool isUnsupported;

		// Token: 0x0400007E RID: 126
		public int priority;
	}
}
