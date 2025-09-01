using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace UnityEngine.Yoga
{
	// Token: 0x02000016 RID: 22
	internal class YogaNode : IEnumerable<YogaNode>, IEnumerable
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x000023C4 File Offset: 0x000005C4
		public YogaNode(YogaConfig config = null)
		{
			this._config = ((config == null) ? YogaConfig.Default : config);
			this._ygNode = Native.YGNodeNewWithConfig(this._config.Handle);
			bool flag = this._ygNode == IntPtr.Zero;
			if (flag)
			{
				throw new InvalidOperationException("Failed to allocate native memory");
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00002420 File Offset: 0x00000620
		public YogaNode(YogaNode srcNode) : this(srcNode._config)
		{
			this.CopyStyle(srcNode);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00002438 File Offset: 0x00000638
		~YogaNode()
		{
			Native.YGNodeFree(this._ygNode);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00002470 File Offset: 0x00000670
		public void Reset()
		{
			this._measureFunction = null;
			this._baselineFunction = null;
			this._data = null;
			Native.YGSetManagedObject(this._ygNode, null);
			Native.YGNodeReset(this._ygNode);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000024A4 File Offset: 0x000006A4
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000024BC File Offset: 0x000006BC
		internal YogaConfig Config
		{
			get
			{
				return this._config;
			}
			set
			{
				this._config = (value ?? YogaConfig.Default);
				Native.YGNodeSetConfig(this._ygNode, this._config.Handle);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000024E8 File Offset: 0x000006E8
		public bool IsDirty
		{
			get
			{
				return Native.YGNodeIsDirty(this._ygNode);
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00002505 File Offset: 0x00000705
		public virtual void MarkDirty()
		{
			Native.YGNodeMarkDirty(this._ygNode);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00002514 File Offset: 0x00000714
		public bool HasNewLayout
		{
			get
			{
				return Native.YGNodeGetHasNewLayout(this._ygNode);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00002531 File Offset: 0x00000731
		public void MarkHasNewLayout()
		{
			Native.YGNodeSetHasNewLayout(this._ygNode, true);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00002544 File Offset: 0x00000744
		public YogaNode Parent
		{
			get
			{
				return (this._parent != null) ? (this._parent.Target as YogaNode) : null;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00002574 File Offset: 0x00000774
		public bool IsMeasureDefined
		{
			get
			{
				return this._measureFunction != null;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00002590 File Offset: 0x00000790
		public bool IsBaselineDefined
		{
			get
			{
				return this._baselineFunction != null;
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000025AB File Offset: 0x000007AB
		public void CopyStyle(YogaNode srcNode)
		{
			Native.YGNodeCopyStyle(this._ygNode, srcNode._ygNode);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000025C0 File Offset: 0x000007C0
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x000025DD File Offset: 0x000007DD
		public YogaDirection StyleDirection
		{
			get
			{
				return Native.YGNodeStyleGetDirection(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetDirection(this._ygNode, value);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000025F0 File Offset: 0x000007F0
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x0000260D File Offset: 0x0000080D
		public YogaFlexDirection FlexDirection
		{
			get
			{
				return Native.YGNodeStyleGetFlexDirection(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetFlexDirection(this._ygNode, value);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00002620 File Offset: 0x00000820
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000263D File Offset: 0x0000083D
		public YogaJustify JustifyContent
		{
			get
			{
				return Native.YGNodeStyleGetJustifyContent(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetJustifyContent(this._ygNode, value);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00002650 File Offset: 0x00000850
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x0000266D File Offset: 0x0000086D
		public YogaDisplay Display
		{
			get
			{
				return Native.YGNodeStyleGetDisplay(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetDisplay(this._ygNode, value);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00002680 File Offset: 0x00000880
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x0000269D File Offset: 0x0000089D
		public YogaAlign AlignItems
		{
			get
			{
				return Native.YGNodeStyleGetAlignItems(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetAlignItems(this._ygNode, value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000026B0 File Offset: 0x000008B0
		// (set) Token: 0x060000BB RID: 187 RVA: 0x000026CD File Offset: 0x000008CD
		public YogaAlign AlignSelf
		{
			get
			{
				return Native.YGNodeStyleGetAlignSelf(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetAlignSelf(this._ygNode, value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000026E0 File Offset: 0x000008E0
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000026FD File Offset: 0x000008FD
		public YogaAlign AlignContent
		{
			get
			{
				return Native.YGNodeStyleGetAlignContent(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetAlignContent(this._ygNode, value);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00002710 File Offset: 0x00000910
		// (set) Token: 0x060000BF RID: 191 RVA: 0x0000272D File Offset: 0x0000092D
		public YogaPositionType PositionType
		{
			get
			{
				return Native.YGNodeStyleGetPositionType(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetPositionType(this._ygNode, value);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00002740 File Offset: 0x00000940
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x0000275D File Offset: 0x0000095D
		public YogaWrap Wrap
		{
			get
			{
				return Native.YGNodeStyleGetFlexWrap(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetFlexWrap(this._ygNode, value);
			}
		}

		// Token: 0x17000014 RID: 20
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x0000276D File Offset: 0x0000096D
		public float Flex
		{
			set
			{
				Native.YGNodeStyleSetFlex(this._ygNode, value);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00002780 File Offset: 0x00000980
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x0000279D File Offset: 0x0000099D
		public float FlexGrow
		{
			get
			{
				return Native.YGNodeStyleGetFlexGrow(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetFlexGrow(this._ygNode, value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000027B0 File Offset: 0x000009B0
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x000027CD File Offset: 0x000009CD
		public float FlexShrink
		{
			get
			{
				return Native.YGNodeStyleGetFlexShrink(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetFlexShrink(this._ygNode, value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000027E0 File Offset: 0x000009E0
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00002804 File Offset: 0x00000A04
		public YogaValue FlexBasis
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetFlexBasis(this._ygNode));
			}
			set
			{
				bool flag = value.Unit == YogaUnit.Percent;
				if (flag)
				{
					Native.YGNodeStyleSetFlexBasisPercent(this._ygNode, value.Value);
				}
				else
				{
					bool flag2 = value.Unit == YogaUnit.Auto;
					if (flag2)
					{
						Native.YGNodeStyleSetFlexBasisAuto(this._ygNode);
					}
					else
					{
						Native.YGNodeStyleSetFlexBasis(this._ygNode, value.Value);
					}
				}
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000286C File Offset: 0x00000A6C
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00002890 File Offset: 0x00000A90
		public YogaValue Width
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetWidth(this._ygNode));
			}
			set
			{
				bool flag = value.Unit == YogaUnit.Percent;
				if (flag)
				{
					Native.YGNodeStyleSetWidthPercent(this._ygNode, value.Value);
				}
				else
				{
					bool flag2 = value.Unit == YogaUnit.Auto;
					if (flag2)
					{
						Native.YGNodeStyleSetWidthAuto(this._ygNode);
					}
					else
					{
						Native.YGNodeStyleSetWidth(this._ygNode, value.Value);
					}
				}
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000028F8 File Offset: 0x00000AF8
		// (set) Token: 0x060000CC RID: 204 RVA: 0x0000291C File Offset: 0x00000B1C
		public YogaValue Height
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetHeight(this._ygNode));
			}
			set
			{
				bool flag = value.Unit == YogaUnit.Percent;
				if (flag)
				{
					Native.YGNodeStyleSetHeightPercent(this._ygNode, value.Value);
				}
				else
				{
					bool flag2 = value.Unit == YogaUnit.Auto;
					if (flag2)
					{
						Native.YGNodeStyleSetHeightAuto(this._ygNode);
					}
					else
					{
						Native.YGNodeStyleSetHeight(this._ygNode, value.Value);
					}
				}
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00002984 File Offset: 0x00000B84
		// (set) Token: 0x060000CE RID: 206 RVA: 0x000029A8 File Offset: 0x00000BA8
		public YogaValue MaxWidth
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMaxWidth(this._ygNode));
			}
			set
			{
				bool flag = value.Unit == YogaUnit.Percent;
				if (flag)
				{
					Native.YGNodeStyleSetMaxWidthPercent(this._ygNode, value.Value);
				}
				else
				{
					Native.YGNodeStyleSetMaxWidth(this._ygNode, value.Value);
				}
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000029F0 File Offset: 0x00000BF0
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00002A14 File Offset: 0x00000C14
		public YogaValue MaxHeight
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMaxHeight(this._ygNode));
			}
			set
			{
				bool flag = value.Unit == YogaUnit.Percent;
				if (flag)
				{
					Native.YGNodeStyleSetMaxHeightPercent(this._ygNode, value.Value);
				}
				else
				{
					Native.YGNodeStyleSetMaxHeight(this._ygNode, value.Value);
				}
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00002A5C File Offset: 0x00000C5C
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00002A80 File Offset: 0x00000C80
		public YogaValue MinWidth
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMinWidth(this._ygNode));
			}
			set
			{
				bool flag = value.Unit == YogaUnit.Percent;
				if (flag)
				{
					Native.YGNodeStyleSetMinWidthPercent(this._ygNode, value.Value);
				}
				else
				{
					Native.YGNodeStyleSetMinWidth(this._ygNode, value.Value);
				}
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00002AC8 File Offset: 0x00000CC8
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00002AEC File Offset: 0x00000CEC
		public YogaValue MinHeight
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMinHeight(this._ygNode));
			}
			set
			{
				bool flag = value.Unit == YogaUnit.Percent;
				if (flag)
				{
					Native.YGNodeStyleSetMinHeightPercent(this._ygNode, value.Value);
				}
				else
				{
					Native.YGNodeStyleSetMinHeight(this._ygNode, value.Value);
				}
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00002B34 File Offset: 0x00000D34
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00002B51 File Offset: 0x00000D51
		public float AspectRatio
		{
			get
			{
				return Native.YGNodeStyleGetAspectRatio(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetAspectRatio(this._ygNode, value);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00002B64 File Offset: 0x00000D64
		public float LayoutX
		{
			get
			{
				return Native.YGNodeLayoutGetLeft(this._ygNode);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00002B84 File Offset: 0x00000D84
		public float LayoutY
		{
			get
			{
				return Native.YGNodeLayoutGetTop(this._ygNode);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00002BA4 File Offset: 0x00000DA4
		public float LayoutRight
		{
			get
			{
				return Native.YGNodeLayoutGetRight(this._ygNode);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public float LayoutBottom
		{
			get
			{
				return Native.YGNodeLayoutGetBottom(this._ygNode);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00002BE4 File Offset: 0x00000DE4
		public float LayoutWidth
		{
			get
			{
				return Native.YGNodeLayoutGetWidth(this._ygNode);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00002C04 File Offset: 0x00000E04
		public float LayoutHeight
		{
			get
			{
				return Native.YGNodeLayoutGetHeight(this._ygNode);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00002C24 File Offset: 0x00000E24
		public YogaDirection LayoutDirection
		{
			get
			{
				return Native.YGNodeLayoutGetDirection(this._ygNode);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00002C44 File Offset: 0x00000E44
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00002C61 File Offset: 0x00000E61
		public YogaOverflow Overflow
		{
			get
			{
				return Native.YGNodeStyleGetOverflow(this._ygNode);
			}
			set
			{
				Native.YGNodeStyleSetOverflow(this._ygNode, value);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00002C74 File Offset: 0x00000E74
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00002C8C File Offset: 0x00000E8C
		public object Data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
			}
		}

		// Token: 0x17000028 RID: 40
		public YogaNode this[int index]
		{
			get
			{
				return this._children[index];
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public int Count
		{
			get
			{
				return (this._children != null) ? this._children.Count : 0;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public void MarkLayoutSeen()
		{
			Native.YGNodeSetHasNewLayout(this._ygNode, false);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00002CF0 File Offset: 0x00000EF0
		public bool ValuesEqual(float f1, float f2)
		{
			bool flag = float.IsNaN(f1) || float.IsNaN(f2);
			bool result;
			if (flag)
			{
				result = (float.IsNaN(f1) && float.IsNaN(f2));
			}
			else
			{
				result = (Math.Abs(f2 - f1) < float.Epsilon);
			}
			return result;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00002D3C File Offset: 0x00000F3C
		public void Insert(int index, YogaNode node)
		{
			bool flag = this._children == null;
			if (flag)
			{
				this._children = new List<YogaNode>(4);
			}
			this._children.Insert(index, node);
			node._parent = new WeakReference(this);
			Native.YGNodeInsertChild(this._ygNode, node._ygNode, (uint)index);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00002D94 File Offset: 0x00000F94
		public void RemoveAt(int index)
		{
			YogaNode yogaNode = this._children[index];
			yogaNode._parent = null;
			this._children.RemoveAt(index);
			Native.YGNodeRemoveChild(this._ygNode, yogaNode._ygNode);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00002DD5 File Offset: 0x00000FD5
		public void AddChild(YogaNode child)
		{
			this.Insert(this.Count, child);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00002DE8 File Offset: 0x00000FE8
		public void RemoveChild(YogaNode child)
		{
			int num = this.IndexOf(child);
			bool flag = num >= 0;
			if (flag)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00002E14 File Offset: 0x00001014
		public void Clear()
		{
			bool flag = this._children != null;
			if (flag)
			{
				while (this._children.Count > 0)
				{
					this.RemoveAt(this._children.Count - 1);
				}
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00002E5C File Offset: 0x0000105C
		public int IndexOf(YogaNode node)
		{
			return (this._children != null) ? this._children.IndexOf(node) : -1;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00002E88 File Offset: 0x00001088
		public void SetMeasureFunction(MeasureFunction measureFunction)
		{
			this._measureFunction = measureFunction;
			bool flag = measureFunction == null;
			if (flag)
			{
				bool flag2 = !this.IsBaselineDefined;
				if (flag2)
				{
					Native.YGSetManagedObject(this._ygNode, null);
				}
				Native.YGNodeRemoveMeasureFunc(this._ygNode);
			}
			else
			{
				Native.YGSetManagedObject(this._ygNode, this);
				Native.YGNodeSetMeasureFunc(this._ygNode);
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00002EEC File Offset: 0x000010EC
		public void SetBaselineFunction(BaselineFunction baselineFunction)
		{
			this._baselineFunction = baselineFunction;
			bool flag = baselineFunction == null;
			if (flag)
			{
				bool flag2 = !this.IsMeasureDefined;
				if (flag2)
				{
					Native.YGSetManagedObject(this._ygNode, null);
				}
				Native.YGNodeRemoveBaselineFunc(this._ygNode);
			}
			else
			{
				Native.YGSetManagedObject(this._ygNode, this);
				Native.YGNodeSetBaselineFunc(this._ygNode);
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00002F50 File Offset: 0x00001150
		public void CalculateLayout(float width = float.NaN, float height = float.NaN)
		{
			Native.YGNodeCalculateLayout(this._ygNode, width, height, Native.YGNodeStyleGetDirection(this._ygNode));
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00002F6C File Offset: 0x0000116C
		public static YogaSize MeasureInternal(YogaNode node, float width, YogaMeasureMode widthMode, float height, YogaMeasureMode heightMode)
		{
			bool flag = node == null || node._measureFunction == null;
			if (flag)
			{
				throw new InvalidOperationException("Measure function is not defined.");
			}
			return node._measureFunction(node, width, widthMode, height, heightMode);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00002FB0 File Offset: 0x000011B0
		public static float BaselineInternal(YogaNode node, float width, float height)
		{
			bool flag = node == null || node._baselineFunction == null;
			if (flag)
			{
				throw new InvalidOperationException("Baseline function is not defined.");
			}
			return node._baselineFunction(node, width, height);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00002FF0 File Offset: 0x000011F0
		public string Print(YogaPrintOptions options = YogaPrintOptions.Layout | YogaPrintOptions.Style | YogaPrintOptions.Children)
		{
			StringBuilder sb = new StringBuilder();
			Logger logger = this._config.Logger;
			this._config.Logger = delegate(YogaConfig config, YogaNode node, YogaLogLevel level, string message)
			{
				sb.Append(message);
			};
			Native.YGNodePrint(this._ygNode, options);
			this._config.Logger = logger;
			return sb.ToString();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000305C File Offset: 0x0000125C
		public IEnumerator<YogaNode> GetEnumerator()
		{
			return (this._children != null) ? ((IEnumerable<YogaNode>)this._children).GetEnumerator() : Enumerable.Empty<YogaNode>().GetEnumerator();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003090 File Offset: 0x00001290
		IEnumerator IEnumerable.GetEnumerator()
		{
			return (this._children != null) ? ((IEnumerable<YogaNode>)this._children).GetEnumerator() : Enumerable.Empty<YogaNode>().GetEnumerator();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000030C4 File Offset: 0x000012C4
		public static int GetInstanceCount()
		{
			return Native.YGNodeGetInstanceCount();
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000030DC File Offset: 0x000012DC
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x000030FF File Offset: 0x000012FF
		public YogaValue Left
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPosition(this._ygNode, YogaEdge.Left));
			}
			set
			{
				this.SetStylePosition(YogaEdge.Left, value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000310C File Offset: 0x0000130C
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x0000312F File Offset: 0x0000132F
		public YogaValue Top
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPosition(this._ygNode, YogaEdge.Top));
			}
			set
			{
				this.SetStylePosition(YogaEdge.Top, value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000313C File Offset: 0x0000133C
		// (set) Token: 0x060000FA RID: 250 RVA: 0x0000315F File Offset: 0x0000135F
		public YogaValue Right
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPosition(this._ygNode, YogaEdge.Right));
			}
			set
			{
				this.SetStylePosition(YogaEdge.Right, value);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000316C File Offset: 0x0000136C
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000318F File Offset: 0x0000138F
		public YogaValue Bottom
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPosition(this._ygNode, YogaEdge.Bottom));
			}
			set
			{
				this.SetStylePosition(YogaEdge.Bottom, value);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000319C File Offset: 0x0000139C
		// (set) Token: 0x060000FE RID: 254 RVA: 0x000031BF File Offset: 0x000013BF
		public YogaValue Start
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPosition(this._ygNode, YogaEdge.Start));
			}
			set
			{
				this.SetStylePosition(YogaEdge.Start, value);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000031CC File Offset: 0x000013CC
		// (set) Token: 0x06000100 RID: 256 RVA: 0x000031EF File Offset: 0x000013EF
		public YogaValue End
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPosition(this._ygNode, YogaEdge.End));
			}
			set
			{
				this.SetStylePosition(YogaEdge.End, value);
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000031FC File Offset: 0x000013FC
		private void SetStylePosition(YogaEdge edge, YogaValue value)
		{
			bool flag = value.Unit == YogaUnit.Percent;
			if (flag)
			{
				Native.YGNodeStyleSetPositionPercent(this._ygNode, edge, value.Value);
			}
			else
			{
				Native.YGNodeStyleSetPosition(this._ygNode, edge, value.Value);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00003248 File Offset: 0x00001448
		// (set) Token: 0x06000103 RID: 259 RVA: 0x0000326B File Offset: 0x0000146B
		public YogaValue MarginLeft
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMargin(this._ygNode, YogaEdge.Left));
			}
			set
			{
				this.SetStyleMargin(YogaEdge.Left, value);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00003278 File Offset: 0x00001478
		// (set) Token: 0x06000105 RID: 261 RVA: 0x0000329B File Offset: 0x0000149B
		public YogaValue MarginTop
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMargin(this._ygNode, YogaEdge.Top));
			}
			set
			{
				this.SetStyleMargin(YogaEdge.Top, value);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000032A8 File Offset: 0x000014A8
		// (set) Token: 0x06000107 RID: 263 RVA: 0x000032CB File Offset: 0x000014CB
		public YogaValue MarginRight
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMargin(this._ygNode, YogaEdge.Right));
			}
			set
			{
				this.SetStyleMargin(YogaEdge.Right, value);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000032D8 File Offset: 0x000014D8
		// (set) Token: 0x06000109 RID: 265 RVA: 0x000032FB File Offset: 0x000014FB
		public YogaValue MarginBottom
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMargin(this._ygNode, YogaEdge.Bottom));
			}
			set
			{
				this.SetStyleMargin(YogaEdge.Bottom, value);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00003308 File Offset: 0x00001508
		// (set) Token: 0x0600010B RID: 267 RVA: 0x0000332B File Offset: 0x0000152B
		public YogaValue MarginStart
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMargin(this._ygNode, YogaEdge.Start));
			}
			set
			{
				this.SetStyleMargin(YogaEdge.Start, value);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00003338 File Offset: 0x00001538
		// (set) Token: 0x0600010D RID: 269 RVA: 0x0000335B File Offset: 0x0000155B
		public YogaValue MarginEnd
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMargin(this._ygNode, YogaEdge.End));
			}
			set
			{
				this.SetStyleMargin(YogaEdge.End, value);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00003368 File Offset: 0x00001568
		// (set) Token: 0x0600010F RID: 271 RVA: 0x0000338B File Offset: 0x0000158B
		public YogaValue MarginHorizontal
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMargin(this._ygNode, YogaEdge.Horizontal));
			}
			set
			{
				this.SetStyleMargin(YogaEdge.Horizontal, value);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00003398 File Offset: 0x00001598
		// (set) Token: 0x06000111 RID: 273 RVA: 0x000033BB File Offset: 0x000015BB
		public YogaValue MarginVertical
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMargin(this._ygNode, YogaEdge.Vertical));
			}
			set
			{
				this.SetStyleMargin(YogaEdge.Vertical, value);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000033C8 File Offset: 0x000015C8
		// (set) Token: 0x06000113 RID: 275 RVA: 0x000033EB File Offset: 0x000015EB
		public YogaValue Margin
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetMargin(this._ygNode, YogaEdge.All));
			}
			set
			{
				this.SetStyleMargin(YogaEdge.All, value);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000033F8 File Offset: 0x000015F8
		private void SetStyleMargin(YogaEdge edge, YogaValue value)
		{
			bool flag = value.Unit == YogaUnit.Percent;
			if (flag)
			{
				Native.YGNodeStyleSetMarginPercent(this._ygNode, edge, value.Value);
			}
			else
			{
				bool flag2 = value.Unit == YogaUnit.Auto;
				if (flag2)
				{
					Native.YGNodeStyleSetMarginAuto(this._ygNode, edge);
				}
				else
				{
					Native.YGNodeStyleSetMargin(this._ygNode, edge, value.Value);
				}
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00003464 File Offset: 0x00001664
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00003487 File Offset: 0x00001687
		public YogaValue PaddingLeft
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPadding(this._ygNode, YogaEdge.Left));
			}
			set
			{
				this.SetStylePadding(YogaEdge.Left, value);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00003494 File Offset: 0x00001694
		// (set) Token: 0x06000118 RID: 280 RVA: 0x000034B7 File Offset: 0x000016B7
		public YogaValue PaddingTop
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPadding(this._ygNode, YogaEdge.Top));
			}
			set
			{
				this.SetStylePadding(YogaEdge.Top, value);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000034C4 File Offset: 0x000016C4
		// (set) Token: 0x0600011A RID: 282 RVA: 0x000034E7 File Offset: 0x000016E7
		public YogaValue PaddingRight
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPadding(this._ygNode, YogaEdge.Right));
			}
			set
			{
				this.SetStylePadding(YogaEdge.Right, value);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000034F4 File Offset: 0x000016F4
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00003517 File Offset: 0x00001717
		public YogaValue PaddingBottom
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPadding(this._ygNode, YogaEdge.Bottom));
			}
			set
			{
				this.SetStylePadding(YogaEdge.Bottom, value);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00003524 File Offset: 0x00001724
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00003547 File Offset: 0x00001747
		public YogaValue PaddingStart
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPadding(this._ygNode, YogaEdge.Start));
			}
			set
			{
				this.SetStylePadding(YogaEdge.Start, value);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00003554 File Offset: 0x00001754
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00003577 File Offset: 0x00001777
		public YogaValue PaddingEnd
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPadding(this._ygNode, YogaEdge.End));
			}
			set
			{
				this.SetStylePadding(YogaEdge.End, value);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00003584 File Offset: 0x00001784
		// (set) Token: 0x06000122 RID: 290 RVA: 0x000035A7 File Offset: 0x000017A7
		public YogaValue PaddingHorizontal
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPadding(this._ygNode, YogaEdge.Horizontal));
			}
			set
			{
				this.SetStylePadding(YogaEdge.Horizontal, value);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000035B4 File Offset: 0x000017B4
		// (set) Token: 0x06000124 RID: 292 RVA: 0x000035D7 File Offset: 0x000017D7
		public YogaValue PaddingVertical
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPadding(this._ygNode, YogaEdge.Vertical));
			}
			set
			{
				this.SetStylePadding(YogaEdge.Vertical, value);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000035E4 File Offset: 0x000017E4
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00003607 File Offset: 0x00001807
		public YogaValue Padding
		{
			get
			{
				return YogaValue.MarshalValue(Native.YGNodeStyleGetPadding(this._ygNode, YogaEdge.All));
			}
			set
			{
				this.SetStylePadding(YogaEdge.All, value);
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00003614 File Offset: 0x00001814
		private void SetStylePadding(YogaEdge edge, YogaValue value)
		{
			bool flag = value.Unit == YogaUnit.Percent;
			if (flag)
			{
				Native.YGNodeStyleSetPaddingPercent(this._ygNode, edge, value.Value);
			}
			else
			{
				Native.YGNodeStyleSetPadding(this._ygNode, edge, value.Value);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00003660 File Offset: 0x00001860
		// (set) Token: 0x06000129 RID: 297 RVA: 0x0000367E File Offset: 0x0000187E
		public float BorderLeftWidth
		{
			get
			{
				return Native.YGNodeStyleGetBorder(this._ygNode, YogaEdge.Left);
			}
			set
			{
				Native.YGNodeStyleSetBorder(this._ygNode, YogaEdge.Left, value);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00003690 File Offset: 0x00001890
		// (set) Token: 0x0600012B RID: 299 RVA: 0x000036AE File Offset: 0x000018AE
		public float BorderTopWidth
		{
			get
			{
				return Native.YGNodeStyleGetBorder(this._ygNode, YogaEdge.Top);
			}
			set
			{
				Native.YGNodeStyleSetBorder(this._ygNode, YogaEdge.Top, value);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000036C0 File Offset: 0x000018C0
		// (set) Token: 0x0600012D RID: 301 RVA: 0x000036DE File Offset: 0x000018DE
		public float BorderRightWidth
		{
			get
			{
				return Native.YGNodeStyleGetBorder(this._ygNode, YogaEdge.Right);
			}
			set
			{
				Native.YGNodeStyleSetBorder(this._ygNode, YogaEdge.Right, value);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000036F0 File Offset: 0x000018F0
		// (set) Token: 0x0600012F RID: 303 RVA: 0x0000370E File Offset: 0x0000190E
		public float BorderBottomWidth
		{
			get
			{
				return Native.YGNodeStyleGetBorder(this._ygNode, YogaEdge.Bottom);
			}
			set
			{
				Native.YGNodeStyleSetBorder(this._ygNode, YogaEdge.Bottom, value);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00003720 File Offset: 0x00001920
		// (set) Token: 0x06000131 RID: 305 RVA: 0x0000373E File Offset: 0x0000193E
		public float BorderStartWidth
		{
			get
			{
				return Native.YGNodeStyleGetBorder(this._ygNode, YogaEdge.Start);
			}
			set
			{
				Native.YGNodeStyleSetBorder(this._ygNode, YogaEdge.Start, value);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00003750 File Offset: 0x00001950
		// (set) Token: 0x06000133 RID: 307 RVA: 0x0000376E File Offset: 0x0000196E
		public float BorderEndWidth
		{
			get
			{
				return Native.YGNodeStyleGetBorder(this._ygNode, YogaEdge.End);
			}
			set
			{
				Native.YGNodeStyleSetBorder(this._ygNode, YogaEdge.End, value);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00003780 File Offset: 0x00001980
		// (set) Token: 0x06000135 RID: 309 RVA: 0x0000379E File Offset: 0x0000199E
		public float BorderWidth
		{
			get
			{
				return Native.YGNodeStyleGetBorder(this._ygNode, YogaEdge.All);
			}
			set
			{
				Native.YGNodeStyleSetBorder(this._ygNode, YogaEdge.All, value);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000037B0 File Offset: 0x000019B0
		public float LayoutMarginLeft
		{
			get
			{
				return Native.YGNodeLayoutGetMargin(this._ygNode, YogaEdge.Left);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000037D0 File Offset: 0x000019D0
		public float LayoutMarginTop
		{
			get
			{
				return Native.YGNodeLayoutGetMargin(this._ygNode, YogaEdge.Top);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000037F0 File Offset: 0x000019F0
		public float LayoutMarginRight
		{
			get
			{
				return Native.YGNodeLayoutGetMargin(this._ygNode, YogaEdge.Right);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00003810 File Offset: 0x00001A10
		public float LayoutMarginBottom
		{
			get
			{
				return Native.YGNodeLayoutGetMargin(this._ygNode, YogaEdge.Bottom);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00003830 File Offset: 0x00001A30
		public float LayoutMarginStart
		{
			get
			{
				return Native.YGNodeLayoutGetMargin(this._ygNode, YogaEdge.Start);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00003850 File Offset: 0x00001A50
		public float LayoutMarginEnd
		{
			get
			{
				return Native.YGNodeLayoutGetMargin(this._ygNode, YogaEdge.End);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00003870 File Offset: 0x00001A70
		public float LayoutPaddingLeft
		{
			get
			{
				return Native.YGNodeLayoutGetPadding(this._ygNode, YogaEdge.Left);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00003890 File Offset: 0x00001A90
		public float LayoutPaddingTop
		{
			get
			{
				return Native.YGNodeLayoutGetPadding(this._ygNode, YogaEdge.Top);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000038B0 File Offset: 0x00001AB0
		public float LayoutPaddingRight
		{
			get
			{
				return Native.YGNodeLayoutGetPadding(this._ygNode, YogaEdge.Right);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000038D0 File Offset: 0x00001AD0
		public float LayoutPaddingBottom
		{
			get
			{
				return Native.YGNodeLayoutGetPadding(this._ygNode, YogaEdge.Bottom);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000038F0 File Offset: 0x00001AF0
		public float LayoutBorderLeft
		{
			get
			{
				return Native.YGNodeLayoutGetBorder(this._ygNode, YogaEdge.Left);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00003910 File Offset: 0x00001B10
		public float LayoutBorderTop
		{
			get
			{
				return Native.YGNodeLayoutGetBorder(this._ygNode, YogaEdge.Top);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00003930 File Offset: 0x00001B30
		public float LayoutBorderRight
		{
			get
			{
				return Native.YGNodeLayoutGetBorder(this._ygNode, YogaEdge.Right);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00003950 File Offset: 0x00001B50
		public float LayoutBorderBottom
		{
			get
			{
				return Native.YGNodeLayoutGetBorder(this._ygNode, YogaEdge.Bottom);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00003970 File Offset: 0x00001B70
		public float LayoutPaddingStart
		{
			get
			{
				return Native.YGNodeLayoutGetPadding(this._ygNode, YogaEdge.Start);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00003990 File Offset: 0x00001B90
		public float LayoutPaddingEnd
		{
			get
			{
				return Native.YGNodeLayoutGetPadding(this._ygNode, YogaEdge.End);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000039B0 File Offset: 0x00001BB0
		public float ComputedFlexBasis
		{
			get
			{
				return Native.YGNodeGetComputedFlexBasis(this._ygNode);
			}
		}

		// Token: 0x0400003A RID: 58
		internal IntPtr _ygNode;

		// Token: 0x0400003B RID: 59
		private YogaConfig _config;

		// Token: 0x0400003C RID: 60
		private WeakReference _parent;

		// Token: 0x0400003D RID: 61
		private List<YogaNode> _children;

		// Token: 0x0400003E RID: 62
		private MeasureFunction _measureFunction;

		// Token: 0x0400003F RID: 63
		private BaselineFunction _baselineFunction;

		// Token: 0x04000040 RID: 64
		private object _data;

		// Token: 0x02000017 RID: 23
		[CompilerGenerated]
		private sealed class <>c__DisplayClass123_0
		{
			// Token: 0x06000147 RID: 327 RVA: 0x0000207B File Offset: 0x0000027B
			public <>c__DisplayClass123_0()
			{
			}

			// Token: 0x06000148 RID: 328 RVA: 0x000039CD File Offset: 0x00001BCD
			internal void <Print>b__0(YogaConfig config, YogaNode node, YogaLogLevel level, string message)
			{
				this.sb.Append(message);
			}

			// Token: 0x04000041 RID: 65
			public StringBuilder sb;
		}
	}
}
