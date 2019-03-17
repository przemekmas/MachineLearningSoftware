using MachineLearningSoftware.Common;
using MachineLearningSoftware.Controls.Entities;
using MachineLearningSoftware.DataAccess;
using MachineLearningSoftware.ViewModels;
using MachineLearningSoftware.Views.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace MachineLearningSoftware.Views.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class TrainDatasetViewModel : BaseViewModel
    {
        private readonly ExceptionLogDataAccess _exceptionLogDataAccess;
        private string _selectedFile;
        private string _generatedPythonCode;
        private int _validationDataCount;
        private int _trainDataCount;
        private int _selectedPredictionColumn;
        private ObservableCollection<ColumnName> _columnNames = new ObservableCollection<ColumnName>();
        private List<Dictionary<int, string>> _data = new List<Dictionary<int, string>>();
        private string _selectedModel;
        private string _validationDataFilePath;
        private string _trainDataFilePath;
        private bool _predictAllClasses;
        private string _predictValue;
        private int _numberOfClasses;
        private bool _canSplitUpData;
        private string _selectedDataSplitPercentage = "10%";
        private int _splitDataSeed;
        private Dictionary<string, int> _splitPercentages = new Dictionary<string, int>() { { "10%", 10 }, { "20%", 20 }, { "30%", 30 }, { "40%", 40 }, { "50%", 50 }, { "60%", 60 }, { "70%", 70 }, { "80%", 80 }, { "90%", 90 } };

        public ObservableCollection<string> Models { get; } = new ObservableCollection<string>() { "all", "wide", "deep", "wide_deep" };
        public ObservableCollection<string> DataSplitPercentages { get; } = new ObservableCollection<string>() { "10%", "20%", "30%", "40%", "50%", "60%", "70%", "80%", "90%" };

        public int NumberOfClasses
        {
            get { return _numberOfClasses; }
            set
            {
                if (_numberOfClasses != value)
                {
                    _numberOfClasses = value;
                    OnPropertyChanged(nameof(NumberOfClasses));
                }
            }
        }

        public bool PredictAllClasses
        {
            get { return _predictAllClasses; }
            set
            {
                if (_predictAllClasses != value)
                {
                    _predictAllClasses = value;
                    OnPropertyChanged(nameof(PredictAllClasses));
                }
            }
        }

        public string PredictValue
        {
            get { return _predictValue; }
            set
            {
                if (_predictValue != value)
                {
                    _predictValue = value;
                    OnPropertyChanged(nameof(PredictValue));
                }
            }
        }

        public string ValidationDataFilePath
        {
            get { return _validationDataFilePath?.Replace('\\','/'); }
            set
            {
                if (_validationDataFilePath != value)
                {
                    _validationDataFilePath = value;
                    OnPropertyChanged(nameof(ValidationDataFilePath));
                }
            }
        }

        public string TrainDataFilePath
        {
            get { return _trainDataFilePath?.Replace('\\', '/'); }
            set
            {
                if (_trainDataFilePath != value)
                {
                    _trainDataFilePath = value;
                    OnPropertyChanged(nameof(TrainDataFilePath));
                }
            }
        }

        public string SelectedModel
        {
            get { return _selectedModel; }
            set
            {
                if (_selectedModel != value)
                {
                    _selectedModel = value;
                    OnPropertyChanged(nameof(SelectedModel));
                }
            }
        }

        public string SelectedFile
        {
            get { return _selectedFile; }
            set
            {
                if (_selectedFile != value)
                {
                    _selectedFile = value;
                    OnPropertyChanged(nameof(SelectedFile));
                }
            }
        }

        public string GeneratedPythonCode
        {
            get { return _generatedPythonCode; }
            set
            {
                if (_generatedPythonCode != value)
                {
                    _generatedPythonCode = value;
                    OnPropertyChanged(nameof(GeneratedPythonCode));
                }
            }
        }

        public int TrainDataCount
        {
            get { return _trainDataCount; }
            set
            {
                if (_trainDataCount != value)
                {
                    _trainDataCount = value;
                    OnPropertyChanged(nameof(TrainDataCount));
                }
            }
        }

        public int ValidationDataCount
        {
            get { return _validationDataCount; }
            set
            {
                if (_validationDataCount != value)
                {
                    _validationDataCount = value;
                    OnPropertyChanged(nameof(ValidationDataCount));
                }
            }
        }

        public ObservableCollection<ColumnName> ColumnNames
        {
            get { return _columnNames; }
            set
            {
                if (_columnNames != value)
                {
                    _columnNames = value;
                    OnPropertyChanged(nameof(ColumnNames));
                }
            }
        }

        public int SelectedPredictionColumn
        {
            get { return _selectedPredictionColumn; }
            set
            {
                if (_selectedPredictionColumn != value)
                {
                    _selectedPredictionColumn = value;
                    OnPropertyChanged(nameof(SelectedPredictionColumn));
                }
            }
        }

        public bool CanSplitUpData
        {
            get { return _canSplitUpData; }
            set
            {
                if (_canSplitUpData != value)
                {
                    _canSplitUpData = value;
                    OnPropertyChanged(nameof(CanSplitUpData));
                }
            }
        }

        public string SelectedDataSplitPercentage
        {
            get { return _selectedDataSplitPercentage; }
            set
            {
                if (_selectedDataSplitPercentage != value)
                {
                    _selectedDataSplitPercentage = value;
                    OnPropertyChanged(nameof(SelectedDataSplitPercentage));
                }
            }
        }

        public int SplitDataSeed
        {
            get { return _splitDataSeed; }
            set
            {
                if (_splitDataSeed != value)
                {
                    _splitDataSeed = value;
                    OnPropertyChanged(nameof(SplitDataSeed));
                }
            }
        }

        public ICommand GeneratePythonScript
        {
            get { return new CommandDelegate(GenerateScript, CanExecute); }
        }

        public ICommand OpenFileDialogCommand
        {
            get { return new CommandDelegate(OpenFileDialog, CanExecute); }
        }

        [ImportingConstructor]
        public TrainDatasetViewModel(ExceptionLogDataAccess exceptionLogDataAccess)
        {
            _exceptionLogDataAccess = exceptionLogDataAccess;
        }

        private void OpenFileDialog(object parameter)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            var fileName = fileDialog.FileName;
            if (!string.IsNullOrEmpty(fileName))
            {
                SelectedFile = fileName;
                ReadDataset(true);
                GenerateColumnNames();
            }
        }

        private void GenerateScript(object parameter)
        {
            ReadDataset();
            IsProgressModalVisible = true;
            Task.Run(() => GenerateScript());
        }

        private void GenerateColumnNames()
        {
            if (_data.Any())
            {
                try
                {
                    ColumnNames = new ObservableCollection<ColumnName>();
                    var columns = _data.First();
                    var columnsDataType = _data[1] ?? null;
                    var colCount = 0;

                    foreach (var column in columns)
                    {
                        ColumnNames.Add(new ColumnName()
                        {
                            GeneratedColumnName = $"Column{colCount}",
                            NewColumnName = column.Value.Replace(" ", ""),
                            ColumnDataType = int.TryParse(columnsDataType[colCount], out int result) ? typeof(int) : typeof(string)
                        });
                        colCount++;
                    }
                }
                catch(Exception ex)
                {
                    _exceptionLogDataAccess.LogException(ex.ToString());
                    ShowInformationWindow(ex.ToString(), this);
                }
            }
        }

        private void GenerateScript()
        {
            try
            {
                if (_data.Any())
                {
                    var columns = _data.First();
                    var colCount = 0;
                    var columnNames = new List<string>();
                    var columnTypes = new List<Type>();

                    foreach (var column in ColumnNames)
                    {
                        var columnName = string.IsNullOrEmpty(column.NewColumnName) ? column.GeneratedColumnName : column.NewColumnName;

                        var distinctColumnData = _data.SelectMany(x => x).Where(c => c.Key == colCount).Skip(1);
                        var columnType = typeof(string);

                        foreach (var uniqueColumn in distinctColumnData.Select(x => x.Value).Distinct())
                        {
                            if (int.TryParse(uniqueColumn, out int result))
                            {
                                columnType = typeof(int);
                            }
                            else
                            {
                                columnType = typeof(string);
                                break;
                            }
                        }

                        columnNames.Add(columnName);
                        columnTypes.Add(columnType);

                        colCount++;
                    }

                    using (var streamReader = new StreamReader(CurrentDirectory.GetPythonAssetsDirectory("WideDeepTrain.py")))
                    {
                        var script = streamReader.ReadToEnd();

                        var continuousColumns = new StringBuilder();
                        var csvColumnTypes = new StringBuilder();
                        var csvColumns = new StringBuilder();
                        var deepColumns = new StringBuilder();
                        csvColumns.Append("_CSV_COLUMNS = [");
                        csvColumnTypes.Append("_CSV_COLUMN_DEFAULTS = [");
                        deepColumns.Append("  deep_columns = [\n");
                        for (var a = 0; a < columns.Count; a++)
                        {
                            csvColumns.Append($"'{columnNames[a]}'");

                            if (columnTypes[a] == typeof(int))
                            {
                                deepColumns.Append($"    {columnNames[a]},\n");
                                csvColumnTypes.Append("[0]");
                                if (SelectedPredictionColumn != a)
                                {
                                    continuousColumns.Append($"  {columnNames[a]} = tf.feature_column.numeric_column('{columnNames[a]}')");
                                    continuousColumns.Append("\n");
                                }
                            }
                            else
                            {
                                if (SelectedPredictionColumn != a)
                                {
                                    deepColumns.Append($"    tf.feature_column.indicator_column({columnNames[a]}),\n");
                                }
                                csvColumnTypes.Append("['']");
                            }

                            if (a < columns.Count - 1)
                            {
                                csvColumns.Append(",");
                                csvColumnTypes.Append(",");
                            }

                            ModalPercentage = ConvertToPercentage(a, columns.Count);
                        }
                        csvColumns.Append("] \n");
                        csvColumnTypes.Append("]");
                        deepColumns.Append("]");

                        var totalCategoryIndex = 0;
                        var columnCount = columns.Count;// _data.Select(x => x.Values).Distinct().Count();

                        for (var column = 0; column < columns.Count; column++)
                        {
                            if (SelectedPredictionColumn == column)
                            {
                                continue;
                            }

                            if (columnTypes[column] == typeof(string))
                            {
                                columnCount += _data.Select(x => x[column]).Distinct().Count();
                            }
                        }

                        var categoricalColumns = new StringBuilder();
                        var wideColumns = new StringBuilder();
                        wideColumns.Append("  wide_columns = [\n");
                        for (var a = 0; a < columns.Count; a++)
                        {
                            if (columnTypes[a] == typeof(string))
                            {
                                if (SelectedPredictionColumn == a)
                                {
                                    continue;
                                }
                                if (!ColumnNames.Any(x => string.Equals(x.GeneratedColumnName, columnNames[a], StringComparison.OrdinalIgnoreCase)
                                    || string.Equals(x.GeneratedColumnName, columnNames[a], StringComparison.OrdinalIgnoreCase)))
                                {
                                    wideColumns.Append($"    {columnNames[a]},\n");
                                }

                                var categoricalColumn = _data.Select(x => x[a]).Distinct();
                                categoricalColumns.Append($"  {columnNames[a]} = tf.feature_column.categorical_column_with_vocabulary_list('{columnNames[a]}',[ ");
                                var categoryIndex = 0;
                                foreach (var category in categoricalColumn)
                                {
                                    categoricalColumns.Append($"'{RemoveSpecialCharacters(category)}'");
                                    if (categoryIndex < categoricalColumn.Count())
                                    {
                                        categoricalColumns.Append($",");
                                    }
                                    categoryIndex++;
                                    totalCategoryIndex++;
                                    ModalPercentage = ConvertToPercentage(a + totalCategoryIndex, columnCount);
                                }
                                categoricalColumns.Append("]) \n \n");
                            }
                            ModalPercentage = ConvertToPercentage(a + totalCategoryIndex, columnCount);
                        }
                        wideColumns.Append("]");
                        string predictionColumn = GetPredictionColumn();
                        var predicitonCode = string.Empty;
                        var numberOfClassesCode = string.Empty;

                        if (PredictAllClasses)
                        {
                            predicitonCode = "labels";
                            numberOfClassesCode = $",n_classes={NumberOfClasses}";
                        }
                        else
                        {
                            if (int.TryParse(PredictValue, out int result))
                            {
                                predicitonCode = $"tf.equal(labels, {result})";
                            }
                            else
                            {
                                predicitonCode = $"tf.equal(labels, '{PredictValue}')";
                            }
                        }

                        SplitUpTestData();

                        var generatedCode = string.Format(CultureInfo.InvariantCulture, script, csvColumns.ToString(), csvColumnTypes.ToString(),
                            continuousColumns.ToString(), categoricalColumns.ToString(), wideColumns.ToString(), deepColumns.ToString(), TrainDataCount,
                            ValidationDataCount, SelectedModel, predictionColumn, TrainDataFilePath, ValidationDataFilePath, predicitonCode,
                            numberOfClassesCode);
                        GeneratedPythonCode = generatedCode;
                    }
                }
            }
            catch(Exception ex)
            {
                _exceptionLogDataAccess.LogException(ex.ToString());
                System.Windows.Application.Current.Dispatcher.Invoke(() => ShowInformationWindow(ex.ToString(), this));
            }
            finally
            {
                IsProgressModalVisible = false;
            }
        }

        private void SplitUpTestData()
        {
            if (CanSplitUpData)
            {
                var splitPercentage = _splitPercentages[SelectedDataSplitPercentage];
                var remainingPercentage = 100 - splitPercentage;
                var totalRows = _data.Count();
                var randomNumber = new Random(SplitDataSeed);

                if (decimal.TryParse($"0.{splitPercentage}", out decimal result))
                {
                    var totalSplitData = result * totalRows;
                    var count = 0;
                    var testDataCount = 0;
                    var splitEvery = Convert.ToInt32((totalRows / totalSplitData));
                    TrainDataCount = Convert.ToInt32(totalRows - totalSplitData);
                    ValidationDataCount = Convert.ToInt32(totalSplitData);

                    using (var streamReader = new StreamReader(SelectedFile))
                    using (var validationFile = new StreamWriter(ValidationDataFilePath))
                    using (var trainFile = new StreamWriter(TrainDataFilePath))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            var line = streamReader.ReadLine();
                            var columns = line.Split(',');
                            if (count > 0)
                            {

                                if (!string.IsNullOrEmpty(line) && line.Contains(","))
                                {
                                    // this loop is counted as a row of data

                                    if (count % splitEvery == 0 && testDataCount <= totalSplitData)
                                    {
                                        validationFile.WriteLine(string.Join(",", columns));
                                        testDataCount++;
                                        // write to test file
                                    }
                                    else
                                    {
                                        trainFile.WriteLine(string.Join(",", columns));
                                        // write to train file
                                    }
                                }
                            }
                            count++;
                        }
                    }
                }
            }
        }

        private string GetPredictionColumn()
        {
            if (string.IsNullOrEmpty(ColumnNames[SelectedPredictionColumn].NewColumnName))
            {
                return ColumnNames[SelectedPredictionColumn].GeneratedColumnName;
            }
            else
            {
                return ColumnNames[SelectedPredictionColumn].NewColumnName;
            }
        }

        private string RemoveSpecialCharacters(string category)
        {
            return category.Replace("'", "\\'");
        }

        private void ReadDataset(bool getColumnCount = false)
        {
            try
            {
                if (!string.IsNullOrEmpty(SelectedFile))
                {
                    using (var streamReader = new StreamReader(SelectedFile))
                    {
                        _data = new List<Dictionary<int, string>>();
                        var count = 1;

                        while (!streamReader.EndOfStream)
                        {
                            var line = streamReader.ReadLine();
                            var columns = line.Split(',');
                            if (!string.IsNullOrEmpty(line) && line.Contains(","))
                            {
                                var rowCount = 0;
                                var values = new Dictionary<int, string>();
                                foreach (var column in columns)
                                {
                                    values[rowCount] = column;
                                    rowCount++;
                                }
                                _data.Add(values);
                                if (getColumnCount && count == 2)
                                {
                                    break;
                                }
                                count++;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                _exceptionLogDataAccess.LogException(ex.ToString());
                ShowInformationWindow(ex.ToString(), this);
            }
        }
    }
}
