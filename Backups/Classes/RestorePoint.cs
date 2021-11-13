using System;
using System.Collections.Generic;
using System.Linq;

namespace Backups.Classes
{
    public class RestorePoint
    {
        private List<JobName> _jobNamesInRestorePoint;
        public RestorePoint(int id, IEnumerable<JobName> jobNames, AlgorithmOfStorage algorithmOfStorage)
        {
            Id = id;
            AlgorithmOfStorage = algorithmOfStorage;
            Date = DateTime.Today;
            _jobNamesInRestorePoint = jobNames.Select(n => new JobName(n.Value + "_" + id)).ToList();
        }

        public int Id { get; }
        public AlgorithmOfStorage AlgorithmOfStorage { get; }
        private DateTime Date { get; }
    }
}
