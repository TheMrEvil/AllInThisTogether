using System;
using System.Collections.Generic;
using UnityEngine.UIElements.UIR.Implementation;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000318 RID: 792
	internal struct RenderChainVEData
	{
		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x0006E700 File Offset: 0x0006C900
		internal RenderChainCommand lastClosingOrLastCommand
		{
			get
			{
				return this.lastClosingCommand ?? this.lastCommand;
			}
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x0006E724 File Offset: 0x0006C924
		internal static bool AllocatesID(BMPAlloc alloc)
		{
			return alloc.ownedState == OwnedState.Owned && alloc.IsValid();
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x0006E74C File Offset: 0x0006C94C
		internal static bool InheritsID(BMPAlloc alloc)
		{
			return alloc.ownedState == OwnedState.Inherited && alloc.IsValid();
		}

		// Token: 0x04000BA5 RID: 2981
		internal VisualElement prev;

		// Token: 0x04000BA6 RID: 2982
		internal VisualElement next;

		// Token: 0x04000BA7 RID: 2983
		internal VisualElement groupTransformAncestor;

		// Token: 0x04000BA8 RID: 2984
		internal VisualElement boneTransformAncestor;

		// Token: 0x04000BA9 RID: 2985
		internal VisualElement prevDirty;

		// Token: 0x04000BAA RID: 2986
		internal VisualElement nextDirty;

		// Token: 0x04000BAB RID: 2987
		internal int hierarchyDepth;

		// Token: 0x04000BAC RID: 2988
		internal RenderDataDirtyTypes dirtiedValues;

		// Token: 0x04000BAD RID: 2989
		internal uint dirtyID;

		// Token: 0x04000BAE RID: 2990
		internal RenderChainCommand firstCommand;

		// Token: 0x04000BAF RID: 2991
		internal RenderChainCommand lastCommand;

		// Token: 0x04000BB0 RID: 2992
		internal RenderChainCommand firstClosingCommand;

		// Token: 0x04000BB1 RID: 2993
		internal RenderChainCommand lastClosingCommand;

		// Token: 0x04000BB2 RID: 2994
		internal bool isInChain;

		// Token: 0x04000BB3 RID: 2995
		internal bool isHierarchyHidden;

		// Token: 0x04000BB4 RID: 2996
		internal bool localFlipsWinding;

		// Token: 0x04000BB5 RID: 2997
		internal bool localTransformScaleZero;

		// Token: 0x04000BB6 RID: 2998
		internal bool worldFlipsWinding;

		// Token: 0x04000BB7 RID: 2999
		internal ClipMethod clipMethod;

		// Token: 0x04000BB8 RID: 3000
		internal int childrenStencilRef;

		// Token: 0x04000BB9 RID: 3001
		internal int childrenMaskDepth;

		// Token: 0x04000BBA RID: 3002
		internal bool disableNudging;

		// Token: 0x04000BBB RID: 3003
		internal bool usesLegacyText;

		// Token: 0x04000BBC RID: 3004
		internal MeshHandle data;

		// Token: 0x04000BBD RID: 3005
		internal MeshHandle closingData;

		// Token: 0x04000BBE RID: 3006
		internal Matrix4x4 verticesSpace;

		// Token: 0x04000BBF RID: 3007
		internal int displacementUVStart;

		// Token: 0x04000BC0 RID: 3008
		internal int displacementUVEnd;

		// Token: 0x04000BC1 RID: 3009
		internal BMPAlloc transformID;

		// Token: 0x04000BC2 RID: 3010
		internal BMPAlloc clipRectID;

		// Token: 0x04000BC3 RID: 3011
		internal BMPAlloc opacityID;

		// Token: 0x04000BC4 RID: 3012
		internal BMPAlloc textCoreSettingsID;

		// Token: 0x04000BC5 RID: 3013
		internal BMPAlloc backgroundColorID;

		// Token: 0x04000BC6 RID: 3014
		internal BMPAlloc borderLeftColorID;

		// Token: 0x04000BC7 RID: 3015
		internal BMPAlloc borderTopColorID;

		// Token: 0x04000BC8 RID: 3016
		internal BMPAlloc borderRightColorID;

		// Token: 0x04000BC9 RID: 3017
		internal BMPAlloc borderBottomColorID;

		// Token: 0x04000BCA RID: 3018
		internal BMPAlloc tintColorID;

		// Token: 0x04000BCB RID: 3019
		internal float compositeOpacity;

		// Token: 0x04000BCC RID: 3020
		internal Color backgroundColor;

		// Token: 0x04000BCD RID: 3021
		internal VisualElement prevText;

		// Token: 0x04000BCE RID: 3022
		internal VisualElement nextText;

		// Token: 0x04000BCF RID: 3023
		internal List<RenderChainTextEntry> textEntries;

		// Token: 0x04000BD0 RID: 3024
		internal BasicNode<TextureEntry> textures;
	}
}
