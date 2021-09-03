﻿// Copyright Scott Whitney. All Rights Reserved.
// Reproduction or transmission in whole or in part, any form or by any
// means, electronic, mechanical or otherwise, is prohibited without the
// prior written consent of the copyright owner.

using System.Windows;
using _3DS_CivilSurveySuite.Model;
using _3DS_CivilSurveySuite.UI.ViewModels;
using _3DS_CivilSurveySuite.UI.Views;
using SimpleInjector;

namespace _3DS_CivilSurveySuite.C3D2017
{
    public static class C3DService
    {
        private static Container Container { get; } = new Container();

        /// <summary>
        /// Registers the service dependencies.
        /// </summary>
        public static void Register()
        {
            Container.Register<ISurfaceSelectService, SurfaceSelectService>(Lifestyle.Singleton);
            Container.Register<IConnectLineworkService, ConnectLineworkService>();
            Container.Register<ICogoPointViewerService, CogoPointViewerService>();

            Container.Register<SurfaceSelectView>();
            Container.Register<SurfaceSelectViewModel>();

            Container.Register<CogoPointMoveLabelView>();
            Container.Register<CogoPointMoveLabelViewModel>();

            Container.Register<CogoPointViewer>();
            Container.Register<CogoPointViewerViewModel>();

            Container.Verify();
        }

        public static void ShowWindow<TView>() where TView : Window
        {
            var view = CreateWindow<TView>();
            Autodesk.AutoCAD.ApplicationServices.Core.Application.ShowModelessWindow(view);
        }

        public static bool? ShowDialog<TView>() where TView : Window
        {
            var view = CreateWindow<TView>();
            return Autodesk.AutoCAD.ApplicationServices.Core.Application.ShowModalWindow(view);
        }

        private static Window CreateWindow<TView>() where TView : Window
        {
            return Container.GetInstance<TView>();
        }

        public static ISurfaceSelectService SurfaceSelect() => Container.GetInstance<ISurfaceSelectService>();

        public static IConnectLineworkService ConnectLinework() => Container.GetInstance<IConnectLineworkService>();

        public static ICogoPointViewerService CogoPointViewer() => Container.GetInstance<ICogoPointViewerService>();
    }
}