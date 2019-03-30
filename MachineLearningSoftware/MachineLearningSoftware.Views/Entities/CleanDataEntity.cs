using System.Collections.Generic;
using System.Text;

namespace MachineLearningSoftware.Views.Entities
{
    public class CleanDataEntity
    {
        public int Id { get; set; }
        public Dictionary<int, string> Data { get; set; } = new Dictionary<int, string>();
        public bool ContainsSpecialCharacter { get; set; }
        public bool IsDuplicate { get; set; }
        public bool IsDuplicateExported { get; set; }

        public override bool Equals(object other)
        {
            if (other is CleanDataEntity cleanDataEntity)
            {
                return cleanDataEntity.GetHashCode() == GetHashCode();
            }
            return false;
        }

        public override int GetHashCode() { return GenerateHashCode(); }

        private int GenerateHashCode()
        {
            var hashCode = new StringBuilder();
            foreach (var data in Data)
            {
                hashCode.Append(data.Value.GetHashCode());
            }
            return hashCode.ToString().GetHashCode();
        }
    }
}
