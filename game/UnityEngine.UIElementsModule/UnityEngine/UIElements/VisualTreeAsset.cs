using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x020002F6 RID: 758
	[HelpURL("UIE-VisualTree-landing")]
	[Serializable]
	public class VisualTreeAsset : ScriptableObject
	{
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001924 RID: 6436 RVA: 0x0006665C File Offset: 0x0006485C
		// (set) Token: 0x06001925 RID: 6437 RVA: 0x00066674 File Offset: 0x00064874
		public bool importedWithErrors
		{
			get
			{
				return this.m_ImportedWithErrors;
			}
			internal set
			{
				this.m_ImportedWithErrors = value;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x00066680 File Offset: 0x00064880
		// (set) Token: 0x06001927 RID: 6439 RVA: 0x00066698 File Offset: 0x00064898
		public bool importedWithWarnings
		{
			get
			{
				return this.m_ImportedWithWarnings;
			}
			internal set
			{
				this.m_ImportedWithWarnings = value;
			}
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x000666A4 File Offset: 0x000648A4
		internal int GetNextChildSerialNumber()
		{
			List<VisualElementAsset> visualElementAssets = this.m_VisualElementAssets;
			int num = (visualElementAssets != null) ? visualElementAssets.Count : 0;
			int num2 = num;
			List<TemplateAsset> templateAssets = this.m_TemplateAssets;
			return num2 + ((templateAssets != null) ? templateAssets.Count : 0);
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x000666E0 File Offset: 0x000648E0
		public IEnumerable<VisualTreeAsset> templateDependencies
		{
			get
			{
				bool flag = this.m_Usings == null || this.m_Usings.Count == 0;
				if (flag)
				{
					yield break;
				}
				HashSet<VisualTreeAsset> sent = new HashSet<VisualTreeAsset>();
				foreach (VisualTreeAsset.UsingEntry entry in this.m_Usings)
				{
					bool flag2 = entry.asset != null && !sent.Contains(entry.asset);
					if (flag2)
					{
						sent.Add(entry.asset);
						yield return entry.asset;
					}
					else
					{
						bool flag3 = !string.IsNullOrEmpty(entry.path);
						if (flag3)
						{
							VisualTreeAsset vta = Panel.LoadResource(entry.path, typeof(VisualTreeAsset), GUIUtility.pixelsPerPoint) as VisualTreeAsset;
							bool flag4 = vta != null && !sent.Contains(entry.asset);
							if (flag4)
							{
								sent.Add(entry.asset);
								yield return vta;
							}
							vta = null;
						}
					}
					entry = default(VisualTreeAsset.UsingEntry);
				}
				List<VisualTreeAsset.UsingEntry>.Enumerator enumerator = default(List<VisualTreeAsset.UsingEntry>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x00066700 File Offset: 0x00064900
		public IEnumerable<StyleSheet> stylesheets
		{
			get
			{
				HashSet<StyleSheet> sent = new HashSet<StyleSheet>();
				foreach (VisualElementAsset vea in this.m_VisualElementAssets)
				{
					bool hasStylesheets = vea.hasStylesheets;
					if (hasStylesheets)
					{
						foreach (StyleSheet stylesheet in vea.stylesheets)
						{
							bool flag = !sent.Contains(stylesheet);
							if (flag)
							{
								sent.Add(stylesheet);
								yield return stylesheet;
							}
							stylesheet = null;
						}
						List<StyleSheet>.Enumerator enumerator2 = default(List<StyleSheet>.Enumerator);
					}
					bool hasStylesheetPaths = vea.hasStylesheetPaths;
					if (hasStylesheetPaths)
					{
						foreach (string stylesheetPath in vea.stylesheetPaths)
						{
							StyleSheet stylesheet2 = Panel.LoadResource(stylesheetPath, typeof(StyleSheet), GUIUtility.pixelsPerPoint) as StyleSheet;
							bool flag2 = stylesheet2 != null && !sent.Contains(stylesheet2);
							if (flag2)
							{
								sent.Add(stylesheet2);
								yield return stylesheet2;
							}
							stylesheet2 = null;
							stylesheetPath = null;
						}
						List<string>.Enumerator enumerator3 = default(List<string>.Enumerator);
					}
					vea = null;
				}
				List<VisualElementAsset>.Enumerator enumerator = default(List<VisualElementAsset>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x00066720 File Offset: 0x00064920
		// (set) Token: 0x0600192C RID: 6444 RVA: 0x00066738 File Offset: 0x00064938
		internal List<VisualElementAsset> visualElementAssets
		{
			get
			{
				return this.m_VisualElementAssets;
			}
			set
			{
				this.m_VisualElementAssets = value;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x0600192D RID: 6445 RVA: 0x00066744 File Offset: 0x00064944
		// (set) Token: 0x0600192E RID: 6446 RVA: 0x0006675C File Offset: 0x0006495C
		internal List<TemplateAsset> templateAssets
		{
			get
			{
				return this.m_TemplateAssets;
			}
			set
			{
				this.m_TemplateAssets = value;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x0600192F RID: 6447 RVA: 0x00066768 File Offset: 0x00064968
		// (set) Token: 0x06001930 RID: 6448 RVA: 0x00066780 File Offset: 0x00064980
		internal List<VisualTreeAsset.SlotDefinition> slots
		{
			get
			{
				return this.m_Slots;
			}
			set
			{
				this.m_Slots = value;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001931 RID: 6449 RVA: 0x0006678C File Offset: 0x0006498C
		// (set) Token: 0x06001932 RID: 6450 RVA: 0x000667A4 File Offset: 0x000649A4
		internal int contentContainerId
		{
			get
			{
				return this.m_ContentContainerId;
			}
			set
			{
				this.m_ContentContainerId = value;
			}
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x000667B0 File Offset: 0x000649B0
		public TemplateContainer Instantiate()
		{
			TemplateContainer templateContainer = new TemplateContainer(base.name);
			try
			{
				this.CloneTree(templateContainer, VisualTreeAsset.s_TemporarySlotInsertionPoints, null);
			}
			finally
			{
				VisualTreeAsset.s_TemporarySlotInsertionPoints.Clear();
			}
			return templateContainer;
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x00066800 File Offset: 0x00064A00
		public TemplateContainer Instantiate(string bindingPath)
		{
			TemplateContainer templateContainer = this.Instantiate();
			templateContainer.bindingPath = bindingPath;
			return templateContainer;
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00066824 File Offset: 0x00064A24
		public TemplateContainer CloneTree()
		{
			return this.Instantiate();
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x0006683C File Offset: 0x00064A3C
		public TemplateContainer CloneTree(string bindingPath)
		{
			return this.Instantiate(bindingPath);
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00066858 File Offset: 0x00064A58
		public void CloneTree(VisualElement target)
		{
			int num;
			int num2;
			this.CloneTree(target, out num, out num2);
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x00066874 File Offset: 0x00064A74
		public void CloneTree(VisualElement target, out int firstElementIndex, out int elementAddedCount)
		{
			bool flag = target == null;
			if (flag)
			{
				throw new ArgumentNullException("target");
			}
			firstElementIndex = target.childCount;
			try
			{
				this.CloneTree(target, VisualTreeAsset.s_TemporarySlotInsertionPoints, null);
			}
			finally
			{
				elementAddedCount = target.childCount - firstElementIndex;
				VisualTreeAsset.s_TemporarySlotInsertionPoints.Clear();
			}
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x000668D8 File Offset: 0x00064AD8
		internal void CloneTree(VisualElement target, Dictionary<string, VisualElement> slotInsertionPoints, List<TemplateAsset.AttributeOverride> attributeOverrides)
		{
			bool flag = target == null;
			if (flag)
			{
				throw new ArgumentNullException("target");
			}
			bool flag2 = (this.visualElementAssets == null || this.visualElementAssets.Count <= 0) && (this.templateAssets == null || this.templateAssets.Count <= 0);
			if (!flag2)
			{
				TemplateContainer templateContainer = target as TemplateContainer;
				bool flag3 = templateContainer != null;
				if (flag3)
				{
					templateContainer.templateSource = this;
				}
				Dictionary<int, List<VisualElementAsset>> dictionary = new Dictionary<int, List<VisualElementAsset>>();
				int num = (this.visualElementAssets == null) ? 0 : this.visualElementAssets.Count;
				int num2 = (this.templateAssets == null) ? 0 : this.templateAssets.Count;
				for (int i = 0; i < num + num2; i++)
				{
					VisualElementAsset visualElementAsset = (i < num) ? this.visualElementAssets[i] : this.templateAssets[i - num];
					List<VisualElementAsset> list;
					bool flag4 = !dictionary.TryGetValue(visualElementAsset.parentId, out list);
					if (flag4)
					{
						list = new List<VisualElementAsset>();
						dictionary.Add(visualElementAsset.parentId, list);
					}
					list.Add(visualElementAsset);
				}
				List<VisualElementAsset> list2;
				dictionary.TryGetValue(0, out list2);
				bool flag5 = list2 == null || list2.Count == 0;
				if (!flag5)
				{
					Debug.Assert(list2.Count == 1);
					VisualElementAsset visualElementAsset2 = list2[0];
					VisualTreeAsset.AssignClassListFromAssetToElement(visualElementAsset2, target);
					VisualTreeAsset.AssignStyleSheetFromAssetToElement(visualElementAsset2, target);
					list2.Clear();
					dictionary.TryGetValue(visualElementAsset2.id, out list2);
					bool flag6 = list2 == null || list2.Count == 0;
					if (!flag6)
					{
						list2.Sort(new Comparison<VisualElementAsset>(VisualTreeAsset.CompareForOrder));
						foreach (VisualElementAsset visualElementAsset3 in list2)
						{
							Assert.IsNotNull<VisualElementAsset>(visualElementAsset3);
							VisualElement visualElement = this.CloneSetupRecursively(visualElementAsset3, dictionary, new CreationContext(slotInsertionPoints, attributeOverrides, this, target));
							bool flag7 = visualElement != null;
							if (flag7)
							{
								visualElement.visualTreeAssetSource = this;
								target.hierarchy.Add(visualElement);
							}
							else
							{
								bool flag8 = visualElementAsset3.fullTypeName == "Unity.UI.Builder.UnityUIBuilderSelectionMarker";
								if (!flag8)
								{
									Debug.LogWarning("VisualTreeAsset instantiated an empty UI. Check the syntax of your UXML document.");
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00066B54 File Offset: 0x00064D54
		private VisualElement CloneSetupRecursively(VisualElementAsset root, Dictionary<int, List<VisualElementAsset>> idToChildren, CreationContext context)
		{
			bool skipClone = root.skipClone;
			VisualElement result;
			if (skipClone)
			{
				result = null;
			}
			else
			{
				VisualElement visualElement = VisualTreeAsset.Create(root, context);
				bool flag = visualElement == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					bool flag2 = root.id == context.visualTreeAsset.contentContainerId;
					if (flag2)
					{
						bool flag3 = context.target is TemplateContainer;
						if (flag3)
						{
							((TemplateContainer)context.target).SetContentContainer(visualElement);
						}
						else
						{
							Debug.LogError("Trying to clone a VisualTreeAsset with a custom content container into a element which is not a template container");
						}
					}
					string key;
					bool flag4 = context.slotInsertionPoints != null && this.TryGetSlotInsertionPoint(root.id, out key);
					if (flag4)
					{
						context.slotInsertionPoints.Add(key, visualElement);
					}
					bool flag5 = root.ruleIndex != -1;
					if (flag5)
					{
						bool flag6 = this.inlineSheet == null;
						if (flag6)
						{
							Debug.LogWarning("VisualElementAsset has a RuleIndex but no inlineStyleSheet");
						}
						else
						{
							StyleRule rule = this.inlineSheet.rules[root.ruleIndex];
							visualElement.SetInlineRule(this.inlineSheet, rule);
						}
					}
					TemplateAsset templateAsset = root as TemplateAsset;
					List<VisualElementAsset> list;
					bool flag7 = idToChildren.TryGetValue(root.id, out list);
					if (flag7)
					{
						list.Sort(new Comparison<VisualElementAsset>(VisualTreeAsset.CompareForOrder));
						using (List<VisualElementAsset>.Enumerator enumerator = list.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								VisualElementAsset childVea = enumerator.Current;
								VisualElement visualElement2 = this.CloneSetupRecursively(childVea, idToChildren, context);
								bool flag8 = visualElement2 == null;
								if (!flag8)
								{
									bool flag9 = templateAsset == null;
									if (flag9)
									{
										visualElement.Add(visualElement2);
									}
									else
									{
										int num = (templateAsset.slotUsages == null) ? -1 : templateAsset.slotUsages.FindIndex((VisualTreeAsset.SlotUsageEntry u) => u.assetId == childVea.id);
										bool flag10 = num != -1;
										if (flag10)
										{
											string slotName = templateAsset.slotUsages[num].slotName;
											Assert.IsFalse(string.IsNullOrEmpty(slotName), "a lost name should not be null or empty, this probably points to an importer or serialization bug");
											VisualElement visualElement3;
											bool flag11 = context.slotInsertionPoints == null || !context.slotInsertionPoints.TryGetValue(slotName, out visualElement3);
											if (flag11)
											{
												Debug.LogErrorFormat("Slot '{0}' was not found. Existing slots: {1}", new object[]
												{
													slotName,
													(context.slotInsertionPoints == null) ? string.Empty : string.Join(", ", context.slotInsertionPoints.Keys.ToArray<string>())
												});
												visualElement.Add(visualElement2);
											}
											else
											{
												visualElement3.Add(visualElement2);
											}
										}
										else
										{
											visualElement.Add(visualElement2);
										}
									}
								}
							}
						}
					}
					bool flag12 = templateAsset != null && context.slotInsertionPoints != null;
					if (flag12)
					{
						context.slotInsertionPoints.Clear();
					}
					result = visualElement;
				}
			}
			return result;
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00066E4C File Offset: 0x0006504C
		private static int CompareForOrder(VisualElementAsset a, VisualElementAsset b)
		{
			return a.orderInDocument.CompareTo(b.orderInDocument);
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00066E70 File Offset: 0x00065070
		internal bool TryGetSlotInsertionPoint(int insertionPointId, out string slotName)
		{
			bool flag = this.m_Slots == null;
			bool result;
			if (flag)
			{
				slotName = null;
				result = false;
			}
			else
			{
				for (int i = 0; i < this.m_Slots.Count; i++)
				{
					VisualTreeAsset.SlotDefinition slotDefinition = this.m_Slots[i];
					bool flag2 = slotDefinition.insertionPointId == insertionPointId;
					if (flag2)
					{
						slotName = slotDefinition.name;
						return true;
					}
				}
				slotName = null;
				result = false;
			}
			return result;
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00066EE4 File Offset: 0x000650E4
		internal VisualTreeAsset ResolveTemplate(string templateName)
		{
			bool flag = this.m_Usings == null || this.m_Usings.Count == 0;
			VisualTreeAsset result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = this.m_Usings.BinarySearch(new VisualTreeAsset.UsingEntry(templateName, string.Empty), VisualTreeAsset.UsingEntry.comparer);
				bool flag2 = num < 0;
				if (flag2)
				{
					result = null;
				}
				else
				{
					bool flag3 = this.m_Usings[num].asset;
					if (flag3)
					{
						result = this.m_Usings[num].asset;
					}
					else
					{
						string path = this.m_Usings[num].path;
						result = (Panel.LoadResource(path, typeof(VisualTreeAsset), GUIUtility.pixelsPerPoint) as VisualTreeAsset);
					}
				}
			}
			return result;
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00066FA0 File Offset: 0x000651A0
		internal static VisualElement Create(VisualElementAsset asset, CreationContext ctx)
		{
			VisualTreeAsset.<>c__DisplayClass49_0 CS$<>8__locals1;
			CS$<>8__locals1.asset = asset;
			List<IUxmlFactory> list;
			bool flag = !VisualElementFactoryRegistry.TryGetValue(CS$<>8__locals1.asset.fullTypeName, out list);
			if (flag)
			{
				bool flag2 = CS$<>8__locals1.asset.fullTypeName.StartsWith("UnityEngine.Experimental.UIElements.") || CS$<>8__locals1.asset.fullTypeName.StartsWith("UnityEditor.Experimental.UIElements.");
				if (flag2)
				{
					string fullTypeName = CS$<>8__locals1.asset.fullTypeName.Replace(".Experimental.UIElements", ".UIElements");
					bool flag3 = !VisualElementFactoryRegistry.TryGetValue(fullTypeName, out list);
					if (flag3)
					{
						return VisualTreeAsset.<Create>g__CreateError|49_0(ref CS$<>8__locals1);
					}
				}
				else
				{
					bool flag4 = CS$<>8__locals1.asset.fullTypeName == "UnityEditor.UIElements.ProgressBar";
					if (flag4)
					{
						string fullTypeName2 = CS$<>8__locals1.asset.fullTypeName.Replace("UnityEditor", "UnityEngine");
						bool flag5 = !VisualElementFactoryRegistry.TryGetValue(fullTypeName2, out list);
						if (flag5)
						{
							return VisualTreeAsset.<Create>g__CreateError|49_0(ref CS$<>8__locals1);
						}
					}
					else
					{
						bool flag6 = CS$<>8__locals1.asset.fullTypeName == "UXML";
						if (!flag6)
						{
							return VisualTreeAsset.<Create>g__CreateError|49_0(ref CS$<>8__locals1);
						}
						VisualElementFactoryRegistry.TryGetValue(typeof(UxmlRootElementFactory).Namespace + "." + CS$<>8__locals1.asset.fullTypeName, out list);
					}
				}
			}
			IUxmlFactory uxmlFactory = null;
			foreach (IUxmlFactory uxmlFactory2 in list)
			{
				bool flag7 = uxmlFactory2.AcceptsAttributeBag(CS$<>8__locals1.asset, ctx);
				if (flag7)
				{
					uxmlFactory = uxmlFactory2;
					break;
				}
			}
			bool flag8 = uxmlFactory == null;
			VisualElement result;
			if (flag8)
			{
				Debug.LogErrorFormat("Element '{0}' has a no factory that accept the set of XML attributes specified.", new object[]
				{
					CS$<>8__locals1.asset.fullTypeName
				});
				result = new Label(string.Format("Type with no factory: '{0}'", CS$<>8__locals1.asset.fullTypeName));
			}
			else
			{
				VisualElement visualElement = uxmlFactory.Create(CS$<>8__locals1.asset, ctx);
				bool flag9 = visualElement != null;
				if (flag9)
				{
					VisualTreeAsset.AssignClassListFromAssetToElement(CS$<>8__locals1.asset, visualElement);
					VisualTreeAsset.AssignStyleSheetFromAssetToElement(CS$<>8__locals1.asset, visualElement);
				}
				result = visualElement;
			}
			return result;
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x000671E8 File Offset: 0x000653E8
		private static void AssignClassListFromAssetToElement(VisualElementAsset asset, VisualElement element)
		{
			bool flag = asset.classes != null;
			if (flag)
			{
				for (int i = 0; i < asset.classes.Length; i++)
				{
					element.AddToClassList(asset.classes[i]);
				}
			}
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00067230 File Offset: 0x00065430
		private static void AssignStyleSheetFromAssetToElement(VisualElementAsset asset, VisualElement element)
		{
			bool hasStylesheetPaths = asset.hasStylesheetPaths;
			if (hasStylesheetPaths)
			{
				for (int i = 0; i < asset.stylesheetPaths.Count; i++)
				{
					element.AddStyleSheetPath(asset.stylesheetPaths[i]);
				}
			}
			bool hasStylesheets = asset.hasStylesheets;
			if (hasStylesheets)
			{
				for (int j = 0; j < asset.stylesheets.Count; j++)
				{
					bool flag = asset.stylesheets[j] != null;
					if (flag)
					{
						element.styleSheets.Add(asset.stylesheets[j]);
					}
				}
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x000672E0 File Offset: 0x000654E0
		// (set) Token: 0x06001942 RID: 6466 RVA: 0x000672F8 File Offset: 0x000654F8
		public int contentHash
		{
			get
			{
				return this.m_ContentHash;
			}
			set
			{
				this.m_ContentHash = value;
			}
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00062340 File Offset: 0x00060540
		public VisualTreeAsset()
		{
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x00067302 File Offset: 0x00065502
		// Note: this type is marked as 'beforefieldinit'.
		static VisualTreeAsset()
		{
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x00067318 File Offset: 0x00065518
		[CompilerGenerated]
		internal static VisualElement <Create>g__CreateError|49_0(ref VisualTreeAsset.<>c__DisplayClass49_0 A_0)
		{
			Debug.LogErrorFormat("Element '{0}' has no registered factory method.", new object[]
			{
				A_0.asset.fullTypeName
			});
			return new Label(string.Format("Unknown type: '{0}'", A_0.asset.fullTypeName));
		}

		// Token: 0x04000AC7 RID: 2759
		internal static string LinkedVEAInTemplatePropertyName = "--unity-linked-vea-in-template";

		// Token: 0x04000AC8 RID: 2760
		[SerializeField]
		private bool m_ImportedWithErrors;

		// Token: 0x04000AC9 RID: 2761
		[SerializeField]
		private bool m_ImportedWithWarnings;

		// Token: 0x04000ACA RID: 2762
		private static readonly Dictionary<string, VisualElement> s_TemporarySlotInsertionPoints = new Dictionary<string, VisualElement>();

		// Token: 0x04000ACB RID: 2763
		[SerializeField]
		private List<VisualTreeAsset.UsingEntry> m_Usings;

		// Token: 0x04000ACC RID: 2764
		[SerializeField]
		internal StyleSheet inlineSheet;

		// Token: 0x04000ACD RID: 2765
		[SerializeField]
		private List<VisualElementAsset> m_VisualElementAssets;

		// Token: 0x04000ACE RID: 2766
		[SerializeField]
		private List<TemplateAsset> m_TemplateAssets;

		// Token: 0x04000ACF RID: 2767
		[SerializeField]
		private List<VisualTreeAsset.SlotDefinition> m_Slots;

		// Token: 0x04000AD0 RID: 2768
		[SerializeField]
		private int m_ContentContainerId;

		// Token: 0x04000AD1 RID: 2769
		[SerializeField]
		private int m_ContentHash;

		// Token: 0x020002F7 RID: 759
		[Serializable]
		internal struct UsingEntry
		{
			// Token: 0x06001946 RID: 6470 RVA: 0x00067363 File Offset: 0x00065563
			public UsingEntry(string alias, string path)
			{
				this.alias = alias;
				this.path = path;
				this.asset = null;
			}

			// Token: 0x06001947 RID: 6471 RVA: 0x0006737B File Offset: 0x0006557B
			public UsingEntry(string alias, VisualTreeAsset asset)
			{
				this.alias = alias;
				this.path = null;
				this.asset = asset;
			}

			// Token: 0x06001948 RID: 6472 RVA: 0x00067393 File Offset: 0x00065593
			// Note: this type is marked as 'beforefieldinit'.
			static UsingEntry()
			{
			}

			// Token: 0x04000AD2 RID: 2770
			internal static readonly IComparer<VisualTreeAsset.UsingEntry> comparer = new VisualTreeAsset.UsingEntryComparer();

			// Token: 0x04000AD3 RID: 2771
			[SerializeField]
			public string alias;

			// Token: 0x04000AD4 RID: 2772
			[SerializeField]
			public string path;

			// Token: 0x04000AD5 RID: 2773
			[SerializeField]
			public VisualTreeAsset asset;
		}

		// Token: 0x020002F8 RID: 760
		private class UsingEntryComparer : IComparer<VisualTreeAsset.UsingEntry>
		{
			// Token: 0x06001949 RID: 6473 RVA: 0x000673A0 File Offset: 0x000655A0
			public int Compare(VisualTreeAsset.UsingEntry x, VisualTreeAsset.UsingEntry y)
			{
				return string.CompareOrdinal(x.alias, y.alias);
			}

			// Token: 0x0600194A RID: 6474 RVA: 0x000020C2 File Offset: 0x000002C2
			public UsingEntryComparer()
			{
			}
		}

		// Token: 0x020002F9 RID: 761
		[Serializable]
		internal struct SlotDefinition
		{
			// Token: 0x04000AD6 RID: 2774
			[SerializeField]
			public string name;

			// Token: 0x04000AD7 RID: 2775
			[SerializeField]
			public int insertionPointId;
		}

		// Token: 0x020002FA RID: 762
		[Serializable]
		internal struct SlotUsageEntry
		{
			// Token: 0x0600194B RID: 6475 RVA: 0x000673C3 File Offset: 0x000655C3
			public SlotUsageEntry(string slotName, int assetId)
			{
				this.slotName = slotName;
				this.assetId = assetId;
			}

			// Token: 0x04000AD8 RID: 2776
			[SerializeField]
			public string slotName;

			// Token: 0x04000AD9 RID: 2777
			[SerializeField]
			public int assetId;
		}

		// Token: 0x020002FB RID: 763
		[CompilerGenerated]
		private sealed class <get_templateDependencies>d__17 : IEnumerable<VisualTreeAsset>, IEnumerable, IEnumerator<VisualTreeAsset>, IEnumerator, IDisposable
		{
			// Token: 0x0600194C RID: 6476 RVA: 0x000673D4 File Offset: 0x000655D4
			[DebuggerHidden]
			public <get_templateDependencies>d__17(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x0600194D RID: 6477 RVA: 0x000673F4 File Offset: 0x000655F4
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num - 1 <= 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x0600194E RID: 6478 RVA: 0x00067434 File Offset: 0x00065634
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					switch (this.<>1__state)
					{
					case 0:
					{
						this.<>1__state = -1;
						bool flag = this.m_Usings == null || this.m_Usings.Count == 0;
						if (flag)
						{
							return false;
						}
						sent = new HashSet<VisualTreeAsset>();
						enumerator = this.m_Usings.GetEnumerator();
						this.<>1__state = -3;
						goto IL_1E1;
					}
					case 1:
						this.<>1__state = -3;
						goto IL_1D4;
					case 2:
						this.<>1__state = -3;
						break;
					default:
						return false;
					}
					IL_1CC:
					vta = null;
					IL_1D4:
					entry = default(VisualTreeAsset.UsingEntry);
					IL_1E1:
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = default(List<VisualTreeAsset.UsingEntry>.Enumerator);
						result = false;
					}
					else
					{
						entry = enumerator.Current;
						bool flag2 = entry.asset != null && !sent.Contains(entry.asset);
						if (flag2)
						{
							sent.Add(entry.asset);
							this.<>2__current = entry.asset;
							this.<>1__state = 1;
							result = true;
						}
						else
						{
							bool flag3 = !string.IsNullOrEmpty(entry.path);
							if (!flag3)
							{
								goto IL_1D4;
							}
							vta = (Panel.LoadResource(entry.path, typeof(VisualTreeAsset), GUIUtility.pixelsPerPoint) as VisualTreeAsset);
							bool flag4 = vta != null && !sent.Contains(entry.asset);
							if (!flag4)
							{
								goto IL_1CC;
							}
							sent.Add(entry.asset);
							this.<>2__current = vta;
							this.<>1__state = 2;
							result = true;
						}
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x0600194F RID: 6479 RVA: 0x00067670 File Offset: 0x00065870
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				((IDisposable)enumerator).Dispose();
			}

			// Token: 0x17000633 RID: 1587
			// (get) Token: 0x06001950 RID: 6480 RVA: 0x0006768B File Offset: 0x0006588B
			VisualTreeAsset IEnumerator<VisualTreeAsset>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001951 RID: 6481 RVA: 0x0000810E File Offset: 0x0000630E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000634 RID: 1588
			// (get) Token: 0x06001952 RID: 6482 RVA: 0x0006768B File Offset: 0x0006588B
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06001953 RID: 6483 RVA: 0x00067694 File Offset: 0x00065894
			[DebuggerHidden]
			IEnumerator<VisualTreeAsset> IEnumerable<VisualTreeAsset>.GetEnumerator()
			{
				VisualTreeAsset.<get_templateDependencies>d__17 <get_templateDependencies>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<get_templateDependencies>d__ = this;
				}
				else
				{
					<get_templateDependencies>d__ = new VisualTreeAsset.<get_templateDependencies>d__17(0);
					<get_templateDependencies>d__.<>4__this = this;
				}
				return <get_templateDependencies>d__;
			}

			// Token: 0x06001954 RID: 6484 RVA: 0x000676DC File Offset: 0x000658DC
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<UnityEngine.UIElements.VisualTreeAsset>.GetEnumerator();
			}

			// Token: 0x04000ADA RID: 2778
			private int <>1__state;

			// Token: 0x04000ADB RID: 2779
			private VisualTreeAsset <>2__current;

			// Token: 0x04000ADC RID: 2780
			private int <>l__initialThreadId;

			// Token: 0x04000ADD RID: 2781
			public VisualTreeAsset <>4__this;

			// Token: 0x04000ADE RID: 2782
			private HashSet<VisualTreeAsset> <sent>5__1;

			// Token: 0x04000ADF RID: 2783
			private List<VisualTreeAsset.UsingEntry>.Enumerator <>s__2;

			// Token: 0x04000AE0 RID: 2784
			private VisualTreeAsset.UsingEntry <entry>5__3;

			// Token: 0x04000AE1 RID: 2785
			private VisualTreeAsset <vta>5__4;
		}

		// Token: 0x020002FC RID: 764
		[CompilerGenerated]
		private sealed class <get_stylesheets>d__21 : IEnumerable<StyleSheet>, IEnumerable, IEnumerator<StyleSheet>, IEnumerator, IDisposable
		{
			// Token: 0x06001955 RID: 6485 RVA: 0x000676E4 File Offset: 0x000658E4
			[DebuggerHidden]
			public <get_stylesheets>d__21(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06001956 RID: 6486 RVA: 0x00067704 File Offset: 0x00065904
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num - -5 <= 2 || num - 1 <= 1)
				{
					try
					{
						if (num <= -4)
						{
							if (num == -5)
							{
								goto IL_44;
							}
							if (num != -4)
							{
								goto IL_50;
							}
						}
						else if (num != 1)
						{
							if (num != 2)
							{
								goto IL_50;
							}
							goto IL_44;
						}
						try
						{
						}
						finally
						{
							this.<>m__Finally2();
						}
						goto IL_50;
						IL_44:
						try
						{
						}
						finally
						{
							this.<>m__Finally3();
						}
						IL_50:;
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06001957 RID: 6487 RVA: 0x00067794 File Offset: 0x00065994
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					switch (this.<>1__state)
					{
					case 0:
						this.<>1__state = -1;
						sent = new HashSet<StyleSheet>();
						enumerator = this.m_VisualElementAssets.GetEnumerator();
						this.<>1__state = -3;
						goto IL_23D;
					case 1:
						this.<>1__state = -4;
						break;
					case 2:
						this.<>1__state = -5;
						goto IL_202;
					default:
						return false;
					}
					IL_10B:
					stylesheet = null;
					IL_113:
					if (!enumerator2.MoveNext())
					{
						this.<>m__Finally2();
						enumerator2 = default(List<StyleSheet>.Enumerator);
					}
					else
					{
						stylesheet = enumerator2.Current;
						bool flag = !sent.Contains(stylesheet);
						if (flag)
						{
							sent.Add(stylesheet);
							this.<>2__current = stylesheet;
							this.<>1__state = 1;
							return true;
						}
						goto IL_10B;
					}
					IL_134:
					bool hasStylesheetPaths = vea.hasStylesheetPaths;
					if (hasStylesheetPaths)
					{
						enumerator3 = vea.stylesheetPaths.GetEnumerator();
						this.<>1__state = -5;
						goto IL_211;
					}
					goto IL_235;
					IL_202:
					stylesheet2 = null;
					stylesheetPath = null;
					IL_211:
					if (!enumerator3.MoveNext())
					{
						this.<>m__Finally3();
						enumerator3 = default(List<string>.Enumerator);
					}
					else
					{
						stylesheetPath = enumerator3.Current;
						stylesheet2 = (Panel.LoadResource(stylesheetPath, typeof(StyleSheet), GUIUtility.pixelsPerPoint) as StyleSheet);
						bool flag2 = stylesheet2 != null && !sent.Contains(stylesheet2);
						if (flag2)
						{
							sent.Add(stylesheet2);
							this.<>2__current = stylesheet2;
							this.<>1__state = 2;
							return true;
						}
						goto IL_202;
					}
					IL_235:
					vea = null;
					IL_23D:
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = default(List<VisualElementAsset>.Enumerator);
						result = false;
					}
					else
					{
						vea = enumerator.Current;
						bool hasStylesheets = vea.hasStylesheets;
						if (hasStylesheets)
						{
							enumerator2 = vea.stylesheets.GetEnumerator();
							this.<>1__state = -4;
							goto IL_113;
						}
						goto IL_134;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x06001958 RID: 6488 RVA: 0x00067A2C File Offset: 0x00065C2C
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				((IDisposable)enumerator).Dispose();
			}

			// Token: 0x06001959 RID: 6489 RVA: 0x00067A47 File Offset: 0x00065C47
			private void <>m__Finally2()
			{
				this.<>1__state = -3;
				((IDisposable)enumerator2).Dispose();
			}

			// Token: 0x0600195A RID: 6490 RVA: 0x00067A63 File Offset: 0x00065C63
			private void <>m__Finally3()
			{
				this.<>1__state = -3;
				((IDisposable)enumerator3).Dispose();
			}

			// Token: 0x17000635 RID: 1589
			// (get) Token: 0x0600195B RID: 6491 RVA: 0x00067A7F File Offset: 0x00065C7F
			StyleSheet IEnumerator<StyleSheet>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600195C RID: 6492 RVA: 0x0000810E File Offset: 0x0000630E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000636 RID: 1590
			// (get) Token: 0x0600195D RID: 6493 RVA: 0x00067A7F File Offset: 0x00065C7F
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600195E RID: 6494 RVA: 0x00067A88 File Offset: 0x00065C88
			[DebuggerHidden]
			IEnumerator<StyleSheet> IEnumerable<StyleSheet>.GetEnumerator()
			{
				VisualTreeAsset.<get_stylesheets>d__21 <get_stylesheets>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<get_stylesheets>d__ = this;
				}
				else
				{
					<get_stylesheets>d__ = new VisualTreeAsset.<get_stylesheets>d__21(0);
					<get_stylesheets>d__.<>4__this = this;
				}
				return <get_stylesheets>d__;
			}

			// Token: 0x0600195F RID: 6495 RVA: 0x00067AD0 File Offset: 0x00065CD0
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<UnityEngine.UIElements.StyleSheet>.GetEnumerator();
			}

			// Token: 0x04000AE2 RID: 2786
			private int <>1__state;

			// Token: 0x04000AE3 RID: 2787
			private StyleSheet <>2__current;

			// Token: 0x04000AE4 RID: 2788
			private int <>l__initialThreadId;

			// Token: 0x04000AE5 RID: 2789
			public VisualTreeAsset <>4__this;

			// Token: 0x04000AE6 RID: 2790
			private HashSet<StyleSheet> <sent>5__1;

			// Token: 0x04000AE7 RID: 2791
			private List<VisualElementAsset>.Enumerator <>s__2;

			// Token: 0x04000AE8 RID: 2792
			private VisualElementAsset <vea>5__3;

			// Token: 0x04000AE9 RID: 2793
			private List<StyleSheet>.Enumerator <>s__4;

			// Token: 0x04000AEA RID: 2794
			private StyleSheet <stylesheet>5__5;

			// Token: 0x04000AEB RID: 2795
			private List<string>.Enumerator <>s__6;

			// Token: 0x04000AEC RID: 2796
			private string <stylesheetPath>5__7;

			// Token: 0x04000AED RID: 2797
			private StyleSheet <stylesheet>5__8;
		}

		// Token: 0x020002FD RID: 765
		[CompilerGenerated]
		private sealed class <>c__DisplayClass45_0
		{
			// Token: 0x06001960 RID: 6496 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c__DisplayClass45_0()
			{
			}

			// Token: 0x06001961 RID: 6497 RVA: 0x00067AD8 File Offset: 0x00065CD8
			internal bool <CloneSetupRecursively>b__0(VisualTreeAsset.SlotUsageEntry u)
			{
				return u.assetId == this.childVea.id;
			}

			// Token: 0x04000AEE RID: 2798
			public VisualElementAsset childVea;
		}

		// Token: 0x020002FE RID: 766
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass49_0
		{
			// Token: 0x04000AEF RID: 2799
			public VisualElementAsset asset;
		}
	}
}
