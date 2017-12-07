﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace ShowTheFolder
{

    public class FolderService 
    {
        //  private Folder FolderModel;
        public async static Task<Folder> GetFolderInfoAsync(StorageFolder SelectFolder)
        {
            var FolderModel = new Folder();

            FolderModel.FolderName = SelectFolder.Name;
            IReadOnlyList<StorageFile> fileList = await SelectFolder?.GetFilesAsync();

            foreach (StorageFile file in fileList)
            {
                var subFile = new File();
                subFile.FileName = file.Name;
                subFile.FilePath = file.Path;
                FolderModel.SubFiles.Add(subFile);

            }
            IReadOnlyList<StorageFolder> folderList = await SelectFolder?.GetFoldersAsync();

            foreach (StorageFolder folder in folderList)
            {
                var subFolder = new Folder();
                subFolder.FolderName = folder.Name;
                subFolder.FolderPath = folder.Path;
                FolderModel.SubFolders.Add(subFolder);

            }

            return FolderModel;
        }
        public async static Task<ObservableCollection<Item>> GetItems(StorageFolder SelectFolder)
        {
            var Model = await GetFolderInfoAsync(SelectFolder);
            var Items = new ObservableCollection<Item>();
            foreach (var file in Model.SubFiles)
            {
                var item = new Item
                {
                    ItemName = file.FileName,
                    IType = ItemType.File,
                    Source = file.FilePath
                };
                Items.Add(item);
            }
            foreach (var folder in Model.SubFolders)
            {
                var item = new Item
                {
                    ItemName = folder.FolderName,
                    IType = ItemType.Folder,
                    Source = folder.FolderPath

                };
                Items.Add(item);
            }
            return Items;
        }
    }
}
