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

namespace ORM_Individual.ViewModels
{
    public class MainWindow_VW : Base_VM
    {
        public ICommand AddTableCommand { get; }
        public ObservableCollection<string> Tables { get; set; }
        public ObservableCollection<FrameworkElement> TableFrames { get; set; }
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
            TableFrames = new ObservableCollection<FrameworkElement>();
            AddTableCommand = new RelayCommand(AddTable);
        }

        public void AddTable(object parameter)
        {
            if (parameter == null) return;

            string tableName = parameter.ToString();
            if (_pages.ContainsKey(tableName))
            {
                var border = new Border
                {
                    Width = 500,
                    Height = 100,
                    Background = Brushes.LightBlue,
                    BorderBrush = Brushes.DarkBlue,
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(5)
                };

                DragBehavior.SetIsDraggable(border, true);

                var frame = new Frame();
                frame.Navigate(_pages[tableName]);
                border.Child = frame;

                Canvas.SetLeft(border, 50 + (TableFrames.Count * 20));
                Canvas.SetTop(border, 50 + (TableFrames.Count * 20));

                TableFrames.Add(border);
                OnPropertyChanged(nameof(TableFrames));
            }
        }
    }

}
