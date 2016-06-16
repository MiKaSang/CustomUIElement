using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ESRI.ArcGIS.ArcMapUI;

namespace CustomUIElement
{
    public class ZoomToLayer : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public ZoomToLayer()
        {
        }


        private void ZoomToActiveLayerInTOC(IMxDocument mxDocument)
        {
            if (mxDocument == null)
            {
                return;
            }
            ESRI.ArcGIS.Carto.IActiveView activeView = mxDocument.ActiveView;
            // Get the TOC
            ESRI.ArcGIS.ArcMapUI.IContentsView IContentsView = mxDocument.CurrentContentsView;
            // Get the selected layer
            System.Object selectedItem = IContentsView.SelectedItem;
            if (!(selectedItem is ESRI.ArcGIS.Carto.ILayer))
            {
                return;
            }
            ESRI.ArcGIS.Carto.ILayer layer = selectedItem as ESRI.ArcGIS.Carto.ILayer;
            // Zoom to the extent of the layer and refresh the map
            activeView.Extent = layer.AreaOfInterest;
            activeView.Refresh();
        }
        protected override void OnClick()
        {
            //
            //  TODO: Sample code showing how to access button host
            //
            ZoomToActiveLayerInTOC(ArcMap.Application.Document as IMxDocument);
            ArcMap.Application.CurrentTool = null;
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
