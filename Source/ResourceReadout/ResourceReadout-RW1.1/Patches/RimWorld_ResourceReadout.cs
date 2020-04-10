﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using HarmonyLib;
using Verse;

namespace ResourceReadout.Patches
{
	/*
	This logging shows that upon game load, the first root ThingCategoryDef treenodes's openBits is set to
	all positive, whereas other treenodes are all zero. This means that the first node will always be, by
	default, open.

	The exact cause of this has not been determined.
	*/
#if DEBUG
	[HarmonyPatch(typeof(RimWorld.ResourceReadout))]
	public static class RimWorld_ResourceReadout
	{
		[HarmonyPatch("DoReadoutCategorized")]
		[HarmonyPrefix]
		public static void Prefix(List<ThingCategoryDef> ___RootThingCategories)
		{
			Log.Message("RimWorld.DoReadoutCategorized_Prefix (start)");
			foreach (var rootThingCategory in ___RootThingCategories)
			{
				Log.Message($"defName: {rootThingCategory.defName}");
				var treeNode = rootThingCategory.treeNode;
				var field = AccessTools.Field(typeof(TreeNode_ThingCategory), "openBits");

				int x = (int)field.GetValue(treeNode);
				Log.Message($"Open bits: {Convert.ToString(x, 2)}");
			}
			Log.Message("RimWorld.DoReadoutCategorized_Prefix (end)");
		}
	}
#endif
}