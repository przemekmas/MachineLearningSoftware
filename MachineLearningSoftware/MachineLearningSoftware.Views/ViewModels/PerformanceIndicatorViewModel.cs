using MachineLearningSoftware.Common;
using MachineLearningSoftware.Controls.Entities;
using MachineLearningSoftware.DataAccess;
using MachineLearningSoftware.ViewModels;
using MachineLearningSoftware.Views.Entities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows.Input;

namespace MachineLearningSoftware.Views.ViewModels
{
    [Export]
    public class PerformanceIndicatorViewModel : BaseViewModel
    {
        private readonly ExceptionLogDataAccess _exceptionLogDataAccess;
        private ObservableCollection<PerformanceIndicatorEntity> _weightConfiguration = new ObservableCollection<PerformanceIndicatorEntity>();
        private ObservableCollection<PerformanceIndicatorEntity> _experimentResults = new ObservableCollection<PerformanceIndicatorEntity>();
        private ObservableCollection<PerformanceIndicatorEntity> _experimentRank = new ObservableCollection<PerformanceIndicatorEntity>();

        public ObservableCollection<PerformanceIndicatorEntity> WeightConfiguration
        {
            get { return _weightConfiguration; }
            set
            {
                if (_weightConfiguration != value)
                {
                    _weightConfiguration = value;
                    OnPropertyChanged(nameof(WeightConfiguration));
                }
            }
        }

        public ObservableCollection<PerformanceIndicatorEntity> ExperimentResults
        {
            get { return _experimentResults; }
            set
            {
                if (_experimentResults != value)
                {
                    _experimentResults = value;
                    OnPropertyChanged(nameof(ExperimentResults));
                }
            }
        }

        public ObservableCollection<PerformanceIndicatorEntity> ExperimentRank
        {
            get { return _experimentRank; }
            set
            {
                if (_experimentRank != value)
                {
                    _experimentRank = value;
                    OnPropertyChanged(nameof(ExperimentRank));
                }
            }
        }

        public ICommand RankExperiments
        {
            get { return new CommandDelegate(OnRankExperiments, CanExecute); }
        }

        [ImportingConstructor]
        public PerformanceIndicatorViewModel(ExceptionLogDataAccess exceptionLogging)
        {
            _exceptionLogDataAccess = exceptionLogging;
            ConfigureHeaderControl(true, true, Properties.PerformanceIndicatorResource.Title, true,
                Properties.PerformanceIndicatorResource.Description);
            WeightConfiguration = new ObservableCollection<PerformanceIndicatorEntity>()
            {
                new PerformanceIndicatorEntity()
                {
                    Accuracy = 1,
                    AccuracyBaseline = 2,
                    AUC = 1,
                    AUCPrecisionRecall = 2,
                    AverageLoss = 1,
                    LabelMean = 1,
                    Loss = 1,
                    Precision = 1,
                    PredictionMean = 1,
                    Recall = 1,
                    TrainTime = 1,
                },
                new PerformanceIndicatorEntity()
                {
                    Accuracy = -1,
                    AccuracyBaseline = -2,
                    AUC = -1,
                    AUCPrecisionRecall = -2,
                    AverageLoss = -1,
                    LabelMean = -1,
                    Loss = -1,
                    Precision = -1,
                    PredictionMean = -1,
                    Recall = -1,
                    TrainTime = -1,
                }
            };
            GetSavedExperimentResults();
        }

        private void OnRankExperiments(object parameter)
        {
            ExperimentRank = new ObservableCollection<PerformanceIndicatorEntity>();

            foreach(var experiment in ExperimentResults)
            {
                var rank = GetRank(experiment);

                ExperimentRank.Add(new PerformanceIndicatorEntity()
                {
                    PreprocessingTechnique = experiment.PreprocessingTechnique,
                    Process = experiment.Process,
                    Rank = rank
                });
            }

            SaveExperimentResults(ExperimentResults);
        }

