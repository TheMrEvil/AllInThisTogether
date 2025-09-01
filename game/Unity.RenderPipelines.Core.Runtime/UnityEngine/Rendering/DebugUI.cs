using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;

namespace UnityEngine.Rendering
{
	// Token: 0x0200006D RID: 109
	public class DebugUI
	{
		// Token: 0x06000385 RID: 901 RVA: 0x00010FFD File Offset: 0x0000F1FD
		public DebugUI()
		{
		}

		// Token: 0x02000148 RID: 328
		public class Container : DebugUI.Widget, DebugUI.IContainer
		{
			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x06000857 RID: 2135 RVA: 0x00022DC3 File Offset: 0x00020FC3
			// (set) Token: 0x06000858 RID: 2136 RVA: 0x00022DCB File Offset: 0x00020FCB
			public ObservableList<DebugUI.Widget> children
			{
				[CompilerGenerated]
				get
				{
					return this.<children>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<children>k__BackingField = value;
				}
			}

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x06000859 RID: 2137 RVA: 0x00022DD4 File Offset: 0x00020FD4
			// (set) Token: 0x0600085A RID: 2138 RVA: 0x00022DDC File Offset: 0x00020FDC
			public override DebugUI.Panel panel
			{
				get
				{
					return this.m_Panel;
				}
				internal set
				{
					this.m_Panel = value;
					foreach (DebugUI.Widget widget in this.children)
					{
						widget.panel = value;
					}
				}
			}

			// Token: 0x0600085B RID: 2139 RVA: 0x00022E30 File Offset: 0x00021030
			public Container()
			{
				base.displayName = "";
				this.children = new ObservableList<DebugUI.Widget>();
				this.children.ItemAdded += this.OnItemAdded;
				this.children.ItemRemoved += this.OnItemRemoved;
			}

			// Token: 0x0600085C RID: 2140 RVA: 0x00022E89 File Offset: 0x00021089
			public Container(string displayName, ObservableList<DebugUI.Widget> children)
			{
				base.displayName = displayName;
				this.children = children;
				children.ItemAdded += this.OnItemAdded;
				children.ItemRemoved += this.OnItemRemoved;
			}

			// Token: 0x0600085D RID: 2141 RVA: 0x00022EC8 File Offset: 0x000210C8
			internal override void GenerateQueryPath()
			{
				base.GenerateQueryPath();
				foreach (DebugUI.Widget widget in this.children)
				{
					widget.GenerateQueryPath();
				}
			}

			// Token: 0x0600085E RID: 2142 RVA: 0x00022F18 File Offset: 0x00021118
			protected virtual void OnItemAdded(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				if (e.item != null)
				{
					e.item.panel = this.m_Panel;
					e.item.parent = this;
				}
				if (this.m_Panel != null)
				{
					this.m_Panel.SetDirty();
				}
			}

			// Token: 0x0600085F RID: 2143 RVA: 0x00022F52 File Offset: 0x00021152
			protected virtual void OnItemRemoved(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				if (e.item != null)
				{
					e.item.panel = null;
					e.item.parent = null;
				}
				if (this.m_Panel != null)
				{
					this.m_Panel.SetDirty();
				}
			}

			// Token: 0x06000860 RID: 2144 RVA: 0x00022F88 File Offset: 0x00021188
			public override int GetHashCode()
			{
				int num = 17;
				num = num * 23 + base.queryPath.GetHashCode();
				foreach (DebugUI.Widget widget in this.children)
				{
					num = num * 23 + widget.GetHashCode();
				}
				return num;
			}

			// Token: 0x04000519 RID: 1305
			[CompilerGenerated]
			private ObservableList<DebugUI.Widget> <children>k__BackingField;
		}

		// Token: 0x02000149 RID: 329
		public class Foldout : DebugUI.Container, DebugUI.IValueField
		{
			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x06000861 RID: 2145 RVA: 0x00022FF0 File Offset: 0x000211F0
			public bool isReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x06000862 RID: 2146 RVA: 0x00022FF3 File Offset: 0x000211F3
			// (set) Token: 0x06000863 RID: 2147 RVA: 0x00022FFB File Offset: 0x000211FB
			public string[] columnLabels
			{
				[CompilerGenerated]
				get
				{
					return this.<columnLabels>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<columnLabels>k__BackingField = value;
				}
			}

			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x06000864 RID: 2148 RVA: 0x00023004 File Offset: 0x00021204
			// (set) Token: 0x06000865 RID: 2149 RVA: 0x0002300C File Offset: 0x0002120C
			public string[] columnTooltips
			{
				[CompilerGenerated]
				get
				{
					return this.<columnTooltips>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<columnTooltips>k__BackingField = value;
				}
			}

			// Token: 0x06000866 RID: 2150 RVA: 0x00023015 File Offset: 0x00021215
			public Foldout()
			{
			}

			// Token: 0x06000867 RID: 2151 RVA: 0x0002301D File Offset: 0x0002121D
			public Foldout(string displayName, ObservableList<DebugUI.Widget> children, string[] columnLabels = null, string[] columnTooltips = null) : base(displayName, children)
			{
				this.columnLabels = columnLabels;
				this.columnTooltips = columnTooltips;
			}

			// Token: 0x06000868 RID: 2152 RVA: 0x00023036 File Offset: 0x00021236
			public bool GetValue()
			{
				return this.opened;
			}

			// Token: 0x06000869 RID: 2153 RVA: 0x0002303E File Offset: 0x0002123E
			object DebugUI.IValueField.GetValue()
			{
				return this.GetValue();
			}

			// Token: 0x0600086A RID: 2154 RVA: 0x0002304B File Offset: 0x0002124B
			public void SetValue(object value)
			{
				this.SetValue((bool)value);
			}

