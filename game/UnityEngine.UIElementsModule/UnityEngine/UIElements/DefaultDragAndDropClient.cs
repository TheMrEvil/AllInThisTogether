using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A7 RID: 423
	internal class DefaultDragAndDropClient : DragAndDropData, IDragAndDrop
	{
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0003A52F File Offset: 0x0003872F
		public override DragVisualMode visualMode
		{
			get
			{
				return this.m_VisualMode;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x0003A537 File Offset: 0x00038737
		public override object source
		{
			get
			{
				return this.GetGenericData("__unity-drag-and-drop__source-view");
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x0003A544 File Offset: 0x00038744
		public override IEnumerable<Object> unityObjectReferences
		{
			get
			{
				return this.m_UnityObjectReferences;
			}
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0003A54C File Offset: 0x0003874C
		public override object GetGenericData(string key)
		{
			return this.m_GenericData.ContainsKey(key) ? this.m_GenericData[key] : null;
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0003A57B File Offset: 0x0003877B
		public override void SetGenericData(string key, object value)
		{
			this.m_GenericData[key] = value;
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0003A58C File Offset: 0x0003878C
		public void StartDrag(StartDragArgs args, Vector3 pointerPosition)
		{
			bool flag = args.unityObjectReferences != null;
			if (flag)
			{
				this.m_UnityObjectReferences = args.unityObjectReferences.ToArray<Object>();
			}
			this.m_VisualMode = args.visualMode;
			foreach (object obj in args.genericData)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.m_GenericData[(string)dictionaryEntry.Key] = dictionaryEntry.Value;
			}
			bool flag2 = string.IsNullOrEmpty(args.title);
			if (!flag2)
			{
				VisualElement visualElement = this.source as VisualElement;
				VisualElement visualElement2 = (visualElement != null) ? visualElement.panel.visualTree : null;
				bool flag3 = visualElement2 == null;
				if (!flag3)
				{
					if (this.m_DraggedInfoLabel == null)
					{
						this.m_DraggedInfoLabel = new Label
						{
							pickingMode = PickingMode.Ignore,
							style = 
							{
								position = Position.Absolute
							}
						};
					}
					this.m_DraggedInfoLabel.text = args.title;
					this.m_DraggedInfoLabel.style.top = pointerPosition.y;
					this.m_DraggedInfoLabel.style.left = pointerPosition.x;
					visualElement2.Add(this.m_DraggedInfoLabel);
				}
			}
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0003A700 File Offset: 0x00038900
		public void UpdateDrag(Vector3 pointerPosition)
		{
			bool flag = this.m_DraggedInfoLabel == null;
			if (!flag)
			{
				this.m_DraggedInfoLabel.style.top = pointerPosition.y;
				this.m_DraggedInfoLabel.style.left = pointerPosition.x;
			}
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00002166 File Offset: 0x00000366
		public void AcceptDrag()
		{
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0003A755 File Offset: 0x00038955
		public void SetVisualMode(DragVisualMode mode)
		{
			this.m_VisualMode = mode;
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0003A75F File Offset: 0x0003895F
		public void DragCleanup()
		{
			this.m_UnityObjectReferences = null;
			Hashtable genericData = this.m_GenericData;
			if (genericData != null)
			{
				genericData.Clear();
			}
			this.SetVisualMode(DragVisualMode.None);
			Label draggedInfoLabel = this.m_DraggedInfoLabel;
			if (draggedInfoLabel != null)
			{
				draggedInfoLabel.RemoveFromHierarchy();
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x0000BAF0 File Offset: 0x00009CF0
		public DragAndDropData data
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0003A795 File Offset: 0x00038995
		public DefaultDragAndDropClient()
		{
		}

		// Token: 0x04000681 RID: 1665
		private readonly Hashtable m_GenericData = new Hashtable();

		// Token: 0x04000682 RID: 1666
		private Label m_DraggedInfoLabel;

		// Token: 0x04000683 RID: 1667
		private DragVisualMode m_VisualMode;

		// Token: 0x04000684 RID: 1668
		private IEnumerable<Object> m_UnityObjectReferences;
	}
}