        private void SaveExperimentResults(ObservableCollection<PerformanceIndicatorEntity> experimentResults)
        {
            using (var file = new StreamWriter(CurrentDirectory.GetAssetsDirectoryFolder(@"CSVFiles\ExperimentResults.csv")))
            {
                foreach (var experiment in experimentResults)
                {
                    file.WriteLine(experiment.ToCSV());
                }
            }
        }

        private void GetSavedExperimentResults()
        {
            ExperimentResults = new ObservableCollection<PerformanceIndicatorEntity>();
            try
            {
                using (var reader = new StreamReader(CurrentDirectory.GetAssetsDirectoryFolder(@"CSVFiles\ExperimentResults.csv")))
                {
                    do
                    {
                        var line = reader.ReadLine();
                        if (line != null)
                        {
                            var values = line.Split(',');
                            var experimentResult = new PerformanceIndicatorEntity()
                            {
                                PreprocessingTechnique = values[0],
                                Process = values[1],
                                Accuracy = int.Parse(values[2]),
                                AccuracyBaseline = int.Parse(values[3]),
                                AUC = int.Parse(values[4]),
                                AUCPrecisionRecall = int.Parse(values[5]),
                                AverageLoss = int.Parse(values[6]),
                                LabelMean = int.Parse(values[7]),
                                Loss = int.Parse(values[8]),
                                Precision = int.Parse(values[9]),
                                PredictionMean = int.Parse(values[10]),
                                Recall = int.Parse(values[11]),
                                TrainTime = int.Parse(values[12]),
                            };
                            ExperimentResults.Add(experimentResult);
                        }
                    }
                    while (!reader.EndOfStream);
                }
            }
            catch (Exception ex)
            {
                _exceptionLogDataAccess.LogException(ex.ToString());
            }
        }

        private int GetRank(PerformanceIndicatorEntity experiment)
        {
            var positiveWeights = WeightConfiguration[0];
            var negativeWeights = WeightConfiguration[1];
            var rank = 0;

            rank += experiment.Accuracy > 0 ? positiveWeights.Accuracy : experiment.Accuracy < 0 ? negativeWeights.Accuracy : 0;
            rank += experiment.AccuracyBaseline > 0 ? positiveWeights.AccuracyBaseline : experiment.AccuracyBaseline < 0 ? negativeWeights.AccuracyBaseline : 0;
            rank += experiment.AUC > 0 ? positiveWeights.AUC : experiment.AUC < 0 ? negativeWeights.AUC : 0;
            rank += experiment.AUCPrecisionRecall > 0 ? positiveWeights.AUCPrecisionRecall : experiment.AUCPrecisionRecall < 0 ? negativeWeights.AUCPrecisionRecall : 0;
            rank += experiment.AverageLoss > 0 ? positiveWeights.AverageLoss : experiment.AverageLoss < 0 ? negativeWeights.AverageLoss : 0;
            rank += experiment.LabelMean > 0 ? positiveWeights.LabelMean : experiment.LabelMean < 0 ? negativeWeights.LabelMean : 0;
            rank += experiment.Loss > 0 ? positiveWeights.Loss : experiment.Loss < 0 ? negativeWeights.Loss : 0;
            rank += experiment.Precision > 0 ? positiveWeights.Precision : experiment.Precision < 0 ? negativeWeights.Precision : 0;
            rank += experiment.PredictionMean > 0 ? positiveWeights.PredictionMean : experiment.PredictionMean < 0 ? negativeWeights.PredictionMean : 0;
            rank += experiment.Recall > 0 ? positiveWeights.Recall : experiment.Recall < 0 ? negativeWeights.Recall : 0;
            rank += experiment.TrainTime > 0 ? positiveWeights.TrainTime : experiment.TrainTime < 0 ? negativeWeights.TrainTime : 0;

            return rank < 0 ? 0 : rank > 11 ? 11 : rank;
        }
    }
}
