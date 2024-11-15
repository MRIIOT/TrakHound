// Copyright (c) 2024 TrakHound Inc., All Rights Reserved.
// TrakHound Inc. licenses this file to you under the MIT license.

using TrakHound.Entities;

namespace TrakHound.Blazor.Components.ObjectExplorerInternal
{
    public class ObjectExplorerDeleteService
    {
        private readonly ObjectExplorerService _explorerService;
        private IEnumerable<string> _deleteModalPaths;
        private bool _deleteModalVisible;
        private bool _deleteModalLoading;


        public IEnumerable<string> DeleteModalPaths => _deleteModalPaths;

        public bool DeleteModalVisible => _deleteModalVisible;

        public bool DeleteModalLoading => _deleteModalLoading;


        public event EventHandler<ITrakHoundObjectEntity> DeleteClicked;


        public ObjectExplorerDeleteService(ObjectExplorerService explorerService)
        {
            _explorerService = explorerService;
        }


        public void DeleteObject(ITrakHoundObjectEntity entity)
        {
            if (entity != null)
            {
                _deleteModalPaths = new List<string>() { entity.GetAbsolutePath() };
                OpenDeleteModal();
            }
        }

        public void DeleteObjects(IEnumerable<ITrakHoundObjectEntity> entities)
        {
            if (!entities.IsNullOrEmpty())
            {
                _deleteModalPaths = entities.Select(o => o.GetAbsolutePath());
                OpenDeleteModal();
            }
        }


        public void OpenDeleteModal()
        {
            _deleteModalVisible = true;
            _explorerService.Update();
        }

        public async void ModalConfirm()
        {
            _deleteModalLoading = true;

            if (!_deleteModalPaths.IsNullOrEmpty())
            {
                if (await _explorerService.Client.Entities.DeleteObjects(_deleteModalPaths))
                {
                    var objectUuids = new List<string>();
                    foreach (var path in _deleteModalPaths)
                    {
                        objectUuids.Add(TrakHoundPath.GetUuid(path));
                    }

                    foreach (var objectUuid in objectUuids)
                    {
                        _explorerService.RemoveObject(objectUuid);
                    }

                    string notificationMessage = "";
                    string notificationDetails = "";

                    if (objectUuids.Count > 1)
                    {
                        notificationMessage = $"{objectUuids.Count} Objects Deleted Successfully";
                    }
                    else
                    {
                        notificationMessage = "1 Object Deleted Successfully";
                        notificationDetails = _deleteModalPaths.FirstOrDefault();
                    }

                    _explorerService.AddNotification(NotificationType.Information, notificationMessage, notificationDetails);
                }
            }

            _deleteModalPaths = null;
            _deleteModalLoading = false;
            _deleteModalVisible = false;
            _explorerService.Update();
        }

        public void ModalCancel()
        {
            _deleteModalPaths = null;
            _deleteModalLoading = false;
            _deleteModalVisible = false;
            _explorerService.Update();
        }
    }
}
