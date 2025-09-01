using System;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200002E RID: 46
	[Preserve]
	public abstract class ES3UnityObjectType : ES3ObjectType
	{
		// Token: 0x06000266 RID: 614 RVA: 0x00009762 File Offset: 0x00007962
		public ES3UnityObjectType(Type type) : base(type)
		{
			this.isValueType = false;
			this.isES3TypeUnityObject = true;
		}

		// Token: 0x06000267 RID: 615
		protected abstract void WriteUnityObject(object obj, ES3Writer writer);

		// Token: 0x06000268 RID: 616
		protected abstract void ReadUnityObject<T>(ES3Reader reader, object obj);

		// Token: 0x06000269 RID: 617
		protected abstract object ReadUnityObject<T>(ES3Reader reader);

		// Token: 0x0600026A RID: 618 RVA: 0x00009779 File Offset: 0x00007979
		protected override void WriteObject(object obj, ES3Writer writer)
		{
			this.WriteObject(obj, writer, ES3.ReferenceMode.ByRefAndValue);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009784 File Offset: 0x00007984
		public virtual void WriteObject(object obj, ES3Writer writer, ES3.ReferenceMode mode)
		{
			if (this.WriteUsingDerivedType(obj, writer, mode))
			{
				return;
			}
			UnityEngine.Object @object = obj as UnityEngine.Object;
			if (obj != null && @object == null)
			{
				string str = "Only types of UnityEngine.Object can be written with this method, but argument given is type of ";
				Type type = obj.GetType();
				throw new ArgumentException(str + ((type != null) ? type.ToString() : null));
			}
			if (mode != ES3.ReferenceMode.ByValue)
			{
				if (ES3ReferenceMgrBase.Current == null)
				{
					throw new InvalidOperationException(string.Format("An Easy Save 3 Manager is required to save references. To add one to your scene, exit playmode and go to Tools > Easy Save 3 > Add Manager to Scene. Object being saved by reference is {0} with name {1}.", @object.GetType(), @object.name));
				}
				writer.WriteRef(@object);
				if (mode == ES3.ReferenceMode.ByRef)
				{
					return;
				}
			}
			this.WriteUnityObject(@object, writer);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00009814 File Offset: 0x00007A14
		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			ES3ReferenceMgrBase es3ReferenceMgrBase = ES3ReferenceMgrBase.Current;
			if (es3ReferenceMgrBase != null)
			{
				foreach (object obj2 in reader.Properties)
				{
					string text = (string)obj2;
					if (!(text == "_ES3Ref"))
					{
						reader.overridePropertiesName = text;
						break;
					}
					es3ReferenceMgrBase.Add((UnityEngine.Object)obj, reader.Read_ref());
				}
			}
			this.ReadUnityObject<T>(reader, obj);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000098A8 File Offset: 0x00007AA8
		protected override object ReadObject<T>(ES3Reader reader)
		{
			ES3ReferenceMgrBase es3ReferenceMgrBase = ES3ReferenceMgrBase.Current;
			if (es3ReferenceMgrBase == null)
			{
				return this.ReadUnityObject<T>(reader);
			}
			long id = -1L;
			UnityEngine.Object @object = null;
			foreach (object obj in reader.Properties)
			{
				string text = (string)obj;
				if (text == "_ES3Ref")
				{
					if (es3ReferenceMgrBase == null)
					{
						throw new InvalidOperationException(string.Format("An Easy Save 3 Manager is required to save references. To add one to your scene, exit playmode and go to Tools > Easy Save 3 > Add Manager to Scene. Object being saved by reference is {0} with name {1}.", @object.GetType(), @object.name));
					}
					id = reader.Read_ref();
					@object = es3ReferenceMgrBase.Get(id, this.type, false);
					if (@object != null)
					{
						break;
					}
				}
				else
				{
					reader.overridePropertiesName = text;
					if (@object == null)
					{
						@object = (UnityEngine.Object)this.ReadUnityObject<T>(reader);
						es3ReferenceMgrBase.Add(@object, id);
						break;
					}
					break;
				}
			}
			this.ReadUnityObject<T>(reader, @object);
			return @object;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000099A8 File Offset: 0x00007BA8
		protected bool WriteUsingDerivedType(object obj, ES3Writer writer, ES3.ReferenceMode mode)
		{
			Type type = obj.GetType();
			if (type != this.type)
			{
				writer.WriteType(type);
				ES3Type orCreateES3Type = ES3TypeMgr.GetOrCreateES3Type(type, true);
				if (orCreateES3Type is ES3UnityObjectType)
				{
					((ES3UnityObjectType)orCreateES3Type).WriteObject(obj, writer, mode);
				}
				else
				{
					orCreateES3Type.Write(obj, writer);
				}
				return true;
			}
			return false;
		}
	}
}
