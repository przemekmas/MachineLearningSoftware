using MachineLearningSoftware.Controls.Entities;
using MachineLearningSoftware.DataAccess;
using MachineLearningSoftware.ViewModels;
using MachineLearningSoftware.Views.Entities;
using MachineLearningSoftware.Views.Views;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace MachineLearningSoftware.Views.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CleanDataViewModel : BaseViewModel
    {
        private readonly ExceptionLogDataAccess _exceptionLogDataAccess;
        private bool _isCorruptValuesChecked;
        private bool _isShowDuplicatesChecked;
        private bool _isLimitRowsChecked;
        private string _fileName;
        private string _regularExpression = "[!@#$%^&*(),.?\":{ }|]";
        private int _selectedPredictionColumnIndex;
        private int _selectedVisualisationColumnIndex;
        private int _duplicateCount;
        private int _selectedColumnIndex;
        private int _corruptValueCount;
        private int _rowReadLimit;
        private object _lockPredictionValuesObject = new object();
        private Dictionary<int, string> _columnsDictionary = new Dictionary<int, string>();
        private List<Dictionary<int, string>> _predictionValues = new List<Dictionary<int, string>>();
        private ObservableCollection<KeyValuePair<string, int>> _selectedColumnItemsCount;

        private ObservableCollection<CleanDataEntity> CleanDataItems { get; } = new ObservableCollection<CleanDataEntity>();

        public ObservableCollection<KeyValuePair<string, int>> SelectedColumnItemsCount
        {
            get { return _selectedColumnItemsCount; }
            set
            {
                if (_selectedColumnItemsCount != value)
                {
                    _selectedColumnItemsCount = value;
                    OnPropertyChanged(nameof(SelectedColumnItemsCount));
                }
            }
        }

        public List<Dictionary<int, string>> PredictionValues
        {
            get { return _predictionValues; }
            set
            {
                if (_predictionValues != value)
                {
                    _predictionValues = value;
                    OnPropertyChanged(nameof(PredictionValues));
                }
            }
        }

        public Dictionary<int, string> ColumnsDictionary
        {
            get { return _columnsDictionary; }
            set
            {
                if (_columnsDictionary != value)
                {
                    _columnsDictionary = value;
                    OnPropertyChanged(nameof(ColumnsDictionary));
                }
            }
        }

        public int CorruptValueCount
        {
            get { return _corruptValueCount; }
            set
            {
                if (_corruptValueCount != value)
                {
                    _corruptValueCount = value;
                    OnPropertyChanged(nameof(CorruptValueCount));
                }
            }
        }
        
        public int SelectedColumnIndex
        {
            get { return _selectedColumnIndex; }
            set
            {
                if (_selectedColumnIndex != value)
                {
                    _selectedColumnIndex = value;
                    OnPropertyChanged(nameof(SelectedColumnIndex));
                }
            }
        }

        public int SelectedPredictionColumnIndex
        {
            get { return _selectedPredictionColumnIndex; }
            set
            {
                if (_selectedPredictionColumnIndex != value)
                {
                    _selectedPredictionColumnIndex = value;
                    OnPropertyChanged(nameof(SelectedPredictionColumnIndex));
                }
            }
        }

        public int SelectedVisualisationColumnIndex
        {
            get { return _selectedVisualisationColumnIndex; }
            set
            {
                if (_selectedVisualisationColumnIndex != value)
                {
                    _selectedVisualisationColumnIndex = value;
                    OnPropertyChanged(nameof(SelectedVisualisationColumnIndex));
                }
            }
        }

        public int DuplicateCount
        {
            get { return _duplicateCount; }
            set
            {
                if (_duplicateCount != value)
                {
                    _duplicateCount = value;
                    OnPropertyChanged(nameof(DuplicateCount));
                }
            }
        }

        public bool IsCorruptValuesChecked
        {
            get { return _isCorruptValuesChecked; }
            set
            {
                if (_isCorruptValuesChecked != value)
                {
                    _isCorruptValuesChecked = value;
                    OnPropertyChanged(nameof(IsCorruptValuesChecked));
                }
            }
        }

        public bool IsShowDuplicatesChecked
        {
            get { return _isShowDuplicatesChecked; }
            set
            {
                if (_isShowDuplicatesChecked != value)
                {
                    _isShowDuplicatesChecked = value;
                    OnPropertyChanged(nameof(IsShowDuplicatesChecked));
                }
            }
        }

        public bool IsLimitRowsChecked
        {
            get { return _isLimitRowsChecked; }
            set
            {
                if (_isLimitRowsChecked != value)
                {
                    _isLimitRowsChecked = value;
                    OnPropertyChanged(nameof(IsLimitRowsChecked));
                }
            }
        }

        public int RowReadLimit
        {
            get { return _rowReadLimit; }
            set
            {
                if (_rowReadLimit != value)
                {
                    _rowReadLimit = value;
                    OnPropertyChanged(nameof(RowReadLimit));
                }
            }
        }

        public string RegularExpression
        {
            get { return _regularExpression; }
            set
            {
                if (_regularExpression != value)
                {
                    _regularExpression = value;
                    OnPropertyChanged(nameof(RegularExpression));
                }
            }
        }

        public string FileName
        {
            get { return _fileName; }
            set
            {
                if (_fileName != value)
                {
                    _fileName = value;
                    OnPropertyChanged(nameof(FileName));
                }
            }
        }

        public ICommand BrowseFileCommand
        {
            get { return new CommandDelegate(OnBrowseFile, CanExecute); }
        }

        public ICommand CleanDataCommand
        {
            get { return new CommandDelegate(OnCleanData, CanExecute); }
        }

        public ICommand ExportDataCommand
        {
            get { return new CommandDelegate(OnExportData, CanExecute); }
        }

        public ICommand DeleteColumnCommand
        {
            get { return new CommandDelegate(OnDeleteColumn, CanExecute); }
        }

        public ICommand ShowPredictionValues
        {
            get { return new CommandDelegate((_) => ShowNewWindow<CleanDataPredicitonValuesView>(this), CanExecute); }
        }

        public ICommand ShowPredictionsCommand
        {
            get { return new CommandDelegate((_) => Task.Run(() => OnShowPredictions(_)), CanExecute); }
        }

        public ICommand VisualiseColumnCommand
        {
            get { return new CommandDelegate((_) => Task.Run(() => OnVisualiseColumnn()), CanExecute); }
        }

        public ICommand ShowVisualisationCommand
        {
            get { return new CommandDelegate((_) => ShowNewWindow<CleanDataVisualizationView>(this), CanExecute); }
        }

        [ImportingConstructor]
        public CleanDataViewModel(ExceptionLogDataAccess exceptionLogDataAccess)
        {
            _exceptionLogDataAccess = exceptionLogDataAccess;
        }

        private void OnVisualiseColumnn()
        {
            if (SelectedVisualisationColumnIndex >= 0 && ColumnsDictionary.Any())
            {
                try
                {
                    SelectedColumnItemsCount = new ObservableCollection<KeyValuePair<string, int>>();
                    var selectedColumnKey = ColumnsDictionary.ElementAt(SelectedVisualisationColumnIndex).Key;
                    var visualiseColumnData = CleanDataItems.SelectMany(x => x.Data).Where(c => c.Key == selectedColumnKey);

                    foreach (var visualColumnData in visualiseColumnData.Select(x => x.Value).Distinct())
                    {
                        Application.Current.Dispatcher.Invoke(() => SelectedColumnItemsCount.Add(new KeyValuePair<string, int>(visualColumnData,
                            visualiseColumnData.Select(x => x).Where(c => string.Equals(c.Value, visualColumnData, StringComparison.OrdinalIgnoreCase)).Count())));
                    }
                }
                catch (Exception ex)
                {
                    _exceptionLogDataAccess.LogException(ex.ToString());
                }   
            }
        }

        private void OnShowPredictions(object parameter)
        {
            lock (_lockPredictionValuesObject)
            {
                if (parameter is DataGrid dataGrid && SelectedPredictionColumnIndex >= 0 && ColumnsDictionary.Any())
                {
                    PredictionValues = new List<Dictionary<int, string>>();
                    var selectedPredictionColumnKey = ColumnsDictionary.ElementAt(SelectedPredictionColumnIndex).Key;
                    var distinctPredicitonValues = CleanDataItems.SelectMany(x => x.Data.Where(z => z.Key == selectedPredictionColumnKey))
                        .Select(c => c.Value).Distinct();

                    foreach (var prediction in distinctPredicitonValues)
                    {
                        var columns = new Dictionary<int, string>();

                        for (var colIndex = 0; colIndex < ColumnsDictionary.Count; colIndex++)
                        {
                            var keyValuePairs = CleanDataItems.Select(x => x.Data).Where(c => c.Values.Contains(prediction));

                            var allPredictionsForColumn = keyValuePairs.SelectMany(x => x).Where(c => c.Key == ColumnsDictionary.ElementAt(colIndex).Key)
                                 .GroupBy(z => z.Value)
                                 .OrderByDescending(v => v.Count()).FirstOrDefault();

                            if (allPredictionsForColumn != null)
                            {
                                foreach (var c in allPredictionsForColumn)
                                {
                                    columns.Add(ColumnsDictionary.ElementAt(colIndex).Key, c.Value);
                                    break;
                                }
                            }
                        }
                        PredictionValues.Add(columns);
                    }

                    Application.Current.Dispatcher.Invoke(() => dataGrid.ItemsSource = PredictionValues);
                    Application.Current.Dispatcher.Invoke(() => dataGrid.Columns.Clear());
                    var colCount = 0;
                    foreach (var prediction in ColumnsDictionary)
                    {
                        Application.Current.Dispatcher.Invoke(() => dataGrid.Columns.Add(new DataGridTextColumn()
                        {
                            Header = prediction.Value,
                            Binding = new Binding($".[{prediction.Key}]"),
                            DisplayIndex = colCount
                        }));

                        colCount++;
                    }
                }
            }
        }

        private void OnDeleteColumn(object parameter)
        {
            if (parameter is DataGrid dataGrid && SelectedColumnIndex >= 0 && ColumnsDictionary.Any())
            {
                foreach (var cleanDataItem in CleanDataItems)
                {
                    cleanDataItem.Data.Remove(ColumnsDictionary.ElementAt(SelectedColumnIndex).Key);
                }
                dataGrid.Columns.Remove(dataGrid.Columns.FirstOrDefault(x => 
                    string.Equals(x.Header.ToString(), ColumnsDictionary.ElementAt(SelectedColumnIndex).Value, StringComparison.OrdinalIgnoreCase)));
                ColumnsDictionary.Remove(ColumnsDictionary.ElementAt(SelectedColumnIndex).Key);

                ColumnsDictionary = new Dictionary<int, string>(ColumnsDictionary);
            }
        }

        private void OnCleanData(object parameter)
        {
            IsModalVisible = true;
            Task.Run(() => CleanData(parameter));
        }

        private void CleanData(object parameter)
        {
            try
            {
                if (parameter is DataGrid dataGrid)
                {
                    if (string.Equals(Path.GetExtension(FileName), ".xlsx", StringComparison.OrdinalIgnoreCase)
                        || string.Equals(Path.GetExtension(FileName), ".xls", StringComparison.OrdinalIgnoreCase))
                    {
                        ReadExcelData(dataGrid);
                    }
                    else
                    {
                        ReadCSVFile(dataGrid);
                    }
                }
            }
            catch (Exception ex)
            {
                _exceptionLogDataAccess.LogException(ex.ToString());
            }
            IsModalVisible = false;
        }

        private void ReadCSVFile(DataGrid dataGrid)
        {
            using (var streamReader = new StreamReader(FileName))
            {
                Application.Current.Dispatcher.Invoke(() => CleanDataItems.Clear());
                var columnsDictionary = new Dictionary<int, string>();
                CorruptValueCount = 0;
                var columnCount = 0;
                var rowCount = 0;
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    var columns = line.Split(',');
                    if (!string.IsNullOrEmpty(line) && line.Contains(","))
                    {
                        var columnsList = new CleanDataEntity();
                        var colCount = 0;
                        foreach (var column in columns)
                        {
                            columnsList.Data.Add(colCount, column);
                            if (IsCorruptValuesChecked && Regex.Match(column, $"{RegularExpression}").Success)
                            {
                                columnsList.ContainsSpecialCharacter = true;
                                CorruptValueCount++;
                            }
                            colCount++;
                        }
                        columnsList.Id = rowCount;
                        Application.Current.Dispatcher.Invoke(() => CleanDataItems.Add(columnsList));
                        columnCount = colCount;
                    }

                    rowCount++;
                    if (IsLimitRowsChecked && RowReadLimit > 0 && rowCount >= RowReadLimit)
                    {
                        break;
                    }
                }

                CheckAndSetDuplicates();
                CreateeColumnsAndBindings(dataGrid, columnCount);
            }
        }

        private void ReadExcelData(DataGrid dataGrid)
        {
            try
            {
                using (var package = new ExcelPackage(new FileInfo(FileName)))
                {
                    Application.Current.Dispatcher.Invoke(() => CleanDataItems.Clear());
                    CorruptValueCount = 0;
                    var workbook = package.Workbook;

                    foreach (var worksheet in workbook.Worksheets)
                    {
                        for (var row = 1; row <= worksheet.Dimension.Rows; row++)
                        {
                            var columnsList = new CleanDataEntity();
                            for (var column = 1; column <= worksheet.Dimension.Columns; column++)
                            {
                                var columnText = worksheet.Cells[row, column].Text;
                                columnsList.Data.Add(column, columnText);
                                if (IsCorruptValuesChecked && Regex.Match(columnText, $"{RegularExpression}").Success)
                                {
                                    columnsList.ContainsSpecialCharacter = true;
                                    CorruptValueCount++;
                                }
                            }
                            columnsList.Id = row;
                            Application.Current.Dispatcher.Invoke(() => CleanDataItems.Add(columnsList));
                            if (IsLimitRowsChecked && RowReadLimit > 0 && row >= RowReadLimit)
                            {
                                break;
                            }
                        }

                        CheckAndSetDuplicates();
                        CreateeColumnsAndBindings(dataGrid, worksheet.Dimension.Columns);
                    }
                }
            }
            catch (Exception ex)
            {
                _exceptionLogDataAccess.LogException(ex.ToString());
            }
        }

        private void CheckAndSetDuplicates()
        {
            if (IsShowDuplicatesChecked)
            {
                DuplicateCount = 0;
                var duplicateValues = CleanDataItems.Select(x => x).GroupBy(c => c.GetHashCode()).Where(x => x.Count() > 1);

                foreach (var duplicate in duplicateValues.SelectMany(x => x))
                {
                    duplicate.IsDuplicate = true;
                    DuplicateCount++;
                }
            }
        }

        private void CreateeColumnsAndBindings(DataGrid dataGrid, int columnCount)
        {
            var columnsDictionary = new Dictionary<int, string>();
            Application.Current.Dispatcher.Invoke(() => dataGrid.ItemsSource = CleanDataItems);
            Application.Current.Dispatcher.Invoke(() => dataGrid.Columns.Clear());
            for (var a = 0; a < columnCount; a++)
            {
                Application.Current.Dispatcher.Invoke(() => dataGrid.Columns.Add(new DataGridTextColumn()
                {
                    Header = $"Column {a}",
                    Binding = new Binding($"Data[{a}]"),
                    DisplayIndex = a
                }));
                columnsDictionary.Add(a, $"Column {a}");
            }
            Application.Current.Dispatcher.Invoke(() => ColumnsDictionary = new Dictionary<int, string>(columnsDictionary));
        }

        private void OnExportData(object parameter)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Filter = "CSV File|*.csv|All Files|*.*";
            saveFileDialog.ShowDialog();
            var folderPath = saveFileDialog.FileName;
            if (!string.IsNullOrEmpty(folderPath))
            {
                IsModalVisible = true;
                Task.Run(() => ExportCleanData(parameter, folderPath));
            }
        }

        private void ExportCleanData(object parameter, string folderPath)
        {
            if (parameter is DataGrid dataGrid)
            {
                using (var streamWriter = new StreamWriter($"{folderPath}"))
                {
                    foreach (var row in dataGrid.Items)
                    {
                        if (row is CleanDataEntity cleanRowData)
                        {
                            if ((IsShowDuplicatesChecked && cleanRowData.IsDuplicate)
                                || IsCorruptValuesChecked && cleanRowData.ContainsSpecialCharacter)
                            {
                                continue;
                            }
                            for (var a = 0; a < cleanRowData.Data.Count; a++)
                            {
                                if (a == (cleanRowData.Data.Count - 1))
                                {
                                    streamWriter.Write(cleanRowData.Data.ElementAt(a).Value);
                                }
                                else
                                {
                                    streamWriter.Write(cleanRowData.Data.ElementAt(a).Value);
                                    streamWriter.Write(",");
                                }
                            }

                            streamWriter.Write("\n");
                        }
                    }
                }
            }
            IsModalVisible = false;
        }

        private void OnBrowseFile(object parameter)
        {
            var fileDialog = new System.Windows.Forms.OpenFileDialog();
            fileDialog.ShowDialog();

            var filename = fileDialog.FileName;
            if (!string.IsNullOrEmpty(filename))
            {
                FileName = filename;
            }
        }
    }
}
