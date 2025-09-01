using System;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x02000028 RID: 40
	[Preserve]
	public abstract class ES3ComponentType : ES3UnityObjectType
	{
		// Token: 0x06000240 RID: 576 RVA: 0x00008BE1 File Offset: 0x00006DE1
		public ES3ComponentType(Type type) : base(type)
		{
		}

		// Token: 0x06000241 RID: 577
		protected abstract void WriteComponent(object obj, ES3Writer writer);

		// Token: 0x06000242 RID: 578
		protected abstract void ReadComponent<T>(ES3Reader reader, object obj);

		// Token: 0x06000243 RID: 579 RVA: 0x00008BEC File Offset: 0x00006DEC
		protected override void WriteUnityObject(object obj, ES3Writer writer)
		{
			Component component = obj as Component;
			if (obj != null && component == null)
			{
				string str = "Only types of UnityEngine.Component can be written with this method, but argument given is type of ";
				Type type = obj.GetType();
				throw new ArgumentException(str + ((type != null) ? type.ToString() : null));
			}
			ES3ReferenceMgrBase managerFromScene = ES3ReferenceMgrBase.GetManagerFromScene(component.gameObject.scene, true);
			if (managerFromScene != null)
			{
				writer.WriteProperty("goID", managerFromScene.Add(component.gameObject).ToString(), ES3Type_string.Instance);
			}
			this.WriteComponent(component, writer);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00008C75 File Offset: 0x00006E75
		protected override void ReadUnityObject<T>(ES3Reader reader, object obj)
		{
			this.ReadComponent<T>(reader, obj);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00008C7F File Offset: 0x00006E7F
		protected override object ReadUnityObject<T>(ES3Reader reader)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00008C88 File Offset: 0x00006E88
		protected override object ReadObject<T>(ES3Reader reader)
		{
			ES3ReferenceMgrBase es3ReferenceMgrBase = ES3ReferenceMgrBase.Current;
			long id = -1L;
			UnityEngine.Object @object = null;
			foreach (object obj in reader.Properties)
			{
				string text = (string)obj;
				if (text == "_ES3Ref")
				{
					id = reader.Read_ref();
					@object = es3ReferenceMgrBase.Get(id, true);
				}
				else if (text == "goID")
				{
					long id2 = reader.Read_ref();
					if (@object != null)
					{
						break;
					}
					GameObject gameObject = (GameObject)es3ReferenceMgrBase.Get(id2, this.type, false);
					if (gameObject == null)
					{
						gameObject = new GameObject("Easy Save 3 Loaded GameObject");
						es3ReferenceMgrBase.Add(gameObject, id2);
					}
					@object = ES3ComponentType.GetOrAddComponent(gameObject, this.type);
					es3ReferenceMgrBase.Add(@object, id);
					break;
				}
				else
				{
					reader.overridePropertiesName = text;
					if (@object == null)
					{
						GameObject gameObject2 = new GameObject("Easy Save 3 Loaded GameObject");
						@object = ES3ComponentType.GetOrAddComponent(gameObject2, this.type);
						es3ReferenceMgrBase.Add(@object, id);
						es3ReferenceMgrBase.Add(gameObject2);
						break;
					}
					break;
				}
			}
			if (@object != null)
			{
				this.ReadComponent<T>(reader, @object);
			}
			return @object;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00008DD8 File Offset: 0x00006FD8
		private static Component GetOrAddComponent(GameObject go, Type type)
		{
			Component component = go.GetComponent(type);
			if (component != null)
			{
				return component;
			}
			return go.AddComponent(type);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00008E00 File Offset: 0x00007000
		public static Component CreateComponent(Type type)
		{
			GameObject gameObject = new GameObject("Easy Save 3 Loaded Component");
			if (type == typeof(Transform))
			{
				return gameObject.GetComponent(type);
			}
			return ES3ComponentType.GetOrAddComponent(gameObject, type);
		}

		// Token: 0x0400006B RID: 107
		protected const string gameObjectPropertyName = "goID";
	}
}
