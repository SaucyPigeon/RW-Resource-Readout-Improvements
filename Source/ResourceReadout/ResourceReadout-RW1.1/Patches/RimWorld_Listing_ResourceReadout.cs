using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Verse;
using RimWorld;
using UnityEngine;
using System.Reflection.Emit;

namespace ResourceReadout.Patches
{
	[HarmonyPatch(typeof(Listing_ResourceReadout))]
	public static class RimWorld_Listing_ResourceReadout
	{
//#if DEBUG
		[HarmonyPatch(nameof(Listing_ResourceReadout.DoCategory))]
		[HarmonyTranspiler]
		public static IEnumerable<CodeInstruction> DoCategory_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator)
		{
			var mi_GUI_DrawTexture = AccessTools.Method(typeof(GUI), nameof(GUI.DrawTexture), parameters: new[] { typeof(Rect), typeof(Texture) });
			var mi_Widgets_ButtonInvisible = AccessTools.Method(typeof(Widgets), nameof(Widgets.ButtonInvisible));

			bool flag = false;

			foreach (var instruction in instructions)
			{
				yield return instruction;

				if (!flag && instruction.Calls(mi_GUI_DrawTexture))
				{
					flag = true;

					var jumpToEnd = new Label();

					// if (Widgets.ButtonInvisible(rect, false))
					// {
					//     SelectAllOnMap(thingCategoryDef)
					// }
					yield return new CodeInstruction(OpCodes.Ldloc_1);
					yield return new CodeInstruction(OpCodes.Ldc_I4_0);
					yield return new CodeInstruction(OpCodes.Call, mi_Widgets_ButtonInvisible);
				}
			}
		}
//#endif



		[HarmonyPatch(nameof(Listing_ResourceReadout.DoCategory))]
		[HarmonyPostfix]
		public static void DoCategory_Postfix(TreeNode_ThingCategory node, int openMask)
		{
//#if DEBUG
//			Log.Message("RimWorld.Listing_ResourceReadout_Postfix (start)");
//#endif

			if (Loader.NodeOpenTracker == null)
			{
				Loader.NodeOpenTracker = new NodeOpenTracker();
			}

			Loader.NodeOpenTracker.SetNode(node, openMask);

//#if DEBUG
//			Log.Message("RimWorld.Listing_ResourceReadout_Postfix (finish)");
//#endif
		}
	}
}
