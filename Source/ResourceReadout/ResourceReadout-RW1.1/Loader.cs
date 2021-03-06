﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using Verse;
using System.Reflection;

namespace ResourceReadout
{
	[StaticConstructorOnStartup]
	public static class Loader
	{
		private static MethodInfo mi_Selector_ShiftIsHeld = AccessTools.PropertyGetter(typeof(Selector), "ShiftIsHeld");

		/*
		Select all instances of the thingDef in the current map's stockpiles.
		Supports shift-click for adding to selection.
		*/
		public static void SelectAllOnMap(ThingDef thingDef)
		{
			var things = Verse.Find.CurrentMap.spawnedThings.Where(x => x.def == thingDef && x.IsInAnyStorage());

			var selector = Verse.Find.Selector;
			if (!(bool)mi_Selector_ShiftIsHeld.Invoke(selector, new object[0]))
			{
				selector.ClearSelection();
			}
			things.Do(x => selector.Select(x));
		}

		static Loader()
		{
			const string Id = "com.saucypigeon.rimworld.mod.resourcereadout";
#if DEBUG
			Harmony.DEBUG = true;
			Log.Warning($"{Id} is in debug mode. Please contact the mod author if you see this.");
#endif
			var harmony = new Harmony(Id);
			harmony.PatchAll(Assembly.GetExecutingAssembly());
		}
	}
}