			// Token: 0x0600086B RID: 2155 RVA: 0x00023059 File Offset: 0x00021259
			public object ValidateValue(object value)
			{
				return value;
			}

			// Token: 0x0600086C RID: 2156 RVA: 0x0002305C File Offset: 0x0002125C
			public void SetValue(bool value)
			{
				this.opened = value;
			}

			// Token: 0x0400051A RID: 1306
			public bool opened;

			// Token: 0x0400051B RID: 1307
			public bool isHeader;

			// Token: 0x0400051C RID: 1308
			public List<DebugUI.Foldout.ContextMenuItem> contextMenuItems;

			// Token: 0x0400051D RID: 1309
			[CompilerGenerated]
			private string[] <columnLabels>k__BackingField;

			// Token: 0x0400051E RID: 1310
			[CompilerGenerated]
			private string[] <columnTooltips>k__BackingField;

			// Token: 0x0200018F RID: 399
			public struct ContextMenuItem
			{
				// Token: 0x040005D8 RID: 1496
				public string displayName;

				// Token: 0x040005D9 RID: 1497
				public Action action;
			}
		}

		// Token: 0x0200014A RID: 330
		public class HBox : DebugUI.Container
		{
			// Token: 0x0600086D RID: 2157 RVA: 0x00023065 File Offset: 0x00021265
			public HBox()
			{
				base.displayName = "HBox";
			}
		}

		// Token: 0x0200014B RID: 331
		public class VBox : DebugUI.Container
		{
			// Token: 0x0600086E RID: 2158 RVA: 0x00023078 File Offset: 0x00021278
			public VBox()
			{
				base.displayName = "VBox";
			}
		}

		// Token: 0x0200014C RID: 332
		public class Table : DebugUI.Container
		{
			// Token: 0x0600086F RID: 2159 RVA: 0x0002308B File Offset: 0x0002128B
			public Table()
			{
				base.displayName = "Array";
			}

			// Token: 0x06000870 RID: 2160 RVA: 0x000230A0 File Offset: 0x000212A0
			public void SetColumnVisibility(int index, bool visible)
			{
				bool[] visibleColumns = this.VisibleColumns;
				if (index < 0 || index > visibleColumns.Length)
				{
					return;
				}
				visibleColumns[index] = visible;
			}

			// Token: 0x06000871 RID: 2161 RVA: 0x000230C4 File Offset: 0x000212C4
			public bool GetColumnVisibility(int index)
			{
				bool[] visibleColumns = this.VisibleColumns;
				return index >= 0 && index <= visibleColumns.Length && visibleColumns[index];
			}

			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x06000872 RID: 2162 RVA: 0x000230E8 File Offset: 0x000212E8
			public bool[] VisibleColumns
			{
				get
				{
					if (this.m_Header != null)
					{
						return this.m_Header;
					}
					int num = 0;
					if (base.children.Count != 0)
					{
						num = ((DebugUI.Container)base.children[0]).children.Count;
						for (int i = 1; i < base.children.Count; i++)
						{
							if (((DebugUI.Container)base.children[i]).children.Count != num)
							{
								Debug.LogError("All rows must have the same number of children.");
								return null;
							}
						}
					}
					this.m_Header = new bool[num];
					for (int j = 0; j < num; j++)
					{
						this.m_Header[j] = true;
					}
					return this.m_Header;
				}
			}

			// Token: 0x06000873 RID: 2163 RVA: 0x00023196 File Offset: 0x00021396
			protected override void OnItemAdded(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				base.OnItemAdded(sender, e);
				this.m_Header = null;
			}

			// Token: 0x06000874 RID: 2164 RVA: 0x000231A7 File Offset: 0x000213A7
			protected override void OnItemRemoved(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				base.OnItemRemoved(sender, e);
				this.m_Header = null;
			}

			// Token: 0x0400051F RID: 1311
			public bool isReadOnly;

			// Token: 0x04000520 RID: 1312
			private bool[] m_Header;

			// Token: 0x02000190 RID: 400
			public class Row : DebugUI.Foldout
			{
				// Token: 0x0600093D RID: 2365 RVA: 0x00024C61 File Offset: 0x00022E61
				public Row()
				{
					base.displayName = "Row";
				}
			}
		}

		// Token: 0x0200014D RID: 333
		[Flags]
		public enum Flags
		{
			// Token: 0x04000522 RID: 1314
			None = 0,
			// Token: 0x04000523 RID: 1315
			EditorOnly = 2,
			// Token: 0x04000524 RID: 1316
			RuntimeOnly = 4,
			// Token: 0x04000525 RID: 1317
			EditorForceUpdate = 8
		}

