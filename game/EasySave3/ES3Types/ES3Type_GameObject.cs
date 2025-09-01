using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ES3Internal;
using UnityEngine;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200008D RID: 141
	[Preserve]
	[ES3Properties(new string[]
	{
		"layer",
		"isStatic",
		"tag",
		"name",
		"hideFlags",
		"children",
		"components"
	})]
	public class ES3Type_GameObject : ES3UnityObjectType
	{
		// Token: 0x06000359 RID: 857 RVA: 0x00010DA5 File Offset: 0x0000EFA5
		public ES3Type_GameObject() : base(typeof(GameObject))
		{
			ES3Type_GameObject.Instance = this;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00010DC0 File Offset: 0x0000EFC0
		public override void WriteObject(object obj, ES3Writer writer, ES3.ReferenceMode mode)
		{
			if (base.WriteUsingDerivedType(obj, writer))
			{
				return;
			}
			GameObject gameObject = (GameObject)obj;
			ES3ReferenceMgrBase managerFromScene = ES3ReferenceMgrBase.GetManagerFromScene(gameObject.scene, true);
			if (mode != ES3.ReferenceMode.ByValue)
			{
				writer.WriteRef(gameObject, "_ES3Ref", managerFromScene);
				if (mode == ES3.ReferenceMode.ByRef)
				{
					return;
				}
				ES3Prefab component = gameObject.GetComponent<ES3Prefab>();
				if (component != null)
				{
					writer.WriteProperty("es3Prefab", component, ES3Type_ES3PrefabInternal.Instance);
				}
				writer.WriteRef(gameObject.transform, "transformID", managerFromScene);
			}
			ES3AutoSave component2 = gameObject.GetComponent<ES3AutoSave>();
			if (component2 == null || component2.saveLayer)
			{
				writer.WriteProperty("layer", gameObject.layer, ES3Type_int.Instance);
			}
			if (component2 == null || component2.saveTag)
			{
				writer.WriteProperty("tag", gameObject.tag, ES3Type_string.Instance);
			}
			if (component2 == null || component2.saveName)
			{
				writer.WriteProperty("name", gameObject.name, ES3Type_string.Instance);
			}
			if (component2 == null || component2.saveHideFlags)
			{
				writer.WriteProperty("hideFlags", gameObject.hideFlags);
			}
			if (component2 == null || component2.saveActive)
			{
				writer.WriteProperty("active", gameObject.activeSelf);
			}
			if ((component2 == null && this.saveChildren) || (component2 != null && component2.saveChildren))
			{
				writer.WriteProperty("children", ES3Type_GameObject.GetChildren(gameObject), ES3.ReferenceMode.ByRefAndValue);
			}
			ES3GameObject component3 = gameObject.GetComponent<ES3GameObject>();
			List<Component> list;
			if (component2 != null)
			{
				component2.componentsToSave.RemoveAll((Component c) => c == null);
				list = component2.componentsToSave;
			}
			else if (component3 != null)
			{
				component3.components.RemoveAll((Component c) => c == null);
				list = component3.components;
			}
			else
			{
				list = new List<Component>();
				foreach (Component component4 in gameObject.GetComponents<Component>())
				{
					if (component4 != null && ES3TypeMgr.GetES3Type(component4.GetType()) != null)
					{
						list.Add(component4);
					}
				}
			}
			if (list != null & list.Count > 0)
			{
				writer.WriteProperty("components", list, ES3.ReferenceMode.ByRefAndValue);
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00011028 File Offset: 0x0000F228
		protected override object ReadObject<T>(ES3Reader reader)
		{
			UnityEngine.Object @object = null;
			ES3ReferenceMgrBase es3ReferenceMgrBase = ES3ReferenceMgrBase.Current;
			long id = 0L;
			while (!(es3ReferenceMgrBase == null))
			{
				string text = base.ReadPropertyName(reader);
				if (text == "__type")
				{
					return ES3TypeMgr.GetOrCreateES3Type(reader.ReadType(), true).Read<T>(reader);
				}
				if (text == "_ES3Ref")
				{
					id = reader.Read_ref();
					@object = es3ReferenceMgrBase.Get(id, true);
				}
				else if (text == "transformID")
				{
					long id2 = reader.Read_ref();
					if (@object == null)
					{
						@object = this.CreateNewGameObject(es3ReferenceMgrBase, id);
					}
					es3ReferenceMgrBase.Add(((GameObject)@object).transform, id2);
				}
				else if (text == "es3Prefab")
				{
					if (@object != null || ES3ReferenceMgrBase.Current == null)
					{
						reader.ReadInto<GameObject>(@object);
					}
					else
					{
						@object = reader.Read<GameObject>(ES3Type_ES3PrefabInternal.Instance);
						ES3ReferenceMgrBase.Current.Add(@object, id);
					}
				}
				else
				{
					if (text == null)
					{
						return @object;
					}
					reader.overridePropertiesName = text;
					if (@object == null)
					{
						@object = this.CreateNewGameObject(es3ReferenceMgrBase, id);
					}
					this.ReadInto<T>(reader, @object);
					return @object;
				}
			}
			throw new InvalidOperationException(string.Format("An Easy Save 3 Manager is required to save references. To add one to your scene, exit playmode and go to Tools > Easy Save 3 > Add Manager to Scene. Object being saved by reference is {0} with name {1}.", @object.GetType(), @object.name));
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00011160 File Offset: 0x0000F360
		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			GameObject gameObject = (GameObject)obj;
			foreach (object obj2 in reader.Properties)
			{
				string text = (string)obj2;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 1739180498U)
				{
					if (num <= 128336118U)
					{
						if (num != 36885379U)
						{
							if (num == 128336118U)
							{
								if (text == "layer")
								{
									gameObject.layer = reader.Read<int>(ES3Type_int.Instance);
									continue;
								}
							}
						}
						else if (text == "prefab")
						{
							continue;
						}
					}
					else if (num != 469045609U)
					{
						if (num == 1739180498U)
						{
							if (text == "children")
							{
								GameObject[] array = reader.Read<GameObject[]>();
								Transform transform = gameObject.transform;
								GameObject[] array2 = array;
								for (int i = 0; i < array2.Length; i++)
								{
									array2[i].transform.SetParent(transform);
								}
								continue;
							}
						}
					}
					else if (text == "components")
					{
						this.ReadComponents(reader, gameObject);
						continue;
					}
				}
				else if (num <= 2516003219U)
				{
					if (num != 2369371622U)
					{
						if (num == 2516003219U)
						{
							if (text == "tag")
							{
								gameObject.tag = reader.Read<string>(ES3Type_string.Instance);
								continue;
							}
						}
					}
					else if (text == "name")
					{
						gameObject.name = reader.Read<string>(ES3Type_string.Instance);
						continue;
					}
				}
				else if (num != 3043476896U)
				{
					if (num != 3648362799U)
					{
						if (num == 3944566772U)
						{
							if (text == "hideFlags")
							{
								gameObject.hideFlags = reader.Read<HideFlags>();
								continue;
							}
						}
					}
					else if (text == "active")
					{
						gameObject.SetActive(reader.Read<bool>(ES3Type_bool.Instance));
						continue;
					}
				}
				else if (text == "_ES3Ref")
				{
					ES3ReferenceMgrBase.Current.Add(gameObject, reader.Read_ref());
					continue;
				}
				reader.Skip();
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x000113E0 File Offset: 0x0000F5E0
		private void ReadComponents(ES3Reader reader, GameObject go)
		{
			if (reader.StartReadCollection())
			{
				return;
			}
			List<Component> list = new List<Component>(go.GetComponents<Component>());
			for (;;)
			{
				if (!reader.StartReadCollectionItem())
				{
					goto IL_18C;
				}
				if (!reader.StartReadObject())
				{
					string text = null;
					Type type = null;
					string text2;
					for (;;)
					{
						text2 = base.ReadPropertyName(reader);
						if (!(text2 == "__type"))
						{
							break;
						}
						text = reader.Read<string>(ES3Type_string.Instance);
						type = ES3Reflection.GetType(text);
					}
					if (text2 == "_ES3Ref")
					{
						if (type == null)
						{
							if (string.IsNullOrEmpty(text))
							{
								break;
							}
							Debug.LogWarning("Cannot load Component of type " + text + " because this type no longer exists in your project. Note that this issue will create an empty GameObject named 'New Game Object' in your scene due to the way in which this Component needs to be skipped.");
							reader.overridePropertiesName = text2;
							this.ReadObject<Component>(reader);
						}
						else
						{
							long id = reader.Read_ref();
							List<Component> list2 = list;
							Predicate<Component> match;
							Predicate<Component> <>9__0;
							if ((match = <>9__0) == null)
							{
								match = (<>9__0 = ((Component x) => x.GetType() == type));
							}
							Component component = list2.Find(match);
							if (component != null)
							{
								if (ES3ReferenceMgrBase.Current != null)
								{
									ES3ReferenceMgrBase.Current.Add(component, id);
								}
								ES3TypeMgr.GetOrCreateES3Type(type, true).ReadInto<Component>(reader, component);
								list.Remove(component);
							}
							else
							{
								Component obj = go.AddComponent(type);
								ES3TypeMgr.GetOrCreateES3Type(type, true).ReadInto<Component>(reader, obj);
								ES3ReferenceMgrBase.Current.Add(obj, id);
							}
						}
					}
					else if (text2 != null)
					{
						reader.overridePropertiesName = text2;
						this.ReadObject<Component>(reader);
					}
					reader.EndReadObject();
					if (reader.EndReadCollectionItem())
					{
						goto IL_18C;
					}
				}
			}
			throw new InvalidOperationException("Cannot load Component because no type data has been stored with it, so it's not possible to determine it's type");
			IL_18C:
			reader.EndReadCollection();
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00011580 File Offset: 0x0000F780
		private GameObject CreateNewGameObject(ES3ReferenceMgrBase refMgr, long id)
		{
			GameObject gameObject = new GameObject();
			if (id != 0L)
			{
				refMgr.Add(gameObject, id);
			}
			else
			{
				refMgr.Add(gameObject);
			}
			return gameObject;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000115AC File Offset: 0x0000F7AC
		public static List<GameObject> GetChildren(GameObject go)
		{
			Transform transform = go.transform;
			List<GameObject> list = new List<GameObject>();
			foreach (object obj in transform)
			{
				Transform transform2 = (Transform)obj;
				list.Add(transform2.gameObject);
			}
			return list;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00011614 File Offset: 0x0000F814
		protected override void WriteUnityObject(object obj, ES3Writer writer)
		{
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00011616 File Offset: 0x0000F816
		protected override void ReadUnityObject<T>(ES3Reader reader, object obj)
		{
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00011618 File Offset: 0x0000F818
		protected override object ReadUnityObject<T>(ES3Reader reader)
		{
			return null;
		}

		// Token: 0x040000DD RID: 221
		private const string prefabPropertyName = "es3Prefab";

		// Token: 0x040000DE RID: 222
		private const string transformPropertyName = "transformID";

		// Token: 0x040000DF RID: 223
		public static ES3Type Instance;

		// Token: 0x040000E0 RID: 224
		public bool saveChildren;

		// Token: 0x02000106 RID: 262
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060005A4 RID: 1444 RVA: 0x00020576 File Offset: 0x0001E776
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060005A5 RID: 1445 RVA: 0x00020582 File Offset: 0x0001E782
			public <>c()
			{
			}

			// Token: 0x060005A6 RID: 1446 RVA: 0x0002058A File Offset: 0x0001E78A
			internal bool <WriteObject>b__5_0(Component c)
			{
				return c == null;
			}

			// Token: 0x060005A7 RID: 1447 RVA: 0x00020593 File Offset: 0x0001E793
			internal bool <WriteObject>b__5_1(Component c)
			{
				return c == null;
			}

			// Token: 0x040001F9 RID: 505
			public static readonly ES3Type_GameObject.<>c <>9 = new ES3Type_GameObject.<>c();

			// Token: 0x040001FA RID: 506
			public static Predicate<Component> <>9__5_0;

			// Token: 0x040001FB RID: 507
			public static Predicate<Component> <>9__5_1;
		}

		// Token: 0x02000107 RID: 263
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0
		{
			// Token: 0x060005A8 RID: 1448 RVA: 0x0002059C File Offset: 0x0001E79C
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x060005A9 RID: 1449 RVA: 0x000205A4 File Offset: 0x0001E7A4
			internal bool <ReadComponents>b__0(Component x)
			{
				return x.GetType() == this.type;
			}

			// Token: 0x040001FC RID: 508
			public Type type;

			// Token: 0x040001FD RID: 509
			public Predicate<Component> <>9__0;
		}
	}
}
