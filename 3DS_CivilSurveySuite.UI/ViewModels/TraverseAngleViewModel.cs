﻿// Copyright Scott Whitney. All Rights Reserved.
// Reproduction or transmission in whole or in part, any form or by any
// means, electronic, mechanical or otherwise, is prohibited without the
// prior written consent of the copyright owner.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using _3DS_CivilSurveySuite.Core;
using _3DS_CivilSurveySuite.Model;
using _3DS_CivilSurveySuite.UI.Services;

namespace _3DS_CivilSurveySuite.UI.ViewModels
{
    public class TraverseAngleViewModel : ViewModelBase
    {
        private string _closeBearing;
        private string _closeDistance;
        private readonly ITraverseService _traverseService;
        private readonly IProcessService _processService;
        private readonly IMessageBoxService _messageBoxService;

        public ObservableCollection<TraverseAngleObject> TraverseAngles { get; } = new ObservableCollection<TraverseAngleObject>();

        public TraverseAngleObject SelectedTraverseAngle { get; set; }

        //BUG: When DataGrid Combobox for this property is changed, the GridUpdateCommand Event is not fired.
        //Probably need to look into ComboBox events and use behaviors to fire the update.
        public static IEnumerable<AngleReferenceDirection> ReferenceDirectionValues => Enum.GetValues(typeof(AngleReferenceDirection)).Cast<AngleReferenceDirection>();

        //BUG: When DataGrid Combobox for this property is changed, the GridUpdateCommand Event is not fired.
        public static IEnumerable<AngleRotationDirection> RotationDirectionValues => Enum.GetValues(typeof(AngleRotationDirection)).Cast<AngleRotationDirection>();

        public string CloseDistance
        {
            get => _closeDistance;
            set
            {
                _closeDistance = value;
                NotifyPropertyChanged();
            }
        }

        public string CloseBearing
        {
            get => _closeBearing;
            set
            {
                _closeBearing = value;
                NotifyPropertyChanged();
            }
        }

        public RelayCommand AddRowCommand => new RelayCommand(AddRow, () => true);

        public RelayCommand RemoveRowCommand => new RelayCommand(RemoveRow, () => true);

        public RelayCommand ClearCommand => new RelayCommand(ClearTraverse, () => true);

        public RelayCommand DrawCommand => new RelayCommand(DrawTraverse, () => true);

        public RelayCommand FeetToMetersCommand => new RelayCommand(FeetToMeters, () => true);

        public RelayCommand LinksToMetersCommand => new RelayCommand(LinksToMeters, () => true);

        public RelayCommand FlipBearingCommand => new RelayCommand(FlipBearing, () => true);

        public RelayCommand ShowHelpCommand => new RelayCommand(ShowHelp, () => true);

        public RelayCommand GridUpdatedCommand => new RelayCommand(GridUpdated, () => true);

        public RelayCommand SetBasePointCommand => new RelayCommand(SetBasePoint, () => true);

        public RelayCommand CloseWindowCommand => new RelayCommand(CloseWindow, () => true);

        public RelayCommand SelectLineCommand => new RelayCommand(SelectLine, () => true);

        public RelayCommand ZoomExtentsCommand => new RelayCommand(Zoom, () => true);

        public TraverseAngleViewModel(ITraverseService traverseService, IProcessService processService, IMessageBoxService messageBoxService)
        {
            _traverseService = traverseService;
            _processService = processService;
            _messageBoxService = messageBoxService;
        }

        private void SetBasePoint()
        {
            _traverseService?.SetBasePoint();

            if (TraverseAngles.Count > 0)
            {
                _traverseService?.DrawTransientLines(TraverseAngles);
            }
        }

        private void Zoom()
        {
            _traverseService?.ZoomTo(TraverseAngles);
        }

        private void SelectLine()
        {
            var angDist = _traverseService.SelectLine();
            TraverseAngles.Add(new TraverseAngleObject(angDist.Angle.ToDouble(), angDist.Distance));
        }

        private void CloseWindow()
        {
            _traverseService?.ClearGraphics();
        }

        private void GridUpdated()
        {
            CloseTraverse();
            _traverseService?.DrawTransientLines(TraverseAngles);
        }

        private void CloseTraverse()
        {
            if (TraverseAngles.Count < 2)
            {
                return;
            }

            var coordinates = PointHelpers.TraverseObjectsToCoordinates(TraverseAngles, new Point(0, 0));

            double distance = PointHelpers.GetDistanceBetweenPoints(coordinates[coordinates.Count - 1], coordinates[0]);
            Angle angle = AngleHelpers.GetAngleBetweenPoints(coordinates[coordinates.Count - 1], coordinates[0]);

            CloseDistance = $"{distance:0.000}";
            CloseBearing = angle.ToString();
        }

        private void AddRow()
        {
            var ti = new TraverseAngleObject();
            TraverseAngles.Add(ti);

            //hack: add index property and update method
            UpdateIndex();
        }

        private void RemoveRow()
        {
            if (SelectedTraverseAngle == null)
            {
                return;
            }

            _ = TraverseAngles.Remove(SelectedTraverseAngle);
            UpdateIndex();
        }

        private void ClearTraverse()
        {
            TraverseAngles.Clear();
        }

        private void DrawTraverse()
        {
            _traverseService?.DrawLines(TraverseAngles);
        }

        private void FeetToMeters()
        {
            if (SelectedTraverseAngle == null) return;

            int index = TraverseAngles.IndexOf(SelectedTraverseAngle);

            double distance = TraverseAngles[index].Distance;
            TraverseAngles[index].Distance = MathHelpers.ConvertFeetToMeters(distance);
            CloseTraverse();
        }

        private void LinksToMeters()
        {
            if (SelectedTraverseAngle == null) return;

            int index = TraverseAngles.IndexOf(SelectedTraverseAngle);

            double distance = TraverseAngles[index].Distance;
            TraverseAngles[index].Distance = MathHelpers.ConvertLinkToMeters(distance);
            CloseTraverse();
        }

        private void FlipBearing()
        {
            if (SelectedTraverseAngle == null)
                return;

            Angle angle;
            if (SelectedTraverseAngle.Angle.Degrees > 180)
                angle = SelectedTraverseAngle.Angle - new Angle(180);
            else
                angle = SelectedTraverseAngle.Angle + new Angle(180);

            SelectedTraverseAngle.Bearing = angle.ToDouble();
            CloseTraverse();
        }

        private void ShowHelp() //TODO: Move help file string to resource or something.
        {
            _processService?.Start(@"Resources\3DSCivilSurveySuite.chm");
        }

        private void UpdateIndex()
        {
            for (var i = 0; i < TraverseAngles.Count; i++)
            {
                TraverseAngles[i].Index = i;
            }
        }
    }
}