		// Token: 0x0200014E RID: 334
		public abstract class Widget
		{
			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x06000875 RID: 2165 RVA: 0x000231B8 File Offset: 0x000213B8
			// (set) Token: 0x06000876 RID: 2166 RVA: 0x000231C0 File Offset: 0x000213C0
			public virtual DebugUI.Panel panel
			{
				get
				{
					return this.m_Panel;
				}
				internal set
				{
					this.m_Panel = value;
				}
			}

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x06000877 RID: 2167 RVA: 0x000231C9 File Offset: 0x000213C9
			// (set) Token: 0x06000878 RID: 2168 RVA: 0x000231D1 File Offset: 0x000213D1
			public virtual DebugUI.IContainer parent
			{
				get
				{
					return this.m_Parent;
				}
				internal set
				{
					this.m_Parent = value;
				}
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x06000879 RID: 2169 RVA: 0x000231DA File Offset: 0x000213DA
			// (set) Token: 0x0600087A RID: 2170 RVA: 0x000231E2 File Offset: 0x000213E2
			public DebugUI.Flags flags
			{
				[CompilerGenerated]
				get
				{
					return this.<flags>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<flags>k__BackingField = value;
				}
			}

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x0600087B RID: 2171 RVA: 0x000231EB File Offset: 0x000213EB
			// (set) Token: 0x0600087C RID: 2172 RVA: 0x000231F3 File Offset: 0x000213F3
			public string displayName
			{
				[CompilerGenerated]
				get
				{
					return this.<displayName>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<displayName>k__BackingField = value;
				}
			}

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x0600087D RID: 2173 RVA: 0x000231FC File Offset: 0x000213FC
			// (set) Token: 0x0600087E RID: 2174 RVA: 0x00023204 File Offset: 0x00021404
			public string tooltip
			{
				[CompilerGenerated]
				get
				{
					return this.<tooltip>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<tooltip>k__BackingField = value;
				}
			}

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x0600087F RID: 2175 RVA: 0x0002320D File Offset: 0x0002140D
			// (set) Token: 0x06000880 RID: 2176 RVA: 0x00023215 File Offset: 0x00021415
			public string queryPath
			{
				[CompilerGenerated]
				get
				{
					return this.<queryPath>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<queryPath>k__BackingField = value;
				}
			}

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x06000881 RID: 2177 RVA: 0x0002321E File Offset: 0x0002141E
			public bool isEditorOnly
			{
				get
				{
					return this.flags.HasFlag(DebugUI.Flags.EditorOnly);
				}
			}

			// Token: 0x17000100 RID: 256
			// (get) Token: 0x06000882 RID: 2178 RVA: 0x00023236 File Offset: 0x00021436
			public bool isRuntimeOnly
			{
				get
				{
					return this.flags.HasFlag(DebugUI.Flags.RuntimeOnly);
				}
			}

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x06000883 RID: 2179 RVA: 0x0002324E File Offset: 0x0002144E
			public bool isInactiveInEditor
			{
				get
				{
					return this.isRuntimeOnly && !Application.isPlaying;
				}
			}

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x06000884 RID: 2180 RVA: 0x00023262 File Offset: 0x00021462
			public bool isHidden
			{
				get
				{
					Func<bool> func = this.isHiddenCallback;
					return func != null && func();
				}
			}

			// Token: 0x06000885 RID: 2181 RVA: 0x00023275 File Offset: 0x00021475
			internal virtual void GenerateQueryPath()
			{
				this.queryPath = this.displayName.Trim();
				if (this.m_Parent != null)
				{
					this.queryPath = this.m_Parent.queryPath + " -> " + this.queryPath;
				}
			}

			// Token: 0x06000886 RID: 2182 RVA: 0x000232B1 File Offset: 0x000214B1
			public override int GetHashCode()
			{
				return this.queryPath.GetHashCode();
			}

			// Token: 0x17000103 RID: 259
			// (set) Token: 0x06000887 RID: 2183 RVA: 0x000232BE File Offset: 0x000214BE
			public DebugUI.Widget.NameAndTooltip nameAndTooltip
			{
				set
				{
					this.displayName = value.name;
					this.tooltip = value.tooltip;
				}
			}

			// Token: 0x06000888 RID: 2184 RVA: 0x000232D8 File Offset: 0x000214D8
			protected Widget()
			{
			}

			// Token: 0x04000526 RID: 1318
			protected DebugUI.Panel m_Panel;

			// Token: 0x04000527 RID: 1319
			protected DebugUI.IContainer m_Parent;

			// Token: 0x04000528 RID: 1320
			[CompilerGenerated]
			private DebugUI.Flags <flags>k__BackingField;

			// Token: 0x04000529 RID: 1321
			[CompilerGenerated]
			private string <displayName>k__BackingField;

			// Token: 0x0400052A RID: 1322
			[CompilerGenerated]
			private string <tooltip>k__BackingField;

			// Token: 0x0400052B RID: 1323
			[CompilerGenerated]
			private string <queryPath>k__BackingField;

			// Token: 0x0400052C RID: 1324
			public Func<bool> isHiddenCallback;

			// Token: 0x02000191 RID: 401
			public struct NameAndTooltip
			{
				// Token: 0x040005DA RID: 1498
				public string name;

				// Token: 0x040005DB RID: 1499
				public string tooltip;
			}
		}

		// Token: 0x0200014F RID: 335
		public interface IContainer
		{
			// Token: 0x17000104 RID: 260
			// (get) Token: 0x06000889 RID: 2185
			ObservableList<DebugUI.Widget> children { get; }

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x0600088A RID: 2186
			// (set) Token: 0x0600088B RID: 2187
			string displayName { get; set; }

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x0600088C RID: 2188
			string queryPath { get; }
		}

		// Token: 0x02000150 RID: 336
		public interface IValueField
		{
			// Token: 0x0600088D RID: 2189
			object GetValue();

			// Token: 0x0600088E RID: 2190
			void SetValue(object value);

			// Token: 0x0600088F RID: 2191
			object ValidateValue(object value);
		}

		// Token: 0x02000151 RID: 337
		public class Button : DebugUI.Widget
		{
			// Token: 0x17000107 RID: 263
			// (get) Token: 0x06000890 RID: 2192 RVA: 0x000232E0 File Offset: 0x000214E0
			// (set) Token: 0x06000891 RID: 2193 RVA: 0x000232E8 File Offset: 0x000214E8
			public Action action
			{
				[CompilerGenerated]
				get
				{
					return this.<action>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<action>k__BackingField = value;
				}
			}

			// Token: 0x06000892 RID: 2194 RVA: 0x000232F1 File Offset: 0x000214F1
			public Button()
			{
			}

			// Token: 0x0400052D RID: 1325
			[CompilerGenerated]
			private Action <action>k__BackingField;
		}

