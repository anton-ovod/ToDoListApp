import {
    GET_STATUSES,
    GET_STATUSES_SUCCESS,
    GET_STATUSES_FAILURE
} from './actionTypes';

import {  GetStatusesAction, GetStatusesFailureAction, GetStatusesSuccessAction } from './actionInterfaces';
import { Status, StorageType } from '../../../../types';

export const getStatuses = (storageType: StorageType): GetStatusesAction => ({
    type: GET_STATUSES,
    payload: storageType
});

export const getStatusesSuccess = (categories: Status[]): GetStatusesSuccessAction => ({
    type: GET_STATUSES_SUCCESS,
    payload: categories,
});

export const getStatusesFailure = (error: any): GetStatusesFailureAction => ({
    type: GET_STATUSES_FAILURE,
    payload: error,
});
