using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Yoga
{
	// Token: 0x02000015 RID: 21
	[NativeHeader("Modules/UIElementsNative/YogaNative.bindings.h")]
	internal static class Native
	{
		// Token: 0x0600002C RID: 44
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr YGNodeNew();

		// Token: 0x0600002D RID: 45
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr YGNodeNewWithConfig(IntPtr config);

		// Token: 0x0600002E RID: 46 RVA: 0x00002258 File Offset: 0x00000458
		public static void YGNodeFree(IntPtr ygNode)
		{
			bool flag = ygNode == IntPtr.Zero;
			if (!flag)
			{
				Native.YGNodeFreeInternal(ygNode);
			}
		}

		// Token: 0x0600002F RID: 47
		[FreeFunction(Name = "YGNodeFree", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeFreeInternal(IntPtr ygNode);

		// Token: 0x06000030 RID: 48
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeReset(IntPtr node);

		// Token: 0x06000031 RID: 49
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGSetManagedObject(IntPtr ygNode, YogaNode node);

		// Token: 0x06000032 RID: 50
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeSetConfig(IntPtr ygNode, IntPtr config);

		// Token: 0x06000033 RID: 51
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr YGConfigGetDefault();

		// Token: 0x06000034 RID: 52
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr YGConfigNew();

		// Token: 0x06000035 RID: 53 RVA: 0x00002280 File Offset: 0x00000480
		public static void YGConfigFree(IntPtr config)
		{
			bool flag = config == IntPtr.Zero;
			if (!flag)
			{
				Native.YGConfigFreeInternal(config);
			}
		}

		// Token: 0x06000036 RID: 54
		[FreeFunction(Name = "YGConfigFree", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGConfigFreeInternal(IntPtr config);

		// Token: 0x06000037 RID: 55
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int YGNodeGetInstanceCount();

		// Token: 0x06000038 RID: 56
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int YGConfigGetInstanceCount();

		// Token: 0x06000039 RID: 57
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGConfigSetExperimentalFeatureEnabled(IntPtr config, YogaExperimentalFeature feature, bool enabled);

		// Token: 0x0600003A RID: 58
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool YGConfigIsExperimentalFeatureEnabled(IntPtr config, YogaExperimentalFeature feature);

		// Token: 0x0600003B RID: 59
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGConfigSetUseWebDefaults(IntPtr config, bool useWebDefaults);

		// Token: 0x0600003C RID: 60
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool YGConfigGetUseWebDefaults(IntPtr config);

		// Token: 0x0600003D RID: 61
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGConfigSetPointScaleFactor(IntPtr config, float pixelsInPoint);

		// Token: 0x0600003E RID: 62
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGConfigGetPointScaleFactor(IntPtr config);

		// Token: 0x0600003F RID: 63
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeInsertChild(IntPtr node, IntPtr child, uint index);

		// Token: 0x06000040 RID: 64
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeRemoveChild(IntPtr node, IntPtr child);

		// Token: 0x06000041 RID: 65
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeCalculateLayout(IntPtr node, float availableWidth, float availableHeight, YogaDirection parentDirection);

		// Token: 0x06000042 RID: 66
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeMarkDirty(IntPtr node);

		// Token: 0x06000043 RID: 67
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool YGNodeIsDirty(IntPtr node);

		// Token: 0x06000044 RID: 68
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodePrint(IntPtr node, YogaPrintOptions options);

		// Token: 0x06000045 RID: 69
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeCopyStyle(IntPtr dstNode, IntPtr srcNode);

		// Token: 0x06000046 RID: 70
		[FreeFunction(Name = "YogaCallback::SetMeasureFunc")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeSetMeasureFunc(IntPtr node);

		// Token: 0x06000047 RID: 71
		[FreeFunction(Name = "YogaCallback::RemoveMeasureFunc")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeRemoveMeasureFunc(IntPtr node);

		// Token: 0x06000048 RID: 72 RVA: 0x000022A6 File Offset: 0x000004A6
		[RequiredByNativeCode]
		public unsafe static void YGNodeMeasureInvoke(YogaNode node, float width, YogaMeasureMode widthMode, float height, YogaMeasureMode heightMode, IntPtr returnValueAddress)
		{
			*(YogaSize*)((void*)returnValueAddress) = YogaNode.MeasureInternal(node, width, widthMode, height, heightMode);
		}

		// Token: 0x06000049 RID: 73
		[FreeFunction(Name = "YogaCallback::SetBaselineFunc")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeSetBaselineFunc(IntPtr node);

		// Token: 0x0600004A RID: 74
		[FreeFunction(Name = "YogaCallback::RemoveBaselineFunc")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeRemoveBaselineFunc(IntPtr node);

		// Token: 0x0600004B RID: 75 RVA: 0x000022C0 File Offset: 0x000004C0
		[RequiredByNativeCode]
		public unsafe static void YGNodeBaselineInvoke(YogaNode node, float width, float height, IntPtr returnValueAddress)
		{
			*(float*)((void*)returnValueAddress) = YogaNode.BaselineInternal(node, width, height);
		}

		// Token: 0x0600004C RID: 76
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeSetHasNewLayout(IntPtr node, bool hasNewLayout);

		// Token: 0x0600004D RID: 77
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool YGNodeGetHasNewLayout(IntPtr node);

		// Token: 0x0600004E RID: 78
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetDirection(IntPtr node, YogaDirection direction);

		// Token: 0x0600004F RID: 79
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaDirection YGNodeStyleGetDirection(IntPtr node);

		// Token: 0x06000050 RID: 80
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetFlexDirection(IntPtr node, YogaFlexDirection flexDirection);

		// Token: 0x06000051 RID: 81
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaFlexDirection YGNodeStyleGetFlexDirection(IntPtr node);

		// Token: 0x06000052 RID: 82
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetJustifyContent(IntPtr node, YogaJustify justifyContent);

		// Token: 0x06000053 RID: 83
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaJustify YGNodeStyleGetJustifyContent(IntPtr node);

		// Token: 0x06000054 RID: 84
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetAlignContent(IntPtr node, YogaAlign alignContent);

		// Token: 0x06000055 RID: 85
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaAlign YGNodeStyleGetAlignContent(IntPtr node);

		// Token: 0x06000056 RID: 86
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetAlignItems(IntPtr node, YogaAlign alignItems);

		// Token: 0x06000057 RID: 87
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaAlign YGNodeStyleGetAlignItems(IntPtr node);

		// Token: 0x06000058 RID: 88
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetAlignSelf(IntPtr node, YogaAlign alignSelf);

		// Token: 0x06000059 RID: 89
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaAlign YGNodeStyleGetAlignSelf(IntPtr node);

		// Token: 0x0600005A RID: 90
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetPositionType(IntPtr node, YogaPositionType positionType);

		// Token: 0x0600005B RID: 91
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaPositionType YGNodeStyleGetPositionType(IntPtr node);

		// Token: 0x0600005C RID: 92
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetFlexWrap(IntPtr node, YogaWrap flexWrap);

		// Token: 0x0600005D RID: 93
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaWrap YGNodeStyleGetFlexWrap(IntPtr node);

		// Token: 0x0600005E RID: 94
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetOverflow(IntPtr node, YogaOverflow flexWrap);

		// Token: 0x0600005F RID: 95
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaOverflow YGNodeStyleGetOverflow(IntPtr node);

		// Token: 0x06000060 RID: 96
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetDisplay(IntPtr node, YogaDisplay display);

		// Token: 0x06000061 RID: 97
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaDisplay YGNodeStyleGetDisplay(IntPtr node);

		// Token: 0x06000062 RID: 98
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetFlex(IntPtr node, float flex);

		// Token: 0x06000063 RID: 99
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetFlexGrow(IntPtr node, float flexGrow);

		// Token: 0x06000064 RID: 100
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeStyleGetFlexGrow(IntPtr node);

		// Token: 0x06000065 RID: 101
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetFlexShrink(IntPtr node, float flexShrink);

		// Token: 0x06000066 RID: 102
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeStyleGetFlexShrink(IntPtr node);

		// Token: 0x06000067 RID: 103
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetFlexBasis(IntPtr node, float flexBasis);

		// Token: 0x06000068 RID: 104
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetFlexBasisPercent(IntPtr node, float flexBasis);

		// Token: 0x06000069 RID: 105
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetFlexBasisAuto(IntPtr node);

		// Token: 0x0600006A RID: 106 RVA: 0x000022D4 File Offset: 0x000004D4
		[FreeFunction]
		public static YogaValue YGNodeStyleGetFlexBasis(IntPtr node)
		{
			YogaValue result;
			Native.YGNodeStyleGetFlexBasis_Injected(node, out result);
			return result;
		}

		// Token: 0x0600006B RID: 107
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeGetComputedFlexBasis(IntPtr node);

		// Token: 0x0600006C RID: 108
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetWidth(IntPtr node, float width);

		// Token: 0x0600006D RID: 109
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetWidthPercent(IntPtr node, float width);

		// Token: 0x0600006E RID: 110
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetWidthAuto(IntPtr node);

		// Token: 0x0600006F RID: 111 RVA: 0x000022EC File Offset: 0x000004EC
		[FreeFunction]
		public static YogaValue YGNodeStyleGetWidth(IntPtr node)
		{
			YogaValue result;
			Native.YGNodeStyleGetWidth_Injected(node, out result);
			return result;
		}

		// Token: 0x06000070 RID: 112
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetHeight(IntPtr node, float height);

		// Token: 0x06000071 RID: 113
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetHeightPercent(IntPtr node, float height);

		// Token: 0x06000072 RID: 114
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetHeightAuto(IntPtr node);

		// Token: 0x06000073 RID: 115 RVA: 0x00002304 File Offset: 0x00000504
		[FreeFunction]
		public static YogaValue YGNodeStyleGetHeight(IntPtr node)
		{
			YogaValue result;
			Native.YGNodeStyleGetHeight_Injected(node, out result);
			return result;
		}

		// Token: 0x06000074 RID: 116
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMinWidth(IntPtr node, float minWidth);

		// Token: 0x06000075 RID: 117
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMinWidthPercent(IntPtr node, float minWidth);

		// Token: 0x06000076 RID: 118 RVA: 0x0000231C File Offset: 0x0000051C
		[FreeFunction]
		public static YogaValue YGNodeStyleGetMinWidth(IntPtr node)
		{
			YogaValue result;
			Native.YGNodeStyleGetMinWidth_Injected(node, out result);
			return result;
		}

		// Token: 0x06000077 RID: 119
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMinHeight(IntPtr node, float minHeight);

		// Token: 0x06000078 RID: 120
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMinHeightPercent(IntPtr node, float minHeight);

		// Token: 0x06000079 RID: 121 RVA: 0x00002334 File Offset: 0x00000534
		[FreeFunction]
		public static YogaValue YGNodeStyleGetMinHeight(IntPtr node)
		{
			YogaValue result;
			Native.YGNodeStyleGetMinHeight_Injected(node, out result);
			return result;
		}

		// Token: 0x0600007A RID: 122
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMaxWidth(IntPtr node, float maxWidth);

		// Token: 0x0600007B RID: 123
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMaxWidthPercent(IntPtr node, float maxWidth);

		// Token: 0x0600007C RID: 124 RVA: 0x0000234C File Offset: 0x0000054C
		[FreeFunction]
		public static YogaValue YGNodeStyleGetMaxWidth(IntPtr node)
		{
			YogaValue result;
			Native.YGNodeStyleGetMaxWidth_Injected(node, out result);
			return result;
		}

		// Token: 0x0600007D RID: 125
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMaxHeight(IntPtr node, float maxHeight);

		// Token: 0x0600007E RID: 126
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMaxHeightPercent(IntPtr node, float maxHeight);

		// Token: 0x0600007F RID: 127 RVA: 0x00002364 File Offset: 0x00000564
		[FreeFunction]
		public static YogaValue YGNodeStyleGetMaxHeight(IntPtr node)
		{
			YogaValue result;
			Native.YGNodeStyleGetMaxHeight_Injected(node, out result);
			return result;
		}

		// Token: 0x06000080 RID: 128
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetAspectRatio(IntPtr node, float aspectRatio);

		// Token: 0x06000081 RID: 129
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeStyleGetAspectRatio(IntPtr node);

		// Token: 0x06000082 RID: 130
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetPosition(IntPtr node, YogaEdge edge, float position);

		// Token: 0x06000083 RID: 131
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetPositionPercent(IntPtr node, YogaEdge edge, float position);

		// Token: 0x06000084 RID: 132 RVA: 0x0000237C File Offset: 0x0000057C
		[FreeFunction]
		public static YogaValue YGNodeStyleGetPosition(IntPtr node, YogaEdge edge)
		{
			YogaValue result;
			Native.YGNodeStyleGetPosition_Injected(node, edge, out result);
			return result;
		}

		// Token: 0x06000085 RID: 133
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMargin(IntPtr node, YogaEdge edge, float margin);

		// Token: 0x06000086 RID: 134
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMarginPercent(IntPtr node, YogaEdge edge, float margin);

		// Token: 0x06000087 RID: 135
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetMarginAuto(IntPtr node, YogaEdge edge);

		// Token: 0x06000088 RID: 136 RVA: 0x00002394 File Offset: 0x00000594
		[FreeFunction]
		public static YogaValue YGNodeStyleGetMargin(IntPtr node, YogaEdge edge)
		{
			YogaValue result;
			Native.YGNodeStyleGetMargin_Injected(node, edge, out result);
			return result;
		}

		// Token: 0x06000089 RID: 137
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetPadding(IntPtr node, YogaEdge edge, float padding);

		// Token: 0x0600008A RID: 138
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetPaddingPercent(IntPtr node, YogaEdge edge, float padding);

		// Token: 0x0600008B RID: 139 RVA: 0x000023AC File Offset: 0x000005AC
		[FreeFunction]
		public static YogaValue YGNodeStyleGetPadding(IntPtr node, YogaEdge edge)
		{
			YogaValue result;
			Native.YGNodeStyleGetPadding_Injected(node, edge, out result);
			return result;
		}

		// Token: 0x0600008C RID: 140
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void YGNodeStyleSetBorder(IntPtr node, YogaEdge edge, float border);

		// Token: 0x0600008D RID: 141
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeStyleGetBorder(IntPtr node, YogaEdge edge);

		// Token: 0x0600008E RID: 142
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeLayoutGetLeft(IntPtr node);

		// Token: 0x0600008F RID: 143
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeLayoutGetTop(IntPtr node);

		// Token: 0x06000090 RID: 144
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeLayoutGetRight(IntPtr node);

		// Token: 0x06000091 RID: 145
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeLayoutGetBottom(IntPtr node);

		// Token: 0x06000092 RID: 146
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeLayoutGetWidth(IntPtr node);

		// Token: 0x06000093 RID: 147
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeLayoutGetHeight(IntPtr node);

		// Token: 0x06000094 RID: 148
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeLayoutGetMargin(IntPtr node, YogaEdge edge);

		// Token: 0x06000095 RID: 149
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeLayoutGetPadding(IntPtr node, YogaEdge edge);

		// Token: 0x06000096 RID: 150
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float YGNodeLayoutGetBorder(IntPtr node, YogaEdge edge);

		// Token: 0x06000097 RID: 151
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern YogaDirection YGNodeLayoutGetDirection(IntPtr node);

		// Token: 0x06000098 RID: 152
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeStyleGetFlexBasis_Injected(IntPtr node, out YogaValue ret);

		// Token: 0x06000099 RID: 153
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeStyleGetWidth_Injected(IntPtr node, out YogaValue ret);

		// Token: 0x0600009A RID: 154
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeStyleGetHeight_Injected(IntPtr node, out YogaValue ret);

		// Token: 0x0600009B RID: 155
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeStyleGetMinWidth_Injected(IntPtr node, out YogaValue ret);

		// Token: 0x0600009C RID: 156
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeStyleGetMinHeight_Injected(IntPtr node, out YogaValue ret);

		// Token: 0x0600009D RID: 157
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeStyleGetMaxWidth_Injected(IntPtr node, out YogaValue ret);

		// Token: 0x0600009E RID: 158
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeStyleGetMaxHeight_Injected(IntPtr node, out YogaValue ret);

		// Token: 0x0600009F RID: 159
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeStyleGetPosition_Injected(IntPtr node, YogaEdge edge, out YogaValue ret);

		// Token: 0x060000A0 RID: 160
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeStyleGetMargin_Injected(IntPtr node, YogaEdge edge, out YogaValue ret);

		// Token: 0x060000A1 RID: 161
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void YGNodeStyleGetPadding_Injected(IntPtr node, YogaEdge edge, out YogaValue ret);
	}
}
