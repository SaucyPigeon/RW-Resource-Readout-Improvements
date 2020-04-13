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
		/*
		Add new Widgets.ButtonInvisible that allows user to select all items that are in the colony's stockpiles.
		*/
		[HarmonyPatch("DoThingDef")]
		[HarmonyTranspiler]
		public static IEnumerable<CodeInstruction> DoThingDef_Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator ilGenerator)
		{
			var mi_Widgets_ButtonInvisible = AccessTools.Method(typeof(Widgets), nameof(Widgets.ButtonInvisible));
			var mi_Patch_SelectAllOnMap = AccessTools.Method(typeof(Loader), nameof(Loader.SelectAllOnMap));

			var ldarg_0_last = instructions.Where(x => x.IsLdarg(0)).Last();

			foreach (var instruction in instructions)
			{
				if (instruction == ldarg_0_last)
				{
					// if (Widgets.ButtonInvisible(rect, false))
					// {
					//     SelectAllOnMap(thingDef)
					// }
					var jumpToEnd = ilGenerator.DefineLabel();

					yield return new CodeInstruction(OpCodes.Ldloc_2);
					yield return new CodeInstruction(OpCodes.Ldc_I4_0);
					yield return new CodeInstruction(OpCodes.Call, mi_Widgets_ButtonInvisible);

					yield return new CodeInstruction(OpCodes.Brfalse_S, jumpToEnd);

					yield return new CodeInstruction(OpCodes.Ldarg_1);
					yield return new CodeInstruction(OpCodes.Call, mi_Patch_SelectAllOnMap);

					yield return new CodeInstruction(OpCodes.Nop) { labels = new List<Label>() { jumpToEnd } };
				}
				yield return instruction;
			}
		}

		/*
		Update which nodes are open in the resource readout.
		*/
		[HarmonyPatch(nameof(Listing_ResourceReadout.DoCategory))]
		[HarmonyPostfix]
		public static void DoCategory_Postfix(TreeNode_ThingCategory node, int openMask)
		{
			var gc = Current.Game.GetComponent<ResourceReadout_GameComponent>();
			
			if (node.IsOpen(openMask))
			{
				gc.NodeOpenTracker.Add(node);
			}
			else
			{
				gc.NodeOpenTracker.Remove(node);
			}
		}
	}
}