		// Token: 0x02000152 RID: 338
		public class Value : DebugUI.Widget
		{
			// Token: 0x17000108 RID: 264
			// (get) Token: 0x06000893 RID: 2195 RVA: 0x000232F9 File Offset: 0x000214F9
			// (set) Token: 0x06000894 RID: 2196 RVA: 0x00023301 File Offset: 0x00021501
			public Func<object> getter
			{
				[CompilerGenerated]
				get
				{
					return this.<getter>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<getter>k__BackingField = value;
				}
			}

			// Token: 0x06000895 RID: 2197 RVA: 0x0002330A File Offset: 0x0002150A
			public Value()
			{
				base.displayName = "";
			}

			// Token: 0x06000896 RID: 2198 RVA: 0x00023328 File Offset: 0x00021528
			public object GetValue()
			{
				return this.getter();
			}

			// Token: 0x0400052E RID: 1326
			[CompilerGenerated]
			private Func<object> <getter>k__BackingField;

			// Token: 0x0400052F RID: 1327
			public float refreshRate = 0.1f;
		}

		// Token: 0x02000153 RID: 339
		public abstract class Field<T> : DebugUI.Widget, DebugUI.IValueField
		{
			// Token: 0x17000109 RID: 265
			// (get) Token: 0x06000897 RID: 2199 RVA: 0x00023335 File Offset: 0x00021535
			// (set) Token: 0x06000898 RID: 2200 RVA: 0x0002333D File Offset: 0x0002153D
			public Func<T> getter
			{
				[CompilerGenerated]
				get
				{
					return this.<getter>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<getter>k__BackingField = value;
				}
			}

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x06000899 RID: 2201 RVA: 0x00023346 File Offset: 0x00021546
			// (set) Token: 0x0600089A RID: 2202 RVA: 0x0002334E File Offset: 0x0002154E
			public Action<T> setter
			{
				[CompilerGenerated]
				get
				{
					return this.<setter>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<setter>k__BackingField = value;
				}
			}

			// Token: 0x0600089B RID: 2203 RVA: 0x00023357 File Offset: 0x00021557
			object DebugUI.IValueField.ValidateValue(object value)
			{
				return this.ValidateValue((T)((object)value));
			}

			// Token: 0x0600089C RID: 2204 RVA: 0x0002336A File Offset: 0x0002156A
			public virtual T ValidateValue(T value)
			{
				return value;
			}

			// Token: 0x0600089D RID: 2205 RVA: 0x0002336D File Offset: 0x0002156D
			object DebugUI.IValueField.GetValue()
			{
				return this.GetValue();
			}

			// Token: 0x0600089E RID: 2206 RVA: 0x0002337A File Offset: 0x0002157A
			public T GetValue()
			{
				return this.getter();
			}

			// Token: 0x0600089F RID: 2207 RVA: 0x00023387 File Offset: 0x00021587
			public void SetValue(object value)
			{
				this.SetValue((T)((object)value));
			}

			// Token: 0x060008A0 RID: 2208 RVA: 0x00023398 File Offset: 0x00021598
			public void SetValue(T value)
			{
				T t = this.ValidateValue(value);
				if (!t.Equals(this.getter()))
				{
					this.setter(t);
					Action<DebugUI.Field<T>, T> action = this.onValueChanged;
					if (action == null)
					{
						return;
					}
					action(this, t);
				}
			}

			// Token: 0x060008A1 RID: 2209 RVA: 0x000233EA File Offset: 0x000215EA
			protected Field()
			{
			}

			// Token: 0x04000530 RID: 1328
			[CompilerGenerated]
			private Func<T> <getter>k__BackingField;

			// Token: 0x04000531 RID: 1329
			[CompilerGenerated]
			private Action<T> <setter>k__BackingField;

			// Token: 0x04000532 RID: 1330
			public Action<DebugUI.Field<T>, T> onValueChanged;
		}

		// Token: 0x02000154 RID: 340
		public class BoolField : DebugUI.Field<bool>
		{
			// Token: 0x060008A2 RID: 2210 RVA: 0x000233F2 File Offset: 0x000215F2
			public BoolField()
			{
			}
		}

		// Token: 0x02000155 RID: 341
		public class HistoryBoolField : DebugUI.BoolField
		{
			// Token: 0x1700010B RID: 267
			// (get) Token: 0x060008A3 RID: 2211 RVA: 0x000233FA File Offset: 0x000215FA
			// (set) Token: 0x060008A4 RID: 2212 RVA: 0x00023402 File Offset: 0x00021602
			public Func<bool>[] historyGetter
			{
				[CompilerGenerated]
				get
				{
					return this.<historyGetter>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<historyGetter>k__BackingField = value;
				}
			}

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0002340B File Offset: 0x0002160B
			public int historyDepth
			{
				get
				{
					Func<bool>[] historyGetter = this.historyGetter;
					if (historyGetter == null)
					{
						return 0;
					}
					return historyGetter.Length;
				}
			}

			// Token: 0x060008A6 RID: 2214 RVA: 0x0002341B File Offset: 0x0002161B
			public bool GetHistoryValue(int historyIndex)
			{
				return this.historyGetter[historyIndex]();
			}

			// Token: 0x060008A7 RID: 2215 RVA: 0x0002342A File Offset: 0x0002162A
			public HistoryBoolField()
			{
			}

			// Token: 0x04000533 RID: 1331
			[CompilerGenerated]
			private Func<bool>[] <historyGetter>k__BackingField;
		}

		// Token: 0x02000156 RID: 342
		public class IntField : DebugUI.Field<int>
		{
			// Token: 0x060008A8 RID: 2216 RVA: 0x00023432 File Offset: 0x00021632
			public override int ValidateValue(int value)
			{
				if (this.min != null)
				{
					value = Mathf.Max(value, this.min());
				}
				if (this.max != null)
				{
					value = Mathf.Min(value, this.max());
				}
				return value;
			}

			// Token: 0x060008A9 RID: 2217 RVA: 0x0002346B File Offset: 0x0002166B
			public IntField()
			{
			}

