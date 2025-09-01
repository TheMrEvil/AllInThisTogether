using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000227 RID: 551
	internal class PropagationPaths
	{
		// Token: 0x060010D8 RID: 4312 RVA: 0x00043184 File Offset: 0x00041384
		public PropagationPaths()
		{
			this.trickleDownPath = new List<VisualElement>(16);
			this.targetElements = new List<VisualElement>(4);
			this.bubbleUpPath = new List<VisualElement>(16);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x000431B4 File Offset: 0x000413B4
		public PropagationPaths(PropagationPaths paths)
		{
			this.trickleDownPath = new List<VisualElement>(paths.trickleDownPath);
			this.targetElements = new List<VisualElement>(paths.targetElements);
			this.bubbleUpPath = new List<VisualElement>(paths.bubbleUpPath);
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x000431F4 File Offset: 0x000413F4
		internal static PropagationPaths Copy(PropagationPaths paths)
		{
			PropagationPaths propagationPaths = PropagationPaths.s_Pool.Get();
			propagationPaths.trickleDownPath.AddRange(paths.trickleDownPath);
			propagationPaths.targetElements.AddRange(paths.targetElements);
			propagationPaths.bubbleUpPath.AddRange(paths.bubbleUpPath);
			return propagationPaths;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x00043248 File Offset: 0x00041448
		public static PropagationPaths Build(VisualElement elem, EventBase evt, PropagationPaths.Type pathTypesRequested)
		{
			bool flag = elem == null || pathTypesRequested == PropagationPaths.Type.None;
			PropagationPaths result;
			if (flag)
			{
				result = null;
			}
			else
			{
				PropagationPaths propagationPaths = PropagationPaths.s_Pool.Get();
				propagationPaths.targetElements.Add(elem);
				while (elem.hierarchy.parent != null)
				{
					elem = elem.hierarchy.parent;
					bool flag2 = elem.isCompositeRoot && !evt.ignoreCompositeRoots;
					if (flag2)
					{
						propagationPaths.targetElements.Add(elem);
					}
					else
					{
						bool flag3 = (pathTypesRequested & PropagationPaths.Type.TrickleDown) == PropagationPaths.Type.TrickleDown && elem.HasTrickleDownHandlers();
						if (flag3)
						{
							propagationPaths.trickleDownPath.Add(elem);
						}
						bool flag4 = (pathTypesRequested & PropagationPaths.Type.BubbleUp) == PropagationPaths.Type.BubbleUp && elem.HasBubbleUpHandlers();
						if (flag4)
						{
							propagationPaths.bubbleUpPath.Add(elem);
						}
					}
				}
				result = propagationPaths;
			}
			return result;
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0004332E File Offset: 0x0004152E
		public void Release()
		{
			this.bubbleUpPath.Clear();
			this.targetElements.Clear();
			this.trickleDownPath.Clear();
			PropagationPaths.s_Pool.Release(this);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00043361 File Offset: 0x00041561
		// Note: this type is marked as 'beforefieldinit'.
		static PropagationPaths()
		{
		}

		// Token: 0x04000767 RID: 1895
		private static readonly ObjectPool<PropagationPaths> s_Pool = new ObjectPool<PropagationPaths>(100);

		// Token: 0x04000768 RID: 1896
		public readonly List<VisualElement> trickleDownPath;

		// Token: 0x04000769 RID: 1897
		public readonly List<VisualElement> targetElements;

		// Token: 0x0400076A RID: 1898
		public readonly List<VisualElement> bubbleUpPath;

		// Token: 0x0400076B RID: 1899
		private const int k_DefaultPropagationDepth = 16;

		// Token: 0x0400076C RID: 1900
		private const int k_DefaultTargetCount = 4;

		// Token: 0x02000228 RID: 552
		[Flags]
		public enum Type
		{
			// Token: 0x0400076E RID: 1902
			None = 0,
			// Token: 0x0400076F RID: 1903
			TrickleDown = 1,
			// Token: 0x04000770 RID: 1904
			BubbleUp = 2
		}
	}
}
