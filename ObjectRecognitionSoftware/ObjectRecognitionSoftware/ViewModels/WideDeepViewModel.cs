using ObjectRecognitionSoftware.Common;
using ObjectRecognitionSoftware.Entities;
using ObjectRecognitionSoftware.Views.DialogBoxes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ObjectRecognitionSoftware.ViewModels
{
    public class WideDeepViewModel : BaseViewModel
    {
        #region Fields

        private const string _predictionColumnNames = "age, workclass, fnlwgt, education, education_num,marital_status, occupation, relationship, race, gender,capital_gain, capital_loss, hours_per_week, native_country";
        private const string _predictionOutputColumnNames = _predictionColumnNames+",predicted_income_bracket,probability";
        private StringBuilder _textLogBuilder = new StringBuilder();
        private string _textBoxLog;
        private bool _pythonInstalled;
        private ObservableCollection<CensusBaseEntity> _predictionInputValues;
        private ObservableCollection<CensusPredictionOutput> _predictionOutputValues;
        public string imageDirectory;

        #endregion

        #region Properties

        public bool IsPythonInstalled
        {
            get { return Python.IsPythonInstalled(); }
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

        public WideDeepViewModel()
        {
            ExecuteCMDCommands.outputHandler = OutputHandler;
            GetPredictions();
            GetPredictionResults();            
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
                ExceptionLogging.LogException(ex.ToString());
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
                            Probability = Convert.ToInt32(values[14])
                        });
                    }
                }
                PredictionOutputValues = predictions;
            }
            catch (Exception ex)
            {
                ExceptionLogging.LogException(ex.ToString());
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
            Task.Run(() =>
                predict()
            );
        }

        private void predict()
        {
            var commands = new List<string>() { string.Format("cd {0}", CurrentDirectory.GetPythonAssetsDirectory("WideDeep")), @"python WideDeepPredict.py" };
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

        #endregion
    }
}