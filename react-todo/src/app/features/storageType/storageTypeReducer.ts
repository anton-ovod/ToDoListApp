import { StorageType } from "../../../types";
import { CHANGE_STORAGE_TYPE } from "./actionTypes";


const initialState: StorageType = "SQL";


const storageTypeReducer = (state = initialState, action: any) => {
    switch (action.type) {
        case CHANGE_STORAGE_TYPE:
            return action.payload;
        default:
            return state;
    }
};

export default storageTypeReducer;