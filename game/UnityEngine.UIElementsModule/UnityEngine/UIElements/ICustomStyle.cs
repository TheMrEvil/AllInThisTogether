using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000274 RID: 628
	public interface ICustomStyle
	{
		// Token: 0x060013A1 RID: 5025
		bool TryGetValue(CustomStyleProperty<float> property, out float value);

		// Token: 0x060013A2 RID: 5026
		bool TryGetValue(CustomStyleProperty<int> property, out int value);

		// Token: 0x060013A3 RID: 5027
		bool TryGetValue(CustomStyleProperty<bool> property, out bool value);

		// Token: 0x060013A4 RID: 5028
		bool TryGetValue(CustomStyleProperty<Color> property, out Color value);

		// Token: 0x060013A5 RID: 5029
		bool TryGetValue(CustomStyleProperty<Texture2D> property, out Texture2D value);

		// Token: 0x060013A6 RID: 5030
		bool TryGetValue(CustomStyleProperty<Sprite> property, out Sprite value);

		// Token: 0x060013A7 RID: 5031
		bool TryGetValue(CustomStyleProperty<VectorImage> property, out VectorImage value);

		// Token: 0x060013A8 RID: 5032
		bool TryGetValue<T>(CustomStyleProperty<T> property, out T value) where T : Object;

		// Token: 0x060013A9 RID: 5033
		bool TryGetValue(CustomStyleProperty<string> property, out string value);
	}
}
