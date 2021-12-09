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
        private readonly string _pathToDirectory;
        private readonly string _pathToBackup;

        public StorageRepositoryWithFileSystem()
        {
            _storages = new List<Storage>();
            _pathToDirectory = string.Empty;
            _pathToBackup = string.Empty;
        }

        private StorageRepositoryWithFileSystem(string backupDirectory, int restorePointNumber)
        {
            _storages = new List<Storage>();
            _pathToDirectory = backupDirectory + Path.PathSeparator + restorePointNumber;
            _pathToBackup = backupDirectory;
        }

        public Storage AddStorage(List<JobObject> jobObjects)
        {
            if (!Directory.Exists(_pathToBackup))
            {
                Directory.CreateDirectory(_pathToBackup);
            }

            if (!Directory.Exists(_pathToDirectory))
            {
                Directory.CreateDirectory(_pathToDirectory);
            }

            var id = Guid.NewGuid();
            using (ZipArchive zipArchive = ZipFile.Open(_pathToDirectory + Path.PathSeparator + id + ".zip", ZipArchiveMode.Update))
            {
                for (int i = 0; i < jobObjects.Count(); i++)
                {
                    if (!File.Exists(jobObjects[i].Path))
                    {
                        throw new BackupsException($"File {jobObjects[i].Path} doesn't exist");
                    }

                    zipArchive.CreateEntryFromFile(jobObjects[i].Path, i.ToString());
                }
            }

            _storages.Add(new Storage(id, _pathToDirectory + Path.PathSeparator + id + ".zip", jobObjects.Select(j => new ArchivedFilePath(j.Path)).ToList()));
            return _storages[^1];
        }

        public Storage AddStorage(JobObject jobObject)
        {
            if (!Directory.Exists(_pathToBackup))
            {
                Directory.CreateDirectory(_pathToBackup);
            }

            if (!Directory.Exists(_pathToDirectory))
            {
                Directory.CreateDirectory(_pathToDirectory);
            }

            var id = Guid.NewGuid();
            using (ZipArchive zipArchive = ZipFile.Open(_pathToDirectory + Path.PathSeparator + id + ".zip", ZipArchiveMode.Update))
            {
                if (!File.Exists(jobObject.Path))
                {
                    throw new BackupsException($"File {jobObject.Path} doesn't exist");
                }

                zipArchive.CreateEntryFromFile(jobObject.Path, 0.ToString());
            }

            _storages.Add(new Storage(id, _pathToDirectory + Path.PathSeparator + id + ".zip", new ArchivedFilePath(jobObject.Path)));
            return _storages[^1];
        }

        public Storage UpdateLastStorage(List<JobObject> jobObjects)
        {
            if (_storages.Count == 0) throw new BackupsException("Repository doesn't have any storages");
            if (!Directory.Exists(_pathToBackup))
            {
                Directory.CreateDirectory(_pathToBackup);
            }

            if (!Directory.Exists(_pathToDirectory))
            {
                Directory.CreateDirectory(_pathToDirectory);
            }

            using (ZipArchive zipArchive = ZipFile.Open(_pathToDirectory + Path.PathSeparator + _storages[^1] + ".zip", ZipArchiveMode.Update))
            {
                for (int i = _storages[^1].Count(); i < _storages[^1].Count() + jobObjects.Count(); i++)
                {
                    if (!File.Exists(jobObjects[i].Path))
                    {
                        throw new BackupsException($"File {jobObjects[i].Path} doesn't exist");
                    }

                    zipArchive.CreateEntryFromFile(jobObjects[i].Path, i.ToString());
                }
            }

            _storages[^1] = new Storage(_storages[^1].Id, _pathToDirectory + Path.PathSeparator + _storages[^1].Id + ".zip", jobObjects.Select(j => new ArchivedFilePath(j.Path)).ToList());
            return _storages[^1];
        }

        public Storage UpdateLastStorage(JobObject jobObject)
        {
            if (_storages.Count == 0) throw new BackupsException("Repository doesn't have any storages");
            if (!Directory.Exists(_pathToBackup))
            {
                Directory.CreateDirectory(_pathToBackup);
            }

            if (!Directory.Exists(_pathToDirectory))
            {
                Directory.CreateDirectory(_pathToDirectory);
            }

            using (ZipArchive zipArchive = ZipFile.Open(_pathToDirectory + Path.PathSeparator + _storages[^1].Id + ".zip", ZipArchiveMode.Update))
            {
                if (!File.Exists(jobObject.Path))
                {
                    throw new BackupsException($"File {jobObject.Path} doesn't exist");
                }

                zipArchive.CreateEntryFromFile(jobObject.Path, 0.ToString());
            }

            _storages[^1] = new Storage(_storages[^1].Id, _pathToDirectory + Path.PathSeparator + _storages[^1].Id + ".zip", new ArchivedFilePath(jobObject.Path));
            return _storages[^1];
        }

        public int Count()
        {
            return _storages.Count;
        }

        IStorageRepository IStorageRepository.WithPath(string backupDirectory, int restorePointNumber)
        {
            return new StorageRepositoryWithFileSystem(backupDirectory, restorePointNumber);
        }

        public Storage GetById(Guid id)
        {
            foreach (Storage s in _storages.Where(s => s.Id == id))
            {
                return s;
            }

            throw new BackupsException("There is no storages with this id");
        }

        public IEnumerable<Storage> GetAllStorages()
        {
            return _storages;
        }

        public void Restore()
        {
            foreach (Storage s in _storages)
            {
                using (ZipArchive zipArchive = ZipFile.Open(s.PathToArchive, ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry entry in zipArchive.Entries)
                    {
                        if (File.Exists(entry.FullName))
                        {
                            File.Delete(entry.FullName);
                        }

                        using (Stream streamFromFileFromArchive = entry.Open())
                        {
                            streamFromFileFromArchive.Seek(0, SeekOrigin.Begin);
                            using (FileStream newFile = File.Create(entry.FullName))
                            {
                                streamFromFileFromArchive.CopyTo(newFile);
                            }
                        }
                    }
                }
            }
        }

        public void Restore(string newPath)
        {
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }

            foreach (Storage s in _storages)
            {
                using (ZipArchive zipArchive = ZipFile.Open(s.PathToArchive, ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry entry in zipArchive.Entries)
                    {
                        if (File.Exists(newPath + Path.PathSeparator + Path.GetFileName(entry.FullName)))
                        {
                            File.Delete(newPath + Path.PathSeparator + Path.GetFileName(entry.FullName));
                        }

                        using (Stream streamFromFileFromArchive = entry.Open())
                        {
                            streamFromFileFromArchive.Seek(0, SeekOrigin.Begin);
                            using (FileStream newFile = File.Create(newPath + Path.PathSeparator + Path.GetFileName(entry.FullName)))
                            {
                                streamFromFileFromArchive.CopyTo(newFile);
                            }
                        }
                    }
                }
            }
        }

        public void DeleteAll(ILogger logger)
        {
            foreach (Storage s in _storages)
            {
                File.Delete(s.PathToArchive);
                logger.Log("Storage was deleted");
            }

            _storages.Clear();
            Directory.Delete(_pathToDirectory);
        }
    }
}