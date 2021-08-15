﻿// Copyright Scott Whitney. All Rights Reserved.
// Reproduction or transmission in whole or in part, any form or by any
// means, electronic, mechanical or otherwise, is prohibited without the
// prior written consent of the copyright owner.

using _3DS_CivilSurveySuite.ACAD2017;
using _3DS_CivilSurveySuite.ACAD2017.Services;
using _3DS_CivilSurveySuite.UI.Views;
using _3DS_CivilSurveySuite.ViewModels;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;

namespace _3DS_CivilSurveySuite.C3D2017
{
    /// <summary>
    /// PaletteFactory class for hooking up Views and ViewModels to be
    /// displayed as Palettes in AutoCAD Civil3D.
    /// </summary>
    public static class C3DPalettes
    {
        private static bool s_paletteVisible;
        private static readonly IPaletteService s_paletteService;

        static C3DPalettes()
        {
            s_paletteService = ServiceLocator.Container.GetInstance<IPaletteService>();
        }

        [CommandMethod("3DSShowConnectLinePalette")]
        public static void ShowConnectLinePalette()
        {
            var view = new ConnectLineworkView();
            var vm = new ConnectLineworkViewModel("Properties.Settings.Default.ConnectLineworkFileName", new ConnectLineworkService());
            s_paletteService.GeneratePalette(view, vm, "Linework");
        }

        private static void DocumentManager_DocumentActivated(object sender, DocumentCollectionEventArgs e)
        {
            if (s_paletteService.PaletteSet == null)
            {
                return;
            }

            s_paletteService.PaletteSet.Visible = e.Document != null && s_paletteVisible;
        }

        private static void DocumentManager_DocumentCreated(object sender, DocumentCollectionEventArgs e)
        {
            if (s_paletteService.PaletteSet == null)
            {
                return;
            }

            s_paletteService.PaletteSet.Visible = s_paletteVisible;
        }

        private static void DocumentManager_DocumentToBeDeactivated(object sender, DocumentCollectionEventArgs e)
        {
            if (s_paletteService.PaletteSet == null)
            {
                return;
            }

            s_paletteVisible = s_paletteService.PaletteSet.Visible;
        }

        private static void DocumentManager_DocumentToBeDestroyed(object sender, DocumentCollectionEventArgs e)
        {
            if (s_paletteService.PaletteSet == null)
            {
                return;
            }

            s_paletteVisible = s_paletteService.PaletteSet.Visible;

            if (AcadApp.DocumentManager.Count == 1)
            {
                s_paletteService.PaletteSet.Visible = false;
            }
        }

        public static void HookupEvents()
        {
            AcadApp.DocumentManager.DocumentActivated += DocumentManager_DocumentActivated;
            AcadApp.DocumentManager.DocumentCreated += DocumentManager_DocumentCreated;
            AcadApp.DocumentManager.DocumentToBeDeactivated += DocumentManager_DocumentToBeDeactivated;
            AcadApp.DocumentManager.DocumentToBeDestroyed += DocumentManager_DocumentToBeDestroyed;
        }

        public static void UnhookEvents()
        {
            AcadApp.DocumentManager.DocumentActivated -= DocumentManager_DocumentActivated;
            AcadApp.DocumentManager.DocumentCreated -= DocumentManager_DocumentCreated;
            AcadApp.DocumentManager.DocumentToBeDeactivated -= DocumentManager_DocumentToBeDeactivated;
            AcadApp.DocumentManager.DocumentToBeDestroyed -= DocumentManager_DocumentToBeDestroyed;
        }
    }
}