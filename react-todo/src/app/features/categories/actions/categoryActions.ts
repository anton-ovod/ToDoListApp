import {
    GET_CATEGORIES,
    GET_CATEGORIES_SUCCESS,
    GET_CATEGORIES_FAILURE
} from './actionTypes';

import { GetCategoriesAction, GetCategoriesSuccessAction, GetCategoriesFailureAction } from './actionInterfaces';
import { Category, StorageType } from '../../../../types';

export const getCategories = (storageType: StorageType): GetCategoriesAction => ({
    type: GET_CATEGORIES,
    payload: storageType
});

export const getCategoriesSuccess = (categories: Category[]): GetCategoriesSuccessAction => ({
    type: GET_CATEGORIES_SUCCESS,
    payload: categories,
});

export const getCategoriesFailure = (error: any): GetCategoriesFailureAction => ({
    type: GET_CATEGORIES_FAILURE,
    payload: error,
});
