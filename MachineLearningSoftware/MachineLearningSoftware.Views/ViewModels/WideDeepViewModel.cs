using MachineLearningSoftware.Common;
using MachineLearningSoftware.Controls.Entities;
using MachineLearningSoftware.DataAccess;
using MachineLearningSoftware.Entities;
using MachineLearningSoftware.ViewModels;
using MachineLearningSoftware.Views.DialogBoxes;
using MachineLearningSoftware.Views.Enumerations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml;

namespace MachineLearningSoftware.Views.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class WideDeepViewModel : BaseViewModel
    {
        #region Fields

        private const string _predictionColumnNames = "age, workclass, fnlwgt, education, education_num,marital_status,"+ 
            "occupation, relationship, race, gender,capital_gain, capital_loss, hours_per_week, native_country";
        private const string _predictionOutputColumnNames = _predictionColumnNames+",predicted_income_bracket,probability";
        private StringBuilder _textLogBuilder = new StringBuilder();
        private string _textBoxLog;
        private bool _pythonInstalled;
        private ObservableCollection<CensusBaseEntity> _predictionInputValues;
        private ObservableCollection<CensusPredictionOutput> _predictionOutputValues;
        private ExceptionLogDataAccess _exceptionLogging;
        private ModelType _selectedModelType;
        private ObservableCollection<ModelType> _modelTypes = new ObservableCollection<ModelType>() { ModelType.DeepModel, ModelType.WideModel, ModelType.WideDeepModel };
        private string _modelFolderDirectory;

        #endregion

        #region Properties

        public bool IsPythonInstalled
        {
            get { return Python.IsPythonInstalled(); }
        }

        public ObservableCollection<ModelType> ModelTypes
        {
            get { return _modelTypes; }
            set
            {
                if (_modelTypes != value)
                {
                    _modelTypes = value;
                    OnPropertyChanged(nameof(ModelTypes));
                }
            }
        }

        public ModelType SelectedModelType
        {
            get { return _selectedModelType; }
            set
            {
                _selectedModelType = value;
                OnPropertyChanged(nameof(SelectedModelType));
            }
        }

        public string TextBoxLog
        {
            get { return _textBoxLog; }
            set
            {
                _textBoxLog = value;
                OnPropertyChanged(nameof(TextBoxLog));
            }
        }

        public bool PythonInstalled
        {
            get { return _pythonInstalled; }
            set
            {
                _pythonInstalled = value;
                OnPropertyChanged(nameof(PythonInstalled)); 
            }
        }
        
        public ObservableCollection<CensusBaseEntity> PredictionInputValues
        {
            get { return _predictionInputValues; }
            set
            {
                _predictionInputValues = value;
                OnPropertyChanged(nameof(PredictionInputValues));
            }
        }

        public ObservableCollection<CensusPredictionOutput> PredictionOutputValues
        {
            get { return _predictionOutputValues; }
            set
            {
                _predictionOutputValues = value;
                OnPropertyChanged(nameof(PredictionOutputValues));
            }
        }

        public ObservableCollection<string> MartialStatusVocabulary { get; } = new ObservableCollection<string>() { "Married-civ-spouse", "Divorced",
            "Married-spouse-absent", "Never-married", "Separated", "Married-AF-spouse", "Widowed" };

        public ObservableCollection<string> EducationVocabulary { get; } = new ObservableCollection<string>() { "Bachelors", "HS-grad", "11th",
            "Masters", "9th", "Some-college", "Assoc-acdm", "Assoc-voc", "7th-8th", "Doctorate", "Prof-school", "5th-6th", "10th", "1st-4th",
            "Preschool", "12th" };

        public ObservableCollection<string> WorkclassVocabulary { get; } = new ObservableCollection<string>() { "Self-emp-not-inc", "Private",
            "State-gov", "Federal-gov", "Local-gov", "?", "Self-emp-inc", "Without-pay", "Never-worked" };

        public ObservableCollection<string> RelationshipVocabulary { get; } = new ObservableCollection<string>() { "Husband", "Not-in-family",
            "Wife", "Own-child", "Unmarried", "Other-relative" };
        
        public ICommand ChooseModelFolderCommand
        {
            get { return new CommandDelegate(ChangeModelFolder, CanExecute); }
        }
        
        public ICommand SavePredictionCommand
        {
            get { return new CommandDelegate(SavePrediction, CanExecute); }
        }

        public ICommand DeletePredictionCommand
        {
            get { return new CommandDelegate(DeletePrediction, CanExecute); }
        }

        public ICommand PredictWideDeepCommand
        {
            get { return new CommandDelegate(Predict, CanExecute); }
        }

        public ICommand InstallTensorFlowCommand
        {
            get { return new CommandDelegate(InstallTensorFlow, CanExecute); }
        }
        public ICommand OpenHelpDialog
        {
            get { return new CommandDelegate(DisplayHelpDialog, CanExecute); }
        }

        #endregion

        #region Constructor

        [ImportingConstructor]
        public WideDeepViewModel(ExceptionLogDataAccess exceptionLogging)
        {
            ConfigureHeaderControl(true, true, Properties.WideDeepResource.Title, true,
                Properties.WideDeepResource.Description);
            _exceptionLogging = exceptionLogging;
            ExecuteCMDCommands.outputHandler = OutputHandler;
            GetPredictions();
            GetPredictionResults();
            CreateConfigurationFile();
        }

        #endregion

        #region Public Methods

        public void GetPredictions()
        {
            try
            {
                var predictions = new ObservableCollection<CensusBaseEntity>();
                using (var reader = new StreamReader(CurrentDirectory.GetPythonAssetsDirectory(@"WideDeep\Prediction.csv")))
                {
                    var line = reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine();
                        var values = line.Split(',');
                        predictions.Add(new CensusBaseEntity()
                        {
                            Age = Convert.ToInt32(values[0]),
                            WorkClass = values[1],
                            Fnlwgt = Convert.ToInt32(values[2]),
                            Education = values[3],
                            EducationNumber = Convert.ToInt32(values[4]),
                            MartialStatus = values[5],
                            Occupation = values[6],
                            Relationship = values[7],
                            Race = values[8],
                            Gender = values[9],
                            CapitalGain = Convert.ToInt32(values[10]),
                            CapticalLoss = Convert.ToInt32(values[11]),
                            HoursPerWeek = Convert.ToInt32(values[12]),
                            Country = values[13]
                        });
                    }
                }
                PredictionInputValues = predictions;
            }
            catch(Exception ex)
            {
                _exceptionLogging.LogException(ex.ToString());
            }           
        }
        
        private void GetPredictionResults()
        {
            try
            {
                var predictions = new ObservableCollection<CensusPredictionOutput>();
                using (var reader = new StreamReader(CurrentDirectory.GetPythonAssetsDirectory(@"WideDeep\CensusOutput.csv")))
                {
                    var line = reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine();
                        var values = line.Split(',');
                        predictions.Add(new CensusPredictionOutput()
                        {
                            Age = Convert.ToInt32(values[0]),
                            WorkClass = values[1],
                            Fnlwgt = Convert.ToInt32(values[2]),
                            Education = values[3],
                            EducationNumber = Convert.ToInt32(values[4]),
                            MartialStatus = values[5],
                            Occupation = values[6],
                            Relationship = values[7],
                            Race = values[8],
                            Gender = values[9],
                            CapitalGain = Convert.ToInt32(values[10]),
                            CapticalLoss = Convert.ToInt32(values[11]),
                            HoursPerWeek = Convert.ToInt32(values[12]),
                            Country = values[13],
                            Prediction = Convert.ToInt32(values[14]),
                            Probability = float.Parse(values[15], CultureInfo.CurrentCulture.NumberFormat)
                        });
                    }
                }
                PredictionOutputValues = predictions;
            }
            catch (Exception ex)
            {
                _exceptionLogging.LogException(ex.ToString());
            }            
        }

        #endregion

        #region Private Methods
               
        private string GetCurrentWindowsDirectory()
        {
            return ExecuteCMDCommands.GetCommandOutput("echo %CD:~0,3%");
        }
        
        private void OutputHandler(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
            {
                TextBoxLog += string.Format("{0} \n", e.Data);
            }
        }

        private void ChangeModelFolder(object context)
        {
            var fileDialog = new FolderBrowserDialog();
            fileDialog.ShowDialog();

            var filePath = fileDialog.SelectedPath;
            if (!string.IsNullOrEmpty(filePath))
            {
                _modelFolderDirectory = filePath;
            }
        }
                
        private void SavePrediction(object context)
        {
            SaveToCSV();
            GetPredictions();
        }

        private void DeletePrediction(object context)
        {
            DeleteFromCSV();
            GetPredictions();
        }

        private void Predict(object context)
        {
            IsModalVisible = true;
            Task.Run(() => Predict());
        }

        private void Predict()
        {
            ModifyModelType();
            var commands = new List<string>() { string.Format("cd {0}", 
                CurrentDirectory.GetPythonAssetsDirectory("WideDeep")), @"python WideDeepPredict.py" };
            ExecuteCMDCommands.RunMultipleCommands(commands, false);
            GetPredictionResults();
            IsModalVisible = false;
        }

        private void InstallTensorFlow(object context)
        {
            Python.InstallUpdateTensorFlow();
        }

        private void SaveToCSV()
        {
            using (var file = new StreamWriter(CurrentDirectory.GetPythonAssetsDirectory(@"WideDeep\Prediction.csv")))
            {
                file.WriteLine(_predictionColumnNames);
                foreach (var prediction in PredictionInputValues)
                {
                    var fromattedPrediction = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}",
                        prediction.Age, prediction.WorkClass, prediction.Fnlwgt, prediction.Education, prediction.EducationNumber, prediction.MartialStatus,
                        prediction.Occupation, prediction.Relationship, prediction.Race, prediction.Gender, prediction.CapitalGain, prediction.CapticalLoss,
                        prediction.HoursPerWeek, prediction.Country);
                    file.WriteLine(fromattedPrediction);
                }
            }
        }

        private void DeleteFromCSV()
        {
            using (var file = new StreamWriter(CurrentDirectory.GetPythonAssetsDirectory(@"WideDeep\Prediction.csv")))
            {
                file.WriteLine(_predictionColumnNames);
            }
        }
        
        private void DisplayHelpDialog(object obj)
        {
            new WideDeepHelpDialog().ShowDialog();
        }

        private void CreateConfigurationFile()
        {
            if (!File.Exists(CurrentDirectory.GetPythonAssetsDirectory(@"WideDeep\Configuration.xml")))
            {
                var xmlWriter = XmlWriter.Create(CurrentDirectory.GetPythonAssetsDirectory(@"WideDeep\Configuration.xml"));
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Configuration");
                xmlWriter.WriteStartElement("Directory");
                xmlWriter.WriteString(ModelType.DeepModel.ToString());
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Close();
            }
        }

        private void ModifyModelType()
        {
            var configurationDirectory = CurrentDirectory.GetPythonAssetsDirectory(@"WideDeep\Configuration.xml");
            if (File.Exists(configurationDirectory))
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(configurationDirectory);
                var directoryElement = xmlDoc.GetElementsByTagName("Directory");
                if (!string.IsNullOrEmpty(_modelFolderDirectory))
                {
                    directoryElement.Item(0).InnerXml = _modelFolderDirectory;
                }
                else
                {
                    directoryElement.Item(0).InnerXml = SelectedModelType.ToString();
                }
                xmlDoc.Save(configurationDirectory);
            }
        }

        #endregion
    }
}