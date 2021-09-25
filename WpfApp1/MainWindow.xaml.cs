using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

using System.Security.Cryptography;


using Microsoft.Msagl;
using Microsoft.Msagl.Core;
using Microsoft.Msagl.Core.GraphAlgorithms;
using Microsoft.Msagl.Core.Routing;
using Microsoft.Msagl.Core.ProjectionSolver;
using Microsoft.Msagl.Core.DataStructures;
using Microsoft.Msagl.WpfGraphControl;
using Microsoft.Msagl.Drawing;

using ColorSpace = Microsoft.Msagl.Drawing;
using ControlsSpace = System.Windows.Controls;
using BindSpace = System.Windows.Data;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> NodeList;
        private Graph mainGraph;
        AutomaticGraphLayoutControl MainGraphControl;
        List<NodeIdPair> CurrentLinks;
        int _edgeCount;
        GraphViewer GView;

        public int EdgeCount {
            get => _edgeCount; 
            set 
            { 
                _edgeCount = value;
                graphUpdate();
            } 
        }


        public MainWindow()
        {
            MainGraphControl = new AutomaticGraphLayoutControl();

            InitializeComponent();

            GraphInit();

        }

       

        void GraphInit()
        {

            //GView = new GraphViewer();
            MainGraphControl.Graph = new Graph("GRAPH", "1");
            var graph = mainGraph = MainGraphControl.Graph;

            //GView.BindToPanel(BruhPanel);
            //GView.Graph = graph;

            //GView.GraphCanvas.Background = Brushes.DarkCyan;

            graph.BoundingBox.ScaleAroundCenter(0.5);
            graph.Attr.BackgroundColor = ColorSpace.Color.Transparent;

            MainGraphControl.Background = Brushes.DarkRed;
            MainGrid.Children.Add(MainGraphControl);


            //----------
            //----------

            _edgeCount = 6;
            NodeList = new List<string>();
            graphUpdate();
            
        }

        void graphUpdate()
        {

            //GView.Graph = null;

            FillCollectionByRandom(NodeList);
            FillByStringCollection(NodeList);
            CreatePairByContext(item =>
            {
                List<NodeIdPair> Edges = new List<NodeIdPair>();
                Random RND = new Random();
                for (int i = 0; i < EdgeCount; ++i)
                {
                    Edges.Add(new NodeIdPair { first = item[RND.Next(0, item.Count)], second = item[RND.Next(0, item.Count)] });

                }
                return Edges;
            });

            SetPairEdges(CurrentLinks);

            mainGraph.GeometryGraph.UpdateBoundingBox();

            //GView.Graph = mainGraph;
            //GView.GraphCanvas.UpdateLayout();

            //mainGraph.Attr.LayerDirection = LayerDirection.LR;

        }

        void FillByStringCollection(List<string> inVals)
        {
            inVals.ForEach(i =>
            {
                if(!mainGraph.Nodes.Any(j => j.Id == i))
                {
                    Node cashe;
                    NodeStyling(cashe = mainGraph.AddNode(i));
                    cashe.LabelText = i;
                    Console.WriteLine($"setted: {i}");
                }
            
            });
        }

        public void NodeStyling(Node inVal)
        {
            inVal.Attr.FillColor = ColorSpace.Color.Cyan;
        }

        public struct NodeIdPair
        {
            public string first;
            public string second;
        }

        void SetPairEdges(List<NodeIdPair> inVals)
        {
            inVals.ForEach(i =>
            {
                if(!mainGraph.Edges.Any(j => j.Source == i.first && j.Target == i.second))
                mainGraph.AddEdge(i.first, i.second);
            });
        }

        void CreatePairByContext( Func<List<string>, List<NodeIdPair>> createCallback)
        {
            CurrentLinks = createCallback.Invoke(NodeList);
        }

        void FillCollectionByRandom(List<string> inVals)
        {
            for(int i = 0; i < 10; ++i)
            {
                if(!inVals.Contains($"Node_{i}"))
                    inVals.Add($"Node_{i}");
            }
        }

        private void EdgeCountInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            //string text = ((ControlsSpace.TextBox)sender).Text;
            //int val;
            //if (int.TryParse(text, out val))
            //{
            //    EdgeCount = val;
            //}

        }

        private void AddNode_Click(object sender, RoutedEventArgs e)
        {
            NodeList.Add(EdgeCountInput.Text);

            //NodeList.ForEach(i =>
            //{
            //    mainGraph.RemoveNode(mainGraph.FindNode(i));
            //});
            //mainGraph.Edges.ToList().ForEach(i =>
            //{
            //    mainGraph.RemoveEdge(i);
            //});
            //mainGraph.NodeMap.Clear();
            //NodeList.Add(EdgeCountInput.Text);
            //Node cashe = mainGraph.AddNode(EdgeCountInput.Text);
            //mainGraph.GeometryGraph.Nodes.FirstOrDefault(i => i==cashe.GeometryNode);

            //var Hno = mainGraph.AddNode($"{EdgeCountInput.Text}");
            //mainGraph.AddEdge(mainGraph.Nodes.FirstOrDefault().Id, $"{EdgeCountInput.Text}");

            graphUpdate();
        }
    }
}
