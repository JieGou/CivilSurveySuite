﻿using System.Windows;
using _3DS_CivilSurveySuite.ACAD;
using _3DS_CivilSurveySuite.CIVIL.Services;
using _3DS_CivilSurveySuite.Shared.Services.Interfaces;
using _3DS_CivilSurveySuite.UI.Services.Implementation;
using _3DS_CivilSurveySuite.UI.ViewModels;
using _3DS_CivilSurveySuite.UI.Views;
using SimpleInjector;

namespace _3DS_CivilSurveySuite.CIVIL
{
    /// <summary>
    /// Civil 3D IOC Container
    /// </summary>
    public static class Ioc
    {
        public static Container Default { get; } = new Container();

        public static void RegisterServices()
        {
            // CIVIL SERVICES
            Default.Register<ICivilSelectService, CivilSelectService>();
            Default.Register<ICogoPointService, CogoPointService>();

            Default.Register<IConnectLineworkService, ConnectLineworkService>();
            Default.Register<ICogoPointSurfaceReportService, CogoPointSurfaceReportService>();
            Default.Register<ICogoPointReplaceDuplicateService, CogoPointReplaceDuplicateService>();

            // DIALOGS
            Default.Register<IOpenFileDialogService, OpenFileDialogService>();
            Default.Register<ISaveFileDialogService, SaveFileDialogService>();
            Default.Register<IFolderBrowserDialogService, FolderBrowserDialogService>();

            // VIEWS AND VIEWMODELS
            Default.Register<SelectSurfaceView>();
            Default.Register<SelectSurfaceViewModel>();
            Default.Register<SelectPointGroupView>();
            Default.Register<SelectPointGroupViewModel>();
            Default.Register<SelectAlignmentView>();
            Default.Register<SelectAlignmentViewModel>();
            Default.Register<CogoPointMoveLabelView>();
            Default.Register<CogoPointMoveLabelViewModel>();
            Default.Register<ConnectLineworkView>();
            Default.Register<ConnectLineworkViewModel>();
            Default.Register<CogoPointEditorView>();
            Default.Register<CogoPointEditorViewModel>();
            Default.Register<CogoPointSurfaceReportView>();
            Default.Register<CogoPointSurfaceReportViewModel>();
            Default.Register<CogoPointReplaceDuplicateView>();
            Default.Register<CogoPointReplaceDuplicateViewModel>();

            Default.Verify(VerificationOption.VerifyAndDiagnose);
            AcadApp.Logger.Info("Civil3D Services registered successfully.");
        }

        public static Window GetRequiredView<TView>() where TView : Window
        {
            AcadApp.Logger.Info($"Creating instance of {typeof(TView)}");
            return Default.GetInstance<TView>();
        }
    }
}