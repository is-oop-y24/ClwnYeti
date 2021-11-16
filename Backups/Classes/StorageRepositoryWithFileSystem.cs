using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Interfaces;
using Backups.Tools;

namespace Backups.Classes
{
    public class StorageRepositoryWithFileSystem : IStorageRepository
    {
        private readonly List<Storage> _storages;
        public StorageRepositoryWithFileSystem()
        {
            _storages = new List<Storage>();
        }

        public Storage Add(List<JobObject> jobObjects, string directory)
        {
            var id = Guid.NewGuid();
            ZipArchive zipArchive = ZipFile.Open(directory + "/" + id, ZipArchiveMode.Update);
            for (int i = 0; i < jobObjects.Count(); i++)
            {
                if (!File.Exists(jobObjects[i].Path))
                {
                    throw new BackupsException($"File {jobObjects[i].Path} doesn't exist");
                }

                zipArchive.CreateEntryFromFile(jobObjects[i].Path, i.ToString());
            }

            _storages.Add(new Storage(id, directory + "/" + id, jobObjects.Select(j => new ArchivedFilePath(j.Path)).ToList()));
            return _storages[^1];
        }

        public Storage Add(JobObject jobObject, string directory)
        {
            var id = Guid.NewGuid();
            ZipArchive zipArchive = ZipFile.Open(directory + "/" + id, ZipArchiveMode.Update);
            if (!File.Exists(jobObject.Path))
            {
                throw new BackupsException($"File {jobObject.Path} doesn't exist");
            }

            zipArchive.CreateEntryFromFile(jobObject.Path, 0.ToString());

            _storages.Add(new Storage(id, directory + "/" + id, new ArchivedFilePath(jobObject.Path)));
            return _storages[^1];
        }

        IStorageRepository IStorageRepository.Empty()
        {
            return new StorageRepositoryWithFileSystem();
        }
    }
}