			// Token: 0x04000534 RID: 1332
			public Func<int> min;

			// Token: 0x04000535 RID: 1333
			public Func<int> max;

			// Token: 0x04000536 RID: 1334
			public int incStep = 1;

			// Token: 0x04000537 RID: 1335
			public int intStepMult = 10;
		}

		// Token: 0x02000157 RID: 343
		public class UIntField : DebugUI.Field<uint>
		{
			// Token: 0x060008AA RID: 2218 RVA: 0x00023482 File Offset: 0x00021682
			public override uint ValidateValue(uint value)
			{
				if (this.min != null)
				{
					value = (uint)Mathf.Max((int)value, (int)this.min());
				}
				if (this.max != null)
				{
					value = (uint)Mathf.Min((int)value, (int)this.max());
				}
				return value;
			}

			// Token: 0x060008AB RID: 2219 RVA: 0x000234BB File Offset: 0x000216BB
			public UIntField()
			{
			}

			// Token: 0x04000538 RID: 1336
			public Func<uint> min;

			// Token: 0x04000539 RID: 1337
			public Func<uint> max;

			// Token: 0x0400053A RID: 1338
			public uint incStep = 1U;

			// Token: 0x0400053B RID: 1339
			public uint intStepMult = 10U;
		}

		// Token: 0x02000158 RID: 344
		public class FloatField : DebugUI.Field<float>
		{
			// Token: 0x060008AC RID: 2220 RVA: 0x000234D2 File Offset: 0x000216D2
			public override float ValidateValue(float value)
			{
				if (this.min != null)
				{
					value = Mathf.Max(value, this.min());
				}
				if (this.max != null)
				{
					value = Mathf.Min(value, this.max());
				}
				return value;
			}

			// Token: 0x060008AD RID: 2221 RVA: 0x0002350B File Offset: 0x0002170B
			public FloatField()
			{
			}

			// Token: 0x0400053C RID: 1340
			public Func<float> min;

			// Token: 0x0400053D RID: 1341
			public Func<float> max;

			// Token: 0x0400053E RID: 1342
			public float incStep = 0.1f;

			// Token: 0x0400053F RID: 1343
			public float incStepMult = 10f;

			// Token: 0x04000540 RID: 1344
			public int decimals = 3;
		}

		// Token: 0x02000159 RID: 345
		private static class EnumUtility
		{
			// Token: 0x060008AE RID: 2222 RVA: 0x00023530 File Offset: 0x00021730
			internal static GUIContent[] MakeEnumNames(Type enumType)
			{
				return enumType.GetFields(BindingFlags.Static | BindingFlags.Public).Select(delegate(FieldInfo fieldInfo)
				{
					object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(InspectorNameAttribute), false);
					if (customAttributes.Length != 0)
					{
						return new GUIContent(((InspectorNameAttribute)customAttributes.First<object>()).displayName);
					}
					return new GUIContent(Regex.Replace(fieldInfo.Name, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 "));
				}).ToArray<GUIContent>();
			}

			// Token: 0x060008AF RID: 2223 RVA: 0x00023564 File Offset: 0x00021764
			internal static int[] MakeEnumValues(Type enumType)
			{
				Array values = Enum.GetValues(enumType);
				int[] array = new int[values.Length];
				for (int i = 0; i < values.Length; i++)
				{
					array[i] = (int)values.GetValue(i);
				}
				return array;
			}

			// Token: 0x02000192 RID: 402
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x0600093E RID: 2366 RVA: 0x00024C74 File Offset: 0x00022E74
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x0600093F RID: 2367 RVA: 0x00024C80 File Offset: 0x00022E80
				public <>c()
				{
				}

				// Token: 0x06000940 RID: 2368 RVA: 0x00024C88 File Offset: 0x00022E88
				internal GUIContent <MakeEnumNames>b__0_0(FieldInfo fieldInfo)
				{
					object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(InspectorNameAttribute), false);
					if (customAttributes.Length != 0)
					{
						return new GUIContent(((InspectorNameAttribute)customAttributes.First<object>()).displayName);
					}
					return new GUIContent(Regex.Replace(fieldInfo.Name, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 "));
				}

				// Token: 0x040005DC RID: 1500
				public static readonly DebugUI.EnumUtility.<>c <>9 = new DebugUI.EnumUtility.<>c();

				// Token: 0x040005DD RID: 1501
				public static Func<FieldInfo, GUIContent> <>9__0_0;
			}
		}

		// Token: 0x0200015A RID: 346
		public class EnumField : DebugUI.Field<int>
		{
			// Token: 0x1700010D RID: 269
			// (get) Token: 0x060008B0 RID: 2224 RVA: 0x000235A5 File Offset: 0x000217A5
			// (set) Token: 0x060008B1 RID: 2225 RVA: 0x000235AD File Offset: 0x000217AD
			public Func<int> getIndex
			{
				[CompilerGenerated]
				get
				{
					return this.<getIndex>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<getIndex>k__BackingField = value;
				}
			}

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x060008B2 RID: 2226 RVA: 0x000235B6 File Offset: 0x000217B6
			// (set) Token: 0x060008B3 RID: 2227 RVA: 0x000235BE File Offset: 0x000217BE
			public Action<int> setIndex
			{
				[CompilerGenerated]
				get
				{
					return this.<setIndex>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<setIndex>k__BackingField = value;
				}
			}

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x060008B4 RID: 2228 RVA: 0x000235C7 File Offset: 0x000217C7
			// (set) Token: 0x060008B5 RID: 2229 RVA: 0x000235D4 File Offset: 0x000217D4
			public int currentIndex
			{
				get
				{
					return this.getIndex();
				}
				set
				{
					this.setIndex(value);
				}
			}

