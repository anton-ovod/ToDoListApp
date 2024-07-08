import { Status, StorageType } from '../../../../types';
import {
    GET_STATUSES,
    GET_STATUSES_FAILURE,
    GET_STATUSES_SUCCESS,
} from './actionTypes';

export interface GetStatusesAction {
    type: typeof GET_STATUSES;
    payload: StorageType;
}

export interface GetStatusesSuccessAction {
    type: typeof GET_STATUSES_SUCCESS;
    payload: Status[];
}

export interface GetStatusesFailureAction {
    type: typeof GET_STATUSES_FAILURE;
    payload: string;
}

export type StatusesActionTypes = 
        | GetStatusesAction
        | GetStatusesSuccessAction
        | GetStatusesFailureAction ;