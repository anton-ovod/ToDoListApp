import { Category, StorageType } from '../../../../types';
import {
    GET_CATEGORIES,
    GET_CATEGORIES_SUCCESS,
    GET_CATEGORIES_FAILURE,
} from './actionTypes';

export interface GetCategoriesAction {
    type: typeof GET_CATEGORIES;
    payload: StorageType;
}

export interface GetCategoriesSuccessAction {
    type: typeof GET_CATEGORIES_SUCCESS;
    payload: Category[];
}

export interface GetCategoriesFailureAction {
    type: typeof GET_CATEGORIES_FAILURE;
    payload: string;
}

export type CategoriesActionTypes = 
        | GetCategoriesAction
        | GetCategoriesSuccessAction
        | GetCategoriesFailureAction ;