			// Token: 0x17000110 RID: 272
			// (set) Token: 0x060008B6 RID: 2230 RVA: 0x000235E2 File Offset: 0x000217E2
			public Type autoEnum
			{
				set
				{
					this.enumNames = DebugUI.EnumUtility.MakeEnumNames(value);
					this.enumValues = DebugUI.EnumUtility.MakeEnumValues(value);
					this.InitIndexes();
					this.InitQuickSeparators();
				}
			}

			// Token: 0x060008B7 RID: 2231 RVA: 0x00023608 File Offset: 0x00021808
			internal void InitQuickSeparators()
			{
				IEnumerable<string> source = this.enumNames.Select(delegate(GUIContent x)
				{
					string[] array = x.text.Split('/', StringSplitOptions.None);
					if (array.Length == 1)
					{
						return "";
					}
					return array[0];
				});
				this.quickSeparators = new int[source.Distinct<string>().Count<string>()];
				string a = null;
				int i = 0;
				int num = 0;
				while (i < this.quickSeparators.Length)
				{
					string text = source.ElementAt(num);
					while (a == text)
					{
						text = source.ElementAt(++num);
					}
					a = text;
					this.quickSeparators[i] = num++;
					i++;
				}
			}

			// Token: 0x060008B8 RID: 2232 RVA: 0x000236A0 File Offset: 0x000218A0
			internal void InitIndexes()
			{
				if (this.enumNames == null)
				{
					this.enumNames = new GUIContent[0];
				}
				this.indexes = new int[this.enumNames.Length];
				for (int i = 0; i < this.enumNames.Length; i++)
				{
					this.indexes[i] = i;
				}
			}

			// Token: 0x060008B9 RID: 2233 RVA: 0x000236F0 File Offset: 0x000218F0
			public EnumField()
			{
			}

			// Token: 0x04000541 RID: 1345
			public GUIContent[] enumNames;

			// Token: 0x04000542 RID: 1346
			public int[] enumValues;

			// Token: 0x04000543 RID: 1347
			internal int[] quickSeparators;

			// Token: 0x04000544 RID: 1348
			internal int[] indexes;

			// Token: 0x04000545 RID: 1349
			[CompilerGenerated]
			private Func<int> <getIndex>k__BackingField;

			// Token: 0x04000546 RID: 1350
			[CompilerGenerated]
			private Action<int> <setIndex>k__BackingField;

			// Token: 0x02000193 RID: 403
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06000941 RID: 2369 RVA: 0x00024CDB File Offset: 0x00022EDB
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06000942 RID: 2370 RVA: 0x00024CE7 File Offset: 0x00022EE7
				public <>c()
				{
				}

				// Token: 0x06000943 RID: 2371 RVA: 0x00024CF0 File Offset: 0x00022EF0
				internal string <InitQuickSeparators>b__17_0(GUIContent x)
				{
					string[] array = x.text.Split('/', StringSplitOptions.None);
					if (array.Length == 1)
					{
						return "";
					}
					return array[0];
				}

				// Token: 0x040005DE RID: 1502
				public static readonly DebugUI.EnumField.<>c <>9 = new DebugUI.EnumField.<>c();

				// Token: 0x040005DF RID: 1503
				public static Func<GUIContent, string> <>9__17_0;
			}
		}

		// Token: 0x0200015B RID: 347
		public class HistoryEnumField : DebugUI.EnumField
		{
			// Token: 0x17000111 RID: 273
			// (get) Token: 0x060008BA RID: 2234 RVA: 0x000236F8 File Offset: 0x000218F8
			// (set) Token: 0x060008BB RID: 2235 RVA: 0x00023700 File Offset: 0x00021900
			public Func<int>[] historyIndexGetter
			{
				[CompilerGenerated]
				get
				{
					return this.<historyIndexGetter>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<historyIndexGetter>k__BackingField = value;
				}
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x060008BC RID: 2236 RVA: 0x00023709 File Offset: 0x00021909
			public int historyDepth
			{
				get
				{
					Func<int>[] historyIndexGetter = this.historyIndexGetter;
					if (historyIndexGetter == null)
					{
						return 0;
					}
					return historyIndexGetter.Length;
				}
			}

			// Token: 0x060008BD RID: 2237 RVA: 0x00023719 File Offset: 0x00021919
			public int GetHistoryValue(int historyIndex)
			{
				return this.historyIndexGetter[historyIndex]();
			}

			// Token: 0x060008BE RID: 2238 RVA: 0x00023728 File Offset: 0x00021928
			public HistoryEnumField()
			{
			}

			// Token: 0x04000547 RID: 1351
			[CompilerGenerated]
			private Func<int>[] <historyIndexGetter>k__BackingField;
		}

		// Token: 0x0200015C RID: 348
		public class BitField : DebugUI.Field<Enum>
		{
			// Token: 0x17000113 RID: 275
			// (get) Token: 0x060008BF RID: 2239 RVA: 0x00023730 File Offset: 0x00021930
			// (set) Token: 0x060008C0 RID: 2240 RVA: 0x00023738 File Offset: 0x00021938
			public GUIContent[] enumNames
			{
				[CompilerGenerated]
				get
				{
					return this.<enumNames>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<enumNames>k__BackingField = value;
				}
			}

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00023741 File Offset: 0x00021941
			// (set) Token: 0x060008C2 RID: 2242 RVA: 0x00023749 File Offset: 0x00021949
			public int[] enumValues
			{
				[CompilerGenerated]
				get
				{
					return this.<enumValues>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<enumValues>k__BackingField = value;
				}
			}

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00023752 File Offset: 0x00021952
			// (set) Token: 0x060008C4 RID: 2244 RVA: 0x0002375A File Offset: 0x0002195A
			public Type enumType
			{
				get
				{
					return this.m_EnumType;
				}
				set
				{
					this.m_EnumType = value;
					this.enumNames = DebugUI.EnumUtility.MakeEnumNames(value);
					this.enumValues = DebugUI.EnumUtility.MakeEnumValues(value);
				}
			}

			// Token: 0x060008C5 RID: 2245 RVA: 0x0002377B File Offset: 0x0002197B
			public BitField()
			{
			}

			// Token: 0x04000548 RID: 1352
			[CompilerGenerated]
			private GUIContent[] <enumNames>k__BackingField;

			// Token: 0x04000549 RID: 1353
			[CompilerGenerated]
			private int[] <enumValues>k__BackingField;

			// Token: 0x0400054A RID: 1354
			private Type m_EnumType;
		}

		// Token: 0x0200015D RID: 349
		public class ColorField : DebugUI.Field<Color>
		{
			// Token: 0x060008C6 RID: 2246 RVA: 0x00023784 File Offset: 0x00021984
			public override Color ValidateValue(Color value)
			{
				if (!this.hdr)
				{
					value.r = Mathf.Clamp01(value.r);
					value.g = Mathf.Clamp01(value.g);
					value.b = Mathf.Clamp01(value.b);
					value.a = Mathf.Clamp01(value.a);
				}
				return value;
			}

			// Token: 0x060008C7 RID: 2247 RVA: 0x000237E2 File Offset: 0x000219E2
			public ColorField()
			{
			}

			// Token: 0x0400054B RID: 1355
			public bool hdr;

			// Token: 0x0400054C RID: 1356
			public bool showAlpha = true;

			// Token: 0x0400054D RID: 1357
			public bool showPicker = true;

			// Token: 0x0400054E RID: 1358
			public float incStep = 0.025f;

			// Token: 0x0400054F RID: 1359
			public float incStepMult = 5f;

			// Token: 0x04000550 RID: 1360
			public int decimals = 3;
		}

		// Token: 0x0200015E RID: 350
		public class Vector2Field : DebugUI.Field<Vector2>
		{
			// Token: 0x060008C8 RID: 2248 RVA: 0x00023815 File Offset: 0x00021A15
			public Vector2Field()
			{
			}

			// Token: 0x04000551 RID: 1361
			public float incStep = 0.025f;

			// Token: 0x04000552 RID: 1362
			public float incStepMult = 10f;

			// Token: 0x04000553 RID: 1363
			public int decimals = 3;
		}

		// Token: 0x0200015F RID: 351
		public class Vector3Field : DebugUI.Field<Vector3>
		{
			// Token: 0x060008C9 RID: 2249 RVA: 0x0002383A File Offset: 0x00021A3A
			public Vector3Field()
			{
			}

			// Token: 0x04000554 RID: 1364
			public float incStep = 0.025f;

			// Token: 0x04000555 RID: 1365
			public float incStepMult = 10f;

			// Token: 0x04000556 RID: 1366
			public int decimals = 3;
		}

		// Token: 0x02000160 RID: 352
		public class Vector4Field : DebugUI.Field<Vector4>
		{
			// Token: 0x060008CA RID: 2250 RVA: 0x0002385F File Offset: 0x00021A5F
			public Vector4Field()
			{
			}

			// Token: 0x04000557 RID: 1367
			public float incStep = 0.025f;

			// Token: 0x04000558 RID: 1368
			public float incStepMult = 10f;

			// Token: 0x04000559 RID: 1369
			public int decimals = 3;
		}

		// Token: 0x02000161 RID: 353
		public class MessageBox : DebugUI.Widget
		{
			// Token: 0x060008CB RID: 2251 RVA: 0x00023884 File Offset: 0x00021A84
			public MessageBox()
			{
			}

			// Token: 0x0400055A RID: 1370
			public DebugUI.MessageBox.Style style;

			// Token: 0x02000194 RID: 404
			public enum Style
			{
				// Token: 0x040005E1 RID: 1505
				Info,
				// Token: 0x040005E2 RID: 1506
				Warning,
				// Token: 0x040005E3 RID: 1507
				Error
			}
		}

