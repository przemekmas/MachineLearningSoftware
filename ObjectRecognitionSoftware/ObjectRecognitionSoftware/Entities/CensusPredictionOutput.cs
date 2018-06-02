namespace ObjectRecognitionSoftware.Entities
{
    public class CensusPredictionOutput : CensusBaseEntity
    {
        public int Prediction { get; set; }
        public float Probability { get; set; }
    }
}
