// taskEpics.js
import { Epic, ofType } from 'redux-observable';
import { ajax } from 'rxjs/ajax';
import { map, catchError, switchMap, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { from, of } from 'rxjs';
import { ADD_TASK, DELETE_TASK, UPDATE_TASK, CHANGE_STATUS, GET_TASKS } from '../actions/actionTypes';
import {
    addTaskSuccess, addTaskFailure,
    deleteTaskSuccess, deleteTaskFailure,
    updateTaskSuccess, updateTaskFailure,
    changeStatusSuccess, changeStatusFailure,
    getTasksSuccess,
    getTasksFailure
} from '../actions/taskActions';
import { Task, RootAction } from '../../../../types';
import { createRequest } from '../../../../misc/RequestCreator';
import { getTasksQuery, addTaskQuery, deleteTaskQuery, updateTaskQuery, changeStatusQuery } from '../../../../ApiQueries/taskQueries';


export const getTasksEpic: Epic<RootAction, RootAction> = (action$) =>
    action$.pipe(
        ofType(GET_TASKS),
        switchMap((action) =>
            from(
                ajax(createRequest(getTasksQuery, action.payload))
                    .pipe(
                        map((response: any) => getTasksSuccess((response.response.data.tasks as Task[]))),
                        catchError(error => of(getTasksFailure(error)))
                    )
            )
        )
    );

export const addTaskEpic: Epic<RootAction, RootAction> = (action$) =>
    action$.pipe(
        ofType(ADD_TASK),
        switchMap((action) =>
            from(
                ajax(createRequest(addTaskQuery(action.payload.task.title,
                                                action.payload.task.description,
                                                action.payload.task.taskCategoryID,
                                                action.payload.task.dueDate)
                                                ,action.payload.storageType))
                    .pipe(
                        map((response: any) => {
                            console.log(response.response.data.addTask);
                            return addTaskSuccess((action.payload.task as Task))
                        }),
                        catchError(response => of(addTaskFailure(response.response.data.addTask)))
                    )
            )
        )
    );

export const deleteTaskEpic: Epic<RootAction, RootAction> = (action$) =>
    action$.pipe(
        ofType(DELETE_TASK),
        switchMap((action) =>
            from(
                ajax(createRequest(deleteTaskQuery(action.payload.taskID), action.payload.storageType))
                    .pipe(
                        map((response: any) => {
                            console.log(response.response.data.deleteTask);
                            return deleteTaskSuccess(action.payload.taskID)
                        }),
                        catchError(response => of(deleteTaskFailure(response.response.data.deleteTask)))
                    )
            )
        )
    );

export const updateTaskEpic: Epic<RootAction, RootAction> = (action$) =>
    action$.pipe(
        ofType(UPDATE_TASK),
        switchMap((action) =>
            from(
                ajax(createRequest(updateTaskQuery(action.payload.task.taskID.toString(), 
                                                action.payload.task.title,
                                                action.payload.task.description,
                                                action.payload.task.dueDate,
                                                action.payload.task.taskCategoryID
                                                )
                                                ,action.payload.storageType))
                    .pipe(
                        map((response: any) => {
                            console.log(response.response.data.updateTask);
                            return updateTaskSuccess((action.payload.task as Task))
                        }),
                        catchError(response => of(updateTaskFailure(response.response.data.updateTask)))
                    )
            )
        )
    );

export const changeStatusEpic: Epic<RootAction, RootAction> = (action$) =>
    action$.pipe(
        ofType(CHANGE_STATUS),
        switchMap((action) =>
            from(
                ajax(createRequest(changeStatusQuery(action.payload.taskID), action.payload.storageType))
                    .pipe(
                        map((response: any) => {
                            console.log(response.response.data.changeStatus);
                            return changeStatusSuccess(action.payload.taskID);
                        }),
                        catchError(response => of(changeStatusFailure(response.response.data.changeStatus)))
                    )
            )
        )
    );