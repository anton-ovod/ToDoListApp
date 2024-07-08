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

import { StorageType, Task } from '../../../../types';
import { AddTaskAction, AddTaskFailureAction, AddTaskSuccessAction, ChangeStatusAction, ChangeStatusFailureAction, ChangeStatusSuccessAction, DeleteTaskAction, DeleteTaskFailureAction, DeleteTaskSuccessAction, GetTasksAction, GetTasksFailureAction, GetTasksSuccessAction, UpdateTaskAction, UpdateTaskFailureAction, UpdateTaskSuccessAction } from './actionInterfaces';

export const getTasks = (storageType: StorageType): GetTasksAction => ({
    type: GET_TASKS,
    payload: storageType,
});

export const getTasksSuccess = (tasks: Task[]): GetTasksSuccessAction => ({
    type: GET_TASKS_SUCCESS,
    payload: tasks,
});

export const getTasksFailure = (error: any): GetTasksFailureAction => ({
    type: GET_TASKS_FAILURE,
    payload: error,
});

export const addTask = (task: Task, storageType: StorageType): AddTaskAction => ({
    type: ADD_TASK,
    payload: {
        task,
        storageType
    }
});

export const addTaskSuccess = (task: Task): AddTaskSuccessAction => ({
    type: ADD_TASK_SUCCESS,
    payload: task
});

export const addTaskFailure = (error: any): AddTaskFailureAction => ({
    type: ADD_TASK_FAILURE,
    payload: error
});

export const deleteTask = (taskID: string, storageType: StorageType): DeleteTaskAction => ({
    type: DELETE_TASK,
    payload: {
        taskID,
        storageType
    }
});

export const deleteTaskSuccess = (taskID: string): DeleteTaskSuccessAction => ({
    type: DELETE_TASK_SUCCESS,
    payload: taskID
});

export const deleteTaskFailure = (error: any): DeleteTaskFailureAction => ({
    type: DELETE_TASK_FAILURE,
    payload: error
});

export const updateTask = (task: Task, storageType: StorageType): UpdateTaskAction => ({
    type: UPDATE_TASK,
    payload:{
        task,
        storageType
    }
});

export const updateTaskSuccess = (task: Task): UpdateTaskSuccessAction => ({
    type: UPDATE_TASK_SUCCESS,
    payload: task
});

export const updateTaskFailure = (error: any): UpdateTaskFailureAction => ({
    type: UPDATE_TASK_FAILURE,
    payload: error
});

export const changeStatus = (taskID: string, storageType: StorageType): ChangeStatusAction => ({
    type: CHANGE_STATUS,
    payload: {
        taskID,
        storageType
    }
});

export const changeStatusSuccess = (taskID: string): ChangeStatusSuccessAction => ({
    type: CHANGE_STATUS_SUCCESS,
    payload: taskID
});

export const changeStatusFailure = (error: any): ChangeStatusFailureAction => ({
    type: CHANGE_STATUS_FAILURE,
    payload: error
});