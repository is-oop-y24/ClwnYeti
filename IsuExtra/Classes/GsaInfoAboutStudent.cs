using Isu.Classes;

namespace IsuExtra.Classes
{
    public class GsaInfoAboutStudent
    {
        public GsaInfoAboutStudent(Id firstGsaGroupId, Id secondGsaGroupId, Id studentId)
        {
            FirstGsaGroupId = firstGsaGroupId;
            SecondGsaGroupId = secondGsaGroupId;
            StudentId = studentId;
        }

        public Id FirstGsaGroupId { get; }
        public Id SecondGsaGroupId { get; }

        public Id StudentId { get; }
    }
}