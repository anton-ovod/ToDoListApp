import { StorageType, Task } from '../../../../types';
import {
    ADD_TASK, DELETE_TASK, UPDATE_TASK, CHANGE_STATUS,
    ADD_TASK_SUCCESS, ADD_TASK_FAILURE,
    DELETE_TASK_SUCCESS, DELETE_TASK_FAILURE,
    UPDATE_TASK_SUCCESS, UPDATE_TASK_FAILURE,
    CHANGE_STATUS_SUCCESS, CHANGE_STATUS_FAILURE,
    GET_TASKS,
    GET_TASKS_SUCCESS,
    GET_TASKS_FAILURE
} from './actionTypes';

export interface GetTasksAction {
    type: typeof GET_TASKS;
    payload: StorageType;
}

export interface GetTasksSuccessAction {
    type: typeof GET_TASKS_SUCCESS;
    payload: Task[];
}

export interface GetTasksFailureAction {
    type: typeof GET_TASKS_FAILURE;
    payload: string;
}

export interface AddTaskAction {
    type: typeof ADD_TASK;
    payload: {
        task: Task, 
        storageType: StorageType
    };
}

export interface DeleteTaskAction {
    type: typeof DELETE_TASK;
    payload: {
        taskID: string,
        storageType: StorageType
    }
}

export interface UpdateTaskAction {
    type: typeof UPDATE_TASK;
    payload: {
        task: Task,
        storageType: StorageType
    };
}

export interface ChangeStatusAction {
    type: typeof CHANGE_STATUS;
    payload: {
        taskID: string,
        storageType: StorageType
    }
}

export interface AddTaskSuccessAction {
    type: typeof ADD_TASK_SUCCESS;
    payload: Task;
}

export interface AddTaskFailureAction {
    type: typeof ADD_TASK_FAILURE;
    payload: string;
}

export interface DeleteTaskSuccessAction {
    type: typeof DELETE_TASK_SUCCESS;
    payload: string; 
}

export interface DeleteTaskFailureAction {
    type: typeof DELETE_TASK_FAILURE;
    payload: string;
}

export interface UpdateTaskSuccessAction {
    type: typeof UPDATE_TASK_SUCCESS;
    payload: Task;
}

export interface UpdateTaskFailureAction {
    type: typeof UPDATE_TASK_FAILURE;
    payload: string;
}

export interface ChangeStatusSuccessAction {
    type: typeof CHANGE_STATUS_SUCCESS;
    payload: string; 
}

export interface ChangeStatusFailureAction {
    type: typeof CHANGE_STATUS_FAILURE;
    payload: string;
}

export type TaskActionTypes =
    | GetTasksAction
    | AddTaskAction
    | DeleteTaskAction
    | UpdateTaskAction
    | ChangeStatusAction
    | GetTasksSuccessAction
    | GetTasksFailureAction
    | AddTaskSuccessAction
    | AddTaskFailureAction
    | DeleteTaskSuccessAction
    | DeleteTaskFailureAction
    | UpdateTaskSuccessAction
    | UpdateTaskFailureAction
    | ChangeStatusSuccessAction
    | ChangeStatusFailureAction;