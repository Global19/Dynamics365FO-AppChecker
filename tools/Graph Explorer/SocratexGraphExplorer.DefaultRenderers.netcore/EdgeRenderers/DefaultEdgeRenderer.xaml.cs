﻿using Neo4j.Driver;
using SocratexGraphExplorer.Common;
using SocratexGraphExplorer.DefaultsPlugin;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SocratexGraphExplorer.DefaultsPlugin
{
    /// <summary>
    /// Interaction logic for the Default edge renderer, that is used when
    /// no other, more specialized, renderer is available.
    /// </summary>
    public partial class DefaultEdgeRenderer : UserControl, IEdgeRenderer
    {
        public ObservableCollection<PropertyItem> Properties { get; private set; }
        private IModel Model { get; set; }
        private IRelationship Edge { get; set; }

        public DefaultEdgeRenderer(IModel model)
        {
            InitializeComponent();

            this.Properties = new ObservableCollection<PropertyItem>();
            this.Model = model;

            this.DataContext = this;
        }

        /// <summary>
        /// Called when the user releases the right mouse in the property grid.
        /// </summary>
        /// <param name="sender">The control that is selected</param>
        /// <param name="args">The arguments. Not used.</param>
        public void OnMouseRightButtonUp(object sender, EventArgs args)
        {
            TextBlock control = sender as TextBlock;
            Clipboard.SetText(control.Text);
        }

        public void SelectEdgeAsync(IRelationship edge)
        {
            this.Edge = edge;
            this.Header.Text = string.Format("Edge {0}", edge.Type);

            this.Properties.Add(new PropertyItem() { Key = "Id", Value = edge.Id.ToString() });
            this.Properties.Add(new PropertyItem() { Key = "Start Node", Value = edge.StartNodeId.ToString() });
            this.Properties.Add(new PropertyItem() { Key = "End Node", Value = edge.EndNodeId.ToString() });

            foreach (var property in edge.Properties)
            {
                this.Properties.Add(new PropertyItem() { Key = property.Key, Value = property.Value.ToString() });
            }
        }
    }
}
