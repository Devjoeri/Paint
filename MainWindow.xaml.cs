using Microsoft.Win32;
using Paint.Commands;
using Paint.Composite;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using System.Diagnostics;
using Paint.Strategies;

namespace Paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum Modes { Rectangle, Ellipse, Selector, Drag, Resize }

        private Modes _currentMode = Modes.Rectangle;

        private Point _currentPoint; //the current mouse position
        private Point _startPoint;
        private Point _endPoint;
        private MasterShape _selectedShape;
        //List with all the selected shapes
        private List<MasterShape> _selectedShapes = new List<MasterShape>();
        //Composite tree
        private Tree _composite = new Tree();
        //Singleton Actionhandler because we only have one actionhandler
        private static ActionHandler ManagerUndoRedo = ActionHandler.GetInstance();
        /// <summary>
        /// constructor for the main window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RectangleBtn_Click(object sender, RoutedEventArgs e)
        {

            this._currentMode = Modes.Rectangle;
        }
        private void EllipseBtn_Click(object sender, RoutedEventArgs e)
        {
            this._currentMode = Modes.Ellipse;
        }

        private void DragBtn_Click(object sender, RoutedEventArgs e)
        {
            this._currentMode = Modes.Drag;
        }

        private void ResizeBtn_Click(object sender, RoutedEventArgs e)
        {
            this._currentMode = Modes.Resize;
        }
        private void SelectBtn_Click(object sender, RoutedEventArgs e)
        {

            this._currentMode = Modes.Selector;
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(this);
            HitTestResult result =
                VisualTreeHelper.HitTest(PaintCanvas, Mouse.GetPosition(PaintCanvas));
            if (result.VisualHit.GetType().BaseType == typeof(MasterShape))
            {
                _selectedShape = (MasterShape)result.VisualHit;
            }

        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            _currentPoint = e.GetPosition(PaintCanvas);
            // Update the X & Y as the mouse moves
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _endPoint = e.GetPosition(this);
            }

        }
        /// <summary>
        /// this function determines witch method has to be called based on te mode which the user has selected 
        /// </summary>
        /// <param name="sender">the mouse</param>
        /// <param name="e">the date for the selected mouse events</param>
        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            switch (_currentMode)
            {
                case Modes.Ellipse:
                    DrawEllipse();
                    break;
                case Modes.Rectangle:
                    DrawRectangle();
                    break;
                case Modes.Drag:
                    MoveShape();
                    break;
                case Modes.Resize:
                    Resizehape();
                    break;
                case Modes.Selector:
                    _selectedShapes.Add(_selectedShape);
                    break;
                default:
                    return;
            }
        }
        /// <summary>
        /// Option to resize the Shape if the selected tool is Resize
        /// </summary>
        private void Resizehape()
        {
            Resize resize = new Resize(_selectedShape, PaintCanvas);

            ManagerUndoRedo.AddCommand(resize);
        }
        /// <summary>
        /// Option to move the shape if the selected tool is Drag
        /// </summary>
        private void MoveShape()
        {
            Point previousPoint = _selectedShape.TranslatePoint(new Point(0, 0), PaintCanvas);

            Move move = new Move(FindGroup(_selectedShape), PaintCanvas, previousPoint, _currentPoint);
            ManagerUndoRedo.AddCommand(move);
        }

        private MasterShape FindGroup(MasterShape selectedShape)
        {
            return _composite.FindByShape(selectedShape);
        }

        /// <summary>
        /// Draw rectangle and put it in the Tree and command handler
        /// </summary>
        private void DrawRectangle()
        {
            BaseShape rec = new BaseShape(new RectangleStrategy(_endPoint.X - _startPoint.X, _endPoint.Y - _startPoint.Y))
            {   
                Fill = Brushes.Green,
                Stroke = Brushes.Red,
                StrokeThickness = 2,
            };

            Canvas.SetLeft(rec, _startPoint.X);
            Canvas.SetTop(rec, _startPoint.Y);

            var shape = makeOrnament(rec);

            Draw draw = new Draw(rec, PaintCanvas);

            
            ManagerUndoRedo.AddCommand(draw);
            _composite.Add(shape);
        }

        /// <summary>
        /// Draw ellipse and put it in the Tree and command handler
        /// </summary>
        private void DrawEllipse()
        {
            BaseShape ellipse = new BaseShape(new EllipseStrategy(_endPoint.X - _startPoint.X, _endPoint.Y - _startPoint.Y))
            {

                Fill = Brushes.Green,
                Stroke = Brushes.Red,
                StrokeThickness = 2,
            };
            
            Canvas.SetLeft(ellipse, _startPoint.X);
            Canvas.SetTop(ellipse, _startPoint.Y);

            var shape = makeOrnament(ellipse);

            Draw draw = new Draw(ellipse, PaintCanvas);

            
            ManagerUndoRedo.AddCommand(draw);
            _composite.Add(shape);
        }
        /// <summary>
        /// Undo command pattern click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        {
            ManagerUndoRedo.Undo();
        }
        /// <summary>
        /// Redo command pattern click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RedoBtn_Click(object sender, RoutedEventArgs e)
        {
            ManagerUndoRedo.Redo();

        }
        /// <summary>
        /// Make group button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MakeGroupBtn_Click(object sender, RoutedEventArgs e)
        {
            
            MakeGroup makeGroup = new MakeGroup(_selectedShapes, _composite);
            ManagerUndoRedo.AddCommand(makeGroup);
        }
        /// <summary>
        /// Save the compositie tree to a file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFileBtn_Click(object sender, RoutedEventArgs e)
        {
            string contextToSave = _composite.Display(_selectedShape, 0);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, contextToSave);
                
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //add ornament to the selected shape
            //_selectedShape;
        }
        private MasterShape makeOrnament(BaseShape shape)
        {
            Ornament ornament = new Ornament(shape);
            ornament.setLocation("top");
            ornament.setText(text.Text);
            ornament.Draw(PaintCanvas);
            return ornament;
        }
    }
}