		// Token: 0x02000162 RID: 354
		public class Panel : DebugUI.IContainer, IComparable<DebugUI.Panel>
		{
			// Token: 0x17000116 RID: 278
			// (get) Token: 0x060008CC RID: 2252 RVA: 0x0002388C File Offset: 0x00021A8C
			// (set) Token: 0x060008CD RID: 2253 RVA: 0x00023894 File Offset: 0x00021A94
			public DebugUI.Flags flags
			{
				[CompilerGenerated]
				get
				{
					return this.<flags>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<flags>k__BackingField = value;
				}
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x060008CE RID: 2254 RVA: 0x0002389D File Offset: 0x00021A9D
			// (set) Token: 0x060008CF RID: 2255 RVA: 0x000238A5 File Offset: 0x00021AA5
			public string displayName
			{
				[CompilerGenerated]
				get
				{
					return this.<displayName>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<displayName>k__BackingField = value;
				}
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x060008D0 RID: 2256 RVA: 0x000238AE File Offset: 0x00021AAE
			// (set) Token: 0x060008D1 RID: 2257 RVA: 0x000238B6 File Offset: 0x00021AB6
			public int groupIndex
			{
				[CompilerGenerated]
				get
				{
					return this.<groupIndex>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<groupIndex>k__BackingField = value;
				}
			}

			// Token: 0x17000119 RID: 281
			// (get) Token: 0x060008D2 RID: 2258 RVA: 0x000238BF File Offset: 0x00021ABF
			public string queryPath
			{
				get
				{
					return this.displayName;
				}
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x060008D3 RID: 2259 RVA: 0x000238C7 File Offset: 0x00021AC7
			public bool isEditorOnly
			{
				get
				{
					return (this.flags & DebugUI.Flags.EditorOnly) > DebugUI.Flags.None;
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x060008D4 RID: 2260 RVA: 0x000238D4 File Offset: 0x00021AD4
			public bool isRuntimeOnly
			{
				get
				{
					return (this.flags & DebugUI.Flags.RuntimeOnly) > DebugUI.Flags.None;
				}
			}

			// Token: 0x1700011C RID: 284
			// (get) Token: 0x060008D5 RID: 2261 RVA: 0x000238E1 File Offset: 0x00021AE1
			public bool isInactiveInEditor
			{
				get
				{
					return this.isRuntimeOnly && !Application.isPlaying;
				}
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x060008D6 RID: 2262 RVA: 0x000238F5 File Offset: 0x00021AF5
			public bool editorForceUpdate
			{
				get
				{
					return (this.flags & DebugUI.Flags.EditorForceUpdate) > DebugUI.Flags.None;
				}
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x060008D7 RID: 2263 RVA: 0x00023902 File Offset: 0x00021B02
			// (set) Token: 0x060008D8 RID: 2264 RVA: 0x0002390A File Offset: 0x00021B0A
			public ObservableList<DebugUI.Widget> children
			{
				[CompilerGenerated]
				get
				{
					return this.<children>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<children>k__BackingField = value;
				}
			}

			// Token: 0x1400000A RID: 10
			// (add) Token: 0x060008D9 RID: 2265 RVA: 0x00023914 File Offset: 0x00021B14
			// (remove) Token: 0x060008DA RID: 2266 RVA: 0x0002394C File Offset: 0x00021B4C
			public event Action<DebugUI.Panel> onSetDirty
			{
				[CompilerGenerated]
				add
				{
					Action<DebugUI.Panel> action = this.onSetDirty;
					Action<DebugUI.Panel> action2;
					do
					{
						action2 = action;
						Action<DebugUI.Panel> value2 = (Action<DebugUI.Panel>)Delegate.Combine(action2, value);
						action = Interlocked.CompareExchange<Action<DebugUI.Panel>>(ref this.onSetDirty, value2, action2);
					}
					while (action != action2);
				}
				[CompilerGenerated]
				remove
				{
					Action<DebugUI.Panel> action = this.onSetDirty;
					Action<DebugUI.Panel> action2;
					do
					{
						action2 = action;
						Action<DebugUI.Panel> value2 = (Action<DebugUI.Panel>)Delegate.Remove(action2, value);
						action = Interlocked.CompareExchange<Action<DebugUI.Panel>>(ref this.onSetDirty, value2, action2);
					}
					while (action != action2);
				}
			}

			// Token: 0x060008DB RID: 2267 RVA: 0x00023984 File Offset: 0x00021B84
			public Panel()
			{
				this.children = new ObservableList<DebugUI.Widget>();
				this.children.ItemAdded += this.OnItemAdded;
				this.children.ItemRemoved += this.OnItemRemoved;
			}

			// Token: 0x060008DC RID: 2268 RVA: 0x000239F7 File Offset: 0x00021BF7
			protected virtual void OnItemAdded(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				if (e.item != null)
				{
					e.item.panel = this;
					e.item.parent = this;
				}
				this.SetDirty();
			}

			// Token: 0x060008DD RID: 2269 RVA: 0x00023A1F File Offset: 0x00021C1F
			protected virtual void OnItemRemoved(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				if (e.item != null)
				{
					e.item.panel = null;
					e.item.parent = null;
				}
				this.SetDirty();
			}

			// Token: 0x060008DE RID: 2270 RVA: 0x00023A48 File Offset: 0x00021C48
			public void SetDirty()
			{
				foreach (DebugUI.Widget widget in this.children)
				{
					widget.GenerateQueryPath();
				}
				this.onSetDirty(this);
			}

			// Token: 0x060008DF RID: 2271 RVA: 0x00023AA0 File Offset: 0x00021CA0
			public override int GetHashCode()
			{
				int num = 17;
				num = num * 23 + this.displayName.GetHashCode();
				foreach (DebugUI.Widget widget in this.children)
				{
					num = num * 23 + widget.GetHashCode();
				}
				return num;
			}

			// Token: 0x060008E0 RID: 2272 RVA: 0x00023B08 File Offset: 0x00021D08
			int IComparable<DebugUI.Panel>.CompareTo(DebugUI.Panel other)
			{
				if (other != null)
				{
					return this.groupIndex.CompareTo(other.groupIndex);
				}
				return 1;
			}

			// Token: 0x0400055B RID: 1371
			[CompilerGenerated]
			private DebugUI.Flags <flags>k__BackingField;

			// Token: 0x0400055C RID: 1372
			[CompilerGenerated]
			private string <displayName>k__BackingField;

			// Token: 0x0400055D RID: 1373
			[CompilerGenerated]
			private int <groupIndex>k__BackingField;

			// Token: 0x0400055E RID: 1374
			[CompilerGenerated]
			private ObservableList<DebugUI.Widget> <children>k__BackingField;

			// Token: 0x0400055F RID: 1375
			[CompilerGenerated]
			private Action<DebugUI.Panel> onSetDirty = delegate(DebugUI.Panel <p0>)
			{
			};

			// Token: 0x02000195 RID: 405
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06000944 RID: 2372 RVA: 0x00024D1B File Offset: 0x00022F1B
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06000945 RID: 2373 RVA: 0x00024D27 File Offset: 0x00022F27
				public <>c()
				{
				}

				// Token: 0x06000946 RID: 2374 RVA: 0x00024D2F File Offset: 0x00022F2F
				internal void <.ctor>b__29_0(DebugUI.Panel <p0>)
				{
				}

				// Token: 0x040005E4 RID: 1508
				public static readonly DebugUI.Panel.<>c <>9 = new DebugUI.Panel.<>c();

				// Token: 0x040005E5 RID: 1509
				public static Action<DebugUI.Panel> <>9__29_0;
			}
		}
	}
}
