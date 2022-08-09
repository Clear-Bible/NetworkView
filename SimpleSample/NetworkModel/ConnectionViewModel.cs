using System;
using System.Diagnostics;
using System.Windows;
using Utils;

namespace NetworkModel
{
    /// <summary>
    /// Defines a connection between two connectors (aka connection points) of two nodes.
    /// </summary>
    public sealed class ConnectionViewModel : AbstractModelBase
    {
        #region Internal Data Members

        /// <summary>
        /// The source connector the connection is attached to.
        /// </summary>
        private ConnectorViewModel _sourceConnector;

        /// <summary>
        /// The destination connector the connection is attached to.
        /// </summary>
        private ConnectorViewModel _destConnector;

        /// <summary>
        /// The source and dest hotspots used for generating connection points.
        /// </summary>
        private Point _sourceConnectorHotspot;
        private Point _destConnectorHotspot;

        #endregion Internal Data Members

        /// <summary>
        /// The source connector the connection is attached to.
        /// </summary>
        public ConnectorViewModel SourceConnector
        {
            get => _sourceConnector;
            set
            {
                if (_sourceConnector == value)
                {
                    return;
                }

                if (_sourceConnector != null)
                {
                    Trace.Assert(_sourceConnector.AttachedConnection == this);

                    _sourceConnector.AttachedConnection = null;
                    _sourceConnector.HotspotUpdated -= SourceConnectorHotspotUpdated;
                }

                _sourceConnector = value;

                if (_sourceConnector != null)
                {
                    Trace.Assert(_sourceConnector.AttachedConnection == null);

                    _sourceConnector.AttachedConnection = this;
                    _sourceConnector.HotspotUpdated += SourceConnectorHotspotUpdated;
                    SourceConnectorHotspot = _sourceConnector.Hotspot;
                }

                OnPropertyChanged("SourceConnector");
            }
        }

        /// <summary>
        /// The destination connector the connection is attached to.
        /// </summary>
        public ConnectorViewModel DestConnector
        {
            get => _destConnector;
            set
            {
                if (_destConnector == value)
                {
                    return;
                }

                if (_destConnector != null)
                {
                    Trace.Assert(_destConnector.AttachedConnection == this);

                    _destConnector.AttachedConnection = null;
                    _destConnector.HotspotUpdated += DestinationConnectorHotspotUpdated;
                }

                _destConnector = value;

                if (_destConnector != null)
                {
                    Trace.Assert(_destConnector.AttachedConnection == null);

                    _destConnector.AttachedConnection = this;
                    _destConnector.HotspotUpdated += DestinationConnectorHotspotUpdated;
                    DestConnectorHotspot = _destConnector.Hotspot;
                }

                OnPropertyChanged("DestConnector");
            }
        }

        /// <summary>
        /// The source and dest hotspots used for generating connection points.
        /// </summary>
        public Point SourceConnectorHotspot
        {
            get => _sourceConnectorHotspot;
            set
            {
                _sourceConnectorHotspot = value;

                OnPropertyChanged("SourceConnectorHotspot");
            }
        }

        public Point DestConnectorHotspot
        {
            get => _destConnectorHotspot;
            set
            {
                _destConnectorHotspot = value;

                OnPropertyChanged("DestConnectorHotspot");
            }
        }

        #region Private Methods

        /// <summary>
        /// Event raised when the hotspot of the source connector has been updated.
        /// </summary>
        private void SourceConnectorHotspotUpdated(object sender, EventArgs e)
        {
            SourceConnectorHotspot = SourceConnector.Hotspot;
        }

        /// <summary>
        /// Event raised when the hotspot of the dest connector has been updated.
        /// </summary>
        private void DestinationConnectorHotspotUpdated(object sender, EventArgs e)
        {
            DestConnectorHotspot = DestConnector.Hotspot;
        }

        #endregion Private Methods
    }
}
