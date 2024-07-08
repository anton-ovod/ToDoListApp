import React from 'react'
import { StorageType } from '../types';
import { StorageTypes } from '../enums';
import { useAppDispatch, useAppSelector } from '../app/hooks';
import { RootState } from '../app/store';
import { changeStorageType } from '../app/features/storageType/storageTypeActions';
import { getTasks } from '../app/features/tasks/actions/taskActions';
import { getCategories } from '../app/features/categories/actions/categoryActions';
import { getStatuses } from '../app/features/statuses/actions/statusActions';

const StorageTypeForm: React.FC = () => {
    const storageTypes = Object.values(StorageTypes) as StorageTypes[];
    const dispatch = useAppDispatch();
    const currentStorageType = useAppSelector((state: RootState) => state.storageType);

    const submitStorageType = (e: React.FormEvent) => {
      e.preventDefault();
      dispatch(getTasks(currentStorageType));
      dispatch(getCategories(currentStorageType));
      dispatch(getStatuses(currentStorageType));
    }

  return (
    <form className="d-flex fs-4 m-3" onSubmit={submitStorageType}>
              <select className="d-flex fs-4 m-3" 
              value={ currentStorageType }
              onChange = { (e) => dispatch(changeStorageType(e.target.value as StorageType)) } >
                <option value="">Select Storage Type</option>
                {storageTypes.map(type => (
                    <option key={type} value={type}>{type}</option>
                ))}
              </select>
              <button type="submit" className="btn btn-primary fs-4">Set Storage</button>
            </form>
  )
}

export default StorageTypeForm
