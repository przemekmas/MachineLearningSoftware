namespace MachineLearningSoftware.Views.Entities
{
    public class PerformanceIndicatorEntity
    {
        public string PreprocessingTechnique { get; set; }
        public string Process { get; set; }
        public int Accuracy { get; set; }
        public int AccuracyBaseline { get; set; }
        public int AUC { get; set; }
        public int AUCPrecisionRecall { get; set; }
        public int AverageLoss { get; set; }
        public int LabelMean { get; set; }
        public int Loss { get; set; }
        public int Precision { get; set; }
        public int PredictionMean { get; set; }
        public int Recall { get; set; }
        public int TrainTime { get; set; }
        public int Rank { get; set; }

        public string ToCSV()
        {
            return $"{PreprocessingTechnique},{Process},{Accuracy},{AccuracyBaseline},{AUC},{AUCPrecisionRecall},{AverageLoss},{LabelMean}," +
                $"{Loss},{Precision},{PredictionMean},{Recall},{TrainTime},{Rank}";
        }
    }
}
