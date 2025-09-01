using System;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200002B RID: 43
	[Preserve]
	public abstract class ES3ScriptableObjectType : ES3UnityObjectType
	{
		// Token: 0x06000253 RID: 595 RVA: 0x000090A2 File Offset: 0x000072A2
		public ES3ScriptableObjectType(Type type) : base(type)
		{
		}

		// Token: 0x06000254 RID: 596
		protected abstract void WriteScriptableObject(object obj, ES3Writer writer);

		// Token: 0x06000255 RID: 597
		protected abstract void ReadScriptableObject<T>(ES3Reader reader, object obj);

		// Token: 0x06000256 RID: 598 RVA: 0x000090AC File Offset: 0x000072AC
		protected override void WriteUnityObject(object obj, ES3Writer writer)
		{
			ScriptableObject scriptableObject = obj as ScriptableObject;
			if (obj != null && scriptableObject == null)
			{
				string str = "Only types of UnityEngine.ScriptableObject can be written with this method, but argument given is type of ";
				Type type = obj.GetType();
				throw new ArgumentException(str + ((type != null) ? type.ToString() : null));
			}
			this.WriteScriptableObject(scriptableObject, writer);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000090F6 File Offset: 0x000072F6
		protected override void ReadUnityObject<T>(ES3Reader reader, object obj)
		{
			this.ReadScriptableObject<T>(reader, obj);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009100 File Offset: 0x00007300
		protected override object ReadUnityObject<T>(ES3Reader reader)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00009108 File Offset: 0x00007308
		protected override object ReadObject<T>(ES3Reader reader)
		{
			ES3ReferenceMgrBase es3ReferenceMgrBase = ES3ReferenceMgrBase.Current;
			long id = -1L;
			UnityEngine.Object @object = null;
			foreach (object obj in reader.Properties)
			{
				string text = (string)obj;
				if (text == "_ES3Ref" && es3ReferenceMgrBase != null)
				{
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
					if (!(@object == null))
					{
						break;
					}
					@object = ScriptableObject.CreateInstance(this.type);
					if (es3ReferenceMgrBase != null)
					{
						es3ReferenceMgrBase.Add(@object, id);
						break;
					}
					break;
				}
			}
			this.ReadScriptableObject<T>(reader, @object);
			return @object;
		}
	}
}
