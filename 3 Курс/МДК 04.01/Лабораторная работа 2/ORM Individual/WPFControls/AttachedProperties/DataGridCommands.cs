using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ORM_Individual.WPFControls.AttachedProperties
{
    public static class DataGridCommands
    {
        public static readonly DependencyProperty RowEditEndingCommandProperty =
            DependencyProperty.RegisterAttached(
                "RowEditEndingCommand",
                typeof(ICommand),
                typeof(DataGridCommands),
                new PropertyMetadata(null, OnRowEditEndingCommandChanged));

        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.RegisterAttached(
                "DeleteCommand",
                typeof(ICommand),
                typeof(DataGridCommands),
                new PropertyMetadata(null, OnDeleteCommandChanged));

        public static void SetRowEditEndingCommand(DependencyObject element, ICommand? value) =>
            element.SetValue(RowEditEndingCommandProperty, value);

        public static ICommand? GetRowEditEndingCommand(DependencyObject element) =>
            (ICommand?)element.GetValue(RowEditEndingCommandProperty);

        public static void SetDeleteCommand(DependencyObject element, ICommand? value) =>
            element.SetValue(DeleteCommandProperty, value);

        public static ICommand? GetDeleteCommand(DependencyObject element) =>
            (ICommand?)element.GetValue(DeleteCommandProperty);

        private static void OnRowEditEndingCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not DataGrid dataGrid)
            {
                return;
            }

            dataGrid.RowEditEnding -= HandleRowEditEnding;

            if (e.NewValue != null)
            {
                dataGrid.RowEditEnding += HandleRowEditEnding;
            }
        }

        private static void OnDeleteCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not DataGrid dataGrid)
            {
                return;
            }

            dataGrid.PreviewKeyDown -= HandlePreviewKeyDown;

            if (e.NewValue != null)
            {
                dataGrid.PreviewKeyDown += HandlePreviewKeyDown;
            }
        }

        private static void HandleRowEditEnding(object? sender, DataGridRowEditEndingEventArgs e)
        {
            if (sender is not DataGrid dataGrid || e.EditAction != DataGridEditAction.Commit)
            {
                return;
            }

            var command = GetRowEditEndingCommand(dataGrid);
            var parameter = e.Row.Item;

            if (parameter != null && command?.CanExecute(parameter) == true)
            {
                command.Execute(parameter);
            }
        }

        private static void HandlePreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete || sender is not DataGrid dataGrid)
            {
                return;
            }

            var command = GetDeleteCommand(dataGrid);
            var parameter = dataGrid.SelectedItem;

            if (parameter != null && command?.CanExecute(parameter) == true)
            {
                command.Execute(parameter);
            }
        }
    }
}
