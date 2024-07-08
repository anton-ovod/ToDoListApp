import { CategoriesActionTypes } from './app/features/categories/actions/actionInterfaces';
import { StatusesActionTypes } from './app/features/statuses/actions/actionInterfaces';
import { TaskActionTypes } from './app/features/tasks/actions/actionInterfaces';
import { Guid } from './misc/Guid';

export interface Task {
    taskID: Guid;
    title: string;
    dueDate?: Date;
    description?: string;
    taskCategoryID?: number;
    taskStatusID: number;
}

export interface Category{
    taskCategoryID: number;
    taskCategoryName: string;
    description?: string;
}

export interface Status {
    taskStatusID: number;
    taskStatusName: string;
    description?: string;
}

export type StorageType = "SQL" | "XML";

export type RootAction = TaskActionTypes | CategoriesActionTypes | StatusesActionTypes;