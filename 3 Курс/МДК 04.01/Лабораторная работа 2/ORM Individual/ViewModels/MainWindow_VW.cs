using ORM_Individual.Models;
using ORM_Individual.Models.Entities;
using ORM_Individual.Models.Repositories;
using ORM_Individual.ViewModels.Commands;
using ORM_Individual.Views.TablePages;
using ORM_Individual.WPFControls.AttachedProperties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ORM_Individual.ViewModels.TableViewModels;

namespace ORM_Individual.ViewModels
{
    public class MainWindow_VW : Base_VM
    {
        public ICommand AddTableCommand { get; }
        public ICommand DeleteTableCommand { get; }
        public ObservableCollection<string> Tables { get; set; }
        public ObservableCollection<Border> TableFrames { get; set; }
        private readonly Dictionary<string, object> _pages;
        public MainWindow_VW()
        {
            _pages = new Dictionary<string, object> {
            { "Component", new ComponentPage()},
            { "ComponentType", new ComponentTypePage()},
            { "Employee", new EmployeePage()},
            { "Order", new OrderPage()},
            { "Position", new PositionPage()},
            { "Service", new ServicePage()},
            { "Customer", new CustomerPage()}
            };

            Tables = new ObservableCollection<string>(_pages.Keys);
            TableFrames = new ObservableCollection<Border>();
            AddTableCommand = new RelayCommand(AddTable);
            DeleteTableCommand = new RelayCommand(DeleteTable);
        }
        public void DeleteTable(object parameter)
        {
            if(parameter is Border border)
            {
                TableFrames.Remove(border);
            }
        }
        public void AddTable(object parameter)
        {
            if (parameter == null) return;

            string tableName = parameter.ToString();
            if (_pages.ContainsKey(tableName))
            {
                var frame = new Frame();
                frame.Navigate(_pages[tableName]);

                var border = new Border
                {
                    Background = Brushes.LightBlue,
                    BorderBrush = Brushes.DarkBlue,
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(5)
                };

                var mi = new MenuItem();
                mi.Header = "Delete Table";
                mi.Command = DeleteTableCommand;
                mi.CommandParameter = border;

                var cm = new ContextMenu();
                cm.Items.Add(mi);
                border.ContextMenu = cm;
                DragBehavior.SetIsDraggable(border, true);

                border.Child = frame;

                Canvas.SetLeft(border, 50 + (TableFrames.Count * 20));
                Canvas.SetTop(border, 50 + (TableFrames.Count * 20));

                TableFrames.Add(border);
                OnPropertyChanged(nameof(TableFrames));
            }
        }
    }
}
