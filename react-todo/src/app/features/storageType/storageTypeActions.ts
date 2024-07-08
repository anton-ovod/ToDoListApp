import { StorageType } from "../../../types";
import { CHANGE_STORAGE_TYPE } from "./actionTypes";

export const changeStorageType = (storageType: StorageType) => (
    {
        type: CHANGE_STORAGE_TYPE,
        payload: storageType
    